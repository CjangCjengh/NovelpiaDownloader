using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            defaults = new Dictionary<string, string>();
            if (File.Exists("account.txt"))
            {
                foreach (string line in File.ReadLines("account.txt"))
                {
                    string[] split = line.Split('>');
                    defaults.Add(split[0], split[1]);
                }
                if (defaults.ContainsKey("email") && defaults.ContainsKey("wd"))
                {
                    if (novelpia.Login(EmailText.Text = defaults["email"], PasswordText.Text = defaults["wd"]))
                    {
                        ConsoleBox.Text += "로그인 성공!\r\n";
                        LoginkeyText.Text = novelpia.loginkey;
                        return;
                    }
                    ConsoleBox.Text += "로그인 실패!\r\n";
                    File.Delete("account.txt");
                }
                if (defaults.ContainsKey("loginkey"))
                    novelpia.loginkey = LoginkeyText.Text = defaults["loginkey"];
            }
        }

        readonly Novelpia novelpia;
        private readonly Dictionary<string, string> defaults;

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            bool saveAsEpub = EpubButton.Checked;
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = saveAsEpub ? "|*.epub" : "|*.txt"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Download(NovelNoText.Text, saveAsEpub, sfd.FileName);
            }
            sfd.Dispose();
        }

        void Download(string novelNo, bool saveAsEpub, string path)
        {
            ConsoleBox.AppendText("다운로드 시작!\r\n");
            string directory = Path.Combine(Path.GetDirectoryName(path), novelNo);
            Directory.CreateDirectory(directory);
            Task.Run(() =>
            {
                int chapterNo = 0;
                int page = 0;
                var chapterIds = new List<string>();
                var chapterNames = new List<string[]>();
                while (true)
                {
                    string data = $"novel_no={novelNo}&sort=DOWN&page={page}";
                    string resp = PostRequest("https://novelpia.com/proc/episode_list", "", data);
                    var chapters = Regex.Matches(resp, @"id=""bookmark_(\d+)""></i>(.+?)</b>");
                    if (chapterIds.Contains(chapters[0].Groups[1].Value))
                        break;
                    foreach (Match chapter in chapters)
                    {
                        resp = PostRequest($"https://novelpia.com/proc/viewer_data/{chapter.Groups[1].Value}", novelpia.loginkey);
                        if (resp != "" && !resp.Contains("본인인증"))
                        {
                            string jsonPath = Path.Combine(directory, $"{chapterNo.ToString().PadLeft(4, '0')}.json");
                            using (var file = new StreamWriter(jsonPath, false))
                            {
                                file.Write(resp);
                            }
                            chapterNames.Add(new string[] { chapter.Groups[2].Value, jsonPath });
                            Invoke(new Action(() => ConsoleBox.AppendText(chapter.Groups[2].Value + "\r\n")));
                        }
                        else
                        {
                            Invoke(new Action(() => ConsoleBox.AppendText(chapter.Groups[2].Value + " ERROR!\r\n")));
                        }
                        chapterNo++;
                        chapterIds.Add(chapter.Groups[1].Value);
                    }
                    page++;
                }
                if (saveAsEpub)
                {
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
                    match = Regex.Match(responseText, @"href='(//image\.novelpia\.com/imagebox/cover/.+?\.file)'");
                    string url = match.Groups[1].Value;
                    Invoke(new Action(() => ConsoleBox.AppendText("커버 다운로드 시작\r\n{url}\r\n")));
                    using (var downloader = new WebClient())
                    {
                        downloader.DownloadFile("https:" + url, Path.Combine(directory, "OEBPS/Images/cover.jpg"));
                    }
                    Invoke(new Action(() => ConsoleBox.AppendText($"커버 다운로드 완료\r\n")));

                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/toc.ncx"), false))
                    {
                        file.Write(EpubTemplate.toc);
                        file.Write($"<text>{title}</text>\n</docTitle>\n<navMap>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            file.Write($"<navPoint id=\"navPoint-{i + 1}\" playOrder=\"{i + 1}\">\n" +
                                "<navLabel>\n" +
                                $"<text>{chapterNames[i][0]}</text>\n" +
                                "</navLabel>\n" +
                                $"<content src=\"Text/chapter{Path.ChangeExtension(Path.GetFileName(chapterNames[i][1]), "html")}\" />\n" +
                                "</navPoint>\n");
                        }
                        file.Write("</navMap>\n</ncx>\n");
                    }

                    int imageNo = 1;
                    using (var file = new StreamWriter(Path.Combine(directory, $"OEBPS/Text/cover.html"), false))
                        file.Write(EpubTemplate.cover);
                    chapterNames.ForEach(s =>
                    {
                        string temp = Path.ChangeExtension(Path.GetFileName(s[1]), "html");
                        using (var file = new StreamWriter(Path.Combine(directory, $"OEBPS/Text/chapter{temp}"), false))
                        {
                            file.Write(EpubTemplate.chapter);
                            file.Write($"<h1>{s[0]}</h1>\n<p>&nbsp;</p>\n");
                            var serializer = new JavaScriptSerializer();
                            using (var reader = new StreamReader(s[1], Encoding.UTF8))
                            {
                                var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                foreach (var text in (ArrayList)texts["s"])
                                {
                                    var textDict = (Dictionary<string, object>)text;
                                    string textStr = (string)textDict["text"];
                                    match = Regex.Match(textStr, @"<img.+?src=\""(.+?)\"".+?>");
                                    if (match.Success)
                                    {
                                        if (!textStr.Contains("cover-wrapper"))
                                        {
                                            url = match.Groups[1].Value;
                                            Invoke(new Action(() => ConsoleBox.AppendText("삽화 다운로드 시작\r\n{url}\r\n")));
                                            using (var downloader = new WebClient())
                                            {
                                                downloader.DownloadFile("https:" + url, Path.Combine(directory, $"OEBPS/Images/{imageNo}.jpg"));
                                            }
                                            Invoke(new Action(() => ConsoleBox.AppendText($"삽화 다운로드 완료\r\n")));
                                            textStr = Regex.Replace(textStr, @"<img.+?src=\"".+?\"".+?>",
                                                $"<img alt=\"{imageNo}\" src=\"../Images/{imageNo}.jpg\" width=\"100%\"/>");
                                            file.Write($"<p>{textStr}</p>\n");
                                            imageNo++;
                                        }
                                        continue;
                                    }
                                    textStr = Regex.Replace(textStr, @"</?[^>]+>|\n", "");
                                    if (textStr == "")
                                        continue;
                                    file.Write($"<p>{textStr}</p>\n");
                                }
                            }
                            file.Write("</body>\n</html>\n");
                        }
                        File.Delete(s[1]);
                    });

                    using (var file = new StreamWriter(Path.Combine(directory, "OEBPS/content.opf"), false))
                    {
                        file.Write(EpubTemplate.content1);
                        file.Write($"<dc:title>{title}</dc:title>\n");
                        file.Write(EpubTemplate.content2);
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            string temp = Path.ChangeExtension(Path.GetFileName(chapterNames[i][1]), "html");
                            file.Write($"<item id=\"chapter{temp}\" href=\"Text/chapter{temp}\" media-type=\"application/xhtml+xml\"/>\n");
                        }
                        for (int i = 1; i < imageNo; i++)
                        {
                            file.Write($"<item id=\"{i}.jpg\" href=\"Images/{i}.jpg\" media-type=\"image/jpeg\"/>\n");
                        }
                        file.Write("</manifest>\n<spine toc=\"ncx\">\n<itemref idref=\"cover.html\"/>\n");
                        for (int i = 0; i < chapterNames.Count; i++)
                        {
                            string temp = Path.ChangeExtension(Path.GetFileName(chapterNames[i][1]), "html");
                            file.Write($"<itemref idref=\"chapter{temp}\"/>\n");
                        }
                        file.Write("</spine>\n<guide>\n" +
                            "<reference type=\"cover\" title=\"Cover\" href=\"Text/cover.html\"/>\n" +
                            "</guide>\n</package>\n");
                    }

                    if (File.Exists(path))
                        File.Delete(path);

                    ZipFile.CreateFromDirectory(directory, path);
                }
                else
                {
                    using (var file = new StreamWriter(path, false))
                    {
                        var serializer = new JavaScriptSerializer();
                        chapterNames.ForEach(s =>
                        {
                            file.Write($"{s[0]}\n\n");
                            using (var reader = new StreamReader(s[1], Encoding.UTF8))
                            {
                                var texts = serializer.Deserialize<Dictionary<string, object>>(reader.ReadToEnd());
                                foreach (var text in (ArrayList)texts["s"])
                                {
                                    var textDict = (Dictionary<string, object>)text;
                                    string textStr = (string)textDict["text"];
                                    textStr = Regex.Replace(textStr, @"<img.+?>", "");
                                    textStr = Regex.Replace(textStr, @"</?[^>]+>|\n", "");
                                    if (textStr == "" || textStr.Contains("cover-wrapper"))
                                        continue;
                                    textStr = HttpUtility.HtmlDecode(textStr);
                                    file.WriteLine(textStr);
                                }
                            }
                            file.Write("\n");
                            File.Delete(s[1]);
                        });
                    }
                }
                Directory.Delete(directory, true);
                Invoke(new Action(() => ConsoleBox.AppendText("다운로드 완료!\r\n")));
            });
        }

        private static string PostRequest(string url, string loginkey, string data = null)
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

        private void LoginButton1_Click(object sender, EventArgs e)
        {
            string email = EmailText.Text;
            string password = PasswordText.Text;
            if (novelpia.Login(email, password))
            {
                ConsoleBox.AppendText("로그인 성공!\r\n");
                LoginkeyText.Text = novelpia.loginkey;
                using (StreamWriter sw = new StreamWriter("account.txt"))
                {
                    sw.WriteLine("email>" + email);
                    sw.WriteLine("wd>" + password);
                    sw.WriteLine("loginkey>" + novelpia.loginkey);
                }
            }
            else
            {
                ConsoleBox.AppendText("로그인 실패!\r\n");
                File.Delete("account.txt");
            }
        }

        private void LoginButton2_Click(object sender, EventArgs e)
        {
            novelpia.loginkey = LoginkeyText.Text;
            using (StreamWriter sw = new StreamWriter("account.txt"))
                sw.WriteLine("loginkey>" + novelpia.loginkey);
        }
    }
}
