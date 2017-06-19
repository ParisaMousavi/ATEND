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
    public class CDistributedLoadBranch
    {
        private string branchName;

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }

        private double lenght;

        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        private double currentAbs;

        public double CurrentAbs
        {
            get { return currentAbs; }
            set { currentAbs = value; }
        }

        private double currentArg;

        public double CurrentArg
        {
            get { return currentArg; }
            set { currentArg = value; }
        }

        private double condutilization;

        public double Condutilization
        {
            get { return condutilization; }
            set { condutilization = value; }
        }

        private double totalLoad;

        public double TotalLoad
        {
            get { return totalLoad; }
            set { totalLoad = value; }
        }

        private string   condName;

        public string   CondName
        {
            get { return condName; }
            set { condName = value; }
        }

        private double condCurrent;

        public double CondCurrent
        {
            get { return condCurrent; }
            set { condCurrent = value; }
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
            OleDbCommand insertCommand = new OleDbCommand("C_DistributedLoadBranch_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iBranchName", BranchName));
            insertCommand.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            insertCommand.Parameters.Add(new OleDbParameter("iCurrentAbs", CurrentAbs));
            insertCommand.Parameters.Add(new OleDbParameter("iCurrentArg", CurrentArg));
            insertCommand.Parameters.Add(new OleDbParameter("iCondutilization", Condutilization));
            insertCommand.Parameters.Add(new OleDbParameter("iTotalLoad", TotalLoad));
            insertCommand.Parameters.Add(new OleDbParameter("iCondName",CondName));
            insertCommand.Parameters.Add(new OleDbParameter("iConCurrent",CondCurrent));
            insertCommand.Parameters.Add(new OleDbParameter("iFrom",From));
            insertCommand.Parameters.Add(new OleDbParameter("iTo", To));

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
                ed.WriteMessage(string.Format("Error cDistributedLoadBranch.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_DistributedLoadBranch_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DistributedLoadBranch_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CDistributedLoadBranch.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
