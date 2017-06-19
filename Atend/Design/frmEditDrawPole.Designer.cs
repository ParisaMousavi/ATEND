namespace Atend.Design
{
    partial class frmEditDrawPole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditDrawPole));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gvPole = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.chkPower = new System.Windows.Forms.CheckBox();
            this.chkHeight = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnNewConsolTip = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gvPoleConsol = new System.Windows.Forms.DataGridView();
            this.tvConsolSubEquipment = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.gvConsolsTip = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboConsolType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cboHalterType = new System.Windows.Forms.ComboBox();
            this.nudHalter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkIsExistance = new System.Windows.Forms.CheckBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCountPole = new System.Windows.Forms.TextBox();
            this.txtBottomCrossSectionArea = new System.Windows.Forms.TextBox();
            this.txtTopCrossSectionArea = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPole)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPoleConsol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConsolsTip)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHalter)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(735, 386);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gvPole);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(727, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "پایه";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gvPole
            // 
            this.gvPole.AllowUserToAddRows = false;
            this.gvPole.AllowUserToDeleteRows = false;
            this.gvPole.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPole.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gvPole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gvPole.Location = new System.Drawing.Point(15, 79);
            this.gvPole.Name = "gvPole";
            this.gvPole.ReadOnly = true;
            this.gvPole.Size = new System.Drawing.Size(695, 261);
            this.gvPole.TabIndex = 1;
            this.gvPole.DoubleClick += new System.EventHandler(this.gvPole_DoubleClick);
            this.gvPole.Click += new System.EventHandler(this.gvPole_Click);
            this.gvPole.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPole_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Code";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Name";
            this.Column2.FillWeight = 63.1984F;
            this.Column2.HeaderText = "نام";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkType);
            this.groupBox2.Controls.Add(this.chkPower);
            this.groupBox2.Controls.Add(this.chkHeight);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.cboType);
            this.groupBox2.Controls.Add(this.nudPower);
            this.groupBox2.Controls.Add(this.nudHeight);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(718, 67);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "جستجو بر اساس ...";
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(366, 30);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(48, 17);
            this.chkType.TabIndex = 8;
            this.chkType.Text = "نوع :";
            this.chkType.UseVisualStyleBackColor = true;
            // 
            // chkPower
            // 
            this.chkPower.AutoSize = true;
            this.chkPower.Location = new System.Drawing.Point(495, 30);
            this.chkPower.Name = "chkPower";
            this.chkPower.Size = new System.Drawing.Size(64, 17);
            this.chkPower.TabIndex = 7;
            this.chkPower.Text = "کشش :";
            this.chkPower.UseVisualStyleBackColor = true;
            // 
            // chkHeight
            // 
            this.chkHeight.AutoSize = true;
            this.chkHeight.Location = new System.Drawing.Point(645, 30);
            this.chkHeight.Name = "chkHeight";
            this.chkHeight.Size = new System.Drawing.Size(59, 17);
            this.chkHeight.TabIndex = 6;
            this.chkHeight.Text = "ارتفاع :";
            this.chkHeight.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(26, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(263, 28);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(96, 21);
            this.cboType.TabIndex = 5;
            // 
            // nudPower
            // 
            this.nudPower.Location = new System.Drawing.Point(430, 28);
            this.nudPower.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPower.Name = "nudPower";
            this.nudPower.Size = new System.Drawing.Size(51, 21);
            this.nudPower.TabIndex = 4;
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(575, 28);
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(51, 21);
            this.nudHeight.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnInsert);
            this.tabPage2.Controls.Add(this.btnNewConsolTip);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.tvConsolSubEquipment);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txtSearch);
            this.tabPage2.Controls.Add(this.gvConsolsTip);
            this.tabPage2.Controls.Add(this.cboConsolType);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(727, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "کنسول";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(87, 195);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 41;
            this.btnInsert.Text = "اضافه";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnNewConsolTip
            // 
            this.btnNewConsolTip.Location = new System.Drawing.Point(6, 195);
            this.btnNewConsolTip.Name = "btnNewConsolTip";
            this.btnNewConsolTip.Size = new System.Drawing.Size(75, 23);
            this.btnNewConsolTip.TabIndex = 40;
            this.btnNewConsolTip.Text = "کنسول جدید";
            this.btnNewConsolTip.UseVisualStyleBackColor = true;
            this.btnNewConsolTip.Click += new System.EventHandler(this.btnNewConsolTip_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.gvPoleConsol);
            this.groupBox4.Location = new System.Drawing.Point(6, 218);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(715, 139);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "کنسول های مربوط به این پایه";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(6, 115);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gvPoleConsol
            // 
            this.gvPoleConsol.AllowUserToAddRows = false;
            this.gvPoleConsol.AllowUserToDeleteRows = false;
            this.gvPoleConsol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPoleConsol.BackgroundColor = System.Drawing.Color.White;
            this.gvPoleConsol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPoleConsol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column9,
            this.Column10,
            this.Column3});
            this.gvPoleConsol.Location = new System.Drawing.Point(6, 20);
            this.gvPoleConsol.MultiSelect = false;
            this.gvPoleConsol.Name = "gvPoleConsol";
            this.gvPoleConsol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPoleConsol.Size = new System.Drawing.Size(703, 89);
            this.gvPoleConsol.TabIndex = 11;
            this.gvPoleConsol.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.gvPoleConsol_ColumnAdded);
            this.gvPoleConsol.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPoleConsol_CellContentClick);
            // 
            // tvConsolSubEquipment
            // 
            this.tvConsolSubEquipment.Location = new System.Drawing.Point(6, 16);
            this.tvConsolSubEquipment.Name = "tvConsolSubEquipment";
            this.tvConsolSubEquipment.RightToLeftLayout = true;
            this.tvConsolSubEquipment.Size = new System.Drawing.Size(218, 173);
            this.tvConsolSubEquipment.TabIndex = 38;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(246, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 21);
            this.button1.TabIndex = 37;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(285, 43);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(328, 21);
            this.txtSearch.TabIndex = 36;
            // 
            // gvConsolsTip
            // 
            this.gvConsolsTip.AllowUserToAddRows = false;
            this.gvConsolsTip.AllowUserToDeleteRows = false;
            this.gvConsolsTip.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvConsolsTip.BackgroundColor = System.Drawing.Color.White;
            this.gvConsolsTip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvConsolsTip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column7});
            this.gvConsolsTip.Location = new System.Drawing.Point(246, 70);
            this.gvConsolsTip.MultiSelect = false;
            this.gvConsolsTip.Name = "gvConsolsTip";
            this.gvConsolsTip.ReadOnly = true;
            this.gvConsolsTip.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvConsolsTip.Size = new System.Drawing.Size(450, 119);
            this.gvConsolsTip.TabIndex = 35;
            this.gvConsolsTip.Click += new System.EventHandler(this.gvConsolsTip_Click);
            this.gvConsolsTip.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvConsolsTip_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.FillWeight = 98.47716F;
            this.dataGridViewTextBoxColumn2.HeaderText = "تیپ کنسول";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "ConsolType";
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // cboConsolType
            // 
            this.cboConsolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsolType.FormattingEnabled = true;
            this.cboConsolType.Items.AddRange(new object[] {
            "عمودی",
            "مثلثی",
            "افقی"});
            this.cboConsolType.Location = new System.Drawing.Point(246, 16);
            this.cboConsolType.Name = "cboConsolType";
            this.cboConsolType.Size = new System.Drawing.Size(367, 21);
            this.cboConsolType.TabIndex = 34;
            this.cboConsolType.SelectedIndexChanged += new System.EventHandler(this.cboConsolType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(647, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "جستجو :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "آرایش کنسول :";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cboHalterType);
            this.tabPage3.Controls.Add(this.nudHalter);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(727, 360);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "مهار";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cboHalterType
            // 
            this.cboHalterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHalterType.FormattingEnabled = true;
            this.cboHalterType.Items.AddRange(new object[] {
            "عمودی",
            "مثلثی",
            "افقی"});
            this.cboHalterType.Location = new System.Drawing.Point(5, 14);
            this.cboHalterType.Name = "cboHalterType";
            this.cboHalterType.Size = new System.Drawing.Size(650, 21);
            this.cboHalterType.TabIndex = 20;
            this.cboHalterType.SelectedIndexChanged += new System.EventHandler(this.cboHalterType_SelectedIndexChanged);
            // 
            // nudHalter
            // 
            this.nudHalter.Location = new System.Drawing.Point(606, 41);
            this.nudHalter.Name = "nudHalter";
            this.nudHalter.Size = new System.Drawing.Size(49, 21);
            this.nudHalter.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(669, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "نوع مهار :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(661, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "تعداد مهار :";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(7, 538);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(88, 538);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkIsExistance
            // 
            this.chkIsExistance.AutoSize = true;
            this.chkIsExistance.Location = new System.Drawing.Point(603, 105);
            this.chkIsExistance.Name = "chkIsExistance";
            this.chkIsExistance.Size = new System.Drawing.Size(118, 17);
            this.chkIsExistance.TabIndex = 11;
            this.chkIsExistance.Text = "پایه موجود می باشد";
            this.chkIsExistance.UseVisualStyleBackColor = true;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(497, 78);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(179, 21);
            this.txtType.TabIndex = 8;
            // 
            // txtPower
            // 
            this.txtPower.Location = new System.Drawing.Point(497, 51);
            this.txtPower.Name = "txtPower";
            this.txtPower.ReadOnly = true;
            this.txtPower.Size = new System.Drawing.Size(179, 21);
            this.txtPower.TabIndex = 7;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(497, 24);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(179, 21);
            this.txtHeight.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(693, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "نوع :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(684, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "قدرت :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(682, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "ارتفاع :";
            // 
            // txtCountPole
            // 
            this.txtCountPole.Location = new System.Drawing.Point(336, 78);
            this.txtCountPole.Name = "txtCountPole";
            this.txtCountPole.Size = new System.Drawing.Size(49, 21);
            this.txtCountPole.TabIndex = 29;
            this.txtCountPole.Text = "1";
            // 
            // txtBottomCrossSectionArea
            // 
            this.txtBottomCrossSectionArea.Location = new System.Drawing.Point(206, 51);
            this.txtBottomCrossSectionArea.Name = "txtBottomCrossSectionArea";
            this.txtBottomCrossSectionArea.ReadOnly = true;
            this.txtBottomCrossSectionArea.Size = new System.Drawing.Size(179, 21);
            this.txtBottomCrossSectionArea.TabIndex = 10;
            // 
            // txtTopCrossSectionArea
            // 
            this.txtTopCrossSectionArea.Location = new System.Drawing.Point(206, 24);
            this.txtTopCrossSectionArea.Name = "txtTopCrossSectionArea";
            this.txtTopCrossSectionArea.ReadOnly = true;
            this.txtTopCrossSectionArea.Size = new System.Drawing.Size(179, 21);
            this.txtTopCrossSectionArea.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCountPole);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.chkIsExistance);
            this.groupBox1.Controls.Add(this.txtBottomCrossSectionArea);
            this.groupBox1.Controls.Add(this.txtTopCrossSectionArea);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtPower);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(7, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات پایه";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(418, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "تعداد پایه:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "ضخامت پایینی :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(391, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "ضخامت بالایی :";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn5.FillWeight = 98.47716F;
            this.dataGridViewTextBoxColumn5.HeaderText = "کنسول";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "ConsolType";
            this.Column9.FillWeight = 40F;
            this.Column9.HeaderText = "نوع كنسول";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.FillWeight = 40F;
            this.Column10.HeaderText = "موجود/پیشنهادی";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // frmEditDrawPole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 573);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditDrawPole";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ترسیم پایه";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrawPole01_FormClosing);
            this.Load += new System.EventHandler(this.frmDrawPole01_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPole)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPoleConsol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConsolsTip)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHalter)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.NumericUpDown nudPower;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gvPole;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.CheckBox chkPower;
        private System.Windows.Forms.CheckBox chkHeight;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView tvConsolSubEquipment;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView gvConsolsTip;
        private System.Windows.Forms.ComboBox cboConsolType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnNewConsolTip;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView gvPoleConsol;
        private System.Windows.Forms.ComboBox cboHalterType;
        private System.Windows.Forms.NumericUpDown nudHalter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox chkIsExistance;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtPower;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCountPole;
        private System.Windows.Forms.TextBox txtBottomCrossSectionArea;
        private System.Windows.Forms.TextBox txtTopCrossSectionArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}