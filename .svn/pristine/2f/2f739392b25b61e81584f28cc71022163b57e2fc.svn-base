using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawSelfKeeper : Form
    {
        public Guid BranchCode;
        public ObjectId BranchObj;
        public int SKTCode;
        bool AllowClose;
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        System.Data.DataColumn ConductorMaterialName = new System.Data.DataColumn("Name", typeof(string));
        System.Data.DataColumn ConductorMaterialCode = new System.Data.DataColumn("Code", typeof(int));
        System.Data.DataTable condMaterialTbl = new System.Data.DataTable();
        int selectedProductCode = -1;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;
        public double Length;

        public frmEditDrawSelfKeeper()
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
            if (gvSelfKeeper.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کابل خودنگهدار مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }

            Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchCode);

            Atend.Base.Design.DBranch _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(Branch.RightNodeCode, Branch.LeftNodeCode);
            if (_DBranch.Code != Guid.Empty)
            {
                if (Convert.ToDouble(txtLenght.Text) != _DBranch.Lenght)
                {
                    if (MessageBox.Show(" تغیر در طول سیم باعث تغییر در طول سایر سیمهای بین این دو پایه میشود . آیا مایل به تغیر طول سیم هستید ؟  ", "خطا", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        txtLenght.Text = Length.ToString();
                        return false;
                    }
                    else
                        return true;

                }
            }
            else
            {
                _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(Branch.LeftNodeCode, Branch.RightNodeCode);
                if (_DBranch.Code != Guid.Empty)
                {
                    if (Convert.ToDouble(txtLenght.Text) != _DBranch.Lenght)
                    {
                        if (MessageBox.Show(" تغیر در طول سیم باعث تغییر در طول سایر سیمهای بین این دو پایه میشود . آیا مایل به تغیر طول سیم هستید ؟  ", "خطا", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            txtLenght.Text = Length.ToString();
                            return false;
                        }
                        else
                            return true;

                    }
                }

            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper DrawSelfKeeper = new Atend.Global.Acad.DrawEquips.AcDrawSelfKeeper();
            if (Validation())
            {
                if (Convert.ToBoolean(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[7].Value) == false)
                {
                    DrawSelfKeeper.UseAccess = true;
                    DrawSelfKeeper.eSelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Convert.ToInt32(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[7].Value))
                {
                    DrawSelfKeeper.UseAccess = false;
                    Atend.Base.Equipment.ESelfKeeperTip selfkeepertip = new Atend.Base.Equipment.ESelfKeeperTip();
                    ed.WriteMessage("SelectByXCode={0}\n", gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[1].Value.ToString());
                    selfkeepertip= Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(new Guid(gvSelfKeeper.Rows[gvSelfKeeper.CurrentRow.Index].Cells[1].Value.ToString()));
                    DrawSelfKeeper.eSelfKeeperTip = selfkeepertip;
                    ed.WriteMessage("SelefKipperTip.XCOde={0}\n",selfkeepertip.XCode);
                    List<Atend.Base.Equipment.ESelfKeeper> list = new List<Atend.Base.Equipment.ESelfKeeper>();
                    list.Add(Atend.Base.Equipment.ESelfKeeper.SelectByXCode(selfkeepertip.PhaseProductxCode));
                    list.Add(Atend.Base.Equipment.ESelfKeeper.SelectByXCode(selfkeepertip.NeutralProductxCode));
                    list.Add(Atend.Base.Equipment.ESelfKeeper.SelectByXCode(selfkeepertip.NightProductxCode));
                    DrawSelfKeeper.eSelfKeepers = list;
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawSelfKeeper.Existance = status.ACode;

                if (cboProject.Items.Count == 0)
                    DrawSelfKeeper.ProjectCode = 0;
                else
                    DrawSelfKeeper.ProjectCode = Convert.ToInt32(cboProject.SelectedValue);

                DrawSelfKeeper.SelectedObjectId = BranchObj;

                DrawSelfKeeper.UpdateSelfKeeperData(Convert.ToDouble(txtLenght.Text),BranchCode);
                //if (DrawSelfKeeper.UpdateSelfKeeperData())
                //    ed.WriteMessage("Update SelfKeeper Success \n");
            }
                
                          
        }

        private void BindDataToSelfKeeperTip()
        {
            //Atend.Base.Equipment.ESelfKeeperTip
            double CrossSectionArea = -1;
            int MaterailCode = -1;

            if (chkMaterail.Checked)
                MaterailCode = Convert.ToInt32(cboMaterial.SelectedValue);
            if (chkSectionArea.Checked)
                CrossSectionArea = Convert.ToDouble(nudCrossSectionArea.Value);


            System.Data.DataTable dt = Atend.Base.Equipment.ESelfKeeperTip.SearchSelfKeeperSelfKeeperTipSelfKeeperMaterialType(CrossSectionArea, MaterailCode);
            System.Data.DataColumn dc = new System.Data.DataColumn("AMP");
            dt.Columns.Add(dc);
            foreach (DataRow dr in dt.Rows)
            {
                dr["AMP"] = string.Format("{0}+{1}j", dr["Resistance"].ToString(), dr["Reactance"].ToString());
            }

            gvSelfKeeper.AutoGenerateColumns = false;
            gvSelfKeeper.DataSource = dt;
            //gvConductor.DataSource = Atend.Base.Equipment.EConductor.DrawSearch(CrossSectionArea, MaterailCode);
        }

        public void BindDataToProjectCode()
        {
            //cboProject.DisplayMember = "Name";
            //cboProject.ValueMember = "Code";
            //cboProject.DataSource = Atend.Base.Base.BProjectCode.AccessSelectAll();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
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
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvSelfKeeper.AutoGenerateColumns = false;
                gvSelfKeeper.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvSelfKeeper.AutoGenerateColumns = false;
                //gvSelfKeeper.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvSelfKeeper, this);
            ChangeColor();
            for (int i = 0; i < gvSelfKeeper.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvSelfKeeper.Rows[i].Cells[0].Value.ToString()) == selectedProductCode /*SelfTip.Code*/)
                {
                    gvSelfKeeper.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            //BindDataToSelfKeeperTip();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmEditDrawSelfKeeper_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void frmEditDrawSelfKeeper_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            dtMerge = Atend.Base.Equipment.ESelfKeeperTip.SelectAllAndMerge();
            Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchCode);
            Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Branch.ProductCode);
            selectedProductCode = SelfTip.Code;

            System.Data.DataColumn dcMaterialName = new System.Data.DataColumn("MaterialName");
            System.Data.DataColumn dcAMP = new System.Data.DataColumn("AMP");
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

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("SelfCode,SelfXCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvSelfKeeper, this);
            ChangeColor();
            for (int i = 0; i < gvSelfKeeper.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvSelfKeeper.Rows[i].Cells[0].Value.ToString()) == selectedProductCode /*SelfTip.Code*/)
                {
                    gvSelfKeeper.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindMaterialToComboBox();
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }
            BindDataToComboBoxIsExist();
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }
            //BindDataToProjectCode();
            if (cboProject.Items.Count > 0)
            {
                cboProject.SelectedIndex = 0;
            }
            txtLenght.Text = Math.Round(Branch.Lenght, 2).ToString();
            Length = Convert.ToDouble(Math.Round(Branch.Lenght, 2).ToString());

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(Branch.IsExist).Code;
            cboProject.SelectedValue = Branch.ProjectCode;
            
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvSelfKeeper.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvSelfKeeper.Rows[i].Cells[7].Value) == false)
                {
                    gvSelfKeeper.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = condMaterialTbl;
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Atend.Equipment.frmSelfKeeperTip SKTip = new Atend.Equipment.frmSelfKeeperTip();
            //SKTip.ShowDialog();
            //BindDataToSelfKeeperTip();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProject.DisplayMember = "Name";
            cboProject.ValueMember = "ACode";
            cboProject.DataSource = dtWorkOrder;
        }

        private void txtLenght_TextChanged(object sender, EventArgs e)
        {

        }

        

        
    }
}