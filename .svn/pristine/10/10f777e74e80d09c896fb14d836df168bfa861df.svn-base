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
   public class DWeather
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }


        private double temp;

       public double Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        private double windSpeed;

        public double  WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }

        private double iceDiagonal;

        public double IceDiagonal
        {
            get { return iceDiagonal; }
            set { iceDiagonal = value; }
        }
        private double saftyFactor;

        public double SaftyFactor
        {
            get { return saftyFactor; }
            set { saftyFactor = value; }
        }

        private byte conditionCode;

        public byte ConditionCode
        {
            get { return conditionCode; }
            set { conditionCode = value; }
        }

        private int typeCode;

        public int TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
        }

   //    public bool insert()
   //{
   //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //    SqlCommand command = new SqlCommand("D_Weather_Insert", connection);
   //    command.CommandType = CommandType.StoredProcedure;
   //    command.Parameters.Add(new SqlParameter("iTemp", Temp));
   //    command.Parameters.Add(new SqlParameter("iWindSpeed", WindSpeed));
   //    command.Parameters.Add(new SqlParameter("iIceDiagonal", IceDiagonal));
   //    command.Parameters.Add(new SqlParameter("iSaftyFactor", SaftyFactor));
   //    command.Parameters.Add(new SqlParameter("iConditionCode", ConditionCode));
   //    command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));

   //    try
   //    {
   //        connection.Open();
   //        command.ExecuteNonQuery();
   //        connection.Close();
   //        return true;
   //    }
   //    catch (System.Exception ex1)
   //    {
   //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
   //        ed.WriteMessage(string.Format("Error EWeather.Insert : {0} \n", ex1.Message));
   //        connection.Close();
   //        return false;
   //    }

   //}

   //    public bool Update()
   //    {
   //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //        SqlCommand command = new SqlCommand("D_Weather_Update", connection);
   //        command.CommandType = CommandType.StoredProcedure;
   //        command.Parameters.Add(new SqlParameter("iCode",Code));
   //        command.Parameters.Add(new SqlParameter("iTemp", Temp));
   //        command.Parameters.Add(new SqlParameter("iWindSpeed", WindSpeed));
   //        command.Parameters.Add(new SqlParameter("iIceDiagonal", IceDiagonal));
   //        command.Parameters.Add(new SqlParameter("iSaftyFactor", SaftyFactor));
   //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

   //        ed.WriteMessage(string.Format("Code:={0},temp:={1},WindSpeed:={2},iIceDiagonal:={3},iSaftyFactor:={4}",code.ToString(),Temp.ToString(),WindSpeed.ToString(),IceDiagonal.ToString(),SaftyFactor.ToString()));
   //        try
   //        {
   //            connection.Open();
   //            command.ExecuteNonQuery();
   //            connection.Close();
   //            return true;
   //        }
   //        catch (System.Exception ex1)
   //        {
   //            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
   //            ed.WriteMessage(string.Format(" ERROR Weather.Update: {0} \n", ex1.Message));

   //            connection.Close();
   //            return false;
   //        }
   //    }
   //    public static  DWeather SelectBySelectByDesignCodeIsSelectedConditionCode(int DesignCode, Boolean IsSelected,int ConditionCode)
   //    {
   //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
   //        //ed.WriteMessage("Start SelectBySelectByDesignCodeIsSelectedConditionCode\n ");
   //        SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //        SqlCommand sqlCommand = new SqlCommand("D_Weather_SelectByDesignCodeIsSelectedConditionCode", Connection);
   //        sqlCommand.CommandType = CommandType.StoredProcedure;
   //        sqlCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
   //        sqlCommand.Parameters.Add(new SqlParameter("iIsSelected", IsSelected));
   //        sqlCommand.Parameters.Add(new SqlParameter("iConditionCode", ConditionCode ));
   //                   Connection.Open();
   //        SqlDataReader reader = sqlCommand.ExecuteReader();
   //        DWeather Weather = new DWeather();
   //        if (reader.Read())
   //        {
   //            Weather.Code = Convert.ToInt32(reader["WeatherCode"].ToString());
   //            Weather.ConditionCode = Convert.ToByte(reader["ConditionCode"]);
   //            Weather.IceDiagonal = Convert.ToDouble(reader["IceDiagonal"]);
   //            Weather.SaftyFactor = Convert.ToDouble(reader["SaftyFactor"]);
   //            Weather.Temp = Convert.ToDouble(reader["Temp"]);
   //            Weather.TypeCode = Convert.ToInt32(reader["TypeCode"]);
   //            Weather.WindSpeed = Convert.ToDouble(reader["WindSpeed"]);
   //           //ed.WriteMessage("WeatherCpde= "+Weather.Code.ToString()+"\n");
   //        }
   //        //ed.WriteMessage("FinishDweatherSelect\n");
   //        Connection.Close();
   //        reader.Close();
   //        return Weather;
   //    }

   //    public static DataTable selectByType(int type)
   //    {
           
   //         SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //         SqlDataAdapter adapter = new SqlDataAdapter("D_Weather_SelectByType", connection);
   //         adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
   //         adapter.SelectCommand.Parameters.Add(new SqlParameter("iTypeCode", type));
   //         DataSet dsWeather = new DataSet();
   //         adapter.Fill(dsWeather);
   //         return dsWeather.Tables[0];
        
   //    }

   //    public static DataTable selectByDesignCodeIsSelected(int DesignCode,Boolean IsSelected)
   //    {

   //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //        SqlDataAdapter adapter = new SqlDataAdapter("D_Weather_SelectByDesignCodeIsSelected", connection);
   //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
   //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
   //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsSelected", IsSelected));

   //        DataSet dsWeather = new DataSet();
   //        adapter.Fill(dsWeather);
   //        return dsWeather.Tables[0];

   //    }

   //    public static DataTable SelectAll()
   //    {
   //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
   //        SqlDataAdapter adapter = new SqlDataAdapter("D_Weather_Select", connection);
   //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
   //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
   //        DataSet dsWeather = new DataSet();
   //        adapter.Fill(dsWeather);
   //        return dsWeather.Tables[0];
   //    }

       //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access PArt~~~~~~~~~~~~~~~~~~~~
       public bool AccessInsert()
       {
           Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
           int containerCode;
           //OleDbTransaction transaction;
           //ed.WriteMessage("AccessInsertD_Weather\n");
           OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand insertCommand = new OleDbCommand("D_Weather_Insert", con);
           insertCommand.CommandType = CommandType.StoredProcedure;

           insertCommand.Parameters.Add(new OleDbParameter("iTemp", Temp));
           insertCommand.Parameters.Add(new OleDbParameter("iWindSpeed", WindSpeed));
           insertCommand.Parameters.Add(new OleDbParameter("iIceDiagonal", IceDiagonal));
           insertCommand.Parameters.Add(new OleDbParameter("iSaftyFactor", SaftyFactor));
           insertCommand.Parameters.Add(new OleDbParameter("iConditionCode", ConditionCode));
           insertCommand.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));


           try
           {
               //if (con.State == ConnectionState.Closed)
               //{
                   con.Open();
               //}
               Code = Convert.ToInt32(insertCommand.ExecuteScalar());
               //ed.WriteMessage("FinishAccessInsertD_Weather\n");
               con.Close();
               return true;
           }
           catch (System.Exception ex)
           {
               ed.WriteMessage(string.Format("Error DWeather.AccessInsert : {0} \n", ex.Message));
               con.Close();
               return false;
           }
       }
       public bool AccessUpdate()
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand command = new OleDbCommand("D_Weather_Update", connection);
           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add(new OleDbParameter("iTemp", Temp));
           command.Parameters.Add(new OleDbParameter("iWindSpeed", WindSpeed));
           command.Parameters.Add(new OleDbParameter("iIceDiagonal", IceDiagonal));
           command.Parameters.Add(new OleDbParameter("iSaftyFactor", SaftyFactor));
           command.Parameters.Add(new OleDbParameter("iCode", Code));

           Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

           ed.WriteMessage(string.Format("Code:={0},temp:={1},WindSpeed:={2},iIceDiagonal:={3},iSaftyFactor:={4}", code.ToString(), Temp.ToString(), WindSpeed.ToString(), IceDiagonal.ToString(), SaftyFactor.ToString()));
           try
           {
               connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
               return true;
           }
           catch (System.Exception ex1)
           {
               //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
               ed.WriteMessage(string.Format(" ERROR Weather.Update: {0} \n", ex1.Message));

               connection.Close();
               return false;
           }
       }
       public static DWeather AccessSelectByIsSelectedConditionCode(int IsSelected, int ConditionCode)
       {
                      Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_SelectByIsSelectedConditionCode", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iConditionCode", ConditionCode));

           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);

           DataRow[] reader = dsWeather.Tables[0].Select(string.Format(" IsSelected={0} AND ConditionCode={1}", IsSelected, ConditionCode));
           DWeather Weather = new DWeather();
           if (reader.Length == 1)
           {
               Weather.Code = Convert.ToInt32(reader[0]["WeatherCode"].ToString());
               Weather.ConditionCode = Convert.ToByte(reader[0]["ConditionCode"]);
               Weather.IceDiagonal = Convert.ToDouble(reader[0]["IceDiagonal"]);
               Weather.SaftyFactor = Convert.ToDouble(reader[0]["SaftyFactor"]);
               Weather.Temp = Convert.ToDouble(reader[0]["Temp"]);
               Weather.TypeCode = Convert.ToInt32(reader[0]["TypeCode"]);
               Weather.WindSpeed = Convert.ToDouble(reader[0]["WindSpeed"]);
               //ed.WriteMessage("*&*Weather.Code={0}\n",Weather.Code);
           }

        
           return Weather;
       }


       public static DWeather AccessSelectByIsSelectedConditionCode(int IsSelected, int ConditionCode,OleDbConnection _Connection)
       {
           Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

           OleDbConnection connection = _Connection;
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_SelectByIsSelectedConditionCode", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iConditionCode", ConditionCode));

           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);

           DataRow[] reader = dsWeather.Tables[0].Select(string.Format(" IsSelected={0} AND ConditionCode={1}", IsSelected, ConditionCode));
           DWeather Weather = new DWeather();
           if (reader.Length == 1)
           {
               Weather.Code = Convert.ToInt32(reader[0]["WeatherCode"].ToString());
               Weather.ConditionCode = Convert.ToByte(reader[0]["ConditionCode"]);
               Weather.IceDiagonal = Convert.ToDouble(reader[0]["IceDiagonal"]);
               Weather.SaftyFactor = Convert.ToDouble(reader[0]["SaftyFactor"]);
               Weather.Temp = Convert.ToDouble(reader[0]["Temp"]);
               Weather.TypeCode = Convert.ToInt32(reader[0]["TypeCode"]);
               Weather.WindSpeed = Convert.ToDouble(reader[0]["WindSpeed"]);
               //ed.WriteMessage("*&*Weather.Code={0}\n",Weather.Code);
           }


           return Weather;
       }
      

       //public static DWeather AccessSelectByIsSelectedConditionCode(int IsSelected, int ConditionCode)
       //{
       //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
       //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
       //    OleDbCommand command = new OleDbCommand("D_Weather_SelectByIsSelectedConditionCode", connection);
       //    command.CommandType = CommandType.StoredProcedure;
       //    command.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));
       //    command.Parameters.Add(new OleDbParameter("iConditionCode", ConditionCode));
       //    connection.Open();
       //    OleDbDataReader reader = command.ExecuteReader();
       //    DWeather weather = new DWeather();
       //    if (reader.Read())
       //    {
       //        weather.Code = Convert.ToInt32(reader["WeatherCode"].ToString());
       //        weather.Temp = Convert.ToDouble(reader["Temp"].ToString());
       //        weather.ConditionCode = Convert.ToByte(reader["ConditionCode"].ToString());
       //        weather.IceDiagonal = Convert.ToDouble(reader["IceDiagonal"].ToString());
       //        weather.SaftyFactor = Convert.ToDouble(reader["SaftyFactor"].ToString());
       //        weather.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
       //        weather.WindSpeed = Convert.ToDouble(reader["WindSpeed"].ToString());
       //        ed.WriteMessage("&&Code={0}\n", weather.Code);
       //    }
       //    reader.Close();
       //    connection.Close();
       //    return weather;
       //}
       public static DataTable AccessSelectByType(int type)
       {

           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_SelectByType", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iTypeCode", type));
           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);
           return dsWeather.Tables[0];

       }
       public static DataTable AccessSelectByIsSelected(Boolean IsSelected)
       {

           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_SelectByDesignCodeIsSelected", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));

           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);
           return dsWeather.Tables[0];

       }
       public static DataTable AccessSelectAll()
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_Select", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);
           return dsWeather.Tables[0];
       }

       public static DataTable AccessSelectTest(int IsSelected,int ConditionCode)
       {
           OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbDataAdapter adapter = new OleDbDataAdapter("D_Weather_SelectByIsSelectedConditionCode", connection);
           adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsSelected", IsSelected));
           adapter.SelectCommand.Parameters.Add(new OleDbParameter("iConditionCode", ConditionCode));

           DataSet dsWeather = new DataSet();
           adapter.Fill(dsWeather);
          
           return dsWeather.Tables[0];
       }
    }
}
