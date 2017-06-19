using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Design
{
    public partial class frmDrawBranch01 : Form
    {

        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtMaterial = new DataTable();
        DataColumn dcName = new DataColumn("Name", typeof(string));
        DataColumn dcCode = new DataColumn("Code", typeof(int));
        DataTable dtMerge = new DataTable();
        bool ForceToClose = false;


        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.ConductorData.eConductors = new List<Atend.Base.Equipment.EConductor>();
            Atend.Base.Acad.AcadGlobal.ConductorData.eConductorTip = new Atend.Base.Equipment.EConductorTip();
            Atend.Base.Acad.AcadGlobal.ConductorData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.ConductorData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.ConductorData.UseAccess = false;
        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvConductor.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public frmDrawBranch01()
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
            //Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode = 0;
            dtMaterial.Columns.Add(dcName);
            dtMaterial.Columns.Add(dcCode);


            DataRow dr1 = dtMaterial.NewRow();
            dr1["Name"] = "مسی(CU)";
            dr1["Code"] = 0;

            DataRow dr2 = dtMaterial.NewRow();
            dr2["Name"] = "آلومینیوم(AAC)";
            dr2["Code"] = 1;

            DataRow dr3 = dtMaterial.NewRow();
            dr3["Name"] = "الومینیو مغز فولاد(ACSR)";
            dr3["Code"] = 2;

            DataRow dr4 = dtMaterial.NewRow();
            dr4["Name"] = "آلیاژ آلومینیوم(AAAC)";
            dr4["Code"] = 3;

            dtMaterial.Rows.Add(dr1);
            dtMaterial.Rows.Add(dr2);
            dtMaterial.Rows.Add(dr3);
            dtMaterial.Rows.Add(dr4);

        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboIsExist.Text))
            //{
            //    MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید","خطا");
            //    return false;
            //}
            //if (Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode == 0)
            //{
            //    MessageBox.Show("لطفا سیم مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

            return true;

        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = dtMaterial;
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EConductorTip.SelectAllAndMerge();
            DataColumn dcAMP = new DataColumn("AMP");
            DataColumn dcMaterialName = new DataColumn("MaterialName");
            dtMerge.Columns.Add(dcMaterialName);
            dtMerge.Columns.Add(dcAMP);

            foreach (DataRow dr in dtMerge.Rows)
            {
                dr["AMP"] = string.Format("{0}+{1}j", dr["Resistance"].ToString(), dr["Reactance"].ToString());
                if (Convert.ToInt32(dr["TypeCode"].ToString()) == 0)
                    dr["MaterialName"] = "مسی";
                if (Convert.ToInt32(dr["TypeCode"].ToString()) == 1)
                    dr["MaterialName"] = "آلومینیوم";
                if (Convert.ToInt32(dr["TypeCode"].ToString()) == 2)
                    dr["MaterialName"] = "آلومینیو مغز فولاد";
                if (Convert.ToInt32(dr["TypeCode"].ToString()) == 3)
                    dr["MaterialName"] = "آلیاژ آلومینیوم";
            }


            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;
            ChangeColor();

        }

        //Confirmed
        private void SetDefaultValues()
        {
            if (Atend.Control.ConnectionString.StatusDef)
            {
                if (Atend.Control.Common.IsExist != -1)
                    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
                if (Atend.Control.Common.ProjectCode != -1)
                    cboProjectCode.SelectedValue = Atend.Control.Common.ProjectCode;
            }
        }


        private void frmDrawBranch01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindMaterialToComboBox();
            BindDataToComboBoxIsExist();
            SetDefaultValues();

        }

        //Confirmed
        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist.DataSource = dtstatus;

            //if (Atend.Control.Common.IsExist == -1)
            //{
            DataRow[] drs = dtstatus.Select("IsDefault=True");
            if (drs.Length > 0)
            {
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
            //}
            //else
            //{
            //    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            //}
        }

        public void BindDataToCounductorTip()
        {
            double CrossSectionArea = -1;
            int MaterailCode = -1;
            bool check = false;
            string strFilter = "";
            if (chkMaterail.Checked)
            {
                MaterailCode = Convert.ToInt32(cboMaterial.SelectedValue);
                strFilter = "TypeCode ='" + MaterailCode + "'";
                check = true;
            }
            if (chkSectionArea.Checked)
            {
                CrossSectionArea = Convert.ToDouble(nudCrossSectionArea.Value);
                if (strFilter != "")
                {
                    strFilter += " AND CrossSectionArea='" + CrossSectionArea + "'";
                }
                else
                {
                    strFilter = "CrossSectionArea='" + CrossSectionArea + "'";
                }
                check = true;
            }

            ed.WriteMessage("TypeCode={0},Cross={1},strFilter={2}\n", MaterailCode, CrossSectionArea, strFilter);
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dv;
            }
            else
            {
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dtMerge;
            }
            ChangeColor();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            BindDataToCounductorTip();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Clear();
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[8].Value) == false)//Access
                {
                    Atend.Base.Acad.AcadGlobal.ConductorData.UseAccess = true;
                    //ed.WriteMessage("Access Grid CondCode={0}\n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                    Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value));
                    //ed.WriteMessage("Access CondTip.Code={0}\n",CondTip.Code);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductorTip = CondTip;

                    Atend.Base.Equipment.EConductor CondPhase = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.PhaseProductCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondPhase);

                    Atend.Base.Equipment.EConductor CondNeutral = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.NeutralProductCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondNeutral);

                    Atend.Base.Equipment.EConductor CondNight = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.NightProductCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondNight);

                }
                else
                {
                    Atend.Base.Acad.AcadGlobal.ConductorData.UseAccess = false;
                    //ed.WriteMessage("Local Grid CondCode={0}\n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString());
                    Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(
                        new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                    //ed.WriteMessage("Local CondTip.Name={0}\n",CondTip.Name);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductorTip = CondTip;

                    Atend.Base.Equipment.EConductor CondPhase = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip.PhaseProductXCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondPhase);

                    Atend.Base.Equipment.EConductor CondNeutral = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip.NeutralProductXCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondNeutral);

                    Atend.Base.Equipment.EConductor CondNight = Atend.Base.Equipment.EConductor.SelectByXCode(CondTip.NightProductXCode);
                    Atend.Base.Acad.AcadGlobal.ConductorData.eConductors.Add(CondNight);

                }
                //ed.WriteMessage("*****ProjectCode={0}\n",Atend.Base.Acad.AcadGlobal.ConductorData.ProjectCode);

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.ConductorData.Existance = status.ACode;

                if (cboProjectCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.ConductorData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.ConductorData.ProjectCode = Convert.ToInt32(cboProjectCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjectCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjectCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowClose = true;
            }
            else
            {
                AllowClose = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmDrawBranch01_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void gvConductor_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmConductorTip02 conductorTip =
                new Atend.Equipment.frmConductorTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(conductorTip);
            BindDataToCounductorTip();

        }

        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
                cboProjectCode.DisplayMember = "Name";
                cboProjectCode.ValueMember = "ACode";
                cboProjectCode.DataSource = dtWorkOrder;
            }
        }

    }
}