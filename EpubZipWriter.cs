using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace NovelpiaDownloader
{
    /// <summary>
    /// 面向 EPUB 的 ZIP 写入器。
    /// 规则：
    ///   - "mimetype" 条目必须是第一条、无扩展字段、STORED(method=0)（EPUB OCF 规范要求）。
    ///   - 其他条目按 <see cref="EnableCompression"/> 选择 Deflate 或 STORED。
    ///
    /// 该类是自包含的（不依赖 System.IO.Compression.ZipArchive 的条目元数据），
    /// 因此即便选 "NoCompression" 也能产生 method=0 的 Stored 条目，
    /// 不会出现 ZipArchive 那种 "CompressionLevel=NoCompression 但文件头仍写 method=8" 的行为。
    /// </summary>
    internal sealed class EpubZipWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly bool _leaveOpen;
        private readonly List<CdRecord> _central = new List<CdRecord>();
        private bool _mimetypeWritten;
        private bool _finished;
        private bool _disposed;

        /// <summary>除 mimetype 外，其他条目是否启用 Deflate 压缩。</summary>
        public bool EnableCompression { get; }

        private struct CdRecord
        {
            public byte[] NameBytes;
            public uint Crc32;
            public uint CompressedSize;
            public uint UncompressedSize;
            public uint LocalHeaderOffset;
            public ushort DosTime;
            public ushort DosDate;
            public ushort Method;
            public bool Utf8;
        }

        public EpubZipWriter(Stream stream, bool enableCompression = false, bool leaveOpen = false)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanWrite) throw new ArgumentException("stream must be writable", nameof(stream));
            _stream = stream;
            _leaveOpen = leaveOpen;
            EnableCompression = enableCompression;
        }

        public void AddEntry(string name, byte[] data, DateTimeOffset lastWrite)
        {
            if (_finished) throw new InvalidOperationException("writer already finished");
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (data == null) data = EmptyBytes;

            name = name.Replace('\\', '/');
            bool isMimetype = string.Equals(name, "mimetype", StringComparison.Ordinal);

            if (isMimetype)
            {
                if (_mimetypeWritten)
                    throw new InvalidOperationException("mimetype already written");
                if (_central.Count != 0)
                    throw new InvalidOperationException("mimetype must be the first entry");
            }

            bool stored = isMimetype || !EnableCompression;
            WriteEntry(name, data, lastWrite, stored);
            if (isMimetype) _mimetypeWritten = true;
        }

        public void AddEntry(string name, string text, DateTimeOffset lastWrite)
        {
            AddEntry(name, Encoding.UTF8.GetBytes(text ?? string.Empty), lastWrite);
        }

        private void WriteEntry(string name, byte[] data, DateTimeOffset lastWrite, bool stored)
        {
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            bool utf8 = ContainsNonAscii(nameBytes);

            uint crc = Crc32.Compute(data);
            uint uncompressed = checked((uint)data.Length);

            byte[] payload;
            ushort method;
            if (stored || data.Length == 0)
            {
                payload = data;
                method = 0;
            }
            else
            {
                payload = DeflateRaw(data);
                method = 8;
                if (payload.Length >= data.Length)
                {
                    payload = data;
                    method = 0;
                }
            }
            uint compressed = checked((uint)payload.Length);

            ushort dosTime, dosDate;
            ToDosDateTime(lastWrite.LocalDateTime, out dosDate, out dosTime);

            uint localOffset = checked((uint)_stream.Position);

            WriteUInt32(0x04034b50);
            WriteUInt16(20);
            WriteUInt16((ushort)(utf8 ? 0x0800 : 0x0000));
            WriteUInt16(method);
            WriteUInt16(dosTime);
            WriteUInt16(dosDate);
            WriteUInt32(crc);
            WriteUInt32(compressed);
            WriteUInt32(uncompressed);
            WriteUInt16((ushort)nameBytes.Length);
            WriteUInt16(0);
            _stream.Write(nameBytes, 0, nameBytes.Length);
            if (compressed > 0) _stream.Write(payload, 0, payload.Length);

            _central.Add(new CdRecord
            {
                NameBytes = nameBytes,
                Crc32 = crc,
                CompressedSize = compressed,
                UncompressedSize = uncompressed,
                LocalHeaderOffset = localOffset,
                DosTime = dosTime,
                DosDate = dosDate,
                Method = method,
                Utf8 = utf8,
            });
        }

        public void Finish()
        {
            if (_finished) return;
            _finished = true;

            uint cdStart = checked((uint)_stream.Position);

            foreach (var r in _central)
            {
                WriteUInt32(0x02014b50);
                WriteUInt16(20);
                WriteUInt16(20);
                WriteUInt16((ushort)(r.Utf8 ? 0x0800 : 0x0000));
                WriteUInt16(r.Method);
                WriteUInt16(r.DosTime);
                WriteUInt16(r.DosDate);
                WriteUInt32(r.Crc32);
                WriteUInt32(r.CompressedSize);
                WriteUInt32(r.UncompressedSize);
                WriteUInt16((ushort)r.NameBytes.Length);
                WriteUInt16(0);
                WriteUInt16(0);
                WriteUInt16(0);
                WriteUInt16(0);
                WriteUInt32(0);
                WriteUInt32(r.LocalHeaderOffset);
                _stream.Write(r.NameBytes, 0, r.NameBytes.Length);
            }

            uint cdEnd = checked((uint)_stream.Position);
            uint cdSize = cdEnd - cdStart;

            WriteUInt32(0x06054b50);
            WriteUInt16(0);
            WriteUInt16(0);
            WriteUInt16((ushort)_central.Count);
            WriteUInt16((ushort)_central.Count);
            WriteUInt32(cdSize);
            WriteUInt32(cdStart);
            WriteUInt16(0);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try { Finish(); }
            finally
            {
                if (!_leaveOpen) _stream.Dispose();
            }
        }

        private static byte[] DeflateRaw(byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                using (var ds = new DeflateStream(ms, CompressionLevel.Optimal, leaveOpen: true))
                {
                    ds.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        private void WriteUInt16(ushort v)
        {
            _stream.WriteByte((byte)(v & 0xFF));
            _stream.WriteByte((byte)((v >> 8) & 0xFF));
        }

        private void WriteUInt32(uint v)
        {
            _stream.WriteByte((byte)(v & 0xFF));
            _stream.WriteByte((byte)((v >> 8) & 0xFF));
            _stream.WriteByte((byte)((v >> 16) & 0xFF));
            _stream.WriteByte((byte)((v >> 24) & 0xFF));
        }

        private static bool ContainsNonAscii(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
                if (b[i] >= 0x80) return true;
            return false;
        }

        private static readonly byte[] EmptyBytes = new byte[0];

        private static void ToDosDateTime(DateTime dt, out ushort dosDate, out ushort dosTime)
        {
            if (dt.Year < 1980) dt = new DateTime(1980, 1, 1, 0, 0, 0);
            if (dt.Year > 2107) dt = new DateTime(2107, 12, 31, 23, 59, 58);

            dosDate = (ushort)(((dt.Year - 1980) << 9) | (dt.Month << 5) | dt.Day);
            dosTime = (ushort)((dt.Hour << 11) | (dt.Minute << 5) | (dt.Second / 2));
        }
    }

    internal static class Crc32
    {
        private static readonly uint[] Table = BuildTable();

        private static uint[] BuildTable()
        {
            const uint poly = 0xEDB88320u;
            var t = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint c = i;
                for (int j = 0; j < 8; j++)
                    c = (c & 1) != 0 ? (poly ^ (c >> 1)) : (c >> 1);
                t[i] = c;
            }
            return t;
        }

        public static uint Compute(byte[] data)
        {
            uint c = 0xFFFFFFFFu;
            for (int i = 0; i < data.Length; i++)
                c = Table[(c ^ data[i]) & 0xFF] ^ (c >> 8);
            return c ^ 0xFFFFFFFFu;
        }
    }
}
