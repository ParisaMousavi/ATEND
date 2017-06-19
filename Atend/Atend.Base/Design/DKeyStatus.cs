using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base.Design
{
    public  class DKeyStatus
    {

        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid nodeCode;
        public Guid NodeCode
        {
            get { return nodeCode; }
            set { nodeCode = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private bool isClosed;
        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }



        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_KeyStatus_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(System.Exception ex)
            {
                ed.WriteMessage("ERROR DKeyStatus.Insert: {0}",ex.Message);
                connection.Close();
                return false;
            }

           return true;
        }

        public bool Insert( OleDbTransaction _transaction , OleDbConnection _connection )
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_KeyStatus_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR DKeyStatus.Insert: {0}", ex.Message);
                connection.Close();
                return false;
            }

            return true;
        }



        public static DKeyStatus SelectByNodeCode(Guid nodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_KeyStatus_SelectByNodeCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iNodeCode", nodeCode));
            DKeyStatus dkeystatuse = new DKeyStatus();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    dkeystatuse.Code = new Guid(reader["Code"].ToString());
                    dkeystatuse.NodeCode = new Guid(reader["NodeCode"].ToString());
                    dkeystatuse.Type = Convert.ToInt32(reader["Type"].ToString());
                    dkeystatuse.IsClosed = Convert.ToBoolean(reader["IsClosed"].ToString());
                }
                else
                {
                    //it means it did not found
                    dkeystatuse.Code = Guid.Empty;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {

                ed.WriteMessage(string.Format(" ERROR DKeyStatus.AccessSelectByNodeCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dkeystatuse;
        }


        public static DKeyStatus SelectByCode(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_KeyStatus_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            DKeyStatus dkeystatuse = new DKeyStatus();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    dkeystatuse.Code = new Guid(reader["Code"].ToString());
                    dkeystatuse.NodeCode = new Guid(reader["NodeCode"].ToString());
                    dkeystatuse.Type = Convert.ToInt32(reader["Type"].ToString());
                    dkeystatuse.IsClosed = Convert.ToBoolean(reader["IsClosed"].ToString());
                }
                else
                {
                    //it means it did not found
                    dkeystatuse.Code = Guid.Empty;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {

                ed.WriteMessage(string.Format(" ERROR DKeyStatus.AccessSelectByCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dkeystatuse;
        }


        public bool Update(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_KeyStatus_Update", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iIsClosed", IsClosed));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR DKeyStatus.Update: {0}", ex.Message);
                connection.Close();
                return false;
            }

            return true;
        }


    }
}
