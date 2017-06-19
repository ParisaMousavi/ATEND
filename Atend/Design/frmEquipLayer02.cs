using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Design
{
    public partial class frmEquipLayer02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        public frmEquipLayer02()
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

        private void BindDataToGrid()
        {

        }

        private void frmEquipLayer02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            gvEquip.AutoGenerateColumns = false;
            DataTable dt = Atend.Base.Design.DProductProperties.AccessSelectAllDrawable();
            foreach (DataRow dr in dt.Rows)
            {
                dr["Scale"] = (1 * Convert.ToDouble(dr["Scale"])) / 0.025;
                dr["CommentScale"] = (1 * Convert.ToDouble(dr["CommentScale"])) / 0.025;
            }
            gvEquip.DataSource = dt;


        }

        private void Save()
        {
            toolStrip1.Focus();
            DataTable dt = (DataTable)gvEquip.DataSource;
            //ed.WriteMessage("row count : {0} \n",dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                Atend.Base.Design.DProductProperties dpp = Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["Code"]));
                dpp.Scale = (Convert.ToDouble(dr["Scale"]) * 0.025) / 1;
                dpp.CommentScale = (Convert.ToDouble(dr["CommentScale"]) * 0.025) / 1;
                dpp.AccessUpdate();
            }

        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }




    }
}