namespace Atend.Report
{
    partial class frmCrossSectionReport
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
            this.crViewerCrossSection = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewerCrossSection
            // 
            this.crViewerCrossSection.ActiveViewIndex = -1;
            this.crViewerCrossSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewerCrossSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewerCrossSection.Location = new System.Drawing.Point(0, 0);
            this.crViewerCrossSection.Name = "crViewerCrossSection";
            this.crViewerCrossSection.SelectionFormula = "";
            this.crViewerCrossSection.Size = new System.Drawing.Size(748, 450);
            this.crViewerCrossSection.TabIndex = 0;
            this.crViewerCrossSection.ViewTimeSelectionFormula = "";

            this.crViewerCrossSection.DisplayGroupTree = false;
            this.crViewerCrossSection.ShowGroupTreeButton = false;
            // 
            // frmCrossSectionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 450);
            this.Controls.Add(this.crViewerCrossSection);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCrossSectionReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "сьм Цчьз";
            this.Load += new System.EventHandler(this.frmCrossSectionReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewerCrossSection;
    }
}