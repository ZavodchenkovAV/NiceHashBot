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

        public double ProfitMining
        {
            get
            {
                return (ActualPrice - MiningPrice) * GetOurPrize(OurHash);
            }
        }

        public double ProfitMiningPercent
        {
            get
            {
                return (ActualPrice - MiningPrice)  / MiningPrice;
                //return $"{profitMining.ToString("00.00000000")}/{profitMiningPercent.ToString("00.000")}%";
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

        private double _ourPrize;
        public double OurPrize
        {
            get
            {
                return _ourPrize;
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
        

        public double ProfitCount
        {
            get
            {
                return (ActualPrice - CountPrice) * OurPrize;
            }
        }

        public double ProfitCountPercent
        {
            get
            {
                var profitCountPercent = (ActualPrice-CountPrice)  / CountPrice;
                if(profitCountPercent>=0.15 && (!_isProfit.HasValue || !_isProfit.Value))
                    _isProfit = true;
                else if (profitCountPercent < 0.15 && _isProfit.HasValue && _isProfit.Value)
                    _isProfit = false;
                return profitCountPercent;
                //return $"{profitCount.ToString("00.00000000")}/{profitCountPercent.ToString("00.000")}%";
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
        public event EventHandler IsProfitChanged;
        private bool? _isProfit;
        public bool? IsProfit
        {
            get { return _isProfit; }
            set
            {
                var oldIsprofit = _isProfit;
                if( _isProfit!=value)
                {
                    _isProfit = value;
                    if(!oldIsprofit.HasValue && value.Value || oldIsprofit.HasValue)
                        IsProfitChanged?.Invoke(this, EventArgs.Empty);                    
                }
                else
                    _isProfit = value;
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
            _ourHash = CalcOurHash();
            _ourPrize = GetOurPrize(OurHash);
            _countPrice = (_algorithmAvg.AvgPrice * OurHash) / ( _ourPrize * Coeff);
            _btcday = GetBtcDay(OurHash);
            //var profit
        }
    }
}
