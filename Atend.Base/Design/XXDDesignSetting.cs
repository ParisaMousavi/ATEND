using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Design
{
    public class XXDDesignSetting
    {

        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //private int designCode;

        //public int DesignCode
        //{
        //    get { return designCode; }
        //    set { designCode = value; }
        //}

        //private int m_value;

        //public int Value
        //{
        //    get { return m_value; }
        //    set { m_value = value; }
        //}

        //private Atend.Control.Enum.DesignSettingType type;

        //public Atend.Control.Enum.DesignSettingType Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}

        //public bool Insert()
        //{

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlCommand Command = new SqlCommand("D_DesignSetting_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iValue", Value));
        //    Command.Parameters.Add(new SqlParameter("iType", Convert.ToByte(Type)));
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

        //public static DDesignSetting SelectByDesingCodeType(int DesignCode, Atend.Control.Enum.DesignSettingType Type)
        //{

        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_DesignSetting_SelectByDesignCodeType", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iType", Convert.ToByte(Type)));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DDesignSetting setting = new DDesignSetting();
        //    if (reader.Read())
        //    {
        //        setting.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        setting.Type = (Atend.Control.Enum.DesignSettingType)(reader["Type"]);
        //        setting.Value = Convert.ToInt32(reader["Value"].ToString());
        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return setting;
        //}

        //public bool Update()
        //{
        //    DDesignSetting setting = DDesignSetting.SelectByDesingCodeType(DesignCode, Type);

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_DesignSetting_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iValue", setting.Value + 1));
        //    Command.Parameters.Add(new SqlParameter("iType", Type));
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

        //public bool Update(SqlTransaction _Transaction, SqlConnection _Connection)
        //{
        //    DDesignSetting setting = DDesignSetting.SelectByDesingCodeType(DesignCode, Type);

        //    SqlConnection Connection = _Connection;
        //    SqlCommand Command = new SqlCommand("D_DesignSetting_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iValue", setting.Value + 1));
        //    Command.Parameters.Add(new SqlParameter("iType", Type));
        //    try
        //    {
        //        //Connection.Open();
        //        Command.Transaction = _Transaction;
        //        Command.ExecuteNonQuery();
        //        //Connection.Close();
        //        //ed.writeMessage("DDesignSetting.Update Done. \n");
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        //Connection.Close();
        //        ed.WriteMessage("ERROR DDesignSetting.Update : {0} \n", ex.Message);
        //        return false;
        //    }
        //}

        //public bool UpdateToDecrease(SqlTransaction _Transaction, SqlConnection _Connection)
        //{
        //    DDesignSetting setting = DDesignSetting.SelectByDesingCodeType(DesignCode, Type);

        //    SqlConnection Connection = _Connection;
        //    SqlCommand Command = new SqlCommand("D_DesignSetting_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iValue", setting.Value - 1));
        //    Command.Parameters.Add(new SqlParameter("iType", Type));
        //    try
        //    {
        //        //Connection.Open();
        //        Command.Transaction = _Transaction;
        //        Command.ExecuteNonQuery();
        //        //ed.writeMessage("DesignSettin Decrease Done \n ");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DesignSetting.UpdateToDecrease : {0} \n", ex.Message));
        //        return false;
        //    }
        //}


    }
}
