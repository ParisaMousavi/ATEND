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
    public partial class frmDrawBreaker02 : Form
    {

        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtMerge = new DataTable();
        bool ForceToClose = false;


        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.BreakerData.eBreaker = new Atend.Base.Equipment.EBreaker();
            Atend.Base.Acad.AcadGlobal.BreakerData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.BreakerData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.BreakerData.UseAccess = false;
        }

        public frmDrawBreaker02()
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
        private void BindDataToMainGrid()
        {

            dtMerge = Atend.Base.Equipment.EBreaker.SelectAllAndMerge();
            gvBreaker.AutoGenerateColumns = false;
            gvBreaker.DataSource = dtMerge;
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

        private void frmDrawBreaker02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToMainGrid();
            BindDataToCboIsExist();
            SetDefaultValues();
        }

        //Confirmed
        public void ChangeColor()
        {
            for (int i = 0; i < gvBreaker.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvBreaker.Rows[i].Cells[2].Value) == false)
                {
                    gvBreaker.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
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

        private void frmDrawBreaker02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private bool Validation()
        {

            if (gvBreaker.Rows.Count > 0 && gvBreaker.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;
            }

            return true;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Name = string.Empty;
            double Amper = -1;
            int type = -1;
            bool check = false;
            string strFilter = "";

            if (chkName.Checked)
            {
                Name = txtName.Text + '%';
                strFilter = " Name LIKE '" + Name + "'";
                check = true;
            }
            if (chkAmper.Checked)
            {
                Amper = Convert.ToDouble(txtAmper.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Amper='" + Amper + "'";
                }
                else
                {
                    strFilter = " Amper='" + Amper + "'";
                }
                check = true;
            }
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvBreaker.AutoGenerateColumns = false;
                gvBreaker.DataSource = dv;
            }
            else
            {
                gvBreaker.AutoGenerateColumns = false;
                gvBreaker.DataSource = dtMerge;
            }
            ChangeColor();
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
                Atend.Base.Equipment.EBreaker breaker;
                bool useAccess = false;
                if (Convert.ToBoolean(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[2].Value) == false)
                {
                    ed.WriteMessage("selected breaker code: {0} \n", gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[1].Value.ToString());
                    breaker = Atend.Base.Equipment.EBreaker.AccessSelectByCode(Convert.ToInt32(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = true;
                }
                else
                {
                    ed.WriteMessage("selected breaker Xcode: {0} \n", gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[0].Value.ToString());
                    breaker = Atend.Base.Equipment.EBreaker.SelectByXCode(new Guid(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = false;
                }
                Atend.Base.Acad.AcadGlobal.BreakerData.UseAccess = useAccess;
                Atend.Base.Acad.AcadGlobal.BreakerData.eBreaker = breaker;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.BreakerData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.BreakerData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.BreakerData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

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