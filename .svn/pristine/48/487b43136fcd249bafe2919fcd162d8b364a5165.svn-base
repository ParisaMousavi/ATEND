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
    public partial class frmEditDrawBranch02 : Form
    {
        bool AllowClose = true;
        ArrayList ConductorList = new ArrayList();
        System.Data.DataColumn TypeName = new System.Data.DataColumn("Name", typeof(string));
        System.Data.DataColumn TypeCode = new System.Data.DataColumn("Code", typeof(int));

        System.Data.DataTable TypeTbl = new System.Data.DataTable();
        int seletedProductCode = -1;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        public frmEditDrawBranch02()
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
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "فاکس";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "مینک";
            dr3["Code"] = 3;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "هاینا";
            dr4["Code"] = 4;

            DataRow dr5 = TypeTbl.NewRow();
            dr5["Name"] = "لینکس";
            dr5["Code"] = 5;

            DataRow dr6 = TypeTbl.NewRow();
            dr6["Name"] = "داگ";
            dr6["Code"] = 6;


            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);
            TypeTbl.Rows.Add(dr5);
            TypeTbl.Rows.Add(dr6);
        }

        private bool Validation()
        {

            if (string.IsNullOrEmpty(cboProjCode.Text))
            {
                MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
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

            return true;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = TypeTbl;// Atend.Base.Equipment.EConductorMaterialType.SelectAll();

        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }


        private void frmDrawBranch01_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindMaterialToComboBox();
            BindDataToComboBoxIsExist();
            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }
            BindDataTocboProjCode();

            //txtEquip1.Text = Atend.Acad.Ribbon.conductorInformation.AutocadId1;
            //txtEquip2.Text = Atend.Acad.Ribbon.conductorInformation.AutocadId2;

            //double ConductorLength = Math.Sqrt(Math.Pow((Atend.Menu.conductorInformation.Point2.X - Atend.Menu.conductorInformation.Point1.X), 2) +
            //                                   Math.Pow((Atend.Menu.conductorInformation.Point2.Y - Atend.Menu.conductorInformation.Point1.Y), 2));

            //txtLength.Text = Convert.ToString(Math.Round(ConductorLength, 2));

            //if (cboCircuitType.Items.Count > 0)
            //{
            //    cboCircuitType.SelectedIndex = 0;
            //}

            //if (cboNetType.Items.Count > 0)
            //{
            //    cboNetType.SelectedIndex = 0;
            //}

            //if (cboUnitType.Items.Count > 0)
            //{
            //    cboUnitType.SelectedIndex = 0;
            //}
            //txtSag.Text = "0";

        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.Items.Clear();
            cboIsExist.Items.Add("نصب");
            cboIsExist.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            double CrossSectionArea = -1;
            int MaterailCode = -1;
            if (chkMaterail.Checked)
                MaterailCode = Convert.ToInt32(cboMaterial.SelectedValue);
            if (chkSectionArea.Checked)
                CrossSectionArea = Convert.ToDouble(nudCrossSectionArea.Value);

            System.Data.DataTable dt = Atend.Base.Equipment.EConductorTip.SearchConductorConductorTipConductorMaterialType(CrossSectionArea, MaterailCode);
            System.Data.DataColumn dc = new System.Data.DataColumn("AMP");
            dt.Columns.Add(dc);
            foreach (DataRow dr in dt.Rows)
            {
                dr["AMP"] = string.Format("{0}+{1}j", dr["Resistance"].ToString(), dr["Reactance"].ToString());
            }

            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dt;
            //gvConductor.AutoGenerateColumns = false;
            //gvConductor.DataSource = Atend.Base.Equipment.EConductorTip.SearchConductorConductorTipConductorMaterialType();
            //gvConductor.DataSource = Atend.Base.Equipment.EConductor.DrawSearch(CrossSectionArea, MaterailCode);
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ConductorList.Count; i++)
            {

                Atend.Global.Calculation.General.General.AutoCAdConductorList Cond = (Atend.Global.Calculation.General.General.AutoCAdConductorList)ConductorList[i];
                Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(Cond.ConductorGuid);
                Atend.Base.Equipment.EConductorTip condTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
                //ed.WriteMessage("CondTip.Code= "+condTip.Code+"\n");
                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Cond.ConductorObjectID);
                if (gvConductor.Rows.Count > 0)
                {
                    //Branch.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                    //Branch.XCode = new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                    //if (chkIsExist.CheckState == CheckState.Unchecked && cboIsExist.SelectedIndex == 0)
                    //    Branch.IsExist = 0;
                    //if (chkIsExist.Checked && cboIsExist.SelectedIndex == 0)
                    //    Branch.IsExist = 1;
                    //if (chkIsExist.Checked && cboIsExist.SelectedIndex == 1)
                    //    Branch.IsExist = 2;

                    //Branch.IsExist = chkIsExist.Checked;
                    Branch.Number = condTip.Description;
                    //if (Branch.AccessUpdate())
                    //{

                    //    //at_info.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());

                    //    //**EDIT**
                    //    ////at_info.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                    //    ////at_info.Insert();
                    //    //ed.WriteMessage("Branch.Number= " + Branch.Number + "\n");
                    //    //ed.WriteMessage("Branch.OBJID=  " + Cond.ConductorObjectID + "\n");
                    //    Atend.Global.Acad.UAcad.ChangeBranchText(Cond.ConductorObjectID, Branch.Number);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ويرايش امكان پذير نمي باشد");
                    //}
                }
            }

            AllowClose = true;
            Close();




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

        private void gvConductor_DoubleClick(object sender, EventArgs e)
        {
            //ed.WriteMessage("***********\n");
            //ed.WriteMessage(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString() + "\n");
            //Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode = Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
            //Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.Code = new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            int ConductorCounter = 1;
            PromptSelectionOptions pso = new PromptSelectionOptions();
            pso.MessageForAdding = "\n Select Area where you want :";

            PromptSelectionResult psr = ed.GetSelection(pso);
            SelectionSet ss = psr.Value;

            ObjectId[] ids = ss.GetObjectIds();

            foreach (ObjectId oi in ids)
            {
                Atend.Global.Calculation.General.General.AutoCAdConductorList Item =
                            new Atend.Global.Calculation.General.General.AutoCAdConductorList();
                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                //ed.WriteMessage("ProductCode=" + at_info.ProductCode + " nodeType= " + at_info.NodeType + "ParentCode=" + at_info.ParentCode + "\n");
                if (at_info.NodeCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                {
                    //ed.WriteMessage("at_info=" + at_info.ParentCode + "\n");
                    Item.ConductorGuid = new Guid(at_info.NodeCode);
                    Item.ConductorObjectID = at_info.SelectedObjectId;
                    ConductorList.Add(Item);
                    ConductorCounter++;

                }

            }




            this.Show();
            lblSelected.Text = ConductorCounter.ToString();
            //ed.WriteMessage("FinishSelect\n");

        }

        //private void chkIsExist_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkIsExist.Checked)
        //    {
        //        cboIsExist.Items.Clear();
        //        cboIsExist.Items.Add("برکناری-مستعمل");
        //        cboIsExist.Items.Add("برکناری-اسقات");
        //    }
        //    else if (chkIsExist.CheckState == CheckState.Unchecked)
        //    {
        //        cboIsExist.Items.Clear();
        //        cboIsExist.Items.Add("نصب");
        //    }
        //    cboIsExist.SelectedIndex = 0;
        //    cboIsExist.Refresh();
        //}
    }
}