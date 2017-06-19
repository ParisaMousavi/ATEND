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
    public class CMaxTension
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        private Guid sectionCode;

    
        //private string conductorName;

        //public string ConductorName
        //{
        //    get { return conductorName; }
        //    set { conductorName = value; }
        //}

        private int sagTensionCode;

        public int SagTensionCode
        {
            get { return sagTensionCode; }
            set { sagTensionCode = value; }
        }

        private double maxNormH;

        public double MaxNormH
        {
            get { return maxNormH; }
            set { maxNormH = value; }
        }





        private double maxIceH;

        public double MaxIceH
        {
            get { return maxIceH; }
            set { maxIceH = value; }
        }






        private double maxWindH;

        public double MaxWindH
        {
            get { return maxWindH; }
            set { maxWindH = value; }
        }





        private double maxMaxTempH;

        public double MaxMaxTempH
        {
            get { return maxMaxTempH; }
            set { maxMaxTempH = value; }
        }




        private double maxWindAndIceH;

        public double MaxWindAndIceH
        {
            get { return maxWindAndIceH; }
            set { maxWindAndIceH = value; }
        }

       
      

        private bool isUTS;

        public bool IsUTS
        {
            get { return isUTS; }
            set { isUTS = value; }
        }

        private double maxMinTempH;

        public double MaxMinTempH
        {
            get { return maxMinTempH; }
            set { maxMinTempH = value; }
        }

        
       
       
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("C_SagAndTension_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            //insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //insertCommand.Parameters.Add(new OleDbParameter("iConductorName", ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxNormH", MaxNormH));
            //insertCommand.Parameters.Add(new OleDbParameter("iNormF", NormF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxIceH", MaxIceH));
            //insertCommand.Parameters.Add(new OleDbParameter("iIceF", IceF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxWindH", MaxWindH));
            //insertCommand.Parameters.Add(new OleDbParameter("iWindF", WindF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxMaxTempH", MaxMaxTempH));
            //insertCommand.Parameters.Add(new OleDbParameter("iMaxTempF", MaxTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxWindAndIceH", MaxWindAndIceH));
            //insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceF", WindAndIceF));
            insertCommand.Parameters.Add(new OleDbParameter("IsUTS", IsUTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxMinTempH",MaxMinTempH));
            //insertCommand.Parameters.Add(new OleDbParameter("iMinTempF", MinTempF));
            //insertCommand.Parameters.Add(new OleDbParameter("iMaxF",MaxF));

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


        public bool AccessInsert(OleDbConnection _Connection,OleDbTransaction _Tranaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection con = _Connection;
            OleDbCommand insertCommand = new OleDbCommand("C_MaxTension_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _Tranaction;

            //insertCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
            //insertCommand.Parameters.Add(new OleDbParameter("iConductorName", ConductorName));
            insertCommand.Parameters.Add(new OleDbParameter("iSagTensionCode",SagTensionCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxNormH", MaxNormH));
            //insertCommand.Parameters.Add(new OleDbParameter("iNormF", NormF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxIceH", MaxIceH));
            //insertCommand.Parameters.Add(new OleDbParameter("iIceF", IceF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxWindH", MaxWindH));
            //insertCommand.Parameters.Add(new OleDbParameter("iWindF", WindF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxMaxTempH", MaxMaxTempH));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxMinTempH", MaxMinTempH));

            //insertCommand.Parameters.Add(new OleDbParameter("iMaxTempF", MaxTempF));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxWindAndIceH", MaxWindAndIceH));
            //insertCommand.Parameters.Add(new OleDbParameter("iWindAndIceF", WindAndIceF));
            //insertCommand.Parameters.Add(new OleDbParameter("iMinTempF", MinTempF));
            //insertCommand.Parameters.Add(new OleDbParameter("iMaxF",MaxF));

            try
            {

                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error DMaxTension.AccessInsert : {0} \n", ex.Message));
                //con.Close();
                return false;
            }
        }


        //public static DataTable AccessSelectBySectionCode(Guid SectionCode, bool isUTS)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_SelectBySectionCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSectionCode", SectionCode));
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
        //    DataSet dsSagAndTension = new DataSet();
        //    adapter.Fill(dsSagAndTension);
        //    return dsSagAndTension.Tables[0];
        //}

        //public static DataTable AccessSelectByIsUTS(bool isUTS)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("C_SagAndTension_SelectByIsUTS", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsUTS", isUTS));
        //    DataSet dsSagAndTension = new DataSet();
        //    adapter.Fill(dsSagAndTension);
        //    return dsSagAndTension.Tables[0];
        //}

        //public static bool AccessDelete()
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand Command = new OleDbCommand("C_SagAndTension_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error CSagTension.AccessDelete : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //}

        public static bool AccessDeleteBysagTensionCode(int SagTensioncode,OleDbTransaction _Transaction,OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection =_Connection;
            OleDbCommand Command = new OleDbCommand("C_MaxTension_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _Transaction;
            Command.Parameters.Add(new OleDbParameter("iSagTensionCode", SagTensioncode));
            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                //Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error CMaxTension.AccessDelete : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }
    }
}
