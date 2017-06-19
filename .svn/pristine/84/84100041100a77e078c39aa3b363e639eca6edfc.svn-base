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
    public partial class frmOptimalSagTensionVTS : Form
    {

        ArrayList SectionCode = new ArrayList();
        Atend.Global.Calculation.Mechanical.CalcOptimalSagTension calcOptimalSagTension;
        ArrayList SelectedList = new ArrayList();
        System.Data.DataTable dtSectionNumber = new System.Data.DataTable();

        System.Data.DataTable dtSagTension = new System.Data.DataTable();
        System.Data.DataTable dtConductorDay = new System.Data.DataTable();
        System.Data.DataTable dtConductorDay1 = new System.Data.DataTable();

        System.Data.DataTable dtPole = new System.Data.DataTable();

        System.Data.DataTable dtRow = new System.Data.DataTable();

        Atend.Base.Calculating.CNetWorkCross NetCross = new Atend.Base.Calculating.CNetWorkCross();
        Dictionary<string, string> Dic = new Dictionary<string, string>();
        Dictionary<String, string> DicPoleForce = new Dictionary<string, string>();
        double se = 0;
        double SpanCount = 0;

        public System.Data.DataTable dtSection = new System.Data.DataTable();

        Atend.Global.Calculation.Section section;
        bool ForceToClose = false;
        public frmOptimalSagTensionVTS()
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

            dtSection.Columns.Add("SectionCode");
            dtSection.Columns.Add("SectionNumber");
            dtSection.Columns.Add("StartPole");
            dtSection.Columns.Add("EndPole");


            //Dic.Add("ConductorName", "نام سیم");
            //Dic.Add("NormH", "شرایط معمول کشش");
            //Dic.Add("NormF", "شرایط معمول فلش");
            //Dic.Add("IceH", "شرایط یخ سنگین کشش");
            //Dic.Add("IceF", "شرایط یخ سنگین فلش");
            //Dic.Add("WindH", "شرایط باد زیاد کشش");
            //Dic.Add("WindF", "شرایط باد زیاد فلش");
            //Dic.Add("MaxTempH", "شرایط حداکثر دما کشش");
            //Dic.Add("MaxTempF", "شرایط حداکثر دما فلش");
            //Dic.Add("MinTempH", "شرایط حداقل دما کشش");
            //Dic.Add("MinTempF", "شرایط حداقل دما فلش");
            //Dic.Add("WindAndIceH", "شرایط باد و یخ کشش");
            //Dic.Add("WindAndIceF", "شرایط باد و یخ فلش");



            //DicPoleForce.Add("dcPole", "شماره پایه");
            //DicPoleForce.Add("dcNorm", "شرایط معمولی");
            //DicPoleForce.Add("dcIceHeavy", "یخ سنگین");
            //DicPoleForce.Add("dcWindSpeed", "باد زیاد");
            //DicPoleForce.Add("dcMaxTemp", "حداکثر دما");
            //DicPoleForce.Add("dcMinTemp", "حداقل دما");
            //DicPoleForce.Add("dcWindIce", "باد و یخ");

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


            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SelectedList.Clear();
            SectionCode.Clear();
            int gvCount = gvNetCross.Rows.Count - 1;
            for (int i = gvCount; i >= 0; i--)
            {
                gvNetCross.Rows.RemoveAt(i);
            }
            gvNetCross.Refresh();

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

                //lblPole.Text = PoleCounter.ToString();
                //lblConductor.Text = ConductorCounter.ToString();
                //ed.WriteMessage("Finish\n");


            }
            else
            {
                //lblPole.Text = "0";
                //lblConductor.Text = "0";
            }

            // ~~~~~~~~~~~~ End Select Entities


            this.Show();


            #endregion
            //cboVolt.SelectedIndex = 0;


            dtSectionNumber = new System.Data.DataTable();
            System.Data.DataColumn dcColumn = new System.Data.DataColumn("SectionNumber");
            System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("SectionCode");
            dtSectionNumber.Columns.Add(dcColumn);
            dtSectionNumber.Columns.Add(dcColumn1);

            System.Data.DataColumn dcNetCross = new System.Data.DataColumn("NetCrossCode", System.Type.GetType("System.Int32"));
            dtSectionNumber.Columns.Add(dcNetCross);

            System.Data.DataColumn dcUTS = new System.Data.DataColumn("UTS");
            dtSectionNumber.Columns.Add(dcUTS);

            AddNetCrossCodeColumn();


            Boolean chk = true;


            System.Data.DataTable dt = Atend.Base.Design.DPoleSection.AccessSelectAll();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < SelectedList.Count; i++)
                {
                    chk = true;
                    Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];

                    Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
                    if (poleSection.SectionCode != System.Guid.Empty)
                    {
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
                                SectionCode.Add(poleSection.SectionCode);
                            }
                        }
                        else
                        {
                            SectionCode.Add(poleSection.SectionCode);
                        }
                    }
                }


                for (int i = 0; i < SectionCode.Count; i++)
                {

                    Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                    System.Data.DataRow dr = dtSectionNumber.NewRow();
                    //ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                    dr["SectionNumber"] = sectionNUmber.Number.ToString();
                    dr["SectionCode"] = sectionNUmber.Code.ToString();
                    dtSectionNumber.Rows.Add(dr);
                }

                if (dtSectionNumber.Rows.Count > 0)
                {
                   // bindDataToEnterInformation();

                    gvNetCross.AutoGenerateColumns = false;
                    gvNetCross.DataSource = dtSectionNumber;


                    BindDataToGridSection();
                    ChangeColor();
                    gvSection.Rows[0].Selected = true;
                    ReadVoltage();
                }
                // btnContinue.Enabled = true;
            }


        }

        private void bindDataToEnterInformation()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("START bindDataToEnterInformation\n");
            Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
            System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            foreach (DataRow dr in dtSectionNumber.Rows)
            {

                Atend.Base.Calculating.CDefaultMec defaultMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), true);
                if (defaultMec.Code != 0)
                {
                    dr["UTS"] = defaultMec.Uts;
                    dr["NetCrossCode"] = defaultMec.NetCross;
                }
                else
                {
                    dr["UTS"] = cSetDefaultmec.Uts;
                    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
                }
            }

            txtDistance.Text = cSetDefaultmec.Distance.ToString();
            txtEnd.Text = cSetDefaultmec.End.ToString();
            txtStart.Text = cSetDefaultmec.Start.ToString();
            //ed.WriteMessage("FINISH bindDataToEnterInformation\n");
        }

        private void BindDataToGridSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Calculating.CSetDefaultMec cSetDefaultmec = Atend.Base.Calculating.CSetDefaultMec.AccessSelect();
            System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();

            foreach (DataRow dr in dtSectionNumber.Rows)
            {

               

                //ed.WriteMessage("SectionCode={0}\n", new Guid(dr["SectionCode"].ToString()));
                Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //ed.WriteMessage("*****Start={0},End={1}\n", startEnd.StartPole, startEnd.EndPole);
                Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);

                Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                //ed.WriteMessage("&&&\n");
                DataRow drSection = dtSection.NewRow();
                drSection["StartPole"] = dPackStart.Number;
                drSection["EndPole"] = dPackEnd.Number;
                dtSection.Rows.Add(drSection);
            }
           
            //ed.WriteMessage(" EXIT BindDataToGridSection\n");
            gvSection.AutoGenerateColumns = false;
            gvSection.DataSource = dtSection;
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
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    SelectedList.Clear();
            //    SectionCode.Clear();
            //    int gvCount = gvNetCross.Rows.Count - 1;
            //    for (int i = gvCount; i >= 0; i--)
            //    {
            //        gvNetCross.Rows.RemoveAt(i);
            //    }
            //    //gvNetCross.Rows.Clear();
            //    gvNetCross.Refresh();


            //    //#region Select
            //    //this.Hide();

            //    //// ~~~~~~~~~~~~ Start Select Entities 


            //    //int PoleCounter = 0;

            //    //int ConductorCounter = 0;

            //    //PromptSelectionOptions pso = new PromptSelectionOptions();

            //    //pso.MessageForAdding = "Select area where you want: \n";

            //    //PromptSelectionResult psr = ed.GetSelection(pso);

            //    //if (psr.Status == PromptStatus.OK)
            //    //{

            //    //    SelectionSet ss = psr.Value;

            //    //    ObjectId[] SelectedObjectID = ss.GetObjectIds();

            //    //    foreach (ObjectId so in SelectedObjectID)
            //    //    {

            //    //        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(so);
            //    //        if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
            //    //        {
            //    //            Atend.Global.Calculation.General.General.AutocadSelectedList Item =
            //    //                new Atend.Global.Calculation.General.General.AutocadSelectedList();

            //    //            Item.ProductGuid = new Guid(at_info.NodeCode);
            //    //            Item.ProductType = at_info.NodeType;
            //    //            Item.ConductorAngle = 0;

            //    //            string PoleNumber = "";
            //    //            Atend.Base.Acad.AT_SUB poleSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(so);
            //    //            foreach (ObjectId oi in poleSub.SubIdCollection)
            //    //            {

            //    //                Atend.Base.Acad.AT_INFO subinfo =
            //    //                    Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
            //    //                if (subinfo.ParentCode != "NONE" && subinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
            //    //                {

            //    //                    MText dbt = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as MText;
            //    //                    if (dbt != null)
            //    //                    {
            //    //                        PoleNumber = dbt.Contents;
            //    //                    }
            //    //                }

            //    //            }

            //    //            Item.PoleNumber = PoleNumber;
            //    //            SelectedList.Add(Item);
            //    //            PoleCounter++;
            //    //        }
            //    //        else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
            //    //        {
            //    //            Atend.Global.Calculation.General.General.AutocadSelectedList Item =
            //    //                new Atend.Global.Calculation.General.General.AutocadSelectedList();

            //    //            Item.ProductGuid = new Guid(at_info.NodeCode);
            //    //            Item.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
            //    //            Item.ConductorAngle = 0;
            //    //            Item.PoleNumber = "";
            //    //            SelectedList.Add(Item);
            //    //            ConductorCounter++;
            //    //        }
            //    //        else if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
            //    //        {
            //    //            Atend.Global.Calculation.General.General.AutocadSelectedList Item =
            //    //                new Atend.Global.Calculation.General.General.AutocadSelectedList();

            //    //            Item.ProductGuid = new Guid(at_info.NodeCode);
            //    //            Item.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
            //    //            Item.ConductorAngle = 0;
            //    //            Item.PoleNumber = "";
            //    //            SelectedList.Add(Item);
            //    //            ConductorCounter++;
            //    //        }


            //    //    }

            //    //    //lblPole.Text = PoleCounter.ToString();
            //    //    //lblConductor.Text = ConductorCounter.ToString();
            //    //    ed.WriteMessage("Finish\n");


            //    //}
            //    //else
            //    //{
            //    //    //lblPole.Text = "0";
            //    //    //lblConductor.Text = "0";
            //    //}

            //    //// ~~~~~~~~~~~~ End Select Entities


            //    //this.Show();


            //    //#endregion

            //    //**********************
            //    ed.WriteMessage("~~~~~~~~~~after select \n");

            //    dtSectionNumber = new System.Data.DataTable();
            //    System.Data.DataColumn dcColumn = new System.Data.DataColumn("Name");
            //    System.Data.DataColumn dcColumn1 = new System.Data.DataColumn("Code");
            //    dtSectionNumber.Columns.Add(dcColumn);
            //    dtSectionNumber.Columns.Add(dcColumn1);

            //    Boolean chk = true;
            //    //ed.WriteMessage("SelectedList.Count= " + SelectedList.Count.ToString() + "\n");

            //    for (int i = 0; i < SelectedList.Count; i++)
            //    {
            //        chk = true;
            //        Atend.Global.Calculation.General.General.AutocadSelectedList cadInfo = (Atend.Global.Calculation.General.General.AutocadSelectedList)SelectedList[i];
            //        //ed.WriteMessage("Guid= "+cadInfo.ProductGuid.ToString()+"ProductType = "+cadInfo.ProductType.ToString()+"\n");

            //        Atend.Base.Design.DPoleSection poleSection = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(cadInfo.ProductGuid, cadInfo.ProductType);
            //        //ed.WriteMessage("PoleSection.SectionCode Is= "+poleSection.SectionCode.ToString()+"\n");
            //        if (SectionCode.Count != 0)
            //        {
            //            for (int j = 0; j < SectionCode.Count; j++)
            //            {

            //                if ((Guid)SectionCode[j] == poleSection.SectionCode)
            //                {
            //                    chk = false;
            //                }


            //            }
            //            if (chk)
            //            {
            //                //    ed.WriteMessage("Add To SectionCode,SectionCode Is={0}\n", poleSection.SectionCode);
            //                SectionCode.Add(poleSection.SectionCode);
            //            }
            //        }
            //        else
            //        {
            //            //  ed.WriteMessage("Add To SectionCode Else,SectionCode Is={0}\n", poleSection.SectionCode);
            //            SectionCode.Add(poleSection.SectionCode);
            //        }
            //        //ed.WriteMessage("SectionCodeList.Count= "+SectionCode.Count.ToString()+"\n");
            //    }

            //    //ed.WriteMessage("Finish Full SectionCode\n");
            //    ////Add  SectionNumber To DataTable

            //    for (int i = 0; i < SectionCode.Count; i++)
            //    {
            //         //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

            //        Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
            //        System.Data.DataRow dr = dtSectionNumber.NewRow();
            //        ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
            //        dr["Name"] = sectionNUmber.Number.ToString();
            //        dr["Code"] = sectionNUmber.Code.ToString();
            //        dtSectionNumber.Rows.Add(dr);
            //    }
            //    //ed.WriteMessage("Finsh Add  SectionNumber To DataTable\n");
            //    ////BindDataToGrid In Combo Box For NetCross
            //    System.Data.DataColumn dcNetCross = new System.Data.DataColumn("NetCrossCode", System.Type.GetType("System.Int32"));
            //    dtSectionNumber.Columns.Add(dcNetCross);

            //    System.Data.DataColumn dcUTS = new System.Data.DataColumn("UTS");
            //    dtSectionNumber.Columns.Add(dcUTS);

            //    AddNetCrossCodeColumn();
            //    System.Data.DataTable dtNetDefault=Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            //    foreach (DataRow dr in dtSectionNumber.Rows)
            //    {
            //        dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
            //        dr["UTS"] = 10;

            //    }



            //    //DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)gvNetCross.Columns["Column2"];
            //    //c.DisplayMember = "Name";
            //    //c.ValueMember = "Code";

            //    //c.DataSource = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            //    //c.Selected = true;
            //    gvNetCross.AutoGenerateColumns = false;
            //    gvNetCross.DataSource = dtSectionNumber;

            //    //Bind Data To ComboBoxSelect Section

            //    cboSelectSection.DisplayMember = "Name";
            //    cboSelectSection.ValueMember = "Code";
            //    cboSelectSection.DataSource = dtSectionNumber;

            //    //*************************

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            btnSection.Focus();
            //if (SelectedList.Count == 0)
            //{
            //    MessageBox.Show("لطفا یک محدوده را جهت محاسبات انتخاب کنید\n", "خطا");
            //    btnSelect.Focus();
            //    return false;
            //}
            if (gvSection.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفا شماره سکشن را مشخص نمایید\n", "خطا");
                gvSection.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtVTS.Text))
            //{
            //    MessageBox.Show("لطفا UTS را مشخص نمایید\n", "خطا");
            //    txtVTS.Focus();
            //    return false;

            //}
            //if (!Atend.Control.NumericValidation.DoubleConverter(txtVTS.Text))
            //{
            //    MessageBox.Show("لطفا UTS را با فرمت مناسب وارد نمایید", "خطا");
            //    txtVTS.Focus();
            //    return false;
            //}
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

            //for (int i = 0; i < gvNetCross.Rows.Count; i++)
            //{
            //    //ed.WriteMessage("gvNetCross.Rows[i].Cells[0].Value= " + gvNetCross.Rows[i].Cells[0].Value.ToString() + " cboSelectSection.SelectedText= " + cboSelectSection.Text+"\n");
            //    if (Convert.ToInt32(gvNetCross.Rows[i].Cells[0].Value) == Convert.ToInt32(cboSelectSection.Text))
            //    {
            //        //ed.WriteMessage("Read NetCross\n");
            //        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
            //        if (cboNetCross.Value == null)
            //        {
            //            MessageBox.Show("لطفا محل عبور شبکه را مشخص نمایید\n", "خطا");
            //            return false;
            //        }


            //    }
            //}

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
            calcOptimalSagTension = new Atend.Global.Calculation.Mechanical.CalcOptimalSagTension();

            if (Validation())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                // ed.WriteMessage("***&&&&&&SectionNumber={0}\n",gvSection.SelectedRows[0].Cells["SectionNumber"].Value.ToString());
                //ed.WriteMessage("***SectionCode={0}\n", gvSection.Rows[0].Cells[0].Value.ToString());

                calcOptimalSagTension.DtPoleSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                //ed.WriteMessage("dtPoleSection1={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
                System.Data.DataTable dtPole1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                //ed.WriteMessage("dtPoleSection2={0}\n", calcOptimalSagTension.DtPoleSection.Rows.Count);
                foreach (DataRow dr in dtPole1.Rows)
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
                //System.Data.DataTable dtPoleCond1 = Atend.Base.Design.DPoleSection.AccessSelectAll();

                calcOptimalSagTension.Volt = Convert.ToDouble(cboVolt.Text);
                //calcOptimalSagTension.DtPoleCond = section.dtGlobal;
                //calcOptimalSagTension.dtBranchList = section.dtBranchList;
                calcOptimalSagTension.DtPoleCond = Atend.Base.Design.DGlobal.AccessSelectAll();
                calcOptimalSagTension.dtBranchList = Atend.Global.Acad.UAcad.FillBranchList();
                //ed.WriteMessage("Befor Read NetCross\n");
                //for (int i = 0; i < gvNetCross.Rows.Count; i++)
                //{
                //    if (Convert.ToInt32(gvNetCross.Rows[i].Cells[0].Value) == Convert.ToInt32(cboSelectSection.Text))
                //    {
                //        DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
                //        NetCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Convert.ToInt32(cboNetCross.Value.ToString()));

                //    }
                //}

                //ed.WriteMessage("GoToREAD NETCROSS\n");
                foreach (DataRow dr in dtSectionNumber.Rows)// (int i = 0; i < gvNetCross.Rows.Count; i++)
                {
                    //ed.WriteMessage("**Code={0}\n", dr["SectionCode"].ToString());
                    if (Convert.ToInt32(dr["SectionNumber"].ToString()) == Convert.ToInt32(gvSection.SelectedRows[0].Cells[1].Value.ToString()))
                    {
                        //DataGridViewComboBoxCell cboNetCross = (DataGridViewComboBoxCell)gvNetCross.Rows[i].Cells["Column2"];
                        //ed.WriteMessage("NetCrossCode={0}\n", dr["NetCrossCode"].ToString());
                        NetCross = Atend.Base.Calculating.CNetWorkCross.AccessSelectByCode(Convert.ToInt32(dr["NetCrossCode"].ToString()));
                        calcOptimalSagTension.VTS = Convert.ToDouble(dr["UTS"].ToString());
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

                calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                if (calcOptimalSagTension.DtconductorSection.Rows.Count == 0)
                {
                    //ed.WriteMessage("Create DtconductorSection={0}\n", cboSelectSection.SelectedValue.ToString());
                    calcOptimalSagTension.DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                    //ed.WriteMessage("Count={0}\n", calcOptimalSagTension.DtconductorSection.Rows.Count);
                }

                System.Data.DataColumn dc1 = new System.Data.DataColumn("Angle");
                calcOptimalSagTension.DtconductorSection.Columns.Add(dc1);
                calcOptimalSagTension.SectionCode = new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString());
                calcOptimalSagTension.Start = Convert.ToInt32(txtStart.Text);
                calcOptimalSagTension.End = Convert.ToInt32(txtEnd.Text);
                calcOptimalSagTension.Distance = Convert.ToInt32(txtDistance.Text);
                Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(calcOptimalSagTension.DtconductorSection.Rows[0]["ProductCode"].ToString()));
                //ed.WriteMessage("OOO\n");
                dtSagTension.Rows.Clear();
                dtPole.Rows.Clear();
                dtConductorDay.Rows.Clear();

                se = calcOptimalSagTension.ComputeSE02();
                //ed.WriteMessage("&&&&&&&&&&&&&&&&&&&SE={0}\n", se.ToString());
                SpanCount = calcOptimalSagTension.DtconductorSection.Rows.Count;
                calcOptimalSagTension.dtStTable.Rows.Clear();


                dtSagTension = calcOptimalSagTension.CalSagTension02(MyBranch).Copy();
                dtConductorDay = calcOptimalSagTension.CalcTempTable02();

                gvSagAndTension.AutoGenerateColumns = false;
                gvPole.AutoGenerateColumns = false;

                gvSagAndTension.DataSource = dtSagTension;
                gvConductorDay.DataSource = dtConductorDay;
                dtPole = calcOptimalSagTension.WindOnPole02();
                gvPole.DataSource = dtPole;

                Save();



                //ed.WriteMessage("***ROWS.COUNT={0}\n", dtPole.Rows.Count);
                //calcOptimalSagTension.IsSagOk();
                //ed.WriteMessage("********UTS={0}\n", calcOptimalSagTension.VTS);

                btnContinue.Enabled = true;
                btnRotate.Enabled = true;
                DataRow dr1 = dtRow.NewRow();
                dr1["ProjectName"] = "";
                dr1["NumSection"] = gvSection.SelectedRows[0].Cells[1].Value.ToString();
                dr1["FirstPole"] = "";
                dr1["LastPole"] = "";
                dr1["UTS"] = calcOptimalSagTension.VTS;
                dr1["SE"] = se.ToString();
                dr1["SpanCount"] = SpanCount;
                dr1["SpanLenght"] = "";
                dr1["CondName"] = gvSagAndTension.Rows[0].Cells[0].Value;
                dtRow.Rows.Add(dr1);
                ChangeColor();
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Atend.Global.Calculation.Sectioning section = new Atend.Global.Calculation.Sectioning();
        //    section.DetermineSection();
        //}

        private void Save()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);
            DataRow[] drDefault = dtSectionNumber.Select(string.Format(" SectionCode='{0}'", gvSection.SelectedRows[0].Cells[0].Value.ToString()));

            if (defMec.Code != 0)
            {
                //ed.WriteMessage("UTS={0}\n", drDefault[0]["UTS"].ToString());
                defMec.Uts = Convert.ToDouble(drDefault[0]["UTS"].ToString());
                defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                defMec.SE = se;
                defMec.AccessUpdate();
            }
            else
            {
                //ed.WriteMessage("NETCROSSCODE={0}\n", drDefault[0]["NetCrossCode"].ToString());
                defMec.NetCross = Convert.ToInt32(drDefault[0]["NetCrossCode"].ToString());
                defMec.Uts = Convert.ToDouble(drDefault[0]["UTS"].ToString());
                defMec.IsUTS = true;
                defMec.SectionCode = new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString());
                defMec.TrustBorder = 0;
                defMec.Vol = Convert.ToDouble(cboVolt.Text);
                defMec.SE = se;
                defMec.AccessInsert();
            }
            System.Data.DataTable dtSag = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);

            if (dtSag.Rows.Count != 0)
            {
                Atend.Base.Calculating.CSagAndTension.AccessDeleteBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);
            }

            System.Data.DataTable dtWind = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);

            if (dtWind.Rows.Count != 0)
            {
                Atend.Base.Calculating.CWindOnPole.AccessDeleteBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);
            }

            System.Data.DataTable dtCondDay = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);

            if (dtCondDay.Rows.Count != 0)
            {
                Atend.Base.Calculating.CConductorDay.AccessDeleteBySectionCode(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), true);
            }

            foreach (DataRow dr in dtSagTension.Rows)
            {
                Atend.Base.Calculating.CSagAndTension cSagTension = new Atend.Base.Calculating.CSagAndTension();
                cSagTension.ConductorName = dr["ConductorName"].ToString();
                cSagTension.IceF = Convert.ToDouble(dr["IceF"].ToString());
                cSagTension.IceH = Convert.ToDouble(dr["IceH"].ToString());
                cSagTension.MaxTempF = Convert.ToDouble(dr["MaxTempF"].ToString());
                cSagTension.MaxTempH = Convert.ToDouble(dr["MaxTempH"].ToString());
                cSagTension.MinTempF = Convert.ToDouble(dr["MinTempF"].ToString());
                cSagTension.MinTempH = Convert.ToDouble(dr["MinTempH"].ToString());
                cSagTension.NormF = Convert.ToDouble(dr["NormF"].ToString());
                cSagTension.NormH = Convert.ToDouble(dr["NormH"].ToString());
                cSagTension.SectionCode = new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString());
                cSagTension.WindAndIceF = Convert.ToDouble(dr["WindAndIceF"].ToString());
                cSagTension.WindAndIceH = Convert.ToDouble(dr["WindAndIceH"].ToString());
                cSagTension.WindF = Convert.ToDouble(dr["WindF"].ToString());
                cSagTension.WindH = Convert.ToDouble(dr["WindH"].ToString());
                cSagTension.IsUTS = true;
                if (!cSagTension.AccessInsert())
                {
                    ed.WriteMessage("Failed SagTension INSERT\n");
                }
            }

            foreach (DataRow dr in dtConductorDay.Rows)
            {
                Atend.Base.Calculating.CConductorDay condDay = new Atend.Base.Calculating.CConductorDay();
                condDay.SectionCode = new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString());
                condDay.From = dr["From"].ToString();
                condDay.To = dr["To"].ToString();
                condDay.SpanLenght = Convert.ToDouble(dr["SpanLenght"].ToString());
                condDay.ConductorName = dr["ConductorName"].ToString();
                condDay.IsUTS = true;
                if (!condDay.AccessInsert())
                {
                    ed.WriteMessage(" Failed CondDay.Access INSERT\n");
                }

                for (int i = Convert.ToInt32(txtEnd.Text); i > Convert.ToInt32(txtStart.Text); i = i - Convert.ToInt32(txtDistance.Text))
                {
                    Atend.Base.Calculating.CTemp cTemp = new Atend.Base.Calculating.CTemp();
                    //ed.WriteMessage("condday.Code={0}\n", condDay.Code);
                    cTemp.ConductorDayCode = condDay.Code;
                    cTemp.Temp = i;
                    cTemp.Sag = Convert.ToDouble(dr[i.ToString() + "F"].ToString());
                    cTemp.Tension = Convert.ToDouble(dr[i.ToString()].ToString());
                    if (!cTemp.AccessInsert())
                    {
                        ed.WriteMessage("Failed Ctemp.AccessInsert\n");
                    }
                }
            }


            foreach (DataRow dr in dtPole.Rows)
            {
                Atend.Base.Calculating.CWindOnPole windPole = new Atend.Base.Calculating.CWindOnPole();
                windPole.DcIceHeavy = Convert.ToDouble(dr["DcIceHeavy"].ToString());
                windPole.DcIsUTS = true;
                windPole.DcMaxTemp = Convert.ToDouble(dr["DcMaxTemp"].ToString());
                windPole.DcMinTemp = Convert.ToDouble(dr["DcMinTemp"].ToString());
                windPole.DcNorm = Convert.ToDouble(dr["DcNorm"].ToString());
                windPole.DcPole = dr["DcPole"].ToString();
                windPole.DcwindIce = Convert.ToDouble(dr["DcWindIce"].ToString());
                windPole.DcWindSpeed = Convert.ToDouble(dr["DcWindSpeed"].ToString());
                windPole.SectionCode = new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString());
                windPole.DcPoleGuid = new Guid(dr["dcPoleGuid"].ToString());
                windPole.DcAngle = Convert.ToDouble(dr["dcAngle"].ToString());
                if (!windPole.AccessInsert())
                {
                    ed.WriteMessage("Failed WindOnPole INSERT\n");
                }

            }
        }

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
            string NameFlash = "نتیجه محاسبات مکانیکی به روش UTS" + date + gvSection.SelectedRows[0].Cells[1].Value.ToString() + ".xls";
            //ed.WriteMessage("ROWS={0}\n", dtPole.Rows.Count);
            Atend.Global.Utility.UReport.CreateExcelFinalMechanical(NameFlash, true);


        }

        private void cboSelectSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSection_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Delete();

            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            dtSectionNumber.Rows.Clear();
            dtSection.Rows.Clear();
            SectionCode.Clear();
            Boolean chk = true;

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


            for (int i = 0; i < SectionCode.Count; i++)
            {
                //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                System.Data.DataRow dr = dtSectionNumber.NewRow();
                ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                dr["SectionNumber"] = sectionNUmber.Number.ToString();
                dr["SectionCode"] = sectionNUmber.Code.ToString();
                dtSectionNumber.Rows.Add(dr);
            }

            //System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            //foreach (DataRow dr in dtSectionNumber.Rows)
            //{
            //    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
            //    dr["UTS"] = 10;

            //}
            //bindDataToEnterInformation();

            gvNetCross.AutoGenerateColumns = false;
            gvNetCross.DataSource = dtSectionNumber;

            //Bind Data To ComboBoxSelect Section

            //cboSelectSection.DisplayMember = "SectionNumber";
            //cboSelectSection.ValueMember = "SectionCode";
            //cboSelectSection.DataSource = dtSectionNumber;

            BindDataToGridSection();
            gvSection.Rows[0].Selected = true;
            ReadVoltage();
            btnContinue.Enabled = false;
            btnRotate.Enabled = false;
        }

        private void Delete()
        {
            Atend.Base.Design.DGlobal.AccessDelete();
            Atend.Base.Design.DPoleSection.AccessDelete();
            Atend.Base.Design.DSection.AccessDelete();
            Atend.Base.Calculating.CStartEnd.AccessDelete();
            Atend.Base.Calculating.CSagAndTension.AccessDelete();
            Atend.Base.Calculating.CWindOnPole.AccessDelete();
            Atend.Base.Calculating.CConductorDay.AccessDelete();
            Atend.Base.Calculating.CTemp.AccessDelete();


            dtSagTension.Rows.Clear();
            dtPole.Rows.Clear();
            dtConductorDay.Rows.Clear();
            dtConductorDay1.Rows.Clear();
        }

        private void ChangeColor()
        {
            for (int i = 0; i < gvSection.Rows.Count; i++)
            {
                System.Data.DataTable cSag = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.Rows[0].Cells[0].Value.ToString()), true);
                if (cSag.Rows.Count != 0)
                {
                    gvSection.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                }
            }
        }

        public void AddNetCrossCodeColumn()
        {
            System.Data.DataTable dt2 = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            DataGridViewComboBoxColumn c = new DataGridViewComboBoxColumn();
            c.DataSource = dt2;
            c.DisplayMember = "Name";
            c.ValueMember = "Code";
            c.DataPropertyName = "NetCrossCode";
            c.HeaderText = "محل عبور شبکه";
            gvNetCross.Columns.Insert(gvNetCross.Columns.Count - 1, c);


        }

        private void gvSection_Click(object sender, EventArgs e)
        {
            ArrayList arTemp = new ArrayList();

            dtSagTension.Rows.Clear();
            dtPole.Rows.Clear();
            dtConductorDay1.Columns.Clear();
            dtConductorDay1.Rows.Clear();

            bool chkAdd = true;

            dtSagTension = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), true);
            dtPole = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), true);
            System.Data.DataTable dtTemp = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), true);
            System.Data.DataTable dtCond = Atend.Base.Calculating.CConductorDay.AccessSelect(new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()), true);

            if (dtTemp.Rows.Count != 0)
            {
                arTemp.Add(dtTemp.Rows[0]["Temp"].ToString());

                foreach (DataRow dr in dtTemp.Rows)
                {
                    chkAdd = true;
                    for (int i = 0; i < arTemp.Count; i++)
                    {
                        if (arTemp[i].ToString() == dr["Temp"].ToString())
                        {
                            chkAdd = false;
                        }
                    }
                    if (chkAdd)
                    {
                        arTemp.Add(dr["Temp"].ToString());
                    }
                }

                dtConductorDay1.Columns.Add("From");
                dtConductorDay1.Columns.Add("TO");
                dtConductorDay1.Columns.Add("SpanLenght");
                dtConductorDay1.Columns.Add("ConductorName");
            }
            for (int i = 0; i < arTemp.Count; i++)
            {
                dtConductorDay1.Columns.Add(arTemp[i].ToString());
                dtConductorDay1.Columns.Add(arTemp[i].ToString() + "F");
            }

            foreach (DataRow dr in dtCond.Rows)
            {
                DataRow dr1 = dtConductorDay1.NewRow();
                dr1["From"] = dr["From"].ToString();
                dr1["To"] = dr["TO"].ToString();
                dr1["Spanlenght"] = dr["SpanLenght"].ToString();
                dr1["ConductorName"] = dr["ConductorName"].ToString();
                for (int i = 0; i < arTemp.Count; i++)
                {
                    DataRow[] drTemp = dtTemp.Select(string.Format(" Temp={0} AND ConductorDayCode={1}", arTemp[i].ToString(), dr["Code"].ToString()));
                    dr1[arTemp[i].ToString()] = drTemp[0]["Tension"].ToString();
                    dr1[arTemp[i].ToString() + "F"] = drTemp[0]["Sag"].ToString();
                }
                dtConductorDay1.Rows.Add(dr1);
            }

            if (dtConductorDay1.Rows.Count != 0)
                gvConductorDay.DataSource = dtConductorDay1;

            if (dtSagTension.Rows.Count != 0)
            {
                gvSagAndTension.AutoGenerateColumns = false;
                gvSagAndTension.DataSource = dtSagTension;
            }

            if (dtPole.Rows.Count != 0)
            {
                gvPole.AutoGenerateColumns = false;
                gvPole.DataSource = dtPole;
                btnContinue.Enabled = true;
                btnRotate.Enabled = true;
            }


        }

        private void ReadVoltage()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("ReadVoltage\n");
            System.Data.DataTable dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
            //ed.WriteMessage("ReadVoltage\n");
            if (dtsec.Rows.Count == 0)
                dtsec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(gvSection.SelectedRows[0].Cells[0].Value.ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

            //ed.WriteMessage("@@\n");
            Atend.Base.Design.DPackage dParent = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dtsec.Rows[0]["ProductCode"].ToString()));
            System.Data.DataTable dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(dParent.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            if (dt.Rows.Count == 0)
                dt = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtsec.Rows[0]["ProductCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            ed.WriteMessage("BeforConsol Count={0}\n", dt.Rows.Count);
            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
            {
                ed.WriteMessage("*******\n");
                Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eConsol.VoltageLevel == 400)
                    cboVolt.Text = "400";

                if (eConsol.VoltageLevel == 1100)
                    cboVolt.Text = "1100";

                if (eConsol.VoltageLevel == 20000)
                    cboVolt.Text = "20000";

                if (eConsol.VoltageLevel == 33000)
                    cboVolt.Text = "33000";
            }

            if (Convert.ToInt32(dt.Rows[0]["Type"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
            {
                Atend.Base.Equipment.EClamp eKalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(dt.Rows[0]["ProductCode"].ToString()));
                if (eKalamp.VoltageLevel == 400)
                    cboVolt.Text = "400";

                if (eKalamp.VoltageLevel == 1100)
                    cboVolt.Text = "1100";

                if (eKalamp.VoltageLevel == 20000)
                    cboVolt.Text = "20000";

                if (eKalamp.VoltageLevel == 33000)
                    cboVolt.Text = "33000";
            }
            ed.WriteMessage("FINISH++\n");


        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            frmChoiceForce Force = new frmChoiceForce(dtPole, true, new Guid(gvSection.Rows[gvSection.CurrentRow.Index].Cells[0].Value.ToString()));
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Force);
        }

        private void سکشنبندیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Delete();

            section = new Atend.Global.Calculation.Section();
            section.DetermineSection();

            dtSectionNumber.Rows.Clear();
            dtSection.Rows.Clear();
            SectionCode.Clear();
            Boolean chk = true;

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


            for (int i = 0; i < SectionCode.Count; i++)
            {
                //ed.WriteMessage("**Sec.Code={0}\n", SectionCode[i].ToString());

                Atend.Base.Design.DSection sectionNUmber = Atend.Base.Design.DSection.AccessSelectByCode((Guid)SectionCode[i]);
                System.Data.DataRow dr = dtSectionNumber.NewRow();
                ed.WriteMessage("Sec.Number={0},Sec.Code={1}\n", sectionNUmber.Number, sectionNUmber.Code);
                dr["SectionNumber"] = sectionNUmber.Number.ToString();
                dr["SectionCode"] = sectionNUmber.Code.ToString();
                dtSectionNumber.Rows.Add(dr);
            }

            //System.Data.DataTable dtNetDefault = Atend.Base.Calculating.CNetWorkCross.AccessSelectAll();
            //foreach (DataRow dr in dtSectionNumber.Rows)
            //{
            //    dr["NetCrossCode"] = Convert.ToInt32(dtNetDefault.Rows[0]["Code"].ToString());
            //    dr["UTS"] = 10;

            //}
            bindDataToEnterInformation();

            gvNetCross.AutoGenerateColumns = false;
            gvNetCross.DataSource = dtSectionNumber;

            //Bind Data To ComboBoxSelect Section

            //cboSelectSection.DisplayMember = "SectionNumber";
            //cboSelectSection.ValueMember = "SectionCode";
            //cboSelectSection.DataSource = dtSectionNumber;

            BindDataToGridSection();
            gvSection.Rows[0].Selected = true;
            ReadVoltage();
            btnContinue.Enabled = false;
            btnRotate.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)//Rotate
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            foreach (DataRow dr in dtPole.Rows)
            {
                Atend.Base.Design.DPackage dpack = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(dr["dcPoleGuid"].ToString()));
                int shape = 0;
                int type = 0;
                if (dpack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.Pole))
                {
                    ed.WriteMessage("***Pole\n");
                    Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dpack.ProductCode);
                    shape = Pole.Shape;
                    type = Pole.Type;
                }
                if (dpack.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))
                {
                    ed.WriteMessage("***PoleTip\n");
                    Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dpack.ProductCode);
                    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.AccessSelectByCode(PoleTip.PoleCode);
                    shape = pole.Shape;
                    type = pole.Type;
                }
                ed.WriteMessage("Type={0},Shape={1}", type, shape);
                ed.WriteMessage("Angle={0},dcPoleGuid={1}", dr["dcAngle"].ToString(), dr["dcPoleGuid"].ToString());
                if (shape == 0)
                {

                    Atend.Global.Acad.DrawEquips.AcDrawCirclePole.RotatePoleCircle(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));
                }
                if (shape == 1)
                {
                    if (type == 2)
                    {
                        Atend.Global.Acad.DrawEquips.AcDrawPolygonPole.RotatePolePolygon(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));

                    }
                    else
                    {
                        Atend.Global.Acad.DrawEquips.AcDrawPole.RotatePole(Convert.ToDouble(dr["dcAngle"].ToString()), new Guid(dr["dcPoleGuid"].ToString()));
                    }


                }

            }
        }


    }
}