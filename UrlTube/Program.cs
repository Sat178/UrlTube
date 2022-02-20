using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Drawing;

namespace UrlTube
{
    internal class Program
    {
        [STAThread] // Required to open Directory
        static void Main(string[] args) {
            Interface(); CLI();
            Directories.StartUp();
            Converter.Loader();
            Console.WriteLine("Finished converting {0} urls!", Color.Green, UrlsDL.Count<string>());
            Console.ReadKey(); // Prevent closing program

        }

        public static void Interface()
        {
            Console.WriteLine(@"  _   _      _ _____      _          ", Color.IndianRed);
            Console.WriteLine(@" | | | |_ __| |_   _|   _| |__   ___ ", Color.IndianRed);
            Console.WriteLine(@" | | | | '__| | | || | | | '_ \ / _ \", Color.IndianRed);
            Console.WriteLine(@" | |_| | |  | | | || |_| | |_) |  __/", Color.IndianRed);
            Console.WriteLine(@"  \___/|_|  |_| |_| \__,_|_.__/ \___|", Color.IndianRed);
            Console.WriteLine(@"                                     ", Color.IndianRed);
        }

        public static void CLI()
        {
            Console.Title = "UrlTube by Avoidy - github.com/Avoidy - Avoidy#3443";

            // File Input
            Console.Write("[", Color.IndianRed);
            Console.Write("Input", Color.White);
            Console.Write("]", Color.IndianRed);
            Console.Write(" >> Please open your file!\n\n", Color.White);
        }

        public static List<string> rawUrls = new List<string>();
        public static List<string> FilteredUrls = new List<string>();
        public static List<string> UrlsDL = new List<string>();
        public static List<string> objs = new List<string>();
        public static string fullID = string.Empty;
        public static string rdyUrl = string.Empty;
        public static int UrlCount = 0;
        public static int icrCount = 1;
    }
}
