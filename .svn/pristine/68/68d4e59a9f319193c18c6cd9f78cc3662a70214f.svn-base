using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Excel1 = Microsoft.Office.Interop.Excel;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;
using System.Data.OleDb;

namespace Atend.Global.Utility
{
    public class UReport
    {

        //get for tehran 7/15
        int PID;
        public DataTable DT = new DataTable();
        public static Excel1.Application ExcelApp1 = new Excel1.ApplicationClass();
        public static Excel1.Workbook objBook1;
        Atend.Control.ConnectionString _ConnectionString;


        public static bool CreateExcelReaport(string FileName, DataTable dataTable, Dictionary<string, string> HeaderName, int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 1;



            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;  //SHOW EXCEL'S FILE
            Excel1.Workbook objBook = ExcelApp.Workbooks.Add(missing);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[1];


            foreach (DataColumn dc in dataTable.Columns)
            {
                if (HeaderName.ContainsKey(dc.ColumnName))  //name
                {
                    string value;
                    HeaderName.TryGetValue(dc.ColumnName, out value);
                    //string value = HeaderName.Values.ToString();
                    RowCounter = 1;
                    objSheet.Cells[RowCounter, ColumnCounter] = value;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //******************************************
                        RowCounter++;
                        objSheet.Cells[RowCounter, ColumnCounter] = dr[dc.ColumnName].ToString();

                    }
                    ColumnCounter++;
                }

            }

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\ProductList\");

            FileName = Atend.Control.Common.DesignFullAddress + @"\ProductList\" + FileName;
            //ed.WriteMessage("FileName={0}\n", FileName);

            try
            {
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void CreateExcelRudSurface(string FileName)
        {
            int RowCounter = 1;
            int ColumnCounter = 1;

            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\RudSurface.xls";
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }

            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[2];
            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            DataTable dtSection = Atend.Base.Calculating.CRudSurface.AccessSelectCountOfSection();

            objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = desProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = desProfile.Zone;
            RowCounter++;
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            foreach (DataRow dr in dtSection.Rows)
            {
                RowCounter++;
                Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                DataTable dtRudSurface = new DataTable();
                dtRudSurface = Atend.Base.Calculating.CRudSurface.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                objSheet.Cells[RowCounter, 2] = dtRudSurface.Rows.Count;
                foreach (DataRow drRudSurface in dtRudSurface.Rows)
                {
                    //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                    RowCounter++;
                    objSheet.Cells[RowCounter, 2] = drRudSurface["dcPole"].ToString();
                    objSheet.Cells[RowCounter, 3] = drRudSurface["dcNorm"].ToString();
                    objSheet.Cells[RowCounter, 4] = drRudSurface["dcIceHeavy"].ToString();
                    objSheet.Cells[RowCounter, 5] = drRudSurface["dcWindSpeed"].ToString();
                    objSheet.Cells[RowCounter, 6] = drRudSurface["dcMaxTemp"].ToString();
                    objSheet.Cells[RowCounter, 7] = drRudSurface["dcMinTemp"].ToString();
                    objSheet.Cells[RowCounter, 8] = drRudSurface["dcWindIce"].ToString();

                }

            }


            try
            {
                ExcelApp.DisplayAlerts = false;

                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                //objBook.SaveAs(FilePath, Excel1.XlFileFormat.xlExcel9795, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
                //objSheet = (Excel1.Worksheet)objBook.Sheets["Data1"];
                //objSheet.Activate();// = (Excel1.Worksheet)objBook.ActiveSheet;

                //objBook.Save();

                //ExcelApp.Workbooks.Close();


            }
            catch (System.Exception ex)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Is :{0}\n", ex.Message);
                //return false;
            }




        }

        public static void CreateExcelPowerWithHalter(string FileName, bool Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 1;

            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\PoleWithHalter.xls";
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            ed.WriteMessage("Sourcepath={0},DistinationPath={1}\n", path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName);

            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[2];
            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CPowerWithHalter.AccessSelectCountOfSection();

            objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = desProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = desProfile.Zone;
            RowCounter++;
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            foreach (DataRow dr in dtSection.Rows)
            {
                RowCounter++;
                Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                DataTable dtPowerWithHalter = new DataTable();
                dtPowerWithHalter = Atend.Base.Calculating.CPowerWithHalter.AccessSelectBySectionCodeIsUTS(new Guid(dr["SectionCode"].ToString()), Type);
                objSheet.Cells[RowCounter, 2] = dtPowerWithHalter.Rows.Count;
                foreach (DataRow drPowerWithHalter in dtPowerWithHalter.Rows)
                {
                    //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                    RowCounter++;
                    objSheet.Cells[RowCounter, 2] = drPowerWithHalter["PoleNum"].ToString();
                    objSheet.Cells[RowCounter, 3] = drPowerWithHalter["PolePower"].ToString();
                    objSheet.Cells[RowCounter, 4] = drPowerWithHalter["PoleCount"].ToString();
                    objSheet.Cells[RowCounter, 5] = drPowerWithHalter["HalterName"].ToString();
                    objSheet.Cells[RowCounter, 6] = drPowerWithHalter["HalterPower"].ToString();
                    objSheet.Cells[RowCounter, 7] = drPowerWithHalter["HalterCount"].ToString();

                }

            }



            try
            {
                ExcelApp.DisplayAlerts = false;

                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                //objBook.SaveAs(FilePath, Excel1.XlFileFormat.xlExcel9795, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
                //objSheet = (Excel1.Worksheet)objBook.Sheets["Data1"];
                //objSheet.Activate();// = (Excel1.Worksheet)objBook.ActiveSheet;

                //objBook.Save();

                //ExcelApp.Workbooks.Close();


            }
            catch (System.Exception ex)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Is :{0}\n", ex.Message);
                //return false;
            }



        }

        public static void CreateExcelPowerWithOutHalter(string FileName, bool Type)
        {
            int RowCounter = 1;
            int ColumnCounter = 1;

            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\PoleWithoutHalter.xls";
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }

            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[2];
            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            DataTable dtSection = Atend.Base.Calculating.CPowerWithOutHalter.AccessSelectCountOfSection();

            objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = desProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = desProfile.Zone;
            RowCounter++;
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            foreach (DataRow dr in dtSection.Rows)
            {
                RowCounter++;
                Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                DataTable dtPowerWithOutHalter = new DataTable();
                dtPowerWithOutHalter = Atend.Base.Calculating.CPowerWithOutHalter.AccessSelectBySectionCodeISUTS(new Guid(dr["SectionCode"].ToString()), Type);
                objSheet.Cells[RowCounter, 2] = dtPowerWithOutHalter.Rows.Count;
                foreach (DataRow drPowerWithOutHalter in dtPowerWithOutHalter.Rows)
                {
                    //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                    RowCounter++;
                    objSheet.Cells[RowCounter, 2] = drPowerWithOutHalter["PoleNum"].ToString();
                    objSheet.Cells[RowCounter, 3] = drPowerWithOutHalter["PolePower"].ToString();
                    objSheet.Cells[RowCounter, 4] = drPowerWithOutHalter["PoleCount"].ToString();

                }

            }
            try
            {
                ExcelApp.DisplayAlerts = false;

                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                //objBook.SaveAs(FilePath, Excel1.XlFileFormat.xlExcel9795, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
                //objSheet = (Excel1.Worksheet)objBook.Sheets["Data1"];
                //objSheet.Activate();// = (Excel1.Worksheet)objBook.ActiveSheet;

                //objBook.Save();

                //ExcelApp.Workbooks.Close();


            }
            catch (System.Exception ex)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Is :{0}\n", ex.Message);
                //return false;
            }

        }

        //use in ribbon
        public static void CreateExcelFinalMechanical(String FileName, bool Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            if (Type)
            {
                #region UTS
                int RowCounter = 1;
                int ColumnCounter = 1;

                System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

                string path = Atend.Control.Common.fullPath + "\\ReportFile\\Mechanical_UTS.xls";
                Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
                if (desProfile.Id == 0)
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "اخطار";
                    notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                    notification.infoCenterBalloon();
                    return;
                }

                //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
                FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;

                File.Delete(FileName);

                object missing = System.Reflection.Missing.Value;
                Excel1.Application ExcelApp = new Excel1.ApplicationClass();
                ExcelApp.Visible = false;
                Excel1.Workbook objBook = ExcelApp.Workbooks.Open(/*FileName*/ path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[4];
                if (desProfile.Id == 0)
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "اخطار";
                    notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                    notification.infoCenterBalloon();
                    return;
                }
                DataTable dtSection = Atend.Base.Calculating.CConductorDay.AccessSelectFindCountOfSection(Type);
                ArrayList arTemp = new ArrayList();

                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
                RowCounter++;//2
                objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;//تعداد سکشن
                bool chkAdd;
                //ed.WriteMessage("drSection.rows.Count={0},SectionCode={1}\n", dtSection.Rows.Count, dtSection.Rows[0]["SectionCode"].ToString());
                RowCounter = 5;
                foreach (DataRow dr in dtSection.Rows)
                {
                    DataTable dtConductorDay1 = new DataTable();
                    System.Data.DataTable dtTemp = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), true);
                    System.Data.DataTable dtCond = Atend.Base.Calculating.CConductorDay.AccessSelect(new Guid(dr["SectionCode"].ToString()), true);
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    //ed.WriteMessage("dtTemp.rows.Count={0},dtCond.Rows.count={1}\n", dtTemp.Rows.Count, dtCond.Rows.Count);

                    Atend.Base.Calculating.CDefaultMec DefMEC = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    //RowCounter++;
                    objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                    objSheet.Cells[RowCounter, 2] = DefMEC.SE;
                    objSheet.Cells[RowCounter, 3] = DefMEC.Uts;
                    if (dtTemp.Rows.Count != 0)
                    {
                        //arTemp.Add(dtTemp.Rows[0]["Temp"].ToString());

                        foreach (DataRow drTemp in dtTemp.Rows)
                        {
                            chkAdd = true;
                            for (int i = 0; i < arTemp.Count; i++)
                            {
                                if (arTemp[i].ToString() == drTemp["Temp"].ToString())
                                {
                                    chkAdd = false;
                                }
                            }
                            if (chkAdd)
                            {
                                arTemp.Add(drTemp["Temp"].ToString());
                            }
                        }
                        //ed.WriteMessage("ArrTemp.Count={0}\n", arTemp.Count);
                        for (int t = 0; t < arTemp.Count; t++)
                        {
                            objSheet.Cells[4, t + 1] = arTemp[t].ToString();
                        }
                        objSheet.Cells[3, 1] = arTemp.Count;//تعداد دما
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

                    foreach (DataRow drCond in dtCond.Rows)
                    {
                        DataRow dr1 = dtConductorDay1.NewRow();
                        dr1["From"] = drCond["From"].ToString();
                        dr1["To"] = drCond["TO"].ToString();
                        dr1["Spanlenght"] = drCond["SpanLenght"].ToString();
                        dr1["ConductorName"] = drCond["ConductorName"].ToString();
                        for (int i = 0; i < arTemp.Count; i++)
                        {
                            DataRow[] drTemp = dtTemp.Select(string.Format(" Temp={0} AND ConductorDayCode={1}", arTemp[i].ToString(), drCond["Code"].ToString()));
                            dr1[arTemp[i].ToString()] = drTemp[0]["Tension"].ToString();
                            dr1[arTemp[i].ToString() + "F"] = drTemp[0]["Sag"].ToString();
                        }
                        dtConductorDay1.Rows.Add(dr1);
                    }

                    objSheet.Cells[RowCounter, 4] = dtConductorDay1.Rows.Count.ToString();
                    RowCounter++;
                    ColumnCounter = 2;
                    for (int RowCounterDT = 0; RowCounterDT < dtConductorDay1.Rows.Count; RowCounterDT++)
                    {
                        ColumnCounter = 2;
                        for (int ColumnCounterDT = 0; ColumnCounterDT < dtConductorDay1.Columns.Count; ColumnCounterDT++)
                        {

                            objSheet.Cells[RowCounter, ColumnCounter] = dtConductorDay1.Rows[RowCounterDT][ColumnCounterDT].ToString();
                            ColumnCounter++;
                        }
                        RowCounter++;

                    }
                }

                //objSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden;
                objSheet = (Excel1.Worksheet)objBook.Sheets[5];

                RowCounter = 1;
                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;

                RowCounter++;//2
                objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

                foreach (DataRow dr in dtSection.Rows)
                {
                    RowCounter++;
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                    Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), true);
                    objSheet.Cells[RowCounter, 2] = defMec.SE;
                    objSheet.Cells[RowCounter, 3] = defMec.Uts;
                    DataTable dtPole = new DataTable();
                    dtPole = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    objSheet.Cells[RowCounter, 4] = dtPole.Rows.Count;
                    foreach (DataRow drPole in dtPole.Rows)
                    {
                        //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                        RowCounter++;
                        objSheet.Cells[RowCounter, 2] = drPole["dcPole"].ToString();
                        objSheet.Cells[RowCounter, 3] = drPole["dcNorm"].ToString();
                        objSheet.Cells[RowCounter, 4] = drPole["dcIceHeavy"].ToString();
                        objSheet.Cells[RowCounter, 5] = drPole["dcWindSpeed"].ToString();
                        objSheet.Cells[RowCounter, 6] = drPole["dcMaxTemp"].ToString();
                        objSheet.Cells[RowCounter, 7] = drPole["dcMinTemp"].ToString();
                        objSheet.Cells[RowCounter, 8] = drPole["dcWindIce"].ToString();

                    }

                }


                objSheet = (Excel1.Worksheet)objBook.Sheets[6];
                RowCounter = 1;
                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;

                RowCounter++;//2
                DataTable dtSagTension1 = Atend.Base.Calculating.CSagAndTension.AccessSelectByIsUTS(true);

                objSheet.Cells[RowCounter, 1] = dtSagTension1.Rows.Count;

                foreach (DataRow dr in dtSection.Rows)
                {
                    //RowCounter++;

                    Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), true);

                    DataTable dtSagTension = new DataTable();
                    dtSagTension = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    foreach (DataRow drSag in dtSagTension.Rows)
                    {
                        //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                        RowCounter++;
                        objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                        objSheet.Cells[RowCounter, 2] = defMec.SE;
                        objSheet.Cells[RowCounter, 3] = defMec.Uts;
                        objSheet.Cells[RowCounter, 4] = drSag["ConductorName"].ToString();
                        objSheet.Cells[RowCounter, 5] = drSag["NormH"].ToString();
                        objSheet.Cells[RowCounter, 6] = drSag["NormF"].ToString();
                        objSheet.Cells[RowCounter, 7] = drSag["WindAndIceH"].ToString();
                        objSheet.Cells[RowCounter, 8] = drSag["WindAndIceF"].ToString();
                        objSheet.Cells[RowCounter, 9] = drSag["MinTempH"].ToString();
                        objSheet.Cells[RowCounter, 10] = drSag["MinTempF"].ToString();

                        objSheet.Cells[RowCounter, 11] = drSag["MaxTempH"].ToString();
                        objSheet.Cells[RowCounter, 12] = drSag["MaxTempF"].ToString();
                        objSheet.Cells[RowCounter, 13] = drSag["WindH"].ToString();
                        objSheet.Cells[RowCounter, 14] = drSag["WindF"].ToString();
                        objSheet.Cells[RowCounter, 15] = drSag["IceH"].ToString();
                        objSheet.Cells[RowCounter, 16] = drSag["IceF"].ToString();

                    }


                }

                //ed.WriteMessage("\n@@@@@@@@Report File Address = {0}", FileName);
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing,
Excel1.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                //objBook.Save();
                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();

                #endregion
            }
            else
            {
                #region MAXF
                int RowCounter = 1;
                int ColumnCounter = 1;

                System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");
                string path = Atend.Control.Common.fullPath + "\\ReportFile\\Mechanical_MaxF.xls";
                Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
                if (desProfile.Id == 0)
                {
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "اخطار";
                    notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                    notification.infoCenterBalloon();
                    return;
                }

                //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
                FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
                File.Delete(FileName);

                object missing = System.Reflection.Missing.Value;
                Excel1.Application ExcelApp = new Excel1.ApplicationClass();
                ExcelApp.Visible = false;
                Excel1.Workbook objBook = ExcelApp.Workbooks.Open(path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[4];
                Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

                DataTable dtSection = Atend.Base.Calculating.CConductorDay.AccessSelectFindCountOfSection(Type);
                ArrayList arTemp = new ArrayList();

                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
                RowCounter++;//2
                objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;//تعداد سکشن
                bool chkAdd;
                //ed.WriteMessage("drSection.rows.Count={0},SectionCode={1}\n", dtSection.Rows.Count, dtSection.Rows[0]["SectionCode"].ToString());
                RowCounter = 5;
                foreach (DataRow dr in dtSection.Rows)
                {
                    DataTable dtConductorDay1 = new DataTable();
                    System.Data.DataTable dtTemp = Atend.Base.Calculating.CConductorDay.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    System.Data.DataTable dtCond = Atend.Base.Calculating.CConductorDay.AccessSelect(new Guid(dr["SectionCode"].ToString()), Type);
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    //ed.WriteMessage("dtTemp.rows.Count={0},dtCond.Rows.count={1}\n", dtTemp.Rows.Count, dtCond.Rows.Count);

                    Atend.Base.Calculating.CDefaultMec DefMEC = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    //RowCounter++;
                    objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                    objSheet.Cells[RowCounter, 2] = DefMEC.SE;
                    //objSheet.Cells[RowCounter, 3] = DefMEC.Uts;
                    if (dtTemp.Rows.Count != 0)
                    {
                        //arTemp.Add(dtTemp.Rows[0]["Temp"].ToString());

                        foreach (DataRow drTemp in dtTemp.Rows)
                        {
                            chkAdd = true;
                            for (int i = 0; i < arTemp.Count; i++)
                            {
                                if (arTemp[i].ToString() == drTemp["Temp"].ToString())
                                {
                                    chkAdd = false;
                                }
                            }
                            if (chkAdd)
                            {
                                arTemp.Add(drTemp["Temp"].ToString());
                            }
                        }
                        //ed.WriteMessage("ArrTemp.Count={0}\n", arTemp.Count);
                        for (int t = 0; t < arTemp.Count; t++)
                        {
                            objSheet.Cells[4, t + 1] = arTemp[t].ToString();
                        }
                        objSheet.Cells[3, 1] = arTemp.Count;//تعداد دما
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

                    foreach (DataRow drCond in dtCond.Rows)
                    {
                        DataRow dr1 = dtConductorDay1.NewRow();
                        dr1["From"] = drCond["From"].ToString();
                        dr1["To"] = drCond["TO"].ToString();
                        dr1["Spanlenght"] = drCond["SpanLenght"].ToString();
                        dr1["ConductorName"] = drCond["ConductorName"].ToString();
                        for (int i = 0; i < arTemp.Count; i++)
                        {
                            DataRow[] drTemp = dtTemp.Select(string.Format(" Temp={0} AND ConductorDayCode={1}", arTemp[i].ToString(), drCond["Code"].ToString()));
                            dr1[arTemp[i].ToString()] = drTemp[0]["Tension"].ToString();
                            dr1[arTemp[i].ToString() + "F"] = drTemp[0]["Sag"].ToString();
                        }
                        dtConductorDay1.Rows.Add(dr1);
                    }

                    objSheet.Cells[RowCounter, 3] = dtConductorDay1.Rows.Count.ToString();
                    RowCounter++;
                    ColumnCounter = 2;
                    for (int RowCounterDT = 0; RowCounterDT < dtConductorDay1.Rows.Count; RowCounterDT++)
                    {
                        ColumnCounter = 2;
                        for (int ColumnCounterDT = 0; ColumnCounterDT < dtConductorDay1.Columns.Count; ColumnCounterDT++)
                        {

                            objSheet.Cells[RowCounter, ColumnCounter] = dtConductorDay1.Rows[RowCounterDT][ColumnCounterDT].ToString();
                            ColumnCounter++;
                        }
                        RowCounter++;

                    }
                }

                //objSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden;
                objSheet = (Excel1.Worksheet)objBook.Sheets[5];

                RowCounter = 1;
                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;

                RowCounter++;//2
                objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

                foreach (DataRow dr in dtSection.Rows)
                {
                    RowCounter++;
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                    Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    objSheet.Cells[RowCounter, 2] = defMec.SE;
                    //objSheet.Cells[RowCounter, 3] = defMec.Uts;
                    DataTable dtPole = new DataTable();
                    dtPole = Atend.Base.Calculating.CWindOnPole.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    objSheet.Cells[RowCounter, 3] = dtPole.Rows.Count;
                    foreach (DataRow drPole in dtPole.Rows)
                    {
                        //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                        RowCounter++;
                        objSheet.Cells[RowCounter, 2] = drPole["dcPole"].ToString();
                        objSheet.Cells[RowCounter, 3] = drPole["dcNorm"].ToString();
                        objSheet.Cells[RowCounter, 4] = drPole["dcIceHeavy"].ToString();
                        objSheet.Cells[RowCounter, 5] = drPole["dcWindSpeed"].ToString();
                        objSheet.Cells[RowCounter, 6] = drPole["dcMaxTemp"].ToString();
                        objSheet.Cells[RowCounter, 7] = drPole["dcMinTemp"].ToString();
                        objSheet.Cells[RowCounter, 8] = drPole["dcWindIce"].ToString();

                    }

                }

                objSheet = (Excel1.Worksheet)objBook.Sheets[6];
                RowCounter = 1;
                objSheet.Cells[RowCounter, 1] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                objSheet.Cells[RowCounter, 2] = desProfile.Designer;
                objSheet.Cells[RowCounter, 3] = _date;
                objSheet.Cells[RowCounter, 4] = desProfile.Zone;
                objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;

                RowCounter++;//2
                DataTable dtSagTension1 = Atend.Base.Calculating.CSagAndTension.AccessSelectByIsUTS(Type);

                objSheet.Cells[RowCounter, 1] = dtSagTension1.Rows.Count;

                foreach (DataRow dr in dtSection.Rows)
                {
                    //RowCounter++;

                    Atend.Base.Calculating.CDefaultMec defMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);

                    DataTable dtSagTension = new DataTable();
                    dtSagTension = Atend.Base.Calculating.CSagAndTension.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), Type);
                    Atend.Base.Design.DSection dtSecNumber = Atend.Base.Design.DSection.AccessSelectByCode(new Guid(dr["SectionCode"].ToString()));

                    foreach (DataRow drSag in dtSagTension.Rows)
                    {
                        //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                        RowCounter++;
                        objSheet.Cells[RowCounter, 1] = dtSecNumber.Number;
                        objSheet.Cells[RowCounter, 2] = defMec.SE;
                        //objSheet.Cells[RowCounter, 3] = defMec.Uts;
                        objSheet.Cells[RowCounter, 3] = drSag["ConductorName"].ToString();
                        objSheet.Cells[RowCounter, 4] = drSag["NormH"].ToString();
                        objSheet.Cells[RowCounter, 5] = drSag["NormF"].ToString();
                        objSheet.Cells[RowCounter, 6] = drSag["WindAndIceH"].ToString();
                        objSheet.Cells[RowCounter, 7] = drSag["WindAndIceF"].ToString();
                        objSheet.Cells[RowCounter, 8] = drSag["MinTempH"].ToString();
                        objSheet.Cells[RowCounter, 9] = drSag["MinTempF"].ToString();

                        objSheet.Cells[RowCounter, 10] = drSag["MaxTempH"].ToString();
                        objSheet.Cells[RowCounter, 11] = drSag["MaxTempF"].ToString();
                        objSheet.Cells[RowCounter, 12] = drSag["WindH"].ToString();
                        objSheet.Cells[RowCounter, 13] = drSag["WindF"].ToString();
                        objSheet.Cells[RowCounter, 14] = drSag["IceH"].ToString();
                        objSheet.Cells[RowCounter, 15] = drSag["IceF"].ToString();

                    }




                }

                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing,
