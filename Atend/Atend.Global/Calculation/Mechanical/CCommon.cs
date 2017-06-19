using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.OleDb;
namespace Atend.Global.Calculation.Mechanical
{
   public class CCommon
    {
       public static int Code;
       public static double UTS;
       public static double Diagonal;
       public static double Alpha;
       public static string Name;
       public static double WC;
       public static double CrossSectionArea;
       public static double Alasticity;
       public static double Type;

       public static void SetBranchData(Atend.Base.Design.DBranch Branch, int BranchMode)
       {
           Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
          // ed.WriteMessage("Branch>productType={0},Mode={1},BranchGuid={2}\n",Branch.ProductType,BranchMode.ToString(),Branch.Code);
           if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
           {
               
               Atend.Base.Equipment.EConductorTip MyConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
               Atend.Base.Equipment.EConductor MyConductor = new Atend.Base.Equipment.EConductor();
               switch ((Atend.Control.Enum.BranchMode)BranchMode)
               {
                   case Atend.Control.Enum.BranchMode.Phase:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.PhaseProductCode);
                           //ed.WriteMessage("***MyMranch.Name={0}\n",MyConductor.Name);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Night:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.NightProductCode);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Netural:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.NeutralProductCode);
                           break;
                       }
               }
              

               
               Atend.Global.Calculation.Mechanical.CCommon.Code = MyConductor.Code;
               //double f=10e-6;
               //ed.WriteMessage("10^-6={0}\n", f.ToString());
               Atend.Global.Calculation.Mechanical.CCommon.Alpha = MyConductor.Alpha*Math.Pow(10,-6);
               Atend.Global.Calculation.Mechanical.CCommon.Diagonal = MyConductor.Diagonal;
               Atend.Global.Calculation.Mechanical.CCommon.UTS = MyConductor.UTS;
               Atend.Global.Calculation.Mechanical.CCommon.Name = MyConductor.Name;
               Atend.Global.Calculation.Mechanical.CCommon.Alasticity = MyConductor.Alasticity;
               Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea = MyConductor.CrossSectionArea;
               Atend.Global.Calculation.Mechanical.CCommon.WC = MyConductor.Wc;
               Atend.Global.Calculation.Mechanical.CCommon.Type = MyConductor.TypeCode;
               //ed.WriteMessage("**************ALPHA={0},ConductorALPHA={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Alpha, MyConductor.Alpha);

           }
           else
           {
               Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Branch.ProductCode);
               Atend.Base.Equipment.ESelfKeeper MySelfKeeper = new Atend.Base.Equipment.ESelfKeeper();
               switch ((Atend.Control.Enum.BranchMode)BranchMode)
               {
                   case Atend.Control.Enum.BranchMode.Phase:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.PhaseProductCode);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Night:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Netural:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                           break;
                       }
               }
               Atend.Global.Calculation.Mechanical.CCommon.Code = MySelfKeeper.Code;
               Atend.Global.Calculation.Mechanical.CCommon.Alpha = MySelfKeeper.Alpha * Math.Pow(10, -6);
               Atend.Global.Calculation.Mechanical.CCommon.Diagonal = MySelfKeeper.Diagonal;
               Atend.Global.Calculation.Mechanical.CCommon.UTS = MySelfKeeper.UTS;
               Atend.Global.Calculation.Mechanical.CCommon.Name = MySelfKeeper.Name;
               Atend.Global.Calculation.Mechanical.CCommon.Alasticity = MySelfKeeper.Alastisity;
               Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea = MySelfKeeper.CrossSectionKeeper;
               Atend.Global.Calculation.Mechanical.CCommon.WC = MySelfKeeper.Weight;

           }
       }



       public static void SetBranchData02(Atend.Base.Design.DBranch Branch, int BranchMode,OleDbConnection _Connection)
       {
           Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
           // ed.WriteMessage("Branch>productType={0},Mode={1},BranchGuid={2}\n",Branch.ProductType,BranchMode.ToString(),Branch.Code);
           if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
           {

               Atend.Base.Equipment.EConductorTip MyConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode,_Connection);
               Atend.Base.Equipment.EConductor MyConductor = new Atend.Base.Equipment.EConductor();
               switch ((Atend.Control.Enum.BranchMode)BranchMode)
               {
                   case Atend.Control.Enum.BranchMode.Phase:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.PhaseProductCode,_Connection);
                           //ed.WriteMessage("***MyMranch.Name={0}\n",MyConductor.Name);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Night:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.NightProductCode,_Connection);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Netural:
                       {
                           MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConductorTip.NeutralProductCode,_Connection);
                           break;
                       }
               }



               Atend.Global.Calculation.Mechanical.CCommon.Code = MyConductor.Code;
               //double f=10e-6;
               //ed.WriteMessage("10^-6={0}\n", f.ToString());
               Atend.Global.Calculation.Mechanical.CCommon.Alpha = MyConductor.Alpha * Math.Pow(10, -6);
               Atend.Global.Calculation.Mechanical.CCommon.Diagonal = MyConductor.Diagonal;
               Atend.Global.Calculation.Mechanical.CCommon.UTS = MyConductor.UTS;
               Atend.Global.Calculation.Mechanical.CCommon.Name = MyConductor.Name;
               Atend.Global.Calculation.Mechanical.CCommon.Alasticity = MyConductor.Alasticity;
               Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea = MyConductor.CrossSectionArea;
               Atend.Global.Calculation.Mechanical.CCommon.WC = MyConductor.Wc;
               //ed.WriteMessage("**************ALPHA={0},ConductorALPHA={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Alpha, MyConductor.Alpha);

           }
           else
           {
               Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Branch.ProductCode);
               Atend.Base.Equipment.ESelfKeeper MySelfKeeper = new Atend.Base.Equipment.ESelfKeeper();
               switch ((Atend.Control.Enum.BranchMode)BranchMode)
               {
                   case Atend.Control.Enum.BranchMode.Phase:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.PhaseProductCode);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Night:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                           break;
                       }
                   case Atend.Control.Enum.BranchMode.Netural:
                       {
                           MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                           break;
                       }
               }
               Atend.Global.Calculation.Mechanical.CCommon.Code = MySelfKeeper.Code;
               Atend.Global.Calculation.Mechanical.CCommon.Alpha = MySelfKeeper.Alpha * Math.Pow(10, -6);
               Atend.Global.Calculation.Mechanical.CCommon.Diagonal = MySelfKeeper.Diagonal;
               Atend.Global.Calculation.Mechanical.CCommon.UTS = MySelfKeeper.UTS;
               Atend.Global.Calculation.Mechanical.CCommon.Name = MySelfKeeper.Name;
               Atend.Global.Calculation.Mechanical.CCommon.Alasticity = MySelfKeeper.Alastisity;
               Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea = MySelfKeeper.CrossSectionKeeper;
               Atend.Global.Calculation.Mechanical.CCommon.WC = MySelfKeeper.Weight;

           }
       }

    }
}
