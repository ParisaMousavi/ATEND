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
    public partial class frmEditDrawStreetBox02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        bool AllowClose = true;
        public ObjectId ObjID;
        public Guid NodeCode;
        int selectedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawStreetBox02()
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

        private void frmEditDrawStreetBox02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
            dtMerge = Atend.Base.Equipment.EStreetBox.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selectedProductCode = dpack.ProductCode;

            gvStreetBox.AutoGenerateColumns = false;
            gvStreetBox.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { dpack.ProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvStreetBox, this);
            ChangeColor();
            for (int i = 0; i < gvStreetBox.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvStreetBox.Rows[i].Cells[0].Value.ToString()) == dpack.ProductCode && Convert.ToBoolean(gvStreetBox.Rows[i].Cells[5].Value.ToString()) == false)
                {
                    gvStreetBox.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToComboBoxIsExist();
            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvStreetBox.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvStreetBox.Rows[i].Cells[5].Value) == false)
                {
                    gvStreetBox.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
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
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvStreetBox.AutoGenerateColumns = false;
                gvStreetBox.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvStreetBox.AutoGenerateColumns = false;
                //gvStreetBox.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvStreetBox, this);
            ChangeColor();
            for (int i = 0; i < gvStreetBox.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvStreetBox.Rows[i].Cells[0].Value.ToString()) == selectedProductCode && Convert.ToBoolean(gvStreetBox.Rows[i].Cells[5].Value.ToString()) == false)
                {
                    gvStreetBox.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void frmEditDrawStreetBox02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                e.Cancel = true;
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
            //if (string.IsNullOrEmpty(cboProjCode.Text))
            //{
            //    MessageBox.Show("لطفاً کد دستور کار را انتخاب کنید", "خطا");
            //    return false;
            //}
            if (string.IsNullOrEmpty(cboIsExist.Text))
            {
                MessageBox.Show("لطفاً کد وضعیت تجهیز را انتخاب کنید", "خطا");
                cboIsExist.Focus();
                return false;
            }

           return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                List<Atend.Base.Equipment.EStreetBoxPhuse> list = new List<Atend.Base.Equipment.EStreetBoxPhuse>();
                System.Data.DataTable dt;
                Atend.Global.Acad.DrawEquips.AcDrawStreetBox DrawStreetBox = new Atend.Global.Acad.DrawEquips.AcDrawStreetBox();
                if (Convert.ToBoolean(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[5].Value) == false)
                {
                    DrawStreetBox.UseAccess = true;
                    DrawStreetBox.eStreetBox = Atend.Base.Equipment.EStreetBox.AccessSelectByCode(Convert.ToInt32(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[0].Value.ToString()));
                    dt = Atend.Base.Equipment.EStreetBoxPhuse.AccessSelectByStreetBoxCode(Convert.ToInt32(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[0].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EStreetBoxPhuse.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString())));
                    }
                }
                else if (Convert.ToBoolean(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[5].Value))
                {
                    DrawStreetBox.UseAccess = false;
                    DrawStreetBox.eStreetBox = Atend.Base.Equipment.EStreetBox.SelectByXCode(new Guid(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[1].Value.ToString()));
                    dt = Atend.Base.Equipment.EStreetBoxPhuse.SelectByStreetBoxXCode(new Guid(gvStreetBox.Rows[gvStreetBox.CurrentRow.Index].Cells[1].Value.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(Atend.Base.Equipment.EStreetBoxPhuse.SelectByXCode(new Guid(dr["XCode"].ToString())));
                    }
                }
               
                DrawStreetBox.eStreetBoxPhuse = list;

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawStreetBox.Existance = status.ACode;

                if (cboProjCode.Items.Count == 0)
                    DrawStreetBox.ProjectCode = 0;
                else
                    DrawStreetBox.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawStreetBox.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawStreetBox.UpdateStreetBoxData(dpack.Code))
                {
                    ed.WriteMessage("Update StreetBox Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }
            else
                AllowClose = false;
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