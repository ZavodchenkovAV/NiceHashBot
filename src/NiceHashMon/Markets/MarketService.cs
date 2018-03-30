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
            new Southxchange()
        };
        public  List<MarketPrice> GetPrices(Coin coin)
        {
            List<MarketPrice> result = new List<MarketPrice>();
            markertList.ForEach(m => result.Add( m.GetPrice(coin.ShortName)));
            return result;
        }
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
    }
}
