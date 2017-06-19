using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Atend2Electrical;
using ComplexMath;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


//get from tahran 7/15
namespace Atend.Calculating
{
    public partial class frmShortCircuitResult1 : Form
    {
        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        DataTable dtResultNode = new DataTable();
        DataColumn dcConsolGuid = new DataColumn("ConsolGuid");
        DataColumn dcVoltAbs = new DataColumn("VoltAbs");
        DataColumn dcVoltAng = new DataColumn("VoltArg");
        DataColumn dcVoltDropPer = new DataColumn("VoltDropPer");
        DataColumn dcLoadPowerReal = new DataColumn("LoadPowerReal");
        DataColumn dcLoadPowerImage = new DataColumn("LoadPowerImg");
        DataColumn dcLoadCurrentAbs = new DataColumn("LoadCurrentAbs");
        DataColumn dcLoadCurrentArg = new DataColumn("LoadCurrentArg");

        DataTable dtResultBranch = new DataTable();
        DataColumn dcCode = new DataColumn("Code");
        DataColumn dcLenght = new DataColumn("Lenght");
        DataColumn dcCurrentAbs = new DataColumn("CurrentAbs");
        DataColumn dcCurrentAng = new DataColumn("CurrentArg");
        DataColumn dcCondUtilization = new DataColumn("CondUtilization");
        DataColumn dcTotalLoadREal = new DataColumn("TotalLoadReal");
        DataColumn dcSCCurrent = new DataColumn("SCCurrent");
        DataColumn CondMax1sCurrent = new DataColumn("CondMax1sCurrent");
        double _FaultDuration;
        bool ForceToClose = false;
        public frmShortCircuitResult1(DataTable dtNodes, DataTable dtBranch,double FalutDuration)
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
            _FaultDuration = FalutDuration;
            dtResultNode.Columns.Add(dcLoadPowerImage);
            dtResultNode.Columns.Add(dcLoadPowerReal);
            dtResultNode.Columns.Add(dcVoltAbs);
            dtResultNode.Columns.Add(dcVoltAng);
            dtResultNode.Columns.Add(dcVoltDropPer);
            dtResultNode.Columns.Add(dcLoadCurrentAbs);
            dtResultNode.Columns.Add(dcLoadCurrentArg);
            dtResultNode.Columns.Add(dcConsolGuid);

            dtResultBranch.Columns.Add(dcCode);
            dtResultBranch .Columns.Add(dcCondUtilization);
            dtResultBranch.Columns.Add(dcLenght);
            dtResultBranch.Columns.Add(dcCurrentAbs);
            dtResultBranch.Columns.Add(dcCurrentAng);
            dtResultBranch.Columns.Add(dcTotalLoadREal);
            dtResultBranch.Columns.Add(dcSCCurrent);
            dtResultBranch.Columns.Add(CondMax1sCurrent);

