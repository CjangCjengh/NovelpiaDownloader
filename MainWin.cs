using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace NovelpiaDownloader
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();
            novelpia = new Novelpia();
            this.SizeChanged += (s, e) => Lang.Relayout(this);

            string lang = Lang.Ko;
            if (File.Exists("config.json"))
            {
                var config_dict = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(File.ReadAllText("config.json"));
                if (config_dict.ContainsKey("language"))
                    lang = config_dict["language"];
                ApplyLanguage(lang);
                if (config_dict.ContainsKey("thread_num"))
                    ThreadNum.Value = config_dict["thread_num"];
                if (config_dict.ContainsKey("interval_num"))
                    IntervalNum.Value = config_dict["interval_num"];
                if (config_dict.ContainsKey("mapping_path"))
                    font_mapping = new FontMapping(FontBox.Text = config_dict["mapping_path"]);
                if (config_dict.ContainsKey("output_dir"))
                    OutputDirText.Text = config_dict["output_dir"];
                if (config_dict.ContainsKey("include_notice"))
                    NoticeCheck.Checked = config_dict["include_notice"];
                if (config_dict.ContainsKey("remove_blank"))
                    RemoveBlankCheck.Checked = config_dict["remove_blank"];
                if (config_dict.ContainsKey("keep_html"))
                    KeepHtmlCheck.Checked = config_dict["keep_html"];
                if (config_dict.ContainsKey("retry_num"))
                    RetryNum.Value = config_dict["retry_num"];
                if (config_dict.ContainsKey("compress"))
                    CompressCheck.Checked = config_dict["compress"];
                if (config_dict.ContainsKey("download_image"))
                    DownloadImageCheck.Checked = config_dict["download_image"];
                if (config_dict.ContainsKey("stop_on_error"))
                    StopOnErrorCheck.Checked = config_dict["stop_on_error"];
                if (config_dict.ContainsKey("email") && config_dict.ContainsKey("wd"))
                    if (novelpia.Login(EmailText.Text = config_dict["email"], PasswordText.Text = config_dict["wd"]))
                    {
                        ConsoleBox.AppendText(Lang.T("login_ok"));
                        LoginkeyText.Text = novelpia.loginkey;
                        return;
                    }
                    else
                        ConsoleBox.AppendText(Lang.T("login_fail"));
                if (config_dict.ContainsKey("loginkey"))
                    novelpia.loginkey = LoginkeyText.Text = config_dict["loginkey"];
            }
            else
                ApplyLanguage(lang);
        }

        void ApplyLanguage(string lang)
        {
            int idx = System.Array.IndexOf(Lang.Codes, lang);
            LanguageBox.SelectedIndex = idx >= 0 ? idx : 0;
            Lang.Apply(this, idx >= 0 ? lang : Lang.Ko);
        }

        private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = LanguageBox.SelectedIndex;
            if (idx < 0 || idx >= Lang.Codes.Length) idx = 0;
            Lang.Apply(this, Lang.Codes[idx]);
        }

        readonly Novelpia novelpia;
        private FontMapping font_mapping;
        private volatile bool _running;
        private volatile bool _cancelRequested;

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                _cancelRequested = true;
                DownloadButton.Enabled = false;
                DownloadButton.Text = Lang.T("btn_stopping");
                return;
            }
            bool saveAsEpub = EpubButton.Checked;
            bool includeNotice = NoticeCheck.Checked;
            bool removeBlank = RemoveBlankCheck.Checked;
            bool keepHtml = KeepHtmlCheck.Checked;
            bool compress = CompressCheck.Checked;
            bool downloadImage = DownloadImageCheck.Checked;
            bool stopOnError = StopOnErrorCheck.Checked;
            int retry = (int)RetryNum.Value;
            string outputDir = OutputDirText.Text.Trim();
            var noMatch = Regex.Match(NovelNoText.Text ?? "", @"\d+");
            if (!noMatch.Success)
                return;
            string novelNo = noMatch.Value;
            string ext = saveAsEpub ? ".epub" : ".txt";
            string targetPath = null;
            if (!string.IsNullOrEmpty(outputDir) && Directory.Exists(outputDir))
            {
                string title = FetchNovelTitle(novelNo);
                if (string.IsNullOrEmpty(title))
                    title = novelNo;
                foreach (char c in Path.GetInvalidFileNameChars())
                    title = title.Replace(c, '_');
                targetPath = Path.Combine(outputDir, title + ext);
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = saveAsEpub ? "|*.epub" : "|*.txt"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                    targetPath = sfd.FileName;
                sfd.Dispose();
            }
            if (targetPath == null)
                return;
            _running = true;
            _cancelRequested = false;
            DownloadButton.Text = Lang.T("btn_stop");
            Download(novelNo, saveAsEpub, includeNotice, removeBlank, keepHtml, compress, downloadImage, stopOnError, retry, targetPath);
        }

        private void OutputDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = Lang.T("folder_select"),
                SelectedPath = OutputDirText.Text
            };
            if (fbd.ShowDialog() == DialogResult.OK)
                OutputDirText.Text = fbd.SelectedPath;
            fbd.Dispose();
        }

        private string FetchNovelTitle(string novelNo)
        {
            try
            {
                string html = GetRequest($"https://novelpia.com/novel/{novelNo}", novelpia.loginkey);
                var m = Regex.Match(html, @"productName = '(.+?)';");
                return m.Success ? HttpUtility.HtmlDecode(m.Groups[1].Value) : null;
            }
            catch
            {
                return null;
            }
        }

        void Download(string novelNo, bool saveAsEpub, bool includeNotice, bool removeBlank, bool keepHtml, bool compress, bool downloadImage, bool stopOnError, int retry, string path)
        {
            Log(Lang.T("sep") + Lang.T("download_start"));
            string directory = Path.Combine(Path.GetDirectoryName(path), novelNo);
            Directory.CreateDirectory(directory);
            int thread_num = (int)ThreadNum.Value;
            float interval = (float)IntervalNum.Value;
            int from = FromCheck.Checked ? (int)FromNum.Value : 1;
            int to = ToCheck.Checked ? (int)ToNum.Value : int.MaxValue;
            _stopOnError = stopOnError;
            _retryCount = retry;
            _hadFatalError = false;
            Task.Run(() =>
            {
                try
                {
                    string novelPageHtml = (saveAsEpub || includeNotice) ? GetRequest($"https://novelpia.com/novel/{novelNo}", novelpia.loginkey) : null;
                    int page = 0;
                    var seenChapterIds = new HashSet<string>();
                    var chapterNames = new List<(string, string)>();
                    List<Thread> threads = new List<Thread>();
                    if (includeNotice && novelPageHtml != null)
                    {
                        var notice_table = Regex.Match(novelPageHtml, @"<table[^>]*class=""[^""]*notice_table[^""]*""[^>]*>(.+?)</table>", RegexOptions.Singleline);
                        if (notice_table.Success)
                        {
                            var notices = Regex.Matches(notice_table.Groups[1].Value, @"location='/viewer/(\d+)';""[^>]*><b>(.+?)</b>", RegexOptions.Singleline);
                            int noticeNo = 1;
                            foreach (Match notice in notices)
                            {
                                string chapterId = notice.Groups[1].Value;
                                string chapterName = notice.Groups[2].Value;
                                string jsonPath = Path.Combine(directory, $"notice_{noticeNo.ToString().PadLeft(4, '0')}.json");
                                string capturedName = chapterName;
                                string capturedId = chapterId;
                                string capturedPath = jsonPath;
                                threads.Add(new Thread(() => DownloadChapter(capturedId, capturedName, capturedPath, true, 0)));
                                chapterNames.Add((HttpUtility.HtmlEncode($"[{Lang.T("notice")}] {chapterName}"), jsonPath));
                                noticeNo++;
                            }
                        }
                    }
                    bool get_content = true;
                    while (get_content)
                    {
                        if (_cancelRequested) break;
                        string data = $"novel_no={novelNo}&sort=DOWN&page={page}";
                        string resp = PostRequest("https://novelpia.com/proc/episode_list", "", data);
                        var chapters = Regex.Matches(resp, @"id=""bookmark_(\d+)""></i>(.+?)</b>.+?>EP\.(\d+)<");
                        if (chapters.Count == 0)
                            break;
                        if (seenChapterIds.Contains(chapters[0].Groups[1].Value))
                            break;
                        foreach (Match chapter in chapters)
                        {
                            int chapterNo = int.Parse(chapter.Groups[3].Value);
                            string chapterId = chapter.Groups[1].Value;
                            if (!seenChapterIds.Add(chapterId))
                                continue;
                            if (chapterNo < from)
                                continue;
                            if (chapterNo > to)
                            {
                                get_content = false;
                                break;
                            }
                            string chapterName = chapter.Groups[2].Value;
                            string jsonPath = Path.Combine(directory, $"{chapterNo.ToString().PadLeft(4, '0')}.json");
                            int capturedNo = chapterNo;
                            threads.Add(new Thread(() => DownloadChapter(chapterId, chapterName, jsonPath, false, capturedNo)));
                            chapterNames.Add((HttpUtility.HtmlEncode(chapterName), jsonPath));
                        }
                        page++;
                    }

                    ExecuteThreads(threads, thread_num, interval);
                    threads.Clear();
                    if (_cancelRequested)
                        Log(Lang.T("cancelled"));
                    else if (_hadFatalError)
                        Log(Lang.T("stop_on_error"));

                    if (saveAsEpub)
                    {
                        string responseText = novelPageHtml;
                        var match = Regex.Match(responseText, @"productName = '(.+?)';");
                        string title = match.Groups[1].Value;
                        match = Regex.Match(responseText, @"<meta[^>]+name=[""']author[""'][^>]+content=[""']([^""']+)[""']");
                        string author = match.Success ? match.Groups[1].Value : "";
                        string titleEnc = HttpUtility.HtmlEncode(title);
                        string authorEnc = HttpUtility.HtmlEncode(author);
                        match = Regex.Match(responseText, @"href=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                        string url = match.Groups[1].Value;
                        if (string.IsNullOrEmpty(url))
                        {
                            match = Regex.Match(responseText, @"src=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                            url = match.Groups[1].Value;
                        }

                        string cover_url = url;
                        string coverPath = Path.Combine(directory, "cover.jpg");
                        if (downloadImage && !_cancelRequested && !string.IsNullOrEmpty(cover_url))
                            threads.Add(new Thread(() => DownloadImage(cover_url, coverPath, Lang.T("cover"))));

                        var entries = new List<(string name, byte[] data)>();
                        var htmlNames = new List<string>(chapterNames.Count);
                        var imagePaths = new List<string>();
                        int imageNo = 1;

                        foreach (var s in chapterNames)
                        {
                            string htmlName = Path.ChangeExtension(Path.GetFileName(s.Item2), "html");
                            htmlNames.Add(htmlName);
                            if (!File.Exists(s.Item2))
                                continue;
                            var sb = new StringBuilder();
                            sb.Append(EpubTemplate.chapter);
                            sb.Append("<h1>").Append(s.Item1).Append("</h1>\n<p>&nbsp;</p>\n");
                            var pendingTags = new List<string>();
                            var serializer = new JavaScriptSerializer();
                            using (var reader = new StreamReader(s.Item2, Encoding.UTF8))
                            {
                                var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                foreach (var text in (ArrayList)texts["s"])
                                {
                                    var textDict = (Dictionary<string, object>)text;
                                    string textStr = (string)textDict["text"];
                                    var imatch = Regex.Match(textStr, @"<img.+?src=\""(.+?)\"".+?>");
                                    if (imatch.Success)
                                    {
                                        if (!textStr.Contains("cover-wrapper"))
                                        {
                                            if (downloadImage)
                                            {
                                                string image_url = imatch.Groups[1].Value;
                                                int image_no = imageNo;
                                                string imgPath = Path.Combine(directory, $"img_{image_no}.jpg");
                                                if (!_cancelRequested)
                                                    threads.Add(new Thread(() => DownloadImage(image_url, imgPath, Lang.T("illustration"))));
                                                imagePaths.Add(imgPath);
                                                textStr = Regex.Replace(textStr, @"<img.+?src=\"".+?\"".+?>",
                                                    $"<img alt=\"{imageNo}\" src=\"../Images/{imageNo}.jpg\" width=\"100%\"/>");
                                                sb.Append("<p>").Append(textStr).Append("</p>\n");
                                                imageNo++;
                                            }
                                            else if (!removeBlank)
                                            {
                                                sb.Append("<p>&#160;</p>\n");
                                            }
                                        }
                                        continue;
                                    }
                                    textStr = CleanText(textStr, keepHtml, pendingTags);
                                    if (textStr == "")
                                    {
                                        if (!removeBlank)
                                            sb.Append("<p>&#160;</p>\n");
                                        continue;
                                    }
                                    if (font_mapping != null)
                                        textStr = font_mapping.DecodeText(textStr);
                                    sb.Append("<p>").Append(textStr).Append("</p>\n");
                                }
                            }
                            sb.Append("</body>\n</html>\n");
                            entries.Add(($"OEBPS/Text/chapter{htmlName}", Encoding.UTF8.GetBytes(sb.ToString())));
                            File.Delete(s.Item2);
                        }

                        var ncx = new StringBuilder();
                        ncx.Append(EpubTemplate.toc);
                        ncx.Append("<text>").Append(titleEnc).Append("</text>\n</docTitle>\n<navMap>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            ncx.Append($"<navPoint id=\"navPoint-{i + 1}\" playOrder=\"{i + 1}\">\n")
                               .Append("<navLabel>\n<text>").Append(chapterNames[i].Item1).Append("</text>\n</navLabel>\n")
                               .Append("<content src=\"Text/chapter").Append(htmlNames[i]).Append("\" />\n")
                               .Append("</navPoint>\n");
                        }
                        ncx.Append("</navMap>\n</ncx>\n");

                        var opf = new StringBuilder();
                        opf.Append(EpubTemplate.content1);
                        opf.Append($"<dc:identifier id=\"BookId\" opf:scheme=\"NovelpiaNovelNo\">{novelNo}</dc:identifier>\n");
                        opf.Append("<dc:title>").Append(titleEnc).Append("</dc:title>\n");
                        opf.Append("<dc:language>ko</dc:language>\n");
                        if (!string.IsNullOrEmpty(author))
                            opf.Append("<dc:creator opf:role=\"aut\">").Append(authorEnc).Append("</dc:creator>\n");
                        opf.Append($"<dc:date>{DateTime.UtcNow:yyyy-MM-dd}</dc:date>\n");
                        bool hasCover = downloadImage;
                        if (hasCover)
                            opf.Append("<meta name=\"cover\" content=\"cover.jpg\"/>\n");
                        opf.Append(EpubTemplate.content2_head);
                        if (hasCover)
                            opf.Append(EpubTemplate.content2_cover);
                        for (int i = 0; i < chapterNames.Count; i++)
                            opf.Append($"<item id=\"chapter{htmlNames[i]}\" href=\"Text/chapter{htmlNames[i]}\" media-type=\"application/xhtml+xml\"/>\n");
                        for (int i = 1; i < imageNo; i++)
                            opf.Append($"<item id=\"img{i}\" href=\"Images/{i}.jpg\" media-type=\"image/jpeg\"/>\n");
                        opf.Append("</manifest>\n<spine toc=\"ncx\">\n");
                        if (hasCover)
                            opf.Append("<itemref idref=\"cover.html\"/>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                            opf.Append($"<itemref idref=\"chapter{htmlNames[i]}\"/>\n");
                        opf.Append("</spine>\n");
                        if (hasCover)
                            opf.Append("<guide>\n<reference type=\"cover\" title=\"Cover\" href=\"Text/cover.html\"/>\n</guide>\n");
                        opf.Append("</package>\n");

                        ExecuteThreads(threads, thread_num, interval);

                        if (File.Exists(path))
                            File.Delete(path);

                        var now = DateTimeOffset.Now;
                        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        using (var zip = new EpubZipWriter(fs, compress))
                        {
                            zip.AddEntry("mimetype", "application/epub+zip", now);
                            zip.AddEntry("META-INF/container.xml", EpubTemplate.container, now);
                            zip.AddEntry("OEBPS/Styles/sgc-toc.css", EpubTemplate.sgctoc, now);
                            zip.AddEntry("OEBPS/Styles/Stylesheet.css", EpubTemplate.stylesheet, now);
                            if (hasCover)
                                zip.AddEntry("OEBPS/Text/cover.html", EpubTemplate.cover, now);
                            if (hasCover && File.Exists(coverPath))
                                zip.AddEntry("OEBPS/Images/cover.jpg", File.ReadAllBytes(coverPath), now);
                            for (int i = 0; i < imagePaths.Count; i++)
                            {
                                string ip = imagePaths[i];
                                if (File.Exists(ip))
                                    zip.AddEntry($"OEBPS/Images/{i + 1}.jpg", File.ReadAllBytes(ip), now);
                            }
                            foreach (var entry in entries)
                                zip.AddEntry(entry.name, entry.data, now);
                            zip.AddEntry("OEBPS/toc.ncx", ncx.ToString(), now);
                            zip.AddEntry("OEBPS/content.opf", opf.ToString(), now);
                        }
                    }
                    else
                    {
                        using (var file = new StreamWriter(path, false))
                        {
                            var serializer = new JavaScriptSerializer();
                            chapterNames.ForEach(s =>
                            {
                                file.Write($"{HttpUtility.HtmlDecode(s.Item1)}\n\n");
                                if (!File.Exists(s.Item2))
                                    return;
                                var pendingTags = new List<string>();
                                using (var reader = new StreamReader(s.Item2, Encoding.UTF8))
                                {
                                    var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                    foreach (var text in (ArrayList)texts["s"])
                                    {
                                        var textDict = (Dictionary<string, object>)text;
                                        string textStr = (string)textDict["text"];
                                        if (textStr.Contains("cover-wrapper"))
                                            continue;
                                        if (!keepHtml)
                                            textStr = Regex.Replace(textStr, @"<img.+?>", "");
                                        textStr = CleanText(textStr, keepHtml, pendingTags);
                                        if (textStr == "")
                                        {
                                            if (!removeBlank)
                                                file.WriteLine();
                                            continue;
                                        }
                                        textStr = HttpUtility.HtmlDecode(textStr);
                                        if (font_mapping != null)
                                            textStr = font_mapping.DecodeText(textStr);
                                        file.WriteLine(textStr);
                                    }
                                }
                                file.Write("\n");
                                File.Delete(s.Item2);
                            });
                        }
                    }
                    Directory.Delete(directory, true);
                    Log(Lang.T("download_done") + Lang.T("sep"));
                }
                finally
                {
                    Invoke(new Action(() =>
                    {
                        _running = false;
                        _cancelRequested = false;
                        DownloadButton.Enabled = true;
                        DownloadButton.Text = Lang.T("btn_download");
                    }));
                }
            });
        }

        private volatile bool _stopOnError;
        private volatile bool _hadFatalError;
        private int _retryCount;

        private void DownloadChapter(string chapterId, string chapterName, string jsonPath, bool isNotice, int epNo)
        {
            if (_cancelRequested || _hadFatalError) return;
            int retry = _retryCount;
            string referer = $"https://novelpia.com/viewer/{chapterId}";
            for (int attempt = 0; attempt <= retry; attempt++)
            {
                try
                {
                    string resp = PostRequest($"https://novelpia.com/proc/viewer_data/{chapterId}", novelpia.loginkey, null, referer);
                    if (string.IsNullOrEmpty(resp) || resp.Contains("본인인증"))
                        throw new Exception();
                    using (var file = new StreamWriter(jsonPath, false))
                        file.Write(resp);
                    Log(isNotice ? Lang.T("notice_ok", chapterName) : Lang.T("chapter_ok", epNo, chapterName));
                    return;
                }
                catch
                {
                    if (attempt < retry)
                        Log(Lang.T("chapter_retry", attempt + 1, retry));
                    else
                    {
                        Log(isNotice ? Lang.T("notice_fail", chapterName) : Lang.T("chapter_fail", epNo, chapterName));
                        if (_stopOnError) _hadFatalError = true;
                    }
                }
            }
        }

        private string CleanText(string textStr, bool keepHtml, List<string> pendingTags)
        {
            textStr = Regex.Replace(textStr, @"<p style='height: 0px; width: 0px;.+?>.*?</p>", "");
            if (!keepHtml)
            {
                textStr = Regex.Replace(textStr, @"</?[^>]+>", "");
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var tag in pendingTags)
                    sb.Append('<').Append(tag).Append('>');
                var stack = new List<string>(pendingTags);
                int pos = 0;
                var tagMatches = Regex.Matches(textStr, @"<(/?)([a-zA-Z][a-zA-Z0-9]*)([^>]*)>");
                foreach (Match m in tagMatches)
                {
                    sb.Append(textStr.Substring(pos, m.Index - pos));
                    string tagName = m.Groups[2].Value.ToLower();
                    bool isClose = m.Groups[1].Value == "/";
                    bool selfClose = m.Value.EndsWith("/>") || tagName == "img" || tagName == "br" || tagName == "hr";
                    if (selfClose)
                    {
                        sb.Append(m.Value);
                    }
                    else if (isClose)
                    {
                        if (stack.Count > 0 && stack[stack.Count - 1].StartsWith(tagName))
                        {
                            stack.RemoveAt(stack.Count - 1);
                            sb.Append(m.Value);
                        }
                    }
                    else
                    {
                        stack.Add(tagName + m.Groups[3].Value);
                        sb.Append(m.Value);
                    }
                    pos = m.Index + m.Length;
                }
                sb.Append(textStr.Substring(pos));
                for (int k = stack.Count - 1; k >= pendingTags.Count; k--)
                {
                    string fullOpen = stack[k];
                    int sp = fullOpen.IndexOf(' ');
                    string tn = sp > 0 ? fullOpen.Substring(0, sp) : fullOpen;
                    sb.Append("</").Append(tn).Append('>');
                }
                pendingTags.Clear();
                pendingTags.AddRange(stack);
                textStr = sb.ToString();
            }
            return textStr.Replace("\n", "").Replace("\r", "");
        }

        private void Log(string message)
        {
            if (InvokeRequired)
                Invoke(new Action(() => Log(message)));
            else
            {
                ConsoleBox.AppendText(message);
                ConsoleBox.SelectionStart = ConsoleBox.TextLength;
                ConsoleBox.ScrollToCaret();
            }
        }

        private void DownloadImage(string url, string path, string type)
        {
            if (!url.StartsWith("http"))
                url = "https:" + url;
            Log(Lang.T("img_start", type, url));
            try
            {
                using (var downloader = new WebClient())
                    downloader.DownloadFile(url, path);
                Log(Lang.T("img_done", type));
            }
            catch
            {
                Log(Lang.T("img_fail", type, url));
            }
        }

        private void ExecuteThreads(List<Thread> threads, int batch_size, float interval)
        {
            for (int i = 0; i < threads.Count; i += batch_size)
            {
                if (_cancelRequested || _hadFatalError) break;
                int remain = threads.Count - i;
                int limit = batch_size < remain ? batch_size : remain;
                for (int j = 0; j < limit; j++)
                    threads[i + j].Start();
                for (int j = 0; j < limit; j++)
                    threads[i + j].Join();
                if (_cancelRequested || _hadFatalError) break;
                Thread.Sleep((int)(interval * 1000));
            }
        }

        private static string PostRequest(string url, string loginkey, string data = null, string referer = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("cookie", $"LOGINKEY={loginkey};");
            if (!string.IsNullOrEmpty(referer))
                request.Referer = referer;
            if (!string.IsNullOrEmpty(data))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
            }
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private static string GetRequest(string url, string loginkey)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
            request.Headers.Add("cookie", $"LOGINKEY={loginkey};");
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private void LoginButton1_Click(object sender, EventArgs e)
        {
            string email = EmailText.Text;
            string password = PasswordText.Text;
            if (novelpia.Login(email, password))
            {
                Log(Lang.T("login_ok"));
                LoginkeyText.Text = novelpia.loginkey;
            }
            else
            {
                Log(Lang.T("login_fail"));
            }
        }

        private void LoginButton2_Click(object sender, EventArgs e)
        {
            novelpia.loginkey = LoginkeyText.Text;
        }

        private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            var config_dict = new Dictionary<string, dynamic>
            {
                { "language", Lang.Current },
                { "thread_num", ThreadNum.Value },
                { "interval_num", IntervalNum.Value },
                { "email", EmailText.Text },
                { "wd", PasswordText.Text },
                { "loginkey", LoginkeyText.Text },
                { "mapping_path", FontBox.Text },
                { "output_dir", OutputDirText.Text },
                { "include_notice", NoticeCheck.Checked },
                { "remove_blank", RemoveBlankCheck.Checked },
                { "keep_html", KeepHtmlCheck.Checked },
                { "retry_num", RetryNum.Value },
                { "compress", CompressCheck.Checked },
                { "download_image", DownloadImageCheck.Checked },
                { "stop_on_error", StopOnErrorCheck.Checked }
            };
            using (StreamWriter sw = new StreamWriter("config.json"))
            {
                sw.Write(new JavaScriptSerializer().Serialize(config_dict));
            }
        }

        private void FromCheck_CheckedChanged(object sender, EventArgs e)
        {
            FromNum.Enabled = FromCheck.Checked;
            FromLabel.Enabled = FromCheck.Checked;
        }

        private void ToCheck_CheckedChanged(object sender, EventArgs e)
        {
            ToNum.Enabled = ToCheck.Checked;
            ToLabel.Enabled = ToCheck.Checked;
        }

        private void FontButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "|*.json"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
                font_mapping = new FontMapping(FontBox.Text = ofd.FileName);
        }

        private void FontBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                font_mapping = new FontMapping(FontBox.Text);
        }
    }
}
