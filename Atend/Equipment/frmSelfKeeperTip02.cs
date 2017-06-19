using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Equipment
{
    public partial class frmSelfKeeperTip02 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public bool IsDefault = false;
        bool ForceToClose = false;
        int Code = -1;

        public frmSelfKeeperTip02()
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

        public Guid SelectConductorTip = Guid.Empty;

        private void frmConductorTip_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Reset();
            BindToComboBox();
            EventArgs e1 = new EventArgs();



        }

        private void BindToComboBox()
        {
            cboPhaseProductCode.ValueMember = "XCode";
            cboPhaseProductCode.DisplayMember = "Name";
            cboPhaseProductCode.DataSource = Atend.Base.Equipment.ESelfKeeper.SelectAllX();

            cboNeutralProductCode.ValueMember = "XCode";
            cboNeutralProductCode.DisplayMember = "Name";
            cboNeutralProductCode.DataSource = Atend.Base.Equipment.ESelfKeeper.SelectAllX();

            cboNightProductCode.ValueMember = "XCode";
            cboNightProductCode.DisplayMember = "Name";
            cboNightProductCode.DataSource = Atend.Base.Equipment.ESelfKeeper.SelectAllX();
            DataTable dt = Atend.Base.Equipment.ESelfKeeper.SelectAllX();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ed.WriteMessage("dr[Name]=" + dr["Name"].ToString() + "\n");
            //}
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            txtName.Text = string.Empty;
            if (cboPhaseCount.Items.Count > 0)
                cboPhaseCount.SelectedIndex = 0;

            if (cboPhaseProductCode.Items.Count > 0)
                cboPhaseProductCode.SelectedIndex = 0;

            if (cboNeutralCount.Items.Count > 0)
                cboNeutralCount.SelectedIndex = 0;

            if (cboNeutralProductCode.Items.Count > 0)
                cboNeutralProductCode.SelectedIndex = 0;

            if (cboNightCount.Items.Count > 0)
                cboNightCount.SelectedIndex = 0;

            if (cboNightProductCode.Items.Count > 0)
                cboNightProductCode.SelectedIndex = 0;

            SelectConductorTip = Guid.Empty;
            cboCrosSectionCount.SelectedIndex = 0;
            cboCrossSection.SelectedIndex = 0;

            lblPhaseCross.Text = "00";
            lblNeutralCross.Text = "00";
            lblNightCross.Text = "00";
            lblMaterialCode.Text = string.Empty;
            lblCross.Text = "00";
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
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectConductorTip == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.ESelfKeeperTip selfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(SelectConductorTip);
                    if (selfKeeperTip.IsDefault || IsDefault)
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
            if (Atend.Base.Equipment.ESelfKeeperTip.SearchByName(txtName.Text) == true && SelectConductorTip == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است");
                return false;
            }

            Atend.Base.Equipment.ESelfKeeperTip selftip = Atend.Base.Equipment.ESelfKeeperTip.CheckForExist(Convert.ToInt32(cboPhaseCount.SelectedItem.ToString()), new Guid(cboPhaseProductCode.SelectedValue.ToString()),
                                                                                                          Convert.ToInt32(cboNeutralCount.SelectedItem.ToString()), new Guid(cboNeutralProductCode.SelectedValue.ToString()),
                                                                                                          Convert.ToInt32(cboNightCount.SelectedItem.ToString()), new Guid(cboNightProductCode.SelectedValue.ToString()),
                                                                                                          Convert.ToInt32(cboCrosSectionCount.SelectedItem.ToString()), Convert.ToInt32(cboCrossSection.SelectedItem.ToString()));
            if (selftip.Code != -1 && SelectConductorTip == Guid.Empty)
            {
                if (MessageBox.Show("تیپ بندی با مشخصات داده شده موجود میباشد\n\n تیپ بندی با مشخصات فوق  : " + selftip.Name+ "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboNightProductCode.Focus();
                    return false;
                }
            }
        
           return  CheckStatuseOfAccessChangeDefault();
            // return true;
        }

        private void Save()
        {
            txtName.Focus();
            string cmd = string.Empty;
            bool sw = false;
            bool swPhase = false;
            if (lblPhase.Text != "0")
            {
                cmd += string.Format("{0}*{1}", lblPhase.Text, lblPhaseCross.Text);
                sw = true;
                swPhase = true;
            }
            if (lblNeutral.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}*{1}", lblNeutral.Text, lblNeutralCross.Text);
                sw = true;
            }
            if (lblNight.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}*{1}", lblNight.Text, lblNightCross.Text);
            }
            if (lblCross.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}*{1}", lblCross.Text, lblCrossSectionArea.Text);
            }
            if (swPhase)
            {
                cmd += "  " + lblMaterialCode.Text;
            }

            //ed.WriteMessage(cmd+"\n");
            Atend.Base.Equipment.ESelfKeeperTip selfKeeperTip = new Atend.Base.Equipment.ESelfKeeperTip();
            selfKeeperTip.Name = txtName.Text;
            selfKeeperTip.PhaseCount = Convert.ToInt32(cboPhaseCount.SelectedItem.ToString());
            selfKeeperTip.NeutralCount = Convert.ToInt32(cboNeutralCount.SelectedItem.ToString());
            selfKeeperTip.NightCount = Convert.ToInt32(cboNightCount.SelectedItem.ToString());
            selfKeeperTip.NeutralProductxCode = new Guid(cboNeutralProductCode.SelectedValue.ToString());
            selfKeeperTip.PhaseProductxCode = new Guid(cboPhaseProductCode.SelectedValue.ToString());
            selfKeeperTip.NightProductxCode = new Guid(cboNightProductCode.SelectedValue.ToString());
            selfKeeperTip.Description ="کابل خودنگهدار "+ cmd;
            selfKeeperTip.CrossSection = Convert.ToInt32(cboCrossSection.SelectedItem.ToString());
            selfKeeperTip.CrossSectionCount = Convert.ToInt32(cboCrosSectionCount.SelectedItem.ToString());
            selfKeeperTip.IsDefault = IsDefault;
            selfKeeperTip.Code = Code;
            //ed.WriteMessage("XXXXXXXXXXXX save:{0}\n", selfKeeperTip.Code);
            if (SelectConductorTip == Guid.Empty)
            {
                if (selfKeeperTip.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                selfKeeperTip.XCode = SelectConductorTip;
                if (selfKeeperTip.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectConductorTip, out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectConductorTip);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectConductorTip != Guid.Empty)
                {
                    if (Atend.Base.Equipment.ESelfKeeperTip.DeleteX(SelectConductorTip))
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
            frmSelfKeeperTipSearch02 selfKeepertipSearch = new frmSelfKeeperTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(selfKeepertipSearch);
        }

        public void BindDataToOwnControl(Guid _Code)
        {
            SelectConductorTip = _Code;
            Atend.Base.Equipment.ESelfKeeperTip selfKeeper = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(_Code);

            txtName.Text = Convert.ToString(selfKeeper.Name.ToString());
            cboPhaseCount.SelectedIndex = Convert.ToInt32(selfKeeper.PhaseCount.ToString());
            cboNeutralCount.SelectedIndex = Convert.ToInt32(selfKeeper.NeutralCount.ToString());
            cboNightCount.SelectedIndex = Convert.ToInt32(selfKeeper.NightCount.ToString());
            cboPhaseProductCode.SelectedValue = new Guid(selfKeeper.PhaseProductxCode.ToString());
            cboNeutralProductCode.SelectedValue = new Guid(selfKeeper.NeutralProductxCode.ToString());
            cboNightProductCode.SelectedValue = new Guid(selfKeeper.NightProductxCode.ToString());
            cboCrosSectionCount.SelectedIndex = Convert.ToInt32(selfKeeper.CrossSectionCount);
            cboCrossSection.Text = selfKeeper.CrossSection.ToString();
            tsbIsDefault.Checked = selfKeeper.IsDefault;
            Code = selfKeeper.Code;
            //ed.WriteMessage("XXXXXXXXXXXX own:{0}\n", Code);
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
            frmSelfKeeperTipSearch02 Search = new frmSelfKeeperTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Search);
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
            Atend.Base.Equipment.ESelfKeeper c1 = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(new Guid(cboPhaseProductCode.SelectedValue.ToString()));
            lblPhaseCross.Text = Convert.ToString(c1.CrossSectionAreaConductor);
            byte mat = Convert.ToByte(c1.MaterialConductorCode.ToString());
            //Extra
            //Atend.Base.Equipment.ESelfKeeperMaterialConductor m1 = Atend.Base.Equipment.ESelfKeeperMaterialConductor.SelectByCode(mat);
            //lblMaterialCode.Text = m1.Name;


            //MessageBox.Show(cboPhaseProductCode.SelectedValue.ToString());
        }

        private void cboPhaseCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPhase.Text = cboPhaseCount.SelectedIndex.ToString();
            if (cboPhaseCount.SelectedIndex != 0)
                cboPhaseProductCode_SelectedIndexChanged(cboPhaseProductCode, e);

        }

        private void cboNeutralCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNeutral.Text = cboNeutralCount.SelectedIndex.ToString();
            if (cboNeutralCount.SelectedIndex != 0)
                cboNeutralProductCode_SelectedIndexChanged(cboNeutralProductCode, e);
        }

        private void cboNightCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNight.Text = cboNightCount.SelectedIndex.ToString();
            if (cboNightCount.SelectedIndex != 0)
                cboNightProductCode_SelectedIndexChanged(cboNightProductCode, e);
        }

        private void cboNeutralProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atend.Base.Equipment.ESelfKeeper c1 = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(new Guid(cboNeutralProductCode.SelectedValue.ToString()));
            lblNeutralCross.Text = Convert.ToString(c1.CrossSectionAreaConductor);
        }

        private void cboNightProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atend.Base.Equipment.ESelfKeeper c1 = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(new Guid(cboNightProductCode.SelectedValue.ToString()));
            lblNightCross.Text = Convert.ToString(c1.CrossSectionAreaConductor);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCrosSectionCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCross.Text = cboCrosSectionCount.Text;
            if (cboCrosSectionCount.SelectedIndex != 0)
            {
                cboCrossSection_SelectedIndexChanged(cboCrossSection, e);

            }
        }

        private void cboCrossSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCrossSectionArea.Text = cboCrossSection.Text;
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

            if (SelectConductorTip != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeperTip), SelectConductorTip))
                {
                    Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(SelectConductorTip);
                    Code = SelfTip.Code;
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

            //if (SelectConductorTip != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.ESelfKeeperTip.ShareOnServer(SelectConductorTip))
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
        }

        private void lblMaterialCode_Click(object sender, EventArgs e)
        {

        }


    }
}