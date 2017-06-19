namespace Atend.Report
{
    partial class frmRudSurfaceReport
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
            this.crViewRudSurface = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewRudSurface
            // 
            this.crViewRudSurface.ActiveViewIndex = -1;
            this.crViewRudSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewRudSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewRudSurface.Location = new System.Drawing.Point(0, 0);
            this.crViewRudSurface.Name = "crViewRudSurface";
            this.crViewRudSurface.SelectionFormula = "";
            this.crViewRudSurface.Size = new System.Drawing.Size(802, 578);
            this.crViewRudSurface.TabIndex = 0;
            this.crViewRudSurface.ViewTimeSelectionFormula = "";

            this.crViewRudSurface.DisplayGroupTree = false;
            this.crViewRudSurface.ShowGroupTreeButton = false;
            // 
            // frmRudSurfaceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 578);
            this.Controls.Add(this.crViewRudSurface);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "frmRudSurfaceReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "ê“«—‘ ”ÿÊÕ ‰«Â„Ê«—";
            this.Load += new System.EventHandler(this.crViewNodeReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewRudSurface;
    }
}