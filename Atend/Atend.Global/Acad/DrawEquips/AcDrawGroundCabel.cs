﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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

//update from tehran 7/15
namespace Atend.Global.Acad.DrawEquips
{

    public class AcDrawGroundCabel
    {
        //~~~~~~~~~~~~~~~~ PROPERTIES ~~~~~~~~~~~~~~~~~~~~~//

        Atend.Base.Design.DBranch _dBranch;
        double OperationBulk = 0;

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

        int _projectCode;
        public int ProjectCode
        {
            get { return _projectCode; }
            set { _projectCode = value; }
        }

        Atend.Base.Equipment.EGroundCabelTip _eGroundCabelTip;
        public Atend.Base.Equipment.EGroundCabelTip eGroundCabelTip
        {
            get { return _eGroundCabelTip; }
            set { _eGroundCabelTip = value; }
        }

        List<Atend.Base.Equipment.EGroundCabel> _eGroundCabels;
        public List<Atend.Base.Equipment.EGroundCabel> eGroundCabels
        {
            get { return _eGroundCabels; }
            set { _eGroundCabels = value; }
        }

        ObjectId selectedObjID;

        public ObjectId SelectedObjID
        {
            get { return selectedObjID; }
            set { selectedObjID = value; }
        }
        //~~~~~~~~~~~~~~~~ CLASS ~~~~~~~~~~~~~~~~~~~~~//

        class DrawGroundCableJig : EntityJig
        {
            Point3dCollection _Point3dCollection;
            Point3d m_tempPoint;
            Plane m_plane;

            public DrawGroundCableJig(Matrix3d ucs)
                : base(new Polyline())
            {
                // Create a point collection to store our vertices
                _Point3dCollection = new Point3dCollection();

                // Create a temporary plane, to help with calcs
                Point3d origin = new Point3d(0, 0, 0);
                Vector3d normal = new Vector3d(0, 0, 1);
                normal = normal.TransformBy(ucs);
                m_plane = new Plane(origin, normal);

                // Create polyline, set defaults, add dummy vertex
                Polyline pline = Entity as Polyline;
                pline.SetDatabaseDefaults();
                pline.Normal = normal;
                pline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                JigPromptPointOptions jigOpts = new JigPromptPointOptions();
                jigOpts.UserInputControls = (UserInputControls.Accept3dCoordinates |
                                             UserInputControls.NullResponseAccepted |
                                             UserInputControls.NoNegativeResponseAccepted);
                if (_Point3dCollection.Count == 0)
                {
                    // For the first vertex, just ask for the point
                    jigOpts.Message = "\nStart point of polyline: ";
                }
                else if (_Point3dCollection.Count > 0)
                {
                    // For subsequent vertices, use a base point
                    jigOpts.BasePoint = _Point3dCollection[_Point3dCollection.Count - 1];
                    jigOpts.UseBasePoint = true;
                    jigOpts.Message = "\nPolyline vertex: ";
                }
                else // should never happen
                {
                    return SamplerStatus.Cancel;
                }
                PromptPointResult res = prompts.AcquirePoint(jigOpts);
                if (m_tempPoint == res.Value)
                {
                    return SamplerStatus.NoChange;
                }
                else if (res.Status == PromptStatus.OK)
                {
                    m_tempPoint = res.Value;
                    return SamplerStatus.OK;
                }
                return SamplerStatus.Cancel;
            }

            protected override bool Update()
            {
                Polyline pline = Entity as Polyline;
                pline.SetPointAt(pline.NumberOfVertices - 1, m_tempPoint.Convert2d(m_plane));
                return true;
            }

            public Entity GetEntity()
            {
                return Entity;
            }

            public void AddLatestVertex()
            {
                _Point3dCollection.Add(m_tempPoint);
                Polyline pline = Entity as Polyline;
                pline.AddVertexAt(pline.NumberOfVertices, new Point2d(0, 0), 0, 0, 0);
            }

            public void RemoveLastVertex()
            {
                Polyline pline = Entity as Polyline;
                pline.RemoveVertexAt(_Point3dCollection.Count);
            }
        }

        //~~~~~~~~~~~~~~~~ METHODS ~~~~~~~~~~~~~~~~~~~~~//

        public AcDrawGroundCabel()
        {
            _dBranch = new Atend.Base.Design.DBranch();
        }

        //OLD ONE
        //public void DrawGroundCable02()
        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;

        //    Editor ed = doc.Editor;
        //    Matrix3d ucs = ed.CurrentUserCoordinateSystem;
        //    //ed.WriteMessage("UCS " + ucs.ToString() + "\n");
        //    DrawGroundCableJig jig = new DrawGroundCableJig(ucs);
        //    ObjectIdCollection HeaderOis = new ObjectIdCollection();

        //    double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).Scale;
        //    double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).CommentScale;

        //    bool bSuccess = true, bComplete = false;
        //    do
        //    {
        //        PromptResult res = ed.Drag(jig);
        //        bSuccess = (res.Status == PromptStatus.OK);
        //        if (bSuccess)
        //        {
        //            Polyline pl = jig.GetEntity() as Polyline;
        //            if (pl != null)
        //            {

        //                if (pl.NumberOfVertices == 1)
        //                {
        //                    Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        //                    //ed.WriteMessage("&&&&&&CurrentPoint:{0}\n", CurrentPoint);
        //                    System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        //                    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);
        //                    bool PointContainerWasFound = false;
        //                    Point3d AcceptedCenterPoint = Point3d.Origin;
        //                    foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                    {
        //                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                        if (drs.Length != 0)
        //                        {
        //                            PointContainerWasFound = true;
        //                            HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                            string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                            string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                            string y = StrTemp[1];
        //                            string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                            //ed.WriteMessage("&&&&& :{0},{1},{2}\n",x,y,z);
        //                            AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                            //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
        //                        }
        //                    }
        //                    if (PointContainerWasFound)
        //                    {
        //                        //Set first Point
        //                        //ed.WriteMessage("Set first Point");
        //                        pl.SetPointAt(0, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));

