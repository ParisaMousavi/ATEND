namespace Atend.Report
{
    partial class frmNodeReport
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
            this.crViewNodeReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewNodeReport
            // 
            this.crViewNodeReport.ActiveViewIndex = -1;
            this.crViewNodeReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewNodeReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewNodeReport.Location = new System.Drawing.Point(0, 0);
            this.crViewNodeReport.Name = "crViewNodeReport";
            this.crViewNodeReport.SelectionFormula = "";
            this.crViewNodeReport.Size = new System.Drawing.Size(717, 437);
            this.crViewNodeReport.TabIndex = 0;
            this.crViewNodeReport.ViewTimeSelectionFormula = "";
            this.crViewNodeReport.Load += new System.EventHandler(this.crViewNodeReport_Load);

            this.crViewNodeReport.DisplayGroupTree = false;
            this.crViewNodeReport.ShowGroupTreeButton = false;
            // 
            // frmNodeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 437);
            this.Controls.Add(this.crViewNodeReport);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmNodeReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "گزارش گره ها";
            this.Load += new System.EventHandler(this.crViewNodeReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewNodeReport;
    }
}