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
    public class CRudSurface
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



        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_RudSurface_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            insertCommand.Parameters.Add(new OleDbParameter("idcPole", DcPole));
            insertCommand.Parameters.Add(new OleDbParameter("idcNorm", DcNorm));
            insertCommand.Parameters.Add(new OleDbParameter("idcIceHeavy", DcIceHeavy));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindSpeed", DcWindSpeed));
            insertCommand.Parameters.Add(new OleDbParameter("idcMaxTemp", DcMaxTemp));

            insertCommand.Parameters.Add(new OleDbParameter("idcMinTemp", DcMinTemp));
            insertCommand.Parameters.Add(new OleDbParameter("idcWindIce", DcwindIce));
          
            try
            {

                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error cRudSurface.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        public static DataTable AccessSelectBySectionCode(Guid SectionCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_RudSurface_SelectBySectionCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));

            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static DataTable AccessSelect()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_RudSurface_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }

        public static DataTable AccessSelectCountOfSection()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_RudSurface_SelectCountOfSection", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsWindOnPole = new DataSet();
            adapter.Fill(dsWindOnPole);
            return dsWindOnPole.Tables[0];
        }
 
        public static bool AccessDelete()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_RudSurface_Delete", Connection);
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
                ed.WriteMessage(string.Format("Error CRudSurface.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDeleteBySectionCode(Guid SectionCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("C_RudSurface_DeleteBySectionCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iSectionCode",SectionCode));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CRudSurface.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }
    }
}
