using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base
{
    public partial class frmEquipStatus : Form
    {
        int selectedEquipStatus = 0;
        DataTable dt = new DataTable();
        bool ForceToClose = false;
        public frmEquipStatus()
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

        private void Reset()
        {
            selectedEquipStatus = 0;
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPrice.Text = "0";
            txtExecutePrice.Text = "0";
            txtWagePrice.Text = "0";
            chkIsDefault.Checked = false;

            for (int i = 0; i < gvEquipStatus.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvEquipStatus.Rows[i].Cells[1];
                chk.Value = 0;
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("لطفاً کد را مشخص نمایید", "خطا");
                txtCode.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtCode.Text))
            {
                MessageBox.Show("لطفا کد  را با فرمت مناسب وارد نمایید", "خطا");
                txtCode.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtPrice.Text))
            {
                MessageBox.Show("لطفا هزینه تجهیز را با فرمت مناسب وارد نمایید", "خطا");
                txtPrice.Focus();
                return false;
            }


            if (!Atend.Control.NumericValidation.DoubleConverter(txtExecutePrice.Text))
            {
                MessageBox.Show("لطفا هزینه اجرا را با فرمت مناسب وارد نمایید", "خطا");
                txtExecutePrice.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtWagePrice.Text))
            {
                MessageBox.Show("لطفا هزینه ماشین آلات را با فرمت مناسب وارد نمایید", "خطا");
                txtWagePrice.Focus();
                return false;
            }



            return true;
        }

        private void Save()
        {
            txtName.Focus();
            Atend.Base.Base.BEquipStatus EquipStatus = new Atend.Base.Base.BEquipStatus();
            EquipStatus.Name = txtName.Text;
            EquipStatus.ACode = Convert.ToInt32(txtCode.Text);

            EquipStatus.PricePercent = Convert.ToDouble(txtPrice.Text);
            EquipStatus.ExecutePricePercent = Convert.ToDouble(txtExecutePrice.Text);
            EquipStatus.WagePricePercent = Convert.ToDouble(txtWagePrice.Text);

            if (chkIsDefault.Checked)
            {
                EquipStatus.UnBindIsDedefault();
                EquipStatus.IsDefault = true;
            }
            else
            {
                EquipStatus.IsDefault = false;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < gvEquipStatus.Rows.Count; i++)
            {
                Atend.Base.Base.BWorkOrder workorder = new Atend.Base.Base.BWorkOrder();
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvEquipStatus.Rows[i].Cells[1];
                if (Convert.ToBoolean(chk.Value))
                {
                    array.Add(gvEquipStatus.Rows[i].Cells[0].Value.ToString());
                }
            }
            EquipStatus.ArraySub = array;

            if (selectedEquipStatus != 0)
            {
                if (!Atend.Base.Base.BStatusWorkOrder.DeleteXWithEquipStatusCode(selectedEquipStatus))
                {
                    MessageBox.Show("خطا در حذف ", "خطا");
                    return;
                }
            }

            if (selectedEquipStatus == 0)
            {
                if (EquipStatus.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                EquipStatus.Code = selectedEquipStatus;
                if (EquipStatus.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void frmEquipStatus_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            dt = Atend.Base.Base.BWorkOrder.SelectByParentCode(0);
            gvEquipStatus.AutoGenerateColumns = false;
            gvEquipStatus.DataSource = dt;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (selectedEquipStatus != 0)
                {
                    if (Atend.Base.Base.BEquipStatus.DeleteX(selectedEquipStatus))
                    {
                        if (Atend.Base.Base.BStatusWorkOrder.DeleteXWithEquipStatusCode(selectedEquipStatus))
                            Reset();
                    }
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmEquipStatusSearch f = new frmEquipStatusSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(f);
        }

        public void BindDataToOwnControl(int Code)
        {
            for (int i = 0; i < gvEquipStatus.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvEquipStatus.Rows[i].Cells[1];
                chk.Value = 0;
            }
            chkIsDefault.Checked = false;

            selectedEquipStatus = Code;
            Atend.Base.Base.BEquipStatus status = Atend.Base.Base.BEquipStatus.SelectByCode(Code);
            txtCode.Text = status.ACode.ToString();
            txtName.Text = status.Name;

            txtPrice.Text = status.PricePercent.ToString();
            txtExecutePrice.Text = status.ExecutePricePercent.ToString();
            txtWagePrice.Text = status.WagePricePercent.ToString();

            if (status.IsDefault)
                chkIsDefault.Checked = true;

            for (int i = 0; i < gvEquipStatus.Rows.Count; i++)
            {
                for (int j = 0; j < status.ArraySub.Count; j++)
                {
                    if (Convert.ToInt32(status.ArraySub[j].ToString()) == Convert.ToInt32(gvEquipStatus.Rows[i].Cells[0].Value.ToString()))
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvEquipStatus.Rows[i].Cells[1];
                        chk.Value = 1;
                    }

                }
            }


        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}