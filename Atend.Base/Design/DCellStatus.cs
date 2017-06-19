using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DCellStatus
    {
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        private Guid code;

        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid jackPanelCode;

        public Guid JackPanelCode
        {
            get { return jackPanelCode; }
            set { jackPanelCode = value; }
        }

        private Guid cellCode;

        public Guid CellCode
        {
            get { return cellCode; }
            set { cellCode = value; }
        }

        private bool isClosed;

        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        /*
        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_CellStatus_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            code = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new SqlParameter("iCellCode", CellCode));
            Command.Parameters.Add(new SqlParameter("iIsClosed", IsClosed));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
                
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DCellStatus.Insert : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction , SqlConnection _connection)
        {
            SqlConnection Connection = _connection;
            SqlCommand Command = new SqlCommand("D_CellStatus_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            code = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new SqlParameter("iCellCode", CellCode));
            Command.Parameters.Add(new SqlParameter("iIsClosed", IsClosed));

            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                //Connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DCellStatus.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }

        public bool Update()//JackPanelCode and CellCode
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_CellStatus_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            command.Parameters.Add(new SqlParameter("iCellCode", CellCode));
            command.Parameters.Add(new SqlParameter("iIsClosed", IsClosed));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error IN  D_CellStatus.Update " + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool DeleteByCellCode(Guid CellCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_CellStatusCellCode_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCellCode", CellCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteByJackPanelCode(Guid JackPanelCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_CellStatusJackPanelCode_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {

                Connection.Close();
                return false;
            }
        }

        public static DataTable SelectByJackPanelCode(Guid JackPanelCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlDataAdapter command = new SqlDataAdapter("D_CellStatus_SelectByJackPAnelCode", connection);
            command.SelectCommand.CommandType = CommandType.StoredProcedure;
            command.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelCode",JackPanelCode));
            DataSet dscell = new DataSet();
            command.Fill(dscell);
            return dscell.Tables[0];
           
        }
        */
        //~~~~~~~~~~~~~~~~~~ACCESS PART~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool AccessInsert()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_CellStatus_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            code = Guid.NewGuid();
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new OleDbParameter("iCellCode", CellCode));
            Command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DCellStatus.Insert : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("D_CellStatus_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            code = Guid.NewGuid();
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new OleDbParameter("iCellCode", CellCode));
            Command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                Command.ExecuteNonQuery();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DCellStatus.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool AccessUpdate()//JackPanelCode and CellCode
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_CellStatus_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            command.Parameters.Add(new OleDbParameter("iCellCode", CellCode));
            command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error IN  D_CellStatus.Update " + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteByCellCode(Guid CellCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_CellStatusCellCode_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCellCode", CellCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {

                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteByJackPanelCode(Guid JackPanelCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_CellStatusJackPanelCode_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {

                Connection.Close();
                return false;
            }
        }

        //MOUSAVI->drawing delete
        public static bool AccessDeleteByJackPanelCode(Guid JackPanelCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("D_CellStatus_DeleteByJackPanelCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            Command.Transaction = _transaction;
            try
            {
                Command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;

        }

        public static DataTable AccessSelectByJackPanelCode(Guid JackPanelCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter command = new OleDbDataAdapter("D_CellStatus_SelectByJackPAnelCode", connection);
            command.SelectCommand.CommandType = CommandType.StoredProcedure;
            command.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            DataSet dscell = new DataSet();
            command.Fill(dscell);
            return dscell.Tables[0];

        }
    }
}
