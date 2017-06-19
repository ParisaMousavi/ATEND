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
    public class XXDLayer
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int designCode;

        public int DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private byte isWeek;

        public byte IsWeek
        {
            get { return isWeek; }
            set { isWeek = value; }
        }


        private bool isGround;

        public bool IsGround
        {
            get { return isGround; }
            set { isGround = value; }
        }

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;



        //public bool Insert()
        //{
            
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlCommand command = new SqlCommand("D_Layer_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;


        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", Atend.Control.Common.SelectedDesignCode));
        //    command.Parameters.Add(new SqlParameter("iIsWeek", IsWeek));
        //    command.Parameters.Add(new SqlParameter("iIsGround", IsGround));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();

        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error In Layer Inserted :{0}\n", ex.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool AccessInsert()
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Layer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iIsWeek", IsWeek));
            command.Parameters.Add(new OleDbParameter("iIsGround", IsGround));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In Layer AccessInserted :{0}\n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlCommand command = new SqlCommand("D_Layer_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;


        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", Atend.Control.Common.SelectedDesignCode));
        //    command.Parameters.Add(new SqlParameter("iIsWeek", IsWeek));
        //    command.Parameters.Add(new SqlParameter("iIsGround", IsGround));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error in Layer Updated :{0}\n", ex.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool AccessUpdate()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Layer_Update", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iIsWeek", IsWeek));
            command.Parameters.Add(new OleDbParameter("iIsGround", IsGround));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error in Layer AccessUpdated :{0}\n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //public static bool Delete(int Code)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlCommand command = new SqlCommand("D_Layer_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error In Layer Deleted :{0}\n", ex.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool AccessDelete(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Layer_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In Layer AccessDeleted :{0}\n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //public static DLayer SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlCommand command = new SqlCommand("D_Layer_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    DLayer layer = new DLayer();
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        layer.Code = Convert.ToInt32(reader["Code"]);
        //        layer.Name = reader["Name"].ToString();
        //        layer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        layer.IsWeek = Convert.ToByte(reader["IsWeek"]);
        //        layer.IsGround=Convert.ToBoolean(reader["IsGround"]);

        //    }
        //    reader.Close();
        //    connection.Close();
        //    return layer;
        //}

        public static XXDLayer AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Layer_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            XXDLayer layer = new XXDLayer();
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                layer.Code = Convert.ToInt32(reader["Code"]);
                layer.Name = reader["Name"].ToString();
                layer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                layer.IsWeek = Convert.ToByte(reader["IsWeek"]);
                layer.IsGround = Convert.ToBoolean(reader["IsGround"]);

            }
            reader.Close();
            connection.Close();
            return layer;
        }

        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Layer_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
        //    DataSet dsLayer = new DataSet();
        //    adapter.Fill(dsLayer);
        //    return dsLayer.Tables[0];

        //}

       
        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Layer_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsLayer = new DataSet();
            adapter.Fill(dsLayer);
            return dsLayer.Tables[0];
        }

        //public static DataTable SelectByDesignCode(int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Layer_SelectByDesignCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    DataSet dsLayer = new DataSet();
        //    adapter.Fill(dsLayer);
        //    return dsLayer.Tables[0];

        //}

        public static DataTable AccessSelectByDesignCode(int DesignCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Layer_SelectByDesignCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            DataSet dsLayer = new DataSet();
            adapter.Fill(dsLayer);
            return dsLayer.Tables[0];

        }



    }
}
