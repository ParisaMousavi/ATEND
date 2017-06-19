using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Report
{
    public partial class frmRemarkReport : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public frmRemarkReport()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nchecking.....\n");
            if (!Atend.Global.Acad.DrawEquips.Dicision.IsHere())
            {
                if (!Atend.Global.Acad.DrawEquips.Dicision.IsThere())
                {
                    System.Diagnostics.Process[] prs = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process pr in prs)
                    {
                        if (pr.ProcessName == "acad")
                        {
                            pr.CloseMainWindow();
                        }
                    }
                }
            }
            InitializeComponent();
        }

        //dsSagAndTension ds = new dsSagAndTension();
        //public void SetDataset(dsSagAndTension Data)
        //{
        //    ds = Data;
        //}
        private void frmRemarkReport_Load(object sender, EventArgs e)
        {
            Atend.Report.dsSagAndTension  dsloadbranch = new Atend.Report.dsSagAndTension();
            Atend.Base.Design.DRemark remark = Atend.Base.Design.DRemark.AccessSelectByCode(1);
            if (remark.Code != -1)
            {
                DataRow drRemark = dsloadbranch.Tables["Remark"].NewRow();
                drRemark["Name"] = remark.Name.ToString();
                //MessageBox.Show(drRemark["Name"].ToString());
                dsloadbranch.Tables["Remark"].Rows.Add(drRemark);


                Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
                DataRow drDesign = dsloadbranch.Tables["Title"].NewRow();
                drDesign["ProjectName"] = designProfile.DesignName;
                drDesign["Designer"] = designProfile.Designer;
                drDesign["Area"] = designProfile.Zone;
                drDesign["Credit"] = designProfile.Validate;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = string.Format("{0}/{1}/{2}", p.GetYear(designProfile.DesignDate).ToString(), p.GetMonth(designProfile.DesignDate).ToString(), p.GetDayOfMonth(designProfile.DesignDate).ToString());

                drDesign["Date"] = _date;

                dsloadbranch.Tables["Title"].Rows.Add(drDesign);


                crRemark02 Remark = new crRemark02();
                Remark.SetDataSource(dsloadbranch);
                crViewerRemark.ReportSource = Remark;
            }
            else
            {
                ed.WriteMessage("در Remark مقداری وجود ندارد\n"); 
            }
        }
    }
}