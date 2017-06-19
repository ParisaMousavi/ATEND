﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;


namespace Atend.Global.Acad.DrawEquips
{


    public class AcDrawConductor
    {
        //~~~~~~~~~~~~~~~~~~~~~~ Properties ~~~~~~~~~~~~~~~~~~//

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

        //~~~~~~~~~~~~~~~~~~~~~~ Methods ~~~~~~~~~~~~~~~~~~//

        public AcDrawConductor()
        {
            _dBranch = new Atend.Base.Design.DBranch();
            _eConductors = new List<Atend.Base.Equipment.EConductor>();
        }

        private void ResetClass()
        {
            _dBranch = new Atend.Base.Design.DBranch();
        }

        public void DrawConductor(Point3d StartPoint, Point3d EndPoint, ObjectId Consol1, ObjectId Consol2, double MyLength)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ResetClass();
            //ed.WriteMessage("~~~~>>>~~~-1\n");
            bool NeedArc1 = false, NeedArc2 = false, IsWeek1 = false, IsWeek2 = false;
            ObjectId newArc1 = ObjectId.Null, newArc2 = ObjectId.Null;
            Atend.Base.Equipment.EConsol eConsol;

            //ed.WriteMessage("~~~~>>>~~~0\n");
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).CommentScale;

            //ed.WriteMessage("~~~~>>>~~~1\n");
            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol1);
            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
            {
                _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
                eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
                if (eConsol.Code == -1)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            //ed.WriteMessage("eConsol.VoltageLevel:{0} and eConsol.ConsolType:{1} \n", eConsol.VoltageLevel, eConsol.ConsolType);
            switch (eConsol.VoltageLevel)
            {
                case 400:
                    IsWeek1 = true;
                    break;
                default:
                    IsWeek1 = false;
                    break;
            }
            switch (eConsol.ConsolType)
            {
                case 0:
                    NeedArc1 = true;
                    break;
                case 1:
                    NeedArc1 = true;
                    break;
                default:
                    NeedArc1 = false;
                    break;
            }
            at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol2);
            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
            {
                _dBranch.RightNodeCode = new Guid(at_info.ParentCode);
                eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
                if (eConsol.Code == -1)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            switch (eConsol.VoltageLevel)
            {
                case 400:
                    IsWeek2 = true;
                    break;
                default:
                    IsWeek2 = false;
                    break;
            }
            switch (eConsol.ConsolType)
            {
                case 0:
                    NeedArc2 = true;
                    break;
                case 1:
                    NeedArc2 = true;
                    break;
                case 2:
                    NeedArc2 = false;
                    break;
                case 3:
                    NeedArc2 = false;
                    break;
            }
            Polyline pline = Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol1) as Polyline;
            if (pline != null)
            {
                Line LineEntity = new Line(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol1)), Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol2)));
                if (IsWeek1 == IsWeek2 && Consol1 != Consol2 && Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol1).ParentCode != Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol2).ParentCode)
                {
                    _dBranch.IsWeek = IsWeek2;
                    _dBranch.Lenght = MyLength;
                    _dBranch.Number = eConductorTip.Description;

                    #region save data here
                    if (SaveConductorData())
                    {
                        if (LineEntity != null)
                        {

                            string LayerName;
                            if (IsWeek1)
                            {
                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString();
                            }
                            else
                            {
                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
                            }


                            ObjectId DrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(LineEntity, LayerName);

                            Atend.Base.Acad.AT_INFO at_infoForConsol = new Atend.Base.Acad.AT_INFO(DrawnLine);
                            at_infoForConsol.ParentCode = "";
                            at_infoForConsol.NodeCode = _dBranch.Code.ToString();
                            at_infoForConsol.NodeType = (int)Atend.Control.Enum.ProductType.Conductor;
                            at_infoForConsol.ProductCode = _dBranch.ProductCode;
                            at_infoForConsol.Insert();


                            Entity EText = Atend.Global.Acad.UAcad.WriteNote(_dBranch.Number, LineEntity.StartPoint, LineEntity.EndPoint, MyCommentScale);
                            ObjectId DrawnText = Atend.Global.Acad.UAcad.DrawEntityOnScreen(EText, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                            Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB(DrawnLine);
                            at_sub.SubIdCollection.Add(DrawnText);
                            at_sub.SubIdCollection.Add(Consol1);
                            at_sub.SubIdCollection.Add(Consol2);
                            at_sub.Insert();

                            Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(DrawnText);
                            TextInfo.ParentCode = _dBranch.Code.ToString();
                            TextInfo.NodeCode = "";
                            TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                            TextInfo.ProductCode = 0;
                            TextInfo.Insert();

                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, Consol1);
                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, Consol2);

                            if (NeedArc1)
                            {
                                //ed.WriteMessage("need arc 1 \n");
                                Arc a = (Arc)CreateArcForSpecialConsol(Consol1);

                                newArc1 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
                                if (newArc1 != ObjectId.Null)
                                {
                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc1, Consol1);
                                    Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol1);
                                    Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
                                    at_info1.SelectedObjectId = newArc1;
                                    at_info1.ParentCode = Parentconsol.NodeCode;
                                    at_info1.NodeCode = "";
                                    at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
                                    at_info1.ProductCode = 0;
                                    at_info1.Insert();
                                }
                            }

                            if (NeedArc2)
                            {
                                //ed.WriteMessage("need arc 2 \n");
                                Arc a = (Arc)CreateArcForSpecialConsol(Consol2);

                                newArc2 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
                                if (newArc2 != ObjectId.Null)
                                {
                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc2, Consol2);
                                    Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Consol2);
                                    Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
                                    at_info1.SelectedObjectId = newArc2;
                                    at_info1.ParentCode = Parentconsol.NodeCode;
                                    at_info1.NodeCode = "";
                                    at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
                                    at_info1.ProductCode = 0;
                                    at_info1.Insert();

                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    #endregion
                }
                else
                {
                    ed.WriteMessage("\n دسته مقره انتخاب شده مجاز نمی باشد \n");

                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "ترسیم سیم";
                    notification.Msg = "دسته مقره انتخاب شده مجاز نمی باشد";
                    notification.infoCenterBalloon();

                }
                _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
            }
        }

        public void DrawConductor()
        {

            Point3dCollection p3Collection = new Point3dCollection();
            ResetClass();
            bool FirstPoint = true;
            bool conti = true;
            Point3d StartPoint = Point3d.Origin, sp;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            bool NeedArc1 = false, NeedArc2 = false, IsWeek1 = false, IsWeek2 = false;
            ObjectId newArc1 = ObjectId.Null, newArc2 = ObjectId.Null;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).CommentScale;

            ObjectId FirstConsol = ObjectId.Null, SecondConsol = ObjectId.Null;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions peo = new PromptEntityOptions("");
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Conductor);
            while (conti)
            {
                if (FirstPoint)
                {
                    peo.Message = "\nانتخاب دسته مقره اول" + "\n";
                    peo.SetRejectMessage("تجهیز اشتباه انتخاب شده است" + "\n");
                    peo.AddAllowedClass(typeof(Polyline), true);
                    PromptEntityResult per = ed.GetEntity(peo);
                    if (per.Status == PromptStatus.OK)
                    {
                        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                        FirstConsol = per.ObjectId;
                        int _MyVoltage = 0;
                        byte _MyType = 0;
                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", at_info.NodeType));
                        if (drs.Length > 0)
                        {
                            bool NodeIsValid = true;
                            switch (((Atend.Control.Enum.ProductType)at_info.NodeType))
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
                                    if (eConsol.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;
                                    }
                                    else
                                    {
                                        _MyVoltage = eConsol.VoltageLevel;
                                        _MyType = eConsol.ConsolType;
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(at_info.ProductCode);
                                    if (eClamp.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;
                                    }
                                    else
                                    {
                                        _MyVoltage = eClamp.VoltageLevel;
                                        _MyType = 2;
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(at_info.ProductCode);
                                    if (eKablsho.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;

                                    }
                                    else
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 2;
                                    }
                                    break;
                            }

                            //ed.WriteMessage("  >>>> at_info.ParentCode ={0} \n", at_info.ParentCode);
                            _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);

                            switch (_MyVoltage)
                            {
                                case 400:
                                    IsWeek1 = true;
                                    break;
                                default:
                                    IsWeek1 = false;
                                    break;
                            }

                            switch (_MyType)
                            {
                                case 0:
                                    NeedArc1 = true;
                                    Atend.Base.Acad.AT_SUB ConsolSub1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
                                    foreach (ObjectId oi in ConsolSub1.SubIdCollection)
                                    {
                                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                                            NodeIsValid = false;
                                    }
                                    break;
                                case 1:
                                    NeedArc1 = true;
                                    Atend.Base.Acad.AT_SUB ConsolSub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
                                    foreach (ObjectId oi in ConsolSub2.SubIdCollection)
                                    {
                                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                                            NodeIsValid = false;
                                    }
                                    break;
                                default:
                                    NeedArc1 = false;
                                    break;
                            }
                            if (NodeIsValid)
                                FirstPoint = false;
                            else
                                FirstPoint = true;

                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        conti = false;
                    }
                }
                else if (!FirstPoint)
                {
                    peo.Message = "\n انتخاب دسته مقره دوم \n";

                    PromptEntityResult per = ed.GetEntity(peo);

                    if (per.Status == PromptStatus.OK)
                    {
                        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                        SecondConsol = per.ObjectId;
                        int _MyVoltage = 0;
                        byte _MyType = 0;
                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", at_info.NodeType));
                        if (drs.Length > 0)
                        {
                            if (at_info.ParentCode != string.Empty)
                            {
                                _dBranch.RightNodeCode = new Guid(at_info.ParentCode);
                            }
                            else
                            {
                                _dBranch.RightNodeCode = new Guid();
                            }
                            bool NodeIsValid = true;
                            switch (((Atend.Control.Enum.ProductType)at_info.NodeType))
                            {
                                case Atend.Control.Enum.ProductType.Consol:
                                    Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
                                    if (eConsol.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;
                                    }
                                    else
                                    {
                                        _MyVoltage = eConsol.VoltageLevel;
                                        _MyType = eConsol.ConsolType;
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.Kalamp:
                                    Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(at_info.ProductCode);
                                    if (eClamp.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;
                                    }
                                    else
                                    {
                                        _MyVoltage = eClamp.VoltageLevel;
                                        _MyType = 2;
                                    }
                                    break;
                                case Atend.Control.Enum.ProductType.KablSho:
                                    Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(at_info.ProductCode);
                                    if (eKablsho.Code == -1)
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 0;
                                    }
                                    else
                                    {
                                        _MyVoltage = 0;
                                        _MyType = 2;
                                    }
                                    break;
                            }
                            switch (_MyVoltage)
                            {
                                case 400:
                                    IsWeek2 = true;
                                    break;
                                default:
                                    IsWeek2 = false;
                                    break;
                            }
                            switch (_MyType)
                            {
                                case 0:
                                    NeedArc2 = true;
                                    Atend.Base.Acad.AT_SUB ConsolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
                                    foreach (ObjectId oi in ConsolSub.SubIdCollection)
                                    {
                                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                                            NodeIsValid = false;
                                    }
                                    break;
                                case 1:
                                    Atend.Base.Acad.AT_SUB ConsolSub1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
                                    foreach (ObjectId oi in ConsolSub1.SubIdCollection)
                                    {
                                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                                            NodeIsValid = false;
                                    }

                                    NeedArc2 = true;
                                    break;
                                case 2:
                                    NeedArc2 = false;
                                    break;
                                case 3:
                                    NeedArc2 = false;
                                    break;
                            }
                            if (NodeIsValid)
                            {
                                Polyline pline = Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId) as Polyline;
                                if (pline != null)
                                {
                                    Line LineEntity = new Line(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(FirstConsol)), Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(SecondConsol)));

                                    if (IsWeek1 == IsWeek2 && FirstConsol != SecondConsol && Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(FirstConsol).ParentCode != Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SecondConsol).ParentCode)
                                    {
                                        _dBranch.IsWeek = IsWeek2;
                                        Atend.Global.Design.frmDrawBranch drawBranch = new Atend.Global.Design.frmDrawBranch(_dBranch.RightNodeCode, _dBranch.LeftNodeCode);
                                        drawBranch.Length = LineEntity.Length;
                                        if (Application.ShowModalDialog(drawBranch) == System.Windows.Forms.DialogResult.OK)
                                        {
                                            _dBranch.Lenght = drawBranch.Length;
                                            //ed.WriteMessage(" >>  length : {0} \n", _dBranch.Lenght);
                                            _dBranch.Number = eConductorTip.Description;

                                            #region save data here
                                            if (SaveConductorData())
                                            {
                                                if (LineEntity != null)
                                                {
                                                    string LayerName;
                                                    if (IsWeek1)
                                                    {
                                                        LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString();
                                                    }
                                                    else
                                                    {
                                                        LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
                                                    }


                                                    ObjectId DrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(LineEntity, LayerName);

                                                    Atend.Base.Acad.AT_INFO at_infoForConsol = new Atend.Base.Acad.AT_INFO();
                                                    at_infoForConsol.SelectedObjectId = DrawnLine;
                                                    at_infoForConsol.ParentCode = "";
                                                    at_infoForConsol.NodeCode = _dBranch.Code.ToString();
                                                    at_infoForConsol.NodeType = (int)Atend.Control.Enum.ProductType.Conductor;
                                                    at_infoForConsol.ProductCode = _dBranch.ProductCode;
                                                    at_infoForConsol.Insert();


                                                    Entity EText = Atend.Global.Acad.UAcad.WriteNote(_dBranch.Number, LineEntity.StartPoint, LineEntity.EndPoint, MyCommentScale);

                                                    ObjectId DrawnText = Atend.Global.Acad.UAcad.DrawEntityOnScreen(EText, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                                    Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
                                                    at_sub.SelectedObjectId = DrawnLine;
                                                    at_sub.SubIdCollection.Add(DrawnText);
                                                    at_sub.SubIdCollection.Add(FirstConsol);
                                                    at_sub.SubIdCollection.Add(SecondConsol);
                                                    at_sub.Insert();


                                                    Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(DrawnText);
                                                    TextInfo.ParentCode = _dBranch.Code.ToString();
                                                    TextInfo.NodeCode = "";
                                                    TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                                    TextInfo.ProductCode = 0;
                                                    TextInfo.Insert();


                                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, FirstConsol);

                                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, SecondConsol);



                                                    if (NeedArc1)
                                                    {
                                                        Arc a = (Arc)CreateArcForSpecialConsol(FirstConsol);
                                                        newArc1 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
                                                        //ed.WriteMessage("~~~>> ARC WAS NEEDED ~~~~~ \n");
                                                        if (newArc1 != ObjectId.Null)
                                                        {
                                                            //ed.WriteMessage("2 \n");
                                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc1, FirstConsol);

                                                            Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(FirstConsol);

                                                            Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
                                                            at_info1.SelectedObjectId = newArc1;
                                                            at_info1.ParentCode = Parentconsol.NodeCode;
                                                            at_info1.NodeCode = "";
                                                            at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
                                                            at_info1.ProductCode = 0;
                                                            at_info1.Insert();


                                                        }


                                                    }

                                                    if (NeedArc2)
                                                    {
                                                        Arc a = (Arc)CreateArcForSpecialConsol(SecondConsol);

                                                        newArc2 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
                                                        //ed.WriteMessage("1 \n");
                                                        if (newArc2 != ObjectId.Null)
                                                        {
                                                            //ed.WriteMessage("2 \n");
                                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc2, SecondConsol);

                                                            Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SecondConsol);

                                                            Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
                                                            at_info1.SelectedObjectId = newArc2;
                                                            at_info1.ParentCode = Parentconsol.NodeCode;
                                                            at_info1.NodeCode = "";
                                                            at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
                                                            at_info1.ProductCode = 0;
                                                            at_info1.Insert();

                                                        }

                                                    }

                                                    conti = false;

                                                }


                                            }
                                            else
                                            {
                                                return;
                                            }
                                            #endregion

                                        }
                                    }
                                    else
                                    {
                                        ed.WriteMessage("\n دسته مقره انتخاب شده مجاز نمی باشد \n");
                                        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                        notification.Title = "ترسیم سیم";
                                        notification.Msg = "دسته مقره انتخاب شده مجاز نمی باشد";
                                        notification.infoCenterBalloon();
                                    }
                                    if (at_info.ParentCode != string.Empty)
                                    {
                                        _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
                                    }
                                    else
                                    {
                                        _dBranch.LeftNodeCode = new Guid();
                                    }
                                }//

                            }
                        }
                        else
                        {
                            //return;
                        }
                    }
                    else
                    {
                        conti = false;
                    }
                }//end if for FirstPoint
            }// end of while
        }

        //public void DrawConductor02()
        //{

        //    Point3dCollection p3Collection = new Point3dCollection();
        //    ResetClass();
        //    bool FirstPoint = true;
        //    bool conti = true;
        //    Point3d StartPoint = Point3d.Origin, sp;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    bool NeedArc1 = false, NeedArc2 = false, IsWeek1 = false, IsWeek2 = false;
        //    ObjectId newArc1 = ObjectId.Null, newArc2 = ObjectId.Null;

        //    double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).Scale;
        //    double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Conductor).CommentScale;


        //    ObjectId FirstConsol = ObjectId.Null, SecondConsol = ObjectId.Null;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //PromptEntityOptions peo = new PromptEntityOptions("");
        //    PromptPointOptions PPo = new PromptPointOptions("انتخاب محل اتصال اول");
        //    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Conductor);
        //    while (conti)
        //    {
        //        if (FirstPoint)
        //        {
        //            //peo.Message = "\nانتخاب دسته مقره اول" + "\n";
        //            //peo.SetRejectMessage("تجهیز اشتباه انتخاب شده است" + "\n");
        //            //peo.AddAllowedClass(typeof(Polyline), true);
        //            //PromptEntityResult per = ed.GetEntity(peo);
        //            PromptPointResult ppr = ed.GetPoint(PPo);
        //            //if (per.Status == PromptStatus.OK)
        //            if (ppr.Status == PromptStatus.OK)
        //            {
        //                System.Data.DataTable InEnts = Atend.Global.Acad.Global.PointInsideWhichEntity(ppr.Value, true);
        //                //add subs here
        //                //foreach (DataRow dr in InEnts.Rows)
        //                //{
        //                //    Atend.Base.Acad.AT_SUB SubEntity = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(new ObjectId(int.Parse(dr["ObjectId"])));
        //                //    foreach (ObjectId oi in SubEntity.SubIdCollection)
        //                //    {
        //                //        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                //        DataRow ndr = InEnts.NewRow();
        //                //        ndr["ObjectId"] = oi.ToString().Substring(1, oi.ToString().Length - 2);
        //                //        ndr["Type"] = at_info.NodeType;
        //                //        ndr["CenterPoint"] = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                //        InEnts.Rows.Add(ndr);
        //                //    }
        //                //}
        //                Dictionary<ObjectId, double> Points = new Dictionary<ObjectId, double>();
        //                foreach (DataRow dr in InEnts.Rows)
        //                {
        //                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", dr["Type"]));
        //                    if (drs.Length > 0)
        //                    {
        //                        ObjectId oi = new ObjectId(int.Parse(dr["ObjectId"]));
        //                        if (!Points.ContainsKey(oi))
        //                        {
        //                            Point3d CenterPoint;
        //                            string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                            string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                            string y = StrTemp[1];
        //                            string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                            CenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));

        //                            Point3d p1 = CenterPoint;
        //                            Points.Add(new ObjectId(int.Parse(dr["ObjectId"])), p1.DistanceTo(ppr.Value));
        //                        }
        //                    }
        //                }

        //                ObjectId NearestOi = ObjectId.Null;
        //                double NearestDis = -1;
        //                foreach (ObjectId oi in Points.Keys)
        //                {
        //                    //ed.WriteMessage("OOOOOOOOOOOOOOOOOi:{0}\n", Points.TryGetValue(oi));
        //                    double dis = -1;
        //                    if (Points.TryGetValue(oi, out dis))
        //                    {
        //                        if (dis != -1 && NearestDis == -1)
        //                        {
        //                            NearestOi = oi;
        //                            NearestDis = dis;
        //                        }
        //                        else if (dis != -1 && NearestDis != -1)
        //                        {
        //                            if (dis <= NearestDis)
        //                            {
        //                                NearestOi = oi;
        //                            }
        //                        }
        //                    }

        //                }
        //                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(NearestOi);
        //                FirstConsol = NearestOi;
        //                int _MyVoltage = 0;
        //                byte _MyType = 0;
        //                if (drs.Length > 0)
        //                {
        //                    bool NodeIsValid = true;
        //                    switch (((Atend.Control.Enum.ProductType)at_info.NodeType))
        //                    {
        //                        case Atend.Control.Enum.ProductType.Consol:
        //                            Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
        //                            if (eConsol.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;
        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = eConsol.VoltageLevel;
        //                                _MyType = eConsol.ConsolType;
        //                            }
        //                            break;
        //                        case Atend.Control.Enum.ProductType.Kalamp:
        //                            Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(at_info.ProductCode);
        //                            if (eClamp.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;
        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = eClamp.VoltageLevel;
        //                                _MyType = 2;
        //                            }
        //                            break;
        //                        case Atend.Control.Enum.ProductType.KablSho:
        //                            Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(at_info.ProductCode);
        //                            if (eKablsho.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;

        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 2;
        //                            }
        //                            break;
        //                    }
        //                    _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
        //                    switch (_MyVoltage)
        //                    {
        //                        case 400:
        //                            IsWeek1 = true;
        //                            break;
        //                        default:
        //                            IsWeek1 = false;
        //                            break;
        //                    }
        //                    switch (_MyType)
        //                    {
        //                        case 0:
        //                            NeedArc1 = true;
        //                            Atend.Base.Acad.AT_SUB ConsolSub1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
        //                            foreach (ObjectId oi in ConsolSub1.SubIdCollection)
        //                            {
        //                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                                    NodeIsValid = false;
        //                            }
        //                            break;
        //                        case 1:
        //                            NeedArc1 = true;
        //                            Atend.Base.Acad.AT_SUB ConsolSub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
        //                            foreach (ObjectId oi in ConsolSub2.SubIdCollection)
        //                            {
        //                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                                    NodeIsValid = false;
        //                            }
        //                            break;
        //                        default:
        //                            NeedArc1 = false;
        //                            break;
        //                    }
        //                    if (NodeIsValid)
        //                        FirstPoint = false;
        //                    else
        //                        FirstPoint = true;

        //                }
        //                else
        //                {
        //                }
        //            }
        //            else
        //            {
        //                conti = false;
        //            }
        //        }
        //        else if (!FirstPoint)
        //        {
        //            peo.Message = "\n انتخاب دسته مقره دوم \n";

        //            PromptEntityResult per = ed.GetEntity(peo);

        //            if (per.Status == PromptStatus.OK)
        //            {
        //                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
        //                SecondConsol = per.ObjectId;
        //                int _MyVoltage = 0;
        //                byte _MyType = 0;
        //                DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", at_info.NodeType));
        //                if (drs.Length > 0)
        //                {
        //                    if (at_info.ParentCode != string.Empty)
        //                    {
        //                        _dBranch.RightNodeCode = new Guid(at_info.ParentCode);
        //                    }
        //                    else
        //                    {
        //                        _dBranch.RightNodeCode = new Guid();
        //                    }
        //                    bool NodeIsValid = true;
        //                    switch (((Atend.Control.Enum.ProductType)at_info.NodeType))
        //                    {
        //                        case Atend.Control.Enum.ProductType.Consol:
        //                            Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.AccessSelectByCode(at_info.ProductCode);
        //                            if (eConsol.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;
        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = eConsol.VoltageLevel;
        //                                _MyType = eConsol.ConsolType;
        //                            }
        //                            break;
        //                        case Atend.Control.Enum.ProductType.Kalamp:
        //                            Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.AccessSelectByCode(at_info.ProductCode);
        //                            if (eClamp.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;
        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = eClamp.VoltageLevel;
        //                                _MyType = 2;
        //                            }
        //                            break;
        //                        case Atend.Control.Enum.ProductType.KablSho:
        //                            Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.AccessSelectByCode(at_info.ProductCode);
        //                            if (eKablsho.Code == -1)
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 0;
        //                            }
        //                            else
        //                            {
        //                                _MyVoltage = 0;
        //                                _MyType = 2;
        //                            }
        //                            break;
        //                    }
        //                    switch (_MyVoltage)
        //                    {
        //                        case 400:
        //                            IsWeek2 = true;
        //                            break;
        //                        default:
        //                            IsWeek2 = false;
        //                            break;
        //                    }
        //                    switch (_MyType)
        //                    {
        //                        case 0:
        //                            NeedArc2 = true;
        //                            Atend.Base.Acad.AT_SUB ConsolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
        //                            foreach (ObjectId oi in ConsolSub.SubIdCollection)
        //                            {
        //                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                                    NodeIsValid = false;
        //                            }
        //                            break;
        //                        case 1:
        //                            Atend.Base.Acad.AT_SUB ConsolSub1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(per.ObjectId);
        //                            foreach (ObjectId oi in ConsolSub1.SubIdCollection)
        //                            {
        //                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi).NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                                    NodeIsValid = false;
        //                            }

        //                            NeedArc2 = true;
        //                            break;
        //                        case 2:
        //                            NeedArc2 = false;
        //                            break;
        //                        case 3:
        //                            NeedArc2 = false;
        //                            break;
        //                    }
        //                    if (NodeIsValid)
        //                    {
        //                        Polyline pline = Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId) as Polyline;
        //                        if (pline != null)
        //                        {
        //                            Line LineEntity = new Line(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(FirstConsol)), Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(SecondConsol)));

        //                            if (IsWeek1 == IsWeek2 && FirstConsol != SecondConsol && Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(FirstConsol).ParentCode != Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SecondConsol).ParentCode)
        //                            {
        //                                _dBranch.IsWeek = IsWeek2;
        //                                Atend.Design.frmDrawBranch drawBranch = new Atend.Design.frmDrawBranch();
        //                                drawBranch.Length = LineEntity.Length;
        //                                if (Application.ShowModalDialog(drawBranch) == System.Windows.Forms.DialogResult.OK)
        //                                {
        //                                    _dBranch.Lenght = drawBranch.Length;
        //                                    _dBranch.Number = eConductorTip.Description;

        //                                    #region save data here
        //                                    if (SaveConductorData())
        //                                    {
        //                                        if (LineEntity != null)
        //                                        {

        //                                            string LayerName;
        //                                            if (IsWeek1)
        //                                            {
        //                                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString();
        //                                            }
        //                                            else
        //                                            {
        //                                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
        //                                            }


        //                                            ObjectId DrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(LineEntity, LayerName);

        //                                            Atend.Base.Acad.AT_INFO at_infoForConsol = new Atend.Base.Acad.AT_INFO();
        //                                            at_infoForConsol.SelectedObjectId = DrawnLine;
        //                                            at_infoForConsol.ParentCode = "";
        //                                            at_infoForConsol.NodeCode = _dBranch.Code.ToString();
        //                                            at_infoForConsol.NodeType = (int)Atend.Control.Enum.ProductType.Conductor;
        //                                            at_infoForConsol.ProductCode = _dBranch.ProductCode;
        //                                            at_infoForConsol.Insert();


        //                                            Entity EText = Atend.Global.Acad.UAcad.WriteNote(_dBranch.Number, LineEntity.StartPoint, LineEntity.EndPoint, MyCommentScale);

        //                                            ObjectId DrawnText = Atend.Global.Acad.UAcad.DrawEntityOnScreen(EText, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //                                            Atend.Base.Acad.AT_SUB at_sub = new Atend.Base.Acad.AT_SUB();
        //                                            at_sub.SelectedObjectId = DrawnLine;
        //                                            at_sub.SubIdCollection.Add(DrawnText);
        //                                            at_sub.SubIdCollection.Add(FirstConsol);
        //                                            at_sub.SubIdCollection.Add(SecondConsol);
        //                                            at_sub.Insert();


        //                                            Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(DrawnText);
        //                                            TextInfo.ParentCode = _dBranch.Code.ToString();
        //                                            TextInfo.NodeCode = "";
        //                                            TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        //                                            TextInfo.ProductCode = 0;
        //                                            TextInfo.Insert();


        //                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, FirstConsol);

        //                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(DrawnLine, SecondConsol);



        //                                            if (NeedArc1)
        //                                            {
        //                                                Arc a = (Arc)CreateArcForSpecialConsol(FirstConsol);
        //                                                newArc1 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
        //                                                //ed.WriteMessage("~~~>> ARC WAS NEEDED ~~~~~ \n");
        //                                                if (newArc1 != ObjectId.Null)
        //                                                {
        //                                                    //ed.WriteMessage("2 \n");
        //                                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc1, FirstConsol);

        //                                                    Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(FirstConsol);

        //                                                    Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
        //                                                    at_info1.SelectedObjectId = newArc1;
        //                                                    at_info1.ParentCode = Parentconsol.NodeCode;
        //                                                    at_info1.NodeCode = "";
        //                                                    at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
        //                                                    at_info1.ProductCode = 0;
        //                                                    at_info1.Insert();


        //                                                }


        //                                            }

        //                                            if (NeedArc2)
        //                                            {
        //                                                Arc a = (Arc)CreateArcForSpecialConsol(SecondConsol);

        //                                                newArc2 = Atend.Global.Acad.UAcad.DrawEntityOnScreen(a, LayerName);
        //                                                //ed.WriteMessage("1 \n");
        //                                                if (newArc2 != ObjectId.Null)
        //                                                {
        //                                                    //ed.WriteMessage("2 \n");
        //                                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(newArc2, SecondConsol);

        //                                                    Atend.Base.Acad.AT_INFO Parentconsol = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SecondConsol);

        //                                                    Atend.Base.Acad.AT_INFO at_info1 = new Atend.Base.Acad.AT_INFO();
        //                                                    at_info1.SelectedObjectId = newArc2;
        //                                                    at_info1.ParentCode = Parentconsol.NodeCode;
        //                                                    at_info1.NodeCode = "";
        //                                                    at_info1.NodeType = (int)Atend.Control.Enum.ProductType.TensionArc;
        //                                                    at_info1.ProductCode = 0;
        //                                                    at_info1.Insert();

        //                                                }

        //                                            }

        //                                            conti = false;

        //                                        }


        //                                    }
        //                                    else
        //                                    {
        //                                        return;
        //                                    }
        //                                    #endregion

        //                                }
        //                            }
        //                            else
        //                            {
        //                                ed.WriteMessage("\n دسته مقره انتخاب شده مجاز نمی باشد \n");
        //                                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
        //                                notification.Title = "ترسیم سیم";
        //                                notification.Msg = "دسته مقره انتخاب شده مجاز نمی باشد";
        //                                notification.infoCenterBalloon();
        //                            }
        //                            if (at_info.ParentCode != string.Empty)
        //                            {
        //                                _dBranch.LeftNodeCode = new Guid(at_info.ParentCode);
        //                            }
        //                            else
        //                            {
        //                                _dBranch.LeftNodeCode = new Guid();
        //                            }
        //                        }//

        //                    }
        //                }
        //                else
        //                {
        //                    //return;
        //                }
        //            }
        //            else
        //            {
        //                conti = false;
        //            }
        //        }//end if for FirstPoint
        //    }// end of while
        //}

        private bool SaveConductorData()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~~~~~~~ Start Save Branch ~~~~~~~~~~~~");
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            //ArrayList ConductorCodes = new ArrayList();

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
                        _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
                        _dBranch.Sag = 0;
                        _dBranch.ProjectCode = ProjectCode;
                        _dBranch.Number = eConductorTip.Description;
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
                            _dBranch.Number = eConductorTip.Description;
                            _dBranch.ProductCode = eConductorTip.Code;
                            _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
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
                    ed.WriteMessage(string.Format("ConductorTransaction for insert failed 02 : {0} \n", ex1.Message));
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ConductorTransaction for insert failed 01 : {0} \n", ex.Message));
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();


            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.ConductorData.UseAccess = true;
            //UseAccess = true;

            #endregion

            //ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");
            return true;

        }

        public bool UpdateConductorData(Guid BranchGuid, ObjectId obj, double Lenght)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~**~~~~~~ Start Save Branch ~~~~~~~~~~~~");
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            //ArrayList ConductorCodes = new ArrayList();
            Atend.Base.Acad.AT_INFO atInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
            _dBranch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchGuid);
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {

                    if (!_UseAccess)
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
                        if (!eConductorTip.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eConductorTip.AccessInsert failed");
                        }
                    }

                    _dBranch.ProductCode = eConductorTip.Code;
                    _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.Conductor;
                    _dBranch.Lenght = Lenght;
                    _dBranch.Sag = 0;
                    _dBranch.Number = eConductorTip.Description;
                    _dBranch.IsExist = Existance;
                    _dBranch.ProjectCode = ProjectCode;
                    atInfo.ProductCode = eConductorTip.Code;
                    if (_dBranch.AccessUpdate(aTransaction, aConnection))
                    {
                        atInfo.Insert();
                        ChangeCounductorComment(obj, eConductorTip.Description);
                    }
                    else
                    {
                        throw new System.Exception("dBranch.AccessInsert failed");
                    }

                    if (!_dBranch.AccessUpdateLenghtByLeftAndRightNode(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessUpdate Failed");
                    }




                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("ConductorTransaction for insert failed 02 : {0} \n", ex1.Message));
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ConductorTransaction for insert failed 01 : {0} \n", ex.Message));
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            //ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");
            return true;
        }

        public static void ChangeCounductorComment(ObjectId SelectedLineObjectID, string Text)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            // Database db = HostApplicationServices.WorkingDatabase;
            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                //using (Transaction tr = db.TransactionManager.StartTransaction())
                //{

                Atend.Base.Acad.AT_SUB at_sub =
                Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(SelectedLineObjectID);

                Atend.Base.Acad.AT_INFO at_info;

                if (at_sub.SubIdCollection.Count > 0)
                {
                    foreach (ObjectId oi in at_sub.SubIdCollection)
                    {
                        at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        if (at_info.ParentCode.Equals("NONE"))
                        {
                            //ed.WriteMessage("ITS NOT ATEND OBJECT.\n");
                        }
                        else if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                        {
                            //ed.WriteMessage("########THING FOUND.\n");

                            ChangeEntityText(oi, Text);

                        }
                    }
                }
                else
                {
                    //ed.WriteMessage("NOTHING FOUND.\n");
                }


                //}
            }


        }

        public static void ChangeEntityText(ObjectId SelectedTextObjectID, string Text)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                DBText dbtext = (DBText)tr.GetObject(SelectedTextObjectID, OpenMode.ForWrite);

                if (dbtext != null)
                {
                    //ed.WriteMessage("*********THING FOUND.\n");

                    dbtext.TextString = Text;
                }

                tr.Commit();

                //ed.Regen();

            }

        }

        private Entity CreateArcForSpecialConsol(ObjectId ContainerConsol)
        {

            Point3d CenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(ContainerConsol));
            double Radius = 0, StartAngle = 0, EndAngle = 0;
            Line ImaginaryLine = new Line();

            Atend.Base.Acad.AT_SUB at_sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(ContainerConsol);
            //ed.WriteMessage("1 : {0} \n", ContainerConsol);
            foreach (ObjectId oi in at_sub.SubIdCollection)
            {
                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                //ed.WriteMessage("22 : {0} \n", oi);
                if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                {
                    //ed.WriteMessage("conductor was found \n");
                    Line ConductorLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
                    if (ConductorLine.StartPoint == CenterPoint)
                    {
                        ImaginaryLine.StartPoint = CenterPoint;
                        ImaginaryLine.EndPoint = ConductorLine.EndPoint;
                    }
                    else
                    {
                        ImaginaryLine.StartPoint = ConductorLine.EndPoint;
                        ImaginaryLine.EndPoint = ConductorLine.StartPoint;
                    }


                    double angle = (180 * ImaginaryLine.Angle) / Math.PI;
                    StartAngle = angle - 15;
                    EndAngle = angle + 15;

                    StartAngle = (StartAngle * Math.PI) / 180;
                    EndAngle = (EndAngle * Math.PI) / 180;


                }
                else if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip)
                {

                    //ed.WriteMessage("Pole Parent was found \n");
                    Polyline poleEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
                    if (poleEntity != null)
                    {
                        //ed.WriteMessage("Pole Parent was found As rectangle\n");
                        LineSegment3d LS1 = poleEntity.GetLineSegmentAt(0);
                        ////ed.WriteMessage("{0}:{1}\n",LS1.StartPoint , LS1.EndPoint);
                        LineSegment3d LS2 = poleEntity.GetLineSegmentAt(1);
                        //ed.WriteMessage("{0}:{1}\n", LS2.StartPoint, LS2.EndPoint);
                        LineSegment3d ls3 = new LineSegment3d(LS1.StartPoint, LS2.EndPoint);
                        //ed.WriteMessage("{0}:{1}\n", ls3.StartPoint, ls3.EndPoint);
                        Radius = ls3.Length / 2;
                    }
                    else
                    {
                        //ed.WriteMessage("Pole Parent was found As circle\n");
                        Circle c = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Circle;
                        if (c != null)
                        {
                            Radius = c.Radius;
                        }
                    }
                }
            }


            //ed.WriteMessage("CenterPoint {0} \n",CenterPoint);
            //ed.WriteMessage("Radius {0} \n", Radius);
            //ed.WriteMessage("StartAngle {0} \n", StartAngle);
            //ed.WriteMessage("EndAngle {0} \n", EndAngle);

            Arc a = new Arc(CenterPoint, Radius, StartAngle, EndAngle);

            return a;
        }

        public static bool DeleteConductorData(ObjectId ConductorOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //delete conductor
                Atend.Base.Acad.AT_INFO conductorinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ConductorOI);
                if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(conductorinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete dbranch\n");
                }

                //delete jumper
                System.Data.DataTable dt = Atend.Base.Design.DBranch.AccessSelectByLeftOrRightNodeCode(new Guid(conductorinfo.NodeCode.ToString()));
                foreach (DataRow dr in dt.Rows)
                {
                    //ed.WriteMessage("## :{0}\n", dr["Code"]);
                    if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(dr["Code"].ToString())))
                    {
                        throw new System.Exception("Error In Delete dbranch(Dependent'Conductor)\n");
                    }
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR CONDUCTOR : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteConductor(ObjectId ConductorOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB CounductorSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(ConductorOI);
                foreach (ObjectId CounductorSubOI in CounductorSub.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO CounductorSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(CounductorSubOI);
                    if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(CounductorSubOI))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                        else
                        {
                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(CounductorSubOI, ConductorOI);
                        }
                    }
                    if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Jumper)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(CounductorSubOI))
                        {
                            throw new System.Exception("Error In Delete Jumper\n");
                        }
                        else
                        {
                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(CounductorSubOI, ConductorOI);
                        }
                    }
                    if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                    {
                        //if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(CounductorSubOI))
                        //{
                        //    throw new System.Exception("GRA while delete conductor sub\n");
                        //}

                        Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        foreach (ObjectId oi in consolSub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.TensionArc)
                            {
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                                {
                                    throw new System.Exception("Error In Delete Arc\n");
                                }
                                else
                                {
                                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                                }
                            }
                            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor && at_info.SelectedObjectId == ConductorOI)
                            {
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                                {
                                    throw new System.Exception("Error In Delete Arc\n");
                                }
                                else
                                {
                                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                                }
                            }
                        }
                    }
                }
                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(ConductorOI))
                {
                    throw new System.Exception("GRA while delete conductor \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR CONDUCTOR : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

    }
}