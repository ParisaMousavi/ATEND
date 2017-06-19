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
    public partial class frmEditConsol : Form
    {
        System.Data.DataTable dtMergeConsol = new System.Data.DataTable();
        public double Volt;
        public Guid NodeCode;
        bool ForceToClose = false;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool AllowClose = false;
        int SelectedProductCode;
        Atend.Base.Design.DPackage Dpack;
        public ObjectId obj;

        public frmEditConsol()
        {
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


        private void Search()
        {
            //DataView dv = new DataView();
            //dv.Table = dtMergeConsol;
            //dv.RowFilter = " Type = '" + Convert.ToInt16(cboType.SelectedIndex) + "'  AND VoltageLevel= '" + Volt + "'";
            //gvConsol.AutoGenerateColumns = false;
            //gvConsol.DataSource = dv;

            dtMergeConsol.DefaultView.RowFilter = " Type = '" + Convert.ToInt16(cboType.SelectedIndex) + "'  AND VoltageLevel= '" + Volt + "'";

            ChangeColor();
        }

        public void ChangeColor()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            for (int i = 0; i < gvConsol.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvConsol.Rows[i].Cells["IsSql"].Value) == false)
                {
                    gvConsol.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        private void gvConsol_Click(object sender, EventArgs e)
        {
            Atend.Base.Equipment.EConsol Consol;
            if (Convert.ToBoolean(gvConsol.SelectedRows[0].Cells["IsSql"].Value) == true)
            {
                Consol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(gvConsol.SelectedRows[0].Cells[0].Value.ToString()));
            }
            else
            {
                Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(gvConsol.SelectedRows[0].Cells[1].Value.ToString()));

            }
            txtName.Text = Consol.Name;
            //txtType.Text = Consol.Type.ToString();
            txtVoltage.Text = Consol.VoltageLevel.ToString();
            //txtConsolType.Text = Consol.ConsolType.ToString();

            switch (Consol.Type)
            {
                case 0: txtType.Text = "عمودی"; break;
                case 1: txtType.Text = "مثلثی"; break;
                case 2: txtType.Text = "افقی"; break;
            }

            switch (Consol.ConsolType)
            {
                case 0: txtConsolType.Text = "کششی"; break;
                case 1: txtConsolType.Text = "انتهای"; break;
                case 2: txtConsolType.Text = "عبوری"; break;
                case 3: txtConsolType.Text = "DP"; break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void frmEditConsol_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            //BindDataToForm();

            BindDataToComboBoxIsExist();
            if (cboType.Items.Count > 0)
                cboType.SelectedIndex = 0;

            Dpack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
            //ed.WriteMessage("((={0}\n", Dpack.Code.ToString());
            SelectedProductCode = Dpack.ProductCode;
            Atend.Base.Equipment.EConsol Consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Dpack.ProductCode);


            Volt = Consol.VoltageLevel;
            txtCount.Text = Dpack.Count.ToString();
            CboIsExist.SelectedValue = Atend.Base.Base.BEquipStatus.SelectByACode(Dpack.IsExistance).Code;
            CboProjCode.SelectedValue = Dpack.ProjectCode;



            //ed.WriteMessage("**Volt={0}\n", Volt);



            dtMergeConsol = Atend.Base.Equipment.EConsol.SelectAllAndMerge();

            dtMergeConsol.DefaultView.RowFilter = " VoltageLevel = '" + Volt + "'";
            //DataView dv = new DataView();
            //dv.Table = dtMergeConsol;
            //dv.RowFilter = " VoltageLevel = '" + Volt + "'";

            gvConsol.AutoGenerateColumns = false;
            gvConsol.DataSource = dtMergeConsol;
            ChangeColor();
            
            for (int i = 0; i < gvConsol.Rows.Count; i++)
            {
                if (Convert.ToInt32(gvConsol.Rows[i].Cells[1].Value.ToString()) == Dpack.ProductCode)
                {
                    gvConsol.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            Atend.Global.Utility.UBinding.SetGridToCurrentSelectedEquip("Code,Xcode", new object[2] { SelectedProductCode, "00000000-0000-0000-0000-000000000000" }, dtMergeConsol, gvConsol, this);



        }

        private void btn_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            Close();
        }

        public void BindDataToComboBoxIsExist()
        {
            CboIsExist.DisplayMember = "Name";
            CboIsExist.ValueMember = "Code";
            CboIsExist.DataSource = Atend.Base.Base.BEquipStatus.SelectAllX();

        }

        private void cboIsExist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataTable dtWorkOrder = Atend.Base.Base.BWorkOrder.SelectJoinOrder(Convert.ToInt32(CboIsExist.SelectedValue.ToString()));
            CboProjCode.DisplayMember = "Name";
            CboProjCode.ValueMember = "ACode";
            CboProjCode.DataSource = dtWorkOrder;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                Atend.Global.Acad.DrawEquips.AcDrawConsol DrawConsol = new Atend.Global.Acad.DrawEquips.AcDrawConsol();

                if (Convert.ToBoolean(gvConsol.SelectedRows[0].Cells["IsSql"].Value))
                {
                    DrawConsol.UseAccess = false;
                    DrawConsol.eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(new Guid(gvConsol.SelectedRows[0].Cells[0].Value.ToString()));
                }
                else
                {
                    DrawConsol.UseAccess = true;
                    DrawConsol.eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(gvConsol.SelectedRows[0].Cells["IsSql"].Value));
                }

                DrawConsol.ConsolConut = Convert.ToInt32(txtCount.Text);

                Atend.Base.Base.BEquipStatus statusConsol = Atend.Base.Base.BEquipStatus.SelectByCode(Convert.ToInt32(CboIsExist.SelectedValue));
                DrawConsol.Existance = statusConsol.ACode;

                if (CboProjCode.Items.Count == 0)
                    DrawConsol.ProjectCode = 0;
                else
                    DrawConsol.ProjectCode = Convert.ToInt32(CboProjCode.SelectedValue);



                if (!DrawConsol.UpdateConsolData(NodeCode,obj))
                {
                    ed.WriteMessage("ویرایش اطلاعات با موفقیت انجام نشد\n");
                }
                else
                {
                    AllowClose = true;
                    Close();
                }
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtCount.Text))
            {
                MessageBox.Show("لطفا تعداد را وارد نمایید", "خطا");
                txtCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtCount.Text))
            {
                MessageBox.Show("لطفا تعداد را با فرمت مناسب وارد نمایید", "خطا");
                txtCount.Focus();
                return false;
            }
            return true;
        }
    }
}