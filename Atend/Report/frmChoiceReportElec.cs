using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Report
{
    public partial class frmChoiceReportElec : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        public frmChoiceReportElec()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nchecking.....\n");
            if (!Atend.Global.Acad.DrawEquips.Dicision.IsHere())
            {
                if (!Atend.Global.Acad.DrawEquips.Dicision.IsThere())
                {
                    System.Diagnostics.Process[] prs = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process pr in prs)
                    {
                        if (pr.ProcessName == "acad")
                        {
                            pr.CloseMainWindow();
                        }
                    }
                }
            }
            InitializeComponent();
        }

        //private void FillLoadNode()
        //{
        //    DataTable dtLoadNode = Atend.Base.Calculating.CDistributedLoadNode.AccessSelectAll();
        //    dsSagAndTension dsloadnode = new dsSagAndTension();

        //    foreach (DataRow dr in dtLoadNode.Rows)
        //    {
        //        DataRow drLoadNode = dsloadnode.Tables["Node"].NewRow();
        //        drLoadNode["NodeName"] = dr["NodeName"].ToString();
        //        drLoadNode["VoltAbs"] = dr["VoltAbs"].ToString();
        //        drLoadNode["VoltArg"] = dr["VoltArg"].ToString();
        //        drLoadNode["DropVolt"] = dr["DropVolt"].ToString();
        //        drLoadNode["LoadPowerActive"] = dr["LoadPowerActive"].ToString();
        //        drLoadNode["LoadPower"] = dr["LoadPower"].ToString();
        //        drLoadNode["LoadCurrnetABS"] = dr["LoadCurrentAbs"].ToString();
        //        drLoadNode["LoadCurrentArg"] = dr["LoadCurrentArg"].ToString();
        //        dsloadnode.Tables["Node"].Rows.Add(drLoadNode);
        //    }

        //    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

        //    DataRow drDesign = dsloadnode.Tables["Title"].NewRow();
        //    drDesign["ProjectName"] = designProfile.DesignName;
        //    drDesign["Designer"] = designProfile.Designer;
        //    drDesign["Area"] = designProfile.Zone;
        //    drDesign["Credit"] = designProfile.Validate;
        //    System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //    string _date = string.Format("{0}/{1}/{2}", p.GetYear(designProfile.DesignDate).ToString(), p.GetMonth(designProfile.DesignDate).ToString(), p.GetDayOfMonth(designProfile.DesignDate).ToString());

        //    drDesign["Date"] = _date;
        //    dsloadnode.Tables["Title"].Rows.Add(drDesign);

        //    Atend.Report.frmNodeReport nodeReport = new frmNodeReport();
        //    nodeReport.SetDataset(dsloadnode);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(nodeReport);

        //}

        //private void FillLoadBranch()
        //{
        //    DataTable dtLoadBranch = Atend.Base.Calculating.CDistributedLoadBranch.AccessSelectAll();
        //    dsSagAndTension dsloadbranch = new dsSagAndTension();

        //    foreach (DataRow dr in dtLoadBranch.Rows)
        //    {
        //        DataRow drLoadBranch = dsloadbranch.Tables["Branch"].NewRow();
        //        drLoadBranch["BranchName"] = dr["BranchName"].ToString();
        //        drLoadBranch["Lenght"] = dr["Lenght"].ToString();
        //        drLoadBranch["CurrnetABS"] = dr["CurrentAbs"].ToString();
        //        drLoadBranch["CurrentArg"] = dr["CurrentArg"].ToString();
        //        drLoadBranch["Condutilization"] = dr["CondUtilization"].ToString();
        //        drLoadBranch["TotalLoad"] = dr["TotalLoad"].ToString();
        //        dsloadbranch.Tables["Branch"].Rows.Add(drLoadBranch);
        //    }

        //    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

        //    DataRow drDesign = dsloadbranch.Tables["Title"].NewRow();
        //    drDesign["ProjectName"] = designProfile.DesignName;
        //    drDesign["Designer"] = designProfile.Designer;
        //    drDesign["Area"] = designProfile.Zone;
        //    drDesign["Credit"] = designProfile.Validate;
        //    System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //    string _date = string.Format("{0}/{1}/{2}", p.GetYear(designProfile.DesignDate).ToString(), p.GetMonth(designProfile.DesignDate).ToString(), p.GetDayOfMonth(designProfile.DesignDate).ToString());

        //    drDesign["Date"] = _date;
        //    dsloadbranch.Tables["Title"].Rows.Add(drDesign);

        //    Atend.Report.frmBranchReport BranchReport = new frmBranchReport();
        //    BranchReport.SetDataset(dsloadbranch);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(BranchReport);

        //}

        //private void FillCrossSection()
        //{
        //    DataTable dtCross = Atend.Base.Calculating.CCrossSection.AccessSelectAll();
        //    dsSagAndTension dscrossSection = new dsSagAndTension();

        //    foreach (DataRow dr in dtCross.Rows)
        //    {
        //        DataRow drSection = dscrossSection.Tables["CrossSection"].NewRow();
        //        ed.WriteMessage("FROM={0}\n", dr["From"].ToString());
        //        drSection["From"] = dr["From"].ToString();
        //        drSection["To"] = dr["To"].ToString();
        //        drSection["NameExist"] = dr["ExistCond"].ToString();
        //        drSection["Lenght"] = dr["Lenght"].ToString();
        //        drSection["NameComment"] = dr["CommentCond"].ToString();
        //        drSection["CrossSection"] = dr["CrossSection"].ToString();
        //        drSection["Current"] = dr["Current"].ToString();
        //        //drSection["Volt"] = dr["Volt"].ToString();
        //        dscrossSection.Tables["CrossSection"].Rows.Add(drSection);
        //    }

        //    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

        //    DataRow drDesign = dscrossSection.Tables["Title"].NewRow();
        //    drDesign["ProjectName"] = designProfile.DesignName;
        //    drDesign["Designer"] = designProfile.Designer;
        //    drDesign["Area"] = designProfile.Zone;
        //    drDesign["Credit"] = designProfile.Validate;
        //    System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //    string _date = string.Format("{0}/{1}/{2}", p.GetYear(designProfile.DesignDate).ToString(), p.GetMonth(designProfile.DesignDate).ToString(), p.GetDayOfMonth(designProfile.DesignDate).ToString());

        //    drDesign["Date"] = _date;
        //    dscrossSection.Tables["Title"].Rows.Add(drDesign);

        //    Atend.Report.frmCrossSectionReport report = new frmCrossSectionReport();
        //    report.SetDataset(dscrossSection);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(report);
        //}
        
        //private void FillTrans()
        //{
        //    DataTable dtTransCurrent = Atend.Base.Calculating.CTransCurrent.AccessSelectAll();
        //    dsSagAndTension dsTransCurrent = new dsSagAndTension();

        //    foreach (DataRow dr in dtTransCurrent.Rows)
        //    {
        //        DataRow drCurrent = dsTransCurrent.Tables["LoadCurrent"].NewRow();
        //        drCurrent["I"] = dr["I"].ToString();
        //        drCurrent["PF"] = dr["PF"].ToString();
        //        drCurrent["Count"] = dr["BranchCount"].ToString();
        //        drCurrent["CF"] = dr["CF"].ToString();
        //        dsTransCurrent.Tables["LoadCurrent"].Rows.Add(drCurrent);
        //    }
        //    ed.WriteMessage("PASS LoadCurrent\n");
        //    DataTable dtLoadPower = Atend.Base.Calculating.CTransPower.AccessSelectAll();
        //    foreach (DataRow dr in dtLoadPower.Rows)
        //    {
        //        DataRow drPower = dsTransCurrent.Tables["LoadPower"].NewRow();
        //        drPower["P"] = dr["P"].ToString();
        //        drPower["PF"] = dr["PF"].ToString();
        //        drPower["Count"] = dr["BranchCount"].ToString();
        //        drPower["CF"] = dr["CF"].ToString();
        //        dsTransCurrent.Tables["LoadPower"].Rows.Add(drPower);
        //    }
        //    ed.WriteMessage("PASS LOADPOWER\n");
        //    DataTable dtLoadBranch = Atend.Base.Calculating.CTransBranch.AccessSelectAll();
        //    foreach (DataRow dr in dtLoadBranch.Rows)
        //    {
        //        DataRow drBranch = dsTransCurrent.Tables["LoadBranch"].NewRow();
        //        drBranch["I"] = dr["I"].ToString();
        //        drBranch["PF"] = dr["PF"].ToString();
        //        drBranch["BranchCount"] = dr["BranchCount"].ToString();
        //        drBranch["PhuseCount"] = dr["PhaseCount"].ToString();
        //        drBranch["CF"] = dr["CF"].ToString();
        //        dsTransCurrent.Tables["LoadBranch"].Rows.Add(drBranch);
        //    }
        //    ed.WriteMessage("PASS BranchName\n");
        //    DataTable dtInput = Atend.Base.Calculating.CTransformer.AccessSelectAll();
        //    ed.WriteMessage("PassSelectROWS.Count={0}\n",dtInput.Rows.Count);
        //    foreach (DataRow dr in dtInput.Rows)
        //    {
        //        DataRow drInput = dsTransCurrent.Tables["InputInfoTranse"].NewRow();
        //        ed.WriteMessage("Build Row\n");
        //        drInput["Height"] = dr["Height"].ToString();
        //        ed.WriteMessage("H\n");
        //        drInput["Load"] = dr["Load"].ToString();
        //        ed.WriteMessage("L\n");
        //        drInput["Result"] = dr["Result"].ToString();
        //        ed.WriteMessage("R\n");
        //        drInput["PowerLoss"] = "0";
        //        ed.WriteMessage("P\n");
        //        dsTransCurrent.Tables["InputInfoTranse"].Rows.Add(drInput);
                
        //    }
        //    ed.WriteMessage("Pass INPUTINFO TrANS\n");
        //    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

        //    DataRow drDesign = dsTransCurrent.Tables["Title"].NewRow();
        //    drDesign["ProjectName"] = designProfile.DesignName;
        //    drDesign["Designer"] = designProfile.Designer;
        //    drDesign["Credit"] = designProfile.Validate;
        //    drDesign["Area"] = designProfile.Zone;

        //    System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //    string _date = string.Format("{0}/{1}/{2}", p.GetYear(designProfile.DesignDate).ToString(), p.GetMonth(designProfile.DesignDate).ToString(), p.GetDayOfMonth(designProfile.DesignDate).ToString());
                
        //    drDesign["Date"] = _date;
        //    dsTransCurrent.Tables["Title"].Rows.Add(drDesign);
        //    ed.WriteMessage("GO TO Page\n");
        //    Atend.Report.frmTranseInfoReport report = new frmTranseInfoReport();
        //    report.SetDataset(dsTransCurrent);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(report);


        //}

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (rdbLoadNode.Checked)
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                _CElectricalOutPut.FillLoadNode();


                Atend.Report.frmNodeReport  Report = new frmNodeReport();
                Report.SetDataset(_CElectricalOutPut.FillLoadNode());
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                //FillLoadNode();
            }

            if (rdbBranch.Checked)
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                _CElectricalOutPut.FillLoadBranch();


                Atend.Report.frmBranchReport  Report = new frmBranchReport();
                Report.SetDataset(_CElectricalOutPut.FillLoadBranch());
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                //FillLoadBranch();
            }

            if (rdbCrossSection.Checked)
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                _CElectricalOutPut.FillCrossSection();


                Atend.Report.frmCrossSectionReport Report = new frmCrossSectionReport();
                Report.SetDataset(_CElectricalOutPut.FillCrossSection());
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                //FillCrossSection();
            }

            if (rdbTrans.Checked)
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                _CElectricalOutPut.FillTrans();


                Atend.Report.frmTranseInfoReport  Report = new frmTranseInfoReport();
                Report.SetDataset(_CElectricalOutPut.FillTrans());
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                //FillTrans();
            }
        }
    }
}