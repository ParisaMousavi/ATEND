using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

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
    public class AcDrawGIS
    {

        //open Atend_Gis.dwg
        //insert gis data to this file

        public void CreateGisFile()
        {
            int ProductType = 0;
            ObjectId CurrentOI = ObjectId.Null;
            switch ((Atend.Control.Enum.ProductType)ProductType)
            {
                case Atend.Control.Enum.ProductType.Pole:
                    ATG_Pole(CurrentOI);
                    break;
                case Atend.Control.Enum.ProductType.PoleTip:
                    break;
                case Atend.Control.Enum.ProductType.Conductor:
                    break;
                case Atend.Control.Enum.ProductType.GroundCabel:
                    break;
                case Atend.Control.Enum.ProductType.SelfKeeper:
                    break;
                case Atend.Control.Enum.ProductType.Jumper:
                    break;
                case Atend.Control.Enum.ProductType.Disconnector://سکسیونر
                    break;
                case Atend.Control.Enum.ProductType.Breaker://دژنگتور
                    break;
                case Atend.Control.Enum.ProductType.CatOut:
                    break;
                case Atend.Control.Enum.ProductType.AirPost:
                    break;
                case Atend.Control.Enum.ProductType.GroundPost:
                    break;
                case Atend.Control.Enum.ProductType.Rod:
                    break;
                case Atend.Control.Enum.ProductType.Khazan:
                    break;
                case Atend.Control.Enum.ProductType.HeaderCabel:
                    break;
                case Atend.Control.Enum.ProductType.Kalamp:
                    break;
                case Atend.Control.Enum.ProductType.KablSho:
                    break;
                case Atend.Control.Enum.ProductType.Consol:
                    break;
                case Atend.Control.Enum.ProductType.StreetBox:
                    break;
                case Atend.Control.Enum.ProductType.DB:
                    break;
                case Atend.Control.Enum.ProductType.Ground:
                    break;
                case Atend.Control.Enum.ProductType.Light:
                    break;
                case Atend.Control.Enum.ProductType.MeasuredJackPanel:
                    break;
                case Atend.Control.Enum.ProductType.Terminal:
                    break;

            }

        }

        private void ATG_Pole(ObjectId PoleOI)
        {
            //change pole to Gis
            Point3d PoleCenterPoint = Atend.Global.Acad.UAcad.CenterOfEntity(Atend.Global.Acad.UAcad.GetEntityByObjectID(PoleOI));
            if (PoleCenterPoint != Point3d.Origin)
            {
            }
        }

        public static void DrawNewBlock(Point3d InsertionPoint)
        {
            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DocumentCollection dm = Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;
            Database destDb = dm.MdiActiveDocument.Database;
            Database sourceDb = new Database(false, true);
            string GisFile = string.Format(@"{0}\GIS\{1}", Atend.Control.Common.DesignFullAddress, "Atend_Gis.dwg");
            string SourceGisFile = string.Format(@"{0}\GIS\{1}", Atend.Control.Common.fullPath, "Atend_Gis.dwg");

            if (!System.IO.Directory.Exists(Atend.Control.Common.DesignFullAddress + @"\GIS"))
            {
                System.IO.Directory.CreateDirectory(Atend.Control.Common.DesignFullAddress + @"\GIS");
            }
            System.IO.File.Copy(SourceGisFile, GisFile, true);
            using (DocumentLock dLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                try
                {
                    if (System.IO.File.Exists(GisFile))
                    {
                        sourceDb.ReadDwgFile(GisFile, System.IO.FileShare.ReadWrite, true, "");
                        //ObjectIdCollection blockIds = new ObjectIdCollection();
                        Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = sourceDb.TransactionManager;
                        using (Transaction myT = tm.StartTransaction())
                        {
                            string BlockName = "POINTBLOCK";
                            BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId, OpenMode.ForWrite, false);
                            BlockTableRecord btr = (BlockTableRecord)myT.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            ObjectId bID;
                            if (bt.Has(BlockName))
                            {
                                bID = bt[BlockName];
                                //        //ed.writeMessage("بلاک مورد نظر یافت شد" + " \n");
                                BlockReference br = new BlockReference(InsertionPoint, bID);
                                ed.WriteMessage("BLOCK REFERENCE CREATED \n");

                                btr.AppendEntity(br);
                                ed.WriteMessage("BLOCK APPENDED\n");

                                myT.AddNewlyCreatedDBObject(br, true);
                                ed.WriteMessage("TRANSACTION \n");
                                myT.Commit();
                            }
                        }
                    }
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    ed.WriteMessage("\nError during copy: " + ex.Message + "\n");
                }
            }
            sourceDb.Dispose();
        }

        private void ATG_PoleTip()
        {
        }

        private void ATG_Jumper()
        {
        }
    }
}
