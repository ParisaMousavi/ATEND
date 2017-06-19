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

//get from tehran 7/15
namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawLight
    {

        //~~~~~~~~~~~~~~~~~~~~~~~~ properties ~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        int projectCode;
        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        int existance;
        public int Existance
        {
            get { return existance; }
            set { existance = value; }
        }

        bool useAccess;
        public bool UseAccess
        {
            get { return useAccess; }
            set { useAccess = value; }
        }

        Atend.Base.Equipment.ELight _eLight;
        public Atend.Base.Equipment.ELight eLight
        {
            get { return _eLight; }
            set { _eLight = value; }
        }

        Atend.Base.Design.DPackage LightPack = new Atend.Base.Design.DPackage();

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~ Class ~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        class DrawLightJig : DrawJig
        {
            public Point3d CenterPoint01 = Point3d.Origin, CenterPoint02 = Point3d.Origin;
            List<Entity> Entitied = new List<Entity>();
            //Point3dCollection p3c;
            Entity ContainerEntity = null;
            public bool PartOneIsActive = true;
            Matrix3d m_ucs;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            double MyScale = 1;

            public DrawLightJig(Entity Container, double Scale)
            {
                //p3c = ConvertEntityToPoint3dCollection(Container);
                MyScale = Scale;
                ContainerEntity = Container;

                CenterPoint02 = new Point3d(CenterPoint01.X + 40, CenterPoint01.Y, CenterPoint01.Z);
                Entitied.Add(DrawConsol(CenterPoint01, 10, 10));
                Entitied.Add(DrawLine(CenterPoint01, CenterPoint02));
                Entitied.Add(DrawRod(CenterPoint02, 6, 6));
                Entitied.Add(DrawTriangle(CenterPoint02));

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

            private Entity DrawTriangle(Point3d BasePoint)
            {

                double BaseX = BasePoint.X;
                double BaseY = BasePoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine _MyPolyline = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                _MyPolyline.AddVertexAt(_MyPolyline.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 3), 0, 0, 0);
                _MyPolyline.AddVertexAt(_MyPolyline.NumberOfVertices, new Point2d(BaseX + 3, BaseY), 0, 0, 0);
                _MyPolyline.AddVertexAt(_MyPolyline.NumberOfVertices, new Point2d(BaseX - 3, BaseY - 3), 0, 0, 0);
                _MyPolyline.AddVertexAt(_MyPolyline.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 3), 0, 0, 0);
                _MyPolyline.Closed = true;


                Atend.Global.Acad.AcadJigs.SaveExtensionData(_MyPolyline, (int)Atend.Control.Enum.ProductType.Else);
                return _MyPolyline;

            }

            private Entity DrawRod(Point3d BasePoint, double Width, double Height)
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


                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.Rod);
                return pLine;

            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
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
                            return SamplerStatus.OK;
                        }
                    }
                    else
                    {
                        return SamplerStatus.Cancel;
                    }

                }
            }

            private Entity DrawCircle(Point3d CenterPoint, double Radius)
            {
                Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

                c.Center = CenterPoint;

                c.Normal = new Vector3d(0, 0, 1);

                c.Radius = Radius;

                //c.ColorIndex = 3;

                //Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.ConnectionPoint);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(c, CodeGuid);
                return c;
            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                //throw new System.Exception("The method or operation is not implemented.");

                Entitied.Clear();
                Entity enti = null;

                if (PartOneIsActive)
                {
                    //CenterPoint02 = new Point3d(CenterPoint01.X + 40, CenterPoint01.Y, CenterPoint01.Z);
                    //Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint01.X, CenterPoint01.Y, 0));
                    //Matrix3d trans2 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint02.X, CenterPoint02.Y, 0));

                    //enti = DrawConsol(CenterPoint01, 10, 10);
                    //enti.TransformBy(trans1);
                    //Entitied.Add(enti);

                    //Entitied.Add(DrawLine(CenterPoint01, CenterPoint02));

                    //enti = DrawRod(CenterPoint02, 6, 6);
                    //enti.TransformBy(trans2);
                    //Entitied.Add(enti);

                    //enti = DrawTriangle(CenterPoint02);
                    //enti.TransformBy(trans2);
                    //Entitied.Add(enti);

                }
                else
                {
                    Line l = new Line(CenterPoint01, CenterPoint02);
                    double newAngle = l.Angle;
                    Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint01.X, CenterPoint01.Y, 0));
                    Matrix3d trans2 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint02.X, CenterPoint02.Y, 0));

                    //enti = DrawConsol(CenterPoint01, 10, 10);
                    //enti.TransformBy(trans1);
                    //Entitied.Add(enti);

                    Entitied.Add(DrawLine(CenterPoint01, CenterPoint02));

                    enti = DrawCircle(CenterPoint02, 6);
                    enti.TransformBy(trans2);
                    Entitied.Add(enti);

                    //enti = DrawTriangle(CenterPoint02);
                    //enti.TransformBy(trans2);
                    //Entitied.Add(enti);

                }

                ////~~~~~~~~ SCALE ~~~~~~~~~~

                //Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint01.X, CenterPoint01.Y, 0));
                //foreach (Entity en in Entitied)
                //{
                //    en.TransformBy(trans1);
                //}

                ////~~~~~~~~~~~~~~~~~~~~~~~~~


                foreach (Entity ent in Entitied)
                {
                    draw.Geometry.Draw(ent);
                }


                return true;
            }

            public List<Entity> GetEntities()
            {
                return Entitied;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~ methods ~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public void DrawLigth()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti = true;
            ObjectIdCollection NewDrawnCollection = new ObjectIdCollection();
            //ObjectId ConsolElseOI = ObjectId.Null, ConnectionOI = ObjectId.Null, RodOI = ObjectId.Null, RodTriangeOI = ObjectId.Null;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Light).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Light).CommentScale;

            PromptEntityOptions peo = new PromptEntityOptions("\nSelect Container :");
            PromptEntityResult per = ed.GetEntity(peo);
            if (per.Status == PromptStatus.OK)
            {
                System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.Light);
                Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(at_info.NodeType)));
                if (drs.Length > 0)
                {
                    DrawLightJig _DrawLightJig;
                    _DrawLightJig = new DrawLightJig(Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId), MyScale);
                    while (conti)
                    {


                        //p = LineSeg.GetClosestPointTo(LineEntity.StartPoint).Point;

                        _DrawLightJig.CenterPoint01 = per.PickedPoint;
                        _DrawLightJig.PartOneIsActive = false;
                        PromptResult pr = ed.Drag(_DrawLightJig);
                        if (pr.Status == PromptStatus.OK && !_DrawLightJig.PartOneIsActive)
                        {
                            //pr = ed.Drag(_DrawLightJig);
                            conti = false;
                            //ed.WriteMessage("1 \n");
                            #region Save Data Here

                            List<Entity> Entities = _DrawLightJig.GetEntities();
                            //ed.WriteMessage("2 \n");
                            if (SaveLightData(new Guid(at_info.NodeCode)))
                            {

                                //---------------------
                                //ObjectId ConsolElseOI = ObjectId.Null;
                                foreach (Entity ent in Entities)
                                {

                                    ObjectId newDrawnoi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.LOW_AIR.ToString());
                                    ////////Atend.Global.Acad.AcadJigs.MyPolyLine mPoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                                    ////////if (mPoly != null)
                                    ////////{
                                    ////////    //ed.WriteMessage("POLY LINE FOUND\n");
                                    ////////    if (mPoly.AdditionalDictionary.ContainsKey("ProductType"))
                                    ////////    {
                                    ////////        object ProductType = null;
                                    ////////        mPoly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                    ////////        if (ProductType != null)
                                    ////////        {
                                    ////////            if (Convert.ToInt32(ProductType) == (int)Atend.Control.Enum.ProductType.ConsolElse)
                                    ////////            {
                                    ////////                ConsolElseOI = newDrawnoi;
                                    ////////                Atend.Base.Acad.AT_INFO ConsolElseInfo = new Atend.Base.Acad.AT_INFO(newDrawnoi);
                                    ////////                ConsolElseInfo.ParentCode = at_info.NodeCode;
                                    ////////                ConsolElseInfo.NodeCode = LightPack.Code.ToString();
                                    ////////                ConsolElseInfo.NodeType = Convert.ToInt32(ProductType);
                                    ////////                ConsolElseInfo.ProductCode = 0;
                                    ////////                ConsolElseInfo.Insert();
                                    ////////            }
                                    ////////        }
                                    ////////    }
                                    ////////}
                                    NewDrawnCollection.Add(newDrawnoi);

                                }

                                //////if (ConsolElseOI != ObjectId.Null)
                                //////{

                                //////    Atend.Base.Acad.AT_SUB ConsolElseSub = new Atend.Base.Acad.AT_SUB(ConsolElseOI);
                                //////    foreach (ObjectId oi in NewDrawnCollection)
                                //////    {
                                //////        if (oi != ConsolElseOI)
                                //////        {
                                //////            //ed.WriteMessage("ConsolElseSubOI:{0}\n", oi);
                                //////            ConsolElseSub.SubIdCollection.Add(oi);
                                //////        }
                                //////    }
                                //////    ConsolElseSub.SubIdCollection.Add(per.ObjectId);
                                //////    ConsolElseSub.Insert();
                                //////}
                                //---------------------




                                foreach (ObjectId oi in NewDrawnCollection)
                                {
                                    //if (oi != ConsolElseOI)
                                    //{
                                    Atend.Base.Acad.AT_INFO a = new Atend.Base.Acad.AT_INFO(oi);
                                    a.ParentCode = at_info.NodeCode;
                                    a.NodeCode = LightPack.Code.ToString();
                                    a.NodeType = (int)Atend.Control.Enum.ProductType.Light;
                                    a.ProductCode = LightPack.ProductCode; ;
                                    a.Insert();
                                    // }
                                }



                                ObjectId NewCreatedGroup = Atend.Global.Acad.Global.MakeGroup(LightPack.Code.ToString(), NewDrawnCollection);
                                ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(eLight.Comment, new Point3d(_DrawLightJig.CenterPoint02.X, _DrawLightJig.CenterPoint02.Y, 0), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                Atend.Base.Acad.AT_INFO GroupInfo1 = new Atend.Base.Acad.AT_INFO(txtOI);
                                GroupInfo1.ParentCode = LightPack.Code.ToString();
                                GroupInfo1.NodeCode = "";
                                GroupInfo1.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                GroupInfo1.ProductCode = 0;
                                GroupInfo1.Insert();


                                Atend.Base.Acad.AT_INFO GroupInfo = new Atend.Base.Acad.AT_INFO(NewCreatedGroup);
                                GroupInfo.ParentCode = at_info.NodeCode;
                                GroupInfo.NodeCode = LightPack.Code.ToString();
                                GroupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Light;
                                GroupInfo.ProductCode = LightPack.ProductCode;
                                GroupInfo.Insert();


                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewCreatedGroup, per.ObjectId);
                                Atend.Base.Acad.AT_SUB GroupSub = new Atend.Base.Acad.AT_SUB(NewCreatedGroup);
                                GroupSub.SubIdCollection.Add(per.ObjectId);
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
                }
                else
                {
                    string s = "";
                    foreach (DataRow dr in Parents.Rows)
                    {
                        s = s + Atend.Base.Design.DProductProperties.AccessSelectByCodeDrawable(Convert.ToInt32(dr["ContainerCode"])).ProductName + "-";
                    }
                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                    notification.Title = "ÇÊÕÇáÇÊ ãÌÇÒ";
                    notification.Msg = s;
                    notification.infoCenterBalloon();

                }
            }//prompt status ok
        }

        private bool SaveLightData(Guid ParentCode)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aconnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction aTransaction;

            try
            {
                aconnection.Open();
                aTransaction = aconnection.BeginTransaction();
                try
                {

                    if (!UseAccess)
                    {
                        if (!eLight.AccessInsert(aTransaction, aconnection, true, true))
                        {
                            throw new System.Exception("eLight.AccessInsert failed");
                        }
                    }

                    LightPack = new Atend.Base.Design.DPackage();
                    LightPack.Count = 1;
                    LightPack.IsExistance = Existance;
                    LightPack.LoadCode = 0;
                    LightPack.NodeCode = new Guid();
                    LightPack.Number = "LIGHT";
                    LightPack.ParentCode = ParentCode;
                    LightPack.ProductCode = eLight.Code;
                    LightPack.ProjectCode = ProjectCode;
                    LightPack.Type = (int)Atend.Control.Enum.ProductType.Light;
                    if (!LightPack.AccessInsert(aTransaction, aconnection))
                    {
                        throw new System.Exception("LightPack.AccessInsert failed");
                    }



                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveLightData 01 :{0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aconnection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveLightData 02 :{0} \n", ex.Message);
                aconnection.Close();
                return false;
            }

            aTransaction.Commit();
            aconnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.LightData.UseAccess = true;
            //UseAccess = true;

            #endregion

            return true;
        }

        public bool UpdateLightData(Guid EXCode)
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
                    LightPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                    if (!UseAccess)
                    {
                        if (!eLight.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eLight.AccessInsert failed");
                        }
                    }
                    LightPack.IsExistance = Existance;
                    LightPack.ProjectCode = ProjectCode;
                    LightPack.ProductCode = eLight.Code;
                    LightPack.Number = "";
                    if (LightPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = eLight.Code;
                        atinfo.Insert();
                        ChangeComment(Atend.Global.Acad.UAcad.GetEntityGroup(selectedObjectId), eLight.Comment);
                    }
                    else
                    {
                        throw new System.Exception("LightPack.AccessInsert2 failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateLight 01(transaction) : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateLight 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteLightData(ObjectId LightOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //delete Light
                Atend.Base.Acad.AT_INFO Lightinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LightOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(Lightinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete dpackage\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR Light : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteLightData(ObjectId LightOI, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //delete Light
                Atend.Base.Acad.AT_INFO Lightinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LightOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(Lightinfo.NodeCode.ToString()), _Transaction, _Connection))
                {
                    throw new System.Exception("Error In Delete dpackage\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR Transaction.Light  : {0} \n", ex.Message);
                _Transaction.Rollback();
                return false;
            }
            return true;
        }

        public static bool DeleteLight(ObjectId LightOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(LightOI);
                //Find Parent
                Atend.Base.Acad.AT_SUB EntitySb = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
                foreach (ObjectId oi in EntitySb.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO pInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                    if (pInfo.ParentCode != "NONE" && (pInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole || pInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        Atend.Base.Acad.AT_SUB pSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                        foreach (ObjectId soi in pSub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO Info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(soi);
                            if (Info1.ParentCode != "NONE" && Info1.NodeType == (int)Atend.Control.Enum.ProductType.Light && id == soi)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(id, oi);
                            }
                        }
                    }
                    if (pInfo.ParentCode != "NONE" && pInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                    }
                }

                //Delete Group
                ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                foreach (ObjectId collect in Collection)
                {
                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                    {
                        throw new System.Exception("Error In Delete Group\n");
                    }
                }

                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(LightOI))
                {
                    throw new System.Exception("GRA while delete StreetBox\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Light: {0} \n", ex.Message);
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

        public static void RotateLight(double LastAngleDegree, double NewAngleDegree, ObjectId PoleOI, ObjectId LightOI, Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    ObjectIdCollection LightSub = Atend.Global.Acad.UAcad.GetGroupSubEntities(LightOI);

                    foreach (ObjectId oi in LightSub)
                    {
                        //Atend.Base.Acad.AT_INFO KhazanSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //if (KhazanSubInfo.ParentCode != "NONE" && KhazanSubInfo.NodeType == (int)Atend.Control.Enum.PLightuctType.ConsolElse)
                        //{
                        Entity ent = tr.GetObject(oi, OpenMode.ForWrite) as Entity;
                        if (ent != null)
                        {

                            ////Polyline LineEntity = ent as Polyline;
                            ////if (LineEntity != null)
                            ////{
                            //ed.WriteMessage("khazan entity found \n");
                            //KhazanOI = oi;
                            //Atend.Acad.AcadMove.LastCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(oi));
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

                    //ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(LightOI);
                    //Atend.Base.Acad.AT_SUB EntityS = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(id);
                    //foreach (ObjectId oisb in EntityS.SubIdCollection)
                    //{
                    //    Atend.Base.Acad.AT_INFO _Info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oisb);
                    //    if (_Info.ParentCode != "NONE" && _Info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    //    {
                    //        Entity ent = tr.GetObject(oisb, OpenMode.ForWrite) as Entity;
                    //        if (ent != null)
                    //        {
                    //            Matrix3d trans = Matrix3d.Rotation(((LastAngleDegree * Math.PI) / 180) * -1, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                    //            ent.TransformBy(trans);
                    //            trans = Matrix3d.Rotation(((NewAngleDegree * Math.PI) / 180), ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                    //            ent.TransformBy(trans);
                    //        }
                    //    }
                    //}
                    tr.Commit();
                    //Atend.Global.Acad.AcadMove.LightOI  = LightOI;
                    //Atend.Global.Acad.AcadMove.AllowToMove = true;
                    //Atend.Global.Acad.AcadMove.MoveLight(LightOI);
                }
            }
        }


    }
}
