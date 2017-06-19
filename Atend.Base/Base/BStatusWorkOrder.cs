using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using Atend.Control;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base
{
    public class BStatusWorkOrder
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int equipStatusCode;
        public int EquipStatusCode
        {
            get { return equipStatusCode; }
            set { equipStatusCode = value; }
        }

        private int workOrderCode;
        public int WorkOrderCode
        {
            get { return workOrderCode; }
            set { workOrderCode = value; }
        }


        //~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Status_WorkOrder_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iEquipStatusCode", EquipStatusCode));
            command.Parameters.Add(new SqlParameter("iWorkOrderCode", WorkOrderCode));
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
                ed.WriteMessage(string.Format(" ERROR BStatusWorkOrder.Insert{0}\n", ex1.Message));
                connection.Close();
                return false;
            }


        }

        public bool UpdateX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Status_WorkOrder_Update";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iEquipStatusCode", EquipStatusCode));
            command.Parameters.Add(new SqlParameter("iWorkOrderCode", WorkOrderCode));
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
                ed.WriteMessage(string.Format(" ERROR BStatusWorkOrder.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteX(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_Status_WorkOrder_Delete";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BStatusWorkOrder.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteXWithEquipStatusCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Status_WorkOrder_DeleteWithEquipStatus",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR BStatusWorkOrder.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Status_WorkOrder_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static BStatusWorkOrder SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Status_WorkOrder_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BStatusWorkOrder product = new BStatusWorkOrder();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.EquipStatusCode = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["EquipStatusCode"].ToString());
                product.WorkOrderCode= Convert.ToInt32(dsProduct.Tables[0].Rows[0]["WorkOrderCode"].ToString());
            }
            else
            {
                product.Code = -1;
            }
            return product;
        }

        public static DataTable SelectByWorkOrderCode(int WorkOrderCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Status_WorkOrder_SelectByWorkOrderCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iWorkOrderCode", WorkOrderCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static DataTable SelectByEquipStatusCode(int EquipStatusCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Status_WorkOrder_SelectByStatusEquipCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iEquipStatusCode", EquipStatusCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        

    }
}
