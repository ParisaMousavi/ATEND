namespace Atend.Report
{
    partial class frmGISSelectReport
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
            this.rbtnForm1 = new System.Windows.Forms.RadioButton();
            this.rbtnForm2 = new System.Windows.Forms.RadioButton();
            this.rbtnForm3 = new System.Windows.Forms.RadioButton();
            this.rbtnForm4 = new System.Windows.Forms.RadioButton();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowReport);
            this.groupBox1.Controls.Add(this.rbtnForm4);
            this.groupBox1.Controls.Add(this.rbtnForm3);
            this.groupBox1.Controls.Add(this.rbtnForm2);
            this.groupBox1.Controls.Add(this.rbtnForm1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "انتخاب گزارش";
            // 
            // rbtnForm1
            // 
            this.rbtnForm1.AutoSize = true;
            this.rbtnForm1.Location = new System.Drawing.Point(98, 36);
            this.rbtnForm1.Name = "rbtnForm1";
            this.rbtnForm1.Size = new System.Drawing.Size(393, 17);
            this.rbtnForm1.TabIndex = 0;
            this.rbtnForm1.TabStop = true;
            this.rbtnForm1.Text = "فرم 1 : برداشت اطلاعات فیدر فشار متوسط هوایی(در مرحله جمع آوری اطلاعات)";
            this.rbtnForm1.UseVisualStyleBackColor = true;
            // 
            // rbtnForm2
            // 
            this.rbtnForm2.AutoSize = true;
            this.rbtnForm2.Location = new System.Drawing.Point(74, 59);
            this.rbtnForm2.Name = "rbtnForm2";
            this.rbtnForm2.Size = new System.Drawing.Size(417, 17);
            this.rbtnForm2.TabIndex = 1;
            this.rbtnForm2.TabStop = true;
            this.rbtnForm2.Text = "فرم 2 : برداشت اطلاعات تکیه فیدر فشار متوسط زمینی (در مرحله جمع آوری اطلاعات)";
            this.rbtnForm2.UseVisualStyleBackColor = true;
            // 
            // rbtnForm3
            // 
            this.rbtnForm3.AutoSize = true;
            this.rbtnForm3.Location = new System.Drawing.Point(268, 82);
            this.rbtnForm3.Name = "rbtnForm3";
            this.rbtnForm3.Size = new System.Drawing.Size(223, 17);
            this.rbtnForm3.TabIndex = 2;
            this.rbtnForm3.TabStop = true;
            this.rbtnForm3.Text = "فرم 3 : برداشت اطلاعات پست توزیع هوایی";
            this.rbtnForm3.UseVisualStyleBackColor = true;
            // 
            // rbtnForm4
            // 
            this.rbtnForm4.AutoSize = true;
            this.rbtnForm4.Location = new System.Drawing.Point(268, 105);
            this.rbtnForm4.Name = "rbtnForm4";
            this.rbtnForm4.Size = new System.Drawing.Size(222, 17);
            this.rbtnForm4.TabIndex = 3;
            this.rbtnForm4.TabStop = true;
            this.rbtnForm4.Text = "فرم 4 : برداشت اطلاعات پست توزیع زمینی";
            this.rbtnForm4.UseVisualStyleBackColor = true;
            // 
            // btnShowReport
            // 
            this.btnShowReport.Location = new System.Drawing.Point(6, 164);
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.Size = new System.Drawing.Size(65, 23);
            this.btnShowReport.TabIndex = 4;
            this.btnShowReport.Text = "تایید";
            this.btnShowReport.UseVisualStyleBackColor = true;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // frmGISSelectReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 217);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmGISSelectReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "گزارشات GIS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnForm4;
        private System.Windows.Forms.RadioButton rbtnForm3;
        private System.Windows.Forms.RadioButton rbtnForm2;
        private System.Windows.Forms.RadioButton rbtnForm1;
        private System.Windows.Forms.Button btnShowReport;
    }
}