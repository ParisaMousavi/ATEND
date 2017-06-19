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
    public partial class frm : Form
    {
        DataTable dtMerge = new DataTable();
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBox = new Atend.Base.Equipment.EStreetBox();
            Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBoxPhuse = new List<Atend.Base.Equipment.EStreetBoxPhuse>();
            Atend.Base.Acad.AcadGlobal.StreetBoxData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.StreetBoxData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.StreetBoxData.UseAccess = false;
        }

        public frm()
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
            //Atend.Base.Acad.AcadGlobal.StreetBoxData.StreetBoxProductCode = 0;
        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboProjCode1.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            //if (Atend.Base.Acad.AcadGlobal.StreetBoxData.StreetBoxProductCode == 0)
            //{
            //    MessageBox.Show("لطفا شالتر مورد نظر را انتخاب نمایید", "خطا");
            //    return false;
            //}
            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
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

            if (checkBox2.Checked)
            {
                iCount = Convert.ToInt32(numericUpDown1.Value);
                strFilter = " InputCount='" + iCount + "'";
                check = true;
            }
            if (checkBox1.Checked)
            {
                oCount = Convert.ToInt32(numericUpDown2.Value);
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
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dv;
            }
            else
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dtMerge;
            }
            ChangeColor();



            //int input = -1, output = -1;
            //if (chkInput.Checked)
            //    input = Convert.ToInt16(nudInputCount.Value);
            //if (chkOutput.Checked)
            //    output = Convert.ToInt16(nudOutputCount.Value);

            //DataTable StreetBoxTbl = Atend.Base.Equipment.EStreetBox.SelectByInputOutputCode(-1, input, output);

            //gvStreetBox02.AutoGenerateColumns = false;
            //gvStreetBox02.DataSource = StreetBoxTbl;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Atend.Base.Equipment.EStreetBox streetbox;
                List<Atend.Base.Equipment.EStreetBoxPhuse> list = new List<Atend.Base.Equipment.EStreetBoxPhuse>();
                bool useAccess = false;
                if (Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) == false)
                {
                    streetbox = Atend.Base.Equipment.EStreetBox.AccessSelectByCode(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()));
                    useAccess = true;
                    DataTable dt = Atend.Base.Equipment.EStreetBoxPhuse.AccessSelectByStreetBoxCode(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EStreetBoxPhuse.AccessSelectByCode(Convert.ToInt32(dr["StreetBoxCode"].ToString())));
                    }
                }
                else //if (Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value) == true)
                {
                    streetbox = Atend.Base.Equipment.EStreetBox.SelectByXCode(new Guid(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()));
                    useAccess = false;
                    DataTable dt = Atend.Base.Equipment.EStreetBoxPhuse.SelectByStreetBoxXCode(new Guid(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EStreetBoxPhuse.SelectByXCode(new Guid(dr["XCode"].ToString())));
                    }
                }

                Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBoxPhuse = list;
                Atend.Base.Acad.AcadGlobal.StreetBoxData.UseAccess = useAccess;
                Atend.Base.Acad.AcadGlobal.StreetBoxData.eStreetBox = streetbox;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist1.SelectedValue));
                Atend.Base.Acad.AcadGlobal.StreetBoxData.Existance = status.ACode;

                if (cboProjCode1.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.StreetBoxData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.StreetBoxData.ProjectCode = Convert.ToInt32(cboProjCode1.SelectedValue);

                Atend.Control.Common.IsExist = Convert.ToInt32(cboIsExist1.SelectedValue.ToString());
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

        private void frmDrawStreetBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        public void BindDataTocboProjCode1()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode1.DisplayMember = "Name";
            //cboProjCode1.ValueMember = "Code";
            //cboProjCode1.DataSource = dt;
        }

        private void gvStreetBox02_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //Atend.Base.Acad.AcadGlobal.StreetBoxData.StreetBoxProductCode = Convert.ToInt16(gvStreetBox02.SelectedRows[0].Cells[0].Value.ToString());
                //Atend.Base.Acad.AcadGlobal.StreetBoxProductCode = new Guid(gvStreetBox02.SelectedRows[0].Cells[0].Value.ToString());
            }

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
            dtMerge = Atend.Base.Equipment.EStreetBox.SelectAllAndMerge();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtMerge;
            ChangeColor();
        }

        private void frmDrawStreetBox02_Load(object sender, EventArgs e)
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
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value) == false)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        //confirmed
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
                //cboIsExist.SelectedIndex = i;
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