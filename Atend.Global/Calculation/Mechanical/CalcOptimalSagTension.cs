using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;

//get from tehran 7/15
namespace Atend.Global.Calculation.Mechanical
{
    public class CalcOptimalSagTension
    {
        double vTS;
        public double VTS
        {
            get { return vTS; }
            set { vTS = value; }
        }

        private double clearance;
        public double Clearance
        {
            get { return clearance; }
            set { clearance = value; }
        }

        private DataTable dtPoleSection;

        private double[] h = new double[10];
        public double[] H
        {
            get { return h; }
            set { h = value; }
        }

        private double[] f = new double[10];
        public double[] F
        {
            get { return f; }
            set { f = value; }
        }

        private double[] w = new double[10];
        public double[] W
        {
            get { return w; }
            set { w = value; }
        }

        //int start, end, distance;
        private int start;
        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        private int end;
        public int End
        {
            get { return end; }
            set { end = value; }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private double volt;
        public double Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        private Guid sectionCode;
        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }


        private bool hasConsol;

        public bool HasConsol
        {
            get { return hasConsol; }
            set { hasConsol = value; }
        }

        private Atend.Base.Equipment.EConsol consol;

        public Atend.Base.Equipment.EConsol Consol
        {
            get { return consol; }
            set { consol = value; }
        }


        private Atend.Base.Equipment.EClamp calamp;

        public Atend.Base.Equipment.EClamp Calamp
        {
            get { return calamp; }
            set { calamp = value; }
        }


        double LenghtChain = .45;
        double InsulatorDiamiter = .0266;
        double InsulatorShapeFactor = .5;
        public DataTable DtPoleSection
        {
            get { return dtPoleSection; }
            set { dtPoleSection = value; }
        }

        private DataTable dtconductorSection;

        public DataTable DtconductorSection
        {
            get { return dtconductorSection; }
            set { dtconductorSection = value; }
        }
        public DataTable dtStTable;
        public DataTable dtBranchList = new DataTable();
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        private DataTable dtPoleCond;

        public DataTable DtPoleCond
        {
            get { return dtPoleCond; }
            set { dtPoleCond = value; }
        }



        private System.Data.DataTable dtPackageParam = new DataTable();
        private System.Data.DataTable dtConsolParam = new DataTable();
        private System.Data.DataTable dtCalampParam = new DataTable();
        private System.Data.DataTable dtConductorTipParam = new DataTable();
        private System.Data.DataTable dtBranchParam = new DataTable();
        private System.Data.DataTable dtSelfKeeperTipParam = new DataTable();
        private System.Data.DataTable dtDConsolParam = new DataTable();
        private System.Data.DataTable dtDPoleSectionParam = new DataTable();
        private System.Data.DataTable dtPoleParm = new DataTable();
        private System.Data.DataTable dtPoleTipParm = new DataTable();



        OleDbConnection aConnection = new OleDbConnection();

        public CalcOptimalSagTension()
        {
            dtStTable = new DataTable();
            DataColumn conductorCode = new DataColumn("ConductorCode");
            DataColumn ConductorName = new DataColumn("ConductorName");
            DataColumn NormH = new DataColumn("NormH");
            DataColumn NormF = new DataColumn("NormF");

            DataColumn IceH = new DataColumn("IceH");
            DataColumn IceF = new DataColumn("IceF");

            DataColumn WindH = new DataColumn("WindH");
            DataColumn WindF = new DataColumn("WindF");

            DataColumn MaxTempH = new DataColumn("MaxTempH");
            DataColumn MaxTempF = new DataColumn("MaxTempF");

            DataColumn MinH = new DataColumn("MinTempH");
            DataColumn MinF = new DataColumn("MinTempF");

            DataColumn WindAndIceH = new DataColumn("WindAndIceH");
            DataColumn WindAndIceF = new DataColumn("WindAndIceF");
            DataColumn SectionCode = new DataColumn("SectionCode");

            DataColumn MaxF = new DataColumn("MAXF");
            dtStTable.Columns.Add(conductorCode);
            dtStTable.Columns.Add(ConductorName);
            dtStTable.Columns.Add(NormH);
            dtStTable.Columns.Add(NormF);
            dtStTable.Columns.Add(IceF);
            dtStTable.Columns.Add(IceH);
            dtStTable.Columns.Add(WindF);
            dtStTable.Columns.Add(WindH);
            dtStTable.Columns.Add(MaxTempF);
            dtStTable.Columns.Add(MaxTempH);
            dtStTable.Columns.Add(MinF);
            dtStTable.Columns.Add(MinH);
            dtStTable.Columns.Add(WindAndIceF);
            dtStTable.Columns.Add(WindAndIceH);
            dtStTable.Columns.Add(SectionCode);
            dtStTable.Columns.Add(MaxF);
            CreateConnection();
            dtPackageParam = Atend.Base.Design.DPackage.AccessSelectAll(aConnection);
            dtDConsolParam = Atend.Base.Design.DConsol.AccessSelectAll(aConnection);
            dtCalampParam = Atend.Base.Equipment.EClamp.AccessSelectAll(aConnection);
            dtBranchParam = Atend.Base.Design.DBranch.AccessSelectAll(aConnection);
            dtConductorTipParam = Atend.Base.Equipment.EConductorTip.AccessSelectAll(aConnection);
            dtSelfKeeperTipParam = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectAll(aConnection);
            dtConsolParam = Atend.Base.Equipment.EConsol.AccessSelectAll(aConnection);
            dtDPoleSectionParam = Atend.Base.Design.DPoleSection.AccessSelectAll(aConnection);
            dtPoleParm = Atend.Base.Equipment.EPole.AccessSelectAll(aConnection);
            dtPoleTipParm = Atend.Base.Equipment.EPoleTip.AccessSelectAll(aConnection);

        }

        //~CalcOptimalSagTension()
        //{
        //    //ed.WriteMessage("CLOSE CONNECTION\n");
        //    aConnection.Close();
        //}
        private void CreateConnection()
        {
            aConnection.ConnectionString = Atend.Control.ConnectionString.AccessCnString;
            try
            {
                if (aConnection.State == ConnectionState.Closed)
                    aConnection.Open();
            }
            catch
            {
                aConnection.Close();
            }
        }

        public void CloseConnection()
        {
            aConnection.Close();
        }
        //محاسبه اسپن بحرانی
        //////// public double computesc()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        ////////     Atend.Base.Design.DWeather weatherWind = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد
        ////////     //ed.WriteMessage("WeatherWind.IceDiagonal=" + weatherWind.IceDiagonal.ToString() + "weatherWind.Temp= " + weatherWind.Temp + "\n");
        ////////     Atend.Base.Design.DWeather weatherIce = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین
        ////////     //ed.WriteMessage("WeatherICE.IceDiagonal= " + weatherIce.IceDiagonal.ToString() + "weatherIce.Temp= " + weatherIce.Temp + "\n");


        ////////     //ed.WriteMessage("Diagonal={0},WC={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Diagonal, Atend.Global.Calculation.Mechanical.CCommon.WC);
        ////////     double iceweight = 913 * Math.PI * weatherIce.IceDiagonal * (weatherIce.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;//wic=913*3.14*i[i+d]*1e-6
        ////////     double WwForIce = Math.Pow(weatherIce.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherIce.IceDiagonal) * 1e-3;
        ////////     double WTotalForIce = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + iceweight, 2) + Math.Pow(WwForIce, 2));
        ////////     //ed.WriteMessage("WTotalForIce={0},iceWeight={1},WwForIce={2},WC={3}\n ", WTotalForIce.ToString(), iceweight, WwForIce, Atend.Global.Calculation.Mechanical.CCommon.WC);

        ////////     double windweight = Math.Pow(weatherWind.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherWind.IceDiagonal) * 1e-3;//ww=v^2/16*(d+2i)*10^-3
        ////////     double WiForWind = 913 * Math.PI * weatherWind.IceDiagonal * (weatherWind.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;
        ////////     double wTotalForWind = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + WiForWind, 2) + Math.Pow(windweight, 2));

        ////////     //ed.WriteMessage("wTotalForWind ={0},windWeight={1},WiForWind={2}\n", wTotalForWind.ToString(), windweight, WiForWind);
        ////////     double hc = ComputeHC();
        ////////     //ed.WriteMessage("HC= " + hc + "\n");
        ////////     //ed.WriteMessage("Conductor.Alpha= " + Atend.Global.Calculation.Mechanical.CCommon.Alpha + "\n");
        ////////     //if (WTotalForIce > wTotalForWind)
        ////////     //{
        ////////     //    //ed.WriteMessage("WTotalForIce > wTotalForWind\n");
        ////////     //    return -1;
        ////////     //}
        ////////     //else
        ////////     //{
        ////////     double sc = (24 * Math.Pow(hc, 2) * Atend.Global.Calculation.Mechanical.CCommon.Alpha * (weatherWind.Temp - weatherIce.Temp)) / (Math.Pow(wTotalForWind, 2) - Math.Pow(WTotalForIce, 2));
        ////////     //ed.WriteMessage("sc={0}\n", sc.ToString());
        ////////     sc = Math.Sqrt(Math.Abs(sc));
        ////////     //ed.WriteMessage("SC= " + sc.ToString() + "\n");
        ////////     return sc;
        ////////     //}
        //////// }

        //////// //محاسبه اسپن معادل
        //////// public double ComputeSE()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     double s1 = 0;
        ////////     double s2 = 0;
        ////////     //ed.WriteMessage("I Am In Compute SE\n");
        ////////     //ed.WriteMessage("dtConductorSection.Count= "+DtconductorSection.Rows.Count.ToString()+"\n");
        ////////     foreach (DataRow dr in dtconductorSection.Rows)
        ////////     {

        ////////         Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode((Guid)(dr["ProductCode"]));
        ////////         //ed.WriteMessage("MyBranch.Code= "+myBranch.Code.ToString()+"MyBranch.Lenght= "+myBranch.Lenght.ToString()+"\n");
        ////////         s1 += Math.Pow(myBranch.Lenght, 3);
        ////////         s2 += myBranch.Lenght;
        ////////     }
        ////////     double se = Math.Sqrt(s1 / s2);
        ////////     //ed.WriteMessage("SE= " + se.ToString() + "\n");
        ////////     return se;
        //////// }
        ////////         public void CalcSagTension()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //ed.WriteMessage("&&&&Start CalcSagTension\n");
        ////////     ////ed.WriteMessage("dtConductorSection.Count= {0}\n",DtconductorSection.Rows.Count);
        ////////     //ed.WriteMessage("dtconductorSection.Rows[0][ProductCode]= " + dtconductorSection.Rows[0]["ProductCode"].ToString()+"\n");
        ////////     Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));

        ////////     //ed.WriteMessage("MyBranch.ProductCode1= " + myBranch.ProductCode.ToString() + "\n");
        ////////     ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(.ProductCode);
        ////////     ////ed.WriteMessage("ConducTor.Code= "+conductor.Code.ToString()+"\n");

        ////////     double BaseH = ComputeHC();
        ////////     //ed.WriteMessage("BaseH= " + BaseH.ToString() + "\n");
        ////////     Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
        ////////     Atend.Base.Design.DWeather WeatherBase;
        ////////     Atend.Base.Design.DWeather WeatherSecond;
        ////////     double normalH;


        ////////     double se = ComputeSE();
        ////////     double sc = computesc();
        ////////     //ed.WriteMessage("SE={0},sc={1}\n", se.ToString(), sc.ToString());
        ////////     //if (sc == -1)
        ////////     //{
        ////////     //    //ed.WriteMessage("Sc=-1\n");
        ////////     //    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////     //}
        ////////     //else
        ////////     //{
        ////////     if (se > sc)
        ////////     {
        ////////         //ed.WriteMessage("Se>SC\n");
        ////////         WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////     }
        ////////     else
        ////////     {
        ////////         //ed.WriteMessage("Se<=SC\n");
        ////////         WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////     }
        ////////     //}
        ////////     WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی معمول شرط ثانویه می باشد
        ////////     //ed.WriteMessage("weatherBase={0},WeatherSecondCode={1}\n", WeatherBase.Code, WeatherSecond.Code);
        ////////     normalH = general.ComputeTension(WeatherBase, WeatherSecond, BaseH, se);
        ////////     h[0] = normalH;
        ////////     f[0] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[0]);
        ////////     //ed.WriteMessage("H[0]= " + h[0].ToString() + "   F[0]= " + f[0].ToString() + "\n");
        ////////     WeatherBase = WeatherSecond;


        ////////     int i = 1;
        ////////     do
        ////////     {

        ////////         WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////         //ed.WriteMessage("*weatherSecond.Code={0},i={1},ConditionCode={2},Temp={3}\n", WeatherSecond.Code, i, WeatherSecond.ConditionCode, WeatherSecond.Temp);

        ////////         h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[0], se);
        ////////         f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[i]);
        ////////         i++;
        ////////     } while (i < 6);
        ////////     //ed.WriteMessage("FiniSh\n");
        ////////     //for (i = 0; i < 6; i++)
        ////////     //{
        ////////     //    ed.WriteMessage("i = " + i.ToString() + "= " + h[i].ToString() + "\n");
        ////////     //}

        //////// }

        //////// public DataTable CalSagTension(Atend.Base.Design.DBranch MyBranch)
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     ed.WriteMessage("Sag1={0}\n", DateTime.Now);

        ////////     double fmax = IsSagOk();
        ////////     //ed.WriteMessage("fmaxxxxxxxx={0}\n",fmax);
        ////////     //ed.WriteMessage("Start CalcMyBranch.ProductCode={0}\n", MyBranch.ProductType);
        ////////     //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
        ////////     if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////     {
        ////////         //ed.WriteMessage("***Conductor\n");
        ////////         #region Conductor
        ////////         Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode);

