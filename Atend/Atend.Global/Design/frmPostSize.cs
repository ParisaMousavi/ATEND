using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Global.Design
{
    public partial class frmPostSize : Form
    {


        private double tol;
        public double Tol
        {
            get { return tol; }
            set
            {
                if (value <= 0)
                    tol = 1;
                else
                    tol = value;
            }
        }

        private double arz;
        public double Arz
        {
            get { return arz; }
            set
            {
                if (value <= 0)
                    arz = 1;
                else
                    arz = value;
            }
        }

        bool ForceToClose = false;


        public frmPostSize()
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtTol.Text != string.Empty)
            {
                Tol = Convert.ToDouble(txtTol.Text);
            }
            else
            {
                tol = 0;
            }


            if (txtArz.Text != string.Empty)
            {
                Arz = Convert.ToDouble(txtArz.Text);
            }
            else
            {
                Arz = 0;
            }

        }

        private void frmPostSize_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }
    }
}