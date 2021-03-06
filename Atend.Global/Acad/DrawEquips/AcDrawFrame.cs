﻿using System;
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

//update from tehran 7/15
namespace Atend.Global.Acad.DrawEquips
{
    public class AcDrawFrame
    {
        private Atend.Base.Design.DDesignProfile CurrentDesignInfo;
        private Point3d BasePoint;

        private double frameWidth;
        public double FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        private double frameHeigh;
        public double FrameHeigh
        {
            get { return frameHeigh; }
            set { frameHeigh = value; }
        }


        private System.Data.DataTable products = new System.Data.DataTable();
        public System.Data.DataTable Products
        {
            get { return products; }
            set { products = value; }
        }


        private bool haveSign;
        public bool HaveSign
        {
            get { return haveSign; }
            set { haveSign = value; }
        }

        private bool haveDescription;
        public bool HaveDescription
        {
            get { return haveDescription; }
            set { haveDescription = value; }
        }

        private bool haveInformation;
        public bool HaveInformation
        {
            get { return haveInformation; }
            set { haveInformation = value; }
        }



        class DrawFrameJig : DrawJig
        {

            private string designName;
            public string DesignName
            {
                get { return designName; }
                set { designName = value; }
            }

            private string designCode;
            public string DesignCode
            {
                get { return designCode; }
                set { designCode = value; }
            }

            private DateTime designDate;
            public DateTime DesignDate
            {
                get { return designDate; }
                set { designDate = value; }
            }

            private float scale;
            public float Scale
            {
                get { return scale; }
                set { scale = value; }
            }

            private string address;
            public string Address
            {
                get { return address; }
                set { address = value; }
            }

            //private string zone;
            //public string Zone
            //{
            //    get { return zone; }
            //    set { zone = value; }
            //}

            private string validate;
            public string Validate
            {
                get { return validate; }
                set { validate = value; }
            }

            //private string employer;
            //public string Employer
            //{
            //    get { return employer; }
            //    set { employer = value; }
            //}

            private string adviser;
            public string Adviser
            {
                get { return adviser; }
                set { adviser = value; }
            }

            private string designer;
            public string Designer
            {
                get { return designer; }
                set { designer = value; }
            }

            private string controller;
            public string Controller
            {
                get { return controller; }
                set { controller = value; }
            }

            private string supporter;
            public string Supporter
            {
                get { return supporter; }
                set { supporter = value; }
            }

            private string approval;
            public string Approval
            {
                get { return approval; }
                set { approval = value; }
            }

            private string planner;
            public string Planner
            {
                get { return planner; }
                set { planner = value; }
            }

            private List<Atend.Control.Enum.ProductType> usedEquips;
            public List<Atend.Control.Enum.ProductType> UsedEquips
            {
                get { return usedEquips; }
                set { usedEquips = value; }
            }

            public Point3d BasePoint = Point3d.Origin;
            List<Entity> Entities = new List<Entity>();
            double MyWidth = 0, MyHeight = 0;


            public DrawFrameJig(double Width, double Height)
            {
                MyWidth = Width;
                MyHeight = Height;
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {

                JigPromptPointOptions PPO = new JigPromptPointOptions("Select Frame Position");
                PromptPointResult PPR = prompts.AcquirePoint(PPO);
                if (PPR.Status == PromptStatus.OK)
                {
                    if (PPR.Value == BasePoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        BasePoint = PPR.Value;
                        return SamplerStatus.OK;
                    }
                }
                else
                {
                    return SamplerStatus.Cancel;
                }
            }

            private Entity CreateFrame(Point3d CenterPoint, double Width, double Height)
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


                Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (int)Atend.Control.Enum.ProductType.Frame);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, (long)ProductCode);
                //Atend.Global.Acad.AcadJigs.SaveExtensionData(pLine, CodeGuid);
                return pLine;

            }

            //private List<Entity> CreateInformationTable(Point3d TopLeftCorner, double Width)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    List<Entity> ents = new List<Entity>();
            //    Point3d StartPoint = TopLeftCorner;
            //    Point3d EndPoint = new Point3d(TopLeftCorner.X + Width, TopLeftCorner.Y, TopLeftCorner.Z);
            //    Point3dCollection TextPoints = new Point3dCollection();


