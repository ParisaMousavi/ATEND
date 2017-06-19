using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Global.Calculation.Mechanical
{
    public class CMecanicalOutPut
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

        public Atend.Report.dsSagAndTension FillSagAndTension(bool IsUTS)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //System.Windows.Forms.MessageBox.Show("salam");
            ed.WriteMessage("-2\n");
            ArrayList arrSection = new ArrayList();
            ed.WriteMessage("-1\n");
            bool chk = true;

            ed.WriteMessage("0\n");
            DataTable dtSagtension = Atend.Base.Calculating.CSagAndTension.AccessSelectByIsUTS(IsUTS);
            ed.WriteMessage("1\n");
            Atend.Report.dsSagAndTension dsSagTensionReport = new Atend.Report.dsSagAndTension();
            ed.WriteMessage("2\n");
            int CountPole = 0;
            ed.WriteMessage("3\n");
            for (int i = 0; i < arrSection.Count; i++)
            {
                DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                CountPole += dtpoleSec.Rows.Count;
                DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                CountPole += dtpoleSec1.Rows.Count;
            }
            ed.WriteMessage("4\n");
            DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
            ed.WriteMessage("5\n");
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            ed.WriteMessage("6\n");
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                dr1["ProjectName"] = desProfile.DesignName;//Atend.Control.Common.DesignName;
                dr1["SectionCount"] = arrSection.Count.ToString();
                dr1["PoleCount"] = CountPole.ToString();
                dr1["Credit"] = desProfile.Validate;
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;

                dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }
            ed.WriteMessage("7\n");

            foreach (DataRow dr in dtSagtension.Rows)
            {
                ed.WriteMessage("FillSagTension\n");
                DataRow drSagTension = dsSagTensionReport.Tables["SagAndTension"].NewRow();
                Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
                Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                Atend.Base.Calculating.CDefaultMec DefMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), IsUTS);
                drSagTension["SectionCode"] = dr["Number"].ToString();
                drSagTension["StartPole"] = dPackStart.Number;
                drSagTension["EndPole"] = dPackEnd.Number;
                drSagTension["NormH"] = dr["NormH"].ToString();
                drSagTension["NormF"] = dr["NormF"].ToString();
                drSagTension["WindAndIceH"] = dr["WindAndIceH"].ToString();
                drSagTension["WindAndIceF"] = dr["WindAndIceF"].ToString();
                drSagTension["MinTempH"] = dr["MinTempH"].ToString();
                drSagTension["MinTempF"] = dr["MinTempF"].ToString();
                drSagTension["MaxTempH"] = dr["MaxTempH"].ToString();
                drSagTension["MaxTempF"] = dr["MaxTempF"].ToString();
                drSagTension["WindH"] = dr["WindH"].ToString();
                drSagTension["WindF"] = dr["WindF"].ToString();
                drSagTension["IceH"] = dr["IceH"].ToString();
                drSagTension["IceF"] = dr["IceF"].ToString();
                drSagTension["CondName"] = dr["ConductorName"].ToString();
                drSagTension["Span"] = DefMec.SE;

                dsSagTensionReport.Tables["SagAndTension"].Rows.Add(drSagTension);

                chk = true;
                for (int i = 0; i < arrSection.Count; i++)
                {
                    if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
                    {
                        chk = false;
                    }
                }
                if (chk)
                {
                    arrSection.Add(dr["SectionCode"].ToString());
                }

            }
            ed.WriteMessage("8\n");
            return dsSagTensionReport;

        }

        public Atend.Report.dsSagAndTension FillPowerWithOutHalter(bool IsUts)
        {
            ArrayList arrSection = new ArrayList();
            bool chk = true;

            DataTable dtPowerWithOutHalter = Atend.Base.Calculating.CPowerWithOutHalter.AccessSelectByISUTS(IsUts);
            Atend.Report.dsSagAndTension dsSagTensionReport = new Atend.Report.dsSagAndTension();

            int CountPole = 0;
            for (int i = 0; i < arrSection.Count; i++)
            {
                DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                CountPole += dtpoleSec.Rows.Count;
                DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                CountPole += dtpoleSec1.Rows.Count;
            }

            DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                dr1["ProjectName"] = desProfile.DesignName;//Atend.Control.Common.DesignName;
                dr1["SectionCount"] = arrSection.Count.ToString();
                dr1["Credit"] = desProfile.Validate;
                dr1["PoleCount"] = CountPole.ToString();
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;

                dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }


            foreach (DataRow dr in dtPowerWithOutHalter.Rows)
            {
                DataRow drPower = dsSagTensionReport.Tables["PoleWithOutHalter"].NewRow();
                //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
                //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                drPower["SectionNumber"] = dr["Number"].ToString();
                drPower["PoleNum"] = dr["PoleNum"].ToString();
                drPower["Power"] = dr["PolePower"].ToString();
                drPower["Count"] = dr["PoleCount"].ToString();
                dsSagTensionReport.Tables["PoleWithOutHalter"].Rows.Add(drPower);

                chk = true;
                for (int i = 0; i < arrSection.Count; i++)
                {
                    if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
                    {
                        chk = false;
                    }
                }
                if (chk)
                {
                    arrSection.Add(dr["SectionCode"].ToString());
                }

            }
            //MessageBox.Show(dsSagTensionReport.Tables["SagAndTension"].Rows.Count.ToString());

            //Atend.Report.frmPoleWithoutHalterReport frm = new frmPoleWithoutHalterReport();
            //frm.SetDataset(dsSagTensionReport);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
            return dsSagTensionReport;
        }

        public Atend.Report.dsSagAndTension FillPowerWithHalter(bool IsUts)
        {
            ArrayList arrSection = new ArrayList();
            bool chk = true;

            DataTable dtPowerWithHalter = Atend.Base.Calculating.CPowerWithHalter.AccessSelectByISUTS(IsUts);
            Atend.Report.dsSagAndTension dsSagTensionReport = new Atend.Report.dsSagAndTension();

            int CountPole = 0;
            for (int i = 0; i < arrSection.Count; i++)
            {
                DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                CountPole += dtpoleSec.Rows.Count;
                DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                CountPole += dtpoleSec1.Rows.Count;
            }

            DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                dr1["SectionCount"] = arrSection.Count.ToString();
                dr1["Credit"] = desProfile.Validate;
                dr1["PoleCount"] = CountPole.ToString();
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;

                dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                // MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }


            foreach (DataRow dr in dtPowerWithHalter.Rows)
            {
                DataRow drPower = dsSagTensionReport.Tables["PoleWithHalter"].NewRow();
                //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
                //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                drPower["SectionNumber"] = dr["Number"].ToString();
                drPower["PoleNum"] = dr["PoleNum"].ToString();
                drPower["Power"] = dr["PolePower"].ToString();
                drPower["Count"] = dr["PoleCount"].ToString();
                drPower["Name"] = dr["HalterName"].ToString();
                drPower["HalterPower"] = dr["HalterPower"].ToString();
                drPower["HalterCount"] = dr["HalterCount"].ToString();
                dsSagTensionReport.Tables["PoleWithHalter"].Rows.Add(drPower);

                chk = true;
                for (int i = 0; i < arrSection.Count; i++)
                {
                    if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
                    {
                        chk = false;
                    }
                }
                if (chk)
                {
                    arrSection.Add(dr["SectionCode"].ToString());
                }

            }
            //MessageBox.Show(dsSagTensionReport.Tables["SagAndTension"].Rows.Count.ToString());

            //Atend.Report.frmPoleWithHalterReport frm = new frmPoleWithHalterReport();
            //frm.SetDataset(dsSagTensionReport);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
            return dsSagTensionReport;
        }

        public Atend.Report.dsSagAndTension FillForcePole(bool IsUTS)
        {
            ArrayList arrSection = new ArrayList();
            bool chk = true;

            DataTable dtForcePole = Atend.Base.Calculating.CWindOnPole.AccessSelectByISUTS(IsUTS);
            Atend.Report.dsSagAndTension dsSagTensionReport = new Atend.Report.dsSagAndTension();

            int CountPole = 0;
            for (int i = 0; i < arrSection.Count; i++)
            {
                DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                CountPole += dtpoleSec.Rows.Count;
                DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                CountPole += dtpoleSec1.Rows.Count;
            }

            DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);

                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                dr1["SectionCount"] = arrSection.Count.ToString();
                dr1["Credit"] = desProfile.Validate;
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

                dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                // MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }


            foreach (DataRow dr in dtForcePole.Rows)
            {
                DataRow drForcePole = dsSagTensionReport.Tables["PoleForce"].NewRow();
                //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
                //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                drForcePole["SectionNumber"] = dr["Number"].ToString();
                drForcePole["PoleNumber"] = dr["DcPole"].ToString();
                drForcePole["Normal"] = dr["DcNorm"].ToString();
                drForcePole["Ice"] = dr["DcIceHeavy"].ToString();
                drForcePole["Wind"] = dr["DcWindSpeed"].ToString();
                drForcePole["MaxTemp"] = dr["DcMaxTemp"].ToString();
                drForcePole["MinTemp"] = dr["DcMinTemp"].ToString();
                drForcePole["IceWind"] = dr["DcWindIce"].ToString();

                dsSagTensionReport.Tables["PoleForce"].Rows.Add(drForcePole);

                chk = true;
                for (int i = 0; i < arrSection.Count; i++)
                {
                    if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
                    {
                        chk = false;
                    }
                }
                if (chk)
                {
                    arrSection.Add(dr["SectionCode"].ToString());
                }

            }
            return dsSagTensionReport;
        }

        public Atend.Report.dsSagAndTension FillRudSurface()
        {
            ArrayList arrSection = new ArrayList();
            bool chk = true;

            DataTable dtrudSurface = Atend.Base.Calculating.CRudSurface.AccessSelect();
            Atend.Report.dsSagAndTension dsSagTensionReport = new Atend.Report.dsSagAndTension();

            int CountPole = 0;
            for (int i = 0; i < arrSection.Count; i++)
            {
                DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                CountPole += dtpoleSec.Rows.Count;
                DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                CountPole += dtpoleSec1.Rows.Count;
            }

            DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
            if (desProfile.Id != 0)
            {
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(desProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(desProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = desProfile.Designer;
                dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
                dr1["SectionCount"] = arrSection.Count.ToString();
                dr1["Credit"] = desProfile.Validate;
                dr1["PoleCount"] = CountPole.ToString();
                if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                {
                    System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                    byte[] reader = new byte[FS.Length];
                    FS.Read(reader, 0, Convert.ToInt32(FS.Length));
                    dr1["Logo"] = reader;
                    FS.Close();
                }
                dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;

                dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                // MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }


            foreach (DataRow dr in dtrudSurface.Rows)
            {
                DataRow drForcePole = dsSagTensionReport.Tables["RudSurface"].NewRow();
                //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
                //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
                //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
                drForcePole["SectionNumber"] = dr["Number"].ToString();
                drForcePole["PoleNumber"] = dr["DcPole"].ToString();
                drForcePole["Normal"] = dr["DcNorm"].ToString();
                drForcePole["Ice"] = dr["DcIceHeavy"].ToString();
                drForcePole["Wind"] = dr["DcWindSpeed"].ToString();
                drForcePole["MaxTemp"] = dr["DcMaxTemp"].ToString();
                drForcePole["MinTemp"] = dr["DcMinTemp"].ToString();
                drForcePole["IceWind"] = dr["DcWindIce"].ToString();

                dsSagTensionReport.Tables["RudSurface"].Rows.Add(drForcePole);

                chk = true;
                for (int i = 0; i < arrSection.Count; i++)
                {
                    if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
                    {
                        chk = false;
                    }
                }
                if (chk)
                {
                    arrSection.Add(dr["SectionCode"].ToString());
                }

            }

            //MessageBox.Show(dsSagTensionReport.Tables["RudSurface"].Rows.Count.ToString());

            //Atend.Report.frmRudSurfaceReport frm = new frmRudSurfaceReport();
            //frm.SetDataset(dsSagTensionReport);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
            return dsSagTensionReport;
        }

    }
}
