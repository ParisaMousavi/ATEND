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
    public partial class frmEditDrawClamp02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public Guid NodeCode;
        public Guid DPackageCode;
        public ObjectId objID;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawClamp02()
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

        private void frmEditDrawClamp02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            dtMerge = Atend.Base.Equipment.EClamp.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            gvClamp.AutoGenerateColumns = false;
            gvClamp.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvClamp, this);
            ChangeColor();
            for (int i = 0; i < gvClamp.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvClamp.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvClamp.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvClamp.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            
            BindDataToComboBoxIsExist();
            //BindDataTocboProjCode();

            //int vol = -1;
            //if (chkVol.Checked)
            //    vol = Convert.ToInt32(cboVol.SelectedValue);
            //DataTable dt = Atend.Base.Equipment.EClamp.DrawSearch(vol);
            //gvClamp.AutoGenerateColumns = false;
            //gvClamp.DataSource = dt;

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvClamp.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvClamp.Rows[i].Cells[3].Value) == false)
                {
                    gvClamp.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int Voltage = -1;
            bool check = false;
            string strFilter = "";

            if (chkVol.Checked)
            {
                Voltage = Convert.ToInt32(cboVol.Text);
                strFilter = " VoltageLevel='" + Voltage.ToString() + "'";
                check = true;
            }

            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvClamp.AutoGenerateColumns = false;
                gvClamp.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvClamp.AutoGenerateColumns = false;
                //gvClamp.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvClamp, this);
            ChangeColor();
            for (int i = 0; i < gvClamp.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvClamp.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvClamp.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    gvClamp.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
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

            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفا موجود یا پیشنهادی را انتخاب نمایید", "خطا");
                return false;
            }

            if (gvClamp.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا کلمپ مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }

            return true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawKalamp DrawKlamp = new Atend.Global.Acad.DrawEquips.AcDrawKalamp();
            if (Validation())
            {
                if (Convert.ToBoolean(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[3].Value) == false)
                {
                    DrawKlamp.UseAccess = true;
                    DrawKlamp.eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[3].Value))
                {
                    DrawKlamp.UseAccess = false;
                    DrawKlamp.eClamp = Atend.Base.Equipment.EClamp.SelectByXCode(new Guid(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawKlamp.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawKlamp.ProjectCode = 0;
                else
                    DrawKlamp.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawKlamp.SelectedObjectId = objID;
                if (DrawKlamp.UpdateKalampData(NodeCode))
                    ed.WriteMessage("Update Kalamp Success \n");
            }


            ////////////////////////////////
            //if (Validation())
            //{
            //    Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(DPackageCode);
            //    Package.ProductCode = Convert.ToInt32(gvClamp.SelectedRows[0].Cells["XCode"].Value.ToString());

            //    if (Convert.ToBoolean(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[3].Value))
            //    {
            //        //UseAccess = false
            //        Atend.Base.Equipment.EClamp clamp = Atend.Base.Equipment.EClamp.SelectByXCode(new Guid(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[1].Value.ToString()));
            //        if (clamp.AccessInsert())
            //        {
            //            Package.NodeCode = clamp.XCode;
            //            if (Package.AccessInsert())
            //            {
            //                Code = new Guid(gvClamp.SelectedRows[0].Cells["XCode"].Value.ToString());
            //                this.Close();
            //            }
            //            else
            //                MessageBox.Show("انجام ويرايش امكانپذير نيست");
            //        }
            //        else
            //            MessageBox.Show("انجام ويرايش امكانپذير نيست");
            //    }



            //    else if (Convert.ToBoolean(gvClamp.Rows[gvClamp.CurrentRow.Index].Cells[3].Value) == false)
            //    {
            //        //UseAccess = true
            //        if (Package.AccessUpdate())
            //        {
            //            Code = new Guid(gvClamp.SelectedRows[0].Cells["XCode"].Value.ToString());
            //            this.Close();
            //        }
            //        else
            //            MessageBox.Show("انجام ويرايش امكانپذير نيست");
            //    }

            //    //Atend.Base.Acad.AcadGlobal.ClampData.Existance = Convert.ToByte(cboIsExist.SelectedIndex);

            //}
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