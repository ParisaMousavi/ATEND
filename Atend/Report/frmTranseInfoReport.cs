using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Report
{
    public partial class frmTranseInfoReport : Form
    {
        public frmTranseInfoReport()
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

        dsSagAndTension ds = new dsSagAndTension();

        public void SetDataset(dsSagAndTension Data)
        {
            ds = Data;
        }
            
        private void frmTranseInfoReport_Load(object sender, EventArgs e)
        {
            crTranseInfo ts = new crTranseInfo();
            ts.SetDataSource(ds);
            crViewerTranseInfo.ReportSource = ts;
        }
    }
}