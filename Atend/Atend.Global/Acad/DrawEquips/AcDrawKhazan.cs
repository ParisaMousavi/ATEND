using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;


namespace Atend.Global.Acad.DrawEquips
{

    public class AcDrawKhazan
    {

        //~~~~~~~~~~~~~~~~~~~~~Properties~~~~~~~~~~~~~~~~~~~~~~~~~~//


        Atend.Base.Equipment.EKhazanTip _eKhazanTip;
        public Atend.Base.Equipment.EKhazanTip eKhazanTip
        {
            get { return _eKhazanTip; }
            set { _eKhazanTip = value; }
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

        bool _useAccess;
        public bool UseAccess
        {
            get { return _useAccess; }
            set { _useAccess = value; }
        }

        Atend.Base.Design.DPackage KhazanPack;

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        public Guid EXCode;

        //~~~~~~~~~~~~~~~~~~~~~Class~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public class DrawKhazanJig : DrawJig
        {

            Point3d CenterPoint01 = Point3d.Origin, CenterPoint02 = Point3d.Origin, CommentPosition = Point3d.Origin;
            List<Entity> Entities = new List<Entity>();
            public bool PartOneIsActive = true;
            Matrix3d m_ucs;
            //Point3dCollection p3c;
            Entity ContainerEntity = null;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double MyScale = 1;

            public DrawKhazanJig(Entity Container, double Scale)
            {
                MyScale = Scale;
                //p3c = ConvertEntityToPoint3dCollection(Container);

                ContainerEntity = Container;
                CenterPoint02 = new Point3d(CenterPoint01.X + 40, CenterPoint01.Y, CenterPoint01.Z);
                CommentPosition = new Point3d(CenterPoint02.X, CenterPoint02.Y, CenterPoint02.Z);


                Entities.Add(DrawConsol(CenterPoint01, 10, 10));
                Entities.Add(DrawLine(CenterPoint01, CenterPoint02));
                Entities.Add(DrawHeader(CenterPoint02));
                Entities.Add(DrawLine(CenterPoint02));
                Entities.Add(DrawLine(new Point3d(CenterPoint02.X + 2, CenterPoint02.Y, CenterPoint02.Z)));

                m_ucs = ed.CurrentUserCoordinateSystem;

            }

            private Entity DrawConsol(Point3d BasePoint, double Width, double Height)
            {

                double BaseX = BasePoint.X;
                double BaseY = BasePoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
                pLine.Closed = true;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.ConsolElse);
                return pLine;

            }

            private Entity DrawLine(Point3d StartPoint, Point3d EndPoint)
            {

                Atend.Global.Acad.AcadJigs.MyLine l = new Atend.Global.Acad.AcadJigs.MyLine();
                l.StartPoint = StartPoint;
                l.EndPoint = EndPoint;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(l, (int)Atend.Control.Enum.ProductType.Connection);
                return l;

            }

            private Entity DrawHeader(Point3d BasePoint)
            {

                Atend.Global.Acad.AcadJigs.MyCircle _MyCircle = new Atend.Global.Acad.AcadJigs.MyCircle();
                _MyCircle.Radius = 5;
                _MyCircle.Normal = Vector3d.ZAxis;
                _MyCircle.Center = BasePoint;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(_MyCircle, (int)Atend.Control.Enum.ProductType.Khazan);
                return _MyCircle;

            }

            private Entity DrawLine(Point3d BasePoint)
            {
                Atend.Global.Acad.AcadJigs.MyLine _MyLine = new Atend.Global.Acad.AcadJigs.MyLine();
                _MyLine.StartPoint = new Point3d(BasePoint.X, BasePoint.Y - 2.5, 0);
                _MyLine.EndPoint = new Point3d(BasePoint.X, BasePoint.Y + 2.5, 0);


                Atend.Global.Acad.AcadJigs.SaveExtensionData(_MyLine, (int)Atend.Control.Enum.ProductType.Else);

                return _MyLine;

            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                //throw new System.Exception("The method or operation is not implemented.");

                JigPromptPointOptions ppo = new JigPromptPointOptions();
                PromptPointResult pr;

                if (PartOneIsActive)
                {

                    ppo.Message = "\nPart One Position : ";
                    pr = prompts.AcquirePoint(ppo);
                    if (pr.Status == PromptStatus.OK)
                    {

                        if (pr.Value == CenterPoint01)
                        {
                            return SamplerStatus.NoChange;
                        }
                        else
                        {
                            if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)ContainerEntity, pr.Value) == true)
                            {
                                CenterPoint01 = pr.Value;
                                return SamplerStatus.OK;
                            }
                            else
                            {
                                return SamplerStatus.NoChange;
                            }
                        }
                    }
                    else
                    {
                        return SamplerStatus.Cancel;
                    }


                }
                else
                {
                    ppo.Message = "\nPart Two Position : ";
                    pr = prompts.AcquirePoint(ppo);
                    if (pr.Status == PromptStatus.OK)
                    {
                        if (pr.Value == CenterPoint02)
                        {
                            return SamplerStatus.NoChange;
                        }
                        else
                        {
                            CenterPoint02 = pr.Value;
                            CommentPosition = new Point3d(CenterPoint02.X, CenterPoint02.Y, CenterPoint02.Z);

                            return SamplerStatus.OK;
                        }
                    }
                    else
                    {
                        return SamplerStatus.Cancel;
                    }

                }


            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                //throw new System.Exception("The method or operation is not implemented.");

