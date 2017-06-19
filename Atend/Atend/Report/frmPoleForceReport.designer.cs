namespace Atend.Report
{
    partial class frmPoleForceReport
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
            this.crViewPoleForce = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crViewPoleForce
            // 
            this.crViewPoleForce.ActiveViewIndex = -1;
            this.crViewPoleForce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewPoleForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewPoleForce.Location = new System.Drawing.Point(0, 0);
            this.crViewPoleForce.Name = "crViewPoleForce";
            this.crViewPoleForce.SelectionFormula = "";
            this.crViewPoleForce.Size = new System.Drawing.Size(624, 398);
            this.crViewPoleForce.TabIndex = 0;
            this.crViewPoleForce.ViewTimeSelectionFormula = "";
            this.crViewPoleForce.Load += new System.EventHandler(this.crViewPoleForce_Load);

            this.crViewPoleForce.DisplayGroupTree = false;
            this.crViewPoleForce.ShowGroupTreeButton = false;
            // 
            // frmPoleForceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 398);
            this.Controls.Add(this.crViewPoleForce);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPoleForceReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Text = "نیروهای وارد بر پایه";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewPoleForce;
    }
}