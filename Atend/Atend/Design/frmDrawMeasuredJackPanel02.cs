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
    public partial class frmDrawMeasuredJackPanel02 : Form
    {
        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.eMeasuredJackPanel = new Atend.Base.Equipment.EMeasuredJackPanel();
            Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.UseAccess = false;
        }

        public frmDrawMeasuredJackPanel02()
        {
            if (!Atend.Global.Acad.DrawEquips.Dicision.IsHere())
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
            InitializeComponent();
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

        //Confirmed
        private void BindDataToGridMain()
        {
            dtMerge = Atend.Base.Equipment.EMeasuredJackPanel.SelectAllAndMerge();
            gvMeasuredJackPanel.AutoGenerateColumns = false;
            gvMeasuredJackPanel.DataSource = dtMerge;
            ChangeColor();
        }

        private void frmMeasuredJackPanel02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataTocboIsExist1();
            SetDefaultValues();
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

        private void frmDrawMeasuredJackPanel02_FormClosing(object sender, FormClosingEventArgs e)
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

            if (gvMeasuredJackPanel.Rows.Count > 0 && gvMeasuredJackPanel.SelectedRows.Count > 0)
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
                Atend.Base.Equipment.EMeasuredJackPanel MeasuredJackPanel = new Atend.Base.Equipment.EMeasuredJackPanel();
                bool useAccess = false;
                if (Convert.ToBoolean(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[4].Value) == false)
                {
                    MeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.AccessSelectByCode(Convert.ToInt32(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = true;
                }
                else if (Convert.ToBoolean(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[4].Value) == true)
                {
                    MeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.SelectByXCode(new Guid(gvMeasuredJackPanel.Rows[gvMeasuredJackPanel.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                }

                Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.eMeasuredJackPanel = MeasuredJackPanel;
                Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.UseAccess = useAccess;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist1.SelectedValue));
                Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.Existance = status.ACode;

                if (cboProjCode1.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.MeasuredJackPanelData.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue);

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
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvMeasuredJackPanel.AutoGenerateColumns = false;
                gvMeasuredJackPanel.DataSource = dv;
            }
            else
            {
                gvMeasuredJackPanel.AutoGenerateColumns = false;
                gvMeasuredJackPanel.DataSource = dtMerge;
            }
            ChangeColor();
        }

        //Confirmed
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