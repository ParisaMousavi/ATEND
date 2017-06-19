namespace Atend.Design
{
    partial class frmDrawFrame
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkInformation = new System.Windows.Forms.CheckBox();
            this.chkDescription = new System.Windows.Forms.CheckBox();
            this.chkSigns = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoLand = new System.Windows.Forms.RadioButton();
            this.rdoPort = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtArz1 = new System.Windows.Forms.TextBox();
            this.txtArz = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTol1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPageSize = new System.Windows.Forms.ComboBox();
            this.rdoSize2 = new System.Windows.Forms.RadioButton();
            this.rdoSize1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gvSigns = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.SignText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSigns)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 332);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtScale);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "تنظیمات اولیه";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkInformation);
            this.groupBox3.Controls.Add(this.chkDescription);
            this.groupBox3.Controls.Add(this.chkSigns);
            this.groupBox3.Location = new System.Drawing.Point(14, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 61);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "اطلاعات موجود در راهنما";
            // 
            // chkInformation
            // 
            this.chkInformation.AutoSize = true;
            this.chkInformation.Checked = true;
            this.chkInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInformation.Location = new System.Drawing.Point(212, 30);
            this.chkInformation.Name = "chkInformation";
            this.chkInformation.Size = new System.Drawing.Size(72, 17);
            this.chkInformation.TabIndex = 0;
            this.chkInformation.Text = "مشخصات";
            this.chkInformation.UseVisualStyleBackColor = true;
            // 
            // chkDescription
            // 
            this.chkDescription.AutoSize = true;
            this.chkDescription.Checked = true;
            this.chkDescription.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDescription.Location = new System.Drawing.Point(328, 30);
            this.chkDescription.Name = "chkDescription";
            this.chkDescription.Size = new System.Drawing.Size(66, 17);
            this.chkDescription.TabIndex = 0;
            this.chkDescription.Text = "توضیحات";
            this.chkDescription.UseVisualStyleBackColor = true;
            // 
            // chkSigns
            // 
            this.chkSigns.AutoSize = true;
            this.chkSigns.Checked = true;
            this.chkSigns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSigns.Location = new System.Drawing.Point(446, 30);
            this.chkSigns.Name = "chkSigns";
            this.chkSigns.Size = new System.Drawing.Size(95, 17);
            this.chkSigns.TabIndex = 0;
            this.chkSigns.Text = "علائم اختصاری";
            this.chkSigns.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoLand);
            this.groupBox2.Controls.Add(this.rdoPort);
            this.groupBox2.Location = new System.Drawing.Point(218, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 56);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "جهت نقشه";
            // 
            // rdoLand
            // 
            this.rdoLand.AutoSize = true;
            this.rdoLand.Checked = true;
            this.rdoLand.Location = new System.Drawing.Point(261, 26);
            this.rdoLand.Name = "rdoLand";
            this.rdoLand.Size = new System.Drawing.Size(76, 17);
            this.rdoLand.TabIndex = 1;
            this.rdoLand.TabStop = true;
            this.rdoLand.Text = "Landscape";
            this.rdoLand.UseVisualStyleBackColor = true;
            this.rdoLand.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rdoPort
            // 
            this.rdoPort.AutoSize = true;
            this.rdoPort.Location = new System.Drawing.Point(129, 26);
            this.rdoPort.Name = "rdoPort";
            this.rdoPort.Size = new System.Drawing.Size(61, 17);
            this.rdoPort.TabIndex = 2;
            this.rdoPort.Text = "Portrait";
            this.rdoPort.UseVisualStyleBackColor = true;
            this.rdoPort.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(14, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 190);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtArz1);
            this.groupBox1.Controls.Add(this.txtArz);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTol1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtTol);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboPageSize);
            this.groupBox1.Controls.Add(this.rdoSize2);
            this.groupBox1.Controls.Add(this.rdoSize1);
            this.groupBox1.Location = new System.Drawing.Point(218, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "سایز کادر";
            // 
            // txtArz1
            // 
            this.txtArz1.Enabled = false;
            this.txtArz1.Location = new System.Drawing.Point(24, 47);
            this.txtArz1.Name = "txtArz1";
            this.txtArz1.ReadOnly = true;
            this.txtArz1.Size = new System.Drawing.Size(56, 21);
            this.txtArz1.TabIndex = 5;
            // 
            // txtArz
            // 
            this.txtArz.Location = new System.Drawing.Point(24, 89);
            this.txtArz.Name = "txtArz";
            this.txtArz.Size = new System.Drawing.Size(56, 21);
            this.txtArz.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(119, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(81, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "عرض";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "عرض";
            // 
            // txtTol1
            // 
            this.txtTol1.Enabled = false;
            this.txtTol1.Location = new System.Drawing.Point(134, 47);
            this.txtTol1.Name = "txtTol1";
            this.txtTol1.ReadOnly = true;
            this.txtTol1.Size = new System.Drawing.Size(56, 21);
            this.txtTol1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(189, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "طول";
            // 
            // txtTol
            // 
            this.txtTol.Location = new System.Drawing.Point(134, 89);
            this.txtTol.Name = "txtTol";
            this.txtTol.Size = new System.Drawing.Size(56, 21);
            this.txtTol.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "طول";
            // 
            // cboPageSize
            // 
            this.cboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageSize.FormattingEnabled = true;
            this.cboPageSize.Location = new System.Drawing.Point(24, 24);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboPageSize.Size = new System.Drawing.Size(195, 21);
            this.cboPageSize.TabIndex = 3;
            this.cboPageSize.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_SelectedIndexChanged);
            // 
            // rdoSize2
            // 
            this.rdoSize2.AutoSize = true;
            this.rdoSize2.Location = new System.Drawing.Point(238, 91);
            this.rdoSize2.Name = "rdoSize2";
            this.rdoSize2.Size = new System.Drawing.Size(99, 17);
            this.rdoSize2.TabIndex = 2;
            this.rdoSize2.Text = "سایز قابل تعریف";
            this.rdoSize2.UseVisualStyleBackColor = true;
            // 
            // rdoSize1
            // 
            this.rdoSize1.AutoSize = true;
            this.rdoSize1.Checked = true;
            this.rdoSize1.Location = new System.Drawing.Point(235, 26);
            this.rdoSize1.Name = "rdoSize1";
            this.rdoSize1.Size = new System.Drawing.Size(102, 17);
            this.rdoSize1.TabIndex = 1;
            this.rdoSize1.TabStop = true;
            this.rdoSize1.Text = "سایز های موجود";
            this.rdoSize1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 144);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "1:";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(460, 140);
            this.txtScale.Name = "txtScale";
            this.txtScale.ReadOnly = true;
            this.txtScale.Size = new System.Drawing.Size(56, 21);
            this.txtScale.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(519, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "مقیاس";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gvSigns);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "تنظیمات علائم اختصاری";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gvSigns
            // 
            this.gvSigns.AllowUserToAddRows = false;
            this.gvSigns.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.gvSigns.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSigns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSigns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Column1,
            this.Column2,
            this.SignText,
            this.ProductCode});
            this.gvSigns.Location = new System.Drawing.Point(16, 17);
            this.gvSigns.Name = "gvSigns";
            this.gvSigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSigns.Size = new System.Drawing.Size(559, 272);
            this.gvSigns.TabIndex = 0;
            this.gvSigns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSigns_CellContentClick);
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.Visible = false;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 27.36041F;
            this.Column1.HeaderText = "مشاهده";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 55.83756F;
            this.Column2.HeaderText = "تصویر";
            this.Column2.Name = "Column2";
            // 
            // SignText
            // 
            this.SignText.FillWeight = 136.802F;
            this.SignText.HeaderText = "عنوان";
            this.SignText.Name = "SignText";
            this.SignText.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SignText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "ProductCode";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Visible = false;
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(16, 350);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "انصراف";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(96, 350);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmDrawFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 385);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "frmDrawFrame";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ترسیم کادر";
            this.Load += new System.EventHandler(this.frmDrawFrame_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrawFrame_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSigns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtArz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPageSize;
        private System.Windows.Forms.RadioButton rdoSize2;
        private System.Windows.Forms.RadioButton rdoSize1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtArz1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTol1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoLand;
        private System.Windows.Forms.RadioButton rdoPort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkInformation;
        private System.Windows.Forms.CheckBox chkDescription;
        private System.Windows.Forms.CheckBox chkSigns;
        private System.Windows.Forms.DataGridView gvSigns;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
    }
}