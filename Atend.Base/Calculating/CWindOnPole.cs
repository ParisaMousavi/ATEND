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
    public class CWindOnPole
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

        private string dcPole;

        public string DcPole
        {
            get { return dcPole; }
            set { dcPole = value; }
        }
        private double dcNorm;

        public double DcNorm
        {
            get { return dcNorm; }
            set { dcNorm = value; }
        }

        private double dcIceHeavy;

        public double DcIceHeavy
        {
            get { return dcIceHeavy; }
            set { dcIceHeavy = value; }
        }

        private double dcWindSpeed;

        public double DcWindSpeed
        {
            get { return dcWindSpeed; }
            set { dcWindSpeed = value; }
        }

        private double dcMaxTemp;

        public double DcMaxTemp
        {
            get { return dcMaxTemp; }
            set { dcMaxTemp = value; }
        }

        private double dcMinTemp;

        public double DcMinTemp
        {
            get { return dcMinTemp; }
            set { dcMinTemp = value; }
        }

        private double dcwindIce;

        public double DcwindIce
        {
            get { return dcwindIce; }
            set { dcwindIce = value; }
        }

        private bool dcIsUTS;

        public bool DcIsUTS
        {
            get { return dcIsUTS; }
            set { dcIsUTS = value; }
        }

        private Guid dcPoleGuid;

        public Guid DcPoleGuid
        {
            get { return dcPoleGuid; }
            set { dcPoleGuid = value; }
        }
        private double dcAngle;

        public double DcAngle
        {
            get { return dcAngle; }
            set { dcAngle = value; }
        }
       

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_WindOnPole_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("idcPole", DcPole));
            insertCommand.Parameters.Add(new OleDbParameter("idcNorm", DcNorm));
            insertCommand.Parameters.Add(new OleDbParameter("idcIceHeavy", DcIceHeavy));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindSpeed", DcWindSpeed));
            insertCommand.Parameters.Add(new OleDbParameter("idcMaxTemp", DcMaxTemp));

            insertCommand.Parameters.Add(new OleDbParameter("idcMinTemp", DcMinTemp));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindIce", DcwindIce));
            insertCommand.Parameters.Add(new OleDbParameter("idcIsUTS", DcIsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("idcPoleGuid", DcPoleGuid));
            insertCommand.Parameters.Add(new OleDbParameter("idcAngle", DcAngle));
            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cWindOnPole.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }


        public bool AccessInsert(OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_WindOnPole_Insert", con);
            insertCommand.Transaction = _Transaction;
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("idcPole", DcPole));
            insertCommand.Parameters.Add(new OleDbParameter("idcNorm", DcNorm));
            insertCommand.Parameters.Add(new OleDbParameter("idcIceHeavy", DcIceHeavy));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindSpeed", DcWindSpeed));
            insertCommand.Parameters.Add(new OleDbParameter("idcMaxTemp", DcMaxTemp));

            insertCommand.Parameters.Add(new OleDbParameter("idcMinTemp", DcMinTemp));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindIce", DcwindIce));
            insertCommand.Parameters.Add(new OleDbParameter("idcIsUTS", DcIsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("idcPoleGuid", DcPoleGuid));
            insertCommand.Parameters.Add(new OleDbParameter("idcAngle", DcAngle));
            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cWindOnPole.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode,bool IsUTS)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_WindOnPole_SelectBySectionCode", connection);
            
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));

            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode, bool IsUTS,OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_WindOnPole_SelectBySectionCode", connection);
            adapter.SelectCommand.Transaction = _Transaction;

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
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_WindOnPole_SelectByIsUTS", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", IsUTS));

            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }


        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_WindOnPole_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CWindOnPole.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode,bool IsUTS)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_WindOnPole_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode",SectionCode));
            Command.Parameters.Add(new OleDbParameter("iIsUTS",IsUTS));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CWindOnPole.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode, bool IsUTS,OleDbConnection _Connection,OleDbTransaction _Transaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_WindOnPole_DeleteBySectionCode", Connection);
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
                ed.WriteMessage(string.Format("Error CWindOnPole.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
