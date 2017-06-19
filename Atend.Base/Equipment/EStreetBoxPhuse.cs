using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Equipment
{
    public class EStreetBoxPhuse
    {

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid streetBoxXCode;
        public Guid StreetBoxXCode
        {
            get { return streetBoxXCode; }
            set { streetBoxXCode = value; }
        }

        private int streetBoxCode;
        public int StreetBoxCode
        {
            get { return streetBoxCode; }
            set { streetBoxCode = value; }
        }

        private int feederNum;
        public int FeederNum
        {
            get { return feederNum; }
            set { feederNum = value; }
        }

        private int phuseCode;
        public int PhuseCode
        {
            get { return phuseCode; }
            set { phuseCode = value; }
        }

        private Guid phuseXCode;
        public Guid PhuseXCode
        {
            get { return phuseXCode; }
            set { phuseXCode = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private byte phuseType;
        public byte PhuseType
        {
            get { return phuseType; }
            set { phuseType = value; }
        }

        private byte shemshNum;
        public byte ShemshNum
        {
            get { return shemshNum; }
            set { shemshNum = value; }
        }

        private bool ioType;
        public bool IOType
        {
            get { return ioType; }
            set { ioType = value; }
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


        //~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_Insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", 0));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Insert 01 : {0}", ex1.Message));
                return false;
            }


        }

        public bool Insert(SqlTransaction Transaction, SqlConnection Connection)
        {
            SqlConnection connection = Connection;
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_Insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            sqlCommand.Transaction = Transaction;
            try
            {
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Insert 01 : {0}", ex1.Message));
                return false;
            }


        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;



            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("~~>>~~Streetbox phuse xcode :{0} \n", XCode);
            //ed.WriteMessage("Streetboxcode :{0} \n", StreetBoxCode);
            //command.Parameters.Add(new OleDbParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("--<<--StreetboxXcode :{0} \n", StreetBoxXCode);
            command.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            command.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("~~>>~~Streetbox phuse xcode :{0} \n", XCode);
            command.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            //ed.WriteMessage("Streetboxcode :{0} \n", StreetBoxCode);
            //command.Parameters.Add(new OleDbParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("--<<--StreetboxXcode :{0} \n", StreetBoxXCode);
            command.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_Update", connection);
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));
            sqlCommand.Parameters.Add(new SqlParameter("iIOType", IOType));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Update : {0}", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool ServerUpdate(SqlTransaction transaction, SqlConnection Connection, int Code, int NewCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction = transaction;
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_UpdateXX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iPhuseCode", Code));
            command.Parameters.Add(new SqlParameter("iNewCode", NewCode));

            try
            {
                //connection.Open();
                //_transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    command.ExecuteNonQuery();
                    return true;
                }

                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    //connection.Close();
                    //_transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                ////Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EStreetBoxPhuse.ServerUpdate {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }


        public static bool Delete(SqlTransaction Transaction, SqlConnection Connection, int StreetBoxCode)
        {
            SqlConnection connection = Connection;
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_Delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            sqlCommand.Transaction = Transaction;


            try
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("exec");
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("return true ");
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Deletee : {0}", ex1.Message));
                return false;
            }
        }

        public static DataTable SelectByStreetBoxCode(int StreetBoxCode, int PhuseType)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));


            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable SelectByStreetBoxCode(int StreetBoxCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));

            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable ServerSelectByStreetBoxCode(int StreetBoxCode,SqlTransaction _transaction,SqlConnection _connection)
        {
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));

            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        //ASHKTORAB
        public static EStreetBoxPhuse SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.PhuseCode = Convert.ToInt32(reader["PhuseCode"].ToString());
            }
            reader.Close();
            connection.Close();

            return sbp;
        }


        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                //sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"].ToString());
                FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                Comment = reader["Comment"].ToString();
                PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                PhuseCode = Convert.ToInt32(reader["PhuseCode"].ToString());
            }
            reader.Close();
            connection.Close();

            //return sbp;
        }

        public static bool DeleteByStreetBoxCode(int StreetBoxCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_DeleteByStreetBoxCode", connection);
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.DeleteByStreetBoxCode : {0}", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //MEDHAT //ShareOnServer
        public static EStreetBoxPhuse ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();

            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                sbp.PhuseCode = Convert.ToInt32(reader["PhuseCode"].ToString());
            }
            else
            {
                sbp.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : street box phuse\n");
            }
            reader.Close();
            return sbp;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~//

        public bool InsertX(SqlTransaction Transaction, SqlConnection Connection)
        {
            SqlConnection connection = Connection;
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_InsertX", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseXCode", PhuseXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));
            //sqlCommand.Parameters.Add(new SqlParameter("iIOType", IOType));

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            sqlCommand.Transaction = Transaction;
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Insert 01 : {0}", ex1.Message));
                return false;
            }


        }
        public bool InsertXX(SqlTransaction Transaction, SqlConnection Connection)
        {
            SqlConnection connection = Connection;
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_InsertX", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseXCode", PhuseXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));
            //sqlCommand.Parameters.Add(new SqlParameter("iIOType", IOType));

            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            sqlCommand.Transaction = Transaction;
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Insert 01 : {0}", ex1.Message));
                return false;
            }


        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("~~>>~~Streetbox phuse xcode :{0} \n", XCode);
            command.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("Streetboxcode :{0} \n", StreetBoxCode);
            //command.Parameters.Add(new OleDbParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("--<<--StreetboxXcode :{0} \n", StreetBoxXCode);
            command.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new SqlParameter("iPhuseXCode", PhuseXCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("~~>>~~Streetbox phuse xcode :{0} \n", XCode);
            command.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("Streetboxcode :{0} \n", StreetBoxCode);
            //command.Parameters.Add(new OleDbParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("--<<--StreetboxXcode :{0} \n", StreetBoxXCode);
            command.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new SqlParameter("iPhuseXCode", PhuseXCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //command.Parameters.Add(new SqlParameter("iiIOType", IOType));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_UpdateX", connection);
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseCode", PhuseCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));
            //sqlCommand.Parameters.Add(new SqlParameter("iIOType", IOType));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.UpdateX : {0}", ex1.Message));
                connection.Close();
                return false;
            }
        }


        //ShareOnServer
        public bool UpdateX(SqlTransaction Transaction, SqlConnection Connection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_UpdateX", connection);

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iFeederNum", FeederNum));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseXCode", PhuseXCode));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iPhuseType", PhuseType));
            //sqlCommand.Parameters.Add(new SqlParameter("iShemshNum", ShemshNum));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlCommand.Transaction = Transaction;

                try
                {
                    //connection.Open();
                    //ed.WriteMessage("\naaaaaaaaa\n");
                    sqlCommand.ExecuteNonQuery();
                    //ed.WriteMessage("\nbbbbbbbbb\n");
                    //connection.Close();
                    return true;
                }
                catch (System.Exception ex1)
                {

                    ed.WriteMessage(string.Format("ERROR EStreetBoxPhuse.UpdateX : {0}", ex1.Message));
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {

                ed.WriteMessage(string.Format("ERROR In Transaction: {0}", ex2.Message));
                //connection.Close();
                return false;
            }
        }


        public static bool DeleteX(SqlTransaction Transaction, SqlConnection Connection, Guid StreetBoxCode)
        {


            SqlConnection connection = Connection;
            SqlCommand sqlCommand = new SqlCommand("E_StreetBoxPhuse_DeleteX", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iStreetBoxCode", StreetBoxCode));
            sqlCommand.Transaction = Transaction;


            try
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("exec");
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("return true ");
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.Deletee : {0}", ex1.Message));
                return false;
            }
        }

        //MEDHAT //SendFromLocalToAccess
        public static EStreetBoxPhuse SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxXCode = new Guid(reader["StreetBoxXCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.PhuseXCode = new Guid(reader["PhuseXCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                sbp.Code = -1;
            }
            reader.Close();
            connection.Close();

            return sbp;
        }

        //MEDHAT
        public static EStreetBoxPhuse SelectByPhuseXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByPhuseXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxXCode = new Guid(reader["StreetBoxXCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.PhuseXCode = new Guid(reader["PhuseXCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                sbp.Code = -1;
            }
            reader.Close();
            connection.Close();

            return sbp;
        }

        //ASHKTORAB
        public static EStreetBoxPhuse SelectByXCode(SqlTransaction Transaction, SqlConnection Connection, Guid XCode)
        {
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = Transaction;
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxXCode = new Guid(reader["StreetBoxXCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                sbp.PhuseXCode = new Guid(reader["PhuseXCode"].ToString());
            }
            reader.Close();
            //connection.Close();

            return sbp;
        }

        //ASHKTORAB //ShareOnServer
        public static EStreetBoxPhuse SelectByXCodeForDesign(Guid XCode, SqlConnection Connection, SqlTransaction Transaction)
        {
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = Transaction;
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxXCode = new Guid(reader["StreetBoxXCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                sbp.PhuseXCode = new Guid(reader["PhuseXCode"].ToString());
            }
            else
                sbp.Code = -1;

            reader.Close();
            //connection.Close();

            return sbp;
        }


        public static EStreetBoxPhuse SelectByCodeForLocal(int Code, SqlTransaction Transaction, SqlConnection Connection)
        {
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBoxPhuse_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = Transaction;
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxXCode = new Guid(reader["StreetBoxXCode"].ToString());
                sbp.XCode = new Guid(reader["XCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                sbp.PhuseXCode = new Guid(reader["PhuseXCode"].ToString());
            }
            else
                sbp.Code = -1;

            reader.Close();
            //connection.Close();

            return sbp;
        }


        //MEDHAT
        public static DataTable SelectByStreetBoxXCode(Guid StreetBoxCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxCode));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        //ShareOnServer
        public static DataTable SelectByStreetBoxXCode(Guid StreetBoxCode, SqlTransaction Transaction, SqlConnection Connection)
        {
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = Transaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxCode));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable SelectByStreetBoxXCodeType(Guid StreetBoxCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBoxPhuse_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iStreetBoxXCode", StreetBoxCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPhuseType", Type));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iPhuseType", type));


            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~//
        //Hatami//MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _Connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_StreetBoxPhuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            //ed.WriteMessage("~~>>~~Streetbox phuse xcode :{0} \n", XCode);
            command.Parameters.Add(new OleDbParameter("iStreetBoxCode", StreetBoxCode));
            //ed.WriteMessage("Streetboxcode :{0} \n", StreetBoxCode);
            //command.Parameters.Add(new OleDbParameter("iStreetBoxXCode", StreetBoxXCode));
            //ed.WriteMessage("--<<--StreetboxXcode :{0} \n", StreetBoxXCode);
            command.Parameters.Add(new OleDbParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new OleDbParameter("iPhuseCode", PhuseCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iPhuseType", PhuseType));
            command.Parameters.Add(new OleDbParameter("iiIOType", IOType));

            try
            {
                Code = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_StreetBoxPhuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iStreetBoxCode", StreetBoxCode));
            command.Parameters.Add(new OleDbParameter("iFeederNum", FeederNum));
            command.Parameters.Add(new OleDbParameter("iPhuseCode", PhuseCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iPhuseType", PhuseType));
            command.Parameters.Add(new OleDbParameter("iiIOType", IOType));
            int OldCode = Code;
            try
            {
                Code = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBoxPhuse.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //Hatami 
        public static DataTable AccessSelectByStreetBoxCode(int StreetBoxCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iStreetBoxCode", StreetBoxCode));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }


        public static DataTable AccessSelectByStreetBoxCodeForConvertor(int StreetBoxCode,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iStreetBoxCode", StreetBoxCode));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        //status report
        public static DataTable AccessSelectByStreetBoxCode(int StreetBoxCode, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_StreetBoxPhuse_SelectByStreetBoxCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iStreetBoxCode", StreetBoxCode));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        //Hatami
        public static EStreetBoxPhuse AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_StreetBoxPhuse_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EStreetBoxPhuse sbp = new EStreetBoxPhuse();
            if (reader.Read())
            {
                sbp.Code = Convert.ToInt32(reader["Code"].ToString());
                sbp.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"].ToString());
                sbp.FeederNum = Convert.ToInt32(reader["FeederNum"].ToString());
                sbp.Comment = reader["Comment"].ToString();
                sbp.PhuseType = Convert.ToByte(reader["PhuseType"].ToString());
                sbp.PhuseCode = Convert.ToInt32(reader["PhuseCode"].ToString());
            }
            reader.Close();
            connection.Close();

            return sbp;
        }
    }
}
