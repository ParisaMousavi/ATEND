using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Design
{
    public class DStatusReport
    {
        public DStatusReport()
        {
 
        }
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int designId;
        public int DesignId
        {
            get { return designId; }
            set { designId = value; }
        }

        private int productcode;
        public int ProductCode
        {
            get { return productcode; }
            set { productcode = value; }
        }

        private double count;
        public double Count
        {
            get { return count; }
            set { count = value; }
        }

        private int projectcode;
        public int ProjectCode
        {
            get { return projectcode; }
            set { projectcode = value; }
        }

        private int existance;
        public int Existance
        {
            get { return existance; }
            set { existance = value; }
        }

        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public bool Insert(SqlTransaction _SqlTransaction, SqlConnection _SqlConnection)
        {
            SqlConnection connection = _SqlConnection; //new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_StatusReport_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _SqlTransaction;

            command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            command.Parameters.Add(new SqlParameter("iProjectCode", ProjectCode));
            command.Parameters.Add(new SqlParameter("iExistance", Existance));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nERROR IN D_StatusReport_Insert\n" + ex1.Message);

                //connection.Close();
                return false;
            }
        }

        public static bool DeleteByDesignId(int DesignID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_StatusReport_DeleteByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iDesignId", DesignID));

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
                ed.WriteMessage("\nERROR IN D_StatusReport_Delete\n" + ex1.Message);

                connection.Close();
                return false;
            }
        }

        //frmSaveDesignServer
        public static bool DeleteByDesignId( SqlTransaction _transaction , SqlConnection _connection, int DesignID)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("D_StatusReport_DeleteByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iDesignId", DesignID));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nERROR IN D_StatusReport_Delete\n" + ex1.Message);
                //connection.Close();
                return false;
            }
        }

        public static DStatusReport SelectByID(int ID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_StatusReport_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iId", ID));
            SqlDataReader reader = command.ExecuteReader();
            DStatusReport Reoprt = new DStatusReport();
            if (reader.Read())
            {
                Reoprt.Code = Convert.ToInt32(reader["Code"].ToString());
                Reoprt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Reoprt.Count = Convert.ToDouble(reader["Count"].ToString());
                Reoprt.ProjectCode = Convert.ToInt32(reader["ProjectCode"].ToString());
                Reoprt.Existance = Convert.ToInt32(reader["Existance"].ToString());
                Reoprt.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                Reoprt.Id = Convert.ToInt32(reader["Id"].ToString());
            }
            reader.Close();
            connection.Close();

            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", Reoprt.ProductCode, Reoprt.Size, Reoprt, Reoprt.Type));


            return Reoprt;
        }

        public static DStatusReport SelectByDesignID(int DesignID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_StatusReport_SelectByDesignID", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iDesignID", DesignID));
            SqlDataReader reader = command.ExecuteReader();
            DStatusReport Reoprt = new DStatusReport();
            if (reader.Read())
            {
                Reoprt.Code = Convert.ToInt32(reader["Code"].ToString());
                Reoprt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Reoprt.Count = Convert.ToDouble(reader["Count"].ToString());
                Reoprt.ProjectCode = Convert.ToInt32(reader["ProjectCode"].ToString());
                Reoprt.Existance = Convert.ToInt32(reader["Existance"].ToString());
                Reoprt.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                Reoprt.Id = Convert.ToInt32(reader["Id"].ToString());
            }
            reader.Close();
            connection.Close();

            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", Reoprt.ProductCode, Reoprt.Size, Reoprt, Reoprt.Type));


            return Reoprt;
        }

        public static DataTable SelectByDesignID(SqlTransaction _transaction , SqlConnection _connection , int DesignId)
        {
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("D_StatusReport_SelectByDesignID", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignID", DesignId));
            DataSet dsReport = new DataSet();
            adapter.Fill(dsReport);
            return dsReport.Tables[0];

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_StatusReport_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", -1));

            DataSet dsReport = new DataSet();
            adapter.Fill(dsReport);
            return dsReport.Tables[0];

        }


    }
}
