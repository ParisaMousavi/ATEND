using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;

using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DUserAccess
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int idAccess;

        public int IdAccess
        {
            get { return idAccess; }
            set { idAccess = value; }
        }

        private int iDUser;

        public int IDUser
        {
            get { return iDUser; }
            set { iDUser = value; }
        }


        //~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//

        //ASHKTORAB
        public bool Insert(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("D_UserAccess_Insert", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;


            _command.Parameters.Add(new SqlParameter("iIDAccess", IdAccess));
            _command.Parameters.Add(new SqlParameter("iIDUser", IDUser));
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                //ed.writeMessage("Befor UserAccess Insert Done Success. \n");

                _command.ExecuteNonQuery();

                //ed.writeMessage("UserAccess Insert Done Success. \n");
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error UserAccess.Insert : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //ASHKTORAB
        public static bool Delete(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int idUser)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("D_UserAccess_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("@iIDUser", idUser));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" D_UserAccess.DeleteX : {0}", ex1.Message));
                return false;
            }
            return true;
        }
        //ASHKTORAB
        public static bool Delete(int idUser)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand _command = new SqlCommand("D_UserAccess_Delete", connection);
            _command.CommandType = CommandType.StoredProcedure;
           

            _command.Parameters.Add(new SqlParameter("@iIDUser", idUser));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" D_UserAccess.DeleteX : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        //ASHKTORAB
        public static DataTable ServerSelectByUserId(int IDUser)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_UserAccess_SelectByIDUser", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIDUser", IDUser));

            DataSet dsAccess = new DataSet();
            adapter.Fill(dsAccess);
            return dsAccess.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~ Local Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


        //MOUSAVI
        public bool InsertX(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("D_UserAccess_Insert", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;


            _command.Parameters.Add(new SqlParameter("iIDAccess", IdAccess));
            _command.Parameters.Add(new SqlParameter("iIDUser", IDUser));
            //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                //ed.writeMessage("Befor UserAccess Insert Done Success. \n");

                _command.ExecuteNonQuery();

                //ed.writeMessage("UserAccess Insert Done Success. \n");
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error UserAccess.InsertX : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //MOUSAVI
        public static bool DeleteX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int idUser)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("D_UserAccess_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("@iIDUser", idUser));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" D_UserAccess.DeleteX : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        public static bool DeleteAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("D_UserAccess_DeleteAll", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;


            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" D_UserAccess.DeleteAll : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        //MOUSAVI
        public static DataTable SelectByUserId(int IDUser)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_UserAccess_SelectByIDUser", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIDUser", IDUser));

            DataSet dsAccess = new DataSet();
            adapter.Fill(dsAccess);
            return dsAccess.Tables[0];
        }

        public static DataTable SelectByUserId(int IDUser, SqlConnection _Connection, SqlTransaction _Transaction)
        {
            SqlConnection connection = _Connection;
            SqlDataAdapter adapter = new SqlDataAdapter("D_UserAccess_SelectByIDUser", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIDUser", IDUser));
            adapter.SelectCommand.Transaction = _Transaction;

            DataSet dsAccess = new DataSet();
            adapter.Fill(dsAccess);
            return dsAccess.Tables[0];
        }

        //public static DataTable SelectChildFirst(int code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Access_SelectChildFirst", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", code));

        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}


        //public static DataTable SelectChildSecond(int code, int SameNameID)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Access_SelectChildSecond", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", code));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iSameNameID", SameNameID));

        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}

        //public static DataTable SelectChildSameName(int code, int sameNameID)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Access_SelectSameNameID", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", code));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iSameNameID", sameNameID));
        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}

        //~~~~~~~~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~//

        //public bool AccessInsert(OleDbTransaction Transaction, OleDbConnection Connection)
        //{
        //    OleDbConnection _connection = Connection;
        //    OleDbCommand _command = new OleDbCommand("D_UserAccess_Insert", _connection);
        //    _command.CommandType = CommandType.StoredProcedure;
        //    _command.Transaction = Transaction;


        //    _command.Parameters.Add(new OleDbParameter("iIDAccess", IdAccess));
        //    _command.Parameters.Add(new OleDbParameter("iIDUser", IDUser));
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    try
        //    {
        //        _command.ExecuteNonQuery();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error UserAccess.AccessInsert : {0} \n", ex1.Message));
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool AccessDelete(OleDbTransaction Transaction, OleDbConnection Connection, int idUser)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection _connection = Connection;
        //    OleDbCommand _command = new OleDbCommand("D_UserAccess_Delete", _connection);
        //    _command.CommandType = CommandType.StoredProcedure;
        //    _command.Transaction = Transaction;

        //    _command.Parameters.Add(new OleDbParameter("@iIDUser", idUser));

        //    try
        //    {
        //        _command.ExecuteNonQuery();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" D_UserAccess.AccessDelete : {0}", ex1.Message));
        //        return false;
        //    }
        //    return true;
        //}

        //public static DataTable AccessSelectRoot()
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("D_Access_SelectRoot", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}

        //public static DataTable AccessSelectChildFirst(int code)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("D_Access_SelectChildFirst", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", code));

        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}

        //public static DataTable AccessSelectChildSecond(int code, int SameNameID)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("D_Access_SelectChildSecond", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", code));
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSameNameID", SameNameID));

        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}


        //public static DataTable AccessSelectChildSameName(int code, int sameNameID)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("D_Access_SelectSameNameID", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", code));
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSameNameID", sameNameID));
        //    DataSet dsAccess = new DataSet();
        //    adapter.Fill(dsAccess);
        //    return dsAccess.Tables[0];
        //}

    }
}
