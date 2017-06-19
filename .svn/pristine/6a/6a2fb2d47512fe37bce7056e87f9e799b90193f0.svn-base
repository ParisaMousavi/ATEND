using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using ComplexMath;

namespace Atend.Calculating
{
    public partial class frmCrossSectionResult : Form
    {
        DataTable _dtBranchResult = new DataTable();
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        int RowIndex;
        int _TypeCode;
        bool ForceToClose = false;
        Point pLocation = new Point();
        public frmCrossSectionResult(DataTable dtBranch, DataTable dtNode, int TypeCode)
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
            //_dtBranchResult = dtBranch;
            _TypeCode = TypeCode;

            _dtBranchResult.Columns.Add("NameExist");
            _dtBranchResult.Columns.Add("NameComment");
            _dtBranchResult.Columns.Add("NameCommentTip");
            _dtBranchResult.Columns.Add("CommentTipXCode");
            _dtBranchResult.Columns.Add("Volt");
            _dtBranchResult.Columns.Add("LossPower");
            _dtBranchResult.Columns.Add("CurrentAbs");
            _dtBranchResult.Columns.Add("FromComment");
            _dtBranchResult.Columns.Add("ToComment");
            _dtBranchResult.Columns.Add("PowerLoss");
            _dtBranchResult.Columns.Add("CrossSectionArea");
            _dtBranchResult.Columns.Add("Length");
            _dtBranchResult.Columns.Add("Select");
            _dtBranchResult.Columns.Add("Code");
            _dtBranchResult.Columns.Add("ProductType");


            double totalloss = 0;
            Atend.Base.Calculating.CCrossSection.AccessDelete();

