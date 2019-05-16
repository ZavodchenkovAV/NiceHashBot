using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class OrderN
    {
        public int type { get; set; }
        public string btc_avail { get; set; }
        public string limit_speed { get; set; }
        public string pool_user { get; set; }
        public int pool_port { get; set; }
        public bool alive { get; set; }
        public int workers { get; set; }
        public string pool_pass { get; set; }
        public double accepted_speed { get; set; }
        public int id { get; set; }
        public int algo { get; set; }
        public string price { get; set; }
        public string btc_paid { get; set; }
        public string pool_host { get; set; }
        public long end { get; set; }
        public int location { get; set; }
    }

    public class OrderNResult
    {
        public List<OrderN> orders { get; set; }
        public int timestamp { get; set; }
    }

    public class OrderNRoot
    {
        public OrderNResult result { get; set; }
        public string method { get; set; }
    }
}
