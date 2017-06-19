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
    public class CSagAndTension
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
        private string conductorName;

        public string ConductorName
        {
            get { return conductorName; }
            set { conductorName = value; }
        }

        private double normH;

        public double NormH
        {
            get { return normH; }
            set { normH = value; }
        }

        private double normF;

        public double NormF
        {
            get { return normF; }
            set { normF = value; }
        }

        private double iceH;

        public double IceH
        {
            get { return iceH; }
            set { iceH = value; }
        }

        private double iceF;

        public double IceF
        {
            get { return iceF; }
            set { iceF = value; }
        }
        private double windH;

        public double WindH
        {
            get { return windH; }
            set { windH = value; }
        }

        private double windF;

        public double WindF
        {
            get { return windF; }
            set { windF = value; }
        }

        private double maxTempH;

        public double MaxTempH
        {
            get { return maxTempH; }
            set { maxTempH = value; }
        }
        private double maxTempF;

        public double MaxTempF
        {
            get { return maxTempF; }
            set { maxTempF = value; }
        }

        private double windAndIceH;

        public double WindAndIceH
        {
            get { return windAndIceH; }
            set { windAndIceH = value; }
        }
        private double windAndIceF;

        public double WindAndIceF
        {
            get { return windAndIceF; }
            set { windAndIceF = value; }
        }

        private bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

        private double minTempH;

        public double MinTempH
        {
            get { return minTempH; }
            set { minTempH = value; }
        }
        private double minTempF;

        public double MinTempF
        {
            get { return minTempF; }
            set { minTempF = value; }
        }


        private double maxF;

        public double MaxF
        {
            get { return maxF; }
            set { maxF = value; }
        }

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_SagAndTension_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iConductorName", ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iNormH", NormH));
            insertCommand.Parameters.Add(new OleDbParameter("iNormF", NormF));
            insertCommand.Parameters.Add(new OleDbParameter("iIceH", IceH));
            insertCommand.Parameters.Add(new OleDbParameter("iIceF", IceF));
            insertCommand.Parameters.Add(new OleDbParameter("iWindH", WindH));
            insertCommand.Parameters.Add(new OleDbParameter("iWindF", WindF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxTempH", MaxTempH));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxTempF", MaxTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceH", WindAndIceH));
            insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceF", WindAndIceF));
            insertCommand.Parameters.Add(new OleDbParameter("IsUTS", IsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMinTempH", MinTempH));
            insertCommand.Parameters.Add(new OleDbParameter("iMinTempF", MinTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxF", MaxF));

            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error DSagAndTension.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public bool AccessInsert(OleDbConnection _Connection, OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_SagAndTension_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _Transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iConductorName", ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iNormH", NormH));
            insertCommand.Parameters.Add(new OleDbParameter("iNormF", NormF));
            insertCommand.Parameters.Add(new OleDbParameter("iIceH", IceH));
            insertCommand.Parameters.Add(new OleDbParameter("iIceF", IceF));
            insertCommand.Parameters.Add(new OleDbParameter("iWindH", WindH));
            insertCommand.Parameters.Add(new OleDbParameter("iWindF", WindF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxTempH", MaxTempH));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxTempF", MaxTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceH", WindAndIceH));
            insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceF", WindAndIceF));
            insertCommand.Parameters.Add(new OleDbParameter("IsUTS", IsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMinTempH", MinTempH));
            insertCommand.Parameters.Add(new OleDbParameter("iMinTempF", MinTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxF", MaxF));

            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();


                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error DSagAndTension.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }


        public static DataTable AccessSelect(Guid SectionCode, bool isUTS)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
            DataSet dsSagAndTension = new DataSet();
            adapter.Fill(dsSagAndTension);
            return dsSagAndTension.Tables[0];
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode, bool isUTS)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_SelectBySectionCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
            DataSet dsSagAndTension = new DataSet();
            adapter.Fill(dsSagAndTension);
            return dsSagAndTension.Tables[0];
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode, bool isUTS,OleDbConnection _connection,OleDbTransaction _Transaction)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_SelectBySectionCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _Transaction;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
            DataSet dsSagAndTension = new DataSet();
            adapter.Fill(dsSagAndTension);
            return dsSagAndTension.Tables[0];
        }


        public static DataTable AccessSelectByIsUTS(bool isUTS)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("*\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            ed.WriteMessage("**\n");
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_SelectByIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
            DataSet dsSagAndTension = new DataSet();
            adapter.Fill(dsSagAndTension);
            return dsSagAndTension.Tables[0];
        }

        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_SagAndTension_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CSagTension.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode, bool IsUTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_SagAndTension_DeleteBySectionCode", Connection);
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
                ed.WriteMessage(string.Format("Error CSagTension.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode, bool IsUTS,OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _Connection;
            OleDbCommand Command = new OleDbCommand("C_SagAndTension_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _Transaction;

            Command.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            Command.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));
            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                //Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CSagTension.AccessDelete : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }
    }
}
