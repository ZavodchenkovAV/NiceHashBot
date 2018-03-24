using ErikEJ.SqlCe;
using NiceHashBotLib;
using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace NiceHashMon
{
    public partial class MonForm : Form
    {
        private int hoursInterval = 6;
        private int hoursDeleteInterval = 12;
        private List<Coin> coinList = new List<Coin>();
        private List<AlgorithmAvg> algorithmAvgList = new List<AlgorithmAvg>();
        private Dictionary<AlgorithmEnum, AlgorithmAvg> avgDict = new Dictionary<AlgorithmEnum, AlgorithmAvg>();
        private TelegramNotifyBot bot = new TelegramNotifyBot();
        private Dictionary<string, bool?> profitdict = new Dictionary<string, bool?>();
        public MonForm()
        {
            InitializeComponent();
        }

        private void MonForm_Load(object sender, EventArgs e)
        {
            deleteOldStat();
            GetAndInsertStat();
            GetCoins();
            timerAlgoritm.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["UpdateTime"]);
            timerAlgoritm.Start();
            timerDelete.Start();
        }

        private void GetCoins()
        {
            coinList.Clear();
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();
                var query = $@"select * from Coin";
                SqlCeCommand command = new SqlCeCommand(query, conn);
                using (var sqlreader = command.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        object[] v = new object[sqlreader.FieldCount];
                        sqlreader.GetValues(v);                        
                        var algorithm = (int)v[2];
                        var coin = new Coin()
                        {
                            CoinName = (string)v[1],
                            Algorithm = (AlgorithmEnum)algorithm,
                            HashRate = (double)v[3],
                            ExplorerUrl = (string)v[4],
                            BlockTime = (int)v[5],
                            CoinPrize = (double)v[6]
                        };
                        //if (v[7] != null && !DBNull.Value.Equals(v[7]))
                        //    coin.CoinPerDay = (int)v[7];
                        if (v[7] != null && !DBNull.Value.Equals(v[7]))
                            coin.ActualPrice = (double)v[7];
                        if (v[8] != null && !DBNull.Value.Equals(v[8]))
                            coin.ActualPools = (string)v[8];
                        //if (v[10] != null && !DBNull.Value.Equals(v[10]))
                        //    coin.Profit = (double)v[10];
                        coinList.Add(coin);
                    }
                }
            }
            dgvCoin.DataSource = null;
            dgvCoin.DataSource = coinList;
            GetCoinApi();
            GetCoinProfit();
        }

        private void GetCoinProfit()
        {
            var coinProfitList = coinList.Select(c => new CoinProfit(c, avgDict[c.Algorithm])).ToList();
            coinProfitList.ForEach(c=> { c.IsProfitChanged += C_IsProfitChanged; });
            coinProfitList.ForEach(cp => cp.Refresh());
            //dgvProfit.DataSource = null;
            coinProfitBindingSource.DataSource = coinProfitList;
            foreach(var key in profitdict.Keys)
            {
                var coinProfit = coinProfitList.FirstOrDefault(c => c.CoinName == key);
                if (coinProfit.IsProfit != profitdict[key])
                    bot.Notify(coinProfit);
            }
            profitdict = coinProfitList.ToDictionary(c => c.CoinName,d=>d.IsProfit);
        }

        private void C_IsProfitChanged(object sender, EventArgs e)
        {
            var coinProfit = sender as CoinProfit;
            bot.Notify(coinProfit);
        }

        private void GetAndInsertStat()
        {
            timerAlgoritm.Stop();
            algorithmAvgList.Clear();
            var stats = APIWrapper.GetAPIStatsCurrent();
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();
                var query = $@"select algorithm, avg(Totalspeed) avgspeed, avg(Price) as avgprice
from AlgorithmStat
where sdatetime > dateadd(hh, -{hoursInterval}, getdate())
group by algorithm";
                SqlCeCommand command = new SqlCeCommand(query, conn);
                using (var sqlreader = command.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        object[] v = new object[3];
                        sqlreader.GetValues(v);
                        var algorithm = (int)v[0];
                        algorithmAvgList.Add(new AlgorithmAvg()
                        {
                            Algorithm = (AlgorithmEnum)algorithm,
                            AvgSpeed = (double)v[1],
                            AvgPrice = (double)v[2],
                            CurrentSpeed = stats.AllStats[algorithm].TotalSpeed,
                            CurrentPrice = stats.AllStats[algorithm].Price,
                            EndTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                            StartTime = DateTime.Now.AddHours(-hoursInterval).ToString("yyyy.MM.dd HH:mm:ss")
                        });
                    }
                }
            }
            //dataGridView1.DataSource = null;
            dgvAlgorithm.DataSource = algorithmAvgList;
            avgDict =  algorithmAvgList.ToDictionary(a => a.Algorithm);
            SqlCeBulkCopyOptions options = new SqlCeBulkCopyOptions();
            var reader = new AlgorithmStatDataReader(stats);
            using (SqlCeBulkCopy bc = new SqlCeBulkCopy(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString, options))
            {
                bc.DestinationTableName = "AlgorithmStat";
                bc.WriteToServer(reader);
            }
            timerAlgoritm.Start();

        }

        private void timerAlgoritm_Tick(object sender, EventArgs e)
        {
            GetAndInsertStat();
            GetCoinApi();
        }

        private void GetCoinApi()
        {
            foreach(var coin in coinList)
            {
                var query = coin.ExplorerUrl.EndsWith("/") ? $"{coin.ExplorerUrl}api/getnetworkhashps" : $"{coin.ExplorerUrl}/api/getnetworkhashps";
                HttpWebRequest WReq = (HttpWebRequest)WebRequest.Create(query);
                WReq.Timeout = 60000;
                try
                {
                    WebResponse WResp = WReq.GetResponse();
                    Stream DataStream = WResp.GetResponseStream();
                    DataStream.ReadTimeout = 60000;
                    StreamReader SReader = new StreamReader(DataStream);
                    var responseData = SReader.ReadToEnd();
                    coin.HashRate = Convert.ToDouble(responseData, CultureInfo.InvariantCulture)/coin.Algorithm.GetRateCoeff();
                    coin.HashFromExplorer = true;
                }
                catch(Exception e)
                {
                    coin.HashFromExplorer = false;
                }
            }
        }

        private void timerDelete_Tick(object sender, EventArgs e)
        {
            deleteOldStat();
        }

        private void deleteOldStat()
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                var query = $@"delete from AlgorithmStat where sdatetime < dateadd(hh, -{hoursDeleteInterval}, getdate())";
                SqlCeCommand command = new SqlCeCommand(query, conn);
                command.ExecuteNonQuery();
            }
        }

        private void btnCoinLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "csv files (*.csv)|*.csv";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    List<Coin> coinList = new List<Coin>();
                    string[] coinTextList = File.ReadAllLines(open.FileName);
                    foreach (var coinText in coinTextList)
                    {
                        var parts = coinText.Split(',');
                        Coin coin = new Coin()
                        {
                            CoinName = parts[0],
                            HashRate = Convert.ToDouble(parts[2], CultureInfo.InvariantCulture),
                            ExplorerUrl = parts[3],
                            BlockTime = Convert.ToInt32(parts[4]),
                            CoinPrize = Convert.ToDouble(parts[5])
                        };
                        int alg;
                        if (int.TryParse(parts[1], out alg))
                            coin.Algorithm = (AlgorithmEnum)alg;
                        else
                            coin.Algorithm = (AlgorithmEnum)Enum.Parse(typeof(AlgorithmEnum), parts[1]);
                        //if (parts.Length > 6)
                        //    coin.CoinPerDay = Convert.ToInt32(parts[6]);
                        if (parts.Length > 6)
                            coin.ActualPrice = Convert.ToDouble(parts[6]);
                        if (parts.Length > 7)
                            coin.ActualPools = parts[7];
                        //if (parts.Length > 9 && !string.IsNullOrEmpty(parts[9]))
                        //    coin.Profit = Convert.ToDouble(parts[9]);
                        coinList.Add(coin);
                    }
                    InsertCoins(coinList);
                    GetCoins();
                }
            }
        }

        private void InsertCoins(List<Coin> coinList)
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                foreach (var coin in coinList)
                {
                    DeleteCoin(coin, conn);

                    var insquery = $@"insert into Coin (CoinName,Algorithm,HashRate,ExplorerUrl,BlockTime,CoinPrize,ActualPrice,ActualPools) 
values ('{coin.CoinName}',{(int)coin.Algorithm},{coin.HashRate.ToString(CultureInfo.InvariantCulture)}, '{coin.ExplorerUrl}',{coin.BlockTime},{coin.CoinPrize.ToString(CultureInfo.InvariantCulture)},@actualPrice,@actualPools)";
                    SqlCeCommand inscommand = new SqlCeCommand(insquery, conn);
                    inscommand.Parameters.AddWithValue("@actualPrice", coin.ActualPrice);
                    inscommand.Parameters.AddWithValue("@actualPools", coin.ActualPools == null ? (object)DBNull.Value : coin.ActualPools);
                    inscommand.ExecuteNonQuery();
                }
            }
        }

        private void DeleteCoin(Coin coin, SqlCeConnection conn)
        {
            var delquery = $@"delete from Coin where CoinName = '{coin.CoinName}'";
            SqlCeCommand delcommand = new SqlCeCommand(delquery, conn);
            delcommand.ExecuteNonQuery();
            GetCoins();
        }

        private void btnCoinUnload_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "csv files (*.csv)|*.csv";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    var coinListText = coinList.Select(c => $"{c.CoinName},{c.Algorithm},{c.HashRate.ToString(CultureInfo.InvariantCulture)},{c.ExplorerUrl},{c.BlockTime},{c.CoinPrize.ToString(CultureInfo.InvariantCulture)},{c.ActualPrice},{c.ActualPools}");
                    File.WriteAllLines(save.FileName, coinListText);
                }
            }
        }

        private void btnCoinAdd_Click(object sender, EventArgs e)
        {
            var coin = new Coin();
            EditCoin(coin);
        }

        private void EditCoin(Coin coin)
        {
            CoinForm cf = new CoinForm(coin);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                InsertCoins(new List<Coin>() { coin });
                GetCoins();
            }
        }

        private void btnCoinEdit_Click(object sender, EventArgs e)
        {
            var coin = dgvCoin.CurrentRow.DataBoundItem as Coin;
            if (coin != null)
                EditCoin(coin);
        }

        private void bynCoinRemove_Click(object sender, EventArgs e)
        {
            var coin = dgvCoin.CurrentRow.DataBoundItem as Coin;
            if (coin != null && MessageBox.Show("Валера, ты уверен?") == DialogResult.OK)
            {
                using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
                {
                    conn.Open();

                    DeleteCoin(coin, conn);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCoin.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewLinkColumn))
            {
                Process.Start(dgvCoin[e.ColumnIndex, e.RowIndex].Value.ToString());
            }

        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dataRow = dgvCoin.Rows[e.RowIndex];
            var coin = dataRow?.DataBoundItem as Coin;
            //var coin = coinList.ElementAtOrDefault(e.RowIndex);
            if (coin != null && coin.HashFromExplorer)
                dataRow.DefaultCellStyle.BackColor = Color.Green;
            //dataGridView2.DataSource
            //e.RowIndex
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            var coinProfit = coinProfitBindingSource.Current as CoinProfit;
            coinProfit.Refresh();
        }

        private void dgvProfit_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dataRow = dgvProfit.Rows[e.RowIndex];
            var coinProfit = dataRow?.DataBoundItem as CoinProfit;
            if(coinProfit!=null && coinProfit.IsProfit==true)
                dataRow.DefaultCellStyle.BackColor = Color.Green;
        }
    }
}