        //                    }
        //                    else
        //                    {
        //                        //Draw header cable
        //                        //ed.WriteMessage("Draw header cable");
        //                        //ed.WriteMessage("~~Hedaer point:{0}\n", CurrentPoint);

        //                        Atend.Design.frmDrawClampOrHeader02 _frmDrawHeaderCabl = new Atend.Design.frmDrawClampOrHeader02();
        //                        //if (_frmDrawHeaderCabl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                        if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
        //                        {
        //                            Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
        //                            _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                            _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                            _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                            _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;

        //                            //Entity Header = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint , ObjectId.Null);
        //                            //ObjectId HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Header, Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString());
        //                            ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
        //                            //ed.WriteMessage("header was drawn \n");
        //                            HeaderOis.Add(HeaderOI);
        //                            Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        //                            HeaderInfo.ParentCode = "";
        //                            HeaderInfo.NodeCode = "";
        //                            HeaderInfo.ProductCode = 0;
        //                            HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        //                            HeaderInfo.Insert();
        //                        }
        //                        else
        //                        {
        //                            return;
        //                        }

        //                    }
        //                    //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        //                    jig.AddLatestVertex();

        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        //                    jig.AddLatestVertex();

        //                }

        //            }
        //        }
        //        bComplete = (res.Status == PromptStatus.None);
        //        if (bComplete)
        //        {
        //            Polyline pl = jig.GetEntity() as Polyline;
        //            if (pl != null)
        //            {
        //                //ed.WriteMessage("Go to add vertex : {0}\n", pl.NumberOfVertices);
        //                jig.RemoveLastVertex();

        //                //------------------
        //                Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        //                //ed.WriteMessage("~~~~CurrentPoint:{0}\n", CurrentPoint);
        //                System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        //                System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);
        //                bool PointContainerWasFound = false;
        //                Point3d AcceptedCenterPoint = Point3d.Origin;
        //                foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                {
        //                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                    if (drs.Length != 0)
        //                    {
        //                        PointContainerWasFound = true;
        //                        HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                        string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                        string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                        string y = StrTemp[1];
        //                        string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                        AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                        //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
        //                    }
        //                }
        //                if (PointContainerWasFound)
        //                {
        //                    //Set first Point
        //                    //ed.WriteMessage("Set first Point");
        //                    pl.SetPointAt(pl.NumberOfVertices - 1, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));

        //                }
        //                else
        //                {
        //                    //Draw header cable
        //                    //ed.WriteMessage("Draw header cable");
        //                    //ed.WriteMessage("~~Hedaer point:{0}\n", CurrentPoint);
        //                    //Entity Header = DrawHeader(CurrentPoint);
        //                    //ObjectId HeaderOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Header, Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString());
        //                    Atend.Design.frmDrawHeaderCable02 _frmDrawHeaderCabl = new Atend.Design.frmDrawHeaderCable02();
        //                    if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
        //                    {

        //                        AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();

        //                        _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                        _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                        _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                        _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;


        //                        ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
        //                        HeaderOis.Add(HeaderOI);
        //                        Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        //                        HeaderInfo.ParentCode = "";
        //                        HeaderInfo.NodeCode = "";
        //                        HeaderInfo.ProductCode = 0;
        //                        HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        //                        HeaderInfo.Insert();
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }


        //                }
        //                //------------------


        //            }
        //        }
        //    } while (bSuccess && !bComplete);
        //    if (bComplete)
        //    {
        //        //Database db = doc.Database;
        //        //Transaction tr = db.TransactionManager.StartTransaction();
        //        //using (tr)
        //        //{
        //        //    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead, false);
        //        //    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite, false);
        //        //    btr.AppendEntity(jig.GetEntity());
        //        //    tr.AddNewlyCreatedDBObject(jig.GetEntity(), true);
        //        //    tr.Commit();
        //        //}



        //        Polyline CableEntity = jig.GetEntity() as Polyline;
        //        if (CableEntity != null)
        //        {
        //            Atend.Design.frmDrawBranch FDB = new Atend.Design.frmDrawBranch();
        //            FDB.Length = CableEntity.Length;
        //            if (Application.ShowModalDialog(FDB) == System.Windows.Forms.DialogResult.OK)
        //            {
        //                _dBranch.Lenght = FDB.Length;
        //                _dBranch.Number = "CABLE";
        //                if (SaveGroundCabelData())
        //                {
        //                    //CableEntity.LinetypeId=

        //                    string LayerName = "";
        //                    foreach (Atend.Base.Equipment.EGroundCabel gb in eGroundCabels)
        //                    {
        //                        if (gb.XCode == eGroundCabelTip.PhaseProductXCode)
        //                        {
        //                            if (gb.Vol == 400)
        //                            {
        //                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString();
        //                            }
        //                            else
        //                            {
        //                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString();
        //                            }
        //                        }
        //                    }

        //                    ObjectId LineOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(CableEntity, LayerName);
        //                    Atend.Base.Acad.AT_INFO CabelInfo = new Atend.Base.Acad.AT_INFO(LineOI);
        //                    CabelInfo.ParentCode = "";
        //                    CabelInfo.NodeCode = _dBranch.Code.ToString();
        //                    CabelInfo.ProductCode = _dBranch.ProductCode;
        //                    CabelInfo.NodeType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //                    CabelInfo.Insert();

        //                    string comment = string.Format("{0} {1}m", eGroundCabelTip.Description, Math.Round(_dBranch.Lenght, 2));
        //                    ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, CableEntity.GetPoint3dAt(0), CableEntity.GetPoint3dAt(CableEntity.NumberOfVertices - 1), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //                    Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(TextOi);
        //                    TextInfo.ParentCode = _dBranch.Code.ToString();
        //                    TextInfo.NodeCode = "";
        //                    TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        //                    TextInfo.ProductCode = 0;
        //                    TextInfo.Insert();



