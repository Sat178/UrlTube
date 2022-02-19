using Leaf.xNet;using Newtonsoft.Json.Linq;using System;using System.IO;using System.Linq;using System.Windows.Forms;using System.Net;namespace UrlTube
{internal class Converter
{public static void Loader()
{using(OpenFileDialog ofd=new OpenFileDialog())
{ofd.Title="Open Video Urls";ofd.Filter="txt files (*.txt)|*.txt|All files (*.*)|*.*";ofd.FilterIndex=2;ofd.RestoreDirectory=true;if(ofd.ShowDialog()==DialogResult.OK)
{string fPath=ofd.FileName;var fUrls=File.ReadAllLines(fPath);foreach(string fUrl in fUrls)Program.rawUrls.Add(fUrl);foreach(string fUrl in Program.rawUrls)Program.FilteredUrls.Add(videoID(fUrl));Program.FilteredUrls=Program.FilteredUrls.Distinct().ToList<string>();foreach(string fID in Program.FilteredUrls)Program.UrlsDL.Add(videoDL(fID));foreach(string dlLoc in Program.UrlsDL)Downloader(dlLoc);}}}
public static string videoID(string vidUrl)
{if(vidUrl.Contains("https://www.youtube.com/watch?v="))
{vidUrl=vidUrl.Split(new string[]{" ",},StringSplitOptions.None)[0];}
return vidUrl;}
public static string videoDL(string vidID)
{string Ldl="{\"url\":\"";string Ldr="\"}";return string.Format("{0}{1}{2}",new object[]
{Ldl,vidID,Ldr});}
public static void Downloader(string dlLoc)
{using(HttpRequest hr=new HttpRequest())
{hr.AddHeader("user-agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.82 Safari/537.36");hr.AllowAutoRedirect=false;hr.AllowEmptyHeaderValues=true;hr.IgnoreInvalidCookie=false;hr.IgnoreProtocolErrors=true;string hID=dlLoc.Split(new string[]
{"{\"url\":\"https://www.youtube.com/watch?v=",},StringSplitOptions.None)[1];Program.fullID=hID.Split(new string[]{"\"}"},StringSplitOptions.None)[0];var rwData=hr.Post("https://api.onlinevideoconverter.pro/api/convert",dlLoc,"application/json").ToString();JsonParser(rwData);Console.WriteLine("Finished: {0}",Program.fullID);}}
public static void JsonParser(string rawData)
{try
{JObject jobj=JObject.Parse(rawData);string Urls=jobj.SelectToken("url").ToString();try
{if(Urls.Contains("https://du."))
{string dlUrl=Urls.Split(new string[]{"https://du."},StringSplitOptions.None)[1];string fdlUrl=dlUrl.Split(new string[]{"\","},StringSplitOptions.None)[0];string append="https://du.";Program.rdyUrl=string.Format("{0}{1}",new object[]{append,fdlUrl});}
else
{string dlUrl=Urls.Split(new string[]{"https:\\/\\/ "},StringSplitOptions.None)[1];string fdlUrl=dlUrl.Split(new string[]{"\","},StringSplitOptions.None)[0];string append="https:\\/\\/";Program.rdyUrl=string.Format("{0}{1}",new object[]{append,fdlUrl});}}
catch
{}
dirDownload(Program.rdyUrl);}
catch(Exception e)
{Console.WriteLine(e);Console.WriteLine("Failed: {0}",Program.fullID);return;}}
public static void dirDownload(string downloadUrl)
{using(var Client=new WebClient())
{string dlloc=AppDomain.CurrentDomain.BaseDirectory"\\"Program.fullID".mp4";Client.DownloadFile(downloadUrl,dlloc);return;}}
public class vidInfo
{public string stream
{get;set;}
public string url
{get;set;}}
public static void Trimmer()
{Console.ReadKey();}}}
