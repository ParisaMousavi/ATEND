namespace Atend.Global.Design
{
    partial class frmEditDrawKablSho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditDrawKablSho));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.chkType = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboProjCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkMaterial = new System.Windows.Forms.CheckBox();
            this.cboIsExist = new System.Windows.Forms.ComboBox();
            this.gvConductor = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.cboMaterial = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWeekHeader = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gvConductor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "11000",
            "20000",
            "33000",
            "400"});
            this.cboType.Location = new System.Drawing.Point(587, 19);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(121, 21);
            this.cboType.TabIndex = 7;
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Location = new System.Drawing.Point(714, 21);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(45, 17);
            this.chkType.TabIndex = 6;
            this.chkType.Text = "نوع:";
            this.chkType.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(681, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "شرح دستور کار:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(18, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 25);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboProjCode
            // 
            this.cboProjCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProjCode.FormattingEnabled = true;
            this.cboProjCode.Location = new System.Drawing.Point(349, 19);
            this.cboProjCode.Name = "cboProjCode";
            this.cboProjCode.Size = new System.Drawing.Size(326, 21);
            this.cboProjCode.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = " وضعیت تجهیز:";
            // 
            // chkMaterial
            // 
            this.chkMaterial.AutoSize = true;
            this.chkMaterial.Location = new System.Drawing.Point(476, 21);
            this.chkMaterial.Name = "chkMaterial";
            this.chkMaterial.Size = new System.Drawing.Size(55, 17);
            this.chkMaterial.TabIndex = 8;
            this.chkMaterial.Text = "جنس:";
            this.chkMaterial.UseVisualStyleBackColor = true;
            // 
            // cboIsExist
            // 
            this.cboIsExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsExist.FormattingEnabled = true;
            this.cboIsExist.Location = new System.Drawing.Point(18, 19);
            this.cboIsExist.Name = "cboIsExist";
            this.cboIsExist.Size = new System.Drawing.Size(206, 21);
            this.cboIsExist.TabIndex = 44;
            //this.cboIsExist.SelectedIndexChanged += new System.EventHandler(this.cboIsExist_SelectedIndexChanged);
            this.cboIsExist.SelectedValueChanged += new System.EventHandler(this.cboIsExist_SelectedValueChanged);
            // 
            // gvConductor
            // 
            this.gvConductor.AllowUserToAddRows = false;
            this.gvConductor.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.gvConductor.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvConductor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvConductor.BackgroundColor = System.Drawing.Color.White;
            this.gvConductor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvConductor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.XCode,
            this.dataGridViewTextBoxColumn2,
            this.IsSql});
            this.gvConductor.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.gvConductor.Location = new System.Drawing.Point(7, 65);
            this.gvConductor.MultiSelect = false;
            this.gvConductor.Name = "gvConductor";
            this.gvConductor.ReadOnly = true;
            this.gvConductor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvConductor.Size = new System.Drawing.Size(779, 280);
            this.gvConductor.TabIndex = 43;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // XCode
            // 
            this.XCode.DataPropertyName = "XCode";
            this.XCode.HeaderText = "XCode";
            this.XCode.Name = "XCode";
            this.XCode.ReadOnly = true;
            this.XCode.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.FillWeight = 70F;
            this.dataGridViewTextBoxColumn2.HeaderText = "نام";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // IsSql
            // 
            this.IsSql.DataPropertyName = "IsSql";
            this.IsSql.HeaderText = "IsSql";
            this.IsSql.Name = "IsSql";
            this.IsSql.ReadOnly = true;
            this.IsSql.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(131, 406);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 25);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cboMaterial
            // 
            this.cboMaterial.FormattingEnabled = true;
            this.cboMaterial.Location = new System.Drawing.Point(349, 19);
            this.cboMaterial.Name = "cboMaterial";
            this.cboMaterial.Size = new System.Drawing.Size(121, 21);
            this.cboMaterial.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboMaterial);
            this.groupBox1.Controls.Add(this.chkMaterial);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.chkType);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(7, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 50);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو بر اساس...";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(25, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboProjCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboIsExist);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(7, 350);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 50);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "مشخصات دستورکار";
            // 
            // btnWeekHeader
            // 
            this.btnWeekHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWeekHeader.AutoSize = true;
            this.btnWeekHeader.Location = new System.Drawing.Point(237, 412);
            this.btnWeekHeader.Name = "btnWeekHeader";
            this.btnWeekHeader.Size = new System.Drawing.Size(133, 13);
            this.btnWeekHeader.TabIndex = 49;
            this.btnWeekHeader.TabStop = true;
            this.btnWeekHeader.Text = "انتخاب سرکابل فشار ضعیف";
            this.btnWeekHeader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnWeekHeader_LinkClicked);
            // 
            // frmDrawKablSho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 436);
            this.Controls.Add(this.btnWeekHeader);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gvConductor);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDrawKablSho";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "تعیین نوع کابلشو";
            this.Load += new System.EventHandler(this.frmDrawKablSho_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrawKablSho_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gvConductor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboProjCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkMaterial;
        private System.Windows.Forms.ComboBox cboIsExist;
        private System.Windows.Forms.DataGridView gvConductor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn XCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSql;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cboMaterial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel btnWeekHeader;
    }
}