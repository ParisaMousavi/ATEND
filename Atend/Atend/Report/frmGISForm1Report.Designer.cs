namespace Atend.Report
{
    partial class frmGISForm1Report
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
            this.crViewGISForm1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewGISForm1
            // 
            this.crViewGISForm1.ActiveViewIndex = -1;
            this.crViewGISForm1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewGISForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewGISForm1.Location = new System.Drawing.Point(0, 0);
            this.crViewGISForm1.Name = "crViewGISForm1";
            this.crViewGISForm1.SelectionFormula = "";
            this.crViewGISForm1.Size = new System.Drawing.Size(895, 697);
            this.crViewGISForm1.TabIndex = 0;
            this.crViewGISForm1.ViewTimeSelectionFormula = "";
            this.crViewGISForm1.Load += new System.EventHandler(this.crViewGISForm1_Load);
            // 
            // frmGISForm1Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 697);
            this.Controls.Add(this.crViewGISForm1);
            this.Name = "frmGISForm1Report";
            this.Text = "frmGISForm1Report";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewGISForm1;
    }
}