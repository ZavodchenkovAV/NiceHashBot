using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NiceHashBotLib
{
    public class APIStatsCurrent
    {
        [JsonProperty(PropertyName = "stats")]
        public APIStats[] AllStats;
    }

    public class APIStats
    {
        [JsonProperty(PropertyName = "algo")]
        public int Algorithm;

        [JsonProperty(PropertyName = "speed")]
        public double TotalSpeed;

        [JsonProperty(PropertyName = "price")]
        public double Price;

        [JsonProperty(PropertyName = "profitability_btc")]
        public double Profitability_btc;

        [JsonProperty(PropertyName = "profitability_above_btc")]
        public double Profitability_above_btc;

        [JsonProperty(PropertyName = "profitability_ltc")]
        public double Profitability_ltc;

        [JsonProperty(PropertyName = "profitability_above_ltc")]
        public double Profitability_above_ltc;
    }
}
