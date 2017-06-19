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
    public class CTransformer
    {

        private double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        private double load;

        public double Load
        {
            get { return load; }
            set { load = value; }
        }

        private double result;

        public double Result
        {
            get { return result; }
            set { result = value; }
        }

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_Transformer_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iHeight", Height));
            insertCommand.Parameters.Add(new OleDbParameter("iLoad", Load));
            insertCommand.Parameters.Add(new OleDbParameter("iResult", Result));

            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cTransformer.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_Transformer_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_Transformer_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CTransformer.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

      

    }
}
