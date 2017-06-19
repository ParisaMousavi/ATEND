using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Report
{
    public partial class frmViewReport : Form
    {
        string[] temp;

        public frmViewReport()
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmViewReport_Load(object sender, EventArgs e)
        {
            string path = Atend.Control.Common.DesignFullAddress;
            path = string.Format(@"{0}\Result", path);
            if (System.IO.Directory.Exists(path))
            {
                temp = System.IO.Directory.GetFileSystemEntries(path);
                for (int i = 0; i < temp.Length; i++)
                {
                    string s = temp[i].ToString();
                    int len = s.Substring(0, s.LastIndexOf(@"Result\")).Length;

                    s = s.Substring(len + 12, s.Length - len - 12);
                    listBox1.Items.Add(s);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            string path = Atend.Control.Common.DesignFullAddress;
            path = string.Format(@"{0}\Result", path);
            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                ed.WriteMessage("فایل موردنظر موجود نمی باشد\n");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (listBox1.SelectedIndex != -1)
            {
                System.Diagnostics.Process.Start(temp[listBox1.SelectedIndex]);
            }
            else
            {
                ed.WriteMessage("لطفا ابتدا گزارش مورد نظرتان را مشخص نمائید.\n");
            }
        }
    }
}