            foreach (DataRow d in dtBranch.Rows)
            {
                totalloss += Convert.ToDouble(d["PowerLoss"].ToString());
            }
            foreach (DataRow dr in dtBranch.Rows)
            {
                Atend.Base.Calculating.CCrossSection crossSection = new Atend.Base.Calculating.CCrossSection();

                if (new Guid(dr["Code"].ToString()) != Guid.Empty)
                {
                    DataRow drNew = _dtBranchResult.NewRow();

                    if (Convert.ToInt32(dr["CondProductType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        Atend.Base.Equipment.ESelfKeeper self = new Atend.Base.Equipment.ESelfKeeper();
                        Atend.Base.Equipment.ESelfKeeperTip SelfTip = new Atend.Base.Equipment.ESelfKeeperTip();
                        if ((new Guid(dr["Code"].ToString()) != Guid.Empty))
                        {
                            SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Convert.ToInt32(dr["CondCode"].ToString()));
                            self = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfTip.PhaseProductCode);
                            drNew["NameExist"] = self.Name;
                            crossSection.ExistCond = self.Name;

                            Atend.Base.Equipment.ESelfKeeper Self1 = Atend.Base.Equipment.ESelfKeeper.SelectByXCode(new Guid(dr["CondCode1"].ToString()));

                            drNew["NameComment"] = Self1.Name;
                            crossSection.CommentCond = Self1.Name;

                            DataTable dtCommentTip = Atend.Base.Equipment.ESelfKeeperTip.SearchConductorSelfKeeperTipConductorTypeX(Convert.ToDouble(dr["CrossSectionArea"].ToString()), TypeCode);
                            //ed.WriteMessage("commentTipName={0}\n", dtCommentTip.Rows[0]["Name"].ToString());
                            if (dtCommentTip.Rows.Count > 0)
                            {

                                drNew["NameCommentTip"] = dtCommentTip.Rows[0]["Name"].ToString();
                                drNew["CommentTipXCode"] = dtCommentTip.Rows[0]["XCode"].ToString();
                            }
                            else
                            {
                                drNew["NameCommentTip"] = "";
                            }


                        }
                    }
                    else if ((Convert.ToInt32(dr["CondProductType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)) || (Convert.ToInt32(dr["CondProductType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Jumper)))
                    {
                        Atend.Base.Equipment.EConductor cond = new Atend.Base.Equipment.EConductor();
                        Atend.Base.Equipment.EConductorTip condTip = new Atend.Base.Equipment.EConductorTip();
                        if ((new Guid(dr["Code"].ToString()) != Guid.Empty))
                        {
                            ed.WriteMessage("ProductType={0},CondCode={1}\n", dr["condProductType"].ToString(), dr["CondCode"].ToString());
                            condTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Convert.ToInt32(dr["CondCode"].ToString()));
                            ed.WriteMessage("condTip.PhaseProductCode={0} \n", condTip.PhaseProductCode);

                            cond = Atend.Base.Equipment.EConductor.AccessSelectByCode(condTip.PhaseProductCode);
                            drNew["NameExist"] = cond.Name;
                            crossSection.ExistCond = cond.Name;
                            ed.WriteMessage("****CondCode1={0}\n", dr["CondCode1"].ToString());
                            Atend.Base.Equipment.EConductor cond1 = Atend.Base.Equipment.EConductor.SelectByXCode(new Guid(dr["CondCode1"].ToString()));

                            drNew["NameComment"] = cond1.Name;
                            crossSection.CommentCond = cond1.Name;

                            ed.WriteMessage("TypeCode={0}\n", TypeCode.ToString());

                            DataTable dtCommentTip = Atend.Base.Equipment.EConductorTip.SearchConductorConductorTipConductorTypeX(Convert.ToDouble(dr["CrossSectionArea"].ToString()), TypeCode);
                            ed.WriteMessage("commentTipName={0}\n", dtCommentTip.Rows[0]["Name"].ToString());
                            if (dtCommentTip.Rows.Count > 0)
                            {

                                drNew["NameCommentTip"] = dtCommentTip.Rows[0]["Name"].ToString();
                                drNew["CommentTipXCode"] = dtCommentTip.Rows[0]["XCode"].ToString();
                            }
                            else
                            {
                                drNew["NameCommentTip"] = "";
                            }

                        }
                    }


                    //از
                    DataRow[] drnodeFrom = dtNode.Select(string.Format("ConsoleObjID={0}", dr["UpNodeId"].ToString()));
                    if (drnodeFrom.Length > 0)
                    {
                        drNew["FromComment"] = FindNodeComment(new Guid(drnodeFrom[0]["ConsoleGuid"].ToString()));
                    }


                    //به
                    DataRow[] drnodeTo = dtNode.Select(string.Format("ConsoleObjID={0}", dr["DnNodeId"].ToString()));
                    if (drnodeTo.Length > 0)
                    {
                        drNew["Volt"] = Math.Round(((Complex)(drnodeTo[0]["Voltage"])).abs, 2).ToString();
                        drNew["ToComment"] = FindNodeComment(new Guid(drnodeTo[0]["ConsoleGuid"].ToString()));
                    }

                    //////{
                    //////    dr["LossPower"] = totalloss / ((Complex)dr["TotalLoad"]).real;

                    //////}
                    
                    drNew["PowerLoss"] = Convert.ToDouble(dr["PowerLoss"].ToString()) / 1000;
                    drNew["CurrentAbs"] = Math.Round(((Complex)(dr["Current"])).abs, 1);
                    drNew["CrossSectionArea"] = Convert.ToDouble(dr["CrossSectionArea"].ToString());
                    drNew["Length"] = Convert.ToDouble(dr["Length"].ToString());
                    drNew["Select"] = true;
                    drNew["Code"] = dr["Code"].ToString();
                    drNew["ProductType"] = Convert.ToInt32(dr["CondProductType"].ToString());


                    _dtBranchResult.Rows.Add(drNew);

                    crossSection.Current = Math.Round(((Complex)(dr["Current"])).abs, 1);
                    crossSection.From = drNew["FromComment"].ToString();
                    crossSection.To = drNew["ToComment"].ToString();
                    crossSection.CrossSection = Convert.ToDouble(drNew["CrossSectionArea"].ToString());
                    crossSection.Lenght = Convert.ToDouble(drNew["Length"].ToString());
                    crossSection.Volt = Math.Round(Convert.ToDouble(drNew["Volt"].ToString()), 1);
                    crossSection.PowerLoss = Convert.ToDouble(drNew["PowerLoss"].ToString());

                    if (!crossSection.AccessInsert())
                    {
                        ed.WriteMessage("CrossSection Insert Failed\n");
                    }
                }


                //Atend.Base.Equipment.EConductor cond1 = Atend.Base.Equipment.EConductor.SelectByXCode(new Guid(dr["CondCode1"].ToString()));

                //dr["NameComment"] = cond1.Name;

            }
            ed.WriteMessage("1\n");
            //DataRow[] drs = _dtBranchResult.Select(" UpNodeId= 0 ");
            //drs[0].Delete();

