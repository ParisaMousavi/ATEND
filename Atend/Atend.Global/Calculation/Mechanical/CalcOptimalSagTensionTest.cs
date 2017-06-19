using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Global.Calculation.Mechanical
{
    public class CalcOptimalSagTensionTest
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

        public CalcOptimalSagTensionTest()
        {
            dtStTable = new DataTable();
          
            DataColumn NormH = new DataColumn("NormH");
            DataColumn IceH = new DataColumn("IceH");
            DataColumn WindH = new DataColumn("WindH");
            DataColumn MaxTempH = new DataColumn("MaxTempH");
            DataColumn MinH = new DataColumn("MinTempH");
            DataColumn WindAndIceH = new DataColumn("WindAndIceH");

            DataColumn NormF = new DataColumn("NormF");
            DataColumn IceF = new DataColumn("IceF");
            DataColumn WindF = new DataColumn("WindF");
            DataColumn MaxTempF = new DataColumn("MaxTempF");
            DataColumn MinF = new DataColumn("MinTempF");
            DataColumn WindAndIceF = new DataColumn("WindAndIceF");

            DataColumn SectionCode = new DataColumn("SectionCode");
            DataColumn conductorCode = new DataColumn("ConductorCode");
            DataColumn ConductorName = new DataColumn("ConductorName");

          
            dtStTable.Columns.Add(NormH);
            dtStTable.Columns.Add(IceH);
            dtStTable.Columns.Add(WindH);
            dtStTable.Columns.Add(MaxTempH);
            dtStTable.Columns.Add(MinH);
            dtStTable.Columns.Add(WindAndIceH);

            dtStTable.Columns.Add(NormF);
            dtStTable.Columns.Add(IceF);
            dtStTable.Columns.Add(WindF);
            dtStTable.Columns.Add(MaxTempF);
            dtStTable.Columns.Add(MinF);
            dtStTable.Columns.Add(WindAndIceF);

            dtStTable.Columns.Add(SectionCode);
            dtStTable.Columns.Add(conductorCode);
            dtStTable.Columns.Add(ConductorName);
        }

        public double ComputeHC()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double HC = (VTS / 100) * Atend.Global.Calculation.Mechanical.CCommon.UTS;
            return HC;
        }
        //محاسبه اسپن بحرانی
        public double computesc()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
           
            Atend.Base.Design.DWeather weatherWind = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد
            //ed.WriteMessage("WeatherWind.IceDiagonal=" + weatherWind.IceDiagonal.ToString() + "weatherWind.Temp= " + weatherWind.Temp + "\n");
            Atend.Base.Design.DWeather weatherIce = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین
            //ed.WriteMessage("WeatherICE.IceDiagonal= " + weatherIce.Temp.ToString() + "weatherIce.IceDiagonal= " + weatherIce.IceDiagonal + "\n");

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
            //ed.WriteMessage("Conductor.Alpha= " + conductor.Alpha + "\n");
            if (WTotalForIce > wTotalForWind)
            {
                //ed.WriteMessage("WTotalForIce > wTotalForWind\n");
                return -1;
            }
            else
            {
                double sc = (24 * Math.Pow(hc, 2) * Atend.Global.Calculation.Mechanical.CCommon.Alpha * (weatherWind.Temp - weatherIce.Temp)) / (Math.Pow(wTotalForWind, 2) - Math.Pow(WTotalForIce, 2));
                sc = Math.Sqrt(sc);
                //ed.WriteMessage("SC= " + sc.ToString() + "\n");
                return sc;
            }
        }
        //محاسبه اسپن معادل
        public double ComputeSE()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double s1 = 0;
            double s2 = 0;
            //ed.WriteMessage("I Am In Compute SE\n");
            //ed.WriteMessage("dtConductorSection.Count= "+DtconductorSection.Rows.Count.ToString()+"\n");
            foreach (DataRow dr in dtconductorSection.Rows)
            {

                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode((Guid)(dr["ProductCode"]));
                //ed.WriteMessage("MyBranch.Code= "+myBranch.Code.ToString()+"MyBranch.Lenght= "+myBranch.Lenght.ToString()+"\n");
                s1 += Math.Pow(myBranch.Lenght, 3);
                s2 += myBranch.Lenght;
            }
            double se = Math.Sqrt(s1 / s2);
            //ed.WriteMessage("SE= " + se.ToString() + "\n");
            return se;
        }

        public void CalcSagTension()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ////ed.WriteMessage("Start CalcSagTension\n");
            ////ed.WriteMessage("dtConductorSection.Count= {0}\n",DtconductorSection.Rows.Count);
            ////ed.WriteMessage("dtconductorSection.Rows[0][ProductCode]= " + dtconductorSection.Rows[0]["ProductCode"].ToString()+"\n");
            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));

            //ed.WriteMessage("MyBranch.ProductCode1= " + myBranch.ProductCode.ToString() + "\n");
            ////Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.SelectByCode(.ProductCode);
            ////ed.WriteMessage("ConducTor.Code= "+conductor.Code.ToString()+"\n");

            double BaseH = ComputeHC();
            //ed.WriteMessage("BaseH= " + BaseH.ToString() + "\n");
            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            Atend.Base.Design.DWeather WeatherBase;
            Atend.Base.Design.DWeather WeatherSecond;
            double normalH;


            double se = ComputeSE();
            double sc = computesc();
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
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 3);//باد زیاد بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
                else
                {
                    //ed.WriteMessage("Se<=SC\n");
                    WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 2);//یخ سنگین بدترین شرط است و شرط اولیه جهت شروع محاسبات می باشد
                }
            }
            WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی معمول شرط ثانویه می باشد
            //ed.WriteMessage("weatherBase={0},WeatherSecondCode={1}\n",WeatherBase.Code,WeatherSecond.Code);
            normalH = general.ComputeTension(WeatherBase, WeatherSecond, BaseH, se);
            h[0] = normalH;
            f[0] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[0]);
            //ed.WriteMessage("H[0]= " + h[0].ToString() + "   F[0]= " + f[0].ToString() + "\n");
            WeatherBase = WeatherSecond;


            int i = 1;
            do
            {

                WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
                //ed.WriteMessage("*weatherSecond.Code={0},i={1},ConditionCode={2},Temp={3}\n", WeatherSecond.Code, i, WeatherSecond.ConditionCode, WeatherSecond.Temp);

                h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[0], se);
                f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(se, 2))) / (8 * h[i]);
                i++;
            } while (i < 6);
            //for (i = 0; i < 6; i++)
            //{
            //    ed.WriteMessage("i = " + i.ToString() + "= " + h[i].ToString() + "\n");
            //}

        }

        public DataTable CalSagTension(Atend.Base.Design.DBranch MyBranch)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            //ed.WriteMessage("Start CalcMyBranch.ProductCode={0}\n", MyBranch.ProductType);
            //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(dtconductorSection.Rows[0]["ProductCode"].ToString()));
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                ed.WriteMessage("***Conductor\n");
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(MyBranch.ProductCode);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                    //ed.WriteMessage("Myconductor.Code={0}\n", MyConducyorTip.Code);
                    CalcSagTension();
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
                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                    CalcSagTension();
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

                    dtStTable.Rows.Add(dr);
                }
                if (MyConducyorTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                    CalcSagTension();
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
                    CalcSagTension();
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

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                    CalcSagTension();
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

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                    CalcSagTension();
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

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            //IsSagOk();

            return dtStTable;
        }

        public void IsSagOk()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(DtPoleSection.Rows[0]["ProductCode"]));
            Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode((Guid)(DtPoleSection.Rows[0]["ProductCode"]));
            Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
            switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
            {
                case Atend.Control.Enum.ProductType.Pole:
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
                    break;
                case Atend.Control.Enum.ProductType.PoleTip:
                    Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
                    break;
            }

            DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            DataTable dtPackageForInsulator = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtPackage.Rows[0]["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));

            Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));
            Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(dtPackageForInsulator.Rows[0]["ProductCode"]));
            //ed.WriteMessage("Insulator.Code="+insulator.Code+"\n");

            double Depth = 0.1 * Convert.ToDouble(pole.Height) + 0.6;
            //ed.WriteMessage("Pole.Height= " + pole.Height + " Depth= " + Depth + " Insulator.LenghtChain= " + insulator.LenghtInsulatorChain + " DistanceCrossArm= " + consol.DistanceCrossArm + " Clearance= " + Clearance + "\n");
            double Fmax = pole.Height - (Depth + .45/*insulator.LenghtInsulatorChain */+ consol.DistanceCrossArm + Clearance);
            ed.WriteMessage("حداکثر فلش قابل قبول= " + Fmax.ToString() + "\n");
            foreach (DataRow dr in dtStTable.Rows)
            {
                if (Convert.ToDouble(dr["NormF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی نرمال از حد مجاز بیشتر می شود\n");
                }


                if (Convert.ToDouble(dr["IceF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی یخ سنگین از حد مجاز بیشتر می شود\n");
                }

                if (Convert.ToDouble(dr["WindF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی باد زیاد از حد مجاز بیشتر می شود\n");
                }


                if (Convert.ToDouble(dr["MaxTempF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی حداکثر دما از حد مجاز بیشتر می شود\n");
                }


                if (Convert.ToDouble(dr["MinTempF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی حداقل دما از حد مجاز بیشتر می شود\n");
                }


                if (Convert.ToDouble(dr["WindAndIceF"].ToString()) > Fmax)
                {
                    ed.WriteMessage("فلش در شرط آب و هوایی باد و یخ از حد مجاز بیشتر می شود\n");
                }
            }


        }

        public double CalcMaxFlash()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double FMax = f[0];
            for (int i = 1; i < 6; i++)
            {
                if (f[i] > FMax)
                    FMax = f[i];
            }
            return FMax;
        }

        public void MinPc()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtBest = new DataTable();
            DataColumn dc = new DataColumn("CrossSectionArea");
            dtBest.Columns.Add(dc);
            DataColumn dc1 = new DataColumn("Type");
            dtBest.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("TypeName");
            dtBest.Columns.Add(dc2);


            double ke = 0;
            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode((Guid)(dtconductorSection.Rows[0]["ProductCode"]));
            Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(myBranch.ProductCode);

            Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(DtPoleSection.Rows[0]["ProductCode"]));
            DataTable dtdConsol = Atend.Base.Design.DConsol.AccessSelectByParentCode(myNode.Code);
            Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(dtdConsol.Rows[0]["ProductCode"]));

            Atend.Base.Design.DPoleInfo myPoleInfo = Atend.Base.Design.DPoleInfo.AccessSelectByNodeCode(myNode.Code);

            Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(conductor.CrossSectionArea);
            switch (eConsol.Type)
            {
                case 0: { ke = cke.Vertical; break; }
                case 1: { ke = cke.Triangle; break; }
                case 2: { ke = cke.Horizental; break; }

            }
            double pc = (ke * Math.Sqrt(CalcMaxFlash() * LenghtChain)) + (volt / 150);
            ed.WriteMessage(string.Format("حداقل فاصله فازی مورد نیاز{0}می باشد", pc.ToString()));
            DataTable dtconsol = Atend.Base.Equipment.EConsol.SelectLessPc(pc);


        }

        public DataTable CalcTempTable()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            if (End < Start)
            {
                //ed.WriteMessage("ExchangeValue\n");
                int t = End;
                End = Start;
                Start = t;
            }
            Atend.Base.Design.DWeather Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی نرمال 
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
                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["ProductCode"].ToString()));
                //ed.WriteMessage("MyBranch.ProductCode= " + myBranch.ProductCode.ToString() + "\n");
                if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {
                    Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode);
                    //ed.WriteMessage("**");
                    DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));

                    Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node1Guid"].ToString()));
                    Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtNode[0]["Node2Guid"].ToString()));
                    if (ConductorTip.PhaseCount > 0)
                    {
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
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
                        dtTempTable.Rows.Add(NewRow);
                    }
                    if (ConductorTip.NeutralCount > 0)
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
                        DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));
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

                    if (ConductorTip.NightCount > 0)
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
                else
                {

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
            return dtTempTable;
        }

        public double WindOnPoleF(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather weather, DataTable dtConductor)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataRow[] dr = dtConductor.Select(string.Format(" NodeType='{0}'", Atend.Control.Enum.ProductType.Consol));

            //ed.WriteMessage("Start WindOnPoleF\n");
            double L = pole.Height - (0.1 * pole.Height + .6);
            double AW = (L * (pole.ButtomCrossSectionArea + pole.TopCrossSectionArea)) / 2;
            double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
            double h1;
            if (dr.Length != 0)
            {
                Atend.Base.Design.DConsol dConsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
                Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dConsol.ProductCode);
                h1 = L - consol.DistanceCrossArm;

            }
            else
            {
                Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
                Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
                h1 = L - Clamp.DistanceSupport;
            }



            double KFactor = .0812;
            if (pole.Shape == 0)
            {
                KFactor = .05;
            }
            //ed.WriteMessage("BCrossSection= " + pole.ButtomCrossSectionArea + "TCrossSectionArea= " + pole.TopCrossSectionArea + " Distance=" + consol.DistanceCrossArm + "K=" + KFactor + "\n");
            //ed.WriteMessage("L=" + L + " Aw" + AW + " h=" + h + "H1="+h1+" weather=" + weather.Code + "\n");
            double Result = (h / h1) * KFactor * AW * Math.Pow(weather.WindSpeed, 2);
            //ed.WriteMessage("PoleF= "+Result+"\n");
            return Result;
        }

        public double WindOnInsulator(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather Weather, int CounOfCrossArm)
        {
            //ed.WriteMessage("CountOfCross= "+CounOfCrossArm+" InsulatorDiameter= "+InsulatorDiamiter+" Shape= "+InsulatorShapeFactor+" LenghtChain= "+LenghtChain+"\n");
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnInsulator\n");
            return 3 * CounOfCrossArm * LenghtChain * InsulatorDiamiter * InsulatorShapeFactor * Math.Pow(Weather.WindSpeed, 2) / 16 * 1e-3;
        }

        public double WindOnConductor(DataTable dtConductor, Atend.Base.Design.DWeather weather)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnConductor\n");
            double sum = 0;
            //ed.WriteMessage("I Am In WindOnConductor\n");
            foreach (DataRow dr in dtConductor.Rows)
            {
                Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["BranchGuid"].ToString()));
                if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {
                    Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(Branch.ProductCode);
                    if (conductorTip.PhaseCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
                        //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
                        sum += conductorTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (conductorTip.NightCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
                        sum += conductorTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (conductorTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
                        sum += conductorTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                }
                else
                {
                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(Branch.ProductCode);
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

            //ed.WriteMessage("Sum ="+sum.ToString()+"\n");
            return sum;
        }

        public double WindOnTangantPole(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("WindOnTangantPole\n");
            Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
            CVector SumTension = new CVector();


            SumTension.Absolute = 0;
            SumTension.Angle = 0;

            foreach (DataRow dr in dtConductor.Rows)
            {
                if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(dr["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Dconsol.ProductCode);
                    if (Econsol.ConsolType == 2 || Econsol.ConsolType == 3)
                    {
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        //ed.WriteMessage("It Is Tangant Consol\n");
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
                        if (drSec.Length == 0)
                        {
                            DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                            sectionCode = Section.SectionCode;
                            CalSagTension(myBranch);
                        }
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode);
                        //ed.WriteMessage("SwichTAngant\n");
                        if (conductorTip.PhaseCount > 0)
                        {
                            int ConductorCode = 0;

                            //ed.WriteMessage("$$$pp");
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());
                            //ed.WriteMessage("NormH= "+dr1[0]["NormH"].ToString()+"Angle= "+dr["Angle"].ToString()+"\n");
                           

                            Tension.Absolute *= conductorTip.PhaseCount;

                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
                        {

                            int ConductorCode = 1;
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());


                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            int ConductorCode = 2;
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

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
                        Atend.Base.Design.DPackage Dpack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dr["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(Dpack.ProductCode);
                        //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                        if (ECalamp.Type == 5)
                        {
                            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                            //ed.WriteMessage("It Is Tangant Consol\n");
                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(dr["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                                sectionCode = Section.SectionCode;
                                CalSagTension(myBranch);
                            }

                            //CalSagTension(myBranch);
                            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(myBranch.ProductCode);
                            if (SelfKeeperTip.PhaseCount > 0)
                            {
                                int ConductorCode = 0;

                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;

                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }


                            if (SelfKeeperTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                            if (SelfKeeperTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                        }
                    }
                }
            }

            //ed.WriteMessage("Tension.Absolute= "+SumTension.Absolute+"\n");
            return SumTension.Absolute;

        }

        public double WindOnDDEPole(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        {
            //ed.WriteMessage("I AM In WindOnDDEPole\n");
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnDDEPole\n");
            Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
            CVector SumTension = new CVector();
            CVector MaxTension = new CVector();
            MaxTension.Absolute = 0;
            MaxTension.Angle = 0;
            SumTension.Absolute = 0;
            SumTension.Angle = 0;
            //****************X1=a+b+c
            //ed.WriteMessage("X1=a+b+c\n");
            foreach (DataRow drConductor in dtConductor.Rows)
            {
                if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(drConductor["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Dconsol.ProductCode);
                    //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                    if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
                    {
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drConductor["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                        if (drSec.Length == 0)
                        {
                            DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                            sectionCode = Section.SectionCode;
                            CalSagTension(myBranch);
                        }
                        //CalSagTension(myBranch);
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
                        {

                            int ConductorCode = 0;

                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
                        {

                            int ConductorCode = 1;

                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            int ConductorCode = 2;

                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

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
                            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drConductor["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                                sectionCode = Section.SectionCode;
                                CalSagTension(myBranch);
                            }
                            //CalSagTension(myBranch);
                            Atend.Base.Equipment.ESelfKeeperTip SelfKeepeTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(myBranch.ProductCode);
                            if (SelfKeepeTip.PhaseCount > 0)
                            {

                                int ConductorCode = 0;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

                                SumTension = SumTension.Add(SumTension, Tension);
                            }


                            if (SelfKeepeTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                            if (SelfKeepeTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeepeTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                SumTension = SumTension.Add(SumTension, Tension);
                            }
                        }
                    }
                }

            }
            //ed.WriteMessage("sumTension="+SumTension.Absolute+"\n");
            //****************************
            //ed.WriteMessage("Second\n");
            foreach (DataRow drConductor in dtConductor.Rows)
            {
                if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(drConductor["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Dconsol.ProductCode);
                    if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
                    {
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drConductor["BranchGuid"].ToString()));

                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
                        {

                            int ConductorCode = 0;
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                        }


                        if (conductorTip.NightCount > 0)
                        {

                            int ConductorCode = 1;
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode,SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                        }
                        if (conductorTip.NeutralCount > 0)
                        {

                            int ConductorCode = 2;
                            DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, SectionCode.ToString()));
                            Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                        }
                    }

                }
                else
                {
                    if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
                    {
                        Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(drConductor["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp eCalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
                        if (eCalamp.Type == 0)
                        {
                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(drConductor["BranchGuid"].ToString()));

                            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(myBranch.ProductCode);
                            if (SelfKeeperTip.PhaseCount > 0)
                            {

                                int ConductorCode = 0;

                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            }


                            if (SelfKeeperTip.NightCount > 0)
                            {

                                int ConductorCode = 1;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, SectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0][i].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            }
                            if (SelfKeeperTip.NeutralCount > 0)
                            {

                                int ConductorCode = 2;
                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());

                                Tension.Absolute *= SelfKeeperTip.PhaseCount;
                                Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                                MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            }
                        }
                    }

                }
            }




            //ed.WriteMessage("MaxTension.Absolute= "+MaxTension.Absolute+"\n");
            return MaxTension.Absolute;


        }

        public DataTable WindOnPole()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double sw = 0;
            double[] temp = new double[6];
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

            DataTable dtPoleConductor = new DataTable();
            dtPoleConductor.Columns.Add("NodeGuid");
            dtPoleConductor.Columns.Add("BranchGuid");
            dtPoleConductor.Columns.Add("Angle");
            dtPoleConductor.Columns.Add("BranchType");
            dtPoleConductor.Columns.Add("NodeType");

            foreach (DataRow dr in dtPoleSection.Rows)
            {
                sw = 0;

                Atend.Base.Design.DWeather Weather = new Atend.Base.Design.DWeather();

                //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(dr["ProductCode"]));
                Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode((Guid)(dr["ProductCode"]));
                Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole(); ;
                ed.WriteMessage("MyPackage.ProductType={0},ProductCode={1}\n",myPackage.Type,myPackage.ProductCode);
                switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
                {
                    case Atend.Control.Enum.ProductType.Pole:

                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
                        ed.WriteMessage("One Switch\n");
                        break;

                    case Atend.Control.Enum.ProductType.PoleTip:
                        Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
                        ed.WriteMessage("POleTip.Code={0}\n",poleTip.PoleCode);
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
                        ed.WriteMessage("PoleTip.Name={0}\n", poleTip.Name);

                        break;
                }
                ed.WriteMessage("Pole.Name={0}\n", pole.Name);


                DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                int countOfCrossArm = dtPackage.Rows.Count;

                double WindOnConductor1 = 0;
                double WindOnPole1 = 0;
                double WindOnInsulator1 = 0;
                double Tension = 0;
                double Total = 0;
                dtPoleConductor.Rows.Clear();
                DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
                //ed.WriteMessage("dtConductor={0}\n",dtConductor.Length);
                foreach (DataRow drConductor in dtConductor)
                {
                    DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
                    if (drs1.Length != 0)
                    {
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
                        DataRow NewRow = dtPoleConductor.NewRow();
                        NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
                        NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
                        NewRow["Angle"] = drs2[0]["Angle2"].ToString();
                        NewRow["BranchType"] = drs2[0]["Type"].ToString();
                        NewRow["NodeType"] = drConductor["NodeType"].ToString();

                        dtPoleConductor.Rows.Add(NewRow);

                    }


                }
                //ed.WriteMessage("dtPoleConductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
                //foreach (DataRow drf in dtPoleConductor.Rows)
                //{
                //    ed.WriteMessage("drf={0},NodeGuid={1}\n",drf["Angle"].ToString(),drf["NodeGuid"].ToString());
                //}

                //DataTable dtPoleConductor = Atend.Global.Acad.UAcad.GetPoleConductors(new Guid(dr["ProductCode"].ToString()));
                for (int i = 0; i < 6; i++)
                {
                    Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
                    //ed.WriteMessage("ProductCode= "+dr["ProductCode"].ToString()+"\n");

                    //ed.WriteMessage("dtPoleConductor.Rows.Count= " + dtPoleConductor.Length.ToString() + "\n");
                    WindOnConductor1 = WindOnConductor(dtPoleConductor, Weather);
                    if (countOfCrossArm != 0)
                    {
                        WindOnInsulator1 = WindOnInsulator(pole, Weather, countOfCrossArm);
                    }
                    //ed.WriteMessage("Insulator= " + WindOnInsulator1 + "\n");
                    WindOnPole1 = WindOnPoleF(pole, Weather, dtPoleConductor);
                    Tension = WindOnTangantPole(Weather, dtPoleConductor, i);
                    Tension += WindOnDDEPole(Weather, dtPoleConductor, i);
                    Total = WindOnConductor1 + WindOnInsulator1 + WindOnPole1 + Tension;
                    temp[i] = Total;
                    //ed.WriteMessage("i= " + i.ToString() + "\n");

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
                //ed.WriteMessage("Add Row\n");

            }
            //ed.WriteMessage("Finish Calculating\n");
            return dtTable;
        }

        public CVector Max(CVector a, CVector b)
        {
            if (a.Absolute > b.Absolute)
                return a;
            return b;
        }

    }

}

