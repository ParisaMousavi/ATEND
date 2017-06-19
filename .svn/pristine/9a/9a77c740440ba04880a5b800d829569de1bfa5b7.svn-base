using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;


namespace Atend.Base.Design
{
    public class 
        XXCoductorTransaction
    {

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public Atend.Control.Common.CounductorInformation conductorInformation;

        //string Comment;

        //public bool lastConductorWasDrawn = false;

        //public PromptEntityResult MyPromptEntityResult;

        //public long LastDrawnAutocadId;

        //public Guid LastDrawnAutocadGuid;

        //DNode rightNode;

        //DNode leftNode;

        //long GroupCodeForRight;

        //long GroupCodeForLeft;

        //DEquipmentGroup LeftNodeInformation;

        //DEquipmentGroup RightNodeInformation;

        //DGroup GroupInformation;

        //System.Data.DataTable LeftNodeEquipmentList;

        //System.Data.DataTable RightNodeEquipmentList;

        //DBranch SourceBranch;

        //DEquipmentGroup SourceInformation;

        //System.Data.DataTable RInerList;

        //System.Data.DataTable LInerList;

        //public Guid CreatedCounductorGuid;



        //public CoductorTransaction()
        //{
        //}

        //public void DrawVirtualInsulator(Point3d point1, Point3d point2, string LineTypeName)
        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Database db = doc.Database;
        //    Editor ed = doc.Editor;

        //    ed.WriteMessage(string.Format("Draw virtual insulator : {0} , {1}", point1, point2));

        //    using (Transaction tr = db.TransactionManager.StartTransaction())
        //    {

        //        ed.WriteMessage("Enter to Draw a Line \n");

        //        // Create a line with this linetype
        //        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
        //        using (DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
        //        {
        //            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
        //            AttributeReference attRef = new AttributeReference();
        //            //////////////////////////////////
        //            ed.WriteMessage(string.Format("point1 : {0} point2: {1}", point1.X.ToString(), point2.X.ToString()));
        //            Line ln = new Line(point1, point2);
        //            ObjectId ltId = ObjectId.Null;
        //            LinetypeTable ltt = (LinetypeTable)tr.GetObject(db.LinetypeTableId, OpenMode.ForRead);
        //            if (ltt.Has(LineTypeName))
        //            {
        //                ltId = ltt[LineTypeName];

        //                ln.SetDatabaseDefaults(db);
        //                ln.LinetypeId = ltId;
        //                ObjectId objectID = btr.AppendEntity(ln);

        //                tr.AddNewlyCreatedDBObject(ln, true);
        //                //AFTER REMOVED AUTOCAD CODE
        //                //Atend.Control.Common.SelectedAutocadObjectGuid = Convert.ToInt64(objectID.ToString().Substring(1, objectID.ToString().Length - 2));
        //                ed.WriteMessage("get cad id " + objectID.ToString().Substring(1, objectID.ToString().Length - 2) + " \n");

        //                tr.Commit();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n Line Type Which you seleced was not found. \n");
        //            }
        //        }
        //    }

        //}


        //public bool Insert()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("~~~~~~~~~~ Start Save Branch ~~~~~~~~~~~~");

        //    SqlTransaction Transaction;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        //try
        //        //{
        //            //// add Save conductor transaction here
        //            if (Atend.Base.Acad.AcadGlobal.dBranch.Insert(Transaction, Connection))
        //            {
        //            }
        //            ////    {

        //            ////        DDesignEquipmentPosition designEquipmentPosition = new DDesignEquipmentPosition();
        //            ////        designEquipmentPosition.BranchCode = Atend.Base.Acad.AcadGlobal.dBranch.Code;
        //            ////        designEquipmentPosition.X = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1.X;
        //            ////        designEquipmentPosition.Y = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1.Y;
        //            ////        designEquipmentPosition.Order = 1;
        //            ////        ed.WriteMessage(string.Format("i am going to designEquipmentPosition1 insertion \n "));
        //            ////        if (designEquipmentPosition.Insert(Transaction, Connection))
        //            ////        {
        //            ////            designEquipmentPosition.BranchCode = Atend.Base.Acad.AcadGlobal.dBranch.Code;
        //            ////            designEquipmentPosition.X = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2.X;
        //            ////            designEquipmentPosition.Y = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2.Y;
        //            ////            designEquipmentPosition.Order = 2;
        //            ////            ed.WriteMessage(string.Format("i am going to designEquipmentPosition2 insertion \n "));
        //            ////            if (designEquipmentPosition.Insert(Transaction, Connection))
        //            ////            {
        //            ////                // save done
        //            ////                //Transaction.Commit();
        //            ////                //Connection.Close();

        //            ////                //return true;
        //            ////            }
        //            ////            else
        //            ////            {
        //            ////                throw new Exception("while saving Equipment Position 2");
        //            ////            }
        //            ////        }
        //            ////        else
        //            ////        {
        //            ////            throw new Exception("while saving Equipment position 1");
        //            ////        }
        //            ////    }
        //            ////    else
        //            ////    {
        //            ////        throw new Exception("while saving Branch");
        //            ////    }
        //            ////}//end of second transaction
        //            ////catch (System.Exception ex1)
        //            ////{
        //            ////    ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex1.Message));
        //            ////    Transaction.Rollback();
        //            ////    Connection.Close();
        //            ////    return false;
        //            ////}
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex.Message));
        //            Connection.Close();
        //            return false;
        //        }

        //        Transaction.Commit();
        //        Connection.Close();
        //        ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");

        //        return true;
        //    }
        

   
        //public bool InsertGroundCable()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("~~~~~~~~~~ Start Save SelfKeeper ~~~~~~~~~~~~");

        //    SqlTransaction Transaction;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {
        //            Atend.Base.Acad.AcadGlobal.dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //            if (Atend.Base.Acad.AcadGlobal.dBranch.Insert(Transaction, Connection))
        //            {
        //                //Well Done
        //            }
        //            else
        //            {
        //                throw new Exception("while saving Cabel");
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error CabelTransaction.Insert : {0} \n", ex1.Message));
        //            Transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error CabelTransaction.Insert : {0} \n", ex.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    ed.WriteMessage("~~~~~~~~~~ End Save SelfKeeper ~~~~~~~~~~~~");
        //    return true;
        //}

        //public bool Update()
        //{
        //    //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlCommand command = new SqlCommand();


        //    return true;
        //}

        //public static bool Delete()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {

        //            //ed.writeMessage(string.Format("Deleted Branch AutocadId is : {0} \n", Atend.Control.Common.SelectedAutocadObjectGuid));
        //            Atend.Base.Design.DBranch DeletedBranch = Atend.Base.Design.DBranch.SelectByDesignCodeAndCode(Atend.Control.Common.SelectedDesignCode, Atend.Control.Common.SelectedAutocadObjectGuid);
        //            //ed.writeMessage(string.Format("Deleted Branch code is : {0} \n", DeletedBranch.Code));

        //            if (Atend.Base.Design.DBranch.Delete(DeletedBranch.Code, Transaction, Connection))
        //            {
        //                //if (Atend.Base.Design.DDesignEquipmentPosition.Delete(DeletedBranch.Code, Transaction, Connection))
        //                //{
        //                //    //ed.writeMessage("All Branch Information Deleted Done \n");
        //                //}
        //                //else
        //                //{
        //                //    throw new Exception("while deleting Branch position \n");
        //                //}
        //            }
        //            else
        //            {
        //                throw new System.Exception("while deleting branch information from dbranch table");
        //            }

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            Transaction.Rollback();
        //            Connection.Close();
        //            ed.WriteMessage(string.Format("Error ConductorTransaction.Delete : {0} \n", ex1.Message));
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error ConductorTransaction.Delete : {0} \n", ex.Message));
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}


        //~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~

        //public bool AccessInsert()
        //{

        //    ed.WriteMessage("~~~~~~~~~~ Start Save Branch ~~~~~~~~~~~~");

        //    OleDbTransaction Transaction;
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.cnString);

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {
        //            //// add Save conductor transaction here
        //            if (Atend.Base.Acad.AcadGlobal.dBranch.AccessInsert(Transaction, Connection))
        //            {

        //                //DDesignEquipmentPosition designEquipmentPosition = new DDesignEquipmentPosition();
        //                //designEquipmentPosition.BranchCode = Atend.Base.Acad.AcadGlobal.dBranch.Code;
        //                //designEquipmentPosition.X = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1.X;
        //                //designEquipmentPosition.Y = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point1.Y;
        //                //designEquipmentPosition.Order = 1;
        //                //ed.WriteMessage(string.Format("i am going to designEquipmentPosition1 insertion \n "));
        //                //if (designEquipmentPosition.Insert(Transaction, Connection))
        //                //{
        //                ////////////designEquipmentPosition.BranchCode = Atend.Base.Acad.AcadGlobal.dBranch.Code;
        //                ////////////designEquipmentPosition.X = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2.X;
        //                ////////////designEquipmentPosition.Y = Atend.Base.Acad.AcadGlobal.dConductorInformation.Point2.Y;
        //                ////////////designEquipmentPosition.Order = 2;
        //                ////////////ed.WriteMessage(string.Format("i am going to designEquipmentPosition2 insertion \n "));
        //                ////////////if (designEquipmentPosition.Insert(Transaction, Connection))
        //                ////////////{
        //                ////////////    // save done
        //                ////////////    //Transaction.Commit();
        //                ////////////    //Connection.Close();

        //                ////////////    //return true;
        //                ////////////}
        //                ////////////else
        //                ////////////{
        //                ////////////    throw new Exception("while saving Equipment Position 2");
        //                ////////////}
        //                //}
        //                //else
        //                //{
        //                //    throw new Exception("while saving Equipment position 1");
        //                //}
        //            }
        //            else
        //            {
        //                throw new Exception("while saving Branch");
        //            }
        //        }//end of second transaction
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex1.Message));
        //            Transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    ed.WriteMessage("~~~~~~~~~~ End Save Branch ~~~~~~~~~~~~");

        //    return true;
        //}

        //public bool AccessInsertSelfKeeper()
        //{

        //    ed.WriteMessage("~~~~~~~~~~ Start Save SelfKeeper ~~~~~~~~~~~~");

        //    OleDbTransaction  Transaction;
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {
        //            Atend.Base.Acad.AcadGlobal.dBranch.ProductType = (int)Atend.Control.Enum.ProductType.SelfKeeper;
        //            if (Atend.Base.Acad.AcadGlobal.dBranch.AccessInsert(Transaction, Connection))
        //            {
        //                //Well Done
        //            }
        //            else
        //            {
        //                throw new Exception("while saving Branch");
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex1.Message));
        //            Transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error ConductorTransaction.Insert : {0} \n", ex.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    ed.WriteMessage("~~~~~~~~~~ End Save SelfKeeper ~~~~~~~~~~~~");
        //    return true;
        //}

        //public bool AccessInsertGroundCable()
        //{

        //    ed.WriteMessage("~~~~~~~~~~ Start Save SelfKeeper ~~~~~~~~~~~~");

        //    OleDbTransaction Transaction;
        //    OleDbConnection  Connection = new OleDbConnection (Atend.Control.ConnectionString.AccessCnString);

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {
        //            Atend.Base.Acad.AcadGlobal.dBranch.ProductType = (int)Atend.Control.Enum.ProductType.GroundCabel;
        //            if (Atend.Base.Acad.AcadGlobal.dBranch.AccessInsert(Transaction, Connection))
        //            {
        //                //Well Done
        //            }
        //            else
        //            {
        //                throw new Exception("while saving Cabel");
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error CabelTransaction.Insert : {0} \n", ex1.Message));
        //            Transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error CabelTransaction.Insert : {0} \n", ex.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    ed.WriteMessage("~~~~~~~~~~ End Save SelfKeeper ~~~~~~~~~~~~");
        //    return true;
        //}

        //public bool AccessUpdate()
        //{
        //    //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlCommand command = new SqlCommand();


        //    return true;
        //}

        //public static bool AccessDelete()
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbTransaction Transaction;

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {

        //            //ed.writeMessage(string.Format("Deleted Branch AutocadId is : {0} \n", Atend.Control.Common.SelectedAutocadObjectGuid));
        //            Atend.Base.Design.DBranch DeletedBranch = Atend.Base.Design.DBranch.AccessSelectByDesignCodeAndCode(Atend.Control.Common.SelectedDesignCode, Atend.Control.Common.SelectedAutocadObjectGuid);
        //            //ed.writeMessage(string.Format("Deleted Branch code is : {0} \n", DeletedBranch.Code));

        //            if (Atend.Base.Design.DBranch.AccessDelete(DeletedBranch.Code, Transaction, Connection))
        //            {
        //                //if (Atend.Base.Design.DDesignEquipmentPosition.Delete(DeletedBranch.Code, Transaction, Connection))
        //                //{
        //                //    //ed.writeMessage("All Branch Information Deleted Done \n");
        //                //}
        //                //else
        //                //{
        //                //    throw new Exception("while deleting Branch position \n");
        //                //}
        //            }
        //            else
        //            {
        //                throw new System.Exception("while deleting branch information from dbranch table");
        //            }

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            Transaction.Rollback();
        //            Connection.Close();
        //            ed.WriteMessage(string.Format("Error ConductorTransaction.Delete : {0} \n", ex1.Message));
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error ConductorTransaction.Delete : {0} \n", ex.Message));
        //        return false;
        //    }

        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    }
}
