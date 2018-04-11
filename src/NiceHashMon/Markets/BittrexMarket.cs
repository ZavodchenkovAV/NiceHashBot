using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class BittrexMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Bittrex", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync<BittrexRoot>($"https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-{shortName.ToLower()}");
                if (root != null && root.success && root.result != null && root.result.FirstOrDefault()!=null)
                {
                    var result = root.result.FirstOrDefault();
                    marketPrice.Price = result.Bid;
                    marketPrice.Volume = result.Volume;
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
    public class BittrexResult
    {
        public string MarketName { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
        public double Last { get; set; }
        public double BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public double PrevDay { get; set; }
        public DateTime Created { get; set; }
    }

    public class BittrexRoot
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BittrexResult> result { get; set; }
    }
}
