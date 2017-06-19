using System;
using System.Collections.Generic;
using System.Text;

namespace Atend.Control
{
    public class Enum
    {
        public enum ProductType : int
        {
            Empty = 1,
            CatOut = 2,
            Phuse = 3,
            Rod = 4,
            AuoKey3p = 5,
            Disconnector = 6,
            Breaker = 7,
            CT = 8,
            PT = 9,
            PhotoCell = 10,
            Countor = 11,
            Bus = 12,
            HeaderCabel = 13,
            ErrorFlag = 14,
            Arm = 15,
            MeasuredJackPanel = 16,
            Transformer = 17,
            WeekJackPanel = 18,
            Conductor = 19,
            Light = 20,
            Pole = 21,
            Insulator = 22,
            GroundCabel = 23,
            StreetBox = 24,
            DB = 25,
            Operation = 26,
            MiddleJackPanel = 27,
            ReCloser = 28,
            PhuseKey = 29,
            MiniatureKey = 30,
            Khazan = 31,
            Mafsal = 32,
            KablSho = 33,
            Kalamp = 34,
            Consol = 35,
            PhusePole = 36,
            Prop = 37,
            Floor = 38,
            Ramp = 39,
            Ground = 40,
            InsulatorChain = 41,
            InsulatorPipe = 42,
            Halter = 43,
            SectionLizer = 44,
            SelfKeeper = 45,
            ConnectionPoint = 47,
            Connection = 48,
            Comment = 49,
            TensionArc = 50,
            ConsolElse = 51,
            BankKhazan = 52,
            Else = 53,
            Key = 69,
            LoadComment = 70,
            Cell = 71,
            KeyElse = 72,
            PoleTip = 73,
            ConductorTip = 76,
            SelfKeeperTip = 77,
            GroundCabelTip = 79,
            ForbiddenArea = 85,
            Frame = 89,
            FrameInformation = 90,
            FrameDescription = 91,
            FrameAbraviation = 92,
            AirPost = 93,
            GroundPost = 94,
            Jumper = 95,
            Terminal = 96,
            Description = 97,
            BusTerminal=98,
            DisconnectorCell=99,
            BreakerCell=100,
            UnknownKey=101,
        }


        public enum FrameProductType : int
        {
            Pole = 1,
            PoleCircle = 2,
            PolePolygon = 3,
            GroundPostOn = 4,
            GroundPostUnder = 5,
            GroundPostKiusk = 6,
            GroundCable = 7,
            Conductor = 8,
            SelfKeeper = 9,
            Disconnector = 10,
            Breaker = 11,
            HeaderCable = 12,
            Klamp = 13,
            Kablsho = 14,
            AirPost = 15,
        }



        public enum SettingType : int
        {
            JarianPakhsheBar = 1,
            PValuePakhsheBar = 2,
            QValuePakhsheBar = 3,
            CospValuePakhsheBar = 4
        }

        public enum EquipmentSettingType : int
        {
            JarianPakhsheBar = 1,
            PValuePakhsheBar = 2,
            QValuePakhsheBar = 3,
            CospValuePakhsheBar = 4,
            Vol = 5,
            VolAngle = 6
        }

        public enum EquipmentType : byte
        {
            Node = 0,
            Branch = 1,
            Jumper = 2,
            Other = 3
        }


        public enum BranchMode : byte
        {
            Phase = 1,
            Night = 2,
            Netural = 3
        }


        public enum circuitType : byte
        {
            kv20 = 0,
            other = 1
        }

        public enum netType : byte
        {

        }

        public enum lenghtUnitType : byte
        {


        }

        public enum DesignSettingType : byte
        {
            LastNodeNumber = 5,
            LastBranchNumber = 6
        }

        public enum Netform : byte
        {
            Vertical = 0,
            Horizontal = 2,
            Triangular = 1
        }



        public enum AutoCadLayerName : int
        {

            MED_AIR = 1,
            MED_GROUND = 124,
            LOW_AIR = 3,
            LOW_GROUND = 4,
            POST = 5,
            GENERAL = 193,
            SHELL = 153,
            DESCRIPTION = 210,

        }


        // colorIndex=1 RED
        // colorIndex=6 MAGENTA
        // colorIndex=5 DrakBlue
        public enum CircuteColor : int
        {
            CircuteOne = 1,
            CircuteTwo = 6,
            CircuteThree = 5

        }

        public enum Setting : int
        {
            /// <summary>
            /// ارتباط با پشتیبان
            /// </summary>
            ConnectToPoshtiban = 1,
            /// <summary>
            /// به روز رسانی دستور کار
            /// </summary> 
            UpdateProduct = 2,
            /// <summary>
            /// به روز رسانی طرح
            /// </summary>
            UpdateDesign = 3,
            /// <summary>
            /// نمایش تجهیز فاقد کد مالی
            /// </summary>
            ShowProduct = 4,
            /// <summary>
            /// تعیین مسیر پیش فرض جهت باز کردن فایل
            /// </summary>
            //OpenDefaultPath=5

        }


    }
}
