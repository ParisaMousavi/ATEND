using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

//get from tehran 7/15
namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawHeaderCabel
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

        int _projectCode;
        public int ProjectCode
        {
            get { return _projectCode; }
            set { _projectCode = value; }
        }

        Atend.Base.Equipment.EHeaderCabel _eHeaderCabel;
        public Atend.Base.Equipment.EHeaderCabel eHeaderCabel
        {
            get { return _eHeaderCabel; }
            set { _eHeaderCabel = value; }
        }

        Atend.Base.Design.DPackage HeaderPack;

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~ CLASS ~~~~~~~~~~~~~~~~~~~~~~~~~//
        public class DrawHeaderCableJig : DrawJig
        {

            private List<Entity> Entities = new List<Entity>();
            public Point2d BasePoint = Point2d.Origin;
            private double NewAngle, BaseAngle = 0;
            public bool GetPoint = false;
            public bool GetAngle = true;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            private Autodesk.AutoCAD.GraphicsInterface.TextStyle _style;
            double MyScale = 1;


            public DrawHeaderCableJig(double Scale)
            {

                MyScale = Scale;
                // SET TEXT STYLE

                _style = new Autodesk.AutoCAD.GraphicsInterface.TextStyle();

                _style.Font = new Autodesk.AutoCAD.GraphicsInterface.FontDescriptor("Calibri", false, true, 0, 0);

                _style.TextSize = 10;

                // END OF SET TEXT STYLE

                Entities.Clear();
                Entities.Add(CreateKalampEntity(BasePoint));

                //~~~~~~~~ SCALE ~~~~~~~~~~

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans1);
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~
            }

            public void MakeEntityReady()
            {

                Entities.Clear();
                Entities.Add(CreateKalampEntity(BasePoint));

                //~~~~~~~~ SCALE ~~~~~~~~~~

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans1);
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~
            }

            private Entity CreateKalampEntity(Point2d BasePoint)
            {

                double BaseX = BasePoint.X;
                double BaseY = BasePoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                pLine.ColorIndex = 40;
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.Closed = true;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.Kalamp);

                return pLine;

            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                //throw new System.Exception("The method or operation is not implemented.");


                if (GetPoint)
                {
                    JigPromptPointOptions jppo = new JigPromptPointOptions("Select headerCabel Position :");

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
                    JigPromptAngleOptions jpao = new JigPromptAngleOptions("Select Header Cabel Angle :");

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
                    Entities.Add(CreateKalampEntity(BasePoint));

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

                    //Entities.Clear();
                    //Entities.Add(CreateKalampEntity(BasePoint));

                    ////~~~~~~~~ SCALE ~~~~~~~~~~

                    //Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                    //foreach (Entity en in Entities)
                    //{
                    //    en.TransformBy(trans1);
                    //}

                    //~~~~~~~~~~~~~~~~~~~~~~~~~

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

        public AcDrawHeaderCabel()
        {
            HeaderPack = new Atend.Base.Design.DPackage();
        }

        public void DrawHeaderCabel()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            //Database db = HostApplicationServices.WorkingDatabase;
            //Point3d TablePosition;
            //ObjectId NewPoleObjectId = ObjectId.Null;
            //ObjectIdCollection NewConsolObjectIds = new ObjectIdCollection();
            //ed.WriteMessage("~~~Design scale :{0}~~~ \n", Atend.Control.Common.SelectedDesignScale);
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).CommentScale;

            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                bool conti = true;
                int i = 0;

                PromptPointOptions ppo = new PromptPointOptions("Select HeaderCable Point");
                PromptPointResult ppr = ed.GetPoint(ppo);
                if (ppr.Status == PromptStatus.OK)
                {
                    ObjectId ParentOI = ObjectId.Null;
                    string ParentCode = string.Empty;
                    System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(ppr.Value);
                    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.HeaderCabel);
                    foreach (System.Data.DataRow dr in PointContainerList.Rows)
                    {
                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                        if (drs.Length != 0)
                        {
                            ParentOI = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));
                            ParentCode = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI).NodeCode;
                        }
                    }
                    DrawHeaderCableJig _drawKalamp = new DrawHeaderCableJig(MyScale);
                    while (conti)
                    {
                        PromptResult pr;
                        _drawKalamp.BasePoint = new Point2d(ppr.Value.X, ppr.Value.Y);
                        _drawKalamp.GetPoint = false;
                        _drawKalamp.GetAngle = true;
                        _drawKalamp.MakeEntityReady();
                        pr = ed.Drag(_drawKalamp);
                        if (pr.Status == PromptStatus.OK && !_drawKalamp.GetAngle)
                        {
                            //_drawKalamp.GetPoint = false;
                            //_drawKalamp.GetAngle = true;
                        }
                        else if (pr.Status == PromptStatus.OK && _drawKalamp.GetAngle)
                        {
                            conti = false;
                            List<Entity> entities = _drawKalamp.GetEntities();
                            if (SaveHeaderCableData())
                            {
                                foreach (Entity ent in entities)
                                {
                                    ObjectId HOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                    Atend.Base.Acad.AT_INFO headerInfo = new Atend.Base.Acad.AT_INFO(HOI);
                                    headerInfo.NodeCode = HeaderPack.Code.ToString();
                                    headerInfo.NodeType = HeaderPack.Type;
                                    headerInfo.ProductCode = eHeaderCabel.Code;
                                    headerInfo.ParentCode = ParentCode;
                                    headerInfo.Insert();


                                    ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(HeaderPack.Number, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(HOI)), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());


                                    Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                                    textInfo.ParentCode = HeaderPack.Code.ToString();
                                    textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                    textInfo.NodeCode = "";
                                    textInfo.ProductCode = 0;
                                    textInfo.Insert();

                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(TextOi, HOI);

                                    if (ParentOI != ObjectId.Null)
                                    {
                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(ParentOI, HOI);
                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(HOI, ParentOI);
                                    }
                                }
                            }
                        }
                        else
                        {
                            conti = false;
                        }
                    }// end of while
                }// end of if
            }

        }

        public ObjectId DrawHeaderCabel(Point3d CenterPoint, ObjectId ParentOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectId HeaderOI = ObjectId.Null;


            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).CommentScale;


            double BaseX = CenterPoint.X - 2.5;
            double BaseY = CenterPoint.Y;

            Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
            pLine.ColorIndex = 40;
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.Closed = true;

            //ed.WriteMessage("\n$$$$$scale {0} \n", MyScale);
            Matrix3d trans1 = Matrix3d.Scaling(MyScale, CenterPoint);
            pLine.TransformBy(trans1);


            if (ParentOI != ObjectId.Null)
            {
                Atend.Base.Acad.AT_INFO ParentInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI);
                HeaderPack.ParentCode = Atend.Base.Design.DPackage.AccessSelectByNodeCode(new Guid(ParentInfo.NodeCode)).Code;
                //ed.WriteMessage("parent code :{0}", ParentInfo.NodeCode);
            }

            if (SaveHeaderCableData())
            {

                string LayerName = "";
                if (eHeaderCabel.Type == 1)
                {
                    LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
                }
                else
                {
                    LayerName = Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString();
                }


                HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(pLine, LayerName);

                Atend.Base.Acad.AT_INFO at_info = new Atend.Base.Acad.AT_INFO(HeaderOI);
                //ed.WriteMessage("ParentCode For connection Point: {0}\n", ParentInfo.NodeCode);
                if (ParentOI != ObjectId.Null)
                {
                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderOI, ParentOI);

                    Atend.Base.Acad.AT_INFO ParentInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ParentOI);
                    at_info.ParentCode = ParentInfo.NodeCode;
                }
                else
                {
                    at_info.ParentCode = "";
                }
                at_info.NodeCode = HeaderPack.Code.ToString();
                at_info.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
                at_info.ProductCode = eHeaderCabel.Code;
                at_info.Insert();


                ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(HeaderPack.Number, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(HeaderOI)), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());


                Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                textInfo.ParentCode = HeaderPack.Code.ToString();
                textInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                textInfo.NodeCode = "";
                textInfo.ProductCode = 0;
                textInfo.Insert();

                Atend.Base.Acad.AT_SUB.AddToAT_SUB(TextOi, HeaderOI);


                Atend.Base.Acad.AT_SUB ConnectionPSub = new Atend.Base.Acad.AT_SUB(HeaderOI);
                ConnectionPSub.SubIdCollection.Add(ParentOI);
                ConnectionPSub.Insert();

            }

            //ed.WriteMessage("return from draw header cable \n");
            return HeaderOI;


        }

        public ObjectId DrawHeaderCabel(Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("CurrentPoint:{0} \n", CenterPoint);
            ObjectId HeaderOI = ObjectId.Null;


            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.HeaderCabel).CommentScale;
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.HeaderCabel);
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
            pLine.ColorIndex = 40;
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
            pLine.Closed = true;

            Matrix3d trans1 = Matrix3d.Scaling(MyScale, CenterPoint);
            pLine.TransformBy(trans1);

            if (SaveHeaderCableData())
            {

                string LayerName = "";
                if (eHeaderCabel.Type == 1)
                {
                    LayerName = Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString();
                }
                else
                {
                    LayerName = Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString();
                }


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
                at_info.NodeCode = HeaderPack.Code.ToString();
                at_info.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
                at_info.ProductCode = eHeaderCabel.Code;
                at_info.Insert();


                ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(HeaderPack.Number, Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(HeaderOI)), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());


                Atend.Base.Acad.AT_INFO textInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                textInfo.ParentCode = HeaderPack.Code.ToString();
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

        public static void RotateHeaderCable(double LastAngleDegree, double NewAngleDegree, ObjectId PoleOI, ObjectId HeaderCableOI, Point3d CenterPoint)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                Atend.Global.Acad.AcadMove.LastCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(HeaderCableOI));
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    Entity ent = (Entity)tr.GetObject(HeaderCableOI, OpenMode.ForWrite);
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
                        Atend.Global.Acad.AcadMove.MoveHeaderCabelANDKablSho(HeaderCableOI);
                    }
                    tr.Commit();
                }
            }
        }

        private bool SaveHeaderCableData()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction aTransaction;
            //ed.WriteMessage("save header cable data \n");
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();

                try
                {
                    if (!UseAccess)
                    {
                        {
                            if (!eHeaderCabel.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("eHeaderCabel.AccessInsert failed");
                            }
                        }
                    }
                    HeaderPack.Count = 1;
                    HeaderPack.IsExistance = Existance;
                    HeaderPack.ProductCode = eHeaderCabel.Code;
                    HeaderPack.Type = (int)Atend.Control.Enum.ProductType.HeaderCabel;
                    HeaderPack.ProjectCode = ProjectCode;
                    Atend.Control.Common.Counters.HeadercableCounter++;
                    HeaderPack.Number = string.Format("Header{0}", Atend.Control.Common.Counters.HeadercableCounter);
                    //ed.WriteMessage("HeaderPack.Number:{0} \n", HeaderPack.Number);
                    if (!HeaderPack.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("HeaderPack.AccessInsert failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveHeaderCableData 02 :{0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveHeaderCableData 01 :{0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess = true;
            //UseAccess = true;

            #endregion

            return true;

        }

        public bool SaveHeaderCableDataForExternalCall(Atend.Base.Acad.AT_INFO NodeInformation)
        {
            if (!SaveHeaderCableData())
            {
                return false;
            }
            else
            {
                NodeInformation.ProductCode = HeaderPack.ProductCode;
                NodeInformation.NodeCode = HeaderPack.Code.ToString();
                NodeInformation.Insert();
            }
            return true;
        }

        public bool UpdateHeaderCabelData(Guid NodeCode)
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
                    HeaderPack = Atend.Base.Design.DPackage.AccessSelectByCode(NodeCode);
                    //ed.WriteMessage("NodeCode={0}\n", NodeCode);
                    if (!UseAccess)
                    {
                        if (!eHeaderCabel.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eHeaderCabel.AccessInsert failed");
                        }
                    }
                    HeaderPack.IsExistance = Existance;
                    HeaderPack.ProjectCode = ProjectCode;
                    HeaderPack.ProductCode = eHeaderCabel.Code;
                    if (HeaderPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = eHeaderCabel.Code;
                        atinfo.Insert();
                    }
                    else
                    {
                        throw new System.Exception("HeaderCabel.AccessInsert2 failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateHeaderCabelData 01 : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateHeaderCabelData 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }
            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteHeaderCabelData(ObjectId HeaderCabelOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB SubGP = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(HeaderCabelOI);
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

                Atend.Base.Acad.AT_INFO HeaderCabelinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(HeaderCabelOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(HeaderCabelinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR HeaderCabel : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteHeaderCabelData(ObjectId HeaderCabelOI, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB SubGP = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(HeaderCabelOI);
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

                Atend.Base.Acad.AT_INFO HeaderCabelinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(HeaderCabelOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(HeaderCabelinfo.NodeCode.ToString()), _Transaction, _Connection))
                {
                    throw new System.Exception("Error In Delete DPackage\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR HeaderCabel : {0} \n", ex.Message);
                _Transaction.Rollback();
                return false;
            }
            return true;
        }

        public static bool DeleteHeaderCabel(ObjectId HeaderCabelOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(HeaderCabelOI);
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
                            //Delete From AT_SUB other headercabel or kablSho
                            if (atinfo.ParentCode != "NONE" && (atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || atinfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho) && collect != HeaderCabelOI)
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
                        // Comment 
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
                            //Delete From AT_SUB other headercabel
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && collect != HeaderCabelOI)
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
                        Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(HeaderCabelOI, objsub);
                    }
                    if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.Terminal)
                    {
                        Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
                        foreach (ObjectId collect in subBranch.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                            //Delete From AT_SUB other headercabel
                            if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && collect != HeaderCabelOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(objsub, collect);
                            }
                        }
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                        {
                            throw new System.Exception("Error In Delete Terminal\n");
                        }
                    }
                }

                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(HeaderCabelOI))
                {
                    throw new System.Exception("GRA while delete HeaderCabel \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR HeaderCabel : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static void DrawShield()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Dictionary<string, Point2dCollection> MyDic = new Dictionary<string, Point2dCollection>();
            Dictionary<string, ObjectId> MyDic1 = new Dictionary<string, ObjectId>();
            try
            {
                TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.LayerName, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString()) };
                SelectionFilter sf = new SelectionFilter(tvs);
                PromptSelectionResult psr = ed.SelectAll(sf);
                if (psr.Value != null)
                {
                    ObjectId[] ids = psr.Value.GetObjectIds();

                    foreach (ObjectId oi in ids)
                    {
                        Atend.Base.Acad.AT_INFO PostInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        if (PostInfo.ParentCode != "NONE" && PostInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                        {
                            Entity ent = Atend.Global.Acad.UAcad.GetEntityByObjectID(oi);
                            Polyline pl = ent as Polyline;
                            Point2dCollection pts = new Point2dCollection(); //p2;
                            if (pl != null)
                            {
                                if (pl.NumberOfVertices == 4)
                                {
                                    pts = new Point2dCollection(4);
                                    for (int i = 0; i < pl.NumberOfVertices; i++)
                                    {
                                        Point2d p = pl.GetPoint2dAt(i);
                                        double a = p.X * 1;
                                        double b = p.Y * 1;
                                        pts.Add(new Point2d(a, b));
                                    }
                                    MyDic.Add(PostInfo.NodeCode, pts);
                                    MyDic1.Add(PostInfo.NodeCode, oi);

                                }
                            }
                        }
                    }
                    ids = null;

                    foreach (string a in MyDic.Keys)
                    {
                        Point2dCollection p = new Point2dCollection();
                        MyDic.TryGetValue(a, out p);
                        Atend.Global.Acad.Global.CreateWhiteBack(p);
                    }

                    foreach (string NodeCode in MyDic1.Keys)
                    {
                        ObjectId PoleOI = ObjectId.Null;

                        if (MyDic1.TryGetValue(NodeCode, out PoleOI))
                        {
                            //ed.WriteMessage("NodeCode:{0}\n", NodeCode);
                            Atend.Base.Design.DPackage PolePack = Atend.Base.Design.DPackage.AccessSelectByCode(new Guid(NodeCode));
                            if (PolePack.Code != Guid.Empty)
                            {
                                Atend.Base.Base.BEquipStatus ES = Atend.Base.Base.BEquipStatus.SelectByACode(PolePack.IsExistance);
                                if (ES.Name.IndexOf("موجود") != -1)
                                {
                                    //ed.WriteMessage("mojood : {0} \n", PoleOI);
                                    //ShieldForPole(PoleOI);
                                    ShieldHeader(PoleOI, true);
                                }
                            }
                            else
                            {
                                Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                notification.Title = "بروز خطا";
                                notification.Msg = "اطلاعات یکی از سرکابل ها در پایگاه داده یافت نشد";
                                notification.infoCenterBalloon();
                                throw new System.Exception("pole was not in DPackage Post");
                            }
                        }
                    }


                    // ed.WriteMessage("Header finished \n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR IN Header :{0} \n", ex.Message);
            }
        }

        private static void ShieldHeader(ObjectId CurrentHeaderOI, bool IsExist)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            //ed.WriteMessage("SHIELD \n");
            try
            {

                Transaction tr = doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    // Check the entity is a closed curve
                    DBObject obj = tr.GetObject(CurrentHeaderOI, OpenMode.ForRead);
                    Curve cur = obj as Curve;
                    if (cur != null && cur.Closed == false)
                    {
                        //ed.WriteMessage("\nLoop must be a closed curve.");
                    }
                    else
                    {
                        if (IsExist)
                        {
                            // We'll add the hatch to the model space
                            BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            Hatch hat = new Hatch();
                            hat.SetDatabaseDefaults();
                            // Firstly make it clear we want a gradient fill
                            hat.HatchObjectType = HatchObjectType.GradientObject;
                            hat.LayerId = Atend.Global.Acad.UAcad.GetLayerById(Atend.Control.Enum.AutoCadLayerName.SHELL.ToString());
                            //Let's use the pre-defined spherical gradient
                            //LINEAR, CYLINDER, INVCYLINDER, SPHERICAL, INVSPHERICAL, HEMISPHERICAL, INVHEMISPHERICAL, CURVED, and INVCURVED. 
                            hat.SetGradient(GradientPatternType.PreDefinedGradient, "LINEAR");
                            // We're defining two colours
                            hat.GradientOneColorMode = false;
                            GradientColor[] gcs = new GradientColor[2];
                            // First colour must have value of 0
                            gcs[0] = new GradientColor(Color.FromRgb(0, 0, 0), 0);
                            // Second colour must have value of 1
                            gcs[1] = new GradientColor(Color.FromRgb(0, 0, 0), 1);
                            hat.SetGradientColors(gcs);
                            // Add the hatch to the model space
                            // and the transaction
                            ObjectId hatId = btr.AppendEntity(hat);
                            tr.AddNewlyCreatedDBObject(hat, true);
                            // Add the hatch loop and complete the hatch
                            ObjectIdCollection ids = new ObjectIdCollection();
                            ids.Add(obj.ObjectId);
                            hat.Associative = true;
                            hat.AppendLoop(HatchLoopTypes.Default, ids);
                            hat.EvaluateHatch(true);
                            tr.Commit();
                        }
                        else
                        {
                            // We'll add the hatch to the model space
                            BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            Hatch hat = new Hatch();
                            hat.SetDatabaseDefaults();
                            // Firstly make it clear we want a gradient fill
                            hat.HatchObjectType = HatchObjectType.GradientObject;
                            hat.LayerId = Atend.Global.Acad.UAcad.GetLayerById(Atend.Control.Enum.AutoCadLayerName.SHELL.ToString());
                            //Let's use the pre-defined spherical gradient
                            //LINEAR, CYLINDER, INVCYLINDER, SPHERICAL, INVSPHERICAL, HEMISPHERICAL, INVHEMISPHERICAL, CURVED, and INVCURVED. 
                            hat.SetGradient(GradientPatternType.PreDefinedGradient, "LINEAR");
                            // We're defining two colours
                            hat.GradientOneColorMode = false;
                            GradientColor[] gcs = new GradientColor[2];
                            // First colour must have value of 0
                            gcs[0] = new GradientColor(Color.FromRgb(255, 255, 255), 0);
                            // Second colour must have value of 1
                            gcs[1] = new GradientColor(Color.FromRgb(255, 255, 255), 1);
                            hat.SetGradientColors(gcs);
                            // Add the hatch to the model space
                            // and the transaction
                            ObjectId hatId = btr.AppendEntity(hat);
                            tr.AddNewlyCreatedDBObject(hat, true);
                            // Add the hatch loop and complete the hatch
                            ObjectIdCollection ids = new ObjectIdCollection();
                            ids.Add(obj.ObjectId);
                            hat.Associative = true;
                            hat.AppendLoop(HatchLoopTypes.Default, ids);
                            hat.EvaluateHatch(true);
                            tr.Commit();

                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR IN POLE shield:{0} \n", ex.Message);
            }
        }

    }
}
