using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Equipment
{
    public partial class frmRamp02 : Form
    {
        public bool IsDefault = false;
        Guid SelectedRampXCode = Guid.Empty;
        int Code = -1;
        bool ForceToClose = false;

        public frmRamp02()
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
        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectedRampXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.ERamp  Eramp = Atend.Base.Equipment.ERamp.SelectByXCode(SelectedRampXCode);
                    if (Eramp.IsDefault || IsDefault)
                    {
                        MessageBox.Show("کاربر گرامی شما اجازه ویرایش  تجهیز به صورت پیش فرض ندارید", "خطا");
                        return false;
                    }
                }
            }
            return true;
        }
        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را وارد نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            return CheckStatuseOfAccessChangeDefault();

        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }
        public void BindDataToOwnControl(Guid XCode)
        {
            Atend.Base.Equipment.ERamp Ramp = Atend.Base.Equipment.ERamp.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(reCloser.ProductCode);
            Atend.Control.Common.selectedProductCode = Ramp.ProductCode;
            SelectProduct();

            SelectedRampXCode = XCode;
            txtName.Text = Ramp.Name;

            tsbIsDefault.Checked = Ramp.IsDefault;
            Code = Ramp.Code;
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            SelectedRampXCode = Guid.Empty;
            Atend.Control.Common.selectedProductCode = -1;
            Code = -1;
            IsDefault = false;
            tsbIsDefault.Checked = false;


        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Ramp);

            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmproductSearch);
            if (Atend.Control.Common.selectedProductCode != -1)
            {
                SelectProduct();
            }
        }
        private void SelectProduct()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByIdX(Atend.Control.Common.selectedProductCode);
            txtName.Text = product.Name;
            txtCode.Text = product.Code.ToString();
            txtBackUpName.Text = product.Name;
        }

        private void Save()
        {
            Atend.Base.Equipment.ERamp Ramp = new Atend.Base.Equipment.ERamp();
            Ramp.Name = txtName.Text;
            Ramp.ProductCode = Atend.Control.Common.selectedProductCode;
            Ramp.IsDefault = IsDefault;
            Ramp.Code = 0;
            if (SelectedRampXCode == Guid.Empty)
            {
                if (Ramp.InsertX())
                {
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
                }
            }
            else
            {
                Ramp.XCode = SelectedRampXCode;
                Ramp.Code = Code;
                if (Ramp.UpdateX())
                {
                    Reset();
                }
                else
                {
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
                }
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedRampXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedRampXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.ERamp.DeleteX(SelectedRampXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (CheckStatuseOfAccessChangeDefault())
                Delete();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmRampSearch02 RampSearch = new frmRampSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(RampSearch);

        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbIsDefault_Click(object sender, EventArgs e)
        {
            if (IsDefault)
            {
                IsDefault = false;
                tsbIsDefault.Checked = false;
            }
            else
            {
                IsDefault = true;
                tsbIsDefault.Checked = true;
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
            if (CheckStatuseOfAccessChangeDefault())
            {
                Delete();
            }
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmRampSearch02 RampSearch = new frmRampSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(RampSearch);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRamp02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tsbShare_Click(object sender, EventArgs e)
        {
            if (SelectedRampXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Ramp), SelectedRampXCode))
                {
                    Atend.Base.Equipment.ERamp Ramp = Atend.Base.Equipment.ERamp.SelectByXCode(SelectedRampXCode);
                    Code = Ramp.Code;
                    MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
                }
                else
                {
                    MessageBox.Show("خطا در به اشتراک گذاری .");
                }
            }
            else
            {
                MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
            }
        }

    }
}