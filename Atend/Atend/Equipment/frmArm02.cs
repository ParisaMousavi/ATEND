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
    public partial class frmArm02 : Form
    {
        public bool IsDefault = false;
        Guid SelectedArmXCode = Guid.Empty;
        int Code = -1;
        bool ForceToClose = false;

        public frmArm02()
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
                if (SelectedArmXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EArm Earm = Atend.Base.Equipment.EArm.SelectByXCode(SelectedArmXCode);
                    if (Earm.IsDefault || IsDefault)
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
            Atend.Base.Equipment.EArm Arm = Atend.Base.Equipment.EArm.SelectByXCode(XCode);
            //Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByCode(reCloser.ProductCode);
            Atend.Control.Common.selectedProductCode = Arm.ProductCode;
            SelectProduct();

            SelectedArmXCode = XCode;
            txtName.Text = Arm.Name;

            tsbIsDefault.Checked = Arm.IsDefault;
            Code = Arm.Code;
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            txtBackUpName.Text = string.Empty;
            txtCode.Text = string.Empty;
            SelectedArmXCode = Guid.Empty;
            Atend.Control.Common.selectedProductCode = -1;
            Code = -1;
            IsDefault = false;
            tsbIsDefault.Checked = false;


        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Atend.Base.frmProductSearch frmproductSearch = new Atend.Base.frmProductSearch(Atend.Control.Enum.ProductType.Arm);

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
            Atend.Base.Equipment.EArm Arm = new Atend.Base.Equipment.EArm();
            Arm.Name = txtName.Text;
            Arm.ProductCode = Atend.Control.Common.selectedProductCode;
            Arm.IsDefault = IsDefault;
            Arm.Code = 0;
            if (SelectedArmXCode == Guid.Empty)
            {
                if (Arm.InsertX())
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
                Arm.XCode = SelectedArmXCode;
                Arm.Code = Code;
                if (Arm.UpdateX())
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
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectedArmXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectedArmXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EArm.DeleteX(SelectedArmXCode))
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
            Atend.Equipment.frmArmSearch02 ArmSearch = new frmArmSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ArmSearch);

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
            Atend.Equipment.frmArmSearch02 ArmSearch = new frmArmSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ArmSearch);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmArm02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Atend.Equipment.frmArmSearch02 ArmSearch = new frmArmSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ArmSearch);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripLabel4_Click_1(object sender, EventArgs e)
        {
            Atend.Equipment.frmArmSearch02 ArmSearch = new frmArmSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(ArmSearch);
        }

        private void toolStripLabel5_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbIsDefault_Click_1(object sender, EventArgs e)
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

        private void tsbShare_Click(object sender, EventArgs e)
        {
            if (SelectedArmXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.Arm), SelectedArmXCode))
                {
                    Atend.Base.Equipment.EArm Arm = Atend.Base.Equipment.EArm.SelectByXCode(SelectedArmXCode);
                    Code = Arm.Code;
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