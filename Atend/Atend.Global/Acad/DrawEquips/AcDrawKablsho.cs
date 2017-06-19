using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

//get from tehran 7/ag
namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawKablsho
    {

        //~~~~~~~~~~~~~~~~~~~~~~~~~~ Properties ~~~~~~~~~~~~~~~~~~~~~~~~~//

        bool _useAccess;
        public bool UseAccess
        {
            get { return _useAccess; }
            set { _useAccess = value; }
        }

        int _existance;
        public int Existance
        {
            get { return _existance; }
            set { _existance = value; }
        }

        Atend.Base.Equipment.EKablsho _eKablsho;
        public Atend.Base.Equipment.EKablsho eKablsho
        {
            get { return _eKablsho; }
            set { _eKablsho = value; }
        }

        int projectCode;
        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        Atend.Base.Design.DPackage KablshoPack;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~ CLASS ~~~~~~~~~~~~~~~~~~~~~~~~~//
        public class DrawKablshoJig : DrawJig
        {

            private List<Entity> Entities = new List<Entity>();
            public Point2d BasePoint = Point2d.Origin;
            private double NewAngle, BaseAngle = 0;
            public bool GetPoint = true;
            public bool GetAngle = false;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            private Autodesk.AutoCAD.GraphicsInterface.TextStyle _style;
            double MyScale = 1;

            public DrawKablshoJig(double Scale)
            {
                MyScale = Scale;

                // SET TEXT STYLE

                _style = new Autodesk.AutoCAD.GraphicsInterface.TextStyle();

                _style.Font = new Autodesk.AutoCAD.GraphicsInterface.FontDescriptor("Calibri", false, true, 0, 0);

                _style.TextSize = 10;

                // END OF SET TEXT STYLE

                //AddRegAppTableRecord(RegAppName);


                //Add Consol



            }

            private Entity CreateKablshoEntity(Point2d BasePoint)
            {

                double BaseX = BasePoint.X;
                double BaseY = BasePoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();

                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.Closed = true;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.KablSho);

                return pLine;

            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                //throw new System.Exception("The method or operation is not implemented.");


                if (GetPoint)
                {
                    JigPromptPointOptions jppo = new JigPromptPointOptions("Select kalamp Position : \n");

                    jppo.UserInputControls = (UserInputControls.Accept3dCoordinates |
                                             UserInputControls.NullResponseAccepted |
                                             UserInputControls.NoNegativeResponseAccepted);

                    PromptPointResult ppr = prompts.AcquirePoint(jppo);

                    if (ppr.Status == PromptStatus.OK)
                    {
                        Point2d NewPoint = new Point2d(ppr.Value.X, ppr.Value.Y);

                        if (NewPoint.IsEqualTo(BasePoint))
                        {

                            //v2d = new Vector2d(0, 0);

                            return SamplerStatus.NoChange;
                        }
                        else
                        {
                            //v2d = BasePoint - NewPoint;

                            BasePoint = new Point2d(ppr.Value.X, ppr.Value.Y);

                            //ed.WriteMessage("POINT OK. \n");

                            return SamplerStatus.OK;
                        }
                    }
                    else
                    {
                        //v2d = new Vector2d(0, 0);

                        return SamplerStatus.Cancel;
                    }

                }
                else if (GetAngle)
                {
                    JigPromptAngleOptions jpao = new JigPromptAngleOptions("Select kalamp Angle : \n");

                    jpao.UseBasePoint = true;

                    jpao.BasePoint = new Point3d(BasePoint.X, BasePoint.Y, 0);

                    PromptDoubleResult pdr = prompts.AcquireAngle(jpao);

                    if (pdr.Status == PromptStatus.OK)
                    {
                        if (pdr.Value == NewAngle)
                        {

                            return SamplerStatus.NoChange;
                        }
                        else
                        {

                            NewAngle = pdr.Value;// -NewAngle;

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

                // SHOW POSITION VALUE

                Autodesk.AutoCAD.GraphicsInterface.WorldGeometry wg2 = draw.Geometry as Autodesk.AutoCAD.GraphicsInterface.WorldGeometry;

                if (wg2 != null)
                {

                    // Push our transforms onto the stack

                    wg2.PushOrientationTransform(Autodesk.AutoCAD.GraphicsInterface.OrientationBehavior.Screen);

                    wg2.PushPositionTransform(Autodesk.AutoCAD.GraphicsInterface.PositionBehavior.Screen, new Point2d(30, 30));

                    // Draw our screen-fixed text

                    wg2.Text(

                        new Point3d(0, 0, 0),  // Position

                        new Vector3d(0, 0, 1), // Normal

                        new Vector3d(1, 0, 0), // Direction

                        BasePoint.ToString() + ":" + NewAngle.ToString() + ":" + BaseAngle.ToString(), // Text

                        true,                  // Rawness

                        _style                // TextStyle

                            );


                    // Remember to pop our transforms off the stack

                    wg2.PopModelTransform();

                    wg2.PopModelTransform();

                }

                // END OF SHOW POSITION VALUE

                if (GetPoint)
                {
                    Entities.Clear();
                    Entities.Add(CreateKablshoEntity(BasePoint));

                    //~~~~~~~~ SCALE ~~~~~~~~~~

                    Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                    foreach (Entity en in Entities)
                    {
                        en.TransformBy(trans1);
                    }

                    //~~~~~~~~~~~~~~~~~~~~~~~~~

                }
                else if (GetAngle)
                {
                    Matrix3d trans = Matrix3d.Rotation(NewAngle - BaseAngle, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis,

                                                       new Point3d(BasePoint.X, BasePoint.Y, 0));


                    foreach (Entity en in Entities)
                    {

                        en.TransformBy(trans);

                    }

                    BaseAngle = NewAngle;
                    //NewAngle = 0;

                }

                //~~~~~~~~ SCALE ~~~~~~~~~~

                //////if (Atend.Control.Common.SelectedDesignScale != 0)
                //////{
                //////    double ScaleValue = 1 / Atend.Control.Common.SelectedDesignScale;
                //////    Matrix3d trans1 = Matrix3d.Scaling(1.50,

                //////                       new Point3d(BasePoint.X, BasePoint.Y, 0));

                //////    foreach (Entity en in Entities)
                //////    {

                //////        en.TransformBy(trans1);

                //////    }
                //////}

                //~~~~~~~~~~~~~~~~~~~~~~~~~


                foreach (Entity en in Entities)
                {

                    draw.Geometry.Draw(en);

                }


                return true;
            }

            public List<Entity> GetEntities()
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

                foreach (Entity ent in Entities)
                {

                    Atend.Global.Acad.AcadJigs.MyPolyLine n = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;

                    //ed.WriteMessage("additiona count : {0}  \n", n.AdditionalDictionary.Count);

                }

                return Entities;
            }


        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~ METHOD ~~~~~~~~~~~~~~~~~~~~~~~~~//

        public static Entity DrawKablsho(Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            double BaseX = CenterPoint.X - 2.5;
            double BaseY = CenterPoint.Y;

            Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.Closed = true;

            return pLine;


        }

        public void DrawKablsho()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            //Database db = HostApplicationServices.WorkingDatabase;
            //Point3d TablePosition;
            //ObjectId NewPoleObjectId = ObjectId.Null;
            //ObjectIdCollection NewConsolObjectIds = new ObjectIdCollection();
            //ed.WriteMessage("~~~Design scale :{0}~~~ \n", Atend.Control.Common.SelectedDesignScale);
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.KablSho).Scale;

            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                bool conti = true;
                int i = 0;
                DrawKablshoJig _drawKalamp = new DrawKablshoJig(MyScale);
                ObjectId ParentOI = ObjectId.Null;
                string ParentCode = string.Empty;

                while (conti)
                {

                    PromptResult pr;

                    pr = ed.Drag(_drawKalamp);

                    if (pr.Status == PromptStatus.OK && !_drawKalamp.GetAngle)
                    {

                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(new Point3d(_drawKalamp.BasePoint.X, _drawKalamp.BasePoint.Y, 0));
                        System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.KablSho);
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                ParentOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));
                                ParentCode = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI).NodeCode;
                                //ed.WriteMessage("Value assigned $$$$$$$$$$$$$$$$$\n");
                            }
                        }


                        _drawKalamp.GetPoint = false;
                        _drawKalamp.GetAngle = true;

                    }
                    else if (pr.Status == PromptStatus.OK && _drawKalamp.GetAngle)
                    {

                        conti = false;
                        List<Entity> entities = _drawKalamp.GetEntities();


                        if (SaveKablshoData())
                        {

                            foreach (Entity ent in entities)
                            {

                                ObjectId HOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                Atend.Base.Acad.AT_INFO headerInfo = new Atend.Base.Acad.AT_INFO(HOI);
                                headerInfo.NodeCode = KablshoPack.Code.ToString();
                                headerInfo.NodeType = KablshoPack.Type;
                                headerInfo.ProductCode = eKablsho.Code;
                                if (ParentOI != ObjectId.Null)
                                {
                                    headerInfo.ParentCode = ParentCode;
                                }
                                else
                                {
                                    headerInfo.ParentCode = "";
                                }
                                headerInfo.Insert();

                                if (ParentOI != ObjectId.Null)
                                {
                                    Atend.Base.Acad.AT_SUB KablshoSub = new Atend.Base.Acad.AT_SUB(HOI);
                                    KablshoSub.SubIdCollection.Add(ParentOI);
                                    KablshoSub.Insert();

                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(HOI, ParentOI);
                                }
                            }

                        }
                    }
                    else
                    {
                        conti = false;
                    }
                }


            }

        }

        public ObjectId DrawKablsho02(Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("CurrentPoint:{0} \n", CenterPoint);
            ObjectId HeaderOI = ObjectId.Null;


            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.KablSho).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.KablSho).CommentScale;
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.KablSho);
            System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CenterPoint);
            ObjectId ParentOI = ObjectId.Null;
            foreach (System.Data.DataRow dr in PointContainerList.Rows)
            {
                DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                if (drs.Length != 0)
                {
                    ParentOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));
                }
            }

            double BaseX = CenterPoint.X - 2.5;
            double BaseY = CenterPoint.Y;

            Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
            //pLine.ColorIndex = 40;
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.Closed = true;

            Matrix3d trans1 = Matrix3d.Scaling(MyScale, CenterPoint);
            pLine.TransformBy(trans1);

            if (SaveKablshoData())
            {

                string LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString();
                HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(pLine, LayerName);
                //ed.WriteMessage("HeaderOI : {0} \n", HeaderOI);
                Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO(HeaderOI);
                if (ParentOI != ObjectId.Null)
                {
                    Atend.Base.Acad.AT_INFO parentInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI);
                    //ed.WriteMessage("parentInfo.NodeCode:{0} \n", parentInfo.NodeCode);
                    at_info.ParentCode = parentInfo.NodeCode;
                }
                else
                {
                    at_info.ParentCode = "";
                }
                //ed.WriteMessage("HeaderPack.Code:{0} \n", HeaderPack.Code);
                at_info.NodeCode = KablshoPack.Code.ToString();
                at_info.NodeType = (int)Atend.Control.Enum.ProductType.KablSho;
                at_info.ProductCode = eKablsho.Code;
                at_info.Insert();


                ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(KablshoPack.Number, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(HeaderOI)), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());


                Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                textInfo.ParentCode = KablshoPack.Code.ToString();
                textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                textInfo.NodeCode = "";
                textInfo.ProductCode = 0;
                textInfo.Insert();

                Atend.Base.Acad.AT_SUB ConnectionPSub = new Atend.Base.Acad.AT_SUB(HeaderOI);
                ConnectionPSub.SubIdCollection.Add(TextOi);
                if (ParentOI != ObjectId.Null)
                {
                    ConnectionPSub.SubIdCollection.Add(ParentOI);
                }
                ConnectionPSub.Insert();

                if (ParentOI != ObjectId.Null)
                {
                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderOI, ParentOI);
                }

            }

            //ed.WriteMessage("return from draw header cable \n");
            return HeaderOI;


        }

        //MOUSAVI
        private bool SaveKablshoData()
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
                        if (!eKablsho.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("EKablsho.AccessInsert failed");
                        }
                    }

                    KablshoPack = new Atend.Base.Design.DPackage();
                    KablshoPack.Count = 1;
                    KablshoPack.IsExistance = Existance;
                    KablshoPack.ProductCode = eKablsho.Code;
                    KablshoPack.ProjectCode = ProjectCode;
                    KablshoPack.LoadCode = 0;
                    KablshoPack.Type = (int)Atend.Control.Enum.ProductType.KablSho;
                    Atend.Control.Common.Counters.KablshoCounter++;
                    KablshoPack.Number = string.Format("Kablsho{0}", Atend.Control.Common.Counters.KablshoCounter);
                    if (!KablshoPack.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("KablshoPack.AccessInsert failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveKablshoData 01 : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveKablshoData 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess = true;
            UseAccess = true;

            #endregion

            return true;

        }

        public bool SaveKablshoDataForExternalCall(Atend.Base.Acad.AT_INFO NodeInformation)
        {
            if (!SaveKablshoData())
            {
                return false;
            }
            else
            {
                NodeInformation.ProductCode = KablshoPack.ProductCode;
                NodeInformation.NodeCode = KablshoPack.Code.ToString();
                NodeInformation.Insert();
            }
            return true;
        }

        public bool UpdateKablshoData(Guid EXCode)
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
                    KablshoPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                    if (!UseAccess)
                    {
                        if (!eKablsho.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eKablsho.AccessInsert failed");
                        }
                    }
                    KablshoPack.IsExistance = Existance;
                    KablshoPack.ProductCode = eKablsho.Code;
                    KablshoPack.ProjectCode = ProjectCode;
                    KablshoPack.Number = "";
                    if (KablshoPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);// id);
                        atinfo.ProductCode = eKablsho.Code;
                        atinfo.Insert();
                    }
                    else
                    {
                        throw new System.Exception("KablshoPack.AccessInsert2 failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateKablsho 01(transaction) : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateKablsho 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteKablshoData(ObjectId KablshoOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB SubGP = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(KablshoOI);
                foreach (ObjectId oi in SubGP.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
                    {
                        if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(atinfo.NodeCode.ToString())))
                        {
                            throw new System.Exception("Error In Delete dbranch\n");
                        }
                    }
                    if (atinfo.ParentCode != "NONE" && (atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
                    {
                        if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(atinfo.NodeCode.ToString())))
                        {
                            throw new System.Exception("Error In Delete DPackage\n");
                        }
                    }
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(atinfo.NodeCode.ToString())))
                        {
                            throw new System.Exception("Error In Delete dbranch\n");
                        }
                    }
                }

                Atend.Base.Acad.AT_INFO Kablshoinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(KablshoOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(Kablshoinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Kablsho : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteKablshoData(ObjectId KablshoOI, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB SubGP = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(KablshoOI);
                foreach (ObjectId oi in SubGP.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
                    {
                        if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(atinfo.NodeCode.ToString()), _Transaction, _Connection))
                        {
                            throw new System.Exception("Error In Delete dbranch\n");
                        }
                    }
                    if (atinfo.ParentCode != "NONE" && (atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Breaker || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.CatOut))
                    {
                        if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(atinfo.NodeCode.ToString()), _Transaction, _Connection))
                        {
                            throw new System.Exception("Error In Delete DPackage\n");
                        }
                    }
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(atinfo.NodeCode.ToString()), _Transaction, _Connection))
                        {
                            throw new System.Exception("Error In Delete dbranch\n");
                        }
                    }
                }

                Atend.Base.Acad.AT_INFO Kablshoinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(KablshoOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(Kablshoinfo.NodeCode.ToString()), _Transaction, _Connection))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Kablsho : {0} \n", ex.Message);
                _Transaction.Rollback();
                return false;
            }
            return true;
        }

        public static bool DeleteKablsho(ObjectId KablshoOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(KablshoOI);
                foreach (ObjectId objsub in sub.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
                    {
                        //Move Comment 
                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
                        foreach (ObjectId collect in subBranch.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                            {
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete Cabel Comment\n");
                                }
                            }
                            //Delete From AT_SUB other kablsho or headercabel
                            if (atinfo.ParentCode != "NONE" && (atinfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel) && collect != KablshoOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(objsub, collect);
                            }
                        }

                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                        {
                            throw new System.Exception("Error In Delete Cabel\n");
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Conductor)
                    {
                        //Move Comment 
                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
                        foreach (ObjectId collect in subBranch.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                            {
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete Conductor Comment\n");
                                }
                            }
                            //Delete From AT_SUB other kablsho
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho && collect != KablshoOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(objsub, collect);
                            }
                        }

                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                        {
                            throw new System.Exception("Error In Delete Conductor\n");
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        // Comment 
                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
                        foreach (ObjectId collect in subBranch.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);

                            //Delete From AT_SUB other kablsho
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho && collect != KablshoOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(objsub, collect);
                            }
                        }

                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                        {
                            throw new System.Exception("Error In Delete Termianl\n");
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Breaker)
                    {
                        ObjectIdCollection CollectionBreaker = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
                        foreach (ObjectId collect in CollectionBreaker)
                        {
                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                            {
                                if (!Atend.Global.Acad.DrawEquips.AcDrawBreaker.DeleteBreaker(collect))
                                {
                                    throw new System.Exception("Error In Delete Breaker\n");
                                }
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete Breaker2\n");
                                }
                            }
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Disconnector)
                    {
                        ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
                        foreach (ObjectId collect in CollectionDisconnector)
                        {
                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                            {
                                if (!Atend.Global.Acad.DrawEquips.AcDrawDisConnector.DeleteDisconnector(collect))
                                {
                                    throw new System.Exception("Error In Delete Disconnector\n");
                                }
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete Disconnector2\n");
                                }
                            }
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.CatOut)
                    {
                        ObjectIdCollection CollectionDisconnector = Atend.Global.Acad.UAcad.GetGroupSubEntities(objsub);
                        foreach (ObjectId collect in CollectionDisconnector)
                        {
                            Atend.Base.Acad.AT_INFO at_infoo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            if (at_infoo.ParentCode != "NONE" && at_infoo.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                            {
                                if (!Atend.Global.Acad.DrawEquips.AcDrawCatOut.DeleteCatOut(collect))
                                {
                                    throw new System.Exception("Error In Delete CatOut\n");
                                }
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                                {
                                    throw new System.Exception("Error In Delete CatOut2\n");
                                }
                            }
                        }
                    }
                    if (at_info1.ParentCode != "NONE" && (at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Pole || at_info1.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(KablshoOI, objsub);
                    }
                }

                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(KablshoOI))
                {
                    throw new System.Exception("GRA while delete KablSho \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR KablSho : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static void RotateKablsho(double LastAngleDegree, double NewAngleDegree, ObjectId PoleOI, ObjectId KablshoOI, Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                Atend.Global.Acad.AcadMove.LastCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(KablshoOI));
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    Entity ent = (Entity)tr.GetObject(KablshoOI, OpenMode.ForWrite);
                    Polyline LineEntity = ent as Polyline;
                    if (LineEntity != null)
                    {
                        //ed.WriteMessage("LineEntity entity found \n");
                        Matrix3d trans = Matrix3d.Rotation(((LastAngleDegree * Math.PI) / 180) * -1, ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                        LineEntity.TransformBy(trans);

                        trans = Matrix3d.Rotation(((NewAngleDegree * Math.PI) / 180), ed.CurrentUserCoordinateSystem.CoordinateSystem3d.Zaxis, CenterPoint);
                        LineEntity.TransformBy(trans);

                        //Matrix3d m = new Matrix3d();
                        //Atend.Acad.AcadMove.ConsolOI = ConsolOI;
                        //Atend.Acad.AcadMove.isMoveConsolOnly = true;
                        Atend.Global.Acad.AcadMove.AllowToMove = true;
                        Atend.Global.Acad.AcadMove.MoveHeaderCabelANDKablSho(KablshoOI);

                    }
                    tr.Commit();
                }
            }
        }

    }
}