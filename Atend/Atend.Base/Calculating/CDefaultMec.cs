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
    public class CDefaultMec
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

        private double uts;

        public double Uts
        {
            get { return uts; }
            set { uts = value; }
        }

        private double vol;

        public double Vol
        {
            get { return vol; }
            set { vol = value; }
        }
        private int netCross;

        public int NetCross
        {
            get { return netCross; }
            set { netCross = value; }
        }

        private double trustBorder;

        public double TrustBorder
        {
            get { return trustBorder; }
            set { trustBorder = value; }
        }

        private bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

        private double sE;

        public double SE
        {
            get { return sE; }
            set { sE = value; }
        }

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_DefaultMec_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", Uts));
            insertCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
            insertCommand.Parameters.Add(new OleDbParameter("iNetCross", NetCross));
            insertCommand.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            insertCommand.Parameters.Add(new OleDbParameter("IsUTS", IsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("iSE", SE));
            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cDefaultMec.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public bool AccessInsert(OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            


            OleDbConnection connection = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_DefaultMec_Insert", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _Transaction;



            //OleDbCommand insertCommand = new OleDbCommand("C_DefaultMec_Insert", con);
            //insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", Uts));
            insertCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
            insertCommand.Parameters.Add(new OleDbParameter("iNetCross", NetCross));
            insertCommand.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            insertCommand.Parameters.Add(new OleDbParameter("IsUTS", IsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("iSE", SE));
            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cDefaultMec.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_DefaultMec_UpdateBySectionCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("UPDATE/n");
            command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            command.Parameters.Add(new OleDbParameter("iUTS", Uts));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            command.Parameters.Add(new OleDbParameter("iNetCross", NetCross));
            command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            command.Parameters.Add(new OleDbParameter("iSE", SE));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in CDefaultMec={0}\n", ex.Message);
                connection.Close();
                return false;
            }
        }

        public bool AccessUpdate(OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("C_DefaultMec_UpdateBySectionCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _Transaction;



           
            //ed.WriteMessage("UPDATE/n");
            command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            command.Parameters.Add(new OleDbParameter("iUTS", Uts));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iTrustBorder", TrustBorder));
            command.Parameters.Add(new OleDbParameter("iNetCross", NetCross));
            command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            command.Parameters.Add(new OleDbParameter("iSE", SE));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in CDefaultMec={0}\n", ex.Message);
                //connection.Close();
                return false;
            }
        }

        public static CDefaultMec AccessSelectBySectionCode(Guid SectionCode, bool isUts)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultMec_SelectBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Command.Parameters.Add(new OleDbParameter("iISUTS", isUts));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            CDefaultMec defaultMec = new CDefaultMec();
            if (reader.Read())
            {
                defaultMec.Code = Convert.ToInt32(reader["Code"].ToString());
                defaultMec.NetCross = Convert.ToInt32(reader["NetCross"].ToString());
                defaultMec.SectionCode = new Guid(reader["SectionCode"].ToString());
                defaultMec.Uts = Convert.ToDouble(reader["UTS"].ToString());
                defaultMec.Vol = Convert.ToDouble(reader["Vol"].ToString());
                defaultMec.TrustBorder = Convert.ToDouble(reader["TrustBorder"].ToString());
                defaultMec.IsUTS = Convert.ToBoolean(reader["IsUTS"].ToString());
                defaultMec.SE = Convert.ToDouble(reader["SE"].ToString());
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

        public static bool Delete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultMec_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CDefaultMec.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