        //                    Atend.Base.Acad.AT_SUB LineSub = new Atend.Base.Acad.AT_SUB(LineOI);
        //                    HeaderOis.Add(TextOi);
        //                    LineSub.SubIdCollection = HeaderOis;
        //                    LineSub.Insert();

        //                    foreach (ObjectId Hoi in HeaderOis)
        //                    {
        //                        //ed.WriteMessage("HOI:{0}\n",Hoi);
        //                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(LineOI, Hoi);

        //                    }


        //                }
        //            }
        //        }


        //    }
        //}

        //private void DrawGroundCable()
        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;

        //    Editor ed = doc.Editor;
        //    Matrix3d ucs = ed.CurrentUserCoordinateSystem;
        //    //ed.WriteMessage("UCS " + ucs.ToString() + "\n");
        //    //ed.WriteMessage("start drawing \n");
        //    DrawGroundCableJig jig = new DrawGroundCableJig(ucs);
        //    ObjectIdCollection HeaderOis = new ObjectIdCollection();

        //    double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).Scale;
        //    double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).CommentScale;
        //    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);

        //    bool bSuccess = true, bComplete = false;
        //    do
        //    {
        //        PromptResult res = ed.Drag(jig);
        //        bSuccess = (res.Status == PromptStatus.OK);
        //        if (bSuccess)
        //        {
        //            Polyline pl = jig.GetEntity() as Polyline;
        //            if (pl != null)
        //            {
        //                if (pl.NumberOfVertices == 1)
        //                {
        //                    Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        //                    System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        //                    bool PointContainerWasFound = false;
        //                    Point3d AcceptedCenterPoint = Point3d.Origin;
        //                    foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                    {
        //                        DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                        if (drs.Length != 0)
        //                        {
        //                            PointContainerWasFound = true;
        //                            HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                            string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                            string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                            string y = StrTemp[1];
        //                            string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                            AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                        }
        //                    }
        //                    if (PointContainerWasFound)
        //                    {
        //                        pl.SetPointAt(0, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));
        //                    }
        //                    else
        //                    {
        //                        Atend.Design.frmDrawHeaderCable02 _frmDrawHeaderCabl = new Atend.Design.frmDrawHeaderCable02();
        //                        if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
        //                        {
        //                            //ed.WriteMessage("go for header cable draw \n");
        //                            Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
        //                            _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                            _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                            _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                            _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
        //                            ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);

        //                            HeaderOis.Add(HeaderOI);
        //                            Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        //                            HeaderInfo.ParentCode = "";
        //                            HeaderInfo.NodeCode = "";
        //                            HeaderInfo.ProductCode = 0;
        //                            HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        //                            HeaderInfo.Insert();
        //                        }
        //                        else
        //                        {
        //                            return;
        //                        }

        //                    }
        //                    jig.AddLatestVertex();
        //                }
        //                else
        //                {
        //                    jig.AddLatestVertex();
        //                }
        //            }
        //        }//if (bSuccess)
        //        bComplete = (res.Status == PromptStatus.None);
        //        if (bComplete)
        //        {
        //            Polyline pl = jig.GetEntity() as Polyline;
        //            if (pl != null)
        //            {
        //                jig.RemoveLastVertex();

        //                Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
        //                System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
        //                bool PointContainerWasFound = false;
        //                Point3d AcceptedCenterPoint = Point3d.Origin;
        //                foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                {
        //                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                    if (drs.Length != 0)
        //                    {
        //                        PointContainerWasFound = true;
        //                        HeaderOis.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                        string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                        string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                        string y = StrTemp[1];
        //                        string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                        AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                        //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
        //                    }
        //                }
        //                if (PointContainerWasFound)
        //                {
        //                    pl.SetPointAt(pl.NumberOfVertices - 1, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));

        //                }
        //                else
        //                {
        //                    Atend.Design.frmDrawHeaderCable02 _frmDrawHeaderCabl = new Atend.Design.frmDrawHeaderCable02();
        //                    if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
        //                    {

        //                        AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();

        //                        _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                        _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                        _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                        _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;


        //                        ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
        //                        HeaderOis.Add(HeaderOI);
        //                        Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
        //                        HeaderInfo.ParentCode = "";
        //                        HeaderInfo.NodeCode = "";
        //                        HeaderInfo.ProductCode = 0;
        //                        HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
        //                        HeaderInfo.Insert();
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //    } while (bSuccess && !bComplete);
        //    if (bComplete)
        //    {
        //        Polyline CableEntity = jig.GetEntity() as Polyline;
        //        if (CableEntity != null)
        //        {
        //            Atend.Design.frmDrawBranch FDB = new Atend.Design.frmDrawBranch();
        //            FDB.Length = CableEntity.Length;
        //            if (Application.ShowModalDialog(FDB) == System.Windows.Forms.DialogResult.OK)
        //            {
        //                _dBranch.Lenght = FDB.Length;
        //                _dBranch.Number = "CABLE";
        //                if (SaveGroundCabelData())
        //                {
        //                    //CableEntity.LinetypeId=

        //                    string LayerName = "";
        //                    foreach (Atend.Base.Equipment.EGroundCabel gb in eGroundCabels)
        //                    {
        //                        if (gb.XCode == eGroundCabelTip.PhaseProductXCode)
        //                        {
        //                            if (gb.Vol == 400)
        //                            {
        //                                LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString();
        //                            }
        //                            else
        //                            {
        //                                LayerName = Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString();
        //                            }
        //                        }
        //                    }

