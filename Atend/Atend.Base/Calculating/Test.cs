using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Calculating
{
    public class Test
    {
        public static DataRow GetCondInfo(int CondCode, int CondProductType)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            System.Data.DataTable dtCond = new System.Data.DataTable();
            dtCond.Columns.Add("Resistance");
            dtCond.Columns.Add("Reactance");
            dtCond.Columns.Add("MaxCurrent");
            dtCond.Columns.Add("Capacitance");
            DataRow dr = dtCond.NewRow();
            //ed.writeMessage("**I Am In GetCondInfo\n");
            switch (((Atend.Control.Enum.ProductType)(CondProductType)))
            {
                case Atend.Control.Enum.ProductType.Conductor:
                    //Atend.Base.Equipment.EConductorTip condTip = Atend.Base.Equipment.EConductorTip.SelectByCode(CondCode);
                    Atend.Base.Equipment.EConductor cond = Atend.Base.Equipment.EConductor.SelectByCode(CondCode);
                    dr["Resistance"] = cond.Resistance.ToString();
                    dr["Reactance"] = cond.Reactance.ToString();
                    dr["MaxCurrent"] = cond.MaxCurrent.ToString();
                    dr["Capacitance"] = cond.Capacitance.ToString();
                    //ed.WriteMessage("@@@@@@@I AM IN GetCondInfo Re= " + cond.Resistance + "\n");
                    dtCond.Rows.Add(dr);
                    break;
                case Atend.Control.Enum.ProductType.SelfKeeper:
                    Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.SelectByCode(CondCode);
                    Atend.Base.Equipment.ESelfKeeper SelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(SelfTip.PhaseProductCode);
                    dr["Resistance"] = SelfKeeper.Resistance.ToString();
                    dr["Reactance"] = SelfKeeper.Reactance.ToString();
                    dr["MaxCurrent"] = SelfKeeper.MaxCurrent.ToString();
                    dr["Capacitance"] = SelfKeeper.Capacitance.ToString();
                    dtCond.Rows.Add(dr);
                    break;
                default:
                    Atend.Base.Equipment.EConductor cond2 = Atend.Base.Equipment.EConductor.SelectByCode(CondCode);
                    break;
            }
            //ed.writeMessage("Cond[Resistence=" + dtCond.Rows[0]["Resistance"].ToString()+"\n");
            return dtCond.Rows[0];

        }
    }
}
