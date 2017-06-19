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
    public partial class frmPoleForceReport : Form
    {
        public frmPoleForceReport()
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

        private void crViewPoleForce_Load(object sender, EventArgs e)
        {
            crPoleForce a = new crPoleForce();
            a.SetDataSource(ds);
            crViewPoleForce.ReportSource = a;
            System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            System.IO.FileStream f = new System.IO.FileStream("C:\\aaaa.pdf", System.IO.FileMode.Create);
            byte []reader = new byte[b.Length];
            b.Read(reader, 0, Convert.ToInt32(b.Length));
            f.Write(reader, 0, reader.Length);
            f.Close();
            
        }

    }
}