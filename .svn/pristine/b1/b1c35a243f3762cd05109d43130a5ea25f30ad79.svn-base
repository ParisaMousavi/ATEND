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
    public partial class frmEditDrawLight02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        bool AllowClose = true;
        public ObjectId ObjID;
        public Guid NodeCode;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;


        public frmEditDrawLight02()
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

        private void frmEditDrawLight02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.ELight.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);

            gvLight.AutoGenerateColumns = false;
            gvLight.DataSource = dtMerge;
            
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { dpack.ProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvLight, this);
            ChangeColor();
            for (int i = 0; i < gvLight.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvLight.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvLight.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvLight.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataTocboIsExist1();
            //BindDataTocboProjCode1();

            cboIsExist1.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code; //dpack.IsExistance;
            cboProjCode1.SelectedValue = dpack.ProjectCode;

            
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvLight.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvLight.Rows[i].Cells[3].Value) == false)
                {
                    gvLight.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
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
            System.Data.DataTable dt = Atend.Base.Base.BWorkOrder.SelectAll(); //Atend.Base.Base.BProjectCode.AccessSelectAll();
            cboProjCode1.DisplayMember = "Name";
            cboProjCode1.ValueMember = "Code";
            cboProjCode1.DataSource = dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Global.Acad.DrawEquips.AcDrawLight light = new Atend.Global.Acad.DrawEquips.AcDrawLight();
                if (Convert.ToBoolean(gvLight.Rows[gvLight.CurrentRow.Index].Cells[3].Value) == false)
                {
                    light.UseAccess = true;
                    light.eLight = Atend.Base.Equipment.ELight.AccessSelectByCode(Convert.ToInt32(gvLight.Rows[gvLight.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvLight.Rows[gvLight.CurrentRow.Index].Cells[3].Value))
                {
                    light.UseAccess = false;
                    light.eLight = Atend.Base.Equipment.ELight.SelectByXCode(new Guid(gvLight.Rows[gvLight.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist1.SelectedValue));
                light.Existance = status.ACode;

                if (cboProjCode1.Items.Count == 0)
                    light.ProjectCode = 0;
                else
                    light.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue);

                light.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (light.UpdateLightData(dpack.Code))
                {
                    ed.WriteMessage("Update Light Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }
            else
            {
                AllowClose = false;
            }
        }

        private void cboIsExist1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist1.SelectedValue.ToString()));
            cboProjCode1.DisplayMember = "Name";
            cboProjCode1.ValueMember = "ACode";
            cboProjCode1.DataSource = dtWorkOrder;
        }

        private void frmEditDrawLight02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        
    }
}