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
    public class CCrossSection
    {
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

        private string existCond;

        public string ExistCond
        {
            get { return existCond; }
            set { existCond = value; }
        }

        private double lenght;

        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        private string commentCond;

        public string CommentCond
        {
            get { return commentCond; }
            set { commentCond = value; }
        }

        private double crossSection;

        public double CrossSection
        {
            get { return crossSection; }
            set { crossSection = value; }
        }

        private double current;

        public double Current
        {
            get { return current; }
            set { current = value; }
        }

        private double volt;

        public double Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        private double powerLoss;

        public double PowerLoss
        {
            get { return powerLoss; }
            set { powerLoss = value; }
        }

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_CrossSection_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iFrom", From));
            insertCommand.Parameters.Add(new OleDbParameter("iTo", To));
            insertCommand.Parameters.Add(new OleDbParameter("iExistCond",ExistCond));
            insertCommand.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new OleDbParameter("iCommentCond", CommentCond));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSection", CrossSection));
            insertCommand.Parameters.Add(new OleDbParameter("iCurrent", Current));
            insertCommand.Parameters.Add(new OleDbParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new OleDbParameter("iPowerLoss", PowerLoss));



            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cCrossSection.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_CrossSection_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_CrossSection_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CCrossSection.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
