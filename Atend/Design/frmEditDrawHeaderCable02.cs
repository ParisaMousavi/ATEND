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
    public partial class frmEditDrawHeaderCable02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public Guid NodeCode;
        public Guid DpakageCode;
        public ObjectId objID;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawHeaderCable02()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int Voltage = -1;
            string Name = string.Empty;
            bool check = false;
            string strFilter = "";
            if (chkName.Checked)
            {
                Name = txtName.Text;
                strFilter = "Name Like '%" + Name + "%'";
                check = true;
            }
           
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvHeaderCabel.AutoGenerateColumns = false;
                gvHeaderCabel.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvHeaderCabel.AutoGenerateColumns = false;
                //gvHeaderCabel.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvHeaderCabel, this);
            ChangeColor();
            for (int i = 0; i < gvHeaderCabel.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvHeaderCabel.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvHeaderCabel.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvHeaderCabel.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvHeaderCabel.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvHeaderCabel.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvHeaderCabel.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
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

            if (gvHeaderCabel.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا سرکابل مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }
            return true;
        }

        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        public void BindDataToCboProjectCode()
        {
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = Atend.Base.Base.BProjectCode.AccessSelectAll();
        }

        private void frmEditDrawHeaderCable02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EHeaderCabel.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;
            
            gvHeaderCabel.AutoGenerateColumns = false;
            gvHeaderCabel.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvHeaderCabel, this);
            ChangeColor();
            for (int i = 0; i < gvHeaderCabel.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvHeaderCabel.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvHeaderCabel.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvHeaderCabel.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToCboIsExist();
            //BindDataToCboProjectCode();
            

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel DrawHeaderCabel = new Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel();
            if (Validation())
            {
                if (Convert.ToBoolean(gvHeaderCabel.Rows[gvHeaderCabel.CurrentRow.Index].Cells[3].Value) == false)
                {
                    DrawHeaderCabel.UseAccess = true;
                    DrawHeaderCabel.eHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(Convert.ToInt32(gvHeaderCabel.Rows[gvHeaderCabel.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvHeaderCabel.Rows[gvHeaderCabel.CurrentRow.Index].Cells[3].Value))
                {
                    DrawHeaderCabel.UseAccess = false;
                    DrawHeaderCabel.eHeaderCabel = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(new Guid(gvHeaderCabel.Rows[gvHeaderCabel.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawHeaderCabel.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawHeaderCabel.ProjectCode = 0;
                else
                    DrawHeaderCabel.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawHeaderCabel.SelectedObjectId = objID;
                if (DrawHeaderCabel.UpdateHeaderCabelData(NodeCode))
                    ed.WriteMessage("Update HeaderCable Success \n");
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