            //    for (int Linecounter = 1; Linecounter <= 9; Linecounter++)
            //    {
            //        Atend.Global.Acad.AcadJigs.MyLine l = new Atend.Global.Acad.AcadJigs.MyLine();
            //        l.StartPoint = new Point3d(StartPoint.X, StartPoint.Y - ((Linecounter - 1) * 10), StartPoint.Z);
            //        l.EndPoint = new Point3d(EndPoint.X, EndPoint.Y - ((Linecounter - 1) * 10), EndPoint.Z);
            //        ents.Add(l);
            //        Atend.Global.Acad.AcadJigs.SaveExtensionData(l, (int)Atend.Control.Enum.ProductType.FrameInformation);

            //        TextPoints.Add(new Point3d(EndPoint.X - (1 * Width / 3) /*- (EndPoint.X / 20)*/, EndPoint.Y - ((Linecounter - 1) * 10) - 5, EndPoint.Z));
            //        TextPoints.Add(new Point3d(EndPoint.X - (2 * Width / 3) /*- (EndPoint.X / 20)*/, EndPoint.Y - ((Linecounter - 1) * 10) - 5, EndPoint.Z));
            //        TextPoints.Add(new Point3d(EndPoint.X - (3 * Width / 3) /*- (EndPoint.X / 20)*/, EndPoint.Y - ((Linecounter - 1) * 10) - 5, EndPoint.Z));
            //    }


            //    EndPoint = new Point3d(TopLeftCorner.X, TopLeftCorner.Y - (8 * 10), TopLeftCorner.Z);
            //    for (int Linecounter = 1; Linecounter <= 4; Linecounter++)
            //    {
            //        if (Linecounter == 2)
            //        {
            //            StartPoint = new Point3d(TopLeftCorner.X, TopLeftCorner.Y - (3 * 10), TopLeftCorner.Z);
            //        }
            //        else if (Linecounter == 3)
            //        {
            //            StartPoint = new Point3d(TopLeftCorner.X, TopLeftCorner.Y - (4 * 10), TopLeftCorner.Z);
            //        }
            //        else
            //        {
            //            StartPoint = TopLeftCorner;
            //        }
            //        Atend.Global.Acad.AcadJigs.MyLine l = new Atend.Global.Acad.AcadJigs.MyLine();
            //        l.StartPoint = new Point3d(StartPoint.X + ((Linecounter - 1) * Width / 3), StartPoint.Y, StartPoint.Z);
            //        l.EndPoint = new Point3d(EndPoint.X + ((Linecounter - 1) * Width / 3), EndPoint.Y, EndPoint.Z);
            //        ents.Add(l);
            //        Atend.Global.Acad.AcadJigs.SaveExtensionData(l, (int)Atend.Control.Enum.ProductType.FrameInformation);
            //    }



