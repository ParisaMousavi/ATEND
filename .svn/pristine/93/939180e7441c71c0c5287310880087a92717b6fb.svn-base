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


namespace Atend.Global.Acad.DrawEquips
{



    public class AcDrawWeekJackPanel02
    {


        //~~~~~~~~~~~~~~~~~~~ Class ~~~~~~~~~~~~~~~~~~~~~~~~//

        public class DrawWeekJackPanelJig : DrawJig
        {

            Point3d CenterPoint = Point3d.Origin;
            List<Entity> Entities = new List<Entity>();
            int _CellCount = 0;
            int BaseLineLength = 0;
            Entity ContainerEntity = null;
            double MyScale = 1;
            DataRow[] _PostEquipInserted = null;


            public DrawWeekJackPanelJig(Entity Container, int CellCount, double Scale, ref DataRow[] PostEquipInserted)
            {
                MyScale = Scale;
                ContainerEntity = Container;
                _CellCount = CellCount;
                _PostEquipInserted = PostEquipInserted;
                BaseLineLength = _CellCount * 20;

                if (_CellCount != 0)
                {
                    int t = BaseLineLength / 2;

                    //header cabel
                    Point3dCollection Points = new Point3dCollection();
                    Points.Add(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 25, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10) - 5, CenterPoint.Y + 20, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10) + 5, CenterPoint.Y + 20, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 25, CenterPoint.Z));
                    Entities.Add(CreateHeaderCable(Points));

