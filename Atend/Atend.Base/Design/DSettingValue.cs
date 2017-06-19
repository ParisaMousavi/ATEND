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
    public class DSettingValue
    {
        private int designCode;

        public int DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private Atend.Control.Enum.SettingType type;

        public Atend.Control.Enum.SettingType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string m_value;

        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_SettingValue_Insert", connection);
        //    command.Parameters.Add(new SqlParameter("iDesginCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iValue", Value));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR DSettingValue.Insert {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }

        //}

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_SettingValue_Insert", connection);
            command.Parameters.Add(new OleDbParameter("iDesginCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iValue", Value));
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
                ed.WriteMessage(string.Format(" ERROR DSettingValue.AccessInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_SettingValue_Update", connection);
        //    command.Parameters.Add(new SqlParameter("iDesginCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iValue", Value));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR DSettingValue.Update {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }

        //}

        public bool AccessUpdate()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_SettingValue_Update", connection);
            command.Parameters.Add(new OleDbParameter("iDesginCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iValue", Value));
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
                ed.WriteMessage(string.Format(" ERROR DSettingValue.AccessUpdate {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //public static DSettingValue SelectByDesignCodeType(int DesignCode, Atend.Control.Enum.SettingType Type)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand sqlCommand = new SqlCommand("D_SettingValue_Select", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    sqlCommand.Parameters.Add(new SqlParameter("iType", Convert.ToByte(Type)));
        //    Connection.Open();
        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    DSettingValue SettingValue = new DSettingValue();

        //    if (reader.Read())
        //    {
        //        SettingValue.DesignCode = Convert.ToInt32(reader["DesignCode"].ToString());
        //        SettingValue.Type = (Atend.Control.Enum.SettingType)(Convert.ToByte((reader["Type"].ToString())));
        //        SettingValue.Value = reader["Value"].ToString();
        //    }
        //    Connection.Close();
        //    reader.Close();
        //    return SettingValue;
        //}

        public static DSettingValue AccessSelectByDesignCodeType(int DesignCode, Atend.Control.Enum.SettingType Type)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_SettingValue_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Convert.ToByte(Type)));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DSettingValue SettingValue = new DSettingValue();

            if (reader.Read())
            {
                SettingValue.DesignCode = Convert.ToInt32(reader["DesignCode"].ToString());
                SettingValue.Type = (Atend.Control.Enum.SettingType)(Convert.ToByte((reader["Type"].ToString())));
                SettingValue.Value = reader["Value"].ToString();
            }
            Connection.Close();
            reader.Close();
            return SettingValue;
        }

    }
}
