using ErikEJ.SqlCe;
using NiceHashBotLib;
using NiceHashMon.Data;
using NiceHashMon.Telegram;
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
using static NiceHashMon.Data.CoinProfit;

namespace NiceHashMon
{
    public partial class MonForm : Form
    {
        private int hoursInterval = 6;
        private SortableBindingList<Coin> coinList = new SortableBindingList<Coin>();
        private SortableBindingList<CoinProfit> coinProfitList = new SortableBindingList<CoinProfit>();
        private List<AlgorithmAvg> algorithmAvgList = new List<AlgorithmAvg>();
        private Dictionary<AlgorithmEnum, AlgorithmAvg> avgDict = new Dictionary<AlgorithmEnum, AlgorithmAvg>();
        private TelegramNotifyBot bot = new TelegramNotifyBot();
        private Dictionary<string, bool> profitdict = new Dictionary<string, bool>();
        private Markets.MarketService marketService = new Markets.MarketService();
        public MonForm()
        {
            InitializeComponent();
        }

        private void MonForm_Load(object sender, EventArgs e)
        {
            SqlHelper.DeleteOldStat();
            GetAndInsertStat();
            GetCoins();
            timerAlgoritm.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["UpdateTime"]);
            timerAlgoritm.Start();
            timerDelete.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Trace.WriteLine("Form Closed");
            Trace.Flush();
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
                        if (v[9] != null && !DBNull.Value.Equals(v[9]))
                            coin.ShortName = (string)v[9];
                        if (v[10] != null && !DBNull.Value.Equals(v[10]))
                            coin.HashCoeff = Convert.ToInt16(v[10]);
                        if (v[11] != null && !DBNull.Value.Equals(v[11]))
                            coin.Coeff = Convert.ToDouble(v[11]);
                        if (v[12] != null && !DBNull.Value.Equals(v[12]))
                            coin.MiningPrice = Convert.ToDouble(v[12]);
                        //if (v[10] != null && !DBNull.Value.Equals(v[10]))
                        //    coin.Profit = (double)v[10];
                        coinList.Add(coin);
                    }
                }
            }
            //dgvCoin.DataSource = null;
            //dgvCoin.DataSource = coinList;
            coinBindingSource.DataSource = coinList;
            marketService.Refresh();
            coinList.ToList().ForEach(async c => await c.Refresh(marketService));
            GetCoinProfit();
        }

        private void GetCoinProfit()
        {
            coinProfitList.Clear();
            var tempList = coinList.Select(c => new CoinProfit(c, avgDict[c.Algorithm])).ToList();

            var now = DateTime.Now;
            var now1 = now.AddHours(-1);
            var now2 = now.AddHours(-2);
            var now3 = now.AddHours(-3);
            var now6 = now.AddHours(-6);
            var profit1Dict = SqlHelper.GetProfitStat(now1, now);
            var profit2Dict = SqlHelper.GetProfitStat(now2, now1);
            var profit3Dict = SqlHelper.GetProfitStat(now3, now2);
            var profit6Dict = SqlHelper.GetProfitStat(now6, now);
            tempList.ForEach(t => 
            {
                if(profit1Dict.ContainsKey(t.CoinName))
                {
                    var profit1 = profit1Dict[t.CoinName];
                    t.ProfitCountC1 = profit1.AvgProfitCount;
                    t.ProfitCountC1Percent = profit1.AvgProfitCountPercent;
                }
                if (profit2Dict.ContainsKey(t.CoinName))
                {
                    var profit2 = profit2Dict[t.CoinName];
                    t.ProfitCountC2 = profit2.AvgProfitCount;
                    t.ProfitCountC2Percent = profit2.AvgProfitCountPercent;
                }
                if (profit3Dict.ContainsKey(t.CoinName))
                {
                    var profit3 = profit3Dict[t.CoinName];
                    t.ProfitCountC3 = profit3.AvgProfitCount;
                    t.ProfitCountC3Percent = profit3.AvgProfitCountPercent;
                }
                if (profit6Dict.ContainsKey(t.CoinName))
                {
                    var profit6 = profit6Dict[t.CoinName];
                    t.ProfitCountC6 = profit6.AvgProfitCount;
                    t.ProfitCountC6Percent = profit6.AvgProfitCountPercent;
                }
            });

            coinProfitList = new SortableBindingList<CoinProfit>(tempList);
            tempList.ForEach(c=> { c.IsProfitChanged += C_IsProfitChanged; });
            tempList.ForEach(cp => cp.Refresh());

            //dgvProfit.DataSource = null;
            coinProfitBindingSource.DataSource = coinProfitList;

            //if(profitdict.Count==0)
            //{
            //    profitdict = coinProfitList.ToDictionary(c => c.CoinName, d => false);
            //}
            //foreach(var key in profitdict.Keys)
            //{
            //    var dictprofit = profitdict[key];
            //    var coinProfit = coinProfitList.FirstOrDefault(c => c.CoinName == key);
            //    if (coinProfit == null) continue;
            //    if (coinProfit.IsProfit != dictprofit)
            //    {
            //        if(coinProfit.IsProfit.Value && coinProfit.ProfitCountC1Percent>0.15)
            //            bot.Notify(coinProfit);
            //        else if(dictprofit.HasValue && dictprofit.Value)
            //            bot.Notify(coinProfit);
            //    }
            //}
            //profitdict = coinProfitList.ToDictionary(c => c.CoinName,d=>d.IsProfit);
        }
        private void AddCoinProfit(Coin c)
        {
            var coinProfit = new CoinProfit(c, avgDict[c.Algorithm]);
            coinProfit.IsProfitChanged += C_IsProfitChanged;
            coinProfit.Refresh();
            coinProfitList.Add(coinProfit);

            AddOrChangeProfit(coinProfit);
        }

        private void AddOrChangeProfit(CoinProfit coinProfit)
        {
            if (profitdict.ContainsKey(coinProfit.CoinName))
                profitdict[coinProfit.CoinName] = coinProfit.IsProfit;
            else
                profitdict.Add(coinProfit.CoinName, coinProfit.IsProfit);
        }

        private void C_IsProfitChanged(object sender, CoinProfitChangedArgs e)
        {
            var coinProfit = sender as CoinProfit;
            if ((!profitdict.ContainsKey(coinProfit.CoinName) && coinProfit.IsProfit) || (profitdict.ContainsKey(coinProfit.CoinName) && profitdict[coinProfit.CoinName] != coinProfit.IsProfit))
                bot.Notify(coinProfit);
            AddOrChangeProfit(coinProfit);
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

            SqlHelper.AlgStatBulkInsert(stats);

            timerAlgoritm.Start();

        }

        private void timerAlgoritm_Tick(object sender, EventArgs e)
        {
            GetAndInsertStat();
            marketService.Refresh();
            coinList.ToList().ForEach(async c => await c.Refresh(marketService));
            GetCoinProfit();
            SqlHelper.ProfitStatBulkInsert(coinProfitList);
        }     
        

        private void timerDelete_Tick(object sender, EventArgs e)
        {
            SqlHelper.DeleteOldStat();
        }        

        private void btnCoinLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "csv files (*.csv)|*.csv";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    var allCoinsDeleted = false;
                    if (MessageBox.Show("Удалить все монеты из базы перед загрузкой?", "Загрузка монет", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SqlHelper.DeleteAllCoins();
                        allCoinsDeleted = true;
                    }

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
                            CoinPrize = Convert.ToDouble(parts[5], CultureInfo.InvariantCulture)
                        };
                        int alg;
                        if (int.TryParse(parts[1], out alg))
                            coin.Algorithm = (AlgorithmEnum)alg;
                        else
                            coin.Algorithm = (AlgorithmEnum)Enum.Parse(typeof(AlgorithmEnum), parts[1]);
                        //if (parts.Length > 6)
                        //    coin.CoinPerDay = Convert.ToInt32(parts[6]);
                        if (parts.Length > 6)
                            coin.ActualPrice = Convert.ToDouble(parts[6], CultureInfo.InvariantCulture);
                        if (parts.Length > 7)
                            coin.ShortName = parts[7];
                        if (parts.Length > 8)
                            coin.HashCoeff = Convert.ToInt16(parts[8]);
                        if (parts.Length > 9)
                            coin.ActualPools = parts[9];
                        //if (parts.Length > 9 && !string.IsNullOrEmpty(parts[9]))
                        //    coin.Profit = Convert.ToDouble(parts[9]);
                        coinList.Add(coin);
                    }
                    InsertCoins(coinList,allCoinsDeleted);
                    GetCoins();
                }
            }
        }

        private void InsertCoins(List<Coin> coinList,bool allCoinsDeleted = false)
        {
            using (SqlCeConnection conn = new SqlCeConnection(ConfigurationManager.ConnectionStrings["MonDb"].ConnectionString))
            {
                conn.Open();

                foreach (var coin in coinList)
                {
                    if(!allCoinsDeleted)
                        DeleteCoin(coin, conn);

                    var insquery = $@"insert into Coin (CoinName,Algorithm,HashRate,ExplorerUrl,BlockTime,CoinPrize,ActualPrice,ActualPools,ShortName,HashCoeff) 
values ('{coin.CoinName}',{(int)coin.Algorithm},{coin.HashRate.ToString(CultureInfo.InvariantCulture)}, '{coin.ExplorerUrl}',{coin.BlockTime},{coin.CoinPrize.ToString(CultureInfo.InvariantCulture)},@actualPrice,@actualPools,@shortName,@hashCoeff)";
                    SqlCeCommand inscommand = new SqlCeCommand(insquery, conn);
                    inscommand.Parameters.AddWithValue("@actualPrice", coin.ActualPrice);
                    inscommand.Parameters.AddWithValue("@actualPools", coin.ActualPools == null ? (object)DBNull.Value : coin.ActualPools);
                    inscommand.Parameters.AddWithValue("@shortName", coin.ShortName==null?string.Empty:coin.ShortName);
                    inscommand.Parameters.AddWithValue("@hashCoeff", coin.HashCoeff);
                    inscommand.ExecuteNonQuery();
                }
            }
        }

        private void DeleteCoin(Coin coin, SqlCeConnection conn)
        {
            var delquery = $@"delete from Coin where CoinName = '{coin.CoinName}'";
            SqlCeCommand delcommand = new SqlCeCommand(delquery, conn);
            delcommand.ExecuteNonQuery();

            if (coinList.Contains(coin))
                coinList.Remove(coin);

            var coinProfit = coinProfitList.FirstOrDefault(c => c.CoinName == coin.CoinName);
            if (coinProfit != null)
                coinProfitList.Remove(coinProfit);
        }

        private void btnCoinUnload_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "csv files (*.csv)|*.csv";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    var coinListText = coinList.Select(c => $"{c.CoinName},{c.Algorithm},{c.HashRate.ToString(CultureInfo.InvariantCulture)},{c.ExplorerUrl},{c.BlockTime},{c.CoinPrize.ToString(CultureInfo.InvariantCulture)},{c.ActualPrice.ToString(CultureInfo.InvariantCulture)},{c.ShortName},{c.HashCoeff},{c.ActualPools}");
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
                coinList.Add(coin);
                AddCoinProfit(coin);
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
            if (coin != null && MessageBox.Show("Валера, ты уверен?","Удаление",MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            var dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewLinkColumn))
            {
                Process.Start(dgv[e.ColumnIndex, e.RowIndex].Value.ToString());
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
            //ApplyValues();
        }

        private void dgvProfit_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dataRow = dgvProfit.Rows[e.RowIndex];
            var coinProfit = dataRow?.DataBoundItem as CoinProfit;
            if(coinProfit!=null && coinProfit.IsProfit==true)
                dataRow.DefaultCellStyle.BackColor = Color.Green;
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyValues();
        }

        private void ApplyValues()
        {
            var coinProfit = coinProfitBindingSource.Current as CoinProfit;
            if (coinProfit == null) return;

            coinProfit.Coeff = (double)nuCoeff.Value;
            coinProfit.MiningPrice = (double)nuMiningPrice.Value;
            SqlHelper.UpdateCoinByProfitTab(coinProfit);
            coinProfit?.Refresh();
            coinProfitBindingSource.ResetCurrentItem();
        }
    }
}
