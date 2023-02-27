using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.IO;

namespace SharepointFileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            // string url = "https://dayonetechcom.sharepoint.com/sites/RedPeTT/Shared%20Documents/Class10/ProjectInfo_b4056a7f-bebb-486f-b9e9-a0781b418485.edp7";
            Console.WriteLine(args.Length);
            Console.WriteLine(args[0]);
            if (args.Length != 2)
            {
                return;
            }
            string url = args[0];
            ClientContext context = new ClientContext("https://dayonetechcom.sharepoint.com/sites/RedPeTT/");
            url = url.Replace("https://dayonetechcom.sharepoint.com", "");
            context.AuthenticationMode = ClientAuthenticationMode.Default;
            string pass = "Brigereli1!";
            System.Security.SecureString password = new System.Security.SecureString();
            foreach (char c in pass)
            {
                password.AppendChar(c);
            }
            context.Credentials = new SharePointOnlineCredentials("gfj0@day-onetech.com", password);

            var list = context.Web.GetFileByServerRelativeUrl(url);
            context.Load(list);
            String destPath = args[1];
            var stream = System.IO.File.Create(destPath);
            ClientResult<Stream> clientResult = list.OpenBinaryStream();
            context.ExecuteQuery();
            Console.WriteLine(list.Name);
            clientResult.Value.CopyTo(stream);
            //System.Net.WebRequest request = System.Net.HttpWebRequest.Create(url);
            //String destPath = "C:\\1.prj";
            //Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            //using (var sReader = new StreamReader(request.GetResponse().GetResponseStream()))
            //{
            //    using (var sWriter = new StreamWriter(destPath))
            //    {
            //        sWriter.Write(sReader.ReadToEnd());
            //    }
            //}
        }
    }
}
