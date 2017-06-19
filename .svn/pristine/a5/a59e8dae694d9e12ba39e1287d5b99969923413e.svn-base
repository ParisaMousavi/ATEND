using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
//
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;
using System.Collections;

namespace Atend.Base.Equipment
{
    public class EFloor
    {
        public EFloor()
        { }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int productcode;
        public int ProductCode
        {
            get { return productcode; }
            set { productcode = value; }
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


        private bool isdefault;
        public bool IsDefault
        {
            get { return isdefault; }
            set { isdefault = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Floor_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                ed.WriteMessage(string.Format(" ERROR EFloor.Insert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Floor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Floor, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in FLoor");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Floor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Floor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Floor_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Floor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Floor, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in FLoor");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Floor");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Floor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Floor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Transaction = _transaction;

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Floor);
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

                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EFloor.InsertX {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand command = new SqlCommand("E_Floor_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iXCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
        //    command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR EAutoKey3p.Update {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Floor_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR EFloor.Delete {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static EFloor SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Floor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            dataReader.Close();
            connection.Close();
            return Floor;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Floor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            //EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                //Code = Convert.ToInt32(dataReader["Code"].ToString());
                //XCode = new Guid(dataReader["XCode"].ToString());
                //ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Comment = dataReader["Comment"].ToString();
                //IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Name = dataReader["Name"].ToString();

            }
            dataReader.Close();
            connection.Close();

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Floor_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsFloor = new DataSet();
            adapter.Fill(dsFloor);
            return dsFloor.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Floor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKey = new DataSet();
            adapter.Fill(dsKey);
            return dsKey.Tables[0];
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor)))
                {
                    canCommitTransaction = true;

                }
                else
                {
                    canCommitTransaction = false;
                }
                //connection.Close();
                if (canCommitTransaction)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EFloor.DeleteX {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static EFloor ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_SelectByXCode", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            dataReader.Close();
            //connection.Close();
            return Floor;
        }