        ////////         if (MyConducyorTip.PhaseCount > 0)
        ////////         {
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////             //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["ConductorCode"] = "0";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(h[0], 2); ;
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             //ed.WriteMessage("%%%%%%%%%fmax={0}\n", fmax);
        ////////             dr["MaxF"] = Math.Round(fmax, 2);
        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         if (MyConducyorTip.NightCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["ConductorCode"] = "1";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////             dr["NormH"] = Math.Round(h[0], 2);
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             dr["MaxF"] = Math.Round(fmax, 2);

        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         if (MyConducyorTip.NeutralCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["conductorCode"] = "2";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(h[0], 2);
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             dr["MaxF"] = Math.Round(fmax, 2);

        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         #endregion
        ////////     }
        ////////     else
        ////////     {
        ////////         #region SelfKeeper
        ////////         //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
        ////////         Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
        ////////         //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
        ////////         if (MySelfKeeperTip.PhaseCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["ConductorCode"] = "0";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(h[0], 2);
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             dr["MaxF"] = Math.Round(fmax, 2);

        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         if (MySelfKeeperTip.NightCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["ConductorCode"] = "1";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////             dr["NormH"] = Math.Round(h[0], 2);
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             dr["MaxF"] = Math.Round(fmax, 2);

        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         if (MySelfKeeperTip.NeutralCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////             CalcSagTension();
        ////////             DataRow dr = dtStTable.NewRow();
        ////////             dr["conductorCode"] = "2";
        ////////             dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(h[0], 2);
        ////////             dr["NormF"] = Math.Round(f[0], 2);
        ////////             dr["IceH"] = Math.Round(h[1], 2);
        ////////             dr["IceF"] = Math.Round(f[1], 2);
        ////////             dr["WindH"] = Math.Round(h[2], 2);
        ////////             dr["WindF"] = Math.Round(f[2], 2);
        ////////             dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////             dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////             dr["MinTempH"] = Math.Round(h[4], 2);
        ////////             dr["MinTempF"] = Math.Round(f[4], 2);
        ////////             dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////             dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////             dr["SectionCode"] = sectionCode;
        ////////             dr["MaxF"] = Math.Round(fmax, 2);

        ////////             dtStTable.Rows.Add(dr);
        ////////         }
        ////////         #endregion
        ////////     }
        ////////     ed.WriteMessage("sag2={0}\n", DateTime.Now);

        ////////     return dtStTable;
        //////// }

        //////// public double IsSagOk()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
        ////////     //Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////     //switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
        ////////     //{
        ////////     //    case Atend.Control.Enum.ProductType.Pole:
        ////////     //        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
        ////////     //        break;
        ////////     //    case Atend.Control.Enum.ProductType.PoleTip:
        ////////     //        Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
        ////////     //        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
        ////////     //        break;
        ////////     //}

        ////////     //double fmax = 0;
        ////////     ////bool isOK = true;
        ////////     //DataTable dtPackage = new DataTable();
        ////////     //dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////     ////DataTable dtPackageForInsulator = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtPackage.Rows[0]["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
        ////////     //if (dtPackage.Rows.Count != 0)
        ////////     //{
        ////////     //    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));
        ////////     //    //Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(dtPackageForInsulator.Rows[0]["ProductCode"]));
        ////////     //    double Depth = 0.1 * Convert.ToDouble(pole.Height) + 0.6;
        ////////     //    //ed.WriteMessage("Depth={0},consol.DistanseCrossArm={1},clearance={2}\n",Depth,consol.DistanceCrossArm,Clearance);

        ////////     //    fmax = pole.Height - (Depth + .45/*insulator.LenghtInsulatorChain */+ consol.DistanceCrossArm + Clearance);
        ////////     //}
        ////////     //else
        ////////     //{
        ////////     //    dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
        ////////     //    Atend.Base.Equipment.EClamp calamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));
        ////////     //    double Depth = 0.1 * Convert.ToDouble(pole.Height) + 0.6;
        ////////     //    fmax = pole.Height - (Depth + .45/*insulator.LenghtInsulatorChain */+ calamp.DistanceSupport + Clearance);
        ////////     //}


        ////////     //return fmax;















        ////////     Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////     //ed.WriteMessage("PoleHeight= " + pole.Height.ToString() + "\n");
        ////////     Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
        ////////     Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////     switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
        ////////     {
        ////////         case Atend.Control.Enum.ProductType.Pole:
        ////////             pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
        ////////             break;
        ////////         case Atend.Control.Enum.ProductType.PoleTip:
        ////////             Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
        ////////             pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
        ////////             break;
        ////////     }



        ////////     double Depth = .1 * pole.Height + .6;
        ////////     double ke = 1;

        ////////     //ed.WriteMessage("Ke= " + ke.ToString() + "\n");
        ////////     //ed.WriteMessage("Ke= "+ke.ToString()+"\n");
        ////////     double fmax1 = 0;
        ////////     DataTable dtPackage = new DataTable();
        ////////     dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

        ////////     if (dtPackage.Rows.Count != 0)
        ////////     {
        ////////         //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
        ////////         fmax1 = pole.Height - (Depth + Calamp.DistanceSupport + clearance /*+ZaribEtminan*/);
        ////////         //ed.WriteMessage("FMAX!={0}\n",fmax1);
        ////////     }

        ////////     //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
        ////////     double fmax2 = 0;
        ////////     double FMAX = fmax1;
        ////////     dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

        ////////     if (dtPackage.Rows.Count != 0)
        ////////     {

        ////////         //Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));

        ////////         double Vn = Volt / 1000;
        ////////         if (cke != null)
        ////////         {
        ////////             switch (Consol.Type)
        ////////             {
        ////////                 case 0: { ke = cke.Vertical; break; }
        ////////                 case 1: { ke = cke.Triangle; break; }
        ////////                 case 2: { ke = cke.Horizental; break; }


        ////////             }

        ////////         }
        ////////         //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n",Depth,LenghtChain,consol.DistanceCrossArm,Clearance,ZaribEtminan);
        ////////         //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
        ////////         fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance /*+ ZaribEtminan*/);
        ////////         //ed.WriteMessage("fMAx1={0}\n",fmax1);
        ////////         //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
        ////////         fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
        ////////         if (fmax1 < fmax2)
        ////////             FMAX = fmax1;
        ////////         else
        ////////             FMAX = fmax2;
        ////////     }

        ////////     //ed.WriteMessage("**********FMAX={0}\n", FMAX);

        ////////     return FMAX;


        //////// }

        //////// public DataTable FindHMax()
        //////// {
        ////////     double[] hMax = new double[6];
        ////////     DataTable dtHMax = new DataTable();
        ////////     DataColumn dcHNorm = new DataColumn("NormH");
        ////////     DataColumn dcHIce = new DataColumn("IceH");
        ////////     DataColumn dcHWind = new DataColumn("WindH");
        ////////     DataColumn dcHMaxTemp = new DataColumn("MaxTempH");
        ////////     DataColumn dcHMinTemp = new DataColumn("MinTempH");
        ////////     DataColumn dchWindIce = new DataColumn("WindIceH");
        ////////     dtHMax.Columns.Add(dcHNorm);
        ////////     dtHMax.Columns.Add(dcHIce);
        ////////     dtHMax.Columns.Add(dcHWind);
        ////////     dtHMax.Columns.Add(dcHMaxTemp);
        ////////     dtHMax.Columns.Add(dcHMinTemp);
        ////////     dtHMax.Columns.Add(dchWindIce);
        ////////     double UTS;
        ////////     Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(DtconductorSection.Rows[0]["ProductCode"].ToString()));
        ////////     if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////     {
        ////////         #region Conductor
        ////////         Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode);

        ////////         if (MyConducyorTip.PhaseCount > 0)
        ////////         {
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));

        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////                 //ed.WriteMessage("HMAX={0},i={1}\n",hMax[i].ToString(),i.ToString());
        ////////             }
        ////////             DataRow dr = dtHMax.NewRow();
        ////////             //dr["ConductorCode"] = "0";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////             //dr["SectionCode"] = sectionCode;

        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         if (MyConducyorTip.NightCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////             }

        ////////             DataRow dr = dtHMax.NewRow();
        ////////             // dr["ConductorCode"] = "1";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////             //dr["SectionCode"] = sectionCode;


        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         if (MyConducyorTip.NeutralCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));

        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////             }
        ////////             DataRow dr = dtHMax.NewRow();
        ////////             //dr["conductorCode"] = "2";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////             //dr["SectionCode"] = sectionCode;


        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         #endregion
        ////////     }
        ////////     else
        ////////     {
        ////////         //Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
        ////////         //Atend.Base.Equipment.ESelfKeeper Self = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfTip.PhaseProductCode);
        ////////         //UTS = Self.UTS;
        ////////         #region SelfKeeper
        ////////         //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
        ////////         Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
        ////////         //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
        ////////         if (MySelfKeeperTip.PhaseCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////             }
        ////////             DataRow dr = dtHMax.NewRow();
        ////////             //dr["ConductorCode"] = "0";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);



        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         if (MySelfKeeperTip.NightCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////             }
        ////////             DataRow dr = dtHMax.NewRow();
        ////////             //dr["ConductorCode"] = "1";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////             //dr["SectionCode"] = sectionCode;


        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         if (MySelfKeeperTip.NeutralCount > 0)
        ////////         {
        ////////             //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////             //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////             Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////             for (int i = 0; i < 6; i++)
        ////////             {
        ////////                 Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                 hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////             }
        ////////             DataRow dr = dtHMax.NewRow();
        ////////             //dr["conductorCode"] = "2";
        ////////             //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////             dr["NormH"] = Math.Round(hMax[0], 2);

        ////////             dr["IceH"] = Math.Round(hMax[1], 2);

        ////////             dr["WindH"] = Math.Round(hMax[2], 2);

        ////////             dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////             dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////             dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////             //dr["SectionCode"] = sectionCode;


        ////////             dtHMax.Rows.Add(dr);
        ////////         }
        ////////         #endregion
        ////////     }


        ////////     return dtHMax;

        //////// }

        //////// public double CalcMaxFlash()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     double FMax = f[0];
        ////////     for (int i = 1; i < 6; i++)
        ////////     {
        ////////         if (f[i] > FMax)
        ////////             FMax = f[i];
        ////////     }
        ////////     return FMax;
        //////// }

        //////// public void MinPc()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     DataTable dtBest = new DataTable();
        ////////     DataColumn dc = new DataColumn("CrossSectionArea");
        ////////     dtBest.Columns.Add(dc);
        ////////     DataColumn dc1 = new DataColumn("Type");
        ////////     dtBest.Columns.Add(dc1);
        ////////     DataColumn dc2 = new DataColumn("TypeName");
        ////////     dtBest.Columns.Add(dc2);


        ////////     double ke = 0;
        ////////     Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode((Guid)(dtconductorSection.Rows[0]["ProductCode"]));
        ////////     Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(myBranch.ProductCode);

        ////////     Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(DtPoleSection.Rows[0]["ProductCode"]));
        ////////     DataTable dtdConsol = Atend.Base.Design.DConsol.AccessSelectByParentCode(myNode.Code);
        ////////     Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(dtdConsol.Rows[0]["ProductCode"]));

        ////////     Atend.Base.Design.DPoleInfo myPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);

        ////////     Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(conductor.CrossSectionArea);
        ////////     switch (eConsol.Type)
        ////////     {
        ////////         case 0: { ke = cke.Vertical; break; }
        ////////         case 1: { ke = cke.Triangle; break; }
        ////////         case 2: { ke = cke.Horizental; break; }

        ////////     }
        ////////     double pc = (ke * Math.Sqrt(CalcMaxFlash() * LenghtChain)) + (volt / 150);
        ////////     ed.WriteMessage(string.Format("حداقل فاصله فازی مورد نیاز{0}می باشد", pc.ToString()));
        ////////     DataTable dtconsol = Atend.Base.Equipment.EConsol.SelectLessPc(pc);


        //////// }

        //////// public DataTable CalcTempTable()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     ed.WriteMessage("Temp1={0}\n", DateTime.Now);

        ////////     Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
        ////////     //ed.WriteMessage("CALC TEMP Table\n");
        ////////     if (End < Start)
        ////////     {
        ////////         //ed.WriteMessage("ExchangeValue\n");
        ////////         int t = End;
        ////////         End = Start;
        ////////         Start = t;
        ////////     }
        ////////     Atend.Base.Design.DWeather Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی نرمال 
        ////////     double hTempTable, fTempTable;
        ////////     DataTable dtTempTable = new DataTable();

        ////////     DataColumn dcColumn2 = new DataColumn("From");
        ////////     DataColumn dcColumn3 = new DataColumn("To");
        ////////     DataColumn dcColumn1 = new DataColumn("SpanLenght");
        ////////     DataColumn dcColumn4 = new DataColumn("ConductorName");

        ////////     dtTempTable.Columns.Add(dcColumn2);
        ////////     dtTempTable.Columns.Add(dcColumn3);
        ////////     dtTempTable.Columns.Add(dcColumn1);
        ////////     dtTempTable.Columns.Add(dcColumn4);
        ////////     //ed.WriteMessage("End= " + End.ToString() + "Start= " + Start.ToString() + "Distance =" + Distance.ToString() + "\n");
        ////////     for (int i = end; i > start; i = i - distance)
        ////////     {
        ////////         //ed.WriteMessage("I Am In The For to BuildColumn\n");
        ////////         DataColumn dtColumn = new DataColumn(i.ToString());
        ////////         DataColumn dtColumnF = new DataColumn(i.ToString() + "F");
        ////////         dtTempTable.Columns.Add(dtColumn);
        ////////         dtTempTable.Columns.Add(dtColumnF);
        ////////     }

        ////////     foreach (DataRow dr in dtconductorSection.Rows)
        ////////     {
        ////////         Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["ProductCode"].ToString()));
        ////////         //ed.WriteMessage("MyBranch.ProductCode= " + myBranch.ProductCode.ToString() + "\n");
        ////////         if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////         {
        ////////             Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode);
        ////////             //ed.WriteMessage("**\n");
        ////////             DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));

        ////////             Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node1Guid"].ToString()));
        ////////             Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node2Guid"].ToString()));
        ////////             if (ConductorTip.PhaseCount > 0)
        ////////             {
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                 DataRow NewRow = dtTempTable.NewRow();



        ////////                 NewRow["From"] = dNumLeft.Number;
        ////////                 NewRow["To"] = dNumRight.Number;
        ////////                 NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                 for (int i = end; i > start; i = i - distance)
        ////////                 {
        ////////                     DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));

        ////////                     //ed.WriteMessage("NormH={0}\n",drCond[0]["NormH"].ToString());
        ////////                     hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                     fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                     NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                     NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                     NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                 }
        ////////                 //ed.WriteMessage("**********************Add\n");
        ////////                 dtTempTable.Rows.Add(NewRow);
        ////////             }


        ////////             if (ConductorTip.NightCount > 0)
        ////////             {
        ////////                 //ed.WriteMessage("NIGHT\n");
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));

        ////////                 DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));
        ////////                 if (drCond.Length > 0)
        ////////                 {
        ////////                     DataRow NewRow = dtTempTable.NewRow();


        ////////                     NewRow["From"] = dNumLeft.Number;
        ////////                     NewRow["To"] = dNumRight.Number;
        ////////                     //CalcSagTension();
        ////////                     NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                     for (int i = end; i > start; i = i - distance)
        ////////                     {
        ////////                         //ed.WriteMessage("I am In CalcTemp\n");
        ////////                         hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                         fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                         NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                         NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                         NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                     }
        ////////                     dtTempTable.Rows.Add(NewRow);
        ////////                 }
        ////////             }
        ////////             if (ConductorTip.NeutralCount > 0)
        ////////             {
        ////////                 //ed.WriteMessage("Netural\n");
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));

        ////////                 //CalcSagTension();
        ////////                 DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));

        ////////                 //ed.WriteMessage("BEFOR FOR\n");
        ////////                 if (drCond.Length > 0)
        ////////                 {
        ////////                     DataRow NewRow = dtTempTable.NewRow();


        ////////                     NewRow["From"] = dNumLeft.Number;
        ////////                     NewRow["To"] = dNumRight.Number;
        ////////                     NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                     for (int i = end; i > start; i = i - distance)
        ////////                     {
        ////////                         //ed.WriteMessage("I am In CalcTemp {0}\n", drCond[0]["NormH"].ToString());
        ////////                         hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                         fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                         NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                         NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                         NewRow["SpanLenght"] = myBranch.Lenght;

        ////////                     }
        ////////                     dtTempTable.Rows.Add(NewRow);
        ////////                 }
        ////////             }
        ////////             //ed.WriteMessage("FINISH CONDUCTOR\n");
        ////////         }
        ////////         else
        ////////         {
        ////////             //ed.WriteMessage("SELFKEEPER\n");
        ////////             Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(myBranch.ProductCode);
        ////////             DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));

        ////////             Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node1Guid"].ToString()));
        ////////             Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node2Guid"].ToString()));



        ////////             if (SelfKeeperTip.PhaseCount > 0)
        ////////             {
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
        ////////                 DataRow NewRow = dtTempTable.NewRow();
        ////////                 //foreach (DataRow dr1 in DtPoleSection.Rows)
        ////////                 //{
        ////////                 //    if (MyNode.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is LeftNode\n");
        ////////                 //        NewRow["From"] = dr1["PoleNumber"].ToString();

        ////////                 //    }
        ////////                 //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is RightNode\n");
        ////////                 //        NewRow["To"] = dr1["PoleNumber"].ToString();
        ////////                 //    }
        ////////                 //}
        ////////                 NewRow["From"] = dNumLeft.Number;
        ////////                 NewRow["To"] = dNumRight.Number;
        ////////                 //CalcSagTension();
        ////////                 NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                 DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));
        ////////                 for (int i = end; i > start; i = i - distance)
        ////////                 {
        ////////                     //ed.WriteMessage("I am In CalcTemp\n");
        ////////                     hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                     fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                     NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                     NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                     NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                 }
        ////////                 dtTempTable.Rows.Add(NewRow);
        ////////             }
        ////////             if (SelfKeeperTip.NightCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                 DataRow NewRow = dtTempTable.NewRow();
        ////////                 //foreach (DataRow dr1 in DtPoleSection.Rows)
        ////////                 //{
        ////////                 //    if (MyNode.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is LeftNode\n");
        ////////                 //        NewRow["From"] = dr1["PoleNumber"].ToString();

        ////////                 //    }
        ////////                 //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is RightNode\n");
        ////////                 //        NewRow["To"] = dr1["PoleNumber"].ToString();
        ////////                 //    }
        ////////                 //}
        ////////                 NewRow["From"] = dNumLeft.Number;
        ////////                 NewRow["To"] = dNumRight.Number;
        ////////                 //CalcSagTension();
        ////////                 NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                 DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));
        ////////                 for (int i = end; i > start; i = i - distance)
        ////////                 {
        ////////                     //ed.WriteMessage("I am In CalcTemp\n");
        ////////                     hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                     fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                     NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                     NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                     NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                 }
        ////////                 dtTempTable.Rows.Add(NewRow);
        ////////             }
        ////////             if (SelfKeeperTip.NeutralCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                 DataRow NewRow = dtTempTable.NewRow();
        ////////                 //foreach (DataRow dr1 in DtPoleSection.Rows)
        ////////                 //{
        ////////                 //    if (MyNode.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is LeftNode\n");
        ////////                 //        NewRow["From"] = dr1["PoleNumber"].ToString();

        ////////                 //    }
        ////////                 //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
        ////////                 //    {
        ////////                 //        //ed.WriteMessage("Current Is RightNode\n");
        ////////                 //        NewRow["To"] = dr1["PoleNumber"].ToString();
        ////////                 //    }
        ////////                 //}
        ////////                 NewRow["From"] = dNumLeft.Number;
        ////////                 NewRow["To"] = dNumRight.Number;
        ////////                 //CalcSagTension();
        ////////                 NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                 DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));
        ////////                 for (int i = end; i > start; i = i - distance)
        ////////                 {
        ////////                     //ed.WriteMessage("I am In CalcTemp\n");
        ////////                     hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                     fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                     NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                     NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                     NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                 }
        ////////                 dtTempTable.Rows.Add(NewRow);
        ////////             }




        ////////         }

        ////////         //}
        ////////         //ed.WriteMessage("Add Row To DtTempTAble\n");

        ////////         //ed.WriteMessage("dtTempTable.Rows.Count= " + dtTempTable.Rows.Count.ToString() + "\n");
        ////////     }
        ////////     //ed.WriteMessage("&&&&FINISH****\n");
        ////////     ed.WriteMessage("TEMP2={0}\n", DateTime.Now);

        ////////     return dtTempTable;
        //////// }


        ////////private double WindOnPoleF2(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather weather, DataTable dtConductor)
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //DataRow[] dr = dtConductor.Select(string.Format(" NodeType='{0}'", Atend.Control.Enum.ProductType.Consol));

        ////////     //ed.WriteMessage("Start WindOnPoleF\n");
        ////////     double L = pole.Height - (0.1 * pole.Height + .6);
        ////////     double AW = (L * (pole.ButtomCrossSectionArea + pole.TopCrossSectionArea)) / 2;
        ////////     double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
        ////////     double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;

        ////////     //if (dr.Length != 0)
        ////////     //{
        ////////     //    //ed.WriteMessage("It Is cOnsol\n");
        ////////     //    //////Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
        ////////     //    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam,new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
        ////////     //    //////Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dPack.ProductCode);
        ////////     //    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode (dtConsolParam,dPack.ProductCode);
        ////////     //    h1 = L - consol.DistanceCrossArm;

        ////////     //}
        ////////     //else
        ////////     //{
        ////////     //    Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam,new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
        ////////     //    //////Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
        ////////     //    Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam,dPackage.ProductCode);
        ////////     //    h1 = L - Clamp.DistanceSupport;
        ////////     //}



        ////////     double KFactor = .0812;
        ////////     if (pole.Shape == 0)
        ////////     {
        ////////         KFactor = .05;
        ////////     }
        ////////     //ed.WriteMessage("BCrossSection= " + pole.ButtomCrossSectionArea + "TCrossSectionArea= " + pole.TopCrossSectionArea + "K=" + KFactor + "\n");
        ////////     //ed.WriteMessage("L=" + L + " Aw" + AW + " h=" + h + "H1=" + h1 + " weather=" + weather.Code + "\n");
        ////////     double Result = (h / h1) * KFactor * AW * Math.Pow(weather.WindSpeed, 2);
        ////////     //ed.WriteMessage("PoleF= " + Result + "\n");
        ////////     return Result;
        //////// }

        //////// private double WindOnInsulator2(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather Weather, int CounOfCrossArm)
        //////// {
        ////////     //ed.WriteMessage("CountOfCross= "+CounOfCrossArm+" InsulatorDiameter= "+InsulatorDiamiter+" Shape= "+InsulatorShapeFactor+" LenghtChain= "+LenghtChain+"\n");
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //ed.WriteMessage("Start WindOnInsulator\n");
        ////////     return 3 * CounOfCrossArm * LenghtChain * InsulatorDiamiter * InsulatorShapeFactor * Math.Pow(Weather.WindSpeed, 2) / 16 * 1e-3;
        //////// }

        //////// private double WindOnConductor2(DataTable dtConductor, Atend.Base.Design.DWeather weather, Atend.Base.Equipment.EPole pole)
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //ed.WriteMessage("Start WindOnConductor\n");
        ////////     double sum = 0;
        ////////     //ed.WriteMessage("I Am In WindOnConductor\n");
        ////////     foreach (DataRow dr in dtConductor.Rows)
        ////////     {
        ////////         //////Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["BranchGuid"].ToString()));
        ////////         Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////         if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////         {
        ////////             //////Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
        ////////             Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, Branch.ProductCode);
        ////////             if (conductorTip.PhaseCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                 //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
        ////////                 sum += conductorTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////             if (conductorTip.NightCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
        ////////                 //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                 sum += conductorTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////             if (conductorTip.NeutralCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
        ////////                 //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                 sum += conductorTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////         }
        ////////         else
        ////////         {
        ////////             Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, Branch.ProductCode);
        ////////             if (SelfKeeperTip.PhaseCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                 //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
        ////////                 sum += SelfKeeperTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////             if (SelfKeeperTip.NightCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
        ////////                 //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                 sum += SelfKeeperTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////             if (SelfKeeperTip.NeutralCount > 0)
        ////////             {
        ////////                 //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
        ////////                 //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
        ////////                 Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                 sum += SelfKeeperTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////             }
        ////////         }
        ////////     }
        ////////     //ed.WriteMessage("******Sum={0}\n",sum);
        ////////     double L = pole.Height - (0.1 * pole.Height + .6);
        ////////     double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
        ////////     double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;
        ////////     //ed.WriteMessage("h={0},h1={1}\n",h,h1);
        ////////     sum = sum * (h / h1);
        ////////     //ed.WriteMessage("Sum =" + sum.ToString() + "\n");
        ////////     return sum;
        //////// }



        //////// private CVector WindOnTangantPole2(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //ed.WriteMessage("WindOnTangantPole\n");
        ////////     Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
        ////////     CVector SumTension = new CVector();


        ////////     SumTension.Absolute = 0;
        ////////     SumTension.Angle = 0;
        ////////     //ed.WriteMessage("*************Tangant\n");
        ////////     foreach (DataRow dr in dtConductor.Rows)
        ////////     {
        ////////         //ed.WriteMessage("%%%\n");
        ////////         if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////         {
        ////////             Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(dr["NodeGuid"].ToString()));
        ////////             //ed.WriteMessage("*\n");
        ////////             Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////             if (Econsol.ConsolType == 2 || Econsol.ConsolType == 3)
        ////////             {
        ////////                 Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

        ////////                 //ed.WriteMessage("It Is Tangant Consol\n");
        ////////                 Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////                 DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
        ////////                 if (drSec.Length == 0)
        ////////                 {

        ////////                     //Must Be Change
        ////////                     DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor),aConnection);
        ////////                     sectionCode = Section.SectionCode;
        ////////                     CalSagTension(myBranch);
        ////////                 }
        ////////                 Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                 //ed.WriteMessage("SwichTAngant\n");
        ////////                 if (conductorTip.PhaseCount > 0)
        ////////                 {
        ////////                     int ConductorCode = 0;
        ////////                     //ed.WriteMessage("9-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);
        ////////                     #region IF
        ////////                     //ed.WriteMessage("****ConductorPhase=\n");

        ////////                     if (i == 0)
        ////////                     {
        ////////                         //ed.WriteMessage("$$$pp");
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                         //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 1)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                         //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


        ////////                     }
        ////////                     else if (i == 2)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                         //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 3)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                         //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


        ////////                     }
        ////////                     else if (i == 4)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                         //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


        ////////                     }
        ////////                     else if (i == 5)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                         //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }


