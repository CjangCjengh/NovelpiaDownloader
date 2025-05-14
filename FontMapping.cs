using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace NovelpiaDownloader
{
    internal class FontMapping
    {
        private Dictionary<char, char> font_mapping;

        public FontMapping(string path)
        {
            if (!File.Exists(path))
                return;
            var tempMapping = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(path));
            font_mapping = tempMapping.ToDictionary(kvp => kvp.Key[0], kvp => kvp.Value[0]);
        }

        public string DecodeText(string text)
        {
            if (font_mapping == null)
                return text;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                if (font_mapping.TryGetValue(c, out char replacement))
                    sb.Append(replacement);
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
