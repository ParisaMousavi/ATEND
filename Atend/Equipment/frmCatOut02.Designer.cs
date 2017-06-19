namespace Atend.Equipment
{
	partial class frmCatOut02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCatOut02));
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnInsert = new System.Windows.Forms.Button();
            this.gvSelectedEquipment = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvEquipment = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOperationName = new System.Windows.Forms.TextBox();
            this.btnDeleteOperation = new System.Windows.Forms.Button();
            this.btnInsertOperation = new System.Windows.Forms.Button();
            this.gvOperation = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvOperation = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ذخیرهسازیToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ذخیرهسازیوخروجToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.جدیدToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.پروندهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.حذفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.جستجوToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.خروجToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbIsDefault = new System.Windows.Forms.ToolStripButton();
            this.tsbShare = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripButton();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtBackUpName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmper = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboVol = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedEquipment)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperation)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "نام تجهیز";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(6, 345);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 25);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Column3
            // 
            this.Column3.FillWeight = 20F;
            this.Column3.HeaderText = "تعداد";
            this.Column3.Name = "Column3";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnInsert);
            this.tabPage1.Controls.Add(this.gvSelectedEquipment);
            this.tabPage1.Controls.Add(this.tvEquipment);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(687, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "تجهیزات جانبی";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnInsert
            // 
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsert.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInsert.Location = new System.Drawing.Point(6, 171);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(100, 25);
            this.btnInsert.TabIndex = 12;
            this.btnInsert.Text = "اضافه";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // gvSelectedEquipment
            // 
            this.gvSelectedEquipment.AllowUserToAddRows = false;
            this.gvSelectedEquipment.AllowUserToDeleteRows = false;
            this.gvSelectedEquipment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSelectedEquipment.BackgroundColor = System.Drawing.Color.White;
            this.gvSelectedEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSelectedEquipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gvSelectedEquipment.Location = new System.Drawing.Point(6, 201);
            this.gvSelectedEquipment.Name = "gvSelectedEquipment";
            this.gvSelectedEquipment.Size = new System.Drawing.Size(674, 141);
            this.gvSelectedEquipment.TabIndex = 10;
            this.gvSelectedEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSelectedEquipment_CellContentClick);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // tvEquipment
            // 
            this.tvEquipment.CheckBoxes = true;
            this.tvEquipment.Location = new System.Drawing.Point(6, 11);
            this.tvEquipment.Name = "tvEquipment";
            this.tvEquipment.RightToLeftLayout = true;
            this.tvEquipment.Size = new System.Drawing.Size(674, 154);
            this.tvEquipment.TabIndex = 11;
            this.tvEquipment.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEquipment_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 309);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(695, 400);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.txtOperationName);
            this.tabPage2.Controls.Add(this.btnDeleteOperation);
            this.tabPage2.Controls.Add(this.btnInsertOperation);
            this.tabPage2.Controls.Add(this.gvOperation);
            this.tabPage2.Controls.Add(this.tvOperation);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(687, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "آماده سازی";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(562, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 13);
            this.label11.TabIndex = 123;
            this.label11.Text = "جستجوی آماده سازی:";
            // 
            // txtOperationName
            // 
            this.txtOperationName.Location = new System.Drawing.Point(6, 10);
            this.txtOperationName.Name = "txtOperationName";
            this.txtOperationName.Size = new System.Drawing.Size(550, 21);
            this.txtOperationName.TabIndex = 12;
            this.txtOperationName.TextChanged += new System.EventHandler(this.txtOperationName_TextChanged);
            // 
            // btnDeleteOperation
            // 
            this.btnDeleteOperation.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteOperation.Image")));
            this.btnDeleteOperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteOperation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteOperation.Location = new System.Drawing.Point(6, 346);
            this.btnDeleteOperation.Name = "btnDeleteOperation";
            this.btnDeleteOperation.Size = new System.Drawing.Size(100, 25);
            this.btnDeleteOperation.TabIndex = 16;
            this.btnDeleteOperation.Text = "حذف";
            this.btnDeleteOperation.UseVisualStyleBackColor = true;
            this.btnDeleteOperation.Click += new System.EventHandler(this.btnDeleteOperation_Click);
            // 
            // btnInsertOperation
            // 
            this.btnInsertOperation.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertOperation.Image")));
            this.btnInsertOperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsertOperation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInsertOperation.Location = new System.Drawing.Point(6, 181);
            this.btnInsertOperation.Name = "btnInsertOperation";
            this.btnInsertOperation.Size = new System.Drawing.Size(100, 25);
            this.btnInsertOperation.TabIndex = 14;
            this.btnInsertOperation.Text = "اضافه";
            this.btnInsertOperation.UseVisualStyleBackColor = true;
            this.btnInsertOperation.Click += new System.EventHandler(this.btnInsertOperation_Click);
            // 
            // gvOperation
            // 
            this.gvOperation.AllowUserToAddRows = false;
            this.gvOperation.AllowUserToDeleteRows = false;
            this.gvOperation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvOperation.BackgroundColor = System.Drawing.Color.White;
            this.gvOperation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOperation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.gvOperation.Location = new System.Drawing.Point(6, 210);
            this.gvOperation.Name = "gvOperation";
            this.gvOperation.Size = new System.Drawing.Size(678, 133);
            this.gvOperation.TabIndex = 15;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "نام آماده سازی";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 30F;
            this.Column7.HeaderText = "واحد";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 20F;
            this.Column8.HeaderText = "تعداد";
            this.Column8.Name = "Column8";
            // 
            // tvOperation
            // 
            this.tvOperation.CheckBoxes = true;
            this.tvOperation.Location = new System.Drawing.Point(6, 38);
            this.tvOperation.Name = "tvOperation";
            this.tvOperation.RightToLeftLayout = true;
            this.tvOperation.Size = new System.Drawing.Size(674, 138);
            this.tvOperation.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(0, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 63;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSelect.Location = new System.Drawing.Point(39, 16);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(115, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "انتخاب از پشتیبان";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(663, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "*";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
            // 
            // ذخیرهسازیToolStripMenuItem
            // 
            this.ذخیرهسازیToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ذخیرهسازیToolStripMenuItem.Image")));
            this.ذخیرهسازیToolStripMenuItem.Name = "ذخیرهسازیToolStripMenuItem";
            this.ذخیرهسازیToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ذخیرهسازیToolStripMenuItem.Text = "ذخیره سازی";
            this.ذخیرهسازیToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهسازیToolStripMenuItem_Click);
            // 
            // ذخیرهسازیوخروجToolStripMenuItem
            // 
            this.ذخیرهسازیوخروجToolStripMenuItem.Name = "ذخیرهسازیوخروجToolStripMenuItem";
            this.ذخیرهسازیوخروجToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ذخیرهسازیوخروجToolStripMenuItem.Text = "ذخیره سازی و خروج";
            this.ذخیرهسازیوخروجToolStripMenuItem.Click += new System.EventHandler(this.ذخیرهسازیوخروجToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(172, 6);
            // 
            // جدیدToolStripMenuItem
            // 
            this.جدیدToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("جدیدToolStripMenuItem.Image")));
            this.جدیدToolStripMenuItem.Name = "جدیدToolStripMenuItem";
            this.جدیدToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.جدیدToolStripMenuItem.Text = "جدید";
            this.جدیدToolStripMenuItem.Click += new System.EventHandler(this.جدیدToolStripMenuItem_Click);
            // 
            // پروندهToolStripMenuItem
            // 
            this.پروندهToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.جدیدToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ذخیرهسازیToolStripMenuItem,
            this.ذخیرهسازیوخروجToolStripMenuItem,
            this.toolStripMenuItem2,
            this.حذفToolStripMenuItem,
            this.toolStripMenuItem3,
            this.جستجوToolStripMenuItem,
            this.toolStripMenuItem4,
            this.خروجToolStripMenuItem});
            this.پروندهToolStripMenuItem.Name = "پروندهToolStripMenuItem";
            this.پروندهToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.پروندهToolStripMenuItem.Text = "پرونده";
            // 
            // حذفToolStripMenuItem
            // 
            this.حذفToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("حذفToolStripMenuItem.Image")));
            this.حذفToolStripMenuItem.Name = "حذفToolStripMenuItem";
            this.حذفToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.حذفToolStripMenuItem.Text = "حذف";
            this.حذفToolStripMenuItem.Click += new System.EventHandler(this.حذفToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(172, 6);
            // 
            // جستجوToolStripMenuItem
            // 
            this.جستجوToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("جستجوToolStripMenuItem.Image")));
            this.جستجوToolStripMenuItem.Name = "جستجوToolStripMenuItem";
            this.جستجوToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.جستجوToolStripMenuItem.Text = "جستجو";
            this.جستجوToolStripMenuItem.Click += new System.EventHandler(this.جستجوToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // خروجToolStripMenuItem
            // 
            this.خروجToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("خروجToolStripMenuItem.Image")));
            this.خروجToolStripMenuItem.Name = "خروجToolStripMenuItem";
            this.خروجToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.خروجToolStripMenuItem.Text = "خروج";
            this.خروجToolStripMenuItem.Click += new System.EventHandler(this.خروجToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(630, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "نام :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(160, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(433, 21);
            this.txtName.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.پروندهToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 51;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this.toolStripLabel4,
            this.toolStripSeparator1,
            this.tsbIsDefault,
            this.tsbShare,
            this.toolStripLabel5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(720, 25);
            this.toolStrip1.TabIndex = 68;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel1.Text = "جدید";
            this.toolStripLabel1.Click += new System.EventHandler(this.جدیدToolStripMenuItem_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel2.Image")));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel2.Text = "ذخیره سازی";
            this.toolStripLabel2.Click += new System.EventHandler(this.ذخیرهسازیToolStripMenuItem_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel3.Image")));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel3.Text = "حذف";
            this.toolStripLabel3.Click += new System.EventHandler(this.حذفToolStripMenuItem_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel4.Image")));
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel4.Text = "جستجو";
            this.toolStripLabel4.Click += new System.EventHandler(this.جستجوToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbIsDefault
            // 
            this.tsbIsDefault.Image = ((System.Drawing.Image)(resources.GetObject("tsbIsDefault.Image")));
            this.tsbIsDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbIsDefault.Name = "tsbIsDefault";
            this.tsbIsDefault.Size = new System.Drawing.Size(78, 22);
            this.tsbIsDefault.Text = "پیش فرض";
            this.tsbIsDefault.Click += new System.EventHandler(this.tsbIsDefault_Click);
            // 
            // tsbShare
            // 
            this.tsbShare.Image = ((System.Drawing.Image)(resources.GetObject("tsbShare.Image")));
            this.tsbShare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShare.Name = "tsbShare";
            this.tsbShare.Size = new System.Drawing.Size(99, 22);
            this.tsbShare.Text = "اشتراک گذاری";
            this.tsbShare.Click += new System.EventHandler(this.tsbShare_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel5.Image")));
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel5.Text = "خروج";
            this.toolStripLabel5.Click += new System.EventHandler(this.خروجToolStripMenuItem_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label12.Location = new System.Drawing.Point(521, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 13);
            this.label12.TabIndex = 69;
            this.label12.Text = ":";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(534, 42);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(59, 21);
            this.txtCode.TabIndex = 3;
            // 
            // txtBackUpName
            // 
            this.txtBackUpName.Enabled = false;
            this.txtBackUpName.Location = new System.Drawing.Point(161, 42);
            this.txtBackUpName.Name = "txtBackUpName";
            this.txtBackUpName.Size = new System.Drawing.Size(355, 21);
            this.txtBackUpName.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.txtBackUpName);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(13, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات دستورکار";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtAmper);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboType);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtComment);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboVol);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(13, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(695, 170);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "مشخصات فنی";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(617, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "آمپراژ :";
            // 
            // txtAmper
            // 
            this.txtAmper.Location = new System.Drawing.Point(469, 23);
            this.txtAmper.Name = "txtAmper";
            this.txtAmper.Size = new System.Drawing.Size(123, 21);
            this.txtAmper.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(623, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "ولتاژ :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(439, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 80;
            this.label10.Text = "آمپر";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(627, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "نوع :";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(469, 71);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(123, 21);
            this.cboType.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(662, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 75;
            this.label13.Text = "*";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(39, 95);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(553, 21);
            this.txtComment.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(662, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(602, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "توضیحات :";
            // 
            // cboVol
            // 
            this.cboVol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVol.FormattingEnabled = true;
            this.cboVol.Items.AddRange(new object[] {
            "11000",
            "20000",
            "33000",
            "400"});
            this.cboVol.Location = new System.Drawing.Point(469, 47);
            this.cboVol.Name = "cboVol";
            this.cboVol.Size = new System.Drawing.Size(123, 21);
            this.cboVol.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(662, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 79;
            this.label9.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(439, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 78;
            this.label7.Text = "ولت";
            // 
            // frmCatOut02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 714);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCatOut02";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "کت اوت";
            this.Load += new System.EventHandler(this.frmCatOut_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmCatOut_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCatOut_FormClosing);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedEquipment)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperation)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.DataGridView gvSelectedEquipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TreeView tvEquipment;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDeleteOperation;
        private System.Windows.Forms.Button btnInsertOperation;
        private System.Windows.Forms.DataGridView gvOperation;
        private System.Windows.Forms.TreeView tvOperation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهسازیToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ذخیرهسازیوخروجToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem جدیدToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem پروندهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem حذفToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem جستجوToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem خروجToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbIsDefault;
        private System.Windows.Forms.ToolStripButton tsbShare;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOperationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtBackUpName;
        private System.Windows.Forms.ToolStripButton toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripLabel4;
        private System.Windows.Forms.ToolStripButton toolStripLabel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmper;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboVol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
	}
}