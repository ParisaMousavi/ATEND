namespace Atend.Calculating
{
    partial class frmSetDefaultMec
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNetCross = new System.Windows.Forms.Button();
            this.btnWeather = new System.Windows.Forms.Button();
            this.cboNetCross = new System.Windows.Forms.ComboBox();
            this.txtTrustBorder = new System.Windows.Forms.TextBox();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtUTS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.cboPower = new System.Windows.Forms.ComboBox();
            this.lstPower = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNetCross);
            this.groupBox1.Controls.Add(this.btnWeather);
            this.groupBox1.Controls.Add(this.cboNetCross);
            this.groupBox1.Controls.Add(this.txtTrustBorder);
            this.groupBox1.Controls.Add(this.txtDistance);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEnd);
            this.groupBox1.Controls.Add(this.txtStart);
            this.groupBox1.Controls.Add(this.txtUTS);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(194, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 254);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تعیین پیش فرض محاسبات مکانیکی";
            // 
            // btnNetCross
            // 
            this.btnNetCross.Location = new System.Drawing.Point(137, 222);
            this.btnNetCross.Name = "btnNetCross";
            this.btnNetCross.Size = new System.Drawing.Size(100, 23);
            this.btnNetCross.TabIndex = 23;
            this.btnNetCross.Text = "محل عبور شبکه";
            this.btnNetCross.UseVisualStyleBackColor = true;
            this.btnNetCross.Click += new System.EventHandler(this.btnNetCross_Click);
            // 
            // btnWeather
            // 
            this.btnWeather.Location = new System.Drawing.Point(29, 222);
            this.btnWeather.Name = "btnWeather";
            this.btnWeather.Size = new System.Drawing.Size(101, 23);
            this.btnWeather.TabIndex = 22;
            this.btnWeather.Text = "شرایط آب و هوایی";
            this.btnWeather.UseVisualStyleBackColor = true;
            this.btnWeather.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboNetCross
            // 
            this.cboNetCross.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNetCross.FormattingEnabled = true;
            this.cboNetCross.Location = new System.Drawing.Point(28, 190);
            this.cboNetCross.Name = "cboNetCross";
            this.cboNetCross.Size = new System.Drawing.Size(121, 21);
            this.cboNetCross.TabIndex = 21;
            // 
            // txtTrustBorder
            // 
            this.txtTrustBorder.Location = new System.Drawing.Point(28, 152);
            this.txtTrustBorder.Name = "txtTrustBorder";
            this.txtTrustBorder.Size = new System.Drawing.Size(121, 21);
            this.txtTrustBorder.TabIndex = 20;
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(28, 118);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(121, 21);
            this.txtDistance.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "فاصله دمایی:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "حاشیه اطمینان:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "محل عبور شبکه:";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(28, 85);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(121, 21);
            this.txtEnd.TabIndex = 15;
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(28, 50);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(121, 21);
            this.txtStart.TabIndex = 14;
            // 
            // txtUTS
            // 
            this.txtUTS.Location = new System.Drawing.Point(28, 16);
            this.txtUTS.Name = "txtUTS";
            this.txtUTS.Size = new System.Drawing.Size(121, 21);
            this.txtUTS.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "UTS:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "شروع دما:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "پایان دما:";
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(71, 268);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(62, 23);
            this.btnsave.TabIndex = 14;
            this.btnsave.Text = "ذخیره";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(8, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnInsert);
            this.groupBox2.Controls.Add(this.cboPower);
            this.groupBox2.Controls.Add(this.lstPower);
            this.groupBox2.Location = new System.Drawing.Point(4, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 254);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "تعیین قدرت پایه های پیشنهادی";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(6, 222);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 23);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(4, 32);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(46, 23);
            this.btnInsert.TabIndex = 27;
            this.btnInsert.Text = "اضافه";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // cboPower
            // 
            this.cboPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPower.FormattingEnabled = true;
            this.cboPower.Items.AddRange(new object[] {
            "200",
            "400",
            "600",
            "800",
            "1000",
            "1200"});
            this.cboPower.Location = new System.Drawing.Point(53, 34);
            this.cboPower.Name = "cboPower";
            this.cboPower.Size = new System.Drawing.Size(121, 21);
            this.cboPower.TabIndex = 25;
            // 
            // lstPower
            // 
            this.lstPower.FormattingEnabled = true;
            this.lstPower.Location = new System.Drawing.Point(6, 74);
            this.lstPower.Name = "lstPower";
            this.lstPower.Size = new System.Drawing.Size(169, 147);
            this.lstPower.TabIndex = 26;
            // 
            // frmSetDefaultMec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 295);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetDefaultMec";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تعیین پیش فرض محاسبات مکانیکی";
            this.Load += new System.EventHandler(this.frmSetDefaultMec_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNetCross;
        private System.Windows.Forms.Button btnWeather;
        private System.Windows.Forms.ComboBox cboNetCross;
        private System.Windows.Forms.TextBox txtTrustBorder;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtUTS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboPower;
        private System.Windows.Forms.ListBox lstPower;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;

    }
}