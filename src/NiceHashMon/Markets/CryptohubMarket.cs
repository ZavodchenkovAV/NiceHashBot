using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class CryptohubMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Cryptohub", CoinShortName = shortName };
            try
            {
                var response = await HttpHelper.GetAsync($"https://cryptohub.online/api/market/ticker/{shortName.ToUpper()}/");
                var obj = JsonConvert.DeserializeObject(response) as Newtonsoft.Json.Linq.JObject;
                if(obj.HasValues)
                {
                    var first = obj.Children().OfType<Newtonsoft.Json.Linq.JProperty>().FirstOrDefault(c => c.Name == $"BTC_{shortName.ToUpper()}");
                    if(first!=null && first.FirstOrDefault()!=null)
                    {
                        var childs = first.FirstOrDefault().Children().OfType<Newtonsoft.Json.Linq.JProperty>();
                        var last = childs.FirstOrDefault(c => c.Name == "last");
                        marketPrice.Price = Convert.ToDouble((last.Value as Newtonsoft.Json.Linq.JValue).Value);
                        marketPrice.Volume = 1;
                        marketPrice.IsGetPrice = true;
                    }
                }
                //var values = root2.Values<CryptohubValue>();
                //var root = await HttpHelper.GetAsync<YobitRoot>($"https://cryptohub.online/api/market/ticker/{shortName.ToUpper()}/");
                //if (root != null && root.ticker != null)
                //{
                //    marketPrice.Price = root.ticker.buy;
                //    marketPrice.Volume = root.ticker.vol_cur;
                //    marketPrice.IsGetPrice = true;
                //}

            }
            catch (Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }
    }
    public class CryptohubValue
    {
        public double baseVolume { get; set; }
        public double high24hr { get; set; }
        public double highestBid { get; set; }
        public int id { get; set; }
        public string isFrozen { get; set; }
        public double last { get; set; }
        public double low24hr { get; set; }
        public double lowestAsk { get; set; }
        public double percentChange { get; set; }
        public double quoteVolume { get; set; }
    }
}
