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
    public partial class frmGroundCabelTip02 : Form
    {
        bool ForceToClose = false;
        int Code = -1;

        public frmGroundCabelTip02()
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

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public bool IsDefault = false;

        public Guid SelectGroundCabelTipXCode = Guid.Empty;

        private void BindToComboBox()
        {
            cboPhaseProductCode.ValueMember = "XCode";
            cboPhaseProductCode.DisplayMember = "Name";
            cboPhaseProductCode.DataSource = Atend.Base.Equipment.EGroundCabel.SelectAllX();

            cboNeutralProductCode.ValueMember = "XCode";
            cboNeutralProductCode.DisplayMember = "Name";
            cboNeutralProductCode.DataSource = Atend.Base.Equipment.EGroundCabel.SelectAllX();

        }

        public void Reset()
        {
            txtName.Text = string.Empty;
            cboPhaseCount.SelectedIndex = 0;

            if(cboPhaseProductCode.Items.Count > 0 )
                cboPhaseProductCode.SelectedIndex = 0;


            cboNeutralCount.SelectedIndex = 0;
            
            if(cboNeutralProductCode.Items.Count > 0 )
                cboNeutralProductCode.SelectedIndex = 0;

            SelectGroundCabelTipXCode = Guid.Empty;


            txtPhaseCross.Text = "00";
            txtNeutralCross.Text = "00";
            txtType.Text = string.Empty;

            IsDefault = false;
            tsbIsDefault.Checked = false;
            Code = -1;
        }

        private bool CheckStatuseOfAccessChangeDefault()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessChangeDefault={0}\n", Atend.Control.Common.AccessChangeDefault);
            if (!Atend.Control.Common.AccessChangeDefault)
            {
                if (SelectGroundCabelTipXCode == Guid.Empty && IsDefault)
                {
                    MessageBox.Show("کاربر گرامی شما اجازه ثبت تجهیز به صورت پیش فرض ندارید", "خطا");
                    return false;
                }
                else
                {
                    Atend.Base.Equipment.EGroundCabelTip Equip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(SelectGroundCabelTipXCode);
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
            if (Atend.Base.Equipment.EGroundCabelTip.SearchByName(txtName.Text) == true && SelectGroundCabelTipXCode == Guid.Empty)
            {
                MessageBox.Show("نام قبلا استفاده شده است", "خطا");
                txtName.Focus();
                return false;
            }
            Atend.Base.Equipment.EGroundCabelTip groundtip = Atend.Base.Equipment.EGroundCabelTip.CheckForExist(Convert.ToInt32(cboPhaseCount.SelectedItem.ToString()), new Guid(cboPhaseProductCode.SelectedValue.ToString()),
                                                                                                              Convert.ToInt32(cboNeutralCount.SelectedItem.ToString()), new Guid(cboNeutralProductCode.SelectedValue.ToString()));
            if (groundtip.Code != -1 && SelectGroundCabelTipXCode == Guid.Empty)
            {
                if (MessageBox.Show("تیپ بندی با مشخصات داده شده موجود میباشد\n\n تیپ بندی با مشخصات فوق  : " + groundtip.Name + "\n\n" + "آیا مایل به ادامه  ثبت می باشید؟", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cboPhaseProductCode.Focus();
                    return false;
                }
            }

            return CheckStatuseOfAccessChangeDefault();
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
                cmd += string.Format("({0}*{1})", lblPhase.Text, txtPhaseCross.Text);
                sw = true;
                swPhase = true;
            }
            if (txtNeutralCross.Text != "0")
            {
                if (sw)
                    cmd += " + ";
                cmd += string.Format("{0}", txtNeutralCross.Text);
                sw = true;
            }

            if (swPhase)
            {
                //if (sw)
                //    cmd += " + ";
                cmd += "  " + string.Format("{0}", txtType.Text);
                sw = true;
            }

            cmd += "  Cable Ground";

            //if (swPhase)
            //{
            //    cmd += "  " + lblMaterialCode.Text;
            //}

            ed.WriteMessage(cmd + "\n");
            Atend.Base.Equipment.EGroundCabelTip GroundCabeltip = new Atend.Base.Equipment.EGroundCabelTip();
            GroundCabeltip.Name = txtName.Text;
            GroundCabeltip.PhaseCount = Convert.ToInt32(cboPhaseCount.SelectedItem.ToString());
            GroundCabeltip.NeutralCount = Convert.ToInt32(cboNeutralCount.SelectedItem.ToString());
            //GroundCabeltip.NightCount = Convert.ToInt32(cboNightCount.SelectedItem.ToString());
            GroundCabeltip.NeutralProductXCode = new Guid(cboNeutralProductCode.SelectedValue.ToString());
            GroundCabeltip.PhaseProductXCode = new Guid(cboPhaseProductCode.SelectedValue.ToString());
            //GroundCabeltip.NightProductXCode = new Guid(cboNightProductCode.SelectedValue.ToString());
            GroundCabeltip.Description = cmd;
            GroundCabeltip.IsDefault = IsDefault;
            //ed.WriteMessage("Go To Insert\n");
            GroundCabeltip.Code = Code;
            if (SelectGroundCabelTipXCode == Guid.Empty)
            {
                if (GroundCabeltip.InsertX())
                    Reset();
                else
                    MessageBox.Show("امکان ثبت اطلاعات نمی باشد", "خطا");
            }
            else
            {
                GroundCabeltip.XCode = SelectGroundCabelTipXCode;
                if (GroundCabeltip.UpdateX())
                    Reset();
                else
                    MessageBox.Show("امکان به روز رسانی اطلاعات نمی باشد", "خطا");
            }
        }

        private void Delete()
        {
            string name = string.Empty;
            if (!Atend.Global.Utility.UBinding.ExistInSubEquip(SelectGroundCabelTipXCode,out name))
            {
                MessageBox.Show(string.Format("حذف بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد\n تجهیز موردنظر زیر تجهیز '{0}' میباشد ", name), "خطا");

                return;
            }
            //Atend.Base.Equipment.EProductPackage _ProductPackage = Atend.Base.Equipment.EProductPackage.SelectByXCode(SelectGroundCabelTipXCode);
            //if (_ProductPackage.Code != -1)
            //{
            //    MessageBox.Show("حذف  بدلیل وجود در تجهیزات جانبی امکانپذیر نمی باشد ", "خطا");
            //    return;
            //}

            if (MessageBox.Show("آیا از حذف کردن اطلاعات اطمینان دارید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (SelectGroundCabelTipXCode != Guid.Empty)
                {
                    if (Atend.Base.Equipment.EGroundCabelTip.DeleteX(SelectGroundCabelTipXCode))
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

            SelectGroundCabelTipXCode = XCode;
            Atend.Base.Equipment.EGroundCabelTip GroundCabeltip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(XCode);

            txtName.Text = Convert.ToString(GroundCabeltip.Name.ToString());
            cboPhaseCount.SelectedIndex = Convert.ToInt32(GroundCabeltip.PhaseCount.ToString());
            cboNeutralCount.SelectedIndex = Convert.ToInt32(GroundCabeltip.NeutralCount.ToString());
            //cboNightCount.SelectedIndex = Convert.ToInt32(GroundCabeltip.NightCount.ToString());
            cboPhaseProductCode.SelectedValue = new Guid(GroundCabeltip.PhaseProductXCode.ToString());
            cboNeutralProductCode.SelectedValue = new Guid(GroundCabeltip.NeutralProductXCode.ToString());
            //cboNightProductCode.SelectedValue = new Guid(GroundCabeltip.NightProductXCode.ToString());
            tsbIsDefault.Checked = GroundCabeltip.IsDefault;
            Code = GroundCabeltip.Code;
        }

        private void cboPhaseProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhaseProductCode.Items.Count > 0)
            {
                Guid XCode = new Guid(cboPhaseProductCode.SelectedValue.ToString());
                //ed.WriteMessage(".SelectedValue=" + cboPhaseProductCode.SelectedValue.ToString() + "\n");
                Atend.Base.Equipment.EGroundCabel c1 = Atend.Base.Equipment.EGroundCabel.SelectByXCode(XCode);
                //ed.WriteMessage("CrossSectionArea=" + c1.CrossSectionArea + "\n");
                txtPhaseCross.Text = Convert.ToString(c1.Size);
                if (c1.Type == 1)
                    txtType.Text = "XLPE از جنس آلمینیوم با عایق خشک";

                if (c1.Type == 2)
                    txtType.Text = "XLPE از جنس مس با عایق خشک";

                if (c1.Type == 3)
                    txtType.Text = "PVC از جنس آلمینیوم با عایق خشک";

                if (c1.Type == 4)
                    txtType.Text = "PVC از جنس مس با عایق خشک";
            }
            //byte mat = Convert.ToByte(c1.MaterialCode.ToString());
            //Atend.Base.Equipment.EGroundCabelMaterialType m1 = Atend.Base.Equipment.EGroundCabelMaterialType.SelectByCode(mat);
            //lblMaterialCode.Text = m1.Name;
            //switch (c1.TypeCode)
            //{
            //    case 0: { lblMaterialCode.Text = "CU"; break; }
            //    case 1: { lblMaterialCode.Text = "AAC"; break; }
            //    case 2: { lblMaterialCode.Text = "ACSR"; break; }
            //    case 3: { lblMaterialCode.Text = "AAAC"; break; }
            //}

        }

        private void cboPhaseCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPhase.Text = cboPhaseCount.SelectedIndex.ToString();
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

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbShare_Click(object sender, EventArgs e)
        {
            if (SelectGroundCabelTipXCode != Guid.Empty)
            {
                if (Atend.Base.Equipment.EContainerPackage.ShareOnServer(Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabelTip), SelectGroundCabelTipXCode))
                {
                    Atend.Base.Equipment.EGroundCabelTip CabelTip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(SelectGroundCabelTipXCode );
                    Code = CabelTip.Code;
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

            //if (SelectGroundCabelTipXCode != Guid.Empty)
            //{
            //    if (Atend.Base.Equipment.EGroundCabelTip.ShareOnServer(SelectGroundCabelTipXCode))
            //        MessageBox.Show("به اشتراک گذاری با موفقیت انجام شد");
            //    else
            //        MessageBox.Show("خطا در به اشتراک گذاری . لطفاً دوباره سعی کنید");
            //}
            //else
            //    MessageBox.Show("لطفا تجهیز مورد نظر را انتخاب کنید");
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

        private void frmGroundCabelTip02_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindToComboBox();

            Reset();
            EventArgs e1 = new EventArgs();


            //cboNeutralProductCode_SelectedIndexChanged(cboNeutralProductCode, e1);
            cboPhaseProductCode_SelectedIndexChanged(cboPhaseProductCode, e1);
            //cboNightProductCode_SelectedIndexChanged(cboNightProductCode, e1);
        }

        private void جستجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGroundCabelTipSearch02 gct = new frmGroundCabelTipSearch02(this);
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(gct);
        }

        private void cboNeutralProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid XCode = new Guid(cboNeutralProductCode.SelectedValue.ToString());
            ed.WriteMessage(".SelectedValue=" + cboNeutralProductCode.SelectedValue.ToString() + "\n");
            Atend.Base.Equipment.EGroundCabel c1 = Atend.Base.Equipment.EGroundCabel.SelectByXCode(XCode);
            ed.WriteMessage("CrossSectionArea=" + c1.CrossSectionArea + "\n");
            txtNeutralCross.Text = Convert.ToString(c1.Size);
        }

    }
}