            gvConductor.AutoGenerateColumns = false;
            gvConductor.DataSource = _dtBranchResult;


            //حذف ستونی که دارای کد 0 می باشد
            //int countOfRows = gvConductor.Rows.Count;
            //for (int i = countOfRows; i >0 ; i--)
            //{
            //    ed.WriteMessage("i={0}\n",i.ToString());
            //    if (gvConductor.Rows[i].Cells["UpNodeId"].Value.ToString() == "0")
            //    {
            //        ed.WriteMessage("I AM In The IF\n");
            //        gvConductor.Rows.RemoveAt(i);
            //    }
            //}
            //gvConductor.Refresh();

            //////////تغییر رنگ بیشترین درصد افت توان در جدول
            ////////ed.WriteMessage("(((");
            ////////double max = Convert.ToDouble(gvConductor.Rows[0].Cells["Column7"].Value.ToString());
            ////////ed.WriteMessage("MAx={0}\n",max);
            ////////int j=0;
            ////////for (int i = 1; i < gvConductor.Rows.Count; i++)
            ////////{
            ////////    if (Convert.ToDouble(gvConductor.Rows[i].Cells["Column7"].Value.ToString()) > max)
            ////////    {
            ////////        max = Convert.ToDouble(gvConductor.Rows[i].Cells["Column7"].Value.ToString());
            ////////        j=i;
            ////////    }
            ////////}
            ////////ed.WriteMessage("MAx={0},J={1}\n",max,j);
            ////////gvConductor.Rows[j].DefaultCellStyle.BackColor=Color.Red;


            //////////یافتن تلفات کل شبکه
            ////////double  sum = 0;
            ////////for (int i = 0; i < gvConductor.Rows.Count; i++)
            ////////{
            ////////    sum += Convert.ToDouble(gvConductor.Rows[i].Cells["Column7"].Value.ToString());
            ////////}
            ////////lblSum.Text = sum.ToString();
            gvConductor.MouseMove += new MouseEventHandler(gvConductor_MouseMove);
        }

        int CurrentX, CurrentY;

        void gvConductor_MouseMove(object sender, MouseEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            CurrentX = e.X;
            CurrentY = e.Y;
        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();

        }

