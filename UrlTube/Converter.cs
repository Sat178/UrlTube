using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Colorful;
using Leaf.xNet;
using Newtonsoft.Json.Linq;

namespace UrlTube
{
	// Token: 0x02000004 RID: 4
	internal class Converter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public static void Loader()
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Open Video Urls";
				ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
				ofd.FilterIndex = 2;
				ofd.RestoreDirectory = true;
				bool flag = ofd.ShowDialog() == DialogResult.OK;
				if (flag)
				{
					string fPath = ofd.FileName;
					string[] fUrls = File.ReadAllLines(fPath);
					foreach (string fUrl in fUrls)
					{
						Program.rawUrls.Add(fUrl);
					}
					foreach (string fUrl2 in Program.rawUrls)
					{
						Program.FilteredUrls.Add(Converter.videoID(fUrl2));
					}
					Program.FilteredUrls = Program.FilteredUrls.Distinct<string>().ToList<string>();
					foreach (string fID in Program.FilteredUrls)
					{
						Program.UrlsDL.Add(Converter.videoDL(fID));
					}
					Colorful.Console.Write("Successfully loaded", Color.IndianRed);
					Colorful.Console.Write(" {0}", Color.White, new object[]
					{
						Program.FilteredUrls.Count<string>()
					});
					Colorful.Console.Write(" valid UrlTube urls!\n\n", Color.IndianRed);
					foreach (string dlLoc in Program.UrlsDL)
					{
						Converter.Downloader(dlLoc);
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002338 File Offset: 0x00000538
		public static string videoID(string vidUrl)
		{
			bool flag = vidUrl.Contains("https://www.youtube.com/watch?v=");
			if (flag)
			{
				vidUrl = vidUrl.Split(new string[]
				{
					" "
				}, StringSplitOptions.None)[0];
			}
			return vidUrl;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002378 File Offset: 0x00000578
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

		// Token: 0x06000008 RID: 8 RVA: 0x000023B4 File Offset: 0x000005B4
		public static void Downloader(string dlLoc)
		{
			using (HttpRequest hr = new HttpRequest())
			{
				hr.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");
				hr.AllowAutoRedirect = false;
				hr.AllowEmptyHeaderValues = true;
				hr.IgnoreInvalidCookie = false;
				hr.IgnoreProtocolErrors = true;
				string hID = dlLoc.Split(new string[]
				{
					"{\"url\":\"https://www.youtube.com/watch?v="
				}, StringSplitOptions.None)[1];
				Program.fullID = hID.Split(new string[]
				{
					"\"}"
				}, StringSplitOptions.None)[0];
				Colorful.Console.Write("Converting Url ", Color.White);
				Colorful.Console.Write("[", Color.IndianRed);
				Colorful.Console.Write("{0}", Color.White, new object[]
				{
					Program.icrCount
				});
				Colorful.Console.Write("] ", Color.IndianRed);
				Colorful.Console.Write(" | ID:");
				Colorful.Console.Write(" {0}", Color.IndianRed, new object[]
				{
					Program.fullID
				});
				Colorful.Console.Write(" | Title: ", Color.White);
				string rwData = hr.Post("https://api.onlinevideoconverter.pro/api/convert", dlLoc, "application/json").ToString();
				Converter.JsonParser(rwData);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002504 File Offset: 0x00000704
		public static void JsonParser(string rawData)
		{
			try
			{
				JObject jobj = JObject.Parse(rawData);
				string Urls = jobj.SelectToken("url").ToString();
				string vid = jobj.SelectToken("meta").ToString();
				JObject jobj2 = JObject.Parse(vid);
				string vidTitle = jobj2.SelectToken("title").ToString();
				Colorful.Console.Write(" " + vidTitle, Color.IndianRed);
				Colorful.Console.Write(" | Status: ", Color.White);
				try
				{
					bool flag = Urls.Contains("https://du.");
					if (flag)
					{
						string dlUrl = Urls.Split(new string[]
						{
							"https://du."
						}, StringSplitOptions.None)[1];
						string fdlUrl = dlUrl.Split(new string[]
						{
							"\","
						}, StringSplitOptions.None)[0];
						string append = "https://du.";
						Program.rdyUrl = string.Format("{0}{1}", new object[]
						{
							append,
							fdlUrl
						});
					}
					else
					{
						string dlUrl2 = Urls.Split(new string[]
						{
							"https://rr"
						}, StringSplitOptions.None)[1];
						string fdlUrl2 = dlUrl2.Split(new string[]
						{
							"\","
						}, StringSplitOptions.None)[0];
						string append2 = "https://rr";
						Program.rdyUrl = string.Format("{0}{1}", new object[]
						{
							append2,
							fdlUrl2
						});
					}
				}
				catch (Exception e)
				{
					Colorful.Console.Write("Fail!\n", Color.Red);
				}
				Converter.dirDownload(Program.rdyUrl);
			}
			catch (Exception e2)
			{
				Colorful.Console.Write("Fail!\n", Color.Red);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026B8 File Offset: 0x000008B8
		public static void dirDownload(string downloadUrl)
		{
			Program.icrCount++;
			using (WebClient Client = new WebClient())
			{
				string dlloc = string.Concat(new string[]
				{
					AppDomain.CurrentDomain.BaseDirectory,
					"\\UrlTube\\",
					Directories.dirTime,
					"\\",
					Program.fullID,
					".mp4"
				});
				bool flag = File.Exists(string.Concat(new string[]
				{
					AppDomain.CurrentDomain.BaseDirectory,
					"\\UrlTube\\",
					Directories.dirTime,
					"\\",
					Program.fullID,
					".mp4"
				}));
				if (flag)
				{
					Colorful.Console.Write("Duplicate!\n", Color.Orange);
				}
				else
				{
					Client.DownloadFile(downloadUrl, dlloc);
					Colorful.Console.Write("Success!\n", Color.Green);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000027AC File Offset: 0x000009AC
		public static void Trimmer()
		{
			Colorful.Console.ReadKey();
		}

		// Token: 0x02000006 RID: 6
		public class vidInfo
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000012 RID: 18 RVA: 0x0000293D File Offset: 0x00000B3D
			// (set) Token: 0x06000013 RID: 19 RVA: 0x00002945 File Offset: 0x00000B45
			public string stream { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000014 RID: 20 RVA: 0x0000294E File Offset: 0x00000B4E
			// (set) Token: 0x06000015 RID: 21 RVA: 0x00002956 File Offset: 0x00000B56
			public string url { get; set; }
		}
	}
}
