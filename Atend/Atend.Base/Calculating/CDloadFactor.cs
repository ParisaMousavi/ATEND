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
    public class CDloadFactor
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        private double amper;

        public double Amper
        {
            get { return amper; }
            set { amper = value; }
        }

        private int phaseCount;

        public int PhaseCount
        {
            get { return phaseCount; }
            set { phaseCount = value; }
        }

        private double factorPower;

        public double FactorPower
        {
            get { return factorPower; }
            set { factorPower = value; }
        }

        private double factorConcurency;

        public double FactorConcurency
        {
            get { return factorConcurency; }
            set { factorConcurency = value; }
        }

        private double voltage;

        public double Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //public bool Insert()
        //{
        //    //ed.writeMessage("Enter Insert\n");
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_DLoadFactor_Insert", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iFactorConcurency", FactorConcurency));
        //    command.Parameters.Add(new SqlParameter("iFactorPower", FactorPower));
        //    command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
        //    command.Parameters.Add(new SqlParameter("iVoltage", Voltage));

        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("CDloadFActor.Insert Error" + ex.Message + "\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_DLoadFActor_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iFactorConcurency", FactorConcurency));
        //    command.Parameters.Add(new SqlParameter("iFactorPower", FactorPower));
        //    command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
        //    command.Parameters.Add(new SqlParameter("iVoltage", Voltage));

        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error In Update LoadFactorUpdate: " + ex.Message + "\n");
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_DLoadFactor_delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }


        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR C_LOadFActor.delete: {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static CDloadFactor SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_DLoadFactor_SelectByCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    CDloadFactor cLoadFActor = new CDloadFactor();
        //    if (reader.Read())
        //    {
        //        cLoadFActor.Code = Convert.ToInt32(reader["Code"].ToString());
        //        //Cnetworkcross.Clearance = Convert.ToSingle(reader["Clearance"].ToString());
        //        cLoadFActor.Name = reader["Name"].ToString();
        //        cLoadFActor.Amper = Convert.ToDouble(reader["Amper"]);
        //        cLoadFActor.FactorConcurency = Convert.ToDouble(reader["FactorConcurency"]);
        //        cLoadFActor.FactorPower = Convert.ToDouble(reader["FactorPower"]);
        //        cLoadFActor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
        //        cLoadFActor.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
        //    }

        //    reader.Close();

        //    connection.Close();
        //    return cLoadFActor;
        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_DLoadFactor_SelectAll", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    DataSet dsLoad = new DataSet();
        //    adapter.Fill(dsLoad);
        //    return dsLoad.Tables[0];
        //}

        //public static DataTable Search(string Name)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_DloadFactor_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
        //    DataSet dsLoad = new DataSet();
        //    adapter.Fill(dsLoad);
        //    return dsLoad.Tables[0];
        //}

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~


        public bool AccessInsert()
        {
            //ed.writeMessage("Enter Insert\n");
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_DLoadFactor_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new OleDbParameter("iFactorPower", FactorPower));
            command.Parameters.Add(new OleDbParameter("iFactorConcurency", FactorConcurency));
            command.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            //ed.WriteMessage("Amper={0}\n", Amper);
            //ed.WriteMessage("PhaseCount={0}\n", PhaseCount);
            //ed.WriteMessage("FactorPower={0}\n", FactorPower);
            //ed.WriteMessage("factorConcurrency={0}\n", FactorConcurency);
            //ed.WriteMessage("Voltage={0}\n", Voltage);
            //ed.WriteMessage("Name={0}\n", Name);


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("CDloadFActor.Insert Error" + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_DLoadFActor_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new OleDbParameter("iFactorPower", FactorPower));
            command.Parameters.Add(new OleDbParameter("iFactorConcurency", FactorConcurency));
            command.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            command.Parameters.Add(new OleDbParameter("iName", Name));

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //ed.WriteMessage("Code={0}\n",Code);
            //ed.WriteMessage("Amper={0}\n", Amper);
            //ed.WriteMessage("PhaseCount={0}\n", PhaseCount);
            //ed.WriteMessage("FactorPower={0}\n", FactorPower);
            //ed.WriteMessage("factorConcurrency={0}\n", FactorConcurency);
            //ed.WriteMessage("Voltage={0}\n", Voltage);
            //ed.WriteMessage("Name={0}\n", Name);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Update LoadFactorUpdate: " + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_DLoadFactor_delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }


            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR C_LOadFActor.delete: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static CDloadFactor AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_DLoadFactor_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            CDloadFactor cLoadFActor = new CDloadFactor();
            if (reader.Read())
            {
                cLoadFActor.Code = Convert.ToInt32(reader["Code"].ToString());
                //Cnetworkcross.Clearance = Convert.ToSingle(reader["Clearance"].ToString());
                cLoadFActor.Name = reader["Name"].ToString();
                cLoadFActor.Amper = Convert.ToDouble(reader["Amper"]);
                cLoadFActor.FactorConcurency = Convert.ToDouble(reader["FactorConcurency"]);
                cLoadFActor.FactorPower = Convert.ToDouble(reader["FactorPower"]);
                cLoadFActor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                cLoadFActor.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }

            reader.Close();

            connection.Close();
            return cLoadFActor;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_DLoadFactor_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsLoad = new DataSet();
            adapter.Fill(dsLoad);
            return dsLoad.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_DloadFactor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsLoad = new DataSet();
            adapter.Fill(dsLoad);
            return dsLoad.Tables[0];
        }

    }
}
