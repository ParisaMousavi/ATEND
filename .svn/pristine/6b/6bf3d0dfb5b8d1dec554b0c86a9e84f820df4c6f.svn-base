using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Design
{
    public partial class frmDrawDisconnector02 : Form
    {

        DataTable dtMerge = new DataTable();
        bool AllowToclose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.DisConnectorData.eDisConnector = new Atend.Base.Equipment.EDisconnector();
            Atend.Base.Acad.AcadGlobal.DisConnectorData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.DisConnectorData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.DisConnectorData.UseAccess = false;
        }

        public frmDrawDisconnector02()
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

        private bool Validation()
        {
            bool flag = true;

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (!string.IsNullOrEmpty(txtAmper.Text))
            {
                try
                {
                    Convert.ToSingle(txtAmper.Text);

                }
                catch
                {
                    MessageBox.Show("لطفاً آمپراژ را با فرمت صحیح وارد نمایید");
                    flag = false;
                }
            }

            if (gvDisconnector.Rows.Count > 0 && gvDisconnector.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }



            return flag;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strFilter = "";
            bool check = false;

            Single amper = -1;

            if (!string.IsNullOrEmpty(txtAmper.Text))
            {
                amper = Convert.ToSingle(txtAmper.Text);
                strFilter = " Amper='" + amper + "'";
                check = true;
            }

            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                //gvDisconnector.AutoGenerateColumns = false;
                dtMerge.DefaultView.RowFilter = strFilter;

                gvDisconnector.DataSource = dtMerge;
            }
            else
            {
                //gvDisconnector.AutoGenerateColumns = false;
                //gvDisconnector.DataSource =dtMerge;
                dtMerge.DefaultView.RowFilter = "";

            }
            ChangeColor();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)//Local
                {
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.eDisConnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.UseAccess = false;
                }
                else if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == false)//Access
                {
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.eDisConnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.UseAccess = true;
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.DisConnectorData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.DisConnectorData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                AllowToclose = true;
            }
            else
            {
                AllowToclose = false;
            }

        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            ed.WriteMessage("Click\n");
            if (gvDisconnector.Rows.Count > 0)
            {
                Atend.Base.Equipment.EDisconnector disconnector;
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(
                        new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(
                       Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                txtName.Text = disconnector.Name;
                txtSelectedAmper.Text = disconnector.Amper.ToString();
                txtType.Text = gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[5].Value.ToString();
            }

        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EDisconnector.SelectAllAndMerge();
            DataColumn dcTypeName = new DataColumn("TypeName");
            dtMerge.Columns.Add(dcTypeName);
            foreach (DataRow dr in dtMerge.Rows)
            {
                if (Convert.ToInt32(dr["Type"].ToString()) == 1)
                {
                    dr["TypeName"] = "سکسیونر هوایی غیر قابل قطع زیر بار";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "سکسیونر هوایی قابل قطع زیر بار";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 3)
                {
                    dr["TypeName"] = "سکسیونر SF6 قابل قطع زیر بار";
                }
            }

            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;
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

        private void frmDrawDisconnector_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToComboBoxIsExist();
            SetDefaultValues();
        }

        //Confirmed
        public void BindDataToComboBoxIsExist()
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
            //cboIsExist.ValueMember = "Code" ;
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedIndex = 4;
        }

        private void frmDrawDisconnector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void gvDisconnector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvDisconnector_Click_1(object sender, EventArgs e)
        {
            if (gvDisconnector.Rows.Count > 0)
            {
                Atend.Base.Equipment.EDisconnector disconnector;
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(
                        new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(
                       Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                txtName.Text = disconnector.Name;
                txtSelectedAmper.Text = disconnector.Amper.ToString();
                txtType.Text = gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[5].Value.ToString();
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