using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using Autodesk.AutoCAD.DatabaseServices;

namespace Atend.Global.Calculation.Electrical
{
    public class LoadFlow
    {
        public enum LoadFlowErr
        {
            NoError,
            NonConverging,
            LoopExists,
            ImpCalcErr,
            LoadCalcErr
        }
        private  ObjectId InvalidConsObjId = ObjectId.Null;

        private readonly ObjectId _RootConsoleObjID;
        private readonly double _NomVolt;
        private readonly double _TevVolt;
        private readonly Complex _TevImp;
        private double
            _TotalLoss = 0,
            _MinVoltage = 0;
        private Complex _TotalLoad, _InputCurrent;


        System.Data.DataTable dtNodes = new System.Data.DataTable();
        System.Data.DataColumn dcVoltage = new System.Data.DataColumn("Voltage", Type.GetType("ComplexMath.Complex"));
        System.Data.DataColumn dcConsolGuid = new System.Data.DataColumn("ConsolGuid");
        System.Data.DataColumn dcConsolObjId = new System.Data.DataColumn("ConsolObjectId");
        System.Data.DataColumn dcVoltageDropPer = new System.Data.DataColumn("VoltageDropPer");
        System.Data.DataColumn dcLoadCurrent = new System.Data.DataColumn("LoadCurrent", Type.GetType("ComplexMath.Complex"));
        System.Data.DataColumn dcLoadPower = new System.Data.DataColumn("LoadPower", Type.GetType("ComplexMath.Complex"));
        System.Data.DataColumn dcPoleGuid = new System.Data.DataColumn("PoleGuid");

        System.Data.DataTable dtBranch = new System.Data.DataTable();
        System.Data.DataColumn dcCode = new System.Data.DataColumn("Code");
        System.Data.DataColumn dcUpNodeID = new System.Data.DataColumn("UpNodeID");
        System.Data.DataColumn dcDnNodeId = new System.Data.DataColumn("DnNodeID");
        System.Data.DataColumn dcLenght = new System.Data.DataColumn("Lenght");
        System.Data.DataColumn dcImpedance = new System.Data.DataColumn("Impedance");
        System.Data.DataColumn dcAdmitance = new System.Data.DataColumn("Admitance");
        System.Data.DataColumn dcCurrent = new System.Data.DataColumn("Current");
        System.Data.DataColumn dcTotalLoad = new System.Data.DataColumn("totalLoad");
        System.Data.DataColumn dcPowerLoss = new System.Data.DataColumn("PowerLoss");
        System.Data.DataColumn dcCondUtilization = new System.Data.DataColumn("CondUtilization");
        System.Data.DataColumn dcMinVoltage = new System.Data.DataColumn("MinVoltage");

        //public readonly ElectricalDataSet.NodesDataTable dtNodes;
        //public readonly ElectricalDataSet.BranchesDataTable dtBranches;
        public Complex TotalLoad
        {
            get
            {
                return _TotalLoad;
            }
        }

        ///<summary>
        ///مقدار دهی پارامترهای مورد نیاز برای شروع محاسبه پخش بار
        ///</summary>
        ///<param name="RootConsoleObjID">
        ///کد گرافیکی گره ورود توان
        ///</param>
        ///<param name="NomVolt">
        ///ولتاژ نامی شبکه
        ///</param>
        ///<param name="TevVolt">
        ///ولتاژ تونن مدار معادل شبکه بالادست
        ///</param>
        ///<param name="TevImp">
        ///امپدانس تونن مدار معادل شبکه بالادست
        ///</param>
        public LoadFlow(ObjectId RootConsoleObjID, double NomVolt, double TevVolt,
            Complex TevImp)
        {
            _RootConsoleObjID = RootConsoleObjID;
            _NomVolt = NomVolt;
            _TevVolt = TevVolt;
            _TevImp = (Complex)TevImp.Clone();
            dtNodes.Columns.Add(dcConsolGuid);
            dtNodes.Columns.Add(dcConsolObjId);
            dtNodes.Columns.Add(dcLoadCurrent);
            dtNodes.Columns.Add(dcLoadPower);
            dtNodes.Columns.Add(dcPoleGuid);
            dtNodes.Columns.Add(dcVoltage);
            dtNodes.Columns.Add(dcVoltageDropPer);

            dtBranch.Columns.Add(dcCode);
            dtBranch.Columns.Add(dcUpNodeID);
            dtBranch.Columns.Add(dcLenght);
            dtBranch.Columns.Add(dcImpedance);
            dtBranch.Columns.Add(dcAdmitance);
            dtBranch.Columns.Add(dcCurrent);
            dtBranch.Columns.Add(dcTotalLoad);
            dtBranch.Columns.Add(dcPowerLoss);
            dtBranch.Columns.Add(dcCondUtilization);
            dtBranch.Columns.Add(dcMinVoltage);
            dtBranch.Columns.Add(dcDnNodeId);

            //InitNodesTable();
            //dtBranches = new ElectricalDataSet.BranchesDataTable();
            //dtbranches
            //DataTable("Branches Table " + RootConsoleObjID.ToString());
            //dtBranches.Clear();
        }

