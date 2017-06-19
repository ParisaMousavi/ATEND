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
    public partial class frmNetWorkCrossSearch : Form
    {
        frmNetWorkCross frmnetWorkcross;
        bool ForceToClose = false;
        public frmNetWorkCrossSearch(frmNetWorkCross _frmnetWorkCross)
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
            frmnetWorkcross  = _frmnetWorkCross ;
        }

        private void Search()
        {
            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = Atend.Base.Calculating.CNetWorkCross.AccessSearch(tstName.Text);

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmConductorSearch_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

            Search();
        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            Atend.Control.Common.flag = true;
            frmnetWorkcross.BindDataToOwnControl(Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString()));
            Close();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tstName_Click(object sender, EventArgs e)
        {

        }
    }
}