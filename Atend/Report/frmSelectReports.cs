using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using PDFlib_dotnet;
using Word1 = Microsoft.Office.Interop.Word;

namespace Atend.Report
{
    public partial class frmSelectReports : Form
    {
        static string Path = Atend.Control.Common.DesignFullAddress + "\\ProductList";
        static private bool[] IsReportCreated = new bool[20];
        int RepCount = 0;

        public frmSelectReports()
        {
            InitializeComponent();
        }

        private void frmSelectReports_Load(object sender, EventArgs e)
        {
            tvReport.ExpandAll();

        }

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

        private void tvReport_AfterCheck(object sender, TreeViewEventArgs e)
        {


            if (e.Node.Name == "Node0")
            {
                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node00"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node20"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node13"].Checked = e.Node.Checked;

                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node4"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node24"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node18"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node19"].Checked = e.Node.Checked;

                tvReport.Nodes["Node0"].Nodes["Node122"].Checked = e.Node.Checked;
                

            }

            if (e.Node.Name == "Node00")
            {
                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Checked = e.Node.Checked;
                tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node4"].Checked = e.Node.Checked;

            }

            if (e.Node.Name == "Node1")
                SetUTSChilds(e.Node.Checked);

            if (e.Node.Name == "Node20")
                SetDiagramChilds(e.Node.Checked);

            if (e.Node.Name == "Node7")
                SetMaxFChilds(e.Node.Checked);

            if (e.Node.Name == "Node13")
                SetElec(e.Node.Checked);

            if (e.Node.Name == "Node24")
                SetDetailChilds(e.Node.Checked);

            if (e.Node.Name == "Node122")
                SetGISChilds(e.Node.Checked);



        }

