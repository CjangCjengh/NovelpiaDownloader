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
                { Ja, "로그인에 실패했습니다。\r\n" },
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
            { "btn_add_to_list", new Dictionary<string, string> {
                { Ko, "목록에 추가" }, { En, "Add to queue" }, { ZhCn, "添加到队列" }, { ZhTw, "加入佇列" },
                { Ja, "キューに追加" }, { Vi, "Thêm vào hàng đợi" }, { Th, "เพิ่มในคิว" }, { Id, "Tambah ke antrian" } } },
            { "btn_queue_delete_all", new Dictionary<string, string> {
                { Ko, "전체 삭제" }, { En, "Clear all" }, { ZhCn, "全部删除" }, { ZhTw, "全部刪除" },
                { Ja, "すべて削除" }, { Vi, "Xoá tất cả" }, { Th, "ลบทั้งหมด" }, { Id, "Hapus semua" } } },
            { "btn_queue_delete_selected", new Dictionary<string, string> {
                { Ko, "선택 삭제" }, { En, "Delete selected" }, { ZhCn, "删除选中" }, { ZhTw, "刪除選取" },
                { Ja, "選択削除" }, { Vi, "Xoá đã chọn" }, { Th, "ลบที่เลือก" }, { Id, "Hapus pilihan" } } },
            { "btn_queue_download", new Dictionary<string, string> {
                { Ko, "다운로드" }, { En, "Download" }, { ZhCn, "下载" }, { ZhTw, "下載" },
                { Ja, "ダウンロード" }, { Vi, "Tải" }, { Th, "ดาวน์โหลด" }, { Id, "Unduh" } } },
            { "queue_added", new Dictionary<string, string> {
                { Ko, "  ➜ 목록에 추가됨: {0}\r\n" }, { En, "  ➜ Added to queue: {0}\r\n" },
                { ZhCn, "  ➜ 已加入队列: {0}\r\n" }, { ZhTw, "  ➜ 已加入佇列: {0}\r\n" },
                { Ja, "  ➜ キューに追加: {0}\r\n" }, { Vi, "  ➜ Đã thêm vào hàng đợi: {0}\r\n" },
                { Th, "  ➜ เพิ่มเข้าคิวแล้ว: {0}\r\n" }, { Id, "  ➜ Ditambahkan ke antrian: {0}\r\n" } } },
            { "queue_dup", new Dictionary<string, string> {
                { Ko, "  ⚠ 이미 목록에 있음: {0}\r\n" }, { En, "  ⚠ Already in queue: {0}\r\n" },
                { ZhCn, "  ⚠ 已在队列中: {0}\r\n" }, { ZhTw, "  ⚠ 已在佇列中: {0}\r\n" },
                { Ja, "  ⚠ 既にキューにあります: {0}\r\n" }, { Vi, "  ⚠ Đã có trong hàng đợi: {0}\r\n" },
                { Th, "  ⚠ มีในคิวอยู่แล้ว: {0}\r\n" }, { Id, "  ⚠ Sudah ada di antrian: {0}\r\n" } } },
            { "queue_skip", new Dictionary<string, string> {
                { Ko, "  ⚠ 저장 경로 없음, 건너뜀: {0}\r\n" }, { En, "  ⚠ No save path, skipped: {0}\r\n" },
                { ZhCn, "  ⚠ 无保存路径,已跳过: {0}\r\n" }, { ZhTw, "  ⚠ 無保存路徑,已跳過: {0}\r\n" },
                { Ja, "  ⚠ 保存先未指定、スキップ: {0}\r\n" }, { Vi, "  ⚠ Chưa đặt đường dẫn, bỏ qua: {0}\r\n" },
                { Th, "  ⚠ ไม่มีพาธ ข้ามไป: {0}\r\n" }, { Id, "  ⚠ Tanpa jalur simpan, dilewati: {0}\r\n" } } },
            { "queue_start", new Dictionary<string, string> {
                { Ko, "📚 일괄 다운로드 시작! (총 {0}권)\r\n" }, { En, "📚 Batch download start! (total {0})\r\n" },
                { ZhCn, "📚 批量下载开始! (共 {0} 本)\r\n" }, { ZhTw, "📚 批次下載開始! (共 {0} 本)\r\n" },
                { Ja, "📚 一括ダウンロード開始! (計 {0}冊)\r\n" }, { Vi, "📚 Bắt đầu tải hàng loạt! (tổng {0})\r\n" },
                { Th, "📚 เริ่มดาวน์โหลดชุด! (ทั้งหมด {0})\r\n" }, { Id, "📚 Mulai unduh massal! (total {0})\r\n" } } },
            { "queue_done", new Dictionary<string, string> {
                { Ko, "🎉 일괄 다운로드 완료!\r\n" }, { En, "🎉 Batch download finished!\r\n" },
                { ZhCn, "🎉 批量下载完成!\r\n" }, { ZhTw, "🎉 批次下載完成!\r\n" },
                { Ja, "🎉 一括ダウンロード完了!\r\n" }, { Vi, "🎉 Hoàn tất tải hàng loạt!\r\n" },
                { Th, "🎉 ดาวน์โหลดชุดเสร็จสิ้น!\r\n" }, { Id, "🎉 Unduh massal selesai!\r\n" } } },
            { "queue_book_header", new Dictionary<string, string> {
                { Ko, "[{0}/{1}] {2}" }, { En, "[{0}/{1}] {2}" }, { ZhCn, "[{0}/{1}] {2}" }, { ZhTw, "[{0}/{1}] {2}" },
                { Ja, "[{0}/{1}] {2}" }, { Vi, "[{0}/{1}] {2}" }, { Th, "[{0}/{1}] {2}" }, { Id, "[{0}/{1}] {2}" } } },
            { "fetching_chapters", new Dictionary<string, string> {
                { Ko, "📖 챕터 목록 가져오는 중..." },
                { En, "📖 Fetching episode list..." },
                { ZhCn, "📖 正在获取章节列表..." },
                { ZhTw, "📖 正在取得章節列表..." },
                { Ja, "📖 エピソード一覧を取得中..." },
                { Vi, "📖 Đang tải danh sách tập..." },
                { Th, "📖 กำลังดึงรายการตอน..." },
                { Id, "📖 Mengambil daftar episode..." } } },
            { "chapter_list_progress", new Dictionary<string, string> {
                { Ko, "  ➜ {0}화 발견" },
                { En, "  ➜ {0} episodes found" },
                { ZhCn, "  ➜ 已找到 {0} 章" },
                { ZhTw, "  ➜ 已找到 {0} 章" },
                { Ja, "  ➜ {0} 話発見" },
                { Vi, "  ➜ Tìm thấy {0} tập" },
                { Th, "  ➜ พบ {0} ตอน" },
                { Id, "  ➜ Ditemukan {0} ep" } } },
            { "chapter_list_done", new Dictionary<string, string> {
                { Ko, "  ✓ 챕터 목록 완료, 총 {0}화" },
                { En, "  ✓ Episode list ready, total {0} episodes" },
                { ZhCn, "  ✓ 章节列表完成,共 {0} 章" },
                { ZhTw, "  ✓ 章節列表完成,共 {0} 章" },
                { Ja, "  ✓ エピソード一覧取得完了、計 {0} 話" },
                { Vi, "  ✓ Xong danh sách tập, tổng {0} tập" },
                { Th, "  ✓ ได้รายการแล้ว ทั้งหมด {0} ตอน" },
                { Id, "  ✓ Daftar episode siap, total {0} ep" } } },
            { "phase_chapters", new Dictionary<string, string> {
                { Ko, "📥 챕터 다운로드" },
                { En, "📥 Downloading episodes" },
                { ZhCn, "📥 下载章节" },
                { ZhTw, "📥 下載章節" },
                { Ja, "📥 エピソードをダウンロード" },
                { Vi, "📥 Tải các tập" },
                { Th, "📥 ดาวน์โหลดตอน" },
                { Id, "📥 Mengunduh episode" } } },
            { "phase_images", new Dictionary<string, string> {
                { Ko, "🖼 이미지 다운로드" },
                { En, "🖼 Downloading images" },
                { ZhCn, "🖼 下载图片" },
                { ZhTw, "🖼 下載圖片" },
                { Ja, "🖼 画像をダウンロード" },
                { Vi, "🖼 Tải ảnh minh hoạ" },
                { Th, "🖼 ดาวน์โหลดภาพ" },
                { Id, "🖼 Mengunduh gambar" } } },
            { "progress_tail", new Dictionary<string, string> {
                { Ko, "완료: {0} | 실패: {1}" },
                { En, "done: {0} | fail: {1}" },
                { ZhCn, "完成: {0} | 失败: {1}" },
                { ZhTw, "完成: {0} | 失敗: {1}" },
                { Ja, "完了: {0} | 失敗: {1}" },
                { Vi, "xong: {0} | lỗi: {1}" },
                { Th, "เสร็จ: {0} | พลาด: {1}" },
                { Id, "selesai: {0} | gagal: {1}" } } },
            { "progress_summary_done", new Dictionary<string, string> {
                { Ko, "✅ {0} 완료! 성공: {1} | 실패: {2} / 총 {3}" },
                { En, "✅ {0} finished. ok: {1} | fail: {2} / total {3}" },
                { ZhCn, "✅ {0} 完成! 成功: {1} | 失败: {2} / 共 {3}" },
                { ZhTw, "✅ {0} 完成! 成功: {1} | 失敗: {2} / 共 {3}" },
                { Ja, "✅ {0} 完了! 成功: {1} | 失敗: {2} / 計 {3}" },
                { Vi, "✅ {0} xong! ok: {1} | lỗi: {2} / tổng {3}" },
                { Th, "✅ {0} เสร็จ! สำเร็จ: {1} | พลาด: {2} / ทั้งหมด {3}" },
                { Id, "✅ {0} selesai! ok: {1} | gagal: {2} / total {3}" } } },
            { "progress_summary_cancel", new Dictionary<string, string> {
                { Ko, "⏹ {0} 중지됨. 성공: {1} | 실패: {2} / 총 {3}" },
                { En, "⏹ {0} stopped. ok: {1} | fail: {2} / total {3}" },
                { ZhCn, "⏹ {0} 已停止. 成功: {1} | 失败: {2} / 共 {3}" },
                { ZhTw, "⏹ {0} 已停止. 成功: {1} | 失敗: {2} / 共 {3}" },
                { Ja, "⏹ {0} 停止. 成功: {1} | 失敗: {2} / 計 {3}" },
                { Vi, "⏹ {0} đã dừng. ok: {1} | lỗi: {2} / tổng {3}" },
                { Th, "⏹ {0} หยุดแล้ว สำเร็จ: {1} | พลาด: {2} / ทั้งหมด {3}" },
                { Id, "⏹ {0} dihentikan. ok: {1} | gagal: {2} / total {3}" } } },
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
                { Ko, "찾아보기" }, { En, "Browse..." }, { ZhCn, "浏览..." }, { ZhTw, "瀏覽..." },
                { Ja, "参照..." }, { Vi, "Duyệt..." }, { Th, "เรียกดู..." }, { Id, "Telusuri..." } } },
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
                { Ko, "간격 (초)" }, { En, "Interval (s)" }, { ZhCn, "间隔 (秒)" }, { ZhTw, "間隔 (秒)" },
                { Ja, "間隔 (秒)" }, { Vi, "Giãn cách (giây)" }, { Th, "ระยะ (วิ)" }, { Id, "Jeda (dtk)" } } },
            { "ui_second", new Dictionary<string, string> {
                { Ko, "" }, { En, "" }, { ZhCn, "" }, { ZhTw, "" },
                { Ja, "" }, { Vi, "" }, { Th, "" }, { Id, "" } } },
            { "ui_stop_on_error", new Dictionary<string, string> {
                { Ko, "오류 시 중단" }, { En, "Stop on error" }, { ZhCn, "出错时中断" }, { ZhTw, "出錯時中斷" },
                { Ja, "エラー時中断" }, { Vi, "Dừng khi lỗi" }, { Th, "หยุดเมื่อพลาด" }, { Id, "Berhenti saat error" } } },
            { "ui_include_novel_no", new Dictionary<string, string> {
                { Ko, "파일명에 소설 번호 추가" }, { En, "Add novel ID to filename" }, { ZhCn, "文件名加编号" }, { ZhTw, "檔名加編號" },
                { Ja, "ファイル名に番号追加" }, { Vi, "Thêm ID vào tên file" }, { Th, "เพิ่มรหัสในชื่อไฟล์" }, { Id, "Tambah ID ke nama file" } } },
            { "ui_include_chapter_range", new Dictionary<string, string> {
                { Ko, "파일명에 화수 범위 추가" }, { En, "Add Ep. range to filename" }, { ZhCn, "文件名加话数" }, { ZhTw, "檔名加話數" },
                { Ja, "ファイル名に話数追加" }, { Vi, "Thêm phạm vi tập vào tên" }, { Th, "เพิ่มช่วงตอนในชื่อ" }, { Id, "Tambah rentang Ep ke nama" } } },
        };

        public static string T(string key)
        {
            if (texts.TryGetValue(key, out var d) && d.TryGetValue(Current, out var s))
                return s;
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

        static int MeasureWidth(Control c, string text, int padding)
        {
            if (string.IsNullOrEmpty(text)) return padding;
            var sz = TextRenderer.MeasureText(text, c.Font);
            return sz.Width + padding;
        }

        public static void Apply(MainWin f, string lang)
        {
            bool known = false;
            foreach (var c in Codes) if (c == lang) { known = true; break; }
            Current = known ? lang : Ko;

            string uiFontName = FontFor(Current);
            float uiSize = f.Font.Size;
            var newFont = new Font(uiFontName, uiSize, FontStyle.Regular, GraphicsUnit.Point);
            f.Font = newFont;
            f.ConsoleBox.Font = newFont;

            f.LoginGroup.Text = T("ui_login_group");
            f.LoginButton1.Text = T("ui_login_btn");
            f.LoginButton2.Text = T("ui_login_btn");
            f.EmailLabel.Text = T("ui_email");
            f.PasswordLabel.Text = T("ui_password");

            f.DownloadGroup.Text = T("ui_download_group");
            f.NovelNoLable.Text = T("ui_novel_no");
            f.ExtensionLabel.Text = T("ui_extension");
            f.DownloadButton.Text = T("btn_download");
            f.AddToListButton.Text = T("btn_add_to_list");
            f.QueueDeleteAllButton.Text = T("btn_queue_delete_all");
            f.QueueDeleteSelectedButton.Text = T("btn_queue_delete_selected");
            f.QueueDownloadButton.Text = T("btn_queue_download");

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
            f.IncludeNovelNoCheck.Text = T("ui_include_novel_no");
            f.IncludeChapterRangeCheck.Text = T("ui_include_chapter_range");

            f.ThreadLabel.Text = T("ui_threads");
            f.IntervalLabel.Text = T("ui_interval");
            f.SecondLabel.Text = T("ui_second");

            Relayout(f);
            f.Height = RequiredHeight(f);
        }

        public static void Relayout(MainWin f)
        {
            const int outerPad = 12;
            const int gap = 12;
            int totalW = f.ClientSize.Width;

            int thrLabW = MeasureWidth(f.ThreadLabel, f.ThreadLabel.Text, 8);
            int intLabW = MeasureWidth(f.IntervalLabel, f.IntervalLabel.Text, 8);
            int retLabW = MeasureWidth(f.RetryLabel, f.RetryLabel.Text, 8);
            int threadRowMinW = 18 + thrLabW + 4 + 70 + 16 + intLabW + 4 + 70 + 16 + retLabW + 4 + 70 + 18;

            int leftHalf = (totalW - outerPad * 2 - gap) / 2;
            if (leftHalf < threadRowMinW) leftHalf = threadRowMinW;
            if (leftHalf < 360) leftHalf = 360;
            int rightX = outerPad + leftHalf + gap;
            int rightW = leftHalf;
            int requiredW = rightX + rightW + outerPad;

            f.LoginGroup.Location = new Point(outerPad, f.LoginGroup.Location.Y);
            f.LoginGroup.Size = new Size(leftHalf, f.LoginGroup.Size.Height);
            f.DownloadGroup.Location = new Point(outerPad, f.DownloadGroup.Location.Y);
            f.DownloadGroup.Size = new Size(leftHalf, f.DownloadGroup.Size.Height);
            f.LanguageBox.Location = new Point(outerPad, f.LanguageBox.Location.Y);
            f.LanguageBox.Size = new Size(leftHalf, f.LanguageBox.Size.Height);
            f.ConsoleBox.Location = new Point(rightX, f.ConsoleBox.Location.Y);
            f.ConsoleBox.Size = new Size(rightW, f.ConsoleBox.Size.Height);
            f.DownloadList.Location = new Point(rightX, f.DownloadList.Location.Y);
            f.DownloadList.Size = new Size(rightW, f.DownloadList.Size.Height);

            int dlInnerW = f.DownloadGroup.ClientSize.Width;
            int col2X = dlInnerW / 2;

            f.FromCheck.AutoSize = true;
            f.FromCheck.Text = T("ui_from_ep");
            f.FromCheck.Location = new Point(15, 33);
            int fromCheckRight = f.FromCheck.Location.X + MeasureWidth(f.FromCheck, f.FromCheck.Text, 24);
            f.FromNum.Location = new Point(fromCheckRight + 4, 30);
            f.FromNum.Size = new Size(70, 31);
            f.FromLabel.Visible = false;

            f.ToCheck.AutoSize = true;
            f.ToCheck.Text = T("ui_to_ep");
            f.ToCheck.Location = new Point(col2X, 33);
            int toCheckRight = f.ToCheck.Location.X + MeasureWidth(f.ToCheck, f.ToCheck.Text, 24);
            f.ToNum.Location = new Point(toCheckRight + 4, 30);
            f.ToNum.Size = new Size(70, 31);
            f.ToLabel.Visible = false;

            f.NoticeCheck.AutoSize = true;
            f.RemoveBlankCheck.AutoSize = true;
            f.KeepHtmlCheck.AutoSize = true;
            f.CompressCheck.AutoSize = true;
            f.DownloadImageCheck.AutoSize = true;
            f.StopOnErrorCheck.AutoSize = true;
            f.IncludeNovelNoCheck.AutoSize = true;
            f.IncludeChapterRangeCheck.AutoSize = true;

            f.NoticeCheck.Location = new Point(15, 68);
            f.RemoveBlankCheck.Location = new Point(col2X, 68);
            f.KeepHtmlCheck.Location = new Point(15, 97);
            f.CompressCheck.Location = new Point(col2X, 97);
            f.DownloadImageCheck.Location = new Point(15, 126);
            f.StopOnErrorCheck.Location = new Point(col2X, 126);
            f.IncludeNovelNoCheck.Location = new Point(15, 155);
            f.IncludeChapterRangeCheck.Location = new Point(col2X, 155);

            f.OutputDirLabel.Location = new Point(15, 188);
            int saveLabelRight = f.OutputDirLabel.Location.X + MeasureWidth(f.OutputDirLabel, f.OutputDirLabel.Text, 8);
            int browseBtnW = MeasureWidth(f.OutputDirButton, f.OutputDirButton.Text, 26);
            if (browseBtnW < 70) browseBtnW = 70;
            int outputRightEdge = f.DownloadGroup.ClientSize.Width - 12;
            f.OutputDirButton.Size = new Size(browseBtnW, 32);
            f.OutputDirButton.Location = new Point(outputRightEdge - browseBtnW, 185);
            f.OutputDirText.Location = new Point(saveLabelRight + 4, 185);
            f.OutputDirText.Size = new Size(f.OutputDirButton.Location.X - 6 - f.OutputDirText.Location.X, 31);

            f.NovelNoLable.Location = new Point(15, 227);
            int novelLabelRight = f.NovelNoLable.Location.X + MeasureWidth(f.NovelNoLable, f.NovelNoLable.Text, 8);
            f.NovelNoText.Location = new Point(novelLabelRight + 4, 224);
            f.NovelNoText.Size = new Size(outputRightEdge - f.NovelNoText.Location.X, 31);

            f.ExtensionLabel.Location = new Point(15, 264);
            int extLabelRight = f.ExtensionLabel.Location.X + MeasureWidth(f.ExtensionLabel, f.ExtensionLabel.Text, 8);
            f.EpubButton.Location = new Point(extLabelRight + 4, 261);
            int epubRight = f.EpubButton.Location.X + MeasureWidth(f.EpubButton, f.EpubButton.Text, 28);
            f.TxtButton.Location = new Point(epubRight + 8, 261);

            int dlBtnW = MeasureWidth(f.DownloadButton, f.DownloadButton.Text, 28);
            if (dlBtnW < 80) dlBtnW = 80;
            f.DownloadButton.Size = new Size(dlBtnW, 36);
            f.DownloadButton.Location = new Point(outputRightEdge - dlBtnW, 259);

            int addBtnW = MeasureWidth(f.AddToListButton, f.AddToListButton.Text, 28);
            if (addBtnW < 80) addBtnW = 80;
            f.AddToListButton.Size = new Size(addBtnW, 36);
            f.AddToListButton.Location = new Point(f.DownloadButton.Location.X - 8 - addBtnW, 259);

            int dlGroupBottomInner = f.DownloadButton.Location.Y + f.DownloadButton.Size.Height + 18;

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

            int fontRowY = f.LoginGroup.Location.Y + f.LoginGroup.Size.Height + 9;
            int fontLabRight = 19 + MeasureWidth(f.FontLabel, f.FontLabel.Text, 8);
            int fontBtnW = MeasureWidth(f.FontButton, f.FontButton.Text, 26);
            if (fontBtnW < 70) fontBtnW = 70;
            f.FontButton.Size = new Size(fontBtnW, 32);
            int fontRightEdge = f.DownloadGroup.Location.X + f.DownloadGroup.Size.Width - 12;
            f.FontLabel.Location = new Point(19, fontRowY + 4);
            f.FontButton.Location = new Point(fontRightEdge - fontBtnW, fontRowY);
            f.FontBox.Location = new Point(fontLabRight + 4, fontRowY + 1);
            f.FontBox.Size = new Size(f.FontButton.Location.X - 6 - f.FontBox.Location.X, 31);
            f.FontLabel.Enabled = false;
            f.FontBox.Enabled = false;
            f.FontButton.Enabled = false;

            int extraW = leftHalf - threadRowMinW;
            if (extraW < 0) extraW = 0;
            int shift1 = extraW / 6;
            int shift2 = extraW / 3;

            int threadRowY = fontRowY + f.FontButton.Size.Height + 6;
            f.ThreadLabel.Location = new Point(18, threadRowY + 3);
            int thrLabRight = 18 + MeasureWidth(f.ThreadLabel, f.ThreadLabel.Text, 8);
            f.ThreadNum.Location = new Point(thrLabRight + 4, threadRowY);
            f.ThreadNum.Size = new Size(70, 31);
            int thrNumRight = f.ThreadNum.Location.X + f.ThreadNum.Size.Width;

            f.IntervalLabel.Visible = true;
            f.IntervalLabel.Location = new Point(thrNumRight + 16 + shift1, threadRowY + 3);
            int intervalLabRight = f.IntervalLabel.Location.X + MeasureWidth(f.IntervalLabel, f.IntervalLabel.Text, 8);
            f.IntervalNum.Location = new Point(intervalLabRight + 4, threadRowY);
            f.IntervalNum.Size = new Size(70, 31);
            int intervalNumRight = f.IntervalNum.Location.X + f.IntervalNum.Size.Width;

            f.SecondLabel.Visible = false;

            f.RetryLabel.Location = new Point(intervalNumRight + 16 + (shift2 - shift1), threadRowY + 3);
            int retryLabRight = f.RetryLabel.Location.X + MeasureWidth(f.RetryLabel, f.RetryLabel.Text, 8);
            f.RetryNum.Location = new Point(retryLabRight + 4, threadRowY);
            f.RetryNum.Size = new Size(70, 31);

            int dlGroupY = threadRowY + 31 + 12;
            f.DownloadGroup.Location = new Point(f.DownloadGroup.Location.X, dlGroupY);
            int dlGroupAvailableH = f.ClientSize.Height - dlGroupY - 12;
            int dlGroupH = dlGroupBottomInner > dlGroupAvailableH ? dlGroupBottomInner : dlGroupAvailableH;
            f.DownloadGroup.Size = new Size(f.DownloadGroup.Size.Width, dlGroupH);

            int rightTop = f.LanguageBox.Location.Y;
            int rightBottom = f.DownloadGroup.Location.Y + f.DownloadGroup.Size.Height;
            int rightTotalH = rightBottom - rightTop;
            const int rightSplitGap = 8;
            const int queueBtnH = 32;
            int qBtn1W = MeasureWidth(f.QueueDeleteAllButton, f.QueueDeleteAllButton.Text, 28);
            int qBtn2W = MeasureWidth(f.QueueDeleteSelectedButton, f.QueueDeleteSelectedButton.Text, 28);
            int qBtn3W = MeasureWidth(f.QueueDownloadButton, f.QueueDownloadButton.Text, 28);
            if (qBtn1W < 80) qBtn1W = 80;
            if (qBtn2W < 80) qBtn2W = 80;
            if (qBtn3W < 80) qBtn3W = 80;
            f.QueueDeleteAllButton.Size = new Size(qBtn1W, queueBtnH);
            f.QueueDeleteSelectedButton.Size = new Size(qBtn2W, queueBtnH);
            f.QueueDownloadButton.Size = new Size(qBtn3W, queueBtnH);
            f.QueueDeleteAllButton.Location = new Point(rightX, rightTop);
            f.QueueDeleteSelectedButton.Location = new Point(rightX + qBtn1W + 6, rightTop);
            f.QueueDownloadButton.Location = new Point(rightX + qBtn1W + 6 + qBtn2W + 6, rightTop);
            int halfH = (rightTotalH - rightSplitGap) / 2;
            int listTop = rightTop + queueBtnH + 6;
            int listH = halfH - queueBtnH - 6;
            if (listH < 60) listH = 60;
            f.DownloadList.Location = new Point(rightX, listTop);
            f.DownloadList.Size = new Size(rightW, listH);
            int consoleTop = rightTop + halfH + rightSplitGap;
            int consoleH = rightBottom - consoleTop;
            if (consoleH < 60) consoleH = 60;
            f.ConsoleBox.Location = new Point(rightX, consoleTop);
            f.ConsoleBox.Size = new Size(rightW, consoleH);

            int desiredClientH = dlGroupY + dlGroupBottomInner + 12;
            int formExtraH = f.Height - f.ClientSize.Height;
            int desiredH = desiredClientH + formExtraH;
            int desiredMinW = outerPad + threadRowMinW + gap + threadRowMinW + outerPad + (f.Width - f.ClientSize.Width);
            if (desiredMinW < 906) desiredMinW = 906;
            f.MinimumSize = new Size(desiredMinW, desiredH);
        }

        public static int RequiredHeight(MainWin f)
        {
            return f.MinimumSize.Height;
        }
    }
}
