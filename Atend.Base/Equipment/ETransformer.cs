using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Equipment
{
    public class ETransformer
    {

        public ETransformer()
        { }

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

        private double capaciy;
        public double Capaciy
        {
            get { return capaciy; }
            set { capaciy = value; }
        }

        private double volt;
        public double Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int typeCode;
        public int TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
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

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server PArt~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new SqlParameter("iCode", 0));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Transaction = _transaction;

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                //connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        ed.WriteMessage("Error For Insert \n");
                    }
                    Counter++;
                }

                if (canCommitTransaction)
                {
                    //transaction.Commit();
                    //Connection.Close();
                    return true;
                }
                else
                {
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.InsertX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Transformer);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in Transform");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Transformer, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.ServerInsert : {0} \n", ex1.Message));
                return false;
            }

        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Transformer_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Transformer))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Transformer, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in Transform");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in transformer");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Transformer, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }

        }

        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format("Error Transformer.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer)))
                {
                    canCommitTransaction = true;

                }
                else
                {
                    canCommitTransaction = false;
                }

                if (canCommitTransaction)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transformer.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static ETransformer SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            reader.Close();
            connection.Close();
            return transformer;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                //transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                //transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                Volt = Convert.ToDouble(reader["Volt"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            reader.Close();
            connection.Close();
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Transformer_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Transformer_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        public static ETransformer SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Transformer_SelectByProductCode", connection);
            ETransformer transform = new ETransformer();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    transform.code = Convert.ToInt16(dataReader["Code"].ToString());
                    transform.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                    transform.Capaciy = Convert.ToDouble(dataReader["Capacity"].ToString());
                    transform.Comment = dataReader["Capacity"].ToString();
                    transform.Volt = Convert.ToDouble(dataReader["Volt"].ToString());
                    transform.Name = dataReader["Name"].ToString();
                    transform.TypeCode = Convert.ToInt32(dataReader["TypeCode"]);
                }
                else
                {
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));
                }
                dataReader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ETransform.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return transform;
        }

        //ASHKTORAB
        public static ETransformer ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //connection.Close();
            return transformer;
        }

        //MEDHAT //ShareOnServer
        public static ETransformer ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();

            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                transformer.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : transformer\n");
            }
            reader.Close();
            return transformer;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Transformer_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.writeMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                        if (containerPackage.InsertX(transaction, Connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    Counter = 0;
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
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
                    //****************

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error ETransformere.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ETransformer.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            //////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error Transformer.InsertX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            command.Transaction = _transaction;

            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);
                    //_EOperation.ProductID = 
                    if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    Counter++;
                }



                if (canCommitTransaction)
                {
                    //transaction.Commit();
                    //Connection.Close();
                    return true;
                }
                else
                {
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }
                //Connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ETransformer.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            /////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error Transformer.InsertX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Transformer_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Transformer, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in Transform");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Transformer, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Transformer_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Transformer))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Transformer, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in Transform");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in transformer");
                    }
                }
                ed.WriteMessage("ETransformer.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Transformer, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }

        }


        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Transformer_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                _transaction = Connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer);

                            if (_EOperation.InsertX(_transaction, Connection) && canCommitTransaction)
                            {
                                canCommitTransaction = true;
                            }
                            else
                            {
                                canCommitTransaction = false;
                            }
                            Counter++;
                        }
                    }

                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(_transaction, Connection) && canCommitTransaction)
                            {
                                canCommitTransaction = true;
                            }
                            else
                            {
                                canCommitTransaction = false;

                            }
                            Counter++;
                        }
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //*************
                    if (canCommitTransaction)
                    {
                        _transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        _transaction.Rollback();
                        Connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }


                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error ETransformer.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ETransformer.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }



            ////////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error Transformer.UpdateX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capaciy));
            command.Parameters.Add(new SqlParameter("iVolt", Volt));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ETransformer.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            //////////////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error Transformer.UpdateX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.Transformer);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Transformer_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer))) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error ETransformer.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ETransformer.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }



            ////////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error Transformer.DeleteX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer)))
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
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error ETransformer.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ETransformer.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            ///////////////////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error Transformer.DeleteX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //frmDrawGroundPost //SentFromLocalToAccess
        public static ETransformer SelectByXCode(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Transformer_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", transformer.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Transformer));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
            }

            reader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", transformer.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Transformer));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeys.Add(Op);

            }

            reader.Close();
            connection.Close();
            return transformer;
        }

        //ShareOnServer
        public static ETransformer SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Transformer_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlTransaction trans = LocalTransaction;
            command.Parameters.Add(new SqlParameter("iXCode", Code));
            ETransformer transformer = new ETransformer();

            try
            {
                command.Transaction = trans;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                    transformer.XCode = new Guid(reader["XCode"].ToString());
                    transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                    transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                    transformer.Comment = reader["Comment"].ToString();
                    transformer.Name = reader["Name"].ToString();
                    transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                    transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    transformer.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ETransformer.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return transformer;
        }

        public static ETransformer SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", transformer.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Transformer));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
            }

            reader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", transformer.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Transformer));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            //connection.Close();
            return transformer;
        }

        //ASHKTORAB
        public static ETransformer SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return transformer;
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Transformer_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Transformer_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Transformer_SearchByName", connection);
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

        //MEDHAT
        public static ETransformer CheckForExist(double _Capacity, double _Vol, int _TypeCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Transformer_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCapacity", _Capacity));
            command.Parameters.Add(new SqlParameter("iVolt", _Vol));
            command.Parameters.Add(new SqlParameter("iTypeCode", _TypeCode));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                transformer.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            connection.Close();
            return transformer;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new OleDbParameter("iCode", 0));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iCapacity", Capaciy));
            command.Parameters.Add(new OleDbParameter("iVolt", Volt));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));

            try
            {
                connection.Open();
                code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iCapacity", Capaciy));
            command.Parameters.Add(new OleDbParameter("iVolt", Volt));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Transformer);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Transformer, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.AccessInsert : {0} \n", ex1.Message));
                return false;
            }

        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Transformer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iCapacity", Capaciy));
            command.Parameters.Add(new OleDbParameter("iVolt", Volt));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));
            int OldCode = Code;
            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Transformer);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Transformer, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Transformer failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Transformer.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //frmDrawGroundPost //StatusReport 
        public static ETransformer AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            connection.Close();
            return transformer;
        }


        public static ETransformer AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            return transformer;
        }

        public static ETransformer AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return transformer;
        }

        //AcDrawGroundPost
        public static ETransformer AccessSelectByCode(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Transformer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt32(reader["Code"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            else
            {
                transformer.Code = -1;
            }
            reader.Close();
            return transformer;
        }

        public static ETransformer AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Transformer_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ETransformer transformer = new ETransformer();
            if (reader.Read())
            {
                transformer.Code = Convert.ToInt16(reader["Code"].ToString());
                transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
                transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
                transformer.Comment = reader["Comment"].ToString();
                transformer.Name = reader["Name"].ToString();
                transformer.XCode = new Guid(reader["XCode"].ToString());
                transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
            }
            reader.Close();
            connection.Close();
            return transformer;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ETransformer AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Transformer_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ETransformer transformer = new ETransformer();
        //    if (reader.Read())
        //    {
        //        transformer.Code = Convert.ToInt16(reader["Code"].ToString());
        //        transformer.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        transformer.Capaciy = Convert.ToDouble(reader["Capacity"].ToString());
        //        transformer.Volt = Convert.ToDouble(reader["Volt"].ToString());
        //        transformer.Comment = reader["Comment"].ToString();
        //        transformer.Name = reader["Name"].ToString();
        //        transformer.XCode = new Guid(reader["XCode"].ToString());
        //        transformer.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
        //    }
        //    else
        //    {
        //        transformer.Code = -1;
        //    }
        //    reader.Close();
        //    return transformer;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Transformer_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Transformer_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsTransformer = new DataSet();
            adapter.Fill(dsTransformer);
            return dsTransformer.Tables[0];
        }

        public static ETransformer AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Transformer_SelectByProductCode", connection);
            ETransformer transform = new ETransformer();
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                OleDbDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    transform.code = Convert.ToInt16(dataReader["Code"].ToString());
                    transform.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                    transform.Capaciy = Convert.ToDouble(dataReader["Capacity"].ToString());
                    transform.Comment = dataReader["Capacity"].ToString();
                    transform.Volt = Convert.ToDouble(dataReader["Volt"].ToString());
                    transform.Name = dataReader["Name"].ToString();
                    transform.TypeCode = Convert.ToInt32(dataReader["TypeCode"]);
                }
                else
                {
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));
                }
                dataReader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ETransform.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return transform;
        }

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
        //        ETransformer _ETransformer = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
        //            ed.WriteMessage("\n111\n");

        //            ETransformer Ap = Atend.Base.Equipment.ETransformer.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ETransformer.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _ETransformer.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _ETransformer.XCode);
        //            _ETransformer.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_ETransformer.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _ETransformer.Code, (int)Atend.Control.Enum.ProductType.Transformer))
        //                {
        //                    if (!_ETransformer.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n112\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
        //                    }
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }


        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ETransformer.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Transformer, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ETransformer.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
        //            ed.WriteMessage("\n111\n");

        //            ETransformer Ap = Atend.Base.Equipment.ETransformer.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ETransformer.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.ETransformer.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Transformer));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Transformer, Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
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
        //        ed.WriteMessage(string.Format(" ERROR ETransformer.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
