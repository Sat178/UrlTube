using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlTube
{
    internal class Program
    {
        [STAThread] // Required to open Directory
        static void Main(string[] args) { 

            Converter.Loader();
            Console.ReadKey(); // Prevent closing program
        }

        public static List<string> rawUrls = new List<string>();
        public static List<string> FilteredUrls = new List<string>();
        public static List<string> UrlsDL = new List<string>();
        public static List<string> objs = new List<string>();
        public static string fullID = string.Empty;
        public static string rdyUrl = string.Empty;
    }
}
