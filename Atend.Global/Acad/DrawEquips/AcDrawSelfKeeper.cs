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

    public class AcDrawSelfKeeper
    {

        //~~~~~~~~~~~~~~~~~~ Properties ~~~~~~~~~~~~~~~~~~~~~~~~~//
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

        Atend.Base.Equipment.ESelfKeeperTip _eSelfKeeperTip;
        public Atend.Base.Equipment.ESelfKeeperTip eSelfKeeperTip
        {
            get { return _eSelfKeeperTip; }
            set { _eSelfKeeperTip = value; }
        }

        List<Atend.Base.Equipment.ESelfKeeper> _eSelfKeepers;
        public List<Atend.Base.Equipment.ESelfKeeper> eSelfKeepers
        {
            get { return _eSelfKeepers; }
            set { _eSelfKeepers = value; }
        }

        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        int projectCode;
        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        //~~~~~~~~~~~~~~~~~~ Class ~~~~~~~~~~~~~~~~~~~~~~~~~//

        class DrawSelfKeerCableJig : EntityJig
        {

            Entity ent = null;
            public Point3d CentrePoint = Point3d.Origin;

            public DrawSelfKeerCableJig()
                : base(new Polyline())
            {
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {

                JigPromptPointOptions ppo = new JigPromptPointOptions("\nSelect Point:");
                PromptPointResult ppr = prompts.AcquirePoint(ppo);
                if (ppr.Status == PromptStatus.OK)
                {
                    if (ppr.Value == CentrePoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        CentrePoint = ppr.Value;
                        return SamplerStatus.OK;
                    }
                }
                else
                {
                    return SamplerStatus.Cancel;
                }

            }

            protected override bool Update()
            {

                Polyline _pl = Entity as Polyline;
                if (_pl != null)
                {
                    if (_pl.NumberOfVertices == 2)
                    {
                        _pl.SetPointAt(_pl.NumberOfVertices - 1, new Point2d(CentrePoint.X, CentrePoint.Y));
                    }
                }

                return true;
            }

            public void AddVertex()
            {
                Polyline _pl = Entity as Polyline;
                if (_pl != null)
                {
                    //_pl.SetPointAt(1, new Point2d(EndPoint.X, EndPoint.Y));
                    _pl.AddVertexAt(_pl.NumberOfVertices, new Point2d(CentrePoint.X, CentrePoint.Y), 0, 0, 0);
                }

            }

            public void SetEndPoint(Point3d EndPoint)
            {
                Polyline _pl = Entity as Polyline;
                if (_pl != null)
                {
                    //_pl.SetPointAt(1, new Point2d(EndPoint.X, EndPoint.Y));
                    _pl.SetPointAt(_pl.NumberOfVertices - 1, new Point2d(EndPoint.X, EndPoint.Y));
                }
            }

            public void SetStartPoint(Point3d StartPoint)
            {
                Polyline _pl = Entity as Polyline;
                if (_pl != null)
                {
                    _pl.AddVertexAt(_pl.NumberOfVertices, new Point2d(StartPoint.X, StartPoint.Y), 0, 0, 0);
                }
            }

            public Entity GetEntity()
            {
                return Entity;
            }

        }

        //~~~~~~~~~~~~~~~~~~ Method ~~~~~~~~~~~~~~~~~~~~~~~~~//

        public AcDrawSelfKeeper()
        {
            _dBranch = new Atend.Base.Design.DBranch();
        }

        public void DrawSelfKeeper(Point3d StartPoint, Point3d EndPoint, ObjectId Clamp1, ObjectId Clamp2, double MyLength)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //bool Conti = true;
            //DrawSelfKeerCableJig CLJ = new DrawSelfKeerCableJig();
            //PromptResult pr;
            //ObjectIdCollection ConnectionsOIs = new ObjectIdCollection();
            //ObjectId LastConnectionPoint = ObjectId.Null;
            //AcDrawKalamp UseForOburi = null;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).CommentScale;


            Line l = new Line(StartPoint, EndPoint);
            _dBranch.Lenght = MyLength;

            if (SaveSelfKeeperData())
            {
                ObjectId NewDrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());

                ////ed.WriteMessage("oi:{0} , oi:{1}", ConnectionsOIs[0], ConnectionsOIs[1]);

                Atend.Base.Acad.AT_INFO SelfKeeperInfo = new Atend.Base.Acad.AT_INFO(NewDrawnLine);
                SelfKeeperInfo.ParentCode = "";
                SelfKeeperInfo.NodeCode = _dBranch.Code.ToString();
                SelfKeeperInfo.NodeType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                SelfKeeperInfo.ProductCode = _dBranch.ProductCode;
                SelfKeeperInfo.Insert();
                ////ed.WriteMessage("11\n");

                string comment = string.Format("{0} {1}m", _dBranch.Number, Math.Round(_dBranch.Lenght, 2));
                ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, l.StartPoint, l.EndPoint, MyCommentScale),
                    Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                Atend.Base.Acad.AT_INFO CommentInfo = new Atend.Base.Acad.AT_INFO(txtOI);
                CommentInfo.ParentCode = _dBranch.Code.ToString();
                CommentInfo.NodeCode = "";
                CommentInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                CommentInfo.ProductCode = 0;
                CommentInfo.Insert();


                Atend.Base.Acad.AT_SUB SelfKeeperSub = new Atend.Base.Acad.AT_SUB(NewDrawnLine);
                SelfKeeperSub.SubIdCollection.Add(txtOI);
                SelfKeeperSub.SubIdCollection.Add(Clamp1);
                SelfKeeperSub.SubIdCollection.Add(Clamp2);
                SelfKeeperSub.Insert();


                Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, Clamp1);
                Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, Clamp2);

            }


        }

        //public void DrawSelfKeeper(Point3d StartPoint, Point3d EndPoint, ObjectId StartNode, ObjectId EndPoint, double Length)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //bool Conti = true;
        //    //DrawSelfKeerCableJig CLJ = new DrawSelfKeerCableJig();
        //    PromptResult pr;
        //    ObjectIdCollection ConnectionsOIs = new ObjectIdCollection();
        //    ObjectId LastConnectionPoint = ObjectId.Null;
        //    AcDrawKalamp UseForOburi = null;

        //    double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).Scale;
        //    double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).CommentScale;


        //    //while (Conti)
        //    //{
        //    //Polyline pl = CLJ.GetEntity() as Polyline;
        //    if (pl != null && pl.NumberOfVertices == 0)
        //    {
        //        pr = ed.Drag(CLJ);
        //        if (pr.Status == PromptStatus.OK)
        //        {

        //            //-------------
        //            bool CanAcceptPoint = false;
        //            Point3d AcceptedCenterPoint = Point3d.Origin;
        //            System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
        //            System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
        //            if (PointContainerList.Rows.Count == 1 && Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole)
        //            {
        //                //ConnectionsOIs.Add(
        //                ///////////AcDrawConnectionPoint.DrawConnectionPoint(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                ///////////AcDrawHeaderCabel.DrawHeaderCable(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));

        //                Atend.Design.frmDrawClampOrHeader02 _drawCH = new Atend.Design.frmDrawClampOrHeader02();
        //                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    if (_drawCH.UseClamp)
        //                    {
        //                        AcDrawKalamp _drawK = new AcDrawKalamp();
        //                        _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                        _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                        _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                        ObjectId newKalampOi = _drawK.DrawKalamp(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                        ////ed.WriteMessage("New oi {0} \n", newKalampOi);
        //                        if (newKalampOi == ObjectId.Null)
        //                        {
        //                            return;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
        //                        _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                        _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                        _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                        ObjectId newHeaderOi = _DrawH.DrawHeaderCabel(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                        ////ed.WriteMessage("New oi {0} \n", newHeaderOi);
        //                        if (newHeaderOi == ObjectId.Null)
        //                        {
        //                            return;
        //                        }

        //                    }
        //                }


        //                //);
        //                PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
        //            }

        //            foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //            {
        //                DataRow[] drs = dt.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                if (drs.Length != 0)
        //                {
        //                    CanAcceptPoint = true;
        //                    if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode != "")
        //                    {
        //                        _dBranch.LeftNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode);
        //                    }
        //                    else
        //                    {
        //                        _dBranch.LeftNodeCode = new Guid();
        //                    }
        //                    string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                    string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                    string y = StrTemp[1];
        //                    string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                    AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                    ConnectionsOIs.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                }
        //            }

        //            if (CanAcceptPoint)
        //            {
        //                ////ed.WriteMessage("Accepter Center : {0} \n", AcceptedCenterPoint);
        //                CLJ.SetStartPoint(AcceptedCenterPoint);
        //                CLJ.AddVertex();
        //            }
        //            //-------------

        //        }
        //        else
        //        {
        //            Conti = false;
        //        }
        //    }
        //    else if (pl != null && pl.NumberOfVertices != 1)
        //    {

        //        pr = ed.Drag(CLJ);
        //        if (pr.Status == PromptStatus.OK)
        //        {

        //            Point3d LastPoint = CLJ.CentrePoint;

        //            //-------------
        //            bool CanAcceptPoint = false;
        //            Point3d AcceptedCenterPoint = Point3d.Origin;
        //            System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
        //            System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
        //            if (PointContainerList.Rows.Count == 1 && Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole)
        //            {


        //                if (UseForOburi == null)
        //                {
        //                    Atend.Design.frmDrawCalamp02 _frmDrawClamp = new Atend.Design.frmDrawCalamp02();
        //                    if (Application.ShowModalDialog(_frmDrawClamp) == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        UseForOburi = new AcDrawKalamp();
        //                        UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                        UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                        UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }

        //                }
        //                else
        //                {

        //                    //ConnectionsOIs.Add(
        //                    //////LastConnectionPoint = AcDrawConnectionPoint.DrawConnectionPoint(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                    UseForOburi = new AcDrawKalamp();
        //                    UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                    UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                    UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                }
        //                LastConnectionPoint = UseForOburi.DrawKalamp(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                //);
        //                PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
        //            }

        //            foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //            {
        //                DataRow[] drs = dt.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                if (drs.Length != 0)
        //                {
        //                    CanAcceptPoint = true;
        //                    if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode != "")
        //                    {
        //                        _dBranch.RightNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode);
        //                    }
        //                    else
        //                    {
        //                        _dBranch.RightNodeCode = new Guid();
        //                    }
        //                    string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                    string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                    string y = StrTemp[1];
        //                    string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                    AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                    ConnectionsOIs.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                }
        //            }

        //            if (CanAcceptPoint)
        //            {

        //                CLJ.SetEndPoint(AcceptedCenterPoint);

        //                //~~~~~~~~~~~~~~~~~~~~~~~~~
        //                #region Save one  line here

        //                Polyline DrawnLine = CLJ.GetEntity() as Polyline;
        //                if (DrawnLine != null)
        //                {
        //                    Line l = new Line(DrawnLine.GetPoint3dAt(0), DrawnLine.GetPoint3dAt(1));

        //                    Atend.Design.frmDrawBranch drawBranch = new Atend.Design.frmDrawBranch();
        //                    drawBranch.Length = l.Length;
        //                    if (drawBranch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        _dBranch.Lenght = drawBranch.Length;

        //                        if (SaveSelfKeeperData())
        //                        {
        //                            ObjectId NewDrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());

        //                            ////ed.WriteMessage("oi:{0} , oi:{1}", ConnectionsOIs[0], ConnectionsOIs[1]);

        //                            Atend.Base.Acad.AT_INFO SelfKeeperInfo = new Atend.Base.Acad.AT_INFO(NewDrawnLine);
        //                            SelfKeeperInfo.ParentCode = "";
        //                            SelfKeeperInfo.NodeCode = _dBranch.Code.ToString();
        //                            SelfKeeperInfo.NodeType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
        //                            SelfKeeperInfo.ProductCode = _dBranch.ProductCode;
        //                            SelfKeeperInfo.Insert();
        //                            ////ed.WriteMessage("11\n");

        //                            string comment = string.Format("{0} {1}m", _dBranch.Number, Math.Round(_dBranch.Lenght, 2));
        //                            ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, l.StartPoint, l.EndPoint, MyCommentScale),
        //                                Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //                            Atend.Base.Acad.AT_INFO CommentInfo = new Atend.Base.Acad.AT_INFO(txtOI);
        //                            CommentInfo.ParentCode = _dBranch.Code.ToString();
        //                            CommentInfo.NodeCode = "";
        //                            CommentInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        //                            CommentInfo.ProductCode = 0;
        //                            CommentInfo.Insert();


        //                            Atend.Base.Acad.AT_SUB SelfKeeperSub = new Atend.Base.Acad.AT_SUB(NewDrawnLine);
        //                            SelfKeeperSub.SubIdCollection = ConnectionsOIs;
        //                            SelfKeeperSub.Insert();
        //                            ////ed.WriteMessage("12\n");

        //                            foreach (ObjectId oi in ConnectionsOIs)
        //                            {
        //                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, oi);
        //                            }
        //                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(txtOI, NewDrawnLine);
        //                        }

        //                    }

        //                }

        //                #endregion
        //                //~~~~~~~~~~~~~~~~~~~~~~~~~
        //                ObjectId FirstConnection = ConnectionsOIs[1];
        //                ConnectionsOIs.Clear();
        //                ConnectionsOIs.Add(FirstConnection);


        //                CLJ = new DrawSelfKeerCableJig();
        //                CLJ.SetStartPoint(AcceptedCenterPoint);
        //                CLJ.AddVertex();

        //            }
        //            //-------------

        //        }
        //        else
        //        {
        //            Conti = false;
        //            ObjectId PoleoI = ObjectId.Null;
        //            if (LastConnectionPoint != ObjectId.Null)
        //            {


        //                Atend.Base.Acad.AT_SUB ConnectionSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
        //                foreach (ObjectId oi in ConnectionSub.SubIdCollection)
        //                {
        //                    Atend.Base.Acad.AT_INFO PoleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                    ////ed.WriteMessage("Type:{0}\n", PoleInfo.NodeType);
        //                    if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
        //                    {
        //                        PoleoI = oi;
        //                    }
        //                }

        //                if (PoleoI != ObjectId.Null)
        //                {
        //                    ////////////////ObjectId HeaderCableOi = AcDrawHeaderCabel.DrawHeaderCable(Atend.Global.Acad.UAcad.CenterOfEntity(
        //                    ////////////////       Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)),
        //                    ////////////////       PoleoI);
        //                    ObjectId HeaderCableOi = ObjectId.Null;
        //                    Atend.Design.frmDrawClampOrHeader02 _drawCH = new Atend.Design.frmDrawClampOrHeader02();
        //                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        if (_drawCH.UseClamp)
        //                        {
        //                            AcDrawKalamp _drawK = new AcDrawKalamp();
        //                            _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                            _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                            _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                            HeaderCableOi = _drawK.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(
        //                           Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)),
        //                           PoleoI);
        //                            if (HeaderCableOi == ObjectId.Null)
        //                            {
        //                                return;
        //                            }

        //                        }
        //                        else
        //                        {
        //                            AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
        //                            _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                            _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                            _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                            HeaderCableOi = _DrawH.DrawHeaderCabel(Atend.Global.Acad.UAcad.CenterOfEntity(
        //                    Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)),
        //                    PoleoI);
        //                            if (HeaderCableOi == ObjectId.Null)
        //                            {
        //                                return;
        //                            }

        //                        }
        //                    }


        //                    foreach (ObjectId oi in ConnectionSub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_SUB oiSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                        foreach (ObjectId oii in oiSub.SubIdCollection)
        //                        {
        //                            if (oii == LastConnectionPoint)
        //                            {
        //                                ////ed.WriteMessage("~~~~~~~~~~\n");
        //                                ////ed.WriteMessage("REM \n");

        //                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oii, oi);
        //                                ////ed.WriteMessage("Ins \n");


        //                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderCableOi, oi);
        //                                ////ed.WriteMessage("~~~~~~~~~~\n");
        //                            }
        //                        }
        //                    }




        //                    ConnectionSub.SelectedObjectId = HeaderCableOi;
        //                    ConnectionSub.Insert();

        //                    // erase for database before screen
        //                    Atend.Base.Acad.AT_INFO information = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LastConnectionPoint);
        //                    if (information.ParentCode != "NONE" && information.NodeCode != "")
        //                    {
        //                        Atend.Base.Design.DPackage.AccessDelete(new Guid(information.NodeCode));
        //                    }

        //                    Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(LastConnectionPoint);
        //                    LastConnectionPoint = ObjectId.Null;
        //                }

        //            }

        //        }

        //        ////////////// form show


        //    }
        //    else
        //    {
        //        Conti = false;
        //    }

        //    //  }


        //}

        public void DrawSelfKeeper()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool Conti = true;
            DrawSelfKeerCableJig CLJ = new DrawSelfKeerCableJig();
            PromptResult pr;
            ObjectIdCollection ConnectionsOIs = new ObjectIdCollection();
            ObjectId LastConnectionPoint = ObjectId.Null;
            AcDrawKalamp UseForOburi = null;

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).CommentScale;


            while (Conti)
            {
                Polyline pl = CLJ.GetEntity() as Polyline;
                if (pl != null && pl.NumberOfVertices == 0)
                {
                    pr = ed.Drag(CLJ);
                    if (pr.Status == PromptStatus.OK)
                    {

                        //-------------
                        bool CanAcceptPoint = false;
                        Point3d AcceptedCenterPoint = Point3d.Origin;
                        System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
                        if (PointContainerList.Rows.Count == 1 && (Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole) || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.PoleTip)
                        {
                            Atend.Global.Design.frmDrawCalamp02 _drawCH = new Atend.Global.Design.frmDrawCalamp02(0);
                            if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
                            {
                                //if (_drawCH.UseClamp)
                                //{
                                AcDrawKalamp _drawK = new AcDrawKalamp();
                                _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                ObjectId newKalampOi = _drawK.DrawKalamp(CLJ.CentrePoint, new ObjectId(new IntPtr(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"]))));
                                ////ed.WriteMessage("New oi {0} \n", newKalampOi);
                                if (newKalampOi == ObjectId.Null)
                                {
                                    return;
                                }

                                //}
                                //else
                                //{
                                //    AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
                                //    _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                //    _DrawH.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
                                //    _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                //    _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                //    ObjectId newHeaderOi = _DrawH.DrawHeaderCabel(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
                                //    ////ed.WriteMessage("New oi {0} \n", newHeaderOi);
                                //    if (newHeaderOi == ObjectId.Null)
                                //    {
                                //        return;
                                //    }

                                //}
                            }
                            else
                            {
                                ed.WriteMessage("do nothing \n");
                            }
                            PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
                        }

                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = dt.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                CanAcceptPoint = true;
                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode != "")
                                {
                                    _dBranch.LeftNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode);
                                }
                                else
                                {
                                    _dBranch.LeftNodeCode = new Guid();
                                }
                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                ConnectionsOIs.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                            }
                        }

                        if (CanAcceptPoint)
                        {
                            ////ed.WriteMessage("Accepter Center : {0} \n", AcceptedCenterPoint);
                            CLJ.SetStartPoint(AcceptedCenterPoint);
                            CLJ.AddVertex();
                        }
                    }
                    else
                    {
                        Conti = false;
                    }
                }
                else if (pl != null && pl.NumberOfVertices != 1)
                {

                    pr = ed.Drag(CLJ);
                    if (pr.Status == PromptStatus.OK)
                    {

                        Point3d LastPoint = CLJ.CentrePoint;

                        //-------------
                        bool CanAcceptPoint = false;
                        Point3d AcceptedCenterPoint = Point3d.Origin;
                        System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
                        if (PointContainerList.Rows.Count == 1 && (Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole))
                        {


                            if (UseForOburi == null)
                            {
                                Atend.Global.Design.frmDrawCalamp02 _frmDrawClamp = new Atend.Global.Design.frmDrawCalamp02(0);
                                if (Application.ShowModalDialog(_frmDrawClamp) == System.Windows.Forms.DialogResult.OK)
                                {
                                    UseForOburi = new AcDrawKalamp();
                                    UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                }
                                else
                                {
                                    return;
                                }

                            }
                            else
                            {

                                //ConnectionsOIs.Add(
                                //////LastConnectionPoint = AcDrawConnectionPoint.DrawConnectionPoint(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
                                UseForOburi = new AcDrawKalamp();
                                UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                            }
                            LastConnectionPoint = UseForOburi.DrawKalamp(CLJ.CentrePoint, new ObjectId(new IntPtr(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"]))));
                            //);
                            PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
                        }

                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = dt.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                CanAcceptPoint = true;
                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode != "")
                                {
                                    _dBranch.RightNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode);
                                }
                                else
                                {
                                    _dBranch.RightNodeCode = new Guid();
                                }
                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                ConnectionsOIs.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                            }
                        }

                        if (CanAcceptPoint)
                        {

                            CLJ.SetEndPoint(AcceptedCenterPoint);

                            //~~~~~~~~~~~~~~~~~~~~~~~~~
                            #region Save one  line here

                            Polyline DrawnLine = CLJ.GetEntity() as Polyline;
                            if (DrawnLine != null)
                            {
                                Line l = new Line(DrawnLine.GetPoint3dAt(0), DrawnLine.GetPoint3dAt(1));

                                Atend.Global.Design.frmDrawBranch drawBranch = new Atend.Global.Design.frmDrawBranch(_dBranch.RightNodeCode, _dBranch.LeftNodeCode);
                                drawBranch.Length = l.Length;
                                if (drawBranch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    _dBranch.Lenght = drawBranch.Length;

                                    if (SaveSelfKeeperData())
                                    {
                                        ObjectId NewDrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());

                                        ////ed.WriteMessage("oi:{0} , oi:{1}", ConnectionsOIs[0], ConnectionsOIs[1]);

                                        Atend.Base.Acad.AT_INFO SelfKeeperInfo = new Atend.Base.Acad.AT_INFO(NewDrawnLine);
                                        SelfKeeperInfo.ParentCode = "";
                                        SelfKeeperInfo.NodeCode = _dBranch.Code.ToString();
                                        SelfKeeperInfo.NodeType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                                        SelfKeeperInfo.ProductCode = _dBranch.ProductCode;
                                        SelfKeeperInfo.Insert();
                                        ////ed.WriteMessage("11\n");

                                        string comment = string.Format("{0} {1}m", _dBranch.Number, Math.Round(_dBranch.Lenght, 2));
                                        ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, l.StartPoint, l.EndPoint, MyCommentScale),
                                            Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                        Atend.Base.Acad.AT_INFO CommentInfo = new Atend.Base.Acad.AT_INFO(txtOI);
                                        CommentInfo.ParentCode = _dBranch.Code.ToString();
                                        CommentInfo.NodeCode = "";
                                        CommentInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                        CommentInfo.ProductCode = 0;
                                        CommentInfo.Insert();


                                        Atend.Base.Acad.AT_SUB SelfKeeperSub = new Atend.Base.Acad.AT_SUB(NewDrawnLine);
                                        SelfKeeperSub.SubIdCollection = ConnectionsOIs;
                                        SelfKeeperSub.Insert();
                                        ////ed.WriteMessage("12\n");

                                        foreach (ObjectId oi in ConnectionsOIs)
                                        {
                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, oi);
                                        }
                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(txtOI, NewDrawnLine);
                                    }

                                }

                            }

                            #endregion
                            //~~~~~~~~~~~~~~~~~~~~~~~~~
                            ObjectId FirstConnection = ConnectionsOIs[1];
                            ConnectionsOIs.Clear();
                            ConnectionsOIs.Add(FirstConnection);


                            CLJ = new DrawSelfKeerCableJig();
                            CLJ.SetStartPoint(AcceptedCenterPoint);
                            CLJ.AddVertex();

                        }
                        //-------------

                    }
                    else
                    {
                        Conti = false;
                        ObjectId PoleoI = ObjectId.Null;
                        if (LastConnectionPoint != ObjectId.Null)
                        {


                            Atend.Base.Acad.AT_SUB ConnectionSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
                            foreach (ObjectId oi in ConnectionSub.SubIdCollection)
                            {
                                Atend.Base.Acad.AT_INFO PoleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                                if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
                                {
                                    PoleoI = oi;
                                }
                            }

                            if (PoleoI != ObjectId.Null)
                            {
                                ObjectId HeaderCableOi = ObjectId.Null;
                                Atend.Global.Design.frmDrawCalamp02 _drawCH = new Atend.Global.Design.frmDrawCalamp02(0);
                                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //if (_drawCH.UseClamp)
                                    //{
                                    AcDrawKalamp _drawK = new AcDrawKalamp();
                                    _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                    HeaderCableOi = _drawK.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)), PoleoI);
                                    if (HeaderCableOi == ObjectId.Null)
                                    {
                                        return;
                                    }

                                    //}
                                    //else
                                    //{
                                    //    AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
                                    //    _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
                                    //    _DrawH.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    //    _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
                                    //    _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
                                    //    HeaderCableOi = _DrawH.DrawHeaderCabel(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)), PoleoI);
                                    //    if (HeaderCableOi == ObjectId.Null)
                                    //    {
                                    //        return;
                                    //    }

                                    //}
                                }


                                foreach (ObjectId oi in ConnectionSub.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_SUB oiSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                                    foreach (ObjectId oii in oiSub.SubIdCollection)
                                    {
                                        if (oii == LastConnectionPoint)
                                        {
                                            ////ed.WriteMessage("~~~~~~~~~~\n");
                                            ////ed.WriteMessage("REM \n");

                                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oii, oi);
                                            ////ed.WriteMessage("Ins \n");


                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderCableOi, oi);
                                            ////ed.WriteMessage("~~~~~~~~~~\n");
                                        }
                                    }
                                }

                                ConnectionSub.SelectedObjectId = HeaderCableOi;
                                ConnectionSub.Insert();

                                // erase for database before screen
                                Atend.Base.Acad.AT_INFO information = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LastConnectionPoint);
                                if (information.ParentCode != "NONE" && information.NodeCode != "")
                                {
                                    Atend.Base.Design.DPackage.AccessDelete(new Guid(information.NodeCode));
                                }

                                Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(LastConnectionPoint);
                                LastConnectionPoint = ObjectId.Null;
                            }
                        }
                    }
                }
                else
                {
                    Conti = false;
                }

            }


        }//دیگه استفاده نمی شود

        public void DrawSelfKeeper02()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool Conti = true;
            DrawSelfKeerCableJig CLJ = new DrawSelfKeerCableJig();
            PromptResult pr;
            ObjectIdCollection ConnectionsOIs = new ObjectIdCollection();
            ObjectId LastConnectionPoint = ObjectId.Null;
            AcDrawKalamp UseForOburi = null;

            System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).CommentScale;


            while (Conti)
            {
                Polyline pl = CLJ.GetEntity() as Polyline;
                if (pl != null && pl.NumberOfVertices == 0)
                {
                    pr = ed.Drag(CLJ);
                    if (pr.Status == PromptStatus.OK)
                    {

                        //-------------
                        bool CanAcceptPoint = false;
                        Point3d AcceptedCenterPoint = Point3d.Origin;
                        //System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
                        ed.WriteMessage("PointContainerList.Rows.Count:{0} \n", PointContainerList.Rows.Count);
                        if (PointContainerList.Rows.Count >= 1)
                        {
                            if ((Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole) || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.PoleTip)
                            {
                                Atend.Global.Design.frmDrawCalamp02 _drawCH = new Atend.Global.Design.frmDrawCalamp02(0);
                                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
                                {
                                    AcDrawKalamp _drawK = new AcDrawKalamp();
                                    _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                    ObjectId newKalampOi = _drawK.DrawKalamp(CLJ.CentrePoint, new ObjectId(new IntPtr(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"]))));
                                    ////ed.WriteMessage("New oi {0} \n", newKalampOi);
                                    if (newKalampOi == ObjectId.Null)
                                    {
                                        return;
                                    }

                                }
                                else
                                {
                                    ed.WriteMessage("do nothing \n");
                                    return;
                                }
                            }
                            PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
                        }
                        else
                        {
                            return;
                        }

                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                CanAcceptPoint = true;
                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode != "")
                                {
                                    _dBranch.LeftNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode);
                                }
                                else
                                {
                                    _dBranch.LeftNodeCode = new Guid();
                                }
                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                ConnectionsOIs.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                            }
                        }

                        if (CanAcceptPoint)
                        {
                            ////ed.WriteMessage("Accepter Center : {0} \n", AcceptedCenterPoint);
                            CLJ.SetStartPoint(AcceptedCenterPoint);
                            CLJ.AddVertex();
                        }
                    }
                    else
                    {
                        Conti = false;
                    }
                }
                else if (pl != null && pl.NumberOfVertices != 1)
                {

                    pr = ed.Drag(CLJ);
                    if (pr.Status == PromptStatus.OK)
                    {

                        Point3d LastPoint = CLJ.CentrePoint;
                        bool CanAcceptPoint = false;
                        Point3d AcceptedCenterPoint = Point3d.Origin;
                        //System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
                        System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
                        if (PointContainerList.Rows.Count == 1 && (Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.PoleTip))
                        {
                            if (UseForOburi == null)
                            {
                                Atend.Global.Design.frmDrawCalamp02 _frmDrawClamp = new Atend.Global.Design.frmDrawCalamp02(0);
                                if (Application.ShowModalDialog(_frmDrawClamp) == System.Windows.Forms.DialogResult.OK)
                                {
                                    UseForOburi = new AcDrawKalamp();
                                    UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                }
                                else
                                {
                                    return;
                                }

                            }
                            else
                            {

                                UseForOburi = new AcDrawKalamp();
                                UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                            }
                            LastConnectionPoint = UseForOburi.DrawKalamp(CLJ.CentrePoint, new ObjectId(new IntPtr(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"]))));
                            PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
                        }
                        else
                        {
                            return;
                        }

                        foreach (System.Data.DataRow dr in PointContainerList.Rows)
                        {
                            DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
                            if (drs.Length != 0)
                            {
                                CanAcceptPoint = true;
                                if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode != "")
                                {
                                    _dBranch.RightNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"])))).ParentCode);
                                }
                                else
                                {
                                    _dBranch.RightNodeCode = new Guid();
                                }
                                string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
                                string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
                                string y = StrTemp[1];
                                string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
                                AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
                                ConnectionsOIs.Add(new ObjectId(new IntPtr(Convert.ToInt32(dr["ObjectId"]))));
                            }
                        }

                        if (CanAcceptPoint)
                        {

                            CLJ.SetEndPoint(AcceptedCenterPoint);

                            //~~~~~~~~~~~~~~~~~~~~~~~~~
                            #region Save one  line here

                            Polyline DrawnLine = CLJ.GetEntity() as Polyline;
                            if (DrawnLine != null)
                            {
                                Line l = new Line(DrawnLine.GetPoint3dAt(0), DrawnLine.GetPoint3dAt(1));

                                Atend.Global.Design.frmDrawBranch drawBranch = new Atend.Global.Design.frmDrawBranch(_dBranch.RightNodeCode, _dBranch.LeftNodeCode);
                                drawBranch.Length = l.Length;
                                if (drawBranch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    _dBranch.Lenght = drawBranch.Length;

                                    if (SaveSelfKeeperData())
                                    {
                                        ObjectId NewDrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());

                                        ////ed.WriteMessage("oi:{0} , oi:{1}", ConnectionsOIs[0], ConnectionsOIs[1]);

                                        Atend.Base.Acad.AT_INFO SelfKeeperInfo = new Atend.Base.Acad.AT_INFO(NewDrawnLine);
                                        SelfKeeperInfo.ParentCode = "";
                                        SelfKeeperInfo.NodeCode = _dBranch.Code.ToString();
                                        SelfKeeperInfo.NodeType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                                        SelfKeeperInfo.ProductCode = _dBranch.ProductCode;
                                        SelfKeeperInfo.Insert();
                                        ////ed.WriteMessage("11\n");

                                        string comment = string.Format("{0} {1}m", _dBranch.Number, Math.Round(_dBranch.Lenght, 2));
                                        ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, l.StartPoint, l.EndPoint, MyCommentScale),
                                            Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                        Atend.Base.Acad.AT_INFO CommentInfo = new Atend.Base.Acad.AT_INFO(txtOI);
                                        CommentInfo.ParentCode = _dBranch.Code.ToString();
                                        CommentInfo.NodeCode = "";
                                        CommentInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                                        CommentInfo.ProductCode = 0;
                                        CommentInfo.Insert();


                                        Atend.Base.Acad.AT_SUB SelfKeeperSub = new Atend.Base.Acad.AT_SUB(NewDrawnLine);
                                        SelfKeeperSub.SubIdCollection = ConnectionsOIs;
                                        SelfKeeperSub.Insert();
                                        ////ed.WriteMessage("12\n");

                                        foreach (ObjectId oi in ConnectionsOIs)
                                        {
                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, oi);
                                        }
                                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(txtOI, NewDrawnLine);
                                    }

                                }

                            }

                            #endregion
                            //~~~~~~~~~~~~~~~~~~~~~~~~~
                            ObjectId FirstConnection = ConnectionsOIs[1];
                            ConnectionsOIs.Clear();
                            ConnectionsOIs.Add(FirstConnection);


                            CLJ = new DrawSelfKeerCableJig();
                            CLJ.SetStartPoint(AcceptedCenterPoint);
                            CLJ.AddVertex();

                        }
                        //-------------

                    }
                    else
                    {
                        Conti = false;
                        ObjectId PoleoI = ObjectId.Null;
                        if (LastConnectionPoint != ObjectId.Null)
                        {

                            Atend.Base.Acad.AT_SUB ConnectionSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
                            foreach (ObjectId oi in ConnectionSub.SubIdCollection)
                            {
                                Atend.Base.Acad.AT_INFO PoleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                                if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
                                {
                                    PoleoI = oi;
                                }
                                else if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip)
                                {
                                    PoleoI = oi;
                                }
                            }

                            if (PoleoI != ObjectId.Null)
                            {
                                ObjectId HeaderCableOi = ObjectId.Null;
                                Atend.Global.Design.frmDrawCalamp02 _drawCH = new Atend.Global.Design.frmDrawCalamp02(0);
                                if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
                                {

                                    AcDrawKalamp _drawK = new AcDrawKalamp();
                                    _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                    HeaderCableOi = _drawK.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)), PoleoI);
                                    if (HeaderCableOi == ObjectId.Null)
                                    {
                                        return;
                                    }

                                }
                                else
                                {
                                    AcDrawKalamp _drawK = new AcDrawKalamp();
                                    _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
                                    _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
                                    _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
                                    _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
                                    HeaderCableOi = _drawK.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)), PoleoI);
                                    if (HeaderCableOi == ObjectId.Null)
                                    {
                                        return;
                                    }
                                }


                                foreach (ObjectId oi in ConnectionSub.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_SUB oiSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
                                    foreach (ObjectId oii in oiSub.SubIdCollection)
                                    {
                                        if (oii == LastConnectionPoint)
                                        {
                                            ////ed.WriteMessage("~~~~~~~~~~\n");
                                            ////ed.WriteMessage("REM \n");

                                            Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oii, oi);
                                            ////ed.WriteMessage("Ins \n");


                                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderCableOi, oi);
                                            ////ed.WriteMessage("~~~~~~~~~~\n");
                                        }
                                    }
                                }

                                ConnectionSub.SelectedObjectId = HeaderCableOi;
                                ConnectionSub.Insert();

                                // erase for database before screen

                                Atend.Base.Acad.AT_INFO information = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LastConnectionPoint);
                                if (information.ParentCode != "NONE" && information.NodeCode != "")
                                {
                                    Atend.Base.Design.DPackage.AccessDelete(new Guid(information.NodeCode));

                                }
                                Atend.Base.Acad.AT_SUB ClampSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
                                foreach (ObjectId oi in ClampSub.SubIdCollection)
                                {
                                    Atend.Base.Acad.AT_INFO CommentInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                                    if (CommentInfo.ParentCode != "NONE" && CommentInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                                    {
                                        Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi);
                                    }
                                }
                                Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(LastConnectionPoint);
                                LastConnectionPoint = ObjectId.Null;
                            }
                        }
                    }
                }
                else
                {
                    Conti = false;
                }
            }


        }//current

        //public void DrawSelfKeeper03()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    bool Conti = true;
        //    DrawSelfKeerCableJig CLJ = new DrawSelfKeerCableJig();
        //    PromptResult pr;
        //    ObjectIdCollection ConnectionsOIs = new ObjectIdCollection();
        //    ObjectId LastConnectionPoint = ObjectId.Null;
        //    AcDrawKalamp UseForOburi = null;

        //    System.Data.DataTable Parents = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
        //    double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).Scale;
        //    double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.SelfKeeper).CommentScale;


        //    while (Conti)
        //    {
        //        Polyline pl = CLJ.GetEntity() as Polyline;
        //        if (pl != null && pl.NumberOfVertices == 0)
        //        {
        //            pr = ed.Drag(CLJ);
        //            if (pr.Status == PromptStatus.OK)
        //            {

        //                //-------------
        //                bool CanAcceptPoint = false;
        //                Point3d AcceptedCenterPoint = Point3d.Origin;
        //                //System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
        //                System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
        //                if (PointContainerList.Rows.Count == 1 && (Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole) || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.PoleTip)
        //                {
        //                    Atend.Global.Design.XXfrmDrawClampOrHeader02 _drawCH = new Atend.Global.Design.XXfrmDrawClampOrHeader02();
        //                    _drawCH.Volatage = Convert.ToInt32(eSelfKeepers[0].NamedVoltage);
        //                    if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
        //                    {
        //                        if (_drawCH.UseClamp)
        //                        {
        //                            AcDrawKalamp _drawK = new AcDrawKalamp();
        //                            _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                            _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                            _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
        //                            _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                            ObjectId newKalampOi = _drawK.DrawKalamp(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                            ////ed.WriteMessage("New oi {0} \n", newKalampOi);
        //                            if (newKalampOi == ObjectId.Null)
        //                            {
        //                                return;
        //                            }

        //                        }
        //                        //else
        //                        //{
        //                        //    AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
        //                        //    _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                        //    _DrawH.ProjectCode = Atend.Base.Acad.AcadGlobal.HeaderCableData.ProjectCode;
        //                        //    _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                        //    _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                        //    ObjectId newHeaderOi = _DrawH.DrawHeaderCabel(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                        //    ////ed.WriteMessage("New oi {0} \n", newHeaderOi);
        //                        //    if (newHeaderOi == ObjectId.Null)
        //                        //    {
        //                        //        return;
        //                        //    }
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("do nothing \n");
        //                    }
        //                    PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(CLJ.CentrePoint);
        //                }

        //                foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                {
        //                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                    if (drs.Length != 0)
        //                    {
        //                        CanAcceptPoint = true;
        //                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode != "")
        //                        {
        //                            _dBranch.LeftNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode);
        //                        }
        //                        else
        //                        {
        //                            _dBranch.LeftNodeCode = new Guid();
        //                        }
        //                        string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                        string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                        string y = StrTemp[1];
        //                        string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                        AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                        ConnectionsOIs.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                    }
        //                }

        //                if (CanAcceptPoint)
        //                {
        //                    ////ed.WriteMessage("Accepter Center : {0} \n", AcceptedCenterPoint);
        //                    CLJ.SetStartPoint(AcceptedCenterPoint);
        //                    CLJ.AddVertex();
        //                }
        //            }
        //            else
        //            {
        //                Conti = false;
        //            }
        //        }
        //        else if (pl != null && pl.NumberOfVertices != 1)
        //        {

        //            pr = ed.Drag(CLJ);
        //            if (pr.Status == PromptStatus.OK)
        //            {

        //                Point3d LastPoint = CLJ.CentrePoint;
        //                bool CanAcceptPoint = false;
        //                Point3d AcceptedCenterPoint = Point3d.Origin;
        //                //System.Data.DataTable dt = Atend.Global.Acad.UAcad.DetermineParent((int)Atend.Control.Enum.ProductType.SelfKeeper);
        //                System.Data.DataTable PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
        //                if (PointContainerList.Rows.Count == 1 && (Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.Pole || Convert.ToInt32(PointContainerList.Rows[0]["Type"]) == (int)Atend.Control.Enum.ProductType.PoleTip))
        //                {
        //                    if (UseForOburi == null)
        //                    {
        //                        Atend.Global.Design.frmDrawCalamp02 _frmDrawClamp = new Atend.Global.Design.frmDrawCalamp02(0);
        //                        if (Application.ShowModalDialog(_frmDrawClamp) == System.Windows.Forms.DialogResult.OK)
        //                        {
        //                            UseForOburi = new AcDrawKalamp();
        //                            UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                            UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                            UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
        //                            UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                        }
        //                        else
        //                        {
        //                            return;
        //                        }

        //                    }
        //                    else
        //                    {

        //                        UseForOburi = new AcDrawKalamp();
        //                        UseForOburi.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                        UseForOburi.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                        UseForOburi.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
        //                        UseForOburi.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                    }
        //                    LastConnectionPoint = UseForOburi.DrawKalamp(CLJ.CentrePoint, new ObjectId(Convert.ToInt32(PointContainerList.Rows[0]["Objectid"])));
        //                    PointContainerList = Atend.Global.Acad.Global.PointInsideWhichEntity(LastPoint);
        //                }

        //                foreach (System.Data.DataRow dr in PointContainerList.Rows)
        //                {
        //                    DataRow[] drs = Parents.Select(string.Format("SoftwareCode={0}", Convert.ToInt32(dr["Type"])));
        //                    if (drs.Length != 0)
        //                    {
        //                        CanAcceptPoint = true;
        //                        if (Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode != "")
        //                        {
        //                            _dBranch.RightNodeCode = new Guid(Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(new ObjectId(Convert.ToInt32(dr["ObjectId"]))).ParentCode);
        //                        }
        //                        else
        //                        {
        //                            _dBranch.RightNodeCode = new Guid();
        //                        }
        //                        string[] StrTemp = dr["CenterPoint"].ToString().Split(',');
        //                        string x = StrTemp[0].Substring(1, StrTemp[0].Length - 1);
        //                        string y = StrTemp[1];
        //                        string z = StrTemp[2].Substring(0, StrTemp[2].Length - 1);
        //                        AcceptedCenterPoint = new Point3d(double.Parse(x), double.Parse(y), double.Parse(z));
        //                        ConnectionsOIs.Add(new ObjectId(Convert.ToInt32(dr["ObjectId"])));
        //                    }
        //                }

        //                if (CanAcceptPoint)
        //                {

        //                    CLJ.SetEndPoint(AcceptedCenterPoint);

        //                    //~~~~~~~~~~~~~~~~~~~~~~~~~
        //                    #region Save one  line here

        //                    Polyline DrawnLine = CLJ.GetEntity() as Polyline;
        //                    if (DrawnLine != null)
        //                    {
        //                        Line l = new Line(DrawnLine.GetPoint3dAt(0), DrawnLine.GetPoint3dAt(1));

        //                        Atend.Global.Design.frmDrawBranch drawBranch = new Atend.Global.Design.frmDrawBranch(_dBranch.RightNodeCode, _dBranch.LeftNodeCode);
        //                        drawBranch.Length = l.Length;
        //                        if (drawBranch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                        {
        //                            _dBranch.Lenght = drawBranch.Length;

        //                            if (SaveSelfKeeperData())
        //                            {
        //                                ObjectId NewDrawnLine = Atend.Global.Acad.UAcad.DrawEntityOnScreen(l, Atend.Control.Enum.AutoCadLayerName.MED_AIR.ToString());

        //                                ////ed.WriteMessage("oi:{0} , oi:{1}", ConnectionsOIs[0], ConnectionsOIs[1]);

        //                                Atend.Base.Acad.AT_INFO SelfKeeperInfo = new Atend.Base.Acad.AT_INFO(NewDrawnLine);
        //                                SelfKeeperInfo.ParentCode = "";
        //                                SelfKeeperInfo.NodeCode = _dBranch.Code.ToString();
        //                                SelfKeeperInfo.NodeType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
        //                                SelfKeeperInfo.ProductCode = _dBranch.ProductCode;
        //                                SelfKeeperInfo.Insert();
        //                                ////ed.WriteMessage("11\n");

        //                                string comment = string.Format("{0} {1}m", _dBranch.Number, Math.Round(_dBranch.Lenght, 2));
        //                                ObjectId txtOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment, l.StartPoint, l.EndPoint, MyCommentScale),
        //                                    Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

        //                                Atend.Base.Acad.AT_INFO CommentInfo = new Atend.Base.Acad.AT_INFO(txtOI);
        //                                CommentInfo.ParentCode = _dBranch.Code.ToString();
        //                                CommentInfo.NodeCode = "";
        //                                CommentInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
        //                                CommentInfo.ProductCode = 0;
        //                                CommentInfo.Insert();


        //                                Atend.Base.Acad.AT_SUB SelfKeeperSub = new Atend.Base.Acad.AT_SUB(NewDrawnLine);
        //                                SelfKeeperSub.SubIdCollection = ConnectionsOIs;
        //                                SelfKeeperSub.Insert();
        //                                ////ed.WriteMessage("12\n");

        //                                foreach (ObjectId oi in ConnectionsOIs)
        //                                {
        //                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(NewDrawnLine, oi);
        //                                }
        //                                Atend.Base.Acad.AT_SUB.AddToAT_SUB(txtOI, NewDrawnLine);
        //                            }

        //                        }

        //                    }

        //                    #endregion
        //                    //~~~~~~~~~~~~~~~~~~~~~~~~~
        //                    ObjectId FirstConnection = ConnectionsOIs[1];
        //                    ConnectionsOIs.Clear();
        //                    ConnectionsOIs.Add(FirstConnection);


        //                    CLJ = new DrawSelfKeerCableJig();
        //                    CLJ.SetStartPoint(AcceptedCenterPoint);
        //                    CLJ.AddVertex();

        //                }
        //                //-------------

        //            }
        //            else
        //            {
        //                Conti = false;
        //                ObjectId PoleoI = ObjectId.Null;
        //                if (LastConnectionPoint != ObjectId.Null)
        //                {

        //                    Atend.Base.Acad.AT_SUB ConnectionSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
        //                    foreach (ObjectId oi in ConnectionSub.SubIdCollection)
        //                    {
        //                        Atend.Base.Acad.AT_INFO PoleInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                        if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.Pole)
        //                        {
        //                            PoleoI = oi;
        //                        }
        //                        else if (PoleInfo.ParentCode != "NONE" && PoleInfo.NodeType == (int)Atend.Control.Enum.ProductType.PoleTip)
        //                        {
        //                            PoleoI = oi;
        //                        }
        //                    }

        //                    if (PoleoI != ObjectId.Null)
        //                    {
        //                        ObjectId HeaderCableOi = ObjectId.Null;
        //                        Atend.Global.Design.XXfrmDrawClampOrHeader02 _drawCH = new Atend.Global.Design.XXfrmDrawClampOrHeader02();
        //                        if (Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(_drawCH) == System.Windows.Forms.DialogResult.OK)
        //                        {
        //                            if (_drawCH.UseClamp)
        //                            {
        //                                AcDrawKalamp _drawK = new AcDrawKalamp();
        //                                _drawK.eClamp = Atend.Base.Acad.AcadGlobal.ClampData.eClamp;
        //                                _drawK.Existance = Atend.Base.Acad.AcadGlobal.ClampData.Existance;
        //                                _drawK.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
        //                                _drawK.UseAccess = Atend.Base.Acad.AcadGlobal.ClampData.UseAccess;
        //                                HeaderCableOi = _drawK.DrawKalamp(Atend.Global.Acad.UAcad.CenterOfEntity(
        //                               Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)),
        //                               PoleoI);
        //                                if (HeaderCableOi == ObjectId.Null)
        //                                {
        //                                    return;
        //                                }

        //                            }
        //                            else
        //                            {
        //                                AcDrawHeaderCabel _DrawH = new AcDrawHeaderCabel();
        //                                _DrawH.Existance = Atend.Base.Acad.AcadGlobal.HeaderCableData.Existance;
        //                                _DrawH.ProjectCode = Atend.Base.Acad.AcadGlobal.ClampData.ProjectCode;
        //                                _DrawH.UseAccess = Atend.Base.Acad.AcadGlobal.HeaderCableData.UseAccess;
        //                                _DrawH.eHeaderCabel = Atend.Base.Acad.AcadGlobal.HeaderCableData.eHeaderCable;
        //                                HeaderCableOi = _DrawH.DrawHeaderCabel(Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(LastConnectionPoint)), PoleoI);
        //                                if (HeaderCableOi == ObjectId.Null)
        //                                {
        //                                    return;
        //                                }

        //                            }
        //                        }


        //                        foreach (ObjectId oi in ConnectionSub.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_SUB oiSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(oi);
        //                            foreach (ObjectId oii in oiSub.SubIdCollection)
        //                            {
        //                                if (oii == LastConnectionPoint)
        //                                {
        //                                    ////ed.WriteMessage("~~~~~~~~~~\n");
        //                                    ////ed.WriteMessage("REM \n");

        //                                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oii, oi);
        //                                    ////ed.WriteMessage("Ins \n");


        //                                    Atend.Base.Acad.AT_SUB.AddToAT_SUB(HeaderCableOi, oi);
        //                                    ////ed.WriteMessage("~~~~~~~~~~\n");
        //                                }
        //                            }
        //                        }

        //                        ConnectionSub.SelectedObjectId = HeaderCableOi;
        //                        ConnectionSub.Insert();

        //                        // erase for database before screen


        //                        Atend.Base.Acad.AT_INFO information = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(LastConnectionPoint);
        //                        if (information.ParentCode != "NONE" && information.NodeCode != "")
        //                        {
        //                            Atend.Base.Design.DPackage.AccessDelete(new Guid(information.NodeCode));

        //                        }
        //                        Atend.Base.Acad.AT_SUB ClampSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(LastConnectionPoint);
        //                        foreach (ObjectId oi in ClampSub.SubIdCollection)
        //                        {
        //                            Atend.Base.Acad.AT_INFO CommentInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
        //                            if (CommentInfo.ParentCode != "NONE" && CommentInfo.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
        //                            {
        //                                Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi);
        //                            }
        //                        }
        //                        Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(LastConnectionPoint);

        //                        LastConnectionPoint = ObjectId.Null;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Conti = false;
        //        }
        //    }


        //}

        private bool SaveSelfKeeperData()
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

                    if (!_UseAccess)
                    {
                        ed.WriteMessage("22222222222222222 \n");
                        foreach (Atend.Base.Equipment.ESelfKeeper eSelfkeeper in eSelfKeepers)
                        {
                            if (!eSelfkeeper.AccessInsert(aTransaction, aConnection, true, true))
                            {
                                throw new System.Exception("eSelfkeeper.AccessInsert failed");
                            }
                        }

                        eSelfKeeperTip.PhaseProductCode = eSelfKeepers[0].Code;
                        eSelfKeeperTip.NeutralProductCode = eSelfKeepers[1].Code;
                        eSelfKeeperTip.NightProductCode = eSelfKeepers[2].Code;

                        //AccessSelectByXCode  ???
                        if (!eSelfKeeperTip.AccessInsert(aTransaction, aConnection))
                        {
                            throw new System.Exception("eConductorTip.AccessInsert failed");
                        }
                    }

                    ed.WriteMessage("3333333333333 \n");
                    _dBranch.Number = eSelfKeeperTip.Description;
                    _dBranch.ProductCode = eSelfKeeperTip.Code;
                    _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                    _dBranch.ProjectCode = ProjectCode;
                    _dBranch.Sag = 0;
                    _dBranch.IsExist = Existance;
                    if (!_dBranch.AccessInsert(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessInsert failed");
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

            Atend.Base.Acad.AcadGlobal.SelfKeeperData.UseAccess = true;
            //UseAccess = true;

            #endregion

            //ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");
            return true;

        }

        public bool UpdateSelfKeeperData(double lenght, Guid BranchGuid)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction aTransaction;
            OleDbConnection aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            ArrayList selfCode = new ArrayList();
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
                        foreach (Atend.Base.Equipment.ESelfKeeper eSelef in eSelfKeepers)
                        {
                            ////ed.WriteMessage("SelefKeeper[i].Comment={0},self.Name={1}\n",  eSelef.Comment, eSelef.Name);
                            if (!eSelef.AccessInsert(aTransaction, aConnection, true, true))
                                throw new System.Exception("eSelfKeepers.AccessInsert failed");
                            else
                                selfCode.Add(eSelef.Code);

                        }
                        ////ed.WriteMessage("SelfKeeperTip.Insert\n");
                        eSelfKeeperTip.PhaseProductCode = Convert.ToInt32(selfCode[0].ToString());
                        eSelfKeeperTip.NeutralProductCode = Convert.ToInt32(selfCode[1].ToString());
                        eSelfKeeperTip.NightProductCode = Convert.ToInt32(selfCode[2].ToString());
                        if (!eSelfKeeperTip.AccessInsert(aTransaction, aConnection))
                        {
                            throw new System.Exception("eSelfKeeperTip.AccessInsert failed");
                        }
                    }
                    ////ed.WriteMessage("eSelefKeeperTip.Code={0}\n",eSelfKeeperTip.Code);
                    _dBranch.IsExist = existance;
                    _dBranch.ProductCode = eSelfKeeperTip.Code;
                    _dBranch.Lenght = lenght;
                    _dBranch.ProjectCode = ProjectCode;
                    ////ed.WriteMessage("dBranch.Insert\n");
                    if (_dBranch.AccessUpdate(aTransaction, aConnection))
                    {
                        Atend.Base.Acad.AT_INFO atinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(selectedObjectId);
                        atinfo.ProductCode = eSelfKeeperTip.Code;
                        atinfo.Insert();
                        ChangeCabelComment(selectedObjectId, eSelfKeeperTip.Description);
                    }
                    else
                    {
                        throw new System.Exception("SelfKeeperPack.AccessInsert2 failed");
                    }

                    _dBranch.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
                    _dBranch.Lenght = lenght;

                    if (!_dBranch.AccessUpdateLenghtByLeftAndRightNode(aTransaction, aConnection))
                    {
                        throw new System.Exception("dBranch.AccessUpdate Failed");
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("ERROR UpdateSelfKeeperData(Transaction) 01 : {0} \n", ex1.Message);
                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;

                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR UpdateSelfKeeperData 01 : {0} \n", ex.Message);
                aConnection.Close();
                return false;
            }

            aTransaction.Commit();
            aConnection.Close();
            return true;
        }

        public static bool DeleteSelfKeeperData(ObjectId ConductorOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Atend.Base.Acad.AT_INFO conductorinfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(ConductorOI);
                if (!Atend.Base.Design.DBranch.AccessDelete(new Guid(conductorinfo.NodeCode.ToString())))
                {
                    throw new System.Exception("Error In Delete DB(dbranch SelfKeeper)\n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR SelfKeeper : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

        public static bool DeleteSelfKeeper(ObjectId ConductorOI)
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
                    if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                    {
                        Atend.Base.Acad.AT_SUB Sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        foreach (ObjectId SubOI in Sub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SubOI);
                            if (SubInfo.ParentCode != "NONE" && SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper && SubOI == ConductorOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(SubOI, CounductorSubOI);
                            }
                        }
                        //Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        //bool isOtherSelfKeeper = false;
                        //foreach (ObjectId oi in consolSub.SubIdCollection)
                        //{
                        //    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //    //if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment )
                        //    //{
                        //    //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                        //    //    {
                        //    //        throw new System.Exception("Error In Delete comment\n");
                        //    //    }
                        //    //    else
                        //    //    {
                        //    //        Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                        //    //    }
                        //    //}

                        //    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper && at_info.SelectedObjectId != ConductorOI)
                        //    {
                        //        isOtherSelfKeeper = true;
                        //    }
                        //}

                        //if (isOtherSelfKeeper == false)
                        //{
                        //    Atend.Base.Acad.AT_SUB _AT_SUB = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(CounductorSubOI))
                        //    {
                        //        throw new System.Exception("Error In Delete Kablsho or Klamp\n");
                        //    }
                        //    else
                        //    {
                        //        //Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(CounductorSubOI, ConductorOI);
                        //        foreach (ObjectId oi in _AT_SUB.SubIdCollection)
                        //        {
                        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                        //            {
                        //                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                        //                {
                        //                    throw new System.Exception("Error In Delete comment\n");
                        //                }
                        //                else
                        //                {
                        //                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
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

        public static bool DeleteSelfKeeper(ObjectId ConductorOI, bool DeleteItSelf)
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
                    if (CounductorSubInfo.ParentCode != "NONE" && CounductorSubInfo.NodeType == (int)Atend.Control.Enum.ProductType.Kalamp)
                    {
                        Atend.Base.Acad.AT_SUB Sub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        foreach (ObjectId SubOI in Sub.SubIdCollection)
                        {
                            Atend.Base.Acad.AT_INFO SubInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(SubOI);
                            if (SubInfo.ParentCode != "NONE" && SubInfo.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper && SubOI == ConductorOI)
                            {
                                Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(SubOI, CounductorSubOI);
                            }
                        }
                        //Atend.Base.Acad.AT_SUB consolSub = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        //bool isOtherSelfKeeper = false;
                        //foreach (ObjectId oi in consolSub.SubIdCollection)
                        //{
                        //    Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //    //if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment )
                        //    //{
                        //    //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                        //    //    {
                        //    //        throw new System.Exception("Error In Delete comment\n");
                        //    //    }
                        //    //    else
                        //    //    {
                        //    //        Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                        //    //    }
                        //    //}

                        //    if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.SelfKeeper && at_info.SelectedObjectId != ConductorOI)
                        //    {
                        //        isOtherSelfKeeper = true;
                        //    }
                        //}

                        //if (isOtherSelfKeeper == false)
                        //{
                        //    Atend.Base.Acad.AT_SUB _AT_SUB = Atend.Base.Acad.AT_SUB.SelectBySelectedObjectId(CounductorSubOI);
                        //    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(CounductorSubOI))
                        //    {
                        //        throw new System.Exception("Error In Delete Kablsho or Klamp\n");
                        //    }
                        //    else
                        //    {
                        //        //Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(CounductorSubOI, ConductorOI);
                        //        foreach (ObjectId oi in _AT_SUB.SubIdCollection)
                        //        {
                        //            Atend.Base.Acad.AT_INFO at_info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                        //            if (at_info.ParentCode != "NONE" && at_info.NodeType == (int)Atend.Control.Enum.ProductType.Comment)
                        //            {
                        //                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(oi))
                        //                {
                        //                    throw new System.Exception("Error In Delete comment\n");
                        //                }
                        //                else
                        //                {
                        //                    Atend.Base.Acad.AT_SUB.RemoveFromAT_SUB(oi, CounductorSubOI);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
                if (DeleteItSelf)
                {
                    if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(ConductorOI))
                    {
                        throw new System.Exception("GRA while delete conductor \n");
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

    }

}