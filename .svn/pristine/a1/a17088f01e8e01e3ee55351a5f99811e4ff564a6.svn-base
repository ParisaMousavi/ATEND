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
    public class EInsulatorPipe
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
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

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private bool isDefault;

        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_Insert", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction  _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips,SqlTransaction LocalTransaction,SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection  con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorPipe_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            ed.WriteMessage("XCode={0}\n",XCode.ToString());
            ed.WriteMessage("Name={0}\n", Name.ToString());
            ed.WriteMessage("ProductCode={0}\n", ProductCode.ToString());
            ed.WriteMessage("IsDefault={0}\n", IsDefault.ToString());

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                ed.WriteMessage("1\n");
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                ed.WriteMessage("2\n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in InsulatorPipe");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, Code, _transaction, _connection,LocalTransaction,LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorPipe_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.InsulatorPipe))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in InsulatorPipe");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in InsulatorPipe");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, Code, _transaction, _connection,LocalTransaction,LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_Delete", Connection);
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
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
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

        public static EInsulatorPipe SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        public static EInsulatorPipe SelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            reader.Close();
            return insulator;
        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Insulatorpipe_Select", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Name = reader["Name"].ToString();
            }
            reader.Close();

            Connection.Close();
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorPipe_Select", Connection);
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
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorPipe_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static EInsulatorPipe ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_SelectByXCode", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            reader.Close();

            return insulator;
        }

        //MEDHAT //ShareOnServer
        public static EInsulatorPipe ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_InsulatorPipe_Select", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            else
            {
                insulator.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : insulator pipe\n");
            }
            reader.Close();
            return insulator;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error in InsPipe {0}\n",ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault",IsDefault));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch (System.Exception ex1)
                {
                    Connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorPipe_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.InsulatorPipe, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in InsulatorPipe");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.InsulatorPipe, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_InsulatorPipe_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.InsulatorPipe, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in InsulatorPipe");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in InsulatorPipe");
                    }

                }
                ed.WriteMessage("EInsulatorPipe.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.InsulatorPipe, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromServerToLocal InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault",IsDefault));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch (System.Exception ex1)
                {
                    //ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    //Connection.Close();
                    //transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                //ed.WriteMessage(string.Format(" ERROR EInsulator.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_InsulatorPipe_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }


                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage("Error In TransActionX:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //SentFromLocalToAccess
        public static EInsulatorPipe SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            reader.Close();

            Connection.Close();
            return insulator;
        }

        //ASHKTORAB //ShareOnServer
        public static EInsulatorPipe SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_InsulatorPipe_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EInsulatorPipe insulator = new EInsulatorPipe();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.XCode = new Guid(reader["XCode"].ToString());
                    insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    insulator.Name = reader["Name"].ToString();
                    insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorpipe.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return insulator;
        }


        public static EInsulatorPipe SelectByCodeForLocal(int Code, SqlTransaction  LocalTransaction, SqlConnection  LocalConnection)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            EInsulatorPipe insulator = new EInsulatorPipe();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.XCode = new Guid(reader["XCode"].ToString());
                    insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    insulator.Name = reader["Name"].ToString();
                    insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                }
                else
                {
                    insulator.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {//#
                ed.WriteMessage("Error Isulatorpipe.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return insulator;
        }

        public static EInsulatorPipe SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            reader.Close();
            return insulator;
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulatorpipe_SelectAll", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_InsulatorPipe_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static EInsulatorPipe SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_InsulatorPipe_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            SqlDataReader reader = command.ExecuteReader();
            EInsulatorPipe insp = new EInsulatorPipe();
            if (reader.Read())
            {
                insp.Code = Convert.ToInt16(reader["Code"].ToString());
                insp.Name = reader["Name"].ToString();
                insp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insp.XCode = new Guid(reader["XCode"].ToString());
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
            DataTable bp = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.InsulatorPipe));
            //ed.WriteMessage("bp.Count={0}\n", bp.Rows.Count);
            foreach (DataRow dr in bp.Rows)
            {
                EInsulatorPipe IsnP = Atend.Base.Equipment.EInsulatorPipe.SelectByProductCode(Convert.ToInt32(dr["ID"].ToString()));
                if (IsnP.Code != -1)
                {
                    //ed.WriteMessage("ramp.Name={0}\n", IsnP.Name);
                    IsnP.Name = dr["Name"].ToString();
                    //ed.WriteMessage("ProductCode={0}\n", dr["ID"].ToString());
                    IsnP.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    if (!IsnP.UpdateX())
                        return false;

                }
                else
                {
                    //ed.WriteMessage("InsertInsul. Pipe\n");
                    IsnP.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    IsnP.Name = dr["Name"].ToString();
                    if (!IsnP.InsertX())
                        return false;
                }
            }
            return true;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorPipe_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
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
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorPipe_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_InsulatorPipe_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType (oldCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.InsulatorPipe, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess InsulatorPipe failed");
                    }
                }

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulatorPipe.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }


        //StatusReport
        public static EInsulatorPipe AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        public static EInsulatorPipe AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            return insulator;
        }

        public static EInsulatorPipe AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand command = new OleDbCommand("E_InsulatorPipe_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Name = reader["Name"].ToString();
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            //Connection.Close();
            return insulator;
        }

        public static EInsulatorPipe AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_InsulatorPipe_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EInsulatorPipe insulator = new EInsulatorPipe();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.Name = reader["Name"].ToString();
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EInsulatorPipe AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_InsulatorPipe_SelectByXCode", _connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EInsulatorPipe _EInsulatorPipe = new EInsulatorPipe();
        //    if (reader.Read())
        //    {
        //        _EInsulatorPipe.Code = Convert.ToInt32(reader["Code"].ToString());
        //        _EInsulatorPipe.Name = reader["Name"].ToString();
        //        _EInsulatorPipe.XCode = new Guid(reader["XCode"].ToString());
        //        _EInsulatorPipe.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //    }
        //    else
        //    {
        //        _EInsulatorPipe.Code = -1;
        //    }
        //    reader.Close();
        //    return _EInsulatorPipe;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_InsulatorPipe_Select", Connection);
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
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_InsulatorPipe_Search", Connection);
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

        public static bool ShareOnServer(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlTransaction Servertransaction;

            SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlTransaction Localtransaction;

            int DeletedCode = 0;

            try
            {
                Serverconnection.Open();
                Servertransaction = Serverconnection.BeginTransaction();

                Localconnection.Open();
                Localtransaction = Localconnection.BeginTransaction();

                EInsulatorPipe _EInsulator = SelectByXCode(Localtransaction, Localconnection, XCode);

                bool canCommitTransaction = true;
                int Counter = 0;

                try
                {



                    EInsulatorPipe Ap = Atend.Base.Equipment.EInsulatorPipe.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

                    if (Ap.XCode != Guid.Empty)
                    {
                        DeletedCode = Ap.Code;
                        if (!Atend.Base.Equipment.EInsulatorPipe.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
                        {
                            Servertransaction.Rollback();
                            Serverconnection.Close();

                            Localtransaction.Rollback();
                            Localconnection.Close();
                        }
                    }


                }
                catch (System.Exception ex1)
                {
                    //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    Servertransaction.Rollback();
                    Serverconnection.Close();

                    Localtransaction.Rollback();
                    Localconnection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //ed.WriteMessage(string.Format(" ERROR EInsulator.ShareOnServer {0}\n", ex1.Message));

                Serverconnection.Close();
                Localconnection.Close();
                return false;
            }

            return true;
        }

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
        //            EInsulatorPipe Ap = Atend.Base.Equipment.EInsulatorPipe.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EInsulatorPipe.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EInsulatorPipe.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
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
        //        ed.WriteMessage(string.Format(" ERROR EInsulator.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
