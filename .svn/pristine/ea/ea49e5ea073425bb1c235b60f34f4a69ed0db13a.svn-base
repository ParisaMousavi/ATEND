using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Global.Utility
{
    public class UOtherOutPut
    {
        public static string ChangeToShamsi(DateTime MiladiDate)
        {
            System.Globalization.PersianCalendar _PersianCalendar = new System.Globalization.PersianCalendar();
            int Year = _PersianCalendar.GetYear(MiladiDate);
            string Month = _PersianCalendar.GetMonth(MiladiDate).ToString();
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }

            string Day = _PersianCalendar.GetDayOfMonth(MiladiDate).ToString();
            if (Day.Length == 1)
            {
                Day = "0" + Day;
            }

            string Answer = string.Format("{0}/{1}/{2}", Year, Month, Day);
            return Answer;
        }

        public Atend.Report.dsSagAndTension FillRemark()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Report.dsSagAndTension dsloadbranch = new Atend.Report.dsSagAndTension();
            Atend.Base.Design.DRemark remark = Atend.Base.Design.DRemark.AccessSelectByCode(1);
            if (remark.Code != -1)
            {
                DataRow drRemark = dsloadbranch.Tables["Remark"].NewRow();
                drRemark["File"] = (byte[])remark.File;
                dsloadbranch.Tables["Remark"].Rows.Add(drRemark);



                //crRemark Remark = new crRemark();
                //Remark.SetDataSource(dsloadbranch);
                //crViewerRemark.ReportSource = Remark;
            }
            else
            {
                ed.WriteMessage("در Remark مقداری وجود ندارد\n");
            }

            DataRow dr1 = dsloadbranch.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

            if (designProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(designProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(designProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = designProfile.Designer;
                dr1["ProjectName"] = designProfile.DesignName;//Atend.Control.Common.DesignName;
                dr1["Credit"] = designProfile.Validate;
                //dr1["SectionCount"] = arrSection.Count.ToString();
                //dr1["PoleCount"] = CountPole.ToString();
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;

                dsloadbranch.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }


            return dsloadbranch;
        }

        public Atend.Report.dsSagAndTension FillStatusReport()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Report.dsSagAndTension ds = new Atend.Report.dsSagAndTension();
            ed.WriteMessage("#### 0\n");
            Atend.Global.Utility.UReport Report = new Atend.Global.Utility.UReport();
            ed.WriteMessage("#### 0.1\n");
            System.Data.DataTable Table = Report.CreateExcelStatus();
            //ed.WriteMessage("ST Report2 - table Row Count = " + Table.Rows.Count.ToString() + "\n");
            ed.WriteMessage("#### 1\n");
            DataRow dr1 = ds.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            ed.WriteMessage("#### 2\n");
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                //ed.WriteMessage();
                dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                dr1["Credit"] = desProfile.Validate;
                //dr1["SectionCount"] = arrSection.Count.ToString();
                //dr1["PoleCount"] = CountPole.ToString();
                ed.WriteMessage(string.Format(@" >> {0}\SupportFiles\default.JPG ", Atend.Control.Common.fullPath) + "\n");
                ed.WriteMessage(string.Format(@" >> {0} ", Atend.Control.ConnectionString.LogoPath) + "\n");
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                //else
                //{
                //    //MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                //    //ed.WriteMessage("2 \n");
                //    if (System.IO.File.Exists(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath)))
                //    {
                //        System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                //        byte[] reader11 = new byte[FS.Length];
                //        FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                //        dr1["Logo"] = reader11;
                //        FS.Close();
                //    }
                //    else
                //    {
                //        ed.WriteMessage("file was not exist in this path : " + string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath) + "\n");
                //    }
                //}
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;
                ds.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                // MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                ed.WriteMessage("#### 3\n");
                return null;
            }
            //____________________________________________________
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic.Add("Code", "كد كالا");
            Dic.Add("Name", "نام كالا");
            Dic.Add("Count", "تعداد");
            Dic.Add("Exist", "نوع صورت وضعیت");
            Dic.Add("ProjectCode", "کد دستور کار تجهیز");
            Dic.Add("Unit", "واحد كالا");
            Dic.Add("Price", "قیمت واحد");
            Dic.Add("ExecutePrice", "قیمت اجرا");
            Dic.Add("WagePrice", "قیمت دستمزد");
            Dic.Add("ProjectName1", "شرح دستور کار");
            ed.WriteMessage("#### 4\n");
            Atend.Base.Design.DDesignProfile DP = Atend.Base.Design.DDesignProfile.AccessSelect();
            ed.WriteMessage("#### 5\n");
            System.Data.DataTable UnitTable = Atend.Base.Base.BUnit.SelectAll();
            ed.WriteMessage("#### 6\n");
            try
            {
                ed.WriteMessage("#### 7 , {0}\n", Table.Rows.Count);
                for (int i = 0; i < Table.Rows.Count; i++)
                {
                    if (Atend.Control.NumericValidation.Int32Converter(Table.Rows[i]["Unit"].ToString()))
                    {
                        DataRow[] drs = UnitTable.Select(string.Format("Code={0}", Convert.ToInt32(Table.Rows[i]["Unit"])));
                        if (drs.Length > 0)
                        {
                            Table.Rows[i]["Unit"] = drs[0]["Name"];
                        }
                    }
                    Table.Rows[i]["Exist"] = Atend.Base.Base.BEquipStatus.SelectByACode(Convert.ToInt32(Table.Rows[i]["Exist"])).Name;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error : {0} \n", ex2.Message));
            }//
            ed.WriteMessage("#### 8\n");
            //ds.StatusReport = Table;
            int Sum = 0;
            //DataColumn DC = new DataColumn("Sum", typeof(int));
            //Table.Columns.Add(DC);


            ed.WriteMessage("#### 9\n");

            foreach (DataRow dr in Table.Rows)
            {
                //ed.WriteMessage("\n Name StRep = {0}", dr["Name"].ToString());
                DataRow drStatusReport = ds.Tables["StatusReport"].NewRow();

                drStatusReport["Code"] = dr["Code"].ToString();
                drStatusReport["Name"] = dr["Name"].ToString().PadRight(dr["Name"].ToString().Length);
                drStatusReport["Count"] = dr["Count"].ToString();
                drStatusReport["Unit"] = dr["Unit"].ToString();
                drStatusReport["Price"] = dr["Price"].ToString();
                drStatusReport["ExecutePrice"] = dr["ExecutePrice"].ToString();
                drStatusReport["WagePrice"] = dr["WagePrice"].ToString();
                drStatusReport["Exist"] = dr["Exist"].ToString();
                drStatusReport["ProjectCode"] = dr["ProjectCode"].ToString();
                drStatusReport["ProjectName1"] = dr["ProjectName1"].ToString();
                //            ed.WriteMessage(" >>>>> Total:{0} \nPrice:{1} \nExecutePrice:{2} \nWagePrice:{3} \nCount: {4}\n",
                //drStatusReport["Total"],
                //(dr["Price"]),
                //(dr["ExecutePrice"]),
                //(dr["WagePrice"]),
                //(dr["Count"].ToString()));
                drStatusReport["Total"] =
                    (Convert.ToDouble(dr["Price"]) +
                     Convert.ToDouble(dr["ExecutePrice"]) +
                     Convert.ToDouble(dr["WagePrice"])) *
                     Convert.ToDouble(dr["Count"].ToString());
                //ed.WriteMessage("  <<<<  Total:{0} \nPrice:{1} \nExecutePrice:{2} \nWagePrice:{3} \nCount: {4}\n",
                //    drStatusReport["Total"],
                //    (dr["Price"]),
                //    (dr["ExecutePrice"]),
                //    (dr["WagePrice"]),
                //    (dr["Count"].ToString()));

                drStatusReport["AreaCoeff"] = 1;
                Sum += (Convert.ToInt32(drStatusReport["Total"].ToString()));
                ds.Tables["StatusReport"].Rows.Add(drStatusReport);
            }

            for (int i = 0; i < ds.Tables["StatusReport"].Rows.Count; i++)
            {
                ds.Tables["StatusReport"].Rows[i]["Sum"] = Sum;
            }
            return ds;
        }

        public Atend.Report.dsSagAndTension FillGisForm1()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Report.dsSagAndTension ds = new Atend.Report.dsSagAndTension();
            //Atend.Report.dsSagAndTension.GISForm1DataTable _GISForm1DataTable = new Atend.Report.dsSagAndTension.GISForm1DataTable();


            //System.Data.OleDb.OleDbConnection aconnection = new System.Data.OleDb.OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            Atend.Control.ConnectionString _ConnectionString = new Atend.Control.ConnectionString();
            _ConnectionString.OpenSingleConnectionAccess();
            DataTable dtPoles=Atend.Base.Design.DNode.AccessSelectAll(_ConnectionString.SingleConnectionAccess);
            foreach (DataRow dr in dtPoles.Rows)
            {
                Atend.Base.Equipment.EPole _EPole = Atend.Base.Equipment.EPole.AccessSelectByCode(Convert.ToInt32(dr["ProductCode"]), _ConnectionString.SingleConnectionAccess);
                DataRow drGISForm1 = ds.Tables["GISForm1"].NewRow();

                drGISForm1["PoleNum"] = dr["Number"].ToString();
                drGISForm1["X"] = "QQQ";
                drGISForm1["Y"] = "QQQ";
                drGISForm1["High"] = _EPole.Height.ToString();
                drGISForm1["Tension"] = "QQQ";
                drGISForm1["Kind"] = "QQQ";

                switch (_EPole.Type)
                {
                    case 0:
                        drGISForm1["PoleType"] = "بتونی";
                        break;
                    case 1:
                        drGISForm1["PoleType"] = "فلزی";
                        break;
                    case 2:
                        drGISForm1["PoleType"] = "تلسکوپی";
                        break;
                    case 3:
                        drGISForm1["PoleType"] = "چوبی";
                        break;
                }
                drGISForm1["OrderType"] = "QQQ";
                drGISForm1["InsulatorCount"] = "QQQ";
                drGISForm1["CircuitCount"] = "QQQ";
                drGISForm1["CrossSection"] = "QQQ";
                drGISForm1["BranchType"] = "QQQ";
                drGISForm1["ProductType"] = "QQQ";
                drGISForm1["Constructor"] = "QQQ";
                drGISForm1["I"] = "QQQ";

                ds.Tables["GISForm1"].Rows.Add(drGISForm1);
            }

            _ConnectionString.CloseSingleConnectionAccess();
            return ds;

        }
    }
}
