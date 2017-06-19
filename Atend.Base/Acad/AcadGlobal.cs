using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Atend.Base.Acad
{
    public class AcadGlobal
    {
        public class MafsalData
        {
            public static Atend.Base.Design.DPackage MafsalInfo = new Atend.Base.Design.DPackage();
            public static int ProjectCode;
        }



        public class PoleData
        {
            //~~~~~~~~~~~~~~~~~~~ New Part ~~~~~~~~~~~~~~~~~~~~~//



            public static Atend.Base.Design.DPoleInfo dPoleInfo = new Atend.Base.Design.DPoleInfo();

            public static bool UseAccess;

            public static Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();

            public static Atend.Base.Equipment.EPoleTip ePoleTip = new Atend.Base.Equipment.EPoleTip();

            public static List<Atend.Base.Equipment.EConsol> eConsols = new List<Atend.Base.Equipment.EConsol>();

            public static ArrayList eConsolUseAccess = new ArrayList();

            public static ArrayList eConsolExistance = new ArrayList();

            public static ArrayList eConsolProjectCode = new ArrayList();

            public static ArrayList eConsolCount = new ArrayList();

            public static int Existance;

            public static int ProjectCode;

            public static double Height;

            public static int HalterExistance;

            public static int HalterProjectCode;

            public static Atend.Base.Equipment.EHalter eHalter = new Atend.Base.Equipment.EHalter();

            public static int eHalterCount;


        }


        public class CirclePoleData
        {
            //~~~~~~~~~~~~~~~~~~~ New Part ~~~~~~~~~~~~~~~~~~~~~//


            public static Atend.Base.Design.DPoleInfo dPoleInfo = new Atend.Base.Design.DPoleInfo();

            public static bool UseAccess;

            public static Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();

            public static Atend.Base.Equipment.EPoleTip ePoleTip = new Atend.Base.Equipment.EPoleTip();

            public static ArrayList eConsolProjectCode = new ArrayList();

            public static List<Atend.Base.Equipment.EConsol> eConsols = new List<Atend.Base.Equipment.EConsol>();

            public static ArrayList eConsolUseAccess = new ArrayList();

            public static ArrayList eConsolExistance = new ArrayList();

            public static ArrayList eConsolCount = new ArrayList();

            public static int Existance;

            public static int ProjectCode;

            public static double Height;

            public static int HalterExistance;

            public static int HalterProjectCode;

            public static Atend.Base.Equipment.EHalter eHalter = new Atend.Base.Equipment.EHalter();

            public static int eHalterCount;


        }


        public class BreakerData
        {
            public static bool UseAccess;

            public static Atend.Base.Equipment.EBreaker eBreaker = new Atend.Base.Equipment.EBreaker();

            public static int Existance;

            public static int ProjectCode;
        }


        public class PolygonPoleData
        {
            //~~~~~~~~~~~~~~~~~~~ New Part ~~~~~~~~~~~~~~~~~~~~~//


            public static Atend.Base.Design.DPoleInfo dPoleInfo = new Atend.Base.Design.DPoleInfo();

            public static bool UseAccess;

            public static Atend.Base.Equipment.EPole ePole = new Atend.Base.Equipment.EPole();

            public static Atend.Base.Equipment.EPoleTip ePoleTip = new Atend.Base.Equipment.EPoleTip();

            public static List<Atend.Base.Equipment.EConsol> eConsols = new List<Atend.Base.Equipment.EConsol>();

            public static ArrayList eConsolProjectCode = new ArrayList();

            public static ArrayList eConsolUseAccess = new ArrayList();

            public static ArrayList eConsolExistance = new ArrayList();

            public static ArrayList eConsolCount = new ArrayList();

            public static int Existance;

            public static int ProjectCode;

            public static double Height;

            public static int HalterExistance;

            public static int HalterProjectCode;

            public static Atend.Base.Equipment.EHalter eHalter = new Atend.Base.Equipment.EHalter();

            public static int eHalterCount;


        }

        public class ConsolData
        {
            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EConsol eConsol;

            public static int ProjectCode;

        }

        public class ConductorData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EConductorTip eConductorTip;

            public static List<Atend.Base.Equipment.EConductor> eConductors = new List<Atend.Base.Equipment.EConductor>();

            public static int ProjectCode;

        }


        public class HeaderCableData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EHeaderCabel eHeaderCable;

            //public static List<Atend.Base.Equipment.EHeaderCabel> eHeaderCables = new List<Atend.Base.Equipment.EHeaderCabel>();

            public static int ProjectCode;
        }


        public class ClampData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EClamp eClamp;

            public static int ProjectCode;
        }

        public class KablshoData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EKablsho eKablsho;

            public static int ProjectCode;
        }



        public class SelfKeeperData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.ESelfKeeperTip eSelfKeeperTip;

            public static List<Atend.Base.Equipment.ESelfKeeper> eSelfKeepers = new List<Atend.Base.Equipment.ESelfKeeper>();

            public static int ProjectCode;
        }


        public class RodData
        {
            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.ERod eRod;


            public static int ProjectCode;

            // public static Atend.Base.Design.DPackage dPackageForRod = new Atend.Base.Design.DPackage();
        }


        //public class DBData
        //{
        //    public static bool UseAccess;

        //    public static int Existance;

        //    public static Atend.Base.Equipment.EDB eDB;


        //    public static int ProjectCode;

        //    // public static Atend.Base.Design.DPackage dPackageForRod = new Atend.Base.Design.DPackage();
        //}

        public class KhazanData
        {

            public static Atend.Base.Equipment.EKhazanTip eKhazanTip = new Atend.Base.Equipment.EKhazanTip();

            public static int Existance = 0;

            public static bool UseAccess = false;

            public static int ProjectCode;
        }



        public class MiddleJackPanelData
        {
            public static ArrayList eJackPanelCells = new ArrayList();

            public static int ProjectCode;
        }

        public class SecsionerCellData
        {
            public static Atend.Base.Equipment.ECell eCell = new Atend.Base.Equipment.ECell();

            public static int ProjectCode;
        }

        public class DezhangtorCellData
        {
            public static Atend.Base.Equipment.ECell eCell = new Atend.Base.Equipment.ECell();

            public static int ProjectCode;
        }

        public class SecsionerBusCouplerCellData
        {
            public static Atend.Base.Equipment.ECell eCell = new Atend.Base.Equipment.ECell();

            public static int ProjectCode;
        }

        public class DezhabgtorBusCouplerCellData
        {
            public static Atend.Base.Equipment.ECell eCell = new Atend.Base.Equipment.ECell();

            public static int ProjectCode;
        }


        public class GroundPostData
        {
            public static bool UseAccess;

            public static Atend.Base.Equipment.EGroundPost eGroundPost = new Atend.Base.Equipment.EGroundPost();

            public static List<Atend.Base.Equipment.EJAckPanel> eJackPanelMiddles = new List<Atend.Base.Equipment.EJAckPanel>();

            public static List<Atend.Base.Equipment.EJackPanelWeek> eJackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();

            public static List<Atend.Base.Equipment.ETransformer> eTransformers = new List<Atend.Base.Equipment.ETransformer>();

            public static int Existance = 0;

            public static ArrayList eJackPanelMiddleExistance = new ArrayList();

            public static ArrayList eJackPanelMiddleProjectCode = new ArrayList();

            public static ArrayList eJackPanelWeekExistance = new ArrayList();

            public static ArrayList eJackPanelWeekProjectCode = new ArrayList();

            public static ArrayList eTransformerExistance = new ArrayList();

            public static ArrayList eTransformerProjectCode = new ArrayList();

            //public static Atend.Global.Acad.DrawEquips.AcDrawGroundPost _AcDrawGroundPost = new Atend.Global.Acad.DrawEquips.AcDrawGroundPost();

            public static int ProjectCode;
        }




        public class AirPostData
        {
            public static bool UseAccess;

            public static Atend.Base.Equipment.EAirPost eAirPost = new Atend.Base.Equipment.EAirPost();

            public static List<Atend.Base.Equipment.EJackPanelWeek> eJackPanelWeeks = new List<Atend.Base.Equipment.EJackPanelWeek>();

            public static List<Atend.Base.Equipment.ETransformer> eTransformers = new List<Atend.Base.Equipment.ETransformer>();

            public static int Existance = 0;

            public static ArrayList eJackPanelWeekExistance = new ArrayList();

            public static ArrayList eJackPanelWeekProjectCode = new ArrayList();

            public static ArrayList eTransformerExistance = new ArrayList();

            public static ArrayList eTransformerProjectCode = new ArrayList();

            public static int ProjectCode;
        }



        public class StreetBoxData
        {

            public static bool UseAccess;

            public static List<Atend.Base.Equipment.EStreetBoxPhuse> eStreetBoxPhuse = new List<Atend.Base.Equipment.EStreetBoxPhuse>();

            public static Atend.Base.Equipment.EStreetBox eStreetBox = new Atend.Base.Equipment.EStreetBox();

            public static int Existance = 0;

            public static int ProjectCode;
        }

        public class CatOutData
        {
            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.ECatOut eCatOut = new Atend.Base.Equipment.ECatOut();

            public static int ProjectCode;
        }

        public class DisConnectorData
        {
            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EDisconnector eDisConnector;

            public static int ProjectCode;
        }

        public class GroundCableData
        {
            public static bool UseAccess;

            public static int Existance;

            public static int ProjectCode;

            public static Atend.Base.Equipment.EGroundCabelTip eGroundCabelTip;

            public static List<Atend.Base.Equipment.EGroundCabel> eGroundCabels = new List<Atend.Base.Equipment.EGroundCabel>();

        }


        public class DBData
        {

            public static bool UseAccess;

            public static int Existance;

            public static int ProjectCode;

            public static Atend.Base.Equipment.EDB eDB = new Atend.Base.Equipment.EDB();

            public static List<Atend.Base.Equipment.EDBPhuse> eDBPhuse = new List<Atend.Base.Equipment.EDBPhuse>();

        }


        public class GroundData
        {
            public static bool UseAccess;

            public static int Existance;

            public static int ProjectCode;

            public static Atend.Base.Equipment.EGround eGround = new Atend.Base.Equipment.EGround();
        }


        public class LightData
        {
            public static bool UseAccess;

            public static int Existance;

            public static int ProjectCode;

            public static Atend.Base.Equipment.ELight eLight = new Atend.Base.Equipment.ELight();
        }

        public class MeasuredJackPanelData
        {
            public static int ProjectCode;

            public static int Existance;

            public static bool UseAccess;

            public static Atend.Base.Equipment.EMeasuredJackPanel eMeasuredJackPanel = new Atend.Base.Equipment.EMeasuredJackPanel();

        }


        public class FrameData
        {
            public static double Width;

            public static double Height;

            public static System.Data.DataTable Products = new System.Data.DataTable();

            public static bool HaveSign;

            public static  bool HaveDescription;

            public static bool HaveInformation;
            
        }

        public class TerminalData
        {

            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EConductorTip eConductorTip;

            public static List<Atend.Base.Equipment.EConductor> eConductors = new List<Atend.Base.Equipment.EConductor>();

            public static int ProjectCode;


        }

        public class BusData
        {
            public static bool UseAccess;

            public static int Existance;

            public static Atend.Base.Equipment.EBus eBus;

            public static int ProjectCode;

        }

    }
}
