using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public interface IMarket
    {
        //string Name { get; }
        MarketPrice GetPrice(string shortName);
        Task<MarketPrice> GetPriceAsync(string shortName);
    }
}
