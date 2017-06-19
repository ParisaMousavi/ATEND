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


//get from tehran 7/15
namespace Atend.Calculating
{
    public partial class frmElectericalResult : Form
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
        DataColumn dcPowerLoss = new DataColumn("PowerLoss");
        DataColumn dcCondCode = new DataColumn("CondName");
        DataColumn dcCondCurrent = new DataColumn("CondCurrent");
        DataColumn dcFrom = new DataColumn("From");
        DataColumn dcTo = new DataColumn("To");


        bool ForceToClose = false;
        //DataColumn dcSCCurrent = new DataColumn("SCCurrent");
        //DataColumn CondMax1sCurrent = new DataColumn("CondMax1sCurrent");

        public frmElectericalResult(DataTable dtNodes, DataTable dtBranch)
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
            dtResultBranch.Columns.Add(dcCondCode);
            dtResultBranch.Columns.Add(dcCondCurrent);
            dtResultBranch.Columns.Add(dcFrom);
            dtResultBranch.Columns.Add(dcTo);
            dtResultBranch.Columns.Add(dcPowerLoss);
            //dtResultBranch.Columns.Add(dcSCCurrent);
            //dtResultBranch.Columns.Add(CondMax1sCurrent);

            //ed.WriteMessage("Print Nodes\n");

            Atend.Base.Calculating.CDistributedLoadBranch.AccessDelete();
            Atend.Base.Calculating.CDistributedLoadNode.AccessDelete();