                Entities.Clear();
                Entity enti;

                if (PartOneIsActive)
                {

                    CenterPoint02 = new Point3d(CenterPoint01.X + 40, CenterPoint01.Y, CenterPoint01.Z);
                    Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint01.X, CenterPoint01.Y, 0));
                    CenterPoint02 = CenterPoint01.ScaleBy(MyScale, CenterPoint01);
                    Matrix3d trans2 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint02.X, CenterPoint02.Y, 0));

                    enti = DrawConsol(CenterPoint01, 10, 10);
                    enti.TransformBy(trans1);
                    Entities.Add(enti);
                    //enti=
                    Entities.Add(DrawLine(CenterPoint01, CenterPoint02));

                    enti = DrawHeader(CenterPoint02);
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

                    enti = DrawLine(CenterPoint02);
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

                    enti = DrawLine(new Point3d(CenterPoint02.X + 2, CenterPoint02.Y, CenterPoint02.Z));
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

                    ////~~~~~~~~ SCALE ~~~~~~~~~~


                    //foreach (Entity en in Entities)
                    //{
                    //    en.TransformBy(trans1);
                    //}

                    ////~~~~~~~~~~~~~~~~~~~~~~~~~


                }
                else
                {

                    Line l = new Line(CenterPoint01, CenterPoint02);
                    double newAngle = l.Angle;
                    Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint01.X, CenterPoint01.Y, 0));
                    Matrix3d trans2 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint02.X, CenterPoint02.Y, 0));


                    enti = DrawConsol(CenterPoint01, 10, 10);
                    enti.TransformBy(trans1);
                    Entities.Add(enti);

                    enti = DrawLine(CenterPoint01, CenterPoint02);
                    //enti.TransformBy(trans1);
                    Entities.Add(enti);

                    enti = DrawHeader(CenterPoint02);
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

                    enti = DrawLine(CenterPoint02);
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

                    enti = DrawLine(new Point3d(CenterPoint02.X + 2, CenterPoint02.Y, CenterPoint02.Z));
                    enti.TransformBy(trans2);
                    Entities.Add(enti);

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

            public Point3d GetCommentPoSition()
            {
                return CommentPosition;
            }

        }
        //~~~~~~~~~~~~~~~~~~~~~Methods~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public AcDrawKhazan()
        {
            KhazanPack = new Atend.Base.Design.DPackage();
        }

        public void DrawKhazan()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti = true;

            //ObjectId ConsolElseOI, ConnectionOI, KhazanOI, ELse1OI, Else2OI;
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.BankKhazan).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.BankKhazan).CommentScale;
            //ed.WriteMessage("AccessSelectBySoftwareCode:{0}:{1}", MyScale, MyCommentScale);

            ObjectIdCollection NewDrawnCollection = new ObjectIdCollection();
            DrawKhazanJig drawKhazan;
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect Container:");
            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status == PromptStatus.OK)
            {
                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                if (at_info.ParentCode != "NONE" && (at_info.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                {

                    Entity entContainer = Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId);
                    if (entContainer != null)
                    {
                        drawKhazan = new DrawKhazanJig(entContainer, MyScale);
                        //ed.WriteMessage("current scale : {0} : {1} \n", MyScale, (int)Atend.Control.Enum.ProductType.BankKhazan);
                        while (conti)
                        {
                            PromptResult pr = ed.Drag(drawKhazan);
                            if (pr.Status == PromptStatus.OK && drawKhazan.PartOneIsActive)
                            {
                                drawKhazan.PartOneIsActive = false;
                                pr = ed.Drag(drawKhazan);
                                if (pr.Status == PromptStatus.OK && !drawKhazan.PartOneIsActive)
                                {
                                    conti = false;
                                    #region Save Data Here

                                    List<Entity> Entities = drawKhazan.GetEntities();
                                    //ed.WriteMessage("2 \n");
                                    if (SaveKhazanData(at_info.NodeCode))
                                    {
                                        //ed.WriteMessage("3 \n");
                                        ObjectId ConsolElseOI = ObjectId.Null;
                                        foreach (Entity ent in Entities)
                                        {
                                            //ed.WriteMessage("1001\n");
                                            ObjectId newDrawnoi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());
                                            Atend.Global.Acad.AcadJigs.MyPolyLine mPoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                                            if (mPoly != null)
                                            {
                                                //ed.WriteMessage("POLY LINE FOUND\n");
                                                if (mPoly.AdditionalDictionary.ContainsKey("ProductType"))
                                                {
                                                    object ProductType = null;
                                                    mPoly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                                    if (ProductType != null)
                                                    {
                                                        if (Convert.ToInt32(ProductType) == (int)Atend.Control.Enum.ProductType.ConsolElse)
                                                        {
                                                            //ed.WriteMessage("1002\n");
                                                            ConsolElseOI = newDrawnoi;
                                                            Atend.Base.Acad.AT_INFO ConsolElseInfo = new Atend.Base.Acad.AT_INFO(newDrawnoi);
                                                            ConsolElseInfo.ParentCode = at_info.NodeCode;
                                                            ConsolElseInfo.NodeCode = KhazanPack.Code.ToString();
                                                            ConsolElseInfo.NodeType = Convert.ToInt32(ProductType);
                                                            ConsolElseInfo.ProductCode = 0;
                                                            ConsolElseInfo.Insert();
                                                        }
                                                    }
                                                }
                                            }
                                            NewDrawnCollection.Add(newDrawnoi);

                                        }

                                        //ed.WriteMessage("ConsolElseOI:{0}\n", ConsolElseOI);
                                        if (ConsolElseOI != ObjectId.Null)
                                        {

                                            Atend.Base.Acad.AT_SUB ConsolElseSub = new Atend.Base.Acad.AT_SUB(ConsolElseOI);
                                            foreach (ObjectId oi in NewDrawnCollection)
                                            {
                                                if (oi != ConsolElseOI)
                                                {
                                                    //ed.WriteMessage("ConsolElseSubOI:{0}\n", oi);
                                                    ConsolElseSub.SubIdCollection.Add(oi);
                                                }
                                            }
                                            ConsolElseSub.SubIdCollection.Add(per.ObjectId);
                                            ConsolElseSub.Insert();
                                        }

                                        foreach (ObjectId oi in NewDrawnCollection)
                                        {
                                            if (oi != ConsolElseOI)
                                            {
                                                Atend.Base.Acad.AT_INFO a = new Atend.Base.Acad.AT_INFO(oi);
                                                a.ParentCode = at_info.NodeCode;
                                                a.NodeCode = KhazanPack.Code.ToString();
                                                a.NodeType = (int)Atend.Control.Enum.ProductType.BankKhazan;
                                                a.ProductCode = KhazanPack.ProductCode;
                                                a.Insert();
                                            }

                                        }


                                        //ed.WriteMessage("Number of Entity : {0} \n", NewDrawnCollection.Count);

                                        ObjectId NewCreatedGroup =
                                        Atend.Global.Acad.Global.MakeGroup(KhazanPack.Code.ToString(), NewDrawnCollection);


                                        ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(
                                        Atend.Global.Acad.UAcad.WriteNote(eKhazanTip.Description, drawKhazan.GetCommentPoSition(), MyCommentScale),
                                        Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                        Atend.Base.Acad.AT_INFO GroupInfo1 = new Atend.Base.Acad.AT_INFO(txtOI);
                                        GroupInfo1.ParentCode = KhazanPack.Code.ToString();
                                        GroupInfo1.NodeCode = "";
                                        GroupInfo1.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                        GroupInfo1.ProductCode = 0;
                                        GroupInfo1.Insert();

                                        Atend.Base.Acad.AT_INFO GroupInfo = new Atend.Base.Acad.AT_INFO(NewCreatedGroup);
                                        GroupInfo.ParentCode = at_info.NodeCode;
                                        GroupInfo.NodeCode = KhazanPack.Code.ToString();
                                        GroupInfo.NodeType = (int)Atend.Control.Enum.ProductType.BankKhazan;
                                        GroupInfo.ProductCode = KhazanPack.ProductCode;
                                        GroupInfo.Insert();

                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewCreatedGroup, per.ObjectId);
                                        //ed.WriteMessage("PoleOI for KHazan:{0}", per.ObjectId);
                                        Atend.Base.Acad.AT_SUB GroupSub = new Atend.Base.Acad.AT_SUB(NewCreatedGroup);
                                        GroupSub.SubIdCollection.Add(per.ObjectId);
                                        //ed.WriteMessage("TXTOI for KHazan:{0}", txtOI);
                                        GroupSub.SubIdCollection.Add(txtOI);
                                        GroupSub.Insert();

                                    }

                                    #endregion
                                }
                                else
                                {
                                    conti = false;
                                }
                            }
                            else
                            {
                                conti = false;
                            }
                        }
                    }
                }//if it was pole
            }
        }

        public static void RotateKhazan(double LastAngleDegree, double NewAngleDegree, ObjectId PoleOI, ObjectId KhazanOI, Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    ObjectIdCollection KhazanSub = Atend.Global.Acad.UAcad.GetGroupSubEntities(KhazanOI);

                    foreach (ObjectId oi in KhazanSub)
                    {
                        //Atend.Base.Acad.AT_INFO KhazanSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //if (KhazanSubInfo.ParentCode != "NONE" && KhazanSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.ConsolElse)
                        //{
                        Entity ent = tr.GetObject(oi, OpenMode.ForWrite) as Entity;
                        if (ent != null)
                        {

                            ////Polyline LineEntity = ent as Polyline;
                            ////if (LineEntity != null)
                            ////{
                            //ed.WriteMessage("khazan entity found \n");
                            KhazanOI = oi;
                            Atend.Global.Acad.AcadMove.LastCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
                            Matrix3d trans = Matrix3d.Rotation(((LastAngleDegree * Math.PI) / 180) * -1, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                            ent.TransformBy(trans);

                            trans = Matrix3d.Rotation(((NewAngleDegree * Math.PI) / 180), ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                            ent.TransformBy(trans);

                            //Matrix3d m = new Matrix3d();
                            //Atend.Acad.AcadMove.ConsolOI = ConsolOI;
                            //Atend.Acad.AcadMove.isMoveConsolOnly = true;

                            ////}
                        }
                        //}
                    }
                    tr.Commit();
                    Atend.Global.Acad.AcadMove.BankKhazanOI = KhazanOI;
                    Atend.Global.Acad.AcadMove.AllowToMove = true;
                    Atend.Global.Acad.AcadMove.MoveKhazan(KhazanOI);
                }
            }
        }

        private bool SaveKhazanData(string ParentCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aconnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction atransaction;
            try
            {
                aconnection.Open();
                atransaction = aconnection.BeginTransaction();
                try
                {
                    //Atend.Base.Equipment.EKhazan kh = Atend.Base.Equipment.EKhazan.AccessSelectByXCode(eKhazanTip.XCode);
                    if (!UseAccess)
                    {
                        if (!eKhazanTip.AccessInsert(atransaction, aconnection, true, true))
                        {
                            throw new System.Exception("eKhazanTip.AccessInsert failed");
                        }
                    }

                    Guid Code = new Guid(ParentCode);
                    Atend.Base.Design.DPackage Package = Atend.Base.Design.DPackage.AccessSelectByNodeCode(Code, atransaction, aconnection);

                    KhazanPack.ParentCode = Package.Code;
                    KhazanPack.Type = (int)Atend.Control.Enum.ProductType.BankKhazan;
                    KhazanPack.LoadCode = 0;
                    KhazanPack.IsExistance = Existance;
                    KhazanPack.Number = "";
                    KhazanPack.ProductCode = eKhazanTip.Code;
                    KhazanPack.ProjectCode = ProjectCode;
                    if (!KhazanPack.AccessInsert(atransaction, aconnection))
                    {
                        throw new System.Exception("KhazanPack.AccessInsert failed");
                    }
                    //ed.WriteMessage("~~~~~~~~~~~ SAVE KHAZAN FINISHED ~~~~~~~~~~\n");

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("SaveKhazanData 02: {0} \n", ex1.Message);
                    atransaction.Rollback();
                    aconnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("SaveKhazanData 01 : {0} \n", ex.Message);
                aconnection.Close();
                return false;
            }

            atransaction.Commit();
            aconnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.KhazanData.UseAccess = true;
            //UseAccess = true;

            #endregion

            return true;

        }

        public bool UpdateKhazanData(Guid EXCode)
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
                    try
                    {
                        KhazanPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                        if (!UseAccess)
                        {
                            if (!eKhazanTip.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("KhazanTip.Insert failed");
                            }
                        }
                        KhazanPack.IsExistance = Existance;
                        KhazanPack.ProjectCode = ProjectCode;
                        KhazanPack.ProductCode = eKhazanTip.Code;
                        KhazanPack.Number = "";
                        if (KhazanPack.AccessUpdate(aTransaction, aConnection))
                        {
                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                            atinfo.ProductCode = eKhazanTip.Code;
                            atinfo.Insert();
                            //Atend.Global.Acad.UAcad.ChangeMText(selectedObjectId, eKhazanTip.Name);
                            ChangeComment(selectedObjectId, eKhazanTip.Description);
                        }
                        else
                        {
                            throw new System.Exception("KhazanPack.AccessInsert2 failed");
                        }
                        //++++++++++++++++++++++++++++++
                        if (!UseAccess)
                        {
                            //WENT TO
                            //if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(eKhazanTip.XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, eKhazanTip.Code, aTransaction, aConnection))
                            //{
                            //    throw new System.Exception("SentFromLocalToAccess failed");
                            //}
                        }
                        //++++++++++++++++++++++++++++++
                    }
                    catch (System.Exception ex1)
                    {
                        ed.WriteMessage("ERROR UpdateKhazanData(transaction) 001 : {0} \n", ex1.Message);
                        aTransaction.Rollback();
                        aConnection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateKhazanData(transaction) 01 : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateKhazanData 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }
            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteKhazanData(ObjectId KhazanOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_INFO khazaninfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(KhazanOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(khazaninfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Khazan Data : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteKhazanData(ObjectId KhazanOI, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_INFO khazaninfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(KhazanOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(khazaninfo.NodeCode.ToString()), _Transaction, _Connection))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR Transaction.Khazan : {0} \n", ex.Message);
                _Transaction.Rollback();
                return false;
            }
            return true;
        }

        public static bool DeleteKhazan(ObjectId KhazanOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(KhazanOI);
                ObjectIdCollection CollectionKhazan = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                foreach (ObjectId collect in CollectionKhazan)
                {
                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.ConsolElse)
                    {
                        Atend.Base.Acad.AT_SUB EntitySb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(KhazanOI);
                        foreach (ObjectId oi in EntitySb.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO poleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (poleInfo.ParentCode != "NONE" && (poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || poleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                            {
                                //Delete comment
                                Atend.Base.Acad.AT_SUB EntitySb2 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                                foreach (ObjectId oisub in EntitySb2.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_INFO poleInfosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisub);
                                    if (poleInfosub.ParentCode != "NONE" && poleInfosub.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
                                    {
                                        Atend.Base.Acad.AT_SUB KhazanCollection = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oisub);
                                        foreach (ObjectId obj in KhazanCollection.SubIdCollection)
                                        {
                                            Atend.Base.Acad.AT_INFO at_infosub = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
                                            if (at_infosub.ParentCode != "NONE" && at_infosub.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                                            {
                                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(obj))
                                                {
                                                    throw new System.Exception("Error In Delete Comment\n");
                                                }
                                            }
                                        }

                                    }
                                }

                                //Delete from AT_SUB
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(id, oi);
                            }
                        }


                    }

                }
                foreach (ObjectId collect in CollectionKhazan)
                {
                    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.BankKhazan)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                    }
                }

                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(KhazanOI))
                {
                    throw new System.Exception("Error In Delete BankKhazan\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Khazan : {0} \n", ex.Message);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    }
}
