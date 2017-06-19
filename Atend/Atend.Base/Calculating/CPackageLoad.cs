using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;
using System.Data.OleDb;

namespace Atend.Base.Calculating
{
    public class CPackageLoad
    {

        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int loadFactorCode;

        public int LoadFactorCode
        {
            get { return loadFactorCode; }
            set { loadFactorCode = value; }
        }

        private int dLoadFactorCode;

        public int DLoadFactorCode
        {
            get { return dLoadFactorCode; }
            set { dLoadFactorCode = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public bool Insert()        
        //{

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_PackageLoad_Insert", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
            
        //    command.Parameters.Add(new SqlParameter("iLoadFactorCode", LoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iDloadFactorCode", DLoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iCount", Count));
            
        //    //SqlParameter InsertedCode = new SqlParameter();
        //    //InsertedCode.SqlDbType = SqlDbType.Int;
        //    //InsertedCode.Direction = ParameterDirection.Output;
        //    //InsertedCode.ParameterName = "iCode";

        //    //command.Parameters.Add(InsertedCode);

        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        //Code = Convert.ToInt32(InsertedCode.Value.ToString());
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("CPackageload.Insert Error" + ex.Message + "\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_PackageLoad_Update", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iLoadFactorCode", LoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iDloadFactorCode", DLoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iCount", Count));
            

        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("CPackageload.Insert Error" + ex.Message + "\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_PackageLoad_Delete", connection);
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
        //        ed.WriteMessage(string.Format(" ERROR C_PackageLoad.delete: {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static bool DeleteByLoadCode(int LoadCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_PackageLoad_DeleteByLoadFactorCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iLoadFactorCode", LoadCode));
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
        //        ed.WriteMessage(string.Format(" ERROR C_PackageLoad.deletebyLoadFactorCode: {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static CPackageLoad SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_PackageLoad_SelectByCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));

        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    CPackageLoad cpl = new CPackageLoad();
        //    if (reader.Read())
        //    {

        //        cpl.Count = Convert.ToInt32(reader["Count"]);
        //        cpl.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
        //        cpl.LoadFactorCode = Convert.ToInt32(reader["LoadFactorCode"]);
        //        cpl.Code = Convert.ToInt32(reader["Code"]);
        //    }
        //    else
        //    {
        //        cpl.Count = 0;
        //        cpl.DLoadFactorCode = 0;
        //        cpl.LoadFactorCode = 0;
        //        cpl.Code = 0;
        //    }

        //    reader.Close();

        //    connection.Close();
        //    return cpl;
        //}

        //public static DataTable SelectByLoadFactorCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_PackageLoad_SelectByLoadFactorCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iLoadFActorCode", Code));
        //    DataSet dsLoad = new DataSet();
        //    adapter.Fill(dsLoad);
        //    return dsLoad.Tables[0];
        //}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~//

        public bool AccessInsert()
        {

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand  command = new OleDbCommand("C_PackageLoad_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iLoadFactorCode", LoadFactorCode));
            command.Parameters.Add(new OleDbParameter("iDloadFactorCode", DLoadFactorCode));
            command.Parameters.Add(new OleDbParameter("iCount", Count));

            //SqlParameter InsertedCode = new SqlParameter();
            //InsertedCode.SqlDbType = SqlDbType.Int;
            //InsertedCode.Direction = ParameterDirection.Output;
            //InsertedCode.ParameterName = "iCode";

            //command.Parameters.Add(InsertedCode);

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                //Code = Convert.ToInt32(InsertedCode.Value.ToString());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("CPackageload.Insert Error" + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_PackageLoad_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iLoadFactorCode", LoadFactorCode));
            command.Parameters.Add(new OleDbParameter("iDloadFactorCode", DLoadFactorCode));
            command.Parameters.Add(new OleDbParameter("iCount", Count));


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("CPackageload.Insert Error" + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_PackageLoad_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new  OleDbParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR C_PackageLoad.delete: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteByLoadCode(int LoadCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_PackageLoad_DeleteByLoadFactorCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iLoadFactorCode", LoadCode));
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
                ed.WriteMessage(string.Format(" ERROR C_PackageLoad.deletebyLoadFactorCode: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static CPackageLoad AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
           OleDbCommand command = new OleDbCommand("C_PackageLoad_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));

            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            CPackageLoad cpl = new CPackageLoad();
            if (reader.Read())
            {

                cpl.Count = Convert.ToInt32(reader["Count"]);
                cpl.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
                cpl.LoadFactorCode = Convert.ToInt32(reader["LoadFactorCode"]);
                cpl.Code = Convert.ToInt32(reader["Code"]);
            }
            else
            {
                cpl.Count = 0;
                cpl.DLoadFactorCode = 0;
                cpl.LoadFactorCode = 0;
                cpl.Code = 0;
            }

            reader.Close();

            connection.Close();
            return cpl;
        }

        public static DataTable AccessSelectByLoadFactorCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_PackageLoad_SelectByLoadFactorCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iLoadFActorCode", Code));
            DataSet dsLoad = new DataSet();
            adapter.Fill(dsLoad);
            return dsLoad.Tables[0];
        }
    }
}
