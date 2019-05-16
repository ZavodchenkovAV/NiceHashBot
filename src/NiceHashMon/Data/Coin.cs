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
        private double _actualPrice;
        public double ActualPrice
        {
            get
            {
                return _actualPrice;
            }
            set
            {
                if (_actualPrice != value)
                {
                    var oldActualPrice = _actualPrice;
                    _actualPrice = value;
                    ActualPriceChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActualPriceChanged;

        public string ActualPools { get; set; }
        public bool HashFromExplorer { get; set; }
        public short HashCoeff { get; set; }

        public double Coeff { get; set; }

        public double MiningPrice { get; set; }

        public int OrderId { get; set; }

        public double CoinPerDay
        {
            get
            {
                return (86400 * CoinPrize)/BlockTime;
            }
        }

        public SortableBindingList<MarketPrice> Prices { get; set; }

        public async Task Refresh(MarketService marketService)
        {
            await GetCoinApi();
            if (!string.IsNullOrEmpty(ShortName))
            {
                var priceList = await marketService.GetPricesFullAsync(this);
                Prices = new SortableBindingList<MarketPrice>(priceList);
                if (Prices.Count > 0)
                    ActualPrice = Prices.OrderByDescending(p => p.Volume).FirstOrDefault().Price;

                //var t1 = marketService.GetPricesFullAsync(this);

                //var t2 = t1.ContinueWith((priceList) =>
                //{                    
                //    if (priceList.Result.Count > 0)
                //        ActualPrice = priceList.Result.OrderByDescending(p => p.Volume).FirstOrDefault().Price;
                //});
                //await t2;
                //Prices = new SortableBindingList<MarketPrice>(t1.Result);
            }
        }

        private async Task GetCoinApi()
        {
            if (string.IsNullOrEmpty(ExplorerUrl)) return;
            //var query = ExplorerUrl.EndsWith("/") ? $"{ExplorerUrl}api/getnetworkhashps" : $"{ExplorerUrl}/api/getnetworkhashps";
            try
            {
                var responseData = await HttpHelper.GetAsync(ExplorerUrl);
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
