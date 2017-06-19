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
    public partial class frmProjectCodeParent : Form
    {
        int selectedParent = 0;
        bool ForceToClose = false;
        public frmProjectCodeParent()
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

        private void frmProjectCodeParent_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            DataTable dt = Atend.Base.Base.BWorkOrder.SelectByParentCode(0);
            gvParent.AutoGenerateColumns = false;
            gvParent.DataSource = dt;
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            return true;
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            selectedParent = 0;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void Save()
        {
            Atend.Base.Base.BWorkOrder WorkOrder = new Atend.Base.Base.BWorkOrder();
            WorkOrder.Name = txtName.Text;
            WorkOrder.ParentCode = 0;
            WorkOrder.ACode = 0;

            if (selectedParent == 0)
            {
                if (WorkOrder.Insert())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                WorkOrder.Code = selectedParent;
                if (WorkOrder.Update())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }

            DataTable table = Atend.Base.Base.BWorkOrder.SelectByParentCode(0);
            gvParent.AutoGenerateColumns = false;
            gvParent.DataSource = table;
        }

        //public void BindDataToOwnControl(int Code)
        //{
        //    selectedParent = Code;

        //    Atend.Base.Base.BWorkOrder work = Atend.Base.Base.BWorkOrder.SelectByCode(Code);
        //    txtName.Text = work.Name;
        //}

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void gvParent_Click(object sender, EventArgs e)
        {
            if (gvParent.Rows.Count > 0)
            {
                txtName.Text = gvParent.Rows[gvParent.CurrentRow.Index].Cells[2].Value.ToString();
                selectedParent = Convert.ToInt32(gvParent.Rows[gvParent.CurrentRow.Index].Cells[0].Value.ToString());
            }
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvParent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}