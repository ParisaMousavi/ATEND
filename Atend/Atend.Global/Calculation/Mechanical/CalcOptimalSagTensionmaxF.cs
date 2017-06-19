using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

//get from tehran 7/15
namespace Atend.Global.Calculation.Mechanical
{
    public class CalcOptimalSagTensionmaxF
    {
        private double volt;

        public double Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        private DataTable dtPoleSection;

        public DataTable DtPoleSection
        {
            get { return dtPoleSection; }
            set { dtPoleSection = value; }
        }

        private DataTable dtConductorSection;

        public DataTable DtConductorSection
        {
            get { return dtConductorSection; }
            set { dtConductorSection = value; }
        }

        private double clearance;

        public double Clearance
        {
            get { return clearance; }
            set { clearance = value; }
        }
        double LenghtChain = 0;

        private int end;

        public int End
        {
            get { return end; }
            set { end = value; }
        }
        public DataTable dtStTable;
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        private int distance;

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private double zaribEtminan;

        public double ZaribEtminan
        {
            get { return zaribEtminan; }
            set { zaribEtminan = value; }
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

        private DataTable dtPoleCond;

        public DataTable DtPoleCond
        {
            get { return dtPoleCond; }
            set { dtPoleCond = value; }
        }

        public DataTable dtBranchList = new DataTable();
        double[] h = new double[10];
        double[] f = new double[10];

        double InsulatorDiamiter = .0266;
        double InsulatorShapeFactor = .5;
        Guid sectionCode;

        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


        private System.Data.DataTable dtPackageParam = new DataTable();
        private System.Data.DataTable dtConsolParam = new DataTable();
        private System.Data.DataTable dtCalampParam = new DataTable();
        private System.Data.DataTable dtBranchParam = new DataTable();
        private System.Data.DataTable dtConductorTipParam = new DataTable();
        private System.Data.DataTable dtSelfKeeperTipParam = new DataTable();
        private System.Data.DataTable dtDConsolParam = new DataTable();
        private System.Data.DataTable dtDPoleSectionParam = new DataTable();
        private System.Data.DataTable dtPoleParm = new DataTable();
        private System.Data.DataTable dtPoleTipParm = new DataTable();

        OleDbConnection aConnection = new OleDbConnection();

        public CalcOptimalSagTensionmaxF()
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

            DataColumn MaxF = new DataColumn("MaxF");

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

        //~CalcOptimalSagTensionmaxF()
        //{
        //    aConnection.Close();
        //}

        private void CreateConnection()
        {
            aConnection.ConnectionString = Atend.Control.ConnectionString.AccessCnString;
            try
            {
                //if (aConnection.State == ConnectionState.Closed)
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
        ////////public void CalcSagTension()
        ////////{
        ////////    double ke = 1;

        ////////    Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();

        ////////    Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(DtConductorSection.Rows[0]["ProductCode"]));

        ////////    Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
        ////////    Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////    switch ((Atend.Control.Enum.ProductType)myPackage.Type)
        ////////    {
        ////////        case Atend.Control.Enum.ProductType.Pole:
        ////////            pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
        ////////            break;
        ////////        case Atend.Control.Enum.ProductType.PoleTip:
        ////////            Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
        ////////            pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
        ////////            break;
        ////////    }


        ////////    DataTable dtPackageConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////    DataTable dtPackageCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

        ////////    double Depth = .1 * pole.Height + .6;



        ////////    double fmax1 = 0;
        ////////    if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
        ////////    {
        ////////        //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
        ////////        fmax1 = pole.Height - (Depth + calamp.DistanceSupport + clearance + ZaribEtminan);
        ////////        //ed.WriteMessage("FMAX!={0}\n",fmax1);
        ////////    }

        ////////    //ed.WriteMessage("Volt= " + Volt + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke=" + ke + "\n");
        ////////    double fmax2 = 0;
        ////////    double FMAX = fmax1;
        ////////    Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////    //ed.WriteMessage("ke={0},CrossSectionArea={1}\n", ke, Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////    if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        double Vn = Volt / 1000;
        ////////        if (cke != null)
        ////////        {
        ////////            switch (consol.Type)
        ////////            {
        ////////                case 0: { ke = cke.Vertical; break; }
        ////////                case 1: { ke = cke.Triangle; break; }
        ////////                case 2: { ke = cke.Horizental; break; }


        ////////            }

        ////////        }
        ////////        //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n", Depth, LenghtChain, consol.DistanceCrossArm, Clearance, ZaribEtminan);
        ////////        //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
        ////////        fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + consol.DistanceCrossArm + clearance + ZaribEtminan);
        ////////        //ed.WriteMessage("fMAx1={0}\n", fmax1);
        ////////        //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n", consol.DistancePhase, Vn, ke);
        ////////        fmax2 = (Math.Pow((consol.DistancePhase - (Vn / 150)) / ke, 2)) - .45 /*insulator.LenghtInsulatorChain*/;
        ////////        // ed.WriteMessage("fMax2={0}\n", fmax2);
        ////////        if (fmax1 < fmax2)
        ////////            FMAX = fmax1;
        ////////        else
        ////////            FMAX = fmax2;
        ////////    }

        ////////    //ed.WriteMessage("FMAx={0}\n", FMAX);


        ////////    //MaxSpan
        ////////    double MaxSpan = ComputeMaxSpan();

        ////////    double SE = ComputeSE();


        ////////    //ed.WriteMessage("fmax1="+fmax1+"\n");
        ////////    //ed.WriteMessage("fmax2=" + fmax2 + "\n");
        ////////    //H & F In MaxTemp
        ////////    Atend.Base.Design.DWeather WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 4);//حداکثر دما
        ////////    //ed.WriteMessage("MaxSpan={0}\n", MaxSpan);
        ////////    h[3] = (general.ComputeTotalWeight(WeatherBase) * (Math.Pow(MaxSpan, 2))) / (8 * FMAX);
        ////////    f[3] = FMAX;
        ////////    //ed.WriteMessage("H[3]= " + h[3].ToString() + "\n");
        ////////    //ed.WriteMessage("F[3]= " + f[3].ToString() + "\n");
        ////////    //ed.WriteMessage("MyBranch>Lenght= " + myBranch.Lenght + "\n");
        ////////    //H & F In OtherCondition
        ////////    int i = 0;
        ////////    do
        ////////    {
        ////////        if (i != 3)
        ////////        {
        ////////            Atend.Base.Design.DWeather WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);

        ////////            h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[3], SE);
        ////////            //ed.WriteMessage("***H[i]={0},SE={1},i={2}\n", h[i].ToString(), SE.ToString(), i);
        ////////            f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(SE, 2))) / (8 * h[i]);
        ////////            //ed.WriteMessage("####F[i]={0},i{1}\n", f[i].ToString(), i);
        ////////        }
        ////////        i++;
        ////////    } while (i < 6);
        ////////}

        ////////public DataTable CalSagTension(Atend.Base.Design.DBranch MyBranch)
        ////////{
        ////////    //ed.WriteMessage("&&&&&&&&&&&&&&&&&&&&dtstTable={0}\n", dtStTable.Rows.Count);
        ////////    double fmax = IsSagOk();
        ////////    //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(DtConductorSection.Rows[0]["ProductCode"].ToString()));
        ////////    if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        #region Conductor
        ////////        Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, MyBranch.ProductCode);

        ////////        if (MyConducyorTip.PhaseCount > 0)
        ////////        {
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MyConductor.Name + "\n");
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);
        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }
        ////////    else
        ////////    {
        ////////        #region SelfKeeper
        ////////        Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, MyBranch.ProductCode);
        ////////        //ed.WriteMessage("MyConductorTip.NAme=" + MyConducyorTip.Name + "\n");
        ////////        if (MySelfKeeperTip.PhaseCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            CalcSagTension();
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
        ////////            dr["SectionCode"] = SectionCode.ToString();
        ////////            dr["MaxF"] = Math.Round(fmax, 2);

        ////////            dtStTable.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }

        ////////    return dtStTable;
        ////////}


        //////////public double getfMax()
        //////////{

        //////////    Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(DtPoleSection.Rows[0]["ProductCode"]));


        //////////    Atend.Base.Equipment.EPole pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myNode.ProductCode);

        //////////    Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCodeType(myNode.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));



        //////////    DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        //////////    DataTable dtPackageForInsulator = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtPackage.Rows[0]["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));


        //////////    Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(dtPackageForInsulator.Rows[0]["ProductCode"]));


        //////////    double Depth = 0.1 * Convert.ToDouble(pole.Height) + 0.6;
        //////////    //ed.WriteMessage("Clearance= " + Clearance + " zaribEtminan =" + ZaribEtminan + "\n");
        //////////    double Fmax = pole.Height - (Depth + insulator.LenghtInsulatorChain + consol.DistanceCrossArm + Clearance + ZaribEtminan);
        //////////    //ed.WriteMessage("Fmax= " + Fmax.ToString() + "\n");
        //////////    return Fmax;
        //////////}
        ////////public double ComputeSE()
        ////////{
        ////////    double s1 = 0;
        ////////    double s2 = 0;
        ////////    //ed.WriteMessage("I Am In Compute SE\n");
        ////////    //ed.WriteMessage("dtConductorSection.Count= " + DtConductorSection.Rows.Count.ToString() + "\n");
        ////////    foreach (DataRow dr in DtConductorSection.Rows)
        ////////    {

