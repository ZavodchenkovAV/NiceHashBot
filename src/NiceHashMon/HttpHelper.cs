using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon
{
    public class HttpHelper
    {
        public static string Get(string query)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            HttpWebRequest WReq = (HttpWebRequest)WebRequest.Create(query);
            WReq.Proxy = null;
            WReq.Timeout = 60000;
            WebResponse WResp = WReq.GetResponse();
            Stream DataStream = WResp.GetResponseStream();
            DataStream.ReadTimeout = 60000;
            StreamReader SReader = new StreamReader(DataStream);
            var result = SReader.ReadToEnd();
            //sw.Stop();
            //Trace.WriteLine($"query={query}, datetime={DateTime.Now}, sw={sw.ElapsedMilliseconds}");
            return result;
        }
        public static async Task<T> GetAsync<T>(string query)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            var service = new JsonServiceClient();
            var r = await service.GetAsync<T>(query);            
            service.Dispose();
            //sw.Stop();
            //Trace.WriteLine($"query={query}, datetime={DateTime.Now}, sw={sw.ElapsedMilliseconds}");
            return r;
        }
        public static async Task<string> GetAsync(string query)
        {
            HttpWebRequest WReq = (HttpWebRequest)WebRequest.Create(query);
            WReq.Proxy = null;
            WReq.Timeout = 60000;
            WebResponse WResp = await WReq.GetResponseAsync();
            Stream DataStream = WResp.GetResponseStream();
            DataStream.ReadTimeout = 60000;
            StreamReader SReader = new StreamReader(DataStream);
            var result = await SReader.ReadToEndAsync();
            return result;
        }
    }
}
