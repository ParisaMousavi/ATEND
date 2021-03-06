using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;
using System.Data.OleDb;

namespace Atend.Global.Acad.DrawEquips
{

    public class AcDrawStreetBox
    {
        //~~~~~~~~~~~~~~~~~~~~~Properties~~~~~~~~~~~~~~//
        bool _UseAccess;
        public bool UseAccess
        {
            get { return _UseAccess; }
            set { _UseAccess = value; }
        }

        private List<Atend.Base.Equipment.EStreetBoxPhuse> _eStreetBoxPhuse;
        public List<Atend.Base.Equipment.EStreetBoxPhuse> eStreetBoxPhuse
        {
            get { return _eStreetBoxPhuse; }
            set { _eStreetBoxPhuse = value; }
        }

        private Atend.Base.Equipment.EStreetBox _eStreetBox;
        public Atend.Base.Equipment.EStreetBox eStreetBox
        {
            get { return _eStreetBox; }
            set { _eStreetBox = value; }
        }

        int _Existance;
        public int Existance
        {
            get { return _Existance; }
            set { _Existance = value; }
        }

        int _projectCode;
        public int ProjectCode
        {
            get { return _projectCode; }
            set { _projectCode = value; }
        }

        Atend.Base.Design.DPackage StreetBoxPack = new Atend.Base.Design.DPackage();

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }



        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~CLass~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public class DrawStreetBoxJig : DrawJig
        {

            Point3d BasePoint = Point3d.Origin;
            public Point3d MyBasePoint
            {
                get { return BasePoint; }
            }

            List<Entity> Entities = new List<Entity>();
            int _feederCount = 0;
            double MyScale = 1;

            public DrawStreetBoxJig(int FeederCount, double Scale)
            {
                MyScale = Scale;
                _feederCount = FeederCount;
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

                mLine.ColorIndex = ColorIndex;

                return mLine;
            }

            private Entity CreateHeaderCable(Point3dCollection P3C)
            {

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                pLine.ColorIndex = 40;
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[0].X, P3C[0].Y), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[1].X, P3C[1].Y), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[2].X, P3C[2].Y), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[0].X, P3C[0].Y), 0, 0, 0);

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.HeaderCabel);
                pLine.Closed = true;

                return pLine;

            }

            private Entity CreateStreetBoxBox(Point3d CenterPoint, double Width, double Height)
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


                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.StreetBox);
                return pLine;

            }

            private void CreateFeeder(Point3d CenterPoint)
            {
                double BaseX = CenterPoint.X;
                double BaseY = CenterPoint.Y;

                //bus
                Entities.Add(CreateLine(
                    new Point3d(BaseX - 8, BaseY, 0),
                    new Point3d(BaseX + 8, BaseY, 0),
                    (int)Atend.Control.Enum.ProductType.Bus,
                    190,
                    0));

                //Additional
                Entities.Add(CreateLine(
                    new Point3d(BaseX, BaseY, 0),
                    new Point3d(BaseX, BaseY - 18, 0),
                    0,
                    0,
                    0));


                Entities.Add(CreateLine(
    new Point3d(BaseX + 2, BaseY - 6, 0),
    new Point3d(BaseX + 2, BaseY - 12, 0),
    (int)Atend.Control.Enum.ProductType.Phuse,
    0,
    0));
                Entities.Add(CreateLine(
                    new Point3d(BaseX + 2, BaseY - 12, 0),
                    new Point3d(BaseX - 2, BaseY - 12, 0),
                    (int)Atend.Control.Enum.ProductType.Phuse,
                    0,
                    0));
                Entities.Add(CreateLine(
                    new Point3d(BaseX - 2, BaseY - 12, 0),
                    new Point3d(BaseX - 2, BaseY - 6, 0),
                    (int)Atend.Control.Enum.ProductType.Phuse,
                    0,
                    0));
                Entities.Add(CreateLine(
                    new Point3d(BaseX + 2, BaseY - 6, 0),
                    new Point3d(BaseX - 2, BaseY - 6, 0),
                    (int)Atend.Control.Enum.ProductType.Phuse,
                    0,
                    0));



                //header cable
                //header cabel
                Point3dCollection Points = new Point3dCollection();
                Points.Add(new Point3d(BaseX, BaseY - 23, 0));
                Points.Add(new Point3d(BaseX - 5, BaseY - 18, 0));
                Points.Add(new Point3d(BaseX + 5, BaseY - 18, 0));
                Points.Add(new Point3d(BaseX, BaseY - 23, 0));
                Entities.Add(CreateHeaderCable(Points));


            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {

                JigPromptPointOptions ppo = new JigPromptPointOptions("\nStreetBox Position:\n");
                PromptPointResult ppr = prompts.AcquirePoint(ppo);

                if (ppr.Status == PromptStatus.OK)
                {

                    if (ppr.Value == BasePoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        BasePoint = ppr.Value;
                        return SamplerStatus.OK;
                    }

                }
                else
                {
                    return SamplerStatus.Cancel;
                }

            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                Entities.Clear();
                int AllCounter = 0;
                double ShemshStart = BasePoint.X;
                for (int i = 1; i <= _feederCount; i++)
                {

                    //int FeederCount = 0;
                    //shemshFeeder.TryGetValue(i, out FeederCount);

                    Point3d CP = new Point3d(ShemshStart + (16 * (i - 1)), BasePoint.Y, BasePoint.Z);
                    CreateFeeder(CP);
                    AllCounter++;

                    //ShemshStart +=16// (16 * (FeederCount + 1));

                }
                AllCounter++;
                AllCounter--;
                Entities.Add(CreateStreetBoxBox(new Point3d((BasePoint.X - 8) + ((AllCounter * 16) / 2), (BasePoint.Y + 5) - (30 / 2), BasePoint.Z), (AllCounter * 16), 30));


                //~~~~~~~~ SCALE ~~~~~~~~~~

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(BasePoint.X, BasePoint.Y, 0));
                foreach (Entity en in Entities)
                {
                    en.TransformBy(trans1);
                }

                //~~~~~~~~~~~~~~~~~~~~~~~~~

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
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void DrawStreetBox()
        {
            bool Conti = true;
            PromptResult pr;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.StreetBox).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.StreetBox).CommentScale;

            DrawStreetBoxJig DSB = new DrawStreetBoxJig(eStreetBoxPhuse.Count, MyScale);

            while (Conti)
            {
                pr = ed.Drag(DSB);
                if (pr.Status == PromptStatus.OK)
                {
                    Conti = false;
                    List<Entity> ENTS = DSB.GetEntities();

                    if (SaveStreetBoxData())
                    {

                        ObjectIdCollection OIC = new ObjectIdCollection();

                        foreach (Entity ent in ENTS)
                        {

                            object ProductType = null;
                            Atend.Global.Acad.AcadJigs.MyPolyLine mPoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                            if (mPoly != null)
                            {
                                //ed.WriteMessage("POLY\n");
                                if (mPoly.AdditionalDictionary.ContainsKey("ProductType"))
                                {
                                    mPoly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                    //ed.WriteMessage("PT:{0}\n", Convert.ToInt32(ProductType));
                                }
                            }
                            else
                            {
                                Atend.Global.Acad.AcadJigs.MyLine mLine = ent as Atend.Global.Acad.AcadJigs.MyLine;
                                if (mLine != null)
                                {
                                    //ed.WriteMessage("Line\n");
                                    if (mLine.AdditionalDictionary.ContainsKey("ProductType"))
                                    {
                                        mLine.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                        //ed.WriteMessage("PT:{0}\n", Convert.ToInt32(ProductType));
                                    }

                                }
                                else
                                {
                                    Atend.Global.Acad.AcadJigs.MyCircle mCircle = ent as Atend.Global.Acad.AcadJigs.MyCircle;
                                    if (mCircle != null)
                                    {

                                        if (mCircle.AdditionalDictionary.ContainsKey("ProductType"))
                                        {
                                            mCircle.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                            //ed.WriteMessage("PT:{0}\n",Convert.ToInt32(ProductType));
                                        }

                                    }
                                }
                            }

                            //~~~~~~~~~~~~~~~~~~~~~~~~~~~

                            ObjectId NewDrawnOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString());
                            OIC.Add(NewDrawnOI);

                            Atend.Base.Acad.AT_INFO info = new Atend.Base.Acad.AT_INFO(NewDrawnOI);
                            info.ParentCode = "";
                            info.ProductCode = StreetBoxPack.ProductCode;
                            info.NodeCode = StreetBoxPack.Code.ToString();
                            if (ProductType != null)
                            {
                                info.NodeType = Convert.ToInt32(ProductType);
                                if (Convert.ToInt32(ProductType) == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                                {
                                    info.ProductCode = 0;
                                }
                            }
                            else
                            {
                                info.NodeType = 0;
                            }
                            info.Insert();

                            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


                        }

                        //text will add here
                        ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(
                            Atend.Global.Acad.UAcad.WriteNote(eStreetBox.Comment, new Point3d(DSB.MyBasePoint.X, DSB.MyBasePoint.Y, DSB.MyBasePoint.Z), MyCommentScale)
                            , Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString()
                            );
                        OIC.Add(TextOi);

                        Atend.Base.Acad.AT_INFO Textinfo = new Atend.Base.Acad.AT_INFO(TextOi);
                        Textinfo.ParentCode = StreetBoxPack.Code.ToString();
                        Textinfo.ProductCode = 0;
                        Textinfo.NodeCode = "";
                        Textinfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                        Textinfo.Insert();



                        ObjectId GOI = Atend.Global.Acad.Global.MakeGroup(Guid.NewGuid().ToString(), OIC);
                        if (GOI != ObjectId.Null)
                        {
                            Atend.Base.Acad.AT_INFO GInfo = new Atend.Base.Acad.AT_INFO(GOI);
                            GInfo.ParentCode = "";
                            GInfo.NodeCode = StreetBoxPack.Code.ToString();
                            GInfo.NodeType = (int)Atend.Control.Enum.ProductType.StreetBox;
                            GInfo.ProductCode = StreetBoxPack.ProductCode;
                            GInfo.Insert();

                            //Atend.Base.Acad.AT_SUB StreetboxSub = new Atend.Base.Acad.AT_SUB(GOI);
                            //StreetboxSub.SubIdCollection.Add(TextOi);
                            //StreetboxSub.Insert();

                        }

                    }



                }
                else
                {
                    Conti = false;
                }

            }


        }

        private bool SaveStreetBoxData()
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
                    //Atend.Base.Equipment.EStreetBox sb = Atend.Base.Equipment.EStreetBox.AccessSelectByXCode(eStreetBox.XCode);
                    if (!UseAccess)
                    {
                        //if (sb.Code == -1)
                        {
                            if (!eStreetBox.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("eStreetBox.AccessInsert failed");
                            }
                        }
                        //}

                        //if (!UseAccess)
                        //{
                        foreach (Atend.Base.Equipment.EStreetBoxPhuse SelectedStreetBoxPhuse in eStreetBoxPhuse)
                        {
                            //ed.WriteMessage("@@{0}\n", SelectedStreetBoxPhuse.PhuseXCode);
                            //ed.WriteMessage("########## SelectedStreetBoxPhuse.PhuseXCode:{0}\n", SelectedStreetBoxPhuse.PhuseXCode);



                            Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(SelectedStreetBoxPhuse.PhuseXCode);
                            //ed.WriteMessage("########## phuse.Code:{0}\n", phuse.Code);
                            if (phuse.Code != -1)
                            {

                                Atend.Base.Equipment.EPhusePole _EPhusePole = Atend.Base.Equipment.EPhusePole.SelectByXCode(phuse.PhusePoleXCode);
                                if (_EPhusePole.Code != -1)
                                {
                                    //ed.WriteMessage("112\n");
                                    if (!_EPhusePole.AccessInsert(aTransaction, aConnection, true, true))
                                    {
                                        throw new System.Exception("AccessInsert failed PhusePole");
                                    }
                                }
                                else
                                {
                                    throw new System.Exception("Lack of data in StreetBox:Phuse:PhusePole");
                                }

                                phuse.PhusePoleCode = _EPhusePole.Code;
                                if (!phuse.AccessInsert(aTransaction, aConnection, true, true))
                                {
                                    throw new System.Exception("ePhuse.AccesInsert failed");
                                }
                                SelectedStreetBoxPhuse.PhuseCode = phuse.Code;
                                SelectedStreetBoxPhuse.StreetBoxCode = eStreetBox.Code;
                                //if (sb.Code == -1)
                                //    SelectedStreetBoxPhuse.StreetBoxCode = eStreetBox.Code;
                                //else
                                //    SelectedStreetBoxPhuse.StreetBoxCode = sb.Code;
                                if (!SelectedStreetBoxPhuse.AccessInsert(aTransaction, aConnection, true, true))
                                {
                                    throw new System.Exception("EStreetBoxPhuseInsert failed");
                                }
                            }
                            else
                            {
                                throw new System.Exception("EPhuse.SelectByXCode failed");
                            }
                        }
                    }

                    StreetBoxPack = new Atend.Base.Design.DPackage();
                    StreetBoxPack.Count = 1;
                    StreetBoxPack.IsExistance = Existance;
                    StreetBoxPack.NodeCode = Guid.Empty;
                    StreetBoxPack.Number = "0";
                    StreetBoxPack.ParentCode = Guid.Empty;
                    StreetBoxPack.ProjectCode = ProjectCode;
                    StreetBoxPack.ProductCode = eStreetBox.Code;
                    //if (sb.Code == -1)
                    //    StreetBoxPack.ProductCode = eStreetBox.Code;
                    //else
                    //    StreetBoxPack.ProductCode = sb.Code;
                    StreetBoxPack.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);

                    if (!StreetBoxPack.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("StreetBoxPack.AccessInsert failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR SaveStreetBoxData 02 :{0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR SaveStreetBoxData 01 :{0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();

            #region Not to Get Green

            Atend.Base.Acad.AcadGlobal.StreetBoxData.UseAccess = true;
            UseAccess = true;

            #endregion


            return true;

        }

        public void DrawStreetBoxUpdate()
        {
            bool Conti = true;
            PromptResult pr;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.StreetBox).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.StreetBox).CommentScale;


            DrawStreetBoxJig DSB = new DrawStreetBoxJig(eStreetBoxPhuse.Count, MyScale);

            while (Conti)
            {
                pr = ed.Drag(DSB);
                if (pr.Status == PromptStatus.OK)
                {
                    Conti = false;
                    List<Entity> ENTS = DSB.GetEntities();

                    //if (SaveStreetBoxData())
                    //{

                    ObjectIdCollection OIC = new ObjectIdCollection();

                    foreach (Entity ent in ENTS)
                    {

                        object ProductType = null;
                        Atend.Global.Acad.AcadJigs.MyPolyLine mPoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                        if (mPoly != null)
                        {
                            //ed.WriteMessage("POLY\n");
                            if (mPoly.AdditionalDictionary.ContainsKey("ProductType"))
                            {
                                mPoly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                //ed.WriteMessage("PT:{0}\n", Convert.ToInt32(ProductType));
                            }
                        }
                        else
                        {
                            Atend.Global.Acad.AcadJigs.MyLine mLine = ent as Atend.Global.Acad.AcadJigs.MyLine;
                            if (mLine != null)
                            {
                                //ed.WriteMessage("Line\n");
                                if (mLine.AdditionalDictionary.ContainsKey("ProductType"))
                                {
                                    mLine.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                    //ed.WriteMessage("PT:{0}\n", Convert.ToInt32(ProductType));
                                }

                            }
                            else
                            {
                                Atend.Global.Acad.AcadJigs.MyCircle mCircle = ent as Atend.Global.Acad.AcadJigs.MyCircle;
                                if (mCircle != null)
                                {

                                    if (mCircle.AdditionalDictionary.ContainsKey("ProductType"))
                                    {
                                        mCircle.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                        //ed.WriteMessage("PT:{0}\n",Convert.ToInt32(ProductType));
                                    }

                                }
                            }
                        }

                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~

                        ObjectId NewDrawnOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString());
                        OIC.Add(NewDrawnOI);

                        Atend.Base.Acad.AT_INFO info = new Atend.Base.Acad.AT_INFO(NewDrawnOI);
                        info.ParentCode = "";
                        info.ProductCode = StreetBoxPack.ProductCode;
                        info.NodeCode = StreetBoxPack.Code.ToString();
                        if (ProductType != null)
                        {
                            info.NodeType = Convert.ToInt32(ProductType);
                        }
                        else
                        {
                            info.NodeType = 0;
                        }
                        info.Insert();

                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


                    }

                    //text will add here
                    ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(
                        Atend.Global.Acad.UAcad.WriteNote(eStreetBox.Comment, new Point3d(DSB.MyBasePoint.X, DSB.MyBasePoint.Y, DSB.MyBasePoint.Z), MyCommentScale)
                        , Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString()
                        );
                    OIC.Add(TextOi);

                    Atend.Base.Acad.AT_INFO Textinfo = new Atend.Base.Acad.AT_INFO(TextOi);
                    Textinfo.ParentCode = StreetBoxPack.Code.ToString();
                    Textinfo.ProductCode = 0;
                    Textinfo.NodeCode = "";
                    Textinfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                    Textinfo.Insert();



                    ObjectId GOI = Atend.Global.Acad.Global.MakeGroup(Guid.NewGuid().ToString(), OIC);
                    if (GOI != ObjectId.Null)
                    {
                        Atend.Base.Acad.AT_INFO GInfo = new Atend.Base.Acad.AT_INFO(GOI);
                        GInfo.ParentCode = "";
                        GInfo.NodeCode = StreetBoxPack.Code.ToString();
                        GInfo.NodeType = (int)Atend.Control.Enum.ProductType.StreetBox;
                        GInfo.ProductCode = StreetBoxPack.ProductCode;
                        GInfo.Insert();

                        //Atend.Base.Acad.AT_SUB StreetboxSub = new Atend.Base.Acad.AT_SUB(GOI);
                        //StreetboxSub.SubIdCollection.Add(TextOi);
                        //StreetboxSub.Insert();

                    }

                    //}



                }
                else
                {
                    Conti = false;
                }
            }
        }

        public bool UpdateStreetBoxData(Guid EXCode)
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
                    StreetBoxPack = Atend.Base.Design.DPackage.AccessSelectByCode(EXCode);
                    if (!UseAccess)
                    {
                        if (!eStreetBox.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eStreetBox.AccessInsert failed");
                        }
                        else
                        {
                            foreach (Atend.Base.Equipment.EStreetBoxPhuse sbPhuses in eStreetBoxPhuse)
                            {
                                if (!sbPhuses.AccessInsert(aTransaction, aConnection, true, true))
                                {
                                    throw new System.Exception("eStreetBoxPhuse.AccessInsert failed");
                                }
                            }
                        }

                        foreach (Atend.Base.Equipment.EStreetBoxPhuse SelectedStreetBoxPhuse in eStreetBoxPhuse)
                        {
                            Atend.Base.Equipment.EPhuse phuse = Atend.Base.Equipment.EPhuse.SelectByXCode(SelectedStreetBoxPhuse.PhuseXCode);
                            if (phuse.Code != -1)
                            {
                                if (!phuse.AccessInsert(aTransaction, aConnection, true, true))
                                {
                                    throw new System.Exception("ePhuse.AccesInsert failed");
                                }
                                SelectedStreetBoxPhuse.PhuseCode = phuse.Code;
                                SelectedStreetBoxPhuse.StreetBoxCode = eStreetBox.Code;
                                if (!SelectedStreetBoxPhuse.AccessInsert(aTransaction, aConnection, true, true))
                                {
                                    throw new System.Exception("EStreetBoxPhuseInsert failed");
                                }
                            }
                            else
                            {
                                throw new System.Exception("EPhuse.SelectByXCode failed");
                            }
                        }
                    }
                    StreetBoxPack.IsExistance = Existance;
                    StreetBoxPack.ProductCode = eStreetBox.Code;
                    StreetBoxPack.ProjectCode = ProjectCode;
                    StreetBoxPack.Number = "";
                    if (StreetBoxPack.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = eStreetBox.Code;
                        atinfo.Insert();
                    }
                    else
                    {
                        throw new System.Exception("StreetBoxPack.AccessInsert2 failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateStreetBox 01(transaction) : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateStreetBox 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }
            aTransaction.Commit();
            DeleteStreetBox(selectedObjectId);
            DrawStreetBoxUpdate();
            aConnection.Close();
            return true;
        }

        //public bool DeleteStreetBox(ObjectId StreetBoxOI)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    try
        //    {
        //        ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(StreetBoxOI);
        //        ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
        //        foreach (ObjectId collect in Collection)
        //        {
        //            if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
        //            {
        //                throw new System.Exception("can not remove entity");
        //            }
        //        }

        //        ///////////////////
        //        //Atend.Base.Acad.AT_SUB streetSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(StreetBoxOI);
        //        //foreach (ObjectId oi in streetSub.SubIdCollection)
        //        //{
        //        //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
        //        //    {
        //        //        throw new System.Exception("can not remove entity");
        //        //    }
        //        //}

        //        //ObjectIdCollection OIC = Atend.Global.Acad.UAcad.GetGroupSubEntities(StreetBoxOI);
        //        //ed.WriteMessage("++++++++ {0}\n", OIC.Count);
        //        //foreach (ObjectId oi in OIC)
        //        //{
        //        //    ed.WriteMessage("@@@ {0}\n", oi);
        //        //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
        //        //    {
        //        //        throw new System.Exception("can not remove entity");
        //        //    }
        //        //}

        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error DeleteStreetBox : {0} \n", ex.Message);
        //        return false;
        //    }
        //    ed.WriteMessage("DELETE STREET BOX\n");
        //    return true;
        //}

        public static bool DeleteStreetBoxData(ObjectId StreetBoxOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(StreetBoxOI);
                ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                foreach (ObjectId collect in Collection)
                {
                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                    {
                        Atend.Base.Acad.AT_SUB Sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
                        foreach (ObjectId oi in Sub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO atinfo2 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (atinfo2.ParentCode != "NONE" && atinfo2.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
                            {
                                if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(atinfo2.NodeCode.ToString())))
                                {
                                    throw new System.Exception("Error In Delete dbranch\n");
                                }
                            }
                        }
                    }
                }

                //delete StreetBox
                Atend.Base.Acad.AT_INFO conductorinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(StreetBoxOI);
                if (!Atend.Base.Design.DPackage.AccessDelete(new Guid(conductorinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete dpackage\n");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR StreetBox : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteStreetBox(ObjectId StreetBoxOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //Move Ground
                Atend.Base.Acad.AT_SUB Collection1 = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(StreetBoxOI);
                foreach (ObjectId obj in Collection1.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO at_info02 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(obj);
                    if (at_info02.ParentCode != "NONE" && at_info02.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
                    {
                        ObjectIdCollection Coll = Atend.Global.Acad.UAcad.GetGroupSubEntities(obj);
                        foreach (ObjectId col in Coll)
                        {
                            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(col);
                            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Ground)
                            {
                                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(col))
                                {
                                    throw new System.Exception("Error In Delete ground\n");
                                }
                            }
                        }
                    }
                }

                //Move Cabel
                ObjectId id = Atend.Global.Acad.UAcad.GetEntityGroup(StreetBoxOI);
                ObjectIdCollection Collection = Atend.Global.Acad.UAcad.GetGroupSubEntities(id);
                foreach (ObjectId collect in Collection)
                {
                    Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect);
                    if (atinfo.ParentCode != "NONE" && atinfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                    {
                        Atend.Base.Acad.AT_SUB sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(collect);
                        foreach (ObjectId objsub in sub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO at_info1 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(objsub);
                            if (at_info1.ParentCode != "NONE" && at_info1.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel)
                            {
                                //Delete Comment 
                                Atend.Base.Acad.AT_SUB subBranch = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(objsub);
                                foreach (ObjectId collect001 in subBranch.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_INFO atinfo001 = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(collect001);
                                    if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                                    {
                                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect001))
                                        {
                                            throw new System.Exception("Error In Delete Comment\n");
                                        }
                                    }
                                    if (atinfo001.ParentCode != "NONE" && atinfo001.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel && atinfo001.SelectedObjectId != collect)
                                    {
                                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(objsub))
                                        {
                                            throw new System.Exception("Error In Delete groundcabel\n");
                                        }
                                        else
                                        {
                                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(objsub, collect001);
                                        }
                                    }

                                }
                            }
                        }
                    }

                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(collect))
                    {
                        throw new System.Exception("Error In Delete Sub Streetbox\n");
                    }

                }

                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(StreetBoxOI))
                {
                    throw new System.Exception("GRA while delete StreetBox\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR StreetBox : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

    }
}
