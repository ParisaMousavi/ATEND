using System;
using System.Collections.Generic;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;



namespace Atend.Base.Acad
{
    public class AT_INFO
    {

        private Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


        private string parentCode;
        public string ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }


        private string nodeCode;
        public string NodeCode
        {
            get { return nodeCode; }
            set { nodeCode = value; }
        }

        private int nodeType;
        public int NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }


        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }


        private ObjectId selectedObjectId;
        public ObjectId SelectedObjectId
        {
            get { return selectedObjectId; }
            set { selectedObjectId = value; }
        }

        private double angle;
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }


        public AT_INFO(ObjectId CurrentObjectId)
        {
            SelectedObjectId = CurrentObjectId;
            Angle = 0;
        }

        public AT_INFO()
        {
            Angle = 0;
        }

        public void Insert()
        {

            //ed.WriteMessage("Go to Insert \n");

            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            using (DocumentLock docLock = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {

                    DBObject dbObject = tr.GetObject(SelectedObjectId, OpenMode.ForRead);
                    if (dbObject.ExtensionDictionary == ObjectId.Null)
                    {
                        dbObject.UpgradeOpen();
                        dbObject.CreateExtensionDictionary();
                    }


                    //ed.WriteMessage(" AT ParentCode : {0} \n", ParentCode);
                    //ed.WriteMessage(" AT NodeCode : {0} \n", NodeCode);
                    //ed.WriteMessage(" AT NodeType : {0} \n", NodeType);
                    //ed.WriteMessage(" AT ProductCode : {0} \n", ProductCode);
                    //ed.WriteMessage(" AT Angle : {0} \n", Angle);

                    DBDictionary ExtensionDictionary = (DBDictionary)tr.GetObject(dbObject.ExtensionDictionary, OpenMode.ForWrite);
                    Xrecord xrec = new Xrecord();
                    xrec.Data = new ResultBuffer(
                        new TypedValue((int)DxfCode.Text, ParentCode),
                        new TypedValue((int)DxfCode.Text, NodeCode),
                        new TypedValue((int)DxfCode.Int32, NodeType),
                        new TypedValue((int)DxfCode.Int32, ProductCode),
                        new TypedValue((int)DxfCode.Real, Angle));

                    ExtensionDictionary.SetAt("AT_INFO", xrec);
                    tr.AddNewlyCreatedDBObject(xrec, true);
                    tr.Commit();
                }
            }
        }

        public static AT_INFO SelectBySelectedObjectId(ObjectId SelectedObjectId)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Enter SelectBySelectedObjectId\n");
            AT_INFO at_INFO = new AT_INFO();
            //ed.WriteMessage("a\n");
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            try
            {
                using (DocumentLock docLock = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        if (SelectedObjectId != ObjectId.Null)
                        {

                            //ed.WriteMessage("b\n");
                            DBObject ent = tr.GetObject(SelectedObjectId, OpenMode.ForRead);
                            //ed.WriteMessage("c\n");
                            if (ent.ExtensionDictionary != ObjectId.Null)
                            {
                                //ed.WriteMessage("d\n");
                                DBDictionary ExtDict = (DBDictionary)tr.GetObject(ent.ExtensionDictionary, OpenMode.ForRead);
                                //ed.WriteMessage("e\n");
                                if (ExtDict.Contains("AT_INFO"))
                                {
                                    //ed.WriteMessage("f\n");
                                    at_INFO.SelectedObjectId = SelectedObjectId;
                                    int counter = 1;
                                    //ed.WriteMessage("g\n");
                                    Xrecord xrec = (Xrecord)tr.GetObject(ExtDict.GetAt("AT_INFO"), OpenMode.ForRead);
                                    //ed.WriteMessage("h\n");
                                    foreach (TypedValue tv in xrec.Data)
                                    {
                                        //ed.WriteMessage("tv.Value.ToString()"+tv.Value.ToString()+"\n");
                                        switch (counter)
                                        {
                                            case 1:

                                                at_INFO.ParentCode = tv.Value.ToString();
                                                //ed.WriteMessage("j\n");
                                                break;
                                            case 2:
                                                at_INFO.NodeCode = tv.Value.ToString();
                                                //ed.WriteMessage("k\n");
                                                break;
                                            case 3:
                                                at_INFO.NodeType = Convert.ToInt32(tv.Value);
                                                //ed.WriteMessage("l\n");
                                                break;
                                            case 4:
                                                //EXTRA
                                                //at_INFO.ProductCode = Convert.ToInt32(tv.Value);
                                                at_INFO.ProductCode = Convert.ToInt32(tv.Value);
                                                //ed.WriteMessage("m\n");
                                                break;
                                            case 5:
                                                at_INFO.Angle = Convert.ToDouble(tv.Value);
                                                break;

                                        }
                                        counter++;
                                    }

                                }
                                else
                                {
                                    //ed.WriteMessage("#####else\n");
                                    at_INFO.ParentCode = "NONE";
                                    //#ed.WriteMessage("AT_INFO :{0} NOT EXIST \n" , SelectedObjectId);
                                }

                            }
                            else
                            {
                                at_INFO.ParentCode = "NONE";
                                //#ed.WriteMessage("AT_INFO : {0} NOT EXTENSION \n" , SelectedObjectId);
                            }
                        }
                        else
                        {
                            at_INFO.ParentCode = "NONE";
                            //#ed.WriteMessage("AT_INFO : {0} NOT Valid \n" , SelectedObjectId);
                        }
                    }// using
                }
            }
            catch (System.Exception ex)
            {
                //#ed.WriteMessage("Error SelectBySelectedObjectId_Info:" + ex.Message + "\n");
            }

            return at_INFO;

        }

        public static AT_INFO __SelectBySelectedObjectId(ObjectId SelectedObjectId, Autodesk.AutoCAD.DatabaseServices.Transaction CadTransaction)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("select info one transaction \n");
            AT_INFO at_INFO = new AT_INFO();
            //ed.WriteMessage("a\n");
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            try
            {
                using (DocumentLock docLock = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    using (Transaction tr = CadTransaction)
                    {
                        if (SelectedObjectId != ObjectId.Null)
                        {
                            //ed.WriteMessage("b\n");
                            DBObject ent = tr.GetObject(SelectedObjectId, OpenMode.ForRead);
                            //ed.WriteMessage("c\n");
                            if (ent.ExtensionDictionary != ObjectId.Null)
                            {
                                //ed.WriteMessage("d\n");
                                DBDictionary ExtDict = (DBDictionary)tr.GetObject(ent.ExtensionDictionary, OpenMode.ForRead);
                                //ed.WriteMessage("e\n");
                                if (ExtDict.Contains("AT_INFO"))
                                {
                                    //ed.WriteMessage("f\n");
                                    at_INFO.SelectedObjectId = SelectedObjectId;
                                    int counter = 1;
                                    //ed.WriteMessage("g\n");
                                    Xrecord xrec = (Xrecord)tr.GetObject(ExtDict.GetAt("AT_INFO"), OpenMode.ForRead);
                                    //ed.WriteMessage("h\n");
                                    foreach (TypedValue tv in xrec.Data)
                                    {
                                        //ed.WriteMessage("tv.Value.ToString()"+tv.Value.ToString()+"\n");
                                        switch (counter)
                                        {
                                            case 1:

                                                at_INFO.ParentCode = tv.Value.ToString();
                                                //ed.WriteMessage("j\n");
                                                break;
                                            case 2:
                                                at_INFO.NodeCode = tv.Value.ToString();
                                                //ed.WriteMessage("k\n");
                                                break;
                                            case 3:
                                                at_INFO.NodeType = Convert.ToInt32(tv.Value);
                                                //ed.WriteMessage("l\n");
                                                break;
                                            case 4:
                                                //EXTRA
                                                //at_INFO.ProductCode = Convert.ToInt32(tv.Value);
                                                at_INFO.ProductCode = Convert.ToInt32(tv.Value);
                                                //ed.WriteMessage("m\n");
                                                break;
                                            case 5:
                                                at_INFO.Angle = Convert.ToDouble(tv.Value);
                                                break;

                                        }
                                        counter++;
                                    }

                                }
                                else
                                {
                                    //ed.WriteMessage("#####else\n");
                                    at_INFO.ParentCode = "NONE";
                                    //#ed.WriteMessage("AT_INFO :{0} NOT EXIST \n" , SelectedObjectId);
                                }

                            }
                            else
                            {
                                at_INFO.ParentCode = "NONE";
                                //#ed.WriteMessage("AT_INFO : {0} NOT EXTENSION \n" , SelectedObjectId);
                            }
                        }
                        else
                        {
                            at_INFO.ParentCode = "NONE";
                            //#ed.WriteMessage("AT_INFO : {0} NOT Valid \n" , SelectedObjectId);
                        }
                    }// using
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error SelectBySelectedObjectId_Info:" + ex.Message + "\n");
            }

            return at_INFO;

        }

    }
}
