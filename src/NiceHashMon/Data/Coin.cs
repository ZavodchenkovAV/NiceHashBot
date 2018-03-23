using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class Coin
    {
        public long? ID { get; set; }
        public string CoinName { get; set; }
        public AlgorithmEnum Algorithm { get; set; }
        public double HashRate { get; set; }
        public string ExplorerUrl { get; set; }
        public int BlockTime { get; set; }
        public double CoinPrize { get; set; }
        //public int CoinPerDay { get; set; }
        public double ActualPrice { get; set; }
        public string ActualPools { get; set; }
        //public double Profit { get; set; }
        public bool HashFromExplorer { get; set; }

        public double CoinPerDay
        {
            get
            {
                return (86400 * CoinPrize)/BlockTime;
            }
        }
    }
}
