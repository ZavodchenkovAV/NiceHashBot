using NiceHashMon.Markets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class Coin
    {
        public long? ID { get; set; }
        public string CoinName { get; set; }
        public string ShortName { get; set; }
        public AlgorithmEnum Algorithm { get; set; }
        public double HashRate { get; set; }
        public string ExplorerUrl { get; set; }
        public int BlockTime { get; set; }
        public double CoinPrize { get; set; }
        public double ActualPrice { get; set; }
        public string ActualPools { get; set; }
        public bool HashFromExplorer { get; set; }
        public short HashCoeff { get; set; }

        public double CoinPerDay
        {
            get
            {
                return (86400 * CoinPrize)/BlockTime;
            }
        }

        public List<MarketPrice> Prices { get; set; }

        public void Refresh(MarketService marketService)
        {
            GetCoinApi();
            if(!string.IsNullOrEmpty(ShortName))
                Prices = marketService.GetPricesAsync(this);
        }

        private void GetCoinApi()
        {
            if (string.IsNullOrEmpty(ExplorerUrl)) return;
            var query = ExplorerUrl.EndsWith("/") ? $"{ExplorerUrl}api/getnetworkhashps" : $"{ExplorerUrl}/api/getnetworkhashps";
            try
            {
                var responseData = HttpHelper.Get(query);
                HashRate = Math.Pow(10,HashCoeff) *  Convert.ToDouble(responseData, CultureInfo.InvariantCulture) / Algorithm.GetRateCoeff();
                HashFromExplorer = true;
            }
            catch (Exception e)
            {
                HashFromExplorer = false;
            }
        }
    }
}
