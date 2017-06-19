using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Atend2Electrical;
using ComplexMath;



namespace Atend.Calculating
{
    public partial class frmShortCircuit : Form
    {
        bool CanDoCalculate = false;
        int consolobj = 0;
        bool ForceToClose = false;
        public frmShortCircuit()
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

        private void محاسبهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Hide();
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect Entity:");
            PromptEntityResult per = ed.GetEntity(peo);
            Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
            if ((atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)) || (atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)) || (atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
            {
                CanDoCalculate = true;
                consolobj = Convert.ToInt32(per.ObjectId.ToString().Substring(1, per.ObjectId.ToString().Length - 2));
                Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(atInfo.NodeCode));
                if (dPack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                {
                    Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPack.ProductCode);
                    txtNamedVoltage.Text = eClamp.VoltageLevel.ToString();
                    txtVoltTev.Text = eClamp.VoltageLevel.ToString();
                }
                else if (dPack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dPack.ProductCode);
                    txtNamedVoltage.Text = eConsol.VoltageLevel.ToString();
                    txtVoltTev.Text = eConsol.VoltageLevel.ToString();
                }

                else if (dPack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel))
                {
                    Atend.Base.Equipment.EHeaderCabel eHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(dPack.ProductCode);
                    txtNamedVoltage.Text = eHeaderCabel.Voltage.ToString();
                    txtVoltTev.Text = eHeaderCabel.Voltage.ToString();
                }

                txtR.Text = "0";
                txtX.Text = "0";



            }
            else
            {


                MessageBox.Show("لطفت جهت شروع محاسبات یک گره را انتخاب کنید");
                CanDoCalculate = false;

            }
            this.Show();
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        public bool Validation()
        {

            if (Atend.Control.Common.Demo)
            {
                //MessageBox.Show("", "");
                return false;
            }
            if (consolobj == 0)
            {
                MessageBox.Show("لطفا گره ورود توان را مشخص نمایید");
                btnSelect.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNamedVoltage.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی را مشخص نمایید", "خطا");
                txtNamedVoltage.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDuration.Text))
            {
                MessageBox.Show("لطفا حداکثر جریان در یک ثانیه را مشخص نمایید", "خطا");
                txtDuration.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtVoltTev.Text))
            {
                MessageBox.Show("لطفا ولتاژ تونن را مشخص نمایید", "خطا");
                txtVoltTev.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtR.Text))
            {
                MessageBox.Show("لطفا R را مشخص نمایید", "خطا");
                txtR.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtX.Text))
            {
                MessageBox.Show("لطفا Xرا مشخص نمایید", "خطا");
                txtX.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtNamedVoltage.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی رابا فرمت مناسب وارد نمایید", "خطا");
                txtNamedVoltage.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtDuration.Text))
            {
                MessageBox.Show("لطفا حداکثر جریان در یک ثانیه  رابا فرمت مناسب وارد نمایید", "خطا");
                txtDuration.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtVoltTev.Text))
            {
                MessageBox.Show("لطفا ولتاژ تونن رابا فرمت مناسب وارد نمایید", "خطا");
                txtVoltTev.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtR.Text))
            {
                MessageBox.Show("لطفا R رابا فرمت مناسب وارد نمایید", "خطا");
                txtR.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtX.Text))
            {
                MessageBox.Show("لطفا X رابا فرمت مناسب وارد نمایید", "خطا");
                txtX.Focus();
                return false;
            }
            return true;
        }

        private void Calculation()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (Validation())
            {
                LoadFlow2 l = new LoadFlow2(consolobj, Convert.ToDouble(txtNamedVoltage.Text), Convert.ToDouble(txtVoltTev.Text), new Complex(Convert.ToDouble(txtR.Text), Convert.ToDouble(txtX.Text)), Convert.ToDouble(txtDuration.Text));
                //ed.WriteMessage("DtBranch>count= " + ((System.Data.DataTable)l.dtBranches).Rows.Count+"\n");
                //ed.WriteMessage("dtNodes.Count= " + ((System.Data.DataTable)l.dtNodes).Rows.Count+"\n");
                //foreach (DataRow dr in ((System.Data.DataTable)l.dtBranches).Rows)
                //{
                //    //ed.WriteMessage("Branch= " + dr["Code"].ToString() +  " upNode= "+dr["upNodeID"].ToString()+" DnNode ="+dr["DnNodeID"].ToString()+"\n");
                //}
                //ed.WriteMessage("######################\n");
                //foreach (DataRow dr in ((System.Data.DataTable)l.dtNodes).Rows)
                //{
                //    ed.WriteMessage("Node= " + dr["ConsoleGuid"].ToString() +"\n");

                //}
                this.Cursor = Cursors.WaitCursor;
                l.Calculate(0, 0);
                ed.WriteMessage("Go To Write Branched\n");
                this.Cursor = Cursors.Default;
                //this.Hide();
                frmShortCircuitResult result = new frmShortCircuitResult(((System.Data.DataTable)l.dtNodes), ((System.Data.DataTable)l.dtBranches), Convert.ToDouble(txtDuration.Text));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(result);
                //this.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShortCircuit_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }
    }
}