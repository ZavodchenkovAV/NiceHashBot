﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Markets
{
    public class CryptoBridge : IMarket,IRefreshable
    {
        private CryptoBridgeRoot[] root;
        public MarketPrice GetPrice(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "CryptoBridge", CoinShortName = shortName };
            try
            {
                var result = HttpHelper.Get($"https://api.crypto-bridge.org/api/v1/ticker");
                var root = JsonConvert.DeserializeObject<CryptoBridgeRoot[]>(result);
                var find = root.FirstOrDefault(f => f.id.Equals($"{shortName}_BTC"));
                if (find != null)
                {
                    marketPrice.Price = find.bid;
                    marketPrice.IsGetPrice = true;
                }
            }
            catch (Exception e)
            {
                marketPrice.IsGetPrice = false;
            }
            return marketPrice;
        }

        public async Task<MarketPrice> GetPriceAsync(string shortName)
        {
            var marketPrice = new MarketPrice() { MarketName = "CryptoBridge", CoinShortName = shortName };
            try
            {
                if(root==null)
                    root = await HttpHelper.GetAsync<CryptoBridgeRoot[]>($"https://api.crypto-bridge.org/api/v1/ticker");
                var find = root.FirstOrDefault(f => f.id.Equals($"{shortName.ToUpper()}_BTC"));
                if(find!=null)
                {
                    marketPrice.Price = find.bid;
                    marketPrice.Volume = find.volume/find.bid;
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
    public class CryptoBridgeRoot
    {
        public string id { get; set; }
        public string last { get; set; }
        public double volume { get; set; }
        public string ask { get; set; }
        public double bid { get; set; }
    }
}
