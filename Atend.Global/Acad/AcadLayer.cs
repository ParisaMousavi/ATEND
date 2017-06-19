using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Autodesk.AutoCAD.EditorInput;
//using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using Autodesk.AutoCAD.Colors;

namespace Atend.Global.Acad
{
    public class AcadLayer
    {

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //---- last later version  -----
        //public bool ApplicationLayering()
        //{
        //    Database db = HostApplicationServices.WorkingDatabase;

        //    using (Transaction tr = db.TransactionManager.StartTransaction())
        //    {

        //        LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);

        //        //~~~~~~~~~~~~~~~~~ MID_AIR ~~~~~~~~~~~~~~~~~~~~~~~~~~

        //        if (lt.Has("MID_AIR"))
        //        {
        //            // layer exist
        //        }
        //        else
        //        {

        //            LayerTableRecord ltr = new LayerTableRecord();

        //            ltr.Name = "MID_AIR";

        //            ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 1);

        //            lt.Add(ltr);

        //            tr.AddNewlyCreatedDBObject(ltr, true);
        //        }

        //        //~~~~~~~~~~~~~~~~~ MID_GROUND ~~~~~~~~~~~~~~~~~~~~~~~~~~

        //        if (lt.Has("MID_GROUND"))
        //        {
        //            // layer exist
        //        }
        //        else
        //        {

        //            LayerTableRecord ltr = new LayerTableRecord();

        //            ltr.Name = "MID_GROUND";

        //            ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 6);

        //            lt.Add(ltr);

        //            tr.AddNewlyCreatedDBObject(ltr, true);


        //        }

        //        //~~~~~~~~~~~~~~~~~ WEEK_AIR ~~~~~~~~~~~~~~~~~~~~~~~~~~

        //        if (lt.Has("WEEK_AIR"))
        //        {

        //           //layer exist     

        //        }
        //        else
        //        {

        //            LayerTableRecord ltr = new LayerTableRecord();

        //            ltr.Name = "WEEK_AIR";

        //            ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 4);

        //            lt.Add(ltr);

        //            tr.AddNewlyCreatedDBObject(ltr, true);
        //        }

        //        //~~~~~~~~~~~~~~~~~ WEEK_GROUND ~~~~~~~~~~~~~~~~~~~~~~~~~~

        //        if (lt.Has("WEEK_GROUND"))
        //        {

        //            // layer  exist

        //        }
        //        else
        //        {

        //            LayerTableRecord ltr = new LayerTableRecord();

        //            ltr.Name = "WEEK_GROUND";

        //            ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 5);

        //            lt.Add(ltr);

        //            tr.AddNewlyCreatedDBObject(ltr, true);
        //        }

        //        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //        tr.Commit();

        //    }

        //    return true;            

        //}
        //---- end of last layer version ----

        public void Insert()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //System.Data.DataTable LayerTable1 = Atend.Base.Design.DLayer.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);

            Database db = Application.DocumentManager.MdiActiveDocument.Database;


            using (DocumentLock dLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);


                    if (lt.Has(Name))
                    {

                        // layer  exist

                    }
                    else
                    {
                        LayerTableRecord ltr = new LayerTableRecord();

                        ltr.Name = Convert.ToString(Name);

                        //Random rand = new Random();

                        //int aa = rand.Next(1,150);

                        //ed.WriteMessage("aa {0} \n", aa.ToString());

                        ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, Convert.ToInt16(Enum.Parse(typeof(Atend.Control.Enum.AutoCadLayerName), Name)));

                        //ed.WriteMessage("Layers Setting Done. \n");

                        lt.Add(ltr);

                        tr.AddNewlyCreatedDBObject(ltr, true);

                        tr.Commit();
                    }

                }

            }

        }

        public void Delete(string LayerName)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //System.Data.DataTable LayerTable1 = Atend.Base.Design.DLayer.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);

            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;

            using (DocumentLock dLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);


                    if (lt.Has(LayerName))
                    {

                        // layer  exist

                        DBObject dbobject = lt[LayerName].GetObject(OpenMode.ForWrite, true);

                        dbobject.Erase();

                        //tr.AddNewlyCreatedDBObject(ltr, true);

                    }


                    tr.Commit();

                }

            }
            //ed.WriteMessage("Layers Setting Done. \n");

        }

        public void Update(string LastLayerName, string NewLayerName)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //System.Data.DataTable LayerTable1 = Atend.Base.Design.DLayer.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);

            //Database db = HostApplicationServices.WorkingDatabase;
            Database db = Application.DocumentManager.MdiActiveDocument.Database;

            using (DocumentLock dLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);


                    if (lt.Has(LastLayerName))
                    {

                        // layer  exist

                        LayerTableRecord dbobject = (LayerTableRecord)lt[LastLayerName].GetObject(OpenMode.ForWrite, true);

                        dbobject.Name = NewLayerName;

                        //tr.AddNewlyCreatedDBObject(ltr, true);

                    }


                    tr.Commit();

                }

            }
            //ed.WriteMessage("Layers Setting Done. \n");

        }

        public static ObjectId GetLayerById(string LayerName)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //System.Data.DataTable LayerTable1 = Atend.Base.Design.DLayer.SelectByDesignCode(Atend.Control.Common.SelectedDesignCode);

            Database db = Application.DocumentManager.MdiActiveDocument.Database;


            using (DocumentLock dLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);


                    if (lt.Has(LayerName))
                    {

                        return lt[LayerName];

                    }
                    else
                    {

                        ObjectId newLayer;

                        LayerTableRecord ltr = new LayerTableRecord();

                        ltr.Name = Convert.ToString(LayerName);

                        ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, Convert.ToInt16(Enum.Parse(typeof(Atend.Control.Enum.AutoCadLayerName), LayerName)));

                        //ed.WriteMessage("Layers Setting Done. \n");

                        newLayer = lt.Add(ltr);

                        tr.AddNewlyCreatedDBObject(ltr, true);

                        tr.Commit();

                        return newLayer;
                    }

                }

            }

        }


    }
}