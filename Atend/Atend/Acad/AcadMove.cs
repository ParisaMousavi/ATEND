using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

//get from tehran 7/15

namespace Atend.Acad
{

    public class AcadMove
    {
        //public static bool isMoveConsolOnly = true;
        //static Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //public static ObjectId ConsolOI = ObjectId.Null, PoleOI = ObjectId.Null, RodOI = ObjectId.Null, BankKhazanOI = ObjectId.Null
        //    , GroundPostOI = ObjectId.Null, CabelOI = ObjectId.Null, AirPostOI = ObjectId.Null, StreetBoxOI = ObjectId.Null, HeaderCabelOI = ObjectId.Null
        //    , MafsalOI = ObjectId.Null, KalampOI = ObjectId.Null, KablShoOI = ObjectId.Null, PoleTipOI = ObjectId.Null, DBOI = ObjectId.Null, LightOI = ObjectId.Null
        //    , MeasuredJackPanelOI = ObjectId.Null, GroundOI = ObjectId.Null, TransformerOI = ObjectId.Null, MiddleJackPanelOI = ObjectId.Null, WeekJackPanelOI = ObjectId.Null;
        //public static Point3d LastCenterPoint;
        //public static Point3d LastCenterPointKL;
        //public static Atend.Base.Acad.AT_INFO EntityInfo = new Atend.Base.Acad.AT_INFO();
        //public static Atend.Base.Acad.AT_SUB EntitySub = new Atend.Base.Acad.AT_SUB();
        //public static bool AllowToMove = false;
        //public static Entity CurrentEntity = null;
        //public static bool swBreaker = true; //for move Breaker in pole
        //public static bool swDisconnector = true; //for move Disconnector in pole
        //public static bool swCatOut = true;
        //public static int ProductType = -1;


        //static void Reset()
        //{
        //    ConsolOI = ObjectId.Null; PoleOI = ObjectId.Null; RodOI = ObjectId.Null; BankKhazanOI = ObjectId.Null;
        //    GroundPostOI = ObjectId.Null; CabelOI = ObjectId.Null; AirPostOI = ObjectId.Null; StreetBoxOI = ObjectId.Null;
        //    HeaderCabelOI = ObjectId.Null; MafsalOI = ObjectId.Null; KalampOI = ObjectId.Null; PoleTipOI = ObjectId.Null;
        //    KablShoOI = ObjectId.Null; DBOI = ObjectId.Null; LightOI = ObjectId.Null; MeasuredJackPanelOI = ObjectId.Null;
        //    GroundOI = ObjectId.Null; TransformerOI = ObjectId.Null;

        //    ProductType = -1;
        //}

        //public static void MovePole(ObjectId Pole)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();

        //        ArrayList BreakerList = new ArrayList();
        //        ArrayList DisconnectorList = new ArrayList();
        //        ArrayList CatOutList = new ArrayList();

        //        Point3d CenterPole = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentEntity);
        //        Matrix3d mat = Matrix3d.Displacement(CenterPole - LastCenterPoint);
        //        foreach (ObjectId oi in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            //Move Consol
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
        //            {
        //                isMoveConsolOnly = false;
        //                ChangeConsolPosition(oi, mat);
        //                MoveConsol(Pole, oi, mat);
        //            }
        //            //Move Comment
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                ChangeConsolPosition(oi, mat);
        //            }
        //            //Move Khazan
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
        //            {
        //                Atend.Base.Acad.AT_SUB KhazanCollection = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId obj in KhazanCollection.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //                    //Move Comment
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(obj, mat);
        //                    }
        //                    //Move CollectionPole
        //                    if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //                    {
        //                        //ObjectId id = UAcad.GetEntityGroup(obj);
        //                        ObjectIdCollection CollectionKhazan = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                        foreach (ObjectId collect in CollectionKhazan)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            if (atinfo.ParentCode != "NONE")// && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
        //                            {
        //                                ////ed.writeMessage("find subcollection of khazan\n");
        //                                ChangeConsolPosition(collect, mat);
        //                            }

        //                        }
        //                    }
        //                }

        //            }
        //            //Move Rod
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Rod)
        //            {
        //                Atend.Base.Acad.AT_SUB RodCollection = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId obj in RodCollection.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //                    //Move Comment
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(obj, mat);
        //                    }
        //                    //Move CollectionPole
        //                    if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //                    {
        //                        ObjectIdCollection CollectionRod = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                        foreach (ObjectId collect in CollectionRod)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            if (atinfo.ParentCode != "NONE")// && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }

        //                        }
        //                    }
        //                }

        //                ////ObjectIdCollection CollectionRod = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                ////foreach (ObjectId collect in CollectionRod)
        //                ////{
        //                ////    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                ////    if (at_info.ParentCode != "NONE")// && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Rod)
        //                ////    {
        //                ////        ChangeConsolPosition(collect, mat);
        //                ////    }
        //                ////}

        //            }
        //            //Move Clamp
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
        //            {
        //                Point3d lastcenterpoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));

        //                Point3d _CenterPoint = Point3d.Origin;
        //                Polyline _Entity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //                if (_Entity != null)
        //                {
        //                    _CenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(_Entity);
        //                }