        ////////                     #endregion
        ////////                     //////#region Swich
        ////////                     ////////ed.WriteMessage("****ConductorPhase=\n");
        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            //ed.WriteMessage("$$$pp");
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     //ed.WriteMessage("**9-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);
        ////////                     Tension.Absolute *= conductorTip.PhaseCount;

        ////////                     Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                     //ed.WriteMessage("SumTension.absu={0},sum.angle={1},ten.abs={2},ten.ang={3}\n",SumTension.Absolute,SumTension.Angle,Tension.Absolute,Tension.Angle);
        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }


        ////////                 if (conductorTip.NightCount > 0)
        ////////                 {
        ////////                     //ed.WriteMessage("*******Night\n");
        ////////                     int ConductorCode = 1;
        ////////                     //ed.WriteMessage("9-1={0}Ni:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     #region IF


        ////////                     if (i == 0)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                         //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 1)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                         //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 2)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                         //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 3)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                         //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 4)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                         //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 5)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                         //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }


        ////////                     #endregion


        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                    // ed.WriteMessage("**9-1={0}Ni:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }
        ////////                 if (conductorTip.NeutralCount > 0)
        ////////                 {

        ////////                     //ed.WriteMessage("******NrtralCount\n");
        ////////                     int ConductorCode = 2;
        ////////                     //ed.WriteMessage("9-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


        ////////                     #region IF


        ////////                     if (i == 0)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                         //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 1)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                         //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 2)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                         //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 3)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                         //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 4)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                         //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 5)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                         //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                     }


        ////////                     #endregion

        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     //ed.WriteMessage("**9-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }
        ////////             }
        ////////         }
        ////////         else
        ////////         {
        ////////             //ed.WriteMessage("KAlamp\n");
        ////////             if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////             {
        ////////                 Atend.Base.Design.DPackage Dpack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dr["NodeGuid"].ToString()));
        ////////                 Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, Dpack.ProductCode);
        ////////                 //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                 if (ECalamp.Type == 5)
        ////////                 {
        ////////                     Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                     //ed.WriteMessage("It Is Tangant Consol\n");
        ////////                     Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////                     DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                     if (drSec.Length == 0)
        ////////                     {
        ////////                         DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                         sectionCode = Section.SectionCode;
        ////////                         CalSagTension(myBranch);
        ////////                     }

        ////////                     //CalSagTension(myBranch);
        ////////                     Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                     if (SelfKeeperTip.PhaseCount > 0)
        ////////                     {
        ////////                         int ConductorCode = 0;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     //ed.WriteMessage("NormH= "+dr1[0]["NormH"].ToString()+"Angle= "+dr["Angle"].ToString()+"\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;

        ////////                         Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }


        ////////                     if (SelfKeeperTip.NightCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 1;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }
        ////////                     if (SelfKeeperTip.NeutralCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 2;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }
        ////////                 }
        ////////             }
        ////////         }
        ////////     }

        ////////     //ed.WriteMessage("Tension.Absolute= "+SumTension.Absolute+"\n");
        ////////     //return SumTension.Absolute;

        ////////     //ed.WriteMessage("*********END Tangant\n");
        ////////     //ed.WriteMessage("sumTension={0}\n",SumTension.Absolute);
        ////////     return SumTension;

        //////// }




        //////// private CVector WindOnDDEPole2(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        //////// {
        ////////     //ed.WriteMessage("I AM In WindOnDDEPole\n");
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     //ed.WriteMessage("Start WindOnDDEPole\n");
        ////////     Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
        ////////     CVector SumTension = new CVector();
        ////////     CVector MaxTension = new CVector();
        ////////     MaxTension.Absolute = 0;
        ////////     MaxTension.Angle = 0;
        ////////     SumTension.Absolute = 0;
        ////////     SumTension.Angle = 0;
        ////////     //ed.WriteMessage("*********DDEPole\n");
        ////////     //****************X1=a+b+c
        ////////     //ed.WriteMessage("X1=a+b+c\n");
        ////////     foreach (DataRow drConductor in dtConductor.Rows)
        ////////     {
        ////////         if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////         {
        ////////             //ed.WriteMessage("NODEGUID={0}\n", drConductor["NodeGuid"].ToString());
        ////////             Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////             Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////             //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////             if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
        ////////             {
        ////////                 //ed.WriteMessage("**Entehaiiiiiiiiiiiiii={0},Name={1}\n",Econsol.ConsolType,Econsol.Name);
        ////////                 Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

        ////////                 Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
        ////////                 DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                 if (drSec.Length == 0)
        ////////                 {
        ////////                     DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////                     sectionCode = Section.SectionCode;
        ////////                     CalSagTension(myBranch);
        ////////                 }
        ////////                 //CalSagTension(myBranch);
        ////////                 //ed.WriteMessage("Angle={0}\n",drConductor["Angle"].ToString());
        ////////                 Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                 if (conductorTip.PhaseCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 0;
        ////////                     //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


        ////////                     #region IF


        ////////                     if (i == 0)
        ////////                     {

        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                         //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 1)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                         //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 2)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                         //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 3)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                         //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 4)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                         //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }
        ////////                     else if (i == 5)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                         //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                     }


        ////////                     #endregion



        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {

        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     //ed.WriteMessage("**10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }


        ////////                 if (conductorTip.NightCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 1;
        ////////                     //ed.WriteMessage("10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     #region IF



        ////////                     if (i == 0)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

        ////////                     }
        ////////                     else if (i == 1)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

        ////////                     }
        ////////                     else if (i == 2)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

        ////////                     }
        ////////                     else if (i == 3)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

        ////////                     }
        ////////                     else if (i == 4)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

        ////////                     }
        ////////                     else if (i == 5)
        ////////                     {
        ////////                         DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                         Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

        ////////                     }


        ////////                     #endregion


        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion

        ////////                     //ed.WriteMessage("**10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }
        ////////                 if (conductorTip.NeutralCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 2;
        ////////                     //ed.WriteMessage("10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     #region IF


        ////////                         if (i==0)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

        ////////                             }
        ////////                         else if (i==1)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

        ////////                             }
        ////////                         else if (i==2)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

        ////////                             }
        ////////                         else if (i==3)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

        ////////                             }
        ////////                         else if (i==4)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                 break;
        ////////                             }
        ////////                         else if (i==5)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

        ////////                             }


        ////////                     #endregion



        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     //ed.WriteMessage("**10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                     SumTension = SumTension.Add(SumTension, Tension);
        ////////                 }
        ////////             }
        ////////         }
        ////////         else
        ////////         {
        ////////             if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////             {
        ////////                 Atend.Base.Design.DPackage DPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(drConductor["NodeGuid"].ToString()));
        ////////                 Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(DPack.ProductCode);
        ////////                 //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                 if (ECalamp.Type == 1 || ECalamp.Type == 2 || ECalamp.Type == 3 || ECalamp.Type == 4 || ECalamp.Type == 6)
        ////////                 {
        ////////                     Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                     Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
        ////////                     DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                     if (drSec.Length == 0)
        ////////                     {
        ////////                         DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                         sectionCode = Section.SectionCode;
        ////////                         CalSagTension(myBranch);
        ////////                     }
        ////////                     //CalSagTension(myBranch);
        ////////                     Atend.Base.Equipment.ESelfKeeperTip SelfKeepeTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                     if (SelfKeepeTip.PhaseCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 0;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {

        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }


        ////////                     if (SelfKeepeTip.NightCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 1;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }
        ////////                     if (SelfKeepeTip.NeutralCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 2;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                         SumTension = SumTension.Add(SumTension, Tension);
        ////////                     }
        ////////                 }
        ////////             }
        ////////         }

        ////////     }
        ////////     //ed.WriteMessage("sumTension="+SumTension.Absolute+"\n");
        ////////     //****************************
        ////////     //ed.WriteMessage("Second\n");
        ////////     foreach (DataRow drConductor in dtConductor.Rows)
        ////////     {
        ////////         if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////         {
        ////////             Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////             Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////             if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
        ////////             {
        ////////                 //ed.WriteMessage("EEEEEEEEEntehai\n");
        ////////                 Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

        ////////                 Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                 if (conductorTip.PhaseCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 0;
        ////////                     //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


        ////////                     #region IF


        ////////                         if (i==0)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                 //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }
        ////////                         else if  (i==1)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                 //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }
        ////////                         else if (i==2)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                 //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }
        ////////                         else if (i==3)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                 //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }
        ////////                         else if (i==4)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                 //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }
        ////////                         else if (i==5)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                 //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

        ////////                             }


        ////////                     #endregion


        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                     MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                 }


        ////////                 if (conductorTip.NightCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 1;
        ////////                     //ed.WriteMessage("10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


        ////////                     #region IF


        ////////                         if (i==0)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

        ////////                             }
        ////////                         else if  (i==1)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

        ////////                             }
        ////////                         else if (i==2)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

        ////////                             }
        ////////                         else if (i==3)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

        ////////                             }
        ////////                         else if (i==4)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

        ////////                             }
        ////////                         else if (i==5)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

        ////////                             }


        ////////                     #endregion


        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion

        ////////                     //ed.WriteMessage("**10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                     MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                 }
        ////////                 if (conductorTip.NeutralCount > 0)
        ////////                 {

        ////////                     int ConductorCode = 2;
        ////////                     //ed.WriteMessage("10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


        ////////                     #region IF


        ////////                         if (i==0)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

        ////////                             }
        ////////                         else if (i==1)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

        ////////                             }
        ////////                         else if (i==2)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

        ////////                             }
        ////////                         else if (i==3)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

        ////////                         }
        ////////                         else if (i==4)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

        ////////                             }
        ////////                         else if (i==5)
        ////////                             {
        ////////                                 DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                 Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

        ////////                             }


        ////////                     #endregion


        ////////                     //////#region Swich

        ////////                     //////switch (i)
        ////////                     //////{
        ////////                     //////    case 0:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 1:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 2:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 3:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 4:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////    case 5:
        ////////                     //////        {
        ////////                     //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                     //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                     //////            break;
        ////////                     //////        };
        ////////                     //////}

        ////////                     //////#endregion
        ////////                     ed.WriteMessage("**10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

        ////////                     Tension.Absolute *= conductorTip.PhaseCount;
        ////////                     Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                     MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                 }
        ////////             }

        ////////         }
        ////////         else
        ////////         {
        ////////             if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////             {
        ////////                 Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////                 Atend.Base.Equipment.EClamp eCalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, dPackage.ProductCode);
        ////////                 if (eCalamp.Type == 0)
        ////////                 {
        ////////                     Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

        ////////                     Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                     if (SelfKeeperTip.PhaseCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 0;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                         MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                     }


        ////////                     if (SelfKeeperTip.NightCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 1;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                         MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                     }
        ////////                     if (SelfKeeperTip.NeutralCount > 0)
        ////////                     {

        ////////                         int ConductorCode = 2;
        ////////                         #region Swich

        ////////                         switch (i)
        ////////                         {
        ////////                             case 0:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 1:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 2:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 3:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 4:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                             case 5:
        ////////                                 {
        ////////                                     DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                     Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                     break;
        ////////                                 };
        ////////                         }

        ////////                         #endregion
        ////////                         Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                         Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                         MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                     }
        ////////                 }
        ////////             }

        ////////         }
        ////////     }




        ////////     // ed.WriteMessage("&&MaxTension.Absolute= "+MaxTension.Absolute+"\n");
        ////////     // return MaxTension.Absolute;
        ////////     //ed.WriteMessage("**********END DDE\n");
        ////////     return MaxTension;


        //////// }




        //////// public DataTable WindOnPole2()
        //////// {
        ////////     Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////     double sw = 0;
        ////////     double[] temp = new double[6];
        ////////     DataTable dtTable = new DataTable();
        ////////     DataColumn dcPoleGuid = new DataColumn("dcPoleGuid");
        ////////     DataColumn dcPole = new DataColumn("dcPole");
        ////////     DataColumn dcNorm = new DataColumn("dcNorm");
        ////////     DataColumn dcIceHeave = new DataColumn("dcIceHeavy");
        ////////     DataColumn dcWindSpeed = new DataColumn("dcWindSpeed");
        ////////     DataColumn dcMaxTemp = new DataColumn("dcMaxTemp");
        ////////     DataColumn dcMinTemp = new DataColumn("dcMinTemp");
        ////////     DataColumn dcWindIce = new DataColumn("dcWindIce");
        ////////     DataColumn dcAngle = new DataColumn("dcAngle");

        ////////     dtTable.Columns.Add(dcPole);
        ////////     dtTable.Columns.Add(dcIceHeave);
        ////////     dtTable.Columns.Add(dcMaxTemp);
        ////////     dtTable.Columns.Add(dcMinTemp);
        ////////     dtTable.Columns.Add(dcNorm);
        ////////     dtTable.Columns.Add(dcWindIce);
        ////////     dtTable.Columns.Add(dcWindSpeed);
        ////////     dtTable.Columns.Add(dcPoleGuid);
        ////////     dtTable.Columns.Add(dcAngle);

        ////////     DataTable dtPoleConductor = new DataTable();
        ////////     dtPoleConductor.Columns.Add("NodeGuid");
        ////////     dtPoleConductor.Columns.Add("BranchGuid");
        ////////     dtPoleConductor.Columns.Add("Angle");
        ////////     dtPoleConductor.Columns.Add("BranchType");
        ////////     dtPoleConductor.Columns.Add("NodeType");
        ////////     //ed.WriteMessage("$$$$$$$$$$$$$$$$WINDONPOLE\n");
        ////////     //ed.WriteMessage("@@@dtPole.rows.coun={0}\n",DtPoleSection.Rows.Count);
        ////////     foreach (DataRow dr in dtPoleSection.Rows)
        ////////     {
        ////////         sw = 0;


        ////////         Atend.Base.Design.DWeather Weather = new Atend.Base.Design.DWeather();

        ////////         //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(dr["ProductCode"]));
        ////////         //ed.WriteMessage("1={0}\n", DateTime.Now);
        ////////         Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(dr["ProductCode"]));
        ////////         //ed.WriteMessage("2={0}\n", DateTime.Now);

        ////////         Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole(); ;
        ////////         //ed.WriteMessage("MyPackage.ProductType={0},ProductCode={1}\n", myPackage.Type, myPackage.ProductCode);
        ////////         switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
        ////////         {
        ////////             case Atend.Control.Enum.ProductType.Pole:

        ////////                 pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, myPackage.ProductCode);
        ////////                 //ed.WriteMessage("One Switch\n");
        ////////                 break;

        ////////             case Atend.Control.Enum.ProductType.PoleTip:
        ////////                 Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dtPoleTipParm, myPackage.ProductCode);
        ////////                 //ed.WriteMessage("POleTip.Code={0}\n", poleTip.PoleCode);
        ////////                 pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, poleTip.PoleCode);
        ////////                 //ed.WriteMessage("PoleTip.Name={0}\n", poleTip.Name);

        ////////                 break;
        ////////         }
        ////////         //ed.WriteMessage("Pole.Name={0}\n", pole.Name);

        ////////         //double h1 = pole.Height / (pole.Height - (.1 * pole.Height + .6) - 60);

        ////////         DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////         //ed.WriteMessage("3={0}\n", DateTime.Now);

        ////////         int countOfCrossArm = dtPackage.Rows.Count;

        ////////         double WindOnConductor1 = 0;
        ////////         double WindOnPole1 = 0;
        ////////         double WindOnInsulator1 = 0;
        ////////         CVector TensionTangant = new CVector();
        ////////         CVector TensionDDE = new CVector();
        ////////         double[] Angle = new double[6];
        ////////         double Total = 0;
        ////////         dtPoleConductor.Rows.Clear();
        ////////         //ed.WriteMessage("4={0}\n", DateTime.Now);

        ////////         DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
        ////////        // ed.WriteMessage("5={0}\n", DateTime.Now);

        ////////         //ed.WriteMessage("dtConductor={0}\n", dtConductor.Length);
        ////////         foreach (DataRow drConductor in dtConductor)
        ////////         {
        ////////             ed.WriteMessage("6={0}\n", DateTime.Now);

        ////////             DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
        ////////             if (drs1.Length != 0)
        ////////             {
        ////////                 DataRow NewRow = dtPoleConductor.NewRow();
        ////////                 NewRow["NodeGuid"] = drs1[0]["Node1Guid"].ToString();
        ////////                 NewRow["BranchGuid"] = drs1[0]["BranchGuid"].ToString();
        ////////                 NewRow["Angle"] = drs1[0]["Angle1"].ToString();
        ////////                 // NewRow["Angle"] = (Convert.ToDouble(drs1[0]["Angle1"].ToString())*180)/3.14;

        ////////                 NewRow["BranchType"] = drs1[0]["Type"].ToString();
        ////////                 NewRow["NodeType"] = drConductor["NodeType"].ToString();
        ////////                 dtPoleConductor.Rows.Add(NewRow);

        ////////             }
        ////////          //   ed.WriteMessage("7={0}\n", DateTime.Now);

        ////////             DataRow[] drs2 = dtBranchList.Select(string.Format(" Node2Guid='{0}'", drConductor["NodeGuid"].ToString()));

        ////////             if (drs2.Length != 0)
        ////////             {
        ////////                 DataRow NewRow = dtPoleConductor.NewRow();
        ////////                 NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
        ////////                 NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
        ////////                 NewRow["Angle"] = drs2[0]["Angle2"].ToString();
        ////////                 //NewRow["Angle"] = (Convert.ToDouble(drs2[0]["Angle1"].ToString()) * 180) / 3.14;

        ////////                 NewRow["BranchType"] = drs2[0]["Type"].ToString();
        ////////                 NewRow["NodeType"] = drConductor["NodeType"].ToString();

        ////////                 dtPoleConductor.Rows.Add(NewRow);

        ////////             }

        ////////         }
        ////////         //ed.WriteMessage("dtPoleConductor.rows.count={0}\n", dtPoleConductor.Rows.Count);
        ////////         //ed.WriteMessage("PoleGuid={0}\n", dr["productCode"].ToString());
        ////////         //ed.WriteMessage("***************&&&&&&&&&&&&&&&^^^^^^^^^^^^^\n");
        ////////         //foreach (DataRow drf in dtPoleConductor.Rows)
        ////////         //{
        ////////         //    ed.WriteMessage("Angle={0},NodeGuid={1},BranchGuid={2}\n", drf["Angle"].ToString(), drf["NodeGuid"].ToString(), drf["BranchGuid"].ToString());
        ////////         //}
        ////////         //ed.WriteMessage("dtpoleconductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
        ////////         //DataTable dtPoleConductor = Atend.Global.Acad.UAcad.GetPoleConductors(new Guid(dr["ProductCode"].ToString()));
        ////////         double max = 0;
        ////////         int k = 0;
        ////////         for (int i = 0; i < 6; i++)
        ////////         {
        ////////             Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////             //ed.WriteMessage("ProductCode= " + dr["ProductCode"].ToString() + "\n");

        ////////             //ed.WriteMessage("dtPoleConductor.Rows.Count= " + dtPoleConductor.Length.ToString() + "\n");
        ////////             WindOnConductor1 = WindOnConductor2(dtPoleConductor, Weather, pole);
        ////////             if (countOfCrossArm != 0)
        ////////             {
        ////////                 WindOnInsulator1 = WindOnInsulator2(pole, Weather, countOfCrossArm);
        ////////             }
        ////////             //ed.WriteMessage("Insulator= " + WindOnInsulator1 + "\n");
        ////////            // ed.WriteMessage("8={0}\n", DateTime.Now);

        ////////             WindOnPole1 = WindOnPoleF2(pole, Weather, dtPoleConductor);
        ////////             //ed.WriteMessage("9={0}\n", DateTime.Now);

        ////////             TensionTangant = WindOnTangantPole2(Weather, dtPoleConductor, i);
        ////////             //ed.WriteMessage("10={0}\n", DateTime.Now);

        ////////             TensionDDE = WindOnDDEPole2(Weather, dtPoleConductor, i);
        ////////             //ed.WriteMessage("***Cond={0},Ins={1},Pole={2},DDe={3},Tangent={4}\n",WindOnConductor1,WindOnInsulator1,WindOnPole1,TensionDDE.Absolute,TensionTangant.Absolute);
        ////////             Total = WindOnConductor1 + WindOnInsulator1 + WindOnPole1 + TensionDDE.Absolute + TensionTangant.Absolute;
        ////////             temp[i] = Total;
        ////////             Angle[i] = TensionDDE.Add(TensionDDE, TensionTangant).Angle;
        ////////             if (temp[i] > max)
        ////////             {
        ////////                 max = temp[i];
        ////////                 k = i;
        ////////             }
        ////////             //ed.WriteMessage("i= " + i.ToString() + "\n");
        ////////         }


        ////////         DataRow dr1 = dtTable.NewRow();
        ////////         dr1["dcPole"] = dr["PoleNumber"].ToString();
        ////////         dr1["dcNorm"] = Math.Round(Convert.ToDouble(temp[0].ToString()), 2);
        ////////         dr1["dcIceHeavy"] = Math.Round(Convert.ToDouble(temp[1].ToString()), 2);
        ////////         dr1["dcWindSpeed"] = Math.Round(Convert.ToDouble(temp[2].ToString()), 2);
        ////////         dr1["dcMaxTemp"] = Math.Round(Convert.ToDouble(temp[3].ToString()), 2);
        ////////         dr1["dcMinTemp"] = Math.Round(Convert.ToDouble(temp[4].ToString()), 2);
        ////////         dr1["dcWindIce"] = Math.Round(Convert.ToDouble(temp[5].ToString()), 2);
        ////////         dr1["dcPoleGuid"] = dr["ProductCode"].ToString();
        ////////         dr1["dcAngle"] = Math.Round(Convert.ToDouble(Angle[k].ToString()), 2);
        ////////         dtTable.Rows.Add(dr1);
        ////////         //ed.WriteMessage("Add Row\n");

        ////////     }
        ////////     //ed.WriteMessage("Finish Calculating\n");
        ////////     return dtTable;
        //////// }



        public double ComputeHC()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double HC = (VTS / 100) * Atend.Global.Calculation.Mechanical.CCommon.UTS;
            return HC;
        }
        public double ComputeSE02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double s1 = 0;
            double s2 = 0;
            //ed.WriteMessage("I Am In Compute SE\n");
            //ed.WriteMessage("dtConductorSection.Count= "+DtconductorSection.Rows.Count.ToString()+"\n");
            foreach (DataRow dr in dtconductorSection.Rows)
            {

                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode((Guid)(dr["ProductCode"]), aConnection);
                //ed.WriteMessage("MyBranch.Code= "+myBranch.Code.ToString()+"MyBranch.Lenght= "+myBranch.Lenght.ToString()+"\n");
                s1 += Math.Pow(myBranch.Lenght, 3);
                s2 += myBranch.Lenght;
            }
            double se = Math.Sqrt(s1 / s2);
            //ed.WriteMessage("SE= " + se.ToString() + "\n");
            return se;
        }
        public double computesc02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Design.DWeather weatherWind = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3, aConnection);//باد زیاد
            //ed.WriteMessage("WeatherWind.IceDiagonal=" + weatherWind.IceDiagonal.ToString() + "weatherWind.Temp= " + weatherWind.Temp + "\n");
            Atend.Base.Design.DWeather weatherIce = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2, aConnection);//یخ سنگین
            //ed.WriteMessage("WeatherICE.IceDiagonal= " + weatherIce.IceDiagonal.ToString() + "weatherIce.Temp= " + weatherIce.Temp + "\n");


            //ed.WriteMessage("Diagonal={0},WC={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Diagonal, Atend.Global.Calculation.Mechanical.CCommon.WC);
            double iceweight = 913 * Math.PI * weatherIce.IceDiagonal * (weatherIce.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;//wic=913*3.14*i[i+d]*1e-6
            double WwForIce = Math.Pow(weatherIce.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherIce.IceDiagonal) * 1e-3;
            double WTotalForIce = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + iceweight, 2) + Math.Pow(WwForIce, 2));
            //ed.WriteMessage("WTotalForIce={0},iceWeight={1},WwForIce={2},WC={3}\n ", WTotalForIce.ToString(), iceweight, WwForIce, Atend.Global.Calculation.Mechanical.CCommon.WC);

            double windweight = Math.Pow(weatherWind.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherWind.IceDiagonal) * 1e-3;//ww=v^2/16*(d+2i)*10^-3
            double WiForWind = 913 * Math.PI * weatherWind.IceDiagonal * (weatherWind.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;
            double wTotalForWind = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + WiForWind, 2) + Math.Pow(windweight, 2));

            //ed.WriteMessage("wTotalForWind ={0},windWeight={1},WiForWind={2}\n", wTotalForWind.ToString(), windweight, WiForWind);
            double hc = ComputeHC();
            //ed.WriteMessage("HC= " + hc + "\n");
            //ed.WriteMessage("Conductor.Alpha= " + Atend.Global.Calculation.Mechanical.CCommon.Alpha + "\n");
            if (WTotalForIce > wTotalForWind)
            {
                //ed.WriteMessage("WTotalForIce > wTotalForWind\n");
                return -1;
            }
            //else
            //{
            double sc = (24 * Math.Pow(hc, 2) * Atend.Global.Calculation.Mechanical.CCommon.Alpha * (weatherWind.Temp - weatherIce.Temp)) / (Math.Pow(wTotalForWind, 2) - Math.Pow(WTotalForIce, 2));
            //ed.WriteMessage("sc={0}\n", sc.ToString());
            sc = Math.Sqrt(Math.Abs(sc));
            //ed.WriteMessage("SC= " + sc.ToString() + "\n");
            return sc;
            //}
        }
        public void CalcSagTension02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("&&&&Start CalcSagTension\n");
            ////ed.WriteMessage("dtConductorSection.Count= {0}\n",DtconductorSection.Rows.Count);
            //ed.WriteMessage("dtconductorSection.Rows[0][ProductCode]= " + dtconductorSection.Rows[0]["ProductCode"].ToString()+"\n");
            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));

            //ed.WriteMessage("MyBranch.ProductCode1= " + myBranch.ProductCode.ToString() + "\n");
            ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(.ProductCode);
            ////ed.WriteMessage("ConducTor.Code= "+conductor.Code.ToString()+"\n");

            double BaseH = ComputeHC();
            //ed.WriteMessage("BaseH= " + BaseH.ToString() + "\n");
            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            Atend.Base.Design.DWeather WeatherBase;
            Atend.Base.Design.DWeather WeatherSecond;
            double normalH;


            double se = ComputeSE02();
            double sc = computesc02();
            //ed.WriteMessage("SE={0},sc={1}\n", se.ToString(), sc.ToString());
            if (sc == -1)
            {
                //ed.WriteMessage("Sc=-1\n");
                WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
            }
            else
            {
                if (se > sc)
                {
                    //ed.WriteMessage("Se>SC\n");
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3, aConnection);//باد زیاد بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
                else
                {
                    //ed.WriteMessage("Se<=SC\n");
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2, aConnection);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
            }
            WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1, aConnection);//شرط آب و هوایی معمول شرط ثانویه می باشد
            //ed.WriteMessage("weatherBase={0},WeatherSecondCode={1}\n", WeatherBase.Code, WeatherSecond.Code);
            normalH = general.ComputeTension(WeatherBase, WeatherSecond, BaseH, se);
            h[0] = normalH;
            f[0] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[0]);
            //ed.WriteMessage("H[0]= " + h[0].ToString() + "   F[0]= " + f[0].ToString() + "\n");
            WeatherBase = WeatherSecond;


            int i = 1;
            do
            {

                WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                //ed.WriteMessage("*weatherSecond.Code={0},i={1},ConditionCode={2},Temp={3}\n", WeatherSecond.Code, i, WeatherSecond.ConditionCode, WeatherSecond.Temp);

                h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[0], se);
                f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[i]);
                i++;
            } while (i < 6);
            //ed.WriteMessage("FiniSh\n");
            //for (i = 0; i < 6; i++)
            //{
            //    ed.WriteMessage("i = " + i.ToString() + "= " + h[i].ToString() + "\n");
            //}

        }
        public DataTable CalSagTension02(Atend.Base.Design.DBranch MyBranch)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Sag1={0}\n", DateTime.Now);

            double fmax = IsSagOk02();
            //ed.WriteMessage("fmaxxxxxxxx={0}\n",fmax);
            //ed.WriteMessage("Start CalcMyBranch.ProductCode={0}\n", MyBranch.ProductType);
            //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                //ed.WriteMessage("***Conductor\n");
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode, aConnection);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2); ;
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //ed.WriteMessage("%%%%%%%%%fmax={0}\n", fmax);
                    dr["MaxF"] = Math.Round(fmax, 2);
                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                #region SelfKeeper
                //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
                Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
                //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
                if (MySelfKeeperTip.PhaseCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            //ed.WriteMessage("sag2={0}\n", DateTime.Now);
            

            return dtStTable;
        }
        public double IsSagOk02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
            //////Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
            //////switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
            //////{
            //////    case Atend.Control.Enum.ProductType.Pole:
            //////        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode, aConnection);
            //////        break;
            //////    case Atend.Control.Enum.ProductType.PoleTip:
            //////        Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode, aConnection);
            //////        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode, aConnection);
            //////        break;
            //////}

            double PoleHeight = calcAvrageOfPoleHeight();

            double Depth = .1 * PoleHeight + .6;
            double ke;

            double fmax1 = 0;
            DataTable dtPackage = new DataTable();
            dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp), aConnection);
           
            //if (dtPackage.Rows.Count != 0)
            //{
            ////ed.WriteMessage("1,CondCount={0}\n", DtconductorSection.Rows[0]["ProductType"].ToString());
            ////if (Convert.ToInt32(DtconductorSection.Rows[0]["ProductType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
            ////{
            if (!HasConsol)
            {
              
                //ed.WriteMessage("DistanceSupport={0},Clearance={1}\n", calamp.DistanceSupport, Clearance);
                fmax1 = PoleHeight - (Depth + Calamp.DistanceSupport + clearance /*+ZaribEtminan*/);
            }
                //ed.WriteMessage("FMAX!={0}\n",fmax1);
            //////}
            //}

            //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
            double fmax2 = 0;
            double FMAX = fmax1;
            dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), aConnection);

            ////if (dtPackage.Rows.Count != 0)
            ////{
           

            ////if (Convert.ToInt32(dtconductorSection.Rows[0]["productType"].ToString())==Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            ////{
            if (HasConsol)
            {
                
                //Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));
                ke = ComputeKe02();
                //ed.WriteMessage("KE={0}\n", ke);
                double Vn = Volt / 1000;

                fmax1 = PoleHeight - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance /*+ ZaribEtminan*/);
                ed.WriteMessage("fMAx1={0}\n",fmax1);
                //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
                //ed.WriteMessage("DistancePhase={0},Volt={1}\n",Consol.DistancePhase,Consol.VoltageLevel);
                fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
                //ed.WriteMessage("Fmax2={0}\n",fmax2);
                if (fmax1 < fmax2)
                    FMAX = fmax1;
                else
                    FMAX = fmax2;
            }
            ////}
            ////}

            //ed.WriteMessage("**********FMAX={0}\n", FMAX);
            

            return FMAX;


        }
        private double calcAvrageOfPoleHeight()
        {
            double PoleHeight = 0;
            foreach (DataRow dr in DtPoleSection.Rows)
            {
                Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, new Guid(dr["ProductCode"].ToString()));
                Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
                switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
                {
                    case Atend.Control.Enum.ProductType.Pole:
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode, aConnection);
                        break;
                    case Atend.Control.Enum.ProductType.PoleTip:
                        Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode, aConnection);
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode, aConnection);
                        break;
                }
                PoleHeight += pole.Height;
            }
            PoleHeight = PoleHeight / DtPoleSection.Rows.Count;
            //ed.WriteMessage("PoleHeiht={0},dtPoleSection={1}\n", PoleHeight, DtPoleSection.Rows.Count);
            return PoleHeight;
        }

        private double ComputeKe02()
        {
            //ed.WriteMessage("Tyep={0},CrossSection={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Type, Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
            double[,] Ke = new double[,] {{0.85, 0.65, 0.7},{0.85, 0.65, 0.7},{0.75, 0.62, 0.65},{0.75, 0.62, 0.65},
                {0.75, 0.62, 0.65},
                {0.85, 0.65, 0.7},
                {0.85, 0.65, 0.7},
                {0.75, 0.62, 0.65},
                {0.75, 0.62, 0.65}};

            int _2ndIndex;
            switch (Consol.Type)
            {
                case 0://Vertical
                    _2ndIndex = 0;
                    break;

                case 2://Horizental
                    _2ndIndex = 1;
                    break;

                case 1://Triangle
                    _2ndIndex = 2;
                    break;

                default:
                    _2ndIndex = 0;
                    break;
            }

            int _1stIndex;
            if (Atend.Global.Calculation.Mechanical.CCommon.Type == 0)//Messi
            {
                if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 16)
                    _1stIndex = 0;
                else if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 25)
                    _1stIndex = 1;
                else if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 35)
                    _1stIndex = 2;
                else if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 50)
                    _1stIndex = 3;
                else
                    _1stIndex = 4;
            }
            else // ACSR
            {
                if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 43)
                    _1stIndex = 5;
                else if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 74)
                    _1stIndex = 6;
                else if (Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea <= 127)
                    _1stIndex = 7;
                else
                    _1stIndex = 8;
            }

            return Ke[_1stIndex, _2ndIndex];
        }
        public DataTable FindHMax02()
        {
            double[] hMax = new double[6];
            DataTable dtHMax = new DataTable();
            DataColumn dcHNorm = new DataColumn("NormH");
            DataColumn dcHIce = new DataColumn("IceH");
            DataColumn dcHWind = new DataColumn("WindH");
            DataColumn dcHMaxTemp = new DataColumn("MaxTempH");
            DataColumn dcHMinTemp = new DataColumn("MinTempH");
            DataColumn dchWindIce = new DataColumn("WindIceH");
            dtHMax.Columns.Add(dcHNorm);
            dtHMax.Columns.Add(dcHIce);
            dtHMax.Columns.Add(dcHWind);
            dtHMax.Columns.Add(dcHMaxTemp);
            dtHMax.Columns.Add(dcHMinTemp);
            dtHMax.Columns.Add(dchWindIce);
            double UTS;
            Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(DtconductorSection.Rows[0]["ProductCode"].ToString()), aConnection);
            if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode, aConnection);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);

                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                        //ed.WriteMessage("HMAX={0},i={1}\n",hMax[i].ToString(),i.ToString());
                    }
                    DataRow dr = dtHMax.NewRow();
                    //dr["ConductorCode"] = "0";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);

                    //dr["SectionCode"] = sectionCode;

                    dtHMax.Rows.Add(dr);
                }
                if (MyConducyorTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                    }

                    DataRow dr = dtHMax.NewRow();
                    // dr["ConductorCode"] = "1";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);

                    //dr["SectionCode"] = sectionCode;


                    dtHMax.Rows.Add(dr);
                }
                if (MyConducyorTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);

                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                    }
                    DataRow dr = dtHMax.NewRow();
                    //dr["conductorCode"] = "2";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);

                    //dr["SectionCode"] = sectionCode;


                    dtHMax.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                //Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
                //Atend.Base.Equipment.ESelfKeeper Self = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfTip.PhaseProductCode);
                //UTS = Self.UTS;
                #region SelfKeeper
                //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
                Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
                //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
                if (MySelfKeeperTip.PhaseCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                    }
                    DataRow dr = dtHMax.NewRow();
                    //dr["ConductorCode"] = "0";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);



                    dtHMax.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                    }
                    DataRow dr = dtHMax.NewRow();
                    //dr["ConductorCode"] = "1";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);

                    //dr["SectionCode"] = sectionCode;


                    dtHMax.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
                        hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
                    }
                    DataRow dr = dtHMax.NewRow();
                    //dr["conductorCode"] = "2";
                    //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(hMax[0], 2);

                    dr["IceH"] = Math.Round(hMax[1], 2);

                    dr["WindH"] = Math.Round(hMax[2], 2);

                    dr["MaxTempH"] = Math.Round(hMax[3], 2);

                    dr["MinTempH"] = Math.Round(hMax[4], 2);

                    dr["WindIceH"] = Math.Round(hMax[5], 2);

                    //dr["SectionCode"] = sectionCode;


                    dtHMax.Rows.Add(dr);
                }
                #endregion
            }


            return dtHMax;

        }
        public DataTable CalcTempTable02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Temp1={0}\n", DateTime.Now);

            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            //ed.WriteMessage("CALC TEMP Table\n");
            if (End < Start)
            {
                //ed.WriteMessage("ExchangeValue\n");
                int t = End;
                End = Start;
                Start = t;
            }
            Atend.Base.Design.DWeather Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1, aConnection);//شرط آب و هوایی نرمال 
            double hTempTable, fTempTable;
            DataTable dtTempTable = new DataTable();

            DataColumn dcColumn2 = new DataColumn("From");
            DataColumn dcColumn3 = new DataColumn("To");
            DataColumn dcColumn1 = new DataColumn("SpanLenght");
            DataColumn dcColumn4 = new DataColumn("ConductorName");

            dtTempTable.Columns.Add(dcColumn2);
            dtTempTable.Columns.Add(dcColumn3);
            dtTempTable.Columns.Add(dcColumn1);
            dtTempTable.Columns.Add(dcColumn4);
            //ed.WriteMessage("End= " + End.ToString() + "Start= " + Start.ToString() + "Distance =" + Distance.ToString() + "\n");
            for (int i = end; i > start; i = i - distance)
            {
                //ed.WriteMessage("I Am In The For to BuildColumn\n");
                DataColumn dtColumn = new DataColumn(i.ToString());
                DataColumn dtColumnF = new DataColumn(i.ToString() + "F");
                dtTempTable.Columns.Add(dtColumn);
                dtTempTable.Columns.Add(dtColumnF);
            }

            foreach (DataRow dr in dtconductorSection.Rows)
            {
                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["ProductCode"].ToString()), aConnection);
                //ed.WriteMessage("MyBranch.ProductCode= " + myBranch.ProductCode.ToString() + "\n");
                if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {
                    Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode, aConnection);
                    //ed.WriteMessage("**\n");
                    DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));

                    Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node1Guid"].ToString()), aConnection);
                    Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node2Guid"].ToString()), aConnection);
                    if (ConductorTip.PhaseCount > 0)
                    {
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                        DataRow NewRow = dtTempTable.NewRow();



                        NewRow["From"] = dNumLeft.Number;
                        NewRow["To"] = dNumRight.Number;
                        NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                        for (int i = end; i > start; i = i - distance)
                        {
                            DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));

                            //ed.WriteMessage("NormH={0}\n",drCond[0]["NormH"].ToString());
                            hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                            fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                            NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                            NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                            NewRow["SpanLenght"] = myBranch.Lenght;
                        }
                        //ed.WriteMessage("**********************Add\n");
                        dtTempTable.Rows.Add(NewRow);
                    }


                    if (ConductorTip.NightCount > 0)
                    {
                        //ed.WriteMessage("NIGHT\n");
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);

                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));
                        if (drCond.Length > 0)
                        {
                            DataRow NewRow = dtTempTable.NewRow();


                            NewRow["From"] = dNumLeft.Number;
                            NewRow["To"] = dNumRight.Number;
                            //CalcSagTension();
                            NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                            for (int i = end; i > start; i = i - distance)
                            {
                                //ed.WriteMessage("I am In CalcTemp\n");
                                hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                                fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                                NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                                NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                                NewRow["SpanLenght"] = myBranch.Lenght;
                            }
                            dtTempTable.Rows.Add(NewRow);
                        }
                    }
                    if (ConductorTip.NeutralCount > 0)
                    {
                        //ed.WriteMessage("Netural\n");
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);

                        //CalcSagTension();
                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));

                        //ed.WriteMessage("BEFOR FOR\n");
                        if (drCond.Length > 0)
                        {
                            DataRow NewRow = dtTempTable.NewRow();


                            NewRow["From"] = dNumLeft.Number;
                            NewRow["To"] = dNumRight.Number;
                            NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                            for (int i = end; i > start; i = i - distance)
                            {
                                //ed.WriteMessage("I am In CalcTemp {0}\n", drCond[0]["NormH"].ToString());
                                hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                                fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                                NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                                NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                                NewRow["SpanLenght"] = myBranch.Lenght;

                            }
                            dtTempTable.Rows.Add(NewRow);
                        }
                    }
                    //ed.WriteMessage("FINISH CONDUCTOR\n");
                }
                else
                {
                    //ed.WriteMessage("SELFKEEPER\n");
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(myBranch.ProductCode);
                    DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));

                    Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node1Guid"].ToString()));
                    Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node2Guid"].ToString()));



                    if (SelfKeeperTip.PhaseCount > 0)
                    {
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
                        DataRow NewRow = dtTempTable.NewRow();
                        //foreach (DataRow dr1 in DtPoleSection.Rows)
                        //{
                        //    if (MyNode.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is LeftNode\n");
                        //        NewRow["From"] = dr1["PoleNumber"].ToString();

                        //    }
                        //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is RightNode\n");
                        //        NewRow["To"] = dr1["PoleNumber"].ToString();
                        //    }
                        //}
                        NewRow["From"] = dNumLeft.Number;
                        NewRow["To"] = dNumRight.Number;
                        //CalcSagTension();
                        NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));
                        for (int i = end; i > start; i = i - distance)
                        {
                            //ed.WriteMessage("I am In CalcTemp\n");
                            hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                            fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                            NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                            NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                            NewRow["SpanLenght"] = myBranch.Lenght;
                        }
                        dtTempTable.Rows.Add(NewRow);
                    }
                    if (SelfKeeperTip.NightCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                        DataRow NewRow = dtTempTable.NewRow();
                        //foreach (DataRow dr1 in DtPoleSection.Rows)
                        //{
                        //    if (MyNode.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is LeftNode\n");
                        //        NewRow["From"] = dr1["PoleNumber"].ToString();

                        //    }
                        //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is RightNode\n");
                        //        NewRow["To"] = dr1["PoleNumber"].ToString();
                        //    }
                        //}
                        NewRow["From"] = dNumLeft.Number;
                        NewRow["To"] = dNumRight.Number;
                        //CalcSagTension();
                        NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));
                        for (int i = end; i > start; i = i - distance)
                        {
                            //ed.WriteMessage("I am In CalcTemp\n");
                            hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                            fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                            NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                            NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                            NewRow["SpanLenght"] = myBranch.Lenght;
                        }
                        dtTempTable.Rows.Add(NewRow);
                    }
                    if (SelfKeeperTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                        DataRow NewRow = dtTempTable.NewRow();
                        //foreach (DataRow dr1 in DtPoleSection.Rows)
                        //{
                        //    if (MyNode.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is LeftNode\n");
                        //        NewRow["From"] = dr1["PoleNumber"].ToString();

                        //    }
                        //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
                        //    {
                        //        //ed.WriteMessage("Current Is RightNode\n");
                        //        NewRow["To"] = dr1["PoleNumber"].ToString();
                        //    }
                        //}
                        NewRow["From"] = dNumLeft.Number;
                        NewRow["To"] = dNumRight.Number;
                        //CalcSagTension();
                        NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));
                        for (int i = end; i > start; i = i - distance)
                        {
                            //ed.WriteMessage("I am In CalcTemp\n");
                            hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
                            fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
                            NewRow[i.ToString()] = Math.Round(hTempTable, 2);
                            NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
                            NewRow["SpanLenght"] = myBranch.Lenght;
                        }
                        dtTempTable.Rows.Add(NewRow);
                    }




                }

                //}
                //ed.WriteMessage("Add Row To DtTempTAble\n");

                //ed.WriteMessage("dtTempTable.Rows.Count= " + dtTempTable.Rows.Count.ToString() + "\n");
            }
            //ed.WriteMessage("&&&&FINISH****\n");
            //ed.WriteMessage("TEMP2={0}\n", DateTime.Now);

            return dtTempTable;
        }
        private double WindOnPoleF02(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather weather, DataTable dtConductor)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataRow[] dr = dtConductor.Select(string.Format(" NodeType='{0}'", Atend.Control.Enum.ProductType.Consol));

            //ed.WriteMessage("Start WindOnPoleF\n");
            ed.WriteMessage("Pole.Height={0},Pole.buttonCrosssectionArea={1},pole.topCrossectionArea={2}\n",pole.Height,pole.ButtomCrossSectionArea,pole.TopCrossSectionArea);
            double L = pole.Height - (0.1 * pole.Height + .6);
            double AW = (L * (pole.ButtomCrossSectionArea + pole.TopCrossSectionArea)) / 2;
            double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
            double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;

            //if (dr.Length != 0)
            //{
            //    //ed.WriteMessage("It Is cOnsol\n");
            //    //////Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
            //    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam,new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
            //    //////Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dPack.ProductCode);
            //    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode (dtConsolParam,dPack.ProductCode);
            //    h1 = L - consol.DistanceCrossArm;

            //}
            //else
            //{
            //    Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam,new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
            //    //////Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
            //    Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam,dPackage.ProductCode);
            //    h1 = L - Clamp.DistanceSupport;
            //}



            double KFactor = .0812;
            if (pole.Shape == 0)
            {
                KFactor = .05;
            }
            //ed.WriteMessage("BCrossSection= " + pole.ButtomCrossSectionArea + "TCrossSectionArea= " + pole.TopCrossSectionArea + "K=" + KFactor + "\n");
            ed.WriteMessage("L=" + L + " Aw" + AW + " h=" + h + "H1=" + h1 + " weather=" + weather.Code + "WindSpeed="+weather.WindSpeed+ "Kfactor="+KFactor+ "\n");
            double Result = (h / h1) * KFactor * AW * Math.Pow(weather.WindSpeed, 2);
            ed.WriteMessage("PoleF= " + Result + "\n");
            return Result;
        }
        private double WindOnInsulator02(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather Weather, int CounOfCrossArm)
        {
            //ed.WriteMessage("CountOfCross= "+CounOfCrossArm+" InsulatorDiameter= "+InsulatorDiamiter+" Shape= "+InsulatorShapeFactor+" LenghtChain= "+LenghtChain+"\n");
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnInsulator\n");
            return 3 * CounOfCrossArm * LenghtChain * InsulatorDiamiter * InsulatorShapeFactor * Math.Pow(Weather.WindSpeed, 2) / 16 * 1e-3;
        }
        private double WindOnConductor02(DataTable dtConductor, Atend.Base.Design.DWeather weather, Atend.Base.Equipment.EPole pole)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnConductor\n");
            double sum = 0;
            //ed.WriteMessage("I Am In WindOnConductor\n");
            foreach (DataRow dr in dtConductor.Rows)
            {
                //////Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["BranchGuid"].ToString()));
                Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {
                    //////Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
                    Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, Branch.ProductCode);
                    if (conductorTip.PhaseCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                        //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
                        sum += conductorTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (conductorTip.NightCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                        sum += conductorTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (conductorTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                        sum += conductorTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                }
                else
                {
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, Branch.ProductCode);
                    if (SelfKeeperTip.PhaseCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                        //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
                        sum += SelfKeeperTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (SelfKeeperTip.NightCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                        sum += SelfKeeperTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (SelfKeeperTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                        sum += SelfKeeperTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                }
            }
            ed.WriteMessage("******Sum={0}\n",sum);
            double L = pole.Height - (0.1 * pole.Height + .6);
            double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
            double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;
            ed.WriteMessage("h={0},h1={1}\n",h,h1);
            sum = sum * (h / h1);
            ed.WriteMessage("Sum =" + sum.ToString() + "\n");
            return sum;
        }
        private CVector WindOnTangantPole02(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("WindOnTangantPole\n");
            Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
            CVector SumTension = new CVector();


            SumTension.Absolute = 0;
            SumTension.Angle = 0;
            //ed.WriteMessage("*************Tangant\n");
            foreach (DataRow dr in dtConductor.Rows)
            {
                //ed.WriteMessage("%%%\n");
                if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(dr["NodeGuid"].ToString()));
                    //ed.WriteMessage("*\n");
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
                    if (Econsol.ConsolType == 2 || Econsol.ConsolType == 3)
                    {
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        //ed.WriteMessage("It Is Tangant Consol\n");
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
                        if (drSec.Length == 0)
                        {

                            //Must Be Change
                            DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor), aConnection);
                            sectionCode = Section.SectionCode;
                            CalSagTension02(myBranch);
                        }
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        //ed.WriteMessage("SwichTAngant\n");
                        if (conductorTip.PhaseCount > 0)
                        {
                            int ConductorCode = 0;
                            //ed.WriteMessage("9-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);
                            #region IF
                            //ed.WriteMessage("****ConductorPhase=\n");

                            if (i == 0)
                            {
                                //ed.WriteMessage("$$$pp");
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");


                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }


                            #endregion
                            //////#region Swich
                            ////////ed.WriteMessage("****ConductorPhase=\n");
                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            //ed.WriteMessage("$$$pp");
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("**9-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);
                            Tension.Absolute *= conductorTip.PhaseCount;

                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            //ed.WriteMessage("SumTension.absu={0},sum.angle={1},ten.abs={2},ten.ang={3}\n",SumTension.Absolute,SumTension.Angle,Tension.Absolute,Tension.Angle);
                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
                        {
                            //ed.WriteMessage("*******Night\n");
                            int ConductorCode = 1;
                            //ed.WriteMessage("9-1={0}Ni:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }


                            #endregion


                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            // ed.WriteMessage("**9-1={0}Ni:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            //ed.WriteMessage("******NrtralCount\n");
                            int ConductorCode = 2;
                            //ed.WriteMessage("9-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                            }


                            #endregion

                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("**9-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                    }
                }
                else
                {
                    //ed.WriteMessage("KAlamp\n");
                    if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                    {
                        Atend.Base.Design.DPackage Dpack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dr["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, Dpack.ProductCode);
                        //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                        if (ECalamp.Type == 5)
                        {
                            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                           ed.WriteMessage("*******************It Is Tangant Kalamp\n");
                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                                sectionCode = Section.SectionCode;
                                CalSagTension02(myBranch);
                            }

                            //CalSagTension(myBranch);
                            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
                            ed.WriteMessage("Self.PhaseCount={0}\n",SelfKeeperTip.PhaseCount);
                            if (SelfKeeperTip.PhaseCount > 0)
                            {
                                int ConductorCode = 0;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            //ed.WriteMessage("NormH= "+dr1[0]["NormH"].ToString()+"Angle= "+dr["Angle"].ToString()+"\n");
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;

                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }


                            if (SelfKeeperTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                            if (SelfKeeperTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                        }
                    }
                }
            }

            //ed.WriteMessage("Tension.Absolute= "+SumTension.Absolute+"\n");
            //return SumTension.Absolute;

            //ed.WriteMessage("*********END Tangant\n");
            ed.WriteMessage("sumTension={0}\n",SumTension.Absolute);
            return SumTension;

        }
        private CVector WindOnDDEPole02(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("I AM In WindOnDDEPole\n");
            
            //ed.WriteMessage("Start WindOnDDEPole\n");
            Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
            CVector SumTension = new CVector();
            CVector MaxTension = new CVector();
            MaxTension.Absolute = 0;
            MaxTension.Angle = 0;
            SumTension.Absolute = 0;
            SumTension.Angle = 0;
            CVector max = new CVector();
            max.Absolute = 0;
            max.Angle = 0;
            //ed.WriteMessage("*********DDEPole\n");
            //****************X1=a+b+c
            //ed.WriteMessage("X1=a+b+c\n");
           // ed.WriteMessage("******dtConductor.Count={0}\n",dtConductor.Rows.Count);
            foreach (DataRow drConductor in dtConductor.Rows)
            {
                if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    //ed.WriteMessage("NODEGUID={0}\n", drConductor["NodeGuid"].ToString());
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
                    //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                    if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
                    {
                        //ed.WriteMessage("**Entehaiiiiiiiiiiiiii={0},Name={1}\n",Econsol.ConsolType,Econsol.Name);
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                        if (drSec.Length == 0)
                        {
                            DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor), aConnection);
                            sectionCode = Section.SectionCode;
                            CalSagTension02(myBranch);
                        }
                        //CalSagTension(myBranch);
                        //ed.WriteMessage("Angle={0}\n",drConductor["Angle"].ToString());
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
                        {

                            int ConductorCode = 0;
                            //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


                            #region IF


                            if (i == 0)
                            {

                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }


                            #endregion


                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {

                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("**10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
                        {

                            int ConductorCode = 1;
                            //ed.WriteMessage("10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            #region IF



                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

                            }


                            #endregion


                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion

                            //ed.WriteMessage("**10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            int ConductorCode = 2;
                            //ed.WriteMessage("10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                break;
                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

                            }


                            #endregion



                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("**10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                    {
                        Atend.Base.Design.DPackage DPack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(drConductor["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(DPack.ProductCode);
                        //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                        if (ECalamp.Type == 1 || ECalamp.Type == 2 || ECalamp.Type == 3 || ECalamp.Type == 4 || ECalamp.Type == 6)
                        {
                            ed.WriteMessage(" کلمپ انتهایی\n");
                            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                                sectionCode = Section.SectionCode;
                                CalSagTension02(myBranch);
                            }
                            //CalSagTension(myBranch);
                            Atend.Base.Equipment.ESelfKeeperTip SelfKeepeTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
                            if (SelfKeepeTip.PhaseCount > 0)
                            {

                                int ConductorCode = 0;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {

                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

                                SumTension = SumTension.Add(SumTension, Tension);
                            }


                            if (SelfKeepeTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                            if (SelfKeepeTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                        }
                    }
                }

            }
            ed.WriteMessage("sumTension="+SumTension.Absolute+"\n");
            //****************************
            //ed.WriteMessage("Second\n");
            foreach (DataRow drConductor in dtConductor.Rows)
            {
                if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
                    if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
                    {
                        //ed.WriteMessage("EEEEEEEEEntehai\n");
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
                        {

                            int ConductorCode = 0;
                            //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");

                            }


                            #endregion


                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("10-1P={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            max = Max(MaxTension, max);
                        }


                        if (conductorTip.NightCount > 0)
                        {

                            int ConductorCode = 1;
                            //ed.WriteMessage("10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

                            }


                            #endregion


                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion

                            //ed.WriteMessage("**10-1NI={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            max = Max(MaxTension, max);
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            int ConductorCode = 2;
                            //ed.WriteMessage("10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);


                            #region IF


                            if (i == 0)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

                            }
                            else if (i == 1)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());

                            }
                            else if (i == 2)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());

                            }
                            else if (i == 3)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());

                            }
                            else if (i == 4)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());

                            }
                            else if (i == 5)
                            {
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());

                            }


                            #endregion


                            //////#region Swich

                            //////switch (i)
                            //////{
                            //////    case 0:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 1:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 2:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 3:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 4:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                            //////            break;
                            //////        };
                            //////    case 5:
                            //////        {
                            //////            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                            //////            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                            //////            break;
                            //////        };
                            //////}

                            //////#endregion
                            //ed.WriteMessage("**10-1NE={0}:{1}\n", DateTime.Now.Second, DateTime.Now.Millisecond);

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            max = Max(MaxTension, max);
                        }
                    }

                }
                else
                {
                    if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                    {
                        Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(drConductor["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp eCalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, dPackage.ProductCode);
                        if (eCalamp.Type == 1 || eCalamp.Type == 2 || eCalamp.Type == 3 || eCalamp.Type == 4 || eCalamp.Type == 6)//(eCalamp.Type == 0)
                        {
                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

                            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
                            if (SelfKeeperTip.PhaseCount > 0)
                            {

                                int ConductorCode = 0;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                                max = Max(MaxTension, max);
                            }


                            if (SelfKeeperTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                                max = Max(MaxTension, max);
                            }
                            if (SelfKeeperTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                #region Swich

                                switch (i)
                                {
                                    case 0:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                            break;
                                        };
                                    case 1:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                            break;
                                        };
                                    case 2:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                            break;
                                        };
                                    case 3:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                            break;
                                        };
                                    case 4:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                            break;
                                        };
                                    case 5:
                                        {
                                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                            Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                            break;
                                        };
                                }

                                #endregion
                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                                max = Max(MaxTension, max);
                            }
                        }
                    }

                }
            }




            // ed.WriteMessage("&&MaxTension.Absolute= "+MaxTension.Absolute+"\n");
            // return MaxTension.Absolute;
            //ed.WriteMessage("**********END DDE\n");
            //return MaxTension;
            return max;


        }
        public DataTable WindOnPole02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double sw = 0;
            double[] temp = new double[6];
            DataTable dtTable = new DataTable();
            DataColumn dcPoleGuid = new DataColumn("dcPoleGuid");
            DataColumn dcPole = new DataColumn("dcPole");
            DataColumn dcNorm = new DataColumn("dcNorm");
            DataColumn dcIceHeave = new DataColumn("dcIceHeavy");
            DataColumn dcWindSpeed = new DataColumn("dcWindSpeed");
            DataColumn dcMaxTemp = new DataColumn("dcMaxTemp");
            DataColumn dcMinTemp = new DataColumn("dcMinTemp");
            DataColumn dcWindIce = new DataColumn("dcWindIce");
            DataColumn dcAngle = new DataColumn("dcAngle");

            dtTable.Columns.Add(dcPole);
            dtTable.Columns.Add(dcIceHeave);
            dtTable.Columns.Add(dcMaxTemp);
            dtTable.Columns.Add(dcMinTemp);
            dtTable.Columns.Add(dcNorm);
            dtTable.Columns.Add(dcWindIce);
            dtTable.Columns.Add(dcWindSpeed);
            dtTable.Columns.Add(dcPoleGuid);
            dtTable.Columns.Add(dcAngle);

            DataTable dtPoleConductor = new DataTable();
            dtPoleConductor.Columns.Add("NodeGuid");
            dtPoleConductor.Columns.Add("BranchGuid");
            dtPoleConductor.Columns.Add("Angle");
            dtPoleConductor.Columns.Add("BranchType");
            dtPoleConductor.Columns.Add("NodeType");
            //ed.WriteMessage("$$$$$$$$$$$$$$$$WINDONPOLE\n");
            //ed.WriteMessage("@@@dtPole.rows.coun={0}\n",DtPoleSection.Rows.Count);
            foreach (DataRow dr in dtPoleSection.Rows)
            {
                sw = 0;


                Atend.Base.Design.DWeather Weather = new Atend.Base.Design.DWeather();

                //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(dr["ProductCode"]));
                //ed.WriteMessage("1={0}\n", DateTime.Now);
                Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(dr["ProductCode"]));
                //ed.WriteMessage("2={0}\n", DateTime.Now);

                Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole(); ;
                //ed.WriteMessage("MyPackage.ProductType={0},ProductCode={1}\n", myPackage.Type, myPackage.ProductCode);
                switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
                {
                    case Atend.Control.Enum.ProductType.Pole:

                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, myPackage.ProductCode);
                        //ed.WriteMessage("One Switch\n");
                        break;

                    case Atend.Control.Enum.ProductType.PoleTip:
                        Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dtPoleTipParm, myPackage.ProductCode);
                        //ed.WriteMessage("POleTip.Code={0}\n", poleTip.PoleCode);
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, poleTip.PoleCode);
                        //ed.WriteMessage("PoleTip.Name={0}\n", poleTip.Name);

                        break;
                }
                //ed.WriteMessage("Pole.Name={0}\n", pole.Name);

                //double h1 = pole.Height / (pole.Height - (.1 * pole.Height + .6) - 60);

                DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), aConnection);
                //ed.WriteMessage("3={0}\n", DateTime.Now);

                int countOfCrossArm = dtPackage.Rows.Count;

                double WindOnConductor1 = 0;
                double WindOnPole1 = 0;
                double WindOnInsulator1 = 0;
                CVector TensionTangant = new CVector();
                CVector TensionDDE = new CVector();
                double[] Angle = new double[6];
                double Total = 0;
                dtPoleConductor.Rows.Clear();
                //ed.WriteMessage("4={0}\n", DateTime.Now);

                DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
                // ed.WriteMessage("5={0}\n", DateTime.Now);

                //ed.WriteMessage("dtConductor={0}\n", dtConductor.Length);
                foreach (DataRow drConductor in dtConductor)
                {
                    //ed.WriteMessage("6={0}\n", DateTime.Now);

                    DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
                    if (drs1.Length != 0)
                    {
                        DataRow NewRow = dtPoleConductor.NewRow();
                        NewRow["NodeGuid"] = drs1[0]["Node1Guid"].ToString();
                        NewRow["BranchGuid"] = drs1[0]["BranchGuid"].ToString();
                        NewRow["Angle"] = drs1[0]["Angle1"].ToString();
                        // NewRow["Angle"] = (Convert.ToDouble(drs1[0]["Angle1"].ToString())*180)/3.14;

                        NewRow["BranchType"] = drs1[0]["Type"].ToString();
                        NewRow["NodeType"] = drConductor["NodeType"].ToString();
                        dtPoleConductor.Rows.Add(NewRow);

                    }
                    //   ed.WriteMessage("7={0}\n", DateTime.Now);

                    DataRow[] drs2 = dtBranchList.Select(string.Format(" Node2Guid='{0}'", drConductor["NodeGuid"].ToString()));

                    if (drs2.Length != 0)
                    {
                        DataRow NewRow = dtPoleConductor.NewRow();
                        NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
                        NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
                        NewRow["Angle"] = drs2[0]["Angle2"].ToString();
                        //NewRow["Angle"] = (Convert.ToDouble(drs2[0]["Angle1"].ToString()) * 180) / 3.14;

                        NewRow["BranchType"] = drs2[0]["Type"].ToString();
                        NewRow["NodeType"] = drConductor["NodeType"].ToString();

                        dtPoleConductor.Rows.Add(NewRow);

                    }

                }
                //ed.WriteMessage("dtPoleConductor.rows.count={0}\n", dtPoleConductor.Rows.Count);
                //ed.WriteMessage("PoleGuid={0}\n", dr["productCode"].ToString());
                //ed.WriteMessage("***************&&&&&&&&&&&&&&&^^^^^^^^^^^^^\n");
                //foreach (DataRow drf in dtPoleConductor.Rows)
                //{
                //    ed.WriteMessage("Angle={0},NodeGuid={1},BranchGuid={2}\n", drf["Angle"].ToString(), drf["NodeGuid"].ToString(), drf["BranchGuid"].ToString());
                //}
                //ed.WriteMessage("dtpoleconductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
                //DataTable dtPoleConductor = Atend.Global.Acad.UAcad.GetPoleConductors(new Guid(dr["ProductCode"].ToString()));
                double max = 0;
                int k = 0;
                for (int i = 0; i < 6; i++)
                {
                    Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                    //ed.WriteMessage("ProductCode= " + dr["ProductCode"].ToString() + "\n");

                    //ed.WriteMessage("dtPoleConductor.Rows.Count= " + dtPoleConductor.Length.ToString() + "\n");
                    WindOnConductor1 = WindOnConductor02(dtPoleConductor, Weather, pole);
                    if (countOfCrossArm != 0)
                    {
                        WindOnInsulator1 = WindOnInsulator02(pole, Weather, countOfCrossArm);
                    }
                    ed.WriteMessage("Insulator= " + WindOnInsulator1 + "\n");
                    // ed.WriteMessage("8={0}\n", DateTime.Now);

                    WindOnPole1 = WindOnPoleF02(pole, Weather, dtPoleConductor);
                    //ed.WriteMessage("9={0}\n", DateTime.Now);

                    TensionTangant = WindOnTangantPole02(Weather, dtPoleConductor, i);
                    //ed.WriteMessage("10={0}\n", DateTime.Now);

                    TensionDDE = WindOnDDEPole02(Weather, dtPoleConductor, i);
                    ed.WriteMessage("***Cond={0},Ins={1},Pole={2},DDe={3},Tangent={4}\n",WindOnConductor1,WindOnInsulator1,WindOnPole1,TensionDDE.Absolute,TensionTangant.Absolute);
                    Total = WindOnConductor1 + WindOnInsulator1 + WindOnPole1 + TensionDDE.Absolute + TensionTangant.Absolute;
                    temp[i] = Total;
                    Angle[i] = TensionDDE.Add(TensionDDE, TensionTangant).Angle;
                    if (temp[i] > max)
                    {
                        max = temp[i];
                        k = i;
                    }
                    //ed.WriteMessage("i= " + i.ToString() + "\n");
                    if (i==2)
                    {
                        ed.WriteMessage("######tangant={0}\n",TensionTangant.Absolute);
                    }
                }


                DataRow dr1 = dtTable.NewRow();
                dr1["dcPole"] = dr["PoleNumber"].ToString();
                dr1["dcNorm"] = Math.Round(Convert.ToDouble(temp[0].ToString()), 2);
                dr1["dcIceHeavy"] = Math.Round(Convert.ToDouble(temp[1].ToString()), 2);
                dr1["dcWindSpeed"] = Math.Round(Convert.ToDouble(temp[2].ToString()), 2);
                dr1["dcMaxTemp"] = Math.Round(Convert.ToDouble(temp[3].ToString()), 2);
                dr1["dcMinTemp"] = Math.Round(Convert.ToDouble(temp[4].ToString()), 2);
                dr1["dcWindIce"] = Math.Round(Convert.ToDouble(temp[5].ToString()), 2);
                dr1["dcPoleGuid"] = dr["ProductCode"].ToString();
                dr1["dcAngle"] = Math.Round(Convert.ToDouble(Angle[k].ToString()), 2);
                dtTable.Rows.Add(dr1);
                //ed.WriteMessage("Add Row\n");

            }
            aConnection.Close();
            //ed.WriteMessage("Finish Calculating\n");
            return dtTable;
        }
        public CVector Max(CVector a, CVector b)
        {
            if (a.Absolute > b.Absolute)
                return a;
            return b;
        }



        ////////public DataTable CalSagTensionForRudSurface(Atend.Base.Design.DBranch MyBranch)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


        ////////    //ed.WriteMessage("fmaxxxxxxxx={0}\n",fmax);
        ////////    //ed.WriteMessage("Start CalcMyBranch.ProductCode={0}\n", MyBranch.ProductType);
        ////////    //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
        ////////    if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        //ed.WriteMessage("***Conductor\n");
        ////////        #region Conductor
        ////////        Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode);

        ////////        if (MyConducyorTip.PhaseCount > 0)
        ////////        {
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "0";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2); ;
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //ed.WriteMessage("%%%%%%%%%fmax={0}\n", fmax);
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);
        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "1";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["conductorCode"] = "2";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }
        ////////    else
        ////////    {
        ////////        #region SelfKeeper
        ////////        //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
        ////////        Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
        ////////        //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
        ////////        if (MySelfKeeperTip.PhaseCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "0";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "1";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension02();
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["conductorCode"] = "2";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = sectionCode;
        ////////            //dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }

        ////////    return dtStTable;
        ////////}

        ////////public DataTable calcRudSurface()
        ////////{
        ////////    DataTable dtTable = new DataTable();
        ////////    DataColumn dcPole = new DataColumn("dcPole");
        ////////    DataColumn dcNorm = new DataColumn("dcNorm");
        ////////    DataColumn dcIceHeave = new DataColumn("dcIceHeavy");
        ////////    DataColumn dcWindSpeed = new DataColumn("dcWindSpeed");
        ////////    DataColumn dcMaxTemp = new DataColumn("dcMaxTemp");
        ////////    DataColumn dcMinTemp = new DataColumn("dcMinTemp");
        ////////    DataColumn dcWindIce = new DataColumn("dcWindIce");
        ////////    dtTable.Columns.Add(dcPole);
        ////////    dtTable.Columns.Add(dcIceHeave);
        ////////    dtTable.Columns.Add(dcMaxTemp);
        ////////    dtTable.Columns.Add(dcMinTemp);
        ////////    dtTable.Columns.Add(dcNorm);
        ////////    dtTable.Columns.Add(dcWindIce);
        ////////    dtTable.Columns.Add(dcWindSpeed);



        ////////    double[] temp = new double[6];
        ////////    DataTable dtPoleConductor = new DataTable();
        ////////    dtPoleConductor.Columns.Add("NodeGuid");
        ////////    dtPoleConductor.Columns.Add("BranchGuid");
        ////////    dtPoleConductor.Columns.Add("Angle");
        ////////    dtPoleConductor.Columns.Add("BranchType");
        ////////    dtPoleConductor.Columns.Add("NodeType");
        ////////    foreach (DataRow dr in DtPoleSection.Rows)
        ////////    {

        ////////        dtPoleConductor.Rows.Clear();
        ////////        //ed.WriteMessage("&&&&&&&&&&&&dtPoleConductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
        ////////        DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
        ////////         //ed.WriteMessage("dtConductor={0}\n", dtConductor.Length);
        ////////        foreach (DataRow drConductor in dtConductor)
        ////////        {
        ////////            DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
        ////////            if (drs1.Length != 0)
        ////////            {
        ////////                //ed.WriteMessage("Add First\n");
        ////////                DataRow NewRow = dtPoleConductor.NewRow();
        ////////                NewRow["NodeGuid"] = drs1[0]["Node1Guid"].ToString();
        ////////                NewRow["BranchGuid"] = drs1[0]["BranchGuid"].ToString();
        ////////                NewRow["Angle"] = drs1[0]["Angle1"].ToString();
        ////////                NewRow["BranchType"] = drs1[0]["Type"].ToString();
        ////////                NewRow["NodeType"] = drConductor["NodeType"].ToString();
        ////////                dtPoleConductor.Rows.Add(NewRow);

        ////////            }

        ////////            DataRow[] drs2 = dtBranchList.Select(string.Format(" Node2Guid='{0}'", drConductor["NodeGuid"].ToString()));

        ////////            if (drs2.Length != 0)
        ////////            {
        ////////                //ed.WriteMessage("Add Secnd\n");
        ////////                DataRow NewRow = dtPoleConductor.NewRow();
        ////////                NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
        ////////                NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
        ////////                NewRow["Angle"] = drs2[0]["Angle2"].ToString();
        ////////                NewRow["BranchType"] = drs2[0]["Type"].ToString();
        ////////                NewRow["NodeType"] = drConductor["NodeType"].ToString();

        ////////                dtPoleConductor.Rows.Add(NewRow);

        ////////            }

        ////////        }


        ////////        for (int i = 0; i < 6; i++)
        ////////        {
        ////////            Atend.Base.Design.DWeather wether = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////            temp[i] = CalcFonRudSurface(dtPoleConductor, wether, new Guid(dr["ProductCode"].ToString()), i);
        ////////        }

        ////////        DataRow dr1 = dtTable.NewRow();
        ////////        dr1["dcPole"] = dr["PoleNumber"].ToString();
        ////////        dr1["dcNorm"] = Math.Round(Convert.ToDouble(temp[0].ToString()), 2);
        ////////        dr1["dcIceHeavy"] = Math.Round(Convert.ToDouble(temp[1].ToString()), 2);
        ////////        dr1["dcWindSpeed"] = Math.Round(Convert.ToDouble(temp[2].ToString()), 2);
        ////////        dr1["dcMaxTemp"] = Math.Round(Convert.ToDouble(temp[3].ToString()), 2);
        ////////        dr1["dcMinTemp"] = Math.Round(Convert.ToDouble(temp[4].ToString()), 2);
        ////////        dr1["dcWindIce"] = Math.Round(Convert.ToDouble(temp[5].ToString()), 2);
        ////////        dtTable.Rows.Add(dr1);

        ////////    }
        ////////    return dtTable;
        ////////}

        ////////public double CalcFonRudSurface(DataTable dtCond, Atend.Base.Design.DWeather weather, Guid PoleGuid1, int i)
        ////////{

        ////////    double f = 0;
        ////////    double H = 0;
        ////////    double DH = 0;
        ////////    double fPrim = 0;
        ////////    double WTotal = 0;
        ////////    double Seq = 0;
        ////////    double Leq = 0;
        ////////    double Force = 0;
        ////////    double FinalForce = 0;

        ////////    Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
        ////////    //ed.WriteMessage("%%%%%%%%%%%dtcond.rows.count={0},Poleguid={1}\n",dtCond.Rows.Count,PoleGuid1.ToString());
        ////////    foreach (DataRow dr in dtCond.Rows)
        ////////    {
        ////////        if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////        {
        ////////            ed.WriteMessage("NodeType=Consol,I={0}\n",i);
        ////////            Guid nextNode = Atend.Global.Acad.UAcad.GetNextNode(new Guid(dr["NodeGuid"].ToString()), new Guid(dr["BranchGuid"].ToString()), dtBranchList);

        ////////            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, nextNode);
        ////////            Atend.Base.Design.DPackage dPackNode = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, dPack.ParentCode);
        ////////            DH = CalcDH(PoleGuid1, dPackNode.NodeCode);
        ////////            //ed.WriteMessage("DH={0}\n",DH.ToString());
        ////////            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

        ////////            //ed.WriteMessage("It Is Tangant Consol\n");
        ////////            Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////            //ed.WriteMessage("SectionCode={0}\n",Section.SectionCode.ToString());
        ////////            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
        ////////            if (drSec.Length == 0)
        ////////            {
        ////////                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////                sectionCode = Section.SectionCode;
        ////////                CalSagTensionForRudSurface(MyBranch);
        ////////                //ed.WriteMessage("CalcSagTension\n");
        ////////            }
        ////////            Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, MyBranch.ProductCode);
        ////////            if (conductorTip.PhaseCount > 0)
        ////////            {

        ////////                ed.WriteMessage("IN Phase\n");
        ////////                int ConductorCode = 0;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode,f);
        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr1[0]["WindAndIceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                //ed.WriteMessage("f={0},DH={1}\n", f, DH);
        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////                //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n",H,fPrim,WTotal);
        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////                //ed.WriteMessage("Seq={0}\n",Seq);
        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal*-1;

        ////////                }
        ////////                //ed.WriteMessage("Force={0}\n",Force);
        ////////                FinalForce += Force * conductorTip.PhaseCount;

        ////////                //ed.WriteMessage("FinalForcePhase={0}\n",FinalForce);
        ////////            }


        ////////            if (conductorTip.NightCount > 0)
        ////////            {
        ////////                ed.WriteMessage("iN tHE nIGHT \n");
        ////////                int ConductorCode = 1;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                //ed.WriteMessage("f={0},DH={1}\n", f, DH);

        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////                //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n", H, fPrim, WTotal);

        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////                //ed.WriteMessage("Seq={0}\n", Seq);

        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal * -1;

        ////////                }
        ////////                //ed.WriteMessage("Force={0}\n", Force);

        ////////                FinalForce += Force * conductorTip.NightCount;
        ////////               // ed.WriteMessage("FinalForceNight={0}\n", FinalForce);
        ////////            }

        ////////            if (conductorTip.NeutralCount > 0)
        ////////            {
        ////////                ed.WriteMessage("In The Neutral\n");
        ////////                int ConductorCode = 2;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                //ed.WriteMessage("f={0},DH={1}\n", f, DH);

        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////               // ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n", H, fPrim, WTotal);

        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////               // ed.WriteMessage("Seq={0}\n", Seq);

        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal * -1;

        ////////                }
        ////////               // ed.WriteMessage("Force={0}\n", Force);

        ////////                FinalForce += Force * conductorTip.NeutralCount;
        ////////                //ed.WriteMessage("FinalForceNetural={0}\n", FinalForce);
        ////////            }
        ////////        }



        ////////        if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////        {

        ////////            //ed.WriteMessage("NodeType=kalamp\n");
        ////////            Guid nextNode = Atend.Global.Acad.UAcad.GetNextNode(new Guid(dr["NodeGuid"].ToString()), new Guid(dr["BranchGuid"].ToString()), dtBranchList);

        ////////            Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, nextNode);
        ////////            Atend.Base.Design.DPackage dPackNode = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, dPack.ParentCode);
        ////////            DH = CalcDH(PoleGuid1, dPackNode.NodeCode);

        ////////            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////            //ed.WriteMessage("It Is Tangant Consol\n");
        ////////            Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));

        ////////            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
        ////////            if (drSec.Length == 0)
        ////////            {
        ////////                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                sectionCode = Section.SectionCode;
        ////////                CalSagTensionForRudSurface(MyBranch);
        ////////                //ed.WriteMessage("CalcSagTension\n");
        ////////            }
        ////////            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, MyBranch.ProductCode);
        ////////            if (SelfKeeperTip.PhaseCount > 0)
        ////////            {

        ////////                //ed.WriteMessage("IN Phase\n");
        ////////                int ConductorCode = 0;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            // ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);


        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr1[0]["WindAndIceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                //ed.WriteMessage("f={0},DH={1}\n", f, DH);
        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////                //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n",H,fPrim,WTotal);
        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////                //ed.WriteMessage("Seq={0}\n",Seq);
        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal * -1;

        ////////                }
        ////////                //ed.WriteMessage("Force={0}\n",Force);
        ////////                FinalForce += Force * SelfKeeperTip.PhaseCount;

        ////////                //ed.WriteMessage("FinalForcePhase={0}\n",FinalForce);
        ////////            }


        ////////            if (SelfKeeperTip.NightCount > 0)
        ////////            {



        ////////                int ConductorCode = 1;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal * -1;

        ////////                }
        ////////                FinalForce += Force * SelfKeeperTip.NightCount;
        ////////                //ed.WriteMessage("FinalForceNight={0}\n", FinalForce);
        ////////            }

        ////////            if (SelfKeeperTip.NeutralCount > 0)
        ////////            {

        ////////                int ConductorCode = 2;
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                WTotal = general.ComputeTotalWeight(weather);
        ////////                #region Swich
        ////////                //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                switch (i)
        ////////                {
        ////////                    case 0:
        ////////                        {
        ////////                            //ed.WriteMessage("$$$pp");
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                            f = Convert.ToDouble(dr1[0]["NormF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                            //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 1:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["IceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                            //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 2:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                            //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 3:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                            //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 4:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                            //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
        ////////                            //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                    case 5:
        ////////                        {
        ////////                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                            f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
        ////////                            H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                            //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
        ////////                           // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

        ////////                            break;
        ////////                        };
        ////////                }

        ////////                #endregion
        ////////                fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
        ////////                Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
        ////////                Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
        ////////                if (DH > (4 * f))
        ////////                {
        ////////                    Force = Leq / 2 * WTotal;
        ////////                }
        ////////                else
        ////////                {
        ////////                    Force = Leq / 2 * WTotal * -1;

        ////////                }
        ////////                FinalForce += Force * SelfKeeperTip.NeutralCount;
        ////////                //ed.WriteMessage("FinalForceNetural={0}\n", FinalForce);
        ////////            }
        ////////        }
        ////////    }
        ////////    return FinalForce;
        ////////}

        ////////private double CalcDH(Guid PoleGuid1, Guid PoleGuid2)
        ////////{
        ////////    Atend.Base.Design.DNode Node1 = Atend.Base.Design.DNode.AccessSelectByCode(PoleGuid1);
        ////////    Atend.Base.Design.DNode Node2 = Atend.Base.Design.DNode.AccessSelectByCode(PoleGuid2);
        ////////    double DH = Node2.Height - Node1.Height;
        ////////    //if (DH > (4 * flash))
        ////////    //{
        ////////    //}
        ////////    //else
        ////////    //{
        ////////    //    if (DH > 0)
        ////////    //    {
        ////////    //        DH = DH * -1;
        ////////    //    }
        ////////    //}
        ////////    ed.WriteMessage("*DH={0}\n",DH.ToString());

        ////////    return DH;
        ////////}

        public DataTable CalSagTensionForRudSurface02(Atend.Base.Design.DBranch MyBranch)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            //ed.WriteMessage("fmaxxxxxxxx={0}\n",fmax);
            //ed.WriteMessage("Start CalcMyBranch.ProductCode={0}\n", MyBranch.ProductType);
            //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                //ed.WriteMessage("***Conductor\n");
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode, aConnection);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2); ;
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //ed.WriteMessage("%%%%%%%%%fmax={0}\n", fmax);
                    //dr["MaxF"] = Math.Round(fmax, 2);
                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                #region SelfKeeper
                //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
                Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
                //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
                if (MySelfKeeperTip.PhaseCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                    CalcSagTension02();
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = sectionCode;
                    //dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }

            return dtStTable;
        }
        public DataTable calcRudSurface02()
        {
            DataTable dtTable = new DataTable();
            DataColumn dcPole = new DataColumn("dcPole");
            DataColumn dcNorm = new DataColumn("dcNorm");
            DataColumn dcIceHeave = new DataColumn("dcIceHeavy");
            DataColumn dcWindSpeed = new DataColumn("dcWindSpeed");
            DataColumn dcMaxTemp = new DataColumn("dcMaxTemp");
            DataColumn dcMinTemp = new DataColumn("dcMinTemp");
            DataColumn dcWindIce = new DataColumn("dcWindIce");
            dtTable.Columns.Add(dcPole);
            dtTable.Columns.Add(dcIceHeave);
            dtTable.Columns.Add(dcMaxTemp);
            dtTable.Columns.Add(dcMinTemp);
            dtTable.Columns.Add(dcNorm);
            dtTable.Columns.Add(dcWindIce);
            dtTable.Columns.Add(dcWindSpeed);



            double[] temp = new double[6];
            DataTable dtPoleConductor = new DataTable();
            dtPoleConductor.Columns.Add("NodeGuid");
            dtPoleConductor.Columns.Add("BranchGuid");
            dtPoleConductor.Columns.Add("Angle");
            dtPoleConductor.Columns.Add("BranchType");
            dtPoleConductor.Columns.Add("NodeType");
            foreach (DataRow dr in DtPoleSection.Rows)
            {

                dtPoleConductor.Rows.Clear();
                //ed.WriteMessage("&&&&&&&&&&&&dtPoleConductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
                DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
                //ed.WriteMessage("dtConductor={0}\n", dtConductor.Length);
                foreach (DataRow drConductor in dtConductor)
                {
                    DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
                    if (drs1.Length != 0)
                    {
                        //ed.WriteMessage("Add First\n");
                        DataRow NewRow = dtPoleConductor.NewRow();
                        NewRow["NodeGuid"] = drs1[0]["Node1Guid"].ToString();
                        NewRow["BranchGuid"] = drs1[0]["BranchGuid"].ToString();
                        NewRow["Angle"] = drs1[0]["Angle1"].ToString();
                        NewRow["BranchType"] = drs1[0]["Type"].ToString();
                        NewRow["NodeType"] = drConductor["NodeType"].ToString();
                        dtPoleConductor.Rows.Add(NewRow);

                    }

                    DataRow[] drs2 = dtBranchList.Select(string.Format(" Node2Guid='{0}'", drConductor["NodeGuid"].ToString()));

                    if (drs2.Length != 0)
                    {
                        //ed.WriteMessage("Add Secnd\n");
                        DataRow NewRow = dtPoleConductor.NewRow();
                        NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
                        NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
                        NewRow["Angle"] = drs2[0]["Angle2"].ToString();
                        NewRow["BranchType"] = drs2[0]["Type"].ToString();
                        NewRow["NodeType"] = drConductor["NodeType"].ToString();

                        dtPoleConductor.Rows.Add(NewRow);

                    }

                }


                for (int i = 0; i < 6; i++)
                {
                    Atend.Base.Design.DWeather wether = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                    temp[i] = CalcFonRudSurface02(dtPoleConductor, wether, new Guid(dr["ProductCode"].ToString()), i);
                }

                DataRow dr1 = dtTable.NewRow();
                dr1["dcPole"] = dr["PoleNumber"].ToString();
                dr1["dcNorm"] = Math.Round(Convert.ToDouble(temp[0].ToString()), 2);
                dr1["dcIceHeavy"] = Math.Round(Convert.ToDouble(temp[1].ToString()), 2);
                dr1["dcWindSpeed"] = Math.Round(Convert.ToDouble(temp[2].ToString()), 2);
                dr1["dcMaxTemp"] = Math.Round(Convert.ToDouble(temp[3].ToString()), 2);
                dr1["dcMinTemp"] = Math.Round(Convert.ToDouble(temp[4].ToString()), 2);
                dr1["dcWindIce"] = Math.Round(Convert.ToDouble(temp[5].ToString()), 2);
                dtTable.Rows.Add(dr1);

            }
            return dtTable;
        }
        public double CalcFonRudSurface02(DataTable dtCond, Atend.Base.Design.DWeather weather, Guid PoleGuid1, int i)
        {

            double f = 0;
            double H = 0;
            double DH = 0;
            double fPrim = 0;
            double WTotal = 0;
            double Seq = 0;
            double Leq = 0;
            double Force = 0;
            double FinalForce = 0;

            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            //ed.WriteMessage("%%%%%%%%%%%dtcond.rows.count={0},Poleguid={1}\n",dtCond.Rows.Count,PoleGuid1.ToString());
            foreach (DataRow dr in dtCond.Rows)
            {
                if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    ed.WriteMessage("NodeType=Consol,I={0}\n", i);
                    Guid nextNode = Atend.Global.Acad.UAcad.GetNextNode(new Guid(dr["NodeGuid"].ToString()), new Guid(dr["BranchGuid"].ToString()), dtBranchList);

                    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, nextNode);
                    Atend.Base.Design.DPackage dPackNode = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, dPack.ParentCode);
                    DH = CalcDH02(PoleGuid1, dPackNode.NodeCode);
                    //ed.WriteMessage("DH={0}\n",DH.ToString());
                    Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                    //ed.WriteMessage("It Is Tangant Consol\n");
                    Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                    //ed.WriteMessage("SectionCode={0}\n",Section.SectionCode.ToString());
                    DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
                    if (drSec.Length == 0)
                    {
                        DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor), aConnection);
                        sectionCode = Section.SectionCode;
                        CalSagTensionForRudSurface02(MyBranch);
                        //ed.WriteMessage("CalcSagTension\n");
                    }
                    Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, MyBranch.ProductCode);
                    if (conductorTip.PhaseCount > 0)
                    {

                        ed.WriteMessage("IN Phase\n");
                        int ConductorCode = 0;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode,f);
                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr1[0]["WindAndIceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        //ed.WriteMessage("f={0},DH={1}\n", f, DH);
                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n",H,fPrim,WTotal);
                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        //ed.WriteMessage("Seq={0}\n",Seq);
                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        //ed.WriteMessage("Force={0}\n",Force);
                        FinalForce += Force * conductorTip.PhaseCount;

                        //ed.WriteMessage("FinalForcePhase={0}\n",FinalForce);
                    }


                    if (conductorTip.NightCount > 0)
                    {
                        ed.WriteMessage("iN tHE nIGHT \n");
                        int ConductorCode = 1;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        //ed.WriteMessage("f={0},DH={1}\n", f, DH);

                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n", H, fPrim, WTotal);

                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        //ed.WriteMessage("Seq={0}\n", Seq);

                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        //ed.WriteMessage("Force={0}\n", Force);

                        FinalForce += Force * conductorTip.NightCount;
                        // ed.WriteMessage("FinalForceNight={0}\n", FinalForce);
                    }

                    if (conductorTip.NeutralCount > 0)
                    {
                        ed.WriteMessage("In The Neutral\n");
                        int ConductorCode = 2;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        //ed.WriteMessage("f={0},DH={1}\n", f, DH);

                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        // ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n", H, fPrim, WTotal);

                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        // ed.WriteMessage("Seq={0}\n", Seq);

                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        // ed.WriteMessage("Force={0}\n", Force);

                        FinalForce += Force * conductorTip.NeutralCount;
                        //ed.WriteMessage("FinalForceNetural={0}\n", FinalForce);
                    }
                }



                if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                {

                    //ed.WriteMessage("NodeType=kalamp\n");
                    Guid nextNode = Atend.Global.Acad.UAcad.GetNextNode(new Guid(dr["NodeGuid"].ToString()), new Guid(dr["BranchGuid"].ToString()), dtBranchList);

                    Atend.Base.Design.DPackage dPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, nextNode);
                    Atend.Base.Design.DPackage dPackNode = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, dPack.ParentCode);
                    DH = CalcDH02(PoleGuid1, dPackNode.NodeCode);

                    Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                    //ed.WriteMessage("It Is Tangant Consol\n");
                    Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));

                    DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
                    if (drSec.Length == 0)
                    {
                        DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                        sectionCode = Section.SectionCode;
                        CalSagTensionForRudSurface02(MyBranch);
                        //ed.WriteMessage("CalcSagTension\n");
                    }
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, MyBranch.ProductCode);
                    if (SelfKeeperTip.PhaseCount > 0)
                    {

                        //ed.WriteMessage("IN Phase\n");
                        int ConductorCode = 0;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    // ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);


                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr1[0]["WindAndIceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        //ed.WriteMessage("f={0},DH={1}\n", f, DH);
                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        //ed.WriteMessage("H={0},fPrim={1},Wtotal={2}\n",H,fPrim,WTotal);
                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        //ed.WriteMessage("Seq={0}\n",Seq);
                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        //ed.WriteMessage("Force={0}\n",Force);
                        FinalForce += Force * SelfKeeperTip.PhaseCount;

                        //ed.WriteMessage("FinalForcePhase={0}\n",FinalForce);
                    }


                    if (SelfKeeperTip.NightCount > 0)
                    {



                        int ConductorCode = 1;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        FinalForce += Force * SelfKeeperTip.NightCount;
                        //ed.WriteMessage("FinalForceNight={0}\n", FinalForce);
                    }

                    if (SelfKeeperTip.NeutralCount > 0)
                    {

                        int ConductorCode = 2;
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                        WTotal = general.ComputeTotalWeight(weather);
                        #region Swich
                        //ed.WriteMessage("i={0}\n",i.ToString());
                        switch (i)
                        {
                            case 0:
                                {
                                    //ed.WriteMessage("$$$pp");
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                                    f = Convert.ToDouble(dr1[0]["NormF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                    //ed.WriteMessage("NormH= {0},NormF={1}\n", dr1[0]["NormH"].ToString(), dr1[0]["NormF"].ToString());
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 1:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["IceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["IceH"].ToString());
                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "IceF= " + dr1[0]["IceF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 2:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindH"].ToString());
                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "WindF= " + dr1[0]["WindF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 3:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MaxTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "MaxTempF= " + dr1[0]["MaxTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 4:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["MinTempF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "MinTempF= " + dr1[0]["MinTempF"].ToString() + "\n");
                                    //DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                            case 5:
                                {
                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                    f = Convert.ToDouble(dr1[0]["WindAndIceF"].ToString());
                                    H = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "WindAndIceF= " + dr["WindAndIceF"].ToString() + "\n");
                                    // DH = CalcDH(PoleGuid1, dPackNode.NodeCode, f);

                                    break;
                                };
                        }

                        #endregion
                        fPrim = f * Math.Pow((1 - (Math.Abs(DH) / 4)), 2);
                        Seq = Math.Pow(((8 * fPrim * H) / WTotal), .5);
                        Leq = Seq + ((Math.Pow(WTotal, 2) * Math.Pow(Seq, 3)) / (24 * Math.Pow(H, 2)));
                        if (DH > (4 * f))
                        {
                            Force = Leq / 2 * WTotal;
                        }
                        else
                        {
                            Force = Leq / 2 * WTotal * -1;

                        }
                        FinalForce += Force * SelfKeeperTip.NeutralCount;
                        //ed.WriteMessage("FinalForceNetural={0}\n", FinalForce);
                    }
                }
            }
            return FinalForce;
        }
        private double CalcDH02(Guid PoleGuid1, Guid PoleGuid2)
        {
            Atend.Base.Design.DNode Node1 = Atend.Base.Design.DNode.AccessSelectByCode(PoleGuid1, aConnection);
            Atend.Base.Design.DNode Node2 = Atend.Base.Design.DNode.AccessSelectByCode(PoleGuid2, aConnection);
            double DH = Node2.Height - Node1.Height;
            //if (DH > (4 * flash))
            //{
            //}
            //else
            //{
            //    if (DH > 0)
            //    {
            //        DH = DH * -1;
            //    }
            //}
            ed.WriteMessage("*DH={0}\n", DH.ToString());

            return DH;
        }



        //***************Mechanical AutoPoleInstalation*******************

        ////////public void CalcSagTension(double se, double UTS)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //ed.WriteMessage("&&&&Start CalcSagTension\n");
        ////////    ////ed.WriteMessage("dtConductorSection.Count= {0}\n",DtconductorSection.Rows.Count);
        ////////    ////ed.WriteMessage("dtconductorSection.Rows[0][ProductCode]= " + dtconductorSection.Rows[0]["ProductCode"].ToString()+"\n");
        ////////    //Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));

        ////////    //ed.WriteMessage("MyBranch.ProductCode1= " + myBranch.ProductCode.ToString() + "\n");
        ////////    ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(.ProductCode);
        ////////    ////ed.WriteMessage("ConducTor.Code= "+conductor.Code.ToString()+"\n");

        ////////    double BaseH = ComputeHC(UTS);
        ////////    //ed.WriteMessage("BaseH= " + BaseH.ToString() + "\n");
        ////////    Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
        ////////    Atend.Base.Design.DWeather WeatherBase;
        ////////    Atend.Base.Design.DWeather WeatherSecond;
        ////////    double normalH;


        ////////    //double se = ComputeSE();
        ////////    double sc = computesc(BaseH);
        ////////    //ed.WriteMessage("sc={0},Se={1}\n", sc.ToString(), se.ToString());
        ////////    //if (sc == -1)
        ////////    //{
        ////////    //    //ed.WriteMessage("Sc=-1\n");
        ////////    //    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////    //}
        ////////    //else
        ////////    //{
        ////////    if (se > sc)
        ////////    {
        ////////        //ed.WriteMessage("Se>SC\n");
        ////////        WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////    }
        ////////    else
        ////////    {
        ////////        //ed.WriteMessage("Se<=SC\n");
        ////////        WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
        ////////    }
        ////////    //}
        ////////    WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی معمول شرط ثانویه می باشد
        ////////    //ed.WriteMessage("weatherBase={0},WeatherSecondCode={1}\n", WeatherBase.Code, WeatherSecond.Code);
        ////////    normalH = general.ComputeTension(WeatherBase, WeatherSecond, BaseH, se);
        ////////    h[0] = normalH;
        ////////    f[0] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[0]);
        ////////    //ed.WriteMessage("H[0]= " + h[0].ToString() + "   F[0]= " + f[0].ToString() + "\n");
        ////////    WeatherBase = WeatherSecond;


        ////////    int i = 1;
        ////////    do
        ////////    {

        ////////        WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////        //ed.WriteMessage("*weatherSecond.Code={0},i={1},ConditionCode={2},Temp={3}\n", WeatherSecond.Code, i, WeatherSecond.ConditionCode, WeatherSecond.Temp);

        ////////        h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[0], se);
        ////////        f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[i]);
        ////////        i++;
        ////////    } while (i < 6);
        ////////    //ed.WriteMessage("FiniSh\n");
        ////////    //for (i = 0; i < 6; i++)
        ////////    //{
        ////////    //    ed.WriteMessage("i = " + i.ToString() + "= " + h[i].ToString() + "\n");
        ////////    //}

        ////////}

        //////////MOUSAVI->autopoleinstallation
        ////////public bool CalSagTension(Atend.Base.Design.DBranch MyBranch, double se, Atend.Base.Equipment.EConsol Consol, Atend.Base.Equipment.EClamp Calamp, Atend.Base.Equipment.EPole Pole, double Clearance, double TrustBorder, double UTS)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    dtStTable.Rows.Clear();

        ////////    //ed.WriteMessage("SPANLENGTH:{0}\n", se);
        ////////    //ed.WriteMessage("~~~~~~~~~~Start SE={0}\n", se.ToString());
        ////////    //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
        ////////    if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        //ed.WriteMessage("***Conductor\n");
        ////////        #region Conductor
        ////////        Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode);

        ////////        if (MyConducyorTip.PhaseCount > 0)
        ////////        {
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "0";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2); ;
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";
        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "1";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["conductorCode"] = "2";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }
        ////////    else
        ////////    {
        ////////        #region SelfKeeper
        ////////        //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
        ////////        Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
        ////////        //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
        ////////        if (MySelfKeeperTip.PhaseCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "0";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["ConductorCode"] = "1";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension(se, UTS);
        ////////            DataRow dr = dtStTable.NewRow();
        ////////            dr["conductorCode"] = "2";
        ////////            dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(h[0], 2);
        ////////            dr["NormF"] = Math.Round(f[0], 2);
        ////////            dr["IceH"] = Math.Round(h[1], 2);
        ////////            dr["IceF"] = Math.Round(f[1], 2);
        ////////            dr["WindH"] = Math.Round(h[2], 2);
        ////////            dr["WindF"] = Math.Round(f[2], 2);
        ////////            dr["MaxTempH"] = Math.Round(h[3], 2);
        ////////            dr["MAxTempF"] = Math.Round(f[3], 2);
        ////////            dr["MinTempH"] = Math.Round(h[4], 2);
        ////////            dr["MinTempF"] = Math.Round(f[4], 2);
        ////////            dr["WindAndIceH"] = Math.Round(h[5], 2);
        ////////            dr["WindAndIceF"] = Math.Round(f[5], 2);
        ////////            dr["SectionCode"] = "0";

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }
        ////////    double hMax = ComputeHC(UTS);
        ////////    double fMax = FindBestFMAx(Pole, Consol, Calamp, clearance, TrustBorder, MyBranch);

        ////////    //foreach (DataRow dr in dtStTable.Rows)
        ////////    //{
        ////////    //    ed.WriteMessage("**NORMF={0}*NormH={1}\n", dr["NormF"].ToString(), dr["NormH"].ToString());
        ////////    //    ed.WriteMessage("**IceF={0}*IceH={1}\n", dr["IceF"].ToString(), dr["IceH"].ToString());
        ////////    //    ed.WriteMessage("**WindF={0}*WindH={1}\n", dr["WindF"].ToString(), dr["WindH"].ToString());
        ////////    //    ed.WriteMessage("**MaxTempFF={0}*MaxTempH={1}\n", dr["MaxTempF"].ToString(), dr["MaxTempH"].ToString());

        ////////    //    ed.WriteMessage("**MinTempF={0}*MinTempH={1}\n", dr["MinTempF"].ToString(), dr["MinTempH"].ToString());
        ////////    //    ed.WriteMessage("**WindAndIceF={0}*WindAndIceH={1}\n", dr["WindAndIceF"].ToString(), dr["WindAndIceH"].ToString());

        ////////    //}
        ////////    //ed.WriteMessage("fMax={0},hMax={1}\n", fMax, hMax);
        ////////    return IsResultOK(fMax, hMax);
        ////////}

        ////////public double computesc(double hc)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        ////////    Atend.Base.Design.DWeather weatherWind = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد
        ////////    //ed.WriteMessage("WeatherWind.IceDiagonal=" + weatherWind.IceDiagonal.ToString() + "weatherWind.Temp= " + weatherWind.Temp + "\n");
        ////////    Atend.Base.Design.DWeather weatherIce = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین
        ////////    //ed.WriteMessage("WeatherICE.IceDiagonal= " + weatherIce.Temp.ToString() + "weatherIce.IceDiagonal= " + weatherIce.IceDiagonal + "\n");

        ////////    double iceweight = 913 * Math.PI * weatherIce.IceDiagonal * (weatherIce.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;//wic=913*3.14*i[i+d]*1e-6
        ////////    double WwForIce = Math.Pow(weatherIce.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherIce.IceDiagonal) * 1e-3;
        ////////    double WTotalForIce = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + iceweight, 2) + Math.Pow(WwForIce, 2));
        ////////    //ed.WriteMessage("WTotalForIce={0},iceWeight={1},WwForIce={2},WC={3}\n ", WTotalForIce.ToString(), iceweight, WwForIce, Atend.Global.Calculation.Mechanical.CCommon.WC);

        ////////    double windweight = Math.Pow(weatherWind.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherWind.IceDiagonal) * 1e-3;//ww=v^2/16*(d+2i)*10^-3
        ////////    double WiForWind = 913 * Math.PI * weatherWind.IceDiagonal * (weatherWind.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;
        ////////    double wTotalForWind = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + WiForWind, 2) + Math.Pow(windweight, 2));

        ////////    //ed.WriteMessage("wTotalForWind ={0},windWeight={1},WiForWind={2}\n", wTotalForWind.ToString(), windweight, WiForWind);
        ////////    //double hc = ComputeHC();
        ////////    //ed.WriteMessage("HC= " + hc + "\n");
        ////////    //ed.WriteMessage("Conductor.Alpha= " + conductor.Alpha + "\n");
        ////////    //if (WTotalForIce > wTotalForWind)
        ////////    //{
        ////////    //    //ed.WriteMessage("WTotalForIce > wTotalForWind\n");
        ////////    //    return -1;
        ////////    //}
        ////////    //else
        ////////    //{
        ////////    double sc = (24 * Math.Pow(hc, 2) * Atend.Global.Calculation.Mechanical.CCommon.Alpha * (weatherWind.Temp - weatherIce.Temp)) / (Math.Pow(wTotalForWind, 2) - Math.Pow(WTotalForIce, 2));
        ////////    sc = Math.Sqrt(Math.Abs(sc));
        ////////    //ed.WriteMessage("***SC= " + sc.ToString() + "\n");
        ////////    return sc;
        ////////    //}
        ////////}

        ////////public double ComputeHC(double UTS)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    double HC = (UTS / 100) * Atend.Global.Calculation.Mechanical.CCommon.UTS;
        ////////    //ed.WriteMessage("HC={0}\n", HC);
        ////////    return HC;
        ////////}

        ////////public double FindBestFMAx(Atend.Base.Equipment.EPole pole, Atend.Base.Equipment.EConsol Consol, Atend.Base.Equipment.EClamp Calamp, double Clearance, double TrusthBorder, Atend.Base.Design.DBranch MyBranch)
        ////////{
        ////////    //ed.WriteMessage("CrossSectionArea={0}\n", Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////    Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////    //ed.WriteMessage("PoleHeight= " + pole.Height.ToString() + "\n");
        ////////    double Depth = .1 * pole.Height + .6;
        ////////    double ke = 1;

        ////////    //ed.WriteMessage("Ke= " + ke.ToString() + "\n");
        ////////    //ed.WriteMessage("Ke= "+ke.ToString()+"\n");
        ////////    double fmax1 = 0;
        ////////    if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
        ////////    {
        ////////        //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
        ////////        fmax1 = pole.Height - (Depth + Calamp.DistanceSupport + clearance + TrusthBorder);
        ////////        //ed.WriteMessage("FMAX!={0}\n",fmax1);
        ////////    }

        ////////    //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
        ////////    double fmax2 = 0;
        ////////    double FMAX = fmax1;
        ////////    if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        double Vn = Volt / 1000;
        ////////        if (cke != null)
        ////////        {
        ////////            switch (Consol.Type)
        ////////            {
        ////////                case 0: { ke = cke.Vertical; break; }
        ////////                case 1: { ke = cke.Triangle; break; }
        ////////                case 2: { ke = cke.Horizental; break; }


        ////////            }

        ////////        }
        ////////        //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n",Depth,LenghtChain,consol.DistanceCrossArm,Clearance,ZaribEtminan);
        ////////        //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
        ////////        fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance + TrusthBorder);
        ////////        //ed.WriteMessage("fMAx1={0}\n",fmax1);
        ////////        //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
        ////////        fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
        ////////        //ed.WriteMessage("**********fMax1={0},fMax2={1}\n", fmax1, fmax2);
        ////////        if (fmax1 < fmax2)
        ////////            FMAX = fmax1;
        ////////        else
        ////////            FMAX = fmax2;
        ////////    }

        ////////    return FMAX;
        ////////}

        ////////public bool IsResultOK(double FMax, double hMax)
        ////////{
        ////////    bool result = true;
        ////////    //foreach (DataRow dr1 in dtStTable.Rows)
        ////////    //{
        ////////    //    ed.WriteMessage("NormF={0},IceF={1},WindF={2},MaxTempF={3},MinTempF={4},WindAndIceF={4}\n", dr1["NormF"].ToString(), dr1["IceF"].ToString(), dr1["WindF"].ToString(), dr1["MaxTempF"].ToString(), dr1["MinTempF"].ToString(), dr1["WindAndIceF"].ToString());
        ////////    //    ed.WriteMessage("NormH={0},IceH={1},WindH={2},MaxTempH={3},MinTempH={4},WindAndIceH={4}\n", dr1["NormH"].ToString(), dr1["IceH"].ToString(), dr1["WindH"].ToString(), dr1["MaxTempH"].ToString(), dr1["MinTempH"].ToString(), dr1["WindAndIceH"].ToString());
        ////////    //    ed.WriteMessage("~~~~~~~~~~~~~\n");
        ////////    //}

        ////////    //ed.WriteMessage("######fMax={0},HMAX={1}\n", FMax, hMax);
        ////////    foreach (DataRow dr in dtStTable.Rows)
        ////////    {

        ////////        #region Sag
        ////////        if (Convert.ToDouble(dr["NormF"].ToString()) > FMax)
        ////////        {
        ////////            //ed.WriteMessage("*******1={0}\n", dr["NormF"].ToString());
        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["IceF"].ToString()) > FMax)
        ////////        {
        ////////            //ed.WriteMessage("*********2={0}\n", dr["IceF"].ToString());

        ////////            result = false;
        ////////        }

        ////////        if (Convert.ToDouble(dr["WindF"].ToString()) > FMax)
        ////////        {
        ////////            //ed.WriteMessage("**********3={0}\n", dr["WindF"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["MaxTempF"].ToString()) > FMax)
        ////////        {
        ////////            //ed.WriteMessage("*********4={0}\n", dr["MaxTempF"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["MinTempF"].ToString()) > FMax)
        ////////        {
        ////////            // ed.WriteMessage("**********5={0}\n", dr["MinTempF"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["WindAndIceF"].ToString()) > FMax)
        ////////        {
        ////////            // ed.WriteMessage("**********6={0}\n", dr["WindAndIceF"].ToString());

        ////////            result = false;
        ////////        }

        ////////        #endregion

        ////////        #region Tension
        ////////        if (Convert.ToDouble(dr["NormH"].ToString()) > hMax)
        ////////        {
        ////////            //ed.WriteMessage("*********7={0}\n", dr["NormH"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["IceH"].ToString()) > hMax)
        ////////        {
        ////////            // ed.WriteMessage("*********8={0}\n", dr["IceH"].ToString());

        ////////            result = false;
        ////////        }

        ////////        if (Convert.ToDouble(dr["WindH"].ToString()) > hMax)
        ////////        {
        ////////            // ed.WriteMessage("*******9={0}\n", dr["WindH"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["MaxTempH"].ToString()) > hMax)
        ////////        {
        ////////            //  ed.WriteMessage("***********10={0}\n", dr["MaxTempH"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["MinTempH"].ToString()) > hMax)
        ////////        {
        ////////            //ed.WriteMessage("********11={0}\n", dr["MinTempH"].ToString());

        ////////            result = false;
        ////////        }


        ////////        if (Convert.ToDouble(dr["WindAndIceH"].ToString()) > hMax)
        ////////        {
        ////////            //ed.WriteMessage("*********12={0}\n", dr["WindAndIceH"].ToString());

        ////////            result = false;
        ////////        }
        ////////        #endregion

        ////////    }
        ////////    ed.WriteMessage("$$$$$$$$$$$$Result ={0}\n", result);
        ////////    return result;
        ////////}




        public void CalcSagTension02(double se, double UTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("&&&&Start CalcSagTension\n");
            ////ed.WriteMessage("dtConductorSection.Count= {0}\n",DtconductorSection.Rows.Count);
            ////ed.WriteMessage("dtconductorSection.Rows[0][ProductCode]= " + dtconductorSection.Rows[0]["ProductCode"].ToString()+"\n");
            //Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));

            //ed.WriteMessage("MyBranch.ProductCode1= " + myBranch.ProductCode.ToString() + "\n");
            ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(.ProductCode);
            ////ed.WriteMessage("ConducTor.Code= "+conductor.Code.ToString()+"\n");

            double BaseH = ComputeHC02(UTS);
            //ed.WriteMessage("BaseH= " + BaseH.ToString() + "\n");
            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            Atend.Base.Design.DWeather WeatherBase;
            Atend.Base.Design.DWeather WeatherSecond;
            double normalH;


            //double se = ComputeSE();
            double sc = computesc02(BaseH);
            //ed.WriteMessage("sc={0},Se={1}\n", sc.ToString(), se.ToString());
            if (sc == -1)
            {
                //ed.WriteMessage("Sc=-1\n");
                WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
            }
            else
            {
                if (se > sc)
                {
                    //ed.WriteMessage("Se>SC\n");
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3, aConnection);//باد زیاد بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
                else
                {
                    //ed.WriteMessage("Se<=SC\n");
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2, aConnection);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
            }
            WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1, aConnection);//شرط آب و هوایی معمول شرط ثانویه می باشد
            //ed.WriteMessage("weatherBase={0},WeatherSecondCode={1}\n", WeatherBase.Code, WeatherSecond.Code);
            normalH = general.ComputeTension(WeatherBase, WeatherSecond, BaseH, se);
            h[0] = normalH;
            f[0] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[0]);
            //ed.WriteMessage("H[0]= " + h[0].ToString() + "   F[0]= " + f[0].ToString() + "\n");
            WeatherBase = WeatherSecond;


            int i = 1;
            do
            {

                WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                //ed.WriteMessage("*weatherSecond.Code={0},i={1},ConditionCode={2},Temp={3}\n", WeatherSecond.Code, i, WeatherSecond.ConditionCode, WeatherSecond.Temp);

                h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[0], se);
                f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[i]);
                i++;
            } while (i < 6);
            //ed.WriteMessage("FiniSh\n");
            //for (i = 0; i < 6; i++)
            //{
            //    ed.WriteMessage("i = " + i.ToString() + "= " + h[i].ToString() + "\n");
            //}

        }
        //MOUSAVI->autopoleinstallation
        public bool CalSagTension02(Atend.Base.Design.DBranch MyBranch, double se, Atend.Base.Equipment.EConsol Consol, Atend.Base.Equipment.EClamp Calamp, Atend.Base.Equipment.EPole Pole, double Clearance, double TrustBorder, double UTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            dtStTable.Rows.Clear();

            //ed.WriteMessage("SPANLENGTH:{0}\n", se);
            //ed.WriteMessage("~~~~~~~~~~Start SE={0}\n", se.ToString());
            //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                //ed.WriteMessage("***Conductor\n");
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode, aConnection);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    //ed.WriteMessage("Myconductor.Name={0},CrossEctionArea={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(), Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea.ToString());
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2); ;
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";
                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";

                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                #region SelfKeeper
                //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
                Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(MyBranch.ProductCode);
                //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
                if (MySelfKeeperTip.PhaseCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "0";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["ConductorCode"] = "1";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                    CalcSagTension02(se, UTS);
                    DataRow dr = dtStTable.NewRow();
                    dr["conductorCode"] = "2";
                    dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
                    dr["NormH"] = Math.Round(h[0], 2);
                    dr["NormF"] = Math.Round(f[0], 2);
                    dr["IceH"] = Math.Round(h[1], 2);
                    dr["IceF"] = Math.Round(f[1], 2);
                    dr["WindH"] = Math.Round(h[2], 2);
                    dr["WindF"] = Math.Round(f[2], 2);
                    dr["MaxTempH"] = Math.Round(h[3], 2);
                    dr["MAxTempF"] = Math.Round(f[3], 2);
                    dr["MinTempH"] = Math.Round(h[4], 2);
                    dr["MinTempF"] = Math.Round(f[4], 2);
                    dr["WindAndIceH"] = Math.Round(h[5], 2);
                    dr["WindAndIceF"] = Math.Round(f[5], 2);
                    dr["SectionCode"] = "0";

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            double hMax = ComputeHC02(UTS);
            double fMax = FindBestFMAx02(Pole, Consol, Calamp, clearance, TrustBorder, MyBranch);

            //foreach (DataRow dr in dtStTable.Rows)
            //{
            //    ed.WriteMessage("**NORMF={0}*NormH={1}\n", dr["NormF"].ToString(), dr["NormH"].ToString());
            //    ed.WriteMessage("**IceF={0}*IceH={1}\n", dr["IceF"].ToString(), dr["IceH"].ToString());
            //    ed.WriteMessage("**WindF={0}*WindH={1}\n", dr["WindF"].ToString(), dr["WindH"].ToString());
            //    ed.WriteMessage("**MaxTempFF={0}*MaxTempH={1}\n", dr["MaxTempF"].ToString(), dr["MaxTempH"].ToString());

            //    ed.WriteMessage("**MinTempF={0}*MinTempH={1}\n", dr["MinTempF"].ToString(), dr["MinTempH"].ToString());
            //    ed.WriteMessage("**WindAndIceF={0}*WindAndIceH={1}\n", dr["WindAndIceF"].ToString(), dr["WindAndIceH"].ToString());

            //}
            //ed.WriteMessage("fMax={0},hMax={1}\n", fMax, hMax);
            return IsResultOK02(fMax, hMax);
        }
        public double computesc02(double hc)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            Atend.Base.Design.DWeather weatherWind = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3, aConnection);//باد زیاد
            //ed.WriteMessage("WeatherWind.IceDiagonal=" + weatherWind.IceDiagonal.ToString() + "weatherWind.Temp= " + weatherWind.Temp + "\n");
            Atend.Base.Design.DWeather weatherIce = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2, aConnection);//یخ سنگین
            //ed.WriteMessage("WeatherICE.IceDiagonal= " + weatherIce.Temp.ToString() + "weatherIce.IceDiagonal= " + weatherIce.IceDiagonal + "\n");

            double iceweight = 913 * Math.PI * weatherIce.IceDiagonal * (weatherIce.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;//wic=913*3.14*i[i+d]*1e-6
            double WwForIce = Math.Pow(weatherIce.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherIce.IceDiagonal) * 1e-3;
            double WTotalForIce = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + iceweight, 2) + Math.Pow(WwForIce, 2));
            //ed.WriteMessage("WTotalForIce={0},iceWeight={1},WwForIce={2},WC={3}\n ", WTotalForIce.ToString(), iceweight, WwForIce, Atend.Global.Calculation.Mechanical.CCommon.WC);

            double windweight = Math.Pow(weatherWind.WindSpeed, 2) / 16 * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weatherWind.IceDiagonal) * 1e-3;//ww=v^2/16*(d+2i)*10^-3
            double WiForWind = 913 * Math.PI * weatherWind.IceDiagonal * (weatherWind.IceDiagonal + Atend.Global.Calculation.Mechanical.CCommon.Diagonal) * 1e-6;
            double wTotalForWind = Math.Sqrt(Math.Pow(Atend.Global.Calculation.Mechanical.CCommon.WC + WiForWind, 2) + Math.Pow(windweight, 2));

            //ed.WriteMessage("wTotalForWind ={0},windWeight={1},WiForWind={2}\n", wTotalForWind.ToString(), windweight, WiForWind);
            //double hc = ComputeHC();
            //ed.WriteMessage("HC= " + hc + "\n");
            //ed.WriteMessage("Conductor.Alpha= " + conductor.Alpha + "\n");
            //if (WTotalForIce > wTotalForWind)
            //{
            //    //ed.WriteMessage("WTotalForIce > wTotalForWind\n");
            //    return -1;
            //}
            //else
            //{
            double sc = (24 * Math.Pow(hc, 2) * Atend.Global.Calculation.Mechanical.CCommon.Alpha * (weatherWind.Temp - weatherIce.Temp)) / (Math.Pow(wTotalForWind, 2) - Math.Pow(WTotalForIce, 2));
            sc = Math.Sqrt(Math.Abs(sc));
            //ed.WriteMessage("***SC= " + sc.ToString() + "\n");
            return sc;
            //}
        }
        public double ComputeHC02(double UTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double HC = (UTS / 100) * Atend.Global.Calculation.Mechanical.CCommon.UTS;
            //ed.WriteMessage("HC={0}\n", HC);
            return HC;
        }
        public double FindBestFMAx02(Atend.Base.Equipment.EPole pole, Atend.Base.Equipment.EConsol Consol, Atend.Base.Equipment.EClamp Calamp, double Clearance, double TrusthBorder, Atend.Base.Design.DBranch MyBranch)
        {
            //ed.WriteMessage("CrossSectionArea={0}\n", Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
            Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea, aConnection);
            //ed.WriteMessage("PoleHeight= " + pole.Height.ToString() + "\n");
            double Depth = .1 * pole.Height + .6;
            double ke = 1;

            //ed.WriteMessage("Ke= " + ke.ToString() + "\n");
            //ed.WriteMessage("Ke= "+ke.ToString()+"\n");
            double fmax1 = 0;
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
            {
                //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
                fmax1 = pole.Height - (Depth + Calamp.DistanceSupport + clearance + TrusthBorder);
                //ed.WriteMessage("FMAX!={0}\n",fmax1);
            }

            //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
            double fmax2 = 0;
            double FMAX = fmax1;
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                double Vn = Volt / 1000;
                if (cke != null)
                {
                    switch (Consol.Type)
                    {
                        case 0: { ke = cke.Vertical; break; }
                        case 1: { ke = cke.Triangle; break; }
                        case 2: { ke = cke.Horizental; break; }


                    }

                }
                //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n",Depth,LenghtChain,consol.DistanceCrossArm,Clearance,ZaribEtminan);
                //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
                fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance + TrusthBorder);
                //ed.WriteMessage("fMAx1={0}\n",fmax1);
                //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
                fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
                //ed.WriteMessage("**********fMax1={0},fMax2={1}\n", fmax1, fmax2);
                if (fmax1 < fmax2)
                    FMAX = fmax1;
                else
                    FMAX = fmax2;
            }

            return FMAX;
        }
        public bool IsResultOK02(double FMax, double hMax)
        {
            bool result = true;
            //foreach (DataRow dr1 in dtStTable.Rows)
            //{
            //    ed.WriteMessage("NormF={0},IceF={1},WindF={2},MaxTempF={3},MinTempF={4},WindAndIceF={4}\n", dr1["NormF"].ToString(), dr1["IceF"].ToString(), dr1["WindF"].ToString(), dr1["MaxTempF"].ToString(), dr1["MinTempF"].ToString(), dr1["WindAndIceF"].ToString());
            //    ed.WriteMessage("NormH={0},IceH={1},WindH={2},MaxTempH={3},MinTempH={4},WindAndIceH={4}\n", dr1["NormH"].ToString(), dr1["IceH"].ToString(), dr1["WindH"].ToString(), dr1["MaxTempH"].ToString(), dr1["MinTempH"].ToString(), dr1["WindAndIceH"].ToString());
            //    ed.WriteMessage("~~~~~~~~~~~~~\n");
            //}

            //ed.WriteMessage("######fMax={0},HMAX={1}\n", FMax, hMax);
            foreach (DataRow dr in dtStTable.Rows)
            {

                #region Sag
                if (Convert.ToDouble(dr["NormF"].ToString()) > FMax)
                {
                    //ed.WriteMessage("*******1={0}\n", dr["NormF"].ToString());
                    result = false;
                }


                if (Convert.ToDouble(dr["IceF"].ToString()) > FMax)
                {
                    //ed.WriteMessage("*********2={0}\n", dr["IceF"].ToString());

                    result = false;
                }

                if (Convert.ToDouble(dr["WindF"].ToString()) > FMax)
                {
                    //ed.WriteMessage("**********3={0}\n", dr["WindF"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["MaxTempF"].ToString()) > FMax)
                {
                    //ed.WriteMessage("*********4={0}\n", dr["MaxTempF"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["MinTempF"].ToString()) > FMax)
                {
                    // ed.WriteMessage("**********5={0}\n", dr["MinTempF"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["WindAndIceF"].ToString()) > FMax)
                {
                    // ed.WriteMessage("**********6={0}\n", dr["WindAndIceF"].ToString());

                    result = false;
                }

                #endregion

                #region Tension
                if (Convert.ToDouble(dr["NormH"].ToString()) > hMax)
                {
                    //ed.WriteMessage("*********7={0}\n", dr["NormH"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["IceH"].ToString()) > hMax)
                {
                    // ed.WriteMessage("*********8={0}\n", dr["IceH"].ToString());

                    result = false;
                }

                if (Convert.ToDouble(dr["WindH"].ToString()) > hMax)
                {
                    // ed.WriteMessage("*******9={0}\n", dr["WindH"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["MaxTempH"].ToString()) > hMax)
                {
                    //  ed.WriteMessage("***********10={0}\n", dr["MaxTempH"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["MinTempH"].ToString()) > hMax)
                {
                    //ed.WriteMessage("********11={0}\n", dr["MinTempH"].ToString());

                    result = false;
                }


                if (Convert.ToDouble(dr["WindAndIceH"].ToString()) > hMax)
                {
                    //ed.WriteMessage("*********12={0}\n", dr["WindAndIceH"].ToString());

                    result = false;
                }
                #endregion

            }
            ed.WriteMessage("$$$$$$$$$$$$Result ={0}\n", result);
            return result;
        }
    }
}

