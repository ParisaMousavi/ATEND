using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmUserSearch : Form
    {
        frmUser frmUser;
        bool ForceToClose = false;


        public frmUserSearch(frmUser user)
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
            frmUser = new frmUser();
            frmUser = user;
        }

        private void Search()
        {
            gvUser.AutoGenerateColumns = false;
            gvUser.DataSource = Atend.Base.Design.DUser.SearchX(tstName.Text);
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void tstName_Click(object sender, EventArgs e)
        {

        }

        private void tstName_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void gvUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvUser.Rows.Count > 0)
            {
                frmUser.BindDataToOwnControl(Convert.ToInt32(
                    gvUser.Rows[gvUser.CurrentRow.Index].Cells[0].Value.ToString()));

                Close();
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUserSearch_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }
    }
}