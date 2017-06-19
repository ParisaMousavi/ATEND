using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

using System.Data.SqlClient;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base
{
    public class XXBOperationType
    {
        //private int code;

        //public int Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private string title;

        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }
        //}

        //private string description;

        //public string Description
        //{
        //    get { return description; }
        //    set { description = value; }
        //}

        //private Atend.Control.Enum.Phase phase;

        //public Atend.Control.Enum.Phase Phase
        //{
        //    get { return phase; }
        //    set { phase = value; }
        //}

        //public bool Insert()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_OperationType_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iTitle", Title));
        //    Command.Parameters.Add(new SqlParameter("iDescription", Description));
        //    Command.Parameters.Add(new SqlParameter("iPhase", Convert.ToByte(Phase)));
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
        //        ed.WriteMessage(string.Format(" ERROR BOperationType.Insert {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}


        //public bool AccessInsert()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
            
        //    OleDbCommand Command = new OleDbCommand("B_OperationType_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Command.Parameters.Add(new OleDbParameter("iTitle", Title));
        //    Command.Parameters.Add(new OleDbParameter("iDescription", Description));
        //    Command.Parameters.Add(new OleDbParameter("iPhase", Convert.ToByte(Phase)));
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
        //        ed.WriteMessage(string.Format(" ERROR BOperationType.Insert {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public static BOperationType SelectByCode(int Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_OperationType_Select", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    BOperationType OperationType = new BOperationType();
        //    if (reader.Read())
        //    {
        //        OperationType.Code = Convert.ToInt32(reader["Code"]);
        //        OperationType.Description = reader["Description"].ToString();
        //        OperationType.Phase = (Atend.Control.Enum.Phase)(reader["Phase"]);
        //        OperationType.Title = reader["Title"].ToString();

        //    }
        //    Connection.Close();
        //    reader.Close();
        //    return OperationType;
        //}

        //public static BOperationType AccessSelectByCode(int Code)
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_OperationType_Select", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Connection.Open();
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    BOperationType OperationType = new BOperationType();
        //    if (reader.Read())
        //    {
        //        OperationType.Code = Convert.ToInt32(reader["Code"]);
        //        OperationType.Description = reader["Description"].ToString();
        //        OperationType.Phase = (Atend.Control.Enum.Phase)(reader["Phase"]);
        //        OperationType.Title = reader["Title"].ToString();

        //    }
        //    Connection.Close();
        //    reader.Close();
        //    return OperationType;
        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_OperationType_Select", Connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", 0));
        //    DataSet dsOperationType = new DataSet();
        //    adapter.Fill(dsOperationType);
        //    return dsOperationType.Tables[0];
        //}


        //public static DataTable AcessSelectAll()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
            
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_OperationType_Select", Connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", 0));
        //    DataSet dsOperationType = new DataSet();
        //    adapter.Fill(dsOperationType);
        //    return dsOperationType.Tables[0];
        //}


    }
}
