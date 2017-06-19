using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using Atend.Control;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Base
{
    public class BReport
    {
        //private int code;

        //public int Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private string value1;

        //public string Value1
        //{
        //    get { return value1; }
        //    set { value1 = value; }
        //}

        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////EXTRA for cnstring
        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand command = new SqlCommand("B_Report_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iValue", Value1));
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
        //        ed.WriteMessage(string.Format(" ERROR BReport.Insert{0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        ////EXTRA for cnstring
        //public bool UpdateByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand command = new SqlCommand("B_Report_UpdateByCode",connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iValue", Value1));
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
        //        ed.WriteMessage(string.Format(" ERROR BReport.Update {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        ////EXTRA for cnstring
        //public static BReport Select_ByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_Report_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
        //    DataSet dsReport = new DataSet();
        //    adapter.Fill(dsReport);
        //    BReport report = new BReport();
        //    if (dsReport.Tables[0].Rows.Count > 0)
        //    {
        //        report.Code = Convert.ToInt16(dsReport.Tables[0].Rows[0]["Code"].ToString());
        //        report.value1 = dsReport.Tables[0].Rows[0]["Value"].ToString();

        //    }
        //    else
        //    {
        //        report.code = 0;
        //        report.Value1 = "NONE";
        //    }
        //    return report;
        //}

        //public static BReport AccessSelectByCode(int Code)
        //{

        //    Atend.Base.Base.BReport report = new BReport();


        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Report_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iCode", Code));


        //    try
        //    {
        //        connection.Open();
        //        OleDbDataReader dataReader = command.ExecuteReader();

        //        if (dataReader.Read())
        //        {
        //            report.Code = Convert.ToInt32(dataReader["Code"].ToString());
        //            report.Value1 = Convert.ToString(dataReader["Value"].ToString());
        //        }
        //        else
        //        {
        //            report.Code = 0;
        //            report.Value1 = "NONE";
        //        }

        //        dataReader.Close();
        //    }
        //    catch(System.Exception ex)
        //    {
        //        report.Code = 0;
        //        report.Value1 = "NONE";
        //    }
        //    connection.Close();
        //    return report;

        //}

        //public bool AccessInsert()
        //{

        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Report_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    command.Parameters.Add(new OleDbParameter("iValue", Value1));

        //    try
        //    {

        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    catch(System.Exception ex)
        //    {
        //        connection.Close();
        //        ed.WriteMessage("Error AccessInsert : {0} \n ",ex.Message);
        //    }


        //    return true;
        //}

        //public bool AccessUpdate()
        //{
        //    OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);
        //    OleDbCommand command = new OleDbCommand("B_Report_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    command.Parameters.Add(new OleDbParameter("iValue", Value1));

        //    try
        //    {

        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        //ed.writeMessage("REport updated. \n");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        connection.Close();
        //        ed.WriteMessage("Error AccessUdate : {0} \n ", ex.Message);
        //    }
        //    return true;
        //}
    }
}