                    Entities.Add(CreateLine(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y, CenterPoint.Z), new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 20, CenterPoint.Z), 0, 0, 0));
                    int CellCounter = 1;
                    for (int i = 1; i <= _CellCount; i++)
                    {

                        List<Entity> temp = GetDemo(new Point3d(CenterPoint.X + (CellCounter - 1) * 20, CenterPoint.Y, CenterPoint.Z));
                        foreach (Entity e1 in temp)
                        {
                            Entities.Add(e1);
                        }
                        CellCounter++;
                    }
                }
            }

            private List<Entity> GetDemo(Point3d BasePoint)
            {
                List<Entity> t = new List<Entity>();

                t.Add(CreateLine(new Point3d(BasePoint.X - 10, BasePoint.Y, BasePoint.Z),
    new Point3d(BasePoint.X + 10, BasePoint.Y, BasePoint.Z),
    0,
    0,
    0));

                t.Add(CreateLine(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
    new Point3d(BasePoint.X, BasePoint.Y - 20, BasePoint.Z),
    0,
    0,
    0));


                //header cabel
                Point3dCollection Points = new Point3dCollection();
                Points.Add(new Point3d(BasePoint.X, BasePoint.Y - 25, BasePoint.Z));
                Points.Add(new Point3d(BasePoint.X - 5, BasePoint.Y - 20, BasePoint.Z));
                Points.Add(new Point3d(BasePoint.X + 5, BasePoint.Y - 20, BasePoint.Z));
                Points.Add(new Point3d(BasePoint.X, BasePoint.Y - 25, BasePoint.Z));
                t.Add(CreateHeaderCable(Points));


                return t;

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


                //if (ProductType != 0)
                //{
                Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, ProductType);

                //}
                //if (ColorIndex != 0)
                //{
                mLine.ColorIndex = ColorIndex;
                //}

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

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                JigPromptPointOptions ppo = new JigPromptPointOptions("\nSelect WeekJackPanel:");
                PromptPointResult ppr = prompts.AcquirePoint(ppo);
                if (ppr.Status == PromptStatus.OK)
                {
                    if (ppr.Value == CenterPoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {

                        if (Atend.Global.Acad.UAcad.IsInsideCurve((Curve)ContainerEntity, ppr.Value) == true)
                        {
                            CenterPoint = ppr.Value;
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

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {
                Entities.Clear();


                if (_CellCount != 0)
                {
                    int t = BaseLineLength / 2;

                    //header cabel
                    Point3dCollection Points = new Point3dCollection();
                    Points.Add(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 25, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10) - 5, CenterPoint.Y + 20, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10) + 5, CenterPoint.Y + 20, CenterPoint.Z));
                    Points.Add(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 25, CenterPoint.Z));
                    Entities.Add(CreateHeaderCable(Points));

                    Entities.Add(CreateLine(new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y, CenterPoint.Z), new Point3d(CenterPoint.X + (t - 10), CenterPoint.Y + 20, CenterPoint.Z), 0, 0, 0));

                    int CellCounter = 1;
                    for (int i = 1; i <= _CellCount; i++)
                    {

                        List<Entity> temp = GetDemo(new Point3d(CenterPoint.X + (CellCounter - 1) * 20, CenterPoint.Y, CenterPoint.Z));
                        foreach (Entity e1 in temp)
                        {
                            Entities.Add(e1);
                        }
                        CellCounter++;
                    }
                    //~~~~~~~~ SCALE ~~~~~~~~~~

                    Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint.X, CenterPoint.Y, 0));
                    foreach (Entity en in Entities)
                    {
                        en.TransformBy(trans1);
                    }

                    //~~~~~~~~~~~~~~~~~~~~~~~~~
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
        }


        public void DrawWeekJackPanel(Entity PostContainerEntity, int CellCount, ref DataRow[] PostEquipInserted)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti = true;
            Guid NodeCode = new Guid(PostEquipInserted[0]["NodeCode"].ToString());
            Guid ParentCode = new Guid(PostEquipInserted[0]["ParentCode"].ToString());
            int ProductCode = Convert.ToInt32(PostEquipInserted[0]["ProductCode"]);


            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.WeekJackPanel).Scale;

            //ed.WriteMessage("DrawWeekJackPanel 01 \n");
            DrawWeekJackPanelJig MidJ = new DrawWeekJackPanelJig(PostContainerEntity, CellCount, MyScale, ref PostEquipInserted);
            PromptResult pr;
            while (conti)
            {
                pr = ed.Drag(MidJ);
                if (pr.Status == PromptStatus.OK)
                {
                    conti = false;
                    #region save data here
                    ObjectIdCollection OIC = new ObjectIdCollection();
                    ObjectIdCollection HeadersOI = new ObjectIdCollection();

                    //ed.WriteMessage("DrawWeekJackPanel 02 \n");
                    List<Entity> Entities = new List<Entity>();
                    //ed.WriteMessage("DrawWeekJackPanel 03 \n");
                    Entities = MidJ.GetEntities();
                    //ed.WriteMessage("DrawWeekJackPanel 04 \n");
                    foreach (Entity ent in Entities)
                    {

                        Atend.Global.Acad.AcadJigs.MyPolyLine HeaderCablePoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                        //ed.WriteMessage("DrawWeekJackPanel 05 \n");
                        if (HeaderCablePoly != null)
                        {
                            ObjectId hoi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.LOW_GROUND.ToString());
                            HeadersOI.Add(hoi);
                            OIC.Add(hoi);
                            //ed.WriteMessage("DrawWeekJackPanel 06 \n");
                        }
                        else
                        {
                            ObjectId NOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString());
                            if (NOI != ObjectId.Null)
                            {
                                OIC.Add(NOI);
                                Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(NOI);
                                HeaderInfo.ParentCode = NodeCode.ToString();
                                HeaderInfo.NodeCode = "";
                                HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.WeekJackPanel;
                                HeaderInfo.ProductCode = 0;
                                HeaderInfo.Insert();
                            }
                            //ed.WriteMessage("DrawWeekJackPanel 07 \n");
                        }

                    }



                    try
                    {
                        foreach (ObjectId HeaderOI in HeadersOI)
                        {
                            Atend.Base.Acad.AT_INFO HeaderInfo = new Atend.Base.Acad.AT_INFO(HeaderOI);
                            //ed.WriteMessage("Header Parent :{0}\n", p.CodeGuid);
                            HeaderInfo.ParentCode = NodeCode.ToString();
                            HeaderInfo.NodeCode = "";
                            HeaderInfo.NodeType = (int)Atend.Control.Enum.ProductType.HeaderCabel;
                            HeaderInfo.ProductCode = 0;
                            HeaderInfo.Insert();
                        }

                        ObjectId WeekGroup = Atend.Global.Acad.Global.MakeGroup(NodeCode.ToString(), OIC);

                        if (WeekGroup != ObjectId.Null)
                        {
                            Atend.Base.Acad.AT_INFO WeekInfo = new Atend.Base.Acad.AT_INFO(WeekGroup);
                            //ed.WriteMessage(">>>>>>Header Parent :{0} , WeekCode :{1} \n", p.ParentCode, p.CodeGuid);
                            WeekInfo.ParentCode = ParentCode.ToString();
                            WeekInfo.NodeCode = NodeCode.ToString();
                            WeekInfo.NodeType = (int)Atend.Control.Enum.ProductType.WeekJackPanel;
                            WeekInfo.ProductCode = ProductCode ;
                            WeekInfo.Insert();

                            //ed.WriteMessage("ADD TO POST WEEK\n");
                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(WeekGroup, PostContainerEntity.ObjectId);
                            Atend.Base.Acad.AT_SUB.AddToAT_SUB(PostContainerEntity.ObjectId, WeekGroup);
                        }


                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage("Error DrawWeekJackPanel : {0} \n", ex.Message);
                    }
                    #endregion
                }
            }
        }

    }
}
