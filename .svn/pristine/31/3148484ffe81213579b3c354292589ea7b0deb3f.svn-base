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
    public class CConductorDay
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid sectionCode;

        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }

        private string from;

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        private string to;

        public string To
        {
            get { return to; }
            set { to = value; }
        }

        private double spanLenght;

        public double SpanLenght
        {
            get { return spanLenght; }
            set { spanLenght = value; }
        }

        private string conductorName;

        public string ConductorName
        {
            get { return conductorName; }
            set { conductorName = value; }
        }

        private bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

       

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_ConductorDay_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iFrom", From));
            insertCommand.Parameters.Add(new OleDbParameter("iTo", To));
            insertCommand.Parameters.Add(new OleDbParameter("iSpanLenght",SpanLenght));
            insertCommand.Parameters.Add(new OleDbParameter("iConductorName",ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iIsUTS",IsUTS));
            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();

                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cConductorDay.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public bool AccessInsert(OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_ConductorDay_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _Transaction;

            ed.WriteMessage("SectionCode={0}, From={1} ,To={2} ,SpanLenght={3} ,CondName={4} ,UTs={5}",SectionCode,From,To,SpanLenght,ConductorName,IsUTS);

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iFrom", From));
            insertCommand.Parameters.Add(new OleDbParameter("iTo", To));
            insertCommand.Parameters.Add(new OleDbParameter("iSpanLenght", SpanLenght));
            insertCommand.Parameters.Add(new OleDbParameter("iConductorName", ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();

                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cConductorDay.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }
  
        public static DataTable  AccessSelectBySectionCode(Guid SectionCode,bool isUts)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_ConductorDay_SelectBySectionCodeIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS",isUts));
            DataSet dsConductorday = new DataSet();
            adapter.Fill(dsConductorday);
            return dsConductorday.Tables[0];
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode, bool isUts,OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection =_Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_ConductorDay_SelectBySectionCodeIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _Transaction;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUts));
            DataSet dsConductorday = new DataSet();
            adapter.Fill(dsConductorday);
            return dsConductorday.Tables[0];
        }

        public static DataTable AccessSelect(Guid SectionCode, bool isUts)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_ConductorDay_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUts));
            DataSet dsConductorday = new DataSet();
            adapter.Fill(dsConductorday);
            return dsConductorday.Tables[0];
        }

        public static DataTable AccessSelectFindCountOfSection(bool isUts)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_ConductorDay_FindCountOfSection", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUts));
            DataSet dsConductorday = new DataSet();
            adapter.Fill(dsConductorday);
            return dsConductorday.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_ConductorDay_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CConductorDay.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode,bool IsUTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_ConductorDay_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CConductorDay.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode, bool IsUTS,OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _Connection;
            OleDbCommand Command = new OleDbCommand("C_ConductorDay_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _Transaction;

            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                //Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CConductorDay.AccessDelete : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }
    }
}
