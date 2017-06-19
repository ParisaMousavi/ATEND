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
    public class CSetDefaultMec
    {

        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private double uts;

        public double Uts
        {
            get { return uts; }
            set { uts = value; }
        }

        private double start;

        public double Start
        {
            get { return start; }
            set { start = value; }
        }

        private double end;

        public double End
        {
            get { return end; }
            set { end = value; }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private double trustBorder;

        public double TrustBorder
        {
            get { return trustBorder; }
            set { trustBorder = value; }
        }

        private int netCross;

        public int NetCross
        {
            get { return netCross; }
            set { netCross = value; }
        }
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_SetDefaultMec_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iUTS", Uts));
            insertCommand.Parameters.Add(new OleDbParameter("iStart", Start));
            insertCommand.Parameters.Add(new OleDbParameter("iEnd", End));
            insertCommand.Parameters.Add(new OleDbParameter("iDistance", Distance));
            insertCommand.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            insertCommand.Parameters.Add(new OleDbParameter("iNetCross", NetCross));

            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cSETDefaultMec.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_SetDefaultMec_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iUts", Uts));
            command.Parameters.Add(new OleDbParameter("iStart", Start));
            command.Parameters.Add(new OleDbParameter("iEnd", End));
            command.Parameters.Add(new OleDbParameter("iDistance", Distance));
            command.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            command.Parameters.Add(new OleDbParameter("iNetCross", NetCross));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in CSetDefaultMec={0}\n", ex.Message);
                connection.Close();
                return false;
            }
        }

        public static CSetDefaultMec AccessSelect()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_SetDefaultMec_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            CSetDefaultMec defaultMec = new CSetDefaultMec();
            if (reader.Read())
            {
                defaultMec.Code = Convert.ToInt32(reader["Code"].ToString());
                defaultMec.Uts = Convert.ToDouble(reader["UTS"].ToString());
                defaultMec.Start = Convert.ToDouble(reader["Start"].ToString());
                defaultMec.End = Convert.ToDouble(reader["End"].ToString());
                defaultMec.Distance = Convert.ToDouble(reader["Distance"].ToString());
                defaultMec.TrustBorder = Convert.ToDouble(reader["TrustBorder"].ToString());
                defaultMec.NetCross = Convert.ToInt32(reader["NetCross"].ToString());

            }
            else
            {
                defaultMec.Code = 0;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            Connection.Close();
            return defaultMec;
        }

    }
}
