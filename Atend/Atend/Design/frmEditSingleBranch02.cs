using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace Atend.Design
{
    public partial class frmEditSingleBranch02 : Form
    {
        bool AllowClose = true;
        public Guid NodeCode;
        int Code;
        public ObjectId obj;
        ArrayList ConductorList = new ArrayList();
        Guid BranchGuid;
        System.Data.DataColumn TypeName = new System.Data.DataColumn("Name", typeof(string));
        System.Data.DataColumn TypeCode = new System.Data.DataColumn("Code", typeof(int));

        System.Data.DataTable TypeTbl = new System.Data.DataTable();

        System.Data.DataTable dtMerge = new System.Data.DataTable();
        int selectedProductCode = -1;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        public double Length;
        //private Guid _StartPole, _EndPole;

        public frmEditSingleBranch02()
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

            TypeTbl.Columns.Add(TypeCode);
            TypeTbl.Columns.Add(TypeName);

            DataRow dr1 = TypeTbl.NewRow();
            dr1["Name"] = "مسی";
            dr1["Code"] = 0;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "آلومینیوم";
            dr2["Code"] = 1;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "آلومینیوم مغز فولاد";
            dr3["Code"] = 2;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "آلیاژ آلومینیوم";
            dr4["Code"] = 3;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);
       }

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtLenght.Text))
            {
                MessageBox.Show("لطفا مقدار طول را وارد نمایید", "خطا");
                txtLenght.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtLenght.Text))
            {
                MessageBox.Show("لطفا  طول رابا فرمت مناسب وارد نمایید", "خطا");
                txtLenght.Focus();
                return false;
            }
            //if (txtEquip1.Text == "")
            //{
            //    MessageBox.Show("لطفاً تجهیز مورد نظر را انتخاب نمایید", "خطا");
            //    txtEquip1.Focus();
            //    return false;
            //}

            //if (txtEquip2.Text == "")
            //{
            //    MessageBox.Show("لطفاً تجهیز مورد نظر را انتخاب نمایید", "خطا");
            //    txtEquip2.Focus();
            //    return false;
            //}

            //if (txtSag.Text == "" )
            //{
            //    MessageBox.Show("لطفا اندازه فلش را مشخص نمایید","خطا");
            //    txtSag.Focus();
            //    return false;
            //}

            //if (txtMaterial.Text == "")
            //{
            //    MessageBox.Show("لطفا سیم مورد نظر را انتخاب نمایید","خطا");
            //    tabControl1.TabPages[0].Focus();
            //    return false;
            //}



            Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(NodeCode);

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = TypeTbl;// Atend.Base.Equipment.EConductorMaterialType.SelectAll();

        }

        private void frmDrawBranch01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            //BindDataTocboProjCode();
            BindMaterialToComboBox();
            BindDataToComboBoxIsExist();
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

            dtMerge = Atend.Base.Equipment.EConductorTip.SelectAllAndMerge();
            System.Data.DataColumn dcAMP = new System.Data.DataColumn("AMP");
            System.Data.DataColumn dcMaterialName = new System.Data.DataColumn("MaterialName");
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

            Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(NodeCode);
            Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
            selectedProductCode = CondTip.Code;

            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("CondCode", new object[1] { selectedProductCode }, dtMerge, gvConductor, this);
            ChangeColor();
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvConductor.Rows[i].Cells[0].Value.ToString()) == CondTip.Code)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            txtLenght.Text = Math.Round(Branch.Lenght, 2).ToString();
            Length = Convert.ToDouble(Math.Round(Branch.Lenght, 2).ToString());
            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(Branch.IsExist).Code;
            cboProjCode.SelectedValue = Branch.ProjectCode;
            

        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
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

            //ed.WriteMessage("TypeCode={0},Cross={1},strFilter={2}\n", MaterailCode, CrossSectionArea, strFilter);
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvConductor.AutoGenerateColumns = false;
                //gvConductor.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("CondCode", new object[1] { selectedProductCode }, dtMerge, gvConductor, this);
            ChangeColor();
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvConductor.Rows[i].Cells[0].Value.ToString()) == selectedProductCode)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void gvConductor_Click(object sender, EventArgs e)
        {
            //ed.WriteMessage("Click\n");
            /*if (gvConductor.Rows.Count > 0)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString() + "\n");
                Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(Convert.ToInt32(
                    gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value));
                Atend.Base.Equipment.EConductorMaterialType MaterialType = Atend.Base.Equipment.EConductorMaterialType.SelectByCode(conductor.MaterialCode);
                ed.WriteMessage(conductor.Alpha.ToString() + "\n");
                txtAlasticity.Text = conductor.Alasticity.ToString();
                txtAlpha.Text = conductor.Alpha.ToString();
                txtCrossSectionArea.Text = conductor.CrossSectionArea.ToString();
                txtDiagonal.Text = conductor.Diagonal.ToString();
                txtGMR.Text = conductor.GMR.ToString();
                txtMaterial.Text = MaterialType.Name;
                txtMaxCurrent.Text = conductor.MaxCurrent.ToString();
                txtReactance.Text = conductor.Reactance.ToString();
                txtResistance.Text = conductor.Resistance.ToString();
                txtUTS.Text = conductor.UTS.ToString();
                txtWC.Text = conductor.Wc.ToString();
                txtWeight.Text = conductor.Weight.ToString();
            }
            */
        }

        //private void btnOk_Click(object sender, EventArgs e)
        //{
        //    Atend.Global.Acad.DrawEquips.AcDrawConductor DrawConductor = new Atend.Global.Acad.DrawEquips.AcDrawConductor();
            
        //    //if (Validation())
        //    //{
        //    //    if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[8].Value) == false)//Access
        //    //    {
        //    //        ed.WriteMessage("Access Update\n");
        //    //        DrawConductor.UseAccess = true;


        //    //        DrawConductor.Existance = Convert.ToByte(cboIsExist.SelectedValue);
        //    //        DrawConductor.ProjectCode = Convert.ToInt32(cboIsExist.SelectedValue);
        //    //    }
        //    //    else
        //    //    {
        //    //        ed.WriteMessage("*******Local Update\n");
        //    //        DrawConductor.UseAccess = false;
        //    //        Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(
        //    //            new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));

        //    //        DrawConductor.eConductorTip = CondTip;

        //    //        Atend.Base.Equipment.EConductor CondPhase = Atend.Base.Equipment.EConductor.SelectByXCode(
        //    //            CondTip.PhaseProductXCode);
        //    //        DrawConductor.eConductors.Add(CondPhase);

        //    //        Atend.Base.Equipment.EConductor CondNeutral = Atend.Base.Equipment.EConductor.SelectByXCode(
        //    //          CondTip.NeutralProductXCode);
        //    //        DrawConductor.eConductors.Add(CondNeutral);

        //    //        Atend.Base.Equipment.EConductor CondNight = Atend.Base.Equipment.EConductor.SelectByXCode(
        //    //           CondTip.NightProductXCode);
        //    //        DrawConductor.eConductors.Add(CondNight);
        //    //        DrawConductor.Existance = Convert.ToByte(cboIsExist.SelectedValue);
        //    //        DrawConductor.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);
        //    //    }
        //    //    //ed.WriteMessage("NodeCode={0}\n");
        //    //    //ed.WriteMessage("OBJ={0}\n", obj);
        //    //}
          



        //}

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

        private void gvConductor_DoubleClick(object sender, EventArgs e)
        {
            //ed.WriteMessage("***********\n");
            //ed.WriteMessage(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString() + "\n");
            //**EDIT**
            //Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
            //Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Code = new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());

        }

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

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            Atend.Global.Acad.DrawEquips.AcDrawConductor DrawConductor = new Atend.Global.Acad.DrawEquips.AcDrawConductor();
            if (Validation())
            {

                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[8].Value) == false)//Access
                {
                    //ed.WriteMessage("Access Update\n");
                    DrawConductor.UseAccess = true;
                    Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(
                       Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString()));
                    DrawConductor.eConductorTip = CondTip;

                }
                else
                {
                    //ed.WriteMessage("*******Local Update\n");
                    DrawConductor.UseAccess = false;
                    DrawConductor.eConductors = new List<Atend.Base.Equipment.EConductor>();
                    Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(
                        new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                    //ed.WriteMessage("condTip.Name={0}\n", CondTip.Name);
                    DrawConductor.eConductorTip = CondTip;
                    //ed.WriteMessage("CondTip.PhaseProductXCode={0}\n", CondTip.PhaseProductXCode);
                    Atend.Base.Equipment.EConductor CondPhase = Atend.Base.Equipment.EConductor.SelectByXCode(
                        CondTip.PhaseProductXCode);
                    //ed.WriteMessage("CondPhase.Name={0}\n", CondPhase.Name);
                    DrawConductor.eConductors.Add(CondPhase);
                    //ed.WriteMessage("***\n");
                    Atend.Base.Equipment.EConductor CondNeutral = Atend.Base.Equipment.EConductor.SelectByXCode(
                      CondTip.NeutralProductXCode);
                    DrawConductor.eConductors.Add(CondNeutral);

                    Atend.Base.Equipment.EConductor CondNight = Atend.Base.Equipment.EConductor.SelectByXCode(
                       CondTip.NightProductXCode);
                    DrawConductor.eConductors.Add(CondNight);
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawConductor.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawConductor.ProjectCode = 0;
                else
                    DrawConductor.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawConductor.UpdateConductorData(NodeCode, obj, Convert.ToDouble(txtLenght.Text));
            }
            else
                AllowClose = false;
            
                        
        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }
    }
}