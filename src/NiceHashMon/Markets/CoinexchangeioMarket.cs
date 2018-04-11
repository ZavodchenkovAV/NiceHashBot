using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class CoinexchangeioMarket : IMarket, IRefreshable
    {
        private CoinexchangeioMarketRoot root;
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "Coinexchangeio", CoinShortName = shortName };
            try
            {
                if (root == null)
                    root = await HttpHelper.GetAsync<CoinexchangeioMarketRoot>($"https://www.coinexchange.io/api/v1/getmarkets");
                if (root.success == "1")
                {
                    var findMarket = root.result.FirstOrDefault(f => f.MarketAssetCode.Equals($"{shortName.ToUpper()}") && f.BaseCurrencyCode=="BTC");
                    if (findMarket != null)
                    {
                        var find = await HttpHelper.GetAsync<CoinexchangeioMarketSummaryRoot>($"https://www.coinexchange.io/api/v1/getmarketsummary?market_id={findMarket.MarketID}");
                        if (find != null && find.success=="1" && find.result!=null)
                        {
                            marketPrice.Price = find.result.BidPrice;
                            marketPrice.Volume = find.result.Volume;
                            marketPrice.IsGetPrice = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }
        public void Refresh()
        {
            root = null;
        }
    }
    public class CoinexchangeioMarketResult
    {
        public string MarketID { get; set; }
        public string MarketAssetName { get; set; }
        public string MarketAssetCode { get; set; }
        public string MarketAssetID { get; set; }
        public string MarketAssetType { get; set; }
        public string BaseCurrency { get; set; }
        public string BaseCurrencyCode { get; set; }
        public string BaseCurrencyID { get; set; }
        public bool Active { get; set; }
    }

    public class CoinexchangeioMarketRoot
    {
        public string success { get; set; }
        public string request { get; set; }
        public string message { get; set; }
        public List<CoinexchangeioMarketResult> result { get; set; }
    }
    public class CoinexchangeioMarketSummaryResult
    {
        public string MarketID { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string HighPrice { get; set; }
        public string LowPrice { get; set; }
        public double Volume { get; set; }
        public string BTCVolume { get; set; }
        public string TradeCount { get; set; }
        public double BidPrice { get; set; }
        public string AskPrice { get; set; }
        public string BuyOrderCount { get; set; }
        public string SellOrderCount { get; set; }
    }

    public class CoinexchangeioMarketSummaryRoot
    {
        public string success { get; set; }
        public string request { get; set; }
        public string message { get; set; }
        public CoinexchangeioMarketSummaryResult result { get; set; }
    }
}
