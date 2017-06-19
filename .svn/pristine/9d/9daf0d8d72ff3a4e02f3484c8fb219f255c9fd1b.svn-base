using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;



namespace Atend.Global.Design
{
    public partial class frmDrawBranchCable : Form
    {
        public double Length;
        public double OperationBulk;
        bool ForceToClose = false;


        public frmDrawBranchCable()
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
            txtLength.Text = Convert.ToString(Length);
        }

        private void frmDrawBranch_Load(object sender, EventArgs e)
        {
            txtLength.Text = Length.ToString();
            btnOk.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtLength.Text == "" || txtOerationBulk.Text == "")
            {
                Length = 0;
                OperationBulk = 0;
            }
            else
            {
                Length = double.Parse(txtLength.Text);
                //change Operation bulk automatically
                OperationBulk = double.Parse(txtOerationBulk.Text);
                //System.Data.DataTable OperationList = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(Atend.Base.Acad.AcadGlobal.GroundCableData.eGroundCabelTip.PhaseProductCode, (int)Atend.Control.Enum.ProductType.GroundCabel);
                //MessageBox.Show(OperationList.Rows.Count.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmDrawBranchCable_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            txtLength.Text = Length.ToString();
            txtOerationBulk.Text = OperationBulk.ToString();
            btnOk.Focus();
        }

    }
}