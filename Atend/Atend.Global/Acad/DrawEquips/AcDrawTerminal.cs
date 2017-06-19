using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawTerminal
    {
        Atend.Base.Design.DBranch _dBranch;

        private bool _UseAccess;
        public bool UseAccess
        {
            get { return _UseAccess; }
            set { _UseAccess = value; }
        }

        int existance;
        public int Existance
        {
            get { return existance; }
            set { existance = value; }
        }

        int projectCode;
        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        Atend.Base.Equipment.EConductorTip _eConductorTip;
        public Atend.Base.Equipment.EConductorTip eConductorTip
        {
            get { return _eConductorTip; }
            set { _eConductorTip = value; }
        }

        List<Atend.Base.Equipment.EConductor> _eConductors;
        public List<Atend.Base.Equipment.EConductor> eConductors
        {
            get { return _eConductors; }
            set { _eConductors = value; }
        }


        class DrawTerminalJig : DrawJig
        {

            public bool IsFirstPoint = false;
            public bool IsSecondPoint = false;
            public Point3d Apoint = new Point3d();
            List<Entity> Entities = new List<Entity>();


            public DrawTerminalJig()
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                Apoint = Point3d.Origin;
                Line l = new Line();
                l.StartPoint = new Point3d(10, 10, 0);
                l.EndPoint = new Point3d(100, 100, 0);
                Entities.Add(l);
                //ed.WriteMessage("constracted \n");
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                JigPromptPointOptions ppo = new JigPromptPointOptions();
                PromptPointResult ppr;

                if (IsFirstPoint == true && IsSecondPoint == false)
                {
                    ppo.Message = "\nSelectFirstPoint";
                    ppr = prompts.AcquirePoint(ppo);
                    if (ppr.Status == PromptStatus.OK)
                    {
                        if (Apoint == ppr.Value)
                        {
                            return SamplerStatus.NoChange;
                        }
                        else
                        {
                            Apoint = ppr.Value;
                            return SamplerStatus.OK;
                        }
                    }
                    else
                    {
                        return SamplerStatus.Cancel;
                    }
                }
                else if (IsFirstPoint == false && IsSecondPoint == true)
                {
                    ppo.Message = "\nSelectSecondPoint";
                    ppr = prompts.AcquirePoint(ppo);
                    if (ppr.Status == PromptStatus.OK)
                    {
                        if (Apoint.Equals(ppr.Value))
                        {
                            return SamplerStatus.NoChange;
                        }
                        else
                        {
                            Apoint = ppr.Value;
                            return SamplerStatus.OK;
                        }
                    }
                    else
                    {
                        return SamplerStatus.Cancel;
                    }
                }

                return SamplerStatus.NoChange;
            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {



                Line l = Entities[0] as Line;
                if (l != null)
                {
                    if (IsFirstPoint == true && IsSecondPoint == false)
                    {
                        l.StartPoint = Apoint;
                        l.EndPoint = Apoint;
                    }
                    else if (IsFirstPoint == false && IsSecondPoint == true)
                    {
                        l.EndPoint = Apoint;
                    }
                }

                foreach (Entity ent in Entities)
                {
                    draw.Geometry.Draw(ent);
                }
                return true;
            }

            public List<Entity> GetEntities()
            {
                return Entities;
            }

            public void SetStartPoint(Point3d StartPoint)
            {
                Line l = Entities[0] as Line;
                if (l != null)
                {
                    l.StartPoint = StartPoint;
                }
            }

            public void SetEndPoint(Point3d EndPoint)
            {
                Line l = Entities[0] as Line;
                if (l != null)
                {
                    l.EndPoint = EndPoint;
                }
            }
        }


        public AcDrawTerminal()
        {
            _dBranch = new Atend.Base.Design.DBranch();
            _eConductors = new List<Atend.Base.Equipment.EConductor>();
        }

        public void DrawTerminal()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti1 = true;
            bool conti2 = true;
            //bool TimeToSetStartPoint = true; ;
            //bool TimeToSetEndPoint = false;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Terminal).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Terminal).CommentScale;
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Terminal);

            Atend.Base.Acad.AT_INFO StartInfo = new Atend.Base.Acad.AT_INFO();
            Atend.Base.Acad.AT_INFO EndInfo = new Atend.Base.Acad.AT_INFO();

            ObjectId StartOI = ObjectId.Null;
            ObjectId EndOI = ObjectId.Null;

            try
            {
                DrawTerminalJig _DrawTerminalJig = new DrawTerminalJig();
                while (conti1)
                {
                    _DrawTerminalJig.IsFirstPoint = true;
                    _DrawTerminalJig.IsSecondPoint = false;
                    PromptResult pr = ed.Drag(_DrawTerminalJig);
                    if (pr.Status == PromptStatus.OK)
                    {
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(_DrawTerminalJig.Apoint);
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                StartOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));

                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                Point3d AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));

                                _DrawTerminalJig.SetStartPoint(AcceptedCenterPoint);
                                //ParentCode = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI).NodeCode;
                            }
                        }
                        //ed.WriteMessage("StartOI:{0}\n", StartOI);
                        if (StartOI != ObjectId.Null)
                        {

                            //if ProductCode = 0
                            StartInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(StartOI);
                            if (StartInfo.ParentCode != "NONE" && StartInfo.ProductCode == 0)
                            {
                                UpdateOtherNodeData(StartInfo);
                            }
                            conti1 = false;
                        }
                        else
                        {
                            string s = "";
                            foreach (DataRow dr in Parents.Rows)
                            {
                                s = s + Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["ContainerCode"])).ProductName + "-";
                            }
                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            notification.Title = "اتصالات مجاز";
                            notification.Msg = s;
                            notification.infoCenterBalloon();

                        }
                    }
                    else
                    {
                        conti1 = false;
                        throw new System.Exception("ESC pressed");
                    }
                }//while (conti1)
                while (conti2)
                {
                    _DrawTerminalJig.IsFirstPoint = false;
                    _DrawTerminalJig.IsSecondPoint = true;
                    PromptResult pr = ed.Drag(_DrawTerminalJig);
                    if (pr.Status == PromptStatus.OK)
                    {
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(_DrawTerminalJig.Apoint);
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                EndOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));

                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                Point3d AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));

                                _DrawTerminalJig.SetEndPoint(AcceptedCenterPoint);

                                //ParentCode = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI).NodeCode;
                            }
                        }
                        //ed.WriteMessage("EndOI:{0}\n", EndOI);
                        if (EndOI != ObjectId.Null)
                        {

                            EndInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(EndOI);
                            if (EndInfo.ParentCode != "NONE" && EndInfo.ProductCode == 0)
                            {
                                UpdateOtherNodeData(EndInfo);
                            }

                            conti2 = false;
                        }
                        else
                        {
                            string s = "";
                            foreach (DataRow dr in Parents.Rows)
                            {
                                s = s + Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["ContainerCode"])).ProductName + "-";
                            }
                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            notification.Title = "اتصالات مجاز";
                            notification.Msg = s;
                            notification.infoCenterBalloon();

                        }
                    }
                    else
                    {
                        conti2 = false;
                        throw new System.Exception("ESC pressed");
                    }
                }//while (conti1)
                //ed.WriteMessage("000\n");
                if (conti1 == false && conti2 == false && StartOI != ObjectId.Null && EndOI != ObjectId.Null)
                {
                    //draw and save data here
                    //ed.WriteMessage("111\n");
                    _dBranch.RightNodeCode = new Guid(StartInfo.NodeCode);
                    _dBranch.LeftNodeCode = new Guid(EndInfo.NodeCode);
                    if (SaveTerminalData())
                    {

                        //determine voltage
                        //ed.WriteMessage("111\n");
                        string LayerName = "";
                        if (StartInfo.ParentCode != "NONE" && EndInfo.ParentCode != "NONE")
                        {
                            double StartVoltage = 0;
                            double EndVoltage = 0;

                            switch ((Atend.Control.Enum.ProductType)StartInfo.NodeType)
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol _EConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EConsol.Code != -1)
                                    {
                                        StartVoltage = _EConsol.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("Econsol data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:
                                    Atend.Base.Equipment.EHeaderCabel _EHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EHeaderCabel.Code != -1)
                                    {
                                        StartVoltage = 11000;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EHeaderCabel data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp _EClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EClamp.Code != -1)
                                    {
                                        StartVoltage = _EClamp.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EClamp data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho _EKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EKablsho.Code != -1)
                                    {
                                        StartVoltage = 400;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EKablsho data failed");
                                    }
                                    break;
                            }


                            switch ((Atend.Control.Enum.ProductType)EndInfo.NodeType)
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol _EConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EConsol.Code != -1)
                                    {
                                        EndVoltage = _EConsol.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("Econsol data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:
                                    Atend.Base.Equipment.EHeaderCabel _EHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EHeaderCabel.Code != -1)
                                    {
                                        EndVoltage = 11000;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EHeaderCabel data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp _EClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EClamp.Code != -1)
                                    {
                                        EndVoltage = _EClamp.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EClamp data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho _EKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EKablsho.Code != -1)
                                    {
                                        EndVoltage = 400;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EKablsho data failed");
                                    }
                                    break;
                            }

                            //ed.WriteMessage("v1:{0} v2:{1} \n", StartVoltage, EndVoltage);
                            //ed.WriteMessage("222\n");
                            if (StartVoltage == EndVoltage && StartVoltage == 400)
                            {
                                //if (StartVoltage == 400)
                                //{
                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString();
                                //}
                                //else
                                //{
                                //    LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();

                                //}
                            }
                            else if (StartVoltage != 400)
                            {

                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();

                            }
                            else
                            {
                                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                notification.Title = "ترسیم ترمینال";
                                notification.Msg = "ولتاز مجاز نمی باشد";
                                notification.infoCenterBalloon();

                                throw new System.Exception("Voltage was not same");
                            }

                        }
                        else
                        {
                            throw new System.Exception("Lack of data on drawing");
                        }

                        //ed.WriteMessage("333 : {0} \n", LayerName);
                        ObjectId TerminalOi = ObjectId.Null;
                        List<Entity> Entities = _DrawTerminalJig.GetEntities();
                        foreach (Entity ent in Entities)
                        {
                            TerminalOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, LayerName);
                        }
                        if (TerminalOi != ObjectId.Null)
                        {
                            Atend.Base.Acad.AT_INFO TerminalInfo = new Atend.Base.Acad.AT_INFO(TerminalOi);
                            TerminalInfo.NodeCode = _dBranch.Code.ToString();
                            TerminalInfo.ParentCode = "";
                            TerminalInfo.NodeType = (int)Atend.Control.Enum.ProductType.Terminal;
                            TerminalInfo.ProductCode = _dBranch.ProductCode;
                            TerminalInfo.Insert();

                            Atend.Base.Acad.AT_SUB TerminalSub = new Atend.Base.Acad.AT_SUB(TerminalOi);
                            TerminalSub.SubIdCollection.Add(StartOI);
                            TerminalSub.SubIdCollection.Add(EndOI);
                            TerminalSub.Insert();

                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(TerminalOi, StartOI);
                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(TerminalOi, EndOI);

                        }
                    }
                }
                else
                {
                    throw new System.Exception("Lack of information");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Draw Terminal : {0} \n", ex.Message);
            }
        }

        public void DrawTerminal(Point3d StartPoint, Point3d EndPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti1 = true;
            bool conti2 = true;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Terminal).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Terminal).CommentScale;
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Terminal);

            Atend.Base.Acad.AT_INFO StartInfo = new Atend.Base.Acad.AT_INFO();
            Atend.Base.Acad.AT_INFO EndInfo = new Atend.Base.Acad.AT_INFO();

            ObjectId StartOI = ObjectId.Null;
            ObjectId EndOI = ObjectId.Null;

            try
            {
                DrawTerminalJig _DrawTerminalJig = new DrawTerminalJig();
                while (conti1)
                {
                    _DrawTerminalJig.IsFirstPoint = true;
                    _DrawTerminalJig.IsSecondPoint = false;
                    PromptResult pr = ed.Drag(_DrawTerminalJig);
                    if (pr.Status == PromptStatus.OK)
                    {
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(_DrawTerminalJig.Apoint);
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                StartOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));

                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                Point3d AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));

                                _DrawTerminalJig.SetStartPoint(AcceptedCenterPoint);
                            }
                        }
                        if (StartOI != ObjectId.Null)
                        {

                            StartInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(StartOI);
                            if (StartInfo.ParentCode != "NONE" && StartInfo.ProductCode == 0)
                            {
                                UpdateOtherNodeData(StartInfo);
                            }
                            conti1 = false;
                        }
                        else
                        {
                            string s = "";
                            foreach (DataRow dr in Parents.Rows)
                            {
                                s = s + Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["ContainerCode"])).ProductName + "-";
                            }
                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            notification.Title = "اتصالات مجاز";
                            notification.Msg = s;
                            notification.infoCenterBalloon();

                        }
                    }
                    else
                    {
                        conti1 = false;
                        throw new System.Exception("ESC pressed");
                    }
                }//while (conti1)
                while (conti2)
                {
                    _DrawTerminalJig.IsFirstPoint = false;
                    _DrawTerminalJig.IsSecondPoint = true;
                    PromptResult pr = ed.Drag(_DrawTerminalJig);
                    if (pr.Status == PromptStatus.OK)
                    {
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(_DrawTerminalJig.Apoint);
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                EndOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));

                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                Point3d AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));

                                _DrawTerminalJig.SetEndPoint(AcceptedCenterPoint);
                            }
                        }
                        if (EndOI != ObjectId.Null)
                        {

                            EndInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(EndOI);
                            if (EndInfo.ParentCode != "NONE" && EndInfo.ProductCode == 0)
                            {
                                UpdateOtherNodeData(EndInfo);
                            }

                            conti2 = false;
                        }
                        else
                        {
                            string s = "";
                            foreach (DataRow dr in Parents.Rows)
                            {
                                s = s + Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["ContainerCode"])).ProductName + "-";
                            }
                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                            notification.Title = "اتصالات مجاز";
                            notification.Msg = s;
                            notification.infoCenterBalloon();

                        }
                    }
                    else
                    {
                        conti2 = false;
                        throw new System.Exception("ESC pressed");
                    }
                }//while (conti1)
                if (conti1 == false && conti2 == false && StartOI != ObjectId.Null && EndOI != ObjectId.Null)
                {
                    _dBranch.RightNodeCode = new Guid(StartInfo.NodeCode);
                    _dBranch.LeftNodeCode = new Guid(EndInfo.NodeCode);
                    if (SaveTerminalData())
                    {

                        string LayerName = "";
                        if (StartInfo.ParentCode != "NONE" && EndInfo.ParentCode != "NONE")
                        {
                            double StartVoltage = 0;
                            double EndVoltage = 0;

                            switch ((Atend.Control.Enum.ProductType)StartInfo.NodeType)
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol _EConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EConsol.Code != -1)
                                    {
                                        StartVoltage = _EConsol.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("Econsol data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:
                                    Atend.Base.Equipment.EHeaderCabel _EHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EHeaderCabel.Code != -1)
                                    {
                                        StartVoltage = 11000;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EHeaderCabel data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp _EClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EClamp.Code != -1)
                                    {
                                        StartVoltage = _EClamp.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EClamp data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho _EKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(StartInfo.ProductCode);
                                    if (_EKablsho.Code != -1)
                                    {
                                        StartVoltage = 400;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EKablsho data failed");
                                    }
                                    break;
                            }


                            switch ((Atend.Control.Enum.ProductType)EndInfo.NodeType)
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol _EConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EConsol.Code != -1)
                                    {
                                        EndVoltage = _EConsol.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("Econsol data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.HeaderCabel:
                                    Atend.Base.Equipment.EHeaderCabel _EHeaderCabel = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EHeaderCabel.Code != -1)
                                    {
                                        EndVoltage = 11000;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EHeaderCabel data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp _EClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EClamp.Code != -1)
                                    {
                                        EndVoltage = _EClamp.VoltageLevel;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EClamp data failed");
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho _EKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(EndInfo.ProductCode);
                                    if (_EKablsho.Code != -1)
                                    {
                                        EndVoltage = 400;
                                    }
                                    else
                                    {
                                        throw new System.Exception("EKablsho data failed");
                                    }
                                    break;
                            }

                            if (StartVoltage == EndVoltage && StartVoltage == 400)
                            {
                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString();
                            }
                            else if (StartVoltage != 400)
                            {

                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
                            }
                            else
                            {
                                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                notification.Title = "ترسیم ترمینال";
                                notification.Msg = "ولتاز مجاز نمی باشد";
                                notification.infoCenterBalloon();

                                throw new System.Exception("Voltage was not same");
                            }

                        }
                        else
                        {
                            throw new System.Exception("Lack of data on drawing");
                        }

                        ObjectId TerminalOi = ObjectId.Null;
                        List<Entity> Entities = _DrawTerminalJig.GetEntities();
                        foreach (Entity ent in Entities)
                        {
                            TerminalOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, LayerName);
                        }
                        if (TerminalOi != ObjectId.Null)
                        {
                            Atend.Base.Acad.AT_INFO TerminalInfo = new Atend.Base.Acad.AT_INFO(TerminalOi);
                            TerminalInfo.NodeCode = _dBranch.Code.ToString();
                            TerminalInfo.ParentCode = "";
                            TerminalInfo.NodeType = (int)Atend.Control.Enum.ProductType.Terminal;
                            TerminalInfo.ProductCode = _dBranch.ProductCode;
                            TerminalInfo.Insert();

                            Atend.Base.Acad.AT_SUB TerminalSub = new Atend.Base.Acad.AT_SUB(TerminalOi);
                            TerminalSub.SubIdCollection.Add(StartOI);
                            TerminalSub.SubIdCollection.Add(EndOI);
                            TerminalSub.Insert();

                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(TerminalOi, StartOI);
                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(TerminalOi, EndOI);

                        }
                    }
                }
                else
                {
                    throw new System.Exception("Lack of information");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Draw Terminal : {0} \n", ex.Message);
            }
        }

        private void UpdateOtherNodeData(Atend.Base.Acad.AT_INFO NodeInformation)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (NodeInformation.ParentCode != "NONE" && NodeInformation.ProductCode == 0)
            {
                switch ((Atend.Control.Enum.ProductType)NodeInformation.NodeType)
                {
                    case Atend.Control.Enum.ProductType.HeaderCabel:
                        if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
                        {
                            Atend.Global.Design.frmEdiDrawHeaderCable _frmDrawHeaderCable = new Atend.Global.Design.frmEdiDrawHeaderCable(0, 0);
                            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDrawHeaderCable) == System.Windows.Forms.DialogResult.OK)
                            {
                                //save kablsho data here
                                Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();

                                _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;

                                _AcDrawHeaderCabel.SaveHeaderCableDataForExternalCall(NodeInformation);
                            }
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Kalamp:
                        if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
                        {
                            Atend.Global.Design.frmEditDrawClamp _frmDrawClamp = new Atend.Global.Design.frmEditDrawClamp(0);
                            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDrawClamp) == System.Windows.Forms.DialogResult.OK)
                            {
                                //save kablsho data here
                                Atend.Global.Acad.DrawEquips.AcDrawKalamp _AcDrawKalamp = new AcDrawKalamp();

                                _AcDrawKalamp.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                _AcDrawKalamp.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                _AcDrawKalamp.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                _AcDrawKalamp.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;

                                _AcDrawKalamp.SaveKalampDataForExternalCall(NodeInformation);
                            }
                        }
                        break;
                    case Atend.Control.Enum.ProductType.KablSho:
                        if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
                        {
                            Atend.Global.Design.frmEditDrawKablSho _frmDrawKablSho = new Atend.Global.Design.frmEditDrawKablSho();
                            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_frmDrawKablSho) == System.Windows.Forms.DialogResult.OK)
                            {
                                //save kablsho data here
                                Atend.Global.Acad.DrawEquips.AcDrawKablsho _AcDrawKablsho = new AcDrawKablsho();

                                _AcDrawKablsho.UseAccess = Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess;
                                _AcDrawKablsho.ProjectCode = Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode;
                                _AcDrawKablsho.Existance = Atend.Base.Acad.AcadGlobal.KablshoData.Existance;
                                _AcDrawKablsho.eKablsho = Atend.Base.Acad.AcadGlobal.KablshoData.eKablsho;

                                _AcDrawKablsho.SaveKablshoDataForExternalCall(NodeInformation);
                            }
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Consol:
                        break;
                }
            }
        }

        private bool SaveTerminalData()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);


            //ed.WriteMessage("econductor count : {0} \n", eConductors.Count);

            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {

                    if (_UseAccess)
                    {
                        //_dBranch.IsExist = IsExist;
                        _dBranch.ProductCode = eConductorTip.Code;
                        _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.Terminal;
                        _dBranch.Sag = 0;
                        _dBranch.ProjectCode = ProjectCode;
                        _dBranch.Number = "Terminal";
                        _dBranch.IsExist = Existance;

                        if (_dBranch.AccessInsert(aTransaction, aConnection))
                        {
                            //do nothing
                        }
                        else
                        {
                            throw new System.Exception("dBranch.AccessInsert failed");
                        }
                    }
                    else
                    {
                        foreach (Atend.Base.Equipment.EConductor econductor in eConductors)
                        {
                            if (!econductor.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("econductor.AccessInsert failed");
                            }

                        }

                        eConductorTip.PhaseProductCode = eConductors[0].Code;
                        eConductorTip.NeutralProductCode = eConductors[1].Code;
                        eConductorTip.NightProductCode = eConductors[2].Code;
                        if (eConductorTip.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            _dBranch.Number = "Terminal";
                            _dBranch.ProductCode = eConductorTip.Code;
                            _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.Terminal;
                            _dBranch.ProjectCode = ProjectCode;
                            _dBranch.IsExist = Existance;

                            if (!_dBranch.AccessInsert(aTransaction, aConnection))
                            {
                                throw new System.Exception("dBranch.AccessInsert failed");
                            }
                        }
                        else
                        {
                            throw new System.Exception("eConductorTip.AccessInsert failed");
                        }
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("TerminalTransaction for insert failed 02 : {0} \n", ex1.Message));
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error TerminalTransaction for insert failed 01 : {0} \n", ex.Message));
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            //ed.WriteMessage("save finished \n");
            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.TerminalData.UseAccess = true;
            UseAccess = true;

            #endregion

            return true;

        }

        public static bool DeleteTerminalData(ObjectId TerminalOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //delete Terminal
                Atend.Base.Acad.AT_INFO Terminalinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(TerminalOI);
                if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(Terminalinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete dbranch\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Terminal : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteTerminal(ObjectId TerminalOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB TerminalSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(TerminalOI);
                foreach (ObjectId TerminalSubOI in TerminalSub.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO TerminalSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(TerminalSubOI);
                    if (TerminalSubInfo.ParentCode != "NONE" && (TerminalSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || TerminalSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho
                                                               || TerminalSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp || TerminalSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol))
                    {
                        Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(TerminalOI, TerminalSubOI);
                    }

                }
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(TerminalOI))
                {
                    throw new System.Exception("GRA while delete Terminal \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Terminal : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static void RotateTerminal(double LastAngleDegree, double NewAngleDegree, ObjectId PoleOI, ObjectId TerminalOI, Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    ObjectIdCollection TerminalSub = Atend.Global.Acad.UAcad.GetGroupSubEntities(TerminalOI);

                    foreach (ObjectId oi in TerminalSub)
                    {
                        //Atend.Base.Acad.AT_INFO KhazanSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //if (KhazanSubInfo.ParentCode != "NONE" && KhazanSubInfo.NodeType == (int)Atend.Control.Enum.PTerminaluctType.ConsolElse)
                        //{
                        Entity ent = tr.GetObject(oi, OpenMode.ForWrite) as Entity;
                        if (ent != null)
                        {

                            Matrix3d trans = Matrix3d.Rotation(((LastAngleDegree * Math.PI) / 180) * -1, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                            ent.TransformBy(trans);

                            trans = Matrix3d.Rotation(((NewAngleDegree * Math.PI) / 180), ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                            ent.TransformBy(trans);


                            ////}
                        }
                        //}
                    }
                    tr.Commit();
                }
            }
        }

    }
}
