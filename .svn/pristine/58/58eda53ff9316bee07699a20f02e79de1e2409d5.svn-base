using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Report
{
    public partial class frmChoiceReportMec : Form
    {
        public frmChoiceReportMec()
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

        private bool Validation()
        {
            if (string.IsNullOrEmpty(cbFormat.Text))
            {
                MessageBox.Show("لطفاً روش محاسبات را انتخاب کنید");
                return false;
            }

            if (!rbConductorDay.Checked && !rbPloe.Checked && !rbSagTension.Checked && !rdbRusSurface.Checked && !rdbPowerWithOutHalter.Checked && !rdbPowerwithHalter.Checked)
            {
                MessageBox.Show("لطفاً نوع گزارش را انتخاب کنید");
                return false;
            }

            return true;
        }

        //private void FillSagAndTension(bool IsUTS)
        //{
        //    ArrayList arrSection = new ArrayList();
        //    bool chk = true;

        //    DataTable dtSagtension = Atend.Base.Calculating.CSagAndTension.AccessSelectByIsUTS(IsUTS);
        //    dsSagAndTension dsSagTensionReport = new dsSagAndTension();

        //    int CountPole = 0;
        //    for (int i = 0; i < arrSection.Count; i++)
        //    {
        //        DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //        CountPole += dtpoleSec.Rows.Count;
        //        DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //        CountPole += dtpoleSec1.Rows.Count;
        //    }
        //    DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
        //    Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
        //    if (desProfile.Id != 0)
        //    {
        //        dr1["Area"] = desProfile.Zone;
        //        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //        string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
        //        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
        //        dr1["Designer"] = desProfile.Designer;
        //        dr1["Credit"] = desProfile.Validate;
        //        dr1["ProjectName"] = desProfile.DesignName;//Atend.Control.Common.DesignName;
        //        dr1["SectionCount"] = arrSection.Count.ToString();
        //        dr1["PoleCount"] = CountPole.ToString();
        //        dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
        //        return;
        //    }
            
            
        //    foreach (DataRow dr in dtSagtension.Rows)
        //    {
        //        DataRow drSagTension = dsSagTensionReport.Tables["SagAndTension"].NewRow();
        //        Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
        //        Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
        //        Atend.Base.Calculating.CDefaultMec DefMec = Atend.Base.Calculating.CDefaultMec.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()), IsUTS);
        //        drSagTension["SectionCode"] = dr["Number"].ToString();
        //        drSagTension["StartPole"] = dPackStart.Number;
        //        drSagTension["EndPole"] = dPackEnd.Number;
        //        drSagTension["NormH"] = dr["NormH"].ToString();
        //        drSagTension["NormF"] = dr["NormF"].ToString();
        //        drSagTension["WindAndIceH"] = dr["WindAndIceH"].ToString();
        //        drSagTension["WindAndIceF"] = dr["WindAndIceF"].ToString();
        //        drSagTension["MinTempH"] = dr["MinTempH"].ToString();
        //        drSagTension["MinTempF"] = dr["MinTempF"].ToString();
        //        drSagTension["MaxTempH"] = dr["MaxTempH"].ToString();
        //        drSagTension["MaxTempF"] = dr["MaxTempF"].ToString();
        //        drSagTension["WindH"] = dr["WindH"].ToString();
        //        drSagTension["WindF"] = dr["WindF"].ToString();
        //        drSagTension["IceH"] = dr["IceH"].ToString();
        //        drSagTension["IceF"] = dr["IceF"].ToString();
        //        drSagTension["CondName"] = dr["ConductorName"].ToString();
        //        drSagTension["Span"] = DefMec.SE;
        //        dsSagTensionReport.Tables["SagAndTension"].Rows.Add(drSagTension);

        //        chk = true;
        //        for (int i = 0; i < arrSection.Count; i++)
        //        {
        //            if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
        //            {
        //                chk = false;
        //            }
        //        }
        //        if (chk)
        //        {
        //            arrSection.Add(dr["SectionCode"].ToString());
        //        }

        //    }
        //    //MessageBox.Show(dsSagTensionReport.Tables["SagAndTension"].Rows.Count.ToString());
            

        //    Atend.Report.frmSagAndTensionReport frm = new frmSagAndTensionReport();
        //    frm.SetDataset(dsSagTensionReport);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);

        //}

        //private void FillPowerWithOutHalter(bool IsUts)
        //{
        //    ArrayList arrSection = new ArrayList();
        //    bool chk = true;

        //    DataTable dtPowerWithOutHalter = Atend.Base.Calculating.CPowerWithOutHalter.AccessSelectByISUTS(IsUts);
        //    dsSagAndTension dsSagTensionReport = new dsSagAndTension();

        //    int CountPole = 0;
        //    for (int i = 0; i < arrSection.Count; i++)
        //    {
        //        DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //        CountPole += dtpoleSec.Rows.Count;
        //        DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //        CountPole += dtpoleSec1.Rows.Count;
        //    }

        //    DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
        //    Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
        //    if (desProfile.Id != 0)
        //    {
        //        dr1["Area"] = desProfile.Zone;
        //        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //        string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
        //        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
        //        dr1["Designer"] = desProfile.Designer;
        //        dr1["ProjectName"] = desProfile.DesignName;//Atend.Control.Common.DesignName;
        //        dr1["SectionCount"] = arrSection.Count.ToString();
        //        dr1["Credit"] = desProfile.Validate;
        //        dr1["PoleCount"] = CountPole.ToString();
        //        dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
        //        return;
        //    }
            
            
        //    foreach (DataRow dr in dtPowerWithOutHalter.Rows)
        //    {
        //        DataRow drPower = dsSagTensionReport.Tables["PoleWithOutHalter"].NewRow();
        //        //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
        //        //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
        //        drPower["SectionNumber"] = dr["Number"].ToString();
        //        drPower["PoleNum"] = dr["PoleNum"].ToString();
        //        drPower["Power"] = dr["PolePower"].ToString();
        //        drPower["Count"] = dr["PoleCount"].ToString();          
        //        dsSagTensionReport.Tables["PoleWithOutHalter"].Rows.Add(drPower);

        //        chk = true;
        //        for (int i = 0; i < arrSection.Count; i++)
        //        {
        //            if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
        //            {
        //                chk = false;
        //            }
        //        }
        //        if (chk)
        //        {
        //            arrSection.Add(dr["SectionCode"].ToString());
        //        }

        //    }
        //    //MessageBox.Show(dsSagTensionReport.Tables["SagAndTension"].Rows.Count.ToString());
            
        //    Atend.Report.frmPoleWithoutHalterReport frm = new frmPoleWithoutHalterReport();
        //    frm.SetDataset(dsSagTensionReport);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
        //}

        //private void FillPowerWithHalter(bool IsUts)
        //{
        //    ArrayList arrSection = new ArrayList();
        //    bool chk = true;

        //    DataTable dtPowerWithHalter = Atend.Base.Calculating.CPowerWithHalter.AccessSelectByISUTS(IsUts);
        //    dsSagAndTension dsSagTensionReport = new dsSagAndTension();

        //    int CountPole = 0;
        //    for (int i = 0; i < arrSection.Count; i++)
        //    {
        //        DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //        CountPole += dtpoleSec.Rows.Count;
        //        DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //        CountPole += dtpoleSec1.Rows.Count;
        //    }

        //    DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
        //    Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
        //    if (desProfile.Id != 0)
        //    {
        //        dr1["Area"] = desProfile.Zone;
        //        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //        string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
        //        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
        //        dr1["Designer"] = desProfile.Designer;
        //        dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
        //        dr1["SectionCount"] = arrSection.Count.ToString();
        //        dr1["Credit"] = desProfile.Validate;
        //        dr1["PoleCount"] = CountPole.ToString();
        //        dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
        //        return;
        //    }
            
            
        //    foreach (DataRow dr in dtPowerWithHalter.Rows)
        //    {
        //        DataRow drPower = dsSagTensionReport.Tables["PoleWithHalter"].NewRow();
        //        //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
        //        //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
        //        drPower["SectionNumber"] = dr["Number"].ToString();
        //        drPower["PoleNum"] = dr["PoleNum"].ToString();
        //        drPower["Power"] = dr["PolePower"].ToString();
        //        drPower["Count"] = dr["PoleCount"].ToString();
        //        drPower["Name"]=dr["HalterName"].ToString();
        //        drPower["HalterPower"] = dr["HalterPower"].ToString();
        //        drPower["HalterCount"] = dr["HalterCount"].ToString();
        //        dsSagTensionReport.Tables["PoleWithHalter"].Rows.Add(drPower);

        //        chk = true;
        //        for (int i = 0; i < arrSection.Count; i++)
        //        {
        //            if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
        //            {
        //                chk = false;
        //            }
        //        }
        //        if (chk)
        //        {
        //            arrSection.Add(dr["SectionCode"].ToString());
        //        }

        //    }
        //    //MessageBox.Show(dsSagTensionReport.Tables["SagAndTension"].Rows.Count.ToString());
            
        //    Atend.Report.frmPoleWithHalterReport frm = new frmPoleWithHalterReport();
        //    frm.SetDataset(dsSagTensionReport);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);
        //}

        //private void FillForcePole(bool IsUTS)
        //{
        //    ArrayList arrSection = new ArrayList();
        //    bool chk = true;

        //    DataTable dtForcePole = Atend.Base.Calculating.CWindOnPole.AccessSelectByISUTS(IsUTS);
        //    dsSagAndTension dsSagTensionReport = new dsSagAndTension();
            
        //    int CountPole = 0;
        //    for (int i = 0; i < arrSection.Count; i++)
        //    {
        //        DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //        CountPole += dtpoleSec.Rows.Count;
        //        DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //        CountPole += dtpoleSec1.Rows.Count;
        //    }

        //    DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
        //    Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
        //    if (desProfile.Id != 0)
        //    {
        //        dr1["Area"] = desProfile.Zone;
        //        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //        string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
        //        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
        //        dr1["Designer"] = desProfile.Designer;
        //        dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
        //        dr1["SectionCount"] = arrSection.Count.ToString();
        //        dr1["Credit"] = desProfile.Validate;
        //        dr1["PoleCount"] = CountPole.ToString();
        //        dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
        //        return;
        //    }
            
            
        //    foreach (DataRow dr in dtForcePole.Rows)
        //    {
        //        DataRow drForcePole = dsSagTensionReport.Tables["PoleForce"].NewRow();
        //        //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
        //        //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
        //        drForcePole["SectionNumber"] = dr["Number"].ToString();
        //        drForcePole["PoleNumber"] = dr["DcPole"].ToString();
        //        drForcePole["Normal"] = dr["DcNorm"].ToString();
        //        drForcePole["Ice"] = dr["DcIceHeavy"].ToString();
        //        drForcePole["Wind"] = dr["DcWindSpeed"].ToString();
        //        drForcePole["MaxTemp"] = dr["DcMaxTemp"].ToString();
        //        drForcePole["MinTemp"] = dr["DcMinTemp"].ToString();
        //        drForcePole["IceWind"] = dr["DcWindIce"].ToString();
               
        //        dsSagTensionReport.Tables["PoleForce"].Rows.Add(drForcePole);

        //        chk = true;
        //        for (int i = 0; i < arrSection.Count; i++)
        //        {
        //            if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
        //            {
        //                chk = false;
        //            }
        //        }
        //        if (chk)
        //        {
        //            arrSection.Add(dr["SectionCode"].ToString());
        //        }

        //    }
        //    //MessageBox.Show(dsSagTensionReport.Tables["PoleForce"].Rows.Count.ToString());
            
        //    Atend.Report.frmPoleForceReport  frm = new frmPoleForceReport();
        //    frm.SetDataset(dsSagTensionReport);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);

        //}

        //private void FillRudSurface()
        //{
        //    ArrayList arrSection = new ArrayList();
        //    bool chk = true;

        //    DataTable dtrudSurface = Atend.Base.Calculating.CRudSurface.AccessSelect();
        //    dsSagAndTension dsSagTensionReport = new dsSagAndTension();

        //    int CountPole = 0;
        //    for (int i = 0; i < arrSection.Count; i++)
        //    {
        //        DataTable dtpoleSec = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //        CountPole += dtpoleSec.Rows.Count;
        //        DataTable dtpoleSec1 = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(new Guid(arrSection[i].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //        CountPole += dtpoleSec1.Rows.Count;
        //    }

        //    DataRow dr1 = dsSagTensionReport.Tables["Title"].NewRow();
        //    Atend.Base.Design.DDesignProfile desProfile = Atend.Base.Design.DDesignProfile.AccessSelect();
        //    if (desProfile.Id != 0)
        //    {
        //        dr1["Area"] = desProfile.Zone;
        //        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        //        string _date = string.Format("{0}/{1}/{2}", p.GetYear(desProfile.DesignDate).ToString(), p.GetMonth(desProfile.DesignDate).ToString(), p.GetDayOfMonth(desProfile.DesignDate).ToString());
        //        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
        //        dr1["Designer"] = desProfile.Designer;
        //        dr1["ProjectName"] = desProfile.DesignName; //Atend.Control.Common.DesignName;
        //        dr1["Credit"] = desProfile.Validate;
        //        dr1["SectionCount"] = arrSection.Count.ToString();
        //        dr1["PoleCount"] = CountPole.ToString();
        //        dsSagTensionReport.Tables["Title"].Rows.Add(dr1);
        //    }
        //    else
        //    {
        //        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
        //        return;
        //    }
            
            
        //    foreach (DataRow dr in dtrudSurface.Rows)
        //    {
        //        DataRow drForcePole = dsSagTensionReport.Tables["RudSurface"].NewRow();
        //        //Atend.Base.Calculating.CStartEnd startEnd = Atend.Base.Calculating.CStartEnd.AccessSelectBySectionCode(new Guid(dr["SectionCode"].ToString()));
        //        //Atend.Base.Design.DPackage dPackStart = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.StartPole);
        //        //Atend.Base.Design.DPackage dPackEnd = Atend.Base.Design.DPackage.AccessSelectByNodeCode(startEnd.EndPole);
        //        drForcePole["SectionNumber"] = dr["Number"].ToString();
        //        drForcePole["PoleNumber"] = dr["DcPole"].ToString();
        //        drForcePole["Normal"] = dr["DcNorm"].ToString();
        //        drForcePole["Ice"] = dr["DcIceHeavy"].ToString();
        //        drForcePole["Wind"] = dr["DcWindSpeed"].ToString();
        //        drForcePole["MaxTemp"] = dr["DcMaxTemp"].ToString();
        //        drForcePole["MinTemp"] = dr["DcMinTemp"].ToString();
        //        drForcePole["IceWind"] = dr["DcWindIce"].ToString();

        //        dsSagTensionReport.Tables["RudSurface"].Rows.Add(drForcePole);

        //        chk = true;
        //        for (int i = 0; i < arrSection.Count; i++)
        //        {
        //            if (new Guid(arrSection[0].ToString()) == new Guid(dr["SectionCode"].ToString()))
        //            {
        //                chk = false;
        //            }
        //        }
        //        if (chk)
        //        {
        //            arrSection.Add(dr["SectionCode"].ToString());
        //        }

        //    }

        //    //MessageBox.Show(dsSagTensionReport.Tables["RudSurface"].Rows.Count.ToString());
            
        //    Atend.Report.frmRudSurfaceReport  frm = new frmRudSurfaceReport();
        //    frm.SetDataset(dsSagTensionReport);
        //    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frm);

        //}

        private void frmChoiceReportMec_Load(object sender, EventArgs e)
        {
            cbFormat.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Validation())
                return;

            bool Type = true;

            if (cbFormat.SelectedIndex == 1)
                Type = false;

            if (rbSagTension.Checked)
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                //_CMecanicalOutPut.FillSagAndTension(Type);
                Atend.Report.frmSagAndTensionReport Report = new frmSagAndTensionReport();
                Report.SetDataset(_CMecanicalOutPut.FillSagAndTension(Type));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);
                //FillSagAndTension(Type);
            }

            if (rbPloe.Checked)
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
               // _CMecanicalOutPut.FillForcePole(Type);
                Atend.Report.frmPoleForceReport  Report = new frmPoleForceReport();
                Report.SetDataset(_CMecanicalOutPut.FillForcePole(Type));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);

                //FillForcePole(Type);
 
            }

            if (rdbRusSurface.Checked)
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                //_CMecanicalOutPut.FillRudSurface();

                Atend.Report.frmRudSurfaceReport  Report = new frmRudSurfaceReport();
                Report.SetDataset(_CMecanicalOutPut.FillRudSurface());
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);
                

               
                //FillRudSurface();
            }

            if (rdbPowerwithHalter.Checked)
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                //_CMecanicalOutPut.FillPowerWithHalter(Type);


                Atend.Report.frmPoleWithHalterReport Report = new frmPoleWithHalterReport();
                Report.SetDataset(_CMecanicalOutPut.FillPowerWithHalter(Type));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);
                //FillPowerWithHalter(Type);
            }

            if (rdbPowerWithOutHalter.Checked)
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                //_CMecanicalOutPut.FillPowerWithOutHalter(Type);
                Atend.Report.frmPoleWithoutHalterReport Report = new frmPoleWithoutHalterReport();
                Report.SetDataset(_CMecanicalOutPut.FillPowerWithOutHalter(Type));
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Report);
                //FillPowerWithOutHalter(Type);
            }
        }

        private void rdbRusSurface_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}