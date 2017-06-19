namespace Atend.Design
{
    partial class frmDesignSaveServer02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesignSaveServer02));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectDWF = new System.Windows.Forms.Button();
            this.btnSelectBook = new System.Windows.Forms.Button();
            this.chkIsComplement = new System.Windows.Forms.CheckBox();
            this.txtDWFAddress = new System.Windows.Forms.TextBox();
            this.txtBookAddress = new System.Windows.Forms.TextBox();
            this.txtDesignFullAddress = new System.Windows.Forms.TextBox();
            this.txtDesignName = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTitleSearch = new System.Windows.Forms.TextBox();
            this.gvDesign1 = new System.Windows.Forms.DataGridView();
            this.imgFile = new System.Windows.Forms.DataGridViewImageColumn();
            this.imgBook = new System.Windows.Forms.DataGridViewImageColumn();
            this.imgDWF = new System.Windows.Forms.DataGridViewImageColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDesign1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectDWF);
            this.groupBox1.Controls.Add(this.btnSelectBook);
            this.groupBox1.Controls.Add(this.chkIsComplement);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txtDWFAddress);
            this.groupBox1.Controls.Add(this.txtBookAddress);
            this.groupBox1.Controls.Add(this.txtDesignFullAddress);
            this.groupBox1.Controls.Add(this.txtDesignName);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 178);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات فایل";
            // 
            // btnSelectDWF
            // 
            this.btnSelectDWF.Location = new System.Drawing.Point(131, 137);
            this.btnSelectDWF.Name = "btnSelectDWF";
            this.btnSelectDWF.Size = new System.Drawing.Size(35, 23);
            this.btnSelectDWF.TabIndex = 4;
            this.btnSelectDWF.Text = "...";
            this.btnSelectDWF.UseVisualStyleBackColor = true;
            this.btnSelectDWF.Click += new System.EventHandler(this.btnSelectDWF_Click);
            // 
            // btnSelectBook
            // 
            this.btnSelectBook.Location = new System.Drawing.Point(131, 108);
            this.btnSelectBook.Name = "btnSelectBook";
            this.btnSelectBook.Size = new System.Drawing.Size(35, 23);
            this.btnSelectBook.TabIndex = 4;
            this.btnSelectBook.Text = "...";
            this.btnSelectBook.UseVisualStyleBackColor = true;
            this.btnSelectBook.Click += new System.EventHandler(this.btnSelectBook_Click);
            // 
            // chkIsComplement
            // 
            this.chkIsComplement.AutoSize = true;
            this.chkIsComplement.Location = new System.Drawing.Point(77, 16);
            this.chkIsComplement.Name = "chkIsComplement";
            this.chkIsComplement.Size = new System.Drawing.Size(175, 17);
            this.chkIsComplement.TabIndex = 3;
            this.chkIsComplement.Text = "ذخیره سازی طرح به صورت متمم";
            this.chkIsComplement.UseVisualStyleBackColor = true;
            this.chkIsComplement.Visible = false;
            // 
            // txtDWFAddress
            // 
            this.txtDWFAddress.Location = new System.Drawing.Point(172, 137);
            this.txtDWFAddress.Name = "txtDWFAddress";
            this.txtDWFAddress.ReadOnly = true;
            this.txtDWFAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDWFAddress.Size = new System.Drawing.Size(424, 21);
            this.txtDWFAddress.TabIndex = 1;
            // 
            // txtBookAddress
            // 
            this.txtBookAddress.Location = new System.Drawing.Point(172, 110);
            this.txtBookAddress.Name = "txtBookAddress";
            this.txtBookAddress.ReadOnly = true;
            this.txtBookAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBookAddress.Size = new System.Drawing.Size(424, 21);
            this.txtBookAddress.TabIndex = 1;
            // 
            // txtDesignFullAddress
            // 
            this.txtDesignFullAddress.Location = new System.Drawing.Point(131, 83);
            this.txtDesignFullAddress.Name = "txtDesignFullAddress";
            this.txtDesignFullAddress.ReadOnly = true;
            this.txtDesignFullAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDesignFullAddress.Size = new System.Drawing.Size(465, 21);
            this.txtDesignFullAddress.TabIndex = 1;
            // 
            // txtDesignName
            // 
            this.txtDesignName.Location = new System.Drawing.Point(415, 29);
            this.txtDesignName.Name = "txtDesignName";
            this.txtDesignName.ReadOnly = true;
            this.txtDesignName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDesignName.Size = new System.Drawing.Size(181, 21);
            this.txtDesignName.TabIndex = 1;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(131, 56);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTitle.Size = new System.Drawing.Size(465, 21);
            this.txtTitle.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(633, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "نام فایل طرح:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(615, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "مسیر فایل DWF:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(606, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "مسیر دفترچه طرح:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(614, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "مسیر جاری فایل:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(642, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "عنوان طرح:";
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Image = ((System.Drawing.Image)(resources.GetObject("btnCancle.Image")));
            this.btnCancle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancle.Location = new System.Drawing.Point(24, 540);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(100, 25);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "انصراف";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(130, 540);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 25);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtTitleSearch);
            this.groupBox2.Controls.Add(this.gvDesign1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(710, 336);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "لیست طرح های موجود در سرور";
            // 
            // txtTitleSearch
            // 
            this.txtTitleSearch.Location = new System.Drawing.Point(77, 30);
            this.txtTitleSearch.Name = "txtTitleSearch";
            this.txtTitleSearch.Size = new System.Drawing.Size(555, 21);
            this.txtTitleSearch.TabIndex = 8;
            this.txtTitleSearch.TextChanged += new System.EventHandler(this.txtTitleSearch_TextChanged);
            // 
            // gvDesign1
            // 
            this.gvDesign1.AllowUserToAddRows = false;
            this.gvDesign1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.gvDesign1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvDesign1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvDesign1.BackgroundColor = System.Drawing.Color.White;
            this.gvDesign1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDesign1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgFile,
            this.imgBook,
            this.imgDWF,
            this.ID,
            this.Column1,
            this.ArchiveNo,
            this.Column5,
            this.RequestType});
            this.gvDesign1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.gvDesign1.Location = new System.Drawing.Point(12, 57);
            this.gvDesign1.Name = "gvDesign1";
            this.gvDesign1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDesign1.Size = new System.Drawing.Size(681, 273);
            this.gvDesign1.TabIndex = 7;
            this.gvDesign1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDesign1_CellContentClick);
            // 
            // imgFile
            // 
            this.imgFile.FillWeight = 9.170926F;
            this.imgFile.HeaderText = "";
            this.imgFile.Name = "imgFile";
            // 
            // imgBook
            // 
            this.imgBook.FillWeight = 10F;
            this.imgBook.HeaderText = "";
            this.imgBook.Name = "imgBook";
            // 
            // imgDWF
            // 
            this.imgDWF.FillWeight = 10F;
            this.imgDWF.HeaderText = "";
            this.imgDWF.Name = "imgDWF";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Code";
            this.Column1.FillWeight = 25F;
            this.Column1.HeaderText = "کد";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // ArchiveNo
            // 
            this.ArchiveNo.DataPropertyName = "ArchiveNo";
            this.ArchiveNo.FillWeight = 39.25219F;
            this.ArchiveNo.HeaderText = "شماره طرح";
            this.ArchiveNo.Name = "ArchiveNo";
            this.ArchiveNo.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Title";
            this.Column5.FillWeight = 98.13049F;
            this.Column5.HeaderText = "عنوان";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // RequestType
            // 
            this.RequestType.DataPropertyName = "RequestType";
            this.RequestType.FillWeight = 9.813047F;
            this.RequestType.HeaderText = "متمم";
            this.RequestType.Name = "RequestType";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(638, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "عنوان طرح";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmDesignSaveServer02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 572);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDesignSaveServer02";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.Text = "ذخیره سازی طرح روی سرور";
            this.Load += new System.EventHandler(this.frmDesignSaveServer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDesignSaveServer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDesign1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDesignFullAddress;
        private System.Windows.Forms.TextBox txtDesignName;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvDesign1;
        private System.Windows.Forms.CheckBox chkIsComplement;
        private System.Windows.Forms.Button btnSelectDWF;
        private System.Windows.Forms.Button btnSelectBook;
        private System.Windows.Forms.TextBox txtDWFAddress;
        private System.Windows.Forms.TextBox txtBookAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitleSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewImageColumn imgFile;
        private System.Windows.Forms.DataGridViewImageColumn imgBook;
        private System.Windows.Forms.DataGridViewImageColumn imgDWF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestType;
        private System.Windows.Forms.Button btnSearch;
    }
}