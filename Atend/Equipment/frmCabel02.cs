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
    public partial class frmCabel02 : Form
    {
        int Code = -1;
        public int productCode = -1;
        public Guid SelectConductorXCode = Guid.Empty;
        bool ForceToClose = false;

        public frmCabel02()
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



        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("لطفا نام را مشخص نمایید", "خطا");
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EConductor.SearchByName(txtName.Text) == true && SelectConductorXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاً سطح مقطع را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDiagonal.Text))
            {
                MessageBox.Show("لطفاً قطر هادی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDiagonal.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAlsticity.Text))
            {
                MessageBox.Show("لطفاً الستیسیتی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlsticity.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtWeight.Text))
            {
                MessageBox.Show("لطفاً وزن را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtWeight.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAlpha.Text))
            {
                MessageBox.Show("لطفاً آلفا را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlpha.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUTS.Text))
            {
                MessageBox.Show("لطفاً UTS را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtUTS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفاً ماکزیمم جریان را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtMaxCurrent.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtResistance.Text))
            {
                MessageBox.Show("لطفاً مقاومت اهمی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtResistance.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtReactance.Text))
            {
                MessageBox.Show("لطفاً مقاومت سلفی را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtCrossSectionArea.Text))
            {
                MessageBox.Show("لطفاٌ سطح مقطع را بافرمت عددی مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtCrossSectionArea.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtDiagonal.Text))
            {
                MessageBox.Show("لطفاً قطر هادی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtDiagonal.Focus();
                return false;
            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtAlsticity.Text))
            {
                MessageBox.Show("لطفاً الاستیسیتی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlsticity.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtWeight.Text))
            {
                MessageBox.Show("لطفاً وزن را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtWeight.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtAlpha.Text))
            {
                MessageBox.Show("لطفاً آلفا را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtAlpha.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtUTS.Text))
            {
                MessageBox.Show("لطفاً UTS را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtUTS.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtMaxCurrent.Text))
            {
                MessageBox.Show("لطفاً ماکزیمم جریان را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtMaxCurrent.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtResistance.Text))
            {
                MessageBox.Show("لطفاً مقاومت اهمی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtResistance.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtReactance.Text))
            {
                MessageBox.Show("لطفاً مقاومت سلفی را با فرمت مناسب وارد نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtReactance.Focus();
                return false;
            }

            return true;
        }

        private void Reset()
        {
            cboMaterial.SelectedIndex = 0;
            txtAlpha.Text = string.Empty;
            txtAlsticity.Text = string.Empty;
            txtCrossSectionArea.Text = string.Empty;
            txtDiagonal.Text = string.Empty;
            txtMaxCurrent.Text = string.Empty;
            txtReactance.Text = string.Empty;
            txtResistance.Text = string.Empty;
            txtUTS.Text = string.Empty;
            txtWeight.Text = string.Empty;
            SelectConductorXCode = Guid.Empty;
            txtComment.Text = string.Empty;
            cboMaterial.Focus();
            productCode = -1;
            Code = -1;

        }

        private void Save()
        {
            Atend.Base.Equipment.EConductor conductor = new Atend.Base.Equipment.EConductor();
            txtName.Focus();
            conductor.Alasticity = Convert.ToDouble(txtAlsticity.Text);
            conductor.Alpha = Convert.ToDouble(txtAlpha.Text);
            //conductor.CabelTypeCode = 0;
            conductor.CrossSectionArea = Convert.ToDouble(txtCrossSectionArea.Text);
            //conductor.DamperType = 0;
            conductor.Diagonal = Convert.ToDouble(txtDiagonal.Text);
            conductor.IsCabel = true;
            conductor.DamperType = Convert.ToByte(cboDamperType.SelectedValue);
            conductor.MaterialCode = Convert.ToByte(cboMaterial.SelectedValue.ToString());
            conductor.MaxCurrent = Convert.ToDouble(txtMaxCurrent.Text);
            conductor.ProductCode = productCode;
            conductor.Reactance = Convert.ToDouble(txtReactance.Text);
            conductor.Resistance = Convert.ToDouble(txtResistance.Text);
            conductor.UTS = Convert.ToDouble(txtUTS.Text);
            conductor.Weight = Convert.ToDouble(txtWeight.Text);
            conductor.GMR = 0;
            conductor.Comment = txtComment.Text;
            conductor.Code = Code;
            if (SelectConductorXCode == Guid.Empty)
            {
                if (conductor.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                conductor.XCode = SelectConductorXCode;
                if (conductor.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectConductorXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectConductorXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectConductorXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EConductor.DeleteX(SelectConductorXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        public void BindDataToOwnControl(Guid XCode)
        {
            SelectConductorXCode = XCode;
            Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByXCode(XCode);
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(conductor.ProductCode);

            cboMaterial.SelectedValue = conductor.MaterialCode;
            cboDamperType.SelectedValue = conductor.DamperType;
            txtName.Text = product.Name;
            productCode = product.Code;

            txtAlpha.Text = Convert.ToString(Math.Round(conductor.Alpha, 2));
            txtAlsticity.Text = Convert.ToString(Math.Round(conductor.Alasticity, 2));
            txtCrossSectionArea.Text = Convert.ToString(Math.Round(conductor.CrossSectionArea, 2));
            txtDiagonal.Text = Convert.ToString(Math.Round(conductor.Diagonal, 2));
            txtMaxCurrent.Text = Convert.ToString(Math.Round(conductor.MaxCurrent, 2));
            txtReactance.Text = Convert.ToString(Math.Round(conductor.Reactance, 2));
            txtResistance.Text = Convert.ToString(Math.Round(conductor.Resistance));
            txtUTS.Text = Convert.ToString(Math.Round(conductor.UTS, 2));
            txtWeight.Text = Convert.ToString(Math.Round(conductor.Weight, 2));
            txtComment.Text = conductor.Comment;
            Code = conductor.Code;
        }

        private void SelectProductByCode()
        {
            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(productCode);
            txtName.Text = product.Name;
        }

        private void BindMaterialToComboBox()
        {
            //Extra
            //cboMaterial.ValueMember = "Code";
            //cboMaterial.DisplayMember = "Name";
            //cboMaterial.DataSource = Atend.Base.Equipment.EConductorMaterialType.SelectAll();

        }

        private void BindDamperTypeToComboBox()
        {
            //Extra
            //cboDamperType.ValueMember = "Code";
            //cboDamperType.DisplayMember = "Name";
            //cboDamperType.DataSource = Atend.Base.Equipment.EConductorDamperType.SelectAll();

        }

        private void frmConductor_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            SelectProductByCode();


            BindMaterialToComboBox();
            BindDamperTypeToComboBox();


            Atend.Base.Base.BProduct product = Atend.Base.Base.BProduct.Select_ByXCode(productCode);
            txtName.Text = product.Name;



            if (cboMaterial.Items.Count > 0)
            {
                cboMaterial.SelectedIndex = cboMaterial.Items.Count - 1;
            }

            if (cboDamperType.Items.Count > 0)
            {
                cboDamperType.SelectedIndex = cboDamperType.Items.Count - 1;
            }

        }

        private void btnNewMaterial_Click(object sender, EventArgs e)
        {
            //Extra
            //frmConductorMaterial frmmaterial = new frmConductorMaterial();
            //frmmaterial.ShowDialog();
            //BindMaterialToComboBox();
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

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCabelSearch02 frmcabelSearch = new frmCabelSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmcabelSearch);
        }

        private void btnNewDamper_Click(object sender, EventArgs e)
        {
            //frmDamperType frmdamperType = new frmDamperType();
            //frmdamperType.ShowDialog();
            //BindDamperTypeToComboBox();
        }


    }
}