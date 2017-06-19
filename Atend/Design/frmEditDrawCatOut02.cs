using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Design
{
    public partial class frmEditDrawCatOut02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        bool AllowClose = true;
        public Guid NodeCode;
        public ObjectId ObjID;
        bool ForceToClose = false;

        public frmEditDrawCatOut02()
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

        private void frmEditDrawCatOut02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataTocboProjCode();
            dtMerge = Atend.Base.Equipment.ECatOut.SelectAllAndMerge();
            System.Data.DataColumn dcTypeName = new System.Data.DataColumn("TypeName");
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
            BindDataToComboBoxIsExist();

            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            for (int i = 0; i < gvCatOut.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvCatOut.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvCatOut.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvCatOut.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
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

        public void BindDataToComboBoxIsExist()
        {
            //cboIsExist.DisplayMember = "Name";
            //cboIsExist.ValueMember = "Code";
            //cboIsExist.DataSource = Atend.Control.Common.StatuseCode;
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

        private void gvCatOut_Click(object sender, EventArgs e)
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
                    catOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(
                     Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                txtName.Text = catOut.Name;
                txtSelectedAmper.Text = catOut.Amper.ToString();
                txtSelectedVol.Text = catOut.Vol.ToString();
                txtType.Text = gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[6].Value.ToString();

            }
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }


        private bool Validation()
        {
            if (string.IsNullOrEmpty(cboProjCode.Text))
            {
                MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
                return false;
            }

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید", "خطا");
                return false;
            }

            if (gvCatOut.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کت اوت مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }
            return true;
        }

        private void frmEditDrawCatOut02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawCatOut DrawCatOut = new Atend.Global.Acad.DrawEquips.AcDrawCatOut();
            if (Validation())
            {
                if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == false)
                {
                    DrawCatOut.UseAccess = true;
                    DrawCatOut.ECatOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value))
                {
                    DrawCatOut.UseAccess = false;
                    DrawCatOut.ECatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));

                }
                DrawCatOut.Existance = Convert.ToByte(cboIsExist.SelectedValue);
                DrawCatOut.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                

                DrawCatOut.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);

                if (DrawCatOut.UpdateCatOutData(dpack.Code))
                {
                    ed.WriteMessage("Update CatOut Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }
        }


    }
}