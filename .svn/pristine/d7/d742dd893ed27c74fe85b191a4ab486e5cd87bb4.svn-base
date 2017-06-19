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
    public class XXBTariffHeader
    {
        //private Guid code;

        //public Guid Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private int tariffYear;

        //public int TariffYear
        //{
        //    get { return tariffYear; }
        //    set { tariffYear = value; }
        //}

        //private int number;

        //public int Number
        //{
        //    get { return number; }
        //    set { number = value; }
        //}

        //private string beginDate;

        //public string BeginDate
        //{
        //    get { return beginDate; }
        //    set { beginDate = value; }
        //}

        //private string endDate;

        //public string EndDate
        //{
        //    get { return endDate; }
        //    set { endDate = value; }
        //}

        //public bool Insert()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_TariffHeader_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iTariffYear", TariffYear));
        //    Command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    Command.Parameters.Add(new SqlParameter("iBeginDate", BeginDate));
        //    Command.Parameters.Add(new SqlParameter("iEndDate", EndDate));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.Insert {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool AccessInsert()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_TariffHeader_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iTariffYear", TariffYear));
        //    Command.Parameters.Add(new OleDbParameter("iNumber", Number));
        //    Command.Parameters.Add(new OleDbParameter("iBeginDate", BeginDate));
        //    Command.Parameters.Add(new OleDbParameter("iEndDate", EndDate));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.AccessInsert {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_TariffHeader_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iTariffYear", TariffYear));
        //    Command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    Command.Parameters.Add(new SqlParameter("iBeginDate", BeginDate));
        //    Command.Parameters.Add(new SqlParameter("iEndDate", EndDate));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.Update {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool AccessUpdate()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_TariffHeader_Update", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Command.Parameters.Add(new OleDbParameter("iTariffYear", TariffYear));
        //    Command.Parameters.Add(new OleDbParameter("iNumber", Number));
        //    Command.Parameters.Add(new OleDbParameter("iBeginDate", BeginDate));
        //    Command.Parameters.Add(new OleDbParameter("iEndDate", EndDate));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.AccessUpdate {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(Guid Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_TariffHeader_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.Delete {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;

        //    }

        //}

        //public static bool AccessDelete(Guid Code)
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_TariffHeader_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
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
        //        ed.WriteMessage(string.Format(" ERROR BTariffHeader.AccessDelete {0}\n", ex1.Message));

        //        Connection.Close();
        //        return false;

        //    }

        //}

        //public static BTariffHeader SelectByCode(Guid Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("B_TariffHeader_Select", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    BTariffHeader header = new BTariffHeader();
        //    if (reader.Read())
        //    {
        //        header.BeginDate = reader["BeginDate"].ToString();
        //        header.Code = new Guid(reader["Code"].ToString());
        //        header.EndDate = reader["EndDate"].ToString();
        //        header.Number = Convert.ToInt32(reader["Number"].ToString());
        //        header.TariffYear = Convert.ToInt32(reader["TariffYear"].ToString());

        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return header;

        //}

        //public static BTariffHeader AccessSelectByCode(Guid Code)
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbCommand Command = new OleDbCommand("B_TariffHeader_Select", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    Connection.Open();
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    BTariffHeader header = new BTariffHeader();
        //    if (reader.Read())
        //    {
        //        header.BeginDate = reader["BeginDate"].ToString();
        //        header.Code = new Guid(reader["Code"].ToString());
        //        header.EndDate = reader["EndDate"].ToString();
        //        header.Number = Convert.ToInt32(reader["Number"].ToString());
        //        header.TariffYear = Convert.ToInt32(reader["TariffYear"].ToString());

        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return header;

        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_TariffHeader_Select", Connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Guid.Empty));
        //    DataSet dsHeader = new DataSet();
        //    adapter.Fill(dsHeader);
        //    return dsHeader.Tables[0];

        //}

        //public static DataTable AccessSelectAll()
        //{
        //    OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

        //    OleDbDataAdapter adapter = new OleDbDataAdapter("B_TariffHeader_Select", Connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", Guid.Empty));
        //    DataSet dsHeader = new DataSet();
        //    adapter.Fill(dsHeader);
        //    return dsHeader.Tables[0];

        //}
    }
}
