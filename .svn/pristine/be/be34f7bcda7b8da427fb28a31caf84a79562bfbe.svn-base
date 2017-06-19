using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atend.Report
{
    public partial class frmGISSelectReport : Form
    {
        public bool IsPDF ;

        public frmGISSelectReport()
        {
            InitializeComponent();
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            if(rbtnForm1.Checked )
            {
                if (!IsPDF)
                {
                    Atend.Global.Utility.UOtherOutPut rep = new Atend.Global.Utility.UOtherOutPut();
                    Atend.Report.dsSagAndTension ds = rep.FillGisForm1();
                    Atend.Global.Utility.UReport.CreateExcelReaportForGISForm1("فرم1 GIS.xls", ds.Tables["GISForm1"]);
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "GIS گزارش";
                    notification.Msg = "گزارش فرم 1 تولید شد";
                    notification.infoCenterBalloon();
                }
                else
                {
                    Atend.Global.Utility.UOtherOutPut rep = new Atend.Global.Utility.UOtherOutPut();
                    Atend.Report.frmGISForm1Report Report = new frmGISForm1Report();
                    Report.SetDataset(rep.FillGisForm1());
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                }

                
            }
        }
    }
}