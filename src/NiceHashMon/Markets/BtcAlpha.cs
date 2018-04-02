using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class BtcAlpha : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "BtcAlpha", CoinShortName = shortName };
            try
            {
                var result = HttpHelper.Get($"https://btc-alpha.com/api/v1/exchanges/?pair={shortName}_BTC&price=");
                var root = JsonConvert.DeserializeObject<BtcAlphaRootObject[]>(result);
                if (root.Length>0)
                {
                    var amount = root.Sum(r => r.amount);
                    var fullPrice = root.Sum(r => r.amount * r.price);
                    marketPrice.Price = fullPrice / amount;
                    marketPrice.IsGetPrice = true;
                }
               
            }
            catch (Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "BtcAlpha", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync< BtcAlphaRootObject[]>($"https://btc-alpha.com/api/v1/exchanges/?pair={shortName}_BTC&price=");
                if (root.Length > 0)
                {
                    var amount = root.Sum(r => r.amount);
                    var fullPrice = root.Sum(r => r.amount * r.price);
                    marketPrice.Price = fullPrice / amount;
                    marketPrice.Volume = amount;
                    marketPrice.IsGetPrice = true;
                }

            }
            catch (Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }
    }
    public class BtcAlphaRootObject
    {
        public int id { get; set; }
        public double timestamp { get; set; }
        public string pair { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public string type { get; set; }
    }
}
