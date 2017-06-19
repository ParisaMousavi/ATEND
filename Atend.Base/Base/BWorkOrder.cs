using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base
{
    public class BWorkOrder
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int parentCode;
        public int ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int aCode;
        public int ACode
        {
            get { return aCode; }
            set { aCode = value; }
        }



        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("B_WorkOrder_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iParentCode", ParentCode));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iACode", ACode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BWorkOrder.Insert {0}\n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //public bool AccessInsert()
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

        //    OleDbCommand Command = new OleDbCommand("B_WorkOrder_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iParentCode", ParentCode));
        //    Command.Parameters.Add(new SqlParameter("iName", Name));
        //    Command.Parameters.Add(new SqlParameter("iACode", ACode));
        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BWorkOrder.AccessInsert {0}\n", ex1.Message));


        //        Connection.Close();
        //        return false;
        //    }
        //}

        public bool Update()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("B_WorkOrder_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iParentCode", ParentCode));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iACode", ACode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BWorkOrder.Update {0}\n", ex1.Message));


                Connection.Close();
                return false;
            }
        }

        //public bool AccessUpdate()
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

        //    OleDbCommand Command = new OleDbCommand("B_WorkOrder_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iParentCode", ParentCode));
        //    Command.Parameters.Add(new SqlParameter("iName", Name));
        //    Command.Parameters.Add(new SqlParameter("iACode", ACode));
        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BWorkOrder.AccessUpdate {0}\n", ex1.Message));


        //        Connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("B_WorkOrder_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BWorkOrder.Delete {0}\n", ex1.Message));


                Connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

            OleDbCommand Command = new OleDbCommand("B_WorkOrder_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BWorkOrder.AccessDelete {0}\n", ex1.Message));


                Connection.Close();
                return false;
            }
        }

        public static BWorkOrder SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("B_WorkOrder_Select", Connection);
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            BWorkOrder workOrder = new BWorkOrder();
            if (reader.Read())
            {
                workOrder.Code = Convert.ToInt32(reader["Code"].ToString());
                workOrder.parentCode = Convert.ToInt32(reader["parentCode"].ToString());
                workOrder.ACode = Convert.ToInt32(reader["ACode"].ToString());
                workOrder.Name = reader["Name"].ToString();
            }

            Connection.Close();
            reader.Close();
            return workOrder;
        }

        public static BWorkOrder SelectByACode(int ACode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("B_WorkOrder_SelectByACode", Connection);
            Command.Parameters.Add(new SqlParameter("iACode", ACode));
            Command.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            BWorkOrder workOrder = new BWorkOrder();
            if (reader.Read())
            {
                workOrder.Code = Convert.ToInt32(reader["Code"].ToString());
                workOrder.parentCode = Convert.ToInt32(reader["parentCode"].ToString());
                workOrder.ACode = Convert.ToInt32(reader["ACode"].ToString());
                workOrder.Name = reader["Name"].ToString();
            }
            else
            {
                workOrder.code = -1;
            }

            Connection.Close();
            reader.Close();
            return workOrder;
        }

        public static BWorkOrder SelectByACode(int ACode, SqlConnection _connectionLocal)
        {
            SqlConnection Connection = _connectionLocal;
            SqlCommand Command = new SqlCommand("B_WorkOrder_SelectByACode", Connection);
            Command.Parameters.Add(new SqlParameter("iACode", ACode));
            Command.CommandType = CommandType.StoredProcedure;
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            BWorkOrder workOrder = new BWorkOrder();
            if (reader.Read())
            {
                workOrder.Code = Convert.ToInt32(reader["Code"].ToString());
                workOrder.parentCode = Convert.ToInt32(reader["parentCode"].ToString());
                workOrder.ACode = Convert.ToInt32(reader["ACode"].ToString());
                workOrder.Name = reader["Name"].ToString();
            }
            else
            {
                workOrder.code = -1;
            }

            //Connection.Close();
            reader.Close();
            return workOrder;
        }

        //public static BWorkOrder AccessSelectByCode(int Code)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

        //    OleDbCommand Command = new OleDbCommand("B_WorkOrder_Select", Connection);
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Connection.Open();
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    BWorkOrder workOrder = new BWorkOrder();
        //    if (reader.Read())
        //    {
        //        workOrder.Code = Convert.ToInt32(reader["Code"].ToString());
        //        workOrder.parentCode = Convert.ToInt32(reader["parentCode"].ToString());
        //        workOrder.ACode = Convert.ToInt32(reader["ACode"].ToString());
        //        workOrder.Name = reader["Name"].ToString();
        //    }

        //    Connection.Close();
        //    reader.Close();
        //    return workOrder;
        //}

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsWorkOrder = new DataSet();
            adapter.Fill(dsWorkOrder);
            return dsWorkOrder.Tables[0];
        }

        //frmDrawAirPost
        public static DataTable SelectChilds()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_SelectChild", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWorkOrder = new DataSet();
            adapter.Fill(dsWorkOrder);
            return dsWorkOrder.Tables[0];
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

            OleDbDataAdapter adapter = new OleDbDataAdapter("B_WorkOrder_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", 0));
            DataSet dsWorkOrder = new DataSet();
            adapter.Fill(dsWorkOrder);
            return dsWorkOrder.Tables[0];
        }

        public static DataTable SelectByParentCode(int ParentCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_SelectByParentCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iParentCode", ParentCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static object Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds.Tables[0];
        }

        public static object SearchParent(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_SearchParent", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds.Tables[0];

        }

        //frmDrawAirPost-frmDrawBranch-frmDrawBreaker
        public static DataTable SelectJoinOrder(int EquipStatusCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_WorkOrder_SelectJionOrder", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iEquipStatusCode", EquipStatusCode));
            DataSet dsWorkOrder = new DataSet();
            adapter.Fill(dsWorkOrder);
            return dsWorkOrder.Tables[0];
        }

    }
}