        private void InitNodesTable()
        {
            dtNodes.Clear();
            /*
            dtNodes.Columns.Clear();
            dtNodes.Columns.Add("ConsoleGuid");//, System.Type.GetType("System.Guid"));
            dtNodes.Columns.Add("ConsoleObjId");
            dtNodes.Columns.Add("Voltage", System.Type.GetType("ComplexMath.Complex"));
            dtNodes.Columns.Add("VoltageDropPer", System.Type.GetType("System.Double"));
            */
        }

        ///<summary>
        ///محاسبه پخش بار
        ///</summary>
        ///<param name="MaxErr">
        ///حداکثر درصد مقدار قابل قبول تغییر ولتاژ گره ها
        ///</param>
        ///<param name="MaxIterations">
        ///حداکثر تعداد تکرار های روال پخش بار
        ///</param>
        public LoadFlowErr Calculate(double MaxErr, int MaxIterations)
        {
            double MaxDV = 0;
            int i;
            LoadFlowErr e;

            if (MaxErr <= 0) MaxErr = 0.001;
            if (MaxIterations <= 0) MaxIterations = 100;

            dtNodes.Clear();
            dtBranch.Clear();

            for (i = 0; (i <= MaxIterations); i++)
            {
                e = ProcessNode(InvalidConsObjId, null, ref MaxDV, i == 0);
                if (e != LoadFlowErr.NoError) return e;
                if ((i > 0) && (MaxDV / _NomVolt * 100 <= MaxErr)) break;
            }

            if (i > MaxIterations) return LoadFlowErr.NonConverging;
            foreach (DataRow dr1 in dtBranch.Rows)
            {
                _TotalLoss += Convert.ToDouble(dr1["PowerLoss"].ToString());
            }
            //_TotalLoss = dtBranch.Sum(br >= br.PowerLoss);
            foreach (DataRow dr in dtNodes.Rows)
            {
                if ((ObjectId)dr["ConsolObjID"] == InvalidConsObjId)

                    dr.Delete();
                else
                    
                    dr["VoltageDropPer"] = ((Electrical.Complex)dr["VoltageDropPer"]).abs / _NomVolt * 100;
            }
            foreach (DataRow  drb in dtBranch.Rows)
            {
                if (new Guid(drb["Code"].ToString()) == Guid.Empty)
                    drb.Delete();
                else
                    drb["CondUtilization"] = ((Complex)drb["Current"]).abs / Convert.ToDouble(drb["CondUtilization"])*100;
                  
            }

            return LoadFlowErr.NoError;
        }

