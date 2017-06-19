using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.SqlClient;

namespace Atend.Calculating
{
    public partial class frmOptimalSagTensionVTSTest : Form
    {

        ArrayList SectionCode = new ArrayList();
        Atend.Global.Calculation.Mechanical.CalcOptimalSagTensionTest calcOptimalSagTension;
        ArrayList SelectedList = new ArrayList();
        System.Data.DataTable dtSectionNumber = new System.Data.DataTable();

        System.Data.DataTable dtSagTension = new System.Data.DataTable();
        System.Data.DataTable dtConductorDay = new System.Data.DataTable();
        System.Data.DataTable dtPole = new System.Data.DataTable();

        System.Data.DataTable dtRow = new System.Data.DataTable();

        Atend.Base.Calculating.CNetWorkCross NetCross = new Atend.Base.Calculating.CNetWorkCross();
        Dictionary<string, string> Dic = new Dictionary<string, string>();
        Dictionary<String, string> DicPoleForce = new Dictionary<string, string>();
        double se = 0;
        double SpanCount = 0;

        Atend.Global.Calculation.Section section;
        bool ForceToClose = false;
        public frmOptimalSagTensionVTSTest()
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
            dtRow.Columns.Add("ProjectName");
            dtRow.Columns.Add("NumSection");
            dtRow.Columns.Add("FirstPole");
            dtRow.Columns.Add("LastPole");
            dtRow.Columns.Add("UTS");
            dtRow.Columns.Add("SE");
            dtRow.Columns.Add("SpanCount");
            dtRow.Columns.Add("SpanLenght");
            dtRow.Columns.Add("CondName");


            Dic.Add("ConductorName", "نام سیم");
            Dic.Add("NormH", "شرایط معمول کشش");
            Dic.Add("NormF", "شرایط معمول فلش");
            Dic.Add("IceH", "شرایط یخ سنگین کشش");
            Dic.Add("IceF", "شرایط یخ سنگین فلش");
            Dic.Add("WindH", "شرایط باد زیاد کشش");
            Dic.Add("WindF", "شرایط باد زیاد فلش");
            Dic.Add("MaxTempH", "شرایط حداکثر دما کشش");
            Dic.Add("MaxTempF", "شرایط حداکثر دما فلش");
            Dic.Add("MinTempH", "شرایط حداقل دما کشش");
            Dic.Add("MinTempF", "شرایط حداقل دما فلش");
            Dic.Add("WindAndIceH", "شرایط باد و یخ کشش");
            Dic.Add("WindAndIceF", "شرایط باد و یخ فلش");



            DicPoleForce.Add("dcPole", "شماره پایه");
            DicPoleForce.Add("dcNorm", "شرایط معمولی");
            DicPoleForce.Add("dcIceHeavy", "یخ سنگین");
            DicPoleForce.Add("dcWindSpeed", "باد زیاد");
            DicPoleForce.Add("dcMaxTemp", "حداکثر دما");
            DicPoleForce.Add("dcMinTemp", "حداقل دما");
            DicPoleForce.Add("dcWindIce", "باد و یخ");

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Atend.Global.Calculation.Section sec = new Atend.Global.Calculation.Section();
            System.Data.DataTable w = Atend.Base.Design.DWeather.AccessSelectTest(1, 2);
            MessageBox.Show("w.Count=" + w.Rows.Count.ToString());
            //sec.DetermineSection();
            // Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            // System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            // int day = calender.GetDayOfMonth(DateTime.Today);
            // int Month = calender.GetMonth(DateTime.Today);
            // int Year = calender.GetYear(DateTime.Today);

            // string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();

            // string Name = "نتایج محاسبات مکانیکی" + date + Atend.Control.Common.DesignName + cboSelectSection.Text;

            // string PAthFile = Atend.Control.Common.fullPath + "\\ReportFile\\mohasebat m  .xlsx";



            // File.Copy(PAthFile, report.Value1 + Name+".xlsx", true);

