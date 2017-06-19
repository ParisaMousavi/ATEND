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



    public class AcDrawTransformer02
    {


        //~~~~~~~~~~~~~~~~~~~ Properties ~~~~~~~~~~~~~~~~~~~~~~~~//

        //~~~~~~~~~~~~~~~~~~~~~~~ CLASS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public class DrawTransformerJig : DrawJig
        {

            Point3d CenterPoint = Point3d.Origin;
            public Point3d MyCenterPoint
            {
                get { return CenterPoint; }
            }

            List<Entity> Entities = new List<Entity>();
            Entity ContainerEntity = null;
            double MyScale = 1;

            public DrawTransformerJig(Entity Container, double Scale)
            {
                MyScale = Scale;
                ContainerEntity = Container;

                Entities.Add(CreateCircle(new Point3d(CenterPoint.X + 10, CenterPoint.Y, CenterPoint.Z), 20, 90));
                Entities.Add(CreateCircle(new Point3d(CenterPoint.X - 10, CenterPoint.Y, CenterPoint.Z), 20, 210));
                Entities.Add(CreateLine(new Point3d(CenterPoint.X + 30, CenterPoint.Y, CenterPoint.Z),
                    new Point3d(CenterPoint.X + 40, CenterPoint.Y, CenterPoint.Z),
                    (int)Atend.Control.Enum.ProductType.Transformer, 0, 10));

                Entities.Add(CreateLine(new Point3d(CenterPoint.X - 30, CenterPoint.Y, CenterPoint.Z),
    new Point3d(CenterPoint.X - 40, CenterPoint.Y, CenterPoint.Z),
    (int)Atend.Control.Enum.ProductType.Transformer, 0, 10));


                Entities.Add(DrawKablsho(new Point3d(CenterPoint.X + 40, CenterPoint.Y, CenterPoint.Z)));
                //header cabel
                Point3dCollection Points = new Point3dCollection();
                Points.Add(new Point3d(CenterPoint.X - 45, CenterPoint.Y, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 40, CenterPoint.Y - 5, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 40, CenterPoint.Y + 5, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 45, CenterPoint.Y, CenterPoint.Z));
                Entities.Add(CreateHeaderCable(Points));

            }

            //private Entity CreateConnectionPoint(Point3d CenterPoint, double Radius)
            //{
            //    Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

            //    c.Center = CenterPoint;

            //    c.Normal = new Vector3d(0, 0, 1);

            //    c.Radius = Radius;

            //    c.ColorIndex = 3;

            //    Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.ConnectionPoint);

            //    return c;
            //}

            private Entity DrawKablsho(Point3d CenterPoint)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ObjectId HeaderOI = ObjectId.Null;

                double BaseX = CenterPoint.X - 2.5;
                double BaseY = CenterPoint.Y;

                Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY - 5), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + 5, BaseY), 0, 0, 0);
                pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX, BaseY + 5), 0, 0, 0);
                pLine.Closed = true;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.KablSho);

                return pLine;


            }

            private Entity CreateCircle(Point3d CenterPoint, double Radius, int ColorIndex)
            {
                Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

                c.Center = CenterPoint;

                c.Normal = new Vector3d(0, 0, 1);

                c.Radius = Radius;

                c.ColorIndex = ColorIndex;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.Transformer);

                return c;
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

                JigPromptPointOptions ppo = new JigPromptPointOptions("\nTransformer Position:");
                PromptPointResult ppr = prompts.AcquirePoint(ppo);
                if (ppr.Status == PromptStatus.OK)
                {
                    if (CenterPoint == ppr.Value)
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

                Entities.Add(CreateCircle(new Point3d(CenterPoint.X + 10, CenterPoint.Y, CenterPoint.Z), 20, 90));
                Entities.Add(CreateCircle(new Point3d(CenterPoint.X - 10, CenterPoint.Y, CenterPoint.Z), 20, 210));
                Entities.Add(CreateLine(new Point3d(CenterPoint.X + 30, CenterPoint.Y, CenterPoint.Z),
                    new Point3d(CenterPoint.X + 40, CenterPoint.Y, CenterPoint.Z),
                    (int)Atend.Control.Enum.ProductType.Transformer, 0, 10));

                Entities.Add(CreateLine(new Point3d(CenterPoint.X - 30, CenterPoint.Y, CenterPoint.Z),
    new Point3d(CenterPoint.X - 40, CenterPoint.Y, CenterPoint.Z),
    (int)Atend.Control.Enum.ProductType.Transformer, 0, 10));


                Entities.Add(DrawKablsho(new Point3d(CenterPoint.X + 40, CenterPoint.Y, CenterPoint.Z)));
                //header cabel
                Point3dCollection Points = new Point3dCollection();
                Points.Add(new Point3d(CenterPoint.X - 45, CenterPoint.Y, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 40, CenterPoint.Y - 5, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 40, CenterPoint.Y + 5, CenterPoint.Z));
                Points.Add(new Point3d(CenterPoint.X - 45, CenterPoint.Y, CenterPoint.Z));
                Entities.Add(CreateHeaderCable(Points));

                //~~~~~~~~ SCALE ~~~~~~~~~~

                Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint.X, CenterPoint.Y, 0));
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

            private Entity CreateConnectionPoint(Point3d CenterPoint, double Radius)
            {
                Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

                c.Center = CenterPoint;
                c.Normal = new Vector3d(0, 0, 1);
                c.Radius = Radius;
                //c.ColorIndex = 3;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.Transformer);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(c, CodeGuid);
                return c;
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

            public List<Entity> GetEntities()
            {
                return Entities;
            }

            public List<Entity> GetDemo(Point3d StartPoint, Point3d EndPoint)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                LineSegment3d ls1 = new LineSegment3d(StartPoint, EndPoint);
                Point3d BasePoint = ls1.MidPoint;

                Vector3d Vect1 = EndPoint - StartPoint, norm1 = Vect1.GetNormal();
                double Length = Vect1.Length / 3;

                CenterPoint = BasePoint;
                Point3d CenterPoint1 = StartPoint + (norm1 * Length * 1);
                Point3d CenterPoint2 = StartPoint + (norm1 * Length * 1);

                double Angle = new Line(StartPoint, EndPoint).Angle;


                Entities.Add(CreateConnectionPoint(CenterPoint1, Vect1.Length / 3));
                Entities.Add(CreateConnectionPoint(CenterPoint2, Vect1.Length / 3));
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
                return Entities;
            }

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void DrawTransformer(Entity PostContainerEntity, ref DataRow[] PostEquipInserted)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            bool conti = true;
            Guid NodeCode = new Guid(PostEquipInserted[0]["NodeCode"].ToString());
            Guid ParentCode = new Guid(PostEquipInserted[0]["ParentCode"].ToString());
            int ProductCode = Convert.ToInt32(PostEquipInserted[0]["ProductCode"]);

            double MyScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Transformer).Scale;
            double MyCommentScale = Atend.Base.Design.DProductProperties.AccessSelectBySoftwareCode((int)Atend.Control.Enum.ProductType.Transformer).CommentScale;


            DrawTransformerJig transformerJig = new DrawTransformerJig(PostContainerEntity, MyScale);
            PromptResult pr;

            while (conti)
            {
                pr = ed.Drag(transformerJig);
                if (pr.Status == PromptStatus.OK)
                {
                    conti = false;

                    #region Save data here

                    List<Entity> Entities = transformerJig.GetEntities();
                    if (NodeCode != null && ParentCode != null)
                    {
                        ObjectIdCollection OIC = new ObjectIdCollection();

                        foreach (Entity ent in Entities)
                        {
                            ObjectId NewEntOI = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.MED_GROUND.ToString());

                            Atend.Base.Acad.AT_INFO EntInfo = new Atend.Base.Acad.AT_INFO(NewEntOI);
                            EntInfo.ParentCode = ParentCode.ToString(); // TransformerParent.ToString();
                            EntInfo.NodeCode = "";


                            Atend.Global.Acad.AcadJigs.MyPolyLine poly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                            object ProductType = null;
                            if (poly != null)
                            {
                                if (poly.AdditionalDictionary.ContainsKey("ProductType"))
                                {
                                    poly.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                }

                            }
                            else
                            {
                                Atend.Global.Acad.AcadJigs.MyLine lin = ent as Atend.Global.Acad.AcadJigs.MyLine;
                                if (lin != null)
                                {

                                    if (lin.AdditionalDictionary.ContainsKey("ProductType"))
                                    {
                                        lin.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                    }


                                }
                                else
                                {
                                    Atend.Global.Acad.AcadJigs.MyCircle cir = ent as Atend.Global.Acad.AcadJigs.MyCircle;
                                    if (cir != null)
                                    {

                                        if (cir.AdditionalDictionary.ContainsKey("ProductType"))
                                        {
                                            cir.AdditionalDictionary.TryGetValue("ProductType", out ProductType);
                                        }


                                    }
                                }
                            }




                            if (ProductType != null)
                            {
                                EntInfo.NodeType = Convert.ToInt32(ProductType);
                            }
                            else
                            {
                                EntInfo.NodeType = 0;
                            }
                            EntInfo.ProductCode = 0;
                            EntInfo.Insert();

                            OIC.Add(NewEntOI);

                        }

                        ObjectId TransformerGroupOI = Atend.Global.Acad.Global.MakeGroup(NodeCode.ToString(), OIC);

                        Atend.Base.Acad.AT_INFO GroupInfo = new Atend.Base.Acad.AT_INFO(TransformerGroupOI);
                        GroupInfo.ParentCode = ParentCode.ToString();
                        GroupInfo.NodeCode = NodeCode.ToString();
                        GroupInfo.NodeType = (int)Atend.Control.Enum.ProductType.Transformer;
                        GroupInfo.ProductCode = ProductCode;
                        GroupInfo.Insert();

                        string comment = string.Format("Tr: {0} KVR", Atend.Base.Equipment.ETransformer.AccessSelectByCode(ProductCode).Capaciy);
                        ObjectId TextOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(Atend.Global.Acad.UAcad.WriteNote(comment,
                            new Point3d(transformerJig.MyCenterPoint.X,
                            transformerJig.MyCenterPoint.Y,
                            transformerJig.MyCenterPoint.Z)
                            , MyCommentScale), Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                        Atend.Base.Acad.AT_INFO TextInfo = new Atend.Base.Acad.AT_INFO(TextOi);
                        TextInfo.ParentCode = NodeCode.ToString();
                        TextInfo.NodeCode = "";
                        TextInfo.NodeType = (int)Atend.Control.Enum.ProductType.Comment;
                        TextInfo.ProductCode = 0;
                        TextInfo.Insert();

                        //ed.WriteMessage("ADD TO POST TRANSFORMER\n");
                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(TransformerGroupOI, PostContainerEntity.ObjectId);
                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(PostContainerEntity.ObjectId, TransformerGroupOI);
                        Atend.Base.Acad.AT_SUB.AddToAT_SUB(TextOi, TransformerGroupOI);


                    }


                    #endregion
                }
                else
                {
                    conti = false;
                }
            }


        }//



    }
}