        //                    ObjectId LineOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(CableEntity, LayerName);
        //                    Atend.Base.Acad.AT_INFO CabelInfo = new Atend.Base.Acad.AT_INFO(LineOI);
        //                    CabelInfo.ParentCode = "";
        //                    CabelInfo.NodeCode = _dBranch.Code.ToString();
        //                    CabelInfo.ProductCode = _dBranch.ProductCode;
        //                    CabelInfo.NodeType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //                    CabelInfo.Insert();

        //                    string comment = string.Format("{0} {1}m", eGroundCabelTip.Description, Math.Round(_dBranch.Lenght, 2));
        //                    ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, CableEntity.GetPoint3dAt(0), CableEntity.GetPoint3dAt(CableEntity.NumberOfVertices - 1), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //                    Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(TextOi);
        //                    TextInfo.ParentCode = _dBranch.Code.ToString();
        //                    TextInfo.NodeCode = "";
        //                    TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        //                    TextInfo.ProductCode = 0;
        //                    TextInfo.Insert();



        //                    Atend.Base.Acad.AT_SUB LineSub = new Atend.Base.Acad.AT_SUB(LineOI);
        //                    HeaderOis.Add(TextOi);
        //                    LineSub.SubIdCollection = HeaderOis;
        //                    LineSub.Insert();

        //                    foreach (ObjectId Hoi in HeaderOis)
        //                    {
        //                        //ed.WriteMessage("HOI:{0}\n",Hoi);
        //                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(LineOI, Hoi);

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //Current

