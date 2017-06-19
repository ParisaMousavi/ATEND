namespace Atend.Report
{
    partial class frmChoiceReportElec
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
            this.rdbTrans = new System.Windows.Forms.RadioButton();
            this.btnShow = new System.Windows.Forms.Button();
            this.rdbLoadNode = new System.Windows.Forms.RadioButton();
            this.rdbBranch = new System.Windows.Forms.RadioButton();
            this.rdbCrossSection = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbTrans);
            this.groupBox1.Controls.Add(this.btnShow);
            this.groupBox1.Controls.Add(this.rdbLoadNode);
            this.groupBox1.Controls.Add(this.rdbBranch);
            this.groupBox1.Controls.Add(this.rdbCrossSection);
            this.groupBox1.Location = new System.Drawing.Point(24, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "گزارشات";
            // 
            // rdbTrans
            // 
            this.rdbTrans.AutoSize = true;
            this.rdbTrans.Location = new System.Drawing.Point(76, 112);
            this.rdbTrans.Name = "rdbTrans";
            this.rdbTrans.Size = new System.Drawing.Size(148, 17);
            this.rdbTrans.TabIndex = 3;
            this.rdbTrans.TabStop = true;
            this.rdbTrans.Text = "گزارش تعیین ظرفیت ترانس";
            this.rdbTrans.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(6, 139);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "نمایش";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // rdbLoadNode
            // 
            this.rdbLoadNode.AutoSize = true;
            this.rdbLoadNode.Location = new System.Drawing.Point(137, 25);
            this.rdbLoadNode.Name = "rdbLoadNode";
            this.rdbLoadNode.Size = new System.Drawing.Size(87, 17);
            this.rdbLoadNode.TabIndex = 2;
            this.rdbLoadNode.TabStop = true;
            this.rdbLoadNode.Text = "گزارش گره ها";
            this.rdbLoadNode.UseVisualStyleBackColor = true;
            // 
            // rdbBranch
            // 
            this.rdbBranch.AutoSize = true;
            this.rdbBranch.Location = new System.Drawing.Point(125, 53);
            this.rdbBranch.Name = "rdbBranch";
            this.rdbBranch.Size = new System.Drawing.Size(99, 17);
            this.rdbBranch.TabIndex = 1;
            this.rdbBranch.TabStop = true;
            this.rdbBranch.Text = "گزارش شاخه ها";
            this.rdbBranch.UseVisualStyleBackColor = true;
            // 
            // rdbCrossSection
            // 
            this.rdbCrossSection.AutoSize = true;
            this.rdbCrossSection.Location = new System.Drawing.Point(110, 82);
            this.rdbCrossSection.Name = "rdbCrossSection";
            this.rdbCrossSection.Size = new System.Drawing.Size(114, 17);
            this.rdbCrossSection.TabIndex = 0;
            this.rdbCrossSection.TabStop = true;
            this.rdbCrossSection.Text = "گزارش سطح مقطع";
            this.rdbCrossSection.UseVisualStyleBackColor = true;
            // 
            // frmChoiceReportElec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 207);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChoiceReportElec";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "گزارشات";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbLoadNode;
        private System.Windows.Forms.RadioButton rdbBranch;
        private System.Windows.Forms.RadioButton rdbCrossSection;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.RadioButton rdbTrans;
    }
}