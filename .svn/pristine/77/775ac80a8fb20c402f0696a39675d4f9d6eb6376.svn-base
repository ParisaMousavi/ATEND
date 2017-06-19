using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DUser
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string family;

        public string Family
        {
            get { return family; }
            set { family = value; }
        }


        List<int> accessList;

        public List<int> AccessList
        {
            get { return accessList; }
            set { accessList = value; }
        }


        public DUser()
        {
            AccessList = new List<int>();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~ServerPart~~~~~~~~~~~~~~~~~~~~~~~//

        //ASHKTORAB
        public bool insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_User_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iUserName", UserName));
            command.Parameters.Add(new SqlParameter("iPassword", Password));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iFamily", Family));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());

                    foreach (int AccessId in AccessList)
                    {
                        Atend.Base.Design.DUserAccess userAccess = new DUserAccess();
                        userAccess.IDUser = Code;
                        userAccess.IdAccess = AccessId;

                        if (userAccess.Insert(transaction, connection))
                        {
                            //nothing
                        }
                        else
                        {
                            throw new System.Exception("userAccess.Insert failed");
                        }
                    }

                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.Insert : {0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.Insert: {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            //transaction.Commit();
            //connection.Close();
            return true;


        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction , SqlConnection _connection ,int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_User_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {

                    Command.ExecuteNonQuery();

                    if (DUserAccess.Delete(transaction, Connection, Code))
                    {
                        //do nothing
                    }
                    else
                    {
                        throw new System.Exception("DUserAccess.DeleteX failed");
                    }

                    //transaction.Commit();
                    //Connection.Close();
                    return true;
                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.Delete {0}\n", ex1.Message));
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static DUser ServerSelectByUsernameAndPassword(SqlTransaction _transaction , SqlConnection _connection, string userName, string password)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByUserNameAndpassword", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iUserName", userName));
            sqlCommand.Parameters.Add(new SqlParameter("iPassword", password));
            sqlCommand.Transaction = _transaction;

            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();

            if (reader.Read())
            {
                user.Code = Convert.ToInt32(reader["Code"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Name = reader["Name"].ToString();
                user.Family = reader["Family"].ToString();
            }
            else
            {
                user.Code = -1;
            }
            //Connection.Close();
            reader.Close();
            return user;

        }

        //ASHKTORAB
        public static DataTable ServerSelectAll()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_User_SelectByCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet LocalUserds = new DataSet();
            adapter.Fill(LocalUserds);

            return LocalUserds.Tables[0];

        }

        //~~~~~~~~~~~~~~~~ Local Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //MOUSAVI
        public bool insertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_User_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iUserName", UserName));
            command.Parameters.Add(new SqlParameter("iPassword", Password));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iFamily", Family));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());

                    foreach (int AccessId in AccessList)
                    {
                        Atend.Base.Design.DUserAccess userAccess = new DUserAccess();
                        userAccess.IDUser = Code;
                        userAccess.IdAccess = AccessId;

                        if (userAccess.InsertX(transaction, connection))
                        {
                            //nothing
                        }
                        else
                        {
                            throw new System.Exception("userAccess.InsertX failed");
                        }
                    }

                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.InsertX : {0}\n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.InsertX: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            transaction.Commit();
            connection.Close();
            return true;


        }

        //ASHKTORAB
        public bool insertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_User_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iUserName", UserName));
            command.Parameters.Add(new SqlParameter("iPassword", Password));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iFamily", Family));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());

                    foreach (int AccessId in AccessList)
                    {
                        Atend.Base.Design.DUserAccess userAccess = new DUserAccess();
                        userAccess.IDUser = Code;
                        userAccess.IdAccess = AccessId;

                        if (userAccess.InsertX(transaction, connection))
                        {
                            //nothing
                        }
                        else
                        {
                            throw new System.Exception("userAccess.Insert failed");
                        }
                    }

                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.Insert : {0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.Insert: {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            //transaction.Commit();
            //connection.Close();
            return true;


        }


        //MOUSAVI
        public bool updateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transAction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_User_Update", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iUserName", UserName));
            command.Parameters.Add(new SqlParameter("iPassword", Password));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iFamily", Family));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                transAction = connection.BeginTransaction();
                command.Transaction = transAction;
                try
                {
                    command.ExecuteNonQuery();

                    if (Atend.Base.Design.DUserAccess.DeleteX(transAction, connection, Code))
                    {
                        foreach (int AccessId in AccessList)
                        {
                            Atend.Base.Design.DUserAccess userAccess = new DUserAccess();
                            userAccess.IDUser = Code;
                            userAccess.IdAccess = AccessId;

                            if (userAccess.InsertX(transAction, connection))
                            {
                                //nothing
                            }
                            else
                            {
                                throw new System.Exception("userAccess.InsertX failed");
                            }
                        }
                    }
                    else
                    {
                        throw new System.Exception("DUserAccess.DeleteX failed");
                    }
                    transAction.Commit();
                    connection.Close();
                    return true;

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format(" ERROR DUser.UpdateX {0}\n", ex1.Message));

                    transAction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR Transaction EDUser.UpdateX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //MOUSAVI
        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("D_User_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {

                    Command.ExecuteNonQuery();

                    if (DUserAccess.DeleteX(transaction, Connection, Code))
                    {
                        //do nothing
                    }
                    else
                    {
                        throw new System.Exception("DUserAccess.DeleteX failed");
                    }

                    transaction.Commit();
                    Connection.Close();
                    return true;
                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.Delete {0}\n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.DeleteX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }

        }

        public static bool Delete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_User_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {

                    Command.ExecuteNonQuery();

                    if (DUserAccess.DeleteX(transaction, Connection, Code))
                    {
                        //do nothing
                    }
                    else
                    {
                        throw new System.Exception("DUserAccess.Delete failed");
                    }

                    //transaction.Commit();
                    //Connection.Close();
                    return true;
                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.DeleteX {0}\n", ex1.Message));
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.Delete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }

        }

        public static bool DeleteAll(SqlConnection _connection,SqlTransaction _transaction)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_User_DeleteAll", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            
            try
            {
                Command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    Command.ExecuteNonQuery();
                    if (!DUserAccess.DeleteAll(Connection,transaction))
                    {
                        throw new System.Exception("DUserAccess.DeleteAll failed");
                    }
                    return true;
                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR DUser.DeleteAll {0}\n", ex1.Message));
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction DUser.DeleteAll: {0} \n", ex1.Message));

                return false;
            }

        }

        //ASHKTORAB
        public static DataTable SelectAll()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_User_SelectByCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet LocalUserds = new DataSet();
            adapter.Fill(LocalUserds);

            return LocalUserds.Tables[0];

        }

        public static DataTable SelectAll(SqlConnection _Connection, SqlTransaction _Transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _Connection;
            SqlDataAdapter adapter = new SqlDataAdapter("D_User_SelectByCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.Transaction = _Transaction;

            DataSet LocalUserds = new DataSet();
            adapter.Fill(LocalUserds);

            return LocalUserds.Tables[0];

        }


        public static DUser SelectByUserName(string username)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByUserName", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iUserName", username));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();

            if (reader.Read())
            {
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
            }
            Connection.Close();
            reader.Close();
            return user;

        }

        //MOUSAVI
        public static DUser SelectByCode(int code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", code));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();

            if (reader.Read())
            {
                user.Code = Convert.ToInt32(reader["Code"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Name = reader["Name"].ToString();
                user.Family = reader["Family"].ToString();

            }
            else
            {
                user.code = -1;
            }
            reader.Close();
            Connection.Close();

            if (user.Code != -1)
            {

                DataTable AccessForUser = DUserAccess.SelectByUserId(user.Code);
                foreach (DataRow dr in AccessForUser.Rows)
                {
                    user.AccessList.Add(Convert.ToInt32(dr["IdAccess"]));
                }

            }
            return user;

        }

        public static DUser SelectByCode(int code, SqlConnection _Connection, SqlTransaction _Transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _Connection;
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", code));
            sqlCommand.Transaction = _Transaction;
            
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();
            if (reader.Read())
            {
                user.Code = Convert.ToInt32(reader["Code"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Name = reader["Name"].ToString();
                user.Family = reader["Family"].ToString();

            }
            else
            {
                user.code = -1;
            }
            reader.Close();

            if (user.Code != -1)
            {

                DataTable AccessForUser = DUserAccess.SelectByUserId(user.Code);
                foreach (DataRow dr in AccessForUser.Rows)
                {
                    user.AccessList.Add(Convert.ToInt32(dr["IdAccess"]));
                }

            }
            return user;

        }

        //MOUSAVI
        public static DUser SelectByUsernameAndPassword(string userName, string password)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByUserNameAndpassword", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iUserName", userName));
            sqlCommand.Parameters.Add(new SqlParameter("iPassword", password));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();

            if (reader.Read())
            {
                user.Code = Convert.ToInt32(reader["Code"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Name = reader["Name"].ToString();
                user.Family = reader["Family"].ToString();
            }
            else
            {
                user.Code = -1;
            }
            Connection.Close();
            reader.Close();
            return user;

        }

        //MOUSAVI
        public static DUser SelectByUsernameAndPassword(SqlTransaction _transaction , SqlConnection _connection ,string userName, string password)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("D_User_SelectByUserNameAndpassword", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iUserName", userName));
            sqlCommand.Parameters.Add(new SqlParameter("iPassword", password));
            sqlCommand.Transaction = _transaction;
            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DUser user = new DUser();

            if (reader.Read())
            {
                user.Code = Convert.ToInt32(reader["Code"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.Name = reader["Name"].ToString();
                user.Family = reader["Family"].ToString();
            }
            else
            {
                user.Code = -1;
            }
            //Connection.Close();
            reader.Close();
            return user;

        }


        public static DataTable SearchX(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_User_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iUserName", Name));
            DataSet dsUser = new DataSet();
            adapter.Fill(dsUser);
            return dsUser.Tables[0];
        }


        public static bool ShareOnServer()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction ServerTransaction = null;
            SqlConnection ServerConnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);

            SqlTransaction LocalTransaction = null;
            SqlConnection LocalConnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            try
            {
                ServerConnection.Open();
                LocalConnection.Open();
                try
                {
                    ServerTransaction = ServerConnection.BeginTransaction();
                    LocalTransaction = LocalConnection.BeginTransaction();

                    if (Atend.Base.Design.DUser.DeleteAll(ServerConnection, ServerTransaction))
                    {
                        DataTable dtUser = Atend.Base.Design.DUser.SelectAll(LocalConnection, LocalTransaction);
                        foreach (DataRow dr in dtUser.Rows)
                        {
                            DUser User = new DUser();
                            User.UserName = dr["UserName"].ToString();
                            User.Password = dr["Password"].ToString();
                            User.Name = dr["Name"].ToString();
                            User.Family = dr["Family"].ToString();

                            DataTable dtUserAccess = DUserAccess.SelectByUserId(Convert.ToInt32(dr["Code"]), LocalConnection, LocalTransaction);
                            foreach (DataRow druseraccess in dtUserAccess.Rows)
                            {
                                User.AccessList.Add(Convert.ToInt32(druseraccess["IDAccess"].ToString()));
                            }

                            if (!User.insertX(ServerTransaction, ServerConnection))
                            {
                                throw new System.Exception("Server Insert failed");
                            }
                        }
                    }
                    else
                    {
                        throw new System.Exception("Server Delete failed");
                    }
                    
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("ERROR D_ShareOnServer catch 1: {0} \n", ex.Message);
                    ServerTransaction.Rollback();
                    ServerConnection.Close();

                    LocalTransaction.Rollback();
                    LocalConnection.Close();
                    return false;
                }

                ServerTransaction.Commit();
                ServerConnection.Close();

                LocalTransaction.Commit();
                LocalConnection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR D_ShareOnServer catch 2: {0} \n", ex1.Message);
                ServerConnection.Close();
                LocalConnection.Close();
                return false;
            }

            return true;

        }

        public static bool GetFromServer()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction ServerTransaction = null;
            SqlConnection ServerConnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);

            SqlTransaction LocalTransaction = null;
            SqlConnection LocalConnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            try
            {
                ServerConnection.Open();
                LocalConnection.Open();
                try
                {
                    ServerTransaction = ServerConnection.BeginTransaction();
                    LocalTransaction = LocalConnection.BeginTransaction();

                    if (Atend.Base.Design.DUser.DeleteAll(LocalConnection, LocalTransaction))
                    {
                        DataTable dtUser = Atend.Base.Design.DUser.SelectAll(ServerConnection, ServerTransaction);
                        foreach (DataRow dr in dtUser.Rows)
                        {
                            DUser User = new DUser();
                            User.UserName = dr["UserName"].ToString();
                            User.Password = dr["Password"].ToString();
                            User.Name = dr["Name"].ToString();
                            User.Family = dr["Family"].ToString();

                            DataTable dtUserAccess = DUserAccess.SelectByUserId(Convert.ToInt32(dr["Code"]), ServerConnection, ServerTransaction);
                            foreach (DataRow druseraccess in dtUserAccess.Rows)
                            {
                                User.AccessList.Add(Convert.ToInt32(druseraccess["IDAccess"].ToString()));
                            }

                            if (!User.insertX(LocalTransaction, LocalConnection))
                            {
                                throw new System.Exception("Local Insert failed");
                            }
                        }
                    }
                    else
                    {
                        throw new System.Exception("Local Delete failed");
                    }

                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("ERROR D_GetFromServer catch 1: {0} \n", ex.Message);
                    ServerTransaction.Rollback();
                    ServerConnection.Close();

                    LocalTransaction.Rollback();
                    LocalConnection.Close();
                    return false;
                }

                ServerTransaction.Commit();
                ServerConnection.Close();

                LocalTransaction.Commit();
                LocalConnection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR D_GetFromServer catch 2: {0} \n", ex1.Message);
                ServerConnection.Close();
                LocalConnection.Close();
                return false;
            }

            return true;

        }

        //~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //public bool Accessinsert()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    int insertRowCode;
        //    OleDbTransaction transaction;
        //OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("D_User_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iUserName", UserName));
        //    command.Parameters.Add(new OleDbParameter("iPassword", Password));
        //    try
        //    {
        //        bool canCommitTransaction = true;
        //        connection.Open();
        //        transaction = connection.BeginTransaction();
        //        command.Transaction = transaction;
        //        try
        //        {
        //            //ed.writeMessage("Befor UserInsert\n");
        //            insertRowCode = Convert.ToInt32(command.ExecuteScalar());
        //            //ed.writeMessage("insertRowCode:"+insertRowCode.ToString()+"\n");
        //            int counter = 0;
        //            while (canCommitTransaction && counter < AccessList.Count)
        //            {
        //                Atend.Base.Design.DUserAccess userAccess;
        //                //userAccess = ((Atend.Base.Design.DUserAccess)AccessList[counter]);
        //                //userAccess.IDUser = insertRowCode;

        //                //if (userAccess.AccessInsert(transaction, connection) && canCommitTransaction)
        //                //{
        //                //    canCommitTransaction = true;
        //                //}
        //                //else
        //                //{
        //                //    canCommitTransaction = false;
        //                //    //ed.writeMessage("Error For Insert \n");
        //                //}
        //                counter++;
        //            }
        //            if (canCommitTransaction)
        //            {
        //                transaction.Commit();
        //                connection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                transaction.Rollback();
        //                connection.Close();
        //                return false;
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //            ed.WriteMessage(string.Format(" ERROR DUser.AccessInsert {0}\n", ex1.Message));
        //            transaction.Rollback();
        //            connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR Transaction EDUser.AccessInsert: {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }

        //}

        //public bool Accessupdate()
        //{
        //    OleDbTransaction transAction;
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("D_User_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iUserName", UserName));

        //    command.Parameters.Add(new OleDbParameter("iPassword", Password));
        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    try
        //    {
        //        connection.Open();
        //        transAction = connection.BeginTransaction();
        //        command.Transaction = transAction;
        //        Boolean canComitTransaction = true;
        //        int Counter = 0;
        //        try
        //        {
        //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //            command.ExecuteNonQuery();

        //            if (Atend.Base.Design.DUserAccess.AccessDelete(transAction, connection, Code))
        //            {
        //                while (canComitTransaction && Counter < accessList.Count)
        //                {
        //                    Atend.Base.Design.DUserAccess useraccess = new DUserAccess();
        //                    //useraccess = ((Atend.Base.Design.DUserAccess)accessList[Counter]);
        //                    useraccess.IDUser = Code;
        //                    if (useraccess.AccessInsert(transAction, connection) && canComitTransaction)
        //                    {
        //                        canComitTransaction = true;
        //                    }
        //                    else
        //                    {
        //                        canComitTransaction = false;
        //                        ed.WriteMessage("Error For Insert \n");
        //                    }
        //                    Counter++;
        //                }
        //            }
        //            if (canComitTransaction)
        //            {
        //                transAction.Commit();
        //                connection.Close();
        //                return true;
        //            }
        //            else
        //            {

        //                transAction.Rollback();
        //                connection.Close();
        //                return false;
        //            }

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //            ed.WriteMessage(string.Format(" ERROR DUer.AccessUpdate {0}\n", ex1.Message));

        //            transAction.Rollback();
        //            connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR Transaction EDUser.AccessUpdate: {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static bool AccessDelete(int Code)
        //{
        //    OleDbTransaction transaction;
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand Command = new OleDbCommand("D_User_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    try
        //    {
        //        Connection.Open();
        //        transaction = Connection.BeginTransaction();
        //        Command.Transaction = transaction;
        //        Boolean canComitTransaction = true;
        //        try
        //        {

        //            Command.ExecuteNonQuery();

        //            if (DUserAccess.AccessDelete(transaction, Connection, Code))
        //            {
        //                canComitTransaction = true;
        //            }
        //            else
        //            {
        //                canComitTransaction = false;
        //            }
        //            if (canComitTransaction)
        //            {
        //                transaction.Commit();
        //                Connection.Close();
        //                return true;
        //            }
        //            else
        //            {

        //                transaction.Rollback();
        //                Connection.Close();
        //                return false;
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //            ed.WriteMessage(string.Format(" ERROR DUser.AccessDelete {0}\n", ex1.Message));

        //            Connection.Close();
        //            return false;

        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR Transaction DUser.AccessDelete: {0} \n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }

        //}

        //public static DUser AccessSelectByUserName(string username)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand sqlCommand = new OleDbCommand("D_User_SelectByUserName", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iUserName", username));
        //    Connection.Open();
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    DUser user = new DUser();

        //    if (reader.Read())
        //    {
        //        user.UserName = reader["UserName"].ToString();
        //        user.Password = reader["Password"].ToString();
        //    }
        //    Connection.Close();
        //    reader.Close();
        //    return user;

        //}

        //public static DUser AccessSelectByCode(int code)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand sqlCommand = new OleDbCommand("D_User_SelectByCode", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iCode", code));
        //    Connection.Open();
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    DUser user = new DUser();

        //    if (reader.Read())
        //    {
        //        user.Code = Convert.ToInt32(reader["Code"].ToString());
        //        user.UserName = reader["UserName"].ToString();
        //        user.Password = reader["Password"].ToString();
        //    }
        //    reader.Close();
        //    sqlCommand.Parameters.Clear();
        //    sqlCommand.CommandText = "D_AccessLevel";
        //    sqlCommand.Parameters.Add(new OleDbParameter("iUserId", user.Code));
        //    //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
        //    reader = sqlCommand.ExecuteReader();
        //    //nodeKey.Clear();
        //    while (reader.Read())
        //    {
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //        //ed.writeMessage("True\n");
        //        //nodeKey.Add(reader["ID"].ToString());
        //    }

        //    reader.Close();
        //    Connection.Close();


        //    return user;

        //}

        //public static DUser AccessSelectByUsernameAndPassword(string userName, string password)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand sqlCommand = new OleDbCommand("D_User_SelectByUserNameAndpassword", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iUserName", userName));
        //    sqlCommand.Parameters.Add(new OleDbParameter("iPassword", password));

        //    Connection.Open();
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    DUser user = new DUser();

        //    if (reader.Read())
        //    {
        //        user.Code = Convert.ToInt32(reader["Code"].ToString());
        //        user.UserName = reader["UserName"].ToString();
        //        user.Password = reader["Password"].ToString();
        //    }
        //    Connection.Close();
        //    reader.Close();
        //    return user;

        //}

        //public static DataTable AccessSearch(string Name)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("D_User_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iUserName", Name));
        //    DataSet dsUser = new DataSet();
        //    adapter.Fill(dsUser);
        //    return dsUser.Tables[0];
        //}

    }
}
