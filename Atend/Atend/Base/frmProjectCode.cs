using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base
{
    public partial class frmProjectCode : Form
    {
        public frmProjectCode()
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
            SelectedProjectCode = 0;
        }

        int SelectedProjectCode = 0;
        DataTable dt = new DataTable();
        bool ForceToClose = false;
        private void Reset()
        {
            SelectedProjectCode = 0;
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            //cboParent.SelectedIndex = 0;
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
                MessageBox.Show("لطفاً کد دستورالعمل را مشخص نمایید", "خطا");
                txtCode.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.Int32Converter(txtCode.Text))
            {
                MessageBox.Show("لطفا کد دستور عمل را با فرمت مناسب وارد نمایید", "خطا");
                txtCode.Focus();
                return false;
            }

            //if (SelectedProjectCode == 0)
            //{
            //    MessageBox.Show("لطفا ابتدا یک کد دستور کار را انتخاب نمایید", "خطا");
            //    return false;

            //}
            //if (SelectedProjectCode == 0)
            //{
            //    Atend.Base.Base.BProjectCode ProjC = Atend.Base.Base.BProjectCode.AccessSelectByAdditionalCode(Convert.ToInt32(txtCode.Text));

            //    if (ProjC.Code != -1)
            //    {
            //        MessageBox.Show("کد دستور العمل قبلاً استفاده شده . لطفاً عدد دیگری را وارد کنید");
            //        return false;
            //    }
            //}

            Atend.Base.Base.BWorkOrder w = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt32(txtCode.Text));
            if (w.Code != -1)
            {
                MessageBox.Show("کد دستور کار قبلا استفاده شده است", "خطا");
                txtCode.Focus();
                return false;
            }

            return true;

        }

        private void Save()
        {
            Atend.Base.Base.BWorkOrder WorkOrder = new Atend.Base.Base.BWorkOrder();
            WorkOrder.Name = txtName.Text;
            WorkOrder.ParentCode = Convert.ToInt32(cboParent.SelectedValue.ToString());
            WorkOrder.ACode = Convert.ToInt32(txtCode.Text);

            if (SelectedProjectCode == 0)
            {
                if (WorkOrder.Insert())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                WorkOrder.Code = SelectedProjectCode;
                if (WorkOrder.Update())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
            DataTable table = Atend.Base.Base.BWorkOrder.SelectByParentCode(Convert.ToInt32(cboParent.SelectedValue.ToString()));
            gvWorkOrder.AutoGenerateColumns = false;
            gvWorkOrder.DataSource = table;
           // Atend.Base.Base.BProjectCode ProjCode = new Atend.Base.Base.BProjectCode();

           // ProjCode.AdditionalCode = Convert.ToInt32(txtCode.Text);
           // ProjCode.Name = txtName.Text;

           // //if (SelectedProjectCode == 0)
           // //{
           // //    //if (ProjCode.AccessInsert())
           // //    //    Reset();
           // //    //else
           // //    //    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
           // //}
           // //else
           // //{
           //     ProjCode.Code = SelectedProjectCode;
           //     if (ProjCode.AccessUpdate())
           //         Reset();
           //     else
           //         MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
           //// }

            
        }

        private void Delete()
        {
            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedProjectCode != 0)
                {
                    if (Atend.Base.Base.BWorkOrder.Delete(SelectedProjectCode))
                    {
                        Reset();
                        DataTable table = Atend.Base.Base.BWorkOrder.SelectByParentCode(Convert.ToInt32(cboParent.SelectedValue.ToString()));
                        gvWorkOrder.AutoGenerateColumns = false;
                        gvWorkOrder.DataSource = table;

                    }
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }
        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void BindDataToOwnControl(int Code)
        {
            SelectedProjectCode = Code;
            //Atend.Base.Base.BProjectCode pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Code);
            Atend.Base.Base.BWorkOrder work = Atend.Base.Base.BWorkOrder.SelectByCode(Code);
            txtCode.Text = work.ACode.ToString();
            txtName.Text = work.Name;
            cboParent.SelectedValue = Convert.ToInt32(work.ParentCode.ToString());
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmProjectCodeSearch Frm = new frmProjectCodeSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Frm);
        }

        private void frmProjectCode_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

            BindToCbo();
        }

        private void BindToCbo()
        {
            dt = Atend.Base.Base.BWorkOrder.SelectByParentCode(0);
            cboParent.DisplayMember = "Name";
            cboParent.ValueMember = "Code";
            cboParent.DataSource = dt;
        }

        private void cboParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = Atend.Base.Base.BWorkOrder.SelectByParentCode(Convert.ToInt32(cboParent.SelectedValue.ToString()));
            gvWorkOrder.AutoGenerateColumns = false;
            gvWorkOrder.DataSource = table;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripLabel4_Click_1(object sender, EventArgs e)
        {
            //frmProjectCodeSearch Frm = new frmProjectCodeSearch(this);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Frm);
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            frmProjectCodeParent p = new frmProjectCodeParent();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(p);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void gvWorkOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProjectCodeSearch Frm = new frmProjectCodeSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Frm);
        }

        private void gvWorkOrder_Click(object sender, EventArgs e)
        {
            if (gvWorkOrder.Rows.Count > 0)
            {
                BindDataToOwnControl(Convert.ToInt32(gvWorkOrder.Rows[gvWorkOrder.CurrentRow.Index].Cells[0].Value.ToString()));
                //txtName.Text = gvParent.Rows[gvParent.CurrentRow.Index].Cells[1].Value.ToString();
                //selectedParent = Convert.ToInt32(gvParent.Rows[gvParent.CurrentRow.Index].Cells[0].Value.ToString());
            }
        }
    }
}