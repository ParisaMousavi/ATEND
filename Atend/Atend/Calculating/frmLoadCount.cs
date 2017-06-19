using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Calculating
{
    public partial class frmLoadCount : Form
    {
        bool ForceToClose = false;
        public frmLoadCount()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nchecking.....\n");
            if (!Atend.Global.Acad.DrawEquips.Dicision.IsHere())
            {
                if (!Atend.Global.Acad.DrawEquips.Dicision.IsThere())
                {
                    //System.Diagnostics.Process[] prs = System.Diagnostics.Process.GetProcesses();
                    //foreach (System.Diagnostics.Process pr in prs)
                    //{
                    //    if (pr.ProcessName == "acad")
                    //    {
                    //        pr.CloseMainWindow();
                    //    }
                    //}
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "شناسایی قفل";
                    notification.Msg = "لطفا وضعیت قفل را بررسی نمایید ";
                    notification.infoCenterBalloon();

                    ForceToClose = true;

                }
            }

            InitializeComponent();
        }

        public int Count = 0;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Count = Convert.ToInt32(txtCount.Text);
                this.Close();
            }
                
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtCount.Text))
            {
                MessageBox.Show("لطفاً تعداد را وارد کنید");
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtCount.Text))
            {
                MessageBox.Show("لطفاً تعداد را با فرمت مناسب وارد کنید");
                return false;
            }

            return true;
        }

        private void frmLoadCount_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

            Count = 0;
        }

        private void txtCount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                if (Validation())
                {
                    Count = Convert.ToInt32(txtCount.Text);
                    this.Close();
                }
        }

        
    }
}