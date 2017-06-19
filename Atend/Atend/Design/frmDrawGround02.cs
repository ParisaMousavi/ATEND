using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmDrawGround02 : Form
    {
        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.GroundData.eGround = new Atend.Base.Equipment.EGround();
            Atend.Base.Acad.AcadGlobal.GroundData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.GroundData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.GroundData.UseAccess = false;
        }

        public frmDrawGround02()
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

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EGround.SelectAllAndMerge();
            gvGround.AutoGenerateColumns = false;
            gvGround.DataSource = dtMerge;
            ChangeColor();

        }

        //Confirmed
        private void SetDefaultValues()
        {
            if (Atend.Control.ConnectionString.StatusDef)
            {
                if (Atend.Control.Common.IsExist != -1)
                    cboIsExist1.SelectedValue = Atend.Control.Common.IsExist;
                if (Atend.Control.Common.ProjectCode != -1)
                    cboProjCode1.SelectedValue = Atend.Control.Common.ProjectCode;
            }
        }

        private void frmDrawGround02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataTocboIsExist1();
            SetDefaultValues();
        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvGround.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvGround.Rows[i].Cells[3].Value) == false)
                {
                    gvGround.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //Confirmed
        public void BindDataTocboIsExist1()
        {
            cboIsExist1.DisplayMember = "Name";
            cboIsExist1.ValueMember = "Code";
            DataTable dtstatus = Atend.Base.Base.BEquipStatus.SelectAllX();
            cboIsExist1.DataSource = dtstatus;

            //if (Atend.Control.Common.IsExist == -1)
            //{
                DataRow[] drs = dtstatus.Select("IsDefault=True");
                if (drs.Length > 0)
                {
                    cboIsExist1.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
                }
            //}
            //else
            //{
            //    cboIsExist1.SelectedValue = Atend.Control.Common.IsExist;
            //}

            //cboIsExist1.DisplayMember = "Name";
            //cboIsExist1.ValueMember = "Code";
            //cboIsExist1.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist1.SelectedIndex = 4;
        }

        public void BindDataTocboProjCode1()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode1.DisplayMember = "Name";
            //cboProjCode1.ValueMember = "Code";
            //cboProjCode1.DataSource = dt;
        }

        private void frmDrawGround02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
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

            if (gvGround.Rows.Count > 0 && gvGround.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EGround Ground = new Atend.Base.Equipment.EGround();
                bool useAccess = false;
                if (Convert.ToBoolean(gvGround.Rows[gvGround.CurrentRow.Index].Cells[3].Value) == false)
                {
                    Ground = Atend.Base.Equipment.EGround.AccessSelectByCode(Convert.ToInt32(gvGround.Rows[gvGround.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = true;
                }
                else if (Convert.ToBoolean(gvGround.Rows[gvGround.CurrentRow.Index].Cells[3].Value) == true)
                {
                    Ground = Atend.Base.Equipment.EGround.SelectByXCode(new Guid(gvGround.Rows[gvGround.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                }

                Atend.Base.Acad.AcadGlobal.GroundData.eGround = Ground;
                Atend.Base.Acad.AcadGlobal.GroundData.UseAccess = useAccess;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist1.SelectedValue));
                Atend.Base.Acad.AcadGlobal.GroundData.Existance = status.ACode;

                if (cboProjCode1.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.GroundData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.GroundData.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist1.SelectedValue.ToString());
                if (cboProjCode1.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowClose = true;
            }
            else
            {
                AllowClose = false;
            }
        }

        private void cboIsExist1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboIsExist1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIsExist1.SelectedValue != null)
            {
                DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist1.SelectedValue.ToString()));
                cboProjCode1.DisplayMember = "Name";
                cboProjCode1.ValueMember = "ACode";
                cboProjCode1.DataSource = dtWorkOrder;

            }
        }

    }
}