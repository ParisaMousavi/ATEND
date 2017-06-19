using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Calculating
{
    public class CNetWorkCross
    {
        public CNetWorkCross()
        { }

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
        private double clearance;

        public double Clearance
        {
            get { return clearance; }
            set { clearance = value; }
        }

        private double v380;

        public double V380
        {
            get { return v380; }
            set { v380 = value; }
        }

        private double kV11;

        public double KV11
        {
            get { return kV11; }
            set { kV11 = value; }
        }

        private double kV20;

        public double KV20
        {
            get { return kV20; }
            set { kV20 = value; }
        }

        private double kV32;

        public double KV32
        {
            get { return kV32; }
            set { kV32 = value; }
        }
        //public bool Insert()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_NetWorkCross_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    //command.Parameters.Add(new SqlParameter("iClearance", Clearance));
        //    command.Parameters.Add(new SqlParameter("iV380", V380));
        //    command.Parameters.Add(new SqlParameter("iKV11", KV11));
        //    command.Parameters.Add(new SqlParameter("iKV20", KV20));
        //    command.Parameters.Add(new SqlParameter("iKV32",KV32));

        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage("Error In C_NetWorkCross_Insert:{0}\n", ex1.Message);
        //        connection.Close();
        //        return false;
        //    }


        //}

        //public bool Update()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_NetWorkCross_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    //command.Parameters.Add(new SqlParameter("iClearance", Clearance));
        //    command.Parameters.Add(new SqlParameter("iV380", V380));
        //    command.Parameters.Add(new SqlParameter("iKV11", KV11));
        //    command.Parameters.Add(new SqlParameter("iKV20", KV20));
        //    command.Parameters.Add(new SqlParameter("iKV32", KV32));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage("Error In C_NetWorkCross_Update:{0}\n", ex1.Message);
        //        connection.Close();
        //        return false;
        //    }


        //}

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_NetWorkCross_delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
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
        //        ed.WriteMessage(string.Format(" ERROR C_NetWorkCross.delete: {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static CNetWorkCross SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_NetWorkCross_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    CNetWorkCross Cnetworkcross = new CNetWorkCross();
        //    if (reader.Read())
        //    {
        //        Cnetworkcross.Code = Convert.ToInt32(reader["Code"].ToString());
        //        //Cnetworkcross.Clearance = Convert.ToSingle(reader["Clearance"].ToString());
        //        Cnetworkcross.Name = reader["Name"].ToString();
        //        Cnetworkcross.V380 = Convert.ToDouble(reader["V380"]);

        //        Cnetworkcross.KV11 = Convert.ToDouble(reader["KV11"]);
        //        Cnetworkcross.KV20 = Convert.ToDouble(reader["KV20"]);
        //        Cnetworkcross.KV32 = Convert.ToDouble(reader["KV32"]);

        //    }

        //    reader.Close();

        //    connection.Close();
        //    return Cnetworkcross;
        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_NetWorkCross_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
        //    DataSet dsCross= new DataSet();
        //    adapter.Fill(dsCross);
        //    return dsCross.Tables[0];
        //}

        //public static DataTable Search(string Name)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_NetWorkCross_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
        //    DataSet dsCross = new DataSet();
        //    adapter.Fill(dsCross);
        //    return dsCross.Tables[0];
        //}

       
        // ~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection  connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_NetWorkCross_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iClearance", Clearance));
            command.Parameters.Add(new OleDbParameter("iV380", V380));
            command.Parameters.Add(new OleDbParameter("iKV11", KV11));
            command.Parameters.Add(new OleDbParameter("iKV20", KV20));
            command.Parameters.Add(new OleDbParameter("iKV32", KV32));

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
                ed.WriteMessage("Error In C_NetWorkCross_Insert:{0}\n", ex1.Message);
                connection.Close();
                return false;
            }


        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand  command = new OleDbCommand("C_NetWorkCross_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iClearance", Clearance));
            command.Parameters.Add(new OleDbParameter("iV380", V380));
            command.Parameters.Add(new OleDbParameter("iKV11", KV11));
            command.Parameters.Add(new OleDbParameter("iKV20", KV20));
            command.Parameters.Add(new OleDbParameter("iKV32", KV32));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error In C_NetWorkCross_Update:{0}\n", ex1.Message);
                connection.Close();
                return false;
            }


        }

        public static bool AccessDelete(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_NetWorkCross_delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR C_NetWorkCross.delete: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static CNetWorkCross AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_NetWorkCross_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            CNetWorkCross Cnetworkcross = new CNetWorkCross();
            if (reader.Read())
            {
                Cnetworkcross.Code = Convert.ToInt32(reader["Code"].ToString());
                //Cnetworkcross.Clearance = Convert.ToSingle(reader["Clearance"].ToString());
                Cnetworkcross.Name = reader["Name"].ToString();
                Cnetworkcross.V380 = Convert.ToDouble(reader["V380"]);

                Cnetworkcross.KV11 = Convert.ToDouble(reader["KV11"]);
                Cnetworkcross.KV20 = Convert.ToDouble(reader["KV20"]);
                Cnetworkcross.KV32 = Convert.ToDouble(reader["KV32"]);

            }

            reader.Close();

            connection.Close();
            return Cnetworkcross;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_NetWorkCross_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsCross = new DataSet();
            adapter.Fill(dsCross);
            return dsCross.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_NetWorkCross_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsCross = new DataSet();
            adapter.Fill(dsCross);
            return dsCross.Tables[0];
        }

    }
}
