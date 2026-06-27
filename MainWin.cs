using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            BonusNeverCheck.CheckedChanged += (s, e) => { if (BonusNeverCheck.Checked && BonusAlwaysCheck.Checked) BonusAlwaysCheck.Checked = false; };
            BonusAlwaysCheck.CheckedChanged += (s, e) => { if (BonusAlwaysCheck.Checked && BonusNeverCheck.Checked) BonusNeverCheck.Checked = false; };

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
                if (config_dict.ContainsKey("include_novel_no"))
                    IncludeNovelNoCheck.Checked = config_dict["include_novel_no"];
                if (config_dict.ContainsKey("include_chapter_range"))
                    IncludeChapterRangeCheck.Checked = config_dict["include_chapter_range"];
                if (config_dict.ContainsKey("vertical"))
                    VerticalCheck.Checked = config_dict["vertical"];
                if (config_dict.ContainsKey("gothic"))
                    GothicCheck.Checked = config_dict["gothic"];
                if (config_dict.ContainsKey("bonus_never"))
                    BonusNeverCheck.Checked = config_dict["bonus_never"];
                if (config_dict.ContainsKey("bonus_always"))
                    BonusAlwaysCheck.Checked = config_dict["bonus_always"];
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
        private readonly List<DownloadJob> _queue = new List<DownloadJob>();
        private bool _queueRunning;

        private class DownloadJob
        {
            public string novelNo;
            public string title;
            public bool saveAsEpub, includeNotice, removeBlank, keepHtml, compress, downloadImage, stopOnError;
            public bool includeNovelNoInName, includeChapterRangeInName;
            public bool vertical, gothic;
            public bool bonusNever, bonusAlways;
            public bool fromChecked, toChecked;
            public int fromN, toN;
            public int retry, threadNum;
            public float interval;
            public string outputDir;
            public string targetPath;
        }
        private int _progress_total;
        private int _progress_done;
        private int _progress_fail;
        private int _progress_skip;
        private int _lastLineStart = -1;
        private long _lastProgressTicks;

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                _cancelRequested = true;
                DownloadButton.Enabled = false;
                DownloadButton.Text = Lang.T("btn_stopping");
                return;
            }
            var job = BuildJobFromUI();
            if (job == null) return;
            if (!ResolveTargetPath(job, askIfMissing: true)) return;
            StartSingleJob(job, onAllDone: null);
        }

        private DownloadJob BuildJobFromUI()
        {
            var noMatch = Regex.Match(NovelNoText.Text ?? "", @"\d+");
            if (!noMatch.Success) return null;
            var job = new DownloadJob
            {
                novelNo = noMatch.Value,
                saveAsEpub = EpubButton.Checked,
                includeNotice = NoticeCheck.Checked,
                removeBlank = RemoveBlankCheck.Checked,
                keepHtml = KeepHtmlCheck.Checked,
                compress = CompressCheck.Checked,
                downloadImage = DownloadImageCheck.Checked,
                stopOnError = StopOnErrorCheck.Checked,
                includeNovelNoInName = IncludeNovelNoCheck.Checked,
                includeChapterRangeInName = IncludeChapterRangeCheck.Checked,
                vertical = VerticalCheck.Checked,
                gothic = GothicCheck.Checked,
                bonusNever = BonusNeverCheck.Checked,
                bonusAlways = BonusAlwaysCheck.Checked,
                fromChecked = FromCheck.Checked,
                toChecked = ToCheck.Checked,
                fromN = (int)FromNum.Value,
                toN = (int)ToNum.Value,
                retry = (int)RetryNum.Value,
                threadNum = (int)ThreadNum.Value,
                interval = (float)IntervalNum.Value,
                outputDir = OutputDirText.Text.Trim(),
            };
            return job;
        }

        private bool ResolveTargetPath(DownloadJob job, bool askIfMissing)
        {
            string ext = job.saveAsEpub ? ".epub" : ".txt";
            if (!string.IsNullOrEmpty(job.outputDir) && Directory.Exists(job.outputDir))
            {
                string title = FetchNovelTitle(job.novelNo);
                if (string.IsNullOrEmpty(title)) title = job.novelNo;
                foreach (char c in Path.GetInvalidFileNameChars())
                    title = title.Replace(c, '_');
                job.title = title;
                if (job.includeNovelNoInName)
                    title = $"{job.novelNo}_{title}";
                if (job.includeChapterRangeInName && (job.fromChecked || job.toChecked))
                {
                    int fromN = job.fromChecked ? job.fromN : 0;
                    string toN = job.toChecked ? job.toN.ToString() : "end";
                    title = $"{title}_{fromN}-{toN}";
                }
                job.targetPath = Path.Combine(job.outputDir, title + ext);
                return true;
            }
            if (!askIfMissing) return false;
            string targetPath = null;
            using (var sfd = new SaveFileDialog { Filter = job.saveAsEpub ? "|*.epub" : "|*.txt" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    targetPath = sfd.FileName;
            }
            if (targetPath == null) return false;
            job.targetPath = targetPath;
            if (string.IsNullOrEmpty(job.title))
                job.title = Path.GetFileNameWithoutExtension(targetPath);
            return true;
        }

        private void StartSingleJob(DownloadJob job, Action onAllDone)
        {
            _running = true;
            _cancelRequested = false;
            DownloadButton.Text = Lang.T("btn_stop");
            Download(job, onAllDone);
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

        private void AddToListButton_Click(object sender, EventArgs e)
        {
            var job = BuildJobFromUI();
            if (job == null) return;
            foreach (var existing in _queue)
            {
                if (existing.novelNo == job.novelNo &&
                    existing.fromChecked == job.fromChecked && existing.toChecked == job.toChecked &&
                    existing.fromN == job.fromN && existing.toN == job.toN &&
                    existing.saveAsEpub == job.saveAsEpub &&
                    existing.vertical == job.vertical && existing.gothic == job.gothic &&
                    existing.bonusNever == job.bonusNever && existing.bonusAlways == job.bonusAlways)
                {
                    Log(Lang.T("queue_dup", FormatJobLabel(job)));
                    return;
                }
            }
            if (string.IsNullOrEmpty(job.title))
            {
                string fetched = FetchNovelTitle(job.novelNo);
                job.title = string.IsNullOrEmpty(fetched) ? job.novelNo : fetched;
            }
            _queue.Add(job);
            DownloadList.Items.Add(FormatJobLabel(job));
            Log(Lang.T("queue_added", FormatJobLabel(job)));
        }

        private static string FormatJobLabel(DownloadJob job)
        {
            string range;
            if (job.fromChecked || job.toChecked)
            {
                string a = job.fromChecked ? job.fromN.ToString() : "0";
                string b = job.toChecked ? job.toN.ToString() : "end";
                range = $"  ({a}-{b})";
            }
            else range = "";
            string ext = job.saveAsEpub ? "epub" : "txt";
            string title = string.IsNullOrEmpty(job.title) ? job.novelNo : job.title;
            return $"[{job.novelNo}] {title}{range}  ·  {ext}";
        }

        private void QueueDeleteAllButton_Click(object sender, EventArgs e)
        {
            if (_queueRunning || _running) return;
            _queue.Clear();
            DownloadList.Items.Clear();
        }

        private void QueueDeleteSelectedButton_Click(object sender, EventArgs e)
        {
            if (_queueRunning || _running) return;
            var indices = new List<int>();
            foreach (int idx in DownloadList.SelectedIndices) indices.Add(idx);
            indices.Sort();
            for (int i = indices.Count - 1; i >= 0; i--)
            {
                int k = indices[i];
                if (k < 0 || k >= _queue.Count) continue;
                _queue.RemoveAt(k);
                DownloadList.Items.RemoveAt(k);
            }
        }

        private void QueueDownloadButton_Click(object sender, EventArgs e)
        {
            if (_running || _queueRunning) return;
            if (_queue.Count == 0) return;
            _queueRunning = true;
            QueueDownloadButton.Enabled = false;
            QueueDeleteAllButton.Enabled = false;
            QueueDeleteSelectedButton.Enabled = false;
            AddToListButton.Enabled = false;
            var snapshot = new List<DownloadJob>(_queue);
            Log(Lang.T("queue_start", snapshot.Count));
            RunQueueChain(snapshot, 0);
        }

        private void RunQueueChain(List<DownloadJob> jobs, int index)
        {
            if (index >= jobs.Count || _cancelRequested)
            {
                FinishQueue();
                return;
            }
            var job = jobs[index];
            if (!ResolveTargetPath(job, askIfMissing: false))
            {
                Log(Lang.T("queue_skip", FormatJobLabel(job)));
                RunQueueChain(jobs, index + 1);
                return;
            }
            Log(Lang.T("queue_book_header", index + 1, jobs.Count, FormatJobLabel(job)) + "\r\n");
            StartSingleJob(job, onAllDone: () => RunQueueChain(jobs, index + 1));
        }

        private void FinishQueue()
        {
            _queueRunning = false;
            QueueDownloadButton.Enabled = true;
            QueueDeleteAllButton.Enabled = true;
            QueueDeleteSelectedButton.Enabled = true;
            AddToListButton.Enabled = true;
            Log(Lang.T("queue_done"));
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

        void Download(DownloadJob job, Action onAllDone)
        {
            string novelNo = job.novelNo;
            bool saveAsEpub = job.saveAsEpub;
            bool includeNotice = job.includeNotice;
            bool removeBlank = job.removeBlank;
            bool keepHtml = job.keepHtml;
            bool compress = job.compress;
            bool downloadImage = job.downloadImage;
            bool vertical = job.vertical;
            bool gothic = job.gothic;
            bool bonusNever = job.bonusNever;
            bool bonusAlways = job.bonusAlways;
            int retry = job.retry;
            string path = job.targetPath;
            Log(Lang.T("sep") + Lang.T("download_start"));
            string directory = Path.Combine(Path.GetDirectoryName(path), novelNo);
            Directory.CreateDirectory(directory);
            int thread_num = job.threadNum;
            float interval = job.interval;
            int from = job.fromChecked ? job.fromN : 0;
            int to = job.toChecked ? job.toN : int.MaxValue;
            _stopOnError = job.stopOnError;
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
                    var chapterHtmls = new ConcurrentDictionary<string, string>();
                    var images = new ImageContext();
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
                                string encodedName = HttpUtility.HtmlEncode($"[{Lang.T("notice")}] {chapterName}");
                                threads.Add(new Thread(() =>
                                {
                                    DownloadChapter(capturedId, capturedName, capturedPath, "");
                                    if (saveAsEpub && File.Exists(capturedPath))
                                    {
                                        string html = BuildChapterHtml(encodedName, capturedPath, keepHtml, removeBlank, downloadImage, images);
                                        if (html != null)
                                            chapterHtmls[capturedPath] = html;
                                    }
                                }));
                                chapterNames.Add((encodedName, capturedPath));
                                noticeNo++;
                            }
                        }
                    }
                    bool get_content = true;
                    int bonusNo = 1;
                    int maxEp = 0;
                    Log(Lang.T("fetching_chapters") + "\r\n");
                    int chaptersFoundLogStart = -1;
                    if (InvokeRequired)
                        Invoke(new Action(() => { chaptersFoundLogStart = ConsoleBox.TextLength; ConsoleBox.AppendText(Lang.T("chapter_list_progress", 0)); ConsoleBox.SelectionStart = ConsoleBox.TextLength; ConsoleBox.ScrollToCaret(); }));
                    else
                    { chaptersFoundLogStart = ConsoleBox.TextLength; ConsoleBox.AppendText(Lang.T("chapter_list_progress", 0)); ConsoleBox.SelectionStart = ConsoleBox.TextLength; ConsoleBox.ScrollToCaret(); }
                    while (get_content)
                    {
                        if (_cancelRequested) break;
                        string data = $"novel_no={novelNo}&sort=DOWN&page={page}";
                        string resp = PostRequest("https://novelpia.com/proc/episode_list", novelpia.loginkey, data, "https://novelpia.com/");
                        var chapters = Regex.Matches(resp, @"id=""bookmark_(\d+)""></i>(.+?)</b>.+?>(EP\.(\d+)|BONUS)<", RegexOptions.Singleline);
                        if (chapters.Count == 0)
                            break;
                        if (seenChapterIds.Contains(chapters[0].Groups[1].Value))
                            break;
                        foreach (Match chapter in chapters)
                        {
                            string kind = chapter.Groups[3].Value;
                            bool isBonus = kind == "BONUS";
                            string chapterId = chapter.Groups[1].Value;
                            if (!seenChapterIds.Add(chapterId))
                                continue;
                            string jsonPath;
                            string label;
                            if (isBonus)
                            {
                                // BONUS chapters have no EP number; index them sequentially
                                // after the last real EP so a bounded "to EP" can reach them.
                                int bonusIdx = bonusNo++;
                                if (bonusNever)
                                    continue;
                                if (!bonusAlways)
                                {
                                    int virtualNo = maxEp + bonusIdx;
                                    if (virtualNo < from)
                                        continue;
                                    if (virtualNo > to)
                                    {
                                        get_content = false;
                                        break;
                                    }
                                }
                                jsonPath = Path.Combine(directory, $"bonus_{bonusIdx.ToString().PadLeft(4, '0')}.json");
                                label = "BONUS";
                            }
                            else
                            {
                                int chapterNo = int.Parse(chapter.Groups[4].Value);
                                if (chapterNo > maxEp) maxEp = chapterNo;
                                if (chapterNo < from)
                                    continue;
                                if (chapterNo > to)
                                {
                                    // When "always BONUS" is on, keep paginating past the
                                    // EP cap so the trailing BONUS chapters are still fetched.
                                    if (bonusAlways)
                                        continue;
                                    get_content = false;
                                    break;
                                }
                                jsonPath = Path.Combine(directory, $"{chapterNo.ToString().PadLeft(4, '0')}.json");
                                label = $"EP.{chapterNo:D4}";
                            }
                            string chapterName = chapter.Groups[2].Value;
                            string encodedName = HttpUtility.HtmlEncode(chapterName);
                            threads.Add(new Thread(() =>
                            {
                                DownloadChapter(chapterId, chapterName, jsonPath, label);
                                if (saveAsEpub && File.Exists(jsonPath))
                                {
                                    string html = BuildChapterHtml(encodedName, jsonPath, keepHtml, removeBlank, downloadImage, images);
                                    if (html != null)
                                        chapterHtmls[jsonPath] = html;
                                }
                            }));
                            chapterNames.Add((encodedName, jsonPath));
                        }
                        // refresh "chapter list progress" line
                        int foundCnt = chapterNames.Count;
                        int capturedStart = chaptersFoundLogStart;
                        BeginInvoke(new Action(() =>
                        {
                            int len = ConsoleBox.TextLength - capturedStart;
                            ConsoleBox.Select(capturedStart, len);
                            ConsoleBox.SelectedText = Lang.T("chapter_list_progress", foundCnt);
                            ConsoleBox.SelectionStart = ConsoleBox.TextLength;
                            ConsoleBox.ScrollToCaret();
                        }));
                        page++;
                    }
                    // finalize chapter list line
                    {
                        int foundCnt = chapterNames.Count;
                        int capturedStart = chaptersFoundLogStart;
                        if (InvokeRequired)
                            Invoke(new Action(() => { int len = ConsoleBox.TextLength - capturedStart; ConsoleBox.Select(capturedStart, len); ConsoleBox.SelectedText = Lang.T("chapter_list_done", foundCnt) + "\r\n"; ConsoleBox.SelectionStart = ConsoleBox.TextLength; ConsoleBox.ScrollToCaret(); }));
                        else
                        { int len = ConsoleBox.TextLength - capturedStart; ConsoleBox.Select(capturedStart, len); ConsoleBox.SelectedText = Lang.T("chapter_list_done", foundCnt) + "\r\n"; ConsoleBox.SelectionStart = ConsoleBox.TextLength; ConsoleBox.ScrollToCaret(); }
                    }

                    string title = null, author = null, titleEnc = null, authorEnc = null, cover_url = null, coverPath = null;
                    Thread coverThread = null;
                    if (saveAsEpub)
                    {
                        var match = Regex.Match(novelPageHtml, @"productName = '(.+?)';");
                        title = match.Groups[1].Value;
                        match = Regex.Match(novelPageHtml, @"<meta[^]>]+name=[""']author[""'][^]>]+content=[""']([^""']+)[""']");
                        author = match.Success ? match.Groups[1].Value : "";
                        titleEnc = HttpUtility.HtmlEncode(title);
                        authorEnc = HttpUtility.HtmlEncode(author);
                        match = Regex.Match(novelPageHtml, @"href=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                        string url = match.Groups[1].Value;
                        if (string.IsNullOrEmpty(url))
                        {
                            match = Regex.Match(novelPageHtml, @"src=""(//images\.novelpia\.com/imagebox/cover/.+?\.file)""");
                            url = match.Groups[1].Value;
                        }
                        cover_url = url;
                        coverPath = Path.Combine(directory, "cover.bin");
                        if (downloadImage && !_cancelRequested && !string.IsNullOrEmpty(cover_url))
                        {
                            coverThread = new Thread(() => DownloadImage(cover_url, coverPath, Lang.T("cover"), countProgress: false));
                            coverThread.Start();
                        }
                    }

                    BeginProgressPhase(threads.Count, Lang.T("phase_chapters"));
                    ExecuteThreads(threads, thread_num, interval);
                    EndProgressPhase(Lang.T("phase_chapters"));
                    threads.Clear();
                    coverThread?.Join();
                    if (_cancelRequested)
                        Log(Lang.T("cancelled"));
                    else if (_hadFatalError)
                        Log(Lang.T("stop_on_error"));

                    if (saveAsEpub)
                    {
                        var entries = new List<(string name, byte[] data)>();
                        var htmlNames = new List<string>(chapterNames.Count);

                        foreach (var s in chapterNames)
                        {
                            string htmlName = Path.ChangeExtension(Path.GetFileName(s.Item2), "html");
                            htmlNames.Add(htmlName);
                            if (!chapterHtmls.TryGetValue(s.Item2, out string html))
                                continue;
                            entries.Add(("OEBPS/Text/chapter" + htmlName, Encoding.UTF8.GetBytes(html)));
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

                        bool hasCover = downloadImage;

                        var opf = new StringBuilder();
                        opf.Append(EpubTemplate.content1);
                        opf.Append($"<dc:identifier id=\"BookId\" opf:scheme=\"NovelpiaNovelNo\">{novelNo}</dc:identifier>\n");
                        opf.Append("<dc:title>").Append(titleEnc).Append("</dc:title>\n");
                        opf.Append("<dc:language>ko</dc:language>\n");
                        if (!string.IsNullOrEmpty(author))
                            opf.Append("<dc:creator opf:role=\"aut\">").Append(authorEnc).Append("</dc:creator>\n");
                        opf.Append($"<dc:date>{DateTime.UtcNow:yyyy-MM-dd}</dc:date>\n");

                        string coverExt = "jpg";
                        string coverMime = "image/jpeg";
                        if (hasCover && File.Exists(coverPath))
                            DetectImageType(coverPath, out coverExt, out coverMime);
                        var imageExts = new Dictionary<int, string>();
                        var imageMimes = new Dictionary<int, string>();
                        foreach (var kv in images.Paths)
                        {
                            int no = kv.Key;
                            string ip = kv.Value;
                            if (File.Exists(ip) && DetectImageType(ip, out string ie, out string im))
                            { imageExts[no] = ie; imageMimes[no] = im; }
                            else { imageExts[no] = "jpg"; imageMimes[no] = "image/jpeg"; }
                        }
                        var orderedImageNos = images.Keys.OrderBy(k => k).ToArray();

                        for (int i = 0; i < entries.Count; i++)
                        {
                            var (n, d) = entries[i];
                            string s = Encoding.UTF8.GetString(d);
                            s = Regex.Replace(s, @"src=""\.\./Images/(\d+)\.__EXT__""", m =>
                            {
                                int k = int.Parse(m.Groups[1].Value);
                                string e2 = imageExts.ContainsKey(k) ? imageExts[k] : "jpg";
                                return $"src=\"../Images/{k}.{e2}\"";
                            });
                            entries[i] = (n, Encoding.UTF8.GetBytes(s));
                        }
                        if (hasCover)
                            opf.Append($"<meta name=\"cover\" content=\"cover-img\"/>\n");
                        opf.Append(EpubTemplate.content2_head);
                        if (hasCover)
                        {
                            opf.Append($"<item id=\"cover.html\" href=\"Text/cover.html\" media-type=\"application/xhtml+xml\"/>\n");
                            opf.Append($"<item id=\"cover-img\" href=\"Images/cover.{coverExt}\" media-type=\"{coverMime}\"/>\n");
                        }
                        for (int i = 0; i < chapterNames.Count; i++)
                            opf.Append($"<item id=\"chapter{htmlNames[i]}\" href=\"Text/chapter{htmlNames[i]}\" media-type=\"application/xhtml+xml\"/>\n");
                        foreach (int no in orderedImageNos)
                            opf.Append($"<item id=\"img{no}\" href=\"Images/{no}.{imageExts[no]}\" media-type=\"{imageMimes[no]}\"/>\n");
                        opf.Append("</manifest>\n<spine toc=\"ncx\">\n");
                        if (hasCover)
                            opf.Append("<itemref idref=\"cover.html\"/>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                            opf.Append($"<itemref idref=\"chapter{htmlNames[i]}\"/>\n");
                        opf.Append("</spine>\n");
                        if (hasCover)
                            opf.Append("<guide>\n<reference type=\"cover\" title=\"Cover\" href=\"Text/cover.html\"/>\n</guide>\n");
                        opf.Append("</package>\n");

                        if (File.Exists(path))
                            File.Delete(path);

                        var now = DateTimeOffset.Now;
                        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        using (var zip = new EpubZipWriter(fs, compress))
                        {
                            zip.AddEntry("mimetype", "application/epub+zip", now);
                            zip.AddEntry("META-INF/container.xml", EpubTemplate.container, now);
                            zip.AddEntry("OEBPS/Styles/sgc-toc.css", EpubTemplate.sgctoc, now);
                            zip.AddEntry("OEBPS/Styles/Stylesheet.css", EpubTemplate.Stylesheet(vertical, gothic), now);
                            if (hasCover)
                                zip.AddEntry("OEBPS/Text/cover.html", EpubTemplate.cover.Replace("__COVER_EXT__", coverExt), now);
                            if (hasCover && File.Exists(coverPath))
                                zip.AddEntry($"OEBPS/Images/cover.{coverExt}", File.ReadAllBytes(coverPath), now);
                            foreach (int no in orderedImageNos)
                            {
                                if (!images.TryGetPath(no, out string ip))
                                    continue;
                                if (File.Exists(ip))
                                    zip.AddEntry($"OEBPS/Images/{no}.{imageExts[no]}", File.ReadAllBytes(ip), now);
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
                        DownloadButton.Enabled = true;
                        DownloadButton.Text = Lang.T("btn_download");
                        onAllDone?.Invoke();
                        _cancelRequested = false;
                    }));
                }
            });
        }

        private volatile bool _stopOnError;
        private volatile bool _hadFatalError;
        private int _retryCount;

        private class ImageContext
        {
            private int _next = 1;
            private readonly ConcurrentDictionary<int, string> _paths = new ConcurrentDictionary<int, string>();

            public int GetNextNo() => Interlocked.Increment(ref _next);
            public void SetPath(int no, string path) => _paths[no] = path;
            public bool TryGetPath(int no, out string path) => _paths.TryGetValue(no, out path);
            public IEnumerable<KeyValuePair<int, string>> Paths => _paths;
            public IEnumerable<int> Keys => _paths.Keys;
        }

        private void DownloadChapter(string chapterId, string chapterName, string jsonPath, string label)
        {
            if (_cancelRequested || _hadFatalError) { Interlocked.Increment(ref _progress_skip); return; }
            string prefix = string.IsNullOrEmpty(label) ? "" : label + " ";
            int retry = _retryCount;
            for (int attempt = 0; attempt <= retry; attempt++)
            {
                try
                {
                    string resp = PostRequest($"https://novelpia.com/proc/viewer_data/{chapterId}", novelpia.loginkey, null, "https://novelpia.com/");
                    if (string.IsNullOrEmpty(resp) || resp.Contains("본인인증"))
                        throw new Exception();
                    using (var file = new StreamWriter(jsonPath, false))
                        file.Write(resp);
                    Interlocked.Increment(ref _progress_done);
                    InsertLineAboveProgress($"  ✓ {prefix}{chapterName}");
                    UpdateProgressThrottled();
                    return;
                }
                catch
                {
                    if (attempt < retry)
                        InsertLineAboveProgress(Lang.T("chapter_retry", attempt + 1, retry).TrimEnd('\r', '\n'));
                    else
                    {
                        Interlocked.Increment(ref _progress_fail);
                        InsertLineAboveProgress($"  ✗ {prefix}{chapterName}");
                        UpdateProgressThrottled(force: true);
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

        private string BuildChapterHtml(string encodedName, string jsonPath, bool keepHtml, bool removeBlank, bool downloadImage, ImageContext images)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append(EpubTemplate.chapter);
                sb.Append("<h1>").Append(encodedName).Append("</h1>\n<p>&nbsp;</p>\n");
                var pendingTags = new List<string>();
                var serializer = new JavaScriptSerializer();
                using (var reader = new StreamReader(jsonPath, Encoding.UTF8))
                {
                    var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                    foreach (var text in (ArrayList)texts["s"])
                    {
                        var textDict = (Dictionary<string, object>)text;
                        string textStr = (string)textDict["text"];
                        var imatch = Regex.Match(textStr, @"<img.+?src=""(.+?)"".+?>");
                        if (imatch.Success)
                        {
                            if (!textStr.Contains("cover-wrapper"))
                            {
                                if (downloadImage)
                                {
                                    int imageNo = images.GetNextNo();
                                    string image_url = imatch.Groups[1].Value;
                                    string imgPath = Path.Combine(Path.GetDirectoryName(jsonPath), $"img_{imageNo}.bin");
                                    images.SetPath(imageNo, imgPath);
                                    if (!_cancelRequested && !_hadFatalError)
                                        DownloadImage(image_url, imgPath, Lang.T("illustration"), countProgress: false);
                                    textStr = Regex.Replace(textStr, @"<img.+?src="".+?"".+?>",
                                        $"<img alt=\"{imageNo}\" src=\"../Images/{imageNo}.__EXT__\" width=\"100%\"/>");
                                    sb.Append("<p>").Append(textStr).Append("</p>\n");
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
                return sb.ToString();
            }
            catch (Exception ex)
            {
                InsertLineAboveProgress($"  ✗ {encodedName}  ({ex.Message})");
                return null;
            }
        }

        private void Log(string message)
        {
            if (InvokeRequired)
                Invoke(new Action(() => Log(message)));
            else
            {
                ConsoleBox.AppendText(message);
                _lastLineStart = -1;
                ConsoleBox.SelectionStart = ConsoleBox.TextLength;
                ConsoleBox.ScrollToCaret();
            }
        }

        private static string BuildProgressBar(int current, int total, int width)
        {
            if (total <= 0) return $"[{new string('░', width)}]";
            int filled = (int)Math.Round((double)current / total * width);
            if (filled > width) filled = width;
            return $"[{new string('█', filled)}{new string('░', width - filled)}]";
        }

        private string BuildProgressLine(string suffix = null)
        {
            int done = _progress_done;
            int fail = _progress_fail;
            int total = _progress_total;
            int processed = done + fail;
            string bar = BuildProgressBar(processed, total, 20);
            string tail = suffix ?? Lang.T("progress_tail", done, fail);
            return $"{bar} [{processed}/{total}] {tail}";
        }

        private void UpdateProgress(string suffix = null)
        {
            string line = BuildProgressLine(suffix);
            if (InvokeRequired)
                BeginInvoke(new Action(() => ReplaceLastLine(line)));
            else
                ReplaceLastLine(line);
        }

        private void UpdateProgressThrottled(bool force = false, int intervalMs = 80)
        {
            long now = DateTime.UtcNow.Ticks;
            long last = Interlocked.Read(ref _lastProgressTicks);
            long intervalTicks = intervalMs * TimeSpan.TicksPerMillisecond;
            if (!force && now - last < intervalTicks) return;
            if (Interlocked.CompareExchange(ref _lastProgressTicks, now, last) != last) return;
            UpdateProgress();
        }

        private void BeginProgressPhase(int total, string header)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => BeginProgressPhase(total, header)));
                return;
            }
            _progress_total = total;
            _progress_done = 0;
            _progress_fail = 0;
            _progress_skip = 0;
            _lastProgressTicks = 0;
            ConsoleBox.AppendText(header + "\r\n");
            _lastLineStart = ConsoleBox.TextLength;
            ConsoleBox.AppendText(BuildProgressLine());
            ConsoleBox.SelectionStart = ConsoleBox.TextLength;
            ConsoleBox.ScrollToCaret();
        }

        private void EndProgressPhase(string doneLabel)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EndProgressPhase(doneLabel)));
                return;
            }
            string summary = _cancelRequested
                ? Lang.T("progress_summary_cancel", doneLabel, _progress_done, _progress_fail, _progress_total)
                : Lang.T("progress_summary_done", doneLabel, _progress_done, _progress_fail, _progress_total);
            ReplaceLastLine(summary);
            ConsoleBox.AppendText("\r\n");
            _lastLineStart = -1;
        }

        private void ReplaceLastLine(string text)
        {
            int start = _lastLineStart >= 0 ? _lastLineStart : ConsoleBox.TextLength;
            int len = ConsoleBox.TextLength - start;
            ConsoleBox.Select(start, len);
            ConsoleBox.SelectedText = text;
            ConsoleBox.SelectionStart = ConsoleBox.TextLength;
            ConsoleBox.ScrollToCaret();
        }

        private void InsertLineAboveProgress(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => InsertLineAboveProgress(text)));
                return;
            }
            if (_lastLineStart < 0)
            {
                ConsoleBox.AppendText(text + "\r\n");
                ConsoleBox.SelectionStart = ConsoleBox.TextLength;
                ConsoleBox.ScrollToCaret();
                return;
            }
            string progressLine = BuildProgressLine();
            int start = _lastLineStart;
            int len = ConsoleBox.TextLength - start;
            string replacement = text + "\r\n" + progressLine;
            ConsoleBox.Select(start, len);
            ConsoleBox.SelectedText = replacement;
            _lastLineStart = start + text.Length + 2;
            ConsoleBox.SelectionStart = ConsoleBox.TextLength;
            ConsoleBox.ScrollToCaret();
        }

        private void DownloadImage(string url, string path, string type, bool countProgress = true)
        {
            if (_cancelRequested || _hadFatalError) { if (countProgress) Interlocked.Increment(ref _progress_skip); return; }
            if (!url.StartsWith("http"))
                url = "https:" + url;
            string label = $"{type} {Path.GetFileNameWithoutExtension(path)}";
            int retry = _retryCount;
            for (int attempt = 0; attempt <= retry; attempt++)
            {
                if (_cancelRequested || _hadFatalError) { if (countProgress) Interlocked.Increment(ref _progress_skip); return; }
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
                    request.Timeout = 30000;
                    request.ReadWriteTimeout = 30000;
                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var src = response.GetResponseStream())
                    using (var dst = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        if (src.CanTimeout) src.ReadTimeout = 30000;
                        var buf = new byte[81920];
                        int n;
                        while ((n = src.Read(buf, 0, buf.Length)) > 0)
                        {
                            if (_cancelRequested || _hadFatalError) return;
                            dst.Write(buf, 0, n);
                        }
                    }
                    if (countProgress) Interlocked.Increment(ref _progress_done);
                    InsertLineAboveProgress($"  ✓ {label}");
                    if (countProgress) UpdateProgressThrottled();
                    return;
                }
                catch
                {
                    try { if (File.Exists(path)) File.Delete(path); } catch { }
                    if (attempt < retry)
                        InsertLineAboveProgress(Lang.T("chapter_retry", attempt + 1, retry).TrimEnd('\r', '\n'));
                    else
                    {
                        if (countProgress) Interlocked.Increment(ref _progress_fail);
                        InsertLineAboveProgress($"  ✗ {label}  ({url})");
                        if (countProgress) UpdateProgressThrottled(force: true);
                        if (_stopOnError) _hadFatalError = true;
                    }
                }
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

        private static bool DetectImageType(string path, out string ext, out string mime)
        {
            ext = "jpg";
            mime = "image/jpeg";
            try
            {
                byte[] head = new byte[16];
                int n;
                using (var fs = File.OpenRead(path))
                    n = fs.Read(head, 0, head.Length);
                if (n >= 8 && head[0] == 0x89 && head[1] == 0x50 && head[2] == 0x4E && head[3] == 0x47
                    && head[4] == 0x0D && head[5] == 0x0A && head[6] == 0x1A && head[7] == 0x0A)
                { ext = "png"; mime = "image/png"; return true; }
                if (n >= 3 && head[0] == 0xFF && head[1] == 0xD8 && head[2] == 0xFF)
                { ext = "jpg"; mime = "image/jpeg"; return true; }
                if (n >= 6 && head[0] == 0x47 && head[1] == 0x49 && head[2] == 0x46 && head[3] == 0x38
                    && (head[4] == 0x37 || head[4] == 0x39) && head[5] == 0x61)
                { ext = "gif"; mime = "image/gif"; return true; }
                if (n >= 12 && head[0] == 0x52 && head[1] == 0x49 && head[2] == 0x46 && head[3] == 0x46
                    && head[8] == 0x57 && head[9] == 0x45 && head[10] == 0x42 && head[11] == 0x50)
                { ext = "webp"; mime = "image/webp"; return true; }
                if (n >= 2 && head[0] == 0x42 && head[1] == 0x4D)
                { ext = "bmp"; mime = "image/bmp"; return true; }
                return false;
            }
            catch
            {
                return false;
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
                { "stop_on_error", StopOnErrorCheck.Checked },
                { "include_novel_no", IncludeNovelNoCheck.Checked },
                { "include_chapter_range", IncludeChapterRangeCheck.Checked },
                { "vertical", VerticalCheck.Checked },
                { "gothic", GothicCheck.Checked },
                { "bonus_never", BonusNeverCheck.Checked },
                { "bonus_always", BonusAlwaysCheck.Checked }
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
