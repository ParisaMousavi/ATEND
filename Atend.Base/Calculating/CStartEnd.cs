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
    public class CStartEnd
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

        private Guid startPole;

        public Guid StartPole
        {
            get { return startPole; }
            set { startPole = value; }
        }

        private Guid endPole;

        public Guid EndPole
        {
            get { return endPole; }
            set { endPole = value; }
        }


        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_StartEnd_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iStartPole", StartPole));
            insertCommand.Parameters.Add(new OleDbParameter("iEndPole", EndPole));

            try
            {

                con.Open();
                //ed.WriteMessage("BEFORINSERT\n");
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("INSERTSTARTENDPOLE\n");
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cStartEnd.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public static CStartEnd  AccessSelectBySectionCode(Guid SectionCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_StartEnd_SelectBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            CStartEnd startEnd = new CStartEnd();
            if (reader.Read())
            {
                startEnd.Code = Convert.ToInt32(reader["Code"].ToString());
                startEnd.EndPole = new Guid(reader["EndPole"].ToString());
                startEnd.StartPole = new Guid(reader["StartPole"].ToString());
                startEnd.SectionCode = new Guid(reader["SectionCode"].ToString());
            }
            else
            {
                startEnd.Code = 0;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            Connection.Close();
            return startEnd;
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_StartEnd_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CStartEnd.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(OleDbConnection aconnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection =aconnection;
            OleDbCommand Command = new OleDbCommand("C_StartEnd_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            try
            {
               // Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CStartEnd.AccessDelete : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }
    }
}
