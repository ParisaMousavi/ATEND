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
   public  class DWeatherConditionType
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

       //public static DWeatherConditionType SelectByCode(int Code)
       //{
       //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlCommand command = new SqlCommand("D_WeatherConditionType_Select", connection);
       //    command.CommandType = CommandType.StoredProcedure;
       //    command.Parameters.Add(new SqlParameter("iCode", Code));

       //    connection.Open();
       //    SqlDataReader reader = command.ExecuteReader();
       //    DWeatherConditionType weatherType = new DWeatherConditionType();
       //    if (reader.Read())
       //    {
       //        weatherType.Code = Convert.ToInt32(reader["Code"]);
       //        weatherType.Name = reader["Name"].ToString();
       //    }
       //    reader.Close();
       //    connection.Close();
       //    return weatherType;
       //}
       //public static DataTable SelectAll()
       //{
       //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
       //    SqlDataAdapter adapter = new SqlDataAdapter("D_WeatherConditionType_Select", connection);
       //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
       //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
       //    DataSet dsWeatherConditionType = new DataSet();
       //    adapter.Fill(dsWeatherConditionType);
       //    return dsWeatherConditionType.Tables[0];
       //}
       //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access PArt~~~~~~~~~~~~~~~~~
       public static DWeatherConditionType AccessSelectByCode(int Code)
       {
           OleDbConnection  connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand command = new OleDbCommand("D_WeatherConditionType_Select", connection);
           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add(new OleDbParameter("iCode", Code));

           connection.Open();
           OleDbDataReader reader = command.ExecuteReader();
           DWeatherConditionType weatherType = new DWeatherConditionType();
           if (reader.Read())
           {
               weatherType.Code = Convert.ToInt32(reader["Code"]);
               weatherType.Name = reader["Name"].ToString();
           }
           reader.Close();
           connection.Close();
           return weatherType;
       }
       public static DataTable AccessSelectAll()
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_WeatherConditionType_Select", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
           DataSet dsWeatherConditionType = new DataSet();
           adapter.Fill(dsWeatherConditionType);
           return dsWeatherConditionType.Tables[0];
       }
    }
}
