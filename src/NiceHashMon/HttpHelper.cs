using NiceHashMon.Data;
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

        public static async Task<List<OrderN>> GetAllOrders(string id, string key)
        {
            List<OrderN> result = new List<OrderN>();
            var values = Enum.GetValues(typeof(AlgorithmEnum)).OfType<AlgorithmEnum>().Select(a=>(int)a).ToList();
            await Task.Run(() =>
            {
                values.ForEach(async value =>
                {
                    result.AddRange(await GetOrderListByLocation(id, key, 0, value));
                    result.AddRange(await GetOrderListByLocation(id, key, 1, value));
                });
                return result;

            });
            if(result.Count==0)
            {

            }
            return result;
        }

        public static async Task<List<OrderN>> GetOrderListByLocation(string id, string key,int location, int value)
        {
            try
            {
                var order0 = await HttpHelper.GetAsync<OrderNRoot>($"https://api.nicehash.com/api?method=orders.get&my&id={id}&key={key}&location={location}&algo={value}");
                if (order0.result.orders.Count > 0)
                {
                    order0.result.orders.ForEach(o => o.location = 0);
                    return order0.result.orders;
                }
            }
            catch(Exception e)
            {
                return new List<OrderN>();
            }
            return new List<OrderN>();
        }
    }
}