Excel1.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);

                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
                #endregion
            }
        }

        public static void CreateExcelReportFinalElectrical(String FileName)
        {
            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\Electrical.xls";

            //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            File.Delete(FileName);

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[8];
            CreateExcelReportForCrossSection(objSheet);

            objSheet = (Excel1.Worksheet)objBook.Sheets[6];
            CreateExcelReportForDistributedlLoadBranch(objSheet);

            objSheet = (Excel1.Worksheet)objBook.Sheets[7];
            CreateExcelReportForDistributedLoadNode(objSheet);

            objSheet = (Excel1.Worksheet)objBook.Sheets[9];
            CreateExcelReportForTransformer(objSheet);

            objSheet = (Excel1.Worksheet)objBook.Sheets[10];
            CreateExcelReportForShortCircuit(objSheet);

            objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing,
Excel1.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);

            if (ExcelApp != null)
                ExcelApp.Quit();

        }

        public static void CreateExcelReportForCrossSection(Excel1.Worksheet objSheet)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            int RowCounter = 1;
            //System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Electrical.xls";
            //Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();


            //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            //FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            //object missing = System.Reflection.Missing.Value;
            //Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            //ExcelApp.Visible = true;
            //Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[8];

            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DesProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CCrossSection.AccessSelectAll();

            objSheet.Cells[RowCounter, 1] = DesProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = DesProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DesProfile.DesignDate).ToString(), p.GetMonth(DesProfile.DesignDate).ToString(), p.GetDayOfMonth(DesProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = DesProfile.Zone;
            objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
            RowCounter++;//2
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            RowCounter = 3;
            foreach (DataRow dr in dtSection.Rows)
            {
                objSheet.Cells[RowCounter, 1] = dr["From"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["To"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["ExistCond"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["Lenght"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["CommentCond"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["CrossSection"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["Current"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["Volt"].ToString();
                RowCounter++;
            }
        }

        public static void CreateExcelReportForDistributedlLoadBranch(Excel1.Worksheet objSheet)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            int RowCounter = 1;
            //System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Electrical.xls";
            //Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();


            //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            //FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            //object missing = System.Reflection.Missing.Value;
            //Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            //ExcelApp.Visible = true;
            //Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[6];

            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DesProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CDistributedLoadBranch.AccessSelectAll();

            objSheet.Cells[RowCounter, 1] = DesProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = DesProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DesProfile.DesignDate).ToString(), p.GetMonth(DesProfile.DesignDate).ToString(), p.GetDayOfMonth(DesProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = DesProfile.Zone;
            objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
            RowCounter++;//2
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            RowCounter = 3;
            foreach (DataRow dr in dtSection.Rows)
            {
                //ed.WriteMessage("CondName={0}\n", dr["CondName"].ToString());
                objSheet.Cells[RowCounter, 1] = dr["BranchName"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["Lenght"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["CondName"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["CondCurrent"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["CurrentAbs"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["CurrentArg"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["CondUtilization"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["TotalLoad"].ToString();
                objSheet.Cells[RowCounter, 9] = 0;
                RowCounter++;
            }
        }

        public static void CreateExcelReportForDistributedLoadNode(Excel1.Worksheet objSheet)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            int RowCounter = 1;
            //System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");
            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Electrical.xls";
            //Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();


            //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            //FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            //object missing = System.Reflection.Missing.Value;
            //Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            //ExcelApp.Visible = true;
            //Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[7];

            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DesProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CDistributedLoadNode.AccessSelectAll();

            objSheet.Cells[RowCounter, 1] = DesProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = DesProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DesProfile.DesignDate).ToString(), p.GetMonth(DesProfile.DesignDate).ToString(), p.GetDayOfMonth(DesProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = DesProfile.Zone;
            objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
            RowCounter++;//2
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            RowCounter = 3;
            foreach (DataRow dr in dtSection.Rows)
            {
                objSheet.Cells[RowCounter, 1] = dr["NodeName"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["VoltAbs"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["VoltArg"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["DropVolt"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["LoadPowerActive"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["LoadPower"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["LoadCurrentAbs"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["LoadCurrentArg"].ToString();
                RowCounter++;
            }
        }



        public static void CreateExcelReportForShortCircuit(Excel1.Worksheet objSheet)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            int RowCounter = 1;


            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DesProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CShortCircuit.AccessSelectAll();

            objSheet.Cells[RowCounter, 1] = DesProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = DesProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DesProfile.DesignDate).ToString(), p.GetMonth(DesProfile.DesignDate).ToString(), p.GetDayOfMonth(DesProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = DesProfile.Zone;
            objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;
            RowCounter++;//2
            objSheet.Cells[RowCounter, 1] = dtSection.Rows.Count;

            RowCounter = 3;
            foreach (DataRow dr in dtSection.Rows)
            {

                objSheet.Cells[RowCounter, 1] = dr["From"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["To"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["CondName"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["Lenght"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["CurrentAbs"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["CurrentArg"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["CondUtilization"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["TotalLoadReal"].ToString();
                objSheet.Cells[RowCounter, 9] = dr["SCCurrent"].ToString();
                objSheet.Cells[RowCounter, 10] = dr["CondMax1SCurrent"].ToString();
                objSheet.Cells[RowCounter, 11] = dr["Volt"].ToString();



                RowCounter++;
            }
        }


        public static void CreateExcelReportForTransformer(Excel1.Worksheet objSheet)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            int RowCounter = 1;
            //System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Electrical.xls";
            //Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();


            //File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            //FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            //object missing = System.Reflection.Missing.Value;
            //Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            //ExcelApp.Visible = true;
            //Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[9];

            Atend.Base.Design.DDesignProfile DesProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (DesProfile.Id == 0)
            {
                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                notification.Title = "اخطار";
                notification.Msg = "لطفا ابتدا مشخصات طرح را تکمیل نمایید";
                notification.infoCenterBalloon();
                return;
            }
            DataTable dtSection = Atend.Base.Calculating.CTransformer.AccessSelectAll();
            objSheet.Cells[RowCounter, 1] = DesProfile.DesignName; //Atend.Control.Common.DesignName;
            objSheet.Cells[RowCounter, 2] = DesProfile.Designer;
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DesProfile.DesignDate).ToString(), p.GetMonth(DesProfile.DesignDate).ToString(), p.GetDayOfMonth(DesProfile.DesignDate).ToString());
            objSheet.Cells[RowCounter, 3] = _date;
            objSheet.Cells[RowCounter, 4] = DesProfile.Zone;
            objSheet.Cells[RowCounter, 5] = Atend.Control.ConnectionString.LogoName;

            RowCounter++;//2
            if (dtSection.Rows.Count > 0)
            {
                objSheet.Cells[RowCounter, 1] = dtSection.Rows[0]["Load"].ToString();
                objSheet.Cells[RowCounter, 2] = dtSection.Rows[0]["Height"].ToString();
                objSheet.Cells[RowCounter, 3] = 0; //dtSection.Rows[0]["PowerLoss"].ToString();
                objSheet.Cells[RowCounter, 4] = dtSection.Rows[0]["Result"].ToString();
            }

            RowCounter++;// 3;
            DataTable dtTransCurrent = Atend.Base.Calculating.CTransCurrent.AccessSelectAll();
            DataTable dtTransPower = Atend.Base.Calculating.CTransPower.AccessSelectAll();
            DataTable dtTransBranch = Atend.Base.Calculating.CTransBranch.AccessSelectAll();
            int Count = 0;
            if (dtTransCurrent.Rows.Count >= dtTransPower.Rows.Count && dtTransCurrent.Rows.Count >= dtTransBranch.Rows.Count)
                Count = dtTransCurrent.Rows.Count;
            else if (dtTransPower.Rows.Count >= dtTransCurrent.Rows.Count && dtTransPower.Rows.Count >= dtTransBranch.Rows.Count)
                Count = dtTransPower.Rows.Count;
            else if (dtTransBranch.Rows.Count >= dtTransCurrent.Rows.Count && dtTransBranch.Rows.Count >= dtTransPower.Rows.Count)
                Count = dtTransBranch.Rows.Count;
            objSheet.Cells[RowCounter, 1] = Count;
            RowCounter++;//4
            for (int i = 0; i < Count; i++)
            {
                if (i < dtTransCurrent.Rows.Count)
                {
                    objSheet.Cells[RowCounter, 1] = dtTransCurrent.Rows[i]["I"].ToString();
                    objSheet.Cells[RowCounter, 2] = dtTransCurrent.Rows[i]["PF"].ToString();
                    objSheet.Cells[RowCounter, 3] = dtTransCurrent.Rows[i]["BranchCount"].ToString();
                    objSheet.Cells[RowCounter, 4] = dtTransCurrent.Rows[i]["CF"].ToString();
                }
                if (i < dtTransPower.Rows.Count)
                {
                    objSheet.Cells[RowCounter, 5] = dtTransPower.Rows[i]["P"].ToString();
                    objSheet.Cells[RowCounter, 6] = dtTransPower.Rows[i]["PF"].ToString();
                    objSheet.Cells[RowCounter, 7] = dtTransPower.Rows[i]["BranchCount"].ToString();
                    objSheet.Cells[RowCounter, 8] = dtTransPower.Rows[i]["CF"].ToString();
                }
                if (i < dtTransBranch.Rows.Count)
                {
                    objSheet.Cells[RowCounter, 9] = dtTransBranch.Rows[i]["BranchName"].ToString();
                    objSheet.Cells[RowCounter, 10] = dtTransBranch.Rows[i]["I"].ToString();
                    objSheet.Cells[RowCounter, 11] = dtTransBranch.Rows[i]["PF"].ToString();
                    objSheet.Cells[RowCounter, 12] = dtTransBranch.Rows[i]["BranchCount"].ToString();
                    objSheet.Cells[RowCounter, 13] = dtTransBranch.Rows[i]["PhaseCount"].ToString();
                    objSheet.Cells[RowCounter, 14] = dtTransBranch.Rows[i]["CF"].ToString();
                }
                RowCounter++;
            }
        }

        public static bool CreateExcelReaportForSagTension(string FileName, DataTable dataTable, DataRow drTitle)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 13;


            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);


            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\SagAndTensionReport.xls";


            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);

            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;  //SHOW EXCEL'S FILE
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets["Data"];

            objSheet.Cells[RowCounter, 1] = drTitle["ProjectName"].ToString();
            objSheet.Cells[RowCounter, 2] = drTitle["NumSection"].ToString();
            objSheet.Cells[RowCounter, 3] = drTitle["FirstPole"].ToString();
            objSheet.Cells[RowCounter, 4] = drTitle["LastPole"].ToString();
            objSheet.Cells[RowCounter, 5] = drTitle["UTS"].ToString();
            objSheet.Cells[RowCounter, 6] = drTitle["SE"].ToString();
            objSheet.Cells[RowCounter, 7] = drTitle["SpanCount"].ToString();
            objSheet.Cells[RowCounter, 8] = drTitle["SpanLenght"].ToString();
            objSheet.Cells[RowCounter, 9] = drTitle["CondName"].ToString();

            RowCounter = 6;
            foreach (DataRow dr in dataTable.Rows)
            {
                objSheet.Cells[RowCounter, 13] = dr["ConductorName"].ToString();
                objSheet.Cells[RowCounter, 12] = dr["NormH"].ToString();
                objSheet.Cells[RowCounter, 11] = dr["NormF"].ToString();
                objSheet.Cells[RowCounter, 10] = dr["IceH"].ToString();
                objSheet.Cells[RowCounter, 9] = dr["IceF"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["WindH"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["WindF"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["MaxTempH"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["MaxTempF"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["MinTempH"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["MinTempF"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["WindAndIceH"].ToString();
                objSheet.Cells[RowCounter, 1] = dr["WindAndIceF"].ToString();
                RowCounter++;
            }



            try
            {
                //objBook.SaveAs(FileName, Excel1.XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CreateExcelReaportForForce(string FileName, DataTable dataTable, DataRow drTitle)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 7;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            // FileName = Atend.Control.Common.DesignFullAddress + @"\ProductList\" + FileName;
            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\نیروهای وارد بر پایه.xls";


            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);

            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;  //SHOW EXCEL'S FILE
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets["Data"];

            objSheet.Cells[2, 1] = dataTable.Rows.Count;

            objSheet.Cells[RowCounter, 1] = drTitle["ProjectName"].ToString();
            objSheet.Cells[RowCounter, 2] = drTitle["NumSection"].ToString();
            objSheet.Cells[RowCounter, 3] = drTitle["FirstPole"].ToString();
            objSheet.Cells[RowCounter, 4] = drTitle["LastPole"].ToString();
            objSheet.Cells[RowCounter, 5] = drTitle["UTS"].ToString();
            objSheet.Cells[RowCounter, 6] = drTitle["SE"].ToString();
            objSheet.Cells[RowCounter, 7] = drTitle["SpanCount"].ToString();
            objSheet.Cells[RowCounter, 8] = drTitle["SpanLenght"].ToString();
            objSheet.Cells[RowCounter, 9] = drTitle["CondName"].ToString();

            RowCounter = 7;
            //ed.WriteMessage("dtPole.Rows.count={0}\n",dataTable.Rows.Count);
            foreach (DataRow dr in dataTable.Rows)
            {
                //ed.WriteMessage("dcPole={0}\n",dr["dcPole"].ToString());
                objSheet.Cells[RowCounter, 7] = dr["dcPole"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["dcNorm"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["dcIceHeavy"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["dcWindSpeed"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["dcMaxTemp"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["dcMinTemp"].ToString();
                objSheet.Cells[RowCounter, 1] = dr["dcWindIce"].ToString();
                RowCounter++;

            }



            try
            {
                //objBook.SaveAs(FileName, Excel1.XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CreateExcelReaportForConductorDay(string FileName, DataTable dataTable, DataRow drTitle)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 5;
            int i = 3;
            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            //FileName = Atend.Control.Common.DesignFullAddress + @"\ProductList\" + FileName;

            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");
            string path = Atend.Control.Common.fullPath + "\\ReportFile\\ConductorDayReport.xls";

            File.Copy(path, Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName, true);
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FileName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets["data"];


            objSheet.Cells[RowCounter, 1] = drTitle["ProjectName"].ToString();
            objSheet.Cells[RowCounter, 2] = drTitle["NumSection"].ToString();
            objSheet.Cells[RowCounter, 3] = drTitle["FirstPole"].ToString();
            objSheet.Cells[RowCounter, 4] = drTitle["LastPole"].ToString();
            objSheet.Cells[RowCounter, 5] = drTitle["UTS"].ToString();
            objSheet.Cells[RowCounter, 6] = drTitle["SE"].ToString();
            objSheet.Cells[RowCounter, 7] = drTitle["SpanCount"].ToString();
            objSheet.Cells[RowCounter, 8] = drTitle["SpanLenght"].ToString();
            objSheet.Cells[RowCounter, 9] = drTitle["CondName"].ToString();

            objSheet.Cells[2, 1] = dataTable.Rows.Count;
            double ColumnCount = (dataTable.Columns.Count - 4) / 2;
            objSheet.Cells[2, 2] = Math.Ceiling(ColumnCount);
            RowCounter = 5;
            i = 4;
            int k = 1;
            for (int j = 4; j < dataTable.Columns.Count; j = j + 2)
            {
                objSheet.Cells[RowCounter, k] = dataTable.Columns[i].ColumnName;
                i = i + 2;
                k++;
            }
            i = 6;
            //ed.WriteMessage("UUU\n");

            for (RowCounter = 0; RowCounter < dataTable.Rows.Count; RowCounter++)
            {
                int j = 1;
                for (ColumnCounter = 0; ColumnCounter < dataTable.Columns.Count; ColumnCounter++)
                {

                    objSheet.Cells[i, j] = dataTable.Rows[RowCounter][ColumnCounter].ToString();
                    j++;
                }
                i++;

            }



            try
            {
                //objBook.SaveAs(FileName, Excel1.XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CreateExcelReaportForStatus(string FileName, DataTable dataTable, int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 5;
            int i = 3;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            //ed.WriteMessage("FULL1:{0}\n", Atend.Control.Common.fullPath);
            //System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            //string fullPath = m.FullyQualifiedName;
            //fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            //ed.WriteMessage("FULL2:{0}\n",Atend.Control.Common.fullPath);

            string ExcellPath = Atend.Control.Common.fullPath + "\\ReportFile\\StatusReport.xls";
            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            System.IO.File.Delete(FileName);
            //System.IO.File.Copy(ExcellPath, FileName, true);


            //FileName =@"E:\a.xls" ;//Atend.Control.Common.DesignFullAddress + @"\" + FileName;

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;


            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(ExcellPath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets["data"];
            Excel1.Worksheet objSheetresultdata = (Excel1.Worksheet)objBook.Sheets["resultdata"];
            i = 4;
            //for (int j = 5; j < dataTable.Columns.Count; j = j + 2)
            //{
            //    objSheet.Cells[RowCounter, j] = dataTable.Columns[i].ColumnName;
            //    i = i + 2;


            //}
            i = 3;
            int k = 0, n = 0;
            int len = 0;
            DataTable ProjCodeTbl = Atend.Base.Base.BWorkOrder.SelectAll();  //Atend.Base.Base.BProjectCode.AccessSelectAll();
            //ed.WriteMessage("\n111\n");
            foreach (DataRow ProjCodeRow in ProjCodeTbl.Rows)
            {
                //ed.WriteMessage("\n222ProjectCode = {0} \n", ProjCodeRow["AdditionalCode"].ToString());

                DataRow[] Row = dataTable.Select("ProjectCode = '" + ProjCodeRow["ACode"].ToString() + "'");

                //ed.WriteMessage("\n333\n");

                if (Row.Length > 0)
                {

                    //ed.WriteMessage("\n444\n");

                    len = Row.Length;


                    int t = i - 1;
                    n = 0;
                    int j;
                    for (j = 0; j < Row.Length; j++)
                    {
                        //ed.WriteMessage("\n555\n");

                        if (Row[j]["Exist"].ToString() != "موجود - موجود")
                        {
                            //ed.WriteMessage("\n777\n");

                            objSheet.Cells[i, 1] = Row[j]["Price"].ToString();
                            //ed.WriteMessage("Price={0} \n", Row[j]["Price"].ToString());
                            objSheet.Cells[i, 2] = Row[j]["Count"].ToString();
                            //ed.WriteMessage("Count={0} \n", Row[j]["Count"].ToString());
                            objSheet.Cells[i, 3] = Row[j]["Unit"].ToString();
                            //ed.WriteMessage("Unit={0} \n", Row[j]["Unit"].ToString());
                            objSheet.Cells[i, 4] = Row[j]["Name"].ToString();
                            //ed.WriteMessage("Name={0} \n", Row[j]["Name"].ToString());
                            objSheet.Cells[i, 5] = Row[j]["ExecutePrice"].ToString();
                            //ed.WriteMessage("ExecutePrice={0} \n", Row[j]["ExecutePrice"].ToString());
                            objSheet.Cells[i, 6] = Row[j]["WagePrice"].ToString();
                            //ed.WriteMessage("WagePrice={0} \n", Row[j]["WagePrice"].ToString());
                            objSheet.Cells[i, 7] = Row[j]["Code"].ToString();
                            //ed.WriteMessage("Code={0} \n", Row[j]["Code"].ToString());
                            objSheet.Cells[i, 8] = Row[j]["Exist"].ToString();
                            //ed.WriteMessage("Exist={0} \n", Row[j]["Exist"].ToString());
                            objSheet.Cells[i, 9] = Row[j]["ProjectCode"].ToString();
                            //ed.WriteMessage("ProjectCode={0} \n", Row[j]["ProjectCode"].ToString());
                            i++;
                            n++;
                        }
                    }

                    if (n > 0)
                    {
                        k++;
                        objSheet.Cells[t, 2] = Row[0]["ProjectCode"].ToString();
                        objSheet.Cells[t, 3] = n;
                        objSheet.Cells[t, 4] = Row[0]["ProjectName1"].ToString();
                        i++;
                    }

                    //objSheet.Cells[i - 1, 3] = n;
                }
            }

            objSheet.Cells[1, 1] = k;

            Atend.Base.Design.DDesignProfile DProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            objSheetresultdata.Cells[1, 1] = DProfile.DesignId.ToString();

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            string _date = string.Format("{0}/{1}/{2}", p.GetYear(DProfile.DesignDate).ToString(), p.GetMonth(DProfile.DesignDate).ToString(), p.GetDayOfMonth(DProfile.DesignDate).ToString());
            objSheetresultdata.Cells[1, 2] = _date;
            objSheetresultdata.Cells[1, 3] = DProfile.Zone.ToString();
            objSheetresultdata.Cells[1, 4] = DProfile.DesignId.ToString();
            objSheetresultdata.Cells[1, 5] = DProfile.DesignName;
            objSheetresultdata.Cells[1, 6] = DProfile.Employer;
            objSheetresultdata.Cells[1, 7] = DProfile.Address;
            objSheetresultdata.Cells[1, 10] = DProfile.Adviser;
            objSheetresultdata.Cells[1, 11] = DProfile.Supporter;
            objSheetresultdata.Cells[1, 12] = DProfile.Approval;
            objSheetresultdata.Cells[1, 13] = DProfile.Planner;
            objSheetresultdata.Cells[1, 14] = "1.48";
            objSheetresultdata.Cells[1, 15] = DProfile.Designer;

            //for (RowCounter = 0; RowCounter < dataTable.Rows.Count; RowCounter++)
            //{
            //    int j = 1;
            //    for (ColumnCounter = 0; ColumnCounter < dataTable.Columns.Count; ColumnCounter++)
            //    {
            //        objSheet.Cells[i, j] = dataTable.Rows[RowCounter][ColumnCounter].ToString();
            //        j++;
            //    }
            //    i++;
            //}




            try
            {



                //objBook.SaveAs(FileName, Excel1.XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //objBook.Close(false, missing, missing);
                //if (ExcelApp != null)
                //    ExcelApp.Quit();


                //oWB1.SaveAs(“c:\\First.xls”, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);


                //ed.WriteMessage("~~~~~~~~~~~~~~~~~{0}~~~~~~~~~~~~~~~~~~~\n", FileName);
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);

            }
            catch
            {
                return false;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    //ed.WriteMessage("\nQUIIIIIIIIIIIIIIIIT\n");
                    ExcelApp.Quit();
                }
            }

            return true;
        }

        public static bool CreateExcelReaportForGISForm1(string FileName, DataTable dataTable)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 5;
            int i = 3;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            //ed.WriteMessage("FULL1:{0}\n", Atend.Control.Common.fullPath);
            //System.Reflection.Module m = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0];
            //string fullPath = m.FullyQualifiedName;
            //fullPath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
            //ed.WriteMessage("FULL2:{0}\n",Atend.Control.Common.fullPath);

            string ExcellPath = Atend.Control.Common.fullPath + "\\ReportFile\\GISForm1.xls";
            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\Result\");
            FileName = Atend.Control.Common.DesignFullAddress + @"\Result\" + FileName;
            System.IO.File.Delete(FileName);
            //System.IO.File.Copy(ExcellPath, FileName, true);


            //FileName =@"E:\a.xls" ;//Atend.Control.Common.DesignFullAddress + @"\" + FileName;

            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;


            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(ExcellPath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets["data"];
            i = 4;
            //for (int j = 5; j < dataTable.Columns.Count; j = j + 2)
            //{
            //    objSheet.Cells[RowCounter, j] = dataTable.Columns[i].ColumnName;
            //    i = i + 2;


            //}
            i = 3;
            int k = 0, n = 0;
            int len = 0;
            //Atend.Base.Base.BProjectCode.AccessSelectAll();

            objSheet.Cells[RowCounter, 1] = dataTable.Rows.Count;

            RowCounter = 2;
            foreach (DataRow dr in dataTable.Rows)
            {
                objSheet.Cells[RowCounter, 1] = dr["PoleNum"].ToString();
                objSheet.Cells[RowCounter, 2] = dr["X"].ToString();
                objSheet.Cells[RowCounter, 3] = dr["Y"].ToString();
                objSheet.Cells[RowCounter, 4] = dr["High"].ToString();
                objSheet.Cells[RowCounter, 5] = dr["Tension"].ToString();
                objSheet.Cells[RowCounter, 6] = dr["Kind"].ToString();
                objSheet.Cells[RowCounter, 7] = dr["PoleType"].ToString();
                objSheet.Cells[RowCounter, 8] = dr["OrderType"].ToString();
                objSheet.Cells[RowCounter, 9] = dr["InsulatorCount"].ToString();
                objSheet.Cells[RowCounter, 10] = dr["CircuitCount"].ToString();
                objSheet.Cells[RowCounter, 11] = dr["CrossSection"].ToString();
                objSheet.Cells[RowCounter, 12] = dr["BranchType"].ToString();
                objSheet.Cells[RowCounter, 13] = dr["ProductType"].ToString();
                objSheet.Cells[RowCounter, 14] = dr["Constructor"].ToString();
                objSheet.Cells[RowCounter, 15] = dr["I"].ToString();

                RowCounter++;
            }

            try
            {



                //objBook.SaveAs(FileName, Excel1.XlFileFormat.xlCSV, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //objBook.Close(false, missing, missing);
                //if (ExcelApp != null)
                //    ExcelApp.Quit();


                //oWB1.SaveAs(“c:\\First.xls”, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);


                //ed.WriteMessage("~~~~~~~~~~~~~~~~~{0}~~~~~~~~~~~~~~~~~~~\n", FileName);
                objBook.SaveAs(FileName, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                objBook.Close(false, missing, missing);

            }
            catch
            {
                return false;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    //ed.WriteMessage("\nQUIIIIIIIIIIIIIIIIT\n");
                    ExcelApp.Quit();
                }
            }

            return true;
        }

        public static bool CreateReport(string FilePath, DataTable dataTable, Dictionary<string, string> HeaderName, string NameOfSheet, int Code)
        {


            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int RowCounter = 1;
            int ColumnCounter = 1;


            object missing = System.Reflection.Missing.Value;
            Excel1.Application ExcelApp = new Excel1.ApplicationClass();
            ExcelApp.Visible = false;  //SHOW EXCEL'S FILE
            //Excel1.Workbook objBook = ExcelApp.Workbooks.Add(missing);
            //ed.WriteMessage("GoToOpen WBook\n");
            Excel1.Workbook objBook = ExcelApp.Workbooks.Open(FilePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //ed.WriteMessage("Open\n");
            Excel1.Worksheet objSheet = (Excel1.Worksheet)objBook.Sheets[NameOfSheet];
            //ed.WriteMessage("Go To Foreach\n");

            foreach (DataColumn dc in dataTable.Columns)
            {
                if (HeaderName.ContainsKey(dc.ColumnName))  //name
                {
                    string value;
                    HeaderName.TryGetValue(dc.ColumnName, out value);
                    //string value = HeaderName.Values.ToString();
                    RowCounter = 1;
                    objSheet.Cells[RowCounter, ColumnCounter] = value;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //******************************************
                        RowCounter++;
                        objSheet.Cells[RowCounter, ColumnCounter] = dr[dc.ColumnName].ToString();

                    }
                    ColumnCounter++;
                }

            }

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(Code);
            //FileName = report.Value1 + FileName;


            try
            {
                ExcelApp.DisplayAlerts = false;

                objBook.SaveAs(FilePath, Excel1.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Excel1.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //objBook.SaveAs(FilePath, Excel1.XlFileFormat.xlExcel9795, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Excel1.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                objBook.Close(false, missing, missing);
                if (ExcelApp != null)
                    ExcelApp.Quit();
                //objSheet = (Excel1.Worksheet)objBook.Sheets["Data1"];
                //objSheet.Activate();// = (Excel1.Worksheet)objBook.ActiveSheet;

                //objBook.Save();

                //ExcelApp.Workbooks.Close();


            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error Is :{0}\n", ex.Message);
                return false;
            }

            return true;

        }

        //#region public static CreateExcelStatusReaport
        public DataTable CreateExcelStatus()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            #region Create GroupDT as return value
            DataTable GroupDT = new DataTable();

            DataColumn Col00 = new DataColumn("Code");
            DataColumn Col11 = new DataColumn("Name");
            DataColumn Col22 = new DataColumn("Count");
            DataColumn Col33 = new DataColumn("Unit");
            DataColumn Col44 = new DataColumn("IsProduct");
            DataColumn Col55 = new DataColumn("Price");
            DataColumn Col66 = new DataColumn("ExecutePrice");
            DataColumn Col77 = new DataColumn("WagePrice");
            DataColumn Col88 = new DataColumn("Exist");
            DataColumn Col99 = new DataColumn("ProjectCode");
            DataColumn Col1010 = new DataColumn("Type");
            DataColumn Col910 = new DataColumn("ProjectName1");

            GroupDT.Columns.Add(Col00);
            GroupDT.Columns.Add(Col11);
            GroupDT.Columns.Add(Col22);
            GroupDT.Columns.Add(Col33);
            GroupDT.Columns.Add(Col44);
            GroupDT.Columns.Add(Col55);
            GroupDT.Columns.Add(Col66);
            GroupDT.Columns.Add(Col77);
            GroupDT.Columns.Add(Col88);
            GroupDT.Columns.Add(Col99);
            GroupDT.Columns.Add(Col1010);
            GroupDT.Columns.Add(Col910);
            #endregion

            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();
            _ConnectionString.OpenSingleSqlConnectionLocal();
            if (_ConnectionString.SingleConnectionAccess != null && _ConnectionString.SingleSqlConnectionLocal != null)
            {

                #region Create DT as global value

                DataColumn Col0 = new DataColumn("Code");
                DataColumn Col1 = new DataColumn("Name");
                DataColumn Col2 = new DataColumn("Count");
                DataColumn Col3 = new DataColumn("Unit");
                DataColumn Col4 = new DataColumn("IsProduct");
                DataColumn Col5 = new DataColumn("Price");
                DataColumn Col6 = new DataColumn("ExecutePrice");
                DataColumn Col7 = new DataColumn("WagePrice");
                DataColumn Col8 = new DataColumn("Exist");
                DataColumn Col9 = new DataColumn("ProjectCode");
                DataColumn Col10 = new DataColumn("Type");
                DataColumn Col9100 = new DataColumn("ProjectName1");


                DT.Columns.Add(Col0);
                DT.Columns.Add(Col1);
                DT.Columns.Add(Col2);
                DT.Columns.Add(Col3);
                DT.Columns.Add(Col4);
                DT.Columns.Add(Col5);
                DT.Columns.Add(Col6);
                DT.Columns.Add(Col7);
                DT.Columns.Add(Col8);
                DT.Columns.Add(Col9);
                DT.Columns.Add(Col10);
                DT.Columns.Add(Col9100);

                #endregion

                //ed.WriteMessage("\n");
                try
                {
                    DT.Rows.Clear();
                    ed.WriteMessage("XXXXXX 0\n");
                    #region All Nodes
                    ed.WriteMessage("\n~~~~~~~~~~Start D node~~~~~~~~~~~\n {0}\n", _ConnectionString.SingleConnectionAccess);
                    DataTable NodeTable = Atend.Base.Design.DNode.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    ed.WriteMessage("count:{0}\n", NodeTable.Rows.Count);
                    for (int i = 0; i < NodeTable.Rows.Count; i++)
                    {
                        Guid NodeID = new Guid(NodeTable.Rows[i]["Code"].ToString());
                        Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeID, _ConnectionString.SingleConnectionAccess);
                        if (Package.Code != Guid.Empty && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)) && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)) && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip)))
                        {
                            ed.WriteMessage("XXXXXX 0.1\n");
                            DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                            ed.WriteMessage("XXXXXX 0.2\n");
                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;
                                //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                PID = 0;
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                                //ed.WriteMessage("Name:{0}\n", dr[1]);
                                ed.WriteMessage("XXXXXX 0.3\n");
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                ed.WriteMessage("XXXXXX 0.4\n");
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                }
                                ed.WriteMessage("XXXXXX 0.5\n");
                                DT.Rows.Add(dr);
                                SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                ed.WriteMessage("XXXXXX 0.6\n");
                            }
                            else
                            {
                                //موجود موجود در لیست نمی آید
                            }
                            ChildFind(PackageTable);//in D_Package
                        }

                        if (Package.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))
                        {
                            //DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code);
                            ed.WriteMessage("XXXXXX 0.7\n");
                            Atend.Base.Equipment.EPoleTip PT = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Package.ProductCode, _ConnectionString.SingleConnectionAccess);
                            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.AccessSelectByCode(PT.PoleCode, _ConnectionString.SingleConnectionAccess);
                            ed.WriteMessage("XXXXXX 0.8\n");
                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;
                                //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                PID = 0;
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                dr[1] = Pole.Name; //FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode);
                                //ed.WriteMessage("\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$Name:{0}\n", dr[1]);
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(Pole.ProductCode, _ConnectionString.SingleSqlConnectionLocal);
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                }
                                DT.Rows.Add(dr);
                                ed.WriteMessage("XXXXXX 0.8.1\n");
                                SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                ed.WriteMessage("XXXXXX 0.8.2\n");
                                SubEquipment(Pole.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Pole), Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Guid.Empty);//in E_ContainerPackage

                                ed.WriteMessage("XXXXXX 0.9\n");
                                DataTable PackTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Package.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Halter), _ConnectionString.SingleConnectionAccess);
                                ed.WriteMessage("XXXXXX 0.10 {0}\n",PackTbl.Rows.Count);
                                foreach (DataRow PackRow in PackTbl.Rows)
                                {
                                    dr = DT.NewRow();
                                    dr[8] = PackRow["IsExistance"].ToString(); ;
                                    //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                    ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                    dr[9] = PackRow["ProjectCode"].ToString();
                                    //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                    dr[2] = PackRow["Count"].ToString(); ;//Childs.Rows[i]["Count"];
                                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                                    ProCode = Convert.ToInt32(PackRow["ProductCode"].ToString());//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                    PID = 0;
                                    dr[10] = PackRow["Type"].ToString();
                                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                                    dr[1] = FindNameAndProductCode(Convert.ToInt32(PackRow["Type"].ToString())/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                                    Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                    if (Product.Code != -1)
                                    {
                                        dr[0] = Product.Code;
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = Product.Unit;
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        if (Product.IsProduct)
                                            dr[4] = 1;
                                        else
                                            dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = Product.Price;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = Product.ExecutePrice;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = Product.WagePrice;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                    }
                                    else
                                    {
                                        dr[0] = "----";
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = "----";
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = 0;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = 0;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = 0;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                    }
                                    DT.Rows.Add(dr);

                                    SubEquipment(Convert.ToInt32(PackRow["ProductCode"].ToString()), Convert.ToInt32(PackRow["Type"].ToString()), Convert.ToInt32(PackRow["ProjectCode"].ToString()), Convert.ToInt32(PackRow["IsExistance"].ToString()), Convert.ToInt32(dr[2]), new Guid(PackRow["Code"].ToString()));//in E_ContainerPackage
                                    
                                }
                                ed.WriteMessage("XXXXXX 0.11\n");
                            }
                            else
                            {
                                //موجود موجود در لیست نمی آید
                            }
                            //ChildFind(PackageTable);//in D_Package
                        }
                        ed.WriteMessage("-----------------> {0}\n", i);
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Node~~~~~~~~~~~\n");
                    #endregion

                    ed.WriteMessage("XXXXXX 1\n");
                    #region All Dpackage
                    //ed.WriteMessage("\n~~~~~~~~~~START NULL D Node~~~~~~~~~~~\n");
                    DataTable AllPack = Atend.Base.Design.DPackage.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    //ed.WriteMessage(" >> PackageTableRowCount = {0}\n", AllPack.Rows.Count);
                    foreach (DataRow adr in AllPack.Rows)
                    {
                        //ed.WriteMessage("Code:{0} ParentCode:{1} \n",adr["Code"],adr["ParentCode"]);
                        if (new Guid(adr["NodeCode"].ToString()) == Guid.Empty && new Guid(adr["ParentCode"].ToString()) == Guid.Empty)
                        {
                            #region Node Guid Empty Parent Guid Empty

                            Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(adr["Code"].ToString()), _ConnectionString.SingleConnectionAccess);
                            if (Package.Code != Guid.Empty)
                            {
                                DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                                if (Package.IsExistance != 0)
                                {
                                    DataRow dr = DT.NewRow();
                                    dr[8] = Package.IsExistance;
                                    //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                    ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                    dr[9] = Package.ProjectCode;
                                    //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                    dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                                    int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                    PID = 0;
                                    dr[10] = Package.Type;
                                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                                    dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                                    Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                    if (Product.Code != -1)
                                    {
                                        dr[0] = Product.Code;
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = Product.Unit;
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        if (Product.IsProduct)
                                            dr[4] = 1;
                                        else
                                            dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = Product.Price;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = Product.ExecutePrice;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = Product.WagePrice;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                    }
                                    else
                                    {
                                        dr[0] = "----";
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = "----";
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = 0;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = 0;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = 0;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                    }
                                    DT.Rows.Add(dr);
                                    SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                }
                                else
                                {
                                    //موجود موجود در لیست نمی آید
                                }
                                ChildFind(PackageTable);//in D_Package
                            }
                            #endregion
                        }
                        else if (new Guid(adr["NodeCode"].ToString()) == Guid.Empty && new Guid(adr["ParentCode"].ToString()) != Guid.Empty)
                        {
                            #region Node Guid Empty Parent Not Guid Empty

                            Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(adr["Code"].ToString()), _ConnectionString.SingleConnectionAccess);
                            if (Package.Code != Guid.Empty)
                            {
                                DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                                if (Package.IsExistance != 0)
                                {
                                    DataRow dr = DT.NewRow();
                                    dr[8] = Package.IsExistance;
                                    //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                    ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                    dr[9] = Package.ProjectCode;
                                    //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                    dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                                    int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                    PID = 0;
                                    dr[10] = Package.Type;
                                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                                    dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                                    Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                    if (Product.Code != -1)
                                    {
                                        dr[0] = Product.Code;
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = Product.Unit;
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        if (Product.IsProduct)
                                            dr[4] = 1;
                                        else
                                            dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = Product.Price;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = Product.ExecutePrice;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = Product.WagePrice;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                    }
                                    else
                                    {
                                        dr[0] = "----";
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = "----";
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = 0;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = 0;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = 0;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                    }
                                    DT.Rows.Add(dr);
                                    SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                }
                                else
                                {
                                    //موجود موجود در لیست نمی آید
                                }
                                ChildFind(PackageTable);//in D_Package
                            }
                            #endregion

                        }
                    }


                    //ed.WriteMessage("\n~~~~~~~~~~End NULL D Node~~~~~~~~~~~\n");
                    #endregion

                    ed.WriteMessage("XXXXXX 2\n");
                    #region All Post
                    //ed.WriteMessage("\n~~~~~~~~~~Start D Post~~~~~~~~~~~\n");
                    DataTable PostTable = Atend.Base.Design.DPost.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    ed.WriteMessage(" >> PostTableRowCount = {0}\n", PostTable.Rows.Count);
                    for (int i = 0; i < PostTable.Rows.Count; i++)
                    {

                        Guid NodeID = new Guid(PostTable.Rows[i]["Code"].ToString());
                        Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeID, _ConnectionString.SingleConnectionAccess);
                        if (Package.Code != Guid.Empty)
                        {
                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                                /////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;// pc;
                                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                PID = 0;
                                dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                                //ed.WriteMessage("Name:{0}\n", dr[1]);
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                                }
                                DT.Rows.Add(dr);
                            }
                            //DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code);
                            SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, 1, Package.Code);
                            //ed.WriteMessage("\n~~~~~~~~~~End of Sub Equipment~~~~~~~~~~~\n");
                            //ChildFind(PackageTable);
                        }
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Post~~~~~~~~~~~\n");
                    #endregion

                    ed.WriteMessage("XXXXXX 3\n");
                    #region All Branches
                    //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");
                    DataTable BranchTable = Atend.Base.Design.DBranch.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    //ed.WriteMessage(" >> BranchTableRowCount = {0}\n", BranchTable.Rows.Count);
                    foreach (DataRow BranchRow in BranchTable.Rows)
                    {
                        if (Convert.ToInt16(BranchRow["ProductType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor))
                            ConductorTip(BranchRow);

                        if (Convert.ToInt16(BranchRow["ProductType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper))
                            SelfKeeperTip(BranchRow);

                        if (Convert.ToInt16(BranchRow["ProductType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel))
                            GroundCabelTip(BranchRow);

                        ////////////if (Convert.ToInt16(BranchRow["ProductType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Jumper))
                        ////////////    SelfKeeperTip(BranchRow);
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Branch~~~~~~~~~~~\n");
                    #endregion
                    ed.WriteMessage("XXXXXX 4\n");

                    //ed.WriteMessage("Start Final Operation \n");
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        int Exist = -1;
                        double Count = 0, P = 0;
                        DataRow[] row = GroupDT.Select("Code = '" + DT.Rows[i]["Code"].ToString() + "' And Type = '" + DT.Rows[i]["Type"].ToString() + "'" /*+ " And Name = '" + DT.Rows[i]["Name"].ToString() + "'"*/);
                        Atend.Control.Enum.ProductType Type = (Atend.Control.Enum.ProductType)Convert.ToInt32(DT.Rows[i]["Type"].ToString());
                        Atend.Base.Base.BSetting CurrentSetting = Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.ShowProduct), _ConnectionString.SingleSqlConnectionLocal);
                        if ((row.Length == 0))
                        {
                            if ((Type != Atend.Control.Enum.ProductType.Cell) && (Type != Atend.Control.Enum.ProductType.GroundPost) && (Type != Atend.Control.Enum.ProductType.AirPost))
                            {
                                if (DT.Rows[i]["Code"].ToString() != "----")
                                {
                                    Exist = Convert.ToInt32(DT.Rows[i]["Exist"].ToString());
                                    Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                    DataRow GroupDR = GroupDT.NewRow();
                                    GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                    GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                    GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                    GroupDR["Count"] = Count;
                                    P = 1;
                                    switch (Exist)
                                    {
                                        case 0:

                                            P = 0;
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                            break;
                                        case 1:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;

                                            break;
                                        case 2:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;
                                            break;
                                        case 3:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;

                                            break;
                                        case 4:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 1;
                                            }
                                            else if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 100 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 199)
                                            {
                                                P = 1;
                                            }
                                            else if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 500 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 599)
                                            {
                                                P = 1;
                                            }

                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                            break;
                                        case 5:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 500 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 599)
                                            {
                                                P = 1;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;
                                            break;
                                        default:
                                            GroupDR["ExecutePrice"] = 0;
                                            GroupDR["WagePrice"] = 0;
                                            GroupDR["Price"] = 0;
                                            break;
                                    }
                                    GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                    GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                    GroupDR["Exist"] = Exist;
                                    GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                    if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                    {
                                        Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                        GroupDR["ProjectName1"] = BW.Name;
                                    }
                                    else
                                    {
                                        GroupDR["ProjectName1"] = string.Empty;
                                    }
                                    GroupDT.Rows.Add(GroupDR);
                                }
                                else if (Convert.ToBoolean(CurrentSetting.Value))
                                {
                                    Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                    DataRow GroupDR = GroupDT.NewRow();
                                    GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                    GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                    GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                    GroupDR["Count"] = Count;
                                    GroupDR["Price"] = 0;
                                    GroupDR["ExecutePrice"] = 0;
                                    GroupDR["WagePrice"] = 0;
                                    GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                    GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                    GroupDR["Exist"] = DT.Rows[i]["Exist"].ToString();
                                    GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                    if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                    {
                                        Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                        GroupDR["ProjectName1"] = BW.Name;
                                    }
                                    else
                                    {
                                        GroupDR["ProjectName1"] = string.Empty;
                                    }
                                    GroupDT.Rows.Add(GroupDR);
                                }
                            }
                        }
                        else
                        {
                            bool SameStatusWasFound = false;
                            for (int ii = 0; ii < row.Length; ii++)
                            {
                                if ((Convert.ToInt32(row[ii]["Exist"]) == Convert.ToInt32(DT.Rows[i]["Exist"])) && Convert.ToInt32(row[ii]["ProjectCode"]) == Convert.ToInt32(DT.Rows[i]["ProjectCode"]) && row[ii]["Name"].ToString() == DT.Rows[i]["Name"].ToString())
                                {
                                    row[ii]["Count"] = Convert.ToDouble(row[ii]["Count"]) + Convert.ToDouble(DT.Rows[i]["Count"]);
                                    SameStatusWasFound = true;
                                }
                            }
                            if (!SameStatusWasFound)
                            {
                                if ((Type != Atend.Control.Enum.ProductType.Cell) && (Type != Atend.Control.Enum.ProductType.GroundPost) && (Type != Atend.Control.Enum.ProductType.AirPost))
                                {
                                    if (DT.Rows[i]["Code"].ToString() != "----")
                                    {
                                        Exist = Convert.ToInt32(DT.Rows[i]["Exist"].ToString());
                                        Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                        DataRow GroupDR = GroupDT.NewRow();
                                        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                        GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                        GroupDR["Count"] = Count;
                                        P = 0;
                                        switch (Exist)
                                        {
                                            case 0:
                                                P = 0;
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                                break;
                                            case 1:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;

                                                break;
                                            case 2:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                            case 3:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                            case 4:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 1;
                                                }
                                                else if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 100 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 199)
                                                {
                                                    P = 1;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;
                                                break;
                                            case 5:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 500 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 599)
                                                {
                                                    P = 1;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                        }
                                        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                        GroupDR["Exist"] = Exist;
                                        GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                        if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                        {
                                            Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                            GroupDR["ProjectName1"] = BW.Name;
                                        }
                                        else
                                        {
                                            GroupDR["ProjectName1"] = string.Empty;
                                        }

                                        GroupDT.Rows.Add(GroupDR);
                                    }
                                    else
                                    {
                                        Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                        DataRow GroupDR = GroupDT.NewRow();
                                        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                        GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                        GroupDR["Count"] = Count;
                                        GroupDR["Price"] = 0;
                                        GroupDR["ExecutePrice"] = 0;
                                        GroupDR["WagePrice"] = 0;
                                        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                        GroupDR["Exist"] = DT.Rows[i]["Exist"].ToString();
                                        GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();

                                        if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                        {
                                            Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                            GroupDR["ProjectName1"] = BW.Name;
                                        }
                                        else
                                        {
                                            GroupDR["ProjectName1"] = string.Empty;
                                        }

                                        GroupDT.Rows.Add(GroupDR);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error CreateExcelStatus: {0} \n", ex.Message);
                }
            }



            //ed.WriteMessage("Access :{0}\nlocal:{1}\n", _ConnectionString.SingleConnectionAccess.State, _ConnectionString.SingleSqlConnectionLocal.State);
            _ConnectionString.CloseSingleConnectionAccess();
            //_ConnectionString.CloseSingleSqlConnectionLocal();
            
            return GroupDT;
        }

        public DataTable CreateExcelStatus(DataTable IDTbl)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            #region Create GroupDT as return value
            DataTable GroupDT = new DataTable();

            DataColumn Col00 = new DataColumn("Code");
            DataColumn Col11 = new DataColumn("Name");
            DataColumn Col22 = new DataColumn("Count");
            DataColumn Col33 = new DataColumn("Unit");
            DataColumn Col44 = new DataColumn("IsProduct");
            DataColumn Col55 = new DataColumn("Price");
            DataColumn Col66 = new DataColumn("ExecutePrice");
            DataColumn Col77 = new DataColumn("WagePrice");
            DataColumn Col88 = new DataColumn("Exist");
            DataColumn Col99 = new DataColumn("ProjectCode");
            DataColumn Col1010 = new DataColumn("Type");
            DataColumn Col910 = new DataColumn("ProjectName1");

            GroupDT.Columns.Add(Col00);
            GroupDT.Columns.Add(Col11);
            GroupDT.Columns.Add(Col22);
            GroupDT.Columns.Add(Col33);
            GroupDT.Columns.Add(Col44);
            GroupDT.Columns.Add(Col55);
            GroupDT.Columns.Add(Col66);
            GroupDT.Columns.Add(Col77);
            GroupDT.Columns.Add(Col88);
            GroupDT.Columns.Add(Col99);
            GroupDT.Columns.Add(Col1010);
            GroupDT.Columns.Add(Col910);
            #endregion

            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();
            _ConnectionString.OpenSingleSqlConnectionLocal();
            if (_ConnectionString.SingleConnectionAccess != null && _ConnectionString.SingleSqlConnectionLocal != null)
            {

                #region Create DT as global value

                DataColumn Col0 = new DataColumn("Code");
                DataColumn Col1 = new DataColumn("Name");
                DataColumn Col2 = new DataColumn("Count");
                DataColumn Col3 = new DataColumn("Unit");
                DataColumn Col4 = new DataColumn("IsProduct");
                DataColumn Col5 = new DataColumn("Price");
                DataColumn Col6 = new DataColumn("ExecutePrice");
                DataColumn Col7 = new DataColumn("WagePrice");
                DataColumn Col8 = new DataColumn("Exist");
                DataColumn Col9 = new DataColumn("ProjectCode");
                DataColumn Col10 = new DataColumn("Type");
                DataColumn Col9100 = new DataColumn("ProjectName1");


                DT.Columns.Add(Col0);
                DT.Columns.Add(Col1);
                DT.Columns.Add(Col2);
                DT.Columns.Add(Col3);
                DT.Columns.Add(Col4);
                DT.Columns.Add(Col5);
                DT.Columns.Add(Col6);
                DT.Columns.Add(Col7);
                DT.Columns.Add(Col8);
                DT.Columns.Add(Col9);
                DT.Columns.Add(Col10);
                DT.Columns.Add(Col9100);

                #endregion

                //ed.WriteMessage("\n");
                try
                {
                    DT.Rows.Clear();

                    #region All Nodes
                    //ed.WriteMessage("\n~~~~~~~~~~Start D node~~~~~~~~~~~\n");
                    DataRow[] NodeRow = IDTbl.Select(string.Format("Type = {0} OR Type = {1}", Convert.ToInt16(Atend.Control.Enum.ProductType.Pole), Convert.ToInt16(Atend.Control.Enum.ProductType.PoleTip)));//Atend.Base.Design.DNode.AccessSelectAll(_ConnectionString.SingleConnectionAccess);

                    //ed.WriteMessage(" >> NodeTableRowCount = {0}\n", NodeTable.Rows.Count);
                    //ed.WriteMessage("\n1111111\n");
                    for (int i = 0; i < NodeRow.Length; i++)
                    {
                        Guid NodeID = new Guid(NodeRow[i]["ID"].ToString());
                        Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeID, _ConnectionString.SingleConnectionAccess);
                        if (Package.Code != Guid.Empty && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)) && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)) && (Package.Type != Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip)))
                        {
                            DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;
                                //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                PID = 0;
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode, _ConnectionString.SingleConnectionAccess);
                                //ed.WriteMessage("Name:{0}\n", dr[1]);
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                }
                                DT.Rows.Add(dr);
                                SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                            }
                            else
                            {
                                //موجود موجود در لیست نمی آید
                            }
                            ChildFind(PackageTable);//in D_Package
                        }

                        if (Package.Type == Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))
                        {
                            //DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code);
                            Atend.Base.Equipment.EPoleTip PT = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(Package.ProductCode, _ConnectionString.SingleConnectionAccess);
                            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.AccessSelectByCode(PT.PoleCode, _ConnectionString.SingleConnectionAccess);

                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;
                                //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                PID = 0;
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                dr[1] = Pole.Name; //FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode);
                                //ed.WriteMessage("\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$Name:{0}\n", dr[1]);
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(Pole.ProductCode, _ConnectionString.SingleSqlConnectionLocal);
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                }
                                DT.Rows.Add(dr);
                                SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                SubEquipment(Pole.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Pole), Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Guid.Empty);//in E_ContainerPackage


                                DataTable PackTbl = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Package.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Halter), _ConnectionString.SingleConnectionAccess);

                                foreach (DataRow PackRow in PackTbl.Rows)
                                {
                                    dr = DT.NewRow();
                                    dr[8] = PackRow["IsExistance"].ToString(); ;
                                    //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                    ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                    dr[9] = PackRow["ProjectCode"].ToString();
                                    //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                    dr[2] = PackRow["Count"].ToString(); ;//Childs.Rows[i]["Count"];
                                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                                    ProCode = Convert.ToInt32(PackRow["ProductCode"].ToString());//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                    PID = 0;
                                    dr[10] = PackRow["Type"].ToString();
                                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                                    dr[1] = FindNameAndProductCode(Convert.ToInt32(PackRow["Type"].ToString())/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode, _ConnectionString.SingleConnectionAccess);
                                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                                    Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                    if (Product.Code != -1)
                                    {
                                        dr[0] = Product.Code;
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = Product.Unit;
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        if (Product.IsProduct)
                                            dr[4] = 1;
                                        else
                                            dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = Product.Price;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = Product.ExecutePrice;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = Product.WagePrice;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                    }
                                    else
                                    {
                                        dr[0] = "----";
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = "----";
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = 0;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = 0;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = 0;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                    }
                                    DT.Rows.Add(dr);

                                    SubEquipment(Convert.ToInt32(PackRow["ProductCode"].ToString()), Convert.ToInt32(PackRow["Type"].ToString()), Convert.ToInt32(PackRow["ProjectCode"].ToString()), Convert.ToInt32(PackRow["IsExistance"].ToString()), Convert.ToInt32(dr[2]), new Guid(PackRow["Code"].ToString()));//in E_ContainerPackage
                                }
                            }
                            else
                            {
                                //موجود موجود در لیست نمی آید
                            }
                            //ChildFind(PackageTable);//in D_Package
                        }

                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Node~~~~~~~~~~~\n");
                    #endregion

                    #region All Dpackage
                    //ed.WriteMessage("\n~~~~~~~~~~START NULL D Node~~~~~~~~~~~\n");
                    DataRow[] AllPack = IDTbl.Select(string.Format("Type <> {0} OR Type <> {1} OR Type <> {2} OR Type <> {3} OR Type <> {4} OR Type <> {5} OR Type <> {6}",
                        Convert.ToInt16(Atend.Control.Enum.ProductType.Pole),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.PoleTip),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel),
                        Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper)));
                    //ed.WriteMessage(" >> PackageTableRowCount = {0}\n", AllPack.Rows.Count);
                    foreach (DataRow adr in AllPack)
                    {

                        //ed.WriteMessage("Code:{0} ParentCode:{1} \n",adr["Code"],adr["ParentCode"]);
                        if (new Guid(adr["NodeCode"].ToString()) == Guid.Empty && new Guid(adr["ParentCode"].ToString()) == Guid.Empty)
                        {
                            Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(adr["Code"].ToString()), _ConnectionString.SingleConnectionAccess);
                            if (Package.Code != Guid.Empty)
                            {
                                DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                                if (Package.IsExistance != 0)
                                {
                                    DataRow dr = DT.NewRow();
                                    dr[8] = Package.IsExistance;
                                    //ed.WriteMessage("Existance:{0}\n", dr[8]);
                                    ////////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                    dr[9] = Package.ProjectCode;
                                    //ed.WriteMessage("Project Code:{0}\n", dr[9]);
                                    dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                                    int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                    PID = 0;
                                    dr[10] = Package.Type;
                                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                                    dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode, _ConnectionString.SingleConnectionAccess);
                                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                                    Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                    if (Product.Code != -1)
                                    {
                                        dr[0] = Product.Code;
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = Product.Unit;
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        if (Product.IsProduct)
                                            dr[4] = 1;
                                        else
                                            dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = Product.Price;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = Product.ExecutePrice;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = Product.WagePrice;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);
                                    }
                                    else
                                    {
                                        dr[0] = "----";
                                        //ed.WriteMessage("Code:{0}\n", dr[0]);
                                        dr[3] = "----";
                                        //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                        dr[4] = 0;
                                        //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                        dr[5] = 0;
                                        //ed.WriteMessage("Price:{0}\n", dr[5]);
                                        dr[6] = 0;
                                        //ed.WriteMessage("Execute Price:{0}\n", dr[6]);
                                        dr[7] = 0;
                                        //ed.WriteMessage("Wage Price:{0}\n", dr[7]);

                                    }
                                    DT.Rows.Add(dr);
                                    SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, Convert.ToInt32(dr[2]), Package.Code);//in E_ContainerPackage
                                }
                                else
                                {
                                    //موجود موجود در لیست نمی آید
                                }
                                ChildFind(PackageTable);//in D_Package
                            }
                        }
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End NULL D Node~~~~~~~~~~~\n");
                    #endregion

                    #region All Post
                    //ed.WriteMessage("\n~~~~~~~~~~Start D Post~~~~~~~~~~~\n");
                    //DataTable PostTable = Atend.Base.Design.DPost.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    DataRow[] PostTable = IDTbl.Select(string.Format("Type <> {0} OR Type <> {1} ",
                       Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost),
                       Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost)));

                    ed.WriteMessage(" >> PostTableRowCount = {0}\n", PostTable.Length.ToString());
                    for (int i = 0; i < PostTable.Length; i++)
                    {

                        Guid NodeID = new Guid(PostTable[i]["ID"].ToString());
                        Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(NodeID, _ConnectionString.SingleConnectionAccess);
                        if (Package.Code != Guid.Empty)
                        {
                            if (Package.IsExistance != 0)
                            {
                                DataRow dr = DT.NewRow();
                                dr[8] = Package.IsExistance;
                                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                                /////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Package.ProjectCode).AdditionalCode;
                                dr[9] = Package.ProjectCode;// pc;
                                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                                dr[2] = Package.Count;//Childs.Rows[i]["Count"];
                                //ed.WriteMessage("Count:{0}\n", dr[2]);
                                int ProCode = Package.ProductCode;//Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                                dr[10] = Package.Type;
                                //ed.WriteMessage("Type:{0}\n", dr[10]);
                                PID = 0;
                                dr[1] = FindNameAndProductCode(Package.Type/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode, _ConnectionString.SingleConnectionAccess);
                                //ed.WriteMessage("Name:{0}\n", dr[1]);
                                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                                if (Product.Code != -1)
                                {
                                    dr[0] = Product.Code;
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = Product.Unit;
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    if (Product.IsProduct)
                                        dr[4] = 1;
                                    else
                                        dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = Product.Price;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = Product.ExecutePrice;
                                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                                    dr[7] = Product.WagePrice;
                                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                                }
                                else
                                {
                                    dr[0] = "----";
                                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                                    dr[3] = "----";
                                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                                    dr[4] = 0;
                                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                                    dr[5] = 0;
                                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                                    dr[6] = 0;
                                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                                    dr[7] = 0;
                                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                                }
                                DT.Rows.Add(dr);
                            }
                            //DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code);
                            SubEquipment(Package.ProductCode, Package.Type, Package.ProjectCode, Package.IsExistance, 1, Package.Code);
                            //ed.WriteMessage("\n~~~~~~~~~~End of Sub Equipment~~~~~~~~~~~\n");
                            //ChildFind(PackageTable);
                        }
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Post~~~~~~~~~~~\n");
                    #endregion

                    #region All Branches
                    //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");
                    //DataTable BranchTable = Atend.Base.Design.DBranch.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
                    DataRow[] BranchTable = IDTbl.Select(string.Format("Type <> {0} OR Type <> {1} OR Type <> {2} ",
                       Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor),
                       Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper),
                       Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel)));

                    //ed.WriteMessage(" >> BranchTableRowCount = {0}\n", BranchTable.Rows.Count);
                    foreach (DataRow BranchRow in BranchTable)
                    {
                        if (Convert.ToInt16(BranchRow["Type"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor))
                            ConductorTip(BranchRow);

                        if (Convert.ToInt16(BranchRow["Type"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper))
                            SelfKeeperTip(BranchRow);

                        if (Convert.ToInt16(BranchRow["Type"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel))
                            GroundCabelTip(BranchRow);

                        ////////////if (Convert.ToInt16(BranchRow["ProductType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Jumper))
                        ////////////    SelfKeeperTip(BranchRow);
                    }
                    //ed.WriteMessage("\n~~~~~~~~~~End of D Branch~~~~~~~~~~~\n");
                    #endregion

                    //ed.WriteMessage("Start Final Operation \n");
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        int Exist = -1;
                        double Count = 0, P = 0;
                        DataRow[] row = GroupDT.Select("Code = '" + DT.Rows[i]["Code"].ToString() + "' And Type = '" + DT.Rows[i]["Type"].ToString() + "'" /*+ " And Name = '" + DT.Rows[i]["Name"].ToString() + "'"*/);
                        Atend.Control.Enum.ProductType Type = (Atend.Control.Enum.ProductType)Convert.ToInt32(DT.Rows[i]["Type"].ToString());
                        Atend.Base.Base.BSetting CurrentSetting = Atend.Base.Base.BSetting.SelectByCode(Convert.ToInt32(Atend.Control.Enum.Setting.ShowProduct), _ConnectionString.SingleSqlConnectionLocal);
                        if ((row.Length == 0))
                        {
                            if ((Type != Atend.Control.Enum.ProductType.Cell) && (Type != Atend.Control.Enum.ProductType.GroundPost) && (Type != Atend.Control.Enum.ProductType.AirPost))
                            {
                                if (DT.Rows[i]["Code"].ToString() != "----")
                                {
                                    Exist = Convert.ToInt32(DT.Rows[i]["Exist"].ToString());
                                    Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                    DataRow GroupDR = GroupDT.NewRow();
                                    GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                    GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                    GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                    GroupDR["Count"] = Count;
                                    P = 0;
                                    switch (Exist)
                                    {
                                        case 0:

                                            P = 0;
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                            break;
                                        case 1:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;

                                            break;
                                        case 2:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;
                                            break;
                                        case 3:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 0.6;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;

                                            break;
                                        case 4:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                            {
                                                P = 1;
                                            }
                                            else if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 100 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 199)
                                            {
                                                P = 1;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                            break;
                                        case 5:
                                            if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 500 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 599)
                                            {
                                                P = 1;
                                            }
                                            GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                            GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                            GroupDR["Price"] = 0;
                                            break;
                                        default:
                                            GroupDR["ExecutePrice"] = 0;
                                            GroupDR["WagePrice"] = 0;
                                            GroupDR["Price"] = 0;
                                            break;
                                    }
                                    GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                    GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                    GroupDR["Exist"] = Exist;
                                    GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                    if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                    {
                                        Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                        GroupDR["ProjectName1"] = BW.Name;
                                    }
                                    else
                                    {
                                        GroupDR["ProjectName1"] = string.Empty;
                                    }
                                    GroupDT.Rows.Add(GroupDR);
                                }
                                else if (Convert.ToBoolean(CurrentSetting.Value))
                                {
                                    Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                    DataRow GroupDR = GroupDT.NewRow();
                                    GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                    GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                    GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                    GroupDR["Count"] = Count;
                                    GroupDR["Price"] = 0;
                                    GroupDR["ExecutePrice"] = 0;
                                    GroupDR["WagePrice"] = 0;
                                    GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                    GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                    GroupDR["Exist"] = DT.Rows[i]["Exist"].ToString();
                                    GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                    if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                    {
                                        Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                        GroupDR["ProjectName1"] = BW.Name;
                                    }
                                    else
                                    {
                                        GroupDR["ProjectName1"] = string.Empty;
                                    }
                                    GroupDT.Rows.Add(GroupDR);
                                }
                            }
                        }
                        else
                        {
                            bool SameStatusWasFound = false;
                            for (int ii = 0; ii < row.Length; ii++)
                            {
                                if ((Convert.ToInt32(row[ii]["Exist"]) == Convert.ToInt32(DT.Rows[i]["Exist"])) && Convert.ToInt32(row[ii]["ProjectCode"]) == Convert.ToInt32(DT.Rows[i]["ProjectCode"]) && row[ii]["Name"].ToString() == DT.Rows[i]["Name"].ToString())
                                {
                                    row[ii]["Count"] = Convert.ToDouble(row[ii]["Count"]) + Convert.ToDouble(DT.Rows[i]["Count"]);
                                    SameStatusWasFound = true;
                                }
                            }
                            if (!SameStatusWasFound)
                            {
                                if ((Type != Atend.Control.Enum.ProductType.Cell) && (Type != Atend.Control.Enum.ProductType.GroundPost) && (Type != Atend.Control.Enum.ProductType.AirPost))
                                {
                                    if (DT.Rows[i]["Code"].ToString() != "----")
                                    {
                                        Exist = Convert.ToInt32(DT.Rows[i]["Exist"].ToString());
                                        Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                        DataRow GroupDR = GroupDT.NewRow();
                                        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                        GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                        GroupDR["Count"] = Count;
                                        P = 0;
                                        switch (Exist)
                                        {
                                            case 0:
                                                P = 0;
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;

                                                break;
                                            case 1:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;

                                                break;
                                            case 2:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                            case 3:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 0.6;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                            case 4:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 300 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 399)
                                                {
                                                    P = 1;
                                                }
                                                else if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 100 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 199)
                                                {
                                                    P = 1;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = Convert.ToInt32(DT.Rows[i]["Price"].ToString()) * P;
                                                break;
                                            case 5:
                                                if (Convert.ToInt32(DT.Rows[i]["ProjectCode"]) >= 500 && Convert.ToInt32(DT.Rows[i]["ProjectCode"]) <= 599)
                                                {
                                                    P = 1;
                                                }
                                                GroupDR["ExecutePrice"] = Convert.ToInt32(DT.Rows[i]["ExecutePrice"].ToString()) * P;
                                                GroupDR["WagePrice"] = Convert.ToInt32(DT.Rows[i]["WagePrice"].ToString()) * P;
                                                GroupDR["Price"] = 0 * P;
                                                break;
                                        }
                                        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                        GroupDR["Exist"] = Exist;
                                        GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();
                                        if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                        {
                                            Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                            GroupDR["ProjectName1"] = BW.Name;
                                        }
                                        else
                                        {
                                            GroupDR["ProjectName1"] = string.Empty;
                                        }

                                        GroupDT.Rows.Add(GroupDR);
                                    }
                                    else
                                    {
                                        Count = Convert.ToDouble(DT.Rows[i]["Count"].ToString());
                                        DataRow GroupDR = GroupDT.NewRow();
                                        GroupDR["Code"] = DT.Rows[i]["Code"].ToString();
                                        GroupDR["Name"] = DT.Rows[i]["Name"].ToString();
                                        GroupDR["Type"] = DT.Rows[i]["Type"].ToString();
                                        GroupDR["Count"] = Count;
                                        GroupDR["Price"] = 0;
                                        GroupDR["ExecutePrice"] = 0;
                                        GroupDR["WagePrice"] = 0;
                                        GroupDR["Unit"] = DT.Rows[i]["Unit"].ToString();
                                        GroupDR["IsProduct"] = DT.Rows[i]["IsProduct"].ToString();
                                        GroupDR["Exist"] = DT.Rows[i]["Exist"].ToString();
                                        GroupDR["ProjectCode"] = DT.Rows[i]["ProjectCode"].ToString();

                                        if (Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()) != 0)
                                        {
                                            Atend.Base.Base.BWorkOrder BW = Atend.Base.Base.BWorkOrder.SelectByACode(Convert.ToInt16(DT.Rows[i]["ProjectCode"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                                            GroupDR["ProjectName1"] = BW.Name;
                                        }
                                        else
                                        {
                                            GroupDR["ProjectName1"] = string.Empty;
                                        }

                                        GroupDT.Rows.Add(GroupDR);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error CreateExcelStatus: {0} \n", ex.Message);
                }
            }
            _ConnectionString.CloseSingleConnectionAccess();
            _ConnectionString.CloseSingleSqlConnectionLocal();
            return GroupDT;
        }

        private void ConductorTip(DataRow BranchRow)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();
            Atend.Base.Equipment.EConductorTip CondTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Convert.ToInt32(BranchRow["ProductCode"].ToString()));

            DataRow dr;

            if (CondTip.NeutralCount > 0)
            {
                dr = DT.NewRow();
                Atend.Base.Equipment.EConductor Cond = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.NeutralProductCode);

                dr[2] = Math.Round(Convert.ToDouble(BranchRow["Lenght"].ToString()) * CondTip.NeutralCount * Cond.Wc, 2);
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), CondTip.NeutralProductCode, _ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();

                SetDataRow(ref dr);

                SubEquipment(CondTip.NeutralProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                DT.Rows.Add(dr);
            }

            if (CondTip.NightCount > 0)
            {
                dr = DT.NewRow();
                Atend.Base.Equipment.EConductor Cond = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.NightProductCode);

                dr[2] = Math.Round(Convert.ToDouble(BranchRow["Lenght"].ToString()) * CondTip.NightCount * Cond.Wc, 2);
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), CondTip.NightProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();

                SubEquipment(CondTip.NightProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                SetDataRow(ref dr);
                DT.Rows.Add(dr);
            }

            if (CondTip.PhaseCount > 0)
            {
                dr = DT.NewRow();
                Atend.Base.Equipment.EConductor Cond = Atend.Base.Equipment.EConductor.AccessSelectByCode(CondTip.PhaseProductCode);

                dr[2] = Math.Round(Convert.ToDouble(BranchRow["Lenght"].ToString()) * CondTip.PhaseCount * Cond.Wc, 2);
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), CondTip.PhaseProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();
                SetDataRow(ref dr);

                SubEquipment(CondTip.PhaseProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.Conductor), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                //ed.WriteMessage("Code:{0}\n", dr[0]);
                //ed.WriteMessage("Name:{0}\n", dr[1]);
                //ed.WriteMessage("Count:{0}\n", dr[2]);
                //ed.WriteMessage("Unit:{0}\n", dr[3]);
                //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                //ed.WriteMessage("Price:{0}\n", dr[5]);
                //ed.WriteMessage("Execute:{0}\n", dr[6]);
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                //ed.WriteMessage("Type:{0}\n", dr[10]);
                DT.Rows.Add(dr);
                //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");
            }

        }

        private void SelfKeeperTip(DataRow BranchRow)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();
            Atend.Base.Equipment.ESelfKeeperTip SelfKTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Convert.ToInt32(BranchRow["ProductCode"].ToString()));
            DataRow dr;

            if (SelfKTip.NeutralCount > 0)
            {
                dr = DT.NewRow();

                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * SelfKTip.NeutralCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), SelfKTip.NeutralProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();

                SetDataRow(ref dr);
                SubEquipment(SelfKTip.NeutralProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                DT.Rows.Add(dr);
            }

            if (SelfKTip.NightCount > 0)
            {
                dr = DT.NewRow();
                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * SelfKTip.NightCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), SelfKTip.NightProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();
                SetDataRow(ref dr);
                SubEquipment(SelfKTip.NightProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                DT.Rows.Add(dr);
            }

            if (SelfKTip.PhaseCount > 0)
            {
                dr = DT.NewRow();
                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * SelfKTip.PhaseCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), SelfKTip.PhaseProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();
                SetDataRow(ref dr);
                SubEquipment(SelfKTip.PhaseProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.SelfKeeper), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                //ed.WriteMessage("Code:{0}\n", dr[0]);
                //ed.WriteMessage("Name:{0}\n", dr[1]);
                //ed.WriteMessage("Count:{0}\n", dr[2]);
                //ed.WriteMessage("Unit:{0}\n", dr[3]);
                //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                //ed.WriteMessage("Price:{0}\n", dr[5]);
                //ed.WriteMessage("Execute:{0}\n", dr[6]);
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                //ed.WriteMessage("Type:{0}\n", dr[10]);
                DT.Rows.Add(dr);
            }
            //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");

        }

        private void GroundCabelTip(DataRow BranchRow)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Equipment.EGroundCabelTip GroundKTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(Convert.ToInt32(BranchRow["ProductCode"].ToString()));
            DataRow dr;

            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();

            if (GroundKTip.NeutralCount > 0)
            {
                dr = DT.NewRow();

                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * GroundKTip.NeutralCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.NeutralProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();

                SetDataRow(ref dr);
                SubEquipment(GroundKTip.NeutralProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);


                DT.Rows.Add(dr);
            }
            //dr = DT.NewRow();
            //dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString());
            //PID = 0;
            //dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
            //dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.NightProductCode);
            //dr[8] = BranchRow["IsExist"].ToString();
            //dr[9] = BranchRow["ProjectCode"].ToString();
            //SetDataRow(ref dr);
            //DT.Rows.Add(dr);

            if (GroundKTip.PhaseCount > 0)
            {
                dr = DT.NewRow();
                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * GroundKTip.PhaseCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.PhaseProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();
                SetDataRow(ref dr);
                SubEquipment(GroundKTip.PhaseProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                //ed.WriteMessage("Code:{0}\n", dr[0]);
                //ed.WriteMessage("Name:{0}\n", dr[1]);
                //ed.WriteMessage("Count:{0}\n", dr[2]);
                //ed.WriteMessage("Unit:{0}\n", dr[3]);
                //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                //ed.WriteMessage("Price:{0}\n", dr[5]);
                //ed.WriteMessage("Execute:{0}\n", dr[6]);
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                //ed.WriteMessage("Type:{0}\n", dr[10]);
                DT.Rows.Add(dr);
            }
            //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");
        }

        private void Jumper(DataRow BranchRow)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Base.Equipment.EGroundCabelTip GroundKTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(Convert.ToInt32(BranchRow["ProductCode"].ToString()));
            DataRow dr;

            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();

            if (GroundKTip.NeutralCount > 0)
            {
                dr = DT.NewRow();

                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * GroundKTip.NeutralCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.NeutralProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();

                SetDataRow(ref dr);
                SubEquipment(GroundKTip.NeutralProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                DT.Rows.Add(dr);
            }
            //dr = DT.NewRow();
            //dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString());
            //PID = 0;
            //dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
            //dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.NightProductCode);
            //dr[8] = BranchRow["IsExist"].ToString();
            //dr[9] = BranchRow["ProjectCode"].ToString();
            //SetDataRow(ref dr);
            //DT.Rows.Add(dr);

            if (GroundKTip.PhaseCount > 0)
            {
                dr = DT.NewRow();
                dr[2] = Convert.ToDouble(BranchRow["Lenght"].ToString()) * GroundKTip.PhaseCount;
                PID = 0;
                dr[10] = Convert.ToInt16(BranchRow["ProductType"].ToString());
                dr[1] = FindNameAndProductCode(Convert.ToInt16(BranchRow["ProductType"].ToString()), GroundKTip.PhaseProductCode,_ConnectionString.SingleConnectionAccess);
                dr[8] = BranchRow["IsExist"].ToString();
                dr[9] = BranchRow["ProjectCode"].ToString();
                SetDataRow(ref dr);
                SubEquipment(GroundKTip.PhaseProductCode, Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel), Convert.ToInt32(dr[9].ToString()), Convert.ToInt32(dr[8].ToString()), 1, Guid.Empty);

                //ed.WriteMessage("Code:{0}\n", dr[0]);
                //ed.WriteMessage("Name:{0}\n", dr[1]);
                //ed.WriteMessage("Count:{0}\n", dr[2]);
                //ed.WriteMessage("Unit:{0}\n", dr[3]);
                //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                //ed.WriteMessage("Price:{0}\n", dr[5]);
                //ed.WriteMessage("Execute:{0}\n", dr[6]);
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                //ed.WriteMessage("Type:{0}\n", dr[10]);
                DT.Rows.Add(dr);
            }
            //ed.WriteMessage("\n~~~~~~~~~~Start of D Branch~~~~~~~~~~~\n");
        }

        private void SetDataRow(ref DataRow dr)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID);

            if (Product.Code > 0)
            {
                //ed.WriteMessage("\n4444111\n");

                dr[0] = Product.Code;
                //ed.WriteMessage("\n4444222\n");
                dr[3] = Product.Unit;
                //ed.WriteMessage("\n4444333\n");

                if (Product.IsProduct)
                    dr[4] = 1;
                else
                    dr[4] = 0;
                //ed.WriteMessage("\n44445555\n");
                dr[5] = Product.Price;
                //ed.WriteMessage("\n4444666\n");
                dr[6] = Product.ExecutePrice;
                //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                dr[7] = Product.WagePrice;
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
            }
            else
            {
                //ed.WriteMessage("\nError1\n");
                dr[0] = "----";
                //ed.WriteMessage("\nError2\n");
                dr[3] = "----";

                //ed.WriteMessage("\nError3\n");
                dr[4] = "----";
                //ed.WriteMessage("\nError4\n");
                dr[5] = "----";
                dr[6] = 0;
                //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                dr[7] = 0;
                //ed.WriteMessage("WagePrice:{0}\n", dr[7]);

            }

        }

        //StatusReport
        private void ChildFind(DataTable Childs)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~ CHILD STARTED ~~~~~~~~~~~~~~~~~\n");
            if (Childs.Rows.Count == 0)
                return;

            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();

            //DataTable Childs = Atend.Base.Design.DPackage.SelectByParentCode(Package.Code);


            for (int i = 0; i < Childs.Rows.Count; i++)
            {
                if (Convert.ToInt32(Childs.Rows[i]["IsExistance"]) != 0)
                {
                    DataRow dr = DT.NewRow();
                    dr[8] = Childs.Rows[i]["IsExistance"].ToString();
                    //ed.WriteMessage("Exist:{0}\n", dr[8]);
                    //////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(Convert.ToInt32(Childs.Rows[i]["ProjectCode"].ToString())).AdditionalCode;
                    dr[9] = Convert.ToInt32(Childs.Rows[i]["ProjectCode"].ToString());// pc;
                    //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                    dr[2] = Childs.Rows[i]["Count"];
                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                    int ProCode = Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString());
                    //ed.WriteMessage("\nPackage Type = " + Childs.Rows[i]["Type"].ToString() + " Package ProductCode = " + ProCode.ToString());
                    dr[10] = Convert.ToInt16(Childs.Rows[i]["Type"].ToString());
                    //ed.WriteMessage("Type:{0}\n", dr[10]);
                    PID = 0;
                    dr[1] = FindNameAndProductCode(Convert.ToInt32(Childs.Rows[i]["Type"].ToString()), ProCode,_ConnectionString.SingleConnectionAccess);
                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                    //if (PID < 1 && Convert.ToInt32(Childs.Rows[i]["Type"].ToString()) == 26)
                    //{
                    //    //continue;
                    //    //dr[1] = FindNameOperation(
                    //    //Atend.Base.Equipment.EOperation eOP = Atend.Base.Equipment.EOperation.SelectByCode(Code);
                    //    Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID);
                    //    ed.WriteMessage("\nssssssss\n");

                    //    //ed.WriteMessage("\nBProduct Code = " + Product.Code + "\n");

                    //    if (Product.Code != 0)
                    //    {
                    //        ed.WriteMessage("\n4444111\n");

                    //        dr[0] = Product.Code;
                    //        ed.WriteMessage("\n4444222\n");
                    //        dr[3] = Product.Unit;
                    //        ed.WriteMessage("\n4444333\n");

                    //        if (Product.IsProduct)
                    //            dr[4] = 1;
                    //        else
                    //            dr[4] = 0;
                    //        ed.WriteMessage("\n44445555\n");
                    //        dr[5] = Product.Price;
                    //        dr[6] = Product.ExecutePrice;
                    //        dr[7] = Product.WagePrice;

                    //        ed.WriteMessage("\n4444666\n");
                    //    }
                    //    else
                    //    {
                    //        dr[0] = "----";
                    //        dr[3] = "----";

                    //        dr[4] = 0;
                    //        dr[5] = 0;
                    //        dr[6] = 0;
                    //        dr[7] = 0;


                    //    }
                    //    DT.Rows.Add(dr);

                    //    ed.WriteMessage("\n5555555\n");


                    //    continue;
                    //}
                    //else
                    //{
                    if (PID > 0)
                    {
                        Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                        if (Product.Code != -1)
                        {
                            dr[0] = Product.Code;
                            //ed.WriteMessage("Code:{0}\n", dr[0]);
                            dr[3] = Product.Unit;
                            //ed.WriteMessage("Unit:{0}\n", dr[3]);
                            if (Product.IsProduct)
                                dr[4] = 1;
                            else
                                dr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                            dr[5] = Product.Price;
                            //ed.WriteMessage("Price:{0}\n", dr[5]);
                            dr[6] = Product.ExecutePrice;
                            //ed.WriteMessage("ExutePrice:{0}\n", dr[6]);
                            dr[7] = Product.WagePrice;
                            //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                        }
                        else
                        {
                            dr[0] = "----";
                            //ed.WriteMessage("Code:{0}\n", dr[0]);
                            dr[3] = "----";
                            //ed.WriteMessage("Unit:{0}\n", dr[3]);
                            dr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                            dr[5] = 0;
                            //ed.WriteMessage("Price:{0}\n", dr[5]);
                            dr[6] = 0;
                            //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                            dr[7] = 0;
                            //ed.WriteMessage("WagePrice:{0}\n", dr[7]);

                        }
                    }
                    else
                    {
                        if (PID < 1)
                        {
                            dr[0] = "----";
                            //ed.WriteMessage("Code:{0}\n", dr[0]);
                            dr[3] = "----";
                            //ed.WriteMessage("Unit:{0}\n", dr[3]);
                            dr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                            dr[5] = 0;
                            //ed.WriteMessage("Price:{0}\n", dr[5]);
                            dr[6] = 0;
                            //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                            dr[7] = 0;
                            //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                        }
                    }
                    DT.Rows.Add(dr);
                    Guid Package = new Guid(Childs.Rows[i]["Code"].ToString());
                    SubEquipment(Convert.ToInt32(Childs.Rows[i]["ProductCode"].ToString()), Convert.ToInt32(Childs.Rows[i]["Type"].ToString()), Convert.ToInt32(Childs.Rows[i]["ProjectCode"].ToString()), Convert.ToInt32(Childs.Rows[i]["IsExistance"].ToString()), Convert.ToInt32(dr[2]), new Guid(Childs.Rows[i]["Code"].ToString()));
                    //ed.WriteMessage("\n~~~~~~~~~~End of CHILD Equipment~~~~~~~~~~~\n");
                    DataTable PackageTable = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package, _ConnectionString.SingleConnectionAccess);
                    ChildFind(PackageTable);
                }
                else
                {
                    //فرزند موجود موجود به لیست اضاف نمی شود
                }
            }
        }

        //StatusReport 
        private string FindNameAndProductCode(int Type, int ProductCode, OleDbConnection _connection)
        {
            //Atend.Base.Equipment.EPole. p = new Atend.Base.Equipment.EPole();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string DBName = "نام اولیه خالی";

            switch ((Atend.Control.Enum.ProductType)Type)
            {

                case Atend.Control.Enum.ProductType.AuoKey3p:
                    Atend.Base.Equipment.EAutoKey_3p eAutoKey_3p = Atend.Base.Equipment.EAutoKey_3p.AccessSelectByCode(ProductCode, _connection);
                    DBName = eAutoKey_3p.Name;
                    PID = eAutoKey_3p.ProductCode;
                    break;


                case Atend.Control.Enum.ProductType.Pole:
                    Atend.Base.Equipment.EPole ePole = Atend.Base.Equipment.EPole.AccessSelectByCode(ProductCode, _connection);
                    DBName = ePole.Name;
                    PID = ePole.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PoleTip:
                    Atend.Base.Equipment.EPoleTip ePoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(ProductCode, _connection);
                    DBName = ePoleTip.Name;
                    PID = -1;
                    break;

                case Atend.Control.Enum.ProductType.Conductor:
                    Atend.Base.Equipment.EConductor eConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ProductCode,_connection);
                    DBName = eConductor.Name;
                    PID = eConductor.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.ConductorTip:
                    Atend.Base.Equipment.EConductorTip eConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(ProductCode,_connection);
                    DBName = eConductorTip.Name;
                    PID = -1;
                    break;

                case Atend.Control.Enum.ProductType.Ramp:
                    Atend.Base.Equipment.ERamp eRamp = Atend.Base.Equipment.ERamp.AccessSelectByCode(ProductCode,_connection);
                    DBName = eRamp.Name;
                    PID = eRamp.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Breaker:
                    Atend.Base.Equipment.EBreaker eBreaker = Atend.Base.Equipment.EBreaker.AccessSelectByCode(ProductCode,_connection);
                    DBName = eBreaker.Name;
                    PID = eBreaker.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Bus:
                    Atend.Base.Equipment.EBus eBus = Atend.Base.Equipment.EBus.AccessSelectByCode(ProductCode,_connection);
                    DBName = eBus.Name;
                    PID = eBus.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Halter:
                    Atend.Base.Equipment.EHalter eHalter = Atend.Base.Equipment.EHalter.AccessSelectByCode(ProductCode, _connection);
                    DBName = eHalter.Name;
                    PID = eHalter.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.CatOut:
                    Atend.Base.Equipment.ECatOut eCatOut = Atend.Base.Equipment.ECatOut.AccessSelectByCode(ProductCode,_connection);
                    DBName = eCatOut.Name;
                    PID = eCatOut.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.CT:
                    Atend.Base.Equipment.ECT eCT = Atend.Base.Equipment.ECT.AccessSelectByCode(ProductCode,_connection);
                    DBName = eCT.Name;
                    PID = eCT.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.DB:
                    Atend.Base.Equipment.EDB eDB = Atend.Base.Equipment.EDB.AccessSelectByCode(ProductCode,_connection);
                    DBName = eDB.Name;
                    PID = eDB.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.HeaderCabel:
                    Atend.Base.Equipment.EHeaderCabel eHeader = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(ProductCode,_connection);
                    DBName = eHeader.Name;
                    PID = eHeader.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Disconnector:
                    Atend.Base.Equipment.EDisconnector eDC = Atend.Base.Equipment.EDisconnector.AccessSelectByCode(ProductCode,_connection);
                    DBName = eDC.Name;
                    PID = eDC.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Rod:
                    Atend.Base.Equipment.ERod eRod = Atend.Base.Equipment.ERod.AccessSelectByCode(ProductCode,_connection);
                    DBName = eRod.Name;
                    PID = eRod.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Countor:
                    Atend.Base.Equipment.ECountor eCounter = Atend.Base.Equipment.ECountor.AccessSelectByCode(ProductCode,_connection);
                    DBName = eCounter.Name;
                    PID = eCounter.ProductCode;
                    break;

                //case Atend.Control.Enum.ProductType.JackPanel:
                //    Atend.Base.Equipment.EJAckPanel eJack = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(ProductCode);
                //    break;

                case Atend.Control.Enum.ProductType.PhotoCell:
                    Atend.Base.Equipment.EPhotoCell ePhotoCell = Atend.Base.Equipment.EPhotoCell.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePhotoCell.Name;
                    PID = ePhotoCell.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Phuse:
                    Atend.Base.Equipment.EPhuse ePhuse = Atend.Base.Equipment.EPhuse.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePhuse.Name;
                    PID = ePhuse.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.StreetBox:
                    Atend.Base.Equipment.EStreetBox eStreet = Atend.Base.Equipment.EStreetBox.AccessSelectByCode(ProductCode,_connection);
                    DBName = eStreet.Name;
                    PID = eStreet.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Transformer:
                    Atend.Base.Equipment.ETransformer eTrans = Atend.Base.Equipment.ETransformer.AccessSelectByCode(ProductCode,_connection);
                    DBName = eTrans.Name;
                    PID = eTrans.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PT:
                    Atend.Base.Equipment.EPT ePT = Atend.Base.Equipment.EPT.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePT.Name;
                    PID = ePT.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Insulator:
                    Atend.Base.Equipment.EInsulator eInsulator = Atend.Base.Equipment.EInsulator.AccessSelectByCode(ProductCode,_connection);
                    DBName = eInsulator.Name;
                    PID = eInsulator.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.InsulatorPipe:
                    Atend.Base.Equipment.EInsulatorPipe eInsulatorPipe = Atend.Base.Equipment.EInsulatorPipe.AccessSelectByCode(ProductCode,_connection);
                    DBName = eInsulatorPipe.Name;
                    PID = eInsulatorPipe.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.InsulatorChain:
                    Atend.Base.Equipment.EInsulatorChain eInsulatorChain = Atend.Base.Equipment.EInsulatorChain.AccessSelectByCode(ProductCode,_connection);
                    DBName = eInsulatorChain.Name;
                    PID = eInsulatorChain.ProductCode;
                    break;


                case Atend.Control.Enum.ProductType.ReCloser:
                    Atend.Base.Equipment.EReCloser eRecloser = Atend.Base.Equipment.EReCloser.AccessSelectByCode(ProductCode,_connection);
                    DBName = eRecloser.Name;
                    PID = eRecloser.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.PhuseKey:
                    Atend.Base.Equipment.EPhuseKey ePhusekey = Atend.Base.Equipment.EPhuseKey.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePhusekey.Name;
                    PID = ePhusekey.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.MiniatureKey:
                    Atend.Base.Equipment.EMiniatorKey eMiniatorKey = Atend.Base.Equipment.EMiniatorKey.AccessSelectByCode(ProductCode,_connection);
                    DBName = eMiniatorKey.Name;
                    PID = eMiniatorKey.ProductCode;
                    break;


                case Atend.Control.Enum.ProductType.Consol:
                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(ProductCode,_connection);
                    DBName = eConsol.Name;
                    PID = eConsol.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Light:
                    Atend.Base.Equipment.ELight eLight = Atend.Base.Equipment.ELight.AccessSelectByCode(ProductCode,_connection);
                    DBName = eLight.Name;
                    PID = eLight.ProductCode;
                    break;


                case Atend.Control.Enum.ProductType.PhusePole:
                    Atend.Base.Equipment.EPhusePole ePhusepole = Atend.Base.Equipment.EPhusePole.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePhusepole.Name;
                    PID = ePhusepole.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    Atend.Base.Equipment.EJAckPanel eJackP = Atend.Base.Equipment.EJAckPanel.AccessSelectByCode(ProductCode,_connection);
                    DBName = eJackP.Name;
                    PID = eJackP.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Prop:
                    Atend.Base.Equipment.EProp eProp = Atend.Base.Equipment.EProp.AccessSelectByCode(ProductCode,_connection);
                    DBName = eProp.Name;
                    PID = eProp.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Khazan:
                    Atend.Base.Equipment.EKhazan eKhazan = Atend.Base.Equipment.EKhazan.AccessSelectByCode(ProductCode,_connection);
                    DBName = eKhazan.Name;
                    PID = eKhazan.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Mafsal:
                    Atend.Base.Equipment.EMafsal eMafsal = Atend.Base.Equipment.EMafsal.AccessSelectByCode(ProductCode,_connection);
                    DBName = eMafsal.Name;
                    PID = eMafsal.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.KablSho:
                    Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(ProductCode,_connection);
                    DBName = eKablsho.Name;
                    PID = eKablsho.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Kalamp:
                    Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(ProductCode,_connection);
                    DBName = eClamp.Name;
                    PID = eClamp.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.GroundPost:
                    Atend.Base.Equipment.EGroundPost ePost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(ProductCode,_connection);
                    DBName = ePost.Name;
                    PID = ePost.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                    Atend.Base.Equipment.EMeasuredJackPanel eMeasuredJackPanel = Atend.Base.Equipment.EMeasuredJackPanel.AccessSelectByCode(ProductCode,_connection);
                    DBName = eMeasuredJackPanel.Name;
                    PID = eMeasuredJackPanel.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Ground:
                    Atend.Base.Equipment.EGround eGround = Atend.Base.Equipment.EGround.AccessSelectByCode(ProductCode,_connection);
                    DBName = eGround.Name;
                    PID = eGround.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Floor:
                    Atend.Base.Equipment.EFloor eFloor = Atend.Base.Equipment.EFloor.AccessSelectByCode(ProductCode,_connection);
                    DBName = eFloor.Name;
                    PID = eFloor.ProductCode;
                    break;


                case Atend.Control.Enum.ProductType.GroundCabel:
                    Atend.Base.Equipment.EGroundCabel eGroundCabel = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(ProductCode,_connection);
                    DBName = eGroundCabel.Name;
                    PID = eGroundCabel.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.GroundCabelTip:
                    Atend.Base.Equipment.EGroundCabelTip eGroundCabelTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(ProductCode,_connection);
                    DBName = eGroundCabelTip.Name;
                    PID = -1;
                    break;


                case Atend.Control.Enum.ProductType.AirPost:
                    Atend.Base.Equipment.EAirPost eAirpost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(ProductCode,_connection);
                    DBName = eAirpost.Name;
                    PID = eAirpost.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    Atend.Base.Equipment.EJackPanelWeek eJPW = Atend.Base.Equipment.EJackPanelWeek.AccessSelectByCode(ProductCode,_connection);
                    DBName = eJPW.Name;
                    PID = eJPW.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.Cell:
                    Atend.Base.Equipment.ECell eCell = Atend.Base.Equipment.ECell.AccessSelectByCode(ProductCode,_connection);
                    DBName = eCell.Name;
                    PID = -1;
                    break;

                case Atend.Control.Enum.ProductType.SectionLizer:
                    Atend.Base.Equipment.ESectionLizer eSectionLizer = Atend.Base.Equipment.ESectionLizer.AccessSelectByCode(ProductCode,_connection);
                    DBName = eSectionLizer.Name;
                    PID = eSectionLizer.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeper:
                    Atend.Base.Equipment.ESelfKeeper eSelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(ProductCode,_connection);
                    DBName = eSelfKeeper.Name;
                    PID = eSelfKeeper.ProductCode;
                    break;

                case Atend.Control.Enum.ProductType.SelfKeeperTip:
                    Atend.Base.Equipment.ESelfKeeperTip eSelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(ProductCode,_connection);
                    DBName = eSelfKeeperTip.Name;
                    PID = -1;
                    break;

            }

            return DBName;

        }

        //StatusReport
        private void SubEquipment(int Code, int Type, int ProjectCode, int Exist, int ParentCount, Guid ParentCode)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Code = {0} Type = {1} ProjectCode = {2} ParentCount = {3} parentCode = {4} \n",
            //    Code,Type,Exist,ParentCount,ParentCode);
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");


            _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();

            #region Operation
            DataTable OpTbl = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(Code, Type, _ConnectionString.SingleConnectionAccess);
            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~~~~~~~Start Operation~~~~~~~~~~~~~~~~~~~~~~~\n");
            foreach (DataRow OpRow in OpTbl.Rows)
            {
                DataRow dr = DT.NewRow();
                dr[8] = Exist;
                //ed.WriteMessage("Exist:{0}\n", dr[8]);
                /////////////////////////////////////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(ProjectCode).AdditionalCode;
                dr[9] = ProjectCode;
                //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);

                if (Type != Convert.ToInt16(Atend.Control.Enum.ProductType.GroundCabel))
                    dr[2] = Convert.ToDouble(OpRow["Count"]) * ParentCount;
                else
                    dr[2] = Convert.ToDouble(OpRow["Count"]);

                //ed.WriteMessage("Count:{0}\n", dr[2]);
                int ProCode = Convert.ToInt32(OpRow["ProductCode"].ToString());
                dr[10] = (int)Atend.Control.Enum.ProductType.Operation;
                //ed.WriteMessage("Type:{0}\n", dr[10]);
                PID = 0;
                Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(Convert.ToInt32(OpRow["ProductId"].ToString()), _ConnectionString.SingleSqlConnectionLocal);
                if (Product.Code != -1)
                {
                    dr[0] = Product.Code;
                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                    dr[1] = Product.Name;
                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                    dr[3] = Product.Unit;
                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                    if (Product.IsProduct)
                        dr[4] = 1;
                    else
                        dr[4] = 0;
                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                    dr[5] = Product.Price;
                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                    dr[6] = Product.ExecutePrice;
                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                    dr[7] = Product.WagePrice;
                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                }
                else
                {
                    dr[0] = "----";
                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                    dr[1] = "----";
                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                    dr[3] = "----";
                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                    dr[4] = 0;
                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                    dr[5] = 0;
                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                    dr[6] = 0;
                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                    dr[7] = 0;
                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);
                }
                DT.Rows.Add(dr);
            }
            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~~~~~~~End Operation~~~~~~~~~~~~~~~~~~~~~~~\n");
            #endregion

            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~~~~~~~Start Contain IN Table~~~~~~~~~~~~~~~~~~~~~~~\n");
            int SProCode = 0;
            Atend.Base.Base.BProduct SProduct = null;
            switch ((Atend.Control.Enum.ProductType)Type)
            {
                case Atend.Control.Enum.ProductType.MiddleJackPanel:
                    DataTable MiddleCells = Atend.Base.Equipment.EJackPanelCell.AccessSelectByJackPanelCode(Code, _ConnectionString.SingleConnectionAccess);
                    foreach (DataRow dr in MiddleCells.Rows)
                    {
                        SubEquipment(Convert.ToInt32(dr["ProductCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell, ProjectCode, Exist, 1, Guid.Empty);
                    }
                    break;
                case Atend.Control.Enum.ProductType.WeekJackPanel:
                    DataTable WeekCells = Atend.Base.Equipment.EJackPanelWeekCell.AccessSelectByJackPanelWeekCode(Code, _ConnectionString.SingleConnectionAccess);
                    foreach (DataRow WeekCell in WeekCells.Rows)
                    {

                        SubEquipment(Convert.ToInt32(WeekCell["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell, ProjectCode, Exist, 1, Guid.Empty);
                    }

                    break;
                case Atend.Control.Enum.ProductType.DB:
                    DataTable DBPhuses = Atend.Base.Equipment.EDBPhuse.AccessSelectByDBCode(Code, _ConnectionString.SingleConnectionAccess);
                    foreach (DataRow dr in DBPhuses.Rows)
                    {
                        DataRow DBPhusedr = DT.NewRow();
                        DBPhusedr[8] = Exist;
                        //ed.WriteMessage("Exist:{0}\n", DBPhusedr[8]);
                        ////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(ProjectCode).AdditionalCode;
                        DBPhusedr[9] = ProjectCode;
                        //ed.WriteMessage("ProjectCode:{0}\n", DBPhusedr[9]);
                        DBPhusedr[2] = 1;
                        //ed.WriteMessage("Count:{0}\n", DBPhusedr[2]);
                        int ProCode = Convert.ToInt32(dr["PhuseCode"].ToString());
                        DBPhusedr[10] = (int)Atend.Control.Enum.ProductType.Phuse;
                        //ed.WriteMessage("Type:{0}\n", DBPhusedr[10]);
                        PID = 0;
                        DBPhusedr[1] = FindNameAndProductCode((int)Atend.Control.Enum.ProductType.Phuse/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                        //ed.WriteMessage("Name:{0}\n", DBPhusedr[1]);
                        Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                        if (Product.Code != -1)
                        {
                            DBPhusedr[0] = Product.Code;
                            //ed.WriteMessage("Code:{0}\n", DBPhusedr[0]);
                            DBPhusedr[3] = Product.Unit;
                            //ed.WriteMessage("Unit:{0}\n", DBPhusedr[3]);
                            if (Product.IsProduct)
                                DBPhusedr[4] = 1;
                            else
                                DBPhusedr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", DBPhusedr[4]);
                            DBPhusedr[5] = Product.Price;
                            //ed.WriteMessage("Price:{0}\n", DBPhusedr[5]);
                            DBPhusedr[6] = Product.ExecutePrice;
                            //ed.WriteMessage("ExecutePrice:{0}\n", DBPhusedr[6]);
                            DBPhusedr[7] = Product.WagePrice;
                            //ed.WriteMessage("WagePrice:{0}\n", DBPhusedr[7]);
                        }
                        else
                        {
                            DBPhusedr[0] = "----";
                            //ed.WriteMessage("Code:{0}\n", DBPhusedr[0]);
                            DBPhusedr[3] = "----";
                            //ed.WriteMessage("Unit:{0}\n", DBPhusedr[3]);
                            DBPhusedr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", DBPhusedr[4]);
                            DBPhusedr[5] = 0;
                            //ed.WriteMessage("Price:{0}\n", DBPhusedr[5]);
                            DBPhusedr[6] = 0;
                            //ed.WriteMessage("ExecutePrice:{0}\n", DBPhusedr[6]);
                            DBPhusedr[7] = 0;
                            //ed.WriteMessage("WagePrice:{0}\n", DBPhusedr[7]);
                        }
                        DT.Rows.Add(DBPhusedr);
                        SubEquipment(Convert.ToInt32(dr["PhuseCode"].ToString()), (int)Atend.Control.Enum.ProductType.Phuse, ProjectCode, Exist, 1, Guid.Empty);
                    }
                    break;
                case Atend.Control.Enum.ProductType.StreetBox:
                    //ed.WriteMessage("Strreet box was there \n");
                    DataTable StreetBoxPhuses = Atend.Base.Equipment.EStreetBoxPhuse.AccessSelectByStreetBoxCode(Code, _ConnectionString.SingleConnectionAccess);
                    foreach (DataRow StreetBoxPhuse in StreetBoxPhuses.Rows)
                    {
                        DataRow StreetBoxPhusedr = DT.NewRow();
                        StreetBoxPhusedr[8] = Exist;
                        //ed.WriteMessage("Exist:{0}\n", StreetBoxPhusedr[8]);
                        ////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(ProjectCode).AdditionalCode;
                        StreetBoxPhusedr[9] = ProjectCode;
                        //ed.WriteMessage("ProjectCode:{0}\n", StreetBoxPhusedr[9]);
                        StreetBoxPhusedr[2] = 1 * ParentCount;
                        //ed.WriteMessage("Count:{0}\n", StreetBoxPhusedr[2]);
                        SProCode = Convert.ToInt32(StreetBoxPhuse["PhuseCode"].ToString());
                        StreetBoxPhusedr[10] = (int)Atend.Control.Enum.ProductType.Phuse;
                        //ed.WriteMessage("Type:{0}\n", StreetBoxPhusedr[10]);
                        PID = 0;
                        StreetBoxPhusedr[1] = FindNameAndProductCode((int)Atend.Control.Enum.ProductType.Phuse/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, SProCode,_ConnectionString.SingleConnectionAccess);
                        //ed.WriteMessage("Name:{0}\n", StreetBoxPhusedr[1]);
                        SProduct = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                        if (SProduct.Code != -1)
                        {
                            StreetBoxPhusedr[0] = SProduct.Code;
                            //ed.WriteMessage("Code:{0}\n", StreetBoxPhusedr[0]);
                            StreetBoxPhusedr[3] = SProduct.Unit;
                            //ed.WriteMessage("Unit:{0}\n", StreetBoxPhusedr[3]);
                            if (SProduct.IsProduct)
                                StreetBoxPhusedr[4] = 1;
                            else
                                StreetBoxPhusedr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", StreetBoxPhusedr[4]);
                            StreetBoxPhusedr[5] = SProduct.Price;
                            //ed.WriteMessage("Price:{0}\n", StreetBoxPhusedr[5]);
                            StreetBoxPhusedr[6] = SProduct.ExecutePrice;
                            //ed.WriteMessage("ExecutePrice:{0}\n", StreetBoxPhusedr[6]);
                            StreetBoxPhusedr[7] = SProduct.WagePrice;
                            //ed.WriteMessage("WagePrice:{0}\n", StreetBoxPhusedr[7]);
                        }
                        else
                        {
                            StreetBoxPhusedr[0] = "----";
                            //ed.WriteMessage("Code:{0}\n", StreetBoxPhusedr[0]);
                            StreetBoxPhusedr[3] = "----";
                            //ed.WriteMessage("Unit:{0}\n", StreetBoxPhusedr[3]);
                            StreetBoxPhusedr[4] = 0;
                            //ed.WriteMessage("IsProduct:{0}\n", StreetBoxPhusedr[4]);
                            StreetBoxPhusedr[5] = 0;
                            //ed.WriteMessage("Price:{0}\n", StreetBoxPhusedr[5]);
                            StreetBoxPhusedr[6] = 0;
                            //ed.WriteMessage("ExecutePrice:{0}\n", StreetBoxPhusedr[6]);
                            StreetBoxPhusedr[7] = 0;
                            //ed.WriteMessage("WagePrice:{0}\n", StreetBoxPhusedr[7]);
                        }
                        DT.Rows.Add(StreetBoxPhusedr);
                        SubEquipment(Convert.ToInt32(StreetBoxPhuse["PhuseCode"].ToString()), (int)Atend.Control.Enum.ProductType.Phuse, ProjectCode, Exist, Convert.ToInt32(StreetBoxPhusedr[2]), Guid.Empty);
                    }
                    break;
                case Atend.Control.Enum.ProductType.Phuse:
                    Atend.Base.Equipment.EPhuse _EPhuse = Atend.Base.Equipment.EPhuse.AccessSelectByCode(Code, _ConnectionString.SingleConnectionAccess);
                    if (_EPhuse.Code != -1)
                    {
                        Atend.Base.Equipment.EPhusePole _EPhusePole = Atend.Base.Equipment.EPhusePole.AccessSelectByCode(_EPhuse.PhusePoleCode, _ConnectionString.SingleConnectionAccess);
                        if (_EPhusePole.Code != -1)
                        {
                            DataRow StreetBoxPhusedr = DT.NewRow();
                            StreetBoxPhusedr[8] = Exist;
                            //ed.WriteMessage("Exist:{0}\n", StreetBoxPhusedr[8]);
                            ////int pc = Atend.Base.Base.BProjectCode.AccessSelectByCode(ProjectCode).AdditionalCode;
                            StreetBoxPhusedr[9] = ProjectCode;
                            //ed.WriteMessage("ProjectCode:{0}\n", StreetBoxPhusedr[9]);
                            StreetBoxPhusedr[2] = 1 * ParentCount;
                            //ed.WriteMessage("Count:{0}\n", StreetBoxPhusedr[2]);
                            SProCode = _EPhusePole.Code;
                            StreetBoxPhusedr[10] = (int)Atend.Control.Enum.ProductType.PhusePole;
                            //ed.WriteMessage("Type:{0}\n", StreetBoxPhusedr[10]);
                            PID = 0;
                            StreetBoxPhusedr[1] = FindNameAndProductCode((int)Atend.Control.Enum.ProductType.PhusePole/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, SProCode,_ConnectionString.SingleConnectionAccess);
                            //ed.WriteMessage("Name:{0}\n", StreetBoxPhusedr[1]);
                            SProduct = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);
                            if (SProduct.Code != -1)
                            {
                                StreetBoxPhusedr[0] = SProduct.Code;
                                //ed.WriteMessage("Code:{0}\n", StreetBoxPhusedr[0]);
                                StreetBoxPhusedr[3] = SProduct.Unit;
                                //ed.WriteMessage("Unit:{0}\n", StreetBoxPhusedr[3]);
                                if (SProduct.IsProduct)
                                    StreetBoxPhusedr[4] = 1;
                                else
                                    StreetBoxPhusedr[4] = 0;
                                //ed.WriteMessage("IsProduct:{0}\n", StreetBoxPhusedr[4]);
                                StreetBoxPhusedr[5] = SProduct.Price;
                                //ed.WriteMessage("Price:{0}\n", StreetBoxPhusedr[5]);
                                StreetBoxPhusedr[6] = SProduct.ExecutePrice;
                                //ed.WriteMessage("ExecutePrice:{0}\n", StreetBoxPhusedr[6]);
                                StreetBoxPhusedr[7] = SProduct.WagePrice;
                                //ed.WriteMessage("WagePrice:{0}\n", StreetBoxPhusedr[7]);
                            }
                            else
                            {
                                StreetBoxPhusedr[0] = "----";
                                //ed.WriteMessage("Code:{0}\n", StreetBoxPhusedr[0]);
                                StreetBoxPhusedr[3] = "----";
                                //ed.WriteMessage("Unit:{0}\n", StreetBoxPhusedr[3]);
                                StreetBoxPhusedr[4] = 0;
                                //ed.WriteMessage("IsProduct:{0}\n", StreetBoxPhusedr[4]);
                                StreetBoxPhusedr[5] = 0;
                                //ed.WriteMessage("Price:{0}\n", StreetBoxPhusedr[5]);
                                StreetBoxPhusedr[6] = 0;
                                //ed.WriteMessage("ExecutePrice:{0}\n", StreetBoxPhusedr[6]);
                                StreetBoxPhusedr[7] = 0;
                                //ed.WriteMessage("WagePrice:{0}\n", StreetBoxPhusedr[7]);
                            }
                            DT.Rows.Add(StreetBoxPhusedr);
                            SubEquipment(_EPhusePole.Code, (int)Atend.Control.Enum.ProductType.PhusePole, ProjectCode, Exist, Convert.ToInt32(StreetBoxPhusedr[2]), Guid.Empty);
                        }
                        else
                        {
                            throw new System.Exception("PhusePole was not exist in status report");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Phuse was not exist in status report");
                    }
                    break;
            }
            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~~~~~~~END Contain IN Table~~~~~~~~~~~~~~~~~~~~~~~\n");

            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~In SubEquipment~~~~~~~~~~~~~~~~~~~~~\n");
            DataTable ContainerTbl = Atend.Base.Equipment.EContainerPackage.AccessSelectByContainerCodeAndTypeDT(Code, Type, _ConnectionString.SingleConnectionAccess);
            //ed.WriteMessage(" >> ContainerTableRowCount = {0}\n", ContainerTbl.Rows.Count);
            int _ProjectCode = 0;
            int _Exist = 0;
            foreach (DataRow Container in ContainerTbl.Rows)
            {
                DataTable ProductTbl = Atend.Base.Equipment.EProductPackage.AccessSelectByContainerPackageCode(Convert.ToInt32(Container["Code"].ToString()), _ConnectionString.SingleConnectionAccess);
                //ed.WriteMessage(" >> ProductTableRowCount = {0}\n", ProductTbl.Rows.Count);
                foreach (DataRow ProductRow in ProductTbl.Rows)
                {
                    bool _IsOneOfSwitchProducts = false;

                    if (ParentCode != Guid.Empty)
                    {
                        Atend.Base.Design.DPackage Pack = Atend.Base.Design.DPackage.AccessSelectByCode(ParentCode, _ConnectionString.SingleConnectionAccess);
                        if (Convert.ToInt16(ProductRow["TableType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Transformer))
                        {
                            if (Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost) || Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost))
                            {
                                DataTable DPT = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Pack.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Transformer), _ConnectionString.SingleConnectionAccess);
                                DataRow[] Row = DPT.Select(string.Format("ProductCode = {0} AND Type = {1}", ProductRow["ProductCode"].ToString(), ProductRow["TableType"].ToString()));
                                _ProjectCode = Convert.ToInt16(Row[0]["ProjectCode"].ToString());
                                _Exist = Convert.ToInt16(Row[0]["IsExistance"].ToString());
                                _IsOneOfSwitchProducts = true;
                            }
                        }

                        if (Convert.ToInt16(ProductRow["TableType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.MiddleJackPanel))
                        {
                            Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByCode(ParentCode, _ConnectionString.SingleConnectionAccess);
                            if (Package.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost) || Package.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost))
                            {
                                DataTable DPT = Atend.Base.Design.DPackage.AccessSelectByParentCode(Package.Code, _ConnectionString.SingleConnectionAccess);
                                DataRow[] Row = DPT.Select(string.Format("ProductCode = {0} AND Type = {1}", ProductRow["ProductCode"].ToString(), ProductRow["TableType"].ToString()));
                                _ProjectCode = Convert.ToInt16(Row[0]["ProjectCode"].ToString());
                                _Exist = Convert.ToInt16(Row[0]["IsExistance"].ToString());
                                _IsOneOfSwitchProducts = true;
                            }
                        }

                        if (Convert.ToInt16(ProductRow["TableType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.WeekJackPanel))
                        {
                            if (Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost) || Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost))
                            {
                                DataTable DPT = Atend.Base.Design.DPackage.AccessSelectByParentCode(Pack.Code, _ConnectionString.SingleConnectionAccess);
                                DataRow[] Row = DPT.Select(string.Format("ProductCode = {0} AND Type = {1}", ProductRow["ProductCode"].ToString(), ProductRow["TableType"].ToString()));
                                _ProjectCode = Convert.ToInt16(Row[0]["ProjectCode"].ToString());
                                _Exist = Convert.ToInt16(Row[0]["IsExistance"].ToString());
                                _IsOneOfSwitchProducts = true;
                            }
                        }

                        if (Convert.ToInt16(ProductRow["TableType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Halter))
                        {
                            if (Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.PoleTip))
                            {
                                DataTable DPT = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Pack.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Halter), _ConnectionString.SingleConnectionAccess);
                                DataRow[] Row = DPT.Select(string.Format("ProductCode = {0} AND Type = {1}", ProductRow["ProductCode"].ToString(), ProductRow["TableType"].ToString()));
                                _ProjectCode = Convert.ToInt16(Row[0]["ProjectCode"].ToString());
                                _Exist = Convert.ToInt16(Row[0]["IsExistance"].ToString());
                                _IsOneOfSwitchProducts = true;
                            }
                        }

                        if (Convert.ToInt16(ProductRow["TableType"].ToString()) == Convert.ToInt16(Atend.Control.Enum.ProductType.Consol))
                        {
                            if (Pack.Type == Convert.ToInt16(Atend.Control.Enum.ProductType.PoleTip))
                            {
                                DataTable DPT = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(Pack.Code, Convert.ToInt16(Atend.Control.Enum.ProductType.Consol), _ConnectionString.SingleConnectionAccess);
                                DataRow[] Row = DPT.Select(string.Format("ProductCode = {0} AND Type = {1}", ProductRow["ProductCode"].ToString(), ProductRow["TableType"].ToString()));
                                _ProjectCode = Convert.ToInt16(Row[0]["ProjectCode"].ToString());
                                _Exist = Convert.ToInt16(Row[0]["IsExistance"].ToString());
                                _IsOneOfSwitchProducts = true;
                            }
                        }
                    }

                    DataRow dr = DT.NewRow();
                    if (_IsOneOfSwitchProducts)
                    {
                        dr[8] = _Exist;
                        dr[9] = _ProjectCode;
                    }
                    else
                    {
                        dr[8] = Exist;
                        dr[9] = ProjectCode;
                    }
                    dr[2] = Convert.ToDouble(ProductRow["Count"]) * ParentCount;
                    int ProCode = Convert.ToInt32(ProductRow["ProductCode"].ToString());
                    dr[10] = Convert.ToInt16(ProductRow["TableType"].ToString());
                    PID = 0;
                    dr[1] = FindNameAndProductCode(Convert.ToInt16(ProductRow["TableType"].ToString())/*Convert.ToInt32(Childs.Rows[i]["Type"].ToString())*/, ProCode,_ConnectionString.SingleConnectionAccess);
                    //ed.WriteMessage("Name:{0}\n", dr[1]);
                    //ed.WriteMessage("Exist:{0}\n", dr[8]);
                    //ed.WriteMessage("ProjectCode:{0}\n", dr[9]);
                    //ed.WriteMessage("Count:{0}\n", dr[2]);
                    //ed.WriteMessage("Type:{0}\n", dr[10]);

                    Atend.Base.Base.BProduct Product = Atend.Base.Base.BProduct.Select_ByIdX(PID, _ConnectionString.SingleSqlConnectionLocal);

                    if (Product.Code != -1)
                    {
                        dr[0] = Product.Code;
                        dr[3] = Product.Unit;
                        if (Product.IsProduct)
                            dr[4] = 1;
                        else
                            dr[4] = 0;
                        dr[5] = Product.Price;
                        dr[6] = Product.ExecutePrice;
                        dr[7] = Product.WagePrice;
                    }
                    else
                    {
                        dr[0] = "----";
                        dr[3] = "----";
                        dr[4] = 0;
                        dr[5] = 0;
                        dr[6] = 0;
                        dr[7] = 0;
                    }
                    //ed.WriteMessage("Code:{0}\n", dr[0]);
                    //ed.WriteMessage("Unit:{0}\n", dr[3]);
                    //ed.WriteMessage("IsProduct:{0}\n", dr[4]);
                    //ed.WriteMessage("Price:{0}\n", dr[5]);
                    //ed.WriteMessage("ExecutePrice:{0}\n", dr[6]);
                    //ed.WriteMessage("WagePrice:{0}\n", dr[7]);

                    DT.Rows.Add(dr);
                    Guid a = ParentCode;
                    ParentCode = Guid.Empty;
                    if (_IsOneOfSwitchProducts)
                    {
                        SubEquipment(Convert.ToInt32(ProductRow["ProductCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), _ProjectCode, _Exist, Convert.ToInt32(dr[2]), ParentCode);
                    }
                    else
                    {
                        SubEquipment(Convert.ToInt32(ProductRow["ProductCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), ProjectCode, Exist, Convert.ToInt32(dr[2]), ParentCode);
                    }
                    ParentCode = a;
                }
            }
            //ed.WriteMessage("\n~~~~~~~~~~~~~~~~~~~~END SubEquipment~~~~~~~~~~~~~~~~~~~~~\n");
        }

        //#endregion
    }
}
