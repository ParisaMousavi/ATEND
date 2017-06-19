using System;
using System.Collections;
using System.Collections.Generic;
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
    public class AcDrawForbidenArea
    {

        //~~~~~~~~~~~~~~~~ CLASS ~~~~~~~~~~~~~~~~~~~~~//

        class DrawForbidenAreaJig : EntityJig
        {
            Point3dCollection _Point3dCollection;
            Point3d m_tempPoint;
            Plane m_plane;

            public DrawForbidenAreaJig(Matrix3d ucs)
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

        public static void DrawForbidenArea()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;
            Matrix3d ucs = ed.CurrentUserCoordinateSystem;
            DrawForbidenAreaJig jig = new DrawForbidenAreaJig(ucs);
            ObjectIdCollection ForbidenIds = new ObjectIdCollection();

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
                            Point3d AcceptedCenterPoint = CurrentPoint;
                            pl.SetPointAt(0, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));
                            jig.AddLatestVertex();
                        }
                        else
                        {
                            jig.AddLatestVertex();
                        }
                    }
                }
                bComplete = (res.Status == PromptStatus.None);
                if (bComplete)
                {
                    Polyline pl = jig.GetEntity() as Polyline;
                    if (pl != null)
                    {
                        jig.RemoveLastVertex();
                        Point3d CurrentPoint = new Point3d(pl.GetPoint3dAt(pl.NumberOfVertices - 1).X, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Y, pl.GetPoint3dAt(pl.NumberOfVertices - 1).Z);
                        Point3d AcceptedCenterPoint = CurrentPoint;
                        pl.SetPointAt(pl.NumberOfVertices - 1, new Point2d(AcceptedCenterPoint.X, AcceptedCenterPoint.Y));
                    }
                }
            } while (bSuccess && !bComplete);
            if (bComplete)
            {
                Polyline CableEntity = jig.GetEntity() as Polyline;
                if (CableEntity != null)
                {
                    if (CableEntity.GetPoint3dAt(CableEntity.NumberOfVertices - 1) == CableEntity.GetPoint3dAt(0))
                    {
                        CableEntity.Closed = true;
                    }
                    else
                    {
                        Point2d p2 = CableEntity.GetPoint2dAt(0);
                        CableEntity.AddVertexAt(CableEntity.NumberOfVertices, p2, 0, 0, 0);
                        CableEntity.Closed = true;
                    }

                    ObjectId ForbidenOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(CableEntity, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                    ForbidenIds.Add(ForbidenOI);
                    Atend.Base.Acad.AT_INFO forbidenInfo = new Atend.Base.Acad.AT_INFO(ForbidenOI);
                    forbidenInfo.ParentCode = "";
                    forbidenInfo.NodeCode = "";
                    forbidenInfo.ProductCode = 0;
                    forbidenInfo.NodeType = (int)Atend.Control.Enum.ProductType.ForbiddenArea;
                    forbidenInfo.Angle = 0;
                    forbidenInfo.Insert();

                    #region forbiden area
                    Transaction tr = doc.TransactionManager.StartTransaction();
                    using (tr)
                    {
                        // Check the entity is a closed curve
                        DBObject obj = tr.GetObject(CableEntity.ObjectId, OpenMode.ForRead);
                        Curve cur = obj as Curve;
                        if (cur != null && cur.Closed == false)
                        {
                            //ed.WriteMessage("\nLoop must be a closed curve.");
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
                            //Let's use the pre-defined spherical gradient
                            //LINEAR, CYLINDER, INVCYLINDER, SPHERICAL, INVSPHERICAL, HEMISPHERICAL, INVHEMISPHERICAL, CURVED, and INVCURVED. 
                            hat.SetGradient(GradientPatternType.PreDefinedGradient, "LINEAR");
                            // We're defining two colours
                            hat.GradientOneColorMode = false;
                            GradientColor[] gcs = new GradientColor[2];
                            // First colour must have value of 0
                            gcs[0] = new GradientColor(Color.FromRgb(0, 0, 255), 0);
                            // Second colour must have value of 1
                            gcs[1] = new GradientColor(Color.FromRgb(47, 165, 208), 1);
                            hat.SetGradientColors(gcs);
                            // Add the hatch to the model space
                            // and the transaction
                            ObjectId hatId = btr.AppendEntity(hat);
                            tr.AddNewlyCreatedDBObject(hat, true);
                            // Add the hatch loop and complete the hatch
                            ObjectIdCollection ids = new ObjectIdCollection();
                            ids.Add(CableEntity.ObjectId);
                            hat.Associative = true;
                            hat.AppendLoop(HatchLoopTypes.Default, ids);
                            hat.EvaluateHatch(true);
                            tr.Commit();

                            Atend.Base.Acad.AT_INFO forbidenInfo1 = new Atend.Base.Acad.AT_INFO(hatId);
                            forbidenInfo1.ParentCode = "";
                            forbidenInfo1.NodeCode = "";
                            forbidenInfo1.ProductCode = 0;
                            forbidenInfo1.NodeType = (int)Atend.Control.Enum.ProductType.ForbiddenArea;
                            forbidenInfo1.Angle = 0;
                            forbidenInfo1.Insert();


                            ForbidenIds.Add(hatId);
                        }
                    }
                    #endregion

                    Atend.Global.Acad.Global.MakeGroup(Guid.NewGuid().ToString(), ForbidenIds);

                }
            }
        }

        public static bool PointWasInForbidenArea(Point3d MyPoint)
        {

            bool Answer = false;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("START TED \n");
            try
            {
                TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "hatch") };
                SelectionFilter sf = new SelectionFilter(tvs);
                PromptSelectionResult psr = ed.SelectAll(sf);
                ObjectId[] ids = psr.Value.GetObjectIds();
                //ed.WriteMessage("hatch found \n");
                foreach (ObjectId oi in ids)
                {
                    if (Answer == false)
                    {
                        ObjectId GroupOi = Atend.Global.Acad.UAcad.GetEntityGroup(oi);
                        //ed.WriteMessage("hatch in progress \n");
                        if (GroupOi != ObjectId.Null)
                        {
                            //ed.WriteMessage("group found \n");
                            ObjectIdCollection OIC = Atend.Global.Acad.UAcad.GetGroupSubEntities(GroupOi);
                            //ed.WriteMessage("hatch sub bring \n");
                            foreach (ObjectId oii in OIC)
                            {
                                Atend.Base.Acad.AT_INFO info = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oii);
                                //ed.WriteMessage("get AT_INFO \n");
                                if (info.ParentCode != "NONE" && info.NodeType == (int)Atend.Control.Enum.ProductType.ForbiddenArea)
                                {
                                    //ed.WriteMessage("AT_INFO found \n");
                                    Polyline p = Atend.Global.Acad.UAcad.GetEntityByObjectID(oii) as Polyline;
                                    if (p != null && Answer == false)
                                    {
                                        //ed.WriteMessage(" IS INSIDE >>> : {0} \n", Atend.Global.Acad.UAcad.IsInsideCurve(p, MyPoint));
                                        //ed.WriteMessage("GO FOR BARASI \n");
                                        Answer = Atend.Global.Acad.UAcad.IsInsideCurve(p, MyPoint);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Answer = false;
            }
            return Answer;
        }

        public static bool DeleteForbiddenArea(ObjectId ForbiddenAreaOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(ForbiddenAreaOI))
                {
                    throw new System.Exception("GRA while delete ForbiddenArea \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR ForbiddenArea : {0} \n", ex.Message);
                return false;
            }
            return true;
        }

    }
}
