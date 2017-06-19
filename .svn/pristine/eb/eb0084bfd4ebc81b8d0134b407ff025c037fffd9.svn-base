using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EGroundPost
    {
        public EGroundPost()
        {

        }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private double capacity;
        public double Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        private int cellCount;
        public int CellCount
        {
            get { return cellCount; }
            set { cellCount = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        private byte groundType;
        public byte GroundType
        {
            get { return groundType; }
            set { groundType = value; }
        }

        private byte advanceType;
        public byte AdvanceType
        {
            get { return advanceType; }
            set { advanceType = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private byte[] image;
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeKeysX = new ArrayList();

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private int insertRowCode;
        public int InsertRowCode
        {
            get { return insertRowCode; }
            set { insertRowCode = value; }
        }

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        private ArrayList cellList;
        public ArrayList CellList
        {
            get { return cellList; }
            set { cellList = value; }
        }


        public static DataTable groundPostSubEquip = new DataTable();

        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();


        //~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //command.Parameters.Add(new SqlParameter("iOperationCode", OperationCode));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EGroundPost.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //ASHKTORAB 
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction; ;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            //command.Parameters.Add(new SqlParameter("iOperationCode", OperationCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    InsertRowCode = Convert.ToInt32(command.ExecuteScalar());
                    Code = InsertRowCode;
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    //ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");

                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");
                    //Operation

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    //**********
                    Counter = 0;


                    //if (canCommitTransaction)
                    //{
                    //    ed.WriteMessage("\n112\n");
                    //    ed.WriteMessage("\n" + XCode + "\n");

                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                    //    if (containerPackage.InsertX(transaction, connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    ed.WriteMessage("j\n");
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;

                    //    if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    //if (_EGroundPostCell.Insert(transaction, connection) && canCommitTransaction)
                    //    //{
                    //    //    canCommitTransaction = true;

                    //    //}
                    //    //else
                    //    //{
                    //    //    canCommitTransaction = false;
                    //    //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                    //    //}
                    //    Counter++;
                    //}

                    if (canCommitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.InsertX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundPost, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in Groundpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundPost, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundPost failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundPost_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.GroundPost))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundPost, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in Groundpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in GroundPost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundPost, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundPost failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }
        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));


            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    //operation
                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //****
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (EGroundPostCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;

                    //    }
                    //}

                    if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))) & canComitTransaction)
                    {

                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    if (canComitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }


                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.Delete: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    //operation
                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //****
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    //DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));


                    if ((EContainerPackage.Delete(transaction, connection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))) & canComitTransaction)
                    {

                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //ed.WriteMessage("\n\n\n Container GroudPost Code = " + containerPackage.Code.ToString() + "\n\n\n");
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    if (canComitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                    }


                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.ServerDelete: {0} \n", ex1.Message));

                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.ServerDelete: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EGroundPost SelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~\n");
            //groundPostSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("productCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //dc.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc3);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                //groundPost.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            reader.Close();
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~\n");

            //command.Parameters.Clear();
            //command.CommandText = "E_PostGroundCell_SelectByContainerCodeType";
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //ed.WriteMessage("groundPost.code:" + groundPost.code + "Type:" + Atend.Control.Enum.ProductType.GroundPost + "\n");
            //reader = command.ExecuteReader();
            //groundPostSubEquip.Rows.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ed.WriteMessage("True\n");
            //    DataRow dr = groundPostSubEquip.NewRow();
            //    dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    groundPostSubEquip.Rows.Add(dr);

            //}

            //reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iCode", groundPost.code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new SqlParameter("iProductCode", groundPost.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~\n");

            reader.Close();
            connection.Close();
            return groundPost;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("~~~~~~~~~~~~~~~~~\n");
            //groundPostSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("productCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //dc.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc3);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                //groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                GroundType = Convert.ToByte(reader["GroundType"].ToString());
                Name = reader["Name"].ToString();

                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_PostGroundCell_SelectByContainerCodeType";
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //ed.WriteMessage("groundPost.code:" + groundPost.code + "Type:" + Atend.Control.Enum.ProductType.GroundPost + "\n");
            //reader = command.ExecuteReader();
            //groundPostSubEquip.Rows.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ed.WriteMessage("True\n");
            //    DataRow dr = groundPostSubEquip.NewRow();
            //    dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    groundPostSubEquip.Rows.Add(dr);

            //}

            //reader.Close();

            connection.Close();

        }

        public static EGroundPost SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EGroundPost groundPost = new EGroundPost();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    groundPost.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                    //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                    groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                    groundPost.Comment = reader["Comment"].ToString();
                    groundPost.Image = (byte[])(reader["Image"]);
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EGroundPost.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return groundPost;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundPost_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
        }

        public static DataTable DrawSearch(double Capacity)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundPost_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCellCount", cellCount));

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];
        }

        //ASHKTORAB
        public static EGroundPost ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //connection.Close();
            return groundPost;
        }

        //MEDHAT //ShareOnServer
        public static EGroundPost ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = ServerTransaction;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();

            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : ground post\n");
            }
            reader.Close();
            return groundPost;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~Local PArt~~~~~~~~~~~~~~~~~~~//

        //GroundPost
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //command.Parameters.Add(new SqlParameter("iOperationCode", OperationCode));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    InsertRowCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    //ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");
                    //Operation
                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    //**********
                    Counter = 0;

                    //ed.WriteMessage("\n111\n");

                    if (canCommitTransaction)
                    {
                        //ed.WriteMessage("\n112\n");
                        //ed.WriteMessage("\n" + XCode + "\n");

                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                        if (containerPackage.InsertX(transaction, connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        //ed.WriteMessage("j\n");
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;

                        if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                        //if (_EGroundPostCell.Insert(transaction, connection) && canCommitTransaction)
                        //{
                        //    canCommitTransaction = true;

                        //}
                        //else
                        //{
                        //    canCommitTransaction = false;
                        //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                        //}
                        Counter++;
                    }

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.InsertX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction; ;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("\nName = " + Name + "\n");

            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));


            //command.Parameters.Add(new SqlParameter("iOperationCode", OperationCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    InsertRowCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    //ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");

                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");
                    //Operation

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = 0;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    //**********
                    Counter = 0;


                    //if (canCommitTransaction)
                    //{
                    //    ed.WriteMessage("\n112\n");
                    //    ed.WriteMessage("\n" + XCode + "\n");

                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                    //    if (containerPackage.InsertX(transaction, connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    ed.WriteMessage("j\n");
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;

                    //    if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    //if (_EGroundPostCell.Insert(transaction, connection) && canCommitTransaction)
                    //    //{
                    //    //    canCommitTransaction = true;

                    //    //}
                    //    //else
                    //    //{
                    //    //    canCommitTransaction = false;
                    //    //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                    //    //}
                    //    Counter++;
                    //}

                    if (canCommitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.InsertX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundPost_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundPost, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in Groundpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundPost, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundPost failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundPost_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.GroundPost))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundPost, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in Groundpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in GroundPost");
                    }
                }
                ed.WriteMessage("EGroundPost.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundPost, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundPost failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Update", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //command.Parameters.Add(new SqlParameter("ioperationCode", OperationCode));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                int Counter = 0;
                try
                {
                    command.ExecuteNonQuery();

                    //Operation
                    Counter = 0;
                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {

                        while (canComitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.ProductCode = code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);

                            if (_EOperation.Insert(transaction, connection) && canComitTransaction)
                            {
                                canComitTransaction = true;
                            }
                            else
                            {
                                canComitTransaction = false;
                            }
                            Counter++;
                        }
                    }
                    //******
                    Counter = 0;
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));




                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (EGroundPostCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;

                    //    }
                    //}
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    while (canComitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        if (_EProductPackage.Insert(transaction, connection) && canComitTransaction)
                        {
                            canComitTransaction = true;
                        }
                        else
                        {
                            canComitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                        //if (_EGroundPostCell.Insert(transaction, connection) && canComitTransaction)
                        //{
                        //    canComitTransaction = true;

                        //}
                        //else
                        //{
                        //    canComitTransaction = false;
                        //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                        //}
                        Counter++;
                    }




                    if (canComitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }

                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.Update: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }


        }

        //ground post
        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));



            //command.Parameters.Add(new SqlParameter("ioperationCode", OperationCode));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                int Counter = 0;
                try
                {
                    command.ExecuteNonQuery();

                    //Operation
                    Counter = 0;
                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {

                        while (canComitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;// code;
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);

                            if (_EOperation.InsertX(transaction, connection) && canComitTransaction)
                            {
                                canComitTransaction = true;
                            }
                            else
                            {
                                canComitTransaction = false;
                            }
                            Counter++;
                        }
                    }
                    //******
                    Counter = 0;
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

                    //DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    //////////////////>>>>>>>>>>>>>DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerXCodeType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));




                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (EGroundPostCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;

                    //    }
                    //}
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    while (canComitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        if (_EProductPackage.InsertX(transaction, connection) && canComitTransaction)
                        {
                            canComitTransaction = true;
                        }
                        else
                        {
                            canComitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                        //if (_EGroundPostCell.Insert(transaction, connection) && canComitTransaction)
                        //{
                        //    canComitTransaction = true;

                        //}
                        //else
                        //{
                        //    canComitTransaction = false;
                        //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                        //}
                        Counter++;
                    }




                    if (canComitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }

                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.UpdateX: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.UpdateX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }


        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iGroundType", GroundType));
            command.Parameters.Add(new SqlParameter("iAdvanceType", AdvanceType));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //command.Parameters.Add(new SqlParameter("ioperationCode", OperationCode));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                int Counter = 0;
                try
                {
                    command.ExecuteNonQuery();

                    //Operation
                    Counter = 0;
                    //if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    //{

                    //    while (canComitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        //_EOperation.ProductCode = 0;// code;
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);

                    //        if (_EOperation.InsertX(transaction, connection) && canComitTransaction)
                    //        {
                    //            canComitTransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canComitTransaction = false;
                    //        }
                    //        Counter++;
                    //    }
                    //}
                    ////******
                    //Counter = 0;
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    //DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));




                    ////foreach (DataRow dr in dt.Rows)
                    ////{
                    ////    if (EGroundPostCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    ////    {
                    ////        canComitTransaction = true;

                    ////    }
                    ////    else
                    ////    {
                    ////        canComitTransaction = false;

                    ////    }
                    ////}
                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    //{
                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;
                    //}
                    //while (canComitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    //Atend.Base.Equipment.EGroundPostCell _EGroundPostCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    //_EGroundPostCell = ((Atend.Base.Equipment.EGroundPostCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.InsertX(transaction, connection) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;
                    //        ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EGroundPostCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    //if (_EGroundPostCell.Insert(transaction, connection) && canComitTransaction)
                    //    //{
                    //    //    canComitTransaction = true;

                    //    //}
                    //    //else
                    //    //{
                    //    //    canComitTransaction = false;
                    //    //    ed.WriteMessage("Error For _EGroundPostCell.Insert \n");
                    //    //}
                    //    Counter++;
                    //}




                    if (canComitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.UpdateX: {0} \n", ex1.Message));

                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.UpdateX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }


        }

        //frmGroundPost
        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundPost);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }



            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    //operation
                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //****



                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    //DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (EGroundPostCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;

                    //    }
                    //}

                    if ((EContainerPackage.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))) & canComitTransaction)
                    {

                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    if (canComitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }


                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.DeleteX: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.DeleteX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    //operation
                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost)))
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //****
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_transaction, _connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

                    DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));


                    if ((EContainerPackage.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost))) & canComitTransaction)
                    {

                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;
                    }
                    //ed.WriteMessage("\n\n\n Container GroudPost Code = " + containerPackage.Code.ToString() + "\n\n\n");
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    {
                        canComitTransaction = true;
                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    if (canComitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                    }


                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGroundPost.DeleteX: {0} \n", ex1.Message));

                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EGroundPost.DeleteX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //frmDrawGroundPost
        public static EGroundPost SelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //groundPostSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("productCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //dc.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc3);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                groundPost.Image = (byte[])(reader["Image"]);
                //ed.WriteMessage("groundPost.Image:{0}\n", groundPost.Image.Length);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_PostGroundCell_SelectByContainerCodeType";
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //ed.WriteMessage("groundPost.code:" + groundPost.code + "Type:" + Atend.Control.Enum.ProductType.GroundPost + "\n");
            //reader = command.ExecuteReader();
            //groundPostSubEquip.Rows.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ed.WriteMessage("True\n");
            //    DataRow dr = groundPostSubEquip.NewRow();
            //    dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    groundPostSubEquip.Rows.Add(dr);
            //}
            //reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeysX.Add(Op);
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            connection.Close();
            return groundPost;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EGroundPost SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
            reader.Close();
            connection.Close();
            return groundPost;
        }

        //ShareOnServer
        public static EGroundPost SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EGroundPost groundPost = new EGroundPost();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                    groundPost.XCode = new Guid(reader["XCode"].ToString());
                    groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                    //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                    groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                    groundPost.Comment = reader["Comment"].ToString();
                    groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                    groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                    groundPost.Name = reader["Name"].ToString();
                    groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    groundPost.Image = (byte[])(reader["Image"]);
                    //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
                }
                else
                {
                    groundPost.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorchain.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return groundPost;
        }

        public static EGroundPost SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", groundPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //connection.Close();
            return groundPost;
        }

        //ASHKTORAB
        public static EGroundPost SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                groundPost.Image = (byte[])(reader["Image"]);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
            reader.Close();

            //connection.Close();
            return groundPost;
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundPost_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundPost_SearchByName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iName", Name));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                connection.Close();
                return true;
            }
            else
            {
                reader.Close();
                connection.Close();
                return false;
            }

        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_GroundPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iCellCount", CellCount));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_GroundPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundPost);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in Groundpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.GroundPost, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess GroundPost failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }
        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection ;
            OleDbCommand insertCommand = new OleDbCommand("E_GroundPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iGroundType", GroundType));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAdvanceType", AdvanceType));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));

            int oldCode = Code;
            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.GroundPost);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in Groundpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.GroundPost, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess GroundPost failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EGroundPost.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //frmDrawGroundPost //StatusReport //AcDrawGroundPostShield
        public static EGroundPost AccessSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //groundPostSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("productCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //dc.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc3);
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.Image = (byte[])(reader["Image"]);
                //ed.WriteMessage("groundPost.Image:{0}\n", groundPost.Image.Length);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", groundPost.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", groundPost.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            connection.Close();
            return groundPost;
        }

        public static EGroundPost AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
           
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.Image = (byte[])(reader["Image"]);
                //ed.WriteMessage("groundPost.Image:{0}\n", groundPost.Image.Length);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
          

            reader.Close();
            //connection.Close();
            return groundPost;
        }

        public static EGroundPost AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //groundPostSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("productCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //dc.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //groundPostSubEquip.Columns.Add(dc3);
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.Image = (byte[])(reader["Image"]);
                //ed.WriteMessage("groundPost.Image:{0}\n", groundPost.Image.Length);
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            else
            {
                groundPost.Code = -1;
            }
            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", groundPost.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("Count" + reader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());

            }

            reader.Close();


            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", groundPost.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //connection.Close();
            return groundPost;
        }


        public static EGroundPost AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy XCode");
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundPost groundPost = new EGroundPost();
            if (reader.Read())
            {
                groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
                groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                groundPost.Comment = reader["Comment"].ToString();
                groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
                groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
                groundPost.Name = reader["Name"].ToString();
                groundPost.XCode = new Guid(reader["XCode"].ToString());
                //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
            }
            reader.Close();
            connection.Close();
            return groundPost;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EGroundPost AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_GroundPost_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    //ed.WriteMessage("I Am In SelectBy XCode");
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EGroundPost groundPost = new EGroundPost();
        //    if (reader.Read())
        //    {
        //        groundPost.Code = Convert.ToInt32(reader["Code"].ToString());
        //        groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
        //        //groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
        //        groundPost.Type = Convert.ToByte(reader["Type"].ToString());
        //        groundPost.Comment = reader["Comment"].ToString();
        //        groundPost.AdvanceType = Convert.ToByte(reader["AdvanceType"].ToString());
        //        groundPost.GroundType = Convert.ToByte(reader["GroundType"].ToString());
        //        groundPost.Name = reader["Name"].ToString();
        //        groundPost.XCode = new Guid(reader["XCode"].ToString());
        //        //groundPost.OperationCode = Convert.ToInt32(reader["OperationCode"].ToString());
        //    }
        //    else
        //    {
        //        groundPost.Code = -1;
        //    }
        //    reader.Close();
        //    return groundPost;
        //}

        public static EGroundPost AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundPost_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EGroundPost groundPost = new EGroundPost();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    groundPost.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    groundPost.Capacity = Convert.ToSingle(reader["Capacity"].ToString());
                    groundPost.CellCount = Convert.ToInt16(reader["CellCount"].ToString());
                    groundPost.Type = Convert.ToByte(reader["Type"].ToString());
                    groundPost.Comment = reader["Comment"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EGroundPost.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return groundPost;
        }

        //frmDrawGroundPost
        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundPost_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
        }

        public static DataTable AccessDrawSearch(double Capacity, int cellCount, byte IsExistance)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundPost_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCellCount", cellCount));

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];
        }

        //frmDrawGroundPost 
        public static DataTable SelectAllAndMerge()
        {

            DataTable AccTbl = AccessSelectAll();
            DataTable SqlTbl = SelectAllX();

            DataTable MergeTbl = SqlTbl.Copy();
            DataColumn IsSql = new DataColumn("IsSql", typeof(bool));
            IsSql.DefaultValue = true;
            MergeTbl.Columns.Add(IsSql);

            foreach (DataRow Dr in AccTbl.Rows)
            {
                DataRow MergeRow = MergeTbl.NewRow();

                foreach (DataColumn Dc in AccTbl.Columns)
                {
                    MergeRow[Dc.ColumnName] = Dr[Dc.ColumnName];
                }

                MergeRow["IsSql"] = false;
                MergeRow["XCode"] = new Guid("00000000-0000-0000-0000-000000000000");
                MergeTbl.Rows.Add(MergeRow);
            }


            return MergeTbl;

        }

        //public static bool ShareOnServer(Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction;

        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    int DeletedCode = 0;

        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();

        //        EGroundPost groundPost = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));
        //            ed.WriteMessage("\n111\n");

        //            EGroundPost Ap = Atend.Base.Equipment.EGroundPost.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            ed.WriteMessage("\n222222222\n");

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                ed.WriteMessage("\n333333333333\n");
        //                if (!Atend.Base.Equipment.EGroundPost.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            //ed.WriteMessage("\n4444444444444\n");

        //            DataTable OperationTbl = new DataTable();
        //            groundPost.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, groundPost.XCode);
        //            groundPost.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            //ed.WriteMessage("\nImage = {0}\n" , groundPost.Image);

        //            if (groundPost.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, groundPost.Code, (int)Atend.Control.Enum.ProductType.GroundPost))
        //                {
        //                    if (!groundPost.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    ed.WriteMessage("\n114\n");

        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }


        //            ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, groundPost.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.GroundPost, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                Servertransaction.Commit();
        //                Serverconnection.Close();

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EGroundPost.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool GetFromServer(int Code)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    Guid DeletedXCode = Guid.NewGuid();

        //    try
        //    {

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        try
        //        {

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));
        //            //ed.WriteMessage("\n111\n");

        //            EGroundPost Ap = Atend.Base.Equipment.EGroundPost.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EGroundPost.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EGroundPost.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.GroundPost, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EGroundPost.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
