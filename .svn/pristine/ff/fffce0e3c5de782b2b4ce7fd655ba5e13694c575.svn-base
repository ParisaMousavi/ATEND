using System;
using System.Collections.Generic;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Base.Acad
{
    public class AT_COUNTER
    {

        static string TableName = "AT_COUNTER";

        static string PoleCounter = "PoleCounter";
        static string ConsolCounter = "ConsolCounter";
        static string ClampCounter = "ClampCounter";
        static string HeaderCounter = "HeaderCounter";
        static string KablshoCounter = "KablshoCounter";


        public static void ReadAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\n READ CALLED \n");
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock docLock = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite);
                        if (NOD.Contains(TableName))
                        {
                            //ed.WriteMessage("Table found\n");
                            DBDictionary myDict = (DBDictionary)tr.GetObject(NOD.GetAt(TableName), OpenMode.ForWrite);
                            if (myDict.Contains(PoleCounter))
                            {
                                Xrecord xrec = (Xrecord)tr.GetObject(myDict.GetAt(PoleCounter), OpenMode.ForWrite);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    ed.WriteMessage("*** PoleCounter:{0}\n", tv.Value);
                                    Atend.Control.Common.Counters.PoleCounter = Convert.ToInt32(tv.Value);
                                }
                            }
                            if (myDict.Contains(ConsolCounter))
                            {
                                Xrecord xrec = (Xrecord)tr.GetObject(myDict.GetAt(ConsolCounter), OpenMode.ForWrite);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    ed.WriteMessage("*** ConsolCounter:{0}\n", tv.Value);
                                    Atend.Control.Common.Counters.ConsolCounter = Convert.ToInt32(tv.Value);
                                }
                            }
                            if (myDict.Contains(ClampCounter))
                            {
                                Xrecord xrec = (Xrecord)tr.GetObject(myDict.GetAt(ClampCounter), OpenMode.ForWrite);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    ed.WriteMessage("*** ClampCounter:{0}\n", tv.Value);
                                    Atend.Control.Common.Counters.ClampCounter = Convert.ToInt32(tv.Value);
                                }
                            }
                            if (myDict.Contains(HeaderCounter))
                            {
                                Xrecord xrec = (Xrecord)tr.GetObject(myDict.GetAt(HeaderCounter), OpenMode.ForWrite);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    ed.WriteMessage("*** HeaderCounter:{0}\n", tv.Value);
                                    Atend.Control.Common.Counters.HeadercableCounter = Convert.ToInt32(tv.Value);
                                }
                            }

                            if (myDict.Contains(KablshoCounter))
                            {
                                Xrecord xrec = (Xrecord)tr.GetObject(myDict.GetAt(KablshoCounter), OpenMode.ForWrite);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    ed.WriteMessage("*** KablshoCounter:{0}\n", tv.Value);
                                    Atend.Control.Common.Counters.KablshoCounter = Convert.ToInt32(tv.Value);
                                }
                            }
                        }
                        else
                        {

                            //ed.WriteMessage("Table not found\n");
                            //table was not exist
                            NOD.UpgradeOpen();
                            DBDictionary MyDict = new DBDictionary();
                            NOD.SetAt(TableName, MyDict);
                            tr.AddNewlyCreatedDBObject(MyDict, true);
                            //ed.WriteMessage("found not dict\n");

                            //set its data here
                            Xrecord Xrec = new Xrecord();
                            Atend.Control.Common.Counters.PoleCounter = 0;
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, 0));
                            MyDict.SetAt(PoleCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            Atend.Control.Common.Counters.ConsolCounter = 0;
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, 0));
                            MyDict.SetAt(ConsolCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            Atend.Control.Common.Counters.ClampCounter = 0;
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, 0));
                            MyDict.SetAt(ClampCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            Atend.Control.Common.Counters.HeadercableCounter = 0;
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, 0));
                            MyDict.SetAt(HeaderCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                            Xrec = new Xrecord();
                            Atend.Control.Common.Counters.KablshoCounter = 0;
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, 0));
                            MyDict.SetAt(KablshoCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                        }
                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage("ERROR:{0}\n", ex.Message);
                    }
                    tr.Commit();
                }
            }


            //ed.WriteMessage("PoleCounter:{0} \n", Atend.Control.Common.Counters.PoleCounter);
            //ed.WriteMessage("ConsolCounter:{0} \n", Atend.Control.Common.Counters.ConsolCounter);
            //ed.WriteMessage("ClampCounter:{0} \n", Atend.Control.Common.Counters.ClampCounter);
            //ed.WriteMessage("HeadercableCounter:{0} \n", Atend.Control.Common.Counters.HeadercableCounter);
            //ed.WriteMessage("KablshoCounter:{0} \n", Atend.Control.Common.Counters.KablshoCounter);

        }

        public static void SaveAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\n SAVE CALLED \n");

            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock docLock = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite);
                        DBDictionary myDict = null;
                        //Atend.Control.Common.Counters.HeadercableCounter = 542;

                        if (NOD.Contains(TableName))
                        {
                            //ed.WriteMessage("Table found\n");
                            myDict = (DBDictionary)tr.GetObject(NOD.GetAt(TableName), OpenMode.ForWrite);

                            //set its data here
                            Xrecord Xrec = new Xrecord();
                            //ed.WriteMessage("PoleCounter:{0}\n", Atend.Control.Common.Counters.PoleCounter);
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.PoleCounter));
                            myDict.SetAt(PoleCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            //ed.WriteMessage("ConsolCounter:{0}\n", Atend.Control.Common.Counters.ConsolCounter);
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.ConsolCounter));
                            myDict.SetAt(ConsolCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            //ed.WriteMessage("ClampCounter:{0}\n", Atend.Control.Common.Counters.ClampCounter);
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.ClampCounter));
                            myDict.SetAt(ClampCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                            Xrec = new Xrecord();
                            //ed.WriteMessage("HeadercableCounter:{0}\n", Atend.Control.Common.Counters.HeadercableCounter);
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.HeadercableCounter));
                            myDict.SetAt(HeaderCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            //ed.WriteMessage("KablshoCounter:{0}\n", Atend.Control.Common.Counters.KablshoCounter);
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.KablshoCounter));
                            myDict.SetAt(KablshoCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                        }
                        else
                        {
                            //table was not exist
                            //ed.WriteMessage("Table not found\n");
                            NOD.UpgradeOpen();
                            myDict = new DBDictionary();
                            NOD.SetAt(TableName, myDict);
                            tr.AddNewlyCreatedDBObject(myDict, true);
                            //ed.WriteMessage("found not dict\n");

                            //set its data here
                            Xrecord Xrec = new Xrecord();
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.PoleCounter));
                            myDict.SetAt(PoleCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.ConsolCounter));
                            myDict.SetAt(ConsolCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);


                            Xrec = new Xrecord();
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.ClampCounter));
                            myDict.SetAt(ClampCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                            Xrec = new Xrecord();
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.HeadercableCounter));
                            myDict.SetAt(HeaderCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                            Xrec = new Xrecord();
                            Xrec.Data = new ResultBuffer(new TypedValue((int)DxfCode.Int32, Atend.Control.Common.Counters.KablshoCounter));
                            myDict.SetAt(KablshoCounter, Xrec);
                            tr.AddNewlyCreatedDBObject(Xrec, true);

                        }



                        DBDictionary n = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead);
                        if (n.Contains(TableName))
                        {
                            DBDictionary m = (DBDictionary)tr.GetObject(n.GetAt(TableName),OpenMode.ForRead);
                            if (m.Contains(HeaderCounter))
                            {
                                Xrecord xrec =(Xrecord) tr.GetObject(m.GetAt(PoleCounter),OpenMode.ForRead);
                                foreach (TypedValue tv in xrec.Data)
                                {
                                    //ed.WriteMessage("Saved value : {0} \n",tv.Value);
                                }
                            }
                        }


                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage("ERROR : {0} \n", ex.Message);
                    }
                    tr.Commit();
                }
            }

            //ReadAll();


        }

        


        //public static ObjectId AddRegAppTableRecord(string regAppName)
        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Editor ed = doc.Editor;
        //    Database db = doc.Database;
        //    ObjectId TableOi = ObjectId.Null;
        //    Transaction tr = doc.TransactionManager.StartTransaction();
        //    using (tr)
        //    {

        //        DBDictionary NOD = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite);
        //        DBDictionary MyDict;

        //        try
        //        {
        //            if (NOD.Contains("AT_COUNTER"))
        //            {
        //                MyDict = (DBDictionary)tr.GetObject(NOD.GetAt("AT_COUNTER"), OpenMode.ForWrite);
        //                ed.WriteMessage("found dict\n");
        //            }
        //            else
        //            {
        //                NOD.UpgradeOpen();
        //                MyDict = new DBDictionary();
        //                NOD.SetAt("AT_COUNTER", MyDict);
        //                tr.AddNewlyCreatedDBObject(MyDict, true);
        //                ed.WriteMessage("found not dict\n");

        //            }


        //            Xrecord Xrec = new Xrecord();
        //            Xrec.Data = new ResultBuffer(
        //                new TypedValue((int)DxfCode.Text, "HELLO 01...")
        //            );
        //            MyDict.SetAt("Key1", Xrec);
        //            tr.AddNewlyCreatedDBObject(Xrec, true);

        //        }

        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage("ERROR : {0} \n", ex.Message);
        //        }

        //        tr.Commit();
        //    }
        //    return TableOi;
        //}///

    }
}