            //ed.WriteMessage("Print Nodes\n");
            foreach (DataRow dr in dtNodes.Rows)
            {

                DataRow drNew = dtResultNode.NewRow();

                drNew["ConsolGuid"] = dr["ConsoleGuid"].ToString();
                drNew["VoltAbs"] = Math.Round(((Complex)dr["Voltage"]).abs, 3);
                drNew["VoltArg"] = Math.Round(((Complex)dr["Voltage"]).arg, 2);

                drNew["VoltDropPer"] = Math.Round(Convert.ToDouble(dr["VoltageDropPer"].ToString()));
                drNew["LoadPowerReal"] = Math.Round(((Complex)dr["LoadPower"]).real, 3);
                drNew["LoadPowerImg"] = Math.Round(((Complex)dr["LoadPower"]).imag, 3);
                drNew["LoadCurrentAbs"] = Math.Round(((Complex)dr["LoadCurrent"]).abs, 3);//.ToString();
                drNew["LoadCurrentArg"] = Math.Round(((Complex)dr["LoadCurrent"]).arg, 3);//.ToString();
                dtResultNode.Rows.Add(drNew);
            }
            ed.WriteMessage("Print Branch\n");
            foreach (DataRow dr in dtBranch.Rows)
            {
                DataRow drNew = dtResultBranch.NewRow();
                drNew["Code"] = dr["Code"].ToString();
                drNew["Lenght"] = dr["Length"].ToString();
                drNew["CondUtilization"] = Math.Round(Convert.ToDouble(dr["CondUtilization"].ToString()),3);
                drNew["CurrentAbs"] = Math.Round(((Complex)dr["Current"]).abs,3);//.ToString();
                drNew["CurrentArg"] = Math.Round(((Complex)dr["Current"]).arg,3);//.ToString();
                drNew["TotalLoadReal"] = Math.Round(((Complex)dr["TotalLoad"]).arg, 3);
                drNew["SCCurrent"] = Math.Round(Convert.ToDouble(dr["CondUtilizationSC"].ToString()), 0);
                ed.WriteMessage("CondCode={0}\n",dr["CondCode"].ToString());
                if (new Guid(dr["Code"].ToString()) != Guid.Empty)
                {
                    Atend.Base.Equipment.EConductorTip condTip = Atend.Base.Equipment.EConductorTip.SelectByCode(Convert.ToInt32(dr["CondCode"].ToString()));

                    Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(condTip.PhaseProductCode);
                    ed.WriteMessage("conductor.MaxCurrent1Second={0},SCCurrent={1}\n", conductor.MaxCurrent1Second, dr["SCCurrent"].ToString());

                    double IbMax = Convert.ToDouble(conductor.MaxCurrent1Second) / Math.Sqrt(_FaultDuration);
                    drNew["CondMax1sCurrent"] = Math.Round((Convert.ToDouble(dr["SCCurrent"]) / IbMax) * 100,1);
                    ed.WriteMessage("#######\n");
                    //ed.WriteMessage("&&&&&&={0}\n", dr["CondMax1sCurrent"].ToString());
                }
                else
                {

                    drNew["CondMax1sCurrent"] = dr["CondUtilizationSC"].ToString();
                }
                dtResultBranch.Rows.Add(drNew);
            }
            foreach (DataRow dr in dtResultBranch.Rows)
            {
                ed.WriteMessage("dtResultBranch.CondMax1sCurrent={0}\n",dr["CondMax1sCurrent"].ToString());
            }

            string code1 = "00000000-0000-0000-0000-000000000000";
            DataRow[] drs = dtResultBranch.Select(" Code = '"+code1+"'");
            drs[0].Delete();

            DataRow[] drs1 = dtResultNode.Select(" ConsolGuid = '" + code1 + "'");
            drs1[0].Delete();

           ed.WriteMessage("BindDAtA\n");
            gvBranch.AutoGenerateColumns = false;
            gvBranch.DataSource = dtResultBranch;

            gvNode.AutoGenerateColumns = false;
            gvNode.DataSource = dtResultNode;

        }

        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void خروجToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void exelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

          
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void TransferToEXCEL()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ConsolGuid", "کد");
            dic.Add("VoltAbs", "اندازه ولتاژ");
            dic.Add("VoltArg", "زاویه ولتاژ");
            dic.Add("VoltDropPer", "ضریب افت ولتاژ");
            dic.Add("LoadPowerReal", "توان اکتیو");
            dic.Add("LoadPowerImg", "توان راکتیو");
            dic.Add("LoadCurrentAbs", "اندازه جریان بار");
            dic.Add("LoadCurrentArg", "زاویه جریان بار");


            Dictionary<string, string> dicBranch = new Dictionary<string, string>();
            dicBranch.Add("Code", "کد");
            dicBranch.Add("Lenght", "طول");
            dicBranch.Add("CurrentAbs", "اندازه جریان");
            dicBranch.Add("CurrentArg", "زاویه جریان");
            dicBranch.Add("CondUtilization", "ضریب بار گذاری");
            dicBranch.Add("TotalLoadReal", "TotalLoadReal");

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string Name = "گزارش محاسبات پخش بار گره ها " + date + Atend.Control.Common.DesignName;
            string NameBranch = "گزارش محاسبات پخش بار شاخه  ها " + date + Atend.Control.Common.DesignName;

            string path = Atend.Control.Common.fullPath + "\\ReportFile\\Report.xlsm";


            Atend.Global.Utility.UReport.CreateExcelReaport(Name, dtResultNode, dic, 1);
            Atend.Global.Utility.UReport.CreateExcelReaport(NameBranch, dtResultBranch, dicBranch, 1);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void frmShortCircuitResult1_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }
    }
}