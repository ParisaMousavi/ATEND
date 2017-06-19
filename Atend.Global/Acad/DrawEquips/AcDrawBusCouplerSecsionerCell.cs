using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
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


    public class AcDrawBusCouplerSecsionerCell
    {

        //~~~~~~~~~~~~~~~~~~~~~~ class ~~~~~~~~~~~~~~~~~~//

    //    private class DrawBusCouplerSecsionerCellJig : DrawJig
    //    {

    //        Point3d CenterPoint = Point3d.Origin;
    //        List<Entity> Entities = new List<Entity>();
    //        int ProductCode = 0;
    //        string CodeGuid;
    //        double MyScale = 1;

    //        public DrawBusCouplerSecsionerCellJig(int CurrentProductCode, Guid Code, double Scale)
    //        {
    //            MyScale = Scale;

    //            CodeGuid = Code.ToString();
    //            ProductCode = CurrentProductCode;
    //            //cell
    //            Entities.Add(CreateCellEntity(CenterPoint, 40, 70));

    //            //bus
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 20, CenterPoint.Y + 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 10, CenterPoint.Y + 30, CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.Bus,
    //                190,
    //                0));

    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 10, CenterPoint.Y + 30, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 20, CenterPoint.Y + 30, CenterPoint.Z),
    //(int)Atend.Control.Enum.ProductType.Bus,
    //190,
    //0));


    //            //additional line
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 30, CenterPoint.Z),
    //new Point3d(CenterPoint.X - 10, CenterPoint.Y + 25, CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 20, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 10, CenterPoint.Y + 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));


    //            //keys
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 20, CenterPoint.Z),
    //new Point3d(CenterPoint.X - 5, CenterPoint.Y + 25, CenterPoint.Z),
    //(int)Atend.Control.Enum.ProductType.Key,
    //150,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X, CenterPoint.Y - 5, CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.Key,
    //                150,
    //                0));


    //            //ground

    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 5, CenterPoint.Y - 15, CenterPoint.Z),
    //                0,
    //                0,
    //                0));



    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 2, CenterPoint.Y - 15, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 8, CenterPoint.Y - 15, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 3, CenterPoint.Y - 16, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 7, CenterPoint.Y - 16, CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 4, CenterPoint.Y - 17, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 6, CenterPoint.Y - 17, CenterPoint.Z),
    //0,
    //0,
    //0));

    //        }

    //        private Entity CreateLine(Point3d StartPoint, Point3d EndPoint, int ProductType, int ColorIndex, double Thickness)
    //        {
    //            Atend.Global.Acad.AcadJigs.MyLine mLine = new Atend.Global.Acad.AcadJigs.MyLine();
    //            mLine.StartPoint = StartPoint;
    //            mLine.EndPoint = EndPoint;

    //            if (Thickness != 0)
    //            {
    //                mLine.Thickness = Thickness;
    //            }


    //            //if (ProductType != 0)
    //            //{
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, ProductType);
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, CodeGuid);
    //            //}
    //            //if (ColorIndex != 0)
    //            //{
    //            mLine.ColorIndex = ColorIndex;
    //            //}

    //            return mLine;
    //        }

    //        private Entity CreateConnectionPoint(Point3d CenterPoint, double Radius)
    //        {
    //            Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

    //            c.Center = CenterPoint;

    //            c.Normal = new Vector3d(0, 0, 1);

    //            c.Radius = Radius;

    //            c.ColorIndex = 3;

    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.ConnectionPoint);
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(c, CodeGuid);
    //            return c;
    //        }

    //        private Entity CreateCellEntity(Point3d CenterPoint, double Width, double Height)
    //        {

    //            double BaseX = CenterPoint.X;
    //            double BaseY = CenterPoint.Y;

    //            Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX + (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY + (Height / 2)), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(BaseX - (Width / 2), BaseY - (Height / 2)), 0, 0, 0);
    //            pLine.Closed = true;


    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.Cell);
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (long)ProductCode);
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
    //            return pLine;

    //        }

    //        private Entity CreateHeaderCable(Point3dCollection P3C)
    //        {

    //            Atend.Global.Acad.AcadJigs.MyPolyLine pLine = new Atend.Global.Acad.AcadJigs.MyPolyLine();
    //            pLine.ColorIndex = 40;
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[0].X, P3C[0].Y), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[1].X, P3C[1].Y), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[2].X, P3C[2].Y), 0, 0, 0);
    //            pLine.AddVertexAt(pLine.NumberOfVertices, new Point2d(P3C[0].X, P3C[0].Y), 0, 0, 0);

    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.HeaderCabel);
    //            Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
    //            pLine.Closed = true;

    //            return pLine;

    //        }

    //        protected override SamplerStatus Sampler(JigPrompts prompts)
    //        {

    //            JigPromptPointOptions ppo = new JigPromptPointOptions("\nBusCouplerSecsionerCell position:");
    //            PromptPointResult ppr = prompts.AcquirePoint(ppo);
    //            if (ppr.Status == PromptStatus.OK)
    //            {
    //                if (ppr.Value == CenterPoint)
    //                {
    //                    return SamplerStatus.NoChange;
    //                }
    //                else
    //                {
    //                    CenterPoint = ppr.Value;
    //                    return SamplerStatus.OK;
    //                }
    //            }
    //            else
    //            {
    //                return SamplerStatus.Cancel;
    //            }

    //        }

    //        protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
    //        {
    //            Entities.Clear();

    //            //cell
    //            Entities.Add(CreateCellEntity(CenterPoint, 40, 70));


    //            //bus
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 20, CenterPoint.Y + 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 10, CenterPoint.Y + 30, CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.Bus,
    //                190,
    //                0));

    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 10, CenterPoint.Y + 30, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 20, CenterPoint.Y + 30, CenterPoint.Z),
    //(int)Atend.Control.Enum.ProductType.Bus,
    //190,
    //0));


    //            //additional line
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 30, CenterPoint.Z),
    //new Point3d(CenterPoint.X - 10, CenterPoint.Y + 25, CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 20, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 10, CenterPoint.Y - 30, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 10, CenterPoint.Y + 30, CenterPoint.Z),
    //                0,
    //                0,
    //                0));


    //            //keys
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y + 20, CenterPoint.Z),
    //new Point3d(CenterPoint.X - 15, CenterPoint.Y + 25, CenterPoint.Z),
    //(int)Atend.Control.Enum.ProductType.Key,
    //150,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X, CenterPoint.Y - 5, CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.Key,
    //                150,
    //                0));


    //            //ground

    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X - 10, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X - 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 5, CenterPoint.Y - 10, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 5, CenterPoint.Y - 15, CenterPoint.Z),
    //                0,
    //                0,
    //                0));



    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 2, CenterPoint.Y - 15, CenterPoint.Z),
    //                new Point3d(CenterPoint.X + 8, CenterPoint.Y - 15, CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 3, CenterPoint.Y - 16, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 7, CenterPoint.Y - 16, CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(CenterPoint.X + 4, CenterPoint.Y - 17, CenterPoint.Z),
    //new Point3d(CenterPoint.X + 6, CenterPoint.Y - 17, CenterPoint.Z),
    //0,
    //0,
    //0));


    //            foreach (Entity ent in Entities)
    //            {
    //                draw.Geometry.Draw(ent);
    //            }
    //            return true;

    //        }

    //        public List<Entity> GetEntities()
    //        {
    //            return Entities;
    //        }

    //        public List<Entity> __GetDemo(Point3d _CenterPoint)
    //        {
    //            Entities.Clear();
    //            //cell
    //            Entities.Add(CreateCellEntity(_CenterPoint, 40, 70));

    //            //bus
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 20, _CenterPoint.Y + 30, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 30, _CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.Bus,
    //                190,
    //                0));

    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 10, _CenterPoint.Y + 30, _CenterPoint.Z),
    //new Point3d(_CenterPoint.X + 20, _CenterPoint.Y + 30, _CenterPoint.Z),
    //(int)Atend.Control.Enum.ProductType.Bus,
    //190,
    //0));


    //            //additional line
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 30, _CenterPoint.Z),
    //new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 25, _CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 20, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 30, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 30, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X + 10, _CenterPoint.Y - 30, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 10, _CenterPoint.Y - 30, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X + 10, _CenterPoint.Y + 30, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));


    //            //keys
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 20, _CenterPoint.Z),
    //         new Point3d(_CenterPoint.X - 15, _CenterPoint.Y + 25, _CenterPoint.Z),
    //         (int)Atend.Control.Enum.ProductType.Key,
    //         150,
    //         0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 5, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X, _CenterPoint.Y - 5, _CenterPoint.Z),
    //                (int)Atend.Control.Enum.ProductType.KeyElse,
    //                150,
    //                0));


    //            //ground

    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X - 5, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 10, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 15, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));



    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 2, _CenterPoint.Y - 15, _CenterPoint.Z),
    //                new Point3d(_CenterPoint.X + 8, _CenterPoint.Y - 15, _CenterPoint.Z),
    //                0,
    //                0,
    //                0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 3, _CenterPoint.Y - 16, _CenterPoint.Z),
    //new Point3d(_CenterPoint.X + 7, _CenterPoint.Y - 16, _CenterPoint.Z),
    //0,
    //0,
    //0));
    //            Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 4, _CenterPoint.Y - 17, _CenterPoint.Z),
    //new Point3d(_CenterPoint.X + 6, _CenterPoint.Y - 17, _CenterPoint.Z),
    //0,
    //0,
    //0));


    //            //Matrix3d trans1 = Matrix3d.Scaling(MyScale, new Point3d(CenterPoint.X, CenterPoint.Y, 0));
    //            //foreach (Entity en in Entities)
    //            //{
    //            //    en.TransformBy(trans1);
    //            //}


    //            return Entities;
    //        }

    //    }

        public class DrawBusCouplerSecsionerCellJig02 : DrawJig
        {

            Point3d CenterPoint = Point3d.Origin;
            List<Entity> Entities = new List<Entity>();
            int ProductCode = 0;
            string CodeGuid;
            double MyScale = 1;

            public DrawBusCouplerSecsionerCellJig02(int CurrentProductCode, Guid Code, double Scale)
            {
                MyScale = Scale;

                CodeGuid = Code.ToString();
                ProductCode = CurrentProductCode;

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
                Atend.Global.Acad.AcadJigs.SaveExtensionData(mLine, CodeGuid);
                //}
                //if (ColorIndex != 0)
                //{
                mLine.ColorIndex = ColorIndex;
                //}

                return mLine;
            }

            private Entity CreateConnectionPoint(Point3d CenterPoint, double Radius)
            {
                Atend.Global.Acad.AcadJigs.MyCircle c = new Atend.Global.Acad.AcadJigs.MyCircle();

                c.Center = CenterPoint;

                c.Normal = new Vector3d(0, 0, 1);

                c.Radius = Radius;

                c.ColorIndex = 3;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, (int)Atend.Control.Enum.ProductType.ConnectionPoint);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(c, CodeGuid);
                return c;
            }

            private Entity CreateCellEntity(Point3d CenterPoint, double Width, double Height)
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


                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.Cell);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (long)ProductCode);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
                return pLine;

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
                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
                pLine.Closed = true;

                return pLine;

            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {

                JigPromptPointOptions ppo = new JigPromptPointOptions("\nBusCouplerSecsionerCell position:");
                PromptPointResult ppr = prompts.AcquirePoint(ppo);
                if (ppr.Status == PromptStatus.OK)
                {
                    if (ppr.Value == CenterPoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        CenterPoint = ppr.Value;
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
                return true;

            }

            private Entity CreateBus1(Point3d BasePoint, int ColorIndex)
            {
                Atend.Global.Acad.AcadJigs.MyPolyLine BusEntity = new AcadJigs.MyPolyLine();
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X - 17.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X - 2.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X - 2.5, BasePoint.Y + 27.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X - 17.5, BasePoint.Y + 27.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X - 17.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.ColorIndex = ColorIndex;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, (int)Atend.Control.Enum.ProductType.Bus);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, (long)ProductCode);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, CodeGuid);

                return BusEntity;
            }

            private Entity CreateBus2(Point3d BasePoint, int ColorIndex)
            {
                Atend.Global.Acad.AcadJigs.MyPolyLine BusEntity = new AcadJigs.MyPolyLine();
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X + 2.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X + 17.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X + 17.5, BasePoint.Y + 27.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X + 2.5, BasePoint.Y + 27.5), 0, 0, 0);
                BusEntity.AddVertexAt(BusEntity.NumberOfVertices, new Point2d(BasePoint.X + 2.5, BasePoint.Y + 32.5), 0, 0, 0);
                BusEntity.ColorIndex = ColorIndex;

                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, (int)Atend.Control.Enum.ProductType.Bus);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, (long)ProductCode);
                Atend.Global.Acad.AcadJigs.SaveExtensionData(BusEntity, CodeGuid);

                return BusEntity;
            }

            public List<Entity> GetEntities()
            {
                return Entities;
            }

            public List<Entity> GetDemo(Point3d _CenterPoint)
            {
                Entities.Clear();
                //cell
                Entities.Add(CreateCellEntity(_CenterPoint, 40, 70));

                //bus
                Entities.Add(CreateBus1(_CenterPoint, 190));
                Entities.Add(CreateBus2(_CenterPoint, 190));

                //additional line
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 30, _CenterPoint.Z), new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 25, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 20, _CenterPoint.Z), new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 30, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 30, _CenterPoint.Z), new Point3d(_CenterPoint.X + 10, _CenterPoint.Y - 30, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 10, _CenterPoint.Y - 30, _CenterPoint.Z), new Point3d(_CenterPoint.X + 10, _CenterPoint.Y + 30, _CenterPoint.Z), 0, 0, 0));

                //keys
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y + 20, _CenterPoint.Z), new Point3d(_CenterPoint.X - 15, _CenterPoint.Y + 25, _CenterPoint.Z), (int)Atend.Control.Enum.ProductType.Key, 150, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 5, _CenterPoint.Y - 10, _CenterPoint.Z), new Point3d(_CenterPoint.X, _CenterPoint.Y - 5, _CenterPoint.Z), (int)Atend.Control.Enum.ProductType.KeyElse, 150, 0));

                //ground
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X - 10, _CenterPoint.Y - 10, _CenterPoint.Z), new Point3d(_CenterPoint.X - 5, _CenterPoint.Y - 10, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X, _CenterPoint.Y - 10, _CenterPoint.Z), new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 10, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 10, _CenterPoint.Z), new Point3d(_CenterPoint.X + 5, _CenterPoint.Y - 15, _CenterPoint.Z), 0, 0, 0));

                Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 2, _CenterPoint.Y - 15, _CenterPoint.Z), new Point3d(_CenterPoint.X + 8, _CenterPoint.Y - 15, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 3, _CenterPoint.Y - 16, _CenterPoint.Z), new Point3d(_CenterPoint.X + 7, _CenterPoint.Y - 16, _CenterPoint.Z), 0, 0, 0));
                Entities.Add(CreateLine(new Point3d(_CenterPoint.X + 4, _CenterPoint.Y - 17, _CenterPoint.Z), new Point3d(_CenterPoint.X + 6, _CenterPoint.Y - 17, _CenterPoint.Z), 0, 0, 0));

                return Entities;
            }

        }




        public void DrawBusCouplerSecsionerCell(int ProductCode, Guid Code)
        {

            //////////bool conti = true;
            //////////Atend.Global.Acad.AcadJigs.DRawBusCouplerSecsionerCellJig DBDC = new Atend.Global.Acad.AcadJigs.DRawBusCouplerSecsionerCellJig(ProductCode, Code);
            //////////PromptResult pr;

            //////////while (conti)
            //////////{
            //////////    pr = ed.Drag(DBDC);
            //////////    if (pr.Status == PromptStatus.OK)
            //////////    {
            //////////        conti = false;
            //////////    }
            //////////    else
            //////////    {
            //////////        conti = false;
            //////////    }

            //////////}

        }//end method


    }
}
