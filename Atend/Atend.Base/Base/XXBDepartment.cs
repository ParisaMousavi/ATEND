using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base

{
    public class XXBDepartment
    {
        //public BDepartment()
        //{ }

        //private int code;

        //public int Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private string name;

        //public string Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        //public bool Insert()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("B_Department_Insert", Connection);

        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.Insert {0}\n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //}

        //public bool AccessInsert()
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Department_Insert", connection);

        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    command.Parameters.Add(new OleDbParameter("iName", Name));
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
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.AccessInsert {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("B_Department_Update",Connection);
            
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.Update {0}\n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }

        //}

        //public bool AccessUpdate()
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Department_Update",connection);
            
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    command.Parameters.Add(new OleDbParameter("iName", Name));
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
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.AccessUpdate {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        //public static bool Delete(byte Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("B_Department_Delete" , connection);
            
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
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.Delete {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        //public static bool AccessDelete(byte Code)
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Department_Delete", connection);

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
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BDepartment.AccessDelete {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        //public static BDepartment SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_Department_Select", connection);
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
        //    DataSet dsDepartment = new DataSet();
        //    adapter.Fill(dsDepartment);
        //    BDepartment Dept = new BDepartment();
        //    if (dsDepartment.Tables[0].Rows.Count > 0)
        //    {
        //        Dept.Code = Convert.ToInt32(dsDepartment.Tables[0].Rows[0]["Code"].ToString());
        //        Dept.Name = dsDepartment.Tables[0].Rows[0]["Name"].ToString();
        //    }
        //    return Dept;

        //}

        //public static BDepartment AccessSelectByCode(int Code)
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
            
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_Department_Select", connection);
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", Code));
        //    DataSet dsDepartment = new DataSet();
        //    adapter.Fill(dsDepartment);
        //    BDepartment Dept = new BDepartment();
        //    if (dsDepartment.Tables[0].Rows.Count > 0)
        //    {
        //        Dept.Code = Convert.ToInt32(dsDepartment.Tables[0].Rows[0]["Code"].ToString());
        //        Dept.Name = dsDepartment.Tables[0].Rows[0]["Name"].ToString();
        //    }
        //    return Dept;

        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_Department_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
        //    DataSet dsDept = new DataSet();
        //    adapter.Fill(dsDept);
        //    return dsDept.Tables[0];
        //}

        //public static DataTable AccessSelectAll()
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_Department_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
        //    DataSet dsDept = new DataSet();
        //    adapter.Fill(dsDept);
        //    return dsDept.Tables[0];
        //}
    }
}
