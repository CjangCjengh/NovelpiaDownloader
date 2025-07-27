using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms; // Required for Form and UI controls

namespace NovelpiaDownloader
{
    public partial class MainWin : Form
    {
        readonly Novelpia novelpia;
        private FontMapping font_mapping;

        // Default constructor for UI mode (when no arguments are passed)
        public MainWin() : this(null) { }

        // Constructor for when arguments are passed (either UI or headless)
        public MainWin(string[] args)
        {
            InitializeComponent(); // Initialize UI components first

            novelpia = new Novelpia(); // Initialize novelpia after components

            // Load existing configuration, including authentication details
            // This ensures the 'novelpia' instance is ready for authenticated requests,
            // whether the application runs in UI mode or headless mode.
            if (File.Exists("config.json"))
            {
                var config_dict = new JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(File.ReadAllText("config.json"));
                if (config_dict.ContainsKey("thread_num"))
                    ThreadNum.Value = config_dict["thread_num"];
                if (config_dict.ContainsKey("interval_num"))
                    IntervalNum.Value = config_dict["interval_num"];
                if (config_dict.ContainsKey("mapping_path"))
                    font_mapping = new FontMapping(FontBox.Text = config_dict["mapping_path"]);
                if (config_dict.ContainsKey("email") && config_dict.ContainsKey("wd"))
                    if (novelpia.Login(EmailText.Text = config_dict["email"], PasswordText.Text = config_dict["wd"]))
                    {
                        // Check if ConsoleBox exists before appending (for headless mode, it will be null)
                        if (ConsoleBox != null) ConsoleBox.Text += "로그인 성공!\r\n";
                        if (LoginkeyText != null) LoginkeyText.Text = novelpia.loginkey;
                    }
                    else
                    {
                        if (ConsoleBox != null) ConsoleBox.Text += "로그인 실패!\r\n";
                    }
                if (config_dict.ContainsKey("loginkey"))
                    novelpia.loginkey = LoginkeyText.Text = config_dict["loginkey"];
                if (config_dict.ContainsKey("include_html_in_txt"))
                    HtmlCheckBox.Checked = config_dict["include_html_in_txt"];
            }

            // Apply parsed arguments to UI controls if running in UI mode
            // This part is purely for pre-filling UI elements if the UI is shown.
            if (args != null && args.Length > 0)
            {
                string novelIdArg = null;
                int? fromChapterArg = null;
                int? toChapterArg = null;
                bool htmlCheckedArg = false;

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {
                        case "-novelid":
                            if (i + 1 < args.Length) novelIdArg = args[++i];
                            break;
                        case "-from":
                            if (i + 1 < args.Length && int.TryParse(args[++i], out int fromVal)) fromChapterArg = fromVal;
                            break;
                        case "-to":
                            if (i + 1 < args.Length && int.TryParse(args[++i], out int toVal)) toChapterArg = toVal;
                            break;
                        case "-html":
                            htmlCheckedArg = true;
                            break;
                        // Consume other args if they exist, but don't apply to UI here
                        case "-output":
                        case "-batch":
                        case "-listfile":
                        case "-outputdir":
                        case "-autostart":
                            if (i + 1 < args.Length && !args[i + 1].StartsWith("-")) ++i; // Consume value if it's not another flag
                            break;
                    }
                }

                // Apply arguments to UI elements if they exist
                if (NovelNoText != null && !string.IsNullOrEmpty(novelIdArg))
                {
                    NovelNoText.Text = novelIdArg;
                }
                if (FromNum != null && FromCheck != null && fromChapterArg.HasValue)
                {
                    FromNum.Value = fromChapterArg.Value;
                    FromCheck.Checked = true;
                }
                if (ToNum != null && ToCheck != null && toChapterArg.HasValue)
                {
                    ToNum.Value = toChapterArg.Value;
                    ToCheck.Checked = true;
                }
                if (HtmlCheckBox != null && htmlCheckedArg)
                {
                    HtmlCheckBox.Checked = true;
                }
            }
        }

