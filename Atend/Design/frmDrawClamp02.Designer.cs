﻿namespace Atend.Design
{
    partial class frmDrawCalamp02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDrawCalamp02));
            this.cboIsExist = new System.Windows.Forms.ComboBox();
            this.gvConductor = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboVol = new System.Windows.Forms.ComboBox();
            this.chkVol = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboProjCode = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvConductor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboIsExist
            // 
            this.cboIsExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsExist.FormattingEnabled = true;
            this.cboIsExist.Location = new System.Drawing.Point(6, 21);
            this.cboIsExist.Name = "cboIsExist";
            this.cboIsExist.Size = new System.Drawing.Size(227, 21);
            this.cboIsExist.TabIndex = 7;
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
            this.gvConductor.Location = new System.Drawing.Point(6, 66);
            this.gvConductor.MultiSelect = false;
            this.gvConductor.Name = "gvConductor";
            this.gvConductor.ReadOnly = true;
            this.gvConductor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvConductor.Size = new System.Drawing.Size(779, 313);
            this.gvConductor.TabIndex = 4;
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
            this.btnOk.Image = global::Atend.ResourceImage._16_button_ok;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(100, 444);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "تایید";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboVol);
            this.groupBox1.Controls.Add(this.chkVol);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو بر اساس...";
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
            this.cboVol.Location = new System.Drawing.Point(563, 22);
            this.cboVol.Name = "cboVol";
            this.cboVol.Size = new System.Drawing.Size(121, 21);
            this.cboVol.TabIndex = 2;
            // 
            // chkVol
            // 
            this.chkVol.AutoSize = true;
            this.chkVol.Location = new System.Drawing.Point(690, 24);
            this.chkVol.Name = "chkVol";
            this.chkVol.Size = new System.Drawing.Size(49, 17);
            this.chkVol.TabIndex = 1;
            this.chkVol.Text = "ولتاژ:";
            this.chkVol.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(20, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Atend.ResourceImage._16button_cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(22, 443);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboProjCode
            // 
            this.cboProjCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProjCode.FormattingEnabled = true;
            this.cboProjCode.Location = new System.Drawing.Point(311, 21);
            this.cboProjCode.Name = "cboProjCode";
            this.cboProjCode.Size = new System.Drawing.Size(366, 21);
            this.cboProjCode.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cboProjCode);
            this.groupBox3.Controls.Add(this.cboIsExist);
            this.groupBox3.Location = new System.Drawing.Point(6, 383);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(779, 54);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "مشخصات دستورکار";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(234, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = " وضعیت تجهیز:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(679, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "شرح دستور کار:";
            // 
            // frmDrawCalamp02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 473);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gvConductor);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDrawCalamp02";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ترسیم کلمپ";
            this.Load += new System.EventHandler(this.frmDrawHeaderCable02_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDrawHeaderCable02_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gvConductor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboIsExist;
        private System.Windows.Forms.DataGridView gvConductor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboVol;
        private System.Windows.Forms.CheckBox chkVol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn XCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSql;
        private System.Windows.Forms.ComboBox cboProjCode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}