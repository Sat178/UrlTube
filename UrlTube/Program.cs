using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlTube
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args) { 

            Converter.Loader();
            Console.ReadKey();
        }

        public static List<string> rawUrls = new List<string>();
        public static List<string> FilteredUrls = new List<string>();
        public static List<string> UrlsDL = new List<string>();
    }
}
