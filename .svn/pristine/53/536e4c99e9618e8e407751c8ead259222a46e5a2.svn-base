using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base
{
    public partial class frmProjectCodeSearch : Form
    {
        frmProjectCode frmPCode;
        bool ForceToClose = false;
        public frmProjectCodeSearch(frmProjectCode PCode)
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
            frmPCode = new frmProjectCode();
            frmPCode = PCode;
        }

        private void Search()
        {

            gvProduct.AutoGenerateColumns = false;
            gvProduct.DataSource = Atend.Base.Base.BWorkOrder.Search(tstName.Text);
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            if (gvProduct.Rows.Count > 0)
            {
                int Code = Convert.ToInt32(gvProduct.Rows[gvProduct.CurrentRow.Index].Cells[0].Value.ToString());
                frmPCode.BindDataToOwnControl(Code);
                Close();
            }
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

        private void frmProjectCodeSearch_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            Search();
        }
    }
}