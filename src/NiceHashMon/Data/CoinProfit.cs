using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class CoinProfit
    {
        private Coin _coin;
        private AlgorithmAvg _algorithmAvg;
        public CoinProfit(Coin coin,AlgorithmAvg algorithmAvg)
        {
            _coin = coin;
            _algorithmAvg = algorithmAvg;
            Coeff = 0.95;
            MiningPrice = 1;
        }

        public string CoinName => _coin.CoinName;

        public double Coeff { get; set; }

        public double ActualPrice => _coin.ActualPrice;

        public double MiningPrice {get; set; }

        public string ProfitMiningDisplay
        {
            get
            {
                var profitMining = ActualPrice - MiningPrice;
                var profitMiningPercent = 100 * profitMining/CountPrice;
                return $"{profitMining}/{profitMiningPercent.ToString("00.000")}%";
            }
        }

        private double _ourHash;
        public double OurHash
        {
            get
            {                
                return _ourHash;
            }
        }
        private double _countPrice;

        public double CountPrice
        {
            get
            {                
                return _countPrice;
            }
        }

        private double _profitCount;

        public double ProfitCount
        {
            get
            {
                return _profitCount;
            }
        }

        public string ProfitCountDisplay
        {
            get
            {
                var profitCountPercent = 100 * ProfitCount / CountPrice;
                return $"{ProfitCount.ToString("00.000000")}/{profitCountPercent.ToString("00.000")}%";
            }
        }
        private double _btcday;

        public double BtcDay
        {
            get
            {
                return _btcday;
            }
        }

        private double CalcOurHash()
        {
            List<double> correctOur = new List<double>();
            for (int i = 1; i <= 20; i++)
            {
                var tempOurHash = (_coin.HashRate * i) / 100;
                var f1 = (GetProfitCount(tempOurHash) * 100) / GetCountPrice(tempOurHash);
                if (f1 > 15 && GetBtcDay(tempOurHash) <= 0.05 && GetProfitCount(tempOurHash) > 0.0005)
                    correctOur.Add(tempOurHash);
            }
            IsProfit = correctOur.Count > 0;

            return correctOur.Count>0? correctOur.Max():_coin.HashRate/100;
        }
        public event EventHandler IsProfitChanged;
        private bool? _isProfit;
        public bool? IsProfit
        {
            get { return _isProfit; }
            set
            {
                if(_isProfit.HasValue && _isProfit.Value!=value)
                {
                    _isProfit = value;
                    IsProfitChanged?.Invoke(this, EventArgs.Empty);                    
                }
                else
                    _isProfit = value;
            }
        }

        private double GetProfitCount(double ourHash)
        {
            return (_coin.ActualPrice - GetCountPrice(ourHash)) / GetOurPrize(ourHash);
        }

        private double GetCountPrice(double ourHash)
        {
            return (_algorithmAvg.AvgPrice * ourHash) / (GetOurPrize(ourHash) * Coeff);
        }

        private double GetOurPrize(double ourHash)
        {
            return (_coin.CoinPerDay * ourHash) / (_coin.HashRate + ourHash);
        }

        private double GetBtcDay(double ourHash)
        {
            return (_algorithmAvg.AvgPrice * ourHash) / 0.97;
        }

        public void Refresh()
        {
            _ourHash = CalcOurHash();
            _countPrice = (_algorithmAvg.AvgSpeed * OurHash) / (GetOurPrize(OurHash) * Coeff);
            _profitCount = GetProfitCount(OurHash);
            _btcday = GetBtcDay(OurHash);
        }
    }
}
