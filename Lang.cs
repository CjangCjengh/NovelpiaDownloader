using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NovelpiaDownloader
{
    public static class Lang
    {
        public const string Ko = "ko";
        public const string En = "en";
        public const string ZhCn = "zh-CN";
        public const string ZhTw = "zh-TW";
        public const string Ja = "ja";
        public const string Vi = "vi";
        public const string Th = "th";
        public const string Id = "id";

        public static readonly string[] Codes = { Ko, En, ZhCn, ZhTw, Ja, Vi, Th, Id };
        public static readonly string[] DisplayNames = { "한국어", "English", "简体中文", "繁體中文", "日本語", "Tiếng Việt", "ภาษาไทย", "Bahasa Indonesia" };

        public static string Current = Ko;

        static readonly Dictionary<string, Dictionary<string, string>> texts = new Dictionary<string, Dictionary<string, string>>
        {
            { "login_ok", new Dictionary<string, string> {
                { Ko, "로그인 성공!\r\n" },
                { En, "Logged in!\r\n" },
                { ZhCn, "登录成功!\r\n" },
                { ZhTw, "登入成功!\r\n" },
                { Ja, "ログインしました!\r\n" },
                { Vi, "Đăng nhập thành công!\r\n" },
                { Th, "เข้าสู่ระบบสำเร็จ!\r\n" },
                { Id, "Berhasil login!\r\n" } } },
            { "login_fail", new Dictionary<string, string> {
                { Ko, "로그인 실패!\r\n" },
                { En, "Login failed.\r\n" },
                { ZhCn, "登录失败。\r\n" },
                { ZhTw, "登入失敗。\r\n" },
                { Ja, "ログインに失敗しました。\r\n" },
                { Vi, "Đăng nhập thất bại.\r\n" },
                { Th, "เข้าสู่ระบบไม่สำเร็จ\r\n" },
                { Id, "Login gagal.\r\n" } } },
            { "download_start", new Dictionary<string, string> {
                { Ko, "다운로드 시작!\r\n" },
                { En, "Download started.\r\n" },
                { ZhCn, "开始下载。\r\n" },
                { ZhTw, "開始下載。\r\n" },
                { Ja, "ダウンロード開始。\r\n" },
                { Vi, "Bắt đầu tải xuống.\r\n" },
                { Th, "เริ่มดาวน์โหลด\r\n" },
                { Id, "Mulai mengunduh.\r\n" } } },
            { "download_done", new Dictionary<string, string> {
                { Ko, "다운로드 완료!\r\n" },
                { En, "Download finished.\r\n" },
                { ZhCn, "下载完成。\r\n" },
                { ZhTw, "下載完成。\r\n" },
                { Ja, "ダウンロード完了。\r\n" },
                { Vi, "Hoàn tất tải xuống.\r\n" },
                { Th, "ดาวน์โหลดเสร็จสิ้น\r\n" },
                { Id, "Unduhan selesai.\r\n" } } },
            { "cover", new Dictionary<string, string> {
                { Ko, "표지" },
                { En, "Cover" },
                { ZhCn, "封面" },
                { ZhTw, "封面" },
                { Ja, "表紙" },
                { Vi, "Bìa" },
                { Th, "ปก" },
                { Id, "Sampul" } } },
            { "illustration", new Dictionary<string, string> {
                { Ko, "삽화" },
                { En, "Illustration" },
                { ZhCn, "插图" },
                { ZhTw, "插圖" },
                { Ja, "挿絵" },
                { Vi, "Tranh minh họa" },
                { Th, "ภาพประกอบ" },
                { Id, "Ilustrasi" } } },
            { "img_start", new Dictionary<string, string> {
                { Ko, "{0} 다운로드 시작\r\n{1}\r\n" },
                { En, "Downloading {0}\r\n{1}\r\n" },
                { ZhCn, "开始下载{0}\r\n{1}\r\n" },
                { ZhTw, "開始下載{0}\r\n{1}\r\n" },
                { Ja, "{0}をダウンロード中\r\n{1}\r\n" },
                { Vi, "Đang tải {0}\r\n{1}\r\n" },
                { Th, "กำลังดาวน์โหลด{0}\r\n{1}\r\n" },
                { Id, "Mengunduh {0}\r\n{1}\r\n" } } },
            { "img_done", new Dictionary<string, string> {
                { Ko, "{0} 다운로드 완료\r\n" },
                { En, "{0} downloaded\r\n" },
                { ZhCn, "{0}下载完成\r\n" },
                { ZhTw, "{0}下載完成\r\n" },
                { Ja, "{0}のダウンロード完了\r\n" },
                { Vi, "Đã tải {0}\r\n" },
                { Th, "ดาวน์โหลด{0}เสร็จแล้ว\r\n" },
                { Id, "{0} terunduh\r\n" } } },
            { "img_fail", new Dictionary<string, string> {
                { Ko, "{0} 다운로드 실패\r\n{1}\r\n" },
                { En, "{0} download failed\r\n{1}\r\n" },
                { ZhCn, "{0}下载失败\r\n{1}\r\n" },
                { ZhTw, "{0}下載失敗\r\n{1}\r\n" },
                { Ja, "{0}のダウンロード失敗\r\n{1}\r\n" },
                { Vi, "Tải {0} thất bại\r\n{1}\r\n" },
                { Th, "ดาวน์โหลด{0}ไม่สำเร็จ\r\n{1}\r\n" },
                { Id, "Gagal mengunduh {0}\r\n{1}\r\n" } } },
            { "notice", new Dictionary<string, string> {
                { Ko, "공지" },
                { En, "Notice" },
                { ZhCn, "公告" },
                { ZhTw, "公告" },
                { Ja, "お知らせ" },
                { Vi, "Thông báo" },
                { Th, "ประกาศ" },
                { Id, "Pengumuman" } } },
            { "folder_select", new Dictionary<string, string> {
                { Ko, "저장 폴더를 선택하세요" },
                { En, "Select output folder" },
                { ZhCn, "选择保存文件夹" },
                { ZhTw, "選擇儲存資料夾" },
                { Ja, "保存フォルダを選択" },
                { Vi, "Chọn thư mục lưu" },
                { Th, "เลือกโฟลเดอร์บันทึก" },
                { Id, "Pilih folder penyimpanan" } } },
            { "chapter_ok", new Dictionary<string, string> {
                { Ko, "  ✓ EP.{0,-4} {1}\r\n" },
                { En, "  OK   EP.{0,-4} {1}\r\n" },
                { ZhCn, "  ✓ EP.{0,-4} {1}\r\n" },
                { ZhTw, "  ✓ EP.{0,-4} {1}\r\n" },
                { Ja, "  ✓ EP.{0,-4} {1}\r\n" },
                { Vi, "  OK   EP.{0,-4} {1}\r\n" },
                { Th, "  OK   EP.{0,-4} {1}\r\n" },
                { Id, "  OK   EP.{0,-4} {1}\r\n" } } },
            { "chapter_fail", new Dictionary<string, string> {
                { Ko, "  ✗ EP.{0,-4} {1}  실패\r\n" },
                { En, "  FAIL EP.{0,-4} {1}\r\n" },
                { ZhCn, "  ✗ EP.{0,-4} {1}  失败\r\n" },
                { ZhTw, "  ✗ EP.{0,-4} {1}  失敗\r\n" },
                { Ja, "  ✗ EP.{0,-4} {1}  失敗\r\n" },
                { Vi, "  FAIL EP.{0,-4} {1}\r\n" },
                { Th, "  FAIL EP.{0,-4} {1}\r\n" },
                { Id, "  FAIL EP.{0,-4} {1}\r\n" } } },
            { "chapter_retry", new Dictionary<string, string> {
                { Ko, "    ↻ 재시도 {0}/{1}\r\n" },
                { En, "    retry {0}/{1}\r\n" },
                { ZhCn, "    ↻ 重试 {0}/{1}\r\n" },
                { ZhTw, "    ↻ 重試 {0}/{1}\r\n" },
                { Ja, "    ↻ 再試行 {0}/{1}\r\n" },
                { Vi, "    thử lại {0}/{1}\r\n" },
                { Th, "    ลองใหม่ {0}/{1}\r\n" },
                { Id, "    coba ulang {0}/{1}\r\n" } } },
            { "notice_ok", new Dictionary<string, string> {
                { Ko, "  ✓ [공지] {0}\r\n" },
                { En, "  OK   [Notice] {0}\r\n" },
                { ZhCn, "  ✓ [公告] {0}\r\n" },
                { ZhTw, "  ✓ [公告] {0}\r\n" },
                { Ja, "  ✓ [お知らせ] {0}\r\n" },
                { Vi, "  OK   [Thông báo] {0}\r\n" },
                { Th, "  OK   [ประกาศ] {0}\r\n" },
                { Id, "  OK   [Pengumuman] {0}\r\n" } } },
            { "notice_fail", new Dictionary<string, string> {
                { Ko, "  ✗ [공지] {0}  실패\r\n" },
                { En, "  FAIL [Notice] {0}\r\n" },
                { ZhCn, "  ✗ [公告] {0}  失败\r\n" },
                { ZhTw, "  ✗ [公告] {0}  失敗\r\n" },
                { Ja, "  ✗ [お知らせ] {0}  失敗\r\n" },
                { Vi, "  FAIL [Thông báo] {0}\r\n" },
                { Th, "  FAIL [ประกาศ] {0}\r\n" },
                { Id, "  FAIL [Pengumuman] {0}\r\n" } } },
            { "sep", new Dictionary<string, string> {
                { Ko, "--------------------------------\r\n" },
                { En, "--------------------------------\r\n" },
                { ZhCn, "--------------------------------\r\n" },
                { ZhTw, "--------------------------------\r\n" },
                { Ja, "--------------------------------\r\n" },
                { Vi, "--------------------------------\r\n" },
                { Th, "--------------------------------\r\n" },
                { Id, "--------------------------------\r\n" } } },
            { "compress", new Dictionary<string, string> {
                { Ko, "EPUB 압축" },
                { En, "Compress EPUB" },
                { ZhCn, "EPUB 压缩" },
                { ZhTw, "EPUB 壓縮" },
                { Ja, "EPUB 圧縮" },
                { Vi, "Nén EPUB" },
                { Th, "บีบอัด EPUB" },
                { Id, "Kompres EPUB" } } },
            { "download_image", new Dictionary<string, string> {
                { Ko, "이미지 다운로드" },
                { En, "Download images" },
                { ZhCn, "下载图片" },
                { ZhTw, "下載圖片" },
                { Ja, "画像をダウンロード" },
                { Vi, "Tải ảnh" },
                { Th, "ดาวน์โหลดรูป" },
                { Id, "Unduh gambar" } } },
            { "btn_download", new Dictionary<string, string> {
                { Ko, "다운로드" },
                { En, "Start" },
                { ZhCn, "下载" },
                { ZhTw, "下載" },
                { Ja, "開始" },
                { Vi, "Tải" },
                { Th, "เริ่ม" },
                { Id, "Mulai" } } },
            { "btn_stop", new Dictionary<string, string> {
                { Ko, "중지" },
                { En, "Stop" },
                { ZhCn, "停止" },
                { ZhTw, "停止" },
                { Ja, "停止" },
                { Vi, "Dừng" },
                { Th, "หยุด" },
                { Id, "Berhenti" } } },
            { "btn_stopping", new Dictionary<string, string> {
                { Ko, "중지 중..." },
                { En, "Stopping..." },
                { ZhCn, "停止中..." },
                { ZhTw, "停止中..." },
                { Ja, "停止中..." },
                { Vi, "Đang dừng..." },
                { Th, "กำลังหยุด..." },
                { Id, "Menghentikan..." } } },
            { "cancelled", new Dictionary<string, string> {
                { Ko, "사용자 중지! 지금까지의 진행으로 조립\r\n" },
                { En, "Stopped by user, packaging current progress\r\n" },
                { ZhCn, "用户已停止,按当前进度组装\r\n" },
                { ZhTw, "使用者已停止,按目前進度組裝\r\n" },
                { Ja, "ユーザー停止。現在の進捗でパッケージ化します\r\n" },
                { Vi, "Đã dừng theo yêu cầu, đóng gói phần đã tải\r\n" },
                { Th, "ผู้ใช้หยุดเอง กำลังประกอบจากที่ดาวน์โหลดแล้ว\r\n" },
                { Id, "Dihentikan pengguna, mengemas progres saat ini\r\n" } } },
            { "stop_on_error", new Dictionary<string, string> {
                { Ko, "오류 발생으로 중단! 지금까지의 진행으로 조립\r\n" },
                { En, "Stopping on error, packaging current progress\r\n" },
                { ZhCn, "出错已停止,按当前进度组装\r\n" },
                { ZhTw, "出錯已停止,按目前進度組裝\r\n" },
                { Ja, "エラーで停止。現在の進捗でパッケージ化します\r\n" },
                { Vi, "Dừng do lỗi, đóng gói phần đã tải\r\n" },
                { Th, "หยุดเพราะข้อผิดพลาด กำลังประกอบจากที่ดาวน์โหลดแล้ว\r\n" },
                { Id, "Berhenti karena error, mengemas progres saat ini\r\n" } } },

            // 控件文本(供 Apply 使用)
            { "ui_login_group", new Dictionary<string, string> {
                { Ko, "로그인" }, { En, "Login" }, { ZhCn, "登录" }, { ZhTw, "登入" },
                { Ja, "ログイン" }, { Vi, "Đăng nhập" }, { Th, "เข้าสู่ระบบ" }, { Id, "Login" } } },
            { "ui_login_btn", new Dictionary<string, string> {
                { Ko, "로그인" }, { En, "Login" }, { ZhCn, "登录" }, { ZhTw, "登入" },
                { Ja, "ログイン" }, { Vi, "Đăng nhập" }, { Th, "เข้าสู่ระบบ" }, { Id, "Login" } } },
            { "ui_email", new Dictionary<string, string> {
                { Ko, "이메일" }, { En, "Email" }, { ZhCn, "邮箱" }, { ZhTw, "信箱" },
                { Ja, "メール" }, { Vi, "Email" }, { Th, "อีเมล" }, { Id, "Email" } } },
            { "ui_password", new Dictionary<string, string> {
                { Ko, "비밀번호" }, { En, "Password" }, { ZhCn, "密码" }, { ZhTw, "密碼" },
                { Ja, "パスワード" }, { Vi, "Mật khẩu" }, { Th, "รหัสผ่าน" }, { Id, "Sandi" } } },
            { "ui_download_group", new Dictionary<string, string> {
                { Ko, "다운로드" }, { En, "Download" }, { ZhCn, "下载" }, { ZhTw, "下載" },
                { Ja, "ダウンロード" }, { Vi, "Tải xuống" }, { Th, "ดาวน์โหลด" }, { Id, "Unduh" } } },
            { "ui_novel_no", new Dictionary<string, string> {
                { Ko, "소설 번호" }, { En, "Novel ID" }, { ZhCn, "小说编号" }, { ZhTw, "小說編號" },
                { Ja, "小説番号" }, { Vi, "ID truyện" }, { Th, "รหัสนิยาย" }, { Id, "ID novel" } } },
            { "ui_extension", new Dictionary<string, string> {
                { Ko, "형식" }, { En, "Format" }, { ZhCn, "格式" }, { ZhTw, "格式" },
                { Ja, "形式" }, { Vi, "Định dạng" }, { Th, "รูปแบบ" }, { Id, "Format" } } },
            { "ui_from_ep", new Dictionary<string, string> {
                { Ko, "화부터" }, { En, "From Ep." }, { ZhCn, "起始话" }, { ZhTw, "起始話" },
                { Ja, "開始話" }, { Vi, "Từ tập" }, { Th, "จากตอน" }, { Id, "Dari Ep." } } },
            { "ui_to_ep", new Dictionary<string, string> {
                { Ko, "화까지" }, { En, "To Ep." }, { ZhCn, "结束话" }, { ZhTw, "結束話" },
                { Ja, "終了話" }, { Vi, "Đến tập" }, { Th, "ถึงตอน" }, { Id, "Sampai Ep." } } },
            { "ui_char_map", new Dictionary<string, string> {
                { Ko, "글자 치환" }, { En, "Char map" }, { ZhCn, "字符映射" }, { ZhTw, "字元對應" },
                { Ja, "文字置換" }, { Vi, "Ánh xạ ký tự" }, { Th, "แมปอักขระ" }, { Id, "Peta karakter" } } },
            { "ui_open", new Dictionary<string, string> {
                { Ko, "열기" }, { En, "Open" }, { ZhCn, "打开" }, { ZhTw, "開啟" },
                { Ja, "開く" }, { Vi, "Mở" }, { Th, "เปิด" }, { Id, "Buka" } } },
            { "ui_notice_check", new Dictionary<string, string> {
                { Ko, "공지 포함" }, { En, "Include notices" }, { ZhCn, "包含公告" }, { ZhTw, "包含公告" },
                { Ja, "お知らせを含む" }, { Vi, "Gồm thông báo" }, { Th, "รวมประกาศ" }, { Id, "Sertakan pengumuman" } } },
            { "ui_save_to", new Dictionary<string, string> {
                { Ko, "저장 경로" }, { En, "Save to" }, { ZhCn, "保存路径" }, { ZhTw, "儲存路徑" },
                { Ja, "保存先" }, { Vi, "Lưu vào" }, { Th, "บันทึกไปที่" }, { Id, "Simpan ke" } } },
            { "ui_browse", new Dictionary<string, string> {
                { Ko, "선택" }, { En, "Browse" }, { ZhCn, "选择" }, { ZhTw, "選擇" },
                { Ja, "選択" }, { Vi, "Chọn" }, { Th, "เลือก" }, { Id, "Pilih" } } },
            { "ui_strip_blanks", new Dictionary<string, string> {
                { Ko, "빈 줄 제거" }, { En, "Strip blanks" }, { ZhCn, "去除空行" }, { ZhTw, "去除空行" },
                { Ja, "空行を除去" }, { Vi, "Bỏ dòng trống" }, { Th, "ลบบรรทัดว่าง" }, { Id, "Hapus baris kosong" } } },
            { "ui_keep_html", new Dictionary<string, string> {
                { Ko, "HTML 유지" }, { En, "Keep HTML" }, { ZhCn, "保留HTML" }, { ZhTw, "保留HTML" },
                { Ja, "HTMLを保持" }, { Vi, "Giữ HTML" }, { Th, "เก็บ HTML" }, { Id, "Simpan HTML" } } },
            { "ui_retry", new Dictionary<string, string> {
                { Ko, "재시도" }, { En, "Retry" }, { ZhCn, "重试" }, { ZhTw, "重試" },
                { Ja, "再試行" }, { Vi, "Thử lại" }, { Th, "ลองใหม่" }, { Id, "Coba ulang" } } },
            { "ui_threads", new Dictionary<string, string> {
                { Ko, "스레드" }, { En, "threads" }, { ZhCn, "线程" }, { ZhTw, "執行緒" },
                { Ja, "スレッド" }, { Vi, "luồng" }, { Th, "เธรด" }, { Id, "utas" } } },
            { "ui_interval", new Dictionary<string, string> {
                { Ko, "간격" }, { En, "" }, { ZhCn, "间隔" }, { ZhTw, "間隔" },
                { Ja, "間隔" }, { Vi, "" }, { Th, "" }, { Id, "" } } },
            { "ui_second", new Dictionary<string, string> {
                { Ko, "초" }, { En, "sec" }, { ZhCn, "秒" }, { ZhTw, "秒" },
                { Ja, "秒" }, { Vi, "giây" }, { Th, "วิ" }, { Id, "dtk" } } },
            { "ui_stop_on_error", new Dictionary<string, string> {
                { Ko, "오류 시 중단" }, { En, "Stop on error" }, { ZhCn, "出错时中断" }, { ZhTw, "出錯時中斷" },
                { Ja, "エラー時中断" }, { Vi, "Dừng khi lỗi" }, { Th, "หยุดเมื่อพลาด" }, { Id, "Berhenti saat error" } } },
        };

        public static string T(string key)
        {
            if (texts.TryGetValue(key, out var d) && d.TryGetValue(Current, out var s))
                return s;
            // 缺失时回退到英文 / 韩文
            if (texts.TryGetValue(key, out d))
            {
                if (d.TryGetValue(En, out s)) return s;
                if (d.TryGetValue(Ko, out s)) return s;
            }
            return key;
        }

        public static string T(string key, params object[] args)
        {
            return string.Format(T(key), args);
        }

        // 各语言对应的窗体字体
        static string FontFor(string lang)
        {
            switch (lang)
            {
                case Ko: return "Malgun Gothic";
                case Ja: return "Yu Gothic UI";
                case ZhCn: return "Microsoft YaHei UI";
                case ZhTw: return "Microsoft JhengHei UI";
                case Th: return "Leelawadee UI";
                default: return "Segoe UI";
            }
        }

        // 测量文本实际像素宽度,使按钮宽度自适应
        static int MeasureWidth(Control c, string text, int padding)
        {
            if (string.IsNullOrEmpty(text)) return padding;
            var sz = TextRenderer.MeasureText(text, c.Font);
            return sz.Width + padding;
        }

        public static void Apply(MainWin f, string lang)
        {
            // 校验语言代码
            bool known = false;
            foreach (var c in Codes) if (c == lang) { known = true; break; }
            Current = known ? lang : Ko;

            // 切换字体(整个窗体 + 控制台共用同一字体)
            string uiFontName = FontFor(Current);
            float uiSize = f.Font.Size;
            var newFont = new Font(uiFontName, uiSize, FontStyle.Regular, GraphicsUnit.Point);
            f.Font = newFont;
            f.ConsoleBox.Font = newFont;

            // 文案
            f.LoginGroup.Text = T("ui_login_group");
            f.LoginButton1.Text = T("ui_login_btn");
            f.LoginButton2.Text = T("ui_login_btn");
            f.EmailLabel.Text = T("ui_email");
            f.PasswordLabel.Text = T("ui_password");

            f.DownloadGroup.Text = T("ui_download_group");
            f.NovelNoLable.Text = T("ui_novel_no");
            f.ExtensionLabel.Text = T("ui_extension");
            f.DownloadButton.Text = T("btn_download");

            f.FontLabel.Text = T("ui_char_map");
            f.FontButton.Text = T("ui_open");

            f.NoticeCheck.Text = T("ui_notice_check");
            f.OutputDirLabel.Text = T("ui_save_to");
            f.OutputDirButton.Text = T("ui_browse");

            f.RemoveBlankCheck.Text = T("ui_strip_blanks");
            f.KeepHtmlCheck.Text = T("ui_keep_html");
            f.RetryLabel.Text = T("ui_retry");
            f.CompressCheck.Text = T("compress");
            f.DownloadImageCheck.Text = T("download_image");
            f.StopOnErrorCheck.Text = T("ui_stop_on_error");

            f.ThreadLabel.Text = T("ui_threads");
            f.IntervalLabel.Text = T("ui_interval");
            f.SecondLabel.Text = T("ui_second");

            // ===== 布局 =====
            // DownloadGroup 内自上而下:
            // (1) 章节范围: y=30
            // (2) 公告/去空行/保留HTML/EPUB压缩/下载图片/出错中止/重试 — 4 行勾选块: y=68/97/126/155
            // (3) 保存路径(与浏览按钮): y=190
            // (4) 小说编号: y=232
            // (5) 格式 + 下载按钮: y=270

            // 章节范围: 统一布局(KeepCheck 自带文字,无独立 Label)
            f.FromCheck.AutoSize = true;
            f.FromCheck.Text = T("ui_from_ep");
            f.FromCheck.Location = new Point(15, 33);
            int fromCheckRight = f.FromCheck.Location.X + MeasureWidth(f.FromCheck, f.FromCheck.Text, 24);
            f.FromNum.Location = new Point(fromCheckRight + 4, 30);
            f.FromNum.Size = new Size(70, 31);
            f.FromLabel.Visible = false;

            f.ToCheck.AutoSize = true;
            f.ToCheck.Text = T("ui_to_ep");
            f.ToCheck.Location = new Point(220, 33);
            int toCheckRight = f.ToCheck.Location.X + MeasureWidth(f.ToCheck, f.ToCheck.Text, 24);
            f.ToNum.Location = new Point(toCheckRight + 4, 30);
            f.ToNum.Size = new Size(70, 31);
            f.ToLabel.Visible = false;

            // 勾选框块: 2 列 × 4 行, 行距 29
            f.NoticeCheck.AutoSize = true;
            f.RemoveBlankCheck.AutoSize = true;
            f.KeepHtmlCheck.AutoSize = true;
            f.CompressCheck.AutoSize = true;
            f.DownloadImageCheck.AutoSize = true;
            f.StopOnErrorCheck.AutoSize = true;

            f.NoticeCheck.Location = new Point(15, 68);
            f.RemoveBlankCheck.Location = new Point(220, 68);
            f.KeepHtmlCheck.Location = new Point(15, 97);
            f.CompressCheck.Location = new Point(220, 97);
            f.DownloadImageCheck.Location = new Point(15, 126);
            f.StopOnErrorCheck.Location = new Point(220, 126);

            // 第 4 行:重试次数
            f.RetryLabel.Location = new Point(15, 158);
            int retryLabelRight = f.RetryLabel.Location.X + MeasureWidth(f.RetryLabel, f.RetryLabel.Text, 8);
            f.RetryNum.Location = new Point(retryLabelRight + 4, 155);
            f.RetryNum.Size = new Size(70, 31);

            // 保存路径行: y=190
            f.OutputDirLabel.Location = new Point(15, 193);
            int saveLabelRight = f.OutputDirLabel.Location.X + MeasureWidth(f.OutputDirLabel, f.OutputDirLabel.Text, 8);
            int browseBtnW = MeasureWidth(f.OutputDirButton, f.OutputDirButton.Text, 26);
            if (browseBtnW < 70) browseBtnW = 70;
            int outputRightEdge = f.DownloadGroup.ClientSize.Width - 12;
            f.OutputDirButton.Size = new Size(browseBtnW, 32);
            f.OutputDirButton.Location = new Point(outputRightEdge - browseBtnW, 190);
            f.OutputDirText.Location = new Point(saveLabelRight + 4, 190);
            f.OutputDirText.Size = new Size(f.OutputDirButton.Location.X - 6 - f.OutputDirText.Location.X, 31);

            // 小说编号: y=232
            f.NovelNoLable.Location = new Point(15, 235);
            int novelLabelRight = f.NovelNoLable.Location.X + MeasureWidth(f.NovelNoLable, f.NovelNoLable.Text, 8);
            f.NovelNoText.Location = new Point(novelLabelRight + 4, 232);
            f.NovelNoText.Size = new Size(outputRightEdge - f.NovelNoText.Location.X, 31);

            // 格式 + 下载按钮: y=270 / y=265
            f.ExtensionLabel.Location = new Point(15, 273);
            int extLabelRight = f.ExtensionLabel.Location.X + MeasureWidth(f.ExtensionLabel, f.ExtensionLabel.Text, 8);
            f.EpubButton.Location = new Point(extLabelRight + 4, 270);
            int epubRight = f.EpubButton.Location.X + MeasureWidth(f.EpubButton, f.EpubButton.Text, 28);
            f.TxtButton.Location = new Point(epubRight + 8, 270);

            int dlBtnW = MeasureWidth(f.DownloadButton, f.DownloadButton.Text, 28);
            if (dlBtnW < 80) dlBtnW = 80;
            f.DownloadButton.Size = new Size(dlBtnW, 36);
            f.DownloadButton.Location = new Point(outputRightEdge - dlBtnW, 268);

            // 下载组高度自适应
            f.DownloadGroup.Size = new Size(f.DownloadGroup.Size.Width, 318);

            // LoginGroup 内: Email/Password 标签宽度根据文本测量
            int emailLabW = MeasureWidth(f.EmailLabel, f.EmailLabel.Text, 0);
            int pwdLabW = MeasureWidth(f.PasswordLabel, f.PasswordLabel.Text, 0);
            int keyLabW = MeasureWidth(f.LoginkeyLabel, f.LoginkeyLabel.Text, 0);
            int loginLabMax = System.Math.Max(System.Math.Max(emailLabW, pwdLabW), keyLabW);
            int loginInputX = 12 + loginLabMax + 8;
            int loginGroupRight = f.LoginGroup.ClientSize.Width - 12;
            int loginBtnW = MeasureWidth(f.LoginButton1, f.LoginButton1.Text, 26);
            if (loginBtnW < 75) loginBtnW = 75;
            f.LoginButton1.Size = new Size(loginBtnW, 68);
            f.LoginButton2.Size = new Size(loginBtnW, 36);
            f.LoginButton1.Location = new Point(loginGroupRight - loginBtnW, 30);
            f.LoginButton2.Location = new Point(loginGroupRight - loginBtnW, 104);
            int loginInputW = f.LoginButton1.Location.X - 8 - loginInputX;

            f.EmailLabel.Location = new Point(12, 33);
            f.EmailText.Location = new Point(loginInputX, 30);
            f.EmailText.Size = new Size(loginInputW, 31);
            f.PasswordLabel.Location = new Point(12, 70);
            f.PasswordText.Location = new Point(loginInputX, 67);
            f.PasswordText.Size = new Size(loginInputW, 31);
            f.LoginkeyLabel.Location = new Point(12, 110);
            f.LoginkeyText.Location = new Point(loginInputX, 107);
            f.LoginkeyText.Size = new Size(loginInputW, 31);

            // FontLabel/FontBox/FontButton 自适应
            int fontLabRight = 19 + MeasureWidth(f.FontLabel, f.FontLabel.Text, 8);
            int fontBtnW = MeasureWidth(f.FontButton, f.FontButton.Text, 26);
            if (fontBtnW < 70) fontBtnW = 70;
            f.FontButton.Size = new Size(fontBtnW, 36);
            int formClient = f.ClientSize.Width;
            int fontRightEdge = f.DownloadGroup.Location.X + f.DownloadGroup.Size.Width - 12;
            f.FontButton.Location = new Point(fontRightEdge - fontBtnW, f.FontLabel.Location.Y - 6);
            f.FontBox.Location = new Point(fontLabRight + 4, f.FontLabel.Location.Y - 3);
            f.FontBox.Size = new Size(f.FontButton.Location.X - 6 - f.FontBox.Location.X, 31);

            // 线程数 / 间隔 行
            f.ThreadLabel.Location = new Point(18, 223);
            int thrLabRight = 18 + MeasureWidth(f.ThreadLabel, f.ThreadLabel.Text, 8);
            f.ThreadNum.Location = new Point(thrLabRight + 4, 220);
            f.ThreadNum.Size = new Size(70, 31);
            int thrNumRight = f.ThreadNum.Location.X + f.ThreadNum.Size.Width;

            bool showInterval = !string.IsNullOrEmpty(f.IntervalLabel.Text);
            int intervalLabRight;
            if (showInterval)
            {
                f.IntervalLabel.Visible = true;
                f.IntervalLabel.Location = new Point(thrNumRight + 16, 223);
                intervalLabRight = f.IntervalLabel.Location.X + MeasureWidth(f.IntervalLabel, f.IntervalLabel.Text, 8);
            }
            else
            {
                f.IntervalLabel.Visible = false;
                intervalLabRight = thrNumRight + 16;
            }
            f.IntervalNum.Location = new Point(intervalLabRight + 4, 220);
            f.IntervalNum.Size = new Size(70, 31);
            f.SecondLabel.Location = new Point(f.IntervalNum.Location.X + f.IntervalNum.Size.Width + 6, 223);
        }
    }
}
