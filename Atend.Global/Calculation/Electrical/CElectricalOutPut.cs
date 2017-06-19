using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Global.Calculation.Electrical
{
    public class CElectricalOutPut
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

        public Atend.Report.dsSagAndTension FillLoadNode()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\n -------------- Enter Fill Node------------------\n");

            DataTable dtLoadNode = Atend.Base.Calculating.CDistributedLoadNode.AccessSelectAll();
            Atend.Report.dsSagAndTension dsloadnode = new Atend.Report.dsSagAndTension();
            ArrayList arrSection = new ArrayList();

            foreach (DataRow dr in dtLoadNode.Rows)
            {
                DataRow drLoadNode = dsloadnode.Tables["Node"].NewRow();
                drLoadNode["NodeName"] = dr["NodeName"].ToString();
                drLoadNode["VoltAbs"] = dr["VoltAbs"].ToString();
                drLoadNode["VoltArg"] = dr["VoltArg"].ToString();
                drLoadNode["DropVolt"] = dr["DropVolt"].ToString();
                drLoadNode["LoadPowerActive"] = dr["LoadPowerActive"].ToString();
                drLoadNode["LoadPower"] = dr["LoadPower"].ToString();
                drLoadNode["LoadCurrnetABS"] = dr["LoadCurrentAbs"].ToString();
                drLoadNode["LoadCurrentArg"] = dr["LoadCurrentArg"].ToString();
                dsloadnode.Tables["Node"].Rows.Add(drLoadNode);
            }

            DataRow dr1 = dsloadnode.Tables["Title"].NewRow();
            Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

            //ed.WriteMessage("\n -----------{0}--------------\n", designProfile.Id);
            if (designProfile.Id != 0)
            {
                ed.WriteMessage("\n -------------- Enter Node Title------------------\n");
                Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(designProfile.Zone);
                dr1["Area"] = b12.SecondCode;
                System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                string _date = ChangeToShamsi(designProfile.DesignDate);
                dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                dr1["Designer"] = designProfile.Designer;
                dr1["ProjectName"] = designProfile.DesignName;//Atend.Control.Common.DesignName;
                //ed.WriteMessage("\n -----------{0}--------------\n", dr1["ProjectName"].ToString());
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
                dsloadnode.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }
            //DataRow drDesign = dsloadnode.Tables["Title"].NewRow();
            //drDesign["ProjectName"] = designProfile.DesignName;
            //drDesign["Designer"] = designProfile.Designer;
            //drDesign["Area"] = designProfile.Zone;
            //drDesign["Date"] = designProfile.DesignDate.ToString();
            //dsloadnode.Tables["Title"].Rows.Add(drDesign);

            //Atend.Report.frmNodeReport nodeReport = new frmNodeReport();
            //nodeReport.SetDataset(dsloadnode);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(nodeReport);
            return dsloadnode;

        }

        public Atend.Report.dsSagAndTension FillLoadBranch()
        {
            DataTable dtLoadBranch = Atend.Base.Calculating.CDistributedLoadBranch.AccessSelectAll();
            Atend.Report.dsSagAndTension dsloadbranch = new Atend.Report.dsSagAndTension();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\n -------------- Branch Row Count = {0} \n" , dtLoadBranch.Rows.Count.ToString());

            foreach (DataRow dr in dtLoadBranch.Rows)
            {
                DataRow drLoadBranch = dsloadbranch.Tables["Branch"].NewRow();
                drLoadBranch["BranchName"] = dr["BranchName"].ToString();
                drLoadBranch["Lenght"] = dr["Lenght"].ToString();
                drLoadBranch["CurrnetABS"] = dr["CurrentAbs"].ToString();
                drLoadBranch["CurrentArg"] = dr["CurrentArg"].ToString();
                drLoadBranch["Condutilization"] = dr["CondUtilization"].ToString();
                drLoadBranch["TotalLoad"] = dr["TotalLoad"].ToString();
                dsloadbranch.Tables["Branch"].Rows.Add(drLoadBranch);
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
            //Atend.Report.frmBranchReport BranchReport = new frmBranchReport();
            //BranchReport.SetDataset(dsloadbranch);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(BranchReport);
            return dsloadbranch;

        }

        public Atend.Report.dsSagAndTension FillCrossSection()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtCross = Atend.Base.Calculating.CCrossSection.AccessSelectAll();
            Atend.Report.dsSagAndTension dscrossSection = new Atend.Report.dsSagAndTension();

            foreach (DataRow dr in dtCross.Rows)
            {
                DataRow drSection = dscrossSection.Tables["CrossSection"].NewRow();
                //ed.WriteMessage("FROM={0}\n", dr["From"].ToString());
                drSection["From"] = dr["From"].ToString();
                drSection["To"] = dr["To"].ToString();
                drSection["NameExist"] = dr["ExistCond"].ToString();
                drSection["Lenght"] = dr["Lenght"].ToString();
                drSection["NameComment"] = dr["CommentCond"].ToString();
                drSection["CrossSection"] = dr["CrossSection"].ToString();
                drSection["Current"] = dr["Current"].ToString();
                drSection["Volt"] = dr["Volt"].ToString();
                dscrossSection.Tables["CrossSection"].Rows.Add(drSection);
            }

            DataRow dr1 = dscrossSection.Tables["Title"].NewRow();
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
                dscrossSection.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }
            //Atend.Report.frmCrossSectionReport report = new frmCrossSectionReport();
            //report.SetDataset(dscrossSection);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(report);
            return dscrossSection;
        }

        public Atend.Report.dsSagAndTension FillTrans()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtTransCurrent = Atend.Base.Calculating.CTransCurrent.AccessSelectAll();
            Atend.Report.dsSagAndTension dsTransCurrent = new Atend.Report.dsSagAndTension();

            foreach (DataRow dr in dtTransCurrent.Rows)
            {
                DataRow drCurrent = dsTransCurrent.Tables["LoadCurrent"].NewRow();
                drCurrent["I"] = dr["I"].ToString();
                drCurrent["PF"] = dr["PF"].ToString();
                drCurrent["Count"] = dr["BranchCount"].ToString();
                drCurrent["CF"] = dr["CF"].ToString();
                dsTransCurrent.Tables["LoadCurrent"].Rows.Add(drCurrent);
            }
            //ed.WriteMessage("PASS LoadCurrent\n");
            DataTable dtLoadPower = Atend.Base.Calculating.CTransPower.AccessSelectAll();
            foreach (DataRow dr in dtLoadPower.Rows)
            {
                DataRow drPower = dsTransCurrent.Tables["LoadPower"].NewRow();
                drPower["P"] = dr["P"].ToString();
                drPower["PF"] = dr["PF"].ToString();
                drPower["Count"] = dr["BranchCount"].ToString();
                drPower["CF"] = dr["CF"].ToString();
                dsTransCurrent.Tables["LoadPower"].Rows.Add(drPower);
            }
            //ed.WriteMessage("PASS LOADPOWER\n");
            DataTable dtLoadBranch = Atend.Base.Calculating.CTransBranch.AccessSelectAll();
            foreach (DataRow dr in dtLoadBranch.Rows)
            {
                DataRow drBranch = dsTransCurrent.Tables["LoadBranch"].NewRow();
                drBranch["I"] = dr["I"].ToString();
                drBranch["PF"] = dr["PF"].ToString();
                drBranch["BranchCount"] = dr["BranchCount"].ToString();
                drBranch["PhuseCount"] = dr["PhaseCount"].ToString();
                drBranch["CF"] = dr["CF"].ToString();
                dsTransCurrent.Tables["LoadBranch"].Rows.Add(drBranch);
            }
            //ed.WriteMessage("PASS BranchName\n");
            DataTable dtInput = Atend.Base.Calculating.CTransformer.AccessSelectAll();
            //ed.WriteMessage("PassSelectROWS.Count={0}\n", dtInput.Rows.Count);
            foreach (DataRow dr in dtInput.Rows)
            {
                DataRow drInput = dsTransCurrent.Tables["InputInfoTranse"].NewRow();
                //ed.WriteMessage("Build Row\n");
                drInput["Height"] = dr["Height"].ToString();
                //ed.WriteMessage("H\n");
                drInput["Load"] = dr["Load"].ToString();
                //ed.WriteMessage("L\n");
                drInput["Result"] = dr["Result"].ToString();
                //ed.WriteMessage("R\n");
                drInput["PowerLoss"] = "0";
                //ed.WriteMessage("P\n");
                dsTransCurrent.Tables["InputInfoTranse"].Rows.Add(drInput);

            }
            //ed.WriteMessage("Pass INPUTINFO TrANS\n");
            DataRow dr1 = dsTransCurrent.Tables["Title"].NewRow();
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
                dsTransCurrent.Tables["Title"].Rows.Add(dr1);
            }
            else
            {
                MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                return null;
            }

            ed.WriteMessage("GO TO Page\n");
            //Atend.Report.frmTranseInfoReport report = new frmTranseInfoReport();
            //report.SetDataset(dsTransCurrent);
            //Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(report);
            return dsTransCurrent;


        }


    }
}
