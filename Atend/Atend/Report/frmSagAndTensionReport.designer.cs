namespace Atend.Report
{
    partial class frmSagAndTensionReport
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
            this.crViewerSagAndTension = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crSagAndTension1 = new Atend.Report.crSagAndTension();
            this.SuspendLayout();
            // 
            // crViewerSagAndTension
            // 
            this.crViewerSagAndTension.ActiveViewIndex = -1;
            this.crViewerSagAndTension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewerSagAndTension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewerSagAndTension.Location = new System.Drawing.Point(0, 0);
            this.crViewerSagAndTension.Name = "crViewerSagAndTension";
            this.crViewerSagAndTension.SelectionFormula = "";
            this.crViewerSagAndTension.Size = new System.Drawing.Size(750, 452);
            this.crViewerSagAndTension.TabIndex = 0;
            this.crViewerSagAndTension.ViewTimeSelectionFormula = "";
            this.crViewerSagAndTension.Load += new System.EventHandler(this.crViewerSagAndTension_Load);

            this.crViewerSagAndTension.DisplayGroupTree = false;
            this.crViewerSagAndTension.ShowGroupTreeButton = false;
            // 
            // frmSagAndTensionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 452);
            this.Controls.Add(this.crViewerSagAndTension);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSagAndTensionReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "جدول کشش و فلش";
            this.Load += new System.EventHandler(this.crViewerSagAndTension_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewerSagAndTension;
        private crSagAndTension crSagAndTension1;
        //private crSagAndTension01 crSagAndTension011;
    }
}