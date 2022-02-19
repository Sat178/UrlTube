using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net;

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
