using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class MarketService
    {
        private readonly List<IMarket> markertList = new List<IMarket>()
        {
            new CryptopiaMarket(),
            new CryptoBridge(),
            new BtcAlpha(),
            new Southxchange(),
            new GraviexMarket(),
            new StocksExchangeMarket(),
            new KucoinMarket(),
            new CoinexchangeioMarket(),
            new BittrexMarket(),
            new YobitMarket(),
            new CryptohubMarket()
        };
        //public  List<MarketPrice> GetPrices(Coin coin)
        //{
        //    List<MarketPrice> result = new List<MarketPrice>();
        //    markertList.ForEach(m => result.Add( m.GetPrice(coin.ShortName)));
        //    return result;
        //}
        public  List<MarketPrice> GetPricesAsync(Coin coin)
        {
            List<MarketPrice> result = new List<MarketPrice>();
            markertList.ForEach(async m =>
            {
                var price = await m.GetPriceAsync(coin.ShortName);
                result.Add(price);
            });
            return result;
        }
        public async Task<List<MarketPrice>> GetPricesFullAsync(Coin coin)
        {
            List<MarketPrice> result = new List<MarketPrice>();
            foreach(var m in markertList)
            {
                var price = await m.GetPriceAsync(coin.ShortName);
                result.Add(price);

            }
            return result;
        }
        public void Refresh()
        {
            markertList.Where(m => m is IRefreshable).Select(m => m as IRefreshable).ToList().ForEach(m => m.Refresh());    
        }
    }
}
