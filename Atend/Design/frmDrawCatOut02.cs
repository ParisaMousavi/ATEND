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
    public partial class frmDrawCatOut02 : Form
    {
        //Atend.Control.Common.NodeInformation mynodeinformation = new Atend.Control.Common.NodeInformation();
        DataTable dtMerge = new DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool AllowToclose = true;
        bool ForceToClose = false;

        private void ResetClass()
        {
            Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut = new Atend.Base.Equipment.ECatOut();
            Atend.Base.Acad.AcadGlobal.CatOutData.Existance = 0;
            Atend.Base.Acad.AcadGlobal.CatOutData.ProjectCode = 0;
            Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = false;
        }

        public frmDrawCatOut02()
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

        public void BindDataTocboProjCode()
        {
            //DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvCatOut.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvCatOut.Rows[i].Cells[2].Value) == false)
                {
                    gvCatOut.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
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

            if (gvCatOut.Rows.Count > 0 && gvCatOut.SelectedRows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;




            //if (!string.IsNullOrEmpty(txtAmper.Text))
            //{
            //    try
            //    {
            //        Convert.ToSingle(txtAmper.Text);

            //    }
            //    catch
            //    {
            //        MessageBox.Show("لطفاً آمپراژ را با فرمت صحیح وارد نمایید");
            //        flag = false;
            //    }
            //}

            //if (!string.IsNullOrEmpty(cboVol.Text))
            //{
            //    try
            //    {
            //        Convert.ToSingle(cboVol.Text);

            //    }
            //    catch
            //    {
            //        MessageBox.Show("لطفاً ولتاژ را با فرمت صحیح وارد نمایید");
            //        flag = false;
            //    }
            //}
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Single amper = -1;
                Single Vol = -1;
                bool Check = false;
                string strFilter = "";

                if (chkAmper.Checked)
                {
                    if (!string.IsNullOrEmpty(txtAmper.Text))
                    {
                        amper = Convert.ToSingle(txtAmper.Text);
                        strFilter = " Amper='" + amper + "'";
                        Check = true;
                    }

                }
                if (chkVol.Checked)
                {
                    if (!string.IsNullOrEmpty(cboVol.Text))
                    {

                        Vol = Convert.ToSingle(cboVol.Text);
                        if (strFilter == "")
                        {
                            strFilter = " Vol='" + Vol + "'";
                        }
                        else
                        {
                            strFilter = " AND Vol='" + Vol + "'";
                        }
                        Check = true;
                    }
                }

                if (Check)
                {
                    DataView dv = new DataView();
                    dv.Table = dtMerge;
                    dv.RowFilter = strFilter;
                    gvCatOut.AutoGenerateColumns = false;
                    gvCatOut.DataSource = dv;
                }
                else
                {
                    gvCatOut.AutoGenerateColumns = false;
                    gvCatOut.DataSource = dtMerge;
                }
                ChangeColor();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == true)//Local
                {
                    Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = false;
                }
                else//Access
                {
                    Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
                    Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = true;
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                Atend.Base.Acad.AcadGlobal.CatOutData.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    Atend.Base.Acad.AcadGlobal.CatOutData.ProjectCode = 0;
                else
                    Atend.Base.Acad.AcadGlobal.CatOutData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

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

            Atend.Base.Equipment.ECatOut catOut;
            if (gvCatOut.Rows.Count > 0)
            {
                if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == true)
                {
                    catOut = Atend.Base.Equipment.ECatOut.SelectByXCode(
                       new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else
                {
                    //ed.WriteMessage("AccessSelect\n");
                    //ed.WriteMessage("Code={0}\n",gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString());
                    catOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(
                     Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                txtName.Text = catOut.Name;
                txtSelectedAmper.Text = catOut.Amper.ToString();
                txtSelectedVol.Text = catOut.Vol.ToString();
                txtType.Text = gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[6].Value.ToString();
            }
        }

        //Confirmed
        private void BindDataToGridMain()
        {

            dtMerge = Atend.Base.Equipment.ECatOut.SelectAllAndMerge();
            DataColumn dcTypeName = new DataColumn("TypeName");
            dtMerge.Columns.Add(dcTypeName);

            foreach (DataRow dr in dtMerge.Rows)
            {
                if (Convert.ToInt32(dr["Type"].ToString()) == 1)
                {
                    dr["TypeName"] = "کات اوت نوع T";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "کات اوت نوع K";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "کات اوت نوع KT";
                }
            }
            gvCatOut.AutoGenerateColumns = false;
            gvCatOut.DataSource = dtMerge;

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

            //cboIsExist.DisplayMember="Name";
            //cboIsExist.ValueMember="Code";
            //cboIsExist.DataSource= Atend.Control.Common.StatuseCode;
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