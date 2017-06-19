using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmUserAccessibility : Form
    {
        bool ForceToClose = false;


        public frmUserAccessibility()
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
        }

        private void frmUserAccessibility_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            lblFamily.Text = string.Empty;
            lblName.Text = string.Empty;
            Atend.Base.Design.DUser user = Atend.Base.Design.DUser.SelectByCode(Atend.Control.Common.userCode);
            lblName.Text = user.Name;
            lblFamily.Text = user.Family;
            BindDataToTreeViw();
            BindItemCheckedTOTreeView();
        }

        private void BindDataToTreeViw()
        {
            DataTable Access = Atend.Base.Design.DAccess.SelectAll();
            foreach (DataRow dr in Access.Rows)
            {
                TreeNode node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Tag = dr["Code"].ToString();
                tvAccess.Nodes.Add(node);
            }
        }

        private void BindItemCheckedTOTreeView()
        {
            foreach (DataRow dr in Atend.Control.Common.AccessList.Rows)
            {
                foreach (TreeNode tn in tvAccess.Nodes)
                {
                    if (Convert.ToInt32(dr["IDAccess"]) == Convert.ToInt32(tn.Tag))
                    {
                        tn.Checked = true;
                    }
                }
            }
        }
    }
}