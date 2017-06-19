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
    public partial class frmEditDrawGroundCable : Form
    {
        public Guid BranchCode;
        public int GCCode;
        bool AllowClose = false;
        bool ForceToClose = false;

        public frmEditDrawGroundCable()
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

        private void BindMaterialToComboBox()
        {
            //cboMaterial.ValueMember = "Code";
            //cboMaterial.DisplayMember = "Name";
            //cboMaterial.DataSource = Atend.Base.Equipment.ECabelType.SelectAll();

        }

        private void frmEditDrawGroundCable_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToCounductorTip();


            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (int.Parse(gvConductor.Rows[i].Cells[0].Value.ToString()) == GCCode)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindMaterialToComboBox();
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

        }

        public void BindDataToCounductorTip()
        {
            float CrossSectionArea = -1;
            int TypeCode = -1;
            int Voltage = -1;
            if (chkMaterail.Checked)
                TypeCode = Convert.ToInt32(cboMaterial.SelectedValue);
            if (chkSectionArea.Checked)
                CrossSectionArea = Convert.ToSingle(nudCrossSectionArea.Value);
           // ed.WriteMessage("aaa\n");

            //DataTable dt = Atend.Base.Equipment.EGroundCabel.DrawSearch(CrossSectionArea, TypeCode , Voltage);

            gvConductor.AutoGenerateColumns = false;
            //gvConductor.DataSource = dt;
            //gvConductor.DataSource = Atend.Base.Equipment.EConductor.DrawSearch(CrossSectionArea, MaterailCode);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToCounductorTip();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (gvConductor.SelectedRows.Count > 0)
            {
                Atend.Base.Equipment.EGroundCabel GC = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(GCCode);

                Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchCode);// SelectByCode(BranchCode);

                Branch.ProductCode = Convert.ToInt32(gvConductor.SelectedRows[0].Cells[0].Value.ToString());
                //if (Branch.AccessUpdate())
                //{
                //    //ed.WriteMessage("OK");
                //    //Atend.Base.Acad.AT_INFO
                //    GCCode = Convert.ToInt32(gvConductor.SelectedRows[0].Cells[0].Value.ToString());
                //    AllowClose = true;
                //    this.Close();
                //}
                //else
                //    MessageBox.Show("انجام ويرايش امكانپذير نيست");
            }
        }

        private void frmEditDrawGroundCable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }               
    }
}