            //    DBText dbText;
            //    int counter = 1;
            //    foreach (Point3d _p in TextPoints)
            //    {
            //        switch (counter)
            //        {
            //            case 3:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "شرکت", "توزیع نیروی برق نواحی تهران");
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 6:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "عنوان", DesignName);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 9:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "آدرس", Address);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;

            //            case 11:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "شماره طرح", DesignCode);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 12:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "طراح", Designer);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 13:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "ویرایش", "ندارد");
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;

            //            case 14:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "نقشه بردار", Planner);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 15:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "کنترل", Controller);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 16:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "شماره شیت", "ندارد");
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 17:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "ترسیم", "ندارد");
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;

            //            case 18:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "تایید کننده", Supporter);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 19:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:   1:{1}", "مقیاس", Scale);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 20:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "تاریخ", "ندارد"/* string.Format("{0}/{1:00}/{2:00}", p.GetYear(DesignDate), p.GetMonth(DesignDate), p.GetDayOfMonth(DesignDate))*/);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 21:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "تصویب", Approval);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 23:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "مدت اعتبار", Validate);
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;
            //            case 24:
            //                dbText = new DBText();
            //                dbText.Position = _p;
            //                dbText.TextString = string.Format("{0}:{1}", "تهیه کننده", "ندارد");
            //                dbText.Height = 4;
            //                ents.Add(dbText);
            //                break;


            //        }

            //        counter++;
            //    }

            //    return ents;
            //}

            private List<Entity> DrawPole()
            {
                List<Entity> entities = new List<Entity>();





                return entities;
            }

            private List<Entity> CreateUsedEquipsTable(Point3d TopLeftCorner, double Width)
            {

                List<Entity> entities = new List<Entity>();

                foreach (Atend.Control.Enum.ProductType PT in UsedEquips)
                {
                    switch (PT)
                    {
                        case Atend.Control.Enum.ProductType.Pole:
                            break;
                        case Atend.Control.Enum.ProductType.GroundPost:
                            break;
                        case Atend.Control.Enum.ProductType.AirPost:
                            break;
                        case Atend.Control.Enum.ProductType.Conductor:
                            break;
                        case Atend.Control.Enum.ProductType.GroundCabel:
                            break;
                        case Atend.Control.Enum.ProductType.Rod:
                            break;
                        case Atend.Control.Enum.ProductType.Khazan:
                            break;
                        case Atend.Control.Enum.ProductType.Ground:
                            break;
                        case Atend.Control.Enum.ProductType.Light:
                            break;
                    }
                }


                return entities;

            }

            protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
            {

                Entities.Clear();
                Entities.Add(CreateFrame(BasePoint, MyWidth, MyHeight));


                //Point3d p = new Point3d(BasePoint.X + (MyWidth / 2) - (MyWidth / 4), BasePoint.Y - ((MyHeight / 2) - 80), BasePoint.Z);
                //List<Entity> InfoEntities = CreateInformationTable(p, MyWidth / 4);
                //foreach (Entity ent1 in InfoEntities)
                //{
                //    Entities.Add(ent1);
                //}



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

        public AcDrawFrame()
        {
            HaveSign = true;
            HaveDescription = true;
            HaveInformation = true;

        }

        public void DrawFrame()
        {
            bool conti = true;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            double Width = FrameWidth;
            double Height = FrameHeigh;
            CurrentDesignInfo = Atend.Base.Design.DDesignProfile.AccessSelect();
            //ed.WriteMessage("W:{0} , H:{1} \n",Width,Height);
            DrawFrameJig _DrawFrameJig = new DrawFrameJig(Width, Height);
            PromptResult pr;

            _DrawFrameJig.DesignName = CurrentDesignInfo.DesignName;
            _DrawFrameJig.Address = CurrentDesignInfo.Address;
            _DrawFrameJig.DesignCode = CurrentDesignInfo.DesignCode;
            _DrawFrameJig.Designer = CurrentDesignInfo.Designer;
            _DrawFrameJig.Planner = CurrentDesignInfo.Planner;
            //_DrawFrameJig.UsedEquips = MyUsedEquips;

            while (conti)
            {
                pr = ed.Drag(_DrawFrameJig);
                if (pr.Status == PromptStatus.OK)
                {
                    conti = false;
                    List<Entity> Entities = _DrawFrameJig.GetEntities();
                    //ed.WriteMessage("count of entity : {0} \n",Entities.Count);
                    ObjectIdCollection Frames = new ObjectIdCollection();
                    ObjectIdCollection FrameInformation = new ObjectIdCollection();
                    ObjectIdCollection AllEntities = new ObjectIdCollection();
                    foreach (Entity ent in Entities)
                    {
                        object ProductType = null;
                        ObjectId NewOi = ObjectId.Null;
                        Atend.Global.Acad.AcadJigs.MyPolyLine MyPoly = ent as Atend.Global.Acad.AcadJigs.MyPolyLine;
                        if (MyPoly != null && MyPoly.AdditionalDictionary.TryGetValue("ProductType", out ProductType))
                        {
                            if (ProductType != null && ((Atend.Control.Enum.ProductType)ProductType) == Atend.Control.Enum.ProductType.Frame)
                            {
                                NewOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());


                                Atend.Base.Acad.AT_INFO EntityInfo = new Atend.Base.Acad.AT_INFO(NewOi);
                                EntityInfo.ParentCode = "";
                                EntityInfo.NodeCode = "";
                                EntityInfo.NodeType = (int)Atend.Control.Enum.ProductType.FrameAbraviation;
                                EntityInfo.ProductCode = 0;
                                EntityInfo.Angle = 0;
                                EntityInfo.Insert();

                                Frames.Add(NewOi);
                                //ed.WriteMessage("Frame:{0} \n", NewOi);
                            }
                        }
                        else
                        {
                            Atend.Global.Acad.AcadJigs.MyLine MyLin = ent as Atend.Global.Acad.AcadJigs.MyLine;
                            if (MyLin != null && MyLin.AdditionalDictionary.TryGetValue("ProductType", out ProductType))
                            {
                                if (ProductType != null && ((Atend.Control.Enum.ProductType)ProductType) == Atend.Control.Enum.ProductType.FrameInformation)
                                {
                                    NewOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                                    FrameInformation.Add(NewOi);
                                    //ed.WriteMessage("FrameInformation:{0} \n", NewOi);
                                }
                            }
                            else
                            {
                                //ed.WriteMessage("db text was written here \n");
                                NewOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

                                //ed.WriteMessage("other:{0} \n", NewOi);
                            }
                        }

                        if (NewOi != ObjectId.Null)
                        {
                            AllEntities.Add(NewOi);
                        }
                    }

                    BasePoint = _DrawFrameJig.BasePoint;
                    Point3d p = new Point3d(BasePoint.X + (FrameWidth / 4), BasePoint.Y - ((Height / 2) - 80), BasePoint.Z);
                    if (HaveInformation)
                    {
                        AllEntities.Add(CreateInformationTable02(p, Width / 4, Height));
                    }

                    if (HaveSign)
                    {
                        ObjectIdCollection SignOIs = CreateSignTable(new Point3d(BasePoint.X + (FrameWidth / 16) * 5, BasePoint.Y + (frameHeigh / 2), 0), new Point3d(BasePoint.X + (FrameWidth / 4), BasePoint.Y + (Height / 2), BasePoint.Z));

                        foreach (ObjectId oi in SignOIs)
                        {
                            AllEntities.Add(oi);
                        }
                    }

                    Atend.Global.Acad.Global.MakeGroup("Frame-" + Guid.NewGuid().ToString(), AllEntities);

                }
                else
                {
                    conti = false;
                }
            }
        }

        private ObjectId CreateInformationTable02(Point3d TopLeftCorner, double Width, double Height)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            CurrentDesignInfo = Atend.Base.Design.DDesignProfile.AccessSelect();

            Table tb = new Table();
            //tb.NumRows = 8;
            //tb.NumColumns = 6;
            tb.SetSize(8,6);
            tb.SetRowHeight(3);
            tb.SetColumnWidth(Width / 6);
            
            //tb.MergeCells(new CellRange(new Cell(0, 2), new Cell(0, 5)));
            tb.MergeCells(CellRange.Create(tb, 0, 2, 0, 5));

            //tb.MergeCells(new CellRange(new Cell(1, 0), new Cell(1, 4)));
            tb.MergeCells(CellRange.Create(tb, 1, 0, 1, 4));

            //tb.MergeCells(new CellRange(new Cell(2, 0), new Cell(2, 4)));
            tb.MergeCells(CellRange.Create(tb,2,0,2,4));

            //tb.MergeCells(new CellRange(new Cell(3, 2), new Cell(3, 4)));
            tb.MergeCells(CellRange.Create(tb,3,2,3,4));
            string[,] str = new string[8, 6];
            str[0, 0] = "ندارد";
            str[0, 1] = "تهیه کننده";
            str[0, 2] = "شرکت توزیع نیروی برق نواحی تهران";
            str[0, 3] = "";
            str[0, 4] = "";
            str[0, 5] = "";

            str[1, 0] = CurrentDesignInfo.DesignName;
            str[1, 1] = "";
            str[1, 2] = "";
            str[1, 3] = "";
            str[1, 4] = "";
            str[1, 5] = "عنوان";

            str[2, 0] = CurrentDesignInfo.Address;
            str[2, 1] = "";
            str[2, 2] = "";
            str[2, 3] = "";
            str[2, 4] = "";
            str[2, 5] = "نشانی";

            str[3, 0] = CurrentDesignInfo.Designer;
            str[3, 1] = "طراح";
            str[3, 2] = CurrentDesignInfo.DesignCode;
            str[3, 3] = "";
            str[3, 4] = "";
            str[3, 5] = "شماره طرح";

            str[4, 0] = CurrentDesignInfo.Controller;
            str[4, 1] = "کنترل";
            str[4, 2] = CurrentDesignInfo.Planner;
            str[4, 3] = "نقشه بردار";
            str[4, 4] = "";
            str[4, 5] = "ویرایش";

            str[5, 0] = CurrentDesignInfo.Supporter;
            str[5, 1] = "تایید";
            str[5, 2] = "";
            str[5, 3] = "ترسیم";
            str[5, 4] = "";
            str[5, 5] = "شماره شیت";
            str[6, 0] = CurrentDesignInfo.Approval;
            str[6, 1] = "تصویب";

            string CurrentDate = "";
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            CurrentDate = string.Format("{0}/{1:00}/{2:00}", p.GetYear(CurrentDesignInfo.DesignDate), p.GetMonth(CurrentDesignInfo.DesignDate), p.GetDayOfMonth(CurrentDesignInfo.DesignDate));
            str[6, 2] = CurrentDate;
            str[6, 3] = "تاریخ";
            str[6, 4] = CurrentDesignInfo.Scale.ToString();
            str[6, 5] = "مقیاس";

            str[7, 0] = CurrentDesignInfo.Adviser;
            str[7, 1] = "تهیه کننده";
            str[7, 2] = CurrentDesignInfo.Validate;
            str[7, 3] = "مدت اعتبار";
            str[7, 4] = "";
            str[7, 5] = "";


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    tb.SetTextHeight(i, j, 2);
                    tb.SetTextString(i, j, str[i, j]);
                    tb.SetAlignment(i, j, CellAlignment.MiddleRight);
                }
            }
            tb.Position = new Point3d(TopLeftCorner.X, (BasePoint.Y - (Height / 2)) + (Height / 8), 0);
            tb.GenerateLayout();
            ObjectId TableOi = Atend.Global.Acad.UAcad.DrawEntityOnScreen(tb, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());

            Atend.Base.Acad.AT_INFO TableInfo = new Atend.Base.Acad.AT_INFO(TableOi);
            TableInfo.ParentCode = "";
            TableInfo.NodeCode = "";
            TableInfo.NodeType = (int)Atend.Control.Enum.ProductType.FrameInformation;
            TableInfo.ProductCode = 0;
            TableInfo.Angle = 0;
            TableInfo.Insert();


            return TableOi;
        }

        private ObjectIdCollection CreateSignTable(Point3d StartPoint, Point3d TopLeftCorner)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int Counter = 30;
            List<Entity> Entities;
            List<Entity> AllEntities = new List<Entity>();
            foreach (System.Data.DataRow dr in products.Rows)
            {
                //ed.WriteMessage("Counter:{0}\n", Counter);
                switch ((Atend.Control.Enum.FrameProductType)Convert.ToInt32(dr["Type"]))
                {
                    case Atend.Control.Enum.FrameProductType.Pole:

                        Entities = PoleSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.PoleCircle:

                        Entities = PoleCircleSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.PolePolygon:

                        Entities = PolePolygonSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;


                    case Atend.Control.Enum.FrameProductType.Conductor:

                        Entities = ConductorSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;
                    case Atend.Control.Enum.FrameProductType.GroundCable:
                        Entities = GroundCableSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }


                        break;
                    case Atend.Control.Enum.FrameProductType.SelfKeeper:
                        Entities = SelfKeeperSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;
                    case Atend.Control.Enum.FrameProductType.HeaderCable:
                        Entities = HeaderCableSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;
                    case Atend.Control.Enum.FrameProductType.Klamp:
                        Entities = KlampSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                            ed.WriteMessage("Klamp\n");
                        }


                        break;
                    case Atend.Control.Enum.FrameProductType.Kablsho:
                        Entities = KablshoSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                            ed.WriteMessage("Kablsho\n");
                        }

                        break;
                    case Atend.Control.Enum.FrameProductType.Breaker:
                        Entities = BreakerSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.Disconnector:
                        Entities = DisconnectorSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.GroundPostOn:
                        Entities = GroundPostOnSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.GroundPostUnder:
                        Entities = GroundPostUnderSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;

                    case Atend.Control.Enum.FrameProductType.GroundPostKiusk:
                        Entities = GroundPostKiuskSign(new Point3d(StartPoint.X, StartPoint.Y - Counter, 0), Convert.ToString(dr["SignText"]));
                        foreach (Entity ent in Entities)
                        {
                            //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                            AllEntities.Add(ent);
                        }

                        break;


                }
                Counter += 16;
            }


            double TotalHeightTable = Counter;
            double TotalHeight = FrameHeigh / 3;


            //ed.WriteMessage("Math.Round(TotalHeight / TotalHeightTable):{0}\n", TotalHeight / TotalHeightTable);
            //ed.WriteMessage("TotalHeight:{0}\n", TotalHeight);
            //ed.WriteMessage("TotalHeightTable:{0}\n", TotalHeightTable);
            if (TotalHeightTable > TotalHeight)
            {
                Matrix3d trans1 = Matrix3d.Scaling(TotalHeight / TotalHeightTable, StartPoint);
                foreach (Entity ent in AllEntities)
                {
                    ent.TransformBy(trans1);

                }
            }
            Entities = FrameSign(new Point3d(TopLeftCorner.X, TopLeftCorner.Y, 0), FrameWidth / 4, FrameHeigh / 3);
            foreach (Entity ent in Entities)
            {
                //Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                AllEntities.Add(ent);
            }

            ObjectIdCollection oic = new ObjectIdCollection();
            foreach (Entity ent in AllEntities)
            {
                ObjectId tmpObjectId = Atend.Global.Acad.UAcad.DrawEntityOnScreen(ent, Atend.Control.Enum.AutoCadLayerName.GENERAL.ToString());
                oic.Add(tmpObjectId);
                Atend.Base.Acad.AT_INFO EntityInfo = new Atend.Base.Acad.AT_INFO(tmpObjectId);
                EntityInfo.ParentCode = "";
                EntityInfo.NodeCode = "";
                EntityInfo.NodeType = (int)Atend.Control.Enum.ProductType.FrameAbraviation;
                EntityInfo.ProductCode = 0;
                EntityInfo.Angle = 0;
                EntityInfo.Insert();
            }
            return oic;

        }

        private List<Entity> FrameSign(Point3d TopLeftCorner, double Width, double Height)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = TopLeftCorner.X;
            double BaseY = TopLeftCorner.Y;
            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX, BaseY), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + Width, BaseY), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + Width, BaseY - Height), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX, BaseY - Height), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX, BaseY), 0, 0, 0);
            pl.Closed = true;

            Entities.Add(pl);

            return Entities;
        }

        private List<Entity> PolePolygonSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;

            Polyline Pl = new Polyline();
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 3), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX + 3, BaseY + 3), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX + 6, BaseY), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX + 3, BaseY - 3), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY - 3), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY), 0, 0, 0);
            Pl.AddVertexAt(Pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 3), 0, 0, 0);
            Pl.Closed = true;
            Entities.Add(Pl);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> PoleCircleSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Circle c = new Circle(new Point3d(BaseX, BaseY, 0), new Vector3d(0, 0, 1), 6);
            Entities.Add(c);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> PoleSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 3, BaseY + 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 3, BaseY - 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY - 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 3, BaseY + 6), 0, 0, 0);
            pl.Closed = true;

            Entities.Add(pl);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> ConductorSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Line l = new Line();
            l.StartPoint = new Point3d(BaseX - 6, BaseY, 0);
            l.EndPoint = new Point3d(BaseX + 6, BaseY, 0);
            Entities.Add(l);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> GroundCableSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Line l = new Line();
            l.StartPoint = new Point3d(BaseX - 6, BaseY, 0);
            l.EndPoint = new Point3d(BaseX + 6, BaseY, 0);
            Entities.Add(l);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> SelfKeeperSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Line l = new Line();
            l.StartPoint = new Point3d(BaseX - 6, BaseY, 0);
            l.EndPoint = new Point3d(BaseX + 6, BaseY, 0);
            Entities.Add(l);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);

            return Entities;
        }

        private List<Entity> HeaderCableSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 6, BaseY), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY - 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;
        }

        private List<Entity> KlampSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 6, BaseY), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY - 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;
        }

        private List<Entity> KablshoSign(Point3d BasePoint, string Text)
        {
            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;
            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 6, BaseY), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY - 6), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 6, BaseY + 6), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;
        }

        private List<Entity> BreakerSign(Point3d BasePoint, string Text)
        {

            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;


            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY + 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY - 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY - 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 2), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            Line l1 = new Line();
            l1.StartPoint = new Point3d(BaseX - 2, BaseY, 0);
            l1.EndPoint = new Point3d(BaseX - 6, BaseY, 0);
            Entities.Add(l1);

            Line l2 = new Line();
            l2.StartPoint = new Point3d(BaseX + 2, BaseY, 0);
            l2.EndPoint = new Point3d(BaseX + 6, BaseY, 0);
            Entities.Add(l2);

            Line l3 = new Line();
            l3.StartPoint = new Point3d(BaseX - 2, BaseY, 0);
            l3.EndPoint = new Point3d(BaseX + 2, BaseY + 1, 0);
            Entities.Add(l3);

            Circle c1 = new Circle(new Point3d(BaseX - 2, BaseY, 0), new Vector3d(0, 0, 1), 0.5);
            Entities.Add(c1);

            Circle c2 = new Circle(new Point3d(BaseX + 2, BaseY, 0), new Vector3d(0, 0, 1), 0.5);
            Entities.Add(c2);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);



            return Entities;

        }

        private List<Entity> DisconnectorSign(Point3d BasePoint, string Text)
        {

            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;


            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY + 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY - 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY - 2), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 2), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            Line l1 = new Line();
            l1.StartPoint = new Point3d(BaseX - 2, BaseY, 0);
            l1.EndPoint = new Point3d(BaseX - 6, BaseY, 0);
            Entities.Add(l1);

            Line l2 = new Line();
            l2.StartPoint = new Point3d(BaseX + 2, BaseY, 0);
            l2.EndPoint = new Point3d(BaseX + 6, BaseY, 0);
            Entities.Add(l2);

            Line l3 = new Line();
            l3.StartPoint = new Point3d(BaseX - 2, BaseY, 0);
            l3.EndPoint = new Point3d(BaseX + 2, BaseY + 1, 0);
            Entities.Add(l3);

            Line l4 = new Line();
            l4.StartPoint = new Point3d(BaseX + 2, BaseY + 0.5, 0);
            l4.EndPoint = new Point3d(BaseX + 2, BaseY - 0.5, 0);
            Entities.Add(l4);

            Circle c1 = new Circle(new Point3d(BaseX - 2, BaseY, 0), new Vector3d(0, 0, 1), 0.5);
            Entities.Add(c1);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;

        }

        private List<Entity> GroundPostKiuskSign(Point3d BasePoint, string Text)
        {

            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;

            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            Line l1 = new Line();
            l1.StartPoint = new Point3d(BaseX, BaseY + 8, 0);
            l1.EndPoint = new Point3d(BaseX - 5, BaseY + 3, 0);
            Entities.Add(l1);

            Line l2 = new Line();
            l2.StartPoint = new Point3d(BaseX, BaseY + 8, 0);
            l2.EndPoint = new Point3d(BaseX + 5, BaseY + 3, 0);
            Entities.Add(l2);


            Circle c1 = new Circle(new Point3d(BaseX, BaseY, 0), new Vector3d(0, 0, 1), 3);
            Entities.Add(c1);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;

        }

        private List<Entity> GroundPostUnderSign(Point3d BasePoint, string Text)
        {

            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;

            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            Line l1 = new Line();
            l1.StartPoint = new Point3d(BaseX - 4, BaseY + 4, 0);
            l1.EndPoint = new Point3d(BaseX, BaseY, 0);
            Entities.Add(l1);

            Line l2 = new Line();
            l2.StartPoint = new Point3d(BaseX + 4, BaseY + 4, 0);
            l2.EndPoint = new Point3d(BaseX, BaseY, 0);
            Entities.Add(l2);


            Circle c1 = new Circle(new Point3d(BaseX, BaseY, 0), new Vector3d(0, 0, 1), 3);
            Entities.Add(c1);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;

        }

        private List<Entity> GroundPostOnSign(Point3d BasePoint, string Text)
        {

            List<Entity> Entities = new List<Entity>();

            double BaseX = BasePoint.X;
            double BaseY = BasePoint.Y;

            Polyline pl = new Polyline();
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY + 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX + 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY - 4), 0, 0, 0);
            pl.AddVertexAt(pl.NumberOfVertices, new Point2d(BaseX - 4, BaseY + 4), 0, 0, 0);
            pl.Closed = true;
            Entities.Add(pl);

            Circle c1 = new Circle(new Point3d(BaseX, BaseY, 0), new Vector3d(0, 0, 1), 3);
            Entities.Add(c1);

            DBText dbText = new DBText();
            dbText.Position = new Point3d(BaseX + 16, BaseY, 0);
            dbText.TextString = Text;
            dbText.Height = 5;
            Entities.Add(dbText);


            return Entities;

        }

        public void UpdateFrame()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            CurrentDesignInfo = Atend.Base.Design.DDesignProfile.AccessSelect();
            using (DocumentLock dlock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                TypedValue[] tvs = new TypedValue[] { new TypedValue((int)DxfCode.Start, "TABLE") };
                SelectionFilter sf = new SelectionFilter(tvs);
                PromptSelectionResult psr = ed.SelectAll();
                try
                {
                    if (psr.Value.Count > 0)
                    {
                        ObjectId[] ids = psr.Value.GetObjectIds();
                        foreach (ObjectId oi in ids)
                        {
                            Atend.Base.Acad.AT_INFO TableInfo = Atend.Base.Acad.AT_INFO.SelectBySelectedObjectId(oi);
                            if (TableInfo.ParentCode != "NONE" && TableInfo.NodeType == (int)Atend.Control.Enum.ProductType.FrameInformation)
                            {
                                //ed.WriteMessage("TOI:{0} \n", oi);
                                Entity ent;
                                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                                using (Transaction tr = db.TransactionManager.StartTransaction())
                                {

                                    ent = (Entity)tr.GetObject(oi, OpenMode.ForWrite);
                                    Table tb = ent as Table;
                                    if (tb != null)
                                    {
                                        string[,] str = new string[8, 6];
                                        str[0, 0] = "ندارد";
                                        str[0, 1] = "تهیه کننده";
                                        str[0, 2] = "شرکت توزیع نیروی برق نواحی تهران";
                                        str[0, 3] = "";
                                        str[0, 4] = "";
                                        str[0, 5] = "";

                                        str[1, 0] = CurrentDesignInfo.DesignName;
                                        str[1, 1] = "";
                                        str[1, 2] = "";
                                        str[1, 3] = "";
                                        str[1, 4] = "";
                                        str[1, 5] = "عنوان";

                                        str[2, 0] = CurrentDesignInfo.Address;
                                        str[2, 1] = "";
                                        str[2, 2] = "";
                                        str[2, 3] = "";
                                        str[2, 4] = "";
                                        str[2, 5] = "نشانی";

                                        str[3, 0] = CurrentDesignInfo.Designer;
                                        str[3, 1] = "طراح";
                                        str[3, 2] = CurrentDesignInfo.DesignCode;
                                        str[3, 3] = "";
                                        str[3, 4] = "";
                                        str[3, 5] = "شماره طرح";

                                        str[4, 0] = CurrentDesignInfo.Controller;
                                        str[4, 1] = "کنترل";
                                        str[4, 2] = CurrentDesignInfo.Planner;
                                        str[4, 3] = "نقشه بردار";
                                        str[4, 4] = "";
                                        str[4, 5] = "ویرایش";

                                        str[5, 0] = CurrentDesignInfo.Supporter;
                                        str[5, 1] = "تایید";
                                        str[5, 2] = "";
                                        str[5, 3] = "ترسیم";
                                        str[5, 4] = "";
                                        str[5, 5] = "شماره شیت";
                                        str[6, 0] = CurrentDesignInfo.Approval;
                                        str[6, 1] = "تصویب";

                                        string CurrentDate = "";
                                        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
                                        CurrentDate = string.Format("{0}/{1:00}/{2:00}", p.GetYear(CurrentDesignInfo.DesignDate), p.GetMonth(CurrentDesignInfo.DesignDate), p.GetDayOfMonth(CurrentDesignInfo.DesignDate));
                                        str[6, 2] = CurrentDate;
                                        str[6, 3] = "تاریخ";
                                        str[6, 4] = CurrentDesignInfo.Scale.ToString();
                                        str[6, 5] = "مقیاس";

                                        str[7, 0] = CurrentDesignInfo.Adviser;
                                        str[7, 1] = "تهیه کننده";
                                        str[7, 2] = CurrentDesignInfo.Validate;
                                        str[7, 3] = "مدت اعتبار";
                                        str[7, 4] = "";
                                        str[7, 5] = "";


                                        for (int i = 0; i < 8; i++)
                                        {
                                            for (int j = 0; j < 6; j++)
                                            {
                                                tb.SetTextHeight(i, j, 2);
                                                tb.SetTextString(i, j, str[i, j]);
                                                tb.SetAlignment(i, j, CellAlignment.MiddleRight);
                                            }
                                        }
                                    }
                                    tr.Commit();
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public static bool DeleteFrame(ObjectId FrameOI)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (!Atend.Global.Acad.AcadRemove.DeleteEntityByObjectId(FrameOI))
                {
                    throw new System.Exception("GRA while delete Frame \n");
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("GRA ERROR Frame : {0} \n", ex.Message);
                return false;
            }
            return true;
        }
    }
}