        ////////        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(dr["ProductCode"]));
        ////////        //ed.WriteMessage("MyBranch.Code= " + myBranch.Code.ToString() + "MyBranch.Lenght= " + myBranch.Lenght.ToString() + "\n");
        ////////        s1 += Math.Pow(myBranch.Lenght, 3);
        ////////        s2 += myBranch.Lenght;
        ////////    }
        ////////    double se = Math.Sqrt(s1 / s2);
        ////////    //ed.WriteMessage("SE= " + se.ToString() + "\n");
        ////////    return se;
        ////////}

        ////////public DataTable FindHMax()
        ////////{
        ////////    double[] hMax = new double[6];
        ////////    DataTable dtHMax = new DataTable();
        ////////    DataColumn dcHNorm = new DataColumn("NormH");
        ////////    DataColumn dcHIce = new DataColumn("IceH");
        ////////    DataColumn dcHWind = new DataColumn("WindH");
        ////////    DataColumn dcHMaxTemp = new DataColumn("MaxTempH");
        ////////    DataColumn dcHMinTemp = new DataColumn("MinTempH");
        ////////    DataColumn dchWindIce = new DataColumn("WindIceH");
        ////////    dtHMax.Columns.Add(dcHNorm);
        ////////    dtHMax.Columns.Add(dcHIce);
        ////////    dtHMax.Columns.Add(dcHWind);
        ////////    dtHMax.Columns.Add(dcHMaxTemp);
        ////////    dtHMax.Columns.Add(dcHMinTemp);
        ////////    dtHMax.Columns.Add(dchWindIce);
        ////////    double UTS;
        ////////    Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(DtConductorSection.Rows[0]["ProductCode"].ToString()));
        ////////    if (branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////    {
        ////////        #region Conductor
        ////////        Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(branch.ProductCode);

