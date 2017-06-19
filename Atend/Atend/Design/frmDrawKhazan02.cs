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
    public partial class frmDrawKhazan02 : Form
    {
        bool AllowClose = true;
        DataTable dtMerge = new DataTable();
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.KhazanData.eKhazanTip = new Atend.Base.Equipment.EKhazanTip();
            Atend.Base.Acad.AcadGlobal.KhazanData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.KhazanData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.KhazanData.UseAccess = false;
        }

        public frmDrawKhazan02()
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
            dtMerge = Atend.Base.Equipment.EKhazanTip.SelectAllAndMerge();
            BindDataToComboBoxIsExist();

            gvBankKhazan.AutoGenerateColumns = false;
            gvBankKhazan.DataSource = dtMerge;

            gvKhazan.AutoGenerateColumns = false;

            //BindDataTocboProjCode();
            ChangeColor();

        }

        private void frmDrawKhazan_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            ResetClass();
            BindDataToGridMain();
            BindDataToComboBoxIsExist();
            SetDefaultValues();
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvBankKhazan.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvBankKhazan.Rows[i].Cells[4].Value) == false)
                {
                    gvBankKhazan.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
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
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
            //cboIsExist.SelectedIndex = 4;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txName.Text))
            {
                string strFilter = txName.Text + '%';
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = " Name Like '" + strFilter + "'";
                gvBankKhazan.DataSource = dv;
                gvKhazan.DataSource = null;
            }
            else
            {
                gvBankKhazan.DataSource = dtMerge;
                gvKhazan.DataSource = null;
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

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (gvBankKhazan.SelectedRows.Count == 0 && gvKhazan.Rows.Count == 0)
            {
                return false;
            }

            if (gvBankKhazan.Rows.Count > 0 && gvBankKhazan.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;


            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EKhazanTip khazantip;
                bool useAccess = false;
                //int Existance = 0;
                if (Convert.ToBoolean(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[4].Value) == true)
                {
                    khazantip = Atend.Base.Equipment.EKhazanTip.SelectByXCode(new Guid(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                }
                else
                {
                    khazantip = Atend.Base.Equipment.EKhazanTip.AccessSelectByCode(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[3].Value));
                    useAccess = true;
                }
                Atend.Base.Acad.AcadGlobal.KhazanData.eKhazanTip = khazantip;
                Atend.Base.Acad.AcadGlobal.KhazanData.UseAccess = useAccess;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.KhazanData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.KhazanData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.KhazanData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                if (cboProjCode.Items.Count != 0)
                    Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                else
                    Atend.Control.Common.ProjectCode = -1;

                //Atend.Base.Design.DPackage dPackge;
                //DataTable dt = (DataTable)gvKhazan.DataSource;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dPackge = new Atend.Base.Design.DPackage();
                //    dPackge.Count = Convert.ToInt32(dr["Count"]);
                //    //dPackge.ProductCode = Convert.ToInt32(dr["ProductCode"]);
                //    dPackge.Code = new Guid(dr["ProductCode"].ToString());
                //    //////dPackge.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);
                //    /*Atend.Base.Acad.AcadGlobal.KhazanData.dPackageForKhazan.Add(dPackge);*/
                //}

                AllowClose = true;
            }
            else
            {
                AllowClose = false;
            }


        }

        private void frmDrawKhazan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AllowClose)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void gvBankKhazan_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Convert.ToBoolean(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[4].Value) == true)
            {
                gvKhazan.DataSource = Atend.Base.Equipment.EKhazan.DrawSearchX(new Guid(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[0].Value.ToString()));

            }
            else
            {

                System.Data.DataTable st = Atend.Base.Equipment.EKhazanTip.AccessDrawSearch(Convert.ToInt32(gvBankKhazan.Rows[gvBankKhazan.CurrentRow.Index].Cells[3].Value.ToString()), (int)Atend.Control.Enum.ProductType.BankKhazan);
                gvKhazan.AutoGenerateColumns = false;
                gvKhazan.DataSource = st;

            }

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