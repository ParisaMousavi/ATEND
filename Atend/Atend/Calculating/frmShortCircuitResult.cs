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
    public partial class frmShortCircuitResult : Form
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
        DataColumn dcFrom = new DataColumn("From");
        DataColumn dcTo = new DataColumn("To");
        DataColumn dcCondName = new DataColumn("CondName");
        DataColumn dcVolt = new DataColumn("Volt");

        double _FaultDuration;
        bool ForceToClose = false;


        public frmShortCircuitResult(DataTable dtNodes, DataTable dtBranch, double FalutDuration)
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
            dtResultBranch.Columns.Add(dcCondUtilization);
            dtResultBranch.Columns.Add(dcLenght);
            dtResultBranch.Columns.Add(dcCurrentAbs);
            dtResultBranch.Columns.Add(dcCurrentAng);
            dtResultBranch.Columns.Add(dcTotalLoadREal);
            dtResultBranch.Columns.Add(dcSCCurrent);
            dtResultBranch.Columns.Add(CondMax1sCurrent);
            dtResultBranch.Columns.Add(dcFrom);
            dtResultBranch.Columns.Add(dcTo);
            dtResultBranch.Columns.Add(dcCondName);
            dtResultBranch.Columns.Add(dcVolt);

            ed.WriteMessage("Print Nodes\n");
            foreach (DataRow dr in dtNodes.Rows)
            {

                DataRow drNew = dtResultNode.NewRow();
                if (new Guid(dr["ConsoleGuid"].ToString()) != Guid.Empty)
                {
                    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dr["ConsoleGuid"].ToString()));
                    drNew["ConsolGuid"] = dPack.Number;
                    drNew["VoltAbs"] = Math.Round(((Complex)dr["Voltage"]).abs, 3);

                    double voltArg = ConvertToDegree(((Complex)dr["Voltage"]).arg);
                    //drNew["VoltArg"] = Math.Round(((Complex)dr["Voltage"]).arg, 2);
                    drNew["VoltArg"] = Math.Round(voltArg, 2);

                    drNew["VoltDropPer"] = Math.Round(Convert.ToDouble(dr["VoltageDropPer"].ToString()));
                    drNew["LoadPowerReal"] = Math.Round(((Complex)dr["LoadPower"]).real, 3);
                    drNew["LoadPowerImg"] = Math.Round(((Complex)dr["LoadPower"]).imag, 3);
                    drNew["LoadCurrentAbs"] = Math.Round(((Complex)dr["LoadCurrent"]).abs, 3);//.ToString();
                    double loadcurrentArg = ConvertToDegree(((Complex)dr["LoadCurrent"]).arg);
                    
                    //drNew["LoadCurrentArg"] = Math.Round(((Complex)dr["LoadCurrent"]).arg, 3);//.ToString();
                    drNew["LoadCurrentArg"] = loadcurrentArg;
                    dtResultNode.Rows.Add(drNew);
                }
            }
            ed.WriteMessage("Print Branch\n");

            Atend.Base.Calculating.CShortCircuit.AccessDelete();

            foreach (DataRow dr in dtBranch.Rows)
            {

                Atend.Base.Calculating.CShortCircuit shortCircuit = new Atend.Base.Calculating.CShortCircuit();
                if (new Guid(dr["Code"].ToString()) != Guid.Empty)
                {
                    DataRow drNew = dtResultBranch.NewRow();
                    Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["Code"].ToString()));
                    //drNew["Code"] = branch.Number;

                    if ((branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)) || (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Jumper)) || (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Terminal)))
                    {
                        Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.EConductor Conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
                        drNew["CondName"] = Conductor.Name;
                        double IbMax = Convert.ToDouble(Conductor.MaxCurrent1Second) / Math.Sqrt(_FaultDuration);
                        drNew["CondMax1sCurrent"] = Math.Round(IbMax, 1);

                        shortCircuit.CondName = Conductor.Name;
                        shortCircuit.CondMax1SCurrent = Math.Round(IbMax, 1);

                    }

                    if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.ESelfKeeper SelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfKeeperTip.PhaseProductCode);
                        drNew["CondName"] = SelfKeeper.Name;
                        double IbMax = Convert.ToDouble(SelfKeeper.MaxCurrent1Second) / Math.Sqrt(_FaultDuration);
                        drNew["CondMax1sCurrent"] = Math.Round(IbMax, 1);
                        shortCircuit.CondName = SelfKeeper.Name;
                        shortCircuit.CondMax1SCurrent = Math.Round(IbMax, 1);
                    }

                    if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
                    {
                        Atend.Base.Equipment.EGroundCabelTip GroundTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.EGroundCabel Ground = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(GroundTip.PhaseProductCode);
                        drNew["CondName"] = Ground.Name;
                        double IbMax = Convert.ToDouble(Ground.MaxCurrent1Second) / Math.Sqrt(_FaultDuration);
                        drNew["CondMax1sCurrent"] = Math.Round(IbMax, 1);
                        shortCircuit.CondName = Ground.Name;
                        shortCircuit.CondMax1SCurrent = Math.Round(IbMax, 1);
                    }


                    drNew["Lenght"] = dr["Length"].ToString();
                    drNew["CondUtilization"] = Math.Round(Convert.ToDouble(dr["CondUtilization"].ToString()), 3);
                    drNew["CurrentAbs"] = Math.Round(((Complex)dr["Current"]).abs, 3);//.ToString();
                    double currentArg = ConvertToDegree(((Complex)dr["Current"]).arg);
                    
                    //drNew["CurrentArg"] = Math.Round(((Complex)dr["Current"]).arg, 3);//.ToString();
                    drNew["CurrentArg"] = Math.Round(currentArg, 3);

                    drNew["TotalLoadReal"] = Math.Round(((Complex)dr["TotalLoad"]).arg, 3);
                    drNew["SCCurrent"] = Math.Round(Convert.ToDouble(dr["SCCurrent"].ToString()), 0);
                    ed.WriteMessage("CondCode={0}\n", dr["CondCode"].ToString());


                    shortCircuit.Lenght = Convert.ToDouble(dr["Length"].ToString());
                    shortCircuit.Condutilization = Math.Round(Convert.ToDouble(dr["CondUtilization"].ToString()), 3);
                    shortCircuit.CurrentAbs = Math.Round(((Complex)dr["Current"]).abs, 3);//.ToString();
                    
                    shortCircuit.CurrentArg = Math.Round(currentArg, 3);//.ToString();
                    
                    shortCircuit.TotalLoad = Math.Round(((Complex)dr["TotalLoad"]).arg, 3);
                    shortCircuit.ScCurrent = Math.Round(Convert.ToDouble(dr["SCCurrent"].ToString()), 0);
                    ed.WriteMessage("Current={0}\n", shortCircuit.ScCurrent);


                    //از
                    DataRow[] drnodeFrom = dtNodes.Select(string.Format("ConsoleObjID={0}", dr["UpNodeId"].ToString()));
                    if (drnodeFrom.Length > 0)
                    {
                        drNew["From"] = FindNodeComment(new Guid(drnodeFrom[0]["ConsoleGuid"].ToString()));
                        shortCircuit.From = drNew["From"].ToString();
                    }


                    //به
                    DataRow[] drnodeTo = dtNodes.Select(string.Format("ConsoleObjID={0}", dr["DnNodeId"].ToString()));
                    if (drnodeTo.Length > 0)
                    {
                        drNew["Volt"] = Math.Round(((Complex)(drnodeTo[0]["Voltage"])).abs, 2).ToString();
                        drNew["To"] = FindNodeComment(new Guid(drnodeTo[0]["ConsoleGuid"].ToString()));
                        shortCircuit.To = drNew["To"].ToString();
                        shortCircuit.Volt = Math.Round(((Complex)(drnodeTo[0]["Voltage"])).abs, 2);
                    }






                    //if (new Guid(dr["Code"].ToString()) != Guid.Empty)
                    //{
                    ////Atend.Base.Equipment.EConductorTip condTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Convert.ToInt32(dr["CondCode"].ToString()));

                    ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(condTip.PhaseProductCode);
                    ////ed.WriteMessage("conductor.MaxCurrent1Second={0},SCCurrent={1}\n", conductor.MaxCurrent1Second, dr["SCCurrent"].ToString());


                    ////ed.WriteMessage("#######\n");

                    //ed.WriteMessage("&&&&&&={0}\n", dr["CondMax1sCurrent"].ToString());
                    //}
                    //else
                    //{

                    //    drNew["CondMax1sCurrent"] = dr["CondUtilizationSC"].ToString();
                    //}

                    dtResultBranch.Rows.Add(drNew);



                    if (!shortCircuit.AccessInsert())
                    {
                        ed.WriteMessage("Insert Failed\n");
                    }
                }
            }
            ////foreach (DataRow dr in dtResultBranch.Rows)
            ////{
            ////    ed.WriteMessage("dtResultBranch.CondMax1sCurrent={0}\n", dr["CondMax1sCurrent"].ToString());
            ////}

            //string code1 = "00000000-0000-0000-0000-000000000000";
            //DataRow[] drs = dtResultBranch.Select(" Code = '"+code1+"'");
            //drs[0].Delete();

            //DataRow[] drs1 = dtResultNode.Select(" ConsolGuid = '" + code1 + "'");
            //drs1[0].Delete();

            ed.WriteMessage("BindDAtA\n");
            gvBranch.AutoGenerateColumns = false;
            gvBranch.DataSource = dtResultBranch;

            //gvNode.AutoGenerateColumns = false;
            //gvNode.DataSource = dtResultNode;
            CheckCurrent();

        }


        private string FindNodeComment(Guid nodeGuid)
        {
            Atend.Base.Design.DPackage package = Atend.Base.Design.DPackage.AccessSelectByCode(nodeGuid);
            return package.Number;
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


            TransferToExcel();
        }

        private void frmShortCircuitResult_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }

        private void TransferToExcel()
        {
          
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string Name = "گزارش محاسبات الکتریکی " + date + Atend.Control.Common.DesignName + ".xls";


            Atend.Global.Utility.UReport.CreateExcelReportFinalElectrical(Name);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TransferToExcel();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckCurrent()
        {
            for (int i = 0; i < gvBranch.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvBranch.Rows[i].Cells["SCCurrent"].Value.ToString()) > Convert.ToDouble(gvBranch.Rows[i].Cells[7].Value.ToString()))
                {
                    gvBranch.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            for (int i = 0; i < gvBranch.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvBranch.Rows[i].Cells["SCCurrent"].Value.ToString()) > Convert.ToDouble(gvBranch.Rows[i].Cells[7].Value.ToString()))
                {
                    gvBranch.Rows[i].Cells[7].Style.BackColor = Color.Red;
                }
            }
        }


        private double ConvertToDegree(double Radyan)
        {
            return (180*Radyan)/Math.PI;
        }
    }
}