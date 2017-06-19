using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmEditDrawGroundCable02 : Form
    {
        public Guid BranchCode;
        public int GCCode;
        public ObjectId BranchObj;
        int selectedProductCode = -1;
        bool AllowClose = true;

        System.Data.DataColumn TypeName = new System.Data.DataColumn("Name", typeof(string));
        System.Data.DataColumn TypeCode = new System.Data.DataColumn("Code", typeof(int));

        System.Data.DataTable TypeTbl = new System.Data.DataTable();
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;
        //public double Length;

        public frmEditDrawGroundCable02()
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
            dr1["Name"] = "XLPE از جنس آلمینیوم با عایق خشک";
            dr1["Code"] = 1;

            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "XLPE از جنس مس با عایق خشک";
            dr2["Code"] = 2;

            DataRow dr3 = TypeTbl.NewRow();
            dr3["Name"] = "PVC از جنس آلمینیوم با عایق خشک";
            dr3["Code"] = 3;

            DataRow dr4 = TypeTbl.NewRow();
            dr4["Name"] = "PVC از جنس مس با عایق خشک";
            dr4["Code"] = 4;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);
            TypeTbl.Rows.Add(dr3);
            TypeTbl.Rows.Add(dr4);
        }

        private void BindMaterialToComboBox()
        {
            cboMaterial.ValueMember = "Code";
            cboMaterial.DisplayMember = "Name";
            cboMaterial.DataSource = TypeTbl;// Atend.Base.Equipment.ECabelType.SelectAll();

        }

        private void frmEditDrawGroundCable_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToComboBoxIsExist();
            BindMaterialToComboBox();
            //BindDataTocboProjCode();

            dtMerge = Atend.Base.Equipment.EGroundCabel.SelectAllAndMerge();
            Atend.Base.Design.DBranch dBranch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchCode);
            selectedProductCode = dBranch.ProductCode;
            
            System.Data.DataColumn dcTypeName = new System.Data.DataColumn("TypeName");
            dtMerge.Columns.Add(dcTypeName);

            foreach (DataRow dr in dtMerge.Rows)
            {
                if (Convert.ToInt32(dr["Type"].ToString()) == 1)
                {
                    dr["TypeName"] = "XLPE از جنس آلمینیوم با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "XLPE از جنس مس با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 3)
                {
                    dr["TypeName"] = "PVC از جنس آلمینیوم با عایق خشک";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 4)
                {
                    dr["TypeName"] = "از جنس مس با عایق خشک";
                }
            }
            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("CabelTipCode", new object[1] { selectedProductCode }, dtMerge, gvConductor, this);
            ChangeColor();
            //BindDataToCounductorTip();
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvConductor.Rows[i].Cells[1].Value.ToString()) == dBranch.ProductCode && Convert.ToBoolean(gvConductor.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dBranch.IsExist).Code;
            cboProjCode.SelectedValue = dBranch.ProjectCode;
            txtLenght.Text = Math.Round(dBranch.Lenght,2).ToString();
            //Length = Convert.ToDouble(Math.Round(dBranch.Lenght, 2).ToString());

            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = 0;
            }

        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvConductor.Rows[i].Cells[4].Value) == false)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }


        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
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
            float CrossSectionArea = -1;
            int TypeCode = -1;
            string strFilter = "";
            bool check = false;

            if (chkMaterail.Checked)
            {
                TypeCode = Convert.ToInt32(cboMaterial.SelectedValue);
              
                    strFilter = " Type='" + TypeCode.ToString() + "'";
                    check = true;
            }
            if (chkSectionArea.Checked)
            {
                CrossSectionArea= Convert.ToSingle(nudCrossSectionArea.Value.ToString());
                if (strFilter != "")
                {
                    strFilter += " AND CrossSectionArea='" + CrossSectionArea.ToString() + "'";
                }
                else
                {
                    strFilter = " CrossSectionArea='" + CrossSectionArea.ToString() + "'";
                }
                check = true;
            }
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
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("CabelTipCode", new object[1] { selectedProductCode }, dtMerge, gvConductor, this);
            ChangeColor();
            //BindDataToCounductorTip();
            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvConductor.Rows[i].Cells[1].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvConductor.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvConductor.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            //BindDataToCounductorTip();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private bool Validation()
        {
            if (gvConductor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کابل خودنگهدار مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }

            if (string.IsNullOrEmpty(txtLenght.Text))
            {
                txtLenght.Focus();
                MessageBox.Show("لطفا طول را وارد نمایید", "خطا");
                return false;
            }

            //Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchCode);

            //Atend.Base.Design.DBranch _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(Branch.RightNodeCode, Branch.LeftNodeCode);
            //if (_DBranch.Code != Guid.Empty)
            //{
            //    if (Convert.ToDouble(txtLenght.Text) != _DBranch.Lenght)
            //    {
            //        if (MessageBox.Show(" تغیر در طول سیم باعث تغییر در طول سایر سیمهای بین این دو پایه میشود . آیا مایل به تغیر طول سیم هستید ؟  ", "خطا", MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            txtLenght.Text = Length.ToString();
            //            return false;
            //        }
            //        else
            //            return true;

            //    }
            //}
            //else
            //{
            //    _DBranch = Atend.Base.Design.DBranch.AccessSelectByRigthAndLeftNodeCode(Branch.LeftNodeCode, Branch.RightNodeCode);
            //    if (_DBranch.Code != Guid.Empty)
            //    {
            //        if (Convert.ToDouble(txtLenght.Text) != _DBranch.Lenght)
            //        {
            //            if (MessageBox.Show(" تغیر در طول سیم باعث تغییر در طول سایر سیمهای بین این دو پایه میشود . آیا مایل به تغیر طول سیم هستید ؟  ", "خطا", MessageBoxButtons.YesNo) == DialogResult.No)
            //            {
            //                txtLenght.Text = Length.ToString();
            //                return false;
            //            }
            //            else
            //                return true;

            //        }
            //    }

            //}


            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Atend.Global.Acad.DrawEquips.AcDrawGroundCabel  DrawGroundCabel = new Atend.Global.Acad.DrawEquips.AcDrawGroundCabel();
            if (Validation())
            {
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[4].Value) == false)
                {
                    DrawGroundCabel.UseAccess = true;
                    DrawGroundCabel.eGroundCabelTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[4].Value))
                {
                    DrawGroundCabel.UseAccess = false;
                    Atend.Base.Equipment.EGroundCabelTip GroundCabeltip = new Atend.Base.Equipment.EGroundCabelTip();
                    ed.WriteMessage("SelectByXCode={0}\n", gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString());
                    GroundCabeltip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString()));
                    DrawGroundCabel.eGroundCabelTip = GroundCabeltip;
                    ed.WriteMessage("GroundCabelTip.XCOde={0}\n", GroundCabeltip.XCode);
                    List<Atend.Base.Equipment.EGroundCabel> list = new List<Atend.Base.Equipment.EGroundCabel>();
                    list.Add(Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundCabeltip.PhaseProductXCode));
                    list.Add(Atend.Base.Equipment.EGroundCabel.SelectByXCode(GroundCabeltip.NeutralProductXCode));
                    DrawGroundCabel.eGroundCabels = list;
                }
                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawGroundCabel.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawGroundCabel.ProjectCode = 0;
                else
                    DrawGroundCabel.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawGroundCabel.SelectedObjID = BranchObj;
                DrawGroundCabel.UpdateGroundCabelData(Convert.ToDouble(txtLenght.Text), BranchCode);
                //if (DrawSelfKeeper.UpdateSelfKeeperData())
                //    ed.WriteMessage("Update SelfKeeper Success \n");
            }
        }

        private void frmEditDrawGroundCable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
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