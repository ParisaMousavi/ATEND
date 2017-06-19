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
    public partial class frmTransCapacityResult : Form
    {
        double ST;
        bool ForceToClose = false;
        double Height, load;
        public frmTransCapacityResult(double _ST, double _Height, double _Load)
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
            ST = _ST;
            Height = _Height;
            load = _Load;
        }


        private void frmTransCapacityResult_Load(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (ForceToClose)
                this.Close();


            txtTransCapacity.Text = ST.ToString();
            DataTable dtTrans = Atend.Base.Equipment.ETransformer.SelectAllX();
            int i=0;

            gvTransCapacity.AutoGenerateColumns = false;
            gvTransCapacity.DataSource = dtTrans;
            Atend.Base.Calculating.CTransformer.AccessDelete();
            Atend.Base.Calculating.CTransformer transformer =new  Atend.Base.Calculating.CTransformer();

            transformer.Result = ST;
            transformer.Load = load;
            transformer.Height=Height;

            foreach (DataRow dr in dtTrans.Rows)
            {
                if (ST < Convert.ToDouble(dr["Capacity"]))
                {
                    gvTransCapacity.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    transformer.Result = Convert.ToDouble(dr["Capacity"]);
                    break;
                }
                i++;

            }

            if (!transformer.AccessInsert())
            {
                ed.WriteMessage("transformer Insert Failed ");
            }
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void انتقالبهفایلExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void انتقالبهفایلEXCELToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();

        }

        private void خروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransferToEXCEL()
        {
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string Name = "گزارش محاسبات الکتریکی " + date + Atend.Control.Common.DesignName + ".xls";
           

            Atend.Global.Utility.UReport.CreateExcelReportFinalElectrical(Name);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}