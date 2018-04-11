using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class ProfitStat
    {
        public string CoinName { get; set; }
        public double AvgProfitCount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public double AvgProfitCountPercent { get; set; }
    }
}
