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
    public partial class frmEditDrawRod02 : Form
    {

        bool AllowClose = true;
        //public int Code;
        public Guid NodeCode;
        public Guid DpakageCode;
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public ObjectId ObjID;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        
        public frmEditDrawRod02()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            double Vol = -1;
            double Amper = -1;
            string strFilter = "";
            bool check = false;

            if (chkVol.Checked)
            {
                Vol = Convert.ToInt32(cboVol.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Vol='" + Vol.ToString() + "'";
                }
                else
                {
                    strFilter = " Vol='" + Vol.ToString() + "'";
                }
                check = true;
            }
            if (chkAmper.Checked)
            {
                Amper = Convert.ToDouble(txtAmper.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Amper='" + Amper.ToString() + "'";
                }
                else
                {
                    strFilter = " Amper='" + Amper.ToString() + "'";
                }
                check = true;
            }
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvRod.AutoGenerateColumns = false;
                gvRod.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvRod.AutoGenerateColumns = false;
                //gvRod.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvRod, this);
            ChangeColor();
            for (int i = 0; i < gvRod.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvRod.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvRod.Rows[i].Cells[5].Value.ToString()) == false)
                {
                    gvRod.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }


            //if (chkVol.Checked)
            //    Vol = Convert.ToDouble(txtVolt.Text);
            //if (chkAmper.Checked)
            //    Amper = Convert.ToDouble(txtAmper.Text);
            //gvRod.AutoGenerateColumns = false;
            //gvRod.DataSource = Atend.Base.Equipment.ERod.DrawSearch(Vol, Amper);
            //for (int i = 0; i < gvRod.Rows.Count; i++)
            //{
            //    if (int.Parse(gvRod.Rows[i].Cells["Column1"].Value.ToString()) == Code)
            //    {
            //        gvRod.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }

        private void frmEditDrawRod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
            }
        }

        private bool Validation()
        {

            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید", "خطا");
                return false;
            }

            if (gvRod.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا برقگیر مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawRod DrawRod = new Atend.Global.Acad.DrawEquips.AcDrawRod();
            if (Validation())
            {
                if (Convert.ToBoolean(gvRod.Rows[gvRod.CurrentRow.Index].Cells[5].Value) == false)
                {
                    DrawRod.UseAccess = true;
                    DrawRod.ERod = Atend.Base.Equipment.ERod.AccessSelectByCode(Convert.ToInt32(gvRod.Rows[gvRod.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvRod.Rows[gvRod.CurrentRow.Index].Cells[5].Value))
                {
                    DrawRod.UseAccess = false;
                    DrawRod.ERod= Atend.Base.Equipment.ERod.SelectByXCode(new Guid(gvRod.Rows[gvRod.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawRod.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawRod.ProjectCode = 0;
                else
                    DrawRod.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawRod.SelectedObjectId = ObjID;
                ed.WriteMessage("SelectedObj={0}\n",DrawRod.SelectedObjectId);
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawRod.UpdateRodData(dpack.Code))
                {
                    ed.WriteMessage("Update Rod Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }



            //if (gvRod.Rows.Count > 0)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(gvRod.SelectedRows[0].Cells["Column1"].Value.ToString() + "\n");
            //    //ed.WriteMessage(gvRod.SelectedRows[0].Index.ToString()+ "\n");
            //    Atend.Base.Design.DPackage Pakage = Atend.Base.Design.DPackage.AccessSelectByCode(DpakageCode);
            //    Pakage.ProductCode = Convert.ToInt32(gvRod.SelectedRows[0].Cells["Column1"].Value.ToString());

            //    if (Pakage.AccessUpdate())
            //    {
            //        //ed.WriteMessage("OK");
            //        //Atend.Base.Acad.AT_INFO
            //        Code = new Guid(gvRod.SelectedRows[0].Cells[0].Value.ToString());
            //        AllowClose = true;
            //        this.Close();
            //    }
            //    else
            //        MessageBox.Show("انجام ويرايش امكانپذير نيست");
            //}
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void frmEditDrawRod_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            //BindDataTocboProjCode();

            dtMerge = Atend.Base.Equipment.ERod.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;
            
            gvRod.AutoGenerateColumns = false;
            gvRod.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvRod, this);
            ChangeColor();
            for (int i = 0; i < gvRod.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvRod.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvRod.Rows[i].Cells[5].Value.ToString()) == false)
                {
                    gvRod.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
            BindDataToComboBoxIsExist();
            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvRod.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvRod.Rows[i].Cells[5].Value) == false)
                {
                    gvRod.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void gvRod_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(gvRod.Rows[gvRod.CurrentRow.Index].Cells[0].Value.ToString());
            //MessageBox.Show(gvRod.Rows[gvRod.CurrentRow.Index].Cells[1].Value.ToString());
        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(cboIsExist.SelectedValue.ToString()));
            cboProjCode.DisplayMember = "Name";
            cboProjCode.ValueMember = "ACode";
            cboProjCode.DataSource = dtWorkOrder;
        }

        private void cboVol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}