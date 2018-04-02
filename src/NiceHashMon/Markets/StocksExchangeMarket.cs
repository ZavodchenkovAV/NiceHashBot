using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class StocksExchangeMarket : IMarket, IRefreshable
    {
        private StocksExchangeRoot[] root;
        public MarketPrice GetPrice(string shortName)
        {
            throw new NotImplementedException();
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "StocksExchange", CoinShortName = shortName };
            try
            {
                if (root == null)
                    root = await HttpHelper.GetAsync<StocksExchangeRoot[]>($"https://stocks.exchange/api2/ticker");

                var find = root.FirstOrDefault(f => f.market_name.Equals($"{shortName}_BTC"));
                if (find != null)
                {
                    marketPrice.Price = find.bid;
                    marketPrice.Volume = find.vol;
                    marketPrice.IsGetPrice = true;
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
    public class StocksExchangeRoot
    {
        public string min_order_amount { get; set; }
        public object ask { get; set; }
        public double bid { get; set; }
        public object last { get; set; }
        public object lastDayAgo { get; set; }
        public double vol { get; set; }
        public object spread { get; set; }
        public string buy_fee_percent { get; set; }
        public string sell_fee_percent { get; set; }
        public string market_name { get; set; }
        public int updated_time { get; set; }
        public int server_time { get; set; }
    }
}
