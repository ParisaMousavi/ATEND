using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    //public class DEquipmentSetting
    //{
    //    private Guid designEquipmentCode;

    //    public Guid DesignEquipmentCode
    //    {
    //        get { return designEquipmentCode; }
    //        set { designEquipmentCode = value; }
    //    }

    //    private string m_Value;

    //    public string Value
    //    {
    //        get { return m_Value; }
    //        set { m_Value = value; }
    //    }

    //    private Atend.Control.Enum.EquipmentSettingType type;

    //    public Atend.Control.Enum.EquipmentSettingType Type
    //    {
    //        get { return type; }
    //        set { type = value; }
    //    }

    //    public bool Insert(SqlTransaction _transaction , SqlConnection _connection)
    //    {
    //        //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand Command = new SqlCommand("D_EquipmentSetting_Insert", _connection);
    //        Command.CommandType = CommandType.StoredProcedure;
    //        Command.Transaction = _transaction;


    //        Command.Parameters.Add(new SqlParameter("iDesignEquipmentCode", DesignEquipmentCode));
    //        Command.Parameters.Add(new SqlParameter("iValue", Value));
    //        Command.Parameters.Add(new SqlParameter("iType", Convert.ToByte(Type)));


    //        try
    //        {
    //            //Connection.Open();
    //            Command.ExecuteNonQuery();
    //            //Connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format(" ERROR DEquipmentSetting.Insert {0}\n", ex1.Message));

    //            //Connection.Close();
    //            return false;
    //        }
    //    }

    //    public bool Update()
    //    {
    //        SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand Command = new SqlCommand("D_EquipmentSetting_Update", Connection);
    //        Command.CommandType = CommandType.StoredProcedure;
    //        Command.Parameters.Add(new SqlParameter("iDesignEquipmentCode", DesignEquipmentCode));
    //        Command.Parameters.Add(new SqlParameter("iValue", Value));
    //        Command.Parameters.Add(new SqlParameter("iType", Type));
    //        try
    //        {
    //            Connection.Open();
    //            Command.ExecuteNonQuery();
    //            Connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format(" ERROR DEquipmentSetting.Update {0}\n", ex1.Message));

    //            Connection.Close();
    //            return false;
    //        }
    //    }

    //    public static bool Delete(Guid DesignEquipmentCode)
    //    { 
    //        SqlConnection Connection=new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand Command = new SqlCommand("D_EquipmentSetting_Delete", Connection);
    //        Command.CommandType = CommandType.StoredProcedure;
    //        Command.Parameters.Add(new SqlParameter("iDesignEquipmentCode", DesignEquipmentCode));
    //        try
    //        {
    //            Connection.Open();
    //            Command.ExecuteNonQuery();
    //            Connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format(" ERROR DEquipmentSetting.Delete: {0} \n", ex1.Message));

    //            Connection.Close();
    //            return false;
    //        }
            
    //    }

    //    public static DEquipmentSetting SelectByDesignEquipmentCodeType(Guid DesignEquipmentCode, Atend.Control.Enum.EquipmentSettingType Type)
    //    {
    //        SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand sqlCommand = new SqlCommand("D_EquipmentSetting_Select", Connection);
    //        sqlCommand.CommandType = CommandType.StoredProcedure;
    //        sqlCommand.Parameters.Add(new SqlParameter("iDesignEquipmentCode", DesignEquipmentCode));
    //        sqlCommand.Parameters.Add(new SqlParameter("iType", Convert.ToByte(Type)));
    //        Connection.Open();
    //        SqlDataReader reader = sqlCommand.ExecuteReader();
    //        DEquipmentSetting equipmentSetting = new DEquipmentSetting();

    //        if (reader.Read())
    //        {
    //            equipmentSetting.DesignEquipmentCode = new Guid (reader["DesignEquipmentCode"].ToString());
    //            equipmentSetting.Type = (Atend.Control.Enum.EquipmentSettingType)(Convert.ToByte((reader["Type"].ToString())));
    //            equipmentSetting.Value = reader["Value"].ToString();
    //        }
    //        Connection.Close();
    //        reader.Close();
    //        return equipmentSetting;
    //    }

    //    public static DataTable SelectByDesignCodeType(int DesignCode, byte Type)
    //    {
    //        SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlDataAdapter adapter = new SqlDataAdapter("D_EquipmentSetting_SelectByDesignCodeType", Connection);
    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
    //        DataSet dsEquipmentSetting = new DataSet();
    //        adapter.Fill(dsEquipmentSetting);
    //        return dsEquipmentSetting.Tables[0];
    //    }

    //}
}
