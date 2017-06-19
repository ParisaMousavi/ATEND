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
    public partial class frmEditDrawDisconnector02 : Form
    {
        System.Data.DataTable dtMerge = new System.Data.DataTable();
        public Guid NodeCode;
        public ObjectId ObjID;
        bool AllowToclose = true;
        int selecetdProductCode = -1;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;

        public frmEditDrawDisconnector02()
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

        private void frmEditDrawDisconnector02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            dtMerge = Atend.Base.Equipment.EDisconnector.SelectAllAndMerge();

            System.Data.DataColumn dcTypeName = new System.Data.DataColumn("TypeName");
            dtMerge.Columns.Add(dcTypeName);
            foreach (DataRow dr in dtMerge.Rows)
            {
                if (Convert.ToInt32(dr["Type"].ToString()) == 1)
                {
                    dr["TypeName"] = "سکسیونر هوایی غیر قابل قطع زیر بار";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 2)
                {
                    dr["TypeName"] = "سکسیونر هوایی قابل قطع زیر بار";
                }
                if (Convert.ToInt32(dr["Type"].ToString()) == 3)
                {
                    dr["TypeName"] = "سکسیونر SF6 قابل قطع زیر بار";
                }
            }
            Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            selecetdProductCode = dpack.ProductCode;

            Atend.Base.Design.DKeyStatus keyStatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(NodeCode);
            
            if (keyStatus.IsClosed)
            {
                rdbClose.Checked = true;
            }
            else
            {
                rdbOpen.Checked = true;
            }


            gvDisconnector.AutoGenerateColumns = false;
            gvDisconnector.DataSource = dtMerge;

            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selecetdProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
            ChangeColor();
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[1].Value.ToString()) == selecetdProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value.ToString()) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            BindDataToComboBoxIsExist();
            //BindDataTocboProjCode();

            cboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(dpack.IsExistance).Code;
            cboProjCode.SelectedValue = dpack.ProjectCode;

            
        }

        public void ChangeColor()
        {
            for (int i = 0; i < gvDisconnector.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value) == false)
                {
                    gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void BindDataToComboBoxIsExist()
        {
            cboIsExist.DisplayMember = "Name";
            cboIsExist.ValueMember = "Code";
            cboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();
        }


        public void BindDataTocboProjCode()
        {
            //System.Data.DataTable dt = Atend.Base.Base.BProjectCode.AccessSelectAll();
            //cboProjCode.DisplayMember = "Name";
            //cboProjCode.ValueMember = "Code";
            //cboProjCode.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strFilter = "";
            bool check = false;
           
                Single amper = -1;

                if (!string.IsNullOrEmpty(txtAmper.Text))
                {
                    if (!Atend.Control.NumericValidation.DoubleConverter(txtAmper.Text))
                    {
                        amper = Convert.ToSingle(txtAmper.Text);
                        strFilter = " Amper='" + amper + "'";
                        check = true;
                    }
                }

                if (check)
                {
                    //DataView dv = new DataView();
                    //dv.Table = dtMerge;
                    //dv.RowFilter = strFilter;
                    dtMerge.DefaultView.RowFilter = strFilter;
                    gvDisconnector.AutoGenerateColumns = false;
                    gvDisconnector.DataSource = dtMerge;
                }
                else
                {
                    dtMerge.DefaultView.RowFilter = "";
                    //gvDisconnector.AutoGenerateColumns = false;
                    //gvDisconnector.DataSource = dtMerge;
                }
                Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,XCode", new object[2] { selecetdProductCode, "00000000-0000-0000-0000-000000000000" }, dtMerge, gvDisconnector, this);
                ChangeColor();
                for (int i = 0; i < gvDisconnector.Rows.Count; i++)
                {
                    if (Convert.ToInt32(gvDisconnector.Rows[i].Cells[1].Value.ToString()) == selecetdProductCode && Convert.ToBoolean(gvDisconnector.Rows[i].Cells[2].Value.ToString()) == false)
                    {
                        gvDisconnector.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            
        }

        private void gvDisconnector_Click(object sender, EventArgs e)
        {
            if (gvDisconnector.Rows.Count > 0)
            {
                Atend.Base.Equipment.EDisconnector disconnector;
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == true)
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(
                        new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                else
                {
                    disconnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(
                       Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                }

                txtName.Text = disconnector.Name;
                txtSelectedAmper.Text = disconnector.Amper.ToString();
                txtType.Text = gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[5].Value.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllowToclose = true;
        }

        private void frmEditDrawDisconnector02_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToclose)
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

            if (gvDisconnector.SelectedRows.Count <= 0)
            {
                MessageBox.Show("لطفا سکسیونر مورد نظر را انتخاب نمایید", "خطا");
                return false;

            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Atend.Global.Acad.DrawEquips.AcDrawDisConnector DrawDisConnector = new Atend.Global.Acad.DrawEquips.AcDrawDisConnector();
            Atend.Base.Design.DKeyStatus keystatus = Atend.Base.Design.DKeyStatus.SelectByNodeCode(NodeCode);
            
            if (Validation())
            {
                if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value) == false)
                {
                    DrawDisConnector.UseAccess = true;
                    DrawDisConnector.eDisConnector = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(Convert.ToInt32(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[1].Value.ToString()));
                }
                else if (Convert.ToBoolean(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[2].Value))
                {
                    DrawDisConnector.UseAccess = false;
                    DrawDisConnector.eDisConnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(new Guid(gvDisconnector.Rows[gvDisconnector.CurrentRow.Index].Cells[0].Value.ToString()));

                }

                Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(cboIsExist.SelectedValue));
                DrawDisConnector.Existance = status.ACode;

                if (rdbClose.Checked)
                {
                    keystatus.IsClosed = true;
                }
                else
                {
                    keystatus.IsClosed = false;
                }

                DrawDisConnector.DKeyStatus = keystatus;

                if (cboProjCode.Items.Count == 0)
                    DrawDisConnector.ProjectCode = 0;
                else
                    DrawDisConnector.ProjectCode = Convert.ToInt32(cboProjCode.SelectedValue);

                DrawDisConnector.SelectedObjectId = ObjID;
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                //DrawDisConnector.EXCode = dpack.Code;
                if (DrawDisConnector.UpdateDisConnectorData(dpack.Code))
                {
                    ed.WriteMessage("Update DisConnector Success \n");
                    AllowToclose = true;
                    this.Close();
                }
                else
                {
                    AllowToclose = false;
                }
            }
            else
            {
                AllowToclose = false;
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