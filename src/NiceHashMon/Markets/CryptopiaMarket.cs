using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class CryptopiaMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Cryptopia", CoinShortName = shortName};
            try
            {
                var result = HttpHelper.Get($"https://www.cryptopia.co.nz/api/GetMarket/{shortName}_BTC");
                var root = JsonConvert.DeserializeObject<CryptopiaRoot>(result);
                //var root = HttpHelper.Get<CryptopiaRoot>($"https://www.cryptopia.co.nz/api/GetMarket/{shortName}_BTC");
                if (root != null && root.Success && root.Error == null && root.Data!=null)
                {
                    marketPrice.Price = root.Data.BidPrice;
                    marketPrice.IsGetPrice = true;
                }
            }
            catch(Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Cryptopia", CoinShortName = shortName };
            try
            {
                var root = await HttpHelper.GetAsync<CryptopiaRoot>($"https://www.cryptopia.co.nz/api/GetMarket/{shortName}_BTC");
                if (root != null && root.Success && root.Error == null && root.Data != null)
                {
                    marketPrice.Price = root.Data.BidPrice;
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

    public class CryptopiaData
    {
        public int TradePairId { get; set; }
        public string Label { get; set; }
        public double AskPrice { get; set; }
        public double BidPrice { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Volume { get; set; }
        public double LastPrice { get; set; }
        public double BuyVolume { get; set; }
        public double SellVolume { get; set; }
        public double Change { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double BaseVolume { get; set; }
        public double BuyBaseVolume { get; set; }
        public double SellBaseVolume { get; set; }
    }

    public class CryptopiaRoot
    {
        public bool Success { get; set; }
        public object Message { get; set; }
        public CryptopiaData Data { get; set; }
        public object Error { get; set; }
    }
}