            // ed.WriteMessage("PathFlash:={0},PAth={1}\n", PAthFile, report.Value1 + Name);
            // string Path = report.Value1 + Name;
            // Atend.Global.Utility.UReport.CreateReport(Path, dtSagTension,Dic,"Data1" ,1);
            // ed.WriteMessage("save First File\n");
            // //Atend.Global.Utility.UReport.CreateReport(NameForce, dtPole,1 ,1);
            //Atend.Global.Utility.UReport.CreateReport(Path, dtPole,DicPoleForce,"Data3",1);


        }

        private Guid Guid(object p)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void BindDataToNetComboBox()
        {
            //cboNetCross.DisplayMember = "Name";
            //cboNetCross.ValueMember = "Code";
            //cboNetCross.DataSource = Atend.Base.Calculating.CNetWorkCross.SelectAll();
        }

        private void frmOptimalSagTensionVTS_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();

            BindDataToNetComboBox();
            cboVolt.SelectedIndex = 0;
        }

        private void btnCalc_Click_1(object sender, EventArgs e)
        {

            //frmOptimalSagTensionResult resultSagTension = new frmOptimalSagTensionResult(calcOptimalSagTension);
            //resultSagTension.Show();
            //this.Close();

        }

        private void cboVolt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedList.Clear();
            SectionCode.Clear();
            int gvCount = gvNetCross.Rows.Count - 1;
            for (int i = gvCount; i >= 0; i--)
            {
                gvNetCross.Rows.RemoveAt(i);
            }
            //gvNetCross.Rows.Clear();
            gvNetCross.Refresh();
            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            #region Select
            this.Hide();

            // ~~~~~~~~~~~~ Start Select Entities 


            int PoleCounter = 0;

            int ConductorCounter = 0;

            PromptSelectionOptions pso = new PromptSelectionOptions();

            pso.MessageForAdding = "Select area where you want: \n";

            PromptSelectionResult psr = ed.GetSelection(pso);

            if (psr.Status == PromptStatus.OK)
            {

                SelectionSet ss = psr.Value;

                ObjectId[] SelectedObjectID = ss.GetObjectIds();

                foreach (ObjectId so in SelectedObjectID)
                {

                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(so);
                    if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = at_info.NodeType;
                        Item.ConductorAngle = 0;

                        string PoleNumber = "";
                        Atend.Base.Acad.AT_SUB poleSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(so);
                        foreach (ObjectId oi in poleSub.SubIdCollection)
                        {

                            Atend.Base.Acad.AT_INFO subinfo =
                                Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (subinfo.ParentCode != "NONE" && subinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                            {

                                MText dbt = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as MText;
                                if (dbt != null)
                                {
                                    PoleNumber = dbt.Contents;
                                }
                            }

                        }

                        Item.PoleNumber = PoleNumber;
                        SelectedList.Add(Item);
                        PoleCounter++;
                    }
                    else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
                        Item.ConductorAngle = 0;
                        Item.PoleNumber = "";
                        SelectedList.Add(Item);
                        ConductorCounter++;
                    }
                    else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
                    {
                        Atend.Global.Calculation.General.General.AutocadSelectedList Item =
                            new Atend.Global.Calculation.General.General.AutocadSelectedList();

                        Item.ProductGuid = new Guid(at_info.NodeCode);
                        Item.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                        Item.ConductorAngle = 0;
                        Item.PoleNumber = "";
                        SelectedList.Add(Item);
                        ConductorCounter++;
                    }


                }

                lblPole.Text = PoleCounter.ToString();
                lblConductor.Text = ConductorCounter.ToString();
                ed.WriteMessage("Finish\n");


            }
            else
            {
                lblPole.Text = "0";
                lblConductor.Text = "0";
            }

            // ~~~~~~~~~~~~ End Select Entities


            this.Show();


            #endregion

            //**********************
            ed.WriteMessage("~~~~~~~~~~after select \n");

            dtSectionNumber = new System.Data.DataTable();
            System.Data.DataColumn dcColumn = new System.Data.DataColumn("Name");
            System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("Code");
            dtSectionNumber.Columns.Add(dcColumn);
            dtSectionNumber.Columns.Add(dcColumn1);

            Boolean chk = true;
            //ed.WriteMessage("SelectedList.Count= " + SelectedList.Count.ToString() + "\n");

            for (int i = 0; i < SelectedList.Count; i++)
            {
                chk = true;
                Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
                //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

                Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
                if (SectionCode.Count != 0)
                {
                    for (int j = 0; j < SectionCode.Count; j++)
                    {

                        if ((Guid)SectionCode[j] == poleSection.SectionCode)
                        {
                            chk = false;
                        }


                    }
                    if (chk)
                    {
                        //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
                        SectionCode.Add(poleSection.SectionCode);
                    }
                }
                else
                {
                    //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
                    SectionCode.Add(poleSection.SectionCode);
                }
                //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
            }

            //ed.WriteMessage("Finish Full SectionCode\n");
            ////Add  SectionNumber To DataTable

            for (int i = 0; i < SectionCode.Count; i++)
            {
                 //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                System.Data.DataRow dr = dtSectionNumber.NewRow();
                ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                dr["Name"] = sectionNUmber.Number.ToString();
                dr["Code"] = sectionNUmber.Code.ToString();
                dtSectionNumber.Rows.Add(dr);
            }
            //ed.WriteMessage("Finsh Add  SectionNumber To DataTable\n");
            ////BindDataToGrid In Combo Box For NetCross




            DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvNetCross.Columns["Column2"];
            c.DisplayMember = "Name";
            c.ValueMember = "Code";

            c.DataSource = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            c.Selected = true;
            gvNetCross.AutoGenerateColumns = false;
            gvNetCross.DataSource = dtSectionNumber;

            //Bind Data To ComboBoxSelect Section

            cboSelectSection.DisplayMember = "Name";
            cboSelectSection.ValueMember = "Code";
            cboSelectSection.DataSource = dtSectionNumber;

            //*************************

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            btnSelect.Focus();
            if (SelectedList.Count == 0)
            {
                MessageBox.Show("لطفا یک محدوده را جهت محاسبات انتخاب کنید\n", "خطا");
                btnSelect.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboSelectSection.Text))
            {
                MessageBox.Show("لطفا شماره سکشن را مشخص نمایید\n", "خطا");
                cboSelectSection.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtVTS.Text))
            {
                MessageBox.Show("لطفا UTS را مشخص نمایید\n", "خطا");
                txtVTS.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.DoubleConverter(txtVTS.Text))
            {
                MessageBox.Show("لطفا UTS را با فرمت مناسب وارد نمایید", "خطا");
                txtVTS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboVolt.Text))
            {
                MessageBox.Show("لطفا ولتاژ را مشخص نمایید\n", "خطا");
                cboVolt.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.DoubleConverter(cboVolt.Text))
            {
                MessageBox.Show("لطفا ولتاژ را با فرمت مناسب وارد نمایید", "خطا");
                cboVolt.Focus();
                return false;
            }

            for (int i = 0; i < gvNetCross.Rows.Count; i++)
            {
                //ed.WriteMessage("gvNetCross.Rows[i].Cells[0].Value= " + gvNetCross.Rows[i].Cells[0].Value.ToString() + " cboSelectSection.SelectedText= " + cboSelectSection.Text+"\n");
                if (Convert.ToInt32(gvNetCross.Rows[i].Cells[0].Value) == Convert.ToInt32(cboSelectSection.Text))
                {
                    //ed.WriteMessage("Read NetCross\n");
                    DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
                    if (cboNetCross.Value == null)
                    {
                        MessageBox.Show("لطفا محل عبور شبکه را مشخص نمایید\n", "خطا");
                        return false;
                    }


                }
            }

            if (string.IsNullOrEmpty(txtStart.Text))
            {
                MessageBox.Show("لطفا شروع دما را مشخص نمایید\n", "خطا");
                txtStart.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtStart.Text))
            {
                MessageBox.Show("لطفا شروع دما را با فرمت مناسب وارد نمایید", "خطا");
                txtStart.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEnd.Text))
            {
                MessageBox.Show("لطفا پایان دما را مشخص نمایید\n", "خطا");
                txtEnd.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtEnd.Text))
            {
                MessageBox.Show("لطفا پایان دما را با فرمت مناسب وارد نمایید", "خطا");
                txtEnd.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDistance.Text))
            {
                MessageBox.Show("لطفا فاصله دما را مشخص نمایید\n", "خطا");
                txtDistance.Focus();
                return false;

            }
            if (!Atend.Control.NumericValidation.Int32Converter(txtDistance.Text))
            {
                MessageBox.Show("لطفا پایان فاصله دما را با فرمت مناسب وارد نمایید", "خطا");
                txtDistance.Focus();
                return false;
            }

            System.Data.DataTable dt = Atend.Base.Design.DWeatherType.AccessSelectAll();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("لطفا ابتدا شرایط اب و هوایی را تنظیم نمایید", "خطا");
                return false;
            }

            return true;

        }

        private void پروندهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calcOptimalSagTension = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTensionTest();

            if (Validation())
            {

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                calcOptimalSagTension.DtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(cboSelectSection.SelectedValue.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                ed.WriteMessage("dtPoleSection1={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
                System.Data.DataTable dtPole = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(cboSelectSection.SelectedValue.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

                ed.WriteMessage("dtPoleSection2={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
                foreach (DataRow dr in dtPole.Rows)
                {
                    DataRow drPole = calcOptimalSagTension.DtPoleSection.NewRow();
                    drPole["ProductType"] = dr["ProductType"].ToString();
                    drPole["ProductCode"] = dr["ProductCode"].ToString();
                    drPole["SectionCode"] = dr["SectionCode"].ToString();
                    calcOptimalSagTension.DtPoleSection.Rows.Add(drPole);
                }

                System.Data.DataColumn dc = new System.Data.DataColumn("PoleNumber");
                calcOptimalSagTension.DtPoleSection.Columns.Add(dc);
                foreach (DataRow dr in calcOptimalSagTension.DtPoleSection.Rows)
                {
                    Atend.Base.Design.DPackage node = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["ProductCode"].ToString()));
                    dr["PoleNumber"] = node.Number.ToString();

                }
                calcOptimalSagTension.VTS = Convert.ToDouble(txtVTS.Text);
                calcOptimalSagTension.Volt = Convert.ToDouble(cboVolt.Text);
                calcOptimalSagTension.DtPoleCond = section.dtGlobal;
                calcOptimalSagTension.dtBranchList = section.dtBranchList;

                //ed.WriteMessage("Befor Read NetCross\n");
                for (int i = 0; i < gvNetCross.Rows.Count; i++)
                {
                    if (Convert.ToInt32(gvNetCross.Rows[i].Cells[0].Value) == Convert.ToInt32(cboSelectSection.Text))
                    {
                        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
                        NetCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Convert.ToInt32(cboNetCross.Value.ToString()));

                    }
                }

                if (cboVolt.Text == "400")
                {
                    calcOptimalSagTension.Clearance = NetCross.V380;
                }
                if (cboVolt.Text == "11000")
                {
                    calcOptimalSagTension.Clearance = NetCross.KV11;
                }
                if (cboVolt.Text == "20000")
                {
                    calcOptimalSagTension.Clearance = NetCross.KV20;
                }
                if (cboVolt.Text == "33000")
                {
                    calcOptimalSagTension.Clearance = NetCross.KV32;
                }
                //ed.WriteMessage("Create DtconductorSectionConsol={0}\n", cboSelectSection.SelectedValue.ToString());

                calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(cboSelectSection.SelectedValue.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                if (calcOptimalSagTension.DtconductorSection.Rows.Count == 0)
                {
                    //ed.WriteMessage("Create DtconductorSection={0}\n", cboSelectSection.SelectedValue.ToString());
                    calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(cboSelectSection.SelectedValue.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                    //ed.WriteMessage("Count={0}\n", calcOptimalSagTension.DtconductorSection.Rows.Count);
                }

                System.Data.DataColumn dc1 = new System.Data.DataColumn("Angle");
                calcOptimalSagTension.DtconductorSection.Columns.Add(dc1);
                calcOptimalSagTension.SectionCode = new Guid(cboSelectSection.SelectedValue.ToString());
                calcOptimalSagTension.Start = Convert.ToInt32(txtStart.Text);
                calcOptimalSagTension.End = Convert.ToInt32(txtEnd.Text);
                calcOptimalSagTension.Distance = Convert.ToInt32(txtDistance.Text);
                Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(calcOptimalSagTension.DtconductorSection.Rows[0]["ProductCode"].ToString()));
                //ed.WriteMessage("OOO\n");
                dtSagTension.Rows.Clear();
                dtPole.Rows.Clear();
                dtConductorDay.Rows.Clear();

                se = calcOptimalSagTension.ComputeSE();
                SpanCount = calcOptimalSagTension.DtconductorSection.Rows.Count;

                dtSagTension = calcOptimalSagTension.CalSagTension(MyBranch).Copy();
                dtConductorDay = calcOptimalSagTension.CalcTempTable();

                gvSagAndTension.AutoGenerateColumns = false;
                gvPole.AutoGenerateColumns = false;

                gvSagAndTension.DataSource = dtSagTension;
                gvConductorDay.DataSource = dtConductorDay;
                dtPole = calcOptimalSagTension.WindOnPole();
                gvPole.DataSource = dtPole;
                //calcOptimalSagTension.IsSagOk();


                DataRow dr1 = dtRow.NewRow();
                dr1["ProjectName"] = "";
                dr1["NumSection"] = cboSelectSection.Text;
                dr1["FirstPole"] = "";
                dr1["LastPole"] = "";
                dr1["UTS"] = txtVTS.Text;
                dr1["SE"] = se.ToString();
                dr1["SpanCount"] = SpanCount;
                dr1["SpanLenght"] = "";
                dr1["CondName"] = gvSagAndTension.Rows[0].Cells[0].Value;
                dtRow.Rows.Add(dr1);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Atend.Global.Calculation.Sectioning section = new Atend.Global.Calculation.Sectioning();
        //    section.DetermineSection();
        //}

        private void txtVTS_TextChanged(object sender, EventArgs e)
        {

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void شرایطابوهواییToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Atend.Design.frmWeather weather = new Atend.Design.frmWeather();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(weather);
        }

        private void انتقالبهفایلExelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string NameFlash = "UTS جدول کشش و فلش به روش" +date+ cboSelectSection.Text + ".xls";
            string NameConductor = "UTS جدول روز سیم کشی به روش" + date + cboSelectSection.Text + ".xls";
            string NameForce = " UTS جدول نیرو های وارد بر پایه به روش" + date + cboSelectSection.Text + ".xls";

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\SagAndTensionReport.xls";
            string pathForce = Atend.Control.Common.fullPath + "\\ReportFile\\نیروهای وارد بر پایه.xls";
            string pathConductorDay =    Atend.Control.Common.fullPath + "\\ReportFile\\ConductorDayReport.xls";

            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\ProductList\" + NameFlash, true);
            File.Copy(pathForce, Atend.Control.Common.DesignFullAddress + @"\ProductList\" + NameForce, true);
            File.Copy(pathConductorDay, Atend.Control.Common.DesignFullAddress + @"\ProductList\" + NameConductor, true);
            ed.WriteMessage("PathFlash:={0},PAth={1}\n", NameFlash, path);
            Atend.Global.Utility.UReport.CreateExcelReaportForSagTension(NameFlash, dtSagTension, dtRow.Rows[0]);
            Atend.Global.Utility.UReport.CreateExcelReaportForForce(NameForce, dtPole, dtRow.Rows[0]);
            Atend.Global.Utility.UReport.CreateExcelReaportForConductorDay(NameConductor, dtConductorDay, dtRow.Rows[0]);

        }

        private void cboSelectSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}