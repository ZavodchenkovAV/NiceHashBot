using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceHashMon
{
    public partial class CoinForm : Form
    {
        private Coin _coin;
        public CoinForm()
        {
            InitializeComponent();
        }
        public CoinForm(Coin coin)
            :this()
        {
            _coin = coin;
            bsCoin.Add(coin);
            comboBox1.DataSource = Enum.GetValues(typeof(AlgorithmEnum));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
