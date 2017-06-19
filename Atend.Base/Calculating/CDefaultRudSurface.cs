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
    public class CDefaultRudSurface
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private double uTS;

        public double UTS
        {
            get { return uTS; }
            set { uTS = value; }
        }

        private int netCross;

        public int NetCross
        {
            get { return netCross; }
            set { netCross = value; }
        }

        private Guid sectionCode;

        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_DefaultRudSurface_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", UTS));
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
                ed.WriteMessage(string.Format("Error cDefaultRudSurface.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public static CDefaultRudSurface AccessSelectBySectionCode(Guid SectionCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultRudSurface_SelectBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            CDefaultRudSurface  defaultRudSurface = new CDefaultRudSurface();
            if (reader.Read())
            {
                defaultRudSurface.Code = Convert.ToInt32(reader["Code"].ToString());
                defaultRudSurface.NetCross = Convert.ToInt32(reader["NetCross"].ToString());
                defaultRudSurface.SectionCode = new Guid(reader["SectionCode"].ToString());
                defaultRudSurface.UTS = Convert.ToDouble(reader["UTS"].ToString());
               
            }
            else
            {
                defaultRudSurface.Code = 0;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            Connection.Close();
            return defaultRudSurface;
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultRudSurface_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CDefaultRudSurface.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultRudSurface_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CDefaultRudSurface.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand UpdateCommand = new OleDbCommand("C_DefaultRudSurface_Update", con);
            UpdateCommand.CommandType = CommandType.StoredProcedure;

            UpdateCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            UpdateCommand.Parameters.Add(new OleDbParameter("iUTS", UTS));
            UpdateCommand.Parameters.Add(new OleDbParameter("iNetCross", NetCross));

            try
            {

                con.Open();
                UpdateCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cDefaultRudSurface.AccessUpdate : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

    }
}
