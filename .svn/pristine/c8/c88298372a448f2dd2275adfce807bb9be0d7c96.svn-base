using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawCatOut
    {

        //update in tehran 7/15

        bool _UseAccess;
        public bool UseAccess
        {
            get { return _UseAccess; }
            set { _UseAccess = value; }
        }

        Atend.Base.Equipment.ECatOut _eCatOut;
        public Atend.Base.Equipment.ECatOut ECatOut
        {
            get { return _eCatOut; }
            set { _eCatOut = value; }
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

        Atend.Base.Design.DPackage CatOutPack;


        //~~~~~~~~~~~~~~~~~~~Class~~~~~~~~~~~~~~~

        class DrawCatOutJig : DrawJig
        {

            List<Entity> Entities = new List<Entity>();
            Point3d CenterPoint = Point3d.Origin;
            public Point3d MyCenterPoint
            {
                get { return CenterPoint; }
            }

            double MyScale = 1;

            public DrawCatOutJig(double Scale)
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

                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.CatOut);
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

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, Type);
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

                Entities.Add(CreateCellEntity(BasePoint, 10, 5, (int)Atend.Control.Enum.ProductType.CatOut));

                Entities.Add(CreateLine(new Point3d(BasePoint.X - 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.CatOut, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.CatOut, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y + 2, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Key, 0, 0));

                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), 0.2));
                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), 0.2));


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

                Entities.Add(CreateCellEntity(BasePoint, 10, 5, (int)Atend.Control.Enum.ProductType.CatOut));

                Entities.Add(CreateLine(new Point3d(BasePoint.X - 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.CatOut, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X + 5, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), (int)Atend.Control.Enum.ProductType.CatOut, 0, 0));
                Entities.Add(CreateLine(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), new Point3d(BasePoint.X + 2, BasePoint.Y + 2, BasePoint.Z), (int)Atend.Control.Enum.ProductType.Key, 0, 0));

                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X - 2, BasePoint.Y, BasePoint.Z), 0.2));
                Entities.Add(CreateConnectionPoint(new Point3d(BasePoint.X + 2, BasePoint.Y, BasePoint.Z), 0.2));


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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Method~~~~~~~~~

        public AcDrawCatOut()
        {
            DKeyStatus = new Atend.Base.Design.DKeyStatus();
        }

        public void DrawCatout()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            System.Data.DataTable ParentList = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.CatOut);
            double V1 = 0;
            double V2 = 0;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.CatOut).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.CatOut).CommentScale;

            try
            {
                PromptEntityOptions peo1 = new PromptEntityOptions("Select first node:");
                PromptEntityResult per = ed.GetEntity(peo1);
                if (per.Status == PromptStatus.OK)
                {
                    Atend.Base.Acad.AT_INFO Info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per.ObjectId);
                    DataRow[] drs = ParentList.Select(string.Format("SoftwareCode={0}", Info1.NodeType));
                    if (drs.Length != 0)
                    {
                        switch (((Atend.Control.Enum.ProductType)drs[0]["SoftwareCode"]))
                        {
                            case Atend.Control.Enum.ProductType.HeaderCabel:
                                //V1 = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(Info1.ProductCode);
                                V1 = 0;
                                break;
                            case Atend.Control.Enum.ProductType.Consol:
                                V1 = Atend.Base.Equipment.EConsol.AccessSelectByCode(Info1.ProductCode).VoltageLevel;
                                break;
                            case Atend.Control.Enum.ProductType.KablSho:
                                //V1 = Atend.Base.Equipment.EKablsho.AccessSelectByCode(Info1.ProductCode);
                                V1 = 0;
                                break;
                            case Atend.Control.Enum.ProductType.Kalamp:
                                V1 = Atend.Base.Equipment.EClamp.AccessSelectByCode(Info1.ProductCode).VoltageLevel;
                                break;
                        }
                        PromptEntityOptions peo2 = new PromptEntityOptions("Select second node:");
                        PromptEntityResult per1 = ed.GetEntity(peo2);
                        if (per1.Status == PromptStatus.OK)
                        {
                            Atend.Base.Acad.AT_INFO Info2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(per1.ObjectId);
                            DataRow[] drs1 = ParentList.Select(string.Format("SoftwareCode={0}", Info2.NodeType));
                            if (drs1.Length != 0)
                            {

                                switch (((Atend.Control.Enum.ProductType)drs1[0]["SoftwareCode"]))
                                {
                                    case Atend.Control.Enum.ProductType.HeaderCabel:
                                        //V1 = Atend.Base.Equipment.EHeaderCabel.AccessSelectByCode(Info1.ProductCode);
                                        V2 = 0;
                                        break;
                                    case Atend.Control.Enum.ProductType.Consol:
                                        V2 = Atend.Base.Equipment.EConsol.AccessSelectByCode(Info2.ProductCode).VoltageLevel;
                                        break;
                                    case Atend.Control.Enum.ProductType.KablSho:
                                        //V1 = Atend.Base.Equipment.EKablsho.AccessSelectByCode(Info1.ProductCode);
                                        V2 = 0;
                                        break;
                                    case Atend.Control.Enum.ProductType.Kalamp:
                                        V2 = Atend.Base.Equipment.EClamp.AccessSelectByCode(Info2.ProductCode).VoltageLevel;
                                        break;
                                }

                                if (V1 == V2 && Info1.ParentCode == Info2.ParentCode)
                                {
                                    //ed.WriteMessage("Voltage was same \n");
                                    DrawCatOutJig _DrawBreakerJig = new DrawCatOutJig(MyScale);
                                    List<Entity> ents = _DrawBreakerJig.GetDemo(
                                        Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(per.ObjectId)),
                                        Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(per1.ObjectId)));
                                    //ed.WriteMessage("COUNT OF ENTITY :{0}\n",ents.Count);
                                    if (SaveCatOutData())
                                    {
                                        //ed.WriteMessage("data saved {0} \n",ents.Count);
                                        ObjectIdCollection OIC = new ObjectIdCollection();
                                        foreach (Entity ent in ents)
                                        {
                                            ObjectId TerminalOI = ObjectId.Null;
                                            ObjectId KetOI = ObjectId.Null;
                                            ObjectId NOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                                            //ed.WriteMessage("111 \n");
                                            Atend.Global.Acad.AcadJigs.MyLine terminal = ent as Atend.Global.Acad.AcadJigs.MyLine;
                                            //ed.WriteMessage("112 \n");
                                            object ProductType = null;
                                            //ed.WriteMessage("changed \n");
                                            if (terminal != null)
                                            {
                                                if (terminal.AdditionalDictionary.TryGetValue("ProductType", out ProductType))
                                                {
                                                    //ed.WriteMessage("113 \n");
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
                                            //ed.WriteMessage("entity added \n");

                                            if (TerminalOI == ObjectId.Null)
                                            {
                                                if (KetOI == ObjectId.Null)
                                                {
                                                    //ed.WriteMessage("KEYOI == null\n");
                                                    Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                                    groupInfo.NodeCode = CatOutPack.Code.ToString();
                                                    groupInfo.ParentCode = "";
                                                    groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.CatOut;
                                                    groupInfo.ProductCode = CatOutPack.ProductCode;
                                                    groupInfo.Insert();
                                                }
                                                else
                                                {
                                                    //ed.WriteMessage("KEYOI != null\n");
                                                    Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                                    groupInfo.NodeCode = CatOutPack.Code.ToString();
                                                    groupInfo.ParentCode = "";
                                                    groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Key;
                                                    groupInfo.ProductCode = CatOutPack.ProductCode;
                                                    groupInfo.Insert();
                                                }
                                            }
                                            else
                                            {
                                                //ed.WriteMessage("terminal != null\n");
                                                Atend.Base.Acad.AT_INFO groupInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                                groupInfo.NodeCode = CatOutPack.Code.ToString();
                                                groupInfo.ParentCode = "";
                                                groupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Terminal;
                                                groupInfo.ProductCode = CatOutPack.ProductCode;
                                                groupInfo.Insert();
                                            }
                                        }

                                        ObjectId GOI = Atend.Global.Acad.Global.MakeGroup(CatOutPack.Code.ToString(), OIC);
                                        //ed.WriteMessage("----<<<<<----GOI:{0}\n", GOI);

                                        Atend.Base.Acad.AT_INFO groupInfo1 = new Atend.Base.Acad.AT_INFO(GOI);
                                        groupInfo1.NodeCode = CatOutPack.Code.ToString();
                                        groupInfo1.ParentCode = "";
                                        groupInfo1.NodeType = (int)Atend.Control.Enum.ProductType.CatOut;
                                        groupInfo1.ProductCode = CatOutPack.ProductCode;
                                        groupInfo1.Insert();


                                        ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(ECatOut.Comment, _DrawBreakerJig.MyCenterPoint, MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                        Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                                        textInfo.NodeCode = "";
                                        textInfo.ParentCode = CatOutPack.Code.ToString();
                                        textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                        textInfo.ProductCode = 0;
                                        textInfo.Insert();
                                        //ed.WriteMessage("text info inserted \n");

                                        Atend.Base.Acad.AT_SUB groupSub = new Atend.Base.Acad.AT_SUB(GOI);
                                        groupSub.SubIdCollection.Add(per.ObjectId);
                                        groupSub.SubIdCollection.Add(per1.ObjectId);
                                        groupSub.SubIdCollection.Add(TextOi);
                                        groupSub.Insert();
                                        //ed.WriteMessage("group info inserted \n");

                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(GOI, per.ObjectId);
                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(GOI, per1.ObjectId);
                                        //ed.WriteMessage("other nodes info inserted \n");

                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("Voltage was not ok \n");
                                }

                            }//if (drs1.Length != 0)
                        }//if (per1.Status == PromptStatus.OK)
                    }//if (drs.Length != 0)
                }//if (per.Status == PromptStatus.OK )
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("DRAW ING FAILED :{0} \n", ex.Message);
            }
        }

        private bool SaveCatOutData()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction aTransaction;

            //ed.WriteMessage("~~~~~~~~~~~~~ Start Catout Save Data ~~~~~~~~~~~~~~\n");
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();

                try
                {
                    if (!UseAccess)
                    {
                        if (!ECatOut.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eCatOut.AccessInsert failed");
                        }
                    }


                    CatOutPack = new Atend.Base.Design.DPackage();
                    CatOutPack.Count = 1;
                    CatOutPack.IsExistance = Existance;
                    CatOutPack.ProductCode = ECatOut.Code;
                    CatOutPack.Type = (int)Atend.Control.Enum.ProductType.CatOut;
                    CatOutPack.ParentCode = Guid.Empty;
                    CatOutPack.NodeCode = Guid.Empty;
                    CatOutPack.LoadCode = 0;
                    CatOutPack.Number = "Catout";
                    CatOutPack.ProjectCode = ProjectCode;
                    if (!CatOutPack.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("dPack.AccessInsert failed");
                    }
                    DKeyStatus = new Atend.Base.Design.DKeyStatus();
                    DKeyStatus.NodeCode = CatOutPack.Code;
                    DKeyStatus.Type = (int)Atend.Control.Enum.ProductType.CatOut;
                    DKeyStatus.IsClosed = false;
                    if (!DKeyStatus.Insert(aTransaction, aConnection))
                    {
                        throw new System.Exception("DKeyStatus.AccessInsert failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveCatOutData 02 :{0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveCatOutData 01 :{0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.CatOutData.UseAccess = true;
            //UseAccess = true;

            #endregion

            //ed.WriteMessage("~~~~~~~~~~~~~ End Catout Save Data ~~~~~~~~~~~~~~\n");
            return true;

        }

        public bool UpdateCatOutData(Guid EXCode)
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
                    CatOutPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                    if (!UseAccess)
                    {
                        if (!ECatOut.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eCatOut.AccessInsert failed");
                        }
                        //if (!Atend.Base.Equipment.EOperation.SentFromLocalToAccess(ECatOut.XCode, (int)Atend.Control.Enum.ProductType.CatOut, ECatOut.Code, aTransaction, aConnection))
                        //{
                        //    throw new System.Exception("operation failed");
                        //}

                    }
                    CatOutPack.IsExistance = Existance;
                    CatOutPack.ProjectCode = ProjectCode;

                    CatOutPack.ProductCode = ECatOut.Code;
                    CatOutPack.Number = "";
                    if (CatOutPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = ECatOut.Code;
                        atinfo.Insert();
                    }
                    else
                    {
                        throw new System.Exception("CatOutPack.AccessInsert2 failed");
                    }

                    if (!DKeyStatus.Update(aTransaction, aConnection))
                    {
                        throw new System.Exception("dkeystatus.Update Failed");
                    }


                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateCatOut 01(transaction) : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateCatOut 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteCatOutData(ObjectId CatOutOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_INFO _info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(CatOutOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(_info.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete CatOut\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(" ERROR CatOut : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteCatOut(ObjectId CatOutOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                ObjectIdCollection CollectionCatOut = Atend.Global.Acad.UAcad.GetGroupSubEntities(Atend.Global.Acad.UAcad.GetEntityGroup(CatOutOI));
                ObjectId oid = ObjectId.Null;
                foreach (ObjectId collect in CollectionCatOut)
                {
                    Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        oid = collect;
                    }
                }

                if (oid == ObjectId.Null)
                    oid = CatOutOI;
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
                            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, h);
                            }
                        }
                    }
                }

                //remove Sub Group
                foreach (ObjectId c2 in CollectionCatOut)
                {
                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(c2))
                    {
                        ed.WriteMessage("Error In Delete CatOut \n");
                    }
                }


            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR CatOut : {0} \n", ex.Message);
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

        public static void AddNewCatout(Point3d StartPoint, Point3d EndPoint, ObjectId GroupOI)
        {
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.CatOut).Scale;
            DrawCatOutJig _DrawCatOutJig = new DrawCatOutJig(MyScale);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectIdCollection OIC = new ObjectIdCollection();
            foreach (Entity ent in _DrawCatOutJig.GetDemoForEdit(StartPoint, EndPoint))
            {
                ObjectId NewOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                OIC.Add(NewOI);
                Atend.Base.Acad.AT_INFO _AT_INFO = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(GroupOI);
                _AT_INFO.SelectedObjectId = NewOI;
                _AT_INFO.Insert();
            }
            Atend.Global.Acad.Global.AddObjectToGroup(GroupOI, OIC);
        }



    }
}
