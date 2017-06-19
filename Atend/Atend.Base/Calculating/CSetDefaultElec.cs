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
    public class CSetDefaultElec
    {

        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private double maxDropVolt;

        public double MaxDropVolt
        {
            get { return maxDropVolt; }
            set { maxDropVolt = value; }
        }

        private double maxDropPower;

        public double MaxDropPower
        {
            get { return maxDropPower; }
            set { maxDropPower = value; }
        }


        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_SetDefaultElec_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iMaxDropVolt", MaxDropVolt));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxDropPower", MaxDropPower));


            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cSETDefaultElec.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_SetDefaultElec_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("MaxDropVolt", MaxDropVolt));
            command.Parameters.Add(new OleDbParameter("MaxDropPower", MaxDropPower));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in CSetDefaultElec={0}\n", ex.Message);
                connection.Close();
                return false;
            }
        }

        public static CSetDefaultElec AccessSelect()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_DefaultElec_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            //Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            CSetDefaultElec defaultElec = new CSetDefaultElec();
            if (reader.Read())
            {
                defaultElec.Code = Convert.ToInt32(reader["Code"].ToString());
                defaultElec.MaxDropVolt = Convert.ToDouble(reader["MaxDropVolt"].ToString());
                defaultElec.MaxDropPower = Convert.ToDouble(reader["MaxDropPower"].ToString());
            }
            else
            {
                defaultElec.Code = 0;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            Connection.Close();
            return defaultElec;
        }


    }
}
