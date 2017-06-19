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
    public partial class frmHalterResult : Form
    {
        DataTable dtForceOnPole = new DataTable();
        bool IsPowerWithHalter;
        bool isUTS;
        bool ForceToClose = false;
        int RowIndex;

        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public frmHalterResult(DataTable _dtForceOnPole, bool _IsPowerWithHalter, bool _IsUTS)
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



            dtForceOnPole.Columns.Add("HalterCount");
            dtForceOnPole.Columns.Add("Name");
            dtForceOnPole.Columns.Add("HalterPower");
            dtForceOnPole.Columns.Add("DcPole");
            dtForceOnPole.Columns.Add("Count");
            dtForceOnPole.Columns.Add("DcPoleGuid");
            dtForceOnPole.Columns.Add("Power");
            dtForceOnPole.Columns.Add("HalterXCode");

            dtForceOnPole.Columns.Add("PoleCode");
            dtForceOnPole.Columns.Add("CurrentPole");
            dtForceOnPole.Columns.Add("CommentPole");
            dtForceOnPole.Columns.Add("Select");
            IsPowerWithHalter = _IsPowerWithHalter;


            if (IsPowerWithHalter)
            {
                ed.WriteMessage("CALCWITHHALTER\n");
                foreach (DataRow dr in _dtForceOnPole.Rows)
                {
                    DataRow drNew = dtForceOnPole.NewRow();
                    drNew["HalterCount"] = dr["HalterCount"].ToString();
                    drNew["Name"] = dr["Name"].ToString();
                    drNew["HalterPower"] = dr["HalterPower"].ToString();
                    drNew["DcPole"] = dr["DcPole"].ToString();
                    drNew["Count"] = dr["Count"].ToString();
                    drNew["DcPoleGuid"] = dr["DcPoleGuid"].ToString();
                    drNew["Power"] = dr["Power"].ToString();
                    drNew["Select"] = true;
                    drNew["HalterXCode"] = dr["HalterXCode"].ToString();
                    drNew["CurrentPole"] = FindPoleName(new Guid(dr["DcPoleGuid"].ToString()));
                    DataTable dtPower = Atend.Base.Equipment.EPole.SelectByPowerX(Convert.ToDouble(dr["Power"].ToString()));

                    if (dtPower.Rows.Count > 0)
                    {
                        drNew["CommentPole"] = dtPower.Rows[0]["Name"].ToString();
                        drNew["PoleCode"] = dtPower.Rows[0]["XCode"].ToString();
                    }
                    else
                    {
                        drNew["CommentPole"] = "";
                        drNew["PoleCode"] = 0;
                    }
                    dtForceOnPole.Rows.Add(drNew);

                }
            }
            else
            {
                foreach (DataRow dr in _dtForceOnPole.Rows)
                {

                    DataRow drNew = dtForceOnPole.NewRow();
                    drNew["DcPole"] = dr["DcPole"].ToString();
                    drNew["Count"] = dr["DcCount"].ToString();
                    drNew["DcPoleGuid"] = dr["DcPoleGuid"].ToString();
                    drNew["Power"] = dr["DcPower"].ToString();
                    drNew["Select"] = true;
                    drNew["CurrentPole"] = FindPoleName(new Guid(dr["DcPoleGuid"].ToString()));
                    DataTable dtPower = Atend.Base.Equipment.EPole.SelectByPowerX(Convert.ToDouble(dr["DcPower"].ToString()));

                    if (dtPower.Rows.Count > 0)
                    {
                        drNew["CommentPole"] = dtPower.Rows[0]["Name"].ToString();
                        drNew["PoleCode"] = dtPower.Rows[0]["XCode"].ToString();
                        ed.WriteMessage("PoleCode={0}\n", dtPower.Rows[0]["XCode"].ToString());
                    }
                    else
                    {
                        drNew["CommentPole"] = "";
                        drNew["PoleCode"] = 0;
                    }
                    dtForceOnPole.Rows.Add(drNew);
                }
            }




            //dtForceOnPole = _dtForceOnPole;
            IsPowerWithHalter = _IsPowerWithHalter;
            isUTS = _IsUTS;
            //Atend.Global.Calculation.Mechanical.CalcOptimalSagTension calc = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTension();
            //ed.WriteMessage("BeforSelectAll\n");
            //DataTable dtHalter = Atend.Base.Equipment.EHalter.SelectAllX();
            //ed.WriteMessage("BeforSelectByXCode\n");
            //Atend.Base.Equipment.EHalter halter = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(dtHalter.Rows[0]["XCode"].ToString()));
            //ed.WriteMessage("CalcResult={0}\n",IsPowerWithHalter);
            if (IsPowerWithHalter)
            {
                gvCommentPowerWithHalter.AutoGenerateColumns = false;
                gvCommentPowerWithHalter.DataSource = dtForceOnPole;
                gvCommentPowerWithHalter.Visible = true;
                gvCommentPower.Visible = false;

            }
            else
            {
                gvCommentPower.AutoGenerateColumns = false;
                gvCommentPower.DataSource = dtForceOnPole;
                gvCommentPower.Visible = true;
                gvCommentPowerWithHalter.Visible = false;
            }
            //DataTable dtResult=calc.CalcHalter(dtForceOnPole,halter);


            // dataGridView1.DataSource = dtResult;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void انتقالبهفایلExelToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransferToEXCEL()
        {
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            if (isUTS)
            {
                if (IsPowerWithHalter)
                {
                    string NameFlash = "پیشنهاد قدرت پایه با مهار به روش UTS" + date + ".xls";
                    //ed.WriteMessage("ROWS={0}\n", dtPole.Rows.Count);
                    Atend.Global.Utility.UReport.CreateExcelPowerWithHalter(NameFlash, true);
                }
                else
                {
                    string NameFlash = "پیشنهاد قدرت پایه بدون مهار به روش UTS" + date + ".xls";
                    //ed.WriteMessage("ROWS={0}\n", dtPole.Rows.Count);
                    Atend.Global.Utility.UReport.CreateExcelPowerWithOutHalter(NameFlash, true);
                }
            }
            else
            {
                if (IsPowerWithHalter)
                {
                    string NameFlash = "پیشنهاد قدرت پایه با مهار به روش MaxF" + date + ".xls";
                    //ed.WriteMessage("ROWS={0}\n", dtPole.Rows.Count);
                    Atend.Global.Utility.UReport.CreateExcelPowerWithHalter(NameFlash, false);
                }
                else
                {
                    string NameFlash = "پیشنهاد قدرت پایه بدون مهار به روش MaxF" + date + ".xls";
                    //ed.WriteMessage("ROWS={0}\n", dtPole.Rows.Count);
                    Atend.Global.Utility.UReport.CreateExcelPowerWithOutHalter(NameFlash, false);
                }
            }
        }

        private void انتقالبهفایلEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void خروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmHalterResult_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }


        private string FindPoleName(Guid PoleGuid)
        {
            Atend.Base.Design.DPackage package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(PoleGuid);
            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.AccessSelectByCode(package.ProductCode);
            return Pole.Name;
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelect.Checked)
            {
                foreach (DataRow dr in dtForceOnPole.Rows)
                {
                    dr["Select"] = true;

                }
            }
            else
            {
                foreach (DataRow dr in dtForceOnPole.Rows)
                {
                    dr["Select"] = false;

                }
            }
        }

        public void BindDataToGridCell(Guid XCode)
        {

            if (IsPowerWithHalter)
            {
                Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(XCode);
                gvCommentPowerWithHalter.Rows[RowIndex].Cells[5].Value = Pole.Name;
                gvCommentPowerWithHalter.Rows[RowIndex].Cells[2].Value = Pole.XCode;
            }
            else
            {
                Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(XCode);
                gvCommentPower.Rows[RowIndex].Cells["CommentPole"].Value = Pole.Name;
                gvCommentPower.Rows[RowIndex].Cells["Code"].Value = Pole.XCode;
            }
        }

        private void gvCommentPower_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {

                RowIndex = e.RowIndex;
                Atend.Calculating.frmHalterComment HalterComment = new frmHalterComment(this, Convert.ToDouble(gvCommentPower.Rows[RowIndex].Cells["Power"].Value.ToString()));
                RowIndex = e.RowIndex;
                HalterComment._Top = CurrentY + gvCommentPower.Top + this.Top + groupBox1.Top + 25;
                //MessageBox.Show(CrComment.Top.ToString());
                HalterComment.Left = CurrentX + this.Left;
                HalterComment.ShowDialog();
                //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(CrComment);
            }
        }
        int CurrentX, CurrentY;

        private void gvCommentPower_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentX = e.X;
            CurrentY = e.Y;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Atend.Global.Calculation.Mechanical.CMechanicalChange MecChange = new Atend.Global.Calculation.Mechanical.CMechanicalChange();
            if (IsPowerWithHalter)
            {
                for (int i = 0; i < gvCommentPowerWithHalter.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvCommentPowerWithHalter.Rows[i].Cells[11];
                    //ed.WriteMessage("");
                    if (chk.Value.ToString() == "True")
                    {
                        //ed.WriteMessage("CommentPole={0}\n",gvCommentPower.Rows[i].Cells[5].Value.ToString());
                        if (gvCommentPowerWithHalter.Rows[i].Cells[5].Value.ToString() != "")
                        {
                            ed.WriteMessage("Add dPack\n");
                            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(gvCommentPowerWithHalter.Rows[i].Cells[1].Value.ToString()));
                            MecChange.DPackage.Add(dPack);

                            Atend.Base.Design.DPoleInfo dPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(new Guid(gvCommentPowerWithHalter.Rows[i].Cells[1].Value.ToString()));
                            MecChange.PoleInfo.Add(dPoleInfo);
                            MecChange.Count.Add(Convert.ToInt32(gvCommentPowerWithHalter.Rows[i].Cells[7].Value.ToString()));

                            //ed.WriteMessage("###={0}\n", gvCommentPower.Rows[i].Cells[2].Value.ToString());
                            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvCommentPowerWithHalter.Rows[i].Cells[2].Value.ToString()));
                            MecChange.Pole.Add(Pole);



                            Atend.Base.Equipment.EHalter Halter = Atend.Base.Equipment.EHalter.SelectByXCode(new Guid(gvCommentPowerWithHalter.Rows[i].Cells[12].Value.ToString()));
                            MecChange.Halter.Add(Halter);
                            MecChange.HalterCount.Add(Convert.ToInt32(gvCommentPowerWithHalter.Rows[i].Cells[10].Value.ToString()));

                        }
                    }
                }

                if (!MecChange.ChangePoleInfoWithHalter())
                {
                    ed.WriteMessage("Error in ChangePoleInfoWithHalter");
                }
            }
            else
            {

                for (int i = 0; i < gvCommentPower.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvCommentPower.Rows[i].Cells[8];
                    //ed.WriteMessage("");
                    if (chk.Value.ToString() == "True")
                    {
                        if (gvCommentPower.Rows[i].Cells["CommentPole"].Value.ToString() != "")
                        {
                            ed.WriteMessage("Add dPack\n");
                            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(gvCommentPower.Rows[i].Cells["DcPoleGuid"].Value.ToString()));
                            MecChange.DPackage.Add(dPack);

                            Atend.Base.Design.DPoleInfo dPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(new Guid(gvCommentPower.Rows[i].Cells["DcPoleGuid"].Value.ToString()));
                            MecChange.PoleInfo.Add(dPoleInfo);
                            MecChange.Count.Add(Convert.ToInt32(gvCommentPower.Rows[i].Cells["Count"].Value.ToString()));

                            ed.WriteMessage("###={0}\n", gvCommentPower.Rows[i].Cells["Code"].Value.ToString());
                            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByXCode(new Guid(gvCommentPower.Rows[i].Cells["Code"].Value.ToString()));
                            MecChange.Pole.Add(Pole);
                        }
                    }
                }

                if (!MecChange.ChangePoleInfo())
                {
                    ed.WriteMessage("Error in ChangePoleInfo");
                }
            }
        }

        private void gvCommentPowerWithHalter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {

                RowIndex = e.RowIndex;
                Atend.Calculating.frmHalterComment HalterComment = new frmHalterComment(this, Convert.ToDouble(gvCommentPowerWithHalter.Rows[RowIndex].Cells["DcPower"].Value.ToString()));
                RowIndex = e.RowIndex;
                HalterComment._Top = CurrentY + gvCommentPowerWithHalter.Top + this.Top + groupBox1.Top + 25;
                //MessageBox.Show(CrComment.Top.ToString());
                HalterComment.Left = CurrentX + this.Left;
                HalterComment.ShowDialog();
                //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(CrComment);
            }
        }

        private void gvCommentPowerWithHalter_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentX = e.X;
            CurrentY = e.Y;
        }
    }
}