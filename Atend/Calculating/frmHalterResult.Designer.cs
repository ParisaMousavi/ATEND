namespace Atend.Calculating
{
    partial class frmHalterResult
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHalterResult));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gvCommentPowerWithHalter = new System.Windows.Forms.DataGridView();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.gvCommentPower = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcPoleGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentPole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.انتقالبهفایلExelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.انتقالبهفایلEXCELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.خروجToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.DcPole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DcCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcHalterPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcHalterCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HalterXCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommentPowerWithHalter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommentPower)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gvCommentPowerWithHalter);
            this.groupBox1.Controls.Add(this.chkSelect);
            this.groupBox1.Controls.Add(this.gvCommentPower);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(679, 369);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "پیشنهاد قدرت پایه";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // gvCommentPowerWithHalter
            // 
            this.gvCommentPowerWithHalter.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.gvCommentPowerWithHalter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCommentPowerWithHalter.BackgroundColor = System.Drawing.Color.White;
            this.gvCommentPowerWithHalter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCommentPowerWithHalter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DcPole,
            this.Column2,
            this.Column3,
            this.Column5,
            this.DcPower,
            this.Column4,
            this.Column8,
            this.DcCount,
            this.DcName,
            this.DcHalterPower,
            this.DcHalterCount,
            this.Column6,
            this.HalterXCode});
            this.gvCommentPowerWithHalter.Location = new System.Drawing.Point(6, 25);
            this.gvCommentPowerWithHalter.Name = "gvCommentPowerWithHalter";
            this.gvCommentPowerWithHalter.ReadOnly = true;
            this.gvCommentPowerWithHalter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCommentPowerWithHalter.Size = new System.Drawing.Size(660, 317);
            this.gvCommentPowerWithHalter.TabIndex = 2;
            this.gvCommentPowerWithHalter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gvCommentPowerWithHalter_MouseMove);
            this.gvCommentPowerWithHalter.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCommentPowerWithHalter_CellClick);
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(555, 346);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(111, 17);
            this.chkSelect.TabIndex = 2;
            this.chkSelect.Text = "انتخاب همه پایه ها";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // gvCommentPower
            // 
            this.gvCommentPower.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.gvCommentPower.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvCommentPower.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvCommentPower.BackgroundColor = System.Drawing.Color.White;
            this.gvCommentPower.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCommentPower.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.DcPoleGuid,
            this.Code,
            this.CurrentPole,
            this.Power,
            this.CommentPole,
            this.Column1,
            this.Count,
            this.Select});
            this.gvCommentPower.Location = new System.Drawing.Point(6, 24);
            this.gvCommentPower.Name = "gvCommentPower";
            this.gvCommentPower.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCommentPower.Size = new System.Drawing.Size(660, 318);
            this.gvCommentPower.TabIndex = 1;
            this.gvCommentPower.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gvCommentPower_MouseMove);
            this.gvCommentPower.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCommentPower_CellClick);
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column7.DataPropertyName = "DcPole";
            this.Column7.HeaderText = "شماره پایه";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 74;
            // 
            // DcPoleGuid
            // 
            this.DcPoleGuid.DataPropertyName = "DcPoleGuid";
            this.DcPoleGuid.HeaderText = "PoleGuid";
            this.DcPoleGuid.Name = "DcPoleGuid";
            this.DcPoleGuid.Visible = false;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Code.DataPropertyName = "PoleCode";
            this.Code.HeaderText = "PoleCode";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Visible = false;
            // 
            // CurrentPole
            // 
            this.CurrentPole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CurrentPole.DataPropertyName = "CurrentPole";
            this.CurrentPole.HeaderText = "پایه موجود";
            this.CurrentPole.Name = "CurrentPole";
            this.CurrentPole.ReadOnly = true;
            this.CurrentPole.Width = 72;
            // 
            // Power
            // 
            this.Power.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Power.DataPropertyName = "Power";
            this.Power.HeaderText = "قدرت پایه پیشنهادی ";
            this.Power.Name = "Power";
            this.Power.ReadOnly = true;
            this.Power.Width = 116;
            // 
            // CommentPole
            // 
            this.CommentPole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CommentPole.DataPropertyName = "CommentPole";
            this.CommentPole.HeaderText = "پایه پیشنهادی";
            this.CommentPole.Name = "CommentPole";
            this.CommentPole.ReadOnly = true;
            this.CommentPole.Width = 89;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "انتخاب";
            this.Column1.Name = "Column1";
            this.Column1.Width = 42;
            // 
            // Count
            // 
            this.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Count.DataPropertyName = "Count";
            this.Count.HeaderText = "تعداد";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Width = 55;
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Select.DataPropertyName = "Select";
            this.Select.HeaderText = "انتخاب";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.Width = 42;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.انتقالبهفایلExelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // انتقالبهفایلExelToolStripMenuItem
            // 
            this.انتقالبهفایلExelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.انتقالبهفایلEXCELToolStripMenuItem,
            this.toolStripSeparator2,
            this.خروجToolStripMenuItem1});
            this.انتقالبهفایلExelToolStripMenuItem.Name = "انتقالبهفایلExelToolStripMenuItem";
            this.انتقالبهفایلExelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.انتقالبهفایلExelToolStripMenuItem.Text = "پرونده";
            this.انتقالبهفایلExelToolStripMenuItem.Click += new System.EventHandler(this.انتقالبهفایلExelToolStripMenuItem_Click);
            // 
            // انتقالبهفایلEXCELToolStripMenuItem
            // 
            this.انتقالبهفایلEXCELToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("انتقالبهفایلEXCELToolStripMenuItem.Image")));
            this.انتقالبهفایلEXCELToolStripMenuItem.Name = "انتقالبهفایلEXCELToolStripMenuItem";
            this.انتقالبهفایلEXCELToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.انتقالبهفایلEXCELToolStripMenuItem.Text = "انتقال به فایل EXCEL";
            this.انتقالبهفایلEXCELToolStripMenuItem.Click += new System.EventHandler(this.انتقالبهفایلEXCELToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // خروجToolStripMenuItem1
            // 
            this.خروجToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("خروجToolStripMenuItem1.Image")));
            this.خروجToolStripMenuItem1.Name = "خروجToolStripMenuItem1";
            this.خروجToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.خروجToolStripMenuItem1.Text = "خروج";
            this.خروجToolStripMenuItem1.Click += new System.EventHandler(this.خروجToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnChange,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(700, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnChange
            // 
            this.btnChange.Image = ((System.Drawing.Image)(resources.GetObject("btnChange.Image")));
            this.btnChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(91, 22);
            this.btnChange.Text = "اعمال تغییرات";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(120, 22);
            this.toolStripButton1.Text = "انتقال به فایل EXCEL";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton2.Text = "خروج";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // DcPole
            // 
            this.DcPole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DcPole.DataPropertyName = "DcPole";
            this.DcPole.HeaderText = "شماره پایه";
            this.DcPole.Name = "DcPole";
            this.DcPole.ReadOnly = true;
            this.DcPole.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DcPoleGuid";
            this.Column2.HeaderText = "DcPoleGuid";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "PoleCode";
            this.Column3.HeaderText = "PoleCode";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.DataPropertyName = "CurrentPole";
            this.Column5.HeaderText = "پایه موجود";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // DcPower
            // 
            this.DcPower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DcPower.DataPropertyName = "Power";
            this.DcPower.FillWeight = 50F;
            this.DcPower.HeaderText = "قدرت پایه";
            this.DcPower.Name = "DcPower";
            this.DcPower.ReadOnly = true;
            this.DcPower.Width = 74;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.DataPropertyName = "CommentPole";
            this.Column4.HeaderText = "پایه پیشنهادی";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 97;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column8.HeaderText = "انتخاب";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 42;
            // 
            // DcCount
            // 
            this.DcCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DcCount.DataPropertyName = "Count";
            this.DcCount.FillWeight = 50F;
            this.DcCount.HeaderText = "تعداد پایه";
            this.DcCount.Name = "DcCount";
            this.DcCount.ReadOnly = true;
            this.DcCount.Width = 73;
            // 
            // DcName
            // 
            this.DcName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DcName.DataPropertyName = "Name";
            this.DcName.HeaderText = "نوع مهار";
            this.DcName.Name = "DcName";
            this.DcName.ReadOnly = true;
            this.DcName.Width = 69;
            // 
            // DcHalterPower
            // 
            this.DcHalterPower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DcHalterPower.DataPropertyName = "HalterPower";
            this.DcHalterPower.HeaderText = "قدرت مهار";
            this.DcHalterPower.Name = "DcHalterPower";
            this.DcHalterPower.ReadOnly = true;
            this.DcHalterPower.Width = 78;
            // 
            // DcHalterCount
            // 
            this.DcHalterCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DcHalterCount.DataPropertyName = "HalterCount";
            this.DcHalterCount.FillWeight = 50F;
            this.DcHalterCount.HeaderText = "تعداد مهار";
            this.DcHalterCount.Name = "DcHalterCount";
            this.DcHalterCount.ReadOnly = true;
            this.DcHalterCount.Width = 77;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Select";
            this.Column6.HeaderText = "انتخاب";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // HalterXCode
            // 
            this.HalterXCode.DataPropertyName = "HalterXCode";
            this.HalterXCode.HeaderText = "HalterXCode";
            this.HalterXCode.Name = "HalterXCode";
            this.HalterXCode.ReadOnly = true;
            this.HalterXCode.Visible = false;
            // 
            // frmHalterResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 433);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHalterResult";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "پیشنهاد قدرت پایه";
            this.Load += new System.EventHandler(this.frmHalterResult_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommentPowerWithHalter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommentPower)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvCommentPower;
        private System.Windows.Forms.DataGridView gvCommentPowerWithHalter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem انتقالبهفایلExelToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem انتقالبهفایلEXCELToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem خروجToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.ToolStripButton btnChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcPoleGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPole;
        private System.Windows.Forms.DataGridViewTextBoxColumn Power;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentPole;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcPole;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcHalterPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcHalterCount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn HalterXCode;
    }
}