using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net;

namespace UrlTube {
    internal class Converter {
        public static void Loader() {
            using(OpenFileDialog ofd = new OpenFileDialog()) // Directory to open your URL file
            {
                ofd.Title = "Open Video Urls";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK) // If file is valid go-on!
                {
                    string fPath = ofd.FileName;
                    var fUrls = File.ReadAllLines(fPath); // Read all lines/urls in the .txt file
                    foreach(string fUrl in fUrls) Program.rawUrls.Add(fUrl); // Add all urls into List<string>
                    foreach(string fUrl in Program.rawUrls) Program.FilteredUrls.Add(videoID(fUrl)); //Foreach url cycle thru videoID to get VideoID (which is currently off, because I don't need it for now)
                    Program.FilteredUrls = Program.FilteredUrls.Distinct().ToList < string > (); // Distinct (remove duplicates from lists)
                    foreach(string fID in Program.FilteredUrls) Program.UrlsDL.Add(videoDL(fID)); // Add all POST-ready request into List<string>
                    //foreach (string finalDL in Program.UrlsDL) Console.WriteLine($"{finalDL}");
                    foreach(string dlLoc in Program.UrlsDL) Downloader(dlLoc);

                }
            }
        }
        public static string videoID(string vidUrl) {
            if (vidUrl.Contains("https://www.youtube.com/watch?v=")) {
                vidUrl = vidUrl.Split(new string[] {
                    " ", // Remove unnecessary blank spaces {"https://www.youtube.com/watch?v="}

                }, StringSplitOptions.None)[0];
            }

            return vidUrl;
        }

        public static string videoDL(string vidID) {
            string Ldl = "{\"url\":\""; //Necessary for POST request
            string Ldr = "\"}"; //Necessary for POST request

            return string.Format("{0}{1}{2}", new object[] // Format string into right POST-format
                {
                    Ldl,
                    vidID,
                    Ldr
                });
        }

        public static void Downloader(string dlLoc) {
            using(HttpRequest hr = new HttpRequest()) {
                hr.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");
                hr.AllowAutoRedirect = false;
                hr.AllowEmptyHeaderValues = true;
                hr.IgnoreInvalidCookie = false;
                hr.IgnoreProtocolErrors = true;

                string hID = dlLoc.Split(new string[] // Example {"url":"https://www.youtube.com/watch?v=t7a-b1zTS60"}
                    {
                        "{\"url\":\"https://www.youtube.com/watch?v=",
                    }, StringSplitOptions.None)[1];

                Program.fullID = hID.Split(new string[] {
                    "\"}"
                }, StringSplitOptions.None)[0];

                //Console.WriteLine(fullID);

                var rwData = hr.Post("https://api.onlinevideoconverter.pro/api/convert", dlLoc, "application/json").ToString();
                JsonParser(rwData);
                //Console.WriteLine(rwData);
                Console.WriteLine("Finished: {0}", Program.fullID);
            }

        }

        public static void JsonParser(string rawData) {

            try {
                JObject jobj = JObject.Parse(rawData);

                string Urls = jobj.SelectToken("url").ToString();
                //Console.WriteLine(Urls);
                try {
                    if (Urls.Contains("https://du.")) {

                        string dlUrl = Urls.Split(new string[] {
                            "https://du."
                        }, StringSplitOptions.None)[1];

                        //Console.WriteLine(dlUrl);

                        string fdlUrl = dlUrl.Split(new string[] {
                            "\","
                        }, StringSplitOptions.None)[0];

                        string append = "https://du.";

                        Program.rdyUrl = string.Format("{0}{1}", new object[] {
                            append,
                            fdlUrl
                        });
                    } else {
                        string dlUrl = Urls.Split(new string[] {
                            "https:\\/\\/ "
                        }, StringSplitOptions.None)[1];

                        string fdlUrl = dlUrl.Split(new string[] {
                            "\","
                        }, StringSplitOptions.None)[0];

                        string append = "https:\\/\\/";

                        Program.rdyUrl = string.Format("{0}{1}", new object[] {
                            append,
                            fdlUrl
                        });
                    }

                } catch {

                }

                dirDownload(Program.rdyUrl);
                //Console.WriteLine("Finished: {0}", Program.fullID);

                //Console.WriteLine(rdyUrl);

            } catch (Exception e) {
                Console.WriteLine(e);
                Console.WriteLine("Failed: {0}", Program.fullID);
                //Console.WriteLine(rawData);
                return;
            }
        }

        public static void dirDownload(string downloadUrl) {
            using(var Client = new WebClient()) {
                string dlloc = AppDomain.CurrentDomain.BaseDirectory + "\\" + Program.fullID + ".mp4";
                Client.DownloadFile(downloadUrl, dlloc);
                return;
            }
        }

        public class vidInfo {
            public string stream {
                get;
                set;
            }
            public string url {
                get;
                set;
            }
        }
        public static void Trimmer() {
            Console.ReadKey();
        }
    }
}
