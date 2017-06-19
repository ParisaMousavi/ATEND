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
    public partial class frmDrawRod02 : Form
    {
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtMerge = new DataTable();
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.RodData.eRod = new Atend.Base.Equipment.ERod();
            Atend.Base.Acad.AcadGlobal.RodData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.RodData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.RodData.UseAccess = false;
        }

        public frmDrawRod02()
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
            for (int i = 0; i < gvRod.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvRod.Rows[i].Cells[2].Value) == false)
                {
                    gvRod.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            double Vol = -1;
            double Amper = -1;
            int type = -1;
            bool check = false;
            string strFilter = "";

            if (chkVol.Checked)
            {
                Vol = Convert.ToDouble(txtVolt.Text);
                strFilter = " Vol='" + Vol + "'";
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
                gvRod.AutoGenerateColumns = false;
                gvRod.DataSource = dv;
            }
            else
            {
                gvRod.AutoGenerateColumns = false;
                gvRod.DataSource = dtMerge;
            }
            ChangeColor();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.ERod rod;
                bool useAccess = false;
                //int Existance = 0;
                if (Convert.ToBoolean(gvRod.Rows[gvRod.CurrentRow.Index].Cells[2].Value) == false)
                {
                    rod = Atend.Base.Equipment.ERod.AccessSelectByCode(Convert.ToInt32(gvRod.Rows[gvRod.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = true;
                }
                else
                {
                    rod = Atend.Base.Equipment.ERod.SelectByXCode(new Guid(gvRod.Rows[gvRod.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = false;
                }
                Atend.Base.Acad.AcadGlobal.RodData.UseAccess = useAccess;
                Atend.Base.Acad.AcadGlobal.RodData.eRod = rod;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.RodData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.RodData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.RodData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

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

        private bool Validation()
        {
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            //if (Atend.Base.Acad.AcadGlobal.RodData.dPackageForRod.ProductCode == 0)
            //{
            //    MessageBox.Show("لطفا برق گیر مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

            if (gvRod.Rows.Count > 0 && gvRod.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }


            return true;

        }

        private void frmDrawRod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void gvRod_Click(object sender, EventArgs e)
        {
            //ed.WriteMessage("gvRod=" + gvRod.CurrentRow.Index + "\n");
            if (gvRod.Rows.Count > 0)
            {


            }
        }

        private void gvRod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            dtMerge = Atend.Base.Equipment.ERod.SelectAllAndMerge();
            gvRod.AutoGenerateColumns = false;
            gvRod.DataSource = dtMerge;

            ChangeColor();
        }

        private void frmDrawRod_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToCboIsExist();
            //BindDataTocboProjCode();
            SetDefaultValues();
        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
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
                //cboIsExist.SelectedIndex = i;
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