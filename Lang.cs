using System.Collections.Generic;
using System.Drawing;

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
                { Ko, "다운\r\n로드" },
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
                { Ko, "찾아보기" }, { En, "Browse" }, { ZhCn, "选择" }, { ZhTw, "選擇" },
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
                { Ko, "스레드 수" }, { En, "threads" }, { ZhCn, "线程数" }, { ZhTw, "執行緒數" },
                { Ja, "スレッド数" }, { Vi, "luồng" }, { Th, "เธรด" }, { Id, "utas" } } },
            { "ui_interval", new Dictionary<string, string> {
                { Ko, "간격" }, { En, "" }, { ZhCn, "间隔" }, { ZhTw, "間隔" },
                { Ja, "間隔" }, { Vi, "" }, { Th, "" }, { Id, "" } } },
            { "ui_second", new Dictionary<string, string> {
                { Ko, "초" }, { En, "sec interval" }, { ZhCn, "秒" }, { ZhTw, "秒" },
                { Ja, "秒" }, { Vi, "giây/lần" }, { Th, "วิ/รอบ" }, { Id, "dtk/jeda" } } },
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

        // 中日韩三语共享 "Cjk" 紧凑布局; 英越泰印 共享 "Latin" 长文本布局
        static bool IsCjk(string lang)
        {
            return lang == Ko || lang == ZhCn || lang == ZhTw || lang == Ja;
        }

        public static void Apply(MainWin f, string lang)
        {
            // 校验语言代码
            bool known = false;
            foreach (var c in Codes) if (c == lang) { known = true; break; }
            Current = known ? lang : Ko;
            bool cjk = IsCjk(Current);

            // ------- 文案(由 Lang.T 自动按 Current 取值) -------
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

            f.ThreadLabel.Text = T("ui_threads");
            f.IntervalLabel.Text = T("ui_interval");
            f.SecondLabel.Text = T("ui_second");

            // ------- 布局(只有两套:CJK 紧凑 / Latin 自适应) -------
            f.NovelNoLable.Location = new Point(11, 70);
            f.EmailLabel.Location = new Point(cjk ? 21 : 27, 33);
            f.PasswordLabel.Location = new Point(cjk ? 12 : 10, 70);

            f.NoticeCheck.Location = new Point(25, 144);
            f.OutputDirLabel.Location = new Point(20, 178);

            f.RemoveBlankCheck.Location = new Point(10, 215);
            f.KeepHtmlCheck.Location = new Point(cjk ? 135 : 150, 215);
            f.RetryLabel.Location = new Point(cjk ? 285 : 290, 218);
            f.RetryNum.Location = new Point(360, 215);
            f.CompressCheck.Location = new Point(10, 252);
            f.DownloadImageCheck.Location = new Point(cjk ? 135 : 150, 252);

            if (cjk)
            {
                // 韩中日:checkbox 是纯方框,文字写在右侧独立 Label
                f.FromCheck.Text = "";
                f.FromCheck.AutoSize = false;
                f.FromCheck.Size = new Size(22, 21);
                f.FromCheck.Location = new Point(25, 35);
                f.FromNum.Location = new Point(53, 30);
                f.FromLabel.Text = T("ui_from_ep");
                f.FromLabel.Location = new Point(128, 33);
                f.FromLabel.Visible = true;

                f.ToCheck.Text = "";
                f.ToCheck.AutoSize = false;
                f.ToCheck.Size = new Size(22, 21);
                f.ToCheck.Location = new Point(249, 35);
                f.ToNum.Location = new Point(277, 30);
                f.ToLabel.Text = T("ui_to_ep");
                f.ToLabel.Location = new Point(352, 33);
                f.ToLabel.Visible = true;

                f.ThreadNum.Location = new Point(132, 220);
                f.IntervalLabel.Location = new Point(268, 223);
                f.IntervalLabel.Visible = true;
                f.IntervalNum.Location = new Point(322, 220);
                f.SecondLabel.Location = new Point(410, 223);
            }
            else
            {
                // 英越泰印:checkbox 自带文字,取消独立 Label
                f.FromCheck.AutoSize = true;
                f.FromCheck.Text = T("ui_from_ep");
                f.FromCheck.Location = new Point(25, 31);
                f.FromNum.Location = new Point(140, 30);
                f.FromLabel.Visible = false;

                f.ToCheck.AutoSize = true;
                f.ToCheck.Text = T("ui_to_ep");
                f.ToCheck.Location = new Point(249, 31);
                f.ToNum.Location = new Point(343, 30);
                f.ToLabel.Visible = false;

                f.ThreadNum.Location = new Point(115, 220);
                f.IntervalLabel.Visible = false;
                f.IntervalNum.Location = new Point(255, 220);
                f.SecondLabel.Location = new Point(343, 223);
            }
        }
    }
}
