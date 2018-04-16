using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class CoinProfit
    {
        public class CoinProfitChangedArgs:EventArgs
        {
            public bool OldProfit { get; set; }
            public bool NewProfit { get; set; }
        }
        private Coin _coin;
        private AlgorithmAvg _algorithmAvg;
        private const double profitConstPercent = 0.15;
        public CoinProfit(Coin coin,AlgorithmAvg algorithmAvg)
        {
            _coin = coin;
            _algorithmAvg = algorithmAvg;
            _coin.ActualPriceChanged -= _coin_ActualPriceChanged;
            _coin.ActualPriceChanged += _coin_ActualPriceChanged;
        }

        private void _coin_ActualPriceChanged(object sender, EventArgs e)
        {
            UpdateByActualPrice();
        }

        public string CoinName => _coin.CoinName;

        public double Coeff
        {
            get { return _coin.Coeff; }
            set { _coin.Coeff = value; }
        }

        public double ActualPrice => _coin.ActualPrice;

        public double MiningPrice
        {
            get { return _coin.MiningPrice; }
            set { _coin.MiningPrice = value; }
        }

        public double ProfitMining { get; set; }

        private void UpdateByActualPrice()
        {
            ProfitMining = (ActualPrice - MiningPrice) * OurPrize;
            ProfitMiningPercent = (ActualPrice - MiningPrice) / MiningPrice;
            ProfitCount = (ActualPrice - CountPrice) * OurPrize;
            ProfitCountPercent = (ActualPrice - CountPrice) / CountPrice;
            IsProfit = ProfitCountPercent >= profitConstPercent;
        }

        public double ProfitMiningPercent { get; set; }

        public double OurHash { get; set; }

        public double OurPrize { get; set; }
        
        public double CountPrice { get; set; }
        
        public double ProfitCount { get; set; }

        public double ProfitCountPercent { get; set; }        

        public double BtcDay { get; set; }

        public double ProfitCountC1 { get; set; }
        public double ProfitCountC1Percent { get; set; }
        public double ProfitCountC2 { get; set; }
        public double ProfitCountC2Percent { get; set; }
        public double ProfitCountC3 { get; set; }
        public double ProfitCountC3Percent { get; set; }
        public double ProfitCountC6 { get; set; }
        public double ProfitCountC6Percent { get; set; }

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
            //IsProfit = correctOur.Count > 0;

            return correctOur.Count>0? correctOur.Max():_coin.HashRate/100;
        }
        public event EventHandler<CoinProfitChangedArgs> IsProfitChanged;
        private bool _isProfit;
        public bool IsProfit
        {
            get { return _isProfit; }
            set
            {
                var oldIsprofit = _isProfit;
                _isProfit = value;
                if (!_isProfit || ProfitCountC1Percent > profitConstPercent)
                    IsProfitChanged?.Invoke(this, new CoinProfitChangedArgs() { OldProfit = oldIsprofit, NewProfit = value });
            }
        }

        private double GetProfitCount(double ourHash)
        {
            return (_coin.ActualPrice - GetCountPrice(ourHash)) * GetOurPrize(ourHash);
        }

        private double GetCountPrice(double ourHash)
        {
            var ourPrize = GetOurPrize(ourHash);
            return (_algorithmAvg.AvgPrice * ourHash) /  (ourPrize * Coeff);
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
            OurHash = CalcOurHash();
            OurPrize = GetOurPrize(OurHash);
            CountPrice = (_algorithmAvg.AvgPrice * OurHash) / ( OurPrize * Coeff);
            BtcDay = GetBtcDay(OurHash);
            UpdateByActualPrice();
        }
    }
}
