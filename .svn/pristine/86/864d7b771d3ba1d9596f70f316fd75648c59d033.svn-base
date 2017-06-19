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
    public partial class frmDrawHeaderCable02 : Form
    {

        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable = new Atend.Base.Equipment.EHeaderCabel();
            Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = false;
        }

        public frmDrawHeaderCable02()
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

            if (gvConductor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا سرکابل مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            if (gvConductor.Rows.Count > 0 && gvConductor.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }


            //if (Atend.Base.Acad.AcadGlobal.ConductorData.dBranch.ProductCode == 0)
            //{
            //    MessageBox.Show("لطفا سیم مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

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

        //Confirmed
        private void BindDataToGridMain()
        {
            dtMerge = Atend.Base.Equipment.EHeaderCabel.SelectAllAndMerge();
            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;

            ChangeColor();

        }

        private void frmDrawHeaderCable02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToCboIsExist();
            if (cboVol.Items.Count > 0)
            {
                cboVol.SelectedIndex = 0;
            }
            SetDefaultValues();
        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        public void BindDataToHeaderCable()
        {
            int Voltage = -1;
            string Name = string.Empty;
            bool check = false;
            string strFilter = "";
            if (chkName.Checked)
            {
                Name = txtName.Text;
                strFilter = "Name ='" + Name + "'";
                check = true;
            }
            if (chkVol.Checked)
            {
                Voltage = Convert.ToInt32(cboVol.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Voltage='" + Voltage.ToString() + "'";
                }
                else
                {
                    strFilter = " Voltage='" + Voltage.ToString() + "'";
                }
                check = true;
            }

            //ed.WriteMessage("TypeCode={0},Cross={1},strFilter={2}\n", Name, Voltage, strFilter);
            if (check)
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
            BindDataToHeaderCable();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EHeaderCabel Header;
                if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[3].Value) == false)//Access
                {
                    //MessageBox.Show("لطفا هادی دیگری انتخاب کنید", "انتخاب هادی صحیح نمی باشد ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = true;

                    Header = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(
                        Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value));

                }
                else
                {
                    Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = false;
                    Header = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(
                        new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));

                }

                Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable = Header;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance = status.ACode;
                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmHeaderCabel02 HeaderCabel02 = new Atend.Equipment.frmHeaderCabel02();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(HeaderCabel02);
            BindDataToHeaderCable();
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