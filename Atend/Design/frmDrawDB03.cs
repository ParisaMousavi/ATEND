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
    public partial class frmDrawDB03 : Form
    {

        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.DBData.eDB = new Atend.Base.Equipment.EDB();
            Atend.Base.Acad.AcadGlobal.DBData.eDBPhuse = new List<Atend.Base.Equipment.EDBPhuse>();
            Atend.Base.Acad.AcadGlobal.DBData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.DBData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.DBData.UseAccess = false;
        }

        public frmDrawDB03()
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
            //Atend.Base.Acad.AcadGlobal.DBData.DBProductCode = 0;
        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            //if (Atend.Base.Acad.AcadGlobal.DBData.DBProductCode == 0)
            //{
            //    MessageBox.Show("لطفا شالتر مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}

            if (gvDB.Rows.Count > 0 && gvDB.SelectedRows.Count > 0)
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
            int iCount = -1;
            int oCount = -1;
            bool check = false;
            string strFilter = "";

            if (chkInput.Checked)
            {
                iCount = Convert.ToInt32(nudInputCount.Value);
                strFilter = " InputCount='" + iCount + "'";
                check = true;
            }
            if (chkOutput.Checked)
            {
                oCount = Convert.ToInt32(nudOutputCount.Value);
                if (strFilter != "")
                {
                    strFilter += " AND OutputCount='" + oCount + "'";
                }
                else
                {
                    strFilter = " OutputCount='" + oCount + "'";
                }
                check = true;
            }
            if (check)
            {
                DataView dv = new DataView();
                dv.Table = dtMerge;
                dv.RowFilter = strFilter;
                gvDB.AutoGenerateColumns = false;
                gvDB.DataSource = dv;
            }
            else
            {
                gvDB.AutoGenerateColumns = false;
                gvDB.DataSource = dtMerge;
            }
            ChangeColor();



            //int input = -1, output = -1;
            //if (chkInput.Checked)
            //    input = Convert.ToInt16(nudInputCount.Value);
            //if (chkOutput.Checked)
            //    output = Convert.ToInt16(nudOutputCount.Value);

            //DataTable DBTbl = Atend.Base.Equipment.EDB.SelectByInputOutputCode(-1, input, output);

            //gvDB02.AutoGenerateColumns = false;
            //gvDB02.DataSource = DBTbl;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EDB DB;
                List<Atend.Base.Equipment.EDBPhuse> list = new List<Atend.Base.Equipment.EDBPhuse>();
                bool useAccess = false;
                if (Convert.ToBoolean(gvDB.Rows[gvDB.CurrentRow.Index].Cells[5].Value) == false)
                {
                    DB = Atend.Base.Equipment.EDB.AccessSelectByCode(Convert.ToInt32(gvDB.Rows[gvDB.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = true;
                    DataTable dt = Atend.Base.Equipment.EDBPhuse.AccessSelectByDBCode(Convert.ToInt32(gvDB.Rows[gvDB.CurrentRow.Index].Cells[0].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EDBPhuse.AccessSelectByCode(Convert.ToInt32(dr["DBCode"].ToString())));
                    }
                }
                else //if (Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) == true)
                {
                    DB = Atend.Base.Equipment.EDB.SelectByXCode(new Guid(gvDB.Rows[gvDB.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                    DataTable dt = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(new Guid(gvDB.Rows[gvDB.CurrentRow.Index].Cells[1].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EDBPhuse.SelectByXCode(new Guid(dr["XCode"].ToString())));
                    }
                }

                Atend.Base.Acad.AcadGlobal.DBData.eDBPhuse = list;
                Atend.Base.Acad.AcadGlobal.DBData.UseAccess = useAccess;
                Atend.Base.Acad.AcadGlobal.DBData.eDB = DB;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.DBData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.DBData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.DBData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

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

        private void frmDrawDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
                AllowClose = true;
            }
        }

        public void BindDataTocboProjCode1()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void gvDB02_Click(object sender, EventArgs e)
        {
            if (gvDB.SelectedRows.Count > 0)
            {
                //Atend.Base.Acad.AcadGlobal.DBData.DBProductCode = Convert.ToInt16(gvDB02.SelectedRows[0].Cells[0].Value.ToString());
                //Atend.Base.Acad.AcadGlobal.DBProductCode = new Guid(gvDB02.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.EDB.SelectAllAndMerge();
            gvDB.AutoGenerateColumns = false;
            gvDB.DataSource = dtMerge;
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

        private void frmDrawDB02_Load(object sender, EventArgs e)
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
            for (int i = 0; i < gvDB.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDB.Rows[i].Cells[5].Value) == false)
                {
                    gvDB.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //Confirmed
        public void BindDataTocboIsExist1()
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