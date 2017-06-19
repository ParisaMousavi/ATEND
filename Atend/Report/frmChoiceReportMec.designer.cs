namespace Atend.Report
{
    partial class frmChoiceReportMec
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
            this.rdbPowerWithOutHalter = new System.Windows.Forms.RadioButton();
            this.rdbPowerwithHalter = new System.Windows.Forms.RadioButton();
            this.rdbRusSurface = new System.Windows.Forms.RadioButton();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPloe = new System.Windows.Forms.RadioButton();
            this.rbConductorDay = new System.Windows.Forms.RadioButton();
            this.rbSagTension = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbPowerWithOutHalter);
            this.groupBox1.Controls.Add(this.rdbPowerwithHalter);
            this.groupBox1.Controls.Add(this.rdbRusSurface);
            this.groupBox1.Controls.Add(this.cbFormat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbPloe);
            this.groupBox1.Controls.Add(this.rbConductorDay);
            this.groupBox1.Controls.Add(this.rbSagTension);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 208);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "گزارش محاسبات";
            // 
            // rdbPowerWithOutHalter
            // 
            this.rdbPowerWithOutHalter.AutoSize = true;
            this.rdbPowerWithOutHalter.Location = new System.Drawing.Point(149, 178);
            this.rdbPowerWithOutHalter.Name = "rdbPowerWithOutHalter";
            this.rdbPowerWithOutHalter.Size = new System.Drawing.Size(153, 17);
            this.rdbPowerWithOutHalter.TabIndex = 7;
            this.rdbPowerWithOutHalter.TabStop = true;
            this.rdbPowerWithOutHalter.Text = "پیشنهاد قدرت پایه بدون مهار";
            this.rdbPowerWithOutHalter.UseVisualStyleBackColor = true;
            // 
            // rdbPowerwithHalter
            // 
            this.rdbPowerwithHalter.AutoSize = true;
            this.rdbPowerwithHalter.Location = new System.Drawing.Point(164, 153);
            this.rdbPowerwithHalter.Name = "rdbPowerwithHalter";
            this.rdbPowerwithHalter.Size = new System.Drawing.Size(138, 17);
            this.rdbPowerwithHalter.TabIndex = 6;
            this.rdbPowerwithHalter.TabStop = true;
            this.rdbPowerwithHalter.Text = "پیشنهاد قدرت پایه با مهار";
            this.rdbPowerwithHalter.UseVisualStyleBackColor = true;
            // 
            // rdbRusSurface
            // 
            this.rdbRusSurface.AutoSize = true;
            this.rdbRusSurface.Location = new System.Drawing.Point(179, 130);
            this.rdbRusSurface.Name = "rdbRusSurface";
            this.rdbRusSurface.Size = new System.Drawing.Size(123, 17);
            this.rdbRusSurface.TabIndex = 5;
            this.rdbRusSurface.TabStop = true;
            this.rdbRusSurface.Text = "جدول سطوح نا هموار";
            this.rdbRusSurface.UseVisualStyleBackColor = true;
            this.rdbRusSurface.CheckedChanged += new System.EventHandler(this.rdbRusSurface_CheckedChanged);
            // 
            // cbFormat
            // 
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "روش UTS",
            "روش MaxF"});
            this.cbFormat.Location = new System.Drawing.Point(117, 34);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(121, 21);
            this.cbFormat.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "انتخاب روش";
            // 
            // rbPloe
            // 
            this.rbPloe.AutoSize = true;
            this.rbPloe.Location = new System.Drawing.Point(162, 107);
            this.rbPloe.Name = "rbPloe";
            this.rbPloe.Size = new System.Drawing.Size(140, 17);
            this.rbPloe.TabIndex = 2;
            this.rbPloe.TabStop = true;
            this.rbPloe.Text = "جدول نیروهای وارد بر پایه";
            this.rbPloe.UseVisualStyleBackColor = true;
            // 
            // rbConductorDay
            // 
            this.rbConductorDay.AutoSize = true;
            this.rbConductorDay.Enabled = false;
            this.rbConductorDay.Location = new System.Drawing.Point(179, 84);
            this.rbConductorDay.Name = "rbConductorDay";
            this.rbConductorDay.Size = new System.Drawing.Size(123, 17);
            this.rbConductorDay.TabIndex = 1;
            this.rbConductorDay.TabStop = true;
            this.rbConductorDay.Text = "جدول روز سیم کشی";
            this.rbConductorDay.UseVisualStyleBackColor = true;
            // 
            // rbSagTension
            // 
            this.rbSagTension.AutoSize = true;
            this.rbSagTension.Location = new System.Drawing.Point(138, 61);
            this.rbSagTension.Name = "rbSagTension";
            this.rbSagTension.Size = new System.Drawing.Size(164, 17);
            this.rbSagTension.TabIndex = 0;
            this.rbSagTension.TabStop = true;
            this.rbSagTension.Text = "جدول محاسبات کشش و فلش";
            this.rbSagTension.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(80, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(49, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "نمایش";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(23, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(51, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmChoiceReportMec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 253);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChoiceReportMec";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "گزارشات";
            this.Load += new System.EventHandler(this.frmChoiceReportMec_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPloe;
        private System.Windows.Forms.RadioButton rbConductorDay;
        private System.Windows.Forms.RadioButton rbSagTension;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdbRusSurface;
        private System.Windows.Forms.RadioButton rdbPowerWithOutHalter;
        private System.Windows.Forms.RadioButton rdbPowerwithHalter;
        private System.Windows.Forms.Button btnCancel;

    }
}