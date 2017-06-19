using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Global.Design
{
    public partial class frmEditDrawKablSho : Form
    {


        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        public bool HeaderIsSelected = false;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataColumn TypeName = new DataColumn("Name", typeof(string));
        DataColumn TypeCode = new DataColumn("Code", typeof(int));

        DataColumn MaterialName = new DataColumn("Name");
        DataColumn MaterialCode = new DataColumn("Code");

        public DataTable TypeTbl = new DataTable();
        public DataTable MaterialTbl = new DataTable();
        bool ForceToClose = false;


        public frmEditDrawKablSho()
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
            TypeTbl.Columns.Add(TypeCode);
            TypeTbl.Columns.Add(TypeName);

            DataRow dr1 = TypeTbl.NewRow();
            dr1["Name"] = "پرسی";
            dr1["Code"] = 1;
            DataRow dr2 = TypeTbl.NewRow();
            dr2["Name"] = "بیمتال";
            dr2["Code"] = 2;

            TypeTbl.Rows.Clear();
            TypeTbl.Rows.Add(dr1);
            TypeTbl.Rows.Add(dr2);


            MaterialTbl.Columns.Add(MaterialCode);
            MaterialTbl.Columns.Add(MaterialName);

            DataRow dr3 = MaterialTbl.NewRow();
            dr3["Name"] = "مسی";
            dr3["Code"] = 1;
            DataRow dr4 = MaterialTbl.NewRow();
            dr4["Name"] = "الومینیومی";
            dr4["Code"] = 2;


            MaterialTbl.Rows.Add(dr3);
            MaterialTbl.Rows.Add(dr4);
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

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید", "خطا");
                return false;
            }

            if (gvConductor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کابلشو مورد نظر را انتخاب نمایید", "خطا");
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
            dtMerge = Atend.Base.Equipment.EKablsho.SelectAllAndMerge();
            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = dtMerge;
            ChangeColor();

        }

        private void frmDrawKablSho_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToGridMain();
            BindDataToCboIsExist();
            BindDataToCboMaterial();
            BindDataToCboType();
            SetDefaultValues();

        }

        public void BindDataToCboType()
        {
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "Code";
            cboType.DataSource = TypeTbl;
        }

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        public void BindDataToCboMaterial()
        {
            cboMaterial.DisplayMember = "Name";
            cboMaterial.ValueMember = "Code";
            cboMaterial.DataSource = MaterialTbl;
        }

        public void BindDataToKablsho()
        {
            int Type = -1;
            int Material = -1;

            bool check = false;
            string strFilter = "";

            if (chkType.Checked)
            {
                Type = Convert.ToInt32(cboType.SelectedValue);
                strFilter = " Type='" + Type.ToString() + "'";
                check = true;
            }
            if (chkMaterial.Checked)
            {
                Material = Convert.ToInt32(cboMaterial.SelectedValue);
                if (strFilter != "")
                {
                    strFilter += " AND Material='" + Material.ToString() + "'";

                }
                else
                {
                    strFilter = " Material='" + Material.ToString() + "'";
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
            BindDataToKablsho();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (HeaderIsSelected == false)
                {
                    Atend.Base.Equipment.EKablsho kablsho;
                    if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[3].Value) == false)//Access
                    {
                        kablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value));
                        Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess = true;

                    }
                    else
                    {
                        kablsho = Atend.Base.Equipment.EKablsho.SelectByXCode(new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));
                        Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess = false;
                    }

                    Atend.Base.Acad.AcadGlobal.KablshoData.eKablsho = kablsho;
                    Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                    Atend.Base.Acad.AcadGlobal.KablshoData.Existance = status.ACode;

                    if (cboProjCode.Items.Count == 0)
                        Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode = 0;
                    else
                        Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                    Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist.SelectedValue.ToString());
                    if (cboProjCode.Items.Count != 0)
                        Atend.Control.Common.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue.ToString());
                    else
                        Atend.Control.Common.ProjectCode = -1;

                    AllowClose = true;
                }
                else
                {
                    Atend.Base.Equipment.EHeaderCabel Header;
                    if (Convert.ToBoolean(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[3].Value) == false)//Access
                    {
                        Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = true;
                        Header = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(Convert.ToInt32(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[0].Value));

                    }
                    else
                    {
                        Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = false;
                        Header = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(new Guid(gvConductor.Rows[gvConductor.CurrentRow.Index].Cells[1].Value.ToString()));

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

        private void frmDrawKablSho_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnWeekHeader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (HeaderIsSelected == false)
            {
                HeaderIsSelected = true;
                btnWeekHeader.Text = "انتخاب کابلشو";
                dtMerge = Atend.Base.Equipment.EHeaderCabel.SelectAllAndMerge();
                gvConductor.AutoGenerateColumns = true;
                gvConductor.DataSource = dtMerge;
                ChangeColor();
            }
            else
            {
                HeaderIsSelected = false;
                btnWeekHeader.Text = "انتخاب سرکابل فشارضعیف";
                dtMerge = Atend.Base.Equipment.EKablsho.SelectAllAndMerge();
                gvConductor.AutoGenerateColumns = true;
                gvConductor.DataSource = dtMerge;
                ChangeColor();

            }

        }

        //Confirmed
        private void cboIsExist_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;

        }



    }
}