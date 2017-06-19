using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Calculating
{
    public partial class frmChoiceForce : Form
    {
        DataTable dtForceOnPole;
        bool IsUts;
        Guid SectionCode;
        bool ForceToClose = false;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public frmChoiceForce(DataTable _dtForceOnPole, bool _IsUTs, Guid _SectionCode)
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
            IsUts = _IsUTs;
            SectionCode = _SectionCode;

            Atend.Global.Calculation.Mechanical.CalcOptimalSagTension calc = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTension();
            ed.WriteMessage("BeforSelectAll\n");
            DataTable dtHalter = Atend.Base.Equipment.EHalter.SelectAllX();
            ed.WriteMessage("BeforSelectByXCode\n");
            Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(dtHalter.Rows[0]["XCode"].ToString()));
            ed.WriteMessage("CalcResult\n");
            //DataTable dtResult=calc.CalcHalter(dtForceOnPole,halter);


            //dataGridView1.DataSource = dtResult;
        }

        public bool Validation()
        {
            if (string.IsNullOrEmpty(txtSaftyFactor.Text))
            {
                MessageBox.Show("لطفا مقدار ضریب اطمینان پایه را مشخص نمایید", "خطا");
                txtSaftyFactor.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtSaftyFactor.Text))
            {
                MessageBox.Show("لطفا مقدار ضریب اطمینان پایه را با فرمت مناسب وارد نمایید", "خطا");
                txtSaftyFactor.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSaftyFactorOburi.Text))
            {
                MessageBox.Show("لطفا مقدار ضریب اطمینان پایه را مشخص نمایید", "خطا");
                txtSaftyFactorOburi.Focus();
                return false;

            }

            if (!Atend.Control.NumericValidation.DoubleConverter(txtSaftyFactorOburi.Text))
            {
                MessageBox.Show("لطفا مقدار ضریب اطمینان پایه را با فرمت مناسب وارد نمایید", "خطا");
                txtSaftyFactorOburi.Focus();
                return false;
            }
            return true;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (!Validation())
                return;
            if (rdbHalter.Checked)
            {
                frmHalter halter = new frmHalter(dtForceOnPole, Convert.ToDouble(txtSaftyFactor.Text), SectionCode, IsUts,Convert.ToDouble(txtSaftyFactorOburi.Text));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(halter);
                this.Close();
            }
            if (rdbWithoutHalter.Checked)
            {
                Atend.Base.Calculating.CPowerWithOutHalter.AccessDeleteBySectionCodeIsUTS(SectionCode, IsUts);
                Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
                this.Cursor = Cursors.WaitCursor;
                DataTable dtResult = general.GetForceOnPole(dtForceOnPole, Convert.ToDouble(txtSaftyFactor.Text),Convert.ToDouble(txtSaftyFactorOburi.Text)).Copy();
                foreach (DataRow dr in dtResult.Rows)
                {
                    Atend.Base.Calculating.CPowerWithOutHalter powerWithOutHalter = new Atend.Base.Calculating.CPowerWithOutHalter();
                    powerWithOutHalter.IsUTS = IsUts;
                    powerWithOutHalter.PoleCount = Convert.ToByte(dr["DcCount"].ToString());
                    powerWithOutHalter.PoleGuid = new Guid(dr["DcPoleGuid"].ToString());
                    powerWithOutHalter.PolePower = Convert.ToDouble(dr["DcPower"].ToString());
                    powerWithOutHalter.SectionCode = SectionCode;
                    powerWithOutHalter.PoleNum = dr["DcPole"].ToString();
                    if (!powerWithOutHalter.AccessInsert())
                    {
                        ed.WriteMessage("PowerWithOutHalter.AccessInser Failed\n");
                    }
                    else
                    {
                        ed.WriteMessage("PoWerWithoutHalter. Access Insert\n");
                    }
                }
                this.Cursor = Cursors.Default;
                frmHalterResult halterResult = new frmHalterResult(dtResult, false,IsUts);
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(halterResult);
                this.Close();
            }
        }

        private void rdbHalter_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmChoiceForce_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }

    }
}