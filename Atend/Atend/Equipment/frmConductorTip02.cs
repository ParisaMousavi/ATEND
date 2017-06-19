using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

//get from tehran 7/15
namespace Atend.Equipment
{
    public partial class frmConductorTip02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public bool IsDefault = false;
        bool ForceToClose = false;
        int Code = -1;

        public frmConductorTip02()
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

        public Guid SelectConductorTipXCode = Guid.Empty;

        private void frmConductorTip_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();


            BindToComboBox();
            EventArgs e1 = new EventArgs();

            Reset();
            cboNeutralProductCode_SelectedIndexChanged(cboNeutralProductCode, e1);
            cboPhaseProductCode_SelectedIndexChanged(cboPhaseProductCode, e1);
            cboNightProductCode_SelectedIndexChanged(cboNightProductCode, e1);
        }

        private void BindToComboBox()
        {
            cboPhaseProductCode.ValueMember = "XCode";
            cboPhaseProductCode.DisplayMember = "Name";
            cboPhaseProductCode.DataSource = Atend.Base.Equipment.EConductor.SelectAllX();

            cboNeutralProductCode.ValueMember = "XCode";
            cboNeutralProductCode.DisplayMember = "Name";
            cboNeutralProductCode.DataSource = Atend.Base.Equipment.EConductor.SelectAllX();

            cboNightProductCode.ValueMember = "XCode";
            cboNightProductCode.DisplayMember = "Name";
            cboNightProductCode.DataSource = Atend.Base.Equipment.EConductor.SelectAllX();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            txtName.Text = string.Empty;
            if (cboPhaseProductCode.Items.Count > 0)
                cboPhaseProductCode.SelectedIndex = 0;
            if (cboNeutralProductCode.Items.Count > 0)
                cboNeutralProductCode.SelectedIndex = 0;
            if (cboNightProductCode.Items.Count > 0)
                cboNightProductCode.SelectedIndex = 0;
            cboPhaseCount.SelectedIndex = 0;
            cboNeutralCount.SelectedIndex = 0;
            cboNightCount.SelectedIndex = 0;
            SelectConductorTipXCode = Guid.Empty;

            txtPhaseCross.Text = "00";
            txtNeutralCross.Text = "00";
            txtNightCross.Text = "00";
            lblMaterialCode.Text = string.Empty;
            IsDefault = false;
            tsbIsDefault.Checked = false;
            Code = -1;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectConductorTipXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EConductorTip Equip = Atend.Base.Equipment.EConductorTip.SelectByXCode(SelectConductorTipXCode);
                    if (Equip.IsDefault || IsDefault)
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
                MessageBox.Show("لطفاً نام تيپ را مشخص نمایید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                txtName.Focus();
                return false;
            }
            if (Atend.Base.Equipment.EConductorTip.SearchByName(txtName.Text) == true && SelectConductorTipXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            Atend.Base.Equipment.EConductorTip conductortip = Atend.Base.Equipment.EConductorTip.CheckForExist(Convert.ToInt32(cboPhaseCount.SelectedItem.ToString()), new Guid(cboPhaseProductCode.SelectedValue.ToString()),
                                                                                                           Convert.ToInt32(cboNeutralCount.SelectedItem.ToString()), new Guid(cboNeutralProductCode.SelectedValue.ToString()),
                                                                                                           Convert.ToInt32(cboNightCount.SelectedItem.ToString()), new Guid(cboNightProductCode.SelectedValue.ToString()));
            if (conductortip.Code != -1 && SelectConductorTipXCode == Guid.Empty)
            {
                if (MessageBox.Show("تیپ بندی سیم با مشخصات داده شده موجود میباشد\n\n تیپ بندی سیم با مشخصات فوق  : " + conductortip.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboPhaseProductCode.Focus();
                    return false;
                }
            }
            return CheckStatuseOfAccessChangeDefault();
            //return true;
        }

        private void Save()
        {
            txtName.Focus();
            string cmd = string.Empty;
            bool sw = false;
            bool swPhase = false;
            if (lblPhase.Text != "0")
            {
                cmd += string.Format("{0}*{1}", lblPhase.Text, txtPhaseCross.Text);
                sw = true;
                swPhase = true;
            }
            if (lblNeutral.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}*{1}", lblNeutral.Text, txtNeutralCross.Text);
                sw = true;
            }
            if (lblNight.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}*{1}", lblNight.Text, txtNightCross.Text);
            }
            if (swPhase)
            {
                cmd += "  " + lblMaterialCode.Text;
            }

            Atend.Base.Equipment.EConductorTip conductortip = new Atend.Base.Equipment.EConductorTip();
            conductortip.Name = txtName.Text;
            conductortip.PhaseCount = Convert.ToInt32(cboPhaseCount.SelectedItem.ToString());
            conductortip.NeutralCount = Convert.ToInt32(cboNeutralCount.SelectedItem.ToString());
            conductortip.NightCount = Convert.ToInt32(cboNightCount.SelectedItem.ToString());
            conductortip.NeutralProductXCode = new Guid(cboNeutralProductCode.SelectedValue.ToString());
            conductortip.PhaseProductXCode = new Guid(cboPhaseProductCode.SelectedValue.ToString());
            conductortip.NightProductXCode = new Guid(cboNightProductCode.SelectedValue.ToString());
            conductortip.Description = cmd;
            conductortip.IsDefault = IsDefault;
            conductortip.Code = Code;
            if (SelectConductorTipXCode == Guid.Empty)
            {
                if (conductortip.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                conductortip.XCode = SelectConductorTipXCode;
                if (conductortip.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectConductorTipXCode, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectConductorTipXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectConductorTipXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EConductorTip.DeleteX(SelectConductorTipXCode))
                        Reset();
                    else
                        MessageBox.Show("امکان حذف کردن اطلاعات نمی باشد", "خطا");
                }
                else
                    MessageBox.Show("لطفاً گزینه مورد نظر را انتخاب نمایید", "خطا");
            }

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            frmConductorTipSearch02 frmconducotortipSearch = new frmConductorTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmconducotortipSearch);
        }

        public void BindDataToOwnControl(Guid XCode)
        {
            SelectConductorTipXCode = XCode;
            Atend.Base.Equipment.EConductorTip conductortip = Atend.Base.Equipment.EConductorTip.SelectByXCode(XCode);

            txtName.Text = Convert.ToString(conductortip.Name.ToString());
            cboPhaseCount.SelectedIndex = Convert.ToInt32(conductortip.PhaseCount.ToString());
            cboNeutralCount.SelectedIndex = Convert.ToInt32(conductortip.NeutralCount.ToString());
            cboNightCount.SelectedIndex = Convert.ToInt32(conductortip.NightCount.ToString());
            cboPhaseProductCode.SelectedValue = new Guid(conductortip.PhaseProductXCode.ToString());
            cboNeutralProductCode.SelectedValue = new Guid(conductortip.NeutralProductXCode.ToString());
            cboNightProductCode.SelectedValue = new Guid(conductortip.NightProductXCode.ToString());
            tsbIsDefault.Checked = conductortip.IsDefault;
            Code = conductortip.Code;
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
                Delete();
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConductorTipSearch02 frmconducotortipSearch = new frmConductorTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmconducotortipSearch);
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboPhaseProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid XCode = new Guid(cboPhaseProductCode.SelectedValue.ToString());
            //ed.WriteMessage(".SelectedValue="+cboPhaseProductCode.SelectedValue.ToString()+"\n");
            Atend.Base.Equipment.EConductor c1 = Atend.Base.Equipment.EConductor.SelectByXCode(XCode);
            //ed.WriteMessage("CrossSectionArea=" + c1.CrossSectionArea + "\n");
            txtPhaseCross.Text = Convert.ToString(c1.CrossSectionArea);
            //byte mat = Convert.ToByte(c1.MaterialCode.ToString());
            //Atend.Base.Equipment.EConductorMaterialType m1 = Atend.Base.Equipment.EConductorMaterialType.SelectByCode(mat);
            //lblMaterialCode.Text = m1.Name;
            switch (c1.TypeCode)
            {
                case 0: { lblMaterialCode.Text = "CU"; break; }
                case 1: { lblMaterialCode.Text = "AAC"; break; }
                case 2: { lblMaterialCode.Text = "ACSR"; break; }
                case 3: { lblMaterialCode.Text = "AAAC"; break; }
            }

        }

        private void cboPhaseCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPhase.Text = cboPhaseCount.SelectedIndex.ToString();
        }

        private void cboNeutralCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNeutral.Text = cboNeutralCount.SelectedIndex.ToString();
        }

        private void cboNightCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNight.Text = cboNightCount.SelectedIndex.ToString();
        }

        private void cboNeutralProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid XCode = new Guid(cboNeutralProductCode.SelectedValue.ToString());
            Atend.Base.Equipment.EConductor c1 = Atend.Base.Equipment.EConductor.SelectByXCode(XCode);
            txtNeutralCross.Text = Convert.ToString(c1.CrossSectionArea);
            //Atend.Base.Equipment.ESelfKeeper c1 = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Convert.ToInt32(cboNeutralProductCode.SelectedValue.ToString()));
            //txtNeutralCross.Text = Convert.ToString(c1.CrossSectionAreaConductor);
        }

        private void cboNightProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid XCode = new Guid(cboNightProductCode.SelectedValue.ToString());
            Atend.Base.Equipment.EConductor c1 = Atend.Base.Equipment.EConductor.SelectByXCode(XCode);
            txtNightCross.Text = Convert.ToString(c1.CrossSectionArea);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
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

        private void tsbShare_Click(object sender, EventArgs e)
        {

            if (SelectConductorTipXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.ConductorTip), SelectConductorTipXCode))
                {
                    Atend.Base.Equipment.EConductorTip Conductor = Atend.Base.Equipment.EConductorTip.SelectByXCode(SelectConductorTipXCode);
                    Code = Conductor.Code;
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

            //if (SelectConductorTipXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EConductorTip.ShareOnServer(SelectConductorTipXCode))
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

    }
}