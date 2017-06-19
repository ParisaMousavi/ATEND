using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class NodeTransaction
    {


        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //string Comment;

        //static ArrayList arraylist = new ArrayList();

        //private static bool AllSubEquipmentsDoneSuccessed = true;

        //public NodeTransaction()
        //{
        //    //MyDPackages = new ArrayList();
        //}


        //public static bool InsertCatOut(Guid Parent, Guid CatoutXCode)
        //{

        //    Editor edl = Application.DocumentManager.MdiActiveDocument.Editor;

        //    Atend.Base.Equipment.ECatOut CatOut = Atend.Base.Equipment.ECatOut.SelectByXCode(CatoutXCode);
        //    Atend.Base.Design.DPackage CPack = new Atend.Base.Design.DPackage();

        //    CPack.ParentCode = Parent;
        //    CPack.ProductCode = CatoutXCode;
        //    CPack.Type = (int)Atend.Control.Enum.ProductType.CatOut;
        //    CPack.Count = 1;

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;

        //    edl.WriteMessage("\n111\n");

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();



        //        try
        //        {
        //            edl.WriteMessage("\n112\n");

        //            if (!CPack.Insert(Transaction, Connection))
        //            {
        //                edl.WriteMessage("Error NodeTransaction.InsertCatOut : while saving DPackage For DCatOut \n");
        //                throw new Exception("while saving Catout in DPackage ");
        //            }

        //            edl.WriteMessage("\n113\n");

        //            Atend.Base.Equipment.EContainerPackage Container = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(CatoutXCode, (int)Atend.Control.Enum.ProductType.CatOut);
        //            DataTable ProductTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(Container.Code);

        //            edl.WriteMessage("\n114\n");
        //            foreach (DataRow ProductRow in ProductTbl.Rows)
        //            {
        //                edl.WriteMessage("\nProductCode = " + ProductRow["ProductCode"].ToString() + "   TYPE = " + ProductRow["TableType"].ToString() + "\n");
        //                Atend.Base.Design.DPackage Package = new DPackage();
        //                Package.ParentCode = CPack.Code;
        //                Package.Count = Convert.ToInt32(ProductRow["Count"].ToString());
        //                Package.ProductCode = new Guid(ProductRow["ProductCode"].ToString());
        //                Package.Type = Convert.ToInt32(ProductRow["TableType"].ToString());

        //                edl.WriteMessage("\n115\n");

        //                if (Package.Insert(Transaction, Connection))
        //                {
        //                    edl.WriteMessage("\n116\n");

        //                    if (!SubProduct(Convert.ToInt32(ProductRow["ProductCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), Package.Code, Transaction, Connection))
        //                    {
        //                        edl.WriteMessage("Error NodeTransaction.InsertCatOut : while saving Sub Products \n");
        //                        throw new Exception("while saving Sub Products in DPackage ");
        //                    }
        //                }
        //                else
        //                {
        //                    edl.WriteMessage("Error NodeTransaction.InsertCatOut : while saving DPackage For Catout Sub Product \n");
        //                    throw new Exception("while saving Default in DPackage ");
        //                }
        //            }

        //            edl.WriteMessage("\n117\n");

        //            DataTable OpTable = Atend.Base.Equipment.EOperation.SelectByXCodeType(CatoutXCode, (int)Atend.Control.Enum.ProductType.CatOut);


        //            foreach (DataRow OpRow in OpTable.Rows)
        //            {
        //                edl.WriteMessage("\n118\n");

        //                Atend.Base.Design.DPackage OpPack = new DPackage();
        //                OpPack.ParentCode = CPack.Code;
        //                OpPack.ProductCode = new Guid(OpRow["ProductId"].ToString());
        //                OpPack.Type = (int)Atend.Control.Enum.ProductType.Operation;
        //                OpPack.Count = 1;// Convert.ToInt32(OpRow["Count"].ToString());

        //                if (!OpPack.Insert(Transaction, Connection))
        //                {
        //                    edl.WriteMessage("Error NodeTransaction.InsertCatOut : while saving DPackage For Operation\n");
        //                    throw new Exception("while saving Operation in DPackage ");
        //                }
        //            }


        //        }
        //        catch (System.Exception ex2)
        //        {
        //            edl.WriteMessage(string.Format("Error NodeTransaction.InsertAirPost  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }

        //    }
        //    catch (System.Exception ex1)
        //    {
        //        edl.WriteMessage(string.Format("Error NodeTransaction.InsertAirPost  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    Connection.Close();
        //    edl.WriteMessage("\nFINISH\n");

        //    return true;
        //}



        //public static bool InsertAirPost(int PostCode)
        //{
        //    Editor edl = Application.DocumentManager.MdiActiveDocument.Editor;

        //    Atend.Base.Equipment.EAirPost APost = Atend.Base.Equipment.EAirPost.SelectByCode(PostCode);
        //    Atend.Base.Design.DPost DPost = new DPost();

        //    DPost.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //    DPost.Number = "N";
        //    DPost.ProductCode = APost.XCode;

        //    edl.WriteMessage("\n111\n");


        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;


        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();


        //        try
        //        {
        //            if (DPost.Insert(Transaction, Connection))
        //            {
        //                edl.WriteMessage("DPost.Code= " + DPost.Code + "\n");
        //                DPackage PostPack = new DPackage();
        //                PostPack.NodeCode = DPost.Code;
        //                PostPack.Count = 1;
        //                PostPack.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
        //                PostPack.ProductCode = DPost.ProductCode;


        //                edl.WriteMessage("\n112\n");
        //                Atend.Base.Acad.AcadGlobal.PostEquips Row = new Atend.Base.Acad.AcadGlobal.PostEquips();

        //                //DPackage PostPack = DPackage.SelectByNodeCode(DPost.Code);

        //                if (PostPack.Insert(Transaction, Connection))
        //                {
        //                    edl.WriteMessage("\n113\n");
        //                    Row.CodeGuid = PostPack.Code;
        //                    Row.ProductType = (int)Atend.Control.Enum.ProductType.AirPost;
        //                    Row.ProductCode = PostPack.ProductCode;

        //                    Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(Row);
        //                }
        //                else
        //                {
        //                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For DPost \n");
        //                    throw new Exception("while saving DPost in DPackage ");
        //                }

        //                Atend.Base.Equipment.EContainerPackage CPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(APost.Code, (int)Atend.Control.Enum.ProductType.AirPost);
        //                DataTable ProductTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(CPackage.Code);

        //                edl.WriteMessage("\n114\n");
        //                foreach (DataRow ProductRow in ProductTbl.Rows)
        //                {
        //                    edl.WriteMessage("\n115\n");
        //                    switch ((Atend.Control.Enum.ProductType)Convert.ToInt16(ProductRow["TableType"].ToString()))
        //                    {

        //                        case Atend.Control.Enum.ProductType.Transformer:

        //                            //edl.WriteMessage("\n116\n");
        //                            Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));
        //                            for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                            {
        //                                Atend.Base.Design.DPackage Pack = new DPackage();
        //                                Pack.ParentCode = PostPack.Code;
        //                                Pack.Count = 1;// Convert.ToInt32(ProductRow["Count"].ToString());
        //                                Pack.ProductCode = Trans.XCode;
        //                                Pack.Type = (int)Atend.Control.Enum.ProductType.Transformer;

        //                                if (Pack.Insert(Transaction, Connection))
        //                                {
        //                                    //edl.WriteMessage("\n117\n");
        //                                    Atend.Base.Acad.AcadGlobal.PostEquips RTrans = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                    RTrans.CodeGuid = Pack.Code;
        //                                    RTrans.ParentCode = Pack.ParentCode;
        //                                    RTrans.ProductCode = Pack.ProductCode;
        //                                    RTrans.ProductType = (int)Atend.Control.Enum.ProductType.Transformer;

        //                                    Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RTrans);

        //                                    //Atend.Base.Equipment.EContainerPackage ContP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Trans.Code, (int)Atend.Control.Enum.ProductType.Transformer);

        //                                    //if (!SubProduct(ContP.Code, (int)Atend.Control.Enum.ProductType.Transformer, Pack.ParentCode, Transaction, Connection))
        //                                    //{
        //                                    //    edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Sub Products \n");
        //                                    //    throw new Exception("while saving Sub Products in DPackage ");
        //                                    //}

        //                                    if (!SubProduct(Convert.ToInt32(ProductRow["ContainerPackageCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), Pack.ParentCode, Transaction, Connection))
        //                                    {
        //                                        edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving Sub Products \n");
        //                                        throw new Exception("while saving Sub Products in DPackage ");
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Transformer \n");
        //                                    throw new Exception("while saving Transformer in DPackage ");
        //                                }
        //                            }
        //                            break;

        //                        case Atend.Control.Enum.ProductType.MiddleJackPanel:

        //                            //edl.WriteMessage("\n118\n");
        //                            Atend.Base.Equipment.EJAckPanel JackPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));

        //                            for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                            {
        //                                Atend.Base.Design.DPackage JackPack = new DPackage();
        //                                JackPack.ParentCode = PostPack.Code;
        //                                JackPack.Count = 1;//Convert.ToInt32(ProductRow["Count"].ToString());
        //                                JackPack.Type = (int)Atend.Control.Enum.ProductType.MiddleJackPanel;
        //                                JackPack.ProductCode = JackPanel.XCode;

        //                                if (JackPack.Insert(Transaction, Connection))
        //                                {
        //                                    //edl.WriteMessage("\n119\n");
        //                                    Atend.Base.Acad.AcadGlobal.PostEquips RJack = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                    RJack.CodeGuid = JackPack.Code;
        //                                    RJack.ParentCode = JackPack.ParentCode;
        //                                    RJack.ProductCode = JackPack.ProductCode;
        //                                    RJack.ProductType = (int)Atend.Control.Enum.ProductType.MiddleJackPanel;

        //                                    Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RJack);
        //                                }
        //                                else
        //                                {
        //                                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For MiddleJackPanel \n");
        //                                    throw new Exception("while saving MiddleJackPanel in DPackage ");
        //                                }

        //                                DataTable JackTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelCode(JackPanel.Code);

        //                                //edl.WriteMessage("\n120\n");
        //                                foreach (DataRow JackRow in JackTbl.Rows)
        //                                {

        //                                    DPackage P = new DPackage();
        //                                    P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                                    P.ParentCode = JackPack.Code;
        //                                    P.Type = (int)Atend.Control.Enum.ProductType.Cell;//Convert.ToInt32(Dr["Type"].ToString());
        //                                    //edl.WriteMessage("~~~~Cell productCode:{0}~~~~\n", JackRow["ProductCode"].ToString());
        //                                    P.ProductCode = new Guid(JackRow["ProductCode"].ToString());

        //                                    if (P.Insert(Transaction, Connection))
        //                                    {
        //                                        //edl.WriteMessage("\n125\n");
        //                                        Atend.Base.Acad.AcadGlobal.PostEquips RCell = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                        RCell.CodeGuid = P.Code;
        //                                        RCell.ParentCode = P.ParentCode;
        //                                        RCell.ProductCode = P.ProductCode;
        //                                        RCell.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                                        Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RCell);
        //                                    }
        //                                    else
        //                                    {
        //                                        edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                                        throw new Exception("while saving Cell in DPackage");
        //                                    }

        //                                    //edl.WriteMessage("\n121\n");
        //                                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JackRow["ProductCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);
        //                                    DataTable Tbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(CP.Code);

        //                                    edl.WriteMessage("\n122\n");
        //                                    foreach (DataRow Dr in Tbl.Rows)
        //                                    {
        //                                        //edl.WriteMessage("\n123\n");
        //                                        DPackage P1 = new DPackage();
        //                                        P1.Count = Convert.ToInt32(Dr["Count"].ToString());
        //                                        P1.ParentCode = P.Code; //JackPack.Code;
        //                                        P1.Type = Convert.ToInt32(Dr["TableType"].ToString());
        //                                        P1.ProductCode = new Guid(Dr["ProductCode"].ToString());


        //                                        //edl.WriteMessage("\n124\n");
        //                                        if (P1.Insert(Transaction, Connection))
        //                                        {
        //                                            //    //edl.WriteMessage("\n125\n");
        //                                            //    Atend.Base.Acad.AcadGlobal.PostEquips RCell = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                            //    RCell.CodeGuid = P.Code;
        //                                            //    RCell.ParentCode = P.ParentCode;
        //                                            //    RCell.ProductCode = P.ProductCode;
        //                                            //    RCell.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                                            //Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RCell);

        //                                            if (!SubProduct(Convert.ToInt32(Dr["ProductCode"].ToString()), Convert.ToInt32(Dr["TableType"].ToString()), P1.Code, Transaction, Connection))
        //                                            {
        //                                                edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving Sub Products \n");
        //                                                throw new Exception("while saving Sub Products in DPackage ");
        //                                            }

        //                                        }
        //                                        else
        //                                        {
        //                                            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                                            throw new Exception("while saving Cell in DPackage");
        //                                        }

        //                                        //edl.WriteMessage("\n126\n");
        //                                        DCellStatus CellSt = new DCellStatus();
        //                                        CellSt.CellCode = P.Code;
        //                                        CellSt.JackPanelCode = JackPack.Code;
        //                                        CellSt.IsClosed = false;
        //                                        if (CellSt.Insert(Transaction, Connection))
        //                                        { }
        //                                        else
        //                                        {
        //                                            edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Cell Status For \n");
        //                                            throw new Exception("while saving Cell Status");
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            break;

        //                        case Atend.Control.Enum.ProductType.WeekJackPanel:

        //                            //edl.WriteMessage("\n127\n");
        //                            Atend.Base.Equipment.EJackPanelWeek JackPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));

        //                            for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                            {
        //                                Atend.Base.Design.DPackage JackPackW = new DPackage();
        //                                JackPackW.ParentCode = PostPack.Code;
        //                                JackPackW.Count = 1;//Convert.ToInt32(ProductRow["Count"].ToString());
        //                                JackPackW.Type = (int)Atend.Control.Enum.ProductType.WeekJackPanel;
        //                                JackPackW.ProductCode = JackPanelW.XCode;

        //                                //edl.WriteMessage("\n128\n");
        //                                if (JackPackW.Insert(Transaction, Connection))
        //                                {
        //                                    //edl.WriteMessage("\n129\n");
        //                                    Atend.Base.Acad.AcadGlobal.PostEquips RJPW = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                    RJPW.CodeGuid = JackPackW.Code;
        //                                    RJPW.ParentCode = JackPackW.ParentCode;
        //                                    RJPW.ProductCode = JackPackW.ProductCode;
        //                                    RJPW.ProductType = (int)Atend.Control.Enum.ProductType.WeekJackPanel;

        //                                    Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RJPW);
        //                                }
        //                                else
        //                                {
        //                                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For WeekJackPanel \n");
        //                                    throw new Exception("while saving WeekJack Panel in DPackage ");
        //                                }


        //                                DataTable JackTblW = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(JackPanelW.Code);
        //                                //edl.WriteMessage("\n130\n");

        //                                foreach (DataRow JackRow in JackTblW.Rows)
        //                                {

        //                                    DPackage P = new DPackage();
        //                                    P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                                    P.ParentCode = JackPackW.Code;
        //                                    P.Type = (int)Atend.Control.Enum.ProductType.Cell;
        //                                    P.ProductCode = new Guid(JackRow["Code"].ToString());

        //                                    //edl.WriteMessage("\n134\n");

        //                                    if (P.Insert(Transaction, Connection))
        //                                    { }
        //                                    else
        //                                    {
        //                                        edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                                        throw new Exception("while saving Cell in DPackage ");
        //                                    }
        //                                    // edl.WriteMessage("\n131\n");

        //                                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JackRow["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);
        //                                    DataTable Tbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(CP.Code);

        //                                    //edl.WriteMessage("\n132\n");

        //                                    foreach (DataRow Dr in Tbl.Rows)
        //                                    {
        //                                        //edl.WriteMessage("\n133\n");

        //                                        DPackage P1 = new DPackage();
        //                                        P1.Count = Convert.ToInt32(Dr["Count"].ToString());
        //                                        P1.ParentCode = P.Code;
        //                                        P1.Type = Convert.ToInt32(Dr["TableType"].ToString());
        //                                        P1.ProductCode = new Guid(Dr["ProductCode"].ToString());

        //                                        ////edl.WriteMessage("\n134\n");

        //                                        if (P1.Insert(Transaction, Connection))
        //                                        {
        //                                            //edl.WriteMessage("\n135\n");
        //                                            //Atend.Base.Acad.AcadGlobal.PostEquips RC = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                            //RC.CodeGuid = P.Code;
        //                                            //RC.ParentCode = P.ParentCode;
        //                                            //RC.ProductCode = P.ProductCode;
        //                                            //RC.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                                            //Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RC);
        //                                            if (!SubProduct(Convert.ToInt32(Dr["ProductCode"].ToString()), Convert.ToInt32(Dr["TableType"].ToString()), P1.Code, Transaction, Connection))
        //                                            {
        //                                                edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving Sub Products \n");
        //                                                throw new Exception("while saving Sub Products in DPackage ");
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                                            throw new Exception("while saving Cell in DPackage ");
        //                                        }

        //                                    }
        //                                }
        //                            }
        //                            break;

        //                        default:

        //                            //edl.WriteMessage("\n136\n");

        //                            Atend.Base.Design.DPackage Package = new DPackage();
        //                            Package.ParentCode = PostPack.Code;
        //                            Package.Count = Convert.ToInt32(ProductRow["Count"].ToString());
        //                            Package.ProductCode = new Guid(ProductRow["ProductCode"].ToString());
        //                            Package.Type = Convert.ToInt32(ProductRow["TableType"].ToString());

        //                            //edl.WriteMessage("\n137\n");

        //                            if (Package.Insert(Transaction, Connection))
        //                            {
        //                                //edl.WriteMessage("\n138\n");

        //                                //Atend.Base.Acad.AcadGlobal.PostEquips RD = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                                //RD.CodeGuid = Package.Code;
        //                                //RD.ParentCode = Package.ParentCode;
        //                                //RD.ProductCode = Package.ProductCode;
        //                                //RD.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                                //Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RD);

        //                                if (!SubProduct(Convert.ToInt32(ProductRow["ProductCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), Package.Code, Transaction, Connection))
        //                                {
        //                                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving Sub Products \n");
        //                                    throw new Exception("while saving Sub Products in DPackage ");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Default \n");
        //                                throw new Exception("while saving Default in DPackage ");
        //                            }
        //                            break;


        //                        //case Atend.Control.Enum.ProductType.Transformer:

        //                        //    edl.WriteMessage("\n116\n");
        //                        //    Atend.Base.Equipment.ETransformer Trans = Atend.Base.Equipment.ETransformer.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));

        //                        //    for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                        //    {
        //                        //        Atend.Base.Design.DPackage Pack = new DPackage();
        //                        //        Pack.ParentCode = PostPack.Code;
        //                        //        Pack.Count = 1;//Convert.ToInt32(ProductRow["Count"].ToString());
        //                        //        Pack.ProductCode = Trans.Code;
        //                        //        Pack.Type = (int)Atend.Control.Enum.ProductType.Transformer;

        //                        //        if (Pack.Insert(Transaction, Connection))
        //                        //        {
        //                        //            edl.WriteMessage("\n117\n");
        //                        //            Atend.Base.Acad.AcadGlobal.PostEquips RTrans = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //            RTrans.CodeGuid = Pack.Code;
        //                        //            RTrans.ParentCode = Pack.ParentCode;
        //                        //            RTrans.ProductCode = Pack.ProductCode;
        //                        //            RTrans.ProductType = (int)Atend.Control.Enum.ProductType.Transformer;

        //                        //            Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RTrans);

        //                        //            if (!SubProduct(Convert.ToInt32(ProductRow["ContainerPackageCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), Pack.ParentCode, Transaction, Connection))
        //                        //            {
        //                        //                edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Sub Products \n");
        //                        //                throw new Exception("while saving Sub Products in DPackage ");
        //                        //            }
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Transformer \n");
        //                        //            throw new Exception("while saving Transformer in DPackage ");
        //                        //        }
        //                        //    }

        //                        //    break;

        //                        //case Atend.Control.Enum.ProductType.MiddleJackPanel:

        //                        //    edl.WriteMessage("\n118\n");
        //                        //    Atend.Base.Equipment.EJAckPanel JackPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));

        //                        //    for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                        //    {
        //                        //        Atend.Base.Design.DPackage JackPack = new DPackage();
        //                        //        JackPack.ParentCode = PostPack.Code;
        //                        //        JackPack.Count = 1;//Convert.ToInt32(ProductRow["Count"].ToString());
        //                        //        JackPack.Type = (int)Atend.Control.Enum.ProductType.MiddleJackPanel;
        //                        //        JackPack.ProductCode = JackPanel.Code;

        //                        //        if (JackPack.Insert(Transaction, Connection))
        //                        //        {
        //                        //            edl.WriteMessage("\n119\n");
        //                        //            Atend.Base.Acad.AcadGlobal.PostEquips RJack = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //            RJack.CodeGuid = JackPack.Code;
        //                        //            RJack.ParentCode = JackPack.ParentCode;
        //                        //            RJack.ProductCode = JackPack.ProductCode;
        //                        //            RJack.ProductType = (int)Atend.Control.Enum.ProductType.MiddleJackPanel;

        //                        //            Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RJack);
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For MiddleJackPanel \n");
        //                        //            throw new Exception("while saving MiddleJackPanel in DPackage ");
        //                        //        }


        //                        //        DataTable JackTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelCode(JackPanel.Code);

        //                        //        edl.WriteMessage("\n120\n");
        //                        //        foreach (DataRow JackRow in JackTbl.Rows)
        //                        //        {
        //                        //            DPackage P = new DPackage();
        //                        //            P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                        //            P.ParentCode = JackPack.Code;
        //                        //            P.Type = (int)Atend.Control.Enum.ProductType.Cell;//Convert.ToInt32(Dr["Type"].ToString());
        //                        //            P.ProductCode = Convert.ToInt32(JackRow["ProductCode"].ToString());

        //                        //            if (P.Insert(Transaction, Connection))
        //                        //            {
        //                        //                //edl.WriteMessage("\n125\n");
        //                        //                Atend.Base.Acad.AcadGlobal.PostEquips RCell = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //                RCell.CodeGuid = P.Code;
        //                        //                RCell.ParentCode = P.ParentCode;
        //                        //                RCell.ProductCode = P.ProductCode;
        //                        //                RCell.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                        //                Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RCell);
        //                        //            }
        //                        //            else
        //                        //            {
        //                        //                edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving DPackage For Cell \n");
        //                        //                throw new Exception("while saving Cell in DPackage");
        //                        //            }

        //                        //            edl.WriteMessage("\n121\n");
        //                        //            Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JackRow["ProductCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);
        //                        //            DataTable Tbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(CP.Code);

        //                        //            edl.WriteMessage("\n122\n");
        //                        //            foreach (DataRow Dr in Tbl.Rows)
        //                        //            {
        //                        //                edl.WriteMessage("\n123\n");
        //                        //                //DPackage P = new DPackage();
        //                        //                //P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                        //                //P.ParentCode = JackPack.Code;
        //                        //                //P.Type = (int)Atend.Control.Enum.ProductType.Cell;
        //                        //                //P.ProductCode = Convert.ToInt32(Dr["ProductCode"].ToString());

        //                        //                //edl.WriteMessage("\n124\n");
        //                        //                //if (P.Insert(Transaction, Connection))
        //                        //                //{
        //                        //                //    edl.WriteMessage("\n125\n");
        //                        //                //    Atend.Base.Acad.AcadGlobal.PostEquips RCell = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //                //    RCell.CodeGuid = P.Code;
        //                        //                //    RCell.ParentCode = P.ParentCode;
        //                        //                //    RCell.ProductCode = P.ProductCode;
        //                        //                //    RCell.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                        //                //    Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RCell);

        //                        //                if (!SubProduct(Convert.ToInt32(Dr["ContainerPackageCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell, P.ParentCode, Transaction, Connection))
        //                        //                {
        //                        //                    edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Sub Products \n");
        //                        //                    throw new Exception("while saving Sub Products in DPackage ");
        //                        //                }

        //                        //                //}
        //                        //                //else
        //                        //                //{
        //                        //                //    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                        //                //    throw new Exception("while saving Cell in DPackage");
        //                        //                //}

        //                        //                edl.WriteMessage("\n126\n");
        //                        //                DCellStatus CellSt = new DCellStatus();
        //                        //                CellSt.CellCode = P.Code;
        //                        //                CellSt.JackPanelCode = JackPack.Code;
        //                        //                CellSt.IsClosed = false;
        //                        //                if (CellSt.Insert(Transaction, Connection))
        //                        //                { }
        //                        //                else
        //                        //                {
        //                        //                    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving Cell Status For \n");
        //                        //                    throw new Exception("while saving Cell Status");
        //                        //                }
        //                        //            }
        //                        //        }
        //                        //    }
        //                        //    break;

        //                        //case Atend.Control.Enum.ProductType.WeekJackPanel:

        //                        //    edl.WriteMessage("\n127\n");
        //                        //    Atend.Base.Equipment.EJackPanelWeek JackPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Convert.ToInt32(ProductRow["ProductCode"].ToString()));

        //                        //    for (int i = 0; i < Convert.ToInt32(ProductRow["Count"].ToString()); i++)
        //                        //    {
        //                        //        Atend.Base.Design.DPackage JackPackW = new DPackage();
        //                        //        JackPackW.ParentCode = PostPack.Code;
        //                        //        JackPackW.Count = 1;//Convert.ToInt32(ProductRow["Count"].ToString());
        //                        //        JackPackW.Type = (int)Atend.Control.Enum.ProductType.WeekJackPanel;
        //                        //        JackPackW.ProductCode = JackPanelW.Code;

        //                        //        edl.WriteMessage("\n128\n");
        //                        //        if (JackPackW.Insert(Transaction, Connection))
        //                        //        {
        //                        //            edl.WriteMessage("\n129\n");
        //                        //            Atend.Base.Acad.AcadGlobal.PostEquips RJPW = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //            RJPW.CodeGuid = JackPackW.Code;
        //                        //            RJPW.ParentCode = JackPackW.ParentCode;
        //                        //            RJPW.ProductCode = JackPackW.ProductCode;
        //                        //            RJPW.ProductType = (int)Atend.Control.Enum.ProductType.WeekJackPanel;

        //                        //            Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RJPW);
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For WeekJackPanel \n");
        //                        //            throw new Exception("while saving WeekJack Panel in DPackage ");
        //                        //        }

        //                        //        DataTable JackTblW = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(JackPanelW.Code);
        //                        //        edl.WriteMessage("\n130\n");

        //                        //        foreach (DataRow JackRow in JackTblW.Rows)
        //                        //        {
        //                        //            DPackage P = new DPackage();
        //                        //            P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                        //            P.ParentCode = JackPackW.Code;
        //                        //            P.Type = (int)Atend.Control.Enum.ProductType.Cell;
        //                        //            P.ProductCode = Convert.ToInt32(JackRow["Code"].ToString());

        //                        //            //edl.WriteMessage("\n134\n");

        //                        //            if (P.Insert(Transaction, Connection))
        //                        //            { }
        //                        //            else
        //                        //            {
        //                        //                edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving DPackage For Cell \n");
        //                        //                throw new Exception("while saving Cell in DPackage ");
        //                        //            }

        //                        //            edl.WriteMessage("\n131\n");

        //                        //            Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JackRow["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);
        //                        //            DataTable Tbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCode(CP.Code);

        //                        //            edl.WriteMessage("\n132\n");

        //                        //            foreach (DataRow Dr in Tbl.Rows)
        //                        //            {
        //                        //                edl.WriteMessage("\n133\n");

        //                        //                //DPackage P = new DPackage();
        //                        //                //P.Count = 1;// Convert.ToInt32(Dr["Count"].ToString());
        //                        //                //P.ParentCode = JackPackW.Code;
        //                        //                //P.Type = (int)Atend.Control.Enum.ProductType.Cell;
        //                        //                //P.ProductCode = Convert.ToInt32(Dr["ProductCode"].ToString());

        //                        //                //edl.WriteMessage("\n134\n");

        //                        //                //if (P.Insert(Transaction, Connection))
        //                        //                //{
        //                        //                //    edl.WriteMessage("\n135\n");

        //                        //                //Atend.Base.Acad.AcadGlobal.PostEquips RC = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //                //RC.CodeGuid = P.Code;
        //                        //                //RC.ParentCode = P.ParentCode;
        //                        //                //RC.ProductCode = P.ProductCode;
        //                        //                //RC.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                        //                //Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RC);
        //                        //                if (!SubProduct(Convert.ToInt32(Dr["ContainerPackageCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell, P.ParentCode, Transaction, Connection))
        //                        //                {
        //                        //                    edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Sub Products \n");
        //                        //                    throw new Exception("while saving Sub Products in DPackage ");
        //                        //                }

        //                        //                //}
        //                        //                //else
        //                        //                //{
        //                        //                //    edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Cell \n");
        //                        //                //    throw new Exception("while saving Cell in DPackage ");
        //                        //                //}

        //                        //            }
        //                        //        }
        //                        //    }
        //                        //    break;

        //                        //default:

        //                        //    edl.WriteMessage("\n136\n");

        //                        //    Atend.Base.Design.DPackage Package = new DPackage();
        //                        //    Package.ParentCode = PostPack.Code;
        //                        //    Package.Count = Convert.ToInt32(ProductRow["Count"].ToString());
        //                        //    Package.ProductCode = Convert.ToInt32(ProductRow["ProductCode"].ToString());
        //                        //    Package.Type = Convert.ToInt32(ProductRow["Type"].ToString());

        //                        //    edl.WriteMessage("\n137\n");

        //                        //    if (Package.Insert(Transaction, Connection))
        //                        //    {
        //                        //        edl.WriteMessage("\n138\n");

        //                        //        Atend.Base.Acad.AcadGlobal.PostEquips RD = new Atend.Base.Acad.AcadGlobal.PostEquips();
        //                        //        RD.CodeGuid = Package.Code;
        //                        //        RD.ParentCode = Package.ParentCode;
        //                        //        RD.ProductCode = Package.ProductCode;
        //                        //        RD.ProductType = (int)Atend.Control.Enum.ProductType.Cell;

        //                        //        Atend.Base.Acad.AcadGlobal.PostEquipInserted.Add(RD);

        //                        //        if (!SubProduct(Convert.ToInt32(ProductRow["ContainerPackageCode"].ToString()), Convert.ToInt32(ProductRow["TableType"].ToString()), Package.ParentCode, Transaction, Connection))
        //                        //        {
        //                        //            edl.WriteMessage("Error NodeTransaction.InsertGroundPost : while saving Sub Products \n");
        //                        //            throw new Exception("while saving Sub Products in DPackage ");
        //                        //        }
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Default \n");
        //                        //        throw new Exception("while saving Default in DPackage ");
        //                        //    }
        //                        //    break;


        //                    }
        //                }

        //                //if (PostPack.Type == (int)Atend.Control.Enum.ProductType.AirPost)
        //                //{
        //                //    //Atend.Base.Design.DPackage Pack = Atend.Base.Design.DPackage.SelectByCode(PR.CodeGuid);
        //                //    DataTable OprTable = Atend.Base.Equipment.EOperation.SelectByProductCodeType(PostPack.ProductCode, (int)Atend.Control.Enum.ProductType.GroundPost);

        //                //    foreach (DataRow OprRow in OprTable.Rows)
        //                //    {
        //                //        Atend.Base.Design.DPackage Package = new DPackage();
        //                //        Package.ParentCode = PostPack.Code;
        //                //        Package.Count = 1;
        //                //        Package.Type = (int)Atend.Control.Enum.ProductType.Operation;
        //                //        Package.ProductCode = Convert.ToInt32(OprRow["ProductID"].ToString());

        //                //        if (!Package.Insert(Transaction, Connection))
        //                //        {
        //                //            edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPackage For Operation \n");
        //                //            throw new Exception("while saving Operation in DPackage ");
        //                //        }
        //                //    }

        //                //}
        //            }
        //            else
        //            {
        //                edl.WriteMessage("Error NodeTransaction.InsertAirPost : while saving DPost  \n");
        //                throw new Exception("while saving DPost");
        //            }

        //        }
        //        catch (System.Exception ex2)
        //        {
        //            edl.WriteMessage(string.Format("Error NodeTransaction.InsertAirPost  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }

        //    }
        //    catch (System.Exception ex1)
        //    {
        //        edl.WriteMessage(string.Format("Error NodeTransaction.InsertAirPost  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //    foreach (Atend.Base.Acad.AcadGlobal.PostEquips PR in Atend.Base.Acad.AcadGlobal.PostEquipInserted)
        //    {
        //        edl.WriteMessage("\nPackageCode = " + PR.CodeGuid.ToString() + "   ProductCode = " + PR.ProductCode.ToString() + "   Type = " + PR.ProductType.ToString() + "\n");

        //    }


        //    foreach (Atend.Base.Acad.AcadGlobal.PostEquips p in Atend.Base.Acad.AcadGlobal.PostEquipInserted)
        //    {
        //        edl.WriteMessage("PC:{0},\nCG:{1},\nPT:{2},\nPC:{3}\n",p.ParentCode,p.CodeGuid,p.ProductType,p.ProductCode);
        //        edl.WriteMessage("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        //    }


        //    Transaction.Commit();
        //    Connection.Close();

        //    return true;
        //}


        //EXTRA
        //public bool InsertPole()
        //{
        //    Atend.Base.Acad.AcadGlobal.dConsolCode.Clear();
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();
        //        try
        //        {

        //            ed.WriteMessage("~~~~~~~ Start Save Pole Information ~~~~~~~~\n");

        //            //DDesignSetting dDesignSetting = new DDesignSetting();
        //            //dDesignSetting.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //            //dDesignSetting.Type = Atend.Control.Enum.DesignSettingType.LastNodeNumber;
        //            //dDesignSetting.Value = Atend.Control.Common.NCounter;
        //            //if (dDesignSetting.Update(Transaction, Connection))
        //            //{
        //            //ed.WriteMessage(string.Format("I am going to insert dnode \n"));
        //            //Atend.Control.Common.NCounter++;
        //            Atend.Base.Acad.AcadGlobal.dNode.Number = "N"; //string.Format("N{0}", Atend.Control.Common.NCounter);
        //            Comment = Atend.Base.Acad.AcadGlobal.dNode.Number;
        //            if (Atend.Base.Acad.AcadGlobal.dNode.Insert(Transaction, Connection))
        //            {
        //                //ed.WriteMessage(string.Format("I am going to insert poleinfo \n"));
        //                Atend.Base.Acad.AcadGlobal.dPoleInfo.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                //ed.WriteMessage(string.Format("dnode code : {0} \n",MyDNode.Code));
        //                if (Atend.Base.Acad.AcadGlobal.dPoleInfo.Insert(Transaction, Connection))
        //                {
        //                    //ed.WriteMessage(string.Format("I am going to insert package \n"));
        //                    Atend.Base.Acad.AcadGlobal.dPackage.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                    //myDPackage.ParentCode = new Guid();
        //                    if (Atend.Base.Acad.AcadGlobal.dPackage.Insert(Transaction, Connection))
        //                    {
        //                        //ed.WriteMessage("Pole record inserted to dpackage\n");
        //                        //Done Successfully
        //                        ed.WriteMessage(string.Format("Going to packages iteration. \n"));
        //                        //ed.WriteMessage("number of rows in MyDPackages: " + MyDPackages.Count.ToString() + "\n");
        //                        for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackages.Count; i++)
        //                        {
        //                            DPackage tempPackage = (DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                            //ed.WriteMessage(string.Format("MyDPackages.type : {0}\n", tempPackage.Type));
        //                            //PromptIntegerResult r31 = ed.GetInteger("sss :");
        //                            tempPackage.ParentCode = Atend.Base.Acad.AcadGlobal.dPackage.Code;
        //                            //ed.WriteMessage(string.Format("I am going to packag insert \n"));
        //                            if (tempPackage.Insert(Transaction, Connection))
        //                            {
        //                                //ed.WriteMessage(string.Format(">>> code: {0} nodecode :{1} parentcode: {2} type:{3} productcode: {4} \n", tempPackage.Code, tempPackage.NodeCode, tempPackage.ParentCode, tempPackage.Type, tempPackage.ProductCode));
        //                                //PromptIntegerResult r2 = ed.GetInteger("dPackage inserted:");
        //                                //save sub equips
        //                                if (tempPackage.Type == (int)Atend.Control.Enum.ProductType.Consol)
        //                                {
        //                                    ed.WriteMessage("TepmPackage.Code= " + tempPackage.Code.ToString() + "\n");
        //                                    Atend.Base.Acad.AcadGlobal.dConsolCode.Add(tempPackage.Code);
        //                                    //PromptIntegerResult r3 = ed.GetInteger("type is consol :");
        //                                    //ed.WriteMessage(string.Format("package was a consol and go to sub product \n"));
        //                                    ed.WriteMessage("Go to SubProducts. \n");
        //                                    if (SubProducts(tempPackage.ProductCode, tempPackage.Type, tempPackage.Code, Transaction, Connection))
        //                                    {
        //                                        ed.WriteMessage(string.Format("All Information for Pole saved successfully \n"));
        //                                    }
        //                                    else
        //                                    {
        //                                        ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole sub package information  \n"));
        //                                        throw new Exception("while saving pole sub package information");
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole package information  \n"));
        //                                throw new Exception("while saving pole package information");
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPackage for pole information \n");
        //                        throw new Exception("while saving DPackage information");
        //                    }
        //                }
        //                else
        //                {
        //                    ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPoleInfo information \n");
        //                    throw new Exception("while saving DPoleInfo information");
        //                }
        //            }
        //            else
        //            {
        //                ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DNode information \n");
        //                throw new Exception("while saving DNode information");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.DesignSettngUpdate : while updating DesignSetting information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}

        //            ed.WriteMessage("~~~~~~~End Save Pole Information ~~~~~~~~~\n");


        //        }
        //        catch (System.Exception ex2)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}

        //EXTRA
        //public bool UpdatePole()
        //{
        //    Atend.Base.Acad.AcadGlobal.dConsolCode.Clear();
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();
        //        try
        //        {

        //            ed.WriteMessage("~~~~~~~ Start Save Pole Information ~~~~~~~~\n");


        //            //Atend.Base.Acad.AcadGlobal.dNode.Number = "N"; //string.Format("N{0}", Atend.Control.Common.NCounter);
        //            //Comment = Atend.Base.Acad.AcadGlobal.dNode.Number;
        //            if (Atend.Base.Acad.AcadGlobal.dNode.UpdateProductCode(Transaction, Connection))
        //            {
        //                //ed.WriteMessage(string.Format("I am going to insert poleinfo \n"));
        //                //Atend.Base.Acad.AcadGlobal.dPoleInfo.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                //ed.WriteMessage(string.Format("dnode code : {0} \n",MyDNode.Code));
        //                if (Atend.Base.Acad.AcadGlobal.dPoleInfo.Update(Transaction, Connection))
        //                {
        //                    //ed.WriteMessage(string.Format("I am going to insert package \n"));
        //                    //Atend.Base.Acad.AcadGlobal.dPackage.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                    //myDPackage.ParentCode = new Guid();
        //                    //if (Atend.Base.Acad.AcadGlobal.dPackage.Insert(Transaction, Connection))
        //                    //{
        //                    //ed.WriteMessage("Pole record inserted to dpackage\n");
        //                    //Done Successfully
        //                    //ed.writeMessage(string.Format("Going to packages iteration. \n"));
        //                    //ed.WriteMessage("number of rows in MyDPackages: " + MyDPackages.Count.ToString() + "\n");
        //                    for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackages.Count; i++)
        //                    {
        //                        DPackage tempPackage = (DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                        //ed.WriteMessage(string.Format("MyDPackages.type : {0}\n", tempPackage.Type));
        //                        //PromptIntegerResult r31 = ed.GetInteger("sss :");
        //                        //tempPackage.ParentCode = Atend.Base.Acad.AcadGlobal.dPackage.Code;
        //                        //ed.WriteMessage(string.Format("I am going to packag insert \n"));
        //                        if (tempPackage.Insert(Transaction, Connection))
        //                        {
        //                            //ed.WriteMessage(string.Format(">>> code: {0} nodecode :{1} parentcode: {2} type:{3} productcode: {4} \n", tempPackage.Code, tempPackage.NodeCode, tempPackage.ParentCode, tempPackage.Type, tempPackage.ProductCode));
        //                            //PromptIntegerResult r2 = ed.GetInteger("dPackage inserted:");
        //                            //save sub equips
        //                            if (tempPackage.Type == (int)Atend.Control.Enum.ProductType.Consol)
        //                            {
        //                                //ed.writeMessage("TepmPackage.Code= " + tempPackage.Code.ToString() + "\n");
        //                                Atend.Base.Acad.AcadGlobal.dConsolCode.Add(tempPackage.Code);
        //                                //PromptIntegerResult r3 = ed.GetInteger("type is consol :");
        //                                //ed.WriteMessage(string.Format("package was a consol and go to sub product \n"));
        //                                //ed.writeMessage("Go to SubProducts. \n");
        //                                if (SubProducts(tempPackage.ProductCode, tempPackage.Type, tempPackage.Code, Transaction, Connection))
        //                                {
        //                                    //ed.writeMessage(string.Format("All Information for Pole saved successfully \n"));
        //                                }
        //                                else
        //                                {
        //                                    //ed.writeMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole sub package information  \n"));
        //                                    throw new Exception("while saving pole sub package information");
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            //ed.writeMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole package information  \n"));
        //                            throw new Exception("while saving pole package information");
        //                        }
        //                    }

        //                    //}
        //                    //else
        //                    //{
        //                    //    ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPackage for pole information \n");
        //                    //    throw new Exception("while saving DPackage information");
        //                    //}
        //                }
        //                else
        //                {
        //                    //ed.writeMessage("Error NodeTransaction.InsertPole : while saving DPoleInfo information \n");
        //                    throw new Exception("while saving DPoleInfo information");
        //                }

        //            }
        //            else
        //            {
        //                //ed.writeMessage("Error NodeTransaction.InsertPole : while saving DNode information \n");
        //                throw new Exception("while saving DNode information");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.DesignSettngUpdate : while updating DesignSetting information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}

        //            ed.WriteMessage("~~~~~~~End Save Pole Information ~~~~~~~~~\n");


        //        }
        //        catch (System.Exception ex2)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}

        //EXTRA
        //public bool EditPole()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();
        //        try
        //        {
        //            //DDesignSetting dDesignSetting = new DDesignSetting();
        //            //dDesignSetting.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //            //dDesignSetting.Type = Atend.Control.Enum.DesignSettingType.LastNodeNumber;
        //            //dDesignSetting.Value = Atend.Control.Common.NCounter;
        //            //if (dDesignSetting.Update(Transaction, Connection))
        //            //{
        //            //ed.WriteMessage(string.Format("I am going to insert dnode \n"));
        //            //Atend.Control.Common.NCounter++;
        //            //myDNode.Number = string.Format("N{0}", Atend.Control.Common.NCounter);
        //            //Comment = myDNode.Number;
        //            //if (myDNode.Insert(Transaction, Connection))
        //            //{
        //            ed.WriteMessage(string.Format("I am going to insert poleinfo \n"));
        //            Atend.Base.Acad.AcadGlobal.dPoleInfo.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //            ed.WriteMessage(string.Format("dnode code : {0} \n", Atend.Base.Acad.AcadGlobal.dNode.Code));
        //            if (Atend.Base.Acad.AcadGlobal.dPoleInfo.Insert(Transaction, Connection))
        //            {
        //                ed.WriteMessage(string.Format("I am going to insert package \n"));
        //                Atend.Base.Acad.AcadGlobal.dPackage.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                Atend.Base.Acad.AcadGlobal.dPackage.ParentCode = new Guid();
        //                if (Atend.Base.Acad.AcadGlobal.dPackage.Insert(Transaction, Connection))
        //                {
        //                    //Done Successfully
        //                    ed.WriteMessage(string.Format("I am going to packages iteration \n"));
        //                    for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackages.Count; i++)
        //                    {
        //                        DPackage tempPackage = (DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                        tempPackage.ParentCode = Atend.Base.Acad.AcadGlobal.dPackage.Code;
        //                        ed.WriteMessage(string.Format("I am going to packag insert \n"));
        //                        if (tempPackage.Insert(Transaction, Connection))
        //                        {
        //                            //save sub equips
        //                            if (tempPackage.Type == (int)Atend.Control.Enum.ProductType.Consol)
        //                            {
        //                                ed.WriteMessage(string.Format("package was a consol and go to sub product \n"));
        //                                if (SubProducts(tempPackage.ProductCode, tempPackage.Type, tempPackage.Code, Transaction, Connection))
        //                                {
        //                                    ed.WriteMessage(string.Format("All Information for Pole saved successfully \n"));
        //                                }
        //                                else
        //                                {
        //                                    ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole sub package information  \n"));
        //                                    throw new Exception("while saving pole sub package information");
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole package information  \n"));
        //                            throw new Exception("while saving pole package information");
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPackage for pole information \n");
        //                    throw new Exception("while saving DPackage information");
        //                }
        //            }
        //            else
        //            {
        //                ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPoleInfo information \n");
        //                throw new Exception("while saving DPoleInfo information");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DNode information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.DesignSettngUpdate : while updating DesignSetting information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}
        //        }
        //        catch (System.Exception ex2)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.Insert  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}

        //public bool XXSubProducts(int ContainerCode, int Type, Guid ParentCode, OleDbTransaction Transaction, OleDbConnection Connection)
        //{

        //    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerCode, Type);
        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {
        //        DPackage packageTemp = new DPackage();
        //        packageTemp.Count = Convert.ToInt32(row1["Count"]);
        //        packageTemp.ParentCode = ParentCode;
        //        packageTemp.Type = Convert.ToInt32(row1["TableType"]);
        //        packageTemp.ProductCode = Convert.ToInt32(row1["ProductCode"]);

        //        if (packageTemp.AccessInsert(Transaction, Connection))
        //        {

        //            SubProducts(Convert.ToInt32(row1["ProductCode"]), Convert.ToInt32(row1["TableType"]), packageTemp.Code, Transaction, Connection);

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public static bool SubProductsForServer(Guid XCode, int ServerCode, int ServerContainerCode, int Type, SqlTransaction LocalTransaction, SqlConnection LocalConnection, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        //{
        //    //if (LocalContainerCode < 1)
        //    //    return true;

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("ContainerCode = " + ServerContainerCode.ToString());

        //    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(ServerTransaction, ServerConnection, ServerContainerCode, Type);

        //    ed.WriteMessage("\nProduct Count = " + productPackageTable.Rows.Count.ToString());


        //    //Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(ServerTransaction, ServerConnection, XCode, Type);
        //    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(LocalTransaction, LocalConnection, XCode, Type);

        //    Atend.Base.Equipment.EContainerPackage containerPackage = new Atend.Base.Equipment.EContainerPackage();
        //    int containercode = 0;

        //    if (CP.Code != 0)
        //    {

        //        containercode = CP.Code;
        //        //CP.XCode = XCode;
        //        //CP.Type = Type;

        //        //if (CP.InsertX(LocalTransaction, LocalConnection))
        //        //{
        //        //    containercode = CP.Code;
        //        //}
        //    }
        //    else
        //        if (productPackageTable.Rows.Count > 0)
        //        {
        //            //containerPackage.ContainerCode = ServerCode;
        //            containerPackage.Type = Type;
        //            containerPackage.XCode = XCode;

        //            if (containerPackage.Insert(LocalTransaction, LocalConnection))
        //            {
        //                containercode = containerPackage.Code;
        //            }

        //        }


        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {
        //        // FindEquipment(ServerTransaction, ServerConnection, LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), new Guid(row1["XCode"].ToString()));


        //        //if (CP.XCode == Guid.Empty)
        //        //{
        //        //    containerPackage.ContainerCode = Code;// containerCode;
        //        //    containerPackage.XCode = XCode;
        //        //    containerPackage.Type = Type;
        //        //    if (containerPackage.Insert(ServerTransaction, ServerConnection))
        //        //    {
        //        //        containercode = containerPackage.Code;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    //if(!Atend.Base.Equipment.EProductPackage.Delete(ServerTransaction,ServerConnection,CP.Code))
        //        //    //    throw new Exception("while delete Products in AtendServer in Sub Product");

        //        //    containercode = CP.Code;
        //        //}



        //        ed.WriteMessage("\nBefor\n");
        //        int DelCode = 0;
        //        int ProdCode = FindEquipment(ServerTransaction, ServerConnection, LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), new Guid(row1["XCode"].ToString()), ref DelCode);
        //        ed.WriteMessage("\nAfter\n");

        //        Atend.Base.Equipment.EProductPackage PPackage = new Atend.Base.Equipment.EProductPackage();
        //        PPackage.Count = Convert.ToInt32(row1["Count"].ToString());
        //        PPackage.ContainerPackageCode = containercode;//Convert.ToInt32(row1["ContainerPackageCode"].ToString());
        //        PPackage.ProductCode = ProdCode;
        //        PPackage.TableType = Convert.ToInt16(row1["TableType"].ToString());
        //        PPackage.XCode = new Guid(row1["XCode"].ToString());
        //        ed.WriteMessage("\nProduct XCode = " + PPackage.XCode.ToString());


        //        if (PPackage.Insert(LocalTransaction, LocalConnection))
        //        {
        //            Atend.Base.Equipment.EContainerPackage CPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(LocalTransaction, LocalConnection, PPackage.XCode, PPackage.TableType);
        //            SubProducts(DelCode, ProdCode, PPackage.XCode, CPackage.Code, CPackage.Type, LocalTransaction, LocalConnection, LocalTransaction, LocalConnection);
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }

        //    return true;
        //}


        ////public static bool SubProductsForServer(int Code, Guid XCode, int ServerContainerCode, int Type, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        ////{
        ////    //if (LocalContainerCode < 1)
        ////    //    return true;

        ////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////    //ed.WriteMessage("&&&&& Code:{0}\n      XCode:{1}\n      ServerContainerCode:{2}\n", Code, XCode, ServerContainerCode);

        ////    ed.WriteMessage("ContainerCode = " + ServerContainerCode.ToString());
        ////    //DataTable productPackagelocal=Atend.Base.Equipment.EContainerPackage.sele

        ////    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(/*ServerTransaction, ServerConnection,*/ ServerContainerCode, Type);
        ////    //Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(ServerTransaction, ServerConnection, XCode, Type);

        ////    Atend.Base.Equipment.EContainerPackage containerPackage = new Atend.Base.Equipment.EContainerPackage();
        ////    int containercode = 0;

        ////    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(LocalTransaction, LocalConnection, XCode, Type);

        ////    if (CP.Code != 0)
        ////    {
        ////        if (Atend.Base.Equipment.EContainerPackage.DeleteX(LocalTransaction, LocalConnection, XCode, Type))
        ////        {
        ////            if ((Atend.Base.Equipment.EProductPackage.DeleteX(LocalTransaction, LocalConnection, CP.Code)))
        ////            {

        ////            }
        ////        }
        ////    }

        ////    ed.WriteMessage("\n~~~~~~~~Prod Count = " + productPackageTable.Rows.Count + "\n");
        ////    if (productPackageTable.Rows.Count > 0)
        ////    {
        ////        containerPackage.ContainerCode = Code;
        ////        containerPackage.Type = Type;
        ////        containerPackage.XCode = XCode;

        ////        if (containerPackage.InsertX(LocalTransaction, LocalConnection))
        ////        {
        ////            containercode = containerPackage.Code;
        ////        }
        ////    }


        ////    foreach (DataRow row1 in productPackageTable.Rows)
        ////    {
        ////        // FindEquipment(ServerTransaction, ServerConnection, LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), new Guid(row1["XCode"].ToString()));


        ////        //if (CP.XCode == Guid.Empty)
        ////        //{
        ////        //    containerPackage.ContainerCode = Code;// containerCode;
        ////        //    containerPackage.XCode = XCode;
        ////        //    containerPackage.Type = Type;
        ////        //    if (containerPackage.Insert(ServerTransaction, ServerConnection))
        ////        //    {
        ////        //        containercode = containerPackage.Code;
        ////        //    }
        ////        //}
        ////        //else
        ////        //{
        ////        //    //if(!Atend.Base.Equipment.EProductPackage.Delete(ServerTransaction,ServerConnection,CP.Code))
        ////        //    //    throw new Exception("while delete Products in AtendServer in Sub Product");

        ////        //    containercode = CP.Code;
        ////        //}



        ////        //ed.WriteMessage("\nBefor\n");
        ////        int DelCode = 0;
        ////        Guid DelXCode = new Guid();
        ////        DelXCode = Guid.Empty;
        ////        ed.WriteMessage("\n" + row1["ProductCode"].ToString() + "\n");

        ////        Guid ProdXCode = FindEquipmentForServer(LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), Convert.ToInt32(row1["ProductCode"].ToString()), ref DelXCode);
        ////        //ed.WriteMessage("\nAfter\n");

        ////        Atend.Base.Equipment.EProductPackage PPackage = new Atend.Base.Equipment.EProductPackage();
        ////        PPackage.Count = Convert.ToInt32(row1["Count"].ToString());
        ////        PPackage.ContainerPackageCode = containercode;//Convert.ToInt32(row1["ContainerPackageCode"].ToString());
        ////        PPackage.ProductCode = Convert.ToInt32(row1["ProductCode"].ToString());
        ////        PPackage.TableType = Convert.ToInt16(row1["TableType"].ToString());
        ////        PPackage.XCode = ProdXCode;// new Guid(row1["XCode"].ToString());
        ////        ed.WriteMessage("\nProduct XCode = " + PPackage.XCode.ToString());


        ////        if (PPackage.InsertX(LocalTransaction, LocalConnection))
        ////        {
        ////            Atend.Base.Equipment.EContainerPackage CPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(PPackage.ProductCode, PPackage.TableType);
        ////            SubProductsForServer(/*DelXCode,*/ PPackage.ProductCode, PPackage.XCode, CPackage.Code, CPackage.Type, LocalTransaction, LocalConnection);
        ////        }
        ////        else
        ////        {
        ////            return false;
        ////        }

        ////    }

        ////    return true;
        ////}














        ////private static Guid FindEquipmentForServer(SqlTransaction _Localtransaction, SqlConnection _Localconnection, int Type, int ServerCode, ref Guid DelXCode)
        ////{
        ////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////    DataTable OperationTbl = new DataTable();
        ////    //Code = 0;
        ////    DelXCode = Guid.NewGuid();

        ////    try
        ////    {
        ////        switch ((Atend.Control.Enum.ProductType)Type)
        ////        {

        ////            case Atend.Control.Enum.ProductType.Pole:
        ////                ed.WriteMessage("\nPOLE\n");

        ////                Atend.Base.Equipment.EPole ePole = Atend.Base.Equipment.EPole.SelectByCode(_Localtransaction, _Localconnection, ServerCode);

        ////                if (ePole.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = ePole.XCode;
        ////                    if (!Atend.Base.Equipment.EPole.DeleteX(_Localtransaction, _Localconnection, ePole.XCode))
        ////                    {
        ////                        throw new Exception("while delete pole in AtendServer");
        ////                    }

        ////                    ePole.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePole = Atend.Base.Equipment.EPole.SelectByCode(ServerCode);
        ////                    ePole.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                ePole.OperationList = new ArrayList();

        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);

        ////                ePole.OperationList = GetOperationListForServer(OperationTbl, ePole.XCode);

        ////                if (!ePole.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new pole in AtendServer");

        ////                return ePole.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.Conductor:
        ////                ed.WriteMessage("\nConductor\n");

        ////                ed.WriteMessage("\nCond1\n");
        ////                Atend.Base.Equipment.EConductor eCond = Atend.Base.Equipment.EConductor.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                ed.WriteMessage("\nCond2\n");

        ////                if (eCond.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eCond.XCode;
        ////                    ed.WriteMessage("\nCond3\n");

        ////                    if (!Atend.Base.Equipment.EConductor.DeleteX(_Localtransaction, _Localconnection, eCond.XCode))
        ////                    {
        ////                        throw new Exception("while delete Conductor in AtendServer");
        ////                    }

        ////                    ed.WriteMessage("\nCond4\n");

        ////                    eCond.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ed.WriteMessage("\nCond5\n");

        ////                    eCond = Atend.Base.Equipment.EConductor.SelectByCode(ServerCode);
        ////                    eCond.XCode = DelXCode;
        ////                    ed.WriteMessage("\nCond6\n");

        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                ed.WriteMessage("\nCond7\n");

        ////                eCond.OperationList = new ArrayList();

        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);

        ////                eCond.OperationList = GetOperationListForServer(OperationTbl, eCond.XCode);

        ////                if (!eCond.InsertX(_Localtransaction, _Localconnection))
        ////                {
        ////                    throw new Exception("while insert new Conductor in AtendServer");
        ////                }
        ////                ed.WriteMessage("\nCond8\n");

        ////                return eCond.XCode;

        ////                break;


        ////            case Atend.Control.Enum.ProductType.Mafsal:
        ////                ed.WriteMessage("\nMafsal\n");
        ////                Atend.Base.Equipment.EMafsal eMafsal = Atend.Base.Equipment.EMafsal.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eMafsal.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eMafsal.XCode;
        ////                    if (!Atend.Base.Equipment.EMafsal.DeleteX(_Localtransaction, _Localconnection, eMafsal.XCode))
        ////                    {
        ////                        throw new Exception("while delete Mafsal in AtendServer");
        ////                    }
        ////                    eMafsal.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eMafsal = Atend.Base.Equipment.EMafsal.SelectByCode(ServerCode);
        ////                    eMafsal.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eMafsal.OperationList = new ArrayList();

        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);

        ////                eMafsal.OperationList = GetOperationListForServer(OperationTbl, eMafsal.XCode);

        ////                if (!eMafsal.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Mafsal in AtendServer");

        ////                return eMafsal.XCode;

        ////                break;


        ////            case Atend.Control.Enum.ProductType.Floor:
        ////                ed.WriteMessage("\nFloor\n");
        ////                Atend.Base.Equipment.EFloor eFloor = Atend.Base.Equipment.EFloor.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eFloor.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eFloor.XCode;
        ////                    if (!Atend.Base.Equipment.EFloor.DeleteX(_Localtransaction, _Localconnection, eFloor.XCode))
        ////                    {
        ////                        throw new Exception("while delete eFloor in AtendServer");
        ////                    }
        ////                    eFloor.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eFloor = Atend.Base.Equipment.EFloor.SelectByCode(ServerCode);
        ////                    eFloor.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eFloor.OperationList = new ArrayList();

        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);

        ////                eFloor.OperationList = GetOperationListForServer(OperationTbl, eFloor.XCode);

        ////                if (!eFloor.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Floor in AtendServer");

        ////                return eFloor.XCode;

        ////                break;



        ////            case Atend.Control.Enum.ProductType.AuoKey3p:
        ////                ed.WriteMessage("\nAutoKey3P\n");
        ////                Atend.Base.Equipment.EAutoKey_3p eAuto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eAuto.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eAuto.XCode;
        ////                    if (!Atend.Base.Equipment.EAutoKey_3p.DeleteX(_Localtransaction, _Localconnection, eAuto.XCode))
        ////                    {
        ////                        throw new Exception("while delete AutoKey3P in AtendServer");
        ////                    }
        ////                    eAuto.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eAuto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(ServerCode);
        ////                    eAuto.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eAuto.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eAuto.OperationList = GetOperationListForServer(OperationTbl, eAuto.XCode);

        ////                if (!eAuto.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new AutoKey3P in AtendServer");


        ////                return eAuto.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.Breaker:
        ////                ed.WriteMessage("\nBreaker\n");
        ////                Atend.Base.Equipment.EBreaker eBreaker = Atend.Base.Equipment.EBreaker.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eBreaker.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eBreaker.XCode;
        ////                    if (!Atend.Base.Equipment.EBreaker.DeleteX(_Localtransaction, _Localconnection, eBreaker.XCode))
        ////                    {
        ////                        throw new Exception("while delete Breaker in AtendServer");
        ////                    }
        ////                    eBreaker.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eBreaker = Atend.Base.Equipment.EBreaker.SelectByCode(ServerCode);
        ////                    eBreaker.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eBreaker.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eBreaker.OperationList = GetOperationListForServer(OperationTbl, eBreaker.XCode);

        ////                if (!eBreaker.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Breaker in AtendServer");


        ////                return eBreaker.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.Cell:
        ////                ed.WriteMessage("\nCell\n");
        ////                Atend.Base.Equipment.ECell eCell = Atend.Base.Equipment.ECell.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eCell.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eCell.XCode;
        ////                    if (!Atend.Base.Equipment.ECell.DeleteX(_Localtransaction, _Localconnection, eCell.XCode))
        ////                    {
        ////                        throw new Exception("while delete Cell in AtendServer");
        ////                    }
        ////                    eCell.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eCell = Atend.Base.Equipment.ECell.SelectByCode(ServerCode);
        ////                    eCell.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eCell.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eCell.OperationList = GetOperationListForServer(OperationTbl, eCell.XCode);

        ////                if (!eCell.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Cell in AtendServer");


        ////                return eCell.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Bus:
        ////                ed.WriteMessage("\nBus\n");
        ////                Atend.Base.Equipment.EBus eBus = Atend.Base.Equipment.EBus.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eBus.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eBus.XCode;
        ////                    if (!Atend.Base.Equipment.EBus.DeleteX(_Localtransaction, _Localconnection, eBus.XCode))
        ////                    {
        ////                        throw new Exception("while delete Bus in AtendServer");
        ////                    }
        ////                    eBus.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eBus = Atend.Base.Equipment.EBus.SelectByCode(ServerCode);
        ////                    eBus.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eBus.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eBus.OperationList = GetOperationListForServer(OperationTbl, eBus.XCode);

        ////                if (!eBus.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Bus in AtendServer");


        ////                return eBus.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.MiniatureKey:
        ////                ed.WriteMessage("\nMiniatureKey\n");
        ////                Atend.Base.Equipment.EMiniatorKey eMiniatorKey = Atend.Base.Equipment.EMiniatorKey.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eMiniatorKey.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eMiniatorKey.XCode;
        ////                    if (!Atend.Base.Equipment.EMiniatorKey.DeleteX(_Localtransaction, _Localconnection, eMiniatorKey.XCode))
        ////                    {
        ////                        throw new Exception("while delete eMiniatorKey in AtendServer");
        ////                    }
        ////                    eMiniatorKey.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eMiniatorKey = Atend.Base.Equipment.EMiniatorKey.SelectByCode(ServerCode);
        ////                    eMiniatorKey.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eMiniatorKey.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eMiniatorKey.OperationList = GetOperationListForServer(OperationTbl, eMiniatorKey.XCode);

        ////                if (!eMiniatorKey.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new eMiniatorKey in AtendServer");


        ////                return eMiniatorKey.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.CatOut:
        ////                ed.WriteMessage("\nCatout\n");
        ////                Atend.Base.Equipment.ECatOut eCatout = Atend.Base.Equipment.ECatOut.SelectByCode(_Localtransaction, _Localconnection, ServerCode);

        ////                if (eCatout.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eCatout.XCode;
        ////                    if (!Atend.Base.Equipment.ECatOut.DeleteX(_Localtransaction, _Localconnection, eCatout.XCode))
        ////                    {
        ////                        throw new Exception("while delete Catout in AtendServer");
        ////                    }
        ////                    eCatout.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eCatout = Atend.Base.Equipment.ECatOut.SelectByCode(ServerCode);
        ////                    eCatout.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }



        ////                eCatout.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eCatout.XCode);

        ////                eCatout.OperationList = GetOperationListForServer(OperationTbl, eCatout.XCode);

        ////                if (!eCatout.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Catout in AtendServer");

        ////                return eCatout.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.GroundCabel:
        ////                ed.WriteMessage("\nMiddleCabel\n");
        ////                Atend.Base.Equipment.EGroundCabel eMiddleCabel = Atend.Base.Equipment.EGroundCabel.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eMiddleCabel.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eMiddleCabel.XCode;
        ////                    if (!Atend.Base.Equipment.EGroundCabel.DeleteX(_Localtransaction, _Localconnection, eMiddleCabel.XCode))
        ////                    {
        ////                        throw new Exception("while delete eMiddleCabel in AtendServer");
        ////                    }
        ////                    eMiddleCabel.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eMiddleCabel = Atend.Base.Equipment.EGroundCabel.SelectByCode(ServerCode);
        ////                    eMiddleCabel.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }


        ////                eMiddleCabel.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eMiddleCabel.XCode);

        ////                eMiddleCabel.OperationList = GetOperationListForServer(OperationTbl, eMiddleCabel.XCode);

        ////                if (!eMiddleCabel.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new eMiddleCabel in AtendServer");

        ////                return eMiddleCabel.XCode;
        ////                break;



        ////            case Atend.Control.Enum.ProductType.CT:
        ////                ed.WriteMessage("\nCT\n");
        ////                Atend.Base.Equipment.ECT eCT = Atend.Base.Equipment.ECT.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eCT.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eCT.XCode;
        ////                    if (!Atend.Base.Equipment.ECT.DeleteX(_Localtransaction, _Localconnection, eCT.XCode))
        ////                    {
        ////                        throw new Exception("while delete CT in AtendServer");
        ////                    }
        ////                    eCT.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eCT = Atend.Base.Equipment.ECT.SelectByCode(ServerCode);
        ////                    eCT.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eCT.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);
        ////                eCT.OperationList = GetOperationListForServer(OperationTbl, eCT.XCode);


        ////                if (!eCT.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new CT in AtendServer");


        ////                return eCT.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.DB:
        ////                ed.WriteMessage("\nDB\n");

        ////                break;

        ////            case Atend.Control.Enum.ProductType.HeaderCabel:
        ////                ed.WriteMessage("\nHeaderCabel\n");
        ////                Atend.Base.Equipment.EHeaderCabel eHeader = Atend.Base.Equipment.EHeaderCabel.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eHeader.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eHeader.XCode;
        ////                    if (!Atend.Base.Equipment.EHeaderCabel.DeleteX(_Localtransaction, _Localconnection, eHeader.XCode))
        ////                    {
        ////                        throw new Exception("while delete HeaderCabel in AtendServer");
        ////                    }
        ////                    eHeader.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eHeader = Atend.Base.Equipment.EHeaderCabel.SelectByCode(ServerCode);
        ////                    eHeader.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eHeader.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);

        ////                eHeader.OperationList = GetOperationListForServer(OperationTbl, eHeader.XCode);

        ////                if (!eHeader.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new HeaderCabel in AtendServer");

        ////                return eHeader.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Disconnector:
        ////                ed.WriteMessage("\nDisconnector\n");
        ////                Atend.Base.Equipment.EDisconnector eDisconnector = Atend.Base.Equipment.EDisconnector.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eDisconnector.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eDisconnector.XCode;
        ////                    if (!Atend.Base.Equipment.EDisconnector.DeleteX(_Localtransaction, _Localconnection, eDisconnector.XCode))
        ////                    {
        ////                        throw new Exception("while delete HeaderCabel in AtendServer");
        ////                    }
        ////                    eDisconnector.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eDisconnector = Atend.Base.Equipment.EDisconnector.SelectByCode(ServerCode);
        ////                    eDisconnector.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }


        ////                eDisconnector.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDisconnector.XCode);

        ////                eDisconnector.OperationList = GetOperationListForServer(OperationTbl, eDisconnector.XCode);

        ////                if (!eDisconnector.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new HeaderCabel in AtendServer");


        ////                return eDisconnector.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Rod:
        ////                ed.WriteMessage("\nRod\n");
        ////                Atend.Base.Equipment.ERod eRod = Atend.Base.Equipment.ERod.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eRod.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eRod.XCode;
        ////                    if (!Atend.Base.Equipment.ERod.DeleteX(_Localtransaction, _Localconnection, eRod.XCode))
        ////                    {
        ////                        throw new Exception("while delete  Rod in AtendServer");
        ////                    }
        ////                    eRod.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eRod = Atend.Base.Equipment.ERod.SelectByCode(ServerCode);
        ////                    eRod.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eRod.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eRod.XCode);

        ////                eRod.OperationList = GetOperationListForServer(OperationTbl, eRod.XCode);

        ////                if (!eRod.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Rod in AtendServer");


        ////                return eRod.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Countor:
        ////                ed.WriteMessage("\nCountor\n");
        ////                Atend.Base.Equipment.ECountor eCountor = Atend.Base.Equipment.ECountor.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eCountor.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eCountor.XCode;
        ////                    if (!Atend.Base.Equipment.ECountor.DeleteX(_Localtransaction, _Localconnection, eCountor.XCode))
        ////                    {
        ////                        throw new Exception("while delete Countor in AtendServer");
        ////                    }
        ////                    eCountor.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eCountor = Atend.Base.Equipment.ECountor.SelectByCode(ServerCode);
        ////                    eCountor.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eCountor.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);

        ////                eCountor.OperationList = GetOperationListForServer(OperationTbl, eCountor.XCode);

        ////                if (!eCountor.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Countor in AtendServer");


        ////                return eCountor.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.PhotoCell:
        ////                ed.WriteMessage("\nPhotoCell\n");
        ////                Atend.Base.Equipment.EPhotoCell ePhotoCell = Atend.Base.Equipment.EPhotoCell.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePhotoCell.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = ePhotoCell.XCode;
        ////                    if (!Atend.Base.Equipment.EPhotoCell.DeleteX(_Localtransaction, _Localconnection, ePhotoCell.XCode))
        ////                    {
        ////                        throw new Exception("while delete PhotoCell in AtendServer");
        ////                    }
        ////                    ePhotoCell.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePhotoCell = Atend.Base.Equipment.EPhotoCell.SelectByCode(ServerCode);
        ////                    ePhotoCell.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                ePhotoCell.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);

        ////                ePhotoCell.OperationList = GetOperationListForServer(OperationTbl, ePhotoCell.XCode);


        ////                if (!ePhotoCell.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new PhotoCell in AtendServer");


        ////                return ePhotoCell.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.KablSho:
        ////                ed.WriteMessage("\nKablSho\n");
        ////                Atend.Base.Equipment.EKablsho eKablsho = Atend.Base.Equipment.EKablsho.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eKablsho.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eKablsho.XCode;
        ////                    if (!Atend.Base.Equipment.EKablsho.DeleteX(_Localtransaction, _Localconnection, eKablsho.XCode))
        ////                    {
        ////                        throw new Exception("while delete Kablsho in AtendServer");
        ////                    }
        ////                    eKablsho.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eKablsho = Atend.Base.Equipment.EKablsho.SelectByCode(ServerCode);
        ////                    eKablsho.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eKablsho.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);

        ////                eKablsho.OperationList = GetOperationListForServer(OperationTbl, eKablsho.XCode);

        ////                if (!eKablsho.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Kablsho in AtendServer");


        ////                return eKablsho.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Kalamp:
        ////                ed.WriteMessage("\nKalamp\n");
        ////                Atend.Base.Equipment.EClamp eClamp = Atend.Base.Equipment.EClamp.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eClamp.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eClamp.XCode;
        ////                    if (!Atend.Base.Equipment.EClamp.DeleteX(_Localtransaction, _Localconnection, eClamp.XCode))
        ////                    {
        ////                        throw new Exception("while delete eClamp in AtendServer");
        ////                    }
        ////                    eClamp.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eClamp = Atend.Base.Equipment.EClamp.SelectByCode(ServerCode);
        ////                    eClamp.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eClamp.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eClamp.OperationList = GetOperationListForServer(OperationTbl, eClamp.XCode);

        ////                if (!eClamp.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new eClamp in AtendServer");


        ////                return eClamp.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.Phuse:
        ////                ed.WriteMessage("\nPhuse\n");
        ////                Atend.Base.Equipment.EPhuse ePhuse = Atend.Base.Equipment.EPhuse.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePhuse.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = ePhuse.XCode;
        ////                    if (!Atend.Base.Equipment.EPhuse.DeleteX(_Localtransaction, _Localconnection, ePhuse.XCode))
        ////                    {
        ////                        throw new Exception("while delete Phuse in AtendServer");
        ////                    }
        ////                    ePhuse.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePhuse = Atend.Base.Equipment.EPhuse.SelectByCode(ServerCode);
        ////                    ePhuse.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }


        ////                Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(_Localtransaction, _Localconnection, ePhuse.PhusePoleCode);
        ////                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        ////                Guid PhusePoleDeleted = Guid.NewGuid();
        ////                if (PHP.XCode != Guid.Empty)
        ////                {
        ////                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                    PhusePoleDeleted = PHP.XCode;

        ////                    if (!Atend.Base.Equipment.EPhusePole.DeleteX(_Localtransaction, _Localconnection, PHP.XCode))
        ////                    {
        ////                        throw new Exception("while delete PhusePole in AtendServer");
        ////                    }

        ////                    PHP.ServerSelectByCode(ePhuse.PhusePoleCode);
        ////                }
        ////                else
        ////                {
        ////                    PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(ePhuse.PhusePoleCode);
        ////                    PHP.XCode = PhusePoleDeleted;
        ////                    PhusePoleDeleted = Guid.Empty;
        ////                }

        ////                PHP.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                PHP.OperationList = GetOperationListForServer(OperationTbl, PHP.XCode);

        ////                if (!PHP.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert PhusePole in AtendServer");


        ////                ePhuse.PhusePoleXCode = PHP.XCode;

        ////                ePhuse.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                ePhuse.OperationList = GetOperationListForServer(OperationTbl, ePhuse.XCode);

        ////                if (!ePhuse.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Phuse in AtendServer");


        ////                return ePhuse.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.StreetBox:
        ////                //Atend.Base.Equipment.EStreetBox eStreet = Atend.Base.Equipment.EStreetBox.SelectByXCode(XCode);
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Transformer:
        ////                ed.WriteMessage("\nTransformer\n");
        ////                Atend.Base.Equipment.ETransformer eTransformer = Atend.Base.Equipment.ETransformer.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eTransformer.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eTransformer.XCode;
        ////                    if (!Atend.Base.Equipment.ETransformer.DeleteX(_Localtransaction, _Localconnection, eTransformer.XCode))
        ////                    {
        ////                        throw new Exception("while delete Transformer in AtendServer");
        ////                    }

        ////                    eTransformer.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eTransformer = Atend.Base.Equipment.ETransformer.SelectByCode(ServerCode);
        ////                    eTransformer.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eTransformer.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eTransformer.OperationList = GetOperationListForServer(OperationTbl, eTransformer.XCode);

        ////                if (!eTransformer.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Transformer in AtendServer");

        ////                return eTransformer.XCode;
        ////                break;


        ////            case Atend.Control.Enum.ProductType.PT:
        ////                ed.WriteMessage("\nPT\n");
        ////                Atend.Base.Equipment.EPT ePT = Atend.Base.Equipment.EPT.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePT.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = ePT.XCode;
        ////                    if (!Atend.Base.Equipment.EPT.DeleteX(_Localtransaction, _Localconnection, ePT.XCode))
        ////                    {
        ////                        throw new Exception("while delete PT in AtendServer");
        ////                    }

        ////                    ePT.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePT = Atend.Base.Equipment.EPT.SelectByCode(ServerCode);
        ////                    ePT.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                ePT.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                ePT.OperationList = GetOperationListForServer(OperationTbl, ePT.XCode);

        ////                if (!ePT.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new PT in AtendServer");


        ////                return ePT.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Insulator:
        ////                ed.WriteMessage("\nInsulator\n");
        ////                Atend.Base.Equipment.EInsulator eInsulator = Atend.Base.Equipment.EInsulator.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eInsulator.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eInsulator.XCode;
        ////                    if (!Atend.Base.Equipment.EInsulator.DeleteX(_Localtransaction, _Localconnection, eInsulator.XCode))
        ////                    {
        ////                        throw new Exception("while delete  Insulator in AtendServer");
        ////                    }
        ////                    eInsulator.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eInsulator = Atend.Base.Equipment.EInsulator.SelectByCode(ServerCode);
        ////                    eInsulator.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eInsulator.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eInsulator.XCode);

        ////                eInsulator.OperationList = GetOperationListForServer(OperationTbl, eInsulator.XCode);

        ////                if (!eInsulator.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Insulator in AtendServer");

        ////                return eInsulator.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.ReCloser:
        ////                ed.WriteMessage("\nReCloser\n");
        ////                Atend.Base.Equipment.EReCloser eReCloser = Atend.Base.Equipment.EReCloser.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eReCloser.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eReCloser.XCode;
        ////                    if (!Atend.Base.Equipment.EReCloser.DeleteX(_Localtransaction, _Localconnection, eReCloser.XCode))
        ////                    {
        ////                        throw new Exception("while delete  ReCloser in AtendServer");
        ////                    }
        ////                    eReCloser.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eReCloser = Atend.Base.Equipment.EReCloser.SelectByCode(ServerCode);
        ////                    eReCloser.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                eReCloser.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eReCloser.OperationList = GetOperationListForServer(OperationTbl, eReCloser.XCode);

        ////                if (!eReCloser.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new  ReCloser in AtendServer");


        ////                return eReCloser.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.PhuseKey:
        ////                ed.WriteMessage("\nPhuseKey\n");
        ////                Atend.Base.Equipment.EPhuseKey ePhuseKey = Atend.Base.Equipment.EPhuseKey.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePhuseKey.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = ePhuseKey.XCode;
        ////                    if (!Atend.Base.Equipment.EPhuseKey.DeleteX(_Localtransaction, _Localconnection, ePhuseKey.XCode))
        ////                    {
        ////                        throw new Exception("while delete  PhuseKey in AtendServer");
        ////                    }
        ////                    ePhuseKey.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePhuseKey = Atend.Base.Equipment.EPhuseKey.SelectByCode(ServerCode);
        ////                    ePhuseKey.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                ePhuseKey.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                ePhuseKey.OperationList = GetOperationListForServer(OperationTbl, ePhuseKey.XCode);

        ////                if (!ePhuseKey.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new  PhuseKey in AtendServer");


        ////                return ePhuseKey.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.SectionLizer:
        ////                ed.WriteMessage("\nPhuseKey\n");
        ////                Atend.Base.Equipment.ESectionLizer eSec = Atend.Base.Equipment.ESectionLizer.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eSec.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eSec.XCode;
        ////                    if (!Atend.Base.Equipment.ESectionLizer.DeleteX(_Localtransaction, _Localconnection, eSec.XCode))
        ////                    {
        ////                        throw new Exception("while delete  ESectionLizer in AtendServer");
        ////                    }
        ////                    eSec.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eSec = Atend.Base.Equipment.ESectionLizer.SelectByCode(ServerCode);
        ////                    eSec.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eSec.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eSec.OperationList = GetOperationListForServer(OperationTbl, eSec.XCode);

        ////                if (!eSec.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new  PhuseKey in AtendServer");


        ////                return eSec.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.Consol:
        ////                ed.WriteMessage("\nConsol\n");
        ////                Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eConsol.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eConsol.XCode;
        ////                    if (!Atend.Base.Equipment.EConsol.DeleteX(_Localtransaction, _Localconnection, eConsol.XCode))
        ////                    {
        ////                        throw new Exception("while delete  Consol in AtendServer");
        ////                    }
        ////                    eConsol.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eConsol = Atend.Base.Equipment.EConsol.SelectByCode(ServerCode);
        ////                    eConsol.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                eConsol.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eConsol.OperationList = GetOperationListForServer(OperationTbl, eConsol.XCode);

        ////                if (!eConsol.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Consol in AtendServer");


        ////                return eConsol.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.PhusePole:
        ////                ed.WriteMessage("\nPhusePole\n");
        ////                Atend.Base.Equipment.EPhusePole ePhusePole = Atend.Base.Equipment.EPhusePole.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePhusePole.XCode != Guid.Empty)
        ////                {
        ////                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                    DelXCode = ePhusePole.XCode;

        ////                    if (!Atend.Base.Equipment.EPhusePole.DeleteX(_Localtransaction, _Localconnection, ePhusePole.XCode))
        ////                    {
        ////                        throw new Exception("while delete PhusePole in AtendServer");
        ////                    }

        ////                    ePhusePole.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ePhusePole = Atend.Base.Equipment.EPhusePole.SelectByCode(ServerCode);
        ////                    ePhusePole.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                ePhusePole.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                ePhusePole.OperationList = GetOperationListForServer(OperationTbl, ePhusePole.XCode);

        ////                if (!ePhusePole.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert PhusePole in AtendServer");

        ////                return ePhusePole.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.MiddleJackPanel:
        ////                ed.WriteMessage("\nMiddleJackPanel\n");

        ////                int del = 0;
        ////                Atend.Base.Equipment.EJAckPanel eJAckPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eJAckPanel.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eJAckPanel.XCode;
        ////                    if (!Atend.Base.Equipment.EJAckPanel.DeleteX(_Localtransaction, _Localconnection, eJAckPanel.XCode))
        ////                    {
        ////                        throw new Exception("while delete JAckPanel in AtendServer");
        ////                    }
        ////                    eJAckPanel.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eJAckPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(ServerCode);
        ////                    eJAckPanel.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                Atend.Base.Equipment.EBus bus = Atend.Base.Equipment.EBus.SelectByCode(_Localtransaction, _Localconnection, eJAckPanel.MasterProductCode);
        ////                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        ////                Guid busDeleted = Guid.NewGuid();
        ////                if (bus.XCode != Guid.Empty)
        ////                {
        ////                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                    busDeleted = bus.XCode;

        ////                    if (!Atend.Base.Equipment.EBus.DeleteX(_Localtransaction, _Localconnection, bus.XCode))
        ////                    {
        ////                        throw new Exception("while delete bus in AtendServer");
        ////                    }

        ////                    bus.ServerSelectByCode(eJAckPanel.MasterProductCode);
        ////                }
        ////                else
        ////                {
        ////                    bus = Atend.Base.Equipment.EBus.SelectByCode(eJAckPanel.MasterProductCode);
        ////                    bus.XCode = busDeleted;
        ////                    busDeleted = Guid.Empty;
        ////                }

        ////                bus.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                bus.OperationList = GetOperationListForServer(OperationTbl, bus.XCode);

        ////                if (!bus.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert bus in AtendServer");


        ////                eJAckPanel.MasterProductXCode = bus.XCode;


        ////                DataTable JPCTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelCode(ServerCode);

        ////                foreach (DataRow JPCRow in JPCTbl.Rows)
        ////                {

        ////                    Atend.Base.Equipment.ECell cell11 = Atend.Base.Equipment.ECell.SelectByCode(_Localtransaction, _Localconnection, Convert.ToInt32(JPCRow["ProductCode"].ToString()));

        ////                    //DelXCode = cell11.XCode;
        ////                    if (!Atend.Base.Equipment.ECell.DeleteX(_Localtransaction, _Localconnection, cell11.XCode))
        ////                        throw new Exception("while delete cell in AtendServer");

        ////                    Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByCode(Convert.ToInt32(JPCRow["ProductCode"].ToString()));

        ////                    cell.OperationList = new ArrayList();
        ////                    OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                    cell.OperationList = GetOperationListForServer(OperationTbl, cell.XCode);

        ////                    if (!cell.InsertX(_Localtransaction, _Localconnection))
        ////                        throw new Exception("while insert cell in AtendServer");

        ////                    ///////////////////////

        ////                    Atend.Base.Equipment.EJackPanelCell JackpCell = Atend.Base.Equipment.EJackPanelCell.SelectByCode(_Localconnection, _Localtransaction, Convert.ToInt32(JPCRow["Code"].ToString()));
        ////                    //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        ////                    Guid JPCDeleted = Guid.NewGuid();
        ////                    if (JackpCell.XCode != Guid.Empty)
        ////                    {
        ////                        //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                        JPCDeleted = JackpCell.XCode;

        ////                        if (!Atend.Base.Equipment.EJackPanelCell.DeleteX(_Localtransaction, _Localconnection, eJAckPanel.XCode))
        ////                        {
        ////                            throw new Exception("while delete JackpCell in AtendServer");
        ////                        }

        ////                        JackpCell.ServerSelectByCode(Convert.ToInt32(JPCRow["Code"].ToString()));
        ////                    }
        ////                    else
        ////                    {
        ////                        JackpCell = Atend.Base.Equipment.EJackPanelCell.SelectByCodeForServer(Convert.ToInt32(JPCRow["Code"].ToString()));
        ////                        JackpCell.XCode = JPCDeleted;
        ////                        JPCDeleted = Guid.Empty;
        ////                    }

        ////                    JackpCell.JackPanelXCode = eJAckPanel.XCode;
        ////                    JackpCell.ProductXCode = cell.XCode;


        ////                    if (!JackpCell.InsertXX(_Localtransaction, _Localconnection))
        ////                        throw new Exception("while insert Jack panel cell in AtendServer");


        ////                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JPCRow["ProductCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        ////                    if (Atend.Base.Design.NodeTransaction.SubProductsForServer(cell.Code, cell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, _Localtransaction, _Localconnection))
        ////                    {
        ////                        ed.WriteMessage("\n113\n");
        ////                    }
        ////                    else
        ////                    {
        ////                        ed.WriteMessage("\n114\n");
        ////                        throw new Exception("while Calling SubProducts in findequip");
        ////                    }
        ////                }

        ////                //DelXCode = del;
        ////                if (!eJAckPanel.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new JAckPanel in AtendServer");


        ////                return eJAckPanel.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.Khazan:
        ////                ed.WriteMessage("\nKhazan\n");
        ////                Atend.Base.Equipment.EKhazan eKhazan = Atend.Base.Equipment.EKhazan.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eKhazan.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eKhazan.XCode;
        ////                    if (!Atend.Base.Equipment.EKhazan.DeleteX(_Localtransaction, _Localconnection, eKhazan.XCode))
        ////                    {
        ////                        throw new Exception("while delete  Khazan in AtendServer");
        ////                    }
        ////                    eKhazan.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eKhazan = Atend.Base.Equipment.EKhazan.SelectByCode(ServerCode);
        ////                    eKhazan.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }


        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eKhazan.XCode);
        ////                eKhazan.OperationList = new ArrayList();
        ////                eKhazan.OperationList = GetOperationListForServer(OperationTbl, eKhazan.XCode);

        ////                if (!eKhazan.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Khazan in AtendServer");


        ////                return eKhazan.XCode;

        ////                break;

        ////            case Atend.Control.Enum.ProductType.GroundPost:
        ////                ed.WriteMessage("\nGround Post\n");

        ////                Atend.Base.Equipment.EGroundPost ePost = Atend.Base.Equipment.EGroundPost.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (ePost.XCode != Guid.Empty)
        ////                {
        ////                    ed.WriteMessage("\n111111\n");
        ////                    DelXCode = ePost.XCode;
        ////                    if (!Atend.Base.Equipment.EGroundPost.DeleteX(_Localtransaction, _Localconnection, ePost.XCode))
        ////                    {
        ////                        throw new Exception("while delete GroundPost in AtendServer");
        ////                    }
        ////                    ePost.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    ed.WriteMessage("ServerCode = " + ServerCode.ToString() + "\n");

        ////                    ePost = Atend.Base.Equipment.EGroundPost.SelectByCode(ServerCode);
        ////                    ed.WriteMessage("\njjjjj\n");
        ////                    ePost.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                ed.WriteMessage("\njjjjj\n");

        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, ePost.XCode);
        ////                ePost.OperationList = new ArrayList();
        ////                ePost.OperationList = GetOperationListForServer(OperationTbl, ePost.XCode);
        ////                ed.WriteMessage("\njjjjj\n");

        ////                if (!ePost.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new GroundPost in AtendServer");

        ////                ed.WriteMessage("\njjjjj\n");

        ////                return ePost.XCode;

        ////                break;


        ////            case Atend.Control.Enum.ProductType.AirPost:
        ////                ed.WriteMessage("\nAir Post\n");

        ////                Atend.Base.Equipment.EAirPost eAPost = Atend.Base.Equipment.EAirPost.SelectByCode(_Localtransaction, _Localconnection, ServerCode);
        ////                if (eAPost.XCode != Guid.Empty)
        ////                {
        ////                    DelXCode = eAPost.XCode;
        ////                    if (!Atend.Base.Equipment.EAirPost.DeleteX(_Localtransaction, _Localconnection, eAPost.XCode))
        ////                    {
        ////                        throw new Exception("while delete Air Post in AtendServer");
        ////                    }
        ////                    eAPost.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eAPost = Atend.Base.Equipment.EAirPost.SelectByCode(ServerCode);
        ////                    eAPost.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }
        ////                eAPost.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eAPost.OperationList = GetOperationListForServer(OperationTbl, eAPost.XCode);

        ////                if (!eAPost.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new Air Post in AtendServer");


        ////                return eAPost.XCode;
        ////                break;

        ////            case Atend.Control.Enum.ProductType.WeekJackPanel:
        ////                ed.WriteMessage("\nWeekJackPanel\n");

        ////                Atend.Base.Equipment.EJackPanelWeek eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(_Localtransaction, _Localconnection, ServerCode);

        ////                DataTable JPWCDelTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(_Localtransaction, _Localconnection, eJAckPanelW.XCode);

        ////                del = 0;
        ////                if (eJAckPanelW.XCode != Guid.Empty)
        ////                {

        ////                    DelXCode = eJAckPanelW.XCode;
        ////                    if (!Atend.Base.Equipment.EJackPanelWeek.DeleteX(_Localtransaction, _Localconnection, eJAckPanelW.XCode))
        ////                    {
        ////                        throw new Exception("while delete JAckPanel in AtendServer");
        ////                    }
        ////                    eJAckPanelW.ServerSelectByCode(ServerCode);
        ////                }
        ////                else
        ////                {
        ////                    eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(ServerCode);
        ////                    eJAckPanelW.XCode = DelXCode;
        ////                    DelXCode = Guid.Empty;
        ////                }

        ////                Atend.Base.Equipment.EAutoKey_3p Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(_Localtransaction, _Localconnection, eJAckPanelW.AutoKey3pCode);
        ////                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        ////                Guid AutoDeleted = Guid.NewGuid();
        ////                if (Auto.XCode != Guid.Empty)
        ////                {
        ////                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                    AutoDeleted = Auto.XCode;

        ////                    if (!Atend.Base.Equipment.EAutoKey_3p.DeleteX(_Localtransaction, _Localconnection, Auto.XCode))
        ////                    {
        ////                        throw new Exception("while delete Auto in AtendServer");
        ////                    }

        ////                    Auto.ServerSelectByCode(eJAckPanelW.AutoKey3pCode);
        ////                }
        ////                else
        ////                {
        ////                    Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(eJAckPanelW.AutoKey3pCode);
        ////                    Auto.XCode = AutoDeleted;
        ////                    AutoDeleted = Guid.Empty;
        ////                }
        ////                Auto.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                Auto.OperationList = GetOperationListForServer(OperationTbl, Auto.XCode);

        ////                if (!Auto.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert bus in AtendServer");


        ////                eJAckPanelW.AutoKey3pXCode = Auto.XCode;


        ////                DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(ServerCode);

        ////                foreach (DataRow JPWCRow in JPWCTbl.Rows)
        ////                {
        ////                    Atend.Base.Equipment.EJackPanelWeekCell JackpWCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByCodeForServer(Convert.ToInt32(JPWCRow["Code"].ToString()));

        ////                    DataRow[] Dr = JPWCDelTbl.Select("XCode = \'" + JPWCRow["XCode"].ToString() + "\'");
        ////                    if (Dr.Length > 0)
        ////                        JackpWCell.XCode = new Guid(JPWCRow["XCode"].ToString());
        ////                    else
        ////                        JackpWCell.XCode = Guid.NewGuid();

        ////                    JackpWCell.JackPanelWeekXCode = eJAckPanelW.XCode;

        ////                    if (!JackpWCell.InsertXX(_Localtransaction, _Localconnection))
        ////                        throw new Exception("while insert Jack panel cell in AtendServer");


        ////                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JPWCRow["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        ////                    if (Atend.Base.Design.NodeTransaction.SubProductsForServer(JackpWCell.Code, JackpWCell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, _Localtransaction, _Localconnection))
        ////                    {
        ////                        ed.WriteMessage("\n113\n");
        ////                    }
        ////                    else
        ////                    {
        ////                        ed.WriteMessage("\n114\n");
        ////                        throw new Exception("while Calling SubProducts in findequip");
        ////                    }
        ////                }
        ////                eJAckPanelW.OperationList = new ArrayList();
        ////                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCode(ServerCode);//, eDB.XCode);
        ////                eJAckPanelW.OperationList = GetOperationListForServer(OperationTbl, eJAckPanelW.XCode);

        ////                //DelXCode = del;
        ////                if (!eJAckPanelW.InsertX(_Localtransaction, _Localconnection))
        ////                    throw new Exception("while insert new JAckPanel in AtendServer");


        ////                return eJAckPanelW.XCode;

        ////                break;
        ////        }
        ////    }
        ////    catch (System.Exception ex1)
        ////    {
        ////        _Localtransaction.Rollback();
        ////        _Localconnection.Close();
        ////        ed.WriteMessage(string.Format("Error NodeTransaction.FindEquip : {0} \n", ex1.Message));
        ////        return Guid.Empty;
        ////    }

        ////    return Guid.Empty;

        ////}

        //public static bool XXSubProducts(int DeletedCode, int Code, Guid XCode, int LocalContainerCode, int Type, SqlTransaction ServerTransaction, SqlConnection ServerConnection, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        //{
        //    //if (LocalContainerCode < 1)
        //    //    return true;

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //ed.WriteMessage("ContainerCode = " + LocalContainerCode.ToString());

        //    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(LocalTransaction, LocalConnection, LocalContainerCode, Type);

        //    //ed.WriteMessage("\nProduct Count = " + productPackageTable.Rows.Count.ToString());


        //    //Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(ServerTransaction, ServerConnection, XCode, Type);
        //    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(ServerTransaction, ServerConnection, DeletedCode, Type);

        //    Atend.Base.Equipment.EContainerPackage containerPackage = new Atend.Base.Equipment.EContainerPackage();
        //    int containercode = 0;

        //    if (CP.Code != 0)
        //    {
        //        if (Atend.Base.Equipment.EContainerPackage.Delete(ServerTransaction, ServerConnection, DeletedCode, Type))
        //        {
        //            if ((Atend.Base.Equipment.EProductPackage.Delete(ServerTransaction, ServerConnection, CP.Code)))
        //            {

        //            }
        //        }
        //    }

        //    if (productPackageTable.Rows.Count > 0)
        //    {
        //        containerPackage.ContainerCode = Code;
        //        containerPackage.Type = Type;
        //        containerPackage.XCode = XCode;

        //        if (containerPackage.Insert(ServerTransaction, ServerConnection))
        //        {
        //            containercode = containerPackage.Code;
        //        }

        //    }


        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {
        //        // FindEquipment(ServerTransaction, ServerConnection, LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), new Guid(row1["XCode"].ToString()));


        //        //if (CP.XCode == Guid.Empty)
        //        //{
        //        //    containerPackage.ContainerCode = Code;// containerCode;
        //        //    containerPackage.XCode = XCode;
        //        //    containerPackage.Type = Type;
        //        //    if (containerPackage.Insert(ServerTransaction, ServerConnection))
        //        //    {
        //        //        containercode = containerPackage.Code;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    //if(!Atend.Base.Equipment.EProductPackage.Delete(ServerTransaction,ServerConnection,CP.Code))
        //        //    //    throw new Exception("while delete Products in AtendServer in Sub Product");

        //        //    containercode = CP.Code;
        //        //}



        //        ed.WriteMessage("\nBefor\n");
        //        int DelCode = 0;
        //        int ProdCode = FindEquipment(ServerTransaction, ServerConnection, LocalTransaction, LocalConnection, Convert.ToInt16(row1["TableType"].ToString()), new Guid(row1["XCode"].ToString()), ref DelCode);
        //        ed.WriteMessage("\nAfter\n");

        //        Atend.Base.Equipment.EProductPackage PPackage = new Atend.Base.Equipment.EProductPackage();
        //        PPackage.Count = Convert.ToInt32(row1["Count"].ToString());
        //        PPackage.ContainerPackageCode = containercode;//Convert.ToInt32(row1["ContainerPackageCode"].ToString());
        //        PPackage.ProductCode = ProdCode;
        //        PPackage.TableType = Convert.ToInt16(row1["TableType"].ToString());
        //        PPackage.XCode = new Guid(row1["XCode"].ToString());
        //        ed.WriteMessage("\nProduct XCode = " + PPackage.XCode.ToString());


        //        if (PPackage.Insert(ServerTransaction, ServerConnection))
        //        {
        //            Atend.Base.Equipment.EContainerPackage CPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(LocalTransaction, LocalConnection, PPackage.XCode, PPackage.TableType);
        //            SubProducts(DelCode, ProdCode, PPackage.XCode, CPackage.Code, CPackage.Type, ServerTransaction, ServerConnection, LocalTransaction, LocalConnection);
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }

        //    return true;
        //}

        //private static int XXFindEquipment(SqlTransaction _Servertransaction, SqlConnection _Serverconnection, SqlTransaction _Localtransaction, SqlConnection _Localconnection, int Type, Guid XCode, ref int Code)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable OperationTbl = new DataTable();
        //    Code = 0;

        //    try
        //    {
        //        switch ((Atend.Control.Enum.ProductType)Type)
        //        {

        //            case Atend.Control.Enum.ProductType.Pole:
        //                ed.WriteMessage("\nPOLE\n");

        //                Atend.Base.Equipment.EPole ePole = Atend.Base.Equipment.EPole.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);

        //                if (ePole.XCode != Guid.Empty)
        //                {
        //                    Code = ePole.Code;
        //                    if (!Atend.Base.Equipment.EPole.ServerDelete(_Servertransaction, _Serverconnection, ePole.Code))
        //                    {

        //                        throw new Exception("while delete pole in AtendServer");
        //                    }
        //                }


        //                ePole = Atend.Base.Equipment.EPole.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                ePole.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePole.XCode);

        //                ePole.OperationList = GetOperationList(OperationTbl);

        //                if (ePole.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePole.Code, Type))
        //                        if (!ePole.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update pole in AtendLocal");

        //                }
        //                else
        //                    throw new Exception("while insert new pole in AtendServer");

        //                return ePole.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.Conductor:
        //                ed.WriteMessage("\nConductor\n");

        //                Atend.Base.Equipment.EConductor eConductor = Atend.Base.Equipment.EConductor.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);

        //                if (eConductor.XCode != Guid.Empty)
        //                {
        //                    Code = eConductor.Code;
        //                    if (!Atend.Base.Equipment.EConductor.ServerDelete(_Servertransaction, _Serverconnection, eConductor.Code))
        //                    {
        //                        throw new Exception("while delete Conductor in AtendServer");
        //                    }
        //                }

        //                eConductor = Atend.Base.Equipment.EConductor.SelectByXCode(_Localtransaction, _Localconnection, XCode);

        //                eConductor.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eConductor.XCode);

        //                eConductor.OperationList = GetOperationList(OperationTbl);


        //                if (eConductor.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EConductorTip.Update(_Servertransaction, _Serverconnection, Code, eConductor.Code))
        //                    {
        //                        if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eConductor.Code, Type))
        //                            if (!eConductor.UpdateX(_Localtransaction, _Localconnection))
        //                                throw new Exception("while Update Conductor in AtendLocal");
        //                    }
        //                    else
        //                        throw new Exception("while Update Conductor in AtendServer");

        //                }
        //                else
        //                    throw new Exception("while insert new Conductor in AtendServer");

        //                return eConductor.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.Mafsal:
        //                ed.WriteMessage("\nMafsal\n");
        //                Atend.Base.Equipment.EMafsal eMafsal = Atend.Base.Equipment.EMafsal.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eMafsal.XCode != Guid.Empty)
        //                {
        //                    Code = eMafsal.Code;
        //                    if (!Atend.Base.Equipment.EMafsal.ServerDelete(_Servertransaction, _Serverconnection, eMafsal.Code))
        //                    {
        //                        throw new Exception("while delete Mafsal in AtendServer");
        //                    }
        //                }

        //                eMafsal = Atend.Base.Equipment.EMafsal.SelectByXCode(_Localtransaction, _Localconnection, XCode);

        //                eMafsal.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eMafsal.XCode);

        //                eMafsal.OperationList = GetOperationList(OperationTbl);

        //                if (eMafsal.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eMafsal.Code, Type))
        //                        if (!eMafsal.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update Mafsal in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Mafsal in AtendServer");

        //                return eMafsal.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.Floor:
        //                ed.WriteMessage("\nFloor\n");
        //                Atend.Base.Equipment.EFloor eFloor = Atend.Base.Equipment.EFloor.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eFloor.XCode != Guid.Empty)
        //                {
        //                    Code = eFloor.Code;
        //                    if (!Atend.Base.Equipment.EFloor.ServerDelete(_Servertransaction, _Serverconnection, eFloor.Code))
        //                    {
        //                        throw new Exception("while delete eFloor in AtendServer");
        //                    }
        //                }

        //                eFloor = Atend.Base.Equipment.EFloor.SelectByXCode(_Localtransaction, _Localconnection, XCode);

        //                eFloor.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eFloor.XCode);

        //                eFloor.OperationList = GetOperationList(OperationTbl);

        //                if (eFloor.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eFloor.Code, Type))
        //                        if (!eFloor.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update eFloor in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new eFloor in AtendServer");

        //                return eFloor.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.KablSho:
        //                ed.WriteMessage("\nKablSho\n");
        //                Atend.Base.Equipment.EKablsho eKablSho = Atend.Base.Equipment.EKablsho.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eKablSho.XCode != Guid.Empty)
        //                {
        //                    Code = eKablSho.Code;
        //                    if (!Atend.Base.Equipment.EKablsho.ServerDelete(_Servertransaction, _Serverconnection, eKablSho.Code))
        //                    {
        //                        throw new Exception("while delete KablSho in AtendServer");
        //                    }
        //                }

        //                eKablSho = Atend.Base.Equipment.EKablsho.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eKablSho.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eKablSho.XCode);

        //                eKablSho.OperationList = GetOperationList(OperationTbl);

        //                if (eKablSho.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eKablSho.Code, Type))
        //                        if (!eKablSho.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update KablSho in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new KablSho in AtendServer");

        //                return eKablSho.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.Kalamp:
        //                ed.WriteMessage("\nKalamp\n");
        //                Atend.Base.Equipment.EClamp eKalamp = Atend.Base.Equipment.EClamp.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eKalamp.XCode != Guid.Empty)
        //                {
        //                    Code = eKalamp.Code;
        //                    if (!Atend.Base.Equipment.EClamp.ServerDelete(_Servertransaction, _Serverconnection, eKalamp.Code))
        //                    {
        //                        throw new Exception("while delete eKalamp in AtendServer");
        //                    }
        //                }

        //                eKalamp = Atend.Base.Equipment.EClamp.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eKalamp.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eKalamp.XCode);

        //                eKalamp.OperationList = GetOperationList(OperationTbl);

        //                if (eKalamp.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eKalamp.Code, Type))
        //                        if (!eKalamp.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update eKalamp in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new eKalamp in AtendServer");

        //                return eKalamp.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.AuoKey3p:
        //                ed.WriteMessage("\nAutoKey3P\n");
        //                Atend.Base.Equipment.EAutoKey_3p eAuto = Atend.Base.Equipment.EAutoKey_3p.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eAuto.XCode != Guid.Empty)
        //                {
        //                    Code = eAuto.Code;
        //                    if (!Atend.Base.Equipment.EAutoKey_3p.ServerDelete(_Servertransaction, _Serverconnection, eAuto.Code))
        //                    {
        //                        throw new Exception("while delete AutoKey3P in AtendServer");
        //                    }
        //                }

        //                eAuto = Atend.Base.Equipment.EAutoKey_3p.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eAuto.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eAuto.XCode);

        //                eAuto.OperationList = GetOperationList(OperationTbl);

        //                if (eAuto.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EJackPanelWeek.Update(_Servertransaction, _Serverconnection, Code, eAuto.Code))
        //                        if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eAuto.Code, Type))
        //                            if (!eAuto.UpdateX(_Localtransaction, _Localconnection))
        //                                throw new Exception("while Update AutoKey3P in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new AutoKey3P in AtendServer");


        //                return eAuto.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.Breaker:
        //                ed.WriteMessage("\nBreaker\n");
        //                Atend.Base.Equipment.EBreaker eBreaker = Atend.Base.Equipment.EBreaker.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eBreaker.XCode != Guid.Empty)
        //                {
        //                    Code = eBreaker.Code;
        //                    if (!Atend.Base.Equipment.EBreaker.ServerDelete(_Servertransaction, _Serverconnection, eBreaker.Code))
        //                    {
        //                        throw new Exception("while delete Breaker in AtendServer");
        //                    }
        //                }

        //                eBreaker = Atend.Base.Equipment.EBreaker.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eBreaker.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eBreaker.XCode);

        //                eBreaker.OperationList = GetOperationList(OperationTbl);

        //                if (eBreaker.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eBreaker.Code, Type))
        //                        if (!eBreaker.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Breaker in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Breaker in AtendServer");


        //                return eBreaker.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.Cell:
        //                ed.WriteMessage("\nCell\n");
        //                Atend.Base.Equipment.ECell eCell = Atend.Base.Equipment.ECell.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eCell.XCode != Guid.Empty)
        //                {
        //                    Code = eCell.Code;
        //                    if (!Atend.Base.Equipment.ECell.ServerDelete(_Servertransaction, _Serverconnection, eCell.Code))
        //                    {
        //                        throw new Exception("while delete Breaker in AtendServer");
        //                    }
        //                }

        //                eCell = Atend.Base.Equipment.ECell.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eCell.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eCell.XCode);

        //                eCell.OperationList = GetOperationList(OperationTbl);

        //                if (eCell.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EJackPanelCell.Update(_Serverconnection, _Servertransaction, Code, eCell.Code))
        //                    {
        //                        if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eCell.Code, Type))
        //                            if (!eCell.UpdateX(_Localtransaction, _Localconnection))
        //                                throw new Exception("while update Breaker in AtendLocal");
        //                    }
        //                }
        //                else
        //                    throw new Exception("while insert new Breaker in AtendServer");


        //                return eCell.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Bus:
        //                ed.WriteMessage("\nBus\n");
        //                Atend.Base.Equipment.EBus eBus = Atend.Base.Equipment.EBus.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eBus.XCode != Guid.Empty)
        //                {
        //                    Code = eBus.Code;
        //                    if (!Atend.Base.Equipment.EBus.ServerDelete(_Servertransaction, _Serverconnection, eBus.Code))
        //                    {
        //                        throw new Exception("while delete Bus in AtendServer");
        //                    }
        //                }

        //                eBus = Atend.Base.Equipment.EBus.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eBus.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eBus.XCode);

        //                eBus.OperationList = GetOperationList(OperationTbl);

        //                if (eBus.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eBus.Code, Type))
        //                        if (!eBus.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Bus in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Bus in AtendServer");


        //                return eBus.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.MiniatureKey:
        //                ed.WriteMessage("\nMiniatureKey\n");
        //                Atend.Base.Equipment.EMiniatorKey eMiniatorKey = Atend.Base.Equipment.EMiniatorKey.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eMiniatorKey.XCode != Guid.Empty)
        //                {
        //                    Code = eMiniatorKey.Code;
        //                    if (!Atend.Base.Equipment.EMiniatorKey.ServerDelete(_Servertransaction, _Serverconnection, eMiniatorKey.Code))
        //                    {
        //                        throw new Exception("while delete eMiniatorKey in AtendServer");
        //                    }
        //                }

        //                eMiniatorKey = Atend.Base.Equipment.EMiniatorKey.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eMiniatorKey.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eMiniatorKey.XCode);

        //                eMiniatorKey.OperationList = GetOperationList(OperationTbl);

        //                if (eMiniatorKey.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eMiniatorKey.Code, Type))
        //                        if (!eMiniatorKey.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update eMiniatorKey in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new eMiniatorKey in AtendServer");


        //                return eMiniatorKey.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.CatOut:
        //                ed.WriteMessage("\nCatout\n");
        //                Atend.Base.Equipment.ECatOut eCatout = Atend.Base.Equipment.ECatOut.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eCatout.XCode != Guid.Empty)
        //                {
        //                    Code = eCatout.Code;
        //                    if (!Atend.Base.Equipment.ECatOut.ServerDelete(_Servertransaction, _Serverconnection, eCatout.Code))
        //                    {
        //                        throw new Exception("while delete Catout in AtendServer");
        //                    }
        //                }

        //                eCatout = Atend.Base.Equipment.ECatOut.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eCatout.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eCatout.XCode);

        //                eCatout.OperationList = GetOperationList(OperationTbl);

        //                if (eCatout.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eCatout.Code, Type))
        //                        if (!eCatout.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Catout in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Catout in AtendServer");

        //                return eCatout.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.GroundCabel:
        //                ed.WriteMessage("\nMiddleCabel\n");
        //                Atend.Base.Equipment.EGroundCabel eMiddleCabel = Atend.Base.Equipment.EGroundCabel.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eMiddleCabel.XCode != Guid.Empty)
        //                {
        //                    Code = eMiddleCabel.Code;
        //                    if (!Atend.Base.Equipment.EGroundCabel.ServerDelete(_Servertransaction, _Serverconnection, eMiddleCabel.Code))
        //                    {
        //                        throw new Exception("while delete eMiddleCabel in AtendServer");
        //                    }
        //                }

        //                eMiddleCabel = Atend.Base.Equipment.EGroundCabel.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eMiddleCabel.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eMiddleCabel.XCode);

        //                eMiddleCabel.OperationList = GetOperationList(OperationTbl);

        //                if (eMiddleCabel.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eMiddleCabel.Code, Type))
        //                        if (!eMiddleCabel.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update eMiddleCabel in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new eMiddleCabel in AtendServer");

        //                return eMiddleCabel.Code;
        //                break;



        //            case Atend.Control.Enum.ProductType.CT:
        //                ed.WriteMessage("\nCT\n");
        //                Atend.Base.Equipment.ECT eCT = Atend.Base.Equipment.ECT.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eCT.XCode != Guid.Empty)
        //                {
        //                    Code = eCT.Code;
        //                    if (!Atend.Base.Equipment.ECT.ServerDelete(_Servertransaction, _Serverconnection, eCT.Code))
        //                    {
        //                        throw new Exception("while delete CT in AtendServer");
        //                    }
        //                }

        //                eCT = Atend.Base.Equipment.ECT.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eCT.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eCT.XCode);

        //                eCT.OperationList = GetOperationList(OperationTbl);

        //                if (eCT.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eCT.Code, Type))
        //                        if (!eCT.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update CT in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new CT in AtendServer");


        //                return eCT.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.DB:
        //                ed.WriteMessage("\nDB\n");
        //                Atend.Base.Equipment.EDB eDB = Atend.Base.Equipment.EDB.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eDB.XCode != Guid.Empty)
        //                {
        //                    Code = eDB.Code;
        //                    if (!Atend.Base.Equipment.EDB.ServerDelete(_Servertransaction, _Serverconnection, eDB.Code))
        //                    {
        //                        throw new Exception("while delete StreetBox in AtendServer");
        //                    }
        //                }

        //                eDB = Atend.Base.Equipment.EDB.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eDB.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eDB.XCode);

        //                eDB.OperationList = GetOperationList(OperationTbl);

        //                DataTable STBPTbl = Atend.Base.Equipment.EDBPhuse.SelectByDBXCode(eDB.XCode, _Localtransaction, _Localconnection);

        //                if (eDB.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    ed.WriteMessage("\n333\n");

        //                    bool PhuseFlag = true;
        //                    foreach (DataRow Row in STBPTbl.Rows)
        //                    {
        //                        ed.WriteMessage("\n444\n");

        //                        if (Atend.Base.Equipment.EPhuse.ShareOnServer(_Servertransaction, _Serverconnection, _Localtransaction, _Localconnection, new Guid(Row["PhuseXCode"].ToString())))
        //                        {
        //                            Atend.Base.Equipment.EDBPhuse SPhuse = Atend.Base.Equipment.EDBPhuse.SelectByXCode(_Localtransaction, _Localconnection, new Guid(Row["XCode"].ToString()));
        //                            SPhuse.PhuseCode = Atend.Base.Equipment.EPhuse.ServerSelectByXCode(_Servertransaction, _Serverconnection, new Guid(Row["PhuseXCode"].ToString())).Code;
        //                            SPhuse.DBCode = eDB.Code;

        //                            if (SPhuse.Insert(_Servertransaction, _Serverconnection))
        //                            {
        //                                if (!SPhuse.LocalUpdateX(_Localtransaction, _Localconnection))
        //                                {
        //                                    throw new Exception("while updatelocal StreetBoxPhuse in AtendLocal");
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("while ShareOnServer StreetBoxPhuse in AtendServer");

        //                        }
        //                    }

        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eDB.Code, (int)Atend.Control.Enum.ProductType.DB))
        //                    {
        //                        if (!eDB.UpdateX(_Localtransaction, _Localconnection))
        //                        {
        //                            throw new Exception("while Update StreetBox in AtendLocal");

        //                        }

        //                    }
        //                    else
        //                    {
        //                        throw new Exception("while Update ProductPackage in AtendServer");
        //                    }


        //                }
        //                else
        //                {
        //                    throw new Exception("while Insert StreetBox in AtendServer");

        //                }
        //                return eDB.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.HeaderCabel:
        //                ed.WriteMessage("\nHeaderCabel\n");
        //                Atend.Base.Equipment.EHeaderCabel eHeader = Atend.Base.Equipment.EHeaderCabel.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eHeader.XCode != Guid.Empty)
        //                {
        //                    Code = eHeader.Code;
        //                    if (!Atend.Base.Equipment.EHeaderCabel.ServerDelete(_Servertransaction, _Serverconnection, eHeader.Code))
        //                    {
        //                        throw new Exception("while delete HeaderCabel in AtendServer");
        //                    }
        //                }

        //                eHeader = Atend.Base.Equipment.EHeaderCabel.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eHeader.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eHeader.XCode);

        //                eHeader.OperationList = GetOperationList(OperationTbl);

        //                if (eHeader.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eHeader.Code, Type))
        //                        if (!eHeader.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update HeaderCabel in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new HeaderCabel in AtendServer");


        //                return eHeader.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Disconnector:
        //                ed.WriteMessage("\nDisconnector\n");
        //                Atend.Base.Equipment.EDisconnector eDisconnector = Atend.Base.Equipment.EDisconnector.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eDisconnector.XCode != Guid.Empty)
        //                {
        //                    Code = eDisconnector.Code;
        //                    if (!Atend.Base.Equipment.EDisconnector.ServerDelete(_Servertransaction, _Serverconnection, eDisconnector.Code))
        //                    {
        //                        throw new Exception("while delete HeaderCabel in AtendServer");
        //                    }
        //                }

        //                eDisconnector = Atend.Base.Equipment.EDisconnector.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eDisconnector.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eDisconnector.XCode);

        //                eDisconnector.OperationList = GetOperationList(OperationTbl);

        //                if (eDisconnector.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eDisconnector.Code, Type))
        //                        if (!eDisconnector.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while Update HeaderCabel in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new HeaderCabel in AtendServer");


        //                return eDisconnector.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Rod:
        //                ed.WriteMessage("\nRod\n");
        //                Atend.Base.Equipment.ERod eRod = Atend.Base.Equipment.ERod.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eRod.XCode != Guid.Empty)
        //                {
        //                    Code = eRod.Code;
        //                    if (!Atend.Base.Equipment.ERod.ServerDelete(_Servertransaction, _Serverconnection, eRod.Code))
        //                    {
        //                        throw new Exception("while delete  Rod in AtendServer");
        //                    }
        //                }

        //                eRod = Atend.Base.Equipment.ERod.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eRod.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eRod.XCode);

        //                eRod.OperationList = GetOperationList(OperationTbl);

        //                if (eRod.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eRod.Code, Type))
        //                        if (!eRod.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Rod in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Rod in AtendServer");


        //                return eRod.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Countor:
        //                ed.WriteMessage("\nCountor\n");
        //                Atend.Base.Equipment.ECountor eCountor = Atend.Base.Equipment.ECountor.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eCountor.XCode != Guid.Empty)
        //                {
        //                    Code = eCountor.Code;
        //                    if (!Atend.Base.Equipment.ECountor.ServerDelete(_Servertransaction, _Serverconnection, eCountor.Code))
        //                    {
        //                        throw new Exception("while delete Countor in AtendServer");
        //                    }
        //                }

        //                eCountor = Atend.Base.Equipment.ECountor.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eCountor.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eCountor.XCode);

        //                eCountor.OperationList = GetOperationList(OperationTbl);

        //                if (eCountor.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eCountor.Code, Type))
        //                        if (!eCountor.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Countor in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Countor in AtendServer");


        //                return eCountor.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.PhotoCell:
        //                ed.WriteMessage("\nPhotoCell\n");
        //                Atend.Base.Equipment.EPhotoCell ePhotoCell = Atend.Base.Equipment.EPhotoCell.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePhotoCell.XCode != Guid.Empty)
        //                {
        //                    Code = ePhotoCell.Code;
        //                    if (!Atend.Base.Equipment.EPhotoCell.ServerDelete(_Servertransaction, _Serverconnection, ePhotoCell.Code))
        //                    {
        //                        throw new Exception("while delete PhotoCell in AtendServer");
        //                    }
        //                }

        //                ePhotoCell = Atend.Base.Equipment.EPhotoCell.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                ePhotoCell.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePhotoCell.XCode);

        //                ePhotoCell.OperationList = GetOperationList(OperationTbl);

        //                if (ePhotoCell.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePhotoCell.Code, Type))
        //                        if (!ePhotoCell.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update PhotoCell in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new PhotoCell in AtendServer");


        //                return ePhotoCell.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Phuse:
        //                ed.WriteMessage("\nPhuse\n");
        //                Atend.Base.Equipment.EPhuse ePhuse = Atend.Base.Equipment.EPhuse.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePhuse.XCode != Guid.Empty)
        //                {
        //                    Code = ePhuse.Code;
        //                    if (!Atend.Base.Equipment.EPhuse.ServerDelete(_Servertransaction, _Serverconnection, ePhuse.Code))
        //                    {
        //                        throw new Exception("while delete Phuse in AtendServer");
        //                    }
        //                }

        //                ePhuse = Atend.Base.Equipment.EPhuse.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                //Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(_Servertransaction, _Serverconnection, ePhuse.PhusePoleXCode);
        //                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() +"\n");

        //                //if (PHP.XCode == Guid.Empty)
        //                //{
        //                //    ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                //    PHP = Atend.Base.Equipment.EPhusePole.SelectByXCode(_Localtransaction , _Localconnection ,ePhuse.PhusePoleXCode);
        //                //    if (!PHP.Insert(_Servertransaction, _Serverconnection))
        //                //        throw new Exception("while insert PhusePole in AtendServer in Phuse Case");

        //                //}

        //                //ePhuse.PhusePoleCode = PHP.Code;


        //                Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(_Servertransaction, _Serverconnection, ePhuse.PhusePoleXCode);
        //                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //                int PhusePoleDeleted = 0;
        //                if (PHP.XCode != Guid.Empty)
        //                {
        //                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                    PhusePoleDeleted = PHP.Code;

        //                    if (!Atend.Base.Equipment.EPhusePole.ServerDelete(_Servertransaction, _Serverconnection, PHP.Code))
        //                    {
        //                        throw new Exception("while delete PhusePole in AtendServer");
        //                    }


        //                }

        //                PHP = Atend.Base.Equipment.EPhusePole.SelectByXCode(_Localtransaction, _Localconnection, ePhuse.PhusePoleXCode);
        //                PHP.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, PHP.XCode);

        //                PHP.OperationList = GetOperationList(OperationTbl);

        //                if (PHP.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, PhusePoleDeleted, PHP.Code, (int)Atend.Control.Enum.ProductType.PhusePole))
        //                    {
        //                        if (!PHP.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update PhusePole in AtendLocal");
        //                    }
        //                }
        //                else
        //                    throw new Exception("while insert PhusePole in AtendServer");


        //                ePhuse.PhusePoleCode = PHP.Code;
        //                ePhuse.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePhuse.XCode);

        //                ePhuse.OperationList = GetOperationList(OperationTbl);


        //                if (ePhuse.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePhuse.Code, Type))
        //                        if (Atend.Base.Equipment.EStreetBoxPhuse.ServerUpdate(_Servertransaction, _Serverconnection, Code, ePhuse.Code))
        //                            if (!ePhuse.UpdateX(_Localtransaction, _Localconnection))
        //                                throw new Exception("while Update Phuse in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Phuse in AtendServer");


        //                return ePhuse.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.StreetBox:
        //                ed.WriteMessage("\nStreetBox\n");
        //                Atend.Base.Equipment.EStreetBox eStreetBox = Atend.Base.Equipment.EStreetBox.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eStreetBox.XCode != Guid.Empty)
        //                {
        //                    Code = eStreetBox.Code;
        //                    if (!Atend.Base.Equipment.EStreetBox.ServerDelete(_Servertransaction, _Serverconnection, eStreetBox.Code))
        //                    {
        //                        throw new Exception("while delete StreetBox in AtendServer");
        //                    }
        //                }

        //                eStreetBox = Atend.Base.Equipment.EStreetBox.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eStreetBox.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eStreetBox.XCode);

        //                eStreetBox.OperationList = GetOperationList(OperationTbl);

        //                DataTable STBPTbl1 = Atend.Base.Equipment.EStreetBoxPhuse.SelectByStreetBoxXCode(eStreetBox.XCode, _Localtransaction, _Localconnection);

        //                if (eStreetBox.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    ed.WriteMessage("\n333\n");

        //                    bool PhuseFlag = true;
        //                    foreach (DataRow Row in STBPTbl1.Rows)
        //                    {
        //                        ed.WriteMessage("\n444\n");

        //                        if (Atend.Base.Equipment.EPhuse.ShareOnServer(_Servertransaction, _Serverconnection, _Localtransaction, _Localconnection, new Guid(Row["PhuseXCode"].ToString())))
        //                        {
        //                            Atend.Base.Equipment.EStreetBoxPhuse SPhuse = Atend.Base.Equipment.EStreetBoxPhuse.SelectByXCode(_Localtransaction, _Localconnection, new Guid(Row["XCode"].ToString()));
        //                            SPhuse.PhuseCode = Atend.Base.Equipment.EPhuse.ServerSelectByXCode(_Servertransaction, _Serverconnection, new Guid(Row["PhuseXCode"].ToString())).Code;
        //                            SPhuse.StreetBoxCode = eStreetBox.Code;

        //                            if (SPhuse.Insert(_Servertransaction, _Serverconnection))
        //                            {
        //                                if (!SPhuse.LocalUpdateX(_Localtransaction, _Localconnection))
        //                                {
        //                                    throw new Exception("while updatelocal StreetBoxPhuse in AtendLocal");
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("while ShareOnServer StreetBoxPhuse in AtendServer");

        //                        }
        //                    }

        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eStreetBox.Code, (int)Atend.Control.Enum.ProductType.StreetBox))
        //                    {
        //                        if (!eStreetBox.UpdateX(_Localtransaction, _Localconnection))
        //                        {
        //                            throw new Exception("while Update StreetBox in AtendLocal");

        //                        }

        //                    }
        //                    else
        //                    {
        //                        throw new Exception("while Update ProductPackage in AtendServer");
        //                    }


        //                }
        //                else
        //                {
        //                    throw new Exception("while Insert StreetBox in AtendServer");

        //                }
        //                return eStreetBox.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Transformer:
        //                ed.WriteMessage("\nTransformer\n");
        //                Atend.Base.Equipment.ETransformer eTransformer = Atend.Base.Equipment.ETransformer.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eTransformer.XCode != Guid.Empty)
        //                {
        //                    Code = eTransformer.Code;
        //                    if (!Atend.Base.Equipment.ETransformer.ServerDelete(_Servertransaction, _Serverconnection, eTransformer.Code))
        //                    {
        //                        throw new Exception("while delete Transformer in AtendServer");
        //                    }
        //                }

        //                eTransformer = Atend.Base.Equipment.ETransformer.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eTransformer.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eTransformer.XCode);

        //                eTransformer.OperationList = GetOperationList(OperationTbl);

        //                if (eTransformer.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eTransformer.Code, Type))
        //                        if (!eTransformer.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Transformer in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Transformer in AtendServer");


        //                return eTransformer.Code;
        //                break;


        //            case Atend.Control.Enum.ProductType.PT:
        //                ed.WriteMessage("\nPT\n");
        //                Atend.Base.Equipment.EPT ePT = Atend.Base.Equipment.EPT.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePT.XCode != Guid.Empty)
        //                {
        //                    Code = ePT.Code;
        //                    if (!Atend.Base.Equipment.EPT.ServerDelete(_Servertransaction, _Serverconnection, ePT.Code))
        //                    {
        //                        throw new Exception("while delete PT in AtendServer");
        //                    }
        //                }

        //                ePT = Atend.Base.Equipment.EPT.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                ePT.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePT.XCode);

        //                ePT.OperationList = GetOperationList(OperationTbl);

        //                if (ePT.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePT.Code, Type))
        //                        if (!ePT.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update PT in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new PT in AtendServer");


        //                return ePT.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Insulator:
        //                ed.WriteMessage("\nInsulator\n");
        //                Atend.Base.Equipment.EInsulator eInsulator = Atend.Base.Equipment.EInsulator.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eInsulator.XCode != Guid.Empty)
        //                {
        //                    Code = eInsulator.Code;
        //                    if (!Atend.Base.Equipment.EInsulator.ServerDelete(_Servertransaction, _Serverconnection, eInsulator.Code))
        //                    {
        //                        throw new Exception("while delete  Insulator in AtendServer");
        //                    }
        //                }

        //                eInsulator = Atend.Base.Equipment.EInsulator.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eInsulator.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eInsulator.XCode);

        //                eInsulator.OperationList = GetOperationList(OperationTbl);

        //                if (eInsulator.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eInsulator.Code, Type))
        //                        if (!eInsulator.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Insulator in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Insulator in AtendServer");


        //                return eInsulator.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.ReCloser:
        //                ed.WriteMessage("\nReCloser\n");
        //                Atend.Base.Equipment.EReCloser eReCloser = Atend.Base.Equipment.EReCloser.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eReCloser.XCode != Guid.Empty)
        //                {
        //                    Code = eReCloser.Code;
        //                    if (!Atend.Base.Equipment.EReCloser.ServerDelete(_Servertransaction, _Serverconnection, eReCloser.Code))
        //                    {
        //                        throw new Exception("while delete  ReCloser in AtendServer");
        //                    }
        //                }

        //                eReCloser = Atend.Base.Equipment.EReCloser.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eReCloser.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eReCloser.XCode);

        //                eReCloser.OperationList = GetOperationList(OperationTbl);

        //                if (eReCloser.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eReCloser.Code, Type))
        //                        if (!eReCloser.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update ReCloser in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new  ReCloser in AtendServer");


        //                return eReCloser.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.PhuseKey:
        //                ed.WriteMessage("\nPhuseKey\n");
        //                Atend.Base.Equipment.EPhuseKey ePhuseKey = Atend.Base.Equipment.EPhuseKey.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePhuseKey.XCode != Guid.Empty)
        //                {
        //                    Code = ePhuseKey.Code;
        //                    if (!Atend.Base.Equipment.EPhuseKey.ServerDelete(_Servertransaction, _Serverconnection, ePhuseKey.Code))
        //                    {
        //                        throw new Exception("while delete  PhuseKey in AtendServer");
        //                    }
        //                }

        //                ePhuseKey = Atend.Base.Equipment.EPhuseKey.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                ePhuseKey.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePhuseKey.XCode);

        //                ePhuseKey.OperationList = GetOperationList(OperationTbl);

        //                if (ePhuseKey.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePhuseKey.Code, Type))
        //                        if (!ePhuseKey.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while udpate PhuseKey in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new  PhuseKey in AtendServer");


        //                return ePhuseKey.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.Consol:
        //                ed.WriteMessage("\nConsol\n");
        //                Atend.Base.Equipment.EConsol eConsol = Atend.Base.Equipment.EConsol.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eConsol.XCode != Guid.Empty)
        //                {
        //                    Code = eConsol.Code;
        //                    if (!Atend.Base.Equipment.EConsol.ServerDelete(_Servertransaction, _Serverconnection, eConsol.Code))
        //                    {
        //                        throw new Exception("while delete  Consol in AtendServer");
        //                    }
        //                }

        //                eConsol = Atend.Base.Equipment.EConsol.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                eConsol.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eConsol.XCode);

        //                eConsol.OperationList = GetOperationList(OperationTbl);

        //                if (eConsol.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eConsol.Code, Type))
        //                        if (!eConsol.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Consol in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Consol in AtendServer");


        //                return eConsol.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.PhusePole:
        //                ed.WriteMessage("\nPhusePole\n");
        //                Atend.Base.Equipment.EPhusePole ePhusePole = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePhusePole.XCode != Guid.Empty)
        //                {
        //                    Code = ePhusePole.Code;
        //                    if (!Atend.Base.Equipment.EPhusePole.ServerDelete(_Servertransaction, _Serverconnection, ePhusePole.Code))
        //                    {
        //                        throw new Exception("while delete  PhusePole in AtendServer");
        //                    }
        //                }

        //                ePhusePole = Atend.Base.Equipment.EPhusePole.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                ePhusePole.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePhusePole.XCode);

        //                ePhusePole.OperationList = GetOperationList(OperationTbl);

        //                if (ePhusePole.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EPhuse.Update(_Servertransaction, _Serverconnection, ePhusePole.Code, Code))
        //                    {
        //                        if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePhusePole.Code, Type))
        //                            if (!ePhusePole.UpdateX(_Localtransaction, _Localconnection))
        //                                throw new Exception("while update PhusePole in AtendLocal");
        //                    }
        //                    else
        //                        throw new Exception("while update Phuse in AtendServer");

        //                }
        //                else
        //                    throw new Exception("while insert new PhusePole in AtendServer");


        //                return ePhusePole.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.MiddleJackPanel:
        //                ed.WriteMessage("\nMiddleJackPanel\n");

        //                int del = 0;
        //                Atend.Base.Equipment.EJAckPanel eJAckPanel = Atend.Base.Equipment.EJAckPanel.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eJAckPanel.XCode != Guid.Empty)
        //                {
        //                    del = eJAckPanel.Code;
        //                    if (!Atend.Base.Equipment.EJAckPanel.ServerDelete(_Servertransaction, _Serverconnection, eJAckPanel.Code))
        //                    {
        //                        throw new Exception("while delete JAckPanel in AtendServer");
        //                    }
        //                }

        //                eJAckPanel = Atend.Base.Equipment.EJAckPanel.SelectByXCode(_Localtransaction, _Localconnection, XCode);


        //                if (Atend.Base.Equipment.EBus.ShareOnServer(_Servertransaction, _Serverconnection, _Localtransaction, _Localconnection, eJAckPanel.MasterProductXCode))
        //                {
        //                    ed.WriteMessage("\n145n");

        //                    eJAckPanel.MasterProductCode = Atend.Base.Equipment.EBus.ServerSelectByXCode(_Servertransaction, _Serverconnection, eJAckPanel.MasterProductXCode).Code;
        //                    ed.WriteMessage("\n146\n");
        //                }
        //                else
        //                {
        //                    throw new Exception("while .... Share On Server at Bus Of JackPanel in AtendServer");
        //                }



        //                DataTable JPCTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelXCode(_Localconnection, _Localtransaction, XCode);

        //                foreach (DataRow JPCRow in JPCTbl.Rows)
        //                {

        //                    Atend.Base.Equipment.ECell cell11 = Atend.Base.Equipment.ECell.ServerSelectByXCode(_Servertransaction, _Serverconnection, new Guid(JPCRow["ProductXCode"].ToString()));

        //                    Code = cell11.Code;
        //                    if (!Atend.Base.Equipment.ECell.ServerDelete(_Servertransaction, _Serverconnection, cell11.Code))
        //                        throw new Exception("while delete cell in AtendServer");

        //                    Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByXCode(_Localtransaction, _Localconnection, new Guid(JPCRow["ProductXCode"].ToString()));

        //                    OperationTbl = new DataTable();
        //                    cell.OperationList = new ArrayList();
        //                    OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, cell.XCode);
        //                    cell.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //                    if (cell.Insert(_Servertransaction, _Serverconnection))
        //                    {
        //                        if (Atend.Base.Equipment.EJackPanelCell.Update(_Serverconnection, _Servertransaction, Code, cell.Code))
        //                        {
        //                            if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, cell.Code, (int)Atend.Control.Enum.ProductType.Cell))
        //                            {
        //                                if (!cell.UpdateX(_Localtransaction, _Localconnection))
        //                                    throw new Exception("while insert cell in AtendServer");
        //                            }
        //                            else
        //                                throw new Exception("while insert cell in AtendServer");
        //                        }
        //                        else
        //                            throw new Exception("while insert cell in AtendServer");

        //                    }
        //                    else
        //                        throw new Exception("while insert cell in AtendServer");

        //                    Atend.Base.Equipment.EJackPanelCell Jpc = new Atend.Base.Equipment.EJackPanelCell();
        //                    //Jpc.Code = 0;
        //                    Jpc.XCode = new Guid(JPCRow["XCode"].ToString());
        //                    Jpc.IsDefault = Convert.ToBoolean(JPCRow["IsDefault"].ToString());
        //                    Jpc.JackPanelXCode = XCode; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                    Jpc.Num = Convert.ToByte(JPCRow["CellNum"].ToString());
        //                    Jpc.ProductType = Convert.ToByte(JPCRow["ProductType"].ToString());
        //                    //Jpc.ProductXCode = new Guid(JPCRow["ProductXCode"].ToString());
        //                    Jpc.ProductCode = cell.Code;
        //                    //eJAckPanel.JackPanelCell.Add(Jpc);


        //                    if (!Jpc.Insert(_Servertransaction, _Serverconnection))
        //                        throw new Exception("while insert Jack panel cell in AtendServer");


        //                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_Localtransaction, _Localconnection, new Guid(JPCRow["ProductXCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        //                    if (Atend.Base.Design.NodeTransaction.SubProducts(Code, cell.Code, cell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, _Servertransaction, _Serverconnection, _Localtransaction, _Localconnection))
        //                    {
        //                        ed.WriteMessage("\n113\n");
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n114\n");
        //                        throw new Exception("while Calling SubProducts in findequip");
        //                    }
        //                }

        //                Code = del;

        //                eJAckPanel.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eJAckPanel.XCode);

        //                eJAckPanel.OperationList = GetOperationList(OperationTbl);

        //                if (eJAckPanel.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eJAckPanel.Code, Type))
        //                        if (!eJAckPanel.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update JAckPanel in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new JAckPanel in AtendServer");


        //                return eJAckPanel.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.Khazan:
        //                ed.WriteMessage("\nKhazan\n");
        //                Atend.Base.Equipment.EKhazan eKhazan = Atend.Base.Equipment.EKhazan.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eKhazan.XCode != Guid.Empty)
        //                {
        //                    Code = eKhazan.Code;
        //                    if (!Atend.Base.Equipment.EKhazan.ServerDelete(_Servertransaction, _Serverconnection, eKhazan.Code))
        //                    {
        //                        throw new Exception("while delete  Khazan in AtendServer");
        //                    }
        //                }

        //                eKhazan = Atend.Base.Equipment.EKhazan.SelectByXCode(_Localtransaction, _Localconnection, XCode);
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eKhazan.XCode);
        //                eKhazan.OperationList = new ArrayList();
        //                eKhazan.OperationList = GetOperationList(OperationTbl);

        //                if (eKhazan.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eKhazan.Code, Type))
        //                        if (!eKhazan.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Khazan in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Khazan in AtendServer");


        //                return eKhazan.Code;

        //                break;

        //            case Atend.Control.Enum.ProductType.GroundPost:
        //                ed.WriteMessage("\nGround Post\n");

        //                Atend.Base.Equipment.EGroundPost ePost = Atend.Base.Equipment.EGroundPost.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (ePost.XCode != Guid.Empty)
        //                {
        //                    Code = ePost.Code;
        //                    if (!Atend.Base.Equipment.EGroundPost.ServerDelete(_Servertransaction, _Serverconnection, ePost.Code))
        //                    {
        //                        throw new Exception("while delete GroundPost in AtendServer");
        //                    }
        //                }

        //                ePost = Atend.Base.Equipment.EGroundPost.SelectByXCode(_Localtransaction, _Localconnection, XCode);

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ePost.XCode);
        //                ePost.OperationList = new ArrayList();
        //                ePost.OperationList = GetOperationList(OperationTbl);

        //                if (ePost.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ePost.Code, Type))
        //                        if (!ePost.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update GroundPost in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new GroundPost in AtendServer");


        //                return ePost.Code;

        //                break;


        //            case Atend.Control.Enum.ProductType.AirPost:
        //                ed.WriteMessage("\nAir Post\n");

        //                Atend.Base.Equipment.EAirPost eAPost = Atend.Base.Equipment.EAirPost.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);
        //                if (eAPost.XCode != Guid.Empty)
        //                {
        //                    Code = eAPost.Code;
        //                    if (!Atend.Base.Equipment.EAirPost.ServerDelete(_Servertransaction, _Serverconnection, eAPost.Code))
        //                    {
        //                        throw new Exception("while delete Air Post in AtendServer");
        //                    }
        //                }

        //                eAPost = Atend.Base.Equipment.EAirPost.SelectByXCode(XCode, _Localtransaction, _Localconnection);

        //                eAPost.OperationList = new ArrayList();

        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, eAPost.XCode);

        //                eAPost.OperationList = GetOperationList(OperationTbl);



        //                if (eAPost.Insert(_Servertransaction, _Serverconnection))
        //                {

        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, eAPost.Code, Type))
        //                        if (!eAPost.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update Air Post in AtendLocal");
        //                }
        //                else
        //                    throw new Exception("while insert new Air Post in AtendServer");


        //                return eAPost.Code;
        //                break;

        //            case Atend.Control.Enum.ProductType.WeekJackPanel:
        //                ed.WriteMessage("\nWeekJackPanel\n");

        //                //DataTable JPWCell;
        //                Atend.Base.Equipment.EJackPanelWeek ewJAckPanel = Atend.Base.Equipment.EJackPanelWeek.ServerSelectByXCode(_Servertransaction, _Serverconnection, XCode);

        //                DataTable DeletedJPWCells = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(_Servertransaction, _Serverconnection, ewJAckPanel.Code);

        //                if (ewJAckPanel.XCode != Guid.Empty)
        //                {
        //                    Code = ewJAckPanel.Code;

        //                    if (!Atend.Base.Equipment.EJackPanelWeek.ServerDelete(_Servertransaction, _Serverconnection, ewJAckPanel.Code))
        //                    {
        //                        //if (!Atend.Base.Equipment.EJackPanelWeekCell.ServerDelete(_Servertransaction, _Serverconnection, ewJAckPanel.XCode))
        //                        //{
        //                        throw new Exception("while delete WeekJackPanel in AtendServer");

        //                        //}
        //                    }
        //                }

        //                ewJAckPanel = Atend.Base.Equipment.EJackPanelWeek.SelectByXCode(_Localtransaction, _Localconnection, XCode);

        //                if (Atend.Base.Equipment.EAutoKey_3p.ShareOnServer(_Servertransaction, _Serverconnection, _Localtransaction, _Localconnection, ewJAckPanel.AutoKey3pXCode))
        //                {
        //                    ed.WriteMessage("\n145n");

        //                    ewJAckPanel.AutoKey3pCode = Atend.Base.Equipment.EAutoKey_3p.ServerSelectByXCode(_Servertransaction, _Serverconnection, ewJAckPanel.AutoKey3pXCode).Code;
        //                    ed.WriteMessage("\n146\n");
        //                }
        //                else
        //                {
        //                    throw new Exception("while .... Share On Server at Autokey Of WeekJackPanel in AtendServer");
        //                }

        //                ewJAckPanel.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(_Localtransaction, _Localconnection, ewJAckPanel.XCode);

        //                ewJAckPanel.OperationList = GetOperationList(OperationTbl);


        //                if (ewJAckPanel.Insert(_Servertransaction, _Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(_Servertransaction, _Serverconnection, Code, ewJAckPanel.Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel))
        //                    {
        //                        if (!ewJAckPanel.UpdateX(_Localtransaction, _Localconnection))
        //                            throw new Exception("while update JAckPanelWeek in AtendLocal");
        //                    }
        //                }
        //                else
        //                    throw new Exception("while insert new JAckPanelWeek in AtendServer");


        //                DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(_Localtransaction, _Localconnection, XCode);

        //                foreach (DataRow JPWCRow in JPWCTbl.Rows)
        //                {
        //                    Atend.Base.Equipment.EJackPanelWeekCell Jpwc = new Atend.Base.Equipment.EJackPanelWeekCell();
        //                    Jpwc.Code = 0;
        //                    Jpwc.XCode = new Guid(JPWCRow["XCode"].ToString());
        //                    Jpwc.JackPanelWeekCode = Convert.ToInt32(JPWCRow["JackPanelCode"].ToString());
        //                    //Jpwc.JackPanelWeekXCode = XCode; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                    Jpwc.JackPanelWeekCode = ewJAckPanel.Code;

        //                    Jpwc.Num = Convert.ToByte(JPWCRow["Num"].ToString());

        //                    ewJAckPanel.CellList.Add(Jpwc);

        //                    if (!Jpwc.Insert(_Servertransaction, _Serverconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");
        //                        throw new Exception("while insert WeekJackPanelCell in AtendServer");
        //                    }

        //                    Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_Localtransaction, _Localconnection, Jpwc.XCode, (int)Atend.Control.Enum.ProductType.Cell);

        //                    DataRow[] DR = DeletedJPWCells.Select("XCode = \'" + Jpwc.XCode.ToString() + "\'");
        //                    int d = 0;

        //                    if (DR.Length > 0)
        //                        d = Convert.ToInt32(DR[0]["Code"].ToString());

        //                    if (Atend.Base.Design.NodeTransaction.SubProducts(d, Jpwc.Code, Jpwc.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, _Servertransaction, _Serverconnection, _Localtransaction, _Localconnection))
        //                    {
        //                        ed.WriteMessage("\n113\n");
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n114\n");
        //                        throw new Exception("while Calling SubProducts in findequip");
        //                    }
        //                }

        //                return ewJAckPanel.Code;

        //                break;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        _Servertransaction.Rollback();
        //        _Serverconnection.Close();
        //        ed.WriteMessage(string.Format("Error NodeTransaction.FindEquip : {0} \n", ex1.Message));
        //        return 0;
        //    }

        //    return 0;

        //}

        //public static ArrayList XXGetOperationList(DataTable OperationTbl)
        //{
        //    Editor edl = Application.DocumentManager.MdiActiveDocument.Editor;

        //    //edl.WriteMessage("\n OP Count = " + OperationTbl.Rows.Count.ToString() + "\n");
        //    ArrayList OperationList = new ArrayList();
        //    foreach (DataRow OPRow in OperationTbl.Rows)
        //    {
        //        Atend.Base.Equipment.EOperation OP = new Atend.Base.Equipment.EOperation();
        //        //OP.ProductCode = Code;// Convert.ToInt32(OPRow["ProductCode"].ToString());
        //        OP.ProductID = Convert.ToInt32(OPRow["ProductID"].ToString());
        //        OP.Type = Convert.ToInt32(OPRow["Type"].ToString());

        //        //edl.WriteMessage("XCode OP = " + OPRow["XCode"].ToString() + "\n");

        //        OP.XCode = new Guid(OPRow["XCode"].ToString());
        //        OP.Count = Convert.ToDouble(OPRow["Count"].ToString());
        //        OperationList.Add(OP);
        //    }

        //    return OperationList;
        //}

        //public static ArrayList XXGetOperationListForServer(DataTable OperationTbl, Guid XCode)
        //{
        //    Editor edl = Application.DocumentManager.MdiActiveDocument.Editor;

        //    edl.WriteMessage("\n OP Count = " + OperationTbl.Rows.Count.ToString() + "\n");
        //    ArrayList OperationList = new ArrayList();
        //    foreach (DataRow OPRow in OperationTbl.Rows)
        //    {
        //        Atend.Base.Equipment.EOperation OP = new Atend.Base.Equipment.EOperation();
        //        OP.ProductCode = Convert.ToInt32(OPRow["ProductCode"].ToString());
        //        OP.ProductID = Convert.ToInt32(OPRow["ProductID"].ToString());
        //        OP.Type = Convert.ToInt32(OPRow["Type"].ToString());
        //        OP.Count = Convert.ToDouble(OPRow["Count"].ToString());
        //        //edl.WriteMessage("XCode OP = " + OPRow["XCode"].ToString() + "\n");

        //        OP.XCode = XCode;
        //        OperationList.Add(OP);
        //    }

        //    return OperationList;
        //}

        //public static bool SubProduct(int ContainerCode, int Type, Guid ParentCode, SqlTransaction Transaction, SqlConnection Connection)
        //{
        //    Editor edl = Application.DocumentManager.MdiActiveDocument.Editor;

        //    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerCode, Type);
        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {

        //        DPackage packageTemp = new DPackage();
        //        packageTemp.Count = Convert.ToInt32(row1["Count"]);
        //        packageTemp.ParentCode = ParentCode;
        //        packageTemp.Type = Convert.ToInt32(row1["TableType"]);
        //        packageTemp.ProductCode = Convert.ToInt32(row1["ProductCode"].ToString());

        //        if (packageTemp.AccessInsert(Transaction, Connection))
        //        {
        //            edl.WriteMessage("\n200\n");
        //            SubProduct(Convert.ToInt32(row1["ProductCode"]), Convert.ToInt32(row1["TableType"]), packageTemp.Code, Transaction, Connection);

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //private static bool DeleteSubEquipment(Guid ParentCode, SqlTransaction Transaction, SqlConnection Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    DataTable subEquipment = DPackage.SelectByParentCode(ParentCode, Transaction, Connection);
        //    if (subEquipment.Rows.Count == 0)
        //    {

        //        if (DPackage.Delete(new Guid(arraylist[arraylist.Count - 1].ToString()), Transaction, Connection))
        //        {
        //            arraylist.RemoveAt(arraylist.Count - 1);
        //            return true;
        //        }
        //        else
        //            return false;

        //    }
        //    else
        //    {
        //        foreach (DataRow dr in subEquipment.Rows)
        //        {
        //            arraylist.Add(dr["Code"]);
        //            DeleteSubEquipment(new Guid(dr["Code"].ToString()), Transaction, Connection);

        //        }
        //    }
        //    return true;

        //    /*
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage("i am in DeleteSubEquipment parentCode : {0} \n", ParentCode);
        //    ed.WriteMessage("###\n");

        //    DataTable subEquipment = DPackage.SelectByParentCode(ParentCode, Transaction, Connection);
        //    ed.WriteMessage(string.Format("Number of found row : {0} \n", subEquipment.Rows.Count));

        //    if (subEquipment.Rows.Count == 0)
        //    {

        //        ed.WriteMessage("@@@\n");
        //        if (DPackage.Delete(ParentCode, Transaction, Connection))
        //        {
        //            ed.WriteMessage("$$$\n");
        //            if (AllSubEquipmentsDoneSuccessed)
        //            {
        //                AllSubEquipmentsDoneSuccessed = true;
        //            }
        //            else
        //            {
        //                AllSubEquipmentsDoneSuccessed = false;
        //            }
        //        }
        //        else
        //        {
        //            AllSubEquipmentsDoneSuccessed = false;
        //        }
        //    }
        //    else
        //    {                
        //        foreach (DataRow dr in subEquipment.Rows)
        //        {
        //            DeleteSubEquipment(new Guid(dr["Code"].ToString()), Transaction, Connection);

        //        }
        //    }
        //    return AllSubEquipmentsDoneSuccessed;
        //     */
        //}

        //public static bool DeleteForEdit()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    bool canContinue = true;
        //    ed.WriteMessage("i am in NodeTransaction.DeleteForEdit \n");

        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();

        //        try
        //        {
        //            Atend.Base.Design.DNode dNode = Atend.Base.Design.DNode.SelectByDesignCodeAndCode((int)Atend.Control.Common.SelectedDesignCode, Atend.Control.Common.SelectedAutocadObjectGuid);
        //            ed.WriteMessage("Deleted node code is " + dNode.Code + "  \n");

        //            long GroupCode = Atend.Base.Design.DEquipmentGroup.SelectByEquipmentCode(dNode.Code);
        //            ed.WriteMessage("Selected GroupCode is : " + GroupCode + "\n");


        //            if (DPoleInfo.Delete(dNode.Code, Transaction, Connection))
        //            {
        //                DPackage dPackage = DPackage.SelectByNodeCodeType(dNode.Code, (int)Atend.Control.Enum.ProductType.Pole);

        //                if (DPackage.Delete(dPackage.Code, Transaction, Connection))
        //                {
        //                    //Transaction.Commit();
        //                    //Transaction=Connection.BeginTransaction();
        //                    ed.WriteMessage("i want to go to DeleteSubEquipment \n");
        //                    if (!DeleteSubEquipment(dPackage.Code, Transaction, Connection))
        //                    {
        //                        throw new Exception("while iterate through sub equipments");
        //                    }
        //                    canContinue = true;
        //                }
        //                else
        //                {
        //                    canContinue = false;
        //                    throw new Exception("while deleting dpackage pole rocord only");
        //                }

        //                //if (GroupCode != -1 && canContinue)
        //                //{
        //                //    ed.WriteMessage("i am going to dequiomentgroup delete \n");
        //                //    if (Atend.Base.Design.DEquipmentGroup.delete(dNode.Code, GroupCode, Transaction, Connection))
        //                //    {
        //                //        ed.WriteMessage("All Information Which Belongs to Node Removed");
        //                //    }
        //                //    else
        //                //    {
        //                //        throw new Exception("while delete DEquipmentGroup from data base");
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    ed.WriteMessage("Selected Object Does Not have any Group Code \n");
        //                //}
        //            }
        //            else
        //            {
        //                throw new Exception("while deleting dpole info");
        //            }


        //            Transaction.Commit();
        //            Connection.Close();
        //            return true;

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.DeleteForEdit.Transaction : {0} \n", ex1.Message));
        //            Transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.DeleteForEdit.Transaction : {0} \n", ex.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~~~//

        //EXTRA
        //public bool AccessInsertPole()
        //{
        //    Atend.Base.Acad.AcadGlobal.dConsolCode.Clear();
        //    ed.WriteMessage("~~ACSPATH:{0}\n",Atend.Control.Common.AccessPathLocal);
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPathLocal);
        //    //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    OleDbTransaction Transaction;
        //    try
        //    {
        //        Connection.Open();
        //        Transaction = Connection.BeginTransaction();
        //        try
        //        {

        //            ed.WriteMessage("~~~~~~~ Start Save Pole Information ~~~~~~~~\n");

        //            //DDesignSetting dDesignSetting = new DDesignSetting();
        //            //dDesignSetting.DesignCode = Atend.Control.Common.SelectedDesignCode;
        //            //dDesignSetting.Type = Atend.Control.Enum.DesignSettingType.LastNodeNumber;
        //            //dDesignSetting.Value = Atend.Control.Common.NCounter;
        //            //if (dDesignSetting.Update(Transaction, Connection))
        //            //{
        //            //ed.WriteMessage(string.Format("I am going to insert dnode \n"));
        //            //Atend.Control.Common.NCounter++;
        //            Atend.Base.Acad.AcadGlobal.dNode.Number = "N"; //string.Format("N{0}", Atend.Control.Common.NCounter);
        //            Comment = Atend.Base.Acad.AcadGlobal.dNode.Number;
        //            if (Atend.Base.Acad.AcadGlobal.dNode.AccessInsert(Transaction, Connection))
        //            {
        //                //ed.WriteMessage(string.Format("I am going to insert poleinfo \n"));
        //                Atend.Base.Acad.AcadGlobal.dPoleInfo.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                //ed.WriteMessage(string.Format("dnode code : {0} \n",MyDNode.Code));
        //                if (Atend.Base.Acad.AcadGlobal.dPoleInfo.AcessInsert(Transaction, Connection))
        //                {
        //                    //ed.WriteMessage(string.Format("I am going to insert package \n"));
        //                    Atend.Base.Acad.AcadGlobal.dPackage.NodeCode = Atend.Base.Acad.AcadGlobal.dNode.Code;
        //                    //myDPackage.ParentCode = new Guid();
        //                    if (Atend.Base.Acad.AcadGlobal.dPackage.AccessInsert(Transaction, Connection))
        //                    {
        //                        //ed.WriteMessage("Pole record inserted to dpackage\n");
        //                        //Done Successfully
        //                        //ed.WriteMessage(string.Format("Going to packages iteration. \n"));
        //                        //ed.WriteMessage("number of rows in MyDPackages: " + MyDPackages.Count.ToString() + "\n");
        //                        for (int i = 0; i < Atend.Base.Acad.AcadGlobal.dPackages.Count; i++)
        //                        {
        //                            DPackage tempPackage = (DPackage)Atend.Base.Acad.AcadGlobal.dPackages[i];
        //                            //ed.WriteMessage(string.Format("MyDPackages.type : {0}\n", tempPackage.Type));
        //                            //PromptIntegerResult r31 = ed.GetInteger("sss :");
        //                            tempPackage.ParentCode = Atend.Base.Acad.AcadGlobal.dPackage.Code;
        //                            //ed.WriteMessage(string.Format("I am going to packag insert \n"));
        //                            if (tempPackage.AccessInsert(Transaction, Connection))
        //                            {
        //                                //ed.WriteMessage(string.Format(">>> code: {0} nodecode :{1} parentcode: {2} type:{3} productcode: {4} \n", tempPackage.Code, tempPackage.NodeCode, tempPackage.ParentCode, tempPackage.Type, tempPackage.ProductCode));
        //                                //PromptIntegerResult r2 = ed.GetInteger("dPackage inserted:");
        //                                //save sub equips
        //                                if (tempPackage.Type == (int)Atend.Control.Enum.ProductType.Consol)
        //                                {
        //                                    ed.WriteMessage("TepmPackage.Code= " + tempPackage.Code.ToString() + "\n");
        //                                    Atend.Base.Acad.AcadGlobal.dConsolCode.Add(tempPackage.Code);
        //                                    //PromptIntegerResult r3 = ed.GetInteger("type is consol :");
        //                                    //ed.WriteMessage(string.Format("package was a consol and go to sub product \n"));
        //                                    //ed.WriteMessage("Go to SubProducts. \n");
        //                                    if (AccessSubProducts(tempPackage.ProductCode, tempPackage.Type, tempPackage.Code, Transaction, Connection))
        //                                    {
        //                                        ed.WriteMessage(string.Format("All Information for Pole saved successfully \n"));
        //                                    }
        //                                    else
        //                                    {
        //                                        //ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole sub package information  \n"));
        //                                        throw new Exception("while saving pole sub package information");
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //ed.WriteMessage(string.Format("Error NodeTransaction.InsertPole : while saving pole package information  \n"));
        //                                throw new Exception("while saving pole package information");
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPackage for pole information \n");
        //                        throw new Exception("while saving DPackage information");
        //                    }
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DPoleInfo information \n");
        //                    throw new Exception("while saving DPoleInfo information");
        //                }
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("Error NodeTransaction.InsertPole : while saving DNode information \n");
        //                throw new Exception("while saving DNode information");
        //            }
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("Error NodeTransaction.DesignSettngUpdate : while updating DesignSetting information \n");
        //            //    throw new Exception("while saving DNode information");
        //            //}

        //            ed.WriteMessage("~~~~~~~End Save Pole Information ~~~~~~~~~\n");


        //        }
        //        catch (System.Exception ex2)
        //        {
        //            ed.WriteMessage(string.Format("Error NodeTransaction.Insert1  : {0} \n", ex2.Message));
        //            Transaction.Rollback();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error NodeTransaction.Insert2  : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //    Transaction.Commit();
        //    Connection.Close();
        //    return true;
        //}

        //public bool XXAccessSubProducts(int ContainerCode, int Type, Guid ParentCode, OleDbTransaction Transaction, OleDbConnection Connection)
        //{

        //    DataTable productPackageTable = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerCode, Type);
        //    foreach (DataRow row1 in productPackageTable.Rows)
        //    {
        //        DPackage packageTemp = new DPackage();
        //        packageTemp.Count = Convert.ToInt32(row1["Count"]);
        //        packageTemp.ParentCode = ParentCode;
        //        packageTemp.Type = Convert.ToInt32(row1["TableType"]);
        //        packageTemp.ProductCode = Convert.ToInt32(row1["ProductCode"].ToString());

        //        if (packageTemp.AccessInsert(Transaction, Connection))
        //        {

        //            AccessSubProducts(Convert.ToInt32(row1["ProductCode"]), Convert.ToInt32(row1["TableType"]), packageTemp.Code, Transaction, Connection);

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //frmSaveDesignServer
        public bool CreateTransactionAndConnection(out SqlTransaction sTransaction, out SqlConnection sConnection, out OleDbTransaction aTransaction, out OleDbConnection aConnection, bool OpenConnectionForLocal)
        {
            sConnection = new SqlConnection();
            if (OpenConnectionForLocal)
            {
                sConnection.ConnectionString = Atend.Control.ConnectionString.LocalcnString;
            }
            else
            {
                sConnection.ConnectionString = Atend.Control.ConnectionString.ServercnString;
            }
            aConnection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            sTransaction = null;
            aTransaction = null;

            try
            {
                sConnection.Open();
                aConnection.Open();

                try
                {
                    sTransaction = sConnection.BeginTransaction();
                    aTransaction = aConnection.BeginTransaction();
                }
                catch
                {

                    sTransaction.Rollback();
                    sConnection.Close();

                    aTransaction.Rollback();
                    aConnection.Close();
                    return false;
                }
            }
            catch
            {
                sConnection.Close();
                aConnection.Close();
                return false;
            }


            return true;
        }

    }
}
