using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections;

namespace Atend.Calculating
{
    public partial class frmHalter : Form
    {
        DataTable dtForceOnPole;
        double SaftyFactor;
        double SaftyFactorOburi;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ArrayList M = new ArrayList();
        ArrayList Count = new ArrayList();
        bool IsUTs;
        Guid SectionCode;
        bool ForceToClose = false;
        public frmHalter(DataTable _dtForceOnPole, double _SafteyFactor, Guid _SectionCode, bool _IsUTs,double _SaftyFactorOburi)
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
            dtForceOnPole = _dtForceOnPole;
            IsUTs = _IsUTs;
            SectionCode = _SectionCode;
            SaftyFactor = _SafteyFactor;
            SaftyFactorOburi = _SaftyFactorOburi;
            Atend.Global.Calculation.Mechanical.CalcOptimalSagTension calc = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTension();
            ed.WriteMessage("BeforSelectAll\n");
            DataTable dtHalter = Atend.Base.Equipment.EHalter.SelectAllX();
            ed.WriteMessage("BeforSelectByXCode\n");
            Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(dtHalter.Rows[0]["XCode"].ToString()));
            ed.WriteMessage("CalcResult\n");
            //DataTable dtResult=calc.CalcHalter(dtForceOnPole,halter);


            // dataGridView1.DataSource = dtResult;
        }


        public bool Validation()
        {

            if (Atend.Control.Common.Demo)
            {
                //MessageBox.Show("", "");
                return false;
            }

            if (string.IsNullOrEmpty(txtX.Text))
            {
                MessageBox.Show("لطفا X را انتخاب نمایید", "خطا");
                txtX.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtX.Text))
            {
                MessageBox.Show("لطفا X را با فرمت مناسب وارد نمایید", "خطا");
                txtX.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtY.Text))
            {
                MessageBox.Show("لطفا Y را انتخاب نمایید", "خطا");
               txtY.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtY.Text))
            {
                MessageBox.Show("لطفاY را با فرمت مناسب وارد نمایید", "خطا");
                txtY.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMAxHalterCount.Text))
            {
                MessageBox.Show("لطفا حداکثر تعداد مهار را انتخاب نمایید", "خطا");
                txtMAxHalterCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtMAxHalterCount.Text))
            {
                MessageBox.Show("لطفا حداکثر تعداد مهار را با فرمت مناسب وارد نمایید", "خطا");
                txtMAxHalterCount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDpCount.Text))
            {
                MessageBox.Show("لطفا تعداد DP را انتخاب نمایید", "خطا");
                txtDpCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtDpCount.Text))
            {
                MessageBox.Show("لطفاتعداد DP مناسب وارد نمایید", "خطا");
                txtDpCount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEntehaiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه انتهایی را انتخاب نمایید", "خطا");
                txtEntehaiCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtEntehaiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه انتهایی را با فرمت مناسب وارد نمایید", "خطا");
                txtEntehaiCount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtKesheshiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه کششی را انتخاب نمایید", "خطا");
                txtKesheshiCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtKesheshiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه کششی را با فرمت مناسب وارد نمایید", "خطا");
                txtKesheshiCount.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtOburiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه عبوری را انتخاب نمایید", "خطا");
                 txtOburiCount.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtOburiCount.Text))
            {
                MessageBox.Show("لطفا تعداد پایه عبوری را با فرمت مناسب وارد نمایید", "خطا");
                txtOburiCount.Focus();
                return false;
            }

            return true;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                //MessageBox.Show("");
                ReadData();
                Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
               // Atend.Base.Calculating.CPowerWithHalter.AccessDeleteBySectionCodeIsUTS(SectionCode, IsUTs);

               // MessageBox.Show("1");
                bool result=true;
                this.Cursor = Cursors.WaitCursor;
                DataTable dtResult = general.CalcHalter(dtForceOnPole, Convert.ToDouble(txtX.Text), Convert.ToDouble(txtY.Text), SaftyFactor, M, Count, Convert.ToInt32(txtMAxHalterCount.Text),out result,SaftyFactorOburi).Copy();
                //MessageBox.Show("result",result.ToString());
                this.Cursor = Cursors.Default;
                if (result)
                {
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        //MessageBox.Show("2");
                        Atend.Base.Calculating.CPowerWithHalter powerWithHalter = new Atend.Base.Calculating.CPowerWithHalter();
                        powerWithHalter.HalterCount = Convert.ToDouble(dr["HalterCount"].ToString());
                        powerWithHalter.HalterName = dr["Name"].ToString();
                        powerWithHalter.HalterPower = Convert.ToDouble(dr["HalterPower"].ToString());
                        powerWithHalter.IsUTS = IsUTs;
                        powerWithHalter.PoleNum = dr["DcPole"].ToString();
                        powerWithHalter.PoleCount = Convert.ToInt16(dr["Count"].ToString());
                        powerWithHalter.PoleGuid = new Guid(dr["DcPoleGuid"].ToString());
                        powerWithHalter.PolePower = Convert.ToDouble(dr["Power"].ToString());
                        powerWithHalter.SectionCode = SectionCode;
                        if (!powerWithHalter.AccessInsert())
                            ed.WriteMessage("PowerWithHalter.AccessInsert Failed \n");

                    }
                   // MessageBox.Show("Befor Re");
                    frmHalterResult result1 = new frmHalterResult(dtResult, true, IsUTs);
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(result1);
                    this.Close();
                }
                else
                {
                    ed.WriteMessage("لطفا مقادیر پیش فرض را تغییر دهید\n");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void ReadData()
        {
            M.Add(cboEntehai.Text);
            M.Add(cboOburi.Text);
            M.Add(cboKesheshi.Text);
            M.Add(cboDP.Text);

            Count.Add(txtEntehaiCount.Text);
            Count.Add(txtOburiCount.Text);
            Count.Add(txtKesheshiCount.Text);
            Count.Add(txtDpCount.Text);

        }

        private void Reset()
        {
            cboDP.SelectedIndex = 2;
            cboEntehai.SelectedIndex = 4;
            cboOburi.SelectedIndex = 1;
            cboKesheshi.SelectedIndex = 4;
        }

        private void frmHalter_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            Reset();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}