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
    public class CPowerWithOutHalter
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid poleGuid;

        public Guid PoleGuid
        {
            get { return poleGuid; }
            set { poleGuid = value; }
        }

        private byte poleCount;

        public byte PoleCount
        {
            get { return poleCount; }
            set { poleCount = value; }
        }

        private double polePower;

        public double PolePower
        {
            get { return polePower; }
            set { polePower = value; }
        }


        private Guid sectionCode;

        public Guid SectionCode
        {
            get { return sectionCode; }
            set { sectionCode = value; }
        }

        private bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

        private string poleNum;

        public string PoleNum
        {
            get { return poleNum; }
            set { poleNum = value; }
        }
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_PowerWithOutHalter_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iPoleGuid", PoleGuid));
            insertCommand.Parameters.Add(new OleDbParameter("iPoleCount", PoleCount));
            insertCommand.Parameters.Add(new OleDbParameter("iPolePower", PolePower));
            insertCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iPoleNum", PoleNum));

            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cPowerWitOuthHalter.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public static DataTable AccessSelectBySectionCodeISUTS(Guid SectionCode, bool IsUTS)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_PowerWithOutHalter_SelectBySectionCodeIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));

            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static DataTable AccessSelectByISUTS(bool IsUTS)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_PowerWithOutHalter_SelectByIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));

            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static DataTable AccessSelectCountOfSection()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_PowerWithOutHalter_SelectCountOfSection", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_PowerWithOutHalter_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CPowerWithHalter.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCodeIsUTS(Guid SectionCode, bool IsUTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_PowerWithOutHalter_DeleteBySectionCodeIsUTS", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CPowerWithOutHalter.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
