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
    public partial class frmEditDrawBreaker02 : Form
    {
        bool AllowClose = true;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public Guid NodeCode;
        public ObjectId ObjID;
        int selecetedProductCode = -1;
        bool ForceToClose = false;

        public frmEditDrawBreaker02()
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

        private void frmEditDrawBreaker02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EBreaker.SelectAllAndMerge();
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selecetedProductCode = dpack.ProductCode;

            Atend.Base.Design.DKeyStatus keystatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(NodeCode);
            ed.WriteMessage("keyStataus.IsClosed={0}\n",keystatus.IsClosed);
            if (keystatus.IsClosed)
            {
                rdbClose.Checked = true;
            }
            else
            {
                rdbOpen.Checked = true;
            }

            gvBreaker.AutoGenerateColumns = false;
            gvBreaker.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { dpack.ProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvBreaker, this);
            ChangeColor();
            for (int i = 0; i < gvBreaker.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvBreaker.Rows[i].Cells[1].Value.ToString()) == selecetedProductCode && Convert.ToBoolean(gvBreaker.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvBreaker.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }


            BindDataToCboIsExist();
            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;
            
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvBreaker.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvBreaker.Rows[i].Cells[2].Value) == false)
                {
                    gvBreaker.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        public void BindDataToCboIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }

        private void frmEditDrawBreaker02_FormClosing(object sender, FormClosingEventArgs e)
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
                MessageBox.Show("لطفاً کد وضعیت تجهیز را انتخاب کنید", "خطا");
                return false;
            }

            if (gvBreaker.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا دژنکتور مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }
            return true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Name = string.Empty;
            double Amper = -1;
            int type = -1;
            bool check = false;
            string strFilter = "";

            if (chkName.Checked)
            {
                Name = txtName.Text + '%';
                strFilter = " Name LIKE '" + Name + "'";
                check = true;
            }
            if (chkAmper.Checked)
            {
                Amper = Convert.ToDouble(txtAmper.Text);
                if (strFilter != "")
                {
                    strFilter += " AND Amper='" + Amper + "'";
                }
                else
                {
                    strFilter = " Amper='" + Amper + "'";
                }
                check = true;
            }
            if (check)
            {
                //DataView dv = new DataView();
                //dv.Table = dtMerge;
                //dv.RowFilter = strFilter;
                dtMerge.DefaultView.RowFilter = strFilter;
                gvBreaker.AutoGenerateColumns = false;
                gvBreaker.DataSource = dtMerge;
            }
            else
            {
                dtMerge.DefaultView.RowFilter = "";
                //gvBreaker.AutoGenerateColumns = false;
                //gvBreaker.DataSource = dtMerge;
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selecetedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvBreaker, this);
            ChangeColor();
            for (int i = 0; i < gvBreaker.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvBreaker.Rows[i].Cells[1].Value.ToString()) == selecetedProductCode && Convert.ToBoolean(gvBreaker.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvBreaker.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Acad.DrawEquips.AcDrawBreaker DrawBreaker = new Atend.Global.Acad.DrawEquips.AcDrawBreaker();
            Atend.Base.Design.DKeyStatus keystatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(NodeCode);
            if (Validation())
            {
                if (Convert.ToBoolean(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[2].Value) == false)
                {
                    DrawBreaker.UseAccess = true;
                    DrawBreaker.eBreaker = Atend.Base.Equipment.EBreaker.AccessSelectByCode(Convert.ToInt32(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[2].Value))
                {
                    DrawBreaker.UseAccess = false;
                    DrawBreaker.eBreaker = Atend.Base.Equipment.EBreaker.SelectByXCode(new Guid(gvBreaker.Rows[gvBreaker.CurrentRow.Index].Cells[0].Value.ToString()));

                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawBreaker.Existance = status.ACode;

                if (rdbClose.Checked)
                {
                    keystatus.IsClosed = true;
                }
                else
                {
                    keystatus.IsClosed = false;
                }

                DrawBreaker.DKeyStatus = keystatus;

                if (cboProjCode.Items.Count == 0)
                    DrawBreaker.ProjectCode = 0;
                else
                    DrawBreaker.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawBreaker.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                if (DrawBreaker.UpdatebreakerData(dpack.Code))
                {
                    ed.WriteMessage("Update Breaker Success \n");
                    AllowClose = true;
                    this.Close();
                }
            }
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