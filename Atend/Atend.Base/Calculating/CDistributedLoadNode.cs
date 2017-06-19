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
    public class CDistributedLoadNode
    {
        private string nodeName;

        public string NodeName
        {
            get { return nodeName; }
            set { nodeName = value; }
        }

        private double voltAbs;

        public double VoltAbs
        {
            get { return voltAbs; }
            set { voltAbs = value; }
        }

        private double voltArg;

        public double VoltArg
        {
            get { return voltArg; }
            set { voltArg = value; }
        }

        private double dropVolt;

        public double DropVolt
        {
            get { return dropVolt; }
            set { dropVolt = value; }
        }

        private double loadPowerActive;

        public double LoadPowerActive
        {
            get { return loadPowerActive; }
            set { loadPowerActive = value; }
        }

        private double loadPower;

        public double LoadPower
        {
            get { return loadPower; }
            set { loadPower = value; }
        }

        private double loadCurrentAbs;

        public double LoadCurrentAbs
        {
            get { return loadCurrentAbs; }
            set { loadCurrentAbs = value; }
        }

        private double loadCurrentArg;

        public double LoadCurrentArg
        {
            get { return loadCurrentArg; }
            set { loadCurrentArg = value; }
        }


        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_DistributedLoadNode_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iNodeName", NodeName));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltAbs", VoltAbs));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltArg", VoltArg));
            insertCommand.Parameters.Add(new OleDbParameter("iDropVolt", DropVolt));
            insertCommand.Parameters.Add(new OleDbParameter("iLoadPowerActive", LoadPowerActive));
            insertCommand.Parameters.Add(new OleDbParameter("iLoadPower", LoadPower));
            insertCommand.Parameters.Add(new OleDbParameter("iLoadCurrentAbs", LoadCurrentAbs));
            insertCommand.Parameters.Add(new OleDbParameter("iLoadCurrentArg", LoadCurrentArg));


            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cDistributedLoadNode.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_DistributedLoadNode_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DistributedLoadNode_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CDistributedLoadNode.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