        public void DrawGroundCable02()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;
            Matrix3d ucs = ed.CurrentUserCoordinateSystem;
            DrawGroundCableJig jig = new DrawGroundCableJig(ucs);
            ObjectIdCollection HeaderOis = new ObjectIdCollection();

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.GroundCabel).CommentScale;
            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.GroundCabel);

            bool bSuccess = true, bComplete = false;
            do
            {
                PromptResult res = ed.Drag(jig);
                bSuccess = (res.Status == PromptStatus.OK);
                if (bSuccess)
                {
                    Polyline pl = jig.GetEntity() as Polyline;
                    if (pl != null)
                    {
                        if (pl.NumberOfVertices == 1)
                        {
                            Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
                            System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
                            bool PointContainerWasFound = false;
                            Point3d AcceptedCenterPoint = Point3d.Origin;
                            ObjectId _oitemp = ObjectId.Null;
                            foreach (System.Data.DataRow dr in PointContainerList.Rows)
                            {
                                DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                                if (drs.Length != 0)
                                {
                                    PointContainerWasFound = true;
                                    HeaderOis.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                                    _oitemp = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));
                                    string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                    string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                    string y = StrTemp[1];
                                    string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                    AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                }
                            }
                            if (PointContainerWasFound)
                            {
                                //ed.WriteMessage("1:PointContainerWasFound \n");
                                pl.SetPointAt(0, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));
                                Atend.Base.Acad.AT_INFO tmpNodeInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_oitemp);
                                if (eGroundCabels[0].Vol == 400)
                                {
                                    if (tmpNodeInfo.ParentCode != "NONE" && tmpNodeInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho)
                                    {
                                        if (tmpNodeInfo.ProductCode == 0)
                                        {
                                            UpdateOtherNodeData(tmpNodeInfo);
                                        }
                                    }
                                    else
                                    {

                                        if (tmpNodeInfo.ParentCode != "NONE" && tmpNodeInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                                        {
                                            if (tmpNodeInfo.ProductCode == 0)
                                            {
                                                UpdateOtherNodeData(tmpNodeInfo);
                                            }
                                        }
                                        else
                                        {

                                            Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                            notification.Title = "ترسیم کابل زمینی";
                                            notification.Msg = "گره انتخاب شده باید فشارضعیف باشد ";
                                            notification.infoCenterBalloon();
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (tmpNodeInfo.ParentCode != "NONE" && tmpNodeInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                                    {
                                        if (tmpNodeInfo.ProductCode == 0)
                                        {
                                            UpdateOtherNodeData(tmpNodeInfo);
                                        }
                                    }
                                    else
                                    {
                                        Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                        notification.Title = "ترسیم کابل زمینی";
                                        notification.Msg = "گره انتخاب شده مجاز نمی باشد ";
                                        notification.infoCenterBalloon();

                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (eGroundCabels[0].Vol == 400)
                                {
                                    //Is Week
                                    Atend.Global.Design.frmDrawKablsho02 _frmDrawKablsho02 = new Atend.Global.Design.frmDrawKablsho02();
                                    if (Application.ShowModalDialog(_frmDrawKablsho02) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        //ed.WriteMessage("go for header cable draw \n");
                                        if (_frmDrawKablsho02.HeaderIsSelected == false)
                                        {
                                            Atend.Global.Acad.DrawEquips.AcDrawKablsho _AcDrawKablsho = new AcDrawKablsho();
                                            _AcDrawKablsho.Existance = Atend.Base.Acad.AcadGlobal.KablshoData.Existance;
                                            _AcDrawKablsho.UseAccess = Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess;
                                            _AcDrawKablsho.eKablsho = Atend.Base.Acad.AcadGlobal.KablshoData.eKablsho;
                                            _AcDrawKablsho.ProjectCode = Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode;
                                            ObjectId HeaderOI = _AcDrawKablsho.DrawKablsho02(CurrentPoint);
                                            HeaderOis.Add(HeaderOI);
                                        }
                                        else
                                        {
                                            Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
                                            _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                            _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                            _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                            _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                            ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
                                            HeaderOis.Add(HeaderOI);
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }
                                else
                                {

                                    Atend.Global.Design.frmDrawHeaderCable02 _frmDrawHeaderCabl = new Atend.Global.Design.frmDrawHeaderCable02(Convert.ToInt32(eGroundCabels[0].Vol), eGroundCabels[0].CrossSectionArea);
                                    if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        //ed.WriteMessage("go for header cable draw \n");
                                        Atend.Global.Acad.DrawEquips.AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
                                        _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                        _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                        _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                        _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                        ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);

                                        HeaderOis.Add(HeaderOI);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }

                            }
                            jig.AddLatestVertex();
                        }
                        else
                        {
                            jig.AddLatestVertex();
                        }
                    }
                }//if (bSuccess)
                bComplete = (res.Status == PromptStatus.None);
                if (bComplete)
                {
                    Polyline pl = jig.GetEntity() as Polyline;
                    if (pl != null)
                    {
                        jig.RemoveLastVertex();

                        Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CurrentPoint);
                        bool PointContainerWasFound = false;
                        Point3d AcceptedCenterPoint = Point3d.Origin;
                        ObjectId _oitemp = ObjectId.Null;
                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                PointContainerWasFound = true;
                                HeaderOis.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                                _oitemp = new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])));
                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                //ed.WriteMessage("~~Accepted point:{0}\n", AcceptedCenterPoint);
                            }
                        }
                        if (PointContainerWasFound)
                        {
                            //ed.WriteMessage("1:PointContainerWasFound \n");
                            pl.SetPointAt(pl.NumberOfVertices - 1, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));
                            Atend.Base.Acad.AT_INFO tmpNodeInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(_oitemp);
                            if (eGroundCabels[0].Vol == 400)
                            {
                                if (tmpNodeInfo.ParentCode != "NONE" && tmpNodeInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho)
                                {
                                    if (tmpNodeInfo.ProductCode == 0)
                                    {
                                        UpdateOtherNodeData(tmpNodeInfo);
                                    }
                                }
                                else
                                {
                                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                    notification.Title = "ترسیم کابل زمینی";
                                    notification.Msg = "گره انتخاب شده مجاز نمی باشد ";
                                    notification.infoCenterBalloon();

                                    return;
                                }
                            }
                            else
                            {
                                if (tmpNodeInfo.ParentCode != "NONE" && tmpNodeInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                                {
                                    if (tmpNodeInfo.ProductCode == 0)
                                    {
                                        UpdateOtherNodeData(tmpNodeInfo);
                                    }
                                }
                                else
                                {
                                    Atend.Global.Acad.Notification notification = new Atend.Global.Acad.Notification();
                                    notification.Title = "ترسیم کابل زمینی";
                                    notification.Msg = "گره انتخاب شده مجاز نمی باشد ";
                                    notification.infoCenterBalloon();

                                    return;
                                }
                            }

                        }
                        else
                        {

                            if (eGroundCabels[0].Vol == 400)
                            {
                                //Is Week
                                Atend.Global.Design.frmDrawKablsho02 _frmDrawKablsho02 = new Atend.Global.Design.frmDrawKablsho02();
                                if (Application.ShowModalDialog(_frmDrawKablsho02) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //ed.WriteMessage("go for header cable draw \n");
                                    if (_frmDrawKablsho02.HeaderIsSelected == false)
                                    {
                                        Atend.Global.Acad.DrawEquips.AcDrawKablsho _AcDrawKablsho = new AcDrawKablsho();
                                        _AcDrawKablsho.Existance = Atend.Base.Acad.AcadGlobal.KablshoData.Existance;
                                        _AcDrawKablsho.UseAccess = Atend.Base.Acad.AcadGlobal.KablshoData.UseAccess;
                                        _AcDrawKablsho.eKablsho = Atend.Base.Acad.AcadGlobal.KablshoData.eKablsho;
                                        _AcDrawKablsho.ProjectCode = Atend.Base.Acad.AcadGlobal.KablshoData.ProjectCode;
                                        ObjectId HeaderOI = _AcDrawKablsho.DrawKablsho02(CurrentPoint);
                                        HeaderOis.Add(HeaderOI);
                                    }
                                    else
                                    {
                                        AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
                                        _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                        _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                        _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                        _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                        ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
                                        HeaderOis.Add(HeaderOI);
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }

                            else
                            {
                                Atend.Global.Design.frmDrawHeaderCable02 _frmDrawHeaderCabl = new Atend.Global.Design.frmDrawHeaderCable02(Convert.ToInt32(eGroundCabels[0].Vol), eGroundCabels[0].CrossSectionArea);
                                if (Application.ShowModalDialog(_frmDrawHeaderCabl) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //ed.WriteMessage("go for header cable draw \n");
                                    AcDrawHeaderCabel _AcDrawHeaderCabel = new AcDrawHeaderCabel();
                                    _AcDrawHeaderCabel.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                    _AcDrawHeaderCabel.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                    _AcDrawHeaderCabel.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                    _AcDrawHeaderCabel.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                    ObjectId HeaderOI = _AcDrawHeaderCabel.DrawHeaderCabel(CurrentPoint);
                                    HeaderOis.Add(HeaderOI);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            } while (bSuccess && !bComplete);
            if (bComplete)
            {
                Polyline CableEntity = jig.GetEntity() as Polyline;
                //ed.WriteMessage("HeaderOis:{0} \n", HeaderOis.Count);
                if (CableEntity != null && HeaderOis.Count == 2)
                {
                    Atend.Global.Design.frmDrawBranchCable FDB = new Atend.Global.Design.frmDrawBranchCable();
                    FDB.Length = Math.Round(CableEntity.Length, 3);
                    FDB.OperationBulk = Math.Round(CableEntity.Length, 3);
                    //FDB.SelectedGroundCableTipCode = eGroundCabelTip.Code; 
                    if (Application.ShowModalDialog(FDB) == System.Windows.Forms.DialogResult.OK)
                    {
                        _dBranch.Lenght = FDB.Length;
                        OperationBulk = FDB.OperationBulk;
                        _dBranch.Number = "CABLE";
                        _dBranch.LeftNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(HeaderOis[0]).NodeCode);
                        _dBranch.RightNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(HeaderOis[1]).NodeCode);
                        if (SaveGroundCabelData02())
                        {
                            //CableEntity.LinetypeId=

                            string LayerName = "";
                            foreach (Atend.Base.Equipment.EGroundCabel gb in eGroundCabels)
                            {
                                if (gb.XCode == eGroundCabelTip.PhaseProductXCode)
                                {
                                    if (gb.Vol == 400)
                                    {
                                        LayerName = Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString();
                                    }
                                    else
                                    {
                                        LayerName = Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString();
                                    }
                                }
                            }

                            ObjectId LineOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(CableEntity, LayerName);
                            Atend.Base.Acad.AT_INFO CabelInfo = new Atend.Base.Acad.AT_INFO(LineOI);
                            CabelInfo.ParentCode = "";
                            CabelInfo.NodeCode = _dBranch.Code.ToString();
                            CabelInfo.ProductCode = _dBranch.ProductCode;
                            CabelInfo.NodeType = (int)Atend.Control.Enum.ProductType.GroundCabel;
                            CabelInfo.Insert();

                            string comment = string.Format("{0} : {1}m", eGroundCabelTip.Description, Math.Round(_dBranch.Lenght, 2));
                            ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, CableEntity.GetPoint3dAt(0), CableEntity.GetPoint3dAt(CableEntity.NumberOfVertices - 1), MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                            Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                            TextInfo.ParentCode = _dBranch.Code.ToString();
                            TextInfo.NodeCode = "";
                            TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                            TextInfo.ProductCode = 0;
                            TextInfo.Insert();

                            Atend.Base.Acad.AT_SUB LineSub = new Atend.Base.Acad.AT_SUB(LineOI);
                            HeaderOis.Add(TextOi);
                            LineSub.SubIdCollection = HeaderOis;
                            LineSub.Insert();

                            foreach (ObjectId Hoi in HeaderOis)
                            {
                                //ed.WriteMessage("HOI:{0}\n",Hoi);
                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(LineOI, Hoi);

                            }
                        }
                    }
                }
            }
        } //current

        //private bool SaveGroundCabelData()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("~~~~~~~~~~ Start Save Branch ~~~~~~~~~~~~");
        //    OleDbTransaction aTransaction;
        //    OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    try
        //    {
        //        aConnection.Open();
        //        aTransaction = aConnection.BeginTransaction();
        //        try
        //        {
        //            if (_UseAccess)
        //            {

        //                ed.WriteMessage("** Code :{0} ** \n", _dBranch.Code);
        //                ed.WriteMessage("** LeftNodeCode :{0} ** \n", _dBranch.LeftNodeCode);
        //                ed.WriteMessage("** RightNodeCode :{0} ** \n", _dBranch.RightNodeCode);
        //                ed.WriteMessage("** Length :{0} ** \n", _dBranch.Lenght);
        //                ed.WriteMessage("** Number :{0} ** \n", _dBranch.Number);
        //                ed.WriteMessage("** Sag :{0} ** \n", _dBranch.Sag);
        //                ed.WriteMessage("** ProductCode :{0} ** \n", _dBranch.ProductCode);
        //                ed.WriteMessage("** ProductType :{0} ** \n", _dBranch.ProductType);
        //                ed.WriteMessage("** IsWeek :{0} ** \n", _dBranch.IsWeek);
        //                ed.WriteMessage("** IsExist :{0} ** \n", _dBranch.IsExist);
        //                ed.WriteMessage("** ProjectCode :{0} ** \n", _dBranch.ProjectCode);

        //                _dBranch.ProductCode = eGroundCabelTip.Code;
        //                _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //                _dBranch.Sag = 0;
        //                _dBranch.ProjectCode = ProjectCode;
        //                _dBranch.Number = eGroundCabelTip.Description;
        //                _dBranch.IsExist = Existance;

        //                if (!_dBranch.AccessInsert(aTransaction, aConnection))
        //                {
        //                    throw new System.Exception("dBranch.AccessInsert failed");
        //                }
        //            }
        //            else
        //            {
        //                //ArrayList l1 = new ArrayList();
        //                //ed.WriteMessage("--{0}--\n",eGroundCabels.Count);
        //                foreach (Atend.Base.Equipment.EGroundCabel _EGroundCabel in eGroundCabels)
        //                {
        //                    //Atend.Base.Equipment.EConductor co = Atend.Base.Equipment.EConductor.AccessSelectByXCode(econductor.XCode);
        //                    //l1.Add(co.Code);
        //                    //if (co.Code == -1)
        //                    //{
        //                    //ed.WriteMessage("go for ground save in access \n");
        //                    if (_EGroundCabel.AccessInsert(aTransaction, aConnection, true, true))
        //                    {
        //                        //ConductorCodes.Add(econductor.Code);
        //                        //ed.WriteMessage("***\n");
        //                    }
        //                    else
        //                    {
        //                        throw new System.Exception("_EGroundCabel.AccessInsert failed");
        //                    }

        //                    //if (!Atend.Base.Equipment.EOperation.SentFromLocalToAccess(econductor.XCode, (int)Atend.Control.Enum.ProductType.Conductor, econductor.Code, aTransaction, aConnection))
        //                    //{
        //                    //    throw new System.Exception("operation failed");
        //                    //}


        //                    //}
        //                }

        //                //if (eConductorTip.AccessInsert(Convert.ToInt32(ConductorCodes[0]), Convert.ToInt32(ConductorCodes[1]), Convert.ToInt32(ConductorCodes[2]), aTransaction, aConnection))
        //                //if (Convert.ToInt32(l1[0].ToString()) == -1)
        //                //    eConductorTip.PhaseProductCode = eConductors[0].Code;
        //                //else
        //                //    eConductorTip.PhaseProductCode = Convert.ToInt32(l1[0].ToString());

        //                //if (Convert.ToInt32(l1[1].ToString()) == -1)
        //                //    eConductorTip.NeutralProductCode = eConductors[1].Code;
        //                //else
        //                //    eConductorTip.NeutralProductCode = Convert.ToInt32(l1[1].ToString());

        //                //if (Convert.ToInt32(l1[2].ToString()) == -1)
        //                //    eConductorTip.NightProductCode = eConductors[2].Code;
        //                //else
        //                //    eConductorTip.NightProductCode = Convert.ToInt32(l1[2].ToString());

        //                eGroundCabelTip.PhaseProductCode = eGroundCabels[0].Code;
        //                eGroundCabelTip.NeutralProductCode = eGroundCabels[1].Code;

        //                //ed.WriteMessage("go for ground tip save in access \n");
        //                if (eGroundCabelTip.AccessInsert(aTransaction, aConnection, true, true))
        //                {
        //                    _dBranch.Number = eGroundCabelTip.Description;
        //                    _dBranch.ProductCode = eGroundCabelTip.Code;
        //                    _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //                    _dBranch.ProjectCode = ProjectCode;
        //                    _dBranch.IsExist = Existance;

        //                }
        //                else
        //                {
        //                    throw new System.Exception("eGroundCabelTip.AccessInsert failed");
        //                }

        //                //ed.WriteMessage("go for branch save in access \n");
        //                if (_dBranch.AccessInsert(aTransaction, aConnection))
        //                {
        //                    // filnished
        //                }
        //                else
        //                {
        //                    throw new System.Exception("dBranch.AccessInsert failed");
        //                }


        //            }

        //            Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
        //            _EOperation.ProductCode = eGroundCabelTip.NeutralProductCode;
        //            _EOperation.Type = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //            _EOperation.Count = OperationBulk;
        //            if (!_EOperation.AccessUpdate(aTransaction, aConnection))
        //            {
        //                throw new System.Exception("dBranch.AccessUpdate failed");
        //            }

        //            _EOperation.ProductCode = eGroundCabelTip.PhaseProductCode;
        //            _EOperation.Type = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //            _EOperation.Count = OperationBulk;
        //            if (!_EOperation.AccessUpdate(aTransaction, aConnection))
        //            {
        //                throw new System.Exception("dBranch.AccessUpdate failed");
        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("ConductorTransaction for insert failed 02 : {0} \n", ex1.Message));
        //            aTransaction.Rollback();
        //            aConnection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error ConductorTransaction for insert failed 01 : {0} \n", ex.Message));
        //        aConnection.Close();
        //        return false;
        //    }

        //    aTransaction.Commit();
        //    aConnection.Close();
        //    //ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");
        //    return true;

        //}

        //current

        private bool SaveGroundCabelData02()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~~~~~~~ Start Save Branch ~~~~~~~~~~~~");
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {
                    if (!_UseAccess)
                    {
                        foreach (Atend.Base.Equipment.EGroundCabel _EGroundCabel in eGroundCabels)
                        {
                            if (!_EGroundCabel.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("_EGroundCabel.AccessInsert failed");
                            }
                        }

                        eGroundCabelTip.PhaseProductCode = eGroundCabels[0].Code;
                        eGroundCabelTip.NeutralProductCode = eGroundCabels[1].Code;

                        //ed.WriteMessage("go for ground tip save in access \n");
                        if (!eGroundCabelTip.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eGroundCabelTip.AccessInsert failed");
                        }

                    }

                    #region Save In DBranch

                    _dBranch.ProductCode = eGroundCabelTip.Code;
                    _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
                    _dBranch.Sag = 0;
                    _dBranch.ProjectCode = ProjectCode;
                    _dBranch.Number = eGroundCabelTip.Description;
                    _dBranch.IsExist = Existance;
                    if (eGroundCabels[0].Vol == 400)
                    {
                        _dBranch.IsWeek = true;
                    }
                    else
                    {
                        _dBranch.IsWeek = false;
                    }

                    //ed.WriteMessage("** Code :{0} ** \n", _dBranch.Code);
                    //ed.WriteMessage("** LeftNodeCode :{0} ** \n", _dBranch.LeftNodeCode);
                    //ed.WriteMessage("** RightNodeCode :{0} ** \n", _dBranch.RightNodeCode);
                    //ed.WriteMessage("** Length :{0} ** \n", _dBranch.Lenght);
                    //ed.WriteMessage("** Number :{0} ** \n", _dBranch.Number);
                    //ed.WriteMessage("** Sag :{0} ** \n", _dBranch.Sag);
                    //ed.WriteMessage("** ProductCode :{0} ** \n", _dBranch.ProductCode);
                    //ed.WriteMessage("** ProductType :{0} ** \n", _dBranch.ProductType);
                    //ed.WriteMessage("** IsWeek :{0} ** \n", _dBranch.IsWeek);
                    //ed.WriteMessage("** IsExist :{0} ** \n", _dBranch.IsExist);
                    //ed.WriteMessage("** ProjectCode :{0} ** \n", _dBranch.ProjectCode);

                    if (!_dBranch.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessInsert failed");
                    }
                    #endregion

                    #region Operation Update
                    Atend.Base.Equipment.EOperation _EOperation = new Atend.Base.Equipment.EOperation();
                    _EOperation.ProductCode = eGroundCabelTip.NeutralProductCode;
                    _EOperation.Type = (int)Atend.Control.Enum.ProductType.GroundCabel;
                    _EOperation.Count = OperationBulk;
                    if (!_EOperation.AccessUpdate(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessUpdate failed");
                    }

                    _EOperation.ProductCode = eGroundCabelTip.PhaseProductCode;
                    _EOperation.Type = (int)Atend.Control.Enum.ProductType.GroundCabel;
                    _EOperation.Count = OperationBulk;
                    if (!_EOperation.AccessUpdate(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessUpdate failed");
                    }
                    #endregion

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

            Atend.Base.Acad.AcadGlobal.GroundCableData.UseAccess = true;
            //UseAccess = true;

            #endregion


            //ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");
            return true;

        } //current

        public bool UpdateGroundCabelData(double lenght, Guid BranchGuid)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            ArrayList GroundCode = new ArrayList();
            //ed.WriteMessage("SELECTEDOBJID+{0}\n",selectedObjID);
            try
            {
                aConnection.Open();
                aTransaction = aConnection.BeginTransaction();
                try
                {
                    ////ed.WriteMessage("eSelecfKeeper.XCode={0},UseAccess={1}\n", eSelfKeeperTip.XCode, UseAccess);
                    _dBranch = Atend.Base.Design.DBranch.AccessSelectByCode(BranchGuid);
                    ////ed.WriteMessage("_DBranch.Code={0}\n", _dBranch.Code);
                    if (!UseAccess)
                    {
                        foreach (Atend.Base.Equipment.EGroundCabel eGroundCabel in eGroundCabels)
                        {
                            ////ed.WriteMessage("SelefKeeper[i].Comment={0},self.Name={1}\n",  eSelef.Comment, eSelef.Name);
                            if (!eGroundCabel.AccessInsert(aTransaction, aConnection, true, true))
                                throw new System.Exception("eGroundCabel.AccessInsert failed");
                            else
                                GroundCode.Add(eGroundCabel.Code);

                        }
                        ////ed.WriteMessage("SelfKeeperTip.Insert\n");
                        eGroundCabelTip.PhaseProductCode = Convert.ToInt32(GroundCode[0].ToString());
                        eGroundCabelTip.NeutralProductCode = Convert.ToInt32(GroundCode[1].ToString());
                        if (!eGroundCabelTip.AccessInsert(aTransaction, aConnection, true, true))
                        {
                            throw new System.Exception("eSelfKeeperTip.AccessInsert failed");
                        }
                    }
                    ////ed.WriteMessage("eSelefKeeperTip.Code={0}\n",eSelfKeeperTip.Code);
                    _dBranch.IsExist = existance;
                    _dBranch.ProductCode = eGroundCabelTip.Code;
                    _dBranch.Lenght = lenght;
                    _dBranch.ProjectCode = ProjectCode;
                    ////ed.WriteMessage("dBranch.Insert\n");
                    if (_dBranch.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjID);
                        atinfo.ProductCode = eGroundCabelTip.Code;
                        atinfo.Insert();
                        ChangeCabelComment(selectedObjID, eGroundCabelTip.Description);
                    }
                    else
                    {
                        throw new System.Exception("GroundCabelPack.AccessInsert2 failed");
                    }

                    //_dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
                    //_dBranch.Lenght = lenght;

                    //if (!_dBranch.AccessUpdateLenghtByLeftAndRightNode(aTransaction, aConnection))
                    //{
                    //    throw new System.Exception("dBranch.AccessUpdate Failed");
                    //}

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateGroundCabelData(Transaction) 01 : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateGroundCabelData 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteGroundCabelData(ObjectId ConductorOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //delete GroundCabel 
                Atend.Base.Acad.AT_INFO conductorinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ConductorOI);
                if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(conductorinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete GroundCabel\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Data ERROR GroundCabel : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteGroundCabel(ObjectId GroundCabelOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_SUB GroundCabelSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(GroundCabelOI);
                foreach (ObjectId GroundCabelSubOI in GroundCabelSub.SubIdCollection)
                {
                    Atend.Base.Acad.AT_INFO GroundCabelSubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(GroundCabelSubOI);
                    if (GroundCabelSubInfo.ParentCode != "NONE" && GroundCabelSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                    {
                        if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(GroundCabelSubOI))
                        {
                            throw new System.Exception("Error In Delete Comment\n");
                        }
                        else
                        {
                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(GroundCabelSubOI, GroundCabelOI);
                        }
                    }
                    if (GroundCabelSubInfo.ParentCode != "NONE" && (GroundCabelSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel || GroundCabelSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.KablSho))
                    {
                        Atend.Base.Acad.AT_SUB Sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(GroundCabelSubOI);
                        foreach (ObjectId SubOI in Sub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SubOI);
                            if (SubInfo.ParentCode != "NONE" && SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel && SubOI == GroundCabelOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(SubOI, GroundCabelSubOI);
                            }
                        }
                    }
                    //if (GroundCabelSubInfo.ParentCode != "NONE" && GroundCabelSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.HeaderCabel)
                    //{
                    //    Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(GroundCabelSubOI);
                    //    foreach (ObjectId oi in consolSub.SubIdCollection)
                    //    {
                    //        Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                    //        if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.GroundCabel && at_info.SelectedObjectId == GroundCabelOI)
                    //        {
                    //            if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                    //            {
                    //                throw new System.Exception("Error In Delete Arc\n");
                    //            }
                    //            else
                    //            {
                    //                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, GroundCabelSubOI);
                    //            }
                    //        }
                    //    }
                    //}
                }
                //+++
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(GroundCabelOI))
                {
                    throw new System.Exception("GRA while delete GroundCabel \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR GroundCabel : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static void ChangeCabelComment(ObjectId SelectedLineObjectID, string Text)
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

        public static void ChangeEntityText(ObjectId SelectedTextObjectID, string Text)
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
                            if (eGroundCabels[0].Vol == 400)
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
                            else
                            {
                                Atend.Global.Design.frmEdiDrawHeaderCable _frmDrawHeaderCable = new Atend.Global.Design.frmEdiDrawHeaderCable(Convert.ToInt32(eGroundCabels[0].Vol), eGroundCabels[0].CrossSectionArea);
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
                        }
                        break;
                    case Atend.Control.Enum.ProductType.Kalamp:
                        if (Atend.Control.Common.userCode != 0 && Atend.Control.Common.DesignName != "")
                        {
                            Atend.Global.Design.frmEditDrawClamp _frmDrawClamp = new Atend.Global.Design.frmEditDrawClamp(Convert.ToInt32(eGroundCabels[0].Vol));
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

    }
}
