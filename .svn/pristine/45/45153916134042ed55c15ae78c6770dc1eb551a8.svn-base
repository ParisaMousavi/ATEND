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
    public partial class frmPreparation : Form
    {
        bool ForceToClose = false;
        public frmPreparation()
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

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفاً نام طرح را مشخص نمایید");
                return false;
            }
            DataTable dt = Atend.Base.Base.BProduct.SelectByUniqueNameTypeX((int)Atend.Control.Enum.ProductType.Operation, txtName.Text);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show(" نام تکراری می باشد");
                return false;
            }
            return true; 
        }

        public void Save()
        {
            Atend.Base.Base.BProduct product = new Atend.Base.Base.BProduct();
            product.Name = txtName.Text;
            product.IsProduct = false;
            product.Type = (int)Atend.Control.Enum.ProductType.Operation;
            product.Code = 0;
            product.Unit = 1;
            product.Price = 0;
            product.ExecutePrice = 0;
            product.WagePrice = 0;
            if (product.InsertX())
                MessageBox.Show("اطلاعات با موفقيت ثبت شد ");
            else
                MessageBox.Show("خطا در ثبت اطلاعات");
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

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPreparation_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }

    }
}