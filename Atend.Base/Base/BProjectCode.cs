using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Base
{
    public class BProjectCode
    {
        //private int code;

        //public int Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private int additionalCode;

        //public int AdditionalCode
        //{
        //    get { return additionalCode; }
        //    set { additionalCode = value; }
        //}

        //private string name;

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}



        ////public bool Insert()
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlCommand command = new SqlCommand("B_ProjectCode_Insert", connection);
        ////    command.CommandType = CommandType.StoredProcedure;

        ////    command.Parameters.Add(new SqlParameter("iAdditionalCode", additionalCode));
        ////    command.Parameters.Add(new SqlParameter("iName", Name));
        ////    try
        ////    {
        ////        connection.Open();
        ////        Code = Convert.ToInt32(command.ExecuteScalar());
        ////        connection.Close();
        ////        return true;
        ////    }
        ////    catch (System.Exception ex1)
        ////    {
        ////        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ////        ed.WriteMessage(string.Format("Error BProjectCode.Insert : {0} \n", ex1.Message));
        ////        connection.Close();
        ////        return false;
        ////    }
        ////}

        ////public bool Update()
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlCommand command = new SqlCommand("B_ProjectCode_Update", connection);
        ////    command.CommandType = CommandType.StoredProcedure;

        ////    command.Parameters.Add(new SqlParameter("iCode", Code));
        ////    command.Parameters.Add(new SqlParameter("iAdditionalCode", AdditionalCode));
        ////    command.Parameters.Add(new SqlParameter("iName", Name));
        ////    try
        ////    {
        ////        connection.Open();
        ////        command.ExecuteNonQuery();
        ////        connection.Close();
        ////        return true;
        ////    }
        ////    catch (System.Exception ex1)
        ////    {
        ////        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ////        ed.WriteMessage(string.Format("Error BProjectCode.Update : {0} \n", ex1.Message));
        ////        connection.Close();
        ////        return false;
        ////    }
        ////}



        ////public static bool Delete(int Code)
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlCommand command = new SqlCommand("B_ProjectCode_Delete", connection);
        ////    command.CommandType = CommandType.StoredProcedure;

        ////    command.Parameters.Add(new SqlParameter("iCode", Code));
        ////    try
        ////    {
        ////        connection.Open();
        ////        command.ExecuteNonQuery();
        ////        connection.Close();
        ////        return true;
        ////    }
        ////    catch (System.Exception ex1)
        ////    {
        ////        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        ////        ed.WriteMessage(string.Format("Error BProjectCode.Delete : {0} \n", ex1.Message));
        ////        connection.Close();
        ////        return false;
        ////    }
        ////}

        ////public static BProjectCode SelectByCode(int Code)
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlCommand command = new SqlCommand("B_ProjectCode_Select", connection);
        ////    command.CommandType = CommandType.StoredProcedure;

        ////    command.Parameters.Add(new SqlParameter("iCode", Code));
        ////    connection.Open();
        ////    SqlDataReader reader = command.ExecuteReader();
        ////    BProjectCode  BProjectCode = new BProjectCode();
        ////    if (reader.Read())
        ////    {
        ////        BProjectCode.Code = Convert.ToInt16(reader["Code"].ToString());
        ////        BProjectCode.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
        ////        BProjectCode.Name = reader["Name"].ToString();
        ////    }
        ////    else
        ////        BProjectCode.Code = -1;

        ////    reader.Close();
        ////    connection.Close();
        ////    return BProjectCode;
        ////}


        ////public static BProjectCode SelectByAdditionalCode(int ACode)
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlCommand command = new SqlCommand("B_ProjectCode_SelectByAdditionalCode", connection);
        ////    command.CommandType = CommandType.StoredProcedure;

        ////    command.Parameters.Add(new SqlParameter("iACode", ACode));
        ////    connection.Open();
        ////    SqlDataReader reader = command.ExecuteReader();
        ////    BProjectCode BProjectCode = new BProjectCode();
        ////    if (reader.Read())
        ////    {
        ////        BProjectCode.Code = Convert.ToInt16(reader["Code"].ToString());
        ////        BProjectCode.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
        ////        BProjectCode.Name = reader["Name"].ToString();
        ////    }
        ////    else
        ////        BProjectCode.Code = -1;

        ////    reader.Close();
        ////    connection.Close();
        ////    return BProjectCode;
        ////}

        ////public static DataTable SelectAll()
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlDataAdapter adapter = new SqlDataAdapter("B_ProjectCode_Select", connection);
        ////    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        ////    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
        ////    DataSet dsPhuse = new DataSet();
        ////    adapter.Fill(dsPhuse);
        ////    return dsPhuse.Tables[0];
        ////}



        ////public static object Search(string Name)
        ////{
        ////    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlDataAdapter adapter = new SqlDataAdapter("B_ProjectCode_Search", connection);
        ////    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        ////    adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
        ////    DataSet ds = new DataSet();
        ////    adapter.Fill(ds);

        ////    return ds.Tables[0];
        ////}



        //public bool AccessInsert()
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("B_ProjectCode_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iAdditionalCode", additionalCode));
        //    command.Parameters.Add(new OleDbParameter("iName", Name));
        //    try
        //    {
        //        connection.Open();
        //        Code = Convert.ToInt32(command.ExecuteScalar());
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("Error BProjectCode.Insert : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public bool AccessUpdate()
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("B_ProjectCode_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    command.Parameters.Add(new OleDbParameter("iName", Name));

        //    command.Parameters.Add(new OleDbParameter("iAdditionalCode", AdditionalCode));
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
        //        ed.WriteMessage(string.Format("Error BProjectCode.Update : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static bool AccessDelete(int Code)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("B_ProjectCode_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
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
        //        ed.WriteMessage(string.Format("Error BProjectCode.Delete : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        ////StatusReport
        //public static BProjectCode AccessSelectByCode(int Code)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("B_ProjectCode_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    connection.Open();
        //    OleDbDataReader reader = command.ExecuteReader();
        //    BProjectCode BProjectCode = new BProjectCode();
        //    if (reader.Read())
        //    {
        //        BProjectCode.Code = Convert.ToInt16(reader["Code"].ToString());
        //        BProjectCode.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
        //        BProjectCode.Name = reader["Name"].ToString();
        //    }
        //    else
        //        BProjectCode.Code = -1;

        //    reader.Close();
        //    connection.Close();
        //    return BProjectCode;
        //}

        //public static BProjectCode AccessSelectByAdditionalCode(int ACode)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("B_ProjectCode_SelectByAdditionalCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iACode", ACode));
        //    connection.Open();
        //    OleDbDataReader reader = command.ExecuteReader();
        //    BProjectCode BProjectCode = new BProjectCode();
        //    if (reader.Read())
        //    {
        //        BProjectCode.Code = Convert.ToInt16(reader["Code"].ToString());
        //        BProjectCode.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
        //        BProjectCode.Name = reader["Name"].ToString();
        //    }
        //    else
        //        BProjectCode.Code = -1;

        //    reader.Close();
        //    connection.Close();
        //    return BProjectCode;
        //}

        ////frmDrawGroundPost
        //public static DataTable AccessSelectAll()
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_ProjectCode_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
        //    DataSet dsPhuse = new DataSet();
        //    adapter.Fill(dsPhuse);
        //    return dsPhuse.Tables[0];
        //}

        //public static object Search(string Name)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_ProjectCode_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);

        //    return ds.Tables[0];
        //}


    }
}
