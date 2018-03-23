using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public class AlgorithmAvg
    {
        public AlgorithmEnum Algorithm { get; set; }
        public double AvgSpeed { get; set; }
        public double CurrentSpeed { get; set; }
        public double AvgPrice { get; set; }
        public double CurrentPrice { get; set; }        
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
