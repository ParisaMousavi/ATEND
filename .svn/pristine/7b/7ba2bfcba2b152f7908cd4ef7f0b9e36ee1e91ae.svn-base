using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.OleDb;
using System.Collections;

namespace Atend.Base.Equipment
{
    public class EReCloser
    {
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

        private double amper;
        public double Amper
        {
            get { return amper; }
            set { amper = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~//


        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));

            try
            {
                connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.Insert : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Transaction = _transaction;

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //.WriteMessage("Error For Insert \n");
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.Insert : {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ReCloser, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.ReCloser, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.ServerInsert : {0} \n", ex.Message));

                return false;
            }

        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Update", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            //sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.ReCloser))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ReCloser, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed  in Recloser");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.ReCloser, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.ServerUpdate : {0} \n", ex.Message));

                return false;
            }

        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Update", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.Update : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Delete", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.Delete : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Delete", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;

            try
            {
                //connection.Open();
                sqlCommand.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser)))
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.ServerDelete : {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        public static EReCloser SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            connection.Close();
            reader.Close();
            return reCloser;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                //reCloser.XCode = Convert.ToInt32(reader["Code"].ToString());
                //reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();

            }
            connection.Close();
            reader.Close();
            //return reCloser;
        }

        public static EReCloser SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_SelectByProductCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EReCloser reCloser = new EReCloser();

            try
            {
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                    reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                    reCloser.Comment = reader["Comment"].ToString();
                    reCloser.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                connection.Close();
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return reCloser;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ReCloser_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ReCloser_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];

        }

        public static EReCloser ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            //connection.Close();
            reader.Close();
            return reCloser;
        }

        //MEDHAt //ShareOnServer
        public static EReCloser ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();

            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                reCloser.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : recloser\n");
            }
            reader.Close();
            return reCloser;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ReCloser_InsertX", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser);
                        if (containerPackage.InsertX(transaction, connection))
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
                        if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EReCloser.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EReCloser.Insert 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }

            /////////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.Insert : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_InsertX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser);
                    //_EOperation.ProductID = 
                    if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
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
                ed.WriteMessage(string.Format("Error EReCloser.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            ///////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.Insert : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_InsertX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.ReCloser, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.ReCloser, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_UpdateX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.ReCloser))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.ReCloser, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed  in Recloser");
                    }
                }
                ed.WriteMessage("ERecloser.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.ReCloser, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.LocalUpdate : {0} \n", ex.Message));

                return false;
            }

        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ReCloser_UpdateX", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser);

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
                    }

                    //Package
                    ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser));
                    ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(_transaction, connection) && canCommitTransaction)
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
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        _transaction.Rollback();
                        connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");
                    }

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EReCloser.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EReCloser.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }


            ///////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.UpdateX : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);

            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_UpdateX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            ////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.UpdateX : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.ReCloser);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_DeleteX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser))) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canCommitTransaction)
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
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EReCloser.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EReCloser.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }


            //////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.DeleteX : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_DeleteX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            sqlCommand.Transaction = _transaction;
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser)))
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
                    ed.WriteMessage(string.Format("Error EReCloser.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EReCloser.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            ////////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EReCloser.DeleteX : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        //MOUSAVI //SentFromLocalToAccess
        public static EReCloser SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                reCloser.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", reCloser.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.ReCloser));
            reader = sqlCommand.ExecuteReader();
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
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", reCloser.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.ReCloser));
            reader = sqlCommand.ExecuteReader();
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
            return reCloser;
        }

        //ASHKTORAB //ShareOnServer
        public static EReCloser SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            EReCloser reCloser = new EReCloser();


            try
            {
                sqlCommand.Transaction = LocalTransaction;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                    reCloser.XCode = new Guid(reader["XCode"].ToString());
                    reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                    reCloser.Comment = reader["Comment"].ToString();
                    reCloser.Name = reader["Name"].ToString();
                    reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    reCloser.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ERecloser.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return reCloser;
        }

        public static EReCloser SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", reCloser.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.ReCloser));
            reader = sqlCommand.ExecuteReader();
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
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", reCloser.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.ReCloser));
            reader = sqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }
            reader.Close();
            //connection.Close();
            return reCloser;
        }

        //ASHKTORAB
        public static EReCloser SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                reCloser.Code = -1;
            }
            //connection.Close();
            reader.Close();
            return reCloser;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ReCloser_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ReCloser_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ReCloser_SearchByName", connection);
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

        public static EReCloser CheckForExist(double _Amper, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ReCloser_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.XCode = new Guid(reader["XCode"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                reCloser.Code = -1;
            }
            reader.Close();
            connection.Close();
            return reCloser;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.AccessInsert : {0} \n", ex.Message));

                return false;
            }

        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ReCloser);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.ReCloser, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.AccessInsert : {0} \n", ex.Message));

                return false;
            }

        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _NewConnection;
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _NewTransaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            int OldCode = Code;

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.ReCloser);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.ReCloser, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess ReCloser failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EReCloser.AccessInsert : {0} \n", ex.Message));

                return false;
            }

        }

        //StatusReport
        public static EReCloser AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();

            }
            else
            {
                reCloser.Code = -1;
            }
            connection.Close();
            reader.Close();
            return reCloser;
        }

        public static EReCloser AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();

            }
            else
            {
                reCloser.Code = -1;
            }
            reader.Close();
            return reCloser;
        }

        public static EReCloser AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
           // connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();

            }
            else
            {
                reCloser.Code = -1;
            }
           // connection.Close();
            reader.Close();
            return reCloser;
        }

        public static EReCloser AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EReCloser reCloser = new EReCloser();
            if (reader.Read())
            {
                reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                reCloser.Comment = reader["Comment"].ToString();
                reCloser.Name = reader["Name"].ToString();
                reCloser.XCode = new Guid(reader["XCode"].ToString());

            }
            reader.Close();
            connection.Close();
            return reCloser;
        }

        //MOUAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EReCloser AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_SelectByXCode", connection);
        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    sqlCommand.Transaction = _transaction;
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    EReCloser reCloser = new EReCloser();
        //    if (reader.Read())
        //    {
        //        reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
        //        reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        reCloser.Type = Convert.ToByte(reader["Type"].ToString());
        //        reCloser.Comment = reader["Comment"].ToString();
        //        reCloser.Name = reader["Name"].ToString();
        //        reCloser.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        reCloser.Code = -1;
        //    }
        //    reader.Close();
        //    return reCloser;
        //}

        public static EReCloser AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_ReCloser_SelectByProductCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EReCloser reCloser = new EReCloser();

            try
            {
                connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    reCloser.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    reCloser.Code = Convert.ToInt32(reader["Code"].ToString());
                    reCloser.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    reCloser.Type = Convert.ToByte(reader["Type"].ToString());
                    reCloser.Comment = reader["Comment"].ToString();
                    reCloser.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                connection.Close();
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EReCloser.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return reCloser;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ReCloser_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ReCloser_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsReCloser = new DataSet();
            adapter.Fill(dsReCloser);
            return dsReCloser.Tables[0];

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

        //        EReCloser _EReCloser = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser));
        //            EReCloser Ap = Atend.Base.Equipment.EReCloser.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EReCloser.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EReCloser.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EReCloser.XCode);
        //            _EReCloser.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EReCloser.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EReCloser.Code, (int)Atend.Control.Enum.ProductType.ReCloser))
        //                {
        //                    if (!_EReCloser.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EReCloser.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.ReCloser, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ERecloser.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser));

        //            EReCloser Ap = Atend.Base.Equipment.EReCloser.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EReCloser.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EReCloser.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ReCloser));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.ReCloser, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EReCloser.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
