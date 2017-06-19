using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawKablsho02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        System.Data.DataColumn TypeName = new System.Data.DataColumn("Name", typeof(string));
        System.Data.DataColumn TypeCode = new System.Data.DataColumn("Code", typeof(int));

        System.Data.DataColumn MaterialName = new System.Data.DataColumn("Name");
        System.Data.DataColumn MaterialCode = new System.Data.DataColumn("Code");

        public System.Data.DataTable TypeTbl = new System.Data.DataTable();
        public System.Data.DataTable MaterialTbl = new System.Data.DataTable();

        public Guid NodeCode;
        public ObjectId ObjID;
        int selectedProductCode = -1;
        bool ForceToClose = false;


        public frmEditDrawKablsho02()
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
            dr1["Name"] = "پرسی";
            dr1["Code"] = 1;
            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "بیمتال";
            dr2["Code"] = 2;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);


            MaterialTbl.Columns.Add(MaterialCode);
            MaterialTbl.Columns.Add(MaterialName);

            DataRow dr3 = MaterialTbl.NewRow();
            dr3["Name"] = "مسی";
            dr3["Code"] = 1;
            DataRow dr4 = MaterialTbl.NewRow();
            dr4["Name"] = "الومینیومی";
            dr4["Code"] = 2;


            MaterialTbl.Rows.Add(dr3);
            MaterialTbl.Rows.Add(dr4);
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvKablsho.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvKablsho.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvKablsho.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید", "خطا");
                return false;
            }

            if (gvKablsho.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کابلشو مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;
        }

        private void frmEditDrawKablsho02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EKablsho.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            gvKablsho.AutoGenerateColumns = false;
            gvKablsho.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { dpack.ProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvKablsho, this);
            ChangeColor();
            for (int i = 0; i < gvKablsho.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvKablsho.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvKablsho.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvKablsho.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToCboIsExist();
            BindDataToCboMaterial();
            BindDataToCboType();

            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;
            
        }

        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        public void BindDataToCboType()
        {
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        public void BindDataToCboMaterial()
        {
            cboMaterial.DisplayMember = "Name";
            cboMaterial.ValueMember = "Code";
            cboMaterial.DataSource = MaterialTbl;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int Type = -1;
            int Material = -1;

            bool check = false;
            string strFilter = "";

            if (chkType.Checked)
            {
                Type = Convert.ToInt32(cboType.SelectedValue);
                strFilter = " Type='" + Type.ToString() + "'";
                check = true;
            }
            if (chkMaterial.Checked)
            {
                Material = Convert.ToInt32(cboMaterial.SelectedValue);
                if (strFilter != "")
                {
                    strFilter += " AND Material='" + Material.ToString() + "'";

                }
                else
                {
                    strFilter = " Material='" + Material.ToString() + "'";
                }
                check = true;
            }

            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvKablsho.AutoGenerateColumns = false;
                //gvKablsho.DataSource = dv;
                gvKablsho.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvKablsho.AutoGenerateColumns = false;
                //gvKablsho.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvKablsho, this);
            ChangeColor();
            for (int i = 0; i < gvKablsho.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvKablsho.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvKablsho.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvKablsho.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmEditDrawKablsho02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawKablsho DrawKablsho = new Atend.Global.Acad.DrawEquips.AcDrawKablsho();
            if (Validation())
            {
                if (Convert.ToBoolean(gvKablsho.Rows[gvKablsho.CurrentRow.Index].Cells[3].Value) == false)
                {
                    DrawKablsho.UseAccess = true;
                    DrawKablsho.eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(Convert.ToInt32(gvKablsho.Rows[gvKablsho.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvKablsho.Rows[gvKablsho.CurrentRow.Index].Cells[3].Value))
                {
                    DrawKablsho.UseAccess = false;
                    DrawKablsho.eKablsho = Atend.Base.Equipment.EKablsho.SelectByXCode(new Guid(gvKablsho.Rows[gvKablsho.CurrentRow.Index].Cells[1].Value.ToString()));

                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawKablsho.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawKablsho.ProjectCode = 0;
                else
                    DrawKablsho.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawKablsho.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawKablsho.UpdateKablshoData(dpack.Code))
                {
                    ed.WriteMessage("Update Kablsho Success \n");
                    AllowClose = true;
                    this.Close();
                }
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