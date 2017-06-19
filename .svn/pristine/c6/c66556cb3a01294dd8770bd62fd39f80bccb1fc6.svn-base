using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DNumberingSystem
    {

        private Guid code;

        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }


        private int designCode;

        public int DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private string partAAbreviation;

        public string PartAAbreviation
        {
            get { return partAAbreviation; }
            set { partAAbreviation = value; }
        }

        private string partBAbreviation;

        public string PartBAbreviation
        {
            get { return partBAbreviation; }
            set { partBAbreviation = value; }
        }

        private int startNo;

        public int StartNo
        {
            get { return startNo; }
            set { startNo = value; }
        }

        private int nodeCount;

        public int NodeCount
        {
            get { return nodeCount; }
            set { nodeCount = value; }
        }


        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


        /*public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_NumberingSystem_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignCode",DesignCode));
            command.Parameters.Add(new SqlParameter("iPartAAbreviation",PartAAbreviation));
            command.Parameters.Add(new SqlParameter("iPartBAbreviation",PartBAbreviation));
            command.Parameters.Add(new SqlParameter("iStartNo",StartNo));
            command.Parameters.Add(new SqlParameter("iNodeCount",NodeCount));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch(System.Exception ex)
            {
                ed.WriteMessage("Error in D_NumberingSystem_Inser :"+ex.Message+"\n");
                connection.Close();
                return false;
            }

        }

        public bool UpdateByCodeDeignCode()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_NumberingSystem_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new SqlParameter("iPartAAbreviation", PartAAbreviation));
            command.Parameters.Add(new SqlParameter("iPartBAbreviation", PartBAbreviation));
            command.Parameters.Add(new SqlParameter("iStartNo", StartNo));
            command.Parameters.Add(new SqlParameter("iNodeCount", NodeCount));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_NumberingSystem_Update :" + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool Delete(Guid code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_NumberingSystem_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", code));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch(SystemException ex)
            {
                ed.WriteMessage("Error in DNumberingSystem.Delete: "+ex.Message+"\n");
                Connection.Close();
                return false;
            }
        }

        public static DNumberingSystem SelectByCodeDesignCode(Guid Code, int DesignCode)
        {

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_NumberingSystem_SelectByCodeDedesignCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            DNumberingSystem dNumberingSystem = new DNumberingSystem();
            if (reader.Read())
            {
                dNumberingSystem.Code = new Guid(reader["Code"].ToString());
                dNumberingSystem.NodeCount = Convert.ToInt32(reader["NodeCount"]);
                dNumberingSystem.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dNumberingSystem.PartAAbreviation=reader["PartAAbreviation"].ToString();
                dNumberingSystem.PartBAbreviation=reader["PArtBAbreviation"].ToString();
                dNumberingSystem.StartNo = Convert.ToInt32(reader["StartNo"]);
               

            }
            reader.Close();
            Connection.Close();
            return dNumberingSystem;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_NumberingSystem_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];
           
        }

        public static DNumberingSystem SelectByCode(Guid Code)
        {

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("D_NumberingSystem_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            DNumberingSystem dNumberingSystem = new DNumberingSystem();
            if (reader.Read())
            {
                dNumberingSystem.Code = new Guid(reader["Code"].ToString());
                dNumberingSystem.NodeCount = Convert.ToInt32(reader["NodeCount"]);
                dNumberingSystem.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dNumberingSystem.PartAAbreviation = reader["PartAAbreviation"].ToString();
                dNumberingSystem.PartBAbreviation = reader["PArtBAbreviation"].ToString();
                dNumberingSystem.StartNo = Convert.ToInt32(reader["StartNo"]);


            }
            reader.Close();

            Connection.Close();
            return dNumberingSystem;
          
        }
        */
        //~~~~~~~~~~~~~~~AccessPart
        public bool AccessInsert()
        {
            OleDbConnection  connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_NumberingSystem_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iPartAAbreviation", PartAAbreviation));
            command.Parameters.Add(new OleDbParameter("iPartBAbreviation", PartBAbreviation));
            command.Parameters.Add(new OleDbParameter("iStartNo", StartNo));
            command.Parameters.Add(new OleDbParameter("iNodeCount", NodeCount));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_NumberingSystem_Inser :" + ex.Message + "\n");
                connection.Close();
                return false;
            }

        }

        public bool AccessUpdateByCodeDeignCode()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_NumberingSystem_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iPartAAbreviation", PartAAbreviation));
            command.Parameters.Add(new OleDbParameter("iPartBAbreviation", PartBAbreviation));
            command.Parameters.Add(new OleDbParameter("iStartNo", StartNo));
            command.Parameters.Add(new OleDbParameter("iNodeCount", NodeCount));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_NumberingSystem_Update :" + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(Guid code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_NumberingSystem_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", code));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (SystemException ex)
            {
                ed.WriteMessage("Error in DNumberingSystem.Delete: " + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public static DNumberingSystem AccessSelectByCodeDesignCode(Guid Code, int DesignCode)
        {

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_NumberingSystem_SelectByCodeDedesignCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            DNumberingSystem dNumberingSystem = new DNumberingSystem();
            if (reader.Read())
            {
                dNumberingSystem.Code = new Guid(reader["Code"].ToString());
                dNumberingSystem.NodeCount = Convert.ToInt32(reader["NodeCount"]);
                dNumberingSystem.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dNumberingSystem.PartAAbreviation = reader["PartAAbreviation"].ToString();
                dNumberingSystem.PartBAbreviation = reader["PArtBAbreviation"].ToString();
                dNumberingSystem.StartNo = Convert.ToInt32(reader["StartNo"]);


            }
            reader.Close();
            Connection.Close();
            return dNumberingSystem;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_NumberingSystem_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        public static DNumberingSystem AccessSelectByCode(Guid Code)
        {

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand  Command = new OleDbCommand("D_NumberingSystem_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            DNumberingSystem dNumberingSystem = new DNumberingSystem();
            if (reader.Read())
            {
                dNumberingSystem.Code = new Guid(reader["Code"].ToString());
                dNumberingSystem.NodeCount = Convert.ToInt32(reader["NodeCount"]);
                dNumberingSystem.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dNumberingSystem.PartAAbreviation = reader["PartAAbreviation"].ToString();
                dNumberingSystem.PartBAbreviation = reader["PArtBAbreviation"].ToString();
                dNumberingSystem.StartNo = Convert.ToInt32(reader["StartNo"]);


            }
            reader.Close();

            Connection.Close();
            return dNumberingSystem;

        }




    }
}