        //MEDHAT //ShareOnServer
        public static EFloor ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Floor_Select", connection);
            command.Transaction = ServerTransaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();

            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : floor\n");
            }
            dataReader.Close();
            return Floor;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Floor_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

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

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Floor);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Floor);
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
                    ed.WriteMessage(string.Format("Error EFloor.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EFloor.Insert 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }

            ///////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.InsertX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command1 = new SqlCommand("E_Floor_InsertX", connection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add(new SqlParameter("iCode", Code));

            command1.Parameters.Add(new SqlParameter("iComment", Comment));
            command1.Parameters.Add(new SqlParameter("iName", Name));
            command1.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            //XCode = Guid.NewGuid();
            command1.Parameters.Add(new SqlParameter("iXCode", XCode));
            command1.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command1.Transaction = _transaction;
            try
            {
                ed.WriteMessage("\nBefor Execute\n");
                //Connection.Open();                 
                command1.ExecuteNonQuery();
                ed.WriteMessage("\nAfter Execute\n");
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Floor);
                    //_EOperation.ProductID = 
                    ed.WriteMessage("\nBefor Operation\n");

                    if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //ed.writeMessage("Error For Insert \n");
                    }
                    ed.WriteMessage("\nAfter Operation\n");

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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EFloor.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Floor_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Floor, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in FLoor");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Floor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Floor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("  >> Efloor.LocalUpdateX - Database Name : {0} \n", _connection.Database);
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Floor_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage("efloor.ExecuteNonQuery passed \n");

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Floor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Floor, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in FLoor");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Floor");
                    }
                }
                ed.WriteMessage("EFloor.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Floor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Floor failed");
                    }
                }
                ed.WriteMessage("Sub passed and floor finished\n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.LocalUpdateX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Floor_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
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

                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Floor);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor));
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
                    ed.WriteMessage(string.Format("Error EFloor.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EFloor.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }

            ///////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.UpdateX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}
        }

        //AHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p)))
                //{

                //    while (canCommitTransaction && Counter < operationList.Count)
                //    {

                //        Atend.Base.Equipment.EOperation _EOperation;
                //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                //        _EOperation.XCode = XCode;
                //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p);

                //        if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                //        {
                //            canCommitTransaction = true;
                //        }
                //        else
                //        {
                //            canCommitTransaction = false;
                //        }
                //        Counter++;
                //    }
                //}
                //if (canCommitTransaction)
                //{
                //    _transaction.Commit();
                //    connection.Close();
                //    return true;
                //}
                //else
                //{
                //    _transaction.Rollback();
                //    connection.Close();
                //    return false;
                //    //throw new Exception("can not commit transaction");

                //}
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EFloor.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }





            ////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.UpdateX {0}\n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Floor);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Floor_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EFloor.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EFloor.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            /////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.DeleteX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackage.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor)))
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
                    ed.WriteMessage(string.Format("Error EFloor.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EFloor.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }




            /////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.DeleteX {0}\n", ex1.Message));

            //    //connection.Close();
            //    return false;
        }


        //SentFromLocalToAccess
        public static EFloor SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Floor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.code = -1;
            }
            dataReader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Floor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Floor));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }

            dataReader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Floor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Floor));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(dataReader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(dataReader["Count"].ToString());

                nodeKeys.Add(Op);
            }
            dataReader.Close();
            connection.Close();
            return Floor;
        }

        public static EFloor SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_SelectByXCode", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Floor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Floor));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }

            dataReader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Floor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Floor));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                nodeKeys.Add(dataReader["ProductID"].ToString());
            }
            dataReader.Close();
            //connection.Close();
            return Floor;
        }

        //MEDHAT //ShareOnServer
        public static EFloor SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_SelectByXCode", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();

            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            return Floor;
        }

        //ASHKTORAB
        public static EFloor SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Floor_Select", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.code = -1;
                //return autoKey_3p;//nothing operation
            }
            dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", autoKey_3p.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AuoKey3p));
            //dataReader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (dataReader.Read())
            //{
            //    nodeKeys.Add(dataReader["ProductID"].ToString());
            //}
            //dataReader.Close();
            return Floor;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Floor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKey = new DataSet();
            adapter.Fill(dsKey);
            return dsKey.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Floor_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsFloor = new DataSet();
            adapter.Fill(dsFloor);
            return dsFloor.Tables[0];
        }

        //public static bool SearchByName(string Name)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlCommand command = new SqlCommand("E_AutoKey_3p_SearchByName", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iName", Name));

        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        reader.Close();
        //        connection.Close();
        //        return true;
        //    }
        //    else
        //    {
        //        reader.Close();
        //        connection.Close();
        //        return false;
        //    }

        //}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction transaction;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Floor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));


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
                ed.WriteMessage(string.Format("Error Floor.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Floor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Floor);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in FLoor");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Floor, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Floor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Floor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Floor);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction , _NewConnection))
                            throw new System.Exception("operation failed in FLoor");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.Floor, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Floor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Floor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EFloor AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Floor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                //Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            connection.Close();
            return Floor;
        }

        public static EFloor AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Floor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                //Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            return Floor;
        }

        public static EFloor AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Floor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                //Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            //connection.Close();
            return Floor;
        }

        public static EFloor AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Floor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EFloor Floor = new EFloor();
            if (dataReader.Read())
            {
                Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Floor.XCode = new Guid(dataReader["XCode"].ToString());
                Floor.Comment = dataReader["Comment"].ToString();
                Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Floor.Name = dataReader["Name"].ToString();

            }
            else
            {
                Floor.Code = -1;
            }
            dataReader.Close();
            connection.Close();
            return Floor;
        }

        //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EFloor AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Floor_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;


        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;


        //    OleDbDataReader dataReader = command.ExecuteReader();
        //    EFloor Floor = new EFloor();
        //    if (dataReader.Read())
        //    {
        //        Floor.Code = Convert.ToInt32(dataReader["Code"].ToString());
        //        Floor.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
        //        Floor.XCode = new Guid(dataReader["XCode"].ToString());
        //        Floor.Comment = dataReader["Comment"].ToString();
        //        Floor.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
        //        Floor.Name = dataReader["Name"].ToString();

        //    }
        //    else
        //    {
        //        Floor.Code = -1;
        //    }
        //    dataReader.Close();
        //    return Floor;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Floor_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsAutoKey_3p = new DataSet();
            adapter.Fill(dsAutoKey_3p);
            return dsAutoKey_3p.Tables[0];
        }

        //public static DataTable AccessSearch(string Name)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("E_FLoor_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
        //    DataSet dsKey = new DataSet();
        //    adapter.Fill(dsKey);
        //    return dsKey.Tables[0];
        //}

        //public static EAutoKey_3p AccessSelectByProductCode(int ProductCode)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("E_AutoKey_3p_SelectProductCode", connection);
        //    EAutoKey_3p autoKey_3p = new EAutoKey_3p();
        //    command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
        //    command.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        connection.Open();
        //        OleDbDataReader dataReader = command.ExecuteReader();
        //        if (dataReader.Read())
        //        {
        //            autoKey_3p.Code = Convert.ToInt16(dataReader["Code"].ToString());
        //            autoKey_3p.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
        //            autoKey_3p.Amper = Convert.ToSingle(dataReader["Amper"].ToString());
        //            autoKey_3p.Comment = dataReader["Comment"].ToString();
        //            autoKey_3p.Name = dataReader["Name"].ToString();
        //        }
        //        else
        //        {
        //            ed.WriteMessage(string.Format("No Record Found for ProductCode : {0}", ProductCode));
        //        }
        //        dataReader.Close();
        //        connection.Close();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error EAutoKey.SelectByProductCode : {0} \n", ex1.Message));
        //        connection.Close();

        //    }

        //    return autoKey_3p;
        //}

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Merge~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
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

        //        EFloor floor = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor));
        //            ed.WriteMessage("\n111\n");

        //            EFloor Ap = Atend.Base.Equipment.EFloor.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EFloor.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            floor.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, floor.XCode);
        //            floor.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (floor.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, floor.Code, (int)Atend.Control.Enum.ProductType.Floor))
        //                {

        //                    if (!floor.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    //else
        //                    //{
        //                    //    ed.WriteMessage("\n113\n");

        //                    //    Servertransaction.Commit();
        //                    //    Serverconnection.Close();

        //                    //    Localtransaction.Commit();
        //                    //    Localconnection.Close();
        //                    //}

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, floor.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Floor, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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



        //            ed.WriteMessage("\n112\n");


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
        //        ed.WriteMessage(string.Format(" ERROR EFloor.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor));
        //            ed.WriteMessage("\n111\n");

        //            EFloor Ap = Atend.Base.Equipment.EFloor.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EFloor.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EFloor.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Floor));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n112\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Floor, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EFloor.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
