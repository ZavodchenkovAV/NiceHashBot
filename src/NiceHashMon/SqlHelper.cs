using ErikEJ.SqlCe;
using NiceHashBotLib;
using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon
{
    public static class SqlHelper
    {
        private static int hoursDeleteInterval = 12;
        public static void DeleteOldStat()
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                var query1 = $@"delete from AlgorithmStat where sdatetime < dateadd(hh, -{hoursDeleteInterval}, getdate())";
                SqlCeCommand command1 = new SqlCeCommand(query1, conn);
                command1.ExecuteNonQuery();

                var query2 = $@"delete from ProfitStat where pdatetime < dateadd(hh, -{hoursDeleteInterval}, getdate())";
                SqlCeCommand command2 = new SqlCeCommand(query2, conn);
                command2.ExecuteNonQuery();
            }
        }

        public static void AlgStatBulkInsert(APIStatsCurrent stats)
        {
            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();
            var reader = new AlgorithmStatDataReader(stats);
            using (SqlCeBulkCopy bc = new SqlCeBulkCopy(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString, options))
            {
                bc.DestinationTableName = "AlgorithmStat";
                bc.WriteToServer(reader);
            }
        }
        public static void ProfitStatBulkInsert(IEnumerable<CoinProfit> coinProfitList)
        {
            var filteredList = coinProfitList.Where(c => !c.ProfitCount.Equals(double.NaN) && !c.ProfitCountPercent.Equals(double.NaN));
            if (filteredList.Count() == 0) return;

            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();
            var reader = new ProfitStatDataReader(filteredList);
            using (SqlCeBulkCopy bc = new SqlCeBulkCopy(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString, options))
            {
                bc.DestinationTableName = "ProfitStat";
                bc.WriteToServer(reader);
            }
        }
        public static Dictionary<string, ProfitStat> GetProfitStat(DateTime from, DateTime to)
        {
            List<ProfitStat> result = new List<ProfitStat>();
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();
                var query = $@"select CoinName, avg(ProfitCount) as AvgProfitCount,avg(ProfitCountPercent) as AvgProfitCountPercent
from ProfitStat
where PDateTime between @p0 and @p1
group by CoinName";
                SqlCeCommand command = new SqlCeCommand(query, conn);
                command.Parameters.AddWithValue("@p0", from);
                command.Parameters.AddWithValue("@p1", to);
                using (var sqlreader = command.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        object[] v = new object[sqlreader.FieldCount];
                        sqlreader.GetValues(v);
                        ProfitStat ps = new ProfitStat()
                        {
                            CoinName = (string)v[0],
                            AvgProfitCount = (double)v[1],
                            AvgProfitCountPercent = (double)v[2]
                        };
                        
                        result.Add(ps);
                    }
                }
            }
            return result.ToDictionary(r=>r.CoinName);
        }


        public static void DeleteAllCoins()
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                var query1 = $@"delete from Coin";
                SqlCeCommand command1 = new SqlCeCommand(query1, conn);
                command1.ExecuteNonQuery();
            }
        }

        public static void UpdateCoinByProfitTab(CoinProfit coinProfit)
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                var query1 = $@"update Coin set Coeff=@p0, MiningPrice=@p1 where CoinName=@p2";
                SqlCeCommand command1 = new SqlCeCommand(query1, conn);
                command1.Parameters.AddWithValue("@p0", coinProfit.Coeff);
                command1.Parameters.AddWithValue("@p1", coinProfit.MiningPrice);
                command1.Parameters.AddWithValue("@p2", coinProfit.CoinName);
                command1.ExecuteNonQuery();
            }
        }
    }
}
