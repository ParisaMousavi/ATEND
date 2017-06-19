using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Atend2Electrical;
using ComplexMath;


namespace Atend.Calculating
{
    public partial class frmCrossSection : Form
    {
        bool CanDoCalculate = false;
        double R, X, TevVolt;
        Atend.Base.Equipment.EConductor cond;
        int Nodeobj = 0;
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;
        public frmCrossSection()
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Hide();

            PromptEntityOptions peo = new PromptEntityOptions("\nSelect Entity:");
            PromptEntityResult per = ed.GetEntity(peo);
            Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
            //ed.WriteMessage("PoleGuid={0} ,ConsolGuid={1}\n", atInfo.ParentCode, atInfo.NodeCode);
            DataTable dtCondAndSelf = Atend.Global.Acad.UAcad.FillBranchList();


            if ((atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)) || (atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)) || (atInfo.NodeType == Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
            {

                CanDoCalculate = true;
                Nodeobj = Convert.ToInt32(per.ObjectId.ToString().Substring(1, per.ObjectId.ToString().Length - 2));
                //ed.WriteMessage("NodeCode={0}\n",atInfo.NodeCode);
                Atend.Base.Design.DPackage dPAck = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(atInfo.NodeCode));
                //ed.WriteMessage("dConsol.ProductCode={0}\n",dconsol.ProductCode);
                if (dPAck.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dPAck.ProductCode);
                    //ed.WriteMessage("^^^^={0}\n",eConsol.Length);
                    txtVolt.Text = eConsol.VoltageLevel.ToString();
                    txtVoltTev.Text = eConsol.VoltageLevel.ToString();
                    if (eConsol.VoltageLevel == 400)
                    {
                        txtMaxDropVolt.Text = "5";
                        txtMaxDropPower.Text = "3";
                    }
                    else
                    {
                        txtMaxDropVolt.Text = "3";
                        txtMaxDropPower.Text = "3";
                    }
                   


                }
                else if (dPAck.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                {
                    //ed.WriteMessage("This Is Kalamp\n");
                    Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPAck.ProductCode);
                    txtVolt.Text = Clamp.VoltageLevel.ToString();
                    txtVoltTev.Text = Clamp.VoltageLevel.ToString();
                    if (Clamp.VoltageLevel == 400)
                    {
                        txtMaxDropVolt.Text = "5";
                        txtMaxDropPower.Text = "3";
                    }
                    else
                    {
                        txtMaxDropVolt.Text = "3";
                        txtMaxDropPower.Text = "3";
                    }
                  
                    
                }
                else if (dPAck.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel))
                {
                    //ed.WriteMessage("This Is Kalamp\n");
                    Atend.Base.Equipment.EHeaderCabel  HeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(dPAck.ProductCode);
                    txtVolt.Text = HeaderCabel.Voltage.ToString();
                    txtVoltTev.Text = HeaderCabel.Voltage.ToString();
                    if (HeaderCabel.Voltage== 400)
                    {
                        txtMaxDropVolt.Text = "5";
                        txtMaxDropPower.Text = "3";
                    }
                    else
                    {
                        txtMaxDropVolt.Text = "3";
                        txtMaxDropPower.Text = "3";
                    }


                }
                //ed.WriteMessage("dtCondAndSelf.Rows.Count={0}\n",dtCondAndSelf.Rows.Count);
                DataRow[] drCond = dtCondAndSelf.Select(string.Format("Type={0}", (int)Atend.Control.Enum.ProductType.Conductor));
                //System.Data.DataTable dtConductor = Atend.Global.Acad.UAcad.GetConsolConductors(new Guid(atInfo.ParentCode), new Guid(atInfo.NodeCode));
                if (drCond.Length != 0)
                {
                    Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drCond[0]["BranchGuid"].ToString()));// SelectByCode(new Guid(dtConductor.Rows[0]["BranchGuid"].ToString()));
                    //ed.WriteMessage("branch.productCode={0}\n",branch.ProductCode);

                    Atend.Base.Equipment.EConductorTip condTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode);
                    cond = Atend.Base.Equipment.EConductor.AccessSelectByCode(condTip.PhaseProductCode);
                    //ed.WriteMessage("Cond.TypeCode={0}\n",cond.TypeCode);
                    cboMaterial.SelectedIndex = cond.TypeCode;
                    //ed.WriteMessage("****\n");

                }

                DataRow[] drSelf = dtCondAndSelf.Select(string.Format("Type={0}", (int)Atend.Control.Enum.ProductType.SelfKeeper));
                if (drSelf.Length != 0)
                {
                    //ed.WriteMessage("Self\n");

                    Atend.Base.Design.DBranch branchSelf = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drSelf[0]["BranchGuid"].ToString()));
                    Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branchSelf.ProductCode);
                    Atend.Base.Equipment.ESelfKeeper Self = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfTip.PhaseProductCode);
                    CboMaterialCalamp.SelectedIndex = Self.MaterialConductorCode - 1;
                }
                txtX.Text = "0";
                txtR.Text = "0";
                txtLoadPercent.Text = "75";
               
                //ed.WriteMessage("FinishSElect\n");

            }
            else
            {
                MessageBox.Show("لطفت جهت شروع محاسبات یک گره را انتخاب کنید");
                CanDoCalculate = false;
            }
            this.Show();
        }

        private void محاسبهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculation();
        }
        public bool Validation()
        {
            if (Atend.Control.Common.Demo)
            {
                //MessageBox.Show("", "");
                return false;
            }

            if (Nodeobj == 0)
            {
                MessageBox.Show("لطفا گره ورود توان را مشخص نمایید");
                btnSelect.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtVolt.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی را مشخص نمایید", "خطا");
                txtVolt.Focus();
                return false;
            }



            if (string.IsNullOrEmpty(txtMaxDropVolt.Text))
            {
                MessageBox.Show("لطفا حداکثر افت ولتاژ را مشخص نمایید", "خطا");
                txtMaxDropVolt.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaxDropPower.Text))
            {
                MessageBox.Show("لطفا حداکثر افت توان را مشخص نمایید", "خطا");
                txtMaxDropPower.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtVolt.Text))
            {
                MessageBox.Show("لطفا ولتاژ نامی رابا فرمت مناسب وارد نمایید", "خطا");
                txtVolt.Focus();
                return false;
            }



            if (!Atend.Control.NumericValidation.DoubleConverter(txtMaxDropPower.Text))
            {
                MessageBox.Show("لطفا حداکثرافت توان رابا فرمت مناسب وارد نمایید", "خطا");
                txtMaxDropPower.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtMaxDropVolt.Text))
            {
                MessageBox.Show("لطفا حداکثر افت ولتاژ رابا فرمت مناسب وارد نمایید", "خطا");
                txtMaxDropVolt.Focus();
                return false;
            }
            //if (chkInfo.Checked)
            //{
            if (string.IsNullOrEmpty(txtVoltTev.Text))
            {
                MessageBox.Show("لطفا ولتاژ تونن را مشخص نمایید", "خطا");
                txtVoltTev.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtLoadPercent.Text))
            {
                MessageBox.Show("لطفا درصد بارگذاری را مشخص نمایید", "خطا");
                txtLoadPercent.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtLoadPercent.Text))
            {
                MessageBox.Show("لطفا درصد بار گذاری رابا فرمت مناسب وارد نمایید", "خطا");
                txtLoadPercent.Focus();
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
                MessageBox.Show("لطفا X را مشخص نمایید", "خطا");
                txtX.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtX.Text))
            {
                MessageBox.Show("لطفا X رابا فرمت مناسب وارد نمایید", "خطا");
                txtX.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtR.Text))
            {
                MessageBox.Show("لطفا R رابا فرمت مناسب وارد نمایید", "خطا");
                txtR.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtVoltTev.Text))
            {
                MessageBox.Show("لطفا ولتاژ تونن رابا فرمت مناسب وارد نمایید", "خطا");
                txtVoltTev.Focus();
                return false;
            }
            //}
            return true;
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCrossSection_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            cboMaterial.SelectedIndex = 0;
            CboMaterialCalamp.SelectedIndex = 0;
        }

        private void Calculation()
        {
            if (Validation())
            {
                //ed.WriteMessage("GO To Calc\n");
                System.Data.DataTable dtCondCrossSection = Atend.Base.Equipment.EConductor.SelectByType(cboMaterial.SelectedIndex);

                dtCondCrossSection.Columns.Add("ProductType");
                foreach (DataRow dr in dtCondCrossSection.Rows)
                {
                    dr["ProductType"] = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);
                }

                System.Data.DataTable dtSelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByType(CboMaterialCalamp.SelectedIndex + 1);
                dtSelfKeeper.Columns.Add("ProductType");
                foreach (DataRow dr in dtSelfKeeper.Rows)
                {
                    dr["ProductType"] = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);
                }
                //ed.WriteMessage("dtCondCrossSection.Rows={0}\n", dtCondCrossSection.Rows[0]["ProductType"].ToString());


                if (CanDoCalculate)
                {
                    //ed.WriteMessage("I Am In The IF\n");
                    //if (chkInfo.Checked)
                    //{
                    //    R = Convert.ToDouble(txtR.Text);
                    //    X = Convert.ToDouble(txtX.Text);
                    //    TevVolt = Convert.ToDouble(txtVoltTev.Text);
                    //}
                    //else
                    //{
                    //    R = 0;
                    //    X = 0;
                    //    TevVolt = 0;
                    //}
                    this.Cursor = Cursors.WaitCursor;

                    ElecCrossSection elec = new ElecCrossSection();
                    if (ElecCrossSection.CrossSectionErr.NoError == elec.FindBestCrossSection(dtCondCrossSection, dtSelfKeeper, Nodeobj, Convert.ToDouble(txtVolt.Text), Convert.ToDouble(txtVoltTev.Text), new Complex(Convert.ToDouble(txtR.Text), Convert.ToDouble(txtX.Text)), Convert.ToDouble(txtMaxDropVolt.Text), Convert.ToDouble(txtMaxDropPower.Text),Convert.ToDouble(txtLoadPercent.Text)))
                    {

                        frmCrossSectionResult CrossSectionResult = new frmCrossSectionResult((System.Data.DataTable)elec.dtBranches, (System.Data.DataTable)elec.dtNodes,cboMaterial.SelectedIndex);
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(CrossSectionResult);
                    }
                    else
                    {
                        MessageBox.Show("عملیات به دلیل نبودن سیم با سطح مقطع بالاتر متوقف شد","خطا");
                    }
                    this.Cursor = Cursors.Default;
                }
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

    }
}