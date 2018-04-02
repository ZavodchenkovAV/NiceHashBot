using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class Southxchange:IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Southxchange", CoinShortName = shortName };
            try
            {
                var result = HttpHelper.Get($"https://www.southxchange.com/api/price/{shortName}/BTC");
                var root = JsonConvert.DeserializeObject<SouthxchangeRootObject>(result);
                if (root != null)
                {
                    marketPrice.Price = root.Bid;
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
            var marketPrice = new MarketPrice() { MarketName = "Southxchange", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync< SouthxchangeRootObject>($"https://www.southxchange.com/api/price/{shortName}/BTC");
                //var root = JsonConvert.DeserializeObject<SouthxchangeRootObject>(result);
                if (root != null)
                {
                    marketPrice.Price = root.Bid;
                    marketPrice.Volume = root.Volume24Hr;
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
    public class SouthxchangeRootObject
    {
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
        public double Variation24Hr { get; set; }
        public double Volume24Hr { get; set; }
    }
}
