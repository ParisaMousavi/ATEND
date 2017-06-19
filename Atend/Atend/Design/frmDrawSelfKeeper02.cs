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
    public partial class frmDrawSelfKeeper02 : Form
    {
        DataTable dtMerge = new DataTable();
        DataColumn ConductorMaterialName = new DataColumn("Name", typeof(string));
        DataColumn ConductorMaterialCode = new DataColumn("Code", typeof(int));
        DataTable condMaterialTbl = new DataTable();
        bool AllowClose = true;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers = new List<Atend.Base.Equipment.ESelfKeeper>();
            Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeeperTip = new Atend.Base.Equipment.ESelfKeeperTip();
            Atend.Base.Acad.AcadGlobal.SelfKeeperData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.SelfKeeperData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.SelfKeeperData.UseAccess = false;
        }

        public frmDrawSelfKeeper02()
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
            //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.ProductCode = 0;
            condMaterialTbl.Columns.Add(ConductorMaterialCode);
            condMaterialTbl.Columns.Add(ConductorMaterialName);

            DataRow drCond1 = condMaterialTbl.NewRow();
            drCond1["Name"] = "مس(CU)";
            drCond1["Code"] = 1;

            DataRow drCond2 = condMaterialTbl.NewRow();
            drCond2["Name"] = "آلومینیوم(AAC)";
            drCond2["Code"] = 2;

            DataRow drCond3 = condMaterialTbl.NewRow();
            drCond3["Name"] = "الومینیوم مغز فولاد(ACSR)";
            drCond3["Code"] = 3;

            DataRow drCond4 = condMaterialTbl.NewRow();
            drCond3["Name"] = "آلیاژ آلومینیوم";
            drCond3["Code"] = 4;

            condMaterialTbl.Rows.Clear();
            condMaterialTbl.Rows.Add(drCond1);
            condMaterialTbl.Rows.Add(drCond2);
            condMaterialTbl.Rows.Add(drCond3);
            condMaterialTbl.Rows.Add(drCond4);

        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            //if (Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode == 0)
            //{
            //    MessageBox.Show("لطفا کابل خودنگهدار مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

            if (gvSelfKeeper.Rows.Count > 0 && gvSelfKeeper.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;

        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = condMaterialTbl;
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvSelfKeeper.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvSelfKeeper.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvSelfKeeper.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //Confirmed
        private void SetDefaultValues()
        {
            if (Atend.Control.ConnectionString.StatusDef)
            {
                if (Atend.Control.Common.IsExist != -1)
                    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
                if (Atend.Control.Common.ProjectCode != -1)
                    cboProjCode.SelectedValue = Atend.Control.Common.ProjectCode;
            }
        }

        //Confirmed
        private void BindDataToGridMain()
        {
            dtMerge = Atend.Base.Equipment.ESelfKeeperTip.SelectAllAndMerge();
            DataColumn dcMaterialName = new DataColumn("MaterialName");
            DataColumn dcAMP = new DataColumn("AMP");
            dtMerge.Columns.Add("AMP");
            dtMerge.Columns.Add(dcMaterialName);

            foreach (DataRow dr in dtMerge.Rows)
            {
                dr["AMP"] = string.Format("{0}+{1}j", dr["Resistance"].ToString(), dr["Reactance"].ToString());
                if (Convert.ToInt32(dr["MaterialConductorCode"].ToString()) == 1)
                    dr["MaterialName"] = "مسی";
                if (Convert.ToInt32(dr["MaterialConductorCode"].ToString()) == 2)
                    dr["MaterialName"] = "آلومینیوم";
                if (Convert.ToInt32(dr["MaterialConductorCode"].ToString()) == 3)
                    dr["MaterialName"] = "آلومینیو مغز فولاد";
                if (Convert.ToInt32(dr["MaterialConductorCode"].ToString()) == 4)
                    dr["MaterialName"] = "آلیاژ آلومینیوم";
            }

            gvSelfKeeper.AutoGenerateColumns = false;
            gvSelfKeeper.DataSource = dtMerge;
            ChangeColor();
        }

        private void frmDrawSelfKeeper_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();

            BindDataToGridMain();
            BindMaterialToComboBox();
            BindDataToComboBoxIsExist();
           
            //BindDataTocboProjCode();
            SetDefaultValues();
        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        //confirmed
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
                //cboIsExist.SelectedIndex = i;
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            } 
            //    }
            //}
            //else
            //{
            //    cboIsExist.SelectedValue = Atend.Control.Common.IsExist;
            //}

            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedIndex = 4;
        }

        public void BindDataToSelfKeeperTip()
        {
            //Atend.Base.Equipment.ESelfKeeperTip
            double CrossSectionArea = -1;
            int MaterailCode = -1;
            bool check = false;
            string strFilter = "";

            if (chkMaterail.Checked)
            {
                MaterailCode = Convert.ToInt32(cboMaterial.SelectedValue);
                strFilter = " MaterialConductorCode='" + MaterailCode + "'";
                check = true;
            }
            if (chkSectionArea.Checked)
            {
                CrossSectionArea = Convert.ToDouble(nudCrossSectionArea.Value);
                if (strFilter != "")
                    strFilter += " AND CrossSectionArea='" + CrossSectionArea + "'";
                else
                {
                    strFilter = " CrossSectionArea='" + CrossSectionArea + "'";
                }
                check = true;
            }

            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvSelfKeeper.AutoGenerateColumns = false;
                gvSelfKeeper.DataSource = dv;
            }
            else
            {
                gvSelfKeeper.AutoGenerateColumns = false;
                gvSelfKeeper.DataSource = dtMerge;
            }
            ChangeColor();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToSelfKeeperTip();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmDrawSelfKeeper_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmSelfKeeperTip02 SKTip = new Atend.Equipment.frmSelfKeeperTip02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(SKTip);
            BindDataToSelfKeeperTip();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {

                if (Convert.ToBoolean(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells["IsSql"].Value) == false)//Access
                {
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.UseAccess = true;
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(
                        Convert.ToInt32(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value));

                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeeperTip = SelfKeeperTip;

                    Atend.Base.Equipment.ESelfKeeper SelfPhase = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(
                       SelfKeeperTip.PhaseProductCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfPhase);

                    Atend.Base.Equipment.ESelfKeeper SelfNeutral = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(
                      SelfKeeperTip.NeutralProductCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfNeutral);

                    Atend.Base.Equipment.ESelfKeeper SelfNight = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(
                       SelfKeeperTip.NightProductCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfNight);
                }
                else
                {
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.UseAccess = false;
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(
                        new Guid(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[1].Value.ToString()));

                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeeperTip = SelfKeeperTip;

                    Atend.Base.Equipment.ESelfKeeper SelfPhase = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(
                       SelfKeeperTip.PhaseProductxCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfPhase);

                    Atend.Base.Equipment.ESelfKeeper SelfNeutral = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(
                      SelfKeeperTip.NeutralProductxCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfNeutral);

                    Atend.Base.Equipment.ESelfKeeper SelfNight = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(
                       SelfKeeperTip.NightProductxCode);
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.eSelfKeepers.Add(SelfNight);

                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.SelfKeeperData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.SelfKeeperData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.Sag = 0;
                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.Order = 0;
                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.DesignCode = Atend.Control.Common.SelectedDesignCode;
                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                //Atend.Base.Acad.AcadGlobal.dBranch.ProductCode = (int)gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value;
                //////Atend.Base.Acad.AcadGlobal.dBranch.XCode = new Guid(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value.ToString());
                //if (chkIsExist.CheckState == CheckState.Unchecked && cboIsExist.SelectedIndex == 0)
                //    //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.IsExist = 0;
                //if (chkIsExist.Checked && cboIsExist.SelectedIndex == 0)
                //    //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.IsExist = 1;
                //if (chkIsExist.Checked && cboIsExist.SelectedIndex == 1)
                //    //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.IsExist = 2;


                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.IsExist = chkIsExist.Checked;
                //Atend.Base.Acad.AcadGlobal.SelfKeeperData.dBranch.Number = Atend.Base.Equipment.ESelfKeeperTip.SelectByCode((int)gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value).Description;

                AllowClose = true;
            }
            else
            {
                AllowClose = false;
            }
        }

        private void gvSelfKeeper_Click(object sender, EventArgs e)
        {
            if (gvSelfKeeper.Rows.Count > 0)
            {
                //ed.WriteMessage("Tip Code {0} \n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                //Atend.Base.Acad.AcadGlobal.dBranch.ProductCode = Convert.ToInt32(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value.ToString());
                //////Atend.Base.Acad.AcadGlobal.dBranch.XCode = new Guid(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value.ToString());
            }
        }

        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
                cboProjCode.DisplayMember = "Name";
                cboProjCode.ValueMember = "ACode";
                cboProjCode.DataSource = dtWorkOrder;

            }
        }



    }
}