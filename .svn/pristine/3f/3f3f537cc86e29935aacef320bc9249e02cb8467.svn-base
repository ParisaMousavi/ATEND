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
    public class DNode
    {

        public DNode()
        {
        }

        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Node_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            Code = Guid.NewGuid();
            //ed.WriteMessage("Code:{0}\nNumber:{1}\nProductCode:{2}\n", Code, Number, ProductCode);
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            try
            {
                command.ExecuteNonQuery();
                //ed.WriteMessage(string.Format("DNode.AccessInsert Done. \n"));
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DNode.AccessInsert : {0} ", ex1.Message));
                return false;
            }

            return true;
        }

        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbCommand command = new OleDbCommand("D_Node_Update", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            try
            {
                command.ExecuteNonQuery();

            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DNode.AccessUpdate : {0} ", ex1.Message));
                return false;
            }

            return true;
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Node_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR DNode.AccessUpdate {0}\n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;


        }

        public static bool AccessDelete(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Node_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DNode.AccessDelete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            return true;
        }

        //MOUSAVI->drawing delete
        public static bool AccessDelete(Guid Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Node_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DNode.AccessDelete : {0} \n", ex1.Message));
                return false;
            }

            return true;
        }

        //frmEditDrawPole
        public static DNode AccessSelectByCode(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Node_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            DNode dNode = new DNode();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    dNode.Code = new Guid(reader["Code"].ToString());
                    dNode.Number = reader["Number"].ToString();
                    dNode.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    dNode.Height = Convert.ToDouble(reader["Height"].ToString());
                }
                else
                {
                    //it means it did not found
                    dNode.productCode = -10;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {

                ed.WriteMessage(string.Format(" ERROR DNode.AccessSelectByCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dNode;
        }

        //CALCULATION HATAMI
        public static DNode AccessSelectByCode(Guid Code, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("D_Node_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            DNode dNode = new DNode();
            try
            {
                //connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    dNode.Code = new Guid(reader["Code"].ToString());
                    dNode.Number = reader["Number"].ToString();
                    dNode.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    dNode.Height = Convert.ToDouble(reader["Height"].ToString());
                }
                else
                {
                    //it means it did not found
                    dNode.productCode = -10;
                    dNode.Code = Guid.Empty;
                }

                reader.Close();
                //connection.Close();

            }
            catch (System.Exception ex1)
            {

                ed.WriteMessage(string.Format(" ERROR DNode.AccessSelectByCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dNode;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Node_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsNode = new DataSet();
            adapter.Fill(dsNode);

            return dsNode.Tables[0];
        }

        //StatusReport
        public static DataTable AccessSelectAll(OleDbConnection _conncetion)
        {
            OleDbConnection connection = _conncetion;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Node_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsNode = new DataSet();
            adapter.Fill(dsNode);
            return dsNode.Tables[0];
        }

    }
}