        private void SetUTSChilds(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node2"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node3"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node5"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node6"].Checked = Check;

        }

        private void SetDiagramChilds(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node21"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node22"].Checked = Check;
        }

        private void SetMaxFChilds(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node8"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node9"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node11"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node12"].Checked = Check;

        }

        private void SetElec(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node14"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node15"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node16"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node17"].Checked = Check;

        }

        private void SetDetailChilds(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node24"].Nodes["Node23"].Checked = Check;
        }

        private void SetGISChilds(bool Check)
        {
            tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node2"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node3"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node4"].Checked = Check;
            tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node5"].Checked = Check;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void CreateSagTensionWithUTS_pdf()
        {
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //try
            //{
            //ed.WriteMessage("ONE\n");
            //MessageBox.Show("one");
            Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
            //MessageBox.Show("1");
            dsSagAndTension ds = _CMecanicalOutPut.FillSagAndTension(true);

            //MessageBox.Show("2");
            crSagAndTension a = new crSagAndTension();
            //MessageBox.Show("3");
            ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
            //MessageBox.Show("4");
            a.SetDataSource(ds);
            //MessageBox.Show("5");
            System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //MessageBox.Show("6");
            System.IO.FileStream f = new System.IO.FileStream(Path + "\\کشش و فلش به روش UTS.pdf", System.IO.FileMode.Create);
            //MessageBox.Show("7");
            byte[] reader = new byte[b.Length];
            //MessageBox.Show("8");
            b.Read(reader, 0, Convert.ToInt32(b.Length));
            //MessageBox.Show("9");
            f.Write(reader, 0, reader.Length);
            //MessageBox.Show("10");
            f.Close();

            this.Text = "1";
            //MessageBox.Show("one payan");
            //}
            //catch(System.Exception ex)
            //{
            //    MessageBox.Show("E:"+ex);
            //    IsReportCreated[1] = false;
            //}
            //ed.WriteMessage("ONE finished\n");
            IsReportCreated[1] = true;
        }

        private static void CreateSagTensionWithMaxf_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillSagAndTension(false);
                crSagAndTension a = new crSagAndTension();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\کشش و فلش به روش MaxF.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[5] = false;
            }
            IsReportCreated[5] = true;
        }

        private static void CreatePoleForceWithUTS_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillForcePole(true);
                crPoleForce a = new crPoleForce();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\نیروهای وارد بر پایه به روش UTS.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();

            }
            catch
            {
                IsReportCreated[2] = false;
            }
            IsReportCreated[2] = true;
        }

        private static void CreatePoleForceWithMaxf_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillForcePole(false);
                crPoleForce a = new crPoleForce();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\نیروهای وارد بر پایه به روش MaxF.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();

            }
            catch
            {
                IsReportCreated[6] = false;
            }
            IsReportCreated[6] = true;
        }

        private static void CreatePoleForceSuggestWithHalterWithUTS_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithHalter(true);
                crPoleWithHalter a = new crPoleWithHalter();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\پیشنهاد قدرت پایه با مهار به روش UTS.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[3] = false;
            }
            IsReportCreated[3] = true;
        }

        private static void CreatePoleForceSuggestWithHalterWithMaxf_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithHalter(false);
                crPoleWithHalter a = new crPoleWithHalter();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\پیشنهاد قدرت پایه با مهار به روش MaxF.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[7] = false;
            }
            IsReportCreated[7] = true;
        }

        private static void CreatePoleForceSuggestWithoutHalterWithUTS_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithOutHalter(true);
                crPoleWithoutHalter a = new crPoleWithoutHalter();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\پیشنهاد قدرت پایه بدون مهار به روش UTS.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[4] = false;
            }
            IsReportCreated[4] = true;
        }

        private static void CreatePoleForceSuggestWithoutHalterWithMaxf_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithOutHalter(false);
                crPoleWithoutHalter a = new crPoleWithoutHalter();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\پیشنهاد قدرت پایه بدون مهار به روش MaxF.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[8] = false;
            }
            IsReportCreated[8] = true;
        }

        private static void CreateNode_pdf()
        {
            try
            {

                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                dsSagAndTension ds = _CElectricalOutPut.FillLoadNode();
                crNode a = new crNode();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\گزارش گره ها.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[9] = false;
            }
            IsReportCreated[9] = true;
        }

        private static void CreateBranch_pdf()
        {
            try
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                dsSagAndTension ds = _CElectricalOutPut.FillLoadBranch();

                crBranch a = new crBranch();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\گزارش شاخه ها.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[10] = false;
            }
            IsReportCreated[10] = true;
        }

        private static void CreateCrossSection_pdf()
        {
            try
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                dsSagAndTension ds = _CElectricalOutPut.FillCrossSection();

                crCrossSection a = new crCrossSection();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\سطح مقطع.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[11] = false;
            }
            IsReportCreated[11] = true;
        }

        private static void CreateTransformerCapacity_pdf()
        {
            try
            {
                Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                dsSagAndTension ds = _CElectricalOutPut.FillTrans();

                crTranseInfo a = new crTranseInfo();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\ظرفیت ترانس.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[12] = false;
            }
            IsReportCreated[12] = true;
        }

        private static void CreateRudSurface_pdf()
        {
            try
            {
                Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                dsSagAndTension ds = _CMecanicalOutPut.FillRudSurface();
                crRudSurface a = new crRudSurface();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\سطوح نا هموار.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[13] = false;
            }
            IsReportCreated[13] = true;
        }

        private static void CreateRemark_pdf()
        {
            try
            {
                Atend.Global.Utility.UOtherOutPut _UOtherOutPut = new Atend.Global.Utility.UOtherOutPut();
                dsSagAndTension ds = _UOtherOutPut.FillRemark();
                crRemark02 a = new crRemark02();

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\Remark.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[14] = false;
            }
            IsReportCreated[14] = false;
        }

        private static void CreateStatusReport_pdf()
        {
            try
            {
                Atend.Global.Utility.UOtherOutPut _UOtherOutPut = new Atend.Global.Utility.UOtherOutPut();
                dsSagAndTension ds = _UOtherOutPut.FillStatusReport();
                crStatusReport a = new crStatusReport();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\فهرست بها.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[15] = false;
            }
            IsReportCreated[15] = true;
        }

        private static void CreateSingleLineAirpost_pdf()
        {
            try
            {
                dsSagAndTension ds = new dsSagAndTension();
                crAirpostDiagram a = new crAirpostDiagram();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost));

                foreach (DataRow PackRow in DPack.Rows)
                {
                    Atend.Base.Equipment.EAirPost Apost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));
                    DataRow DR = ds.AirPostDiagram.NewRow();
                    DR["Name"] = Apost.Name;
                    DR["Image"] = Apost.Image;
                    DR["Capacity"] = Apost.Capacity;
                    ds.AirPostDiagram.Rows.Add(DR);
                }

                //ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.AirPostDiagram.Rows.Count.ToString());
                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\دیاگرام تک خطی پست هوایی.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[16] = false;
            }
            IsReportCreated[16] = true;
        }

        private static void CreateSingleLineGroundpost_pdf()
        {
            try
            {
                dsSagAndTension ds = new dsSagAndTension();
                crGroundPostDiagram a = new crGroundPostDiagram();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost));

                foreach (DataRow PackRow in DPack.Rows)
                {
                    Atend.Base.Equipment.EGroundPost Apost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));
                    DataRow DR = ds.GroundPostDiagram.NewRow();
                    DR["Name"] = Apost.Name;
                    DR["Image"] = Apost.Image;
                    DR["Capacity"] = Apost.Capacity;
                    ds.GroundPostDiagram.Rows.Add(DR);
                }

                //ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.GroundPostDiagram.Rows.Count.ToString());
                a.SetDataSource(ds);
                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\دیاگرام تک خطی پست زمینی.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[17] = false;
            }
            IsReportCreated[17] = true;
        }

        private static void CreateSingleLineConsol_pdf()
        {
            try
            {
                dsSagAndTension ds = new dsSagAndTension();
                crConsolDiagram a = new crConsolDiagram();
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.Consol));

                foreach (DataRow PackRow in DPack.Rows)
                {
                    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));
                    DataRow DR = ds.ConsolDiagram.NewRow();
                    DR["Name"] = consol.Name;
                    DR["Image"] = consol.Image;
                    ds.ConsolDiagram.Rows.Add(DR);
                }

                //ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.GroundPostDiagram.Rows.Count.ToString());
                //ed.WriteMessage("\nDiagram Image Lenght = {0} \n", ds.GroundPostDiagram.Rows[0]["Image"].ToString());
                a.SetDataSource(ds);
                ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;


                System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                System.IO.FileStream f = new System.IO.FileStream(Path + "\\دیاگرام تک خطی کنسول.pdf", System.IO.FileMode.Create);
                byte[] reader = new byte[b.Length];
                b.Read(reader, 0, Convert.ToInt32(b.Length));
                f.Write(reader, 0, reader.Length);
                f.Close();
            }
            catch
            {
                IsReportCreated[18] = false;
            }
            IsReportCreated[18] = true;
        }
        
        
        private void toolCreateBook_Click(object sender, EventArgs e)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            for (int i = 0; i <= 19; i++)
            {
                IsReportCreated[i] = true;
            }
            progress.Value = 0;
            RepCount = 0;

            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node2"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node3"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node5"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node6"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node8"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node9"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node11"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node12"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node4"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node14"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node15"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node16"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node17"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node21"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node22"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node24"].Nodes["Node23"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node18"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node19"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node2"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node3"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node4"].Checked == true)
                RepCount++;
            if (tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node5"].Checked == true)
                RepCount++;


            int val = Convert.ToInt32(100 / RepCount);

            dsSagAndTension DataSet = new dsSagAndTension();
            //System.IO.FileStream FS;
            //byte[] reader;
            //if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
            //{
            //    FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
            //    reader = new byte[FS.Length];
            //}


            if (!System.IO.Directory.Exists(Path))
                System.IO.Directory.CreateDirectory(Path);

            if (System.IO.Directory.Exists(Path))
            {
                #region FillRemark
                if (tvReport.Nodes["Node0"].Nodes["Node18"].Checked == true)
                {
                    Atend.Global.Utility.UOtherOutPut _UOtherOutPut = new Atend.Global.Utility.UOtherOutPut();
                    dsSagAndTension ds = _UOtherOutPut.FillRemark();
                    //crRemark02 a = new crRemark02();

                    //a.SetDataSource(ds);
                    //((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    //System.IO.Stream b = ;//a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    if (ds.Tables["Remark"].Rows.Count > 0 )
                    {
                        System.IO.FileStream f = new System.IO.FileStream(Path + "\\ریمارک.doc", System.IO.FileMode.Create);
                        byte[] reader = (byte[])(ds.Tables["Remark"].Rows[0]["File"]);
                        //b.Read(reader, 0, Convert.ToInt32(b.Length));
                        f.Write(reader, 0, reader.Length);
                        f.Close();

                        string filename1 = "ریمارک.doc";
                        string folder_to_save_in = Path;
                        string filePath = Path + "\\ریمارک.doc";
                        // This bit does the actual file upload:
                        //_OpenFileDialog.SaveAs(filePath);

                        // Here we set up a WOrd Application...
                        Word1.ApplicationClass wordApplication = new Word1.ApplicationClass();

                        // Opening a Word doc requires many parameters, but we leave most of them blank...
                        object o_nullobject = Type.Missing; //System.Reflection.Missing.Value;
                        object o_filePath = filePath;
                        Word1.Document doc = wordApplication.Documents.Open(ref o_filePath,
                        ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
                        ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject,
                        ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject, ref o_nullobject);

                        // Here we save it in html format...
                        // This assumes it was called "something.doc"
                        string newfilename = Path + "\\ریمارک.pdf"; ;//filename1.Replace(".doc", ".pdf");
                        object o_newfilename = newfilename;
                        object o_format = Word1.WdSaveFormat.wdFormatPDF;
                        //object o_encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
                        //object o_endings = Word.WdLineEndingType.wdCRLF;


                        // Once again, we leave many of the parameters blank.
                        // See http://msdn.microsoft.com/library/default.asp?url=/library/en-us/vbawd11/html/womthSaveAs1_HV05213080.asp
                        // for full list of parameters.

                        wordApplication.ActiveDocument.ExportAsFixedFormat(o_newfilename.ToString(), Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, false, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument, 1, 1, Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent, true, false, Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, true, ref o_nullobject);
                        doc.Close(ref o_nullobject, ref o_nullobject, ref o_nullobject);


                        DataRow ContentRow = DataSet.Content.NewRow();
                        ContentRow["Name"] = "ریمارک";
                        ContentRow["BookMark"] = "ریمارک";
                        DataSet.Content.Rows.Add(ContentRow);
                        progress.Value += val;
                    }
                }
                #endregion

                //MessageBox.Show("1\n");

                #region FillSagAndTension=true
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node2"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillSagAndTension(true);
                    crSagAndTension a = new crSagAndTension();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\UTS کشش و فلش به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "UTS کشش و فلش به روش";
                    ContentRow["BookMark"] = "کشش و فلش به روش UTS";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;

                }
                #endregion

                //MessageBox.Show("2\n");

                #region FillForcePole=false
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node3"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillForcePole(true);
                    crPoleForce a = new crPoleForce();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\UTS نیروهای وارد بر پایه به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "UTS نیروهای وارد بر پایه به روش";
                    ContentRow["BookMark"] = "نیروهای وارد بر پایه به روش UTS";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;

                }
                #endregion

                //MessageBox.Show("3\n");

                #region FillPowerWithHalter=true
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node5"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithHalter(true);
                    crPoleWithHalter a = new crPoleWithHalter();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\UTS پیشنهاد قدرت پایه با مهار به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "UTS پیشنهاد قدرت پایه با مهار به روش";
                    ContentRow["BookMark"] = "پیشنهاد قدرت پایه با مهار به روش UTS";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;

                }
                #endregion

                //MessageBox.Show("4\n");

                #region FillPowerWithOutHalter=true
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node1"].Nodes["Node6"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithOutHalter(true);
                    crPoleWithoutHalter a = new crPoleWithoutHalter();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\UTS پیشنهاد قدرت پایه بدون مهار به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "UTS پیشنهاد قدرت پایه بدون مهار به روش";
                    ContentRow["BookMark"] = "پیشنهاد قدرت پایه بدون مهار به روش UTS";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;

                }
                #endregion

                //MessageBox.Show("5\n");

                #region FillSagAndTension=false
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node8"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillSagAndTension(false);
                    crSagAndTension a = new crSagAndTension();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\MaxF کشش و فلش به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "MaxF کشش و فلش به روش";
                    ContentRow["BookMark"] = "کشش و فلش به روش MaxF";
                    //ContentRow["Page"] = PageNumber;
                    DataSet.Content.Rows.Add(ContentRow);

                    //IsReportCreated[5] = false;
                    //T5.Start();
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("6\n");

                #region FillForcePole=false
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node9"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillForcePole(false);
                    crPoleForce a = new crPoleForce();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\MaxF نیروهای وارد بر پایه به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "MaxF نیروهای وارد بر پایه به روش";
                    ContentRow["BookMark"] = "نیروهای وارد بر پایه به روش MaxF";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("7\n");

                #region FillPowerWithHalter=false
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node11"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithHalter(false);
                    crPoleWithHalter a = new crPoleWithHalter();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\MaxF پیشنهاد قدرت پایه با مهار به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "MaxF پیشنهاد قدرت پایه با مهار به روش";
                    ContentRow["BookMark"] = "پیشنهاد قدرت پایه با مهار به روش MaxF";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("8\n");

                #region FillPowerWithOutHalter=false
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node7"].Nodes["Node12"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillPowerWithOutHalter(false);
                    crPoleWithoutHalter a = new crPoleWithoutHalter();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\MaxF پیشنهاد قدرت پایه بدون مهار به روش.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "MaxF پیشنهاد قدرت پایه بدون مهار به روش";
                    ContentRow["BookMark"] = "پیشنهاد قدرت پایه بدون مهار به روش MaxF";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("9\n");

                #region FillRudSurface
                if (tvReport.Nodes["Node0"].Nodes["Node00"].Nodes["Node4"].Checked == true)
                {
                    Atend.Global.Calculation.Mechanical.CMecanicalOutPut _CMecanicalOutPut = new Atend.Global.Calculation.Mechanical.CMecanicalOutPut();
                    dsSagAndTension ds = _CMecanicalOutPut.FillRudSurface();
                    crRudSurface a = new crRudSurface();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\سطوح نا هموار.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "سطوح نا هموار";
                    ContentRow["BookMark"] = "سطوح نا هموار";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("10\n");

                #region FillLoadNode
                if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node14"].Checked == true)
                {
                    Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                    dsSagAndTension ds = _CElectricalOutPut.FillLoadNode();
                    crNode a = new crNode();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\گزارش گره ها.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "گزارش گره ها";
                    ContentRow["BookMark"] = "گزارش گره ها";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("11\n");

                #region FillLoadBranch
                if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node15"].Checked == true)
                {
                    Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                    dsSagAndTension ds = _CElectricalOutPut.FillLoadBranch();
                    crBranch a = new crBranch();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\گزارش شاخه ها.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "گزارش شاخه ها";
                    ContentRow["BookMark"] = "گزارش شاخه ها";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("12\n");

                #region FillCrossSection
                if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node16"].Checked == true)
                {
                    Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                    dsSagAndTension ds = _CElectricalOutPut.FillCrossSection();
                    crCrossSection a = new crCrossSection();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\سطح مقطع.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "سطح مقطع";
                    ContentRow["BookMark"] = "سطح مقطع";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("13\n");

                #region FillTrans
                if (tvReport.Nodes["Node0"].Nodes["Node13"].Nodes["Node17"].Checked == true)
                {
                    Atend.Global.Calculation.Electrical.CElectricalOutPut _CElectricalOutPut = new Atend.Global.Calculation.Electrical.CElectricalOutPut();
                    dsSagAndTension ds = _CElectricalOutPut.FillTrans();

                    crTranseInfo a = new crTranseInfo();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\ظرفیت ترانس.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "ظرفیت ترانس";
                    ContentRow["BookMark"] = "ظرفیت ترانس";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("14\n");

                #region Single Diagram Air Post
                if (tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node21"].Checked == true)
                {
                    dsSagAndTension ds = new dsSagAndTension();
                    crAirpostDiagram a = new crAirpostDiagram();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.AirPost));

                    foreach (DataRow PackRow in DPack.Rows)
                    {
                        Atend.Base.Equipment.EAirPost Apost = Atend.Base.Equipment.EAirPost.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));

                        DataRow[] ARow = ds.AirPostDiagram.Select("XCode = '" + Apost.XCode.ToString() + "'");
                        if (ARow.Length == 0)
                        {
                            DataRow DR = ds.AirPostDiagram.NewRow();
                            DR["Name"] = Apost.Name;
                            DR["Image"] = Apost.Image;
                            DR["Capacity"] = Apost.Capacity;
                            DR["XCode"] = Apost.XCode.ToString();
                            ds.AirPostDiagram.Rows.Add(DR);
                        }
                    }

                    DataRow dr1 = ds.Tables["Title"].NewRow();
                    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

                    if (designProfile.Id != 0)
                    {
                        Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(designProfile.Zone);
                        dr1["Area"] = b12.SecondCode;
                        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                        string _date = ChangeToShamsi(designProfile.DesignDate);
                        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                        dr1["Designer"] = designProfile.Designer;
                        dr1["ProjectName"] = designProfile.DesignName;//Atend.Control.Common.DesignName;
                        dr1["Credit"] = designProfile.Validate;
                        if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                        {
                            System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                            byte[] reader1 = new byte[FS.Length];
                            FS.Read(reader1, 0, Convert.ToInt32(FS.Length));
                            dr1["Logo"] = reader1;
                            FS.Close();
                        }
                        //else
                        //{
                        //    MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                        //    System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                        //    byte[] reader11 = new byte[FS.Length];
                        //    FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                        //    dr1["Logo"] = reader11;
                        //    FS.Close();

                        //}
                        dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;
                        ds.Tables["Title"].Rows.Add(dr1);
                    }
                    else
                    {
                        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                        return;
                    }

                    ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.AirPostDiagram.Rows.Count.ToString());
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\دیاگرام تک خطی پست هوایی.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "دیاگرام تک خطی پست هوایی";
                    ContentRow["BookMark"] = "دیاگرام تک خطی پست هوایی";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("15\n");

                #region Single Diagram Ground Post
                if (tvReport.Nodes["Node0"].Nodes["Node20"].Nodes["Node22"].Checked == true)
                {
                    dsSagAndTension ds = new dsSagAndTension();
                    crGroundPostDiagram a = new crGroundPostDiagram();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.GroundPost));

                    foreach (DataRow PackRow in DPack.Rows)
                    {
                        Atend.Base.Equipment.EGroundPost Apost = Atend.Base.Equipment.EGroundPost.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));

                        DataRow[] GRow = ds.GroundPostDiagram.Select("XCode = '" + Apost.XCode.ToString() + "'");
                        if (GRow.Length == 0)
                        {
                            DataRow DR = ds.GroundPostDiagram.NewRow();
                            DR["Name"] = Apost.Name;
                            DR["Image"] = Apost.Image;
                            DR["Capacity"] = Apost.Capacity;
                            DR["XCode"] = Apost.XCode.ToString();
                            ds.GroundPostDiagram.Rows.Add(DR);
                        }
                    }

                    DataRow dr1 = ds.Tables["Title"].NewRow();
                    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

                    if (designProfile.Id != 0)
                    {
                        Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(designProfile.Zone);
                        dr1["Area"] = b12.SecondCode;
                        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                        string _date = ChangeToShamsi(designProfile.DesignDate);
                        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                        dr1["Designer"] = designProfile.Designer;
                        dr1["ProjectName"] = designProfile.DesignName;//Atend.Control.Common.DesignName;
                        dr1["Credit"] = designProfile.Validate;
                        ed.WriteMessage("\n Credit IN Ground Post = {0}\n", designProfile.Validate);
                        //dr1["SectionCount"] = arrSection.Count.ToString();
                        //dr1["PoleCount"] = CountPole.ToString();
                        if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                        {
                            System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                            byte[] reader1 = new byte[FS.Length];
                            FS.Read(reader1, 0, Convert.ToInt32(FS.Length));
                            dr1["Logo"] = reader1;
                            FS.Close();
                        }
                        //else
                        //{
                        //    MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                        //    System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                        //    byte[] reader11 = new byte[FS.Length];
                        //    FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                        //    dr1["Logo"] = reader11;
                        //    FS.Close();

                        //}
                        dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;
                        ds.Tables["Title"].Rows.Add(dr1);
                    }
                    else
                    {
                        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                        return;
                    }

                    ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.GroundPostDiagram.Rows.Count.ToString());
                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\دیاگرام تک خطی پست زمینی.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "دیاگرام تک خطی پست زمینی";
                    ContentRow["BookMark"] = "دیاگرام تک خطی پست زمینی";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("16\n");

                #region Single Diagram Consol
                if (tvReport.Nodes["Node0"].Nodes["Node24"].Nodes["Node23"].Checked == true)
                {
                    dsSagAndTension ds = new dsSagAndTension();
                    crConsolDiagram a = new crConsolDiagram();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                    DataTable DPack = Atend.Base.Design.DPackage.AccessSelectByType(Convert.ToInt16(Atend.Control.Enum.ProductType.Consol));

                    foreach (DataRow PackRow in DPack.Rows)
                    {
                        Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(PackRow["ProductCode"].ToString()));

                        DataRow[] CRow = ds.ConsolDiagram.Select("XCode = '" + consol.XCode.ToString() + "'");
                        if (CRow.Length == 0)
                        {
                            DataRow DR = ds.ConsolDiagram.NewRow();
                            DR["Name"] = consol.Name;
                            DR["Image"] = consol.Image;
                            DR["XCode"] = consol.XCode.ToString();

                            ds.ConsolDiagram.Rows.Add(DR);
                        }
                    }

                    Atend.Base.Design.DDesignProfile designProfile = Atend.Base.Design.DDesignProfile.AccessSelect();

                    DataRow dr1 = ds.Tables["Title"].NewRow();
                    if (designProfile.Id != 0)
                    {
                        Atend.Base.Base.BRegion b12 = Atend.Base.Base.BRegion.SelectByCode(designProfile.Zone);
                        dr1["Area"] = b12.SecondCode;
                        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                        string _date = ChangeToShamsi(designProfile.DesignDate);
                        dr1["Date"] = _date; //desProfile.DesignDate.ToString();
                        dr1["Designer"] = designProfile.Designer;
                        dr1["ProjectName"] = designProfile.DesignName;//Atend.Control.Common.DesignName;
                        dr1["Credit"] = designProfile.Validate;
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
                        //    MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                        //    System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                        //    byte[] reader11 = new byte[FS.Length];
                        //    FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                        //    dr1["Logo"] = reader11;
                        //    FS.Close();

                        //}
                        dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;
                        ds.Tables["Title"].Rows.Add(dr1);
                    }
                    else
                    {
                        MessageBox.Show("لطفا ابتدا مشخصات طرح را تکمیل نمایید", "اخطار");
                        return;
                    }

                    ed.WriteMessage("\nDiagram Row Count = {0} \n", ds.GroundPostDiagram.Rows.Count.ToString());
                    //ed.WriteMessage("\nDiagram Image Lenght = {0} \n", ds.GroundPostDiagram.Rows[0]["Image"].ToString());
                    a.SetDataSource(ds);
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;


                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\آرایش کنسول.pdf", System.IO.FileMode.Create);
                    byte[] reader1 = new byte[b.Length];
                    b.Read(reader1, 0, Convert.ToInt32(b.Length));
                    f.Write(reader1, 0, reader1.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "آرایش کنسول";
                    ContentRow["BookMark"] = "آرایش کنسول";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("17\n");

                #region StateReport
                if (tvReport.Nodes["Node0"].Nodes["Node19"].Checked == true)
                {
                    Atend.Global.Utility.UOtherOutPut _UOtherOutPut = new Atend.Global.Utility.UOtherOutPut();
                    dsSagAndTension ds1 = _UOtherOutPut.FillStatusReport();


                    crStatusReport f45 = new crStatusReport();
                    f45.SetDataSource(ds1);

                    //frmStatusReport aaa = new frmStatusReport();
                    //aaa.crViewerStatusReport.ReportSource = f45;
                    //aaa.ShowDialog();


                    //MessageBox.Show("101");
                    ArrayList arr = new ArrayList();
                    foreach (DataRow dr in ds1.StatusReport.Rows)
                    {
                        if (Convert.ToInt32(dr["ProjectCode"].ToString()) != 0)
                        {
                            if (arr.IndexOf(dr["ProjectCode"].ToString()) < 0)
                            {
                                arr.Add(dr["ProjectCode"].ToString());
                            }
                        }
                    }
                    //MessageBox.Show("102");
                    DataTable table = ds1.StatusReport.Copy();
                    //MessageBox.Show("103");
                    for (int i = 0; i < arr.Count; i++)
                    {
                        DataRow[] rows = table.Select("ProjectCode = '" + arr[i].ToString() + "'");
                        ds1.StatusReport.Clear();
                        for (int j = 0; j < rows.Length; j++)
                        {

                            //MessageBox.Show("Name = " + rows[j]["Name"].ToString());
                            DataRow newDr = ds1.StatusReport.NewRow();
                            newDr["Code"] = rows[j]["Code"];
                            newDr["Name"] = rows[j]["Name"];
                            newDr["Count"] = rows[j]["Count"];
                            newDr["Unit"] = rows[j]["Unit"];
                            //newDr["IsProduct"] = rows[j]["IsProduct"];
                            newDr["Price"] = rows[j]["Price"];
                            newDr["ExecutePrice"] = rows[j]["ExecutePrice"];
                            newDr["WagePrice"] = rows[j]["WagePrice"];
                            //MessageBox.Show("EXIst = " + rows[j]["Exist"] );
                            newDr["Exist"] = rows[j]["Exist"];
                            newDr["ProjectCode"] = rows[j]["ProjectCode"];
                            newDr["ProjectName1"] = rows[j]["ProjectName1"];
                            //ed.WriteMessage("\nNAAAAAAAAME = {0}\n", newDr["ProjectName"].ToString());
                            //newDr["Type"] = rows[j]["Type"];
                            newDr["Total"] = rows[j]["Total"];
                            newDr["AreaCoeff"] = rows[j]["AreaCoeff"];
                            newDr["Sum"] = rows[j]["Sum"];
                            //ds1.StatusReport.Rows.Add(rows[j]);
                            if (newDr["Exist"].ToString() != "موجود - موجود")
                                ds1.StatusReport.Rows.Add(newDr);
                        }

                        //MessageBox.Show("104");
                        crStatusReport a = new crStatusReport();
                        ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;
                        a.SetDataSource(ds1);


                        //MessageBox.Show("105");
                        //ed.WriteMessage("\\n%%%%%%%%%%%%%%% LOgO NaMe = " + ds1.Tables["Title"].Rows[0]["LogoName"].ToString() + "\\n");

                        //MessageBox.Show(Path + "\\" + "دستور کار" + i.ToString() + ".pdf");


                        //Atend.Calculating.frmTest tt = new Atend.Calculating.frmTest();
                        //tt.dataGridView4.DataSource = ds1.Tables["StatusReport"];
                        //tt.dataGridView5.DataSource = ds1.Tables["Title"];
                        //tt.ShowDialog();



                        //MemoryStream oStream; // using System.IO
                        //oStream = (MemoryStream)
                        //report.ExportToStream(
                        //CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(oStream.ToArray());
                        //Response.End();


                        System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        System.IO.FileStream f = new System.IO.FileStream(Path + "\\" + "دستور کار" + i.ToString() + ".pdf", System.IO.FileMode.Create);
                        byte[] reader = new byte[b.Length];
                        b.Read(reader, 0, Convert.ToInt32(b.Length));
                        f.Write(reader, 0, reader.Length);
                        f.Close();
                        //MessageBox.Show("106");

                        DataRow ContentRow = DataSet.Content.NewRow();
                        ContentRow["Name"] = "دستور کار" + i.ToString();
                        ContentRow["BookMark"] = "دستور کار";
                        DataSet.Content.Rows.Add(ContentRow);

                    }
                    progress.Value += val;
                }
                #endregion

                //MessageBox.Show("18\n");

                #region FillGISForm1
                if (tvReport.Nodes["Node0"].Nodes["Node122"].Nodes["Node2"].Checked == true)
                {
                    Atend.Global.Utility.UOtherOutPut OutPut = new Atend.Global.Utility.UOtherOutPut();
                    dsSagAndTension ds = OutPut.FillGisForm1();
                    crGISForm1 a = new crGISForm1();
                    ((CrystalDecisions.CrystalReports.Engine.FieldObject)a.ReportDefinition.ReportObjects["PageNumber1"]).Height = 0;

                    a.SetDataSource(ds);
                    System.IO.Stream b = a.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    System.IO.FileStream f = new System.IO.FileStream(Path + "\\فرم شماره 1 - توانیر.pdf", System.IO.FileMode.Create);
                    byte[] reader = new byte[b.Length];
                    b.Read(reader, 0, Convert.ToInt32(b.Length));
                    f.Write(reader, 0, reader.Length);
                    f.Close();

                    DataRow ContentRow = DataSet.Content.NewRow();
                    ContentRow["Name"] = "فرم شماره 1 - توانیر";
                    ContentRow["BookMark"] = "فرم شماره 1 - توانیر";
                    DataSet.Content.Rows.Add(ContentRow);
                    progress.Value += val;

                }
                #endregion

                progress.Value = 100;
                int PageNumber = 1;
                PDFlib p = new PDFlib();
                bool CanContinue = true;
                if (CanContinue)
                {
                    bool flag = true;
                    try
                    {

                        // This means we must check return values of load_font() etc.
                        p.set_parameter("errorpolicy", "return");
                        // p.set_parameter("SearchPath", searchpath);
                        if (p.begin_document(Atend.Control.Common.DesignFullAddress + "\\ProductList\\ATEND Report_New.pdf", "") == -1)
                            throw new Exception("Error: " + p.get_errmsg());

                        //p.set_info("Creator", "PDFlib starter sample");
                        //p.set_info("Title", "starter_pdfmerge");

                        #region concat pdfs
                        for (int indx = 0; indx < DataSet.Content.Rows.Count; indx++)
                        {
                            //string ext = s.Substring(s.IndexOf(".") + 1, 3);
                            //string str = s.Substring(0, s.IndexOf("."));
                            //ed.WriteMessage("\n!!!!!!!!!File Name = {0} , Extension = {1} ", str, ext);
                            //if (ext.ToLower() == "pdf")
                            //{
                            //p.define_layer("L1", "");
                            //p.set_layer_dependency("", "printsubtype = " + "Vahid");
                            //for (i = 0; i < pdffiles.Length; i++)
                            //{
                            int indoc, endpage, pageno, page;

                            /* Open the input PDF */
                            string s = Path + "\\" + DataSet.Content.Rows[indx]["Name"] + ".pdf";
                            indoc = p.open_pdi_document(s, "");
                            if (indoc == -1)
                            {
                                Console.WriteLine("Error: " + p.get_errmsg());
                                continue;
                            }
                            endpage = (int)p.pcos_get_number(indoc, "/Root/Pages/Count");
                            /* Loop over all pages of the input document */
                            for (pageno = 1; pageno <= endpage; pageno++)
                            {
                                page = p.open_pdi_page(indoc, pageno, "");
                                if (page == -1)
                                {
                                    Console.WriteLine("Error: " + p.get_errmsg());
                                    continue;
                                }
                                /* Dummy page size; will be adjusted later */
                                p.begin_page_ext(10, 10, "");
                                /* Create a bookmark with the file name */
                                if (pageno == 1)
                                {
                                    String optlist = ""; /* "fontstyle=bold textcolor={rgb 0 0 1}";*/
                                    DataRow[] cntrow = DataSet.Content.Select("BookMark = '" + DataSet.Content.Rows[indx]["BookMark"].ToString() + "'");
                                    if (cntrow.Length == 1)
                                    {
                                        p.create_bookmark(DataSet.Content.Rows[indx]["BookMark"].ToString(), optlist);
                                        DataSet.Content.Rows[indx]["Page"] = PageNumber;
                                    }
                                    else
                                        if (flag)
                                        {
                                            p.create_bookmark(DataSet.Content.Rows[indx]["BookMark"].ToString(), optlist);
                                            DataSet.Content.Rows[indx]["Page"] = PageNumber;
                                            flag = false;
                                        }

                                }
                                /* Place the imported page on the output page, and
                                 * adjust the page size
                                 */

                                int font = p.load_font("Helvetica", "winansi", "");
                                if (font == -1)
                                    throw new Exception("Error: " + p.get_errmsg());
                                p.setfont(font, 12.0);
                                p.show_xy(PageNumber.ToString(), 80, 40);
                                p.fit_pdi_page(page, 0, 0, "adjustpage");
                                p.close_pdi_page(page);
                                p.end_page_ext("");
                                PageNumber += 1;
                            }
                            p.close_pdi_document(indoc);
                            System.IO.File.Delete(s);
                            //}
                        }
                        #endregion

                        //MessageBox.Show("concat finished");

                        p.end_document("");
                        int cunt = DataSet.Content.Rows.Count;
                        for (int d = 0; d < cunt; d++)
                        {
                            if (DataSet.Content.Rows[d]["Name"].ToString().Contains("دستور کار") == true)
                            {
                                if (DataSet.Content.Rows[d]["Name"].ToString() != "دستور کار" + "0")
                                {
                                    DataSet.Content.Rows.RemoveAt(d);
                                    d--;
                                    cunt--;
                                }
                                else
                                    DataSet.Content.Rows[d]["Name"] = "دستور کار";
                            }
                        }

                        //MessageBox.Show("1 finished");

                        DataRow dr1 = DataSet.Title.NewRow();
                        if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                        {
                            System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                            byte[] reader11 = new byte[FS.Length];
                            FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                            dr1["Logo"] = reader11;
                            FS.Close();
                        }
                        //else
                        //{
                        //    MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                        //    System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                        //    byte[] reader11 = new byte[FS.Length];
                        //    FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                        //    dr1["Logo"] = reader11;
                        //    FS.Close();

                        //}
                        dr1["LogoName"] = Atend.Control.ConnectionString.LogoName;
                        DataSet.Tables["Title"].Rows.Add(dr1);


                        #region Create Fehrest
                        crContent Content = new crContent();
                        Content.SetDataSource(DataSet);
                        System.IO.Stream b = Content.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        System.IO.FileStream f = new System.IO.FileStream(Path + "\\فهرست.pdf", System.IO.FileMode.Create);
                        byte[] reader = new byte[b.Length];
                        b.Read(reader, 0, Convert.ToInt32(b.Length));
                        f.Write(reader, 0, reader.Length);
                        f.Close();
                        if (p != null)
                        {
                            p.Dispose();
                        }
                        Atend.Base.Design.DDesignProfile DP = Atend.Base.Design.DDesignProfile.AccessSelect();
                        DataRow DPDR = DataSet.DesignProfile.NewRow();
                        DPDR["DesignName"] = DP.DesignName;
                        DPDR["DesignCode"] = DP.DesignCode;
                        DPDR["Scale"] = DP.Scale;
                        try
                        {
                            //System.Globalization.PersianCalendar p11 = new System.Globalization.PersianCalendar();
                            string _date = ChangeToShamsi(DP.DesignDate);
                            DPDR["DesignDate"] =_date;
                        }
                        catch
                        {
                            //DPDR["DesignDate"] = DateTime.Now;
                        }
                        DPDR["Address"] = DP.Address;
                        if (DP.Zone.ToString() == "")
                            DPDR["Zone"] = 0;
                        else
                            DPDR["Zone"] = DP.Zone;
                        DPDR["Validate"] = DP.Validate;
                        DPDR["Employer"] = DP.Employer;
                        DPDR["Adviser"] = DP.Adviser;
                        DPDR["Designer"] = DP.Designer;
                        DPDR["Controller"] = DP.Controller;
                        DPDR["Supporter"] = DP.Supporter;
                        DPDR["Approval"] = DP.Approval;
                        DPDR["Planner"] = DP.Planner;
                        DataSet.DesignProfile.Rows.Add(DPDR);
                        DataSet.Tables["Title"].Clear();
                        DataRow dr11 = DataSet.Title.NewRow();
                        if (System.IO.File.Exists(Atend.Control.ConnectionString.LogoPath))
                        {
                            System.IO.FileStream FS = new System.IO.FileStream(Atend.Control.ConnectionString.LogoPath, System.IO.FileMode.Open);
                            byte[] reader11 = new byte[FS.Length];
                            FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                            dr11["Logo"] = reader11;
                            FS.Close();
                        }
                        //else
                        //{
                        //    MessageBox.Show(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath));
                        //    System.IO.FileStream FS = new System.IO.FileStream(string.Format(@"{0}\SupportFiles\default.JPG", Atend.Control.Common.fullPath), System.IO.FileMode.Open);
                        //    byte[] reader11 = new byte[FS.Length];
                        //    FS.Read(reader11, 0, Convert.ToInt32(FS.Length));
                        //    dr1["Logo"] = reader11;
                        //    FS.Close();

                        //}
                        dr11["LogoName"] = Atend.Control.ConnectionString.LogoName;
                        DataSet.Tables["Title"].Rows.Add(dr11);
                        #endregion

                        //MessageBox.Show("fehrest finished");

                        #region Create Jeld
                        crDesignProfile crDP = new crDesignProfile();
                        crDP.SetDataSource(DataSet);

                        System.IO.Stream b1 = crDP.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        System.IO.FileStream f1 = new System.IO.FileStream(Path + "\\جلد.pdf", System.IO.FileMode.Create);
                        byte[] reader1 = new byte[b1.Length];
                        b1.Read(reader1, 0, Convert.ToInt32(b1.Length));
                        f1.Write(reader1, 0, reader1.Length);
                        f1.Close();
                        /* Open the input PDF */
                        PDFlib q = new PDFlib();
                        q.set_parameter("errorpolicy", "return");
                        int indoc1, endpage1, pageno1, page1;
                        if (!System.IO.Directory.Exists(Atend.Control.Common.DesignFullAddress + "\\Result\\"))
                            System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + "\\Result\\");
                        if (q.begin_document(Atend.Control.Common.DesignFullAddress + "\\Result\\ATEND Report.pdf", "") == -1)
                            throw new Exception("Error: " + q.get_errmsg());
                        #endregion

                        //MessageBox.Show("jeld finished");

                        string[] str1 ={ Path + "\\جلد.pdf", Path + "\\فهرست.pdf", Atend.Control.Common.DesignFullAddress + "\\ProductList\\ATEND Report_New.pdf" };
                        int k = 0, counter = 0;
                        foreach (string s in str1)
                        {
                            k++;
                            ed.WriteMessage("\nFile Name = {0}\n", s);

                            string str11 = s.Substring(0, s.IndexOf("."));

                            indoc1 = q.open_pdi_document(s, "");
                            if (indoc1 == -1)
                            {
                                Console.WriteLine("Error: " + q.get_errmsg());
                                continue;
                            }
                            endpage1 = (int)q.pcos_get_number(indoc1, "/Root/Pages/Count");
                            /* Loop over all pages of the input document */
                            for (pageno1 = 1; pageno1 <= endpage1; pageno1++)
                            {
                                ed.WriteMessage("\nPageNo = {0}\n", pageno1.ToString());
                                page1 = q.open_pdi_page(indoc1, pageno1, "");

                                if (page1 == -1)
                                {
                                    Console.WriteLine("Error: " + q.get_errmsg());
                                    continue;
                                }
                                /* Dummy page size; will be adjusted later */
                                q.begin_page_ext(10, 10, "");
                                /* Create a bookmark with the file name */
                                String optlist = ""; /* "fontstyle=bold textcolor={rgb 0 0 1}";*/
                                if (k > 2)
                                {
                                    //if (pageno1 - 1 < DataSet.Content.Rows.Count)
                                    {
                                        //if (pageno1 - 1 == Convert.ToInt32(DataSet.Content.Rows[counter]["Page"].ToString()))
                                        int convert = pageno1;
                                        DataRow[] Row = DataSet.Content.Select(string.Format("Page = '{0}'", convert.ToString()));
                                        if (Row.Length > 0)
                                        {
                                            //ed.WriteMessage("\nBookMark = {0}\n", DataSet.Content.Rows[pageno1 - 1]["Name"].ToString());
                                            q.create_bookmark(Row[0]["BookMark"].ToString(), optlist);
                                            counter++;
                                        }
                                    }
                                }
                                else
                                    if (k > 1)
                                        q.create_bookmark(str11.Substring(str11.LastIndexOf("\\") + 1), optlist);

                                //DataRow ContentRow = DataSet.Content.NewRow();

                                //ContentRow["Name"] = str.Substring(str.LastIndexOf("\\") + 1);
                                //ContentRow["Page"] = PageNumber;
                                //DataSet.Content.Rows.Add(ContentRow);


                                /* Place the imported page on the output page, and
                                 * adjust the page size
                                 */
                                //p.begin_page_ext(0, 0, "width=a4.width height=a4.height");

                                //int font = p.load_font("Helvetica", "winansi", "");
                                //if (font == -1)
                                //    throw new Exception("Error: " + p.get_errmsg());
                                //p.setfont(font, 12.0);

                                //p.show_xy(PageNumber.ToString(), 80, 40);
                                q.fit_pdi_page(page1, 0, 0, "adjustpage");
                                q.close_pdi_page(page1);

                                q.end_page_ext("");
                            }

                            PageNumber += (pageno1 - 1);
                            q.close_pdi_document(indoc1);
                            //}
                            System.IO.File.Delete(s);
                        }
                        q.end_document("");

                        if (q != null)
                        {
                            q.Dispose();
                        }

                        foreach (DataRow Dr in DataSet.Content.Rows)
                        {
                            ed.WriteMessage("\n ~~~~~~~Name = {0} , Page = {1}\n", Dr["name"], Dr["Page"]);
                        }


                    }
                    catch (PDFlibException err)
                    {
                        // caught exception thrown by PDFlib
                        ed.WriteMessage("PDFlib exception occurred:");
                        ed.WriteMessage("[{0}] {1}: {2}\n", err.get_errnum(),
                                err.get_apiname(), err.get_errmsg());
                    }
                    finally
                    {
                        if (p != null)
                        {
                            p.Dispose();
                            //MessageBox.Show(Atend.Control.Common.DesignFullAddress + "\\Result\\ATEND Report.pdf");
                            if (System.IO.File.Exists(Atend.Control.Common.DesignFullAddress + "\\Result\\ATEND Report.pdf"))
                            {
                                System.Diagnostics.Process.Start(Atend.Control.Common.DesignFullAddress + "\\Result\\ATEND Report.pdf");
                            }

                        }
                    }
                }
            }
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}