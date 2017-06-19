namespace Atend.Calculating
{
    partial class frmChoiceForce
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
            this.txtSaftyFactorOburi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSaftyFactor = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.rdbWithoutHalter = new System.Windows.Forms.RadioButton();
            this.rdbHalter = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSaftyFactorOburi);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSaftyFactor);
            this.groupBox1.Controls.Add(this.btnContinue);
            this.groupBox1.Controls.Add(this.rdbWithoutHalter);
            this.groupBox1.Controls.Add(this.rdbHalter);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "پیشنهاد قدرت پایه";
            // 
            // txtSaftyFactorOburi
            // 
            this.txtSaftyFactorOburi.Location = new System.Drawing.Point(6, 90);
            this.txtSaftyFactorOburi.Name = "txtSaftyFactorOburi";
            this.txtSaftyFactorOburi.Size = new System.Drawing.Size(119, 21);
            this.txtSaftyFactorOburi.TabIndex = 19;
            this.txtSaftyFactorOburi.Text = "1.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "ضریب اطمینان جهت قدرت پایه عبوری:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(6, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "ضریب اطمینان جهت قدرت پایه انتهایی:";
            // 
            // txtSaftyFactor
            // 
            this.txtSaftyFactor.Location = new System.Drawing.Point(6, 63);
            this.txtSaftyFactor.Name = "txtSaftyFactor";
            this.txtSaftyFactor.Size = new System.Drawing.Size(119, 21);
            this.txtSaftyFactor.TabIndex = 15;
            this.txtSaftyFactor.Text = "2.5";
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(69, 116);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(57, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "ادامه";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // rdbWithoutHalter
            // 
            this.rdbWithoutHalter.AutoSize = true;
            this.rdbWithoutHalter.Checked = true;
            this.rdbWithoutHalter.Location = new System.Drawing.Point(19, 40);
            this.rdbWithoutHalter.Name = "rdbWithoutHalter";
            this.rdbWithoutHalter.Size = new System.Drawing.Size(68, 17);
            this.rdbWithoutHalter.TabIndex = 1;
            this.rdbWithoutHalter.TabStop = true;
            this.rdbWithoutHalter.Text = "بدون مهار";
            this.rdbWithoutHalter.UseVisualStyleBackColor = true;
            // 
            // rdbHalter
            // 
            this.rdbHalter.AutoSize = true;
            this.rdbHalter.Location = new System.Drawing.Point(235, 40);
            this.rdbHalter.Name = "rdbHalter";
            this.rdbHalter.Size = new System.Drawing.Size(53, 17);
            this.rdbHalter.TabIndex = 0;
            this.rdbHalter.Text = "با مهار";
            this.rdbHalter.UseVisualStyleBackColor = true;
            this.rdbHalter.CheckedChanged += new System.EventHandler(this.rdbHalter_CheckedChanged);
            // 
            // frmChoiceForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 169);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChoiceForce";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "پیشنهاد قدرت پایه";
            this.Load += new System.EventHandler(this.frmChoiceForce_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbWithoutHalter;
        private System.Windows.Forms.RadioButton rdbHalter;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtSaftyFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSaftyFactorOburi;
        private System.Windows.Forms.Label label1;
    }
}