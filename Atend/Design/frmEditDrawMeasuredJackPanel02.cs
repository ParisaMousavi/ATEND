using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawMeasuredJackPanel02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        bool AllowClose = true;
        public ObjectId ObjID;
        public Guid NodeCode;
        int selectedProductCode = -1;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;


        public frmEditDrawMeasuredJackPanel02()
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

        private void frmEditDrawMeasuredJackPanel02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EMeasuredJackPanel.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            gvMeasuredJackPanel.AutoGenerateColumns = false;
            gvMeasuredJackPanel.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvMeasuredJackPanel, this);
            ChangeColor();
            for (int i = 0; i < gvMeasuredJackPanel.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvMeasuredJackPanel.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvMeasuredJackPanel.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvMeasuredJackPanel.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
            BindDataTocboIsExist1();
            //BindDataTocboProjCode1();

            cboIsExist1.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode1.SelectedValue = dpack.ProjectCode;
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvMeasuredJackPanel.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvMeasuredJackPanel.Rows[i].Cells[4].Value) == false)
                {
                    gvMeasuredJackPanel.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataTocboIsExist1()
        {
            cboIsExist1.DisplayMember = "Name";
            cboIsExist1.ValueMember = "Code";
            cboIsExist1.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        public void BindDataTocboProjCode1()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode1.DisplayMember = "Name";
            //cboProjCode1.ValueMember = "Code";
            //cboProjCode1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Count = -1;
            bool check = false;
            string strFilter = "";

            if (chkCount.Checked)
            {
                Count = Convert.ToInt32(numCount.Value);
                strFilter = " Count='" + Count + "'";
                check = true;
            }
            DataView dv = new DataView();
            if (check)
            {
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvMeasuredJackPanel.AutoGenerateColumns = false;
                gvMeasuredJackPanel.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvMeasuredJackPanel.AutoGenerateColumns = false;
                //gvMeasuredJackPanel.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvMeasuredJackPanel, this);
            ChangeColor();
            for (int i = 0; i < gvMeasuredJackPanel.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvMeasuredJackPanel.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvMeasuredJackPanel.Rows[i].Cells[4].Value.ToString()) == false)
                {
                    gvMeasuredJackPanel.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode1.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    cboProjCode1.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboIsExist1.Text))
            {
                MessageBox.Show("لطفاً کد وضعیت تجهیز را انتخاب کنید", "خطا");
                cboIsExist1.Focus();
                return false;
            }
            return true;
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel MeasuredJackPanel = new Atend.Global.Acad.DrawEquips.AcDrawMeasuredJackPanel();
                if (Convert.ToBoolean(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[4].Value) == false)
                {
                    MeasuredJackPanel.UseAccess = true;
                    MeasuredJackPanel.eMeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.AccessSelectByCode(Convert.ToInt32(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[4].Value))
                {
                    MeasuredJackPanel.UseAccess = false;
                    MeasuredJackPanel.eMeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.SelectByXCode(new Guid(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist1.SelectedValue));
                MeasuredJackPanel.Existance = status.ACode;

                if (cboProjCode1.Items.Count == 0)
                    MeasuredJackPanel.ProjectCode = 0;
                else
                    MeasuredJackPanel.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue);

                MeasuredJackPanel.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (MeasuredJackPanel.UpdateMeasuredJackPanelData(dpack.Code))
                {
                    ed.WriteMessage("Update MeasuredJackPanel Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }
            else
            {
                AllowClose = false;
            }
        }

        private void frmEditDrawMeasuredJackPanel02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void cboIsExist1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist1.SelectedValue.ToString()));
            cboProjCode1.DisplayMember = "Name";
            cboProjCode1.ValueMember = "ACode";
            cboProjCode1.DataSource = dtWorkOrder;
        }

    }
}