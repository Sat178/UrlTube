using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace UrlTube
{
    internal class Program
    {
        public static List<string> FilteredUrls = new List<string>();

        public static string fullID = string.Empty;

        public static int icrCount = 1;

        public static List<string> objs = new List<string>();

        public static List<string> rawUrls = new List<string>();

        public static string rdyUrl = string.Empty;

        public static int UrlCount = 0;

        public static string Urls = String.Empty;

        public static List<string> UrlsDL = new List<string>();

        public static string vidTitle = String.Empty;

        public static void CLI()
        {
            Console.Title = "UrlTube by Avoidy - github.com/Avoidy - Avoidy#3443";

            // File Input
            Console.Write("[", Color.IndianRed);
            Console.Write("Input", Color.White);
            Console.Write("]", Color.IndianRed);
            Console.Write(" >> Please open your file!\n\n", Color.White);
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

        [STAThread] // Required to open Directory
        private static void Main(string[] args)
        {
            Interface(); CLI();
            Directories.StartUp();
            Converter.Loader();
            Console.WriteLine("Finished converting {0} urls!", Color.Green, UrlsDL.Count<string>());
            Console.ReadKey(); // Prevent closing program
        }
    }
}