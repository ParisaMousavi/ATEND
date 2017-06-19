using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace Atend.Base.Design
{
    public class DWorkOrder
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private int designID;

        public int DesignID
        {
            get { return designID; }
            set { designID = value; }
        }

        private int workOrder;

        public int WorkOrder
        {
            get { return workOrder; }
            set { workOrder = value; }
        }

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //Hatami
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_WorkOrder_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignID", DesignID));
            command.Parameters.Add(new SqlParameter("iWorkOrder", WorkOrder));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in Insert WorkOrder={0}\n",ex.Message);
                connection.Close();
                return false;
            }
        }

        //frmDesignSaveServer
        public bool Insert(SqlTransaction sTransaction,SqlConnection sConnection)
        {
            SqlConnection connection =sConnection;
            SqlCommand command = new SqlCommand("D_WorkOrder_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignID", DesignID));
            command.Parameters.Add(new SqlParameter("iWorkOrder", WorkOrder));
            command.Transaction = sTransaction;
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in Insert WorkOrder={0}\n", ex.Message);
                return false;
            }
        }

        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_WorkOrder_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iDesignID", DesignID));
            command.Parameters.Add(new SqlParameter("iWorkOrder", WorkOrder));
            command.Parameters.Add(new SqlParameter("iID", ID));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in Update WorkOrder={0}\n", ex.Message);
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_WorkOrder_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iDesignID", id));

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
                ed.WriteMessage(string.Format(" ERROR DWorOrder.Deletet : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }


        //frmDesignSaveServer
        public static bool Delete(int DesignId,SqlTransaction sTransaction,SqlConnection sConnection)
        {
            SqlConnection connection = sConnection;
            SqlCommand command = new SqlCommand("D_WorkOrder_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iDesignID", DesignId));
            command.Transaction = sTransaction;

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DWorOrder.Deletet : {0} \n", ex1.Message));
                return false;
            }
        }


    }
}
