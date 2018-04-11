using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class YobitMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Yobit", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync<YobitRoot>($"https://yobit.net/api/2/{shortName.ToLower()}_btc/ticker");
                if (root != null && root.ticker!= null)
                {
                    marketPrice.Price = root.ticker.buy;
                    marketPrice.Volume = root.ticker.vol_cur;
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
    public class YobitTicker
    {
        public double high { get; set; }
        public double low { get; set; }
        public double avg { get; set; }
        public double vol { get; set; }
        public double vol_cur { get; set; }
        public double last { get; set; }
        public double buy { get; set; }
        public double sell { get; set; }
        public int updated { get; set; }
        public int server_time { get; set; }
    }

    public class YobitRoot
    {
        public YobitTicker ticker { get; set; }
    }
}