        //                ChangeConsolPosition(oi, mat);
        //                Atend.Base.Acad.AT_SUB CPSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oi112 in CPSub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi112);
        //                    //Move Comment
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(oi112, mat);
        //                    }
        //                    //Move SelfKeeper 
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //                    {
        //                        Point3d CPCenterPoint = Point3d.Origin;
        //                        Polyline CPEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //                        if (CPEntity != null)
        //                        {
        //                            CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CPEntity);
        //                        }
        //                        //Database db = HostApplicationServices.WorkingDatabase;
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oi112, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == lastcenterpoint)
        //                                {
        //                                    LineEntity.StartPoint = CPCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = CPCenterPoint;
        //                                }

        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                    //Move Breaker 
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                    {
        //                        swBreaker = true;
        //                        for (int i = 0; i < BreakerList.Count; i++)
        //                        {
        //                            if (BreakerList[i].ToString() == oi.ToString())
        //                                swBreaker = false;
        //                        }
        //                        if (swBreaker)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
        //                                    {
        //                                        BreakerList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move Disconnector
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                    {
        //                        swDisconnector = true;
        //                        //ed.WriteMessage("++++++++++ :DISCONNECTOR(1)\n");
        //                        //ed.WriteMessage("++++++++++ Count:{0}\n", DisconnectorList.Count);
        //                        for (int i = 0; i < DisconnectorList.Count; i++)
        //                        {
        //                            if (DisconnectorList[i].ToString() == oi.ToString())
        //                                swDisconnector = false;
        //                        }
        //                        if (swDisconnector)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
        //                                    {
        //                                        DisconnectorList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move CatOut
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                    {
        //                        swCatOut = true;
        //                        for (int i = 0; i < CatOutList.Count; i++)
        //                        {
        //                            if (CatOutList[i].ToString() == oi.ToString())
        //                                swCatOut = false;
        //                        }
        //                        if (swCatOut)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi112);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
        //                                    {
        //                                        CatOutList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move Terminal
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        Point3d ConsolCenterPoint = Point3d.Origin;
        //                        Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
        //                        if (consolEntity != null)
        //                        {
        //                            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oi112, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == LastCenterPoint)
        //                                {
        //                                    LineEntity.StartPoint = ConsolCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = ConsolCenterPoint;
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                }
        //            }
        //            //Move HeaderCabel Or KablSho
        //            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
        //            {
        //                Point3d lst = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                ChangeConsolPosition(oi, mat);
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                        if (cener != null)
        //                        {
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    double x = Math.Abs(cabelent.StartPoint.X - lst.X);
        //                                    double y = Math.Abs(cabelent.EndPoint.X - lst.X);
        //                                    if (cabelent.StartPoint == lst || x < y)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == lst || x > y)//|| (cabelent.EndPoint.X - lst.X < 1))
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect001 in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo001 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect001);
        //                            if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = lst;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = lst;
        //                                }
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect001, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    //Move Selfkeeper
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //                    {
        //                        Point3d _CPCenterPoint = Point3d.Origin;
        //                        Polyline CLine = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //                        if (CLine != null)
        //                        {
        //                            _CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CLine);
        //                        }
        //                        Line cabelent = new Line();
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                cabelent = (Line)ent;
        //                                if (cabelent != null)
        //                                {
        //                                    if (cabelent.StartPoint == LastCenterPoint)
        //                                    {
        //                                        cabelent.StartPoint = _CPCenterPoint;
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                                    {
        //                                        cabelent.EndPoint = _CPCenterPoint;
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == cabelent.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;
        //                                    end = cabelent.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == cabelent.EndPoint)
        //                                {
        //                                    start = cabelent.StartPoint;
        //                                    end = LastCenterPoint;
        //                                }

        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move Kalamp alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(cabelent.StartPoint, cabelent.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(objsub, mat);
        //                    }
        //                    //Move Breaker 
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                    {
        //                        swBreaker = true;
        //                        for (int i = 0; i < BreakerList.Count; i++)
        //                        {
        //                            if (BreakerList[i].ToString() == oi.ToString())
        //                                swBreaker = false;
        //                        }
        //                        if (swBreaker)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && (at_info20.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || at_info20.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
        //                                    {
        //                                        BreakerList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move Disconnector
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                    {
        //                        swDisconnector = true;
        //                        //ed.WriteMessage("++++++++++ :DISCONNECTOR(2)\n");
        //                        for (int i = 0; i < DisconnectorList.Count; i++)
        //                        {
        //                            if (DisconnectorList[i].ToString() == oi.ToString())
        //                                swDisconnector = false;
        //                        }
        //                        if (swDisconnector)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && (at_info20.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || at_info20.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
        //                                    {
        //                                        DisconnectorList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move CatOut
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                    {
        //                        swCatOut = true;
        //                        for (int i = 0; i < CatOutList.Count; i++)
        //                        {
        //                            if (CatOutList[i].ToString() == oi.ToString())
        //                                swCatOut = false;
        //                        }
        //                        if (swCatOut)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                            ObjectId oid = ObjectId.Null;
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                ChangeConsolPosition(collect, mat);
        //                            }
        //                        }

        //                        ObjectIdCollection _Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                        foreach (ObjectId _collect in _Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(_collect);
        //                                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                foreach (ObjectId h in sub2.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                    if (at_info20.ParentCode != "NONE" && (at_info20.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || at_info20.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
        //                                    {
        //                                        CatOutList.Add(h);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //Move Terminal
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        Point3d ConsolCenterPoint = Point3d.Origin;
        //                        Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
        //                        if (consolEntity != null)
        //                        {
        //                            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == LastCenterPoint)
        //                                {
        //                                    LineEntity.StartPoint = ConsolCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = ConsolCenterPoint;
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                }
        //            }
        //            //Move ConnectionPoint
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.ConnectionPoint)
        //            {
        //                Point3d lastcenterpoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                ChangeConsolPosition(oi, mat);
        //                Atend.Base.Acad.AT_SUB CPSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oi11 in CPSub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi11);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //                    {
        //                        Point3d CPCenterPoint = Point3d.Origin;
        //                        Circle CPEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Circle;
        //                        if (CPEntity != null)
        //                        {
        //                            CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CPEntity);
        //                        }
        //                        //Database db = HostApplicationServices.WorkingDatabase;
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oi11, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == lastcenterpoint)
        //                                {
        //                                    LineEntity.StartPoint = CPCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = CPCenterPoint;
        //                                }

        //                            }
        //                            tr.Commit();
        //                        }
        //                    }


        //                }

        //            }

        //            //Move Ground
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //            {
        //                //Move Comment
        //                Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oisb in EntityS.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
        //                    if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(oisb, mat);
        //                    }
        //                }

        //                ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                foreach (ObjectId collect in Collection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //                    {
        //                        ChangeConsolPosition(collect, mat);
        //                    }
        //                }


        //            }

        //            //Move Light
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Light)
        //            {
        //                //Move Comment
        //                Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oisb in EntityS.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
        //                    if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(oisb, mat);
        //                    }
        //                }

        //                ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                foreach (ObjectId collect in Collection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Light)
        //                    {
        //                        ChangeConsolPosition(collect, mat);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //}

        //private static void ChangeConsolPosition(ObjectId ConsolObjectId, Matrix3d DisplacementMatrix)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    Entity ent;
        //    //Database db = HostApplicationServices.WorkingDatabase;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    using (Transaction tr = db.TransactionManager.StartTransaction())
        //    {
        //        ent = (Entity)tr.GetObject(ConsolObjectId, OpenMode.ForWrite);
        //        ent.TransformBy(DisplacementMatrix);
        //        tr.Commit();
        //    }
        //}

        //public static void MoveConsol(ObjectId Pole, ObjectId Consol, Matrix3d poleMat)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    Point3d LSP = LastCenterPoint;
        //    if (AllowToMove && ConsolOI != null)
        //    {
        //        Reset();
        //        Point3d CenterConsol = new Point3d();
        //        Matrix3d mat = new Matrix3d();

        //        //for next consol
        //        ArrayList arrayNextConsol = new ArrayList();
        //        Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(Consol);
        //        foreach (ObjectId oi in consolSub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            //ed.WriteMessage("@@@@@@@ {0}\n",at_info.NodeType);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //            {
        //                arrayNextConsol.Add(at_info.NodeCode);
        //            }
        //        }


        //        // compute center point of consol
        //        Point3d ConsolCenterPoint = Point3d.Origin;
        //        Polyline consolEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol) as Polyline;
        //        if (consolEntity != null)
        //        {
        //            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //        }
        //        // end compute center point of consol

        //        Entity Parent = null;
        //        foreach (ObjectId oi in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //            {
        //                Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);// as Polyline;
        //                //p3c = Atend.Global.Acad.UAcad.ConvertEntityToPoint3dCollection(Parent);
        //                //ParentCurve = Parent as Curve;
        //            }

        //        }
        //        Curve ParentCurve = Parent as Curve;
        //        if (ParentCurve != null)
        //        {
        //            if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, ConsolCenterPoint) == false)
        //            {
        //                ReturnConsolEntity(Consol);
        //                return;
        //            }
        //        }
        //        //Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(Consol);

        //        foreach (ObjectId oi in consolSub.SubIdCollection)
        //        {

        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            //ed.WriteMessage("&&&&&&& {0}\n", at_info.NodeType);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //            {
        //                //MoveConductor(ConsolOI, oi, LastCenterPoint, IsMovePole);
        //                MoveConductor(Consol, oi, LastCenterPoint);
        //            }

        //            //Move Terminal
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //            {
        //                //Point3d ConsolCenterPoint = Point3d.Origin;
        //                //Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol);
        //                //if (consolEntity != null)
        //                //{
        //                //    ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                //}
        //                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                {
        //                    Entity ent = (Entity)tr.GetObject(oi, OpenMode.ForWrite);
        //                    Line LineEntity = (Line)ent;
        //                    if (LineEntity != null)
        //                    {
        //                        if (LineEntity.StartPoint == LastCenterPoint)
        //                        {
        //                            LineEntity.StartPoint = ConsolCenterPoint;
        //                        }
        //                        else
        //                        {
        //                            LineEntity.EndPoint = ConsolCenterPoint;
        //                        }
        //                    }
        //                    tr.Commit();
        //                }
        //            }

        //            //Move Comment
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                //ed.WriteMessage("LSP:{0}\n", LSP);
        //                //ed.WriteMessage("ConsolCenterPoint:{0}\n", ConsolCenterPoint);
        //                //ed.WriteMessage("isMoveConsolOnly:{0}\n", isMoveConsolOnly);
        //                mat = Matrix3d.Displacement(ConsolCenterPoint - LSP);
        //                if (isMoveConsolOnly == false)
        //                    ChangeConsolPosition(oi, poleMat);
        //                else
        //                    ChangeConsolPosition(oi, mat);
        //            }
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //            {
        //                //ed.WriteMessage("is breaker\n");
        //                if (isMoveConsolOnly == true)
        //                {
        //                    ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                    ObjectId oid = ObjectId.Null;
        //                    foreach (ObjectId collect in Collectionbreaker)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                        if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                        {
        //                            oid = collect;
        //                            Collectionbreaker.Remove(collect);
        //                        }
        //                    }
        //                    Point3d start = new Point3d();
        //                    Point3d end = new Point3d();
        //                    Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                    if (LSP == CLine.StartPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.EndPoint, ConsolCenterPoint);
        //                    }
        //                    else if (LSP == CLine.EndPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.StartPoint, ConsolCenterPoint);
        //                    }

        //                    //remove Sub Group
        //                    foreach (ObjectId c2 in Collectionbreaker)
        //                    {
        //                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                        {
        //                            ed.WriteMessage("Breaker Not Removed\n");
        //                        }
        //                    }
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                        Line LineEntity = (Line)ent;
        //                        if (LineEntity != null)
        //                        {
        //                            if (LSP == CLine.StartPoint)
        //                            {
        //                                LineEntity.StartPoint = CLine.EndPoint;
        //                                LineEntity.EndPoint = ConsolCenterPoint;
        //                            }
        //                            else if (LSP == CLine.EndPoint)
        //                            {
        //                                LineEntity.EndPoint = CLine.StartPoint;
        //                                LineEntity.StartPoint = ConsolCenterPoint;
        //                            }
        //                            start = LineEntity.StartPoint;
        //                            end = LineEntity.EndPoint;
        //                            Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                        }

        //                        tr.Commit();
        //                    }


        //                    //Move Comment
        //                    ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                    Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                    foreach (ObjectId h in sub2.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                        if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                        {
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                            ChangeConsolPosition(h, matcom);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (ObjectId oisub in consolSub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
        //                        if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                        {
        //                            ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(oisub);
        //                            foreach (ObjectId collect in Collectionbreaker)
        //                            {
        //                                if (swBreaker)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                                    //Move Comment
        //                                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                                    {
        //                                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
        //                                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                        foreach (ObjectId h in sub2.SubIdCollection)
        //                                        {
        //                                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                            {
        //                                                ChangeConsolPosition(h, poleMat);
        //                                            }
        //                                        }
        //                                    }

        //                                    ChangeConsolPosition(collect, poleMat);
        //                                }
        //                            }
        //                            swBreaker = false;
        //                        }
        //                    }
        //                }
        //            }
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //            {
        //                //ed.WriteMessage("is Disconnector\n");
        //                //ed.WriteMessage("++++++++++ :DISCONNECTOR(3)\n");
        //                if (isMoveConsolOnly == true)
        //                {
        //                    ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                    ObjectId oid = ObjectId.Null;
        //                    foreach (ObjectId collect in CollectionDisconnector)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                        if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                        {
        //                            oid = collect;
        //                            CollectionDisconnector.Remove(collect);
        //                        }
        //                    }
        //                    Point3d start = new Point3d();
        //                    Point3d end = new Point3d();
        //                    Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                    if (LSP == CLine.StartPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(CollectionDisconnector, LineSeg.MidPoint, CLine.EndPoint, ConsolCenterPoint);
        //                    }
        //                    else if (LSP == CLine.EndPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(CollectionDisconnector, LineSeg.MidPoint, CLine.StartPoint, ConsolCenterPoint);
        //                    }

        //                    //remove Sub Group
        //                    foreach (ObjectId c2 in CollectionDisconnector)
        //                    {
        //                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                        {
        //                            ed.WriteMessage("Disconnector Not Removed\n");
        //                        }
        //                    }
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                        Line LineEntity = (Line)ent;
        //                        if (LineEntity != null)
        //                        {
        //                            if (LSP == CLine.StartPoint)
        //                            {
        //                                LineEntity.StartPoint = CLine.EndPoint;
        //                                LineEntity.EndPoint = ConsolCenterPoint;
        //                            }
        //                            else if (LSP == CLine.EndPoint)
        //                            {
        //                                LineEntity.EndPoint = CLine.StartPoint;
        //                                LineEntity.StartPoint = ConsolCenterPoint;
        //                            }
        //                            start = LineEntity.StartPoint;
        //                            end = LineEntity.EndPoint;
        //                            Atend.Global.Acad.DrawEquips.AcDrawDisConnector.AddNewDisconnector(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                        }

        //                        tr.Commit();
        //                    }


        //                    //Move Comment
        //                    ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                    Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                    foreach (ObjectId h in sub2.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                        if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                        {
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                            ChangeConsolPosition(h, matcom);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (ObjectId oisub in consolSub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
        //                        if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                        {
        //                            //ed.WriteMessage("++++++++++ :DISCONNECTOR(4)\n");
        //                            ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(oisub);
        //                            foreach (ObjectId collect in CollectionDisconnector)
        //                            {
        //                                if (swDisconnector)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                                    //Move Comment
        //                                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                                    {
        //                                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
        //                                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                        foreach (ObjectId h in sub2.SubIdCollection)
        //                                        {
        //                                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                            {
        //                                                ChangeConsolPosition(h, poleMat);
        //                                            }
        //                                        }
        //                                    }
        //                                    ChangeConsolPosition(collect, poleMat);
        //                                }
        //                            }
        //                            swDisconnector = false;
        //                        }
        //                    }
        //                }
        //            }
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //            {
        //                if (isMoveConsolOnly == true)
        //                {
        //                    ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(oi);
        //                    ObjectId oid = ObjectId.Null;
        //                    foreach (ObjectId collect in CollectionDisconnector)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                        if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                        {
        //                            oid = collect;
        //                            CollectionDisconnector.Remove(collect);
        //                        }
        //                    }
        //                    Point3d start = new Point3d();
        //                    Point3d end = new Point3d();
        //                    Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                    if (LSP == CLine.StartPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(CollectionDisconnector, LineSeg.MidPoint, CLine.EndPoint, ConsolCenterPoint);
        //                    }
        //                    else if (LSP == CLine.EndPoint)
        //                    {
        //                        Atend.Global.Acad.UAcad.RotateBy2Point(CollectionDisconnector, LineSeg.MidPoint, CLine.StartPoint, ConsolCenterPoint);
        //                    }

        //                    //remove Sub Group
        //                    foreach (ObjectId c2 in CollectionDisconnector)
        //                    {
        //                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                        {
        //                            ed.WriteMessage("CatOut Not Removed\n");
        //                        }
        //                    }
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                        Line LineEntity = (Line)ent;
        //                        if (LineEntity != null)
        //                        {
        //                            if (LSP == CLine.StartPoint)
        //                            {
        //                                LineEntity.StartPoint = CLine.EndPoint;
        //                                LineEntity.EndPoint = ConsolCenterPoint;
        //                            }
        //                            else if (LSP == CLine.EndPoint)
        //                            {
        //                                LineEntity.EndPoint = CLine.StartPoint;
        //                                LineEntity.StartPoint = ConsolCenterPoint;
        //                            }
        //                            start = LineEntity.StartPoint;
        //                            end = LineEntity.EndPoint;
        //                            Atend.Global.Acad.DrawEquips.AcDrawDisConnector.AddNewDisconnector(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                        }

        //                        tr.Commit();
        //                    }


        //                    //Move Comment
        //                    ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                    Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                    foreach (ObjectId h in sub2.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                        if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                        {
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                            ChangeConsolPosition(h, matcom);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (ObjectId oisub in consolSub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
        //                        if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                        {
        //                            ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(oisub);
        //                            foreach (ObjectId collect in CollectionDisconnector)
        //                            {
        //                                if (swCatOut)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                                    //Move Comment
        //                                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                                    {
        //                                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
        //                                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                                        foreach (ObjectId h in sub2.SubIdCollection)
        //                                        {
        //                                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                            {
        //                                                ChangeConsolPosition(h, poleMat);
        //                                            }
        //                                        }
        //                                    }
        //                                    ChangeConsolPosition(collect, poleMat);
        //                                }
        //                            }
        //                            swCatOut = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }


        //        // move its arc
        //        foreach (ObjectId oi in consolSub.SubIdCollection)
        //        {
        //            //ed.writeMessage("MoveConsol 7-2 \n");
        //            Atend.Base.Acad.AT_INFO subInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);

        //            foreach (ObjectId oi1 in EntitySub.SubIdCollection)
        //            {

        //                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi1);
        //                if (subInfo.ParentCode != "NONE" && subInfo.NodeType == (int)Atend.Control.Enum.ProductType.TensionArc)
        //                {
        //                    //ed.writeMessage("arc was found. \n");
        //                    ChangeArcForSpecialConsol(Consol);
        //                }

        //                //ed.WriteMessage("MoveConsol 7-3 \n");
        //                //ed.WriteMessage("#######{0} \n", at_info.NodeCode.ToString());
        //                //ed.WriteMessage("#######{0},{1},{2} \n", at_info.NodeType, (int)Atend.Control.Enum.ProductType.Conductor, at_info.ParentCode);
        //                ////if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //                ////{
        //                ////    ed.WriteMessage("nextconsol was found. \n");
        //                ////    ChangeArcForSpecialConsol(Atend.Global.Acad.UAcad.GetNextConsol(Consol, new Guid(at_info.NodeCode)));
        //                ////}

        //            }

        //        }

        //        for (int i = 0; i < arrayNextConsol.Count; i++)
        //        {
        //            ChangeArcForSpecialConsol(Atend.Global.Acad.UAcad.GetNextConsol(Consol, new Guid(arrayNextConsol[i].ToString())));
        //        }
        //        // end move its arc
        //    }

        //    isMoveConsolOnly = true;
        //    //swch = true;
        //}

        //public static void MoveTensioArc(ObjectId Consol, ObjectId Conductor, ObjectId TensionArc)
        //{
        //    if (AllowToMove)
        //    {
        //    }
        //}

        //private static void MoveConductor(ObjectId Consol, ObjectId Conductor, Point3d LastCenterPoint)
        //{
        //    Line LastConductorEntity = new Line();
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d ConsolCenterPoint = Point3d.Origin;

        //        // compute center point of consol
        //        Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(Consol);
        //        if (consolEntity != null)
        //        {
        //            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //        }

        //        // end compute center point of consol

        //        //Database db = HostApplicationServices.WorkingDatabase;
        //        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {
        //            //for position of next consol
        //            Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(Conductor);
        //            ObjectId io2 = Atend.Global.Acad.UAcad.GetNextConsol(Consol, new Guid(at_info1.NodeCode));
        //            //Entity en = Atend.Global.Acad.UAcad.GetEntityByObjectID(io2);
        //            Point3d p3 = Point3d.Origin;
        //            p3 = Atend.Global.Acad.UAcad.CenterOfEntity((Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(io2));

        //            //


        //            //ed.WriteMessage("MoveConductor 4 \n");
        //            Entity ent = (Entity)tr.GetObject(Conductor, OpenMode.ForWrite);

        //            //ed.WriteMessage("MoveConductor 5 \n");
        //            Line LineEntity = (Line)ent;

        //            if (LineEntity != null)
        //            {
        //                LastConductorEntity.StartPoint = LineEntity.StartPoint;
        //                LastConductorEntity.EndPoint = LineEntity.EndPoint;
        //                //ed.WriteMessage("MoveConductor 6 \n");
        //                //ed.writeMessage("{0},{1} \n", ConsolCenterPoint.ToString(), LastCenterPoint.ToString());
        //                if (LineEntity.StartPoint == LastCenterPoint)
        //                {
        //                    //ed.WriteMessage("MoveConductor 7 \n");
        //                    LineEntity.StartPoint = ConsolCenterPoint;
        //                    LineEntity.EndPoint = p3;
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("MoveConductor 8 \n");
        //                    LineEntity.EndPoint = ConsolCenterPoint;
        //                    LineEntity.StartPoint = p3;
        //                }

        //            }

        //            tr.Commit();
        //        }


        //        // move its comment here
        //        Atend.Base.Acad.AT_SUB conductorSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(Conductor);
        //        //ed.WriteMessage("MoveConductor 10 \n");
        //        foreach (ObjectId oi in conductorSub.SubIdCollection)
        //        {
        //            //ed.WriteMessage("MoveConductor 11 \n");
        //            Atend.Base.Acad.AT_INFO subInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            //ed.writeMessage("-->{0},,,{1} \n", subInfo.NodeType.ToString(), subInfo.ParentCode.ToString());

        //            if (subInfo.ParentCode != "NONE" && subInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                //ed.writeMessage("Comment was found. \n");
        //                MoveConductorComment(Conductor, oi);
        //            }
        //            //move jumper
        //            if (subInfo.ParentCode != "NONE" && subInfo.NodeType == (int)Atend.Control.Enum.ProductType.Jumper)
        //            {
        //                ed.WriteMessage("Jumper was found. \n");
        //                MoveJumper(Conductor, oi, LastConductorEntity);
        //            }
        //            //end move jumper
        //        }

        //        // end move its comment here





        //    }

        //}

        //private static void MoveConductorComment(ObjectId Conductor, ObjectId Comment)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        //Database db = HostApplicationServices.WorkingDatabase;
        //        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {

        //            DBText ent = (DBText)tr.GetObject(Comment, OpenMode.ForWrite);
        //            Line ConductorEntity = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(Conductor);
        //            if (ConductorEntity != null)
        //            {
        //                ent.Position = new LineSegment3d(ConductorEntity.StartPoint, ConductorEntity.EndPoint).MidPoint;
        //                ent.Rotation = new Point2d(ConductorEntity.StartPoint.X, ConductorEntity.StartPoint.Y).GetVectorTo(new Point2d(ConductorEntity.EndPoint.X, ConductorEntity.EndPoint.Y)).Angle;
        //            }

        //            tr.Commit();
        //        }
        //    }
        //}

        //private static void MoveJumper(ObjectId Conductor, ObjectId Jumper, Line LastCondutor)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();

        //        //Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //        //using (Transaction tr = db.TransactionManager.StartTransaction())
        //        //{
        //        //    Entity ent = (Entity)tr.GetObject(Jumper, OpenMode.ForWrite);
        //        //    Line LineEntity = (Line)ent;
        //        //    Line ConductorLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(Conductor);
        //        //    LineSegment3d LineSeg = new LineSegment3d(ConductorLine.StartPoint, ConductorLine.EndPoint);
        //        //    LineSegment3d LineSegLast = new LineSegment3d(LastCondutor.StartPoint, LastCondutor.EndPoint);
        //        //    bool sw1 = LineSegLast.IsOn(LineEntity.StartPoint);
        //        //    bool sw2 = LineSegLast.IsOn(LineEntity.EndPoint);
        //        //    Point3d p;
        //        //    if (sw1)
        //        //    {
        //        //        p = LineSeg.GetClosestPointTo(LineEntity.StartPoint).Point;
        //        //        LineEntity.StartPoint = p;
        //        //    }
        //        //    else
        //        //    {
        //        //        p = LineSeg.GetClosestPointTo(LineEntity.EndPoint).Point;
        //        //        LineEntity.EndPoint = p;
        //        //    }

        //        //    //ed.writeMessage("move jumper 6\n");


        //        //    tr.Commit();
        //        //    //ed.writeMessage("move jumper 7\n");
        //        //}

        //        ////if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(Jumper))
        //        ////{
        //        ////    ed.WriteMessage("Jumper Not Removed\n");
        //        ////}
        //        ////Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));


        //        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {
        //            Entity ent = (Entity)tr.GetObject(Jumper, OpenMode.ForWrite);
        //            Polyline LineEntity = (Polyline)ent;
        //            Line ConductorLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(Conductor);
        //            LineSegment3d LineSeg = new LineSegment3d(ConductorLine.StartPoint, ConductorLine.EndPoint);
        //            LineSegment3d LineSegLast = new LineSegment3d(LastCondutor.StartPoint, LastCondutor.EndPoint);
        //            bool sw1 = LineSegLast.IsOn(LineEntity.StartPoint);
        //            bool sw2 = LineSegLast.IsOn(LineEntity.EndPoint);
        //            Point3d p;
        //            if (sw1)
        //            {
        //                //p = LineSeg.GetClosestPointTo(LineEntity.StartPoint).Point;
        //                //double d = LineSeg.StartPoint.DistanceTo(LineEntity.StartPoint);
        //                //ed.WriteMessage("d:  {0}\n", d);
        //                //Vector3d v = LineSeg.StartPoint - LineSeg.EndPoint;
        //                //ed.WriteMessage("v:  {0},{1},{2}\n", v.X, v.Y, v.Z);
        //                //Vector3d vn = v.GetNormal();
        //                //ed.WriteMessage("vn:  {0},{1},{2}\n", vn.X, vn.Y, vn.Z);z
        //                //LineEntity.SetPointAt(0, new Point2d(p.X, p.Y));

        //                p = LineSeg.GetClosestPointTo(LineEntity.StartPoint).Point;
        //                ed.WriteMessage("p: {0}\n", p);
        //                LineEntity.SetPointAt(0, new Point2d(p.X, p.Y));
        //            }
        //            else
        //            {
        //                //p = LineSeg.GetClosestPointTo(LineEntity.StartPoint).Point;
        //                //double d = LineSeg.StartPoint.DistanceTo(LineEntity.EndPoint);
        //                //ed.WriteMessage("d:  {0}\n", d);
        //                //Vector3d v = LineSeg.StartPoint - LineSeg.EndPoint;
        //                //ed.WriteMessage("v:  {0},{1},{2}\n", v.X, v.Y, v.Z);
        //                //Vector3d vn = v.GetNormal();
        //                //ed.WriteMessage("vn:  {0},{1},{2}\n", vn.X, vn.Y, vn.Z);
        //                //LineEntity.SetPointAt(1, new Point2d(p.X, p.Y));

        //                p = LineSeg.GetClosestPointTo(LineEntity.EndPoint).Point;
        //                ed.WriteMessage("p: {0}\n", p);
        //                LineEntity.SetPointAt(1, new Point2d(p.X, p.Y));
        //            }
        //            tr.Commit();
        //        }
        //    }
        //}

        //private static void ReturnConsolEntity(ObjectId Consol)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ////Polyline lastPoly = (Polyline)Atend.Global.Acad.AcadMove.CurrentEntity;
        //    ////if (lastPoly != null)
        //    ////{
        //    ////    for (int i = 0; i < lastPoly.NumberOfVertices; i++)
        //    ////    {
        //    ////        ed.WriteMessage("LPOINT: {0} \n", lastPoly.GetPoint2dAt(i));
        //    ////    }
        //    ////}

        //    //Database db = HostApplicationServices.WorkingDatabase;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    using (Transaction tr = db.TransactionManager.StartTransaction())
        //    {
        //        //ed.writeMessage("ReturnConsolEntity 1 \n");
        //        Polyline pLine = tr.GetObject(Consol, OpenMode.ForWrite) as Polyline;
        //        if (pLine != null)
        //        {
        //            //ed.writeMessage("ReturnConsolEntity 2 \n");
        //            //for (int i = pLine.NumberOfVertices - 1; i >= 0; i--)
        //            //{
        //            //    ed.WriteMessage("ReturnConsolEntity 3 \n");
        //            //    pLine.RemoveVertexAt(i);
        //            //}

        //            if (CurrentEntity != null)
        //            {
        //                //ed.writeMessage("ReturnConsolEntity not null \n");
        //                Polyline LastEntity = CurrentEntity as Polyline;
        //                //ed.writeMessage("ReturnConsolEntity 4 \n");
        //                if (LastEntity != null)
        //                {


        //                    //ed.writeMessage("ReturnConsolEntity 5 \n");

        //                    for (int i = pLine.NumberOfVertices - 1; i >= 0; i--)
        //                    {
        //                        //ed.writeMessage("ReturnConsolEntity 3 \n");
        //                        pLine.SetPointAt(i, LastEntity.GetPoint2dAt(i));
        //                    }

        //                    ////for (int i = LastEntity.NumberOfVertices - 1; i >= 0; i--)
        //                    //{
        //                    //ed.writeMessage("point: {0} \n", LastEntity.GetPoint2dAt(i));
        //                    //pLine.SetPointAt(i, LastEntity.GetPoint2dAt(i));
        //                    //}

        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);
        //                    pLine.SetPointAt(0, LastEntity.GetPoint2dAt(0));
        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);
        //                    pLine.SetPointAt(1, LastEntity.GetPoint2dAt(1));
        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);
        //                    pLine.SetPointAt(2, LastEntity.GetPoint2dAt(2));
        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);
        //                    pLine.SetPointAt(3, LastEntity.GetPoint2dAt(3));
        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);
        //                    pLine.SetPointAt(4, LastEntity.GetPoint2dAt(4));
        //                    //ed.writeMessage("NV {0} \n", pLine.NumberOfVertices);

        //                }
        //            }
        //        }
        //        //ed.WriteMessage("ReturnConsolEntity 6 \n");
        //        tr.Commit();
        //    }

        //}

        //private static void ReturnHeaderEntity(ObjectId Header)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //    using (Transaction tr = db.TransactionManager.StartTransaction())
        //    {
        //        Polyline pLine = tr.GetObject(Header, OpenMode.ForWrite) as Polyline;
        //        if (pLine != null)
        //        {
        //            if (CurrentEntity != null)
        //            {
        //                Polyline LastEntity = CurrentEntity as Polyline;
        //                if (LastEntity != null)
        //                {
        //                    for (int i = pLine.NumberOfVertices - 1; i >= 0; i--)
        //                    {
        //                        pLine.SetPointAt(i, LastEntity.GetPoint2dAt(i));
        //                    }
        //                    pLine.SetPointAt(0, LastEntity.GetPoint2dAt(0));
        //                    pLine.SetPointAt(1, LastEntity.GetPoint2dAt(1));
        //                    pLine.SetPointAt(2, LastEntity.GetPoint2dAt(2));
        //                    pLine.SetPointAt(3, LastEntity.GetPoint2dAt(3));
        //                }
        //            }
        //        }
        //        tr.Commit();
        //    }
        //}

        //private static void ChangeArcForSpecialConsol(ObjectId ContainerConsol)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("start change arc\n");
        //    //Point3d CenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity((Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(ContainerConsol));
        //    Point3d CenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(ContainerConsol));
        //    double Radius = 0, StartAngle = 0, EndAngle = 0;
        //    Line ImaginaryLine = new Line();
        //    ObjectId TensionArcOI = ObjectId.Null;
        //    //ed.WriteMessage("MoveArc 1 \n");

        //    Atend.Base.Acad.AT_SUB at_sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(ContainerConsol);
        //    //ed.WriteMessage("1 : {0} \n", ContainerConsol);
        //    //ed.WriteMessage("MoveArc 2 \n");
        //    foreach (ObjectId oi in at_sub.SubIdCollection)
        //    {
        //        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //        //ed.WriteMessage("22 : {0} \n", oi);
        //        if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
        //        {
        //            //ed.WriteMessage("MoveArc 3 \n");
        //            //ed.WriteMessage("conductor was found \n");
        //            Line ConductorLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
        //            if (ConductorLine.StartPoint == CenterPoint)
        //            {
        //                //ed.WriteMessage("MoveArc 4 \n");
        //                ImaginaryLine.StartPoint = CenterPoint;
        //                ImaginaryLine.EndPoint = ConductorLine.EndPoint;
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("MoveArc 5 \n");
        //                ImaginaryLine.StartPoint = ConductorLine.EndPoint;
        //                ImaginaryLine.EndPoint = ConductorLine.StartPoint;
        //            }
        //            //ed.WriteMessage("StartPoint:{0}\nEndPoint:{1}\n",ImaginaryLine.StartPoint,ImaginaryLine.EndPoint);
        //            //ed.WriteMessage("MoveArc 6 \n");
        //            double angle = (180 * ImaginaryLine.Angle) / Math.PI;
        //            StartAngle = angle - 15;
        //            EndAngle = angle + 15;

        //            StartAngle = (StartAngle * Math.PI) / 180;
        //            EndAngle = (EndAngle * Math.PI) / 180;



        //        }
        //        else if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip)
        //        {

        //            //ed.WriteMessage("Pole Parent was found \n");
        //            Polyline poleEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //            if (poleEntity != null)
        //            {

        //                switch (poleEntity.NumberOfVertices)
        //                {
        //                    case 5:
        //                        LineSegment3d LS1 = poleEntity.GetLineSegmentAt(0);
        //                        //ed.WriteMessage("{0}:{1}\n",LS1.StartPoint , LS1.EndPoint);
        //                        LineSegment3d LS2 = poleEntity.GetLineSegmentAt(1);
        //                        //ed.WriteMessage("{0}:{1}\n", LS2.StartPoint, LS2.EndPoint);
        //                        LineSegment3d ls3 = new LineSegment3d(LS1.StartPoint, LS2.EndPoint);
        //                        //ed.WriteMessage("{0}:{1}\n", ls3.StartPoint, ls3.EndPoint);
        //                        Radius = ls3.Length / 2;

        //                        break;
        //                    case 7:
        //                        LineSegment3d LS11 = poleEntity.GetLineSegmentAt(0);
        //                        //ed.WriteMessage("{0}:{1}\n",LS1.StartPoint , LS1.EndPoint);
        //                        LineSegment3d LS21 = poleEntity.GetLineSegmentAt(2);
        //                        //ed.WriteMessage("{0}:{1}\n", LS2.StartPoint, LS2.EndPoint);
        //                        LineSegment3d ls31 = new LineSegment3d(LS11.StartPoint, LS21.EndPoint);
        //                        //ed.WriteMessage("{0}:{1}\n", ls3.StartPoint, ls3.EndPoint);
        //                        Radius = ls31.Length / 2;
        //                        break;
        //                }

        //            }
        //            else
        //            {
        //                Circle poleEntity1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Circle;
        //                if (poleEntity1 != null)
        //                {
        //                    Radius = poleEntity1.Radius;
        //                }
        //            }

        //        }
        //        else if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.TensionArc)
        //        {
        //            TensionArcOI = oi;
        //        }
        //    }

        //    Arc a = new Arc(CenterPoint, Radius, StartAngle, EndAngle);


        //    if (TensionArcOI != ObjectId.Null)
        //    {
        //        //Database db = HostApplicationServices.WorkingDatabase;
        //        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //        using (Transaction tr = db.TransactionManager.StartTransaction())
        //        {
        //            Arc ent = (Arc)tr.GetObject(TensionArcOI, OpenMode.ForWrite);
        //            if (ent != null)
        //            {
        //                ent.Center = CenterPoint;
        //                ent.Radius = Radius;
        //                ent.StartAngle = StartAngle;
        //                ent.EndAngle = EndAngle;
        //            }
        //            tr.Commit();
        //        }
        //    }

        //    //ed.WriteMessage("end change arc\n");

        //}

        //public static void MoveKhazan(ObjectId Khazan)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    Point3d LSP = LastCenterPoint;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d currentCPConsolElse = new Point3d();
        //        Matrix3d matConsolElse = new Matrix3d();
        //        bool sw = false;
        //        ArrayList arpol = new ArrayList();
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(Khazan);
        //        ObjectIdCollection CollectionKhazan = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in CollectionKhazan)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.ConsolElse)
        //            {
        //                currentCPConsolElse = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                Point3d KhazanCenterPoint = Point3d.Origin;
        //                Polyline KhazanEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (KhazanEntity != null)
        //                {
        //                    KhazanCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(KhazanEntity);
        //                }
        //                Entity Parent = null;
        //                foreach (ObjectId oi in EntitySub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                    if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //                    {
        //                        Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);// as Polyline;
        //                        //for move command
        //                        arpol.Add(oi);
        //                    }
        //                }
        //                Curve ParentCurve = Parent as Curve;
        //                if (ParentCurve != null)
        //                {
        //                    if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, KhazanCenterPoint) == false)
        //                    {
        //                        ReturnConsolEntity(collect);
        //                        sw = true;
        //                    }
        //                    else
        //                    {
        //                        //Move Comment Of Khazan
        //                        ObjectId oicommand = (ObjectId)arpol[0];
        //                        EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oicommand);
        //                        foreach (ObjectId oisub in EntitySub.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO poleInfosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
        //                            if (poleInfosub.ParentCode != "NONE" && poleInfosub.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
        //                            {
        //                                Atend.Base.Acad.AT_SUB KhazanCollection = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oisub);
        //                                foreach (ObjectId obj in KhazanCollection.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //                                    if (at_infosub.ParentCode != "NONE" && at_infosub.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                    {
        //                                        Point3d currentcenetpoint1 = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                                        Matrix3d mat = Matrix3d.Displacement(currentcenetpoint1 - LastCenterPoint);
        //                                        ChangeConsolPosition(obj, mat);
        //                                    }
        //                                }

        //                            }
        //                        }
        //                        arpol.RemoveAt(0);
        //                    }
        //                }

        //            }

        //        }
        //        if (sw)
        //        {
        //            foreach (ObjectId collect in CollectionKhazan)
        //            {
        //                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
        //                {
        //                    matConsolElse = Matrix3d.Displacement(LSP - currentCPConsolElse);
        //                    ChangeConsolPosition(collect, matConsolElse);
        //                }
        //            }
        //        }

        //    }
        //}

        //public static void MoveRod(ObjectId Rod)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    Point3d LSP = LastCenterPoint;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d currentCPConsolElse = new Point3d();
        //        Matrix3d matConsolElse = new Matrix3d();
        //        bool sw = false;
        //        ArrayList arpol = new ArrayList();
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(Rod);
        //        ObjectIdCollection CollectionRod = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in CollectionRod)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.ConsolElse)
        //            {
        //                currentCPConsolElse = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                Point3d RodCenterPoint = Point3d.Origin;
        //                Polyline RodEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (RodEntity != null)
        //                {
        //                    RodCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(RodEntity);
        //                }
        //                Entity Parent = null;
        //                foreach (ObjectId oi in EntitySub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                    if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //                    {
        //                        Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);// as Polyline;
        //                        //for move commant
        //                        arpol.Add(oi);
        //                    }
        //                }
        //                Curve ParentCurve = Parent as Curve;
        //                if (ParentCurve != null)
        //                {
        //                    if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, RodCenterPoint) == false)
        //                    {
        //                        ReturnConsolEntity(collect);
        //                        sw = true;
        //                    }
        //                    else
        //                    {
        //                        //Move Comment Of rod
        //                        ObjectId oicommand = (ObjectId)arpol[0];
        //                        EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oicommand);
        //                        foreach (ObjectId oisub in EntitySub.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO poleInfosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
        //                            if (poleInfosub.ParentCode != "NONE" && poleInfosub.NodeType == (int)Atend.Control.Enum.ProductType.Rod)
        //                            {
        //                                Atend.Base.Acad.AT_SUB KhazanCollection = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oisub);
        //                                foreach (ObjectId obj in KhazanCollection.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //                                    if (at_infosub.ParentCode != "NONE" && at_infosub.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                    {
        //                                        Point3d currentcenetpoint1 = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                                        Matrix3d mat = Matrix3d.Displacement(currentcenetpoint1 - LastCenterPoint);
        //                                        ChangeConsolPosition(obj, mat);
        //                                    }
        //                                }

        //                            }
        //                        }
        //                        arpol.RemoveAt(0);
        //                    }

        //                }

        //            }

        //        }
        //        if (sw)
        //        {
        //            foreach (ObjectId collect in CollectionRod)
        //            {
        //                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Rod)
        //                {
        //                    matConsolElse = Matrix3d.Displacement(LSP - currentCPConsolElse);
        //                    ChangeConsolPosition(collect, matConsolElse);
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void MoveGroundPost(ObjectId GroundPost)
        //{
        //    ObjectId NewGP = GroundPost;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        ArrayList arrIsMoveCabel = new ArrayList();
        //        Point3d CenterGP = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewGP));
        //        Matrix3d mat = Matrix3d.Displacement(CenterGP - LastCenterPoint);
        //        ObjectIdCollection headerobji = new ObjectIdCollection();
        //        foreach (ObjectId obj in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //            {
        //                Point3d lsp = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(obj));
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                ChangeConsolPosition(obj, mat);
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(obj) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(obj);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(obj));
        //                        if (cener != null)
        //                        {
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    if (cabelent.StartPoint == lsp)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(HeaderCenterPoint.X, HeaderCenterPoint.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == lsp)
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(HeaderCenterPoint.X, HeaderCenterPoint.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = lsp;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = lsp;
        //                                }
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect, matcom);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(objsub, mat);
        //                    }
        //                }
        //            }
        //            ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(at_info.SelectedObjectId);
        //            if (Collection.Count == 0)
        //            {
        //                if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                {
        //                    ChangeConsolPosition(obj, mat);
        //                }
        //            }
        //            else
        //            {
        //                bool ismove = false;
        //                foreach (ObjectId collect in Collection)
        //                {
        //                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Transformer)
        //                    {
        //                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
        //                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                        foreach (ObjectId h in sub2.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (ismove == false)
        //                                {
        //                                    ChangeConsolPosition(h, mat);
        //                                }
        //                                ismove = true;
        //                            }
        //                        }
        //                    }
        //                    ChangeConsolPosition(collect, mat);

        //                    //for out of area headercabel's in GP 
        //                    Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                    foreach (ObjectId objsub in sub.SubIdCollection)
        //                    {
        //                        bool sw = false;
        //                        Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                        if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.KablSho)) //XXX
        //                        {
        //                            for (int i = 0; i < arrIsMoveCabel.Count; i++)
        //                            {
        //                                if (arrIsMoveCabel[i].ToString() == objsub.ToString())
        //                                    sw = true;
        //                            }
        //                            if (!sw)
        //                            {
        //                                Atend.Base.Acad.AT_SUB subcable = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(at_info1.SelectedObjectId);
        //                                foreach (ObjectId headeroi in subcable.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infocom = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(headeroi);
        //                                    if (at_infocom.ParentCode != "NONE" && at_infocom.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                    {
        //                                        ChangeConsolPosition(headeroi, mat);
        //                                    }
        //                                    if (at_infocom.ParentCode != "NONE" && at_infocom.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                                    {
        //                                        Polyline p1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewGP) as Polyline;
        //                                        if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)p1, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(headeroi))) == false)
        //                                        {
        //                                            headerobji.Add(headeroi);
        //                                        }
        //                                    }
        //                                }
        //                                arrIsMoveCabel.Add(objsub);
        //                                ChangeConsolPosition(objsub, mat);
        //                            }
        //                            else
        //                            {

        //                            }
        //                        }
        //                    }
        //                }
        //            }


        //        }

        //        foreach (ObjectId oi in headerobji)
        //        {
        //            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //            if (cener != null)
        //            {
        //                Atend.Base.Acad.AT_SUB sb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in sb.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);

        //                    if (infosub.ParentCode != "NONE" && infosub.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        //Database db = HostApplicationServices.WorkingDatabase;
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                            Polyline cabelent = ent as Polyline;
        //                            if (cabelent != null)
        //                            {
        //                                if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewGP), cabelent.GetPoint3dAt(0)) == false)//start
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(0, new Point2d(hc.X, hc.Y));
        //                                }
        //                                else if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewGP), cabelent.GetPoint3dAt(cabelent.NumberOfVertices - 1)) == false)//end
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(hc.X, hc.Y));
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }


        //                    }
        //                }
        //            }
        //        }

        //    }
        //}

        //public static void MoveAirPost(ObjectId AirPost)
        //{
        //    ObjectId NewAir = AirPost;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        ArrayList arrIsMoveCabel = new ArrayList();
        //        Point3d CenterAir = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewAir));
        //        Matrix3d mat = Matrix3d.Displacement(CenterAir - LastCenterPoint);
        //        ObjectIdCollection headerobji = new ObjectIdCollection();
        //        foreach (ObjectId obj in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //            {
        //                Point3d lsp = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(obj));
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                ChangeConsolPosition(obj, mat);
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(obj) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(obj);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(obj));
        //                        if (cener != null)
        //                        {
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    if (cabelent.StartPoint == lsp)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(HeaderCenterPoint.X, HeaderCenterPoint.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == lsp)
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(HeaderCenterPoint.X, HeaderCenterPoint.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = lsp;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = lsp;
        //                                }
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect, matcom);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        ChangeConsolPosition(objsub, mat);
        //                    }
        //                }
        //            }
        //            ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(at_info.SelectedObjectId);
        //            if (Collection.Count == 0)
        //            {
        //                if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                {
        //                    ChangeConsolPosition(obj, mat);
        //                }
        //            }
        //            else
        //            {
        //                bool ismove = false;
        //                foreach (ObjectId collect in Collection)
        //                {
        //                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Transformer)
        //                    {
        //                        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(collect);
        //                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                        foreach (ObjectId h in sub2.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (ismove == false)
        //                                {
        //                                    ChangeConsolPosition(h, mat);
        //                                }
        //                                ismove = true;
        //                            }
        //                        }
        //                    }

        //                    ChangeConsolPosition(collect, mat);

        //                    //for out of area headercabel's (in Airpost) 
        //                    Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                    foreach (ObjectId objsub in sub.SubIdCollection)
        //                    {
        //                        bool sw = false;
        //                        Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                        if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                        {
        //                            for (int i = 0; i < arrIsMoveCabel.Count; i++)
        //                            {
        //                                if (arrIsMoveCabel[i].ToString() == objsub.ToString())
        //                                    sw = true;
        //                            }
        //                            if (!sw)
        //                            {
        //                                Atend.Base.Acad.AT_SUB subcable = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(at_info1.SelectedObjectId);
        //                                foreach (ObjectId headeroi in subcable.SubIdCollection)
        //                                {
        //                                    Atend.Base.Acad.AT_INFO at_infocom = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(headeroi);
        //                                    if (at_infocom.ParentCode != "NONE" && at_infocom.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                    {
        //                                        ChangeConsolPosition(headeroi, mat);
        //                                    }
        //                                    if (at_infocom.ParentCode != "NONE" && at_infocom.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                                    {
        //                                        Polyline p1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewAir) as Polyline;
        //                                        if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)p1, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(headeroi))) == false)
        //                                        {
        //                                            headerobji.Add(headeroi);
        //                                        }
        //                                    }
        //                                }
        //                                arrIsMoveCabel.Add(objsub);
        //                                ChangeConsolPosition(objsub, mat);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        foreach (ObjectId oi in headerobji)
        //        {
        //            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //            if (cener != null)
        //            {
        //                Atend.Base.Acad.AT_SUB sb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in sb.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);

        //                    if (infosub.ParentCode != "NONE" && infosub.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        //Database db = HostApplicationServices.WorkingDatabase;
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                            Polyline cabelent = ent as Polyline;
        //                            if (cabelent != null)
        //                            {
        //                                if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewAir), cabelent.GetPoint3dAt(0)) == false)//start
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(0, new Point2d(hc.X, hc.Y));
        //                                }
        //                                else if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewAir), cabelent.GetPoint3dAt(cabelent.NumberOfVertices - 1)) == false)//end
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(hc.X, hc.Y));
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }


        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void MoveCabel(ObjectId Cabel)
        //{
        //    ReturnConsolEntity(Cabel);
        //}

        //public static void MoveStreetBox(ObjectId StreetBox)
        //{
        //    ObjectId NewStreetBox = StreetBox;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterStreetBox = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox));
        //        Matrix3d mat = Matrix3d.Displacement(CenterStreetBox - LastCenterPoint);

        //        ObjectIdCollection headerobji = new ObjectIdCollection();

        //        //Move Ground 
        //        Atend.Base.Acad.AT_SUB Collection1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(StreetBox);
        //        foreach (ObjectId obj in Collection1.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //            {
        //                ObjectIdCollection Coll = Atend.Global.Acad.UAcad.GetGroupSubEntities(obj);
        //                foreach (ObjectId col in Coll)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(col);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //                    {
        //                        ChangeConsolPosition(col, mat);
        //                    }
        //                }
        //            }
        //        }

        //        //Move Cabel
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(NewStreetBox);
        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in Collection)
        //        {
        //            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //            {
        //                //_____________________
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                //_____________________
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        if (cener != null)
        //                        {

        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                                    //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                                    //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                                    double x = Math.Abs(cabelent.StartPoint.X - LastCenterPoint.X);
        //                                    double y = Math.Abs(cabelent.EndPoint.X - LastCenterPoint.X);
        //                                    if (cabelent.StartPoint == LastCenterPoint || x < y)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || x > y)//|| (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect001 in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo001 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect001);
        //                            //ed.WriteMessage("### :{0}\n", atinfo001.NodeType);
        //                            if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = LastCenterPoint;
        //                                }
        //                                //ed.WriteMessage("LastCenterPoint   : {0}\n", LastCenterPoint);
        //                                //ed.WriteMessage("HeaderCenterPoint : {0}\n", HeaderCenterPoint);
        //                                //ed.WriteMessage("CLine.StartPoint  : {0}\n", CLine.StartPoint);
        //                                //ed.WriteMessage("CLine.EndPoint    : {0}\n", CLine.EndPoint);
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect001, matcom);
        //                                }
        //                            }
        //                        }
        //                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //                        //+++++++++++++++++++++++++++++++++
        //                        //Atend.Base.Acad.AT_SUB subcable = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(at_info1.SelectedObjectId);
        //                        //foreach (ObjectId headeroi in subcable.SubIdCollection)
        //                        //{
        //                        //    Polyline p1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox) as Polyline;
        //                        //    if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)p1, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(headeroi))) == false)
        //                        //    {
        //                        //        headerobji.Add(headeroi);
        //                        //    }
        //                        //}
        //                        //ChangeConsolPosition(objsub, mat);
        //                        //+++++++++++++++++++++++++++++++++
        //                    }
        //                }
        //            }

        //        }
        //        foreach (ObjectId oi in headerobji)
        //        {
        //            //ed.WriteMessage("@id: {0}\n", oi); 
        //            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //            if (cener != null)
        //            {
        //                Atend.Base.Acad.AT_SUB sb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in sb.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
        //                    if (infosub.ParentCode != "NONE" && infosub.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        //ed.WriteMessage("yes\n");
        //                        ///////////////////////////////////////////
        //                        Point3d start = new Point3d();
        //                        Point3d end = new Point3d();
        //                        Point3d HeaderCenterPoint = Point3d.Origin;
        //                        Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi) as Polyline;
        //                        if (HeaderEntity != null)
        //                        {
        //                            HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                        }

        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(oii);
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                                    //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                                    //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                                    if (cabelent.StartPoint == LastCenterPoint)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oii);
        //                        foreach (ObjectId collect in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                            //ed.WriteMessage("### :{0}\n", atinfo.NodeType);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = LastCenterPoint;
        //                                }
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect, matcom);
        //                                }
        //                            }
        //                        }
        //                        //////////////////////////////////////////
        //                        //**************************
        //                        //Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        //using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        //{
        //                        //    Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                        //    Polyline cabelent = ent as Polyline;
        //                        //    if (cabelent != null)
        //                        //    {
        //                        //        if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox), cabelent.GetPoint3dAt(0)) == false)//start
        //                        //        {
        //                        //            ed.WriteMessage("@#1\n");
        //                        //            Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                        //            cabelent.SetPointAt(0, new Point2d(hc.X, hc.Y));
        //                        //        }
        //                        //        else if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox), cabelent.GetPoint3dAt(cabelent.NumberOfVertices - 1)) == false)//end
        //                        //        {
        //                        //            ed.WriteMessage("@#2\n");
        //                        //            Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                        //            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(hc.X, hc.Y));
        //                        //        }
        //                        //    }
        //                        //    tr.Commit();
        //                        //}
        //                        //**************************
        //                    }
        //                }
        //            }
        //        }




        //        ////////////////////
        //        /*ed.WriteMessage("1\n");
        //        Point3d CenterStreetBox = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox));
        //        Matrix3d mat = Matrix3d.Displacement(CenterStreetBox - LastCenterPoint);
        //        ObjectIdCollection headerobji = new ObjectIdCollection();
        //        foreach (ObjectId obj in EntitySub.SubIdCollection)
        //        {
        //            ed.WriteMessage("2\n");
        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(at_info.SelectedObjectId);

        //            foreach (ObjectId collect in Collection)
        //            {
        //                ed.WriteMessage("3\n");
        //                ChangeConsolPosition(collect, mat);
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Atend.Base.Acad.AT_SUB subcable = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(at_info1.SelectedObjectId);
        //                        foreach (ObjectId headeroi in subcable.SubIdCollection)
        //                        {
        //                            Polyline p1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox) as Polyline;
        //                            if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)p1, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(headeroi))) == false)
        //                            {
        //                                headerobji.Add(headeroi);
        //                            }
        //                        }
        //                        ChangeConsolPosition(objsub, mat);
        //                    }
        //                }
        //            }

        //        }

        //        foreach (ObjectId oi in headerobji)
        //        {
        //            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //            if (cener != null)
        //            {
        //                Atend.Base.Acad.AT_SUB sb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in sb.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
        //                    if (infosub.ParentCode != "NONE" && infosub.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Database db = HostApplicationServices.WorkingDatabase;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                            Polyline cabelent = ent as Polyline;
        //                            if (cabelent != null)
        //                            {
        //                                if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox), cabelent.GetPoint3dAt(0)) == false)//start
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(0, new Point2d(hc.X, hc.Y));
        //                                }
        //                                else if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewStreetBox), cabelent.GetPoint3dAt(cabelent.NumberOfVertices - 1)) == false)//end
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(hc.X, hc.Y));
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                }
        //            }
        //        }*/
        //    }
        //}

        //public static void MoveDB(ObjectId DB)
        //{
        //    ObjectId NewDB = DB;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterDB = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewDB));
        //        Matrix3d mat = Matrix3d.Displacement(CenterDB - LastCenterPoint);

        //        ObjectIdCollection headerobji = new ObjectIdCollection();

        //        //Move Ground 
        //        Atend.Base.Acad.AT_SUB Collection1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(DB);
        //        foreach (ObjectId obj in Collection1.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //            {
        //                ObjectIdCollection Coll = Atend.Global.Acad.UAcad.GetGroupSubEntities(obj);
        //                foreach (ObjectId col in Coll)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(col);
        //                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
        //                    {
        //                        ChangeConsolPosition(col, mat);
        //                    }
        //                }
        //            }
        //        }

        //        //Move Cabel
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(NewDB);
        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in Collection)
        //        {
        //            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //            {
        //                //_____________________
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                //_____________________
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        if (cener != null)
        //                        {

        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                                    //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                                    //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                                    double x = Math.Abs(cabelent.StartPoint.X - LastCenterPoint.X);
        //                                    double y = Math.Abs(cabelent.EndPoint.X - LastCenterPoint.X);
        //                                    if (cabelent.StartPoint == LastCenterPoint || x < y)
        //                                    {
        //                                        cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || x > y)//|| (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                                    {
        //                                        cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collect001 in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo001 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect001);
        //                            //ed.WriteMessage("### :{0}\n", atinfo001.NodeType);
        //                            if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = LastCenterPoint;
        //                                }
        //                                //ed.WriteMessage("LastCenterPoint   : {0}\n", LastCenterPoint);
        //                                //ed.WriteMessage("HeaderCenterPoint : {0}\n", HeaderCenterPoint);
        //                                //ed.WriteMessage("CLine.StartPoint  : {0}\n", CLine.StartPoint);
        //                                //ed.WriteMessage("CLine.EndPoint    : {0}\n", CLine.EndPoint);
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    ChangeConsolPosition(collect001, matcom);
        //                                }
        //                            }
        //                        }


        //                        //Atend.Base.Acad.AT_SUB subcable = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(at_info1.SelectedObjectId);
        //                        //foreach (ObjectId headeroi in subcable.SubIdCollection)
        //                        //{
        //                        //    Polyline p1 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewDB) as Polyline;
        //                        //    if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)p1, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(headeroi))) == false)
        //                        //    {
        //                        //        headerobji.Add(headeroi);
        //                        //    }
        //                        //}
        //                        //ChangeConsolPosition(objsub, mat);
        //                    }
        //                }
        //            }

        //        }
        //        foreach (ObjectId oi in headerobji)
        //        {
        //            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //            if (cener != null)
        //            {
        //                Atend.Base.Acad.AT_SUB sb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                foreach (ObjectId oii in sb.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
        //                    if (infosub.ParentCode != "NONE" && infosub.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oii, OpenMode.ForWrite);
        //                            Polyline cabelent = ent as Polyline;
        //                            if (cabelent != null)
        //                            {
        //                                if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewDB), cabelent.GetPoint3dAt(0)) == false)//start
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(0, new Point2d(hc.X, hc.Y));
        //                                }
        //                                else if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)Atend.Global.Acad.UAcad.GetEntityByObjectID(NewDB), cabelent.GetPoint3dAt(cabelent.NumberOfVertices - 1)) == false)//end
        //                                {
        //                                    Point3d hc = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
        //                                    cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(hc.X, hc.Y));
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void MoveHeaderCabelANDKablSho(ObjectId HeaderCabel)
        //{
        //    ObjectId NewHeaderCabel = HeaderCabel;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        //ed.WriteMessage("MOVE HEADER\n");
        //        Reset();
        //        Point3d start = new Point3d();
        //        Point3d end = new Point3d();
        //        Point3d HeaderCenterPoint = Point3d.Origin;
        //        //=========================
        //        Point3d HeaderCenterPoint02 = Point3d.Origin;
        //        Polyline HeaderEntity02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel) as Polyline;
        //        if (HeaderEntity02 != null)
        //        {
        //            HeaderCenterPoint02 = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity02);
        //        }
        //        Entity Parent02 = null;
        //        foreach (ObjectId oi in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //            {
        //                Parent02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
        //            }

        //        }
        //        //ed.WriteMessage("1\n");
        //        Curve ParentCurve02 = Parent02 as Curve;
        //        if (ParentCurve02 != null)
        //        {
        //            if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve02, HeaderCenterPoint02) == false)
        //            {
        //                //ed.WriteMessage("2 :{0}\n",NewHeaderCabel);
        //                ReturnHeaderEntity(NewHeaderCabel);
        //                return;
        //            }
        //            //ed.WriteMessage("3\n");
        //        }
        //        //=========================
        //        Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel) as Polyline;
        //        if (HeaderEntity != null)
        //        {
        //            HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //        }
        //        Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(NewHeaderCabel);
        //        foreach (ObjectId objsub in sub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //            //Move Terminal
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //            {
        //                Point3d ConsolCenterPoint = Point3d.Origin;
        //                Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(HeaderCabel);
        //                if (consolEntity != null)
        //                {
        //                    ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                }
        //                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                {
        //                    Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                    Line LineEntity = (Line)ent;
        //                    if (LineEntity != null)
        //                    {
        //                        if (LineEntity.StartPoint == LastCenterPoint)
        //                        {
        //                            LineEntity.StartPoint = ConsolCenterPoint;
        //                        }
        //                        else
        //                        {
        //                            LineEntity.EndPoint = ConsolCenterPoint;
        //                        }
        //                    }
        //                    tr.Commit();
        //                }
        //            }
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //            {
        //                Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel));
        //                if (cener != null)
        //                {

        //                    //Database db = HostApplicationServices.WorkingDatabase;
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                        Polyline cabelent = ent as Polyline;
        //                        if (cabelent != null)
        //                        {
        //                            //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                            //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                            //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                            if (cabelent.StartPoint == LastCenterPoint)
        //                            {
        //                                cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                            }
        //                            else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                            {
        //                                cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                            }
        //                        }
        //                        tr.Commit();
        //                    }
        //                }

        //                //Move Comment 
        //                Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                foreach (ObjectId collect in subBranch.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    //ed.WriteMessage("### :{0}\n", atinfo.NodeType);
        //                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        if (HeaderCenterPoint == CLine.StartPoint)
        //                        {
        //                            start = LastCenterPoint;
        //                            end = CLine.EndPoint;
        //                        }
        //                        else if (HeaderCenterPoint == CLine.EndPoint)
        //                        {
        //                            start = CLine.StartPoint;
        //                            end = LastCenterPoint;
        //                        }
        //                        //ed.WriteMessage("LastCenterPoint   : {0}\n", LastCenterPoint);
        //                        //ed.WriteMessage("HeaderCenterPoint : {0}\n", HeaderCenterPoint);
        //                        //ed.WriteMessage("CLine.StartPoint  : {0}\n", CLine.StartPoint);
        //                        //ed.WriteMessage("CLine.EndPoint    : {0}\n", CLine.EndPoint);
        //                        Point3d test = new Point3d(0, 0, 0);
        //                        if (start == test && end == test)
        //                        {
        //                            //move headercabel alone ???
        //                        }
        //                        else
        //                        {
        //                            LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                            ChangeConsolPosition(collect, matcom);
        //                        }
        //                    }
        //                }
        //            }

        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //            {
        //                Point3d _CPCenterPoint = Point3d.Origin;
        //                Polyline CLine = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel) as Polyline;
        //                if (CLine != null)
        //                {
        //                    _CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CLine);
        //                }
        //                Line cabelent = new Line();
        //                Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel));
        //                if (cener != null)
        //                {
        //                    //Database db = HostApplicationServices.WorkingDatabase;
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                        cabelent = (Line)ent;
        //                        if (cabelent != null)
        //                        {
        //                            if (cabelent.StartPoint == LastCenterPoint)
        //                            {
        //                                cabelent.StartPoint = _CPCenterPoint;
        //                            }
        //                            else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                            {
        //                                cabelent.EndPoint = _CPCenterPoint;
        //                            }
        //                        }
        //                        tr.Commit();
        //                    }
        //                }

        //                //Move Comment 
        //                Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                foreach (ObjectId collect in subBranch.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        if (HeaderCenterPoint == cabelent.StartPoint)
        //                        {
        //                            start = LastCenterPoint;
        //                            end = cabelent.EndPoint;
        //                        }
        //                        else if (HeaderCenterPoint == cabelent.EndPoint)
        //                        {
        //                            start = cabelent.StartPoint;
        //                            end = LastCenterPoint;
        //                        }

        //                        Point3d test = new Point3d(0, 0, 0);
        //                        if (start == test && end == test)
        //                        {
        //                            //move Kalamp alone ???
        //                        }
        //                        else
        //                        {
        //                            LineSegment3d LineSeg = new LineSegment3d(cabelent.StartPoint, cabelent.EndPoint);
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                            ChangeConsolPosition(collect, matcom);
        //                        }
        //                    }
        //                }
        //            }

        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                Point3d CenterHeaderCabel = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewHeaderCabel));
        //                Matrix3d mat2 = Matrix3d.Displacement(CenterHeaderCabel - LastCenterPoint);
        //                ChangeConsolPosition(objsub, mat2);
        //            }
        //            if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
        //            {
        //                ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                ObjectId oid = ObjectId.Null;
        //                foreach (ObjectId collect in Collectionbreaker)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        oid = collect;
        //                        Collectionbreaker.Remove(collect);
        //                    }
        //                }
        //                start = new Point3d();
        //                end = new Point3d();
        //                Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                if (LastCenterPoint == CLine.StartPoint)
        //                {
        //                    Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.EndPoint, HeaderCenterPoint02);
        //                }
        //                else if (LastCenterPoint == CLine.EndPoint)
        //                {
        //                    Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.StartPoint, HeaderCenterPoint02);
        //                }

        //                //remove Sub Group
        //                foreach (ObjectId c2 in Collectionbreaker)
        //                {
        //                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                    {
        //                        ed.WriteMessage("Breaker Not Removed\n");
        //                    }
        //                }
        //                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                {
        //                    Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                    Line LineEntity = (Line)ent;
        //                    if (LineEntity != null)
        //                    {
        //                        if (LastCenterPoint == CLine.StartPoint)
        //                        {
        //                            LineEntity.StartPoint = CLine.EndPoint;
        //                            LineEntity.EndPoint = HeaderCenterPoint02;
        //                        }
        //                        else if (LastCenterPoint == CLine.EndPoint)
        //                        {
        //                            LineEntity.EndPoint = CLine.StartPoint;
        //                            LineEntity.StartPoint = HeaderCenterPoint02;
        //                        }
        //                        start = LineEntity.StartPoint;
        //                        end = LineEntity.EndPoint;
        //                        if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                            Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                        else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                            Atend.Global.Acad.DrawEquips.AcDrawDisConnector.AddNewDisconnector(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                        else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                            Atend.Global.Acad.DrawEquips.AcDrawCatOut.AddNewCatout(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                    }

        //                    tr.Commit();
        //                }


        //                //Move Comment
        //                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                foreach (ObjectId h in sub2.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                        Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                        ChangeConsolPosition(h, matcom);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void MoveMafsal(ObjectId Mafsal)
        //{
        //    ObjectId NewMafsal = Mafsal;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(NewMafsal);
        //        foreach (ObjectId obj in sub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //            {
        //                Point3d center = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewMafsal));
        //                if (center != null)
        //                {
        //                    //Database db = HostApplicationServices.WorkingDatabase;
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(obj, OpenMode.ForWrite);
        //                        Polyline cabelent = ent as Polyline;
        //                        if (cabelent != null)
        //                        {
        //                            if (cabelent.StartPoint == LastCenterPoint)
        //                            {
        //                                cabelent.SetPointAt(0, new Point2d(center.X, center.Y));
        //                            }
        //                            else
        //                            {
        //                                cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(center.X, center.Y));
        //                            }
        //                        }
        //                        tr.Commit();

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void MoveKalamp(ObjectId Kalamp)
        //{
        //    ObjectId NewKalamp = Kalamp;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        //=====================
        //        Point3d HeaderCenterPoint02 = Point3d.Origin;
        //        Polyline HeaderEntity02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewKalamp) as Polyline;
        //        if (HeaderEntity02 != null)
        //        {
        //            HeaderCenterPoint02 = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity02);
        //        }
        //        Entity Parent02 = null;
        //        foreach (ObjectId oi in EntitySub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
        //            {
        //                Parent02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
        //            }

        //        }
        //        Curve ParentCurve02 = Parent02 as Curve;
        //        if (ParentCurve02 != null)
        //        {
        //            if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve02, HeaderCenterPoint02) == false)
        //            {
        //                //ed.WriteMessage("2 :{0}\n", NewKalamp);
        //                ReturnHeaderEntity(Kalamp);
        //                return;
        //            }
        //        }
        //        //=====================
        //        Point3d start = new Point3d();
        //        Point3d end = new Point3d();
        //        Point3d HeaderCenterPoint = Point3d.Origin;
        //        Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewKalamp) as Polyline;
        //        if (HeaderEntity != null)
        //        {
        //            HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //        }
        //        Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(NewKalamp);
        //        foreach (ObjectId objsub in sub.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //            //Move Terminal
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //            {
        //                Point3d ConsolCenterPoint = Point3d.Origin;
        //                Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(Kalamp);
        //                if (consolEntity != null)
        //                {
        //                    ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                }
        //                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                {
        //                    Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                    Line LineEntity = (Line)ent;
        //                    if (LineEntity != null)
        //                    {
        //                        if (LineEntity.StartPoint == LastCenterPoint)
        //                        {
        //                            LineEntity.StartPoint = ConsolCenterPoint;
        //                        }
        //                        else
        //                        {
        //                            LineEntity.EndPoint = ConsolCenterPoint;
        //                        }
        //                    }
        //                    tr.Commit();
        //                }
        //            }
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //            {
        //                Point3d _CPCenterPoint = Point3d.Origin;
        //                Polyline CLine = Atend.Global.Acad.UAcad.GetEntityByObjectID(NewKalamp) as Polyline;
        //                if (CLine != null)
        //                {
        //                    _CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CLine);
        //                }
        //                Line cabelent = new Line();
        //                Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewKalamp));
        //                if (cener != null)
        //                {
        //                    //Database db = HostApplicationServices.WorkingDatabase;
        //                    Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                    using (Transaction tr = db.TransactionManager.StartTransaction())
        //                    {
        //                        Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                        cabelent = (Line)ent;
        //                        if (cabelent != null)
        //                        {
        //                            //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                            //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                            //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                            //ed.WriteMessage("CPCenterPoint        : {0}\n", _CPCenterPoint);
        //                            if (cabelent.StartPoint == LastCenterPoint)
        //                            {
        //                                cabelent.StartPoint = _CPCenterPoint;
        //                            }
        //                            else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                            {
        //                                cabelent.EndPoint = _CPCenterPoint;
        //                            }
        //                        }
        //                        tr.Commit();
        //                    }
        //                }

        //                //Move Comment 
        //                Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                foreach (ObjectId collect in subBranch.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        //ed.WriteMessage("cabelent.StartPoint   : {0}\n", cabelent.StartPoint);
        //                        //ed.WriteMessage("cabelent.EndPoint     : {0}\n", cabelent.EndPoint);
        //                        //ed.WriteMessage("LastCenterPoint       : {0}\n", LastCenterPoint);
        //                        //ed.WriteMessage("HeaderCenterPoint     : {0}\n", HeaderCenterPoint);
        //                        //ed.WriteMessage("CLine.StartPoint      : {0}\n", CLine.StartPoint);
        //                        //ed.WriteMessage("CLine.EndPoint        : {0}\n", CLine.EndPoint);
        //                        if (HeaderCenterPoint == cabelent.StartPoint)
        //                        {
        //                            start = LastCenterPoint;
        //                            end = cabelent.EndPoint;
        //                        }
        //                        else if (HeaderCenterPoint == cabelent.EndPoint)
        //                        {
        //                            start = cabelent.StartPoint;
        //                            end = LastCenterPoint;
        //                        }

        //                        Point3d test = new Point3d(0, 0, 0);
        //                        if (start == test && end == test)
        //                        {
        //                            //move Kalamp alone ???
        //                        }
        //                        else
        //                        {
        //                            LineSegment3d LineSeg = new LineSegment3d(cabelent.StartPoint, cabelent.EndPoint);
        //                            LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                            Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                            ChangeConsolPosition(collect, matcom);
        //                        }
        //                    }
        //                }
        //            }
        //            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                Point3d CenterKalamp = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewKalamp));
        //                Matrix3d mat2 = Matrix3d.Displacement(CenterKalamp - LastCenterPoint);
        //                ChangeConsolPosition(objsub, mat2);
        //            }
        //            if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
        //            {
        //                ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                ObjectId oid = ObjectId.Null;
        //                foreach (ObjectId collect in Collectionbreaker)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        oid = collect;
        //                        Collectionbreaker.Remove(collect);
        //                    }
        //                }
        //                start = new Point3d();
        //                end = new Point3d();
        //                Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                if (LastCenterPoint == CLine.StartPoint)
        //                {
        //                    Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.EndPoint, HeaderCenterPoint02);
        //                }
        //                else if (LastCenterPoint == CLine.EndPoint)
        //                {
        //                    Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.StartPoint, HeaderCenterPoint02);
        //                }

        //                //remove Sub Group
        //                foreach (ObjectId c2 in Collectionbreaker)
        //                {
        //                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                    {
        //                        ed.WriteMessage("Breaker Not Removed\n");
        //                    }
        //                }
        //                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                {
        //                    Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                    Line LineEntity = (Line)ent;
        //                    if (LineEntity != null)
        //                    {
        //                        if (LastCenterPoint == CLine.StartPoint)
        //                        {
        //                            LineEntity.StartPoint = CLine.EndPoint;
        //                            LineEntity.EndPoint = HeaderCenterPoint02;
        //                        }
        //                        else if (LastCenterPoint == CLine.EndPoint)
        //                        {
        //                            LineEntity.EndPoint = CLine.StartPoint;
        //                            LineEntity.StartPoint = HeaderCenterPoint02;
        //                        }
        //                        start = LineEntity.StartPoint;
        //                        end = LineEntity.EndPoint;
        //                        Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                    }

        //                    tr.Commit();
        //                }


        //                //Move Comment
        //                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //                foreach (ObjectId h in sub2.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                        Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                        ChangeConsolPosition(h, matcom);
        //                    }
        //                }
        //            }

        //        }
        //    }
        //}

        //public static void MoveLight(ObjectId Light)
        //{
        //    ObjectId NewLight = Light;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterLight = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentEntity);
        //        Matrix3d mat = Matrix3d.Displacement(CenterLight - LastCenterPoint);
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(Light);
        //        Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //        foreach (ObjectId oisb in EntityS.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
        //            if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                ChangeConsolPosition(oisb, mat);
        //            }
        //        }
        //    }
        //}

        //public static void MoveGround(ObjectId Ground)
        //{
        //    ObjectId NewGround = Ground;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterGround = Atend.Global.Acad.UAcad.CenterOfEntity(CurrentEntity);
        //        Matrix3d mat = Matrix3d.Displacement(CenterGround - LastCenterPoint);
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(Ground);
        //        Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //        foreach (ObjectId oisb in EntityS.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
        //            if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                ChangeConsolPosition(oisb, mat);
        //            }
        //        }
        //    }
        //}

        //public static void MoveTransformer(ObjectId Transformer)
        //{
        //    ObjectId NewTransformer = Transformer;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterGround = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewTransformer));
        //        Matrix3d mat = Matrix3d.Displacement(CenterGround - LastCenterPoint);

        //        Matrix3d matRev = Matrix3d.Displacement(LastCenterPoint - CenterGround);
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(Transformer);

        //        Atend.Base.Acad.AT_SUB subtr = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //        Entity Parent = null;
        //        foreach (ObjectId obj in subtr.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.AirPost || _Info.NodeType == (int)Atend.Control.Enum.ProductType.GroundPost))
        //            {
        //                Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(obj);
        //            }
        //        }
        //        Curve ParentCurve = Parent as Curve;
        //        if (ParentCurve != null)
        //        {
        //            if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, CenterGround) == false)
        //            {
        //                ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //                foreach (ObjectId _collect in _Collection)
        //                {
        //                    Circle c = Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect) as Circle;
        //                    Line l = Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect) as Line;
        //                    Polyline p = Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect) as Polyline;
        //                    if (c != null)
        //                    {
        //                        ChangeConsolPosition(_collect, matRev);
        //                    }
        //                    if (l != null)
        //                    {
        //                        //ed.WriteMessage("LINE \n");
        //                        //ReturnConsolEntity(_collect);

        //                        AllowToMove = true;
        //                        Atend.Acad.AcadMove.EntityInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                        Atend.Acad.AcadMove.EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(_collect);

        //                        Atend.Acad.AcadMove.LastCenterPoint =
        //                            Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect));
        //                        MoveHeaderCabelANDKablSho(_collect);

        //                        ChangeConsolPosition(_collect, matRev);
        //                    }
        //                    if (p != null)
        //                    {
        //                        //ed.WriteMessage("POLYLINE \n");
        //                        //ReturnHeaderEntity(_collect);

        //                        AllowToMove = true;
        //                        Atend.Acad.AcadMove.EntityInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                        Atend.Acad.AcadMove.EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(_collect);

        //                        Atend.Acad.AcadMove.LastCenterPoint =
        //                            Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect));
        //                        MoveHeaderCabelANDKablSho(_collect);

        //                        ChangeConsolPosition(_collect, matRev);
        //                    }
        //                }
        //                return;

        //            }




        //            //Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            //if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.AirPost || _Info.NodeType == (int)Atend.Control.Enum.ProductType.GroundPost))
        //            //{
        //            //    Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(obj);
        //            //}
        //            //if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, ConsolCenterPoint) == false)
        //            //{
        //            //    ReturnConsolEntity(Consol);
        //            //    return;
        //            //}
        //        }


        //        //ed.WriteMessage("^^ :{0}\n", EntitySub.SelectedObjectId);
        //        //foreach (ObjectId oi in EntitySub.SubIdCollection)
        //        //{
        //        //    Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //        //    ed.WriteMessage("@#@ x:{0}\n", poleInfo);

        //        //}



        //        //Move Comment
        //        foreach (ObjectId obj in subtr.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                //ed.WriteMessage("2:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", mat.Translation.X, mat.Translation.Y, mat.Translation.Z);
        //                ChangeConsolPosition(obj, mat);
        //            }
        //        }

        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in Collection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            //ed.WriteMessage("@@1 :{0}\n", poleInfo.NodeType);
        //            //+++++++++++++++++++++++++++++++++++++++
        //            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
        //            {
        //                Point3d LastCenterPointHeader = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Point3d HeaderCenterPoint02 = Point3d.Origin;
        //                Polyline HeaderEntity02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity02 != null)
        //                {
        //                    HeaderCenterPoint02 = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity02);
        //                }
        //                Entity Parent02 = null;
        //                Atend.Base.Acad.AT_SUB hcSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    //Move Terminal
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        Point3d ConsolCenterPoint = Point3d.Origin;
        //                        Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(collect);
        //                        if (consolEntity != null)
        //                        {
        //                            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == LastCenterPoint)//x
        //                                {
        //                                    LineEntity.StartPoint = ConsolCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = ConsolCenterPoint;
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));//@@objsub
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho)
        //                                    {
        //                                        ed.WriteMessage("LastCenterPointKL    : {0}\n", LastCenterPointKL);
        //                                        ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                                        ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                                        ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                                        if (cabelent.StartPoint == LastCenterPointKL || (Math.Abs(cabelent.StartPoint.X - LastCenterPointKL.X) < 1))
        //                                        {
        //                                            ed.WriteMessage("11\n");
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (cabelent.EndPoint == LastCenterPointKL || (Math.Abs(cabelent.EndPoint.X - LastCenterPointKL.X) < 1))
        //                                        {
        //                                            ed.WriteMessage("22\n");
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.StartPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            ed.WriteMessage("33\n");
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.EndPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            ed.WriteMessage("44\n");
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                    }
        //                                    else if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                                    {
        //                                        if (cabelent.StartPoint == LastCenterPoint)
        //                                        {
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))
        //                                        {
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.StartPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.EndPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collectcom in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collectcom);
        //                            //ed.WriteMessage("### :{0}\n", atinfo.NodeType);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;//x
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = LastCenterPoint;//x
        //                                }
        //                                //ed.WriteMessage("LastCenterPoint   : {0}\n", LastCenterPoint);
        //                                //ed.WriteMessage("HeaderCenterPoint : {0}\n", HeaderCenterPoint);
        //                                //ed.WriteMessage("CLine.StartPoint  : {0}\n", CLine.StartPoint);
        //                                //ed.WriteMessage("CLine.EndPoint    : {0}\n", CLine.EndPoint);
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    //ed.WriteMessage("3:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                    ChangeConsolPosition(collectcom, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //                    {
        //                        Point3d _CPCenterPoint = Point3d.Origin;
        //                        Polyline CLine = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                        if (CLine != null)
        //                        {
        //                            _CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CLine);
        //                        }
        //                        Line cabelent = new Line();
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                cabelent = (Line)ent;
        //                                if (cabelent != null)
        //                                {
        //                                    if (cabelent.StartPoint == LastCenterPoint)//x
        //                                    {
        //                                        cabelent.StartPoint = _CPCenterPoint;
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))//x
        //                                    {
        //                                        cabelent.EndPoint = _CPCenterPoint;
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collectcomment in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collectcomment);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == cabelent.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;//x
        //                                    end = cabelent.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == cabelent.EndPoint)
        //                                {
        //                                    start = cabelent.StartPoint;
        //                                    end = LastCenterPoint;//x
        //                                }

        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move Kalamp alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(cabelent.StartPoint, cabelent.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    //ed.WriteMessage("4:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                    ChangeConsolPosition(collectcomment, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        Point3d CenterHeaderCabel = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        Matrix3d mat2 = Matrix3d.Displacement(CenterHeaderCabel - LastCenterPoint);//x
        //                        //ed.WriteMessage("5:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", mat2.Translation.X, mat2.Translation.Y, mat2.Translation.Z);
        //                        ChangeConsolPosition(objsub, mat2);
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
        //                    {
        //                        ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                        ObjectId oid = ObjectId.Null;
        //                        foreach (ObjectId collect3 in Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect3);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                oid = collect3;
        //                                Collectionbreaker.Remove(collect3);
        //                            }
        //                        }
        //                        start = new Point3d();
        //                        end = new Point3d();
        //                        Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                        LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                        if (LastCenterPoint == CLine.StartPoint)//x
        //                        {
        //                            Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.EndPoint, HeaderCenterPoint02);
        //                        }
        //                        else if (LastCenterPoint == CLine.EndPoint)//x
        //                        {
        //                            Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.StartPoint, HeaderCenterPoint02);
        //                        }

        //                        //remove Sub Group
        //                        foreach (ObjectId c2 in Collectionbreaker)
        //                        {
        //                            if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                            {
        //                                ed.WriteMessage("Breaker Not Removed\n");
        //                            }
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LastCenterPoint == CLine.StartPoint)//x
        //                                {
        //                                    LineEntity.StartPoint = CLine.EndPoint;
        //                                    LineEntity.EndPoint = HeaderCenterPoint02;
        //                                }
        //                                else if (LastCenterPoint == CLine.EndPoint)//x
        //                                {
        //                                    LineEntity.EndPoint = CLine.StartPoint;
        //                                    LineEntity.StartPoint = HeaderCenterPoint02;
        //                                }
        //                                start = LineEntity.StartPoint;
        //                                end = LineEntity.EndPoint;
        //                                if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                                else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawDisConnector.AddNewDisconnector(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                                else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawCatOut.AddNewCatout(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                            }

        //                            tr.Commit();
        //                        }


        //                        //Move Comment
        //                        ObjectId id3 = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id3);
        //                        foreach (ObjectId h in sub2.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                                //ed.WriteMessage("6:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                ChangeConsolPosition(h, matcom);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //+++++++++++++++++++++++++++++++++++++++


        //            //if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Transformer)
        //            {
        //                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub2 in sub2.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO _Info2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub2);
        //                    ed.WriteMessage("*** sub2:{0}\n", _Info2.NodeType);
        //                    //if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    //{
        //                    //    ChangeConsolPosition(objsub, mat);
        //                    //}
        //                }

        //                //currentCPConsolElse = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                //Point3d RodCenterPoint = Point3d.Origin;
        //                //Polyline RodEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                //if (RodEntity != null)
        //                //{
        //                //    RodCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(RodEntity);
        //                //}
        //                //Entity Parent = null;
        //                //foreach (ObjectId oi in EntitySub.SubIdCollection)
        //                //{
        //                //    Atend.Base.Acad.AT_INFO poleInfo2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                //    ed.WriteMessage("@@2 :{0}\n", poleInfo2.NodeType);
        //                //}
        //            }
        //        }
        //    }
        //}

        //public static void MoveJackPanel(ObjectId WeekJackPanel)
        //{
        //    ObjectId NewWeekJackPanel = WeekJackPanel;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterGround = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewWeekJackPanel));
        //        Matrix3d mat = Matrix3d.Displacement(CenterGround - LastCenterPoint);
        //        Matrix3d matRev = Matrix3d.Displacement(LastCenterPoint - CenterGround);
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(WeekJackPanel);

        //        Atend.Base.Acad.AT_SUB subtr = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //        Entity Parent = null;

        //        foreach (ObjectId obj in subtr.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
        //            if (_Info.ParentCode != "NONE" && (_Info.NodeType == (int)Atend.Control.Enum.ProductType.AirPost || _Info.NodeType == (int)Atend.Control.Enum.ProductType.GroundPost))
        //            {
        //                Parent = Atend.Global.Acad.UAcad.GetEntityByObjectID(obj);
        //            }
        //        }
        //        Curve ParentCurve = Parent as Curve;
        //        if (ParentCurve != null)
        //        {
        //            if (Atend.Global.Acad.UAcad.IsInsideCurve(ParentCurve, CenterGround) == false)
        //            {
        //                ObjectIdCollection _Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //                foreach (ObjectId _collect in _Collection)
        //                {
        //                    ChangeConsolPosition(_collect, matRev);


        //                    //Line l = Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect) as Line;
        //                    //Polyline p = Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect) as Polyline;
        //                    //if (l != null)
        //                    //{
        //                    //    //ed.WriteMessage("LINE \n");
        //                    //    //ReturnConsolEntity(_collect);

        //                    //    //AllowToMove = true;
        //                    //    //Atend.Acad.AcadMove.EntityInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                    //    //Atend.Acad.AcadMove.EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(_collect);

        //                    //    //Atend.Acad.AcadMove.LastCenterPoint =
        //                    //    //    Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect));

        //                    //    //ObjectIdCollection Collection4 = Atend.Global.Acad.UAcad.GetGroupSubEntities(Atend.Global.Acad.UAcad.GetEntityGroup(_collect));
        //                    //    //foreach (ObjectId collect4 in Collection4)
        //                    //    //{
        //                    //    //    Atend.Base.Acad.AT_INFO Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect4);
        //                    //    //    if (Info.ParentCode != "NONE" && Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                    //    //    {
        //                    //    //        Atend.Acad.AcadMove.LastCenterPointKL = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect4));
        //                    //    //    }
        //                    //    //}

        //                    //    //MoveHeaderCabelANDKablSho(_collect);

        //                    //    ChangeConsolPosition(_collect, matRev);
        //                    //}
        //                    //if (p != null)
        //                    //{
        //                    //    //ed.WriteMessage("POLYLINE \n");
        //                    //    //ReturnHeaderEntity(_collect);

        //                    //    //AllowToMove = true;
        //                    //    //Atend.Acad.AcadMove.EntityInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_collect);
        //                    //    //Atend.Acad.AcadMove.EntitySub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(_collect);

        //                    //    //Atend.Acad.AcadMove.LastCenterPoint =
        //                    //    //    Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(_collect));

        //                    //    //ObjectIdCollection Collection4 = Atend.Global.Acad.UAcad.GetGroupSubEntities(Atend.Global.Acad.UAcad.GetEntityGroup(_collect));
        //                    //    //foreach (ObjectId collect4 in Collection4)
        //                    //    //{
        //                    //    //    Atend.Base.Acad.AT_INFO Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect4);
        //                    //    //    if (Info.ParentCode != "NONE" && Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                    //    //    {
        //                    //    //        Atend.Acad.AcadMove.LastCenterPointKL = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect4));
        //                    //    //    }
        //                    //    //}

        //                    //    //MoveHeaderCabelANDKablSho(_collect);

        //                    //    ChangeConsolPosition(_collect, matRev);
        //                    //}
        //                }
        //                return;

        //            }
        //        }

        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in Collection)
        //        {
        //            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
        //            if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //            {
        //                Point3d LastCenterPointHeader = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                Point3d start = new Point3d();
        //                Point3d end = new Point3d();
        //                Point3d HeaderCenterPoint = Point3d.Origin;
        //                Point3d HeaderCenterPoint02 = Point3d.Origin;
        //                Polyline HeaderEntity02 = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity02 != null)
        //                {
        //                    HeaderCenterPoint02 = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity02);
        //                }
        //                Entity Parent02 = null;
        //                Atend.Base.Acad.AT_SUB hcSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                if (HeaderEntity != null)
        //                {
        //                    HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                }
        //                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
        //                foreach (ObjectId objsub in sub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                    //Move Terminal
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                    {
        //                        Point3d ConsolCenterPoint = Point3d.Origin;
        //                        Polyline consolEntity = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(collect);
        //                        if (consolEntity != null)
        //                        {
        //                            ConsolCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(consolEntity);
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LineEntity.StartPoint == LastCenterPoint)//x
        //                                {
        //                                    LineEntity.StartPoint = ConsolCenterPoint;
        //                                }
        //                                else
        //                                {
        //                                    LineEntity.EndPoint = ConsolCenterPoint;
        //                                }
        //                            }
        //                            tr.Commit();
        //                        }
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                    {
        //                        Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));//@@objsub
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                Polyline cabelent = ent as Polyline;
        //                                if (cabelent != null)
        //                                {
        //                                    if (poleInfo.ParentCode != "NONE" && poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                                    {
        //                                        //ed.WriteMessage("LastCenterPointKL    : {0}\n", LastCenterPointKL);
        //                                        //ed.WriteMessage("LastCenterPoint      : {0}\n", LastCenterPoint);
        //                                        //ed.WriteMessage("cabelent.StartPoint  : {0}\n", cabelent.StartPoint);
        //                                        //ed.WriteMessage("cabelent.EndPoint    : {0}\n", cabelent.EndPoint);
        //                                        if (cabelent.StartPoint == LastCenterPointKL || (Math.Abs(cabelent.StartPoint.X - LastCenterPointKL.X) < 1))
        //                                        {
        //                                            //ed.WriteMessage("11\n");
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (cabelent.EndPoint == LastCenterPointKL || (Math.Abs(cabelent.EndPoint.X - LastCenterPointKL.X) < 1))
        //                                        {
        //                                            //ed.WriteMessage("22\n");
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.StartPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            //ed.WriteMessage("33\n");
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (Math.Abs(cabelent.EndPoint.Y - LastCenterPointKL.Y) < 1)
        //                                        {
        //                                            //ed.WriteMessage("44\n");
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collectcom in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collectcom);
        //                            //ed.WriteMessage("### :{0}\n", atinfo.NodeType);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == CLine.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;//x
        //                                    end = CLine.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == CLine.EndPoint)
        //                                {
        //                                    start = CLine.StartPoint;
        //                                    end = LastCenterPoint;//x
        //                                }
        //                                //ed.WriteMessage("LastCenterPoint   : {0}\n", LastCenterPoint);
        //                                //ed.WriteMessage("HeaderCenterPoint : {0}\n", HeaderCenterPoint);
        //                                //ed.WriteMessage("CLine.StartPoint  : {0}\n", CLine.StartPoint);
        //                                //ed.WriteMessage("CLine.EndPoint    : {0}\n", CLine.EndPoint);
        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move headercabel alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    //ed.WriteMessage("3:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                    ChangeConsolPosition(collectcom, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper)
        //                    {
        //                        Point3d _CPCenterPoint = Point3d.Origin;
        //                        Polyline CLine = Atend.Global.Acad.UAcad.GetEntityByObjectID(collect) as Polyline;
        //                        if (CLine != null)
        //                        {
        //                            _CPCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(CLine);
        //                        }
        //                        Line cabelent = new Line();
        //                        Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        if (cener != null)
        //                        {
        //                            //Database db = HostApplicationServices.WorkingDatabase;
        //                            Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                            using (Transaction tr = db.TransactionManager.StartTransaction())
        //                            {
        //                                Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                cabelent = (Line)ent;
        //                                if (cabelent != null)
        //                                {
        //                                    if (cabelent.StartPoint == LastCenterPoint)//x
        //                                    {
        //                                        cabelent.StartPoint = _CPCenterPoint;
        //                                    }
        //                                    else if (cabelent.EndPoint == LastCenterPoint || (cabelent.EndPoint.X - LastCenterPoint.X < 1))//x
        //                                    {
        //                                        cabelent.EndPoint = _CPCenterPoint;
        //                                    }
        //                                }
        //                                tr.Commit();
        //                            }
        //                        }

        //                        //Move Comment 
        //                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                        foreach (ObjectId collectcomment in subBranch.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collectcomment);
        //                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                if (HeaderCenterPoint == cabelent.StartPoint)
        //                                {
        //                                    start = LastCenterPoint;//x
        //                                    end = cabelent.EndPoint;
        //                                }
        //                                else if (HeaderCenterPoint == cabelent.EndPoint)
        //                                {
        //                                    start = cabelent.StartPoint;
        //                                    end = LastCenterPoint;//x
        //                                }

        //                                Point3d test = new Point3d(0, 0, 0);
        //                                if (start == test && end == test)
        //                                {
        //                                    //move Kalamp alone ???
        //                                }
        //                                else
        //                                {
        //                                    LineSegment3d LineSeg = new LineSegment3d(cabelent.StartPoint, cabelent.EndPoint);
        //                                    LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                    Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                    //ed.WriteMessage("4:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                    ChangeConsolPosition(collectcomment, matcom);
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                    {
        //                        Point3d CenterHeaderCabel = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(collect));
        //                        Matrix3d mat2 = Matrix3d.Displacement(CenterHeaderCabel - LastCenterPoint);//x
        //                        //ed.WriteMessage("5:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", mat2.Translation.X, mat2.Translation.Y, mat2.Translation.Z);
        //                        ChangeConsolPosition(objsub, mat2);
        //                    }
        //                    if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
        //                    {
        //                        ObjectIdCollection Collectionbreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
        //                        ObjectId oid = ObjectId.Null;
        //                        foreach (ObjectId collect3 in Collectionbreaker)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect3);
        //                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
        //                            {
        //                                oid = collect3;
        //                                Collectionbreaker.Remove(collect3);
        //                            }
        //                        }
        //                        start = new Point3d();
        //                        end = new Point3d();
        //                        Line CLine = (Line)Atend.Global.Acad.UAcad.GetEntityByObjectID(oid);
        //                        LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                        if (LastCenterPoint == CLine.StartPoint)//x
        //                        {
        //                            Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.EndPoint, HeaderCenterPoint02);
        //                        }
        //                        else if (LastCenterPoint == CLine.EndPoint)//x
        //                        {
        //                            Atend.Global.Acad.UAcad.RotateBy2Point(Collectionbreaker, LineSeg.MidPoint, CLine.StartPoint, HeaderCenterPoint02);
        //                        }

        //                        //remove Sub Group
        //                        foreach (ObjectId c2 in Collectionbreaker)
        //                        {
        //                            if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
        //                            {
        //                                ed.WriteMessage("Breaker Not Removed\n");
        //                            }
        //                        }
        //                        Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                        using (Transaction tr = db.TransactionManager.StartTransaction())
        //                        {
        //                            Entity ent = (Entity)tr.GetObject(oid, OpenMode.ForWrite);
        //                            Line LineEntity = (Line)ent;
        //                            if (LineEntity != null)
        //                            {
        //                                if (LastCenterPoint == CLine.StartPoint)//x
        //                                {
        //                                    LineEntity.StartPoint = CLine.EndPoint;
        //                                    LineEntity.EndPoint = HeaderCenterPoint02;
        //                                }
        //                                else if (LastCenterPoint == CLine.EndPoint)//x
        //                                {
        //                                    LineEntity.EndPoint = CLine.StartPoint;
        //                                    LineEntity.StartPoint = HeaderCenterPoint02;
        //                                }
        //                                start = LineEntity.StartPoint;
        //                                end = LineEntity.EndPoint;
        //                                if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawBreaker.AddNewBreaker(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                                else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawDisConnector.AddNewDisconnector(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                                else if (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
        //                                    Atend.Global.Acad.DrawEquips.AcDrawCatOut.AddNewCatout(LineEntity.StartPoint, LineEntity.EndPoint, Atend.Global.Acad.UAcad.GetEntityGroup(oid));
        //                            }

        //                            tr.Commit();
        //                        }


        //                        //Move Comment
        //                        ObjectId id3 = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
        //                        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id3);
        //                        foreach (ObjectId h in sub2.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //                            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                Matrix3d matcom = Matrix3d.Displacement(LineSeg2.MidPoint - LineSeg.MidPoint);
        //                                //ed.WriteMessage("6:mat.Translation.X:{0} \nmat.Translation.Y:{1} \nmat.Translation.Z:{2} \n", matcom.Translation.X, matcom.Translation.Y, matcom.Translation.Z);
        //                                ChangeConsolPosition(h, matcom);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //+++++++++++++++++++++++++++++++++++++++++
        //        }
        //    }
        //}

        //public static void MoveMeasuredJackPanel(ObjectId MeasuredJackPanel)
        //{
        //    ObjectId NewMeasuredJackPanel = MeasuredJackPanel;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    if (AllowToMove)
        //    {
        //        Reset();
        //        Point3d CenterMeasure = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(NewMeasuredJackPanel));
        //        Matrix3d mat = Matrix3d.Displacement(CenterMeasure - LastCenterPoint);
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(NewMeasuredJackPanel);

        //        //Move HeaderCabel
        //        //Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
        //        //foreach (ObjectId oisb in EntityS.SubIdCollection)

        //        //****************
        //        //Move Comment
        //        ObjectId id2 = Atend.Global.Acad.UAcad.GetEntityGroup(MeasuredJackPanel);
        //        Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id2);
        //        foreach (ObjectId h in sub2.SubIdCollection)
        //        {
        //            Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
        //            if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //            {
        //                ChangeConsolPosition(h, mat);
        //            }
        //        }
        //        //****************

        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId oisub in Collection)
        //        {
        //            Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oisub);
        //            foreach (ObjectId oisb in EntityS.SubIdCollection)
        //            {
        //                Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
        //                ed.WriteMessage("@@@@@@ :{0}\n", _Info.NodeType);
        //                if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
        //                {
        //                    Point3d lsp = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oisb));
        //                    ChangeConsolPosition(oisb, mat);

        //                    Point3d start = new Point3d();
        //                    Point3d end = new Point3d();
        //                    Point3d HeaderCenterPoint = Point3d.Origin;
        //                    Polyline HeaderEntity = Atend.Global.Acad.UAcad.GetEntityByObjectID(oisb) as Polyline;
        //                    if (HeaderEntity != null)
        //                    {
        //                        HeaderCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(HeaderEntity);
        //                    }
        //                    Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oisb);
        //                    foreach (ObjectId objsub in sub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
        //                        if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                        {
        //                            ChangeConsolPosition(objsub, mat);
        //                        }
        //                        if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
        //                        {
        //                            Polyline CLine = (Polyline)Atend.Global.Acad.UAcad.GetEntityByObjectID(objsub);
        //                            Point3d cener = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oisb));
        //                            if (cener != null)
        //                            {
        //                                Database db = Application.DocumentManager.MdiActiveDocument.Database;
        //                                using (Transaction tr = db.TransactionManager.StartTransaction())
        //                                {
        //                                    Entity ent = (Entity)tr.GetObject(objsub, OpenMode.ForWrite);
        //                                    Polyline cabelent = ent as Polyline;
        //                                    if (cabelent != null)
        //                                    {
        //                                        double x = Math.Abs(cabelent.StartPoint.X - lsp.X);
        //                                        double y = Math.Abs(cabelent.EndPoint.X - lsp.X);

        //                                        if (cabelent.StartPoint == lsp || x < y)
        //                                        {
        //                                            cabelent.SetPointAt(0, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        else if (cabelent.EndPoint == lsp || x > y)//|| (cabelent.EndPoint.X - lsp.X < 1))
        //                                        {
        //                                            cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                        }
        //                                        //cabelent.SetPointAt(cabelent.NumberOfVertices - 1, new Point2d(cener.X, cener.Y));
        //                                    }
        //                                    tr.Commit();
        //                                }
        //                            }

        //                            //Move Comment 
        //                            Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
        //                            foreach (ObjectId collect001 in subBranch.SubIdCollection)
        //                            {
        //                                Atend.Base.Acad.AT_INFO atinfo001 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect001);
        //                                if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                                {
        //                                    if (HeaderCenterPoint == CLine.StartPoint)
        //                                    {
        //                                        start = lsp;
        //                                        end = CLine.EndPoint;
        //                                    }
        //                                    else if (HeaderCenterPoint == CLine.EndPoint)
        //                                    {
        //                                        start = CLine.StartPoint;
        //                                        end = lsp;
        //                                    }
        //                                    Point3d test = new Point3d(0, 0, 0);
        //                                    if (start == test && end == test)
        //                                    {
        //                                        //move headercabel alone ???
        //                                    }
        //                                    else
        //                                    {
        //                                        LineSegment3d LineSeg = new LineSegment3d(CLine.StartPoint, CLine.EndPoint);
        //                                        LineSegment3d LineSeg2 = new LineSegment3d(start, end);
        //                                        Matrix3d matcom = Matrix3d.Displacement(LineSeg.MidPoint - LineSeg2.MidPoint);
        //                                        ChangeConsolPosition(collect001, mat);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                {
        //                    ChangeConsolPosition(oisb, mat);
        //                }
        //            }

        //        }
        //    }
        //}


    }
}
