namespace NiceHashMon
{
    partial class MonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerAlgoritm = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAlgSpeed = new System.Windows.Forms.TabPage();
            this.dgvAlgorithm = new System.Windows.Forms.DataGridView();
            this.algorithmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvgSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvgPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.algorithmAvgBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageCoins = new System.Windows.Forms.TabPage();
            this.bynCoinRemove = new System.Windows.Forms.Button();
            this.btnCoinEdit = new System.Windows.Forms.Button();
            this.btnCoinAdd = new System.Windows.Forms.Button();
            this.btnCoinUnload = new System.Windows.Forms.Button();
            this.btnCoinLoad = new System.Windows.Forms.Button();
            this.dgvCoin = new System.Windows.Forms.DataGridView();
            this.coinNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.algorithmDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.explorerUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.blockTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinPrizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualPoolsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HashFromExplorer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.coinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageProfit = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.coinProfitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.nuCoeff = new System.Windows.Forms.NumericUpDown();
            this.dgvProfit = new System.Windows.Forms.DataGridView();
            this.coinNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coeffDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualPriceDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.miningPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitMiningDisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ourHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitCountDisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btcDayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsProfit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.timerDelete = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageAlgSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlgorithm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmAvgBindingSource)).BeginInit();
            this.tabPageCoins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinBindingSource)).BeginInit();
            this.tabPageProfit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinProfitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuCoeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfit)).BeginInit();
            this.SuspendLayout();
            // 
            // timerAlgoritm
            // 
            this.timerAlgoritm.Interval = 1000;
            this.timerAlgoritm.Tick += new System.EventHandler(this.timerAlgoritm_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAlgSpeed);
            this.tabControl1.Controls.Add(this.tabPageCoins);
            this.tabControl1.Controls.Add(this.tabPageProfit);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(904, 401);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAlgSpeed
            // 
            this.tabPageAlgSpeed.Controls.Add(this.dgvAlgorithm);
            this.tabPageAlgSpeed.Location = new System.Drawing.Point(4, 22);
            this.tabPageAlgSpeed.Name = "tabPageAlgSpeed";
            this.tabPageAlgSpeed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAlgSpeed.Size = new System.Drawing.Size(896, 375);
            this.tabPageAlgSpeed.TabIndex = 0;
            this.tabPageAlgSpeed.Text = "Средняя скорость алгоритмов";
            this.tabPageAlgSpeed.UseVisualStyleBackColor = true;
            // 
            // dgvAlgorithm
            // 
            this.dgvAlgorithm.AllowUserToAddRows = false;
            this.dgvAlgorithm.AllowUserToDeleteRows = false;
            this.dgvAlgorithm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAlgorithm.AutoGenerateColumns = false;
            this.dgvAlgorithm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlgorithm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.algorithmDataGridViewTextBoxColumn,
            this.AvgSpeed,
            this.CurrentSpeed,
            this.AvgPrice,
            this.CurrentPrice,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn});
            this.dgvAlgorithm.DataSource = this.algorithmAvgBindingSource;
            this.dgvAlgorithm.Location = new System.Drawing.Point(3, 3);
            this.dgvAlgorithm.Name = "dgvAlgorithm";
            this.dgvAlgorithm.ReadOnly = true;
            this.dgvAlgorithm.Size = new System.Drawing.Size(890, 369);
            this.dgvAlgorithm.TabIndex = 0;
            // 
            // algorithmDataGridViewTextBoxColumn
            // 
            this.algorithmDataGridViewTextBoxColumn.DataPropertyName = "Algorithm";
            this.algorithmDataGridViewTextBoxColumn.HeaderText = "Algorithm";
            this.algorithmDataGridViewTextBoxColumn.Name = "algorithmDataGridViewTextBoxColumn";
            this.algorithmDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // AvgSpeed
            // 
            this.AvgSpeed.DataPropertyName = "AvgSpeed";
            this.AvgSpeed.HeaderText = "AvgSpeed";
            this.AvgSpeed.Name = "AvgSpeed";
            this.AvgSpeed.ReadOnly = true;
            // 
            // CurrentSpeed
            // 
            this.CurrentSpeed.DataPropertyName = "CurrentSpeed";
            this.CurrentSpeed.HeaderText = "CurrentSpeed";
            this.CurrentSpeed.Name = "CurrentSpeed";
            this.CurrentSpeed.ReadOnly = true;
            // 
            // AvgPrice
            // 
            this.AvgPrice.DataPropertyName = "AvgPrice";
            this.AvgPrice.HeaderText = "AvgPrice";
            this.AvgPrice.Name = "AvgPrice";
            this.AvgPrice.ReadOnly = true;
            // 
            // CurrentPrice
            // 
            this.CurrentPrice.DataPropertyName = "CurrentPrice";
            this.CurrentPrice.HeaderText = "CurrentPrice";
            this.CurrentPrice.Name = "CurrentPrice";
            this.CurrentPrice.ReadOnly = true;
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // algorithmAvgBindingSource
            // 
            this.algorithmAvgBindingSource.DataSource = typeof(NiceHashMon.Data.AlgorithmAvg);
            // 
            // tabPageCoins
            // 
            this.tabPageCoins.Controls.Add(this.bynCoinRemove);
            this.tabPageCoins.Controls.Add(this.btnCoinEdit);
            this.tabPageCoins.Controls.Add(this.btnCoinAdd);
            this.tabPageCoins.Controls.Add(this.btnCoinUnload);
            this.tabPageCoins.Controls.Add(this.btnCoinLoad);
            this.tabPageCoins.Controls.Add(this.dgvCoin);
            this.tabPageCoins.Location = new System.Drawing.Point(4, 22);
            this.tabPageCoins.Name = "tabPageCoins";
            this.tabPageCoins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCoins.Size = new System.Drawing.Size(896, 375);
            this.tabPageCoins.TabIndex = 1;
            this.tabPageCoins.Text = "Монеты";
            this.tabPageCoins.UseVisualStyleBackColor = true;
            // 
            // bynCoinRemove
            // 
            this.bynCoinRemove.Location = new System.Drawing.Point(358, 9);
            this.bynCoinRemove.Name = "bynCoinRemove";
            this.bynCoinRemove.Size = new System.Drawing.Size(75, 23);
            this.bynCoinRemove.TabIndex = 5;
            this.bynCoinRemove.Text = "Удалить";
            this.bynCoinRemove.UseVisualStyleBackColor = true;
            this.bynCoinRemove.Click += new System.EventHandler(this.bynCoinRemove_Click);
            // 
            // btnCoinEdit
            // 
            this.btnCoinEdit.Location = new System.Drawing.Point(252, 9);
            this.btnCoinEdit.Name = "btnCoinEdit";
            this.btnCoinEdit.Size = new System.Drawing.Size(100, 23);
            this.btnCoinEdit.TabIndex = 4;
            this.btnCoinEdit.Text = "Редактировать";
            this.btnCoinEdit.UseVisualStyleBackColor = true;
            this.btnCoinEdit.Click += new System.EventHandler(this.btnCoinEdit_Click);
            // 
            // btnCoinAdd
            // 
            this.btnCoinAdd.Location = new System.Drawing.Point(171, 9);
            this.btnCoinAdd.Name = "btnCoinAdd";
            this.btnCoinAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCoinAdd.TabIndex = 3;
            this.btnCoinAdd.Text = "Добавить";
            this.btnCoinAdd.UseVisualStyleBackColor = true;
            this.btnCoinAdd.Click += new System.EventHandler(this.btnCoinAdd_Click);
            // 
            // btnCoinUnload
            // 
            this.btnCoinUnload.Location = new System.Drawing.Point(90, 9);
            this.btnCoinUnload.Name = "btnCoinUnload";
            this.btnCoinUnload.Size = new System.Drawing.Size(75, 23);
            this.btnCoinUnload.TabIndex = 2;
            this.btnCoinUnload.Text = "Выгрузить";
            this.btnCoinUnload.UseVisualStyleBackColor = true;
            this.btnCoinUnload.Click += new System.EventHandler(this.btnCoinUnload_Click);
            // 
            // btnCoinLoad
            // 
            this.btnCoinLoad.Location = new System.Drawing.Point(9, 9);
            this.btnCoinLoad.Name = "btnCoinLoad";
            this.btnCoinLoad.Size = new System.Drawing.Size(75, 23);
            this.btnCoinLoad.TabIndex = 1;
            this.btnCoinLoad.Text = "Загрузить";
            this.btnCoinLoad.UseVisualStyleBackColor = true;
            this.btnCoinLoad.Click += new System.EventHandler(this.btnCoinLoad_Click);
            // 
            // dgvCoin
            // 
            this.dgvCoin.AutoGenerateColumns = false;
            this.dgvCoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coinNameDataGridViewTextBoxColumn,
            this.algorithmDataGridViewTextBoxColumn1,
            this.hashRateDataGridViewTextBoxColumn,
            this.explorerUrlDataGridViewTextBoxColumn,
            this.blockTimeDataGridViewTextBoxColumn,
            this.coinPrizeDataGridViewTextBoxColumn,
            this.actualPriceDataGridViewTextBoxColumn,
            this.actualPoolsDataGridViewTextBoxColumn,
            this.HashFromExplorer});
            this.dgvCoin.DataSource = this.coinBindingSource;
            this.dgvCoin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvCoin.Location = new System.Drawing.Point(3, 32);
            this.dgvCoin.Name = "dgvCoin";
            this.dgvCoin.ReadOnly = true;
            this.dgvCoin.Size = new System.Drawing.Size(890, 340);
            this.dgvCoin.TabIndex = 0;
            this.dgvCoin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dgvCoin.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
            // 
            // coinNameDataGridViewTextBoxColumn
            // 
            this.coinNameDataGridViewTextBoxColumn.DataPropertyName = "CoinName";
            this.coinNameDataGridViewTextBoxColumn.HeaderText = "CoinName";
            this.coinNameDataGridViewTextBoxColumn.Name = "coinNameDataGridViewTextBoxColumn";
            this.coinNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // algorithmDataGridViewTextBoxColumn1
            // 
            this.algorithmDataGridViewTextBoxColumn1.DataPropertyName = "Algorithm";
            this.algorithmDataGridViewTextBoxColumn1.HeaderText = "Algorithm";
            this.algorithmDataGridViewTextBoxColumn1.Name = "algorithmDataGridViewTextBoxColumn1";
            this.algorithmDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // hashRateDataGridViewTextBoxColumn
            // 
            this.hashRateDataGridViewTextBoxColumn.DataPropertyName = "HashRate";
            this.hashRateDataGridViewTextBoxColumn.HeaderText = "HashRate";
            this.hashRateDataGridViewTextBoxColumn.Name = "hashRateDataGridViewTextBoxColumn";
            this.hashRateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // explorerUrlDataGridViewTextBoxColumn
            // 
            this.explorerUrlDataGridViewTextBoxColumn.DataPropertyName = "ExplorerUrl";
            this.explorerUrlDataGridViewTextBoxColumn.HeaderText = "ExplorerUrl";
            this.explorerUrlDataGridViewTextBoxColumn.Name = "explorerUrlDataGridViewTextBoxColumn";
            this.explorerUrlDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blockTimeDataGridViewTextBoxColumn
            // 
            this.blockTimeDataGridViewTextBoxColumn.DataPropertyName = "BlockTime";
            this.blockTimeDataGridViewTextBoxColumn.HeaderText = "BlockTime";
            this.blockTimeDataGridViewTextBoxColumn.Name = "blockTimeDataGridViewTextBoxColumn";
            this.blockTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // coinPrizeDataGridViewTextBoxColumn
            // 
            this.coinPrizeDataGridViewTextBoxColumn.DataPropertyName = "CoinPrize";
            this.coinPrizeDataGridViewTextBoxColumn.HeaderText = "CoinPrize";
            this.coinPrizeDataGridViewTextBoxColumn.Name = "coinPrizeDataGridViewTextBoxColumn";
            this.coinPrizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // actualPriceDataGridViewTextBoxColumn
            // 
            this.actualPriceDataGridViewTextBoxColumn.DataPropertyName = "ActualPrice";
            dataGridViewCellStyle1.Format = "N8";
            dataGridViewCellStyle1.NullValue = null;
            this.actualPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.actualPriceDataGridViewTextBoxColumn.HeaderText = "ActualPrice";
            this.actualPriceDataGridViewTextBoxColumn.Name = "actualPriceDataGridViewTextBoxColumn";
            this.actualPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // actualPoolsDataGridViewTextBoxColumn
            // 
            this.actualPoolsDataGridViewTextBoxColumn.DataPropertyName = "ActualPools";
            this.actualPoolsDataGridViewTextBoxColumn.HeaderText = "ActualPools";
            this.actualPoolsDataGridViewTextBoxColumn.Name = "actualPoolsDataGridViewTextBoxColumn";
            this.actualPoolsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // HashFromExplorer
            // 
            this.HashFromExplorer.DataPropertyName = "HashFromExplorer";
            this.HashFromExplorer.HeaderText = "HashFromExplorer";
            this.HashFromExplorer.Name = "HashFromExplorer";
            this.HashFromExplorer.ReadOnly = true;
            // 
            // coinBindingSource
            // 
            this.coinBindingSource.DataSource = typeof(NiceHashMon.Data.Coin);
            // 
            // tabPageProfit
            // 
            this.tabPageProfit.Controls.Add(this.label2);
            this.tabPageProfit.Controls.Add(this.numericUpDown1);
            this.tabPageProfit.Controls.Add(this.label1);
            this.tabPageProfit.Controls.Add(this.nuCoeff);
            this.tabPageProfit.Controls.Add(this.dgvProfit);
            this.tabPageProfit.Location = new System.Drawing.Point(4, 22);
            this.tabPageProfit.Name = "tabPageProfit";
            this.tabPageProfit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProfit.Size = new System.Drawing.Size(896, 375);
            this.tabPageProfit.TabIndex = 2;
            this.tabPageProfit.Text = "Прибыль";
            this.tabPageProfit.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "MiningPrice";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.coinProfitBindingSource, "MiningPrice", true));
            this.numericUpDown1.DecimalPlaces = 6;
            this.numericUpDown1.Location = new System.Drawing.Point(268, 5);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // coinProfitBindingSource
            // 
            this.coinProfitBindingSource.DataSource = typeof(NiceHashMon.Data.CoinProfit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Coeff";
            // 
            // nuCoeff
            // 
            this.nuCoeff.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.coinProfitBindingSource, "Coeff", true));
            this.nuCoeff.DecimalPlaces = 6;
            this.nuCoeff.Location = new System.Drawing.Point(70, 5);
            this.nuCoeff.Name = "nuCoeff";
            this.nuCoeff.Size = new System.Drawing.Size(120, 20);
            this.nuCoeff.TabIndex = 1;
            this.nuCoeff.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // dgvProfit
            // 
            this.dgvProfit.AutoGenerateColumns = false;
            this.dgvProfit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coinNameDataGridViewTextBoxColumn1,
            this.coeffDataGridViewTextBoxColumn,
            this.actualPriceDataGridViewTextBoxColumn1,
            this.miningPriceDataGridViewTextBoxColumn,
            this.profitMiningDisplayDataGridViewTextBoxColumn,
            this.countPriceDataGridViewTextBoxColumn,
            this.ourHashDataGridViewTextBoxColumn,
            this.profitCountDisplayDataGridViewTextBoxColumn,
            this.btcDayDataGridViewTextBoxColumn,
            this.IsProfit});
            this.dgvProfit.DataSource = this.coinProfitBindingSource;
            this.dgvProfit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvProfit.Location = new System.Drawing.Point(3, 31);
            this.dgvProfit.Name = "dgvProfit";
            this.dgvProfit.Size = new System.Drawing.Size(890, 341);
            this.dgvProfit.TabIndex = 0;
            this.dgvProfit.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProfit_RowPostPaint);
            // 
            // coinNameDataGridViewTextBoxColumn1
            // 
            this.coinNameDataGridViewTextBoxColumn1.DataPropertyName = "CoinName";
            this.coinNameDataGridViewTextBoxColumn1.HeaderText = "CoinName";
            this.coinNameDataGridViewTextBoxColumn1.Name = "coinNameDataGridViewTextBoxColumn1";
            // 
            // coeffDataGridViewTextBoxColumn
            // 
            this.coeffDataGridViewTextBoxColumn.DataPropertyName = "Coeff";
            this.coeffDataGridViewTextBoxColumn.HeaderText = "Coeff";
            this.coeffDataGridViewTextBoxColumn.Name = "coeffDataGridViewTextBoxColumn";
            // 
            // actualPriceDataGridViewTextBoxColumn1
            // 
            this.actualPriceDataGridViewTextBoxColumn1.DataPropertyName = "ActualPrice";
            dataGridViewCellStyle2.Format = "N8";
            dataGridViewCellStyle2.NullValue = null;
            this.actualPriceDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.actualPriceDataGridViewTextBoxColumn1.HeaderText = "ActualPrice";
            this.actualPriceDataGridViewTextBoxColumn1.Name = "actualPriceDataGridViewTextBoxColumn1";
            // 
            // miningPriceDataGridViewTextBoxColumn
            // 
            this.miningPriceDataGridViewTextBoxColumn.DataPropertyName = "MiningPrice";
            dataGridViewCellStyle3.Format = "N6";
            dataGridViewCellStyle3.NullValue = null;
            this.miningPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.miningPriceDataGridViewTextBoxColumn.HeaderText = "MiningPrice";
            this.miningPriceDataGridViewTextBoxColumn.Name = "miningPriceDataGridViewTextBoxColumn";
            // 
            // profitMiningDisplayDataGridViewTextBoxColumn
            // 
            this.profitMiningDisplayDataGridViewTextBoxColumn.DataPropertyName = "ProfitMiningDisplay";
            this.profitMiningDisplayDataGridViewTextBoxColumn.HeaderText = "ProfitMiningDisplay";
            this.profitMiningDisplayDataGridViewTextBoxColumn.Name = "profitMiningDisplayDataGridViewTextBoxColumn";
            // 
            // countPriceDataGridViewTextBoxColumn
            // 
            this.countPriceDataGridViewTextBoxColumn.DataPropertyName = "CountPrice";
            dataGridViewCellStyle4.Format = "N6";
            dataGridViewCellStyle4.NullValue = null;
            this.countPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.countPriceDataGridViewTextBoxColumn.HeaderText = "CountPrice";
            this.countPriceDataGridViewTextBoxColumn.Name = "countPriceDataGridViewTextBoxColumn";
            // 
            // ourHashDataGridViewTextBoxColumn
            // 
            this.ourHashDataGridViewTextBoxColumn.DataPropertyName = "OurHash";
            dataGridViewCellStyle5.Format = "N6";
            dataGridViewCellStyle5.NullValue = null;
            this.ourHashDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.ourHashDataGridViewTextBoxColumn.HeaderText = "OurHash";
            this.ourHashDataGridViewTextBoxColumn.Name = "ourHashDataGridViewTextBoxColumn";
            // 
            // profitCountDisplayDataGridViewTextBoxColumn
            // 
            this.profitCountDisplayDataGridViewTextBoxColumn.DataPropertyName = "ProfitCountDisplay";
            this.profitCountDisplayDataGridViewTextBoxColumn.HeaderText = "ProfitCountDisplay";
            this.profitCountDisplayDataGridViewTextBoxColumn.Name = "profitCountDisplayDataGridViewTextBoxColumn";
            // 
            // btcDayDataGridViewTextBoxColumn
            // 
            this.btcDayDataGridViewTextBoxColumn.DataPropertyName = "BtcDay";
            dataGridViewCellStyle6.Format = "N6";
            dataGridViewCellStyle6.NullValue = null;
            this.btcDayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.btcDayDataGridViewTextBoxColumn.HeaderText = "BtcDay";
            this.btcDayDataGridViewTextBoxColumn.Name = "btcDayDataGridViewTextBoxColumn";
            // 
            // IsProfit
            // 
            this.IsProfit.DataPropertyName = "IsProfit";
            this.IsProfit.HeaderText = "IsProfit";
            this.IsProfit.Name = "IsProfit";
            // 
            // timerDelete
            // 
            this.timerDelete.Interval = 43200000;
            this.timerDelete.Tick += new System.EventHandler(this.timerDelete_Tick);
            // 
            // MonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 401);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(920, 440);
            this.Name = "MonForm";
            this.Text = "Мониторинг";
            this.Load += new System.EventHandler(this.MonForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAlgSpeed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlgorithm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmAvgBindingSource)).EndInit();
            this.tabPageCoins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinBindingSource)).EndInit();
            this.tabPageProfit.ResumeLayout(false);
            this.tabPageProfit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coinProfitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuCoeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerAlgoritm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAlgSpeed;
        private System.Windows.Forms.TabPage tabPageCoins;
        private System.Windows.Forms.DataGridView dgvAlgorithm;
        private System.Windows.Forms.BindingSource algorithmAvgBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn algorithmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvgSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvgPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Timer timerDelete;
        private System.Windows.Forms.DataGridView dgvCoin;
        private System.Windows.Forms.Button btnCoinLoad;
        private System.Windows.Forms.BindingSource coinBindingSource;
        private System.Windows.Forms.Button bynCoinRemove;
        private System.Windows.Forms.Button btnCoinEdit;
        private System.Windows.Forms.Button btnCoinAdd;
        private System.Windows.Forms.Button btnCoinUnload;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinPerDayDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabPageProfit;
        private System.Windows.Forms.DataGridView dgvProfit;
        private System.Windows.Forms.BindingSource coinProfitBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nuCoeff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn algorithmDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hashRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn explorerUrlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blockTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinPrizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualPoolsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HashFromExplorer;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coeffDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualPriceDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn miningPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitMiningDisplayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ourHashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitCountDisplayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn btcDayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsProfit;
    }
}

