using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

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
                    foreach (string fUrl in Program.FilteredUrls) Console.WriteLine($"{fUrl}");

                }
            }



            
        }
        public static string videoID(string vidUrl)
        {
            if (vidUrl.Contains("https://www.youtube.com/watch?v="))
            {
                vidUrl = vidUrl.Split(new string[]
                {
                        "https://www.youtube.com/watch?v="

                }, StringSplitOptions.None)[1];
            }

            return vidUrl;
        }
        public static void Trimmer()
        {

            Console.ReadKey();
        }
    }
}
