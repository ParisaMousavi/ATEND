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
    public class CTemp
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int conductorDayCode;

        public int ConductorDayCode
        {
            get { return conductorDayCode; }
            set { conductorDayCode = value; }
        }

        private double temp;

        public double Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        private double sag;

        public double Sag
        {
            get { return sag; }
            set { sag = value; }
        }

        private double tension;

        public double Tension
        {
            get { return tension; }
            set { tension = value; }
        }


        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_Temp_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iConductorDayCode", ConductorDayCode));
            insertCommand.Parameters.Add(new OleDbParameter("iTemp", Temp));
            insertCommand.Parameters.Add(new OleDbParameter("iSag", Sag));

            insertCommand.Parameters.Add(new OleDbParameter("iTension", Tension));
            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cTemp.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public bool AccessInsert(OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_Temp_Insert", con);
            insertCommand.Transaction = _Transaction;
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iConductorDayCode", ConductorDayCode));
            insertCommand.Parameters.Add(new OleDbParameter("iTemp", Temp));
            insertCommand.Parameters.Add(new OleDbParameter("iSag", Sag));

            insertCommand.Parameters.Add(new OleDbParameter("iTension", Tension));
            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cTemp.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_Temp_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //Command.Parameters.Add(new OleDbParameter("ConductorDayCode",conductorDay));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CTemp.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
