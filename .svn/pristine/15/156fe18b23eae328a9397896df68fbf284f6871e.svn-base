using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atend.UControls.Design
{
    public partial class frmWordRemarkSearch : Form
    {

        frmWordRemarks frmRemarks = null;
        frmWordRemark frmRemark = null;
        bool ForceToClose = false;

        public frmWordRemarkSearch(frmWordRemarks Frm)
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\nchecking.....\n");
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
            frmRemarks = new frmWordRemarks();
            frmRemarks = Frm;
        }

        public frmWordRemarkSearch(frmWordRemark Frm)
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\nchecking.....\n");
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
            frmRemark = new frmWordRemark();
            frmRemark = Frm;
        }

        private void Search()
        {
            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = Atend.Base.Design.DRemark.Search(tstName.Text);
        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            if (gvProduct.Rows.Count > 0)
            {
                //Atend.Control.Common.flag = true;
                if (frmRemark != null)
                {
                    frmRemark.BindDataToOwnControl(Convert.ToInt16(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString()));
                    Close();
                }
                else if (frmRemarks != null)
                {
                    frmRemarks.BindDataToOwnControl(Convert.ToInt16(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString()));
                    Close();
                }
            }
        }

        private void tsbSearch_Click_1(object sender, EventArgs e)
        {
            Search();

        }


        private void frmWordRemarkSearch_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Search();
        }
    }
}