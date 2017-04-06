using System;
using System.Collections.Generic;
using System.Net;
using WebDav;

namespace WebDavConsole
{
    /// <summary>
    /// This class is for test purpose only.
    /// </summary>
    class Program
    {
    	private static WebDavManager _webDavManager;
        private static string _dirName = "http://localhost:8080/webdav/";
    	
        static void Main(string[] args)
        {
            // Disable the SSL check
            ServicePointManager.ServerCertificateValidationCallback += delegate
            { return true; };
            
            _webDavManager = new WebDavManager();

            ListDirectory();

            Console.WriteLine("End");
            Console.ReadKey();
        }

        static void DownloadTestFile()
        {
            _webDavManager.DownloadFile(_dirName + "test.txt", @"C:\test.txt");
        }

        static void UploadTestFile()
        {
            _webDavManager.UploadFile(String.Format("{0}{1}.txt", _dirName, DateTime.Now.ToString("yyyyMMddHHmm")), @"C:\test.txt");
        }
        
        static void CreateDirectory()
        {
        	_webDavManager.CreateDirectory(String.Format("{0}{1}/", _dirName, DateTime.Now.ToString("yyyyMMddHHmm")));
        }
        
        static void DeleteDirectory()
        {
        	string dirName = DateTime.Now.ToString("yyyyMMddHHmm");
        	
        	_webDavManager.CreateDirectory(String.Format("{0}{1}/", _dirName, dirName));
        	_webDavManager.Delete(String.Format("{0}{1}/", _dirName, dirName));
        }

        static void ListDirectory()
        {
            List<WebDavResource> webDavResources = _webDavManager.List(_dirName);

            Console.WriteLine("List directory: " + _dirName);

            foreach (WebDavResource resource in webDavResources)
            {
                Console.WriteLine("Name: " + resource.Name + " | Size: " + resource.Size + " | Url: " + resource.Uri);
                Console.WriteLine("Created: " + resource.Created + " | Modified: " + resource.Modified);
                Console.WriteLine("***************************");
            }

            Console.WriteLine("End of list directory.");
        }
    }
}
