using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Xml;
namespace Atend.Calculating
{
    public partial class frmNetWorkCross : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        int SelectedNetWorkCross = -1;
        bool ForceToClose = false;
        public frmNetWorkCross()
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
            SelectedNetWorkCross = -1;
            txt11.Text = string.Empty;
            txt33.Text = string.Empty;
            txt20.Text = string.Empty;
            txt380.Text = string.Empty;
            txtName.Text = string.Empty;

        }

        private bool Validation()
        {


           
            if (string.IsNullOrEmpty(txt11.Text))
            {

                MessageBox.Show("لطفا کلیرانس 11ولت وارد نمایید", "خطا");
                txt11.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txt11.Text))
            {
                MessageBox.Show("لطفا کلیرانس 11ولت رابا فرمت مناسب مشخص نمایید", "خطا");
                txt11.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txt20.Text))
            {

                MessageBox.Show("لطفا کلیرانس 20کیلو ولت راوارد نمایید", "خطا");
                txt20.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txt20.Text))
            {
                MessageBox.Show("لطفا کلیرانس 20کیلو ولت رابا فرمت مناسب مشخص نمایید", "خطا");
                txt20.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txt33.Text))
            {

                MessageBox.Show("لطفا کلیرانس 33کیلو ولت را وارد نمایید", "خطا");
                txt33.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txt33.Text))
            {
                MessageBox.Show("لطفا کلیرانس 33کیلو ولت رابا فرمت مناسب مشخص نمایید", "خطا");
                txt33.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txt380.Text))
            {
               
                    MessageBox.Show("لطفا کلیرانس 380 ولت راوارد نمایید", "خطا");
                    txt380.Focus();
                    return false;
             }

            if (!Atend.Control.NumericValidation.DoubleConverter(txt380.Text))
            {
                MessageBox.Show("لطفا کلیرانس 380 ولت رابا فرمت مناسب مشخص نمایید", "خطا");
                txt380.Focus();
                return false;
            }
            return true;
        }

        private void Save()
        {
            Atend.Base.Calculating.CNetWorkCross cNetWorkCross = new Atend.Base.Calculating.CNetWorkCross();
            //if (string.IsNullOrEmpty(txt11.Text))
            //{
            //    txt11.Text = "0";
            //}


            //if (string.IsNullOrEmpty(txt20.Text))
            //{
            //    txt20.Text = "0";
            //}

            //if (string.IsNullOrEmpty(txt33.Text))
            //{
            //    txt33.Text = "0";
            //}

            //if (string.IsNullOrEmpty(txt380.Text))
            //{
            //    txt380.Text = "0";
            //}
            cNetWorkCross.KV11 = Convert.ToDouble(txt11.Text);
            cNetWorkCross.KV20 = Convert.ToDouble(txt20.Text);
            cNetWorkCross.KV32 = Convert.ToDouble(txt33.Text);
            cNetWorkCross.V380 = Convert.ToDouble(txt380.Text);
            cNetWorkCross.Name = txtName.Text;



            if (SelectedNetWorkCross == -1)
            {
                if (cNetWorkCross.AccessInsert())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                cNetWorkCross.Code = SelectedNetWorkCross;
                if (cNetWorkCross.AccessUpdate())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }

        }

        private void Delete()
        {
            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedNetWorkCross != -1)
                {
                    if (Atend.Base.Calculating.CNetWorkCross.AccessDelete(SelectedNetWorkCross))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "حذف");
            }
        }

        public void BindDataToOwnControl(int Code)
        {
            SelectedNetWorkCross = Code;
            Atend.Base.Calculating.CNetWorkCross NetWorkCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Code);




            txtName.Text = NetWorkCross.Name;
            txt11.Text = NetWorkCross.KV11.ToString();
            txt380.Text = NetWorkCross.V380.ToString();
            txt20.Text = NetWorkCross.KV20.ToString();
            txt33.Text = NetWorkCross.KV32.ToString();

        }

        private void frmDB_Load(object sender, EventArgs e)
        {
            //


            if (ForceToClose)
                this.Close();

        }

        private void جدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
            }
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

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNetWorkCrossSearch netWorkCrossSearch = new frmNetWorkCrossSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(netWorkCrossSearch);

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



    }
}