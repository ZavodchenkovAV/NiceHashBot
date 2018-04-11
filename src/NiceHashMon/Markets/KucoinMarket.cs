using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class KucoinMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Kucoin", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync<KucoinRoot>($"https://api.kucoin.com/v1/open/tick?symbol={shortName.ToUpper()}-BTC");
                if (root != null && root.success && root.data != null)
                {
                    marketPrice.Price = root.data.buy;
                    marketPrice.Volume = root.data.vol;
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
    public class KucoinData
    {
        public string coinType { get; set; }
        public bool trading { get; set; }
        public string symbol { get; set; }
        public double lastDealPrice { get; set; }
        public double buy { get; set; }
        public double sell { get; set; }
        public double change { get; set; }
        public string coinTypePair { get; set; }
        public int sort { get; set; }
        public double feeRate { get; set; }
        public double volValue { get; set; }
        public double high { get; set; }
        public long datetime { get; set; }
        public double vol { get; set; }
        public double low { get; set; }
        public double changeRate { get; set; }
    }

    public class KucoinRoot
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public long timestamp { get; set; }
        public KucoinData data { get; set; }
    }
}
