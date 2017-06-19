using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base
{
    public class BSetting
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlCommand command = new SqlCommand("B_Setting_Insert", connection);
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iIsConnectedToPoshtiban", IsConnectedToPoshtiban));
        //    command.Parameters.Add(new SqlParameter("iShowWithoutProductCode", ShowWithoutProductCode));
        //    command.Parameters.Add(new SqlParameter("iIsUpdateProduct",IsUpdateProduct));
        //    command.Parameters.Add(new SqlParameter("iIsUpdateDesign", IsUpdateDesign));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error in B.setting.Insert={0}\n", ex.Message);
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Setting_Update", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("1={0}\n", IsConnectedToPoshtiban);
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //ed.WriteMessage("2={0}\n", ShowWithoutProductCode);

            command.Parameters.Add(new SqlParameter("iName", Name));
            //ed.WriteMessage("3={0}\n", Code);

            command.Parameters.Add(new SqlParameter("iValue", Value));
            //ed.WriteMessage("4={0}\n", IsUpdateProduct);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in B_Setting_Update ={0}", ex.Message);
                connection.Close();
                return false;
            }

        }

        public static BSetting SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("a1\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Setting_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            BSetting BS = new BSetting();
            //ed.WriteMessage("a2\n");
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("a3\n");
                if (reader.Read())
                {
                    BS.Code = Convert.ToInt32(reader["Code"].ToString());
                    BS.Value = reader["Value"].ToString();
                    BS.Name = reader["Name"].ToString();
                    ed.WriteMessage("Record found \n");
                }
                else
                {

                    BS.Code = 0;
                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error BSetting.Select : {0} \n", ex1.Message));
                connection.Close();
            }
            return BS;
        }

        //status report
        public static BSetting SelectByCode(int Code, SqlConnection _connectionLocal)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("a1\n");
            SqlConnection connection = _connectionLocal;
            SqlCommand command = new SqlCommand("B_Setting_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            BSetting BS = new BSetting();
            //ed.WriteMessage("a2\n");
            try
            {
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("a3\n");
                if (reader.Read())
                {
                    BS.Code = Convert.ToInt32(reader["Code"].ToString());
                    BS.Value = reader["Value"].ToString();
                    BS.Name = reader["Name"].ToString();
                    ed.WriteMessage("Record found \n");
                }
                else
                {

                    BS.Code = 0;
                }
                reader.Close();
                //connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error BSetting.Select : {0} \n", ex1.Message));
                //connection.Close();
            }
            return BS;
        }

        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_Setting_Update", Connection);
        //    Command.CommandType = System.Data.CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iKey", Key));
        //    Command.Parameters.Add(new SqlParameter("iValue", Value));
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
        //        ed.WriteMessage(string.Format(" ERROR BSetting.Update {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}


        //public bool AccessUpdate()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_Setting_Update", Connection);
        //    Command.CommandType = System.Data.CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iKey", Key));
        //    Command.Parameters.Add(new OleDbParameter("iValue", Value));
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
        //        ed.WriteMessage(string.Format(" ERROR BSetting.Update {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}


        //public static BSetting SelectByKey(string Key)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand sqlCommand = new SqlCommand("B_Setting_SelectByKey", Connection);
        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new SqlParameter("iKey", Key));
        //    Connection.Open();
        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    BSetting setting = new BSetting();
        //    if (reader.Read())
        //    {
        //        setting.Comment = reader["Comment"].ToString();
        //        setting.Key = reader["Key"].ToString();
        //        setting.Value = reader["Value"].ToString();
        //    }
        //    return setting;
        //}

        //public static BSetting AccessSelectByKey(string Key)
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand sqlCommand = new OleDbCommand("B_Setting_SelectByKey", Connection);
        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iKey", Key));
        //    Connection.Open();
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    BSetting setting = new BSetting();
        //    if (reader.Read())
        //    {
        //        setting.Comment = reader["Comment"].ToString();
        //        setting.Key = reader["Key"].ToString();
        //        setting.Value = reader["Value"].ToString();
        //    }
        //    return setting;
        //}

    }
}