            foreach (DataRow dr in dtNodes.Rows)
            {

                DataRow drNew = dtResultNode.NewRow();
                Atend.Base.Calculating.CDistributedLoadNode loadNode = new Atend.Base.Calculating.CDistributedLoadNode();

                Atend.Base.Design.DPackage pack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dr["ConsoleGuid"].ToString()));
                if (pack.Code != Guid.Empty)
                {
                    drNew["ConsolGuid"] = pack.Number;
                    loadNode.NodeName = pack.Number;

                    drNew["VoltAbs"] = Math.Round(((Complex)dr["Voltage"]).abs, 3);
                    loadNode.VoltAbs = Math.Round(((Complex)dr["Voltage"]).abs, 3);
                    double voltDegree = ConvertToDegree(((Complex)dr["Voltage"]).arg);
                    //drNew["VoltArg"] = Math.Round(((Complex)dr["Voltage"]).arg, 2);
                    //loadNode.VoltArg = Math.Round(((Complex)dr["Voltage"]).arg, 2);

                    drNew["VoltArg"] = Math.Round(voltDegree, 2);
                    loadNode.VoltArg = Math.Round(voltDegree, 2);

                    double voltDropPer = 100 - Convert.ToDouble(dr["VoltageDropPer"].ToString());

                    drNew["VoltDropPer"] = Math.Round(voltDropPer,2);
                    loadNode.DropVolt = Convert.ToDouble(Math.Round(voltDropPer,2));

                    drNew["LoadPowerReal"] = Math.Round(((Complex)dr["LoadPower"]).imag, 3)/1000;
                    loadNode.LoadPower = Math.Round(((Complex)dr["LoadPower"]).imag, 3)/1000;


                    drNew["LoadPowerImg"] = Math.Round(((Complex)dr["LoadPower"]).real, 3)/1000;
                    loadNode.LoadPowerActive = Math.Round(((Complex)dr["LoadPower"]).real, 3)/1000;

                    drNew["LoadCurrentAbs"] = Math.Round(((Complex)dr["LoadCurrent"]).abs, 3);//.ToString();
                    loadNode.LoadCurrentAbs = Math.Round(((Complex)dr["LoadCurrent"]).abs, 3);


                    double LoadCurrentArg = ConvertToDegree(((Complex)dr["LoadCurrent"]).arg);
                    //drNew["LoadCurrentArg"] = Math.Round(((Complex)dr["LoadCurrent"]).arg, 3);
                    //loadNode.LoadCurrentArg = Math.Round(((Complex)dr["LoadCurrent"]).arg, 3);


                    drNew["LoadCurrentArg"] = Math.Round(LoadCurrentArg, 3);
                    loadNode.LoadCurrentArg = Math.Round(LoadCurrentArg, 3);

                    dtResultNode.Rows.Add(drNew);

                    if (!loadNode.AccessInsert())
                    {
                        ed.WriteMessage("LoadNode Insert Failed\n");
                    }
                }
            }
            //ed.WriteMessage("Print Branch\n");
            double totalLoad = 0;
            foreach (DataRow dr in dtBranch.Rows)
            {
                DataRow drNew = dtResultBranch.NewRow();
                Atend.Base.Calculating.CDistributedLoadBranch LoadBranch = new Atend.Base.Calculating.CDistributedLoadBranch();
                Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["Code"].ToString()));

                if (branch.Code != Guid.Empty)
                {
                    drNew["Code"] = branch.Number;
                    LoadBranch.BranchName = branch.Number;

                    drNew["Lenght"] =Math.Round(Convert.ToDouble(dr["Length"].ToString()),2);
                    LoadBranch.Lenght = Math.Round(Convert.ToDouble(dr["Length"].ToString()),3);

                    drNew["CondUtilization"] = Math.Round(Convert.ToDouble(dr["CondUtilization"].ToString()), 3);
                    LoadBranch.Condutilization = Math.Round(Convert.ToDouble(dr["CondUtilization"].ToString()), 3);

                    drNew["CurrentAbs"] = Math.Round(((Complex)dr["Current"]).abs, 3);//.ToString();
                    LoadBranch.CurrentAbs = Math.Round(((Complex)dr["Current"]).abs, 3);


                    double currentArg = ConvertToDegree(((Complex)dr["Current"]).arg);
                    //drNew["CurrentArg"] = Math.Round(((Complex)dr["Current"]).arg, 3);
                    //LoadBranch.CurrentArg = Math.Round(((Complex)dr["Current"]).arg, 3);
                    drNew["CurrentArg"] = Math.Round(currentArg, 3);
                    LoadBranch.CurrentArg = Math.Round(currentArg, 3);

                    //MessageBox.Show(dr["PowerLoss"].ToString());


                    totalLoad += Math.Round(((Complex)dr["TotalLoad"]).real / 1000 + (Convert.ToDouble(dr["PowerLoss"].ToString()) / 1000), 3);
                    drNew["TotalLoadReal"] = totalLoad;
                    LoadBranch.TotalLoad = totalLoad;//Math.Round(((Complex)dr["TotalLoad"]).real / 1000 + (Convert.ToDouble(dr["PowerLoss"].ToString()) / 1000), 3);


                    ed.WriteMessage("CondCode={0}\n",dr["CondCode"].ToString());

                    if ((branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)) || (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Jumper)) || (branch.ProductType ==Convert.ToInt32(Atend.Control.Enum.ProductType.Terminal)))
                    {
                        Atend.Base.Equipment.EConductorTip econdTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.EConductor eCond = Atend.Base.Equipment.EConductor.AccessSelectByCode(econdTip.PhaseProductCode);

                        drNew["CondName"] = eCond.Name;
                        drNew["CondCurrent"] = eCond.MaxCurrent;

                        LoadBranch.CondName =eCond.Name;
                        LoadBranch.CondCurrent = eCond.MaxCurrent;
                    }
                    else if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        Atend.Base.Equipment.ESelfKeeperTip  eSelTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.ESelfKeeper eSelf = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(eSelTip.PhaseProductCode);

                        drNew["CondName"] = eSelf.Name;
                        drNew["CondCurrent"] = eSelf.MaxCurrent;

                        LoadBranch.CondName = eSelf.Name;
                        LoadBranch.CondCurrent = eSelf.MaxCurrent;
                    }
                    else if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))
                    {
                        Atend.Base.Equipment.EGroundCabelTip  egroundTip = Atend.Base.Equipment.EGroundCabelTip.AccessSelectByCode(branch.ProductCode);
                        Atend.Base.Equipment.EGroundCabel eGround = Atend.Base.Equipment.EGroundCabel.AccessSelectByCode(egroundTip.PhaseProductCode);

                        drNew["CondName"] = eGround.Name;
                        drNew["CondCurrent"] = eGround.MaxCurrent;

                        LoadBranch.CondName = eGround.Name;
                        LoadBranch.CondCurrent = eGround.MaxCurrent;
                    }

                    DataRow[] drnodeFrom = dtNodes.Select(string.Format("ConsoleObjID={0}", dr["UpNodeId"].ToString()));
                    if (drnodeFrom.Length > 0)
                    {
                        drNew["From"] = FindNodeComment(new Guid(drnodeFrom[0]["ConsoleGuid"].ToString()));
                        LoadBranch.From = drNew["From"].ToString();
                    }


                    //به
                    DataRow[] drnodeTo = dtNodes.Select(string.Format("ConsoleObjID={0}", dr["DnNodeId"].ToString()));
                    if (drnodeTo.Length > 0)
                    {
                        //drNew["Volt"] = Math.Round(((Complex)(drnodeTo[0]["Voltage"])).abs, 2).ToString();
                        drNew["To"] = FindNodeComment(new Guid(drnodeTo[0]["ConsoleGuid"].ToString()));
                        LoadBranch.To = drNew["To"].ToString();
                    }
                    LoadBranch.PowerLoss = Math.Round(Convert.ToDouble(dr["PowerLoss"])/1000,2);
                    drNew["PowerLoss"] = Math.Round(Convert.ToDouble(dr["PowerLoss"])/1000,2);



                    dtResultBranch.Rows.Add(drNew);
                    if (!LoadBranch.AccessInsert())
                    {
                        ed.WriteMessage("LoadBranch Inser Failed\n");
                    }
                }
                //drNew["SCCurrent"] = Math.Round(Convert.ToDouble(dr["SCCurrent"].ToString()), 3);
                //drNew["CondMax1sCurrent"] = Math.Round(Convert.ToDouble(dr["CondMax1sCurrent"].ToString()), 3);

                
            }
            //ed.WriteMessage("BindDAtA\n");
            //string Code1 = "00000000-0000-0000-0000-000000000000";
            //DataRow[] drs = dtResultBranch.Select(" Code= '" + Code1 + "'");
            //drs[0].Delete();

            //DataRow[] drs1 = dtResultNode.Select(" ConsolGuid ='" + Code1 + "'");
            //drs1[0].Delete();

            gvBranch.AutoGenerateColumns = false;
            gvBranch.DataSource = dtResultBranch;

            gvNode.AutoGenerateColumns = false;
            gvNode.DataSource = dtResultNode;

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

          
           //Atend.Global.Utility.UReport.CreateExcelReaport(NameBranch, dtResultBranch, dicBranch, 1);
        }

        private void TransferToExcel()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //Atend.Base.Base.BReport report = Atend.Base.Base.BReport.Select_ByCode(1);
            System.Globalization.PersianCalendar calender = new System.Globalization.PersianCalendar();
            int day = calender.GetDayOfMonth(DateTime.Today);
            int Month = calender.GetMonth(DateTime.Today);
            int Year = calender.GetYear(DateTime.Today);

            string date = Year.ToString() + "-" + Month.ToString() + "-" + day.ToString();
            string Name = "گزارش محاسبات الکتریکی " + date + Atend.Control.Common.DesignName + ".xls";
            //string NameBranch = "گزارش محاسبات پخش بار شاخه  ها " + date + Atend.Control.Common.DesignName;

            //string path = Atend.Control.Common.fullPath + "\\ReportFile\\Report.xlsm";


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

        private void انتقالبهفایلEXCELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferToExcel();
        }

        private void frmElectericalResult_Load(object sender, EventArgs e)
        {
            if (ForceToClose)
                this.Close();
        }

        private double ConvertToDegree(double Radyan)
        {
            return (180 * Radyan) / Math.PI;
        }
    }
}