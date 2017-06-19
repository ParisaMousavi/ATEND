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
    public partial class frmTransCapacity : Form
    {
        public DataTable dtPhaseCount = new DataTable();
        DataColumn dcName = new DataColumn("Name");
        DataColumn dcCode = new DataColumn("Code");
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        bool ForceToClose = false;
        
        public frmTransCapacity()
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
            dtPhaseCount.Columns.Add(dcName);
            dtPhaseCount.Columns.Add(dcCode);

            DataRow dr1 = dtPhaseCount.NewRow();
            dr1["Name"] = "تک فاز";
            dr1["Code"] = "1";

            DataRow dr2 = dtPhaseCount.NewRow();
            dr2["Name"] = "سه فاز";
            dr2["Code"] = "3";

            dtPhaseCount.Rows.Add(dr1);
            dtPhaseCount.Rows.Add(dr2);
           
        }

        public void BindDataToCboBranch()
        {
            cboBranch.DisplayMember = "Name";
            cboBranch.ValueMember = "Code";
            DataTable dt = Atend.Base.Calculating.CDloadFactor.AccessSelectAll();
            ed.WriteMessage("dt.rows.count={0}\n", dt.Rows.Count);
            cboBranch.DataSource = Atend.Base.Calculating.CDloadFactor.AccessSelectAll();

        }

        //public void BindDataTocboCurrent(DataGridView gv)
        //{
        //    DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gv.Columns[2];
        //    c.DataSource = Atend.Base.Calculating.CDloadFactor.AccessSelectAll();
        //    c.DisplayMember = "Name";
        //    c.ValueMember = "Code";
        //}

        public void BindDataToPhaseCount(DataGridView gv)
        {
            DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gv.Columns[2];
            c.DataSource = dtPhaseCount;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (ValidationForInsert())
            {
                if (rdbCurrent.Checked)
                {
                    gvCurrent.Rows.Add();
                    gvCurrent.Rows[gvCurrent.Rows.Count - 1].Cells[0].Value = txtI.Text;
                    gvCurrent.Rows[gvCurrent.Rows.Count - 1].Cells[1].Value = txtPFC.Text;
                    gvCurrent.Rows[gvCurrent.Rows.Count - 1].Cells[2].Value = 1;
                    gvCurrent.Rows[gvCurrent.Rows.Count - 1].Cells[3].Value = 100;
                }

                if (rdbPower.Checked)
                {
                    gvPower.Rows.Add();
                    gvPower.Rows[gvPower.Rows.Count - 1].Cells[0].Value = txtP.Text;
                    gvPower.Rows[gvPower.Rows.Count - 1].Cells[1].Value = txtPFP.Text;
                    gvPower.Rows[gvPower.Rows.Count - 1].Cells[2].Value = 1;
                    gvPower.Rows[gvPower.Rows.Count - 1].Cells[3].Value = 100;

                }
                if (rdbBranch.Checked)
                {
                    gvBranch.Rows.Add();
                    gvBranch.Rows[gvBranch.Rows.Count - 1].Cells[0].Value = cboBranch.Text;
                    gvBranch.Rows[gvBranch.Rows.Count - 1].Cells[1].Value = cboBranch.SelectedValue;
                    gvBranch.Rows[gvBranch.Rows.Count - 1].Cells[2].Value = 1;
                    //gvBranch.Rows[gvBranch.Rows.Count - 1].Cells[3].Value = 1;

                }
            }
        }

        private void محاسبهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculation();
        }

        private void frmTransCapacity_Load(object sender, EventArgs e)
        {

            if (ForceToClose)
                this.Close();

            BindDataToCboBranch();
            //BindDataToPhaseCount(gvPower);
            //BindDataToPhaseCount(gvCurrent);


            if (cboBranch.Items.Count > 0)
                cboBranch.SelectedIndex = 0;
        }

        private void Calculation()
        {
            if (Validation())
            {

                double Pt = 0;
                double Pt3 = 0;
                double s = 0, St = 0;
                Atend.Base.Calculating.CTransBranch.AccessDelete();
                Atend.Base.Calculating.CTransCurrent.AccessDelete();
                //Atend.Base.Calculating.CTransformer.AccessDelete();
                Atend.Base.Calculating.CTransPower.AccessDelete();

                Atend.Base.Calculating.CTransBranch transBranch = new Atend.Base.Calculating.CTransBranch();
                Atend.Base.Calculating.CTransCurrent transCurrent = new Atend.Base.Calculating.CTransCurrent();
                Atend.Base.Calculating.CTransPower transPower = new Atend.Base.Calculating.CTransPower();
                //Atend.Base.Calculating.CTransformer transformer = new Atend.Base.Calculating.CTransformer();


                for (int i = 0; i < gvCurrent.Rows.Count; i++)
                {
                    double cf = Convert.ToDouble(gvCurrent.Rows[i].Cells[3].Value.ToString());
                    double Count = Convert.ToDouble(gvCurrent.Rows[i].Cells[2].Value.ToString());
                    double I = Convert.ToDouble(gvCurrent.Rows[i].Cells[0].Value.ToString());
                    double PF = Convert.ToDouble(gvCurrent.Rows[i].Cells[1].Value.ToString());

                    transCurrent.CF = cf;
                    transCurrent.BranchCount = Convert.ToInt32(Count.ToString());
                    transCurrent.I = I;
                    transCurrent.PF = PF;
                    if (!transCurrent.AccessInsert())
                    {
                        ed.WriteMessage("Failed TransCurrent Insert\n");
                    }

                    Pt = Math.Sqrt(3) * 400 * Count * I * (cf / 100) * (PF / 100);
                    s += (Pt / 1000) / (PF / 100);
                }

                for (int i = 0; i < gvPower.Rows.Count; i++)
                {
                    Pt = 0;
                    double cf = Convert.ToDouble(gvPower.Rows[i].Cells[3].Value.ToString());
                    double Count = Convert.ToDouble(gvPower.Rows[i].Cells[2].Value.ToString());
                    double P = Convert.ToDouble(gvPower.Rows[i].Cells[0].Value.ToString());
                    double PF = Convert.ToDouble(gvPower.Rows[i].Cells[1].Value.ToString());
                    transPower.BranchCount = Convert.ToInt32(Count.ToString());
                    transPower.CF = cf;
                    transPower.P = P;
                    transPower.PF = PF;
                    if (!transPower.AccessInsert())
                    {
                        ed.WriteMessage("TransPower Insert Failed\n");
                    }

                    Pt = P * (cf / 100);
                    s += Pt / (PF / 100);

                }
                Pt = 0;
                for (int i = 0; i < gvBranch.Rows.Count; i++)
                {
                    Atend.Base.Calculating.CDloadFactor dLoadFactor = Atend.Base.Calculating.CDloadFactor.AccessSelectByCode(Convert.ToInt32(gvBranch.Rows[i].Cells[1].Value.ToString()));

                    double cf = dLoadFactor.FactorConcurency;
                    double PF = dLoadFactor.FactorPower;
                    double I = dLoadFactor.Amper;
                    int CountPhase = dLoadFactor.PhaseCount;
                    double Count = Convert.ToDouble(gvBranch.Rows[i].Cells[2].Value.ToString());

                    transBranch.BranchCount = Convert.ToInt32(Count.ToString());
                    transBranch.BranchName = gvBranch.Rows[i].Cells[0].Value.ToString();
                    transBranch.CF = cf;
                    transBranch.I = I;
                    transBranch.PF = PF;
                    transBranch.PhaseCount = CountPhase;

                    if (!transBranch.AccessInsert())
                    {
                        ed.WriteMessage("TransBranch.Access Inser Failed\n");
                    }
                    if (CountPhase == 1)//تک فاز
                    {
                        Pt = Math.Sqrt(3) * 400 * (Count * I / 3) * (PF / 100) * (cf / 100);
                        s += (Pt / 1000) / (PF / 100);
                    }
                    else if (CountPhase == 3)//سه فاز
                    {
                        Pt = Math.Sqrt(3) * 400 * (Count * I) * (PF / 100) * (cf / 100);
                        s += (Pt / 1000) / (PF / 100);
                    }

                }
                double Load = Convert.ToDouble(txtLoad.Text) / 100;
                St = Math.Round(s / Load, 3);

                //transformer.Height = Convert.ToDouble(txtheight.Text);
                //transformer.Load = Convert.ToDouble(txtLoad.Text) / 100;
                //transformer.Result = St;
                //if (!transformer.AccessInsert())
                //{
                //    ed.WriteMessage("Transformer Access Insert Failed\n");
                //}
                Atend.Calculating.frmTransCapacityResult transResult = new frmTransCapacityResult(St, Convert.ToDouble(txtheight.Text), Convert.ToDouble(txtLoad.Text));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(transResult);
            }
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

            if (string.IsNullOrEmpty(txtheight.Text))
            {
                MessageBox.Show("لطفا ارتفاع از سطح دریا را مشخص نمایید", "خطا");
                txtheight.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtheight.Text))
            {
                MessageBox.Show("لطفا ارتفاع از سطح دریا را با فرمت  مناسب وارد نمایید", "خطا");
                txtheight.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLoad.Text))
            {
                MessageBox.Show("لطفا درصد بار گذاری را مشخص نمایید", "خطا");
                txtLoad.Focus();
                return false;
            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtLoad.Text))
            {
                MessageBox.Show("لطفا درصد بار گذاری را با فرمت  مناسب وارد نمایید", "خطا");
                txtLoad.Focus();
                return false;
            }
            #region Current
            for (int i = 0; i < gvCurrent.Rows.Count; i++)
            {
                if (gvCurrent.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("لطفا مقدار جریان را وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (gvCurrent.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("لطفا مقدار PF را وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (gvCurrent.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("لطفا تعداد انشعاب را وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (gvCurrent.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("لطفا ضریب همزمانی را وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(gvCurrent.Rows[i].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار جریان را با فرمت مناسب وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvCurrent.Rows[i].Cells[1].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار PF را با فرمت مناسب وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvCurrent.Rows[i].Cells[2].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار تعداد انشعاب را با فرمت مناسب وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvCurrent.Rows[i].Cells[3].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار ضریب همزمانی را با فرمت مناسب وارد نمایید", "خطا");
                    gvCurrent.Focus();
                    return false;
                }
            }
            #endregion
            #region Power
            for (int i = 0; i < gvPower.Rows.Count; i++)
            {
                if (gvPower.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("لطفا مقدار P را وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (gvPower.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("لطفا مقدار PF را وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (gvPower.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("لطفا تعداد انشعاب را وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (gvPower.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("لطفا ضریب همزمانی را وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(gvPower.Rows[i].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار P را با فرمت مناسب وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvPower.Rows[i].Cells[1].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار PF را با فرمت مناسب وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvPower.Rows[i].Cells[2].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار تعداد انشعاب را با فرمت مناسب وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(gvPower.Rows[i].Cells[3].Value.ToString()))
                {
                    MessageBox.Show("لطفا مقدار ضریب همزمانی را با فرمت مناسب وارد نمایید", "خطا");
                    gvPower.Focus();
                    return false;
                }
            }
            #endregion
            #region Branch
            for (int i = 0; i < gvBranch.Rows.Count; i++)
            {
                if (gvBranch.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("لطفا تعداد انشعاب  را وارد نمایید", "خطا");
                    gvBranch.Focus();
                    return false;
                }


                if (!Atend.Control.NumericValidation.DoubleConverter(gvBranch.Rows[i].Cells[2].Value.ToString()))
                {
                    MessageBox.Show("لطفا تعداد انشعاب  را با فرمت مناسب وارد نمایید", "خطا");
                    gvBranch.Focus();
                    return false;
                }

            }
            #endregion
            return true;

        }

        public bool ValidationForInsert()
        {
            if (rdbCurrent.Checked)
            {
                if (string.IsNullOrEmpty(txtI.Text))
                {
                    MessageBox.Show("لطفا مقدار I را وارد نمایید", "خطا");
                    txtI.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtPFC.Text))
                {
                    MessageBox.Show("لطفا مقدار PF را وارد نمایید", "خطا");
                    txtPFC.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(txtI.Text))
                {
                    MessageBox.Show("لطفا مقدار I را با فرمت مناسب وارد نمایید", "خطا");
                    txtI.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(txtPFC.Text))
                {
                    MessageBox.Show("لطفا مقدار PF را با فرمت مناسب وارد نمایید", "خطا");
                    txtPFC.Focus();
                    return false;
                }
            }

            if (rdbPower.Checked)
            {
                if (string.IsNullOrEmpty(txtP.Text))
                {
                    MessageBox.Show("لطفا مقدار P را وارد نمایید", "خطا");
                    txtP.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtPFP.Text))
                {
                    MessageBox.Show("لطفا مقدار PF را وارد نمایید", "خطا");
                    txtPFP.Focus();
                    return false;
                }

                if (!Atend.Control.NumericValidation.DoubleConverter(txtP.Text))
                {
                    MessageBox.Show("لطفا مقدار P را با فرمت مناسب وارد نمایید", "خطا");
                    txtP.Focus();
                    return false;
                }
                if (!Atend.Control.NumericValidation.DoubleConverter(txtPFP.Text))
                {
                    MessageBox.Show("لطفا مقدار PF را با فرمت مناسب وارد نمایید", "خطا");
                    txtPFP.Focus();
                    return false;
                }
            }

            if (rdbBranch.Checked)
            {
                if (string.IsNullOrEmpty(cboBranch.Text))
                {
                    MessageBox.Show("لطفا مقدار انشعابی  را وارد نمایید", "خطا");
                    cboBranch.Focus();
                    return false;
                }

            }


            return true;
        }

        private void btnDeletePower_Click(object sender, EventArgs e)
        {
            gvPower.Rows.RemoveAt(gvPower.CurrentRow.Index);
        }

        private void btnDeleteBranch_Click(object sender, EventArgs e)
        {
            gvBranch.Rows.RemoveAt(gvBranch.CurrentRow.Index);
        }

        private void btnDeleteCurrent_Click(object sender, EventArgs e)
        {
            gvCurrent.Rows.RemoveAt(gvCurrent.CurrentRow.Index);
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