        private void خروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransferToEXCEL()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);


            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string Name = "گزارش محاسبات الکتریکی " + date + Atend.Control.Common.DesignName + ".xls";
            //string NameBranch = "گزارش محاسبات پخش بار شاخه  ها " + date + Atend.Control.Common.DesignName;

            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Report.xlsm";


            Atend.Global.Utility.UReport.CreateExcelReportFinalElectrical(Name);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TransferToEXCEL();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string FindNodeComment(Guid nodeGuid)
        {
            Atend.Base.Design.DPackage package = Atend.Base.Design.DPackage.AccessSelectByCode(nodeGuid);
            return package.Number;
        }

        private void gvConductor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                //gvConductor.Rows[0].Cells[0].PositionEditingPanel(
                //MessageBox.Show(string.Format("x:{0} y:{1}", CurrentX, CurrentY));
                //pLocation = new Point(CurrentX, CurrentY);
                RowIndex = e.RowIndex;
                Atend.Calculating.frmCrossSectionComment CrComment = new frmCrossSectionComment(this, _TypeCode, Convert.ToDouble(gvConductor.Rows[e.RowIndex].Cells["CrossSectionArea"].Value.ToString()), Convert.ToInt32(gvConductor.Rows[e.RowIndex].Cells["ProductType"].Value.ToString()));
                //CrComment.Location = pLocation;
                //CrComment.Top = CurrentY + gvConductor.Top;
                CrComment._Top = CurrentY + gvConductor.Top + this.Top + groupBox1.Top + 25;
                //MessageBox.Show(CrComment.Top.ToString());
                CrComment.Left = CurrentX + this.Left;
                CrComment.ShowDialog();
                //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(CrComment);
            }
        }

        public void BindDataToGridCell(Guid XCode)
        {
            ed.WriteMessage("Xcode={0}\n", XCode.ToString());
            Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(XCode);
            ed.WriteMessage("**Name={0}\n", CondTip.Name);
            gvConductor.Rows[RowIndex].Cells["NameCommentTip"].Value = CondTip.Name;
            gvConductor.Rows[RowIndex].Cells["CommentTipXCode"].Value = CondTip.XCode;
        }

        private void frmCrossSectionResult_MouseClick(object sender, MouseEventArgs e)
        {
            //pLocation = e.Location;
            //MessageBox.Show(pLocation.X.ToString()+"  "+pLocation.Y.ToString());
        }

        private void gvConductor_MouseClick(object sender, MouseEventArgs e)
        {
            //pLocation = e.Location;
            //MessageBox.Show(pLocation.X.ToString() + "  " + pLocation.Y.ToString());
        }

        private void frmCrossSectionResult_MouseMove(object sender, MouseEventArgs e)
        {
            //pLocation = e.Location;
            ////MessageBox.Show(pLocation.X.ToString() + "  " + pLocation.Y.ToString());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelect.Checked)
            {
                foreach (DataRow dr in _dtBranchResult.Rows)
                {
                    dr["Select"] = true;

                }
            }
            else
            {
                foreach (DataRow dr in _dtBranchResult.Rows)
                {
                    dr["Select"] = false;

                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Atend.Global.Calculation.Electrical.CElectricalCrossSectionChane change = new Atend.Global.Calculation.Electrical.CElectricalCrossSectionChane();

            for (int i = 0; i < gvConductor.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvConductor.Rows[i].Cells["chkSelect"];
                if (chk.Value.ToString() == "True")
                {
                    if (gvConductor.Rows[i].Cells["NameCommentTip"].Value.ToString()!="")
                    {
                        Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(gvConductor.Rows[i].Cells["Code"].Value.ToString()));
                        change.DBranch.Add(branch);

                        if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                        {
                            Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.SelectByXCode(new Guid(gvConductor.Rows[i].Cells["CommentTipXCode"].Value.ToString()));
                            change.CondTip.Add(CondTip);
                        }

                        if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                        {
                            Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.SelectByXCode(new Guid(gvConductor.Rows[i].Cells["CommentTipXCode"].Value.ToString()));
                            change.SelfTip.Add(SelfTip);
                        }

                        //if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
                        //{
                        //    Atend.Base.Equipment.EGroundCabelTip  GroundTip = Atend.Base.Equipment.EGroundCabelTip.SelectByXCode(new Guid(gvConductor.Rows[i].Cells["CommentTipXCode"].Value.ToString()));
                        //    change.GroundTip.Add(GroundTip);
                        //}
                    }
                }
            }

            if (change.ChangeBranchInfo())
            {
                MessageBox.Show("اعمال تغییرات با موفقیت انجام شد");
            }
            else
            {
                MessageBox.Show("اعمال تغییرات با موفقیت انجام نشد");

            }

        }

        private void frmCrossSectionResult_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }

    }
}