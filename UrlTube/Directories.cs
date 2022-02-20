using System;
using System.IO;

namespace UrlTube
{
    internal class Directories
    {
        public static string exeLoc = AppDomain.CurrentDomain.BaseDirectory;
        public static string dirTime = DateTime.Now.ToString("dd-MM-yyyy");

        public static void StartUp()
        {
            if (!Directory.Exists(exeLoc + @"\UrlTube\"))
            {
                Directory.CreateDirectory(exeLoc + @"\UrlTube\");
            }
            else
            {
                if (!Directory.Exists(exeLoc + @"\UrlTube\" + dirTime + @"\"))
                {
                    Directory.CreateDirectory(exeLoc + @"\UrlTube\" + dirTime + @"\");
                }
            }
        }
    }
}