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
    public partial class frmGroundPostDiagramReport : Form
    {

        public frmGroundPostDiagramReport()
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

        private void crViewDiagramReport_Load(object sender, EventArgs e)
        {
            crGroundPostDiagram Diagram = new crGroundPostDiagram();

            DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost));

            foreach (DataRow PackRow in DPack.Rows)
            {
                Atend.Base.Equipment.EGroundPost Apost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));
                DataRow DR = ds.GroundPostDiagram.NewRow();
                DR["Name"] = Apost.Name;
                DR["Image"] = Apost.Image;
                DR["Capacity"] = Apost.Capacity;
                ds.GroundPostDiagram.Rows.Add(DR);
            }

            Diagram.SetDataSource(ds);
            crViewDiagramReport.ReportSource = Diagram;
        }
    }
}