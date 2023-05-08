using System;
using System.IO;
using System.Linq;
using System.Net;

namespace NovelpiaDownloader
{
    public class Novelpia
    {
        public string loginkey;

        public Novelpia()
        {
            var random = new Random();
            var characters = "0123456789abcdef";
            var firstPart = new string(Enumerable.Range(0, 32).Select(_ => characters[random.Next(characters.Length)]).ToArray());
            var secondPart = new string(Enumerable.Range(0, 32).Select(_ => characters[random.Next(characters.Length)]).ToArray());
            loginkey = firstPart + "_" + secondPart;
        }

        public bool Login(string id, string pw)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://novelpia.com/proc/login");
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Mobile Safari/537.36";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("cookie", $"LOGINKEY={loginkey};");
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write($"redirectrurl=&email={id}&wd={pw}");
            }
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                if (streamReader.ReadToEnd().Contains("감사합니다"))
                    return true;
            }
            return false;
        }
    }
}
