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
    public partial class frmEditDrawCatout03 : Form
    {
        public Guid NodeCode;
        bool AllowClose = true;
        public ObjectId ObjID;
        int selectedProductCode = -1;

        System.Data.DataTable dtMerge = new System.Data.DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        bool AllowToclose = true;
        bool ForceToClose = false;

        public frmEditDrawCatout03()
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
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
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
                    //DataView dv = new DataView();
                    //dv.Table = dtMerge;
                    //dv.RowFilter = strFilter;
                    dtMerge.DefaultView.RowFilter = strFilter;
                    gvCatOut.AutoGenerateColumns = false;
                    gvCatOut.DataSource = dtMerge;
                }
                else
                {
                    dtMerge.DefaultView.RowFilter = "";
                    //gvCatOut.AutoGenerateColumns = false;
                    //gvCatOut.DataSource =dtMerge;
                }
                Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvCatOut, this);
                ChangeColor();
                for (int i = 0; i < gvCatOut.Rows.Count; i++)
                {
                    if (Convert.ToInt32(gvCatOut.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvCatOut.Rows[i].Cells[2].Value.ToString()) == false)
                    {
                        gvCatOut.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawCatOut DrawCatout = new Atend.Global.Acad.DrawEquips.AcDrawCatOut();
            Atend.Base.Design.DKeyStatus keystatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(NodeCode);
            if (Validation())
            {
                if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == false)
                {
                    DrawCatout.UseAccess = true;
                    DrawCatout.ECatOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value))
                {
                    DrawCatout.UseAccess = false;
                    DrawCatout.ECatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawCatout.Existance = status.ACode;


                if (rdbClose.Checked)
                {
                    keystatus.IsClosed = true;
                }
                else
                {
                    keystatus.IsClosed = false;
                }
                DrawCatout.DKeyStatus = keystatus;


                if (cboProjCode.Items.Count == 0)
                    DrawCatout.ProjectCode = 0;
                else
                    DrawCatout.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawCatout.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawCatout.UpdateCatOutData(dpack.Code))
                {
                    ed.WriteMessage("Update Rod Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }

           
            //if (Validation())
            //{
            //    if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == true)//Local
            //    {
            //        Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));
            //        Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = false;
            //    }
            //    else//Access
            //    {
            //        Atend.Base.Acad.AcadGlobal.CatOutData.eCatOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
            //        Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = true;
            //    }
            //    Atend.Base.Acad.AcadGlobal.CatOutData.Existance =Convert.ToByte(cboIsExist.SelectedValue);
            //    Atend.Base.Acad.AcadGlobal.CatOutData.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

            //    AllowToclose = true;
            //}
            //else
            //{
            //    AllowToclose = false;
            //}
           
        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {

            //Atend.Base.Equipment.ECatOut catOut;
            //if (gvCatOut.Rows.Count > 0)
            //{
            //    if (Convert.ToBoolean(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[2].Value) == true)
            //    {
            //        catOut = Atend.Base.Equipment.ECatOut.SelectByXCode(
            //           new Guid(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[0].Value.ToString()));
            //    }
            //    else
            //    {
            //        //ed.WriteMessage("AccessSelect\n");
            //        //ed.WriteMessage("Code={0}\n",gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString());
            //        catOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(
            //         Convert.ToInt32(gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[1].Value.ToString()));
            //    }
            //    txtName.Text = catOut.Name;
            //    txtSelectedAmper.Text = catOut.Amper.ToString();
            //    txtSelectedVol.Text = catOut.Vol.ToString();
            //    txtType.Text = gvCatOut.Rows[gvCatOut.CurrentRow.Index].Cells[6].Value.ToString();

            //}

        }

        private void frmDrawDisconnector_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

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
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            Atend.Base.Design.DKeyStatus keystatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(dpack.Code);
            if (keystatus.IsClosed)
            {
                rdbClose.Checked = true;
            }
            else
            {
                rdbOpen.Checked = true;
            }


            gvCatOut.AutoGenerateColumns = false;
            gvCatOut.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvCatOut, this);
            ChangeColor();
            for (int i = 0; i < gvCatOut.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvCatOut.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvCatOut.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvCatOut.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
            BindDataToComboBoxIsExist();
            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

            
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember="Name";
            cboIsExist.ValueMember="Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
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

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }

    }
}