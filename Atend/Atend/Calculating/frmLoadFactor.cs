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
    public partial class frmLoadFactor : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        int SelectedloadFActor = -1;
        bool ForceToClose = false;
        public frmLoadFactor()
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
            SelectedloadFActor = -1;
            txtFactorPower.Text = string.Empty;
            txtFactorConcurency.Text = string.Empty;
            txtPhaseCount.Text = string.Empty;
            txtAmper.Text = string.Empty;
            txtName.Text = string.Empty;
            txtVoltage.Text = string.Empty;

        }

        private bool Validation()
        {


            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAmper.Text))
            {
                MessageBox.Show("لطفا امپراژ مشخص نمایید", "خطا");
                txtAmper.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtVoltage.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی مشخص نمایید", "خطا");
                txtAmper.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtAmper.Text))
            {
                MessageBox.Show("لطفا امپراژ رابا فرمت مناسب مشخص نمایید", "خطا");
                txtAmper.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtFactorPower.Text))
            {
                MessageBox.Show("لطفا ضریب توان را مشخص نمایید", "خطا");
                txtFactorPower.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtFactorPower.Text))
            {
                MessageBox.Show("لطفا ضریب توان رابا فرمت مناسب مشخص نمایید", "خطا");
                txtFactorPower.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtVoltage.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی رابا فرمت مناسب مشخص نمایید", "خطا");
                txtFactorPower.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtPhaseCount.Text))
            {
                MessageBox.Show("لطفا تعداد فاز را مشخص نمایید", "خطا");
                txtPhaseCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtPhaseCount.Text))
            {
                MessageBox.Show("لطفا تعداد فاز رابا فرمت مناسب مشخص نمایید", "خطا");
                txtPhaseCount.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtFactorConcurency.Text))
            {
                MessageBox.Show("لطفا ضریب همزمانی را مشخص نمایید", "خطا");
                txtFactorConcurency.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtFactorConcurency.Text))
            {
                MessageBox.Show("لطفا ضریب همزمانی رابا فرمت مناسب مشخص نمایید", "خطا");
                txtFactorConcurency.Focus();
                return false;
            }




            return true;
        }

        private void Save()
        {
            Atend.Base.Calculating.CDloadFactor cloadFActor = new Atend.Base.Calculating.CDloadFactor();

            cloadFActor.Amper = Convert.ToDouble(txtAmper.Text);
            cloadFActor.FactorConcurency = Convert.ToDouble(txtFactorConcurency.Text);
            cloadFActor.FactorPower = Convert.ToDouble(txtFactorPower.Text);
            cloadFActor.Name = txtName.Text;
            cloadFActor.PhaseCount = Convert.ToInt32(txtPhaseCount.Text);
            cloadFActor.Voltage = Convert.ToDouble(txtVoltage.Text);

            //ed.WriteMessage("aa\n");
            if (SelectedloadFActor == -1)
            {
                if (cloadFActor.AccessInsert())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت کردن اطلاعات نمی باشد", "خطا");
            }
            else
            {
                cloadFActor.Code = SelectedloadFActor;
                if (cloadFActor.AccessUpdate())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");

            }

        }

        private void Delete()
        {
            if (MessageBox.Show("آیا مایل به حذف کردن اطلاعات می باشید؟", "خطا", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedloadFActor != -1)
                {
                    if (Atend.Base.Calculating.CDloadFactor.AccessDelete(SelectedloadFActor))
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
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedloadFActor = Code;
            Atend.Base.Calculating.CDloadFactor loadAfctor = Atend.Base.Calculating.CDloadFactor.AccessSelectByCode(Code);
            ed.WriteMessage("SelectedLoadFactor={0}\n",SelectedloadFActor);
            txtName.Text = loadAfctor.Name;
            txtFactorPower.Text = loadAfctor.FactorPower.ToString();
            txtAmper.Text = loadAfctor.Amper.ToString();
            txtPhaseCount.Text = loadAfctor.PhaseCount.ToString();
            txtFactorConcurency.Text = loadAfctor.FactorConcurency.ToString();
            txtVoltage.Text = loadAfctor.Voltage.ToString();
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
            frmLoadFactorSearch loadFActorSearch = new frmLoadFactorSearch(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(loadFActorSearch);

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }



    }
}