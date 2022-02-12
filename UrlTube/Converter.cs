using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Leaf.xNet;

namespace UrlTube
{
    internal class Converter
    {
        public static void Loader()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Open Video Urls";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fPath = ofd.FileName;
                    var fUrls = File.ReadAllLines(fPath);
                    foreach (string fUrl in fUrls) Program.rawUrls.Add(fUrl);
                    foreach (string fUrl in Program.rawUrls) Program.FilteredUrls.Add(videoID(fUrl));
                    Program.FilteredUrls = Program.FilteredUrls.Distinct().ToList<string>();
                    foreach (string fID in Program.FilteredUrls) Program.UrlsDL.Add(videoDL(fID));
                    foreach (string finalDL in Program.UrlsDL) Console.WriteLine($"{finalDL}");
                    foreach (string dlLoc in Program.UrlsDL) Downloader(dlLoc);

                }
            }     
        }
        public static string videoID(string vidUrl)
        {
            if (vidUrl.Contains("https://www.youtube.com/watch?v="))
            {
                vidUrl = vidUrl.Split(new string[]
                {
                        " ",

                }, StringSplitOptions.None)[0];
            }

            return vidUrl;
        }

        public static string videoDL(string vidID)
        {
            string Ldl = "{\"url\":\"";
            string Ldr = "\"}";

            return string.Format("{0}{1}{2}", new object[]
            {
                Ldl,
                vidID,
                Ldr
            });
        }

        public static void Downloader(string dlLoc)
        {
            using (HttpRequest hr = new HttpRequest())
            {
                hr.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");
                hr.AllowAutoRedirect = false;
                hr.AllowEmptyHeaderValues = true;
                hr.IgnoreInvalidCookie = false;
                hr.IgnoreProtocolErrors = true;

                var data = hr.Post("https://api.onlinevideoconverter.pro/api/convert", dlLoc, "application/json");
                Console.WriteLine(data);
            }
                
            /*
            var x = webBrowser.Document.GetElementById("buttonTitle").ToString();
            Console.WriteLine(x);

            webBrowser.Document.GetElementById("downloadButton").InvokeMember("onclick");

            */

            // Console.WriteLine(pInfo);

        }
        public static void Trimmer()
        {
            Console.ReadKey();
        }
    }
}