        /// <summary>
        /// تابع بازگشتی یافتن درخت شبکه
        /// </summary>
        /// <param name="ConsoleObjId"></param>
        /// <param name="UpBranchGuid"></param>
        /// <param name="MaxDV"></param>
        /// <param name="FirstRun"></param>
        /// <returns></returns>
        private LoadFlowErr ProcessNode(
            ObjectId ConsoleObjId,
            System.Data.DataRow UpBranch, //Guid?
            ref double MaxDV,
            bool FirstRun)
        {
            bool TevNode = (ConsoleObjId == InvalidConsObjId);
            System.Data.DataRow curNode;
            System.Data.DataTable dtConsInfo = null;
            //ElectricalDataSet.BranchesRow UpBranch = null;
            Complex brCurrent, brLoad;
            double brMinVolt;
            Guid brGuid;
            LoadFlowErr e;
            System.Data.DataRow curBranch;

            if (TevNode)
            {
                dtConsInfo = new System.Data.DataTable(); 
                dtConsInfo.Columns.Add("BranchGuid");
                DataRow b = dtConsInfo.NewRow();
                b["BranchGuid"] = Guid.Empty.ToString();
                dtConsInfo.Rows.Add(b);
            }
            else
                dtConsInfo = GetConsolInfoByObjectId(ConsoleObjId);
            //if (UpBranchGuid.HasValue)
            //{
            //    UpBranch = dtBranches.Single(br => br.Code == UpBranchGuid.Value);
            //}

            if (FirstRun)
            {
                curNode = AddNode(TevNode ? null : dtConsInfo.Rows[0]);
            }
            else
            {
               DataRow[] curNode1 = dtNodes.Select("ConsolObjectId Like '" + ConsoleObjId.ToString() + "'");// FindByConsoleObjId(ConsoleObjId);
               curNode = curNode1[0];
                if (UpBranch != null)
                {
                    Complex v = CalcNodeVolt(UpBranch);
                    if (v.real <= 0) return LoadFlowErr.NonConverging;
                    double dv = ((Complex)(curNode["Voltage"]) - v).abs;
                    MaxDV = Math.Max(MaxDV, dv);
                    curNode["Voltage"] = v;
                }
            }

            if (!TevNode && (CalcNodeLoad(curNode) != 0)) return LoadFlowErr.LoadCalcErr;
            brCurrent = ((Complex)curNode["LoadCurrent"]);
            brLoad = ((Complex)curNode["LoadPower"]);
            brMinVolt = ((Complex)curNode["Voltage"]).abs;

            ObjectId NextNode;
            foreach (DataRow br in dtConsInfo.Rows)
            {
                brGuid = new Guid((string)br["BranchGuid"]);
                
                if ((UpBranch != null) && (brGuid ==new Guid(UpBranch["Code"].ToString())))
                    continue;
                NextNode = TevNode ? _RootConsoleObjID :
                   Atend.Global.Acad.UAcad.GetNextConsol(ConsoleObjId, brGuid);

                if (FirstRun)
                {
                    DataRow[] dr=dtNodes.Select( "ConsoleObjId Like '"+NextNode.ToString()+"'");
                    if (dr.Length>0)
                        return LoadFlowErr.LoopExists;
                    curBranch = AddBranch(brGuid, ConsoleObjId, NextNode);
                    if (curBranch == null) return LoadFlowErr.ImpCalcErr;
                }
                else
                {
                    System.Data.DataRow[] curBranch1 = dtBranch.Select("Code like '" + brGuid.ToString() + "'");
                    curBranch = curBranch1[0];
                }

                e = ProcessNode(NextNode, curBranch, ref MaxDV, FirstRun);
                if (e != LoadFlowErr.NoError) return e;

                brCurrent +=(Complex)curBranch["Current"];
                brLoad += (Complex)curBranch["TotalLoad"];
                CalcBranchLoss(curBranch);
                brMinVolt = Math.Min(brMinVolt,Convert.ToDouble(curBranch["MinVoltage"]));
            }

            if (UpBranch == null)
            {
                _InputCurrent = brCurrent;
                _TotalLoad = brLoad;
                _MinVoltage = brMinVolt;
            }
            else
            {
                UpBranch["Current"] = brCurrent;
                UpBranch["TotalLoad"] = brLoad;
                UpBranch["MinVoltage"] = brMinVolt;
            }
            
            return LoadFlowErr.NoError;
        }

        private void CalcBranchLoss(System.Data.DataRow curBranch)
        {
            curBranch["PowerLoss"] = 3 * Math.Pow(((Complex)curBranch["Current"]).abs, 2) *(( Complex) curBranch["Impedance"]).real;
        }

        private System.Data.DataRow AddBranch(Guid brGuid, ObjectId UpNodeId, ObjectId DnNodeId)
        {
            DataRow br = dtBranch.NewRow();
            br["Code"] = brGuid;
            br["UpNodeId"] = UpNodeId;
            br["DnNodeId"] = DnNodeId;
            if (CalcBranchImpAdm(brGuid, br) != 0) return null;
            dtBranch.Rows.Add(br);
            return br;
        }

        private int CalcBranchImpAdm(Guid brGuid, System.Data.DataRow br)
        {
            DataRow drBranch = GetBranchInfoByObjectId(brGuid);
            br["Lenght"] = (double)drBranch["Lenght"];
            throw new NotImplementedException();
        }

        private DataRow GetBranchInfoByObjectId(Guid brGuid)
        {
            throw new NotImplementedException();
        }

        private uint GetNextConsol(uint ConsoleObjId, Guid guid)
        {
            throw new NotImplementedException();
        }

        private int CalcNodeLoad(System.Data.DataRow curNode)
        {
            throw new NotImplementedException();
        }

        private Complex CalcNodeVolt(System.Data.DataRow UpBranch)
        {
            throw new NotImplementedException();
        }

        private System.Data.DataRow AddNode(DataRow drConsInfo)
        {
            System.Data.DataRow curNode;
            curNode = dtNodes.NewRow();
            if (drConsInfo == null)
            {
                
                curNode["ConsoleObjId"] = InvalidConsObjId;
                curNode["ConsoleGuid"] = Guid.Empty;
                curNode["PoleGuid"] = Guid.Empty;
            }
            else
            {
                curNode["ConsoleObjId"] = (uint)drConsInfo["ConsolObjectId"];
                curNode["ConsoleGuid"] = new Guid(drConsInfo["ConsolGuid"].ToString());
                curNode["PoleGuid"] = new Guid(drConsInfo["PoleGuid"].ToString());
            }
            /*if (curNode.Voltage == null)*/
            curNode["Voltage"] = new Complex(_TevVolt);
            curNode["VoltageDropPer"] = 0;
            curNode["LoadCurrent"] = new Complex();
            curNode["LoadPower"] = new Complex();
            dtNodes.Rows.Add(curNode);
            return curNode;
        }

        private System.Data.DataTable GetConsolInfoByObjectId(ObjectId ConsoleObjId)
        {
            throw new NotImplementedException();
        }
    }
}
