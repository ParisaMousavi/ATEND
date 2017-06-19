using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EInsulatorChain
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private double lenght;
        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_Insert", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_Insert", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                //Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
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
                return true;
            }
            catch (System.Exception ex1)
            {
                //Connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection  con = _connection;
            SqlCommand  insertCommand = new SqlCommand("E_InsulatorChain_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in InsulatorChain");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, Code, _transaction, _connection,LocalTransaction,LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorChain_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.InsulatorChain))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in InsulatorChain");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted  Operation Failed in InsulatorChain");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, Code, _transaction, _connection,LocalTransaction,LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Connection.Close();
                return false;

            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();

                    bool canCommitTransaction = true;
                    if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain)))
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
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                return false;
            }

            return true;
        }

        public static EInsulatorChain SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Comment = reader["Comment"].ToString();
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        public static EInsulatorChain SelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.ProductCode = Convert.ToInt32( reader["ProductCode"].ToString());
            }
            reader.Close();
            return insulator;
        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_Select", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Name = reader["Name"].ToString();
                Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                Comment = reader["Comment"].ToString();
                Code = Convert.ToInt32(reader["Code"].ToString());
                IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                XCode = new Guid(reader["XCode"].ToString());


            }
            reader.Close();

            Connection.Close();
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorChain_Select", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorChain_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static EInsulatorChain ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_SelectByXCode", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.productCode = Convert.ToInt32( reader["ProductCode"].ToString());

            }
            reader.Close();

            return insulator;
        }

        //MEDHAT //ShareOnServer
        public static EInsulatorChain ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_InsulatorChain_Select", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.productCode = Convert.ToInt32(reader["ProductCode"].ToString());

            }
            else
            {
                insulator.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : insulator chain\n");
            }
            reader.Close();
            return insulator;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));

            ed.WriteMessage("\n Comment = {0}\n", Comment);

            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain);
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
                    ed.WriteMessage(string.Format("Error EInsulatorChain.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EInsulatorChain.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            
            ////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

            sqlCommand.Transaction = transaction;
            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;
                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain);
                    //_EOperation.ProductID = 
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
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EInsulatorChain.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
            
            
            
            //try
            //{
            //    //Connection.Open();
            //    //transaction = Connection.BeginTransaction();
            //    sqlCommand.Transaction = transaction;
            //    try
            //    {
            //        sqlCommand.ExecuteNonQuery();
            //        return true;
            //    }
            //    catch (System.Exception ex1)
            //    {
            //        Connection.Close();
            //        return false;
            //    }

            //}
            //catch (System.Exception ex1)
            //{
            //    Connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorChain_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in InsulatorChain");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorChain_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction );
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in InsulatorChain");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted  Operation Failed in InsulatorChain");
                    }
                }
                ed.WriteMessage("EInsulatorChain.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Connection.Close();
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                Connection.Open();
                _transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain);

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
                    ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
                    ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                    ed.WriteMessage(string.Format("Error EInsulatorChain.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EInsulatorChain.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }
            
            
            //////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Connection.Close();
            //    return false;
            //}
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iLenght", Lenght));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Transaction = _transaction;
            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EInsulatorChain.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
            
            
            
            ////////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    //transaction = Connection.BeginTransaction();
            //    sqlCommand.Transaction = transaction;
            //    try
            //    {
            //        sqlCommand.ExecuteNonQuery();
            //        return true;
            //    }
            //    catch (System.Exception ex1)
            //    {
            //        //ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
            //        //Connection.Close();
            //        //transaction.Rollback();
            //        return false;
            //    }

            //}
            //catch (System.Exception ex1)
            //{
            //    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("\n");
            //    //ed.WriteMessage(string.Format(" ERROR EInsulator.Update: {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EInsulatorChain.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EInsulatorChain.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
            
            
            ////////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorChain_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain)))
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
                    ed.WriteMessage(string.Format("Error EInsulatorChain.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EInsulatorChain.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
            
            
            
            ///////////////////
            //try
            //{
            //    //Connection.Open();
            //    //transaction = Connection.BeginTransaction();
            //    sqlCommand.Transaction = transaction;
            //    try
            //    {
            //        sqlCommand.ExecuteNonQuery();
            //        return true;
            //    }


            //    catch (System.Exception ex1)
            //    {
            //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //        //ed.WriteMessage("Error In TransActionX:{0}\n", ex1.Message);
            //        //transaction.Rollback();
            //        //Connection.Close();
            //        return false;
            //    }
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        public static EInsulatorChain SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insp = new EInsulatorChain();
            if (reader.Read())
            {
                insp.Code = Convert.ToInt32(reader["Code"].ToString());
                insp.XCode = new Guid(reader["XCode"].ToString());
                insp.Name = reader["Name"].ToString();
                insp.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insp.Comment = reader["Comment"].ToString();
                insp.productCode = Convert.ToInt32(reader["ProductCode"].ToString());
            }
            else
            {
                insp.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));

            return insp;
        }

        public static bool GetFromBProductLocal()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable bp = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
            //ed.WriteMessage("bp.Count={0}\n", bp.Rows.Count);
            foreach (DataRow dr in bp.Rows)
            {
                EInsulatorChain IsnC = Atend.Base.Equipment.EInsulatorChain.SelectByProductCode(Convert.ToInt32(dr["ID"].ToString()));
                if (IsnC.Code != -1)
                {
                    //ed.WriteMessage("ramp.Name={0}\n", IsnC.Name);
                    IsnC.Name = dr["Name"].ToString();
                    //ed.WriteMessage("ProductCode={0}\n", dr["ID"].ToString());
                    IsnC.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    if (!IsnC.UpdateX())
                        return false;

                }
                else
                {
                    //ed.WriteMessage("InsertInsul. Pipe\n");
                    IsnC.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    IsnC.Name = dr["Name"].ToString();
                    IsnC.Comment = "";
                    if (!IsnC.InsertX())
                        return false;
                }
            }
            return true;

        }

        //SentFromLocalToAccess
        public static EInsulatorChain SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.productCode =Convert.ToInt32( reader["ProductCode"].ToString());
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.InsulatorChain));
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
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.InsulatorChain));
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
            Connection.Close();
            return insulator;
        }

        //ASHKTORAB //ShareOnServer
        public static EInsulatorChain SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_InsulatorChain_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EInsulatorChain insulator = new EInsulatorChain();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.XCode = new Guid(reader["XCode"].ToString());
                    insulator.Name = reader["Name"].ToString();
                    insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                    insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    insulator.Comment = reader["Comment"].ToString();
                    insulator.productCode = Convert.ToInt32(reader["ProductCode"].ToString());
                }
                reader.Close();
              
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorchain.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return insulator;
        }


        public static EInsulatorChain SelectByCodeForLocal(int Code, SqlTransaction  LocalTransaction, SqlConnection Localconnection)
        {
            SqlConnection Connection = Localconnection;
            SqlCommand command = new SqlCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            EInsulatorChain insulator = new EInsulatorChain();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.XCode = new Guid(reader["XCode"].ToString());
                    insulator.Name = reader["Name"].ToString();
                    insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                    insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    insulator.Comment = reader["Comment"].ToString();
                    insulator.productCode = Convert.ToInt32(reader["ProductCode"].ToString());
                }
                else
                {
                    insulator.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorchain.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return insulator;
        }


        public static EInsulatorChain SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.ProductCode = Convert.ToInt32( reader["ProductCode"].ToString());
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.InsulatorChain));
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
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.InsulatorChain));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            return insulator;
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorChain_SelectAll", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            //ed.WriteMessage("DT.Rows>count={0}\n",dsInsulator.Tables[0].Rows.Count);
            return dsInsulator.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorChain_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_InsulatorChain_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorChain_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                insertCommand.ExecuteNonQuery();
                return true;
            }

            catch (System.Exception ex)
            {
                //ed.WriteMessage(string.Format("Error ETransferInsulator.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorChain_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorChain_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            int OldCode = Code;
            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.InsulatorChain);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.InsulatorChain, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess InsulatorChain failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorChain.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EInsulatorChain AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.ProductCode =Convert.ToInt32( reader["ProductCode"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        public static EInsulatorChain AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            return insulator;
        }

        public static EInsulatorChain AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection =_connection;
            OleDbCommand command = new OleDbCommand("E_InsulatorChain_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
           // Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            //Connection.Close();
            return insulator;
        }

        public static EInsulatorChain AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorChain_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorChain insulator = new EInsulatorChain();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
                insulator.Comment = reader["Comment"].ToString();
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EInsulatorChain AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_InsulatorChain_SelectByXCode", _connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EInsulatorChain _EInsulatorChain = new EInsulatorChain();
        //    if (reader.Read())
        //    {
        //        _EInsulatorChain.Code = Convert.ToInt32(reader["Code"].ToString());
        //        _EInsulatorChain.Name = reader["Name"].ToString();
        //        _EInsulatorChain.XCode = new Guid(reader["XCode"].ToString());
        //        _EInsulatorChain.Lenght = Convert.ToDouble(reader["Lenght"].ToString());
        //        _EInsulatorChain.Comment = reader["Comment"].ToString();
        //    }
        //    else
        //    {
        //        _EInsulatorChain.Code = -1;
        //    }
        //    reader.Close();
        //    return _EInsulatorChain;
        //}


        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_InsulatorChain_Select", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_InsulatorChain_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
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
        //        EInsulatorChain catout = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
        //            EInsulatorChain Ap = Atend.Base.Equipment.EInsulatorChain.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EInsulatorChain.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            DataTable OperationTbl = new DataTable();
        //            catout.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, catout.XCode);
        //            catout.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);
        //            if (catout.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, catout.Code, (int)Atend.Control.Enum.ProductType.InsulatorChain))
        //                {
        //                    if (!catout.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
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
        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, catout.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                Servertransaction.Commit();
        //                Serverconnection.Close();
        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
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
        //        ed.WriteMessage(string.Format(" ERROR EInsulatorChain.ShareOnServer {0}\n", ex1.Message));
        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;




        //    //**************************
        //    //SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    //SqlTransaction Servertransaction;

        //    //SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    //SqlTransaction Localtransaction;

        //    //int DeletedCode = 0;

        //    //try
        //    //{
        //    //    Serverconnection.Open();
        //    //    Servertransaction = Serverconnection.BeginTransaction();

        //    //    Localconnection.Open();
        //    //    Localtransaction = Localconnection.BeginTransaction();

        //    //    EInsulatorChain _EInsulator = SelectByXCode(Localtransaction, Localconnection, XCode);

        //    //    bool canCommitTransaction = true;
        //    //    int Counter = 0;

        //    //    try
        //    //    {

        //    //        Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));


        //    //        EInsulatorChain Ap = Atend.Base.Equipment.EInsulatorChain.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //    //        if (Ap.XCode != Guid.Empty)
        //    //        {
        //    //            DeletedCode = Ap.Code;
        //    //            if (!Atend.Base.Equipment.EInsulatorChain.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //    //            {
        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();

        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //            }
        //    //        }

        //    //        DataTable OperationTbl = new DataTable();
        //    //        _EInsulator.OperationList = new ArrayList();
        //    //        OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EInsulator.XCode);
        //    //        _EInsulator.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //    //        if (_EInsulator.Insert(Servertransaction, Serverconnection))
        //    //        {
        //    //            if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EInsulator.Code, (int)Atend.Control.Enum.ProductType.InsulatorChain))
        //    //            {
        //    //                if (!_EInsulator.UpdateX(Localtransaction, Localconnection))
        //    //                {
        //    //                    //ed.WriteMessage("\n115\n");

        //    //                    Servertransaction.Rollback();
        //    //                    Serverconnection.Close();
        //    //                    Localtransaction.Rollback();
        //    //                    Localconnection.Close();
        //    //                    return false;
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                //ed.WriteMessage("\n114\n");

        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();

        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //                return false;
        //    //            }

        //    //            //Servertransaction.Commit();
        //    //            //Serverconnection.Close();

        //    //            //Localtransaction.Commit();
        //    //            //Localconnection.Close();
        //    //            //return true;
        //    //        }
        //    //        else
        //    //        {
        //    //            //ed.WriteMessage("\n114\n");

        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }

        //    //        if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EInsulator.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //    //        {
        //    //            ed.WriteMessage("\n113\n");

        //    //            Servertransaction.Commit();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Commit();
        //    //            Localconnection.Close();
        //    //        }
        //    //        else
        //    //        {
        //    //            ed.WriteMessage("\n114\n");

        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }




        //    //    }
        //    //    catch (System.Exception ex1)
        //    //    {
        //    //        //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //    //        Servertransaction.Rollback();
        //    //        Serverconnection.Close();

        //    //        Localtransaction.Rollback();
        //    //        Localconnection.Close();
        //    //        return false;
        //    //    }


        //    //}
        //    //catch (System.Exception ex1)
        //    //{
        //    //    //ed.WriteMessage(string.Format(" ERROR EInsulator.ShareOnServer {0}\n", ex1.Message));

        //    //    Serverconnection.Close();
        //    //    Localconnection.Close();
        //    //    return false;
        //    //}

        //    //return true;
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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
        //            EInsulatorChain Ap = Atend.Base.Equipment.EInsulatorChain.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EInsulatorChain.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EInsulatorChain.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorChain));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.InsulatorChain, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EInsulatorChain.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;
        //}


    }
}
