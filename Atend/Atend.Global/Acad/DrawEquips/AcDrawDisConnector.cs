using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;


namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawDisConnector
    {
        bool _UseAccess;
        public bool UseAccess
        {
            get { return _UseAccess; }
            set { _UseAccess = value; }
        }

        Atend.Base.Equipment.EDisconnector _eDisConnector;
        public Atend.Base.Equipment.EDisconnector eDisConnector
        {
            get { return _eDisConnector; }
            set { _eDisConnector = value; }
        }


        Atend.Base.Design.DKeyStatus _dKeyStatus;
        public Atend.Base.Design.DKeyStatus DKeyStatus
        {
            get { return _dKeyStatus; }
            set { _dKeyStatus = value; }
        }


        int _existance;
        public int Existance
        {
            get { return _existance; }
            set { _existance = value; }
        }

        int _projectCode;
        public int ProjectCode
        {
            get { return _projectCode; }
            set { _projectCode = value; }
        }

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        Atend.Base.Design.DPackage DisConnectorPack;

        //public Guid EXCode;
        //~~~~~~~~~~~~~~~~~~~CLASS~~~~~~~~~~~~~~

        public class DrawDisconnectorJig : DrawJig
        {

            List<Entity> Entities = new List<Entity>();
            Point3d CenterPoint = Point3d.Origin;
            public Point3d MyCenterPoint
            {
                get { return CenterPoint; }
            }

            double MyScale = 1;

            public DrawDisconnectorJig(double Scale)
            {
                MyScale = Scale;
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                return SamplerStatus.Cancel;
            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                return true;
            }

            private Entity CreateLine(Point3d StartPoint, Point3d EndPoint, int ProductType, int ColorIndex, double Thickness)
            {
                Atend.Global.Acad.AcadJigs.MyLine mLine = new Atend.Global.Acad.AcadJigs.MyLine();
                mLine.StartPoint = StartPoint;
                mLine.EndPoint = EndPoint;

                if (Thickness != 0)
                {
                    mLine.Thickness = Thickness;
                }


                Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, ProductType);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, CodeGuid);
                mLine.ColorIndex = ColorIndex;

                return mLine;
            }

            private Entity CreateConnectionPoint(Point3d CenterPoint, double Radius)
            {
                Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

                c.Center = CenterPoint;
                c.Normal = new Vector3d(0, 0, 1);
                c.Radius = Radius;
                //c.ColorIndex = 3;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.Breaker);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(c, CodeGuid);
                return c;
            }

            private Entity CreateCellEntity(Point3d CenterPoint, double Width, double Height, int Type)
            {

                double BaseX = CenterPoint.X;
                double BaseY = CenterPoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.Closed = true;

                //Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, Type);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (long)ProductCode);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
                return pLine;

            }

            public List<Entity> GetDemo(Point3d StartPoint, Point3d EndPoint)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                LineSegment3d ls1 = new LineSegment3d(StartPoint, EndPoint);
                Point3d BasePoint = ls1.MidPoint;
                CenterPoint = BasePoint;
                double Angle = new Line(StartPoint, EndPoint).Angle;

                Entities.Add(CreateCellEntity(BasePoint, 10, 5, (int)Atend.Control.Enum.ProductType.Disconnector));

                Entities.Add(CreateLine(new Point3d(BasePoint.X - 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y + 2, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Key, 0, 0));

                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), 0.2));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 2, BasePoint.Y + 0.5, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y - 0.5, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Matrix3d trans = Matrix3d.Rotation(Angle, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                   new Point3d(BasePoint.X, BasePoint.Y, 0));

                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans);
                }

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans1);
                }

                Entity FinalLine = CreateLine(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z), (int)Atend.Control.Enum.ProductType.Terminal, 0, 0);
                Entities.Add(FinalLine);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(FinalLine, (int)Atend.Control.Enum.ProductType.Terminal);
                return Entities;
            }

            public List<Entity> GetDemoForEdit(Point3d StartPoint, Point3d EndPoint)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                LineSegment3d ls1 = new LineSegment3d(StartPoint, EndPoint);
                Point3d BasePoint = ls1.MidPoint;
                CenterPoint = BasePoint;
                double Angle = new Line(StartPoint, EndPoint).Angle;

                Entities.Add(CreateCellEntity(BasePoint, 10, 5, (int)Atend.Control.Enum.ProductType.Breaker));

                Entities.Add(CreateLine(new Point3d(BasePoint.X - 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y + 2, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Key, 0, 0));

                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), 0.2));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 2, BasePoint.Y + 0.5, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y - 0.5, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Breaker, 0, 0));
                Matrix3d trans = Matrix3d.Rotation(Angle, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,
                                   new Point3d(BasePoint.X, BasePoint.Y, 0));

                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans);
                }

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans1);
                }

                return Entities;
            }

        }

        //~~~~~~~~~~~~~~~~~~~Method~~~~~~~~~~~~~~

        public AcDrawDisConnector()
        {
            eDisConnector = new Atend.Base.Equipment.EDisconnector();
            DKeyStatus = new Atend.Base.Design.DKeyStatus();
        }

        //update in tehran 7/15
        public void DrawDisconnector()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            System.Data.DataTable ParentList = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Disconnector);

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Disconnector).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Disconnector).CommentScale;


            PromptEntityOptions peo1 = new PromptEntityOptions("Select first node:");
            PromptEntityResult per = ed.GetEntity(peo1);
            if (per.Status == PromptStatus.OK)
            {
                Atend.Base.Acad.AT_INFO Info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                DataRow[] drs = ParentList.Select(string.Format("SoftwareCode={0}", Info1.NodeType));
                if (drs.Length != 0)
                {
                    PromptEntityOptions peo2 = new PromptEntityOptions("Select second node:");
                    PromptEntityResult per1 = ed.GetEntity(peo2);
                    if (per1.Status == PromptStatus.OK)
                    {
                        Atend.Base.Acad.AT_INFO Info2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per1.ObjectId);
                        DataRow[] drs1 = ParentList.Select(string.Format("SoftwareCode={0}", Info2.NodeType));
                        if (drs1.Length != 0 && Info1.ParentCode == Info2.ParentCode)
                        {

                            DrawDisconnectorJig _DrawDisconnectorJig = new DrawDisconnectorJig(MyScale);
                            List<Entity> ents = _DrawDisconnectorJig.GetDemo(
                                Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId)),
                                Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(per1.ObjectId)));

                            if (SaveDisConnectorData())
                            {

                                ObjectIdCollection OIC = new ObjectIdCollection();
                                //ed.WriteMessage("go for each\n");
                                foreach (Entity ent in ents)
                                {
                                    ObjectId TerminalOI = ObjectId.Null;
                                    ObjectId KetOI = ObjectId.Null;
                                    ObjectId NOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                                    Atend.Global.Acad.AcadJigs.MyLine terminal = ent as Atend.Global.Acad.AcadJigs.MyLine;
                                    if (terminal != null)
                                    {
                                        object ProductType = null;
                                        if (terminal.AdditionalDictionary.TryGetValue("ProductType", out ProductType))
                                        {
                                            if (Convert.ToInt32(ProductType) == (int)Atend.Control.Enum.ProductType.Terminal)
                                            {
                                                //ed.WriteMessage("terminal aws found\n");
                                                if (ProductType != null)
                                                {
                                                    TerminalOI = NOI;
                                                }
                                            }
                                            else if (Convert.ToInt32(ProductType) == (int)Atend.Control.Enum.ProductType.Key)
                                            {
                                                //ed.WriteMessage("Key aws found\n");
                                                if (ProductType != null)
                                                {
                                                    KetOI = NOI;
                                                }

                                            }
                                        }

                                    }
                                    OIC.Add(NOI);

                                    if (TerminalOI == ObjectId.Null)
                                    {
                                        if (KetOI == ObjectId.Null)
                                        {
                                            //ed.WriteMessage("TerminalOI == null\n");
                                            Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                            groupInfo.NodeCode = DisConnectorPack.Code.ToString();
                                            groupInfo.ParentCode = "";
                                            groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Disconnector;
                                            groupInfo.ProductCode = DisConnectorPack.ProductCode;
                                            groupInfo.Insert();
                                        }
                                        else
                                        {
                                            //ed.WriteMessage("KEYOI != null\n");
                                            Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                            groupInfo.NodeCode = DisConnectorPack.Code.ToString();
                                            groupInfo.ParentCode = "";
                                            groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Key;
                                            groupInfo.ProductCode = DisConnectorPack.ProductCode;
                                            groupInfo.Insert();
                                        }
                                    }
                                    else
                                    {
                                        //ed.WriteMessage("TerminalOI != null\n");
                                        Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                        groupInfo.NodeCode = DisConnectorPack.Code.ToString();
                                        groupInfo.ParentCode = "";
                                        groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Terminal;
                                        groupInfo.ProductCode = DisConnectorPack.ProductCode;
                                        groupInfo.Insert();
                                    }

                                }

                                ObjectId GOI = Atend.Global.Acad.Global.MakeGroup(DisConnectorPack.Code.ToString(), OIC);

                                Atend.Base.Acad.AT_INFO groupInfo1 = new Atend.Base.Acad.AT_INFO(GOI);
                                groupInfo1.NodeCode = DisConnectorPack.Code.ToString();
                                groupInfo1.ParentCode = "";
                                groupInfo1.NodeType = (int)Atend.Control.Enum.ProductType.Disconnector;
                                groupInfo1.ProductCode = DisConnectorPack.ProductCode;
                                groupInfo1.Insert();


                                ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(
                                    Atend.Global.Acad.UAcad.WriteNote(eDisConnector.Comment,
                                    _DrawDisconnectorJig.MyCenterPoint
                                    , MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString()
                                    );

                                Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                                textInfo.NodeCode = "";
                                textInfo.ParentCode = DisConnectorPack.Code.ToString();
                                textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                textInfo.ProductCode = 0;
                                textInfo.Insert();

                                Atend.Base.Acad.AT_SUB groupSub = new Atend.Base.Acad.AT_SUB(GOI);
                                groupSub.SubIdCollection.Add(per.ObjectId);
                                groupSub.SubIdCollection.Add(per1.ObjectId);
                                groupSub.SubIdCollection.Add(TextOi);
                                groupSub.Insert();

                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(GOI, per.ObjectId);
                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(GOI, per1.ObjectId);

                            }
                        }//if (drs1.Length != 0)
                    }//if (per1.Status == PromptStatus.OK)
                }//if (drs.Length != 0)
            }//if (per.Status == PromptStatus.OK )
        }

        //MOUSAVI 
        private bool SaveDisConnectorData()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction aTransaction;

            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();

                try
                {
                    if (!UseAccess)
                    {
                        {
                            if (!eDisConnector.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("eDisconnector.AccessInsert failed");
                            }
                        }
                    }


                    DisConnectorPack = new Atend.Base.Design.DPackage();
                    DisConnectorPack.Count = 1;
                    DisConnectorPack.IsExistance = Existance;
                    DisConnectorPack.ProjectCode = ProjectCode;
                    DisConnectorPack.ProductCode = eDisConnector.Code;
                    DisConnectorPack.Type = (int)Atend.Control.Enum.ProductType.Disconnector;
                    DisConnectorPack.ParentCode = Guid.Empty;
                    DisConnectorPack.LoadCode = 0;
                    DisConnectorPack.Number = "";
                    DisConnectorPack.NodeCode = Guid.Empty;
                    DisConnectorPack.ProjectCode = ProjectCode;
                    if (!DisConnectorPack.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("dPack.AccessInsert failed");
                    }

                    DKeyStatus = new Atend.Base.Design.DKeyStatus();
                    DKeyStatus.NodeCode = DisConnectorPack.Code;
                    DKeyStatus.Type = (int)Atend.Control.Enum.ProductType.Disconnector;
                    DKeyStatus.IsClosed = false;
                    if (!DKeyStatus.Insert(aTransaction, aConnection))
                    {
                        throw new System.Exception("DKeyStatus.AccessInsert failed");
                    }


                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveDisConnectorData 02 :{0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveDisConnectorData 01 :{0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.DisConnectorData.UseAccess = true;
            //UseAccess = true;

            #endregion

            return true;


        }

        public bool UpdateDisConnectorData(Guid EXCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {
                    DisConnectorPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                    if (!UseAccess)
                    {
                        if (!eDisConnector.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eDisConnector.AccessInsert failed");
                        }
                        //if (!Atend.Base.Equipment.EOperation.SentFromLocalToAccess(eDisConnector.XCode, (int)Atend.Control.Enum.ProductType.Disconnector, eDisConnector.Code, aTransaction, aConnection))
                        //{
                        //    throw new System.Exception("operation failed");
                        //}

                    }
                    DisConnectorPack.IsExistance = Existance;
                    DisConnectorPack.ProductCode = eDisConnector.Code;
                    DisConnectorPack.ProjectCode = ProjectCode;
                    DisConnectorPack.Number = "";
                    if (DisConnectorPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = eDisConnector.Code;
                        atinfo.Insert();
                        ChangeComment(Atend.Global.Acad.UAcad.GetEntityGroup(selectedObjectId), eDisConnector.Comment);
                    }
                    else
                    {
                        throw new System.Exception("DisConnectorPack.AccessInsert2 failed");
                    }

                    if (!DKeyStatus.Update(aTransaction, aConnection))
                    {
                        throw new System.Exception("dKeyStatus.accessUpdate Failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateDisConnector 01(transaction) : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateDisConnector 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static void AddNewDisconnector(Point3d StartPoint, Point3d EndPoint, ObjectId GroupOI)
        {
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Disconnector).Scale;
            //Database db = Application.DocumentManager.MdiActiveDocument.Database;
            DrawDisconnectorJig _DrawDisconnectorJig = new DrawDisconnectorJig(MyScale);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start22:{0} end22:{1} \n", StartPoint, EndPoint);
            ObjectIdCollection OIC = new ObjectIdCollection();
            foreach (Entity ent in _DrawDisconnectorJig.GetDemoForEdit(StartPoint, EndPoint))
            {
                //Group group = tr.GetObject(GroupOI, OpenMode.ForWrite, true) as Group;
                //if (group != null)
                //{
                //ObjectId[] objs = group.GetAllEntityIds();
                //foreach (ObjectId id in objs)
                //{

                //    OIC.Add(id);

                //}
                ObjectId NewOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                OIC.Add(NewOI);
                Atend.Base.Acad.AT_INFO _AT_INFO = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(GroupOI);
                _AT_INFO.SelectedObjectId = NewOI;
                _AT_INFO.Insert();


                //}
            }
            Atend.Global.Acad.Global.AddObjectToGroup(GroupOI, OIC);
            //ed.WriteMessage("add to group for new drawing finished\n");
            //}
            //}
        }

        public static bool DeleteDisconnectorData(ObjectId DisOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_INFO _info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(DisOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(_info.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete Disconnector\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(" ERROR Disconnector : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteDisconnector(ObjectId DisOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(Atend.Global.Acad.UAcad.GetEntityGroup(DisOI));
                ObjectId oid = ObjectId.Null;
                foreach (ObjectId collect in CollectionDisconnector)
                {
                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        oid = collect;
                        //CollectionDisconnector.Remove(collect);
                    }
                }

                if (oid == ObjectId.Null)
                    oid = DisOI;

                //Move Comment
                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(oid);
                Atend.Base.Acad.AT_SUB sub2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
                foreach (ObjectId h in sub2.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO at_info20 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(h);
                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(h))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                    }
                    if (at_info20.ParentCode != "NONE" && at_info20.NodeType == (int)Atend.Control.Enum.ProductType.Consol)
                    {
                        Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(h);
                        foreach (ObjectId oi in consolSub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, h);
                            }
                        }
                    }
                }

                //remove Sub Group
                foreach (ObjectId c2 in CollectionDisconnector)
                {
                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
                    {
                        ed.WriteMessage("Error In Delete Disconnector \n");
                    }
                }

                //+++
                //if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(DisOI))
                //{
                //    throw new System.Exception("GRA while delete DisConnector \n");
                //}
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Disconnector : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public void ChangeComment(ObjectId SelectedLineObjectID, string Text)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
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
                        }
                        else if (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                        {
                            ChangeEntityText(oi, Text);
                        }
                    }
                }
                else
                {
                }
            }
        }

        public void ChangeEntityText(ObjectId SelectedTextObjectID, string Text)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBText dbtext = (DBText)tr.GetObject(SelectedTextObjectID, OpenMode.ForWrite);
                if (dbtext != null)
                {
                    dbtext.TextString = Text;
                }
                tr.Commit();
            }
        }

    }
}