        /// <summary>
        /// Core download logic for a single novel, used by both UI and headless modes.
        /// </summary>
        /// <param name="novelNo">The ID of the novel to download.</param>
        /// <param name="saveAsEpub">True to save as EPUB, false to save as TXT.</param>
        /// <param name="path">The full output path including filename (e.g., C:\novel.txt or C:\novel.epub).</param>
        /// <param name="fromChapter">Optional: Starting chapter number (1-indexed).</param>
        /// <param name="toChapter">Optional: Ending chapter number (1-indexed).</param>
        /// <param name="includeHtmlInTxt">True to include raw HTML in TXT output, false for plain text.</param>
        /// <param name="isHeadless">True if running in headless mode (logs to Console), false for UI mode (logs to ConsoleBox).</param>
        public void DownloadCore(
            string novelNo,
            bool saveAsEpub,
            string path,
            int? fromChapter = null,
            int? toChapter = null,
            bool includeHtmlInTxt = false,
            bool isHeadless = false)
        {
            Action<string> log = (msg) =>
            {
                if (isHeadless)
                {
                    Console.WriteLine(msg);
                }
                else
                {
                    // Ensure ConsoleBox exists before invoking
                    if (ConsoleBox != null && ConsoleBox.InvokeRequired)
                    {
                        ConsoleBox.Invoke(new Action(() => ConsoleBox.AppendText(msg)));
                    }
                    else if (ConsoleBox != null)
                    {
                        ConsoleBox.AppendText(msg);
                    }
                }
            };

            log("다운로드 시작!\r\n");
            string directory = Path.Combine(Path.GetDirectoryName(path), novelNo);
            Directory.CreateDirectory(directory);

            // Use values from config/UI, or defaults if not available (e.g., in headless mode without config)
            int thread_num = 1;
            float interval = 0.5f;
            if (!isHeadless && ThreadNum != null) thread_num = (int)ThreadNum.Value;
            if (!isHeadless && IntervalNum != null) interval = (float)IntervalNum.Value;

            int from = fromChapter.HasValue ? fromChapter.Value - 1 : 0;
            int to = toChapter.HasValue ? toChapter.Value : int.MaxValue;

            // Declare imageNo here so it's accessible to both EPUB and HTML-TXT paths
            int imageNo = 1;

            // Run the download logic in a Task to keep it asynchronous
            Task downloadTask = Task.Run(() => // Store the task in a variable
            {
                int chapterNo = 0;
                int page = 0;
                var chapterIds = new List<string>();
                var chapterNames = new List<(string, string)>();
                List<Thread> threads = new List<Thread>();
                bool get_content = true;
                while (get_content)
                {
                    string data = $"novel_no={novelNo}&sort=DOWN&page={page}";
                    string resp = PostRequest(log, $"https://novelpia.com/proc/episode_list", novelpia.loginkey, data);
                    if (resp == null || resp.Contains("본인인증"))
                    {
                        log("Authentication failed or content not available. Exiting download.\r\n");
                        get_content = false;
                        break;
                    }
                    var chapters = Regex.Matches(resp, @"id=""bookmark_(\d+)""></i>(.+?)</b>");
                    if (chapters.Count == 0 || chapterIds.Contains(chapters[0].Groups[1].Value))
                        break;
                    foreach (Match chapter in chapters)
                    {
                        if (chapterNo < from)
                        {
                            chapterNo++;
                            continue;
                        }
                        if (chapterNo >= to)
                        {
                            get_content = false;
                            break;
                        }
                        string chapterId = chapter.Groups[1].Value;
                        string chapterName = chapter.Groups[2].Value;
                        string jsonPath = Path.Combine(directory, $"{chapterNo.ToString().PadLeft(4, '0')}.json");
                        threads.Add(new Thread(() => DownloadChapter(log, chapterId, chapterName, jsonPath)));
                        chapterNames.Add((HttpUtility.HtmlEncode(chapterName), jsonPath));
                        chapterIds.Add(chapterId);
                        chapterNo++;
                    }
                    page++;
                }

                ExecuteThreads(threads, thread_num, interval);
                threads.Clear();

                if (saveAsEpub)
                {
                    // EPUB creation logic (unchanged, as it's not affected by HtmlCheckBox)
                    Directory.CreateDirectory(Path.Combine(directory, "META-INF"));
                    Directory.CreateDirectory(Path.Combine(directory, "OEBPS/Styles"));
                    Directory.CreateDirectory(Path.Combine(directory, "OEBPS/Text"));
                    Directory.CreateDirectory(Path.Combine(directory, "OEBPS/Images"));
                    using (var file = new StreamWriter(Path.Combine(directory, "mimetype"), false))
                        file.Write("application/epub+zip");
                    using (var file = new StreamWriter(Path.Combine(directory, "META-INF/container.xml"), false))
                        file.Write(EpubTemplate.container);
                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/Styles/sgc-toc.css"), false))
                        file.Write(EpubTemplate.sgctoc);
                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/Styles/Stylesheet.css"), false))
                        file.Write(EpubTemplate.stylesheet);
                    string responseText;
                    var request = (HttpWebRequest)WebRequest.Create($"https://novelpia.com/novel/{novelNo}");
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
                    request.Headers.Add("cookie", $"LOGINKEY={novelpia.loginkey};");
                    var response = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = streamReader.ReadToEnd();
                    }

                    var match = Regex.Match(responseText, @"productName = '(.+?)';");
                    string title = match.Groups[1].Value;

                    var authorMatch = Regex.Match(responseText, @"<a class=""writer-name""[^>]*>\s*(.+?)\s*</a>");
                    string author = authorMatch.Success ? authorMatch.Groups[1].Value.Trim() : "Unknown Author";
                    var tagMatches = Regex.Matches(responseText, @"<span class=""tag"".*?>(#.+?)</span>");
                    List<string> tags = new List<string>();
                    foreach (Match tagMatchItem in tagMatches)
                    {
                        tags.Add(tagMatchItem.Groups[1].Value.TrimStart('#'));
                    }
                    tags = tags.Distinct().ToList();

                    var synopsisMatch = Regex.Match(responseText, @"<div class=""synopsis"">(.*?)</div>", RegexOptions.Singleline);
                    string synopsis = synopsisMatch.Success ? HttpUtility.HtmlDecode(synopsisMatch.Groups[1].Value.Trim()) : "No synopsis available.";

                    string status = "";
                    var completionMatch = Regex.Match(responseText, @"<span class=""b_comp s_inv"">(.+?)</span>");
                    if (completionMatch.Success)
                    {
                        status = completionMatch.Groups[1].Value.Trim();
                    }
                    else
                    {
                        var suspensionMatch = Regex.Match(responseText, @"<span class=""s_inv"" style="".*?"">연재중단</span>");
                        if (suspensionMatch.Success)
                        {
                            status = "연재중단";
                        }
                    }

                    match = Regex.Match(responseText, @"href=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                    string url = match.Groups[1].Value;
                    if (string.IsNullOrEmpty(url))
                    {
                        match = Regex.Match(responseText, @"src=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                        url = match.Groups[1].Value;
                    }

                    string cover_url = url;
                    threads.Add(new Thread(() => DownloadImage(log, cover_url,
                        Path.Combine(directory, $"OEBPS/Images/cover.jpg"), "커버")));

                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/toc.ncx"), false))
                    {
                        file.Write(EpubTemplate.toc);
                        file.Write($"<text>{title}</text>\n</docTitle>\n<navMap>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            file.Write($"<navPoint id=\"navPoint-{i + 1}\" playOrder=\"{i + 1}\">\n" +
                                "<navLabel>\n" +
                                $"<text>{chapterNames[i].Item1}</text>\n" +
                                "</navLabel>\n" +
                                $"<content src=\"Text/chapter{Path.ChangeExtension(Path.GetFileName(chapterNames[i].Item2), "html")}\" />\n" +
                                "</navPoint>\n");
                        }
                        file.Write("</navMap>\n</ncx>\n");
                    }

                    // imageNo is already declared at the method level
                    using (var file = new StreamWriter(Path.Combine(directory, $"OEBPS/Text/cover.html"), false))
                    {
                        file.Write(EpubTemplate.cover);
                        file.Write($"<h1>{HttpUtility.HtmlEncode(title)}</h1>\n");
                        file.Write($"<p><strong>Author:</strong> {HttpUtility.HtmlEncode(author)}</p>\n");
                        if (tags.Count > 0)
                        {
                            file.Write("<p><strong>Tags:</strong> ");
                            file.Write(string.Join(", ", tags.Select(t => HttpUtility.HtmlEncode(t))));
                            file.Write("</p>\n");
                        }
                        if (!string.IsNullOrEmpty(status))
                        {
                            file.Write($"<p><strong>Status:</strong> {HttpUtility.HtmlEncode(status)}</p>\n");
                        }
                        file.Write($"<h2>Synopsis</h2>\n");
                        file.Write($"{synopsis}\n");
                        file.Write("<p>&nbsp;</p>\n");
                        file.Write("</body>\n</html>\n");
                    }

                    chapterNames.ForEach(s =>
                    {
                        if (!File.Exists(s.Item2))
                            return;
                        string temp = Path.ChangeExtension(Path.GetFileName(s.Item2), "html");
                        using (var file = new StreamWriter(Path.Combine(directory, $"OEBPS/Text/chapter{temp}"), false))
                        {
                            file.Write(EpubTemplate.chapter);
                            file.Write($"<h1>{s.Item1}</h1>\n<p>&nbsp;</p>\n");
                            var serializer = new JavaScriptSerializer();
                            using (var reader = new StreamReader(s.Item2, Encoding.UTF8))
                            {
                                var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                foreach (var text in (ArrayList)texts["s"])
                                {
                                    var textDict = (Dictionary<string, object>)text;
                                    string textStr = (string)textDict["text"];

                                    textStr = HttpUtility.HtmlDecode(textStr);
                                    textStr = Regex.Replace(textStr, @"\sid=""docs-internal-guid-[^""]*""", "");
                                    textStr = Regex.Replace(textStr, @"<p style='height: 0px; width: 0px;.+?>.*?</p>", "");

                                    match = Regex.Match(textStr, @"<img.+?src=\""(.+?)\"".+?>");
                                    if (match.Success)
                                    {
                                        if (!textStr.Contains("cover-wrapper"))
                                        {
                                            url = match.Groups[1].Value;
                                            int currentImageNo = imageNo; // Use a local variable to capture imageNo for the thread
                                            threads.Add(new Thread(() => DownloadImage(log, url,
                                                Path.Combine(directory, $"OEBPS/Images/{currentImageNo}.jpg"), "삽화")));
                                            textStr = Regex.Replace(textStr, @"<img.+?src=\"".+?\"".+?>",
                                                $"<img alt=\"{currentImageNo}\" src=\"../Images/{currentImageNo}.jpg\" width=\"100%\"/>");
                                            imageNo++; // Increment the shared imageNo counter
                                        }
                                        continue;
                                    }

                                    if (string.IsNullOrEmpty(textStr.Trim()))
                                    {
                                        file.Write("<p>&nbsp;</p>\n");
                                    }
                                    else
                                    {
                                        bool alreadyContainsParagraphs = Regex.IsMatch(textStr, @"<p\b[^>]*>.*?</p>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                                        if (alreadyContainsParagraphs)
                                        {
                                            file.Write($"{textStr}\n");
                                        }
                                        else
                                        {
                                            string[] lines = textStr.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
                                            foreach (string line in lines)
                                            {
                                                string trimmedLine = line.Trim();
                                                if (string.IsNullOrEmpty(trimmedLine))
                                                {
                                                    file.Write("<p>&nbsp;</p>\n");
                                                }
                                                else
                                                {
                                                    string encodedLine = Regex.Replace(trimmedLine, "(<[^>]+>|&[^;]+;)|([^<>&]+)",
                                                                         m => m.Groups[1].Success ? m.Value : HttpUtility.HtmlEncode(m.Groups[2].Value));
                                                    file.Write($"<p>{encodedLine}</p>\n");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        File.Delete(s.Item2);
                    });

                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/content.opf"), false))
                    {
                        file.Write(EpubTemplate.content1);
                        file.Write($"<dc:title>{title}</dc:title>\n");
                        file.Write($"<dc:creator opf:role=\"aut\">{HttpUtility.HtmlEncode(author)}</dc:creator>\n");
                        file.Write($"<dc:description>{HttpUtility.HtmlEncode(synopsis)}</dc:description>\n");
                        foreach (string tag in tags)
                        {
                            file.Write($"<dc:subject>{HttpUtility.HtmlEncode(tag)}</dc:subject>\n");
                        }
                        if (!string.IsNullOrEmpty(status))
                        {
                            file.Write($"<dc:subject>{HttpUtility.HtmlEncode(status)}</dc:subject>\n");
                        }
                        file.Write(EpubTemplate.content2);
                        file.Write("<item id=\"cover.html\" href=\"Text/cover.html\" media-type=\"application/xhtml+xml\"/>\n");
                        file.Write("<item id=\"cover-image\" href=\"Images/cover.jpg\" media-type=\"image/jpeg\" properties=\"cover-image\"/>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            string temp = Path.ChangeExtension(Path.GetFileName(chapterNames[i].Item2), "html");
                            file.Write($"<item id=\"chapter{temp}\" href=\"Text/chapter{temp}\" media-type=\"application/xhtml+xml\"/>\n");
                        }
                        for (int i = 1; i < imageNo; i++) // Loop up to the final imageNo value
                        {
                            file.Write($"<item id=\"{i}.jpg\" href=\"Images/{i}.jpg\" media-type=\"image/jpeg\"/>\n");
                        }
                        file.Write("</manifest>\n<spine toc=\"ncx\">\n<itemref idref=\"cover.html\"/>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            string temp = Path.ChangeExtension(Path.GetFileName(chapterNames[i].Item2), "html");
                            file.Write($"<itemref idref=\"chapter{temp}\"/>\n");
                        }
                        file.Write("</spine>\n<guide>\n" +
                            "<reference type=\"cover\" title=\"Cover\" href=\"Text/cover.html\"/>\n" +
                            "</guide>\n</package>\n");
                    }

                    if (File.Exists(path))
                        File.Delete(path);

                    ExecuteThreads(threads, thread_num, interval);

                    ZipFile.CreateFromDirectory(directory, path);
                }
                else // Save as .txt
                {
                    bool includeHtml = includeHtmlInTxt;

                    using (var file = new StreamWriter(path, false))
                    {
                        var serializer = new JavaScriptSerializer();

                        if (includeHtml)
                        {
                            file.Write(EpubTemplate.chapter);
                            if (chapterNames.Any())
                            {
                                file.Write($"<h1>{chapterNames[0].Item1}</h1>\n<p>&nbsp;</p>\n");
                            }
                        }

                        chapterNames.ForEach(s =>
                        {
                            if (!includeHtml)
                            {
                                file.Write($"{s.Item1}\n\n");
                            }

                            if (!File.Exists(s.Item2))
                                return;
                            using (var reader = new StreamReader(s.Item2, Encoding.UTF8))
                            {
                                var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                foreach (var text in (ArrayList)texts["s"])
                                {
                                    var textDict = (Dictionary<string, object>)text;
                                    string textStr = (string)textDict["text"];
                                    if (textStr.Contains("cover-wrapper"))
                                        continue;

                                    textStr = HttpUtility.HtmlDecode(textStr);
                                    textStr = Regex.Replace(textStr, @"<p style='height: 0px; width: 0px;.+?>.*?</p>", "");

                                    if (!includeHtml)
                                    {
                                        textStr = Regex.Replace(textStr, @"</?[^>]+>|\n", "");
                                    }

                                    if (string.IsNullOrEmpty(textStr.Trim()))
                                    {
                                        if (includeHtml) file.Write("<p>&nbsp;</p>\n");
                                        continue;
                                    }
                                    if (font_mapping != null)
                                        textStr = font_mapping.DecodeText(textStr);

                                    if (includeHtml)
                                    {
                                        bool alreadyContainsParagraphs = Regex.IsMatch(textStr, @"<p\b[^>]*>.*?</p>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                                        if (alreadyContainsParagraphs)
                                        {
                                            Match match = Regex.Match(textStr, @"<img.+?src=\""(.+?)\"".+?>");
                                            if (match.Success && !textStr.Contains("cover-wrapper"))
                                            {
                                                string url = match.Groups[1].Value;
                                                int currentImageNo = imageNo; // Use a local variable to capture imageNo for the thread
                                                threads.Add(new Thread(() => DownloadImage(log, url,
                                                    Path.Combine(directory, $"OEBPS/Images/{currentImageNo}.jpg"), "삽화")));
                                                textStr = Regex.Replace(textStr, @"<img.+?src=\"".+?\"".+?>",
                                                    $"<img alt=\"{currentImageNo}\" src=\"../Images/{currentImageNo}.jpg\" width=\"100%\"/>");
                                                imageNo++; // Increment the shared imageNo counter
                                            }
                                            file.Write($"{textStr}\n");
                                        }
                                        else
                                        {
                                            string[] lines = textStr.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
                                            foreach (string line in lines)
                                            {
                                                string trimmedLine = line.Trim();
                                                if (string.IsNullOrEmpty(trimmedLine))
                                                {
                                                    file.Write("<p>&nbsp;</p>\n");
                                                }
                                                else
                                                {
                                                    Match match = Regex.Match(trimmedLine, @"<img.+?src=\""(.+?)\"".+?>");
                                                    if (match.Success && !trimmedLine.Contains("cover-wrapper"))
                                                    {
                                                        string url = match.Groups[1].Value;
                                                        int currentImageNo = imageNo; // Use a local variable to capture imageNo for the thread
                                                        threads.Add(new Thread(() => DownloadImage(log, url,
                                                            Path.Combine(directory, $"OEBPS/Images/{currentImageNo}.jpg"), "삽화")));
                                                        trimmedLine = Regex.Replace(trimmedLine, @"<img.+?src=\"".+?\"".+?>",
                                                            $"<img alt=\"{currentImageNo}\" src=\"../Images/{currentImageNo}.jpg\" width=\"100%\"/>");
                                                        imageNo++; // Increment the shared imageNo counter
                                                    }

                                                    string encodedLine = Regex.Replace(trimmedLine, "(<[^>]+>|&[^;]+;)|([^<>&]+)",
                                                                         m => m.Groups[1].Success ? m.Value : HttpUtility.HtmlEncode(m.Groups[2].Value));
                                                    file.Write($"<p>{encodedLine}</p>\n");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        file.WriteLine(textStr);
                                    }
                                }
                            }
                            if (!includeHtml) file.Write("\n");
                            File.Delete(s.Item2);
                        });

                        if (includeHtml)
                        {
                            file.Write("</body>\n</html>\n");
                        }
                    }
                }
                Directory.Delete(directory, true);
                log("다운로드 완료!\r\n");
            }); // Removed .Wait() here for UI mode

            // If in headless mode, wait for the task to complete
            if (isHeadless)
            {
                downloadTask.Wait();
            }
        }

        /// <summary>
        /// Core batch download logic, used by both UI and headless modes.
        /// </summary>
        /// <param name="listFilePath">Path to the text file containing novel titles and IDs.</param>
        /// <param name="outputDirectory">Directory where downloaded novels will be saved.</param>
        /// <param name="includeHtmlInTxt">True to include raw HTML in TXT output, false for plain text.</param>
        /// <param name="isHeadless">True if running in headless mode (logs to Console), false for UI mode (logs to ConsoleBox).</param>
        public void BatchDownloadCore(string listFilePath, string outputDirectory, bool includeHtmlInTxt, bool isHeadless = false)
        {
            Action<string> log = (msg) =>
            {
                if (isHeadless)
                {
                    Console.WriteLine(msg);
                }
                else
                {
                    if (ConsoleBox != null && ConsoleBox.InvokeRequired)
                    {
                        ConsoleBox.Invoke(new Action(() => ConsoleBox.AppendText(msg)));
                    }
                    else if (ConsoleBox != null)
                    {
                        ConsoleBox.AppendText(msg);
                    }
                }
            };

            log($"Starting batch download from: {listFilePath}\r\n");
            Task batchDownloadTask = Task.Run(() => // Store the task in a variable
            {
                try
                {
                    if (!File.Exists(listFilePath))
                    {
                        log($"Error: List file not found at {listFilePath}\r\n");
                        return;
                    }
                    if (!Directory.Exists(outputDirectory))
                    {
                        Directory.CreateDirectory(outputDirectory);
                    }

                    string[] lines = File.ReadAllLines(listFilePath);

                    foreach (string line in lines)
                    {
                        string trimmedLine = line.Trim();
                        if (string.IsNullOrEmpty(trimmedLine))
                        {
                            continue; // Skip empty lines
                        }

                        string[] parts = trimmedLine.Split(',');
                        if (parts.Length == 2)
                        {
                            string title = parts[0].Trim();
                            string novelId = parts[1].Trim();

                            string safeTitle = SanitizeFilename(title);

                            // For batch download, assume .txt. EPUB would require another parameter.
                            bool saveAsEpub = false; // Default to TXT for headless batch unless explicitly passed

                            string fileExtension = ".txt"; // Always .txt for batch for now, HTML content controlled by includeHtmlInTxt

                            string outputPath = Path.Combine(outputDirectory, $"{safeTitle}{fileExtension}");

                            log($"Attempting to download '{title}' (ID: {novelId}) to {outputPath}\r\n");

                            // Call the core single download method for each novel
                            DownloadCore(
                                novelId,
                                saveAsEpub,
                                outputPath,
                                null, // No chapter range for batch items from list file
                                null, // No chapter range for batch items from list file
                                includeHtmlInTxt, // Pass the HTML checkbox state
                                isHeadless // Pass the headless flag
                            );

                            log($"Finished downloading '{title}'\r\n");
                            Thread.Sleep(2000); // 2 seconds delay between each novel download
                        }
                        else
                        {
                            log($"Skipping malformed line in list file: {trimmedLine}\r\n");
                        }
                    }
                    log("Batch download completed!\r\n");
                }
                catch (Exception ex)
                {
                    log($"An error occurred during batch download: {ex.Message}\r\n");
                }
            });

            // If in headless mode, wait for the task to complete
            if (isHeadless)
            {
                batchDownloadTask.Wait();
            }
        }


        // Helper method for downloading a single chapter's JSON data
        private void DownloadChapter(Action<string> log, string chapterId, string chapterName, string jsonPath)
        {
            try
            {
                string resp = PostRequest(log, $"https://novelpia.com/proc/viewer_data/{chapterId}", novelpia.loginkey);
                if (string.IsNullOrEmpty(resp) || resp.Contains("본인인증"))
                    throw new Exception("Authentication failed or content not available.");
                using (var file = new StreamWriter(jsonPath, false))
                    file.Write(resp);
                log(chapterName + "\r\n");
            }
            catch (Exception ex)
            {
                log($"{chapterName} ERROR! {ex.Message}\r\n");
            }
        }

        // Helper method for downloading an image
        private void DownloadImage(Action<string> log, string url, string path, string type)
        {
            if (!url.StartsWith("http"))
                url = "https:" + url;
            log($"{type} 다운로드 시작\r\n{url}\r\n");
            try
            {
                using (var downloader = new WebClient())
                    downloader.DownloadFile(url, path);
                log($"{type} 다운로드 완료!\r\n");
            }
            catch (Exception ex)
            {
                log($"{type} 다운로드 실패! {ex.Message}\r\n{url}\r\n");
            }
        }

        private void ExecuteThreads(List<Thread> threads, int batch_size, float interval)
        {
            for (int i = 0; i < threads.Count; i += batch_size)
            {
                int remain = threads.Count - i;
                int limit = batch_size < remain ? batch_size : remain;
                for (int j = 0; j < limit; j++)
                    threads[i + j].Start();
                for (int j = 0; j < limit; j++)
                    threads[i + j].Join();
                Thread.Sleep((int)(interval * 1000));
            }
        }

        // Modified PostRequest to accept a logging action
        private static string PostRequest(Action<string> log, string url, string loginkey, string data = null)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("cookie", $"LOGINKEY={loginkey};");
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
            catch (WebException ex)
            {
                log($"Web request error for {url}: {ex.Message}\r\n");
                if (ex.Response != null)
                {
                    using (var errorStream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(errorStream))
                    {
                        log($"Response: {reader.ReadToEnd()}\r\n");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                log($"An unexpected error occurred in PostRequest for {url}: {ex.Message}\r\n");
                return null;
            }
        }

        // --- UI Event Handlers ---
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            bool saveAsEpub = EpubButton.Checked;
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = saveAsEpub ? "|*.epub" : "|*.txt"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DownloadCore(
                    NovelNoText.Text,
                    saveAsEpub,
                    sfd.FileName,
                    FromCheck.Checked ? (int?)FromNum.Value : null,
                    ToCheck.Checked ? (int?)ToNum.Value : null,
                    HtmlCheckBox.Checked,
                    false // isHeadless = false for UI mode
                );
            }
            sfd.Dispose();
        }

        private void LoginButton1_Click(object sender, EventArgs e)
        {
            string email = EmailText.Text;
            string password = PasswordText.Text;
            if (novelpia.Login(email, password))
            {
                ConsoleBox.AppendText("로그인 성공!\r\n");
                LoginkeyText.Text = novelpia.loginkey;
            }
            else
            {
                ConsoleBox.AppendText("로그인 실패!\r\n");
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
                { "thread_num", ThreadNum.Value },
                { "interval_num", IntervalNum.Value },
                { "email", EmailText.Text },
                { "wd", PasswordText.Text },
                { "loginkey", LoginkeyText.Text },
                { "mapping_path", FontBox.Text },
                { "include_html_in_txt", HtmlCheckBox.Checked }
            };
            using (StreamWriter sw = new StreamWriter("config.json"))
            {
                sw.Write(new JavaScriptSerializer().Serialize(config_dict));
            }
        }

        private void FromCheck_CheckedChanged(object sender, EventArgs e)
        {
            FromNum.Enabled = FromLabel.Enabled = FromCheck.Checked;
        }

        private void ToCheck_CheckedChanged(object sender, EventArgs e)
        {
            ToNum.Enabled = ToLabel.Enabled = ToCheck.Checked;
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

        private void ExtensionLabel_Click(object sender, EventArgs e)
        {

        }

        private void HtmlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // No direct action needed here, state is read in DownloadCore
        }

        private void BatchDownloadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files|*.txt",
                Title = "Select the Novel List File"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string listFilePath = openFileDialog.FileName;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Select the output directory for downloaded novels"
            };

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string outputDirectory = folderBrowserDialog.SelectedPath;
            bool includeHtml = HtmlCheckBox.Checked;

            Task.Run(() =>
            {
                BatchDownloadCore(listFilePath, outputDirectory, includeHtml, false); // isHeadless = false for UI mode
                // For UI mode, provide feedback that the process has started and to check console (or ConsoleBox)
                if (ConsoleBox != null && ConsoleBox.InvokeRequired)
                {
                    ConsoleBox.Invoke(new Action(() => ConsoleBox.AppendText("Batch download process initiated. Check console for details.\r\n")));
                }
                else if (ConsoleBox != null)
                {
                    ConsoleBox.AppendText("Batch download process initiated. Check console for details.\r\n");
                }
            });
        }

        /// <summary>
        /// Sanitizes a string to be used as a valid filename.
        /// Removes invalid characters and replaces them with a safe alternative.
        /// </summary>
        /// <param name="filename">The original string.</param>
        /// <returns>A sanitized string suitable for a filename.</returns>
        private string SanitizeFilename(string filename)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"[{0}]", invalidChars);
            return Regex.Replace(filename, invalidRegStr, "_");
        }
    }
}
