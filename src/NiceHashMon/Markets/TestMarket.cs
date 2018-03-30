using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class TestMarket : IMarket
    {
        public MarketPrice GetPrice(string shortName)
        {
            return new MarketPrice() { MarketName = "Test", Price = 0, CoinShortName = shortName, IsGetPrice = false };
        }

        public Task<MarketPrice> GetPriceAsync(string shortName)
        {
            throw new NotImplementedException();
        }
    }
}
