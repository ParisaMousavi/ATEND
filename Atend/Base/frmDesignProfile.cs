using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Atend.Base
{
    public partial class frmDesignProfile : Form
    {
        bool IsValid = false;
        int Code = -1;
        int DesignId = 0;
        bool ForceToClose = false;
        public frmDesignProfile()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
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

        private void ذخیرهسازیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
                Save();

        }

        public void Save()
        {
            Atend.Base.Design.DDesignProfile DP = new Atend.Base.Design.DDesignProfile();
            DP.DesignId = DesignId;
            DP.DesignName = txtDesignName.Text;
            DP.DesignCode = Convert.ToString(txtDesignCode.Text);
            DP.Scale = Convert.ToSingle(txtScale.Text);
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string a = txtDate.Text;
            string[] result = a.Split('/');
            DP.DesignDate = p.ToDateTime(Convert.ToInt32(result[0]), Convert.ToInt32(result[1]), Convert.ToInt32(result[2]), 0, 0, 0, 0);
            DP.Address = txtAddres.Text;
            //DP.Zone = txtZone.Text;
            DP.Zone = Convert.ToInt32(cboZone.SelectedValue.ToString());
            DP.Validate = txtValidate.Text;
            DP.Employer = txtEmployer.Text;
            DP.Adviser = txtAdviser.Text;
            DP.Designer = txtDesigner.Text;
            DP.Controller = txtController.Text;
            DP.Supporter = txtSupporter.Text;
            DP.Approval = txtApproval.Text;
            DP.Planner = txtPlanner.Text;
            DP.Id = Code;
            DP.Edition = txtEdition.Text;

            if (!IsValid)
            {
                if (DP.AccessInsert())
                    MessageBox.Show("با موفقيت ثبت شد");
                else
                    MessageBox.Show("خطا در ثبت اطلاعات");
            }
            else
            {
                if (DP.AccessUpdate())
                    MessageBox.Show("با موفقيت ويرايش شد");
                else
                    MessageBox.Show("خطا در ويرايش اطلاعات");
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtDesignName.Text))
            {
                MessageBox.Show("لطفاً نام طرح را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtDesignCode.Text))
            {
                MessageBox.Show("لطفاً شماره طرح را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtScale.Text))
            {
                MessageBox.Show("لطفاً مقیاس طرح را انتخاب كنيد");
                return false;
            }


            if (!Atend.Control.NumericValidation.DoubleConverter(txtScale.Text))
            {
                MessageBox.Show("لطفاً مقیاس طرح را با فرمت مناسب وارد نمایید");
                return false;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MessageBox.Show("لطفاً تاریخ طرح را انتخاب كنيد");
                return false;
            }

            if (!Atend.Control.NumericValidation.DateConverter(txtDate.Text))
            {
                MessageBox.Show("لطفاً تاریخ طرح را با فرمت مناسب وارد نمایید.");
                return false;
            }

            if (string.IsNullOrEmpty(txtAddres.Text))
            {
                MessageBox.Show("لطفاً آدرس طرح را انتخاب كنيد");
                return false;
            }

            //if (string.IsNullOrEmpty(txtZone.Text))
            //{
            //    MessageBox.Show("لطفاً منطقه برق را انتخاب كنيد");
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtValidate.Text))
            {
                MessageBox.Show("لطفاً مدت اعتبار را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtEmployer.Text))
            {
                MessageBox.Show("لطفاً کارفرما را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtAdviser.Text))
            {
                MessageBox.Show("لطفاً مشاور را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtDesigner.Text))
            {
                MessageBox.Show("لطفاً طراح را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtController.Text))
            {
                MessageBox.Show("لطفاً کنترل کننده را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtSupporter.Text))
            {
                MessageBox.Show("لطفاً تائید کننده را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtApproval.Text))
            {
                MessageBox.Show("لطفاً تصویب کننده را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtPlanner.Text))
            {
                MessageBox.Show("لطفاً نقشه بردار را انتخاب كنيد");
                return false;
            }

            if (string.IsNullOrEmpty(txtEdition.Text))
            {
                MessageBox.Show("لطفاً ویرایش را انتخاب كنيد");
                return false;
            }
            return true;

        }

        private void frmDesignProfile_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToCboZone();
            BindDataToCboSize();
            if (cboZone.Items.Count > 0)
                cboZone.SelectedIndex = 0;
            if (cboSize.Items.Count > 0)
                cboSize.SelectedIndex = 0;
            string CurrentDate = "";
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            CurrentDate = string.Format("{0}/{1:00}/{2:00}", p.GetYear(DateTime.Now), p.GetMonth(DateTime.Now), p.GetDayOfMonth(DateTime.Now));
            txtDate.Text = CurrentDate;
            Atend.Base.Design.DDesignProfile DP = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DP.Id != 0)
            {
                IsValid = true;
                Code = DP.Id;
                DesignId = DP.DesignId;
                txtDesignName.Text = DP.DesignName;
                txtDesignCode.Text = DP.DesignCode.ToString();
                txtScale.Text = DP.Scale.ToString();
                txtDate.Text = string.Format("{0}/{1:00}/{2:00}", p.GetYear(DP.DesignDate), p.GetMonth(DP.DesignDate), p.GetDayOfMonth(DP.DesignDate));
                txtAddres.Text = DP.Address;
                //txtZone.Text = DP.Zone;
                if (DP.Zone == 0)
                    cboZone.SelectedIndex = 0;
                else
                    cboZone.SelectedValue = DP.Zone;

                txtValidate.Text = DP.Validate;
                txtEmployer.Text = DP.Employer;
                txtAdviser.Text = DP.Adviser;
                txtDesigner.Text = DP.Designer;
                txtController.Text = DP.Controller;
                txtSupporter.Text = DP.Supporter;
                txtApproval.Text = DP.Approval;
                txtPlanner.Text = DP.Planner;
                txtEdition.Text = DP.Edition;
            }
            else
            {
                txtDate.Text = CurrentDate;
            }
        }

        private void BindDataToCboZone()
        {
            cboZone.ValueMember = "Code";
            cboZone.DisplayMember = "Name";
            cboZone.DataSource = Atend.Base.Base.BRegion.SelectAllLocal();
        }

        private void BindDataToCboSize()
        {
            cboSize.ValueMember = "Code";
            cboSize.DisplayMember = "Name";
            cboSize.DataSource = Atend.Base.Design.DPrintSize.SelectAll();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ذخیرهسازیوخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Save();
                Close();
            }
        }

        private void btnDesignCode_Click(object sender, EventArgs e)
        {
            string strCode = string.Empty;
            strCode = Atend.Base.Base.BRegion.SelectByCode(Convert.ToInt32(cboZone.SelectedValue.ToString())).SecondCode.Substring(1, 2).ToString();
            strCode += cboSize.Text;
            strCode += txtDate.Text.Substring(2, 2).ToString();
            Random r = new Random(DateTime.Now.Millisecond);
            strCode += string.Format("{0:0000}", r.Next(0, 9999)).ToString();
            txtDesignCode.Text = strCode;
        }
    }
}