        ////////        if (MyConducyorTip.PhaseCount > 0)
        ////////        {
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));

        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////                ed.WriteMessage("HMAX={0},i={1}\n", hMax[i].ToString(), i.ToString());
        ////////            }
        ////////            DataRow dr = dtHMax.NewRow();
        ////////            //dr["ConductorCode"] = "0";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////            //dr["SectionCode"] = sectionCode;

        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////            }

        ////////            DataRow dr = dtHMax.NewRow();
        ////////            // dr["ConductorCode"] = "1";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////            //dr["SectionCode"] = sectionCode;


        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        if (MyConducyorTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.EConductor MyConductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(MyConducyorTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));

        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////            }
        ////////            DataRow dr = dtHMax.NewRow();
        ////////            //dr["conductorCode"] = "2";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////            //dr["SectionCode"] = sectionCode;


        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }
        ////////    else
        ////////    {
        ////////        //Atend.Base.Equipment.ESelfKeeperTip SelfTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
        ////////        //Atend.Base.Equipment.ESelfKeeper Self = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(SelfTip.PhaseProductCode);
        ////////        //UTS = Self.UTS;
        ////////        #region SelfKeeper
        ////////        //ed.WriteMessage("####SelfKeepere={0}\n", MyBranch.ProductCode);
        ////////        Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(branch.ProductCode);
        ////////        //ed.WriteMessage("MyConductorTip.NAme=" + MySelfKeeperTip.Name + "\n");
        ////////        if (MySelfKeeperTip.PhaseCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MySelfKeeperTip.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////            }
        ////////            DataRow dr = dtHMax.NewRow();
        ////////            //dr["ConductorCode"] = "0";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);



        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NightCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Night = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////            }
        ////////            DataRow dr = dtHMax.NewRow();
        ////////            //dr["ConductorCode"] = "1";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString(); ;
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////            //dr["SectionCode"] = sectionCode;


        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        if (MySelfKeeperTip.NeutralCount > 0)
        ////////        {
        ////////            //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
        ////////            //ed.WriteMessage("Myconductor.NAMae In The Neutral = " + MyConductor.Name + "\n");
        ////////            Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////            for (int i = 0; i < 6; i++)
        ////////            {
        ////////                Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////                hMax[i] = Atend.Global.Calculation.Mechanical.CCommon.UTS / weather.SaftyFactor;
        ////////            }
        ////////            DataRow dr = dtHMax.NewRow();
        ////////            //dr["conductorCode"] = "2";
        ////////            //dr["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name.ToString();
        ////////            dr["NormH"] = Math.Round(hMax[0], 2);

        ////////            dr["IceH"] = Math.Round(hMax[1], 2);

        ////////            dr["WindH"] = Math.Round(hMax[2], 2);

        ////////            dr["MaxTempH"] = Math.Round(hMax[3], 2);

        ////////            dr["MinTempH"] = Math.Round(hMax[4], 2);

        ////////            dr["WindIceH"] = Math.Round(hMax[5], 2);

        ////////            //dr["SectionCode"] = sectionCode;


        ////////            dtHMax.Rows.Add(dr);
        ////////        }
        ////////        #endregion
        ////////    }


        ////////    return dtHMax;

        ////////}

        ////////public double ComputeMaxSpan()
        ////////{
        ////////    double MaxSpan = 0;
        ////////    foreach (DataRow dr in dtConductorSection.Rows)
        ////////    {
        ////////        Atend.Base.Design.DBranch myBranch1 = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(dr["ProductCode"]));
        ////////        if (myBranch1.Lenght > MaxSpan)
        ////////            MaxSpan = myBranch1.Lenght;

        ////////    }
        ////////    return MaxSpan;
        ////////}

        ////////public DataTable CalcTempTable()
        ////////{
        ////////    // ed.WriteMessage("@@@@@@@@@@@@@@@Start CalcTempTable@@@@@@@@@@@@@@@@@@@@\n");
        ////////    if (end < start)
        ////////    {
        ////////        int t = end;
        ////////        end = start;
        ////////        start = t;
        ////////    }
        ////////    Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
        ////////    Atend.Base.Design.DWeather Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1);//شرط آب و هوایی نرمال 
        ////////    double hTempTable, fTempTable;
        ////////    DataTable dtTempTable = new DataTable();
        ////////    DataColumn dcColumn1 = new DataColumn("SpanLenght");
        ////////    DataColumn dcColumn2 = new DataColumn("From");
        ////////    DataColumn dcColumn3 = new DataColumn("To");
        ////////    DataColumn dcColumn4 = new DataColumn("ConductorName");

        ////////    dtTempTable.Columns.Add(dcColumn2);
        ////////    dtTempTable.Columns.Add(dcColumn3);
        ////////    dtTempTable.Columns.Add(dcColumn1);
        ////////    dtTempTable.Columns.Add(dcColumn4);
        ////////    //ed.WriteMessage("End= " + End.ToString() + "Start= " + Start.ToString() + "Distance =" + Distance.ToString() + "\n");
        ////////    for (int i = end; i > start; i = i - distance)
        ////////    {
        ////////        //ed.WriteMessage("I Am In The For to BuildColumn\n");
        ////////        DataColumn dtColumn = new DataColumn(i.ToString());
        ////////        DataColumn dtColumnF = new DataColumn(i.ToString() + "F");
        ////////        dtTempTable.Columns.Add(dtColumn);
        ////////        dtTempTable.Columns.Add(dtColumnF);
        ////////    }

        ////////    foreach (DataRow dr in DtConductorSection.Rows)
        ////////    {
        ////////        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["ProductCode"].ToString()));
        ////////        //ed.WriteMessage("MyBranch.ProductCode= " + myBranch.ProductCode.ToString() + "\n");
        ////////        DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));
        ////////        Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dtNode[0]["Node1Guid"].ToString()));
        ////////        Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dtNode[0]["Node2Guid"].ToString()));

        ////////        if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////        {


        ////////            Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);


        ////////            if (ConductorTip.PhaseCount > 0)
        ////////            {
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
        ////////                DataRow NewRow = dtTempTable.NewRow();
        ////////                //foreach (DataRow dr1 in DtPoleSection.Rows)
        ////////                //{
        ////////                //    if (MyNode.Code == (Guid)dr1["ProductCode"])
        ////////                //    {
        ////////                //        //ed.WriteMessage("Current Is LeftNode\n");
        ////////                //        NewRow["From"] = dr1["PoleNumber"].ToString();

        ////////                //    }
        ////////                //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
        ////////                //    {
        ////////                //        //ed.WriteMessage("Current Is RightNode\n");
        ////////                //        NewRow["To"] = dr1["PoleNumber"].ToString();
        ////////                //    }
        ////////                //}
        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));
        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {


        ////////                    //ed.WriteMessage("NormH={0}\n", drCond[0]["NormH"].ToString());
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }


        ////////            if (ConductorTip.NightCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                DataRow NewRow = dtTempTable.NewRow();
        ////////                //foreach (DataRow dr1 in DtPoleSection.Rows)
        ////////                //{
        ////////                //    if (MyNode.Code == (Guid)dr1["ProductCode"])
        ////////                //    {
        ////////                //        //ed.WriteMessage("Current Is LeftNode\n");
        ////////                //        NewRow["From"] = dr1["PoleNumber"].ToString();

        ////////                //    }
        ////////                //    if (MyNodeRigt.Code == (Guid)dr1["ProductCode"])
        ////////                //    {
        ////////                //        //ed.WriteMessage("Current Is RightNode\n");
        ////////                //        NewRow["To"] = dr1["PoleNumber"].ToString();
        ////////                //    }
        ////////                //}
        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));

        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {
        ////////                    //ed.WriteMessage("I am In CalcTemp\n");
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }
        ////////            if (ConductorTip.NeutralCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                DataRow NewRow = dtTempTable.NewRow();

        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));

        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {
        ////////                    //ed.WriteMessage("I am In CalcTemp\n");
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }
        ////////        }
        ////////        else
        ////////        {

        ////////            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);


        ////////            if (SelfKeeperTip.PhaseCount > 0)
        ////////            {
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
        ////////                DataRow NewRow = dtTempTable.NewRow();

        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 0, SectionCode));

        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {
        ////////                    //ed.WriteMessage("I am In CalcTemp\n");
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }


        ////////            if (SelfKeeperTip.NightCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NightProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                DataRow NewRow = dtTempTable.NewRow();

        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 1, SectionCode));

        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {
        ////////                    //ed.WriteMessage("I am In CalcTemp\n");
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }
        ////////            if (SelfKeeperTip.NeutralCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                DataRow NewRow = dtTempTable.NewRow();

        ////////                NewRow["From"] = dNumLeft.Number;
        ////////                NewRow["To"] = dNumRight.Number;
        ////////                //CalcSagTension();
        ////////                NewRow["ConductorName"] = Atend.Global.Calculation.Mechanical.CCommon.Name;
        ////////                DataRow[] drCond = dtStTable.Select(string.Format(" ConductorCode={0} And SectionCode='{1}'", 2, SectionCode));

        ////////                for (int i = end; i > start; i = i - distance)
        ////////                {
        ////////                    //ed.WriteMessage("I am In CalcTemp\n");
        ////////                    hTempTable = general.ComputeTension(Weather, i, Convert.ToDouble(drCond[0]["NormH"].ToString()), myBranch.Lenght);
        ////////                    fTempTable = (general.ComputeTotalWeight(Weather) * Math.Pow(myBranch.Lenght, 2)) / (8 * hTempTable);
        ////////                    NewRow[i.ToString()] = Math.Round(hTempTable, 2);
        ////////                    NewRow[i.ToString() + "F"] = Math.Round(fTempTable, 2);
        ////////                    NewRow["SpanLenght"] = myBranch.Lenght;
        ////////                }
        ////////                dtTempTable.Rows.Add(NewRow);
        ////////            }


        ////////        }

        ////////        //}
        ////////        //ed.WriteMessage("Add Row To DtTempTAble\n");

        ////////        //ed.WriteMessage("dtTempTable.Rows.Count= " + dtTempTable.Rows.Count.ToString() + "\n");
        ////////    }
        ////////    return dtTempTable;

        ////////}

        ////////public double IsSagOk()
        ////////{

        ////////    Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
        ////////    //ed.WriteMessage("PoleHeight= " + pole.Height.ToString() + "\n");
        ////////    Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
        ////////    Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////    switch ((Atend.Control.Enum.ProductType)(myPackage.Type))
        ////////    {
        ////////        case Atend.Control.Enum.ProductType.Pole:
        ////////            pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
        ////////            break;
        ////////        case Atend.Control.Enum.ProductType.PoleTip:
        ////////            Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
        ////////            pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode);
        ////////            break;
        ////////    }



        ////////    double Depth = .1 * pole.Height + .6;
        ////////    double ke = 1;

        ////////    //ed.WriteMessage("Ke= " + ke.ToString() + "\n");
        ////////    //ed.WriteMessage("Ke= "+ke.ToString()+"\n");
        ////////    double fmax1 = 0;
        ////////    DataTable dtPackage = new DataTable();
        ////////    dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

        ////////    if (dtPackage.Rows.Count != 0)
        ////////    {
        ////////        //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
        ////////        fmax1 = pole.Height - (Depth + Calamp.DistanceSupport + clearance + ZaribEtminan);
        ////////        //ed.WriteMessage("FMAX!={0}\n",fmax1);
        ////////    }

        ////////    //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
        ////////    double fmax2 = 0;
        ////////    double FMAX = fmax1;
        ////////    dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////    if (dtPackage.Rows.Count != 0)
        ////////    {

        ////////        //Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));

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
        ////////        fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance + ZaribEtminan);
        ////////        //ed.WriteMessage("fMAx1={0}\n",fmax1);
        ////////        //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
        ////////        fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
        ////////        if (fmax1 < fmax2)
        ////////            FMAX = fmax1;
        ////////        else
        ////////            FMAX = fmax2;
        ////////    }



        ////////    return FMAX;



        ////////    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam,(Guid)(DtPoleSection.Rows[0]["ProductCode"]));

        ////////    //Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////    //switch ((Atend.Control.Enum.ProductType)myPackage.Type)
        ////////    //{
        ////////    //    case Atend.Control.Enum.ProductType.Pole:
        ////////    //        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode);
        ////////    //        break;
        ////////    //    case Atend.Control.Enum.ProductType.PoleTip:
        ////////    //        Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode);
        ////////    //        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(PoleTip.PoleCode);
        ////////    //        break;
        ////////    //}

        ////////    //DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////    //DataTable dtPackageForInsulator = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(new Guid(dtPackage.Rows[0]["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));

        ////////    //Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.SelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));
        ////////    //Atend.Base.Equipment.EInsulator insulator = Atend.Base.Equipment.EInsulator.SelectByCode(Convert.ToInt32(dtPackageForInsulator.Rows[0]["ProductCode"]));
        ////////    ////ed.WriteMessage("Insulator.Code=" + insulator.Code + "\n");

        ////////    //double Depth = 0.1 * Convert.ToDouble(pole.Height) + 0.6;
        ////////    ////ed.WriteMessage("Pole.Height= " + pole.Height + " Depth= " + Depth + " Insulator.LenghtChain= " + insulator.LenghtInsulatorChain + " DistanceCrossArm= " + consol.DistanceCrossArm + " Clearance= " + Clearance + "\n");
        ////////    //double Fmax = pole.Height - (Depth +/* insulator.LenghtInsulatorChain*/.45 + consol.DistanceCrossArm + Clearance);
        ////////    //ed.WriteMessage("حداکثر فلش قابل قبول= " + Fmax.ToString() + "\n");
        ////////    //foreach (DataRow dr in dtStTable.Rows)
        ////////    //{
        ////////    //    if (Convert.ToDouble(dr["NormF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی نرمال از حد مجاز بیشتر می شود\n");
        ////////    //    }


        ////////    //    if (Convert.ToDouble(dr["IceF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی یخ سنگین از حد مجاز بیشتر می شود\n");
        ////////    //    }

        ////////    //    if (Convert.ToDouble(dr["WindF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی باد زیاد از حد مجاز بیشتر می شود\n");
        ////////    //    }


        ////////    //    if (Convert.ToDouble(dr["MaxTempF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی حداکثر دما از حد مجاز بیشتر می شود\n");
        ////////    //    }


        ////////    //    if (Convert.ToDouble(dr["MinTempF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی حداقل دما از حد مجاز بیشتر می شود\n");
        ////////    //    }


        ////////    //    if (Convert.ToDouble(dr["WindAndIceF"].ToString()) > Fmax)
        ////////    //    {
        ////////    //        ed.WriteMessage("فلش در شرط آب و هوایی باد و یخ از حد مجاز بیشتر می شود\n");
        ////////    //    }
        ////////    //}


        ////////}

        ////////public double WindOnPoleF2(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather weather, DataTable dtConductor)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    DataRow[] dr = dtConductor.Select(string.Format(" NodeType='{0}'", Atend.Control.Enum.ProductType.Consol));

        ////////    //ed.WriteMessage("Start WindOnPoleF\n");
        ////////    double L = pole.Height - (0.1 * pole.Height + .6);
        ////////    double AW = (L * (pole.ButtomCrossSectionArea + pole.TopCrossSectionArea)) / 2;
        ////////    double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));

        ////////    double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;

        ////////    //if (dr.Length != 0)
        ////////    //{
        ////////    //    Atend.Base.Design.DConsol dConsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
        ////////    //    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dConsol.ProductCode);
        ////////    //    h1 = L - consol.DistanceCrossArm;

        ////////    //}
        ////////    //else
        ////////    //{
        ////////    //    Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
        ////////    //    Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
        ////////    //    h1 = L - Clamp.DistanceSupport;
        ////////    //}



        ////////    double KFactor = .0812;
        ////////    if (pole.Shape == 0)
        ////////    {
        ////////        KFactor = .05;
        ////////    }
        ////////    //ed.WriteMessage("BCrossSection= " + pole.ButtomCrossSectionArea + "TCrossSectionArea= " + pole.TopCrossSectionArea + " Distance=" + consol.DistanceCrossArm + "K=" + KFactor + "\n");
        ////////    //ed.WriteMessage("L=" + L + " Aw" + AW + " h=" + h + "H1="+h1+" weather=" + weather.Code + "\n");
        ////////    double Result = (h / h1) * KFactor * AW * Math.Pow(weather.WindSpeed, 2);
        ////////    //ed.WriteMessage("PoleF= "+Result+"\n");
        ////////    return Result;
        ////////}

        ////////public double WindOnInsulator2(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather Weather, int CounOfCrossArm)
        ////////{
        ////////    //ed.WriteMessage("CountOfCross= "+CounOfCrossArm+" InsulatorDiameter= "+InsulatorDiamiter+" Shape= "+InsulatorShapeFactor+" LenghtChain= "+LenghtChain+"\n");
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //ed.WriteMessage("Start WindOnInsulator\n");
        ////////    return 3 * CounOfCrossArm * LenghtChain * InsulatorDiamiter * InsulatorShapeFactor * Math.Pow(Weather.WindSpeed, 2) / 16 * 1e-3;
        ////////}

        ////////public double WindOnConductor2(DataTable dtConductor, Atend.Base.Design.DWeather weather, Atend.Base.Equipment.EPole pole)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //ed.WriteMessage("Start WindOnConductor\n");
        ////////    double sum = 0;
        ////////    //ed.WriteMessage("I Am In WindOnConductor\n");
        ////////    foreach (DataRow dr in dtConductor.Rows)
        ////////    {
        ////////        Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////        if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
        ////////        {
        ////////            Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, Branch.ProductCode);
        ////////            if (conductorTip.PhaseCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
        ////////                sum += conductorTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////            if (conductorTip.NightCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
        ////////                //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                sum += conductorTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////            if (conductorTip.NeutralCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
        ////////                //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                sum += conductorTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////        }
        ////////        else
        ////////        {
        ////////            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, Branch.ProductCode);
        ////////            if (SelfKeeperTip.PhaseCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.PhaseProductCode);
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase));
        ////////                //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
        ////////                sum += SelfKeeperTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////            if (SelfKeeperTip.NightCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
        ////////                //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night));
        ////////                sum += SelfKeeperTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////            if (SelfKeeperTip.NeutralCount > 0)
        ////////            {
        ////////                //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
        ////////                //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
        ////////                Atend.Global.Calculation.Mechanical.CCommon.SetBranchData(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural));
        ////////                sum += SelfKeeperTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
        ////////            }
        ////////        }
        ////////    }

        ////////    //ed.WriteMessage("Sum ="+sum.ToString()+"\n");


        ////////    double L = pole.Height - (0.1 * pole.Height + .6);
        ////////    double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
        ////////    double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;
        ////////    sum = sum * (h / h1);
        ////////    return sum;
        ////////}

        ////////public CVector WindOnTangantPole2(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //ed.WriteMessage("WindOnTangantPole\n");
        ////////    Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
        ////////    CVector SumTension = new CVector();


        ////////    SumTension.Absolute = 0;
        ////////    SumTension.Angle = 0;
        ////////    foreach (DataRow dr in dtConductor.Rows)
        ////////    {
        ////////        if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////        {
        ////////            Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(dr["NodeGuid"].ToString()));
        ////////            Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////            if (Econsol.ConsolType == 2 || Econsol.ConsolType == 3)
        ////////            {
        ////////                Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

        ////////                //ed.WriteMessage("It Is Tangant Consol\n");
        ////////                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////                DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
        ////////                if (drSec.Length == 0)
        ////////                {
        ////////                    DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////                    sectionCode = Section.SectionCode;
        ////////                    CalSagTension(myBranch);
        ////////                }
        ////////                Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                //ed.WriteMessage("SwichTAngant\n");
        ////////                if (conductorTip.PhaseCount > 0)
        ////////                {
        ////////                    int ConductorCode = 0;
        ////////                    #region Swich
        ////////                    //ed.WriteMessage("i={0}\n",i.ToString());
        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                //ed.WriteMessage("$$$pp");
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                //ed.WriteMessage("NormH= "+dr1[0]["NormH"].ToString()+"Angle= "+dr["Angle"].ToString()+"\n");
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;

        ////////                    Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }


        ////////                if (conductorTip.NightCount > 0)
        ////////                {

        ////////                    int ConductorCode = 1;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }
        ////////                if (conductorTip.NeutralCount > 0)
        ////////                {

        ////////                    int ConductorCode = 2;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }
        ////////            }
        ////////        }
        ////////        else
        ////////        {
        ////////            //ed.WriteMessage("KAlamp\n");
        ////////            if (Convert.ToInt32(dr["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////            {
        ////////                Atend.Base.Design.DPackage Dpack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dr["NodeGuid"].ToString()));
        ////////                Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, Dpack.ProductCode);
        ////////                //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                if (ECalamp.Type == 5)
        ////////                {
        ////////                    Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                    //ed.WriteMessage("It Is Tangant Consol\n");
        ////////                    Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
        ////////                    DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                    if (drSec.Length == 0)
        ////////                    {
        ////////                        DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                        sectionCode = Section.SectionCode;
        ////////                        CalSagTension(myBranch);
        ////////                    }

        ////////                    //CalSagTension(myBranch);
        ////////                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                    if (SelfKeeperTip.PhaseCount > 0)
        ////////                    {
        ////////                        int ConductorCode = 0;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    //ed.WriteMessage("NormH= "+dr1[0]["NormH"].ToString()+"Angle= "+dr["Angle"].ToString()+"\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");

        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;

        ////////                        Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }


        ////////                    if (SelfKeeperTip.NightCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 1;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }
        ////////                    if (SelfKeeperTip.NeutralCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 2;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + dr["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }
        ////////                }
        ////////            }
        ////////        }
        ////////    }

        ////////    //ed.WriteMessage("Tension.Absolute= "+SumTension.Absolute+"\n");
        ////////    return SumTension;

        ////////}

        ////////public CVector WindOnDDEPole2(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
        ////////{
        ////////    //ed.WriteMessage("I AM In WindOnDDEPole\n");
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    //ed.WriteMessage("Start WindOnDDEPole\n");
        ////////    Atend.Global.Calculation.Mechanical.CVector Tension = new CVector();
        ////////    CVector SumTension = new CVector();
        ////////    CVector MaxTension = new CVector();
        ////////    MaxTension.Absolute = 0;
        ////////    MaxTension.Angle = 0;
        ////////    SumTension.Absolute = 0;
        ////////    SumTension.Angle = 0;
        ////////    //****************X1=a+b+c
        ////////    //ed.WriteMessage("X1=a+b+c\n");
        ////////    foreach (DataRow drConductor in dtConductor.Rows)
        ////////    {
        ////////        if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////        {
        ////////            Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////            Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////            //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////            if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
        ////////            {
        ////////                Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

        ////////                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
        ////////                DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                if (drSec.Length == 0)
        ////////                {
        ////////                    DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        ////////                    sectionCode = Section.SectionCode;
        ////////                    CalSagTension(myBranch);
        ////////                }
        ////////                //CalSagTension(myBranch);
        ////////                Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                if (conductorTip.PhaseCount > 0)
        ////////                {

        ////////                    int ConductorCode = 0;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {

        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }


        ////////                if (conductorTip.NightCount > 0)
        ////////                {

        ////////                    int ConductorCode = 1;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }
        ////////                if (conductorTip.NeutralCount > 0)
        ////////                {

        ////////                    int ConductorCode = 2;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                    SumTension = SumTension.Add(SumTension, Tension);
        ////////                }
        ////////            }
        ////////        }
        ////////        else
        ////////        {
        ////////            if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////            {
        ////////                Atend.Base.Design.DPackage DPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////                Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, DPack.ProductCode);
        ////////                //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                if (ECalamp.Type == 1 || ECalamp.Type == 2 || ECalamp.Type == 3 || ECalamp.Type == 4 || ECalamp.Type == 6)
        ////////                {
        ////////                    Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

        ////////                    Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
        ////////                    DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

        ////////                    if (drSec.Length == 0)
        ////////                    {
        ////////                        DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        ////////                        sectionCode = Section.SectionCode;
        ////////                        CalSagTension(myBranch);
        ////////                    }
        ////////                    //CalSagTension(myBranch);
        ////////                    Atend.Base.Equipment.ESelfKeeperTip SelfKeepeTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                    if (SelfKeepeTip.PhaseCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 0;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {

        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }


        ////////                    if (SelfKeepeTip.NightCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 1;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }
        ////////                    if (SelfKeepeTip.NeutralCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 2;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeepeTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                        SumTension = SumTension.Add(SumTension, Tension);
        ////////                    }
        ////////                }
        ////////            }
        ////////        }

        ////////    }
        ////////    //ed.WriteMessage("sumTension="+SumTension.Absolute+"\n");
        ////////    //****************************
        ////////    //ed.WriteMessage("Second\n");
        ////////    foreach (DataRow drConductor in dtConductor.Rows)
        ////////    {
        ////////        if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
        ////////        {
        ////////            Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////            Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
        ////////            if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
        ////////            {
        ////////                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

        ////////                Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
        ////////                if (conductorTip.PhaseCount > 0)
        ////////                {

        ////////                    int ConductorCode = 0;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                    MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                }


        ////////                if (conductorTip.NightCount > 0)
        ////////                {

        ////////                    int ConductorCode = 1;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                    MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                }
        ////////                if (conductorTip.NeutralCount > 0)
        ////////                {

        ////////                    int ConductorCode = 2;
        ////////                    #region Swich

        ////////                    switch (i)
        ////////                    {
        ////////                        case 0:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 1:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 2:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 3:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 4:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                        case 5:
        ////////                            {
        ////////                                DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                break;
        ////////                            };
        ////////                    }

        ////////                    #endregion
        ////////                    Tension.Absolute *= conductorTip.PhaseCount;
        ////////                    Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                    MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                }
        ////////            }

        ////////        }
        ////////        else
        ////////        {
        ////////            if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))
        ////////            {
        ////////                Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(drConductor["NodeGuid"].ToString()));
        ////////                Atend.Base.Equipment.EClamp eCalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, dPackage.ProductCode);
        ////////                if (eCalamp.Type == 0)
        ////////                {
        ////////                    Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

        ////////                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
        ////////                    if (SelfKeeperTip.PhaseCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 0;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    //ed.WriteMessage("IceH= " + dr1[0]["IceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    //ed.WriteMessage("WindH= " + dr1[0]["WindH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    //ed.WriteMessage("MaxTempH= " + dr1[0]["MaxTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    //ed.WriteMessage("MinTempH= " + dr1[0]["MinTempH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    //ed.WriteMessage("WindAndIceH= " + dr1[0]["WindAndIceH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                        MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                    }


        ////////                    if (SelfKeeperTip.NightCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 1;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                        MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                    }
        ////////                    if (SelfKeeperTip.NeutralCount > 0)
        ////////                    {

        ////////                        int ConductorCode = 2;
        ////////                        #region Swich

        ////////                        switch (i)
        ////////                        {
        ////////                            case 0:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 1:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["IceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 2:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 3:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MaxTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 4:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["MinTempH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                            case 5:
        ////////                                {
        ////////                                    DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, sectionCode.ToString()));
        ////////                                    Tension.Absolute = Convert.ToDouble(dr1[0]["WindAndIceH"].ToString());
        ////////                                    break;
        ////////                                };
        ////////                        }

        ////////                        #endregion
        ////////                        Tension.Absolute *= SelfKeeperTip.PhaseCount;
        ////////                        Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
        ////////                        MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
        ////////                    }
        ////////                }
        ////////            }

        ////////        }
        ////////    }




        ////////    //ed.WriteMessage("MaxTension.Absolute= "+MaxTension.Absolute+"\n");
        ////////    return MaxTension;


        ////////}

        ////////public DataTable WindOnPole2()
        ////////{
        ////////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////////    double sw = 0;
        ////////    double[] temp = new double[6];
        ////////    DataTable dtTable = new DataTable();
        ////////    DataColumn dcPole = new DataColumn("dcPole");
        ////////    DataColumn dcNorm = new DataColumn("dcNorm");
        ////////    DataColumn dcIceHeave = new DataColumn("dcIceHeavy");
        ////////    DataColumn dcWindSpeed = new DataColumn("dcWindSpeed");
        ////////    DataColumn dcMaxTemp = new DataColumn("dcMaxTemp");
        ////////    DataColumn dcMinTemp = new DataColumn("dcMinTemp");
        ////////    DataColumn dcWindIce = new DataColumn("dcWindIce");
        ////////    DataColumn dcPoleGuid = new DataColumn("dcPoleGuid");
        ////////    DataColumn dcAngle = new DataColumn("dcAngle");

        ////////    dtTable.Columns.Add(dcPole);
        ////////    dtTable.Columns.Add(dcIceHeave);
        ////////    dtTable.Columns.Add(dcMaxTemp);
        ////////    dtTable.Columns.Add(dcMinTemp);
        ////////    dtTable.Columns.Add(dcNorm);
        ////////    dtTable.Columns.Add(dcWindIce);
        ////////    dtTable.Columns.Add(dcWindSpeed);
        ////////    dtTable.Columns.Add(dcPoleGuid);
        ////////    dtTable.Columns.Add(dcAngle);

        ////////    DataTable dtPoleConductor = new DataTable();
        ////////    dtPoleConductor.Columns.Add("NodeGuid");
        ////////    dtPoleConductor.Columns.Add("BranchGuid");
        ////////    dtPoleConductor.Columns.Add("Angle");
        ////////    dtPoleConductor.Columns.Add("BranchType");
        ////////    dtPoleConductor.Columns.Add("NodeType");

        ////////    foreach (DataRow dr in dtPoleSection.Rows)
        ////////    {
        ////////        sw = 0;

        ////////        Atend.Base.Design.DWeather Weather = new Atend.Base.Design.DWeather();

        ////////        //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(dr["ProductCode"]));
        ////////        Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(dr["ProductCode"]));
        ////////        Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
        ////////        switch ((Atend.Control.Enum.ProductType)myPackage.Type)
        ////////        {
        ////////            case Atend.Control.Enum.ProductType.Pole:
        ////////                pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, myPackage.ProductCode);
        ////////                break;

        ////////            case Atend.Control.Enum.ProductType.PoleTip:
        ////////                Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dtPoleTipParm, myPackage.ProductCode);
        ////////                pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, PoleTip.PoleCode);
        ////////                break;
        ////////        }


        ////////        DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////////        int countOfCrossArm = dtPackage.Rows.Count;

        ////////        double WindOnConductor1 = 0;
        ////////        double WindOnPole1 = 0;
        ////////        double WindOnInsulator1 = 0;
        ////////        CVector TensionTangant = new CVector();
        ////////        CVector TensionDDE = new CVector();
        ////////        double[] Angle = new double[6];

        ////////        double Total = 0;
        ////////        dtPoleConductor.Rows.Clear();
        ////////        DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
        ////////        foreach (DataRow drConductor in dtConductor)
        ////////        {
        ////////            DataRow[] drs1 = dtBranchList.Select(string.Format(" Node1Guid='{0}'", drConductor["NodeGuid"].ToString()));
        ////////            if (drs1.Length != 0)
        ////////            {
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
        ////////                DataRow NewRow = dtPoleConductor.NewRow();
        ////////                NewRow["NodeGuid"] = drs2[0]["Node2Guid"].ToString();
        ////////                NewRow["BranchGuid"] = drs2[0]["BranchGuid"].ToString();
        ////////                NewRow["Angle"] = drs2[0]["Angle2"].ToString();
        ////////                NewRow["BranchType"] = drs2[0]["Type"].ToString();
        ////////                NewRow["NodeType"] = drConductor["NodeType"].ToString();

        ////////                dtPoleConductor.Rows.Add(NewRow);

        ////////            }


        ////////        }
        ////////        //ed.WriteMessage("dtPoleConductor.rows.count={0}\n",dtPoleConductor.Rows.Count);
        ////////        //foreach (DataRow drf in dtPoleConductor.Rows)
        ////////        //{
        ////////        //    ed.WriteMessage("drf={0},NodeGuid={1}\n",drf["Angle"].ToString(),drf["NodeGuid"].ToString());
        ////////        //}

        ////////        //DataTable dtPoleConductor = Atend.Global.Acad.UAcad.GetPoleConductors(new Guid(dr["ProductCode"].ToString()));
        ////////        double max = 0;
        ////////        int k = 0;
        ////////        for (int i = 0; i < 6; i++)
        ////////        {
        ////////            Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1);
        ////////            //ed.WriteMessage("ProductCode= "+dr["ProductCode"].ToString()+"\n");

        ////////            //ed.WriteMessage("dtPoleConductor.Rows.Count= " + dtPoleConductor.Length.ToString() + "\n");
        ////////            WindOnConductor1 = WindOnConductor2(dtPoleConductor, Weather, pole);
        ////////            if (countOfCrossArm != 0)
        ////////            {
        ////////                WindOnInsulator1 = WindOnInsulator2(pole, Weather, countOfCrossArm);
        ////////            }
        ////////            //ed.WriteMessage("Insulator= " + WindOnInsulator1 + "\n");
        ////////            WindOnPole1 = WindOnPoleF2(pole, Weather, dtPoleConductor);
        ////////            TensionTangant = WindOnTangantPole2(Weather, dtPoleConductor, i);
        ////////            TensionDDE = WindOnDDEPole2(Weather, dtPoleConductor, i);
        ////////            Total = WindOnConductor1 + WindOnInsulator1 + WindOnPole1 + TensionTangant.Absolute + TensionDDE.Absolute;
        ////////            temp[i] = Total;
        ////////            Angle[i] = TensionDDE.Add(TensionDDE, TensionTangant).Angle;
        ////////            if (temp[i] > max)
        ////////            {
        ////////                max = temp[i];
        ////////                k = i;
        ////////            }
        ////////            //ed.WriteMessage("i= " + i.ToString() + "\n");

        ////////        }

        ////////        DataRow dr1 = dtTable.NewRow();
        ////////        dr1["dcPole"] = dr["PoleNumber"].ToString();
        ////////        dr1["dcNorm"] = Math.Round(Convert.ToDouble(temp[0].ToString()), 2);
        ////////        dr1["dcIceHeavy"] = Math.Round(Convert.ToDouble(temp[1].ToString()), 2);
        ////////        dr1["dcWindSpeed"] = Math.Round(Convert.ToDouble(temp[2].ToString()), 2);
        ////////        dr1["dcMaxTemp"] = Math.Round(Convert.ToDouble(temp[3].ToString()), 2);
        ////////        dr1["dcMinTemp"] = Math.Round(Convert.ToDouble(temp[4].ToString()), 2);
        ////////        dr1["dcWindIce"] = Math.Round(Convert.ToDouble(temp[5].ToString()), 2);
        ////////        dr1["dcPoleGuid"] = dr["ProductCode"].ToString();
        ////////        dr1["dcAngle"] = Math.Round(Convert.ToDouble(Angle[k].ToString()), 2);
        ////////        dtTable.Rows.Add(dr1);
        ////////        //ed.WriteMessage("Add Row\n");

        ////////    }
        ////////    //ed.WriteMessage("Finish Calculating\n");
        ////////    return dtTable;
        ////////}

        public CVector Max(CVector a, CVector b)
        {
            if (a.Absolute > b.Absolute)
                return a;
            return b;
        }


        public void CalcSagTension02()
        {
            double ke = 1;

            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();

            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(DtConductorSection.Rows[0]["ProductCode"]));

            Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(DtPoleSection.Rows[0]["ProductCode"]));
            Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
            switch ((Atend.Control.Enum.ProductType)myPackage.Type)
            {
                case Atend.Control.Enum.ProductType.Pole:
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(myPackage.ProductCode, aConnection);
                    break;
                case Atend.Control.Enum.ProductType.PoleTip:
                    Atend.Base.Equipment.EPoleTip poleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(myPackage.ProductCode, aConnection);
                    pole = Atend.Base.Equipment.EPole.AccessSelectByCode(poleTip.PoleCode, aConnection);
                    break;
            }


            DataTable dtPackageConsol = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), aConnection);
            DataTable dtPackageCalamp = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp), aConnection);

            double Depth = .1 * pole.Height + .6;



            double fmax1 = 0;
            if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))
            {
                //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
                fmax1 = pole.Height - (Depth + calamp.DistanceSupport + clearance + ZaribEtminan);
                //ed.WriteMessage("FMAX!={0}\n",fmax1);
            }

            //ed.WriteMessage("Volt= " + Volt + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke=" + ke + "\n");
            double fmax2 = 0;
            double FMAX = fmax1;
            Atend.Base.Calculating.CKE cke = Atend.Base.Calculating.CKE.AccessSelectByCrossSectionArea(Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
            //ed.WriteMessage("ke={0},CrossSectionArea={1}\n", ke, Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
            if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                double Vn = Volt / 1000;
                if (cke != null)
                {
                    switch (consol.Type)
                    {
                        case 0: { ke = cke.Vertical; break; }
                        case 1: { ke = cke.Triangle; break; }
                        case 2: { ke = cke.Horizental; break; }


                    }

                }
                //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n", Depth, LenghtChain, consol.DistanceCrossArm, Clearance, ZaribEtminan);
                //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
                fmax1 = pole.Height - (Depth +/*insulator.LenghtInsulatorChain*/.45 + consol.DistanceCrossArm + clearance + ZaribEtminan);
                //ed.WriteMessage("fMAx1={0}\n", fmax1);
                //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n", consol.DistancePhase, Vn, ke);
                fmax2 = (Math.Pow((consol.DistancePhase - (Vn / 150)) / ke, 2)) - .45 /*insulator.LenghtInsulatorChain*/;
                // ed.WriteMessage("fMax2={0}\n", fmax2);
                if (fmax1 < fmax2)
                    FMAX = fmax1;
                else
                    FMAX = fmax2;
            }

            //ed.WriteMessage("FMAx={0}\n", FMAX);


            //MaxSpan
            double MaxSpan = ComputeMaxSpan02();

            double SE = ComputeSE02();


            //ed.WriteMessage("fmax1="+fmax1+"\n");
            //ed.WriteMessage("fmax2=" + fmax2 + "\n");
            //H & F In MaxTemp
            Atend.Base.Design.DWeather WeatherBase = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 4, aConnection);//حداکثر دما
            //ed.WriteMessage("MaxSpan={0}\n", MaxSpan);
            h[3] = (general.ComputeTotalWeight(WeatherBase) * (Math.Pow(MaxSpan, 2))) / (8 * FMAX);
            f[3] = FMAX;
            //ed.WriteMessage("H[3]= " + h[3].ToString() + "\n");
            //ed.WriteMessage("F[3]= " + f[3].ToString() + "\n");
            //ed.WriteMessage("MyBranch>Lenght= " + myBranch.Lenght + "\n");
            //H & F In OtherCondition
            int i = 0;
            do
            {
                if (i != 3)
                {
                    Atend.Base.Design.DWeather WeatherSecond = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);

                    h[i] = general.ComputeTension(WeatherBase, WeatherSecond, h[3], SE);
                    //ed.WriteMessage("***H[i]={0},SE={1},i={2}\n", h[i].ToString(), SE.ToString(), i);
                    f[i] = (general.ComputeTotalWeight(WeatherSecond) * (Math.Pow(SE, 2))) / (8 * h[i]);
                    //ed.WriteMessage("####F[i]={0},i{1}\n", f[i].ToString(), i);
                }
                i++;
            } while (i < 6);
        }
        public DataTable CalSagTension02(Atend.Base.Design.DBranch MyBranch)
        {
            //ed.WriteMessage("&&&&&&&&&&&&&&&&&&&&dtstTable={0}\n", dtStTable.Rows.Count);
            double fmax = IsSagOk02();
            //Atend.Base.Design.DBranch MyBranch = Atend.Base.Design.DBranch.SelectByCode(new Guid(DtConductorSection.Rows[0]["ProductCode"].ToString()));
            if (MyBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
            {
                #region Conductor
                Atend.Base.Equipment.EConductorTip MyConducyorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, MyBranch.ProductCode);

                if (MyConducyorTip.PhaseCount > 0)
                {
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MyConductor.Name + "\n");
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
                    dr["SectionCode"] = SectionCode.ToString();
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
                    dr["SectionCode"] = SectionCode.ToString();
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
                    dr["SectionCode"] = SectionCode.ToString();
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }
            else
            {
                #region SelfKeeper
                Atend.Base.Equipment.ESelfKeeperTip MySelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, MyBranch.ProductCode);
                //ed.WriteMessage("MyConductorTip.NAme=" + MyConducyorTip.Name + "\n");
                if (MySelfKeeperTip.PhaseCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.SelectByCode(MySelfKeeperTip.PhaseProductCode);
                    //ed.WriteMessage("Myconductor.NAMae In The Phase = " + MyConductor.Name + "\n");
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(MyBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
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
                    dr["SectionCode"] = SectionCode.ToString();
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NightCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NightProductCode);
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
                    dr["SectionCode"] = SectionCode.ToString();
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                if (MySelfKeeperTip.NeutralCount > 0)
                {
                    //Atend.Base.Equipment.ESelfKeeper MySelfKeeper = Atend.Base.Equipment.ESelfKeeper.AccessSelectByCode(MySelfKeeperTip.NeutralProductCode);
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
                    dr["SectionCode"] = SectionCode.ToString();
                    dr["MaxF"] = Math.Round(fmax, 2);

                    dtStTable.Rows.Add(dr);
                }
                #endregion
            }

            return dtStTable;
        }
        public double ComputeSE02()
        {
            double s1 = 0;
            double s2 = 0;
            //ed.WriteMessage("I Am In Compute SE\n");
            //ed.WriteMessage("dtConductorSection.Count= " + DtConductorSection.Rows.Count.ToString() + "\n");
            foreach (DataRow dr in DtConductorSection.Rows)
            {

                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(dr["ProductCode"]));
                //ed.WriteMessage("MyBranch.Code= " + myBranch.Code.ToString() + "MyBranch.Lenght= " + myBranch.Lenght.ToString() + "\n");
                s1 += Math.Pow(myBranch.Lenght, 3);
                s2 += myBranch.Lenght;
            }
            double se = Math.Sqrt(s1 / s2);
            //ed.WriteMessage("SE= " + se.ToString() + "\n");
            return se;
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
            Atend.Base.Design.DBranch branch = Atend.Base.Design.DBranch.AccessSelectByCode(new Guid(DtConductorSection.Rows[0]["ProductCode"].ToString()), aConnection);
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
                        ed.WriteMessage("HMAX={0},i={1}\n", hMax[i].ToString(), i.ToString());
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
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
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
                    Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                    for (int i = 0; i < 6; i++)
                    {
                        Atend.Base.Design.DWeather weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
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


            return dtHMax;

        }
        public double ComputeMaxSpan02()
        {
            double MaxSpan = 0;
            foreach (DataRow dr in dtConductorSection.Rows)
            {
                Atend.Base.Design.DBranch myBranch1 = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, (Guid)(dr["ProductCode"]));
                if (myBranch1.Lenght > MaxSpan)
                    MaxSpan = myBranch1.Lenght;

            }
            return MaxSpan;
        }
        public DataTable CalcTempTable02()
        {
            // ed.WriteMessage("@@@@@@@@@@@@@@@Start CalcTempTable@@@@@@@@@@@@@@@@@@@@\n");
            if (end < start)
            {
                int t = end;
                end = start;
                start = t;
            }
            Atend.Global.Calculation.General.General general = new Atend.Global.Calculation.General.General();
            Atend.Base.Design.DWeather Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, 1, aConnection);//شرط آب و هوایی نرمال 
            double hTempTable, fTempTable;
            DataTable dtTempTable = new DataTable();
            DataColumn dcColumn1 = new DataColumn("SpanLenght");
            DataColumn dcColumn2 = new DataColumn("From");
            DataColumn dcColumn3 = new DataColumn("To");
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

            foreach (DataRow dr in DtConductorSection.Rows)
            {
                Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["ProductCode"].ToString()));
                //ed.WriteMessage("MyBranch.ProductCode= " + myBranch.ProductCode.ToString() + "\n");
                DataRow[] dtNode = dtBranchList.Select(string.Format(" BranchGuid='{0}'", myBranch.Code.ToString()));
                Atend.Base.Design.DPackage dNumLeft = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dtNode[0]["Node1Guid"].ToString()));
                Atend.Base.Design.DPackage dNumRight = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(dtNode[0]["Node2Guid"].ToString()));

                if (myBranch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {


                    Atend.Base.Equipment.EConductorTip ConductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);


                    if (ConductorTip.PhaseCount > 0)
                    {
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
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


                            //ed.WriteMessage("NormH={0}\n", drCond[0]["NormH"].ToString());
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
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
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
                    if (ConductorTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.NeutralProductCode);
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                        DataRow NewRow = dtTempTable.NewRow();

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

                    Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);


                    if (SelfKeeperTip.PhaseCount > 0)
                    {
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(ConductorTip.PhaseProductCode);
                        DataRow NewRow = dtTempTable.NewRow();

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
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                        DataRow NewRow = dtTempTable.NewRow();

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
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(myBranch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                        DataRow NewRow = dtTempTable.NewRow();

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
        public double WindOnPoleF02(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather weather, DataTable dtConductor)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataRow[] dr = dtConductor.Select(string.Format(" NodeType='{0}'", Atend.Control.Enum.ProductType.Consol));

            //ed.WriteMessage("Start WindOnPoleF\n");
            double L = pole.Height - (0.1 * pole.Height + .6);
            double AW = (L * (pole.ButtomCrossSectionArea + pole.TopCrossSectionArea)) / 2;
            double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));

            double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;

            //if (dr.Length != 0)
            //{
            //    Atend.Base.Design.DConsol dConsol = Atend.Base.Design.DConsol.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
            //    Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dConsol.ProductCode);
            //    h1 = L - consol.DistanceCrossArm;

            //}
            //else
            //{
            //    Atend.Base.Design.DPackage dPackage = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(dtConductor.Rows[0]["NodeGuid"].ToString()));
            //    Atend.Base.Equipment.EClamp Clamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dPackage.ProductCode);
            //    h1 = L - Clamp.DistanceSupport;
            //}



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
        public double WindOnInsulator02(Atend.Base.Equipment.EPole pole, Atend.Base.Design.DWeather Weather, int CounOfCrossArm)
        {
            //ed.WriteMessage("CountOfCross= "+CounOfCrossArm+" InsulatorDiameter= "+InsulatorDiamiter+" Shape= "+InsulatorShapeFactor+" LenghtChain= "+LenghtChain+"\n");
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnInsulator\n");
            return 3 * CounOfCrossArm * LenghtChain * InsulatorDiamiter * InsulatorShapeFactor * Math.Pow(Weather.WindSpeed, 2) / 16 * 1e-3;
        }
        public double WindOnConductor02(DataTable dtConductor, Atend.Base.Design.DWeather weather, Atend.Base.Equipment.EPole pole)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start WindOnConductor\n");
            double sum = 0;
            //ed.WriteMessage("I Am In WindOnConductor\n");
            foreach (DataRow dr in dtConductor.Rows)
            {
                Atend.Base.Design.DBranch Branch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                if (Branch.ProductType == Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))
                {
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
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Phase), aConnection);
                        //ed.WriteMessage("Branch>lenght= "+ Branch.Lenght+" ConductorCode= "+conductor.Code+" Conductor.Diagonal= "+conductor.Diagonal+" PhaseCount= "+conductorTip.PhaseCount+"\n");
                        sum += SelfKeeperTip.PhaseCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (SelfKeeperTip.NightCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NightProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal + " PhaseCount= "+conductorTip.NightCount+"\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Night), aConnection);
                        sum += SelfKeeperTip.NightCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                    if (SelfKeeperTip.NeutralCount > 0)
                    {
                        //Atend.Base.Equipment.EConductor conductor = Atend.Base.Equipment.EConductor.AccessSelectByCode(conductorTip.NeutralProductCode);
                        //ed.WriteMessage("Branch>lenght= " + Branch.Lenght + " ConductorCode= " + conductor.Code + "Conductor.Diagonal= " + conductor.Diagonal +" PhaseCount= "+conductorTip.NeutralCount+ "\n");
                        Atend.Global.Calculation.Mechanical.CCommon.SetBranchData02(Branch, Convert.ToInt32(Atend.Control.Enum.BranchMode.Netural), aConnection);
                        sum += SelfKeeperTip.NeutralCount * (Branch.Lenght / 2) * (Atend.Global.Calculation.Mechanical.CCommon.Diagonal + 2 * weather.IceDiagonal) * Math.Pow(weather.WindSpeed, 2) / 16 * 1e-3;
                    }
                }
            }

            //ed.WriteMessage("Sum ="+sum.ToString()+"\n");


            double L = pole.Height - (0.1 * pole.Height + .6);
            double h = (pole.ButtomCrossSectionArea - (Math.Pow((Math.Pow(pole.TopCrossSectionArea, 2) + Math.Pow(pole.ButtomCrossSectionArea, 2)) / 2, .5))) * (L / (pole.ButtomCrossSectionArea - pole.TopCrossSectionArea));
            double h1 = pole.Height - (0.1 * pole.Height + .6) - .6;
            sum = sum * (h / h1);
            return sum;
        }
        public CVector WindOnTangantPole02(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
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
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(dr["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
                    if (Econsol.ConsolType == 2 || Econsol.ConsolType == 3)
                    {
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(dr["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        //ed.WriteMessage("It Is Tangant Consol\n");
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));
                        if (drSec.Length == 0)
                        {
                            DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor), aConnection);
                            sectionCode = Section.SectionCode;
                            CalSagTension02(myBranch);
                        }
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        //ed.WriteMessage("SwichTAngant\n");
                        if (conductorTip.PhaseCount > 0)
                        {
                            int ConductorCode = 0;
                            #region Swich
                            //ed.WriteMessage("i={0}\n",i.ToString());
                            switch (i)
                            {
                                case 0:
                                    {
                                        //ed.WriteMessage("$$$pp");
                                        DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                        //ed.WriteMessage("dr1.lenght={0}\n",dr1.Length);
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
                            Tension.Absolute *= conductorTip.PhaseCount;

                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
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
                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(dr["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
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

                            //ed.WriteMessage("It Is Tangant Consol\n");
                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(dr["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper), aConnection);
                                sectionCode = Section.SectionCode;
                                CalSagTension02(myBranch);
                            }

                            //CalSagTension(myBranch);
                            Atend.Base.Equipment.ESelfKeeperTip SelfKeeperTip = Atend.Base.Equipment.ESelfKeeperTip.AccessSelectByCode(dtSelfKeeperTipParam, myBranch.ProductCode);
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
            return SumTension;

        }
        public CVector WindOnDDEPole02(Atend.Base.Design.DWeather weather, DataTable dtConductor, int i)
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
            CVector max = new CVector();
            max.Absolute = 0;
            max.Angle = 0;
            //****************X1=a+b+c
            //ed.WriteMessage("X1=a+b+c\n");
            foreach (DataRow drConductor in dtConductor.Rows)
            {
                if (Convert.ToInt32(drConductor["NodeType"].ToString()) == Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))
                {
                    Atend.Base.Design.DConsol Dconsol = Atend.Base.Design.DConsol.AccessSelectByCode(dtDConsolParam, new Guid(drConductor["NodeGuid"].ToString()));
                    Atend.Base.Equipment.EConsol Econsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(dtConsolParam, Dconsol.ProductCode);
                    //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                    if (Econsol.ConsolType == 0 || Econsol.ConsolType == 1)
                    {
                        Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
                        DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                        if (drSec.Length == 0)
                        {
                            DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor), aConnection);
                            sectionCode = Section.SectionCode;
                            CalSagTension02(myBranch);
                        }
                        //CalSagTension(myBranch);
                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
                        {

                            int ConductorCode = 0;
                            #region Swich

                            switch (i)
                            {
                                case 0:
                                    {

                                        DataRow[] dr1 = dtStTable.Select(string.Format(" ConductorCode={0} AND SectionCode='{1}'", ConductorCode, Section.SectionCode.ToString()));
                                        Tension.Absolute = Convert.ToDouble(dr1[0]["NormH"].ToString());
                                        //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
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
                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());

                            SumTension = SumTension.Add(SumTension, Tension);
                        }


                        if (conductorTip.NightCount > 0)
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
                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            SumTension = SumTension.Add(SumTension, Tension);
                        }
                        if (conductorTip.NeutralCount > 0)
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
                        Atend.Base.Design.DPackage DPack = Atend.Base.Design.DPackage.AccessSelectByCode(dtPackageParam, new Guid(drConductor["NodeGuid"].ToString()));
                        Atend.Base.Equipment.EClamp ECalamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(dtCalampParam, DPack.ProductCode);
                        //DtconductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                        if (ECalamp.Type == 1 || ECalamp.Type == 2 || ECalamp.Type == 3 || ECalamp.Type == 4 || ECalamp.Type == 6)
                        {
                            Atend.Base.Design.DPoleSection Section = Atend.Base.Design.DPoleSection.AccessSelectByProductCodeProductType(dtDPoleSectionParam, new Guid(drConductor["BranchGuid"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));

                            Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));
                            DataRow[] drSec = dtStTable.Select(string.Format(" SectionCode='{0}'", Section.SectionCode.ToString()));

                            if (drSec.Length == 0)
                            {
                                DtConductorSection = Atend.Base.Design.DPoleSection.AccessSelectBySectionCodeProductType(Section.SectionCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
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
                                            //ed.WriteMessage("NormH= " + dr1[0]["NormH"].ToString() + "Angle= " + drConductor["Angle"].ToString() + "\n");
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
            //ed.WriteMessage("sumTension="+SumTension.Absolute+"\n");
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
                        Atend.Base.Design.DBranch myBranch = Atend.Base.Design.DBranch.AccessSelectByCode(dtBranchParam, new Guid(drConductor["BranchGuid"].ToString()));

                        Atend.Base.Equipment.EConductorTip conductorTip = Atend.Base.Equipment.EConductorTip.AccessSelectByCode(dtConductorTipParam, myBranch.ProductCode);
                        if (conductorTip.PhaseCount > 0)
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
                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            max = Max(MaxTension, max);
                        }


                        if (conductorTip.NightCount > 0)
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
                            Tension.Absolute *= conductorTip.PhaseCount;
                            Tension.Angle = Convert.ToDouble(drConductor["Angle"].ToString());
                            MaxTension = Max(SumTension, MaxTension.Mines(SumTension, Tension));
                            max = Max(MaxTension, max);
                        }
                        if (conductorTip.NeutralCount > 0)
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
                        if (eCalamp.Type == 1 || eCalamp.Type == 2 || eCalamp.Type == 3 || eCalamp.Type == 4 || eCalamp.Type == 6)
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




            //ed.WriteMessage("MaxTension.Absolute= "+MaxTension.Absolute+"\n");
            //return MaxTension;
            return max;
            


        }
        public DataTable WindOnPole02()
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
            DataColumn dcPoleGuid = new DataColumn("dcPoleGuid");
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

            foreach (DataRow dr in dtPoleSection.Rows)
            {
                sw = 0;

                Atend.Base.Design.DWeather Weather = new Atend.Base.Design.DWeather();

                //Atend.Base.Design.DNode myNode = Atend.Base.Design.DNode.AccessSelectByCode((Guid)(dr["ProductCode"]));
                Atend.Base.Design.DPackage myPackage = Atend.Base.Design.DPackage.AccessSelectByNodeCode(dtPackageParam, (Guid)(dr["ProductCode"]));
                Atend.Base.Equipment.EPole pole = new Atend.Base.Equipment.EPole();
                switch ((Atend.Control.Enum.ProductType)myPackage.Type)
                {
                    case Atend.Control.Enum.ProductType.Pole:
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, myPackage.ProductCode);
                        break;

                    case Atend.Control.Enum.ProductType.PoleTip:
                        Atend.Base.Equipment.EPoleTip PoleTip = Atend.Base.Equipment.EPoleTip.AccessSelectByCode(dtPoleTipParm, myPackage.ProductCode);
                        pole = Atend.Base.Equipment.EPole.AccessSelectByCode(dtPoleParm, PoleTip.PoleCode);
                        break;
                }


                DataTable dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol), aConnection);
                int countOfCrossArm = dtPackage.Rows.Count;

                double WindOnConductor1 = 0;
                double WindOnPole1 = 0;
                double WindOnInsulator1 = 0;
                CVector TensionTangant = new CVector();
                CVector TensionDDE = new CVector();
                double[] Angle = new double[6];

                double Total = 0;
                dtPoleConductor.Rows.Clear();
                DataRow[] dtConductor = dtPoleCond.Select(string.Format("PoleGuid='{0}'", dr["ProductCode"].ToString()));
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
                double max = 0;
                int k = 0;
                for (int i = 0; i < 6; i++)
                {
                    Weather = Atend.Base.Design.DWeather.AccessSelectByIsSelectedConditionCode(1, i + 1, aConnection);
                    //ed.WriteMessage("ProductCode= "+dr["ProductCode"].ToString()+"\n");

                    //ed.WriteMessage("dtPoleConductor.Rows.Count= " + dtPoleConductor.Length.ToString() + "\n");
                    WindOnConductor1 = WindOnConductor02(dtPoleConductor, Weather, pole);
                    if (countOfCrossArm != 0)
                    {
                        WindOnInsulator1 = WindOnInsulator02(pole, Weather, countOfCrossArm);
                    }
                    //ed.WriteMessage("Insulator= " + WindOnInsulator1 + "\n");
                    WindOnPole1 = WindOnPoleF02(pole, Weather, dtPoleConductor);
                    TensionTangant = WindOnTangantPole02(Weather, dtPoleConductor, i);
                    TensionDDE = WindOnDDEPole02(Weather, dtPoleConductor, i);
                    Total = WindOnConductor1 + WindOnInsulator1 + WindOnPole1 + TensionTangant.Absolute + TensionDDE.Absolute;
                    temp[i] = Total;
                    Angle[i] = TensionDDE.Add(TensionDDE, TensionTangant).Angle;
                    if (temp[i] > max)
                    {
                        max = temp[i];
                        k = i;
                    }
                    if (i==2)
                    {
                        ed.WriteMessage("WindOn TangantPole={0}\n",TensionTangant.Absolute);
                    }
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
                dr1["dcPoleGuid"] = dr["ProductCode"].ToString();
                dr1["dcAngle"] = Math.Round(Convert.ToDouble(Angle[k].ToString()), 2);
                dtTable.Rows.Add(dr1);
                //ed.WriteMessage("Add Row\n");

            }
            //ed.WriteMessage("Finish Calculating\n");
            return dtTable;
        }
        public double IsSagOk02()
        {

         
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

            double poleHeight = calcAvrageOfPoleHeight();

            double Depth = .1 * poleHeight + .6;
            double ke=1;

            //ed.WriteMessage("Ke= " + ke.ToString() + "\n");
            //ed.WriteMessage("Ke= "+ke.ToString()+"\n");
            double fmax1 = 0;
            DataTable dtPackage = new DataTable();
            dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            if (dtPackage.Rows.Count != 0)
            {
                //ed.WriteMessage("DistanceSupport={0},Clearance={1},ZaribEtminan={2}\n",calamp.DistanceSupport,Clearance,ZaribEtminan);
                fmax1 = poleHeight - (Depth + Calamp.DistanceSupport + clearance + ZaribEtminan);
                //ed.WriteMessage("FMAX!={0}\n",fmax1);
            }

            //ed.WriteMessage("Volt= " + Vn + " Consol.DistancePhase= " + Consol.DistancePhase + "lenghtChain =" + LenghtChain + " ke="+ke+"\n");
            double fmax2 = 0;
            double FMAX = fmax1;
            dtPackage = Atend.Base.Design.DPackage.AccessSelectByParentCodeAndType(myPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
            if (dtPackage.Rows.Count != 0)
            {

                //Atend.Base.Equipment.EConsol consol = Atend.Base.Equipment.EConsol.AccessSelectByCode(Convert.ToInt32(dtPackage.Rows[0]["ProductCode"]));

                double Vn = Volt / 1000;
                ke = ComputeKe02();
                //ed.WriteMessage("Depth={0},LenghtChain={1},DistanceCrossArm={2},Clearance={3},ZaribEtminan={4}\n",Depth,LenghtChain,consol.DistanceCrossArm,Clearance,ZaribEtminan);
                //ed.WriteMessage("Ins={0}\n", insulator.LenghtInsulatorChain);
                fmax1 = poleHeight - (Depth +/*insulator.LenghtInsulatorChain*/.45 + Consol.DistanceCrossArm + clearance + ZaribEtminan);
                //ed.WriteMessage("fMAx1={0}\n",fmax1);
                //ed.WriteMessage("DistancePhase={0},vn={1},ke={2}\n",consol.DistancePhase,Vn,ke);
                fmax2 = (Math.Pow(((Consol.DistancePhase - (((Consol.VoltageLevel) / 1000) / 150)) / ke), 2)) - .45 /*insulator.LenghtInsulatorChain*/;
                if (fmax1 < fmax2)
                    FMAX = fmax1;
                else
                    FMAX = fmax2;
            }

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
            ed.WriteMessage("PoleHeiht={0},dtPoleSection={1}\n", PoleHeight, DtPoleSection.Rows.Count);
            return PoleHeight;
        }
        private double ComputeKe02()
        {
            ed.WriteMessage("Tyep={0},CrossSection={1}\n", Atend.Global.Calculation.Mechanical.CCommon.Type, Atend.Global.Calculation.Mechanical.CCommon.CrossSectionArea);
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
    }
}
