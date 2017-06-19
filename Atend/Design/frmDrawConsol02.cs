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
    public partial class frmDrawConsol02 : Form
    {

        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.ConsolData.eConsol = new Atend.Base.Equipment.EConsol();
            Atend.Base.Acad.AcadGlobal.ConsolData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.ConsolData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.ConsolData.UseAccess = false;
        }

        public frmDrawConsol02()
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

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private bool Validation()
        {


            if (gvConductor.Rows.Count > 0 && gvConductor.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }


            return true;

        }

        //Confirmed
        public void BindDataToCboIsExist()
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
                cboIsExist.SelectedValue = Convert.ToInt32(drs[0]["Code"]);
            }
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

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EConsol.SelectAllAndMerge();
            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;
            ChangeColor();

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


        private void frmDrawHeaderCable02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToCboIsExist();
            cboType.SelectedIndex = 0;
            cboVoltagelevel.SelectedIndex = 0;
            SetDefaultValues();
        }

        public void BindDataToConsol()
        {
            int Type = -1;
            //int Volt = 0;
            bool checkType = false;
            bool checkName = false;
            string strFilter = "";

            if (chkType.Checked)
            {
                Type = Convert.ToInt32(cboType.SelectedIndex);
                strFilter = " Type='" + Type.ToString() + "'";
                checkType = true;
            }

            if (chkName.Checked)
            {
               
                strFilter = " Name Like '%' + '" + txtName.Text + "' + '%'";
                checkName = true;
            }
            //if (chkVol.Checked)
            //{
            //    Volt = Convert.ToInt32(cboVoltagelevel.Text);
            //    if (strFilter != "")
            //    {
            //        strFilter += " AND VoltageLevel='" + Volt + "'";
            //    }
            //    else
            //    {
            //        strFilter = "VoltageLevel='" + Volt + "'";
            //    }
            //    check = true;
            //}
            if (checkType)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dv;
            }
            else
            if (checkName)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dv;
            }
            else
            {
                gvConductor.AutoGenerateColumns = false;
                gvConductor.DataSource = dtMerge;
            }
            ChangeColor();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToConsol();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EConsol consol;
                bool useAccess = false;
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[3].Value) == false)//Access
                {
                    consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = true;
                }
                else
                {
                    consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                }
                Atend.Base.Acad.AcadGlobal.ConsolData.UseAccess = useAccess;
                Atend.Base.Acad.AcadGlobal.ConsolData.eConsol = consol;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.ConsolData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.ConsolData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.ConsolData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());

                AllowClose = true;
            }
            else
            {
                AllowClose = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmDrawHeaderCable02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                AllowClose = true;
            }
        }
 
        private void gvConductor_Click(object sender, EventArgs e)
        {
            Atend.Base.Equipment.EConsol consol;
            if (gvConductor.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[3].Value))
                {
                    consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(
                        gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else
                {
                    consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(
                   gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                txtVoltage.Text = consol.VoltageLevel.ToString();
                if (Convert.ToInt32(consol.ConsolType) == 0)
                {
                    txtType.Text = "کششی";
                }
                if (Convert.ToInt32(consol.ConsolType) == 1)
                {
                    txtType.Text = "انتهایی";
                }
                if (Convert.ToInt32(consol.ConsolType) == 2)
                {
                    txtType.Text = "عبوری";
                }
                if (Convert.ToInt32(consol.ConsolType) == 3)
                {
                    txtType.Text = "DP";
                }
            }
        }

        //Confirmed
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