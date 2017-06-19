namespace Atend.Report
{
    partial class frmTranseInfoReport
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
            this.crViewerTranseInfo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewerTranseInfo
            // 
            this.crViewerTranseInfo.ActiveViewIndex = -1;
            this.crViewerTranseInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewerTranseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewerTranseInfo.Location = new System.Drawing.Point(0, 0);
            this.crViewerTranseInfo.Name = "crViewerTranseInfo";
            this.crViewerTranseInfo.SelectionFormula = "";
            this.crViewerTranseInfo.Size = new System.Drawing.Size(750, 452);
            this.crViewerTranseInfo.TabIndex = 0;
            this.crViewerTranseInfo.ViewTimeSelectionFormula = "";

            this.crViewerTranseInfo.DisplayGroupTree = false;
            this.crViewerTranseInfo.ShowGroupTreeButton = false;
            // 
            // frmTranseInfoReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 452);
            this.Controls.Add(this.crViewerTranseInfo);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTranseInfoReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "ظرفیت ترانس";
            this.Load += new System.EventHandler(this.frmTranseInfoReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewerTranseInfo;
    }
}