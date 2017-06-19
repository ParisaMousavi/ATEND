using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
   public class DWeatherType
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
        private Boolean isSelected;

        public Boolean IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        private Int32 designCode;

        public Int32 DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

       // public bool insert()
       // {
       //     SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //     SqlCommand command = new SqlCommand("D_WeatherType_Insert", connection);
       //     command.CommandType = CommandType.StoredProcedure;
       //     command.Parameters.Add(new SqlParameter("iName", Name));
       //     command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
       //     command.Parameters.Add(new SqlParameter("iIsSelected", IsSelected));
       //     try
       //     {
       //         connection.Open();
       //         command.ExecuteNonQuery();
       //         connection.Close();
       //         return true;
       //     }
       //     catch (System.Exception ex1)
       //     {
       //         Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
       //         ed.WriteMessage(string.Format("Error DWeatherType.Insert : {0} \n", ex1.Message));
       //         connection.Close();
       //         return false;
       //     }
       // }

       // public static DataTable SelectAll()
       // {
       //     SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //     SqlDataAdapter adapter = new SqlDataAdapter("D_WeatherType_Select", connection);
       //     adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
       //     adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
       //     DataSet dsWeatherType = new DataSet();
       //     adapter.Fill(dsWeatherType);
       //     return dsWeatherType.Tables[0];
       // }

       //public static DataTable SelectByDesignCode(int DesignCode)
       //{
       //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlDataAdapter adapter = new SqlDataAdapter("D_WeatherType_SelectByDesignCode", connection);
       //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
       //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
       //    DataSet dsWeatherType = new DataSet();
       //    adapter.Fill(dsWeatherType);
       //    return dsWeatherType.Tables[0];
       //}

       //public static bool UpdateSelectedStatus(int Code, int DesignCode, bool IsSelected)
       //{
       //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlCommand Command = new SqlCommand("D_WeatherType_UpdateSelectedstatus", Connection);
       //    Command.CommandType = CommandType.StoredProcedure;
       //    Command.Parameters.Add(new SqlParameter("iCode", Code));
       //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
       //    Command.Parameters.Add(new SqlParameter("iIsSelected", IsSelected));
       //    try
       //    {
       //        Connection.Open();
       //        Command.ExecuteNonQuery();
       //        Connection.Close();
       //        return true;
       //    }
       //    catch 
       //    {
       //        Connection.Close();
       //        return false;
               
       //    }
       //}

       //public static DWeatherType SelectByCode(int Code)
       //{
       //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlCommand Command = new SqlCommand("D_WeatherType_SelectByCode",Connection);
       //    Command.CommandType = CommandType.StoredProcedure;
       //    Command.Parameters.Add(new SqlParameter("iCode", Code));
       //    Connection.Open();
       //    SqlDataReader reader = Command.ExecuteReader();
       //    DWeatherType weatherType = new DWeatherType();
       //    if (reader.Read())
       //    {
       //        weatherType.Code = Convert.ToInt32(reader["Code"]);
       //        weatherType.DesignCode = Convert.ToInt32(reader["DesignCode"]);
       //        weatherType.IsSelected = Convert.ToBoolean(reader["IsSelected"]);
       //    }

       //    return weatherType;

       //}

       //public static DWeatherType SelectByDesignCodeSelectedStatus(int DesignCode, bool IsSelected)
       //{
       //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlCommand Command = new SqlCommand("D_WeatherType_SelectByDesignCodeSelectedStatus",Connection);
       //    Command.CommandType = CommandType.StoredProcedure;
       //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
       //    Command.Parameters.Add(new SqlParameter("iIsSelected", IsSelected));
           
       //    Connection.Open();
       //    SqlDataReader reader = Command.ExecuteReader();
       //    DWeatherType weatherType = new DWeatherType();
       //    if (reader.Read())
       //    {
       //        weatherType.Code = Convert.ToInt32(reader["Code"]);
       //        weatherType.DesignCode = Convert.ToInt32(reader["DesignCode"]);
       //        weatherType.IsSelected = Convert.ToBoolean(reader["IsSelected"]);
       //        weatherType.Name = reader["Name"].ToString();
               
       //    }
       //    reader.Close();
       //    Connection.Close();
       //    return weatherType;
       //}

       //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access PArt~~~~~~~~~~~~~~~~~~~~~~~
       public bool Accessinsert()
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand command = new OleDbCommand("D_WeatherType_Insert", connection);
           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add(new OleDbParameter("iName", Name));
           //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
           command.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));
           try
           {
               connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
               return true;
           }
           catch (System.Exception ex1)
           {
               Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
               ed.WriteMessage(string.Format("Error DWeatherType.Insert : {0} \n", ex1.Message));
               connection.Close();
               return false;
           }
       }

       public static DataTable AccessSelectAll()
       {

           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_WeatherType_Select", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
           DataSet dsWeatherType = new DataSet();
           adapter.Fill(dsWeatherType);
           return dsWeatherType.Tables[0];
       }

       //public static DataTable AccessSelectByDesignCode(int DesignCode)
       //{
       //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
       //    OleDbDataAdapter  adapter = new OleDbDataAdapter("D_WeatherType_SelectByDesignCode", connection);
       //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
       //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
       //    DataSet dsWeatherType = new DataSet();
       //    adapter.Fill(dsWeatherType);
       //    return dsWeatherType.Tables[0];
       //}

       public static bool AccessUpdateSelectedStatus(int Code,int IsSelected)
       {
           OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand Command = new OleDbCommand("D_WeatherType_UpdateSelectedstatus", Connection);
           Command.CommandType = CommandType.StoredProcedure;
           Command.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));

           Command.Parameters.Add(new OleDbParameter("iCode", Code));
           //Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
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

       public static DWeatherType AccessSelectByCode(int Code)
       {
           OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand Command = new OleDbCommand("D_WeatherType_Select", Connection);
           Command.CommandType = CommandType.StoredProcedure;
           Command.Parameters.Add(new OleDbParameter("iCode", Code));
           Connection.Open();
           OleDbDataReader reader = Command.ExecuteReader();
           DWeatherType weatherType = new DWeatherType();
           if (reader.Read())
           {
               weatherType.Code = Convert.ToInt32(reader["Code"]);
               //weatherType.DesignCode = Convert.ToInt32(reader["DesignCode"]);
               weatherType.IsSelected = Convert.ToBoolean(reader["IsSelected"]);
           }

           return weatherType;

       }

       public static DWeatherType AccessSelectBySelectedStatus(bool IsSelected)
       {
           OleDbConnection  Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand Command = new OleDbCommand("D_WeatherType_SelectBySelectedStatus", Connection);
           Command.CommandType = CommandType.StoredProcedure;
           //Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
           Command.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));

           Connection.Open();
           OleDbDataReader reader = Command.ExecuteReader();
           DWeatherType weatherType = new DWeatherType();
           if (reader.Read())
           {
               weatherType.Code = Convert.ToInt32(reader["Code"]);
               //weatherType.DesignCode = Convert.ToInt32(reader["DesignCode"]);
               weatherType.IsSelected = Convert.ToBoolean(reader["IsSelected"]);
               weatherType.Name = reader["Name"].ToString();

           }
           reader.Close();
           Connection.Close();
           return weatherType;
       }


    }
}
