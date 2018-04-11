using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class GraviexMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Graviex", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync<GraviexRootObject>($"https://graviex.net:443//api/v2/tickers/{shortName.ToLower()}btc.json");
                if (root!=null && root.ticker!=null)
                {
                    marketPrice.Price = root.ticker.buy;
                    marketPrice.Volume = root.ticker.vol;
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
    public class GraviexTicker
    {
        public double buy { get; set; }
        public double sell { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string last { get; set; }
        public double vol { get; set; }
        public int volbtc { get; set; }
        public string change { get; set; }
    }

    public class GraviexRootObject
    {
        public int at { get; set; }
        public GraviexTicker ticker { get; set; }
    }
}
