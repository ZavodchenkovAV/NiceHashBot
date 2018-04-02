using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class MarketPrice
    {
        public string MarketName { get; set; }
        public double Price { get; set; }
        public string CoinShortName { get; set; }
        public double Volume { get; set; }
        public bool IsGetPrice { get; set; }
    }
}
