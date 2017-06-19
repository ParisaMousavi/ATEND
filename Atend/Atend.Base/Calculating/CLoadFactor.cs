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
    public class CLoadFactor
    {
        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }
        private int designCode;

        public int DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private double p;

        public double P
        {
            get { return p; }
            set { p = value; }
        }
        private double q;

        public double Q
        {
            get { return q; }
            set { q = value; }
        }
        //private double p1;

        //public double P1
        //{
        //    get { return p1; }
        //    set { p1 = value; }
        //}
        private double pf;

        public double PF
        {
            get { return pf; }
            set { pf = value; }
        }

        private double i2;

        public double I2
        {
            get { return i2; }
            set { i2 = value; }
        }

        private double pf2;

        public double PF2
        {
            get { return pf2; }
            set { pf2 = value; }
        }
        private double r;

        public double R
        {
            get { return r; }
            set { r = value; }
        }
        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        //private int dLoadFactorCode;

        //public int DLoadFactorCode
        //{
        //    get { return dLoadFactorCode; }
        //    set { dLoadFactorCode = value; }
        //}
        //private int count;

        //public int Count
        //{
        //    get { return count; }
        //    set { count = value; }
        //}

        private string modeNumber;

        public string ModeNumber
        {
            get { return modeNumber; }
            set { modeNumber = value; }
        }
        private int typeCode;

        public int TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
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
           
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_LoadFactor_Insert", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iP", P));
        //    command.Parameters.Add(new SqlParameter("iQ", Q));
        //    //command.Parameters.Add(new SqlParameter("iP1", P1));
        //    command.Parameters.Add(new SqlParameter("iPF", PF));
        //    command.Parameters.Add(new SqlParameter("iI2", I2));
        //    command.Parameters.Add(new SqlParameter("iPF2", PF2));
        //    command.Parameters.Add(new SqlParameter("iR", R));
        //    command.Parameters.Add(new SqlParameter("iX", X));
        //    command.Parameters.Add(new SqlParameter("iDesignCode",DesignCode));
        //    //command.Parameters.Add(new SqlParameter("iDloadFactorCode", DLoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iModeNumber", ModeNumber));
        //    command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
        //    //command.Parameters.Add(new SqlParameter("iCount", Count));
        //    command.Parameters.Add(new SqlParameter("iName", Name));

        //    SqlParameter InsertedCode = new SqlParameter();
        //    InsertedCode.SqlDbType = SqlDbType.Int;
        //    InsertedCode.Direction = ParameterDirection.Output;
        //    InsertedCode.ParameterName = "iCode";

        //    command.Parameters.Add(InsertedCode);

        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        Code = Convert.ToInt32(InsertedCode.Value.ToString());
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("CloadFActor.Insert Error" + ex.Message + "\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_LoadFactor_Update", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode",Code ));
        //    command.Parameters.Add(new SqlParameter("iP", P));
        //    command.Parameters.Add(new SqlParameter("iQ", Q));
        //    //command.Parameters.Add(new SqlParameter("iP1", P1));
        //    command.Parameters.Add(new SqlParameter("iPF", PF));
        //    command.Parameters.Add(new SqlParameter("iI2", I2));
        //    command.Parameters.Add(new SqlParameter("iPF2", PF2));
        //    command.Parameters.Add(new SqlParameter("iR", R));
        //    command.Parameters.Add(new SqlParameter("iX", X));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    //command.Parameters.Add(new SqlParameter("iDloadFactorCode", DLoadFactorCode));
        //    command.Parameters.Add(new SqlParameter("iModeNumber", ModeNumber));
        //    command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
        //    //command.Parameters.Add(new SqlParameter("iCount", Count));
        //    command.Parameters.Add(new SqlParameter("iName", Name));

        //    try
        //    {
        //        Connection.Open();
        //        command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("CloadFActor.Insert Error" + ex.Message + "\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_LoadFactor_Delete", connection);
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

        //public static CLoadFactor SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("C_LoadFactor_SelectByCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    CLoadFactor  cLoadFActor = new CLoadFactor();
        //    if (reader.Read())
        //    {
        //        cLoadFActor.Code = Convert.ToInt32(reader["Code"]);
        //        cLoadFActor.PF = Convert.ToDouble(reader["PF"]);
        //        cLoadFActor.PF2 = Convert.ToDouble(reader["PF2"]);
        //        //cLoadFActor.Cos3 = Convert.ToDouble(reader["Cos3"]);
        //        //cLoadFActor.Count = Convert.ToInt32(reader["Count"]);
        //        cLoadFActor.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        //cLoadFActor.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
        //        cLoadFActor.I2 = Convert.ToDouble(reader["I2"]);
        //        cLoadFActor.ModeNumber = reader["ModeNumber"].ToString();
        //        cLoadFActor.P = Convert.ToDouble(reader["P"]);

        //        cLoadFActor.R = Convert.ToDouble(reader["R"]);
        //        cLoadFActor.Q = Convert.ToDouble(reader["Q"]);
        //        cLoadFActor.X = Convert.ToDouble(reader["X"]);

        //        cLoadFActor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
        //        cLoadFActor.Name = reader["Name"].ToString();

        //    }
        //    else
        //    {
        //        cLoadFActor.Code = 0;
        //        cLoadFActor.PF = 0;
        //        cLoadFActor.PF2 = 0;
        //        //cLoadFActor.Cos3 = Convert.ToDouble(reader["Cos3"]);
        //        //cLoadFActor.Count = Convert.ToInt32(reader["Count"]);
        //        cLoadFActor.DesignCode = 0;
        //        //cLoadFActor.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
        //        cLoadFActor.I2 = 0;
        //        cLoadFActor.ModeNumber = string.Empty;
        //        cLoadFActor.P = 0;

        //        cLoadFActor.R = 0;
        //        cLoadFActor.Q = 0;
        //        cLoadFActor.X = 0;

        //        cLoadFActor.TypeCode = 0;
        //        cLoadFActor.Name = string.Empty;

            
        //    }

        //    reader.Close();

        //    connection.Close();
        //    return cLoadFActor;
        //}

        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("C_LoadFactor_SelectByCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
        //    DataSet dsLoad = new DataSet();
        //    adapter.Fill(dsLoad);
        //    return dsLoad.Tables[0];
        //}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~
        public bool AccessInsert()
        {
            //ed.WriteMessage("Access Inser LOAdFActor\n");
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_LoadFactor_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iP", P));
            command.Parameters.Add(new OleDbParameter("iQ", Q));
            command.Parameters.Add(new OleDbParameter("iPF", PF));
            command.Parameters.Add(new OleDbParameter("iI2", I2));
            command.Parameters.Add(new OleDbParameter("iPF2", PF2));
            command.Parameters.Add(new OleDbParameter("iR", R));
            command.Parameters.Add(new OleDbParameter("iX", X));
            //command.Parameters.Add(new  OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iModeNumber", ModeNumber));
            command.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));



           //ed.WriteMessage("iP={0}\n", P);
             //ed.WriteMessage("iQ={0}\n", Q);
            //ed.WriteMessage("iPF={0}\n", PF);
             //ed.WriteMessage("iI2={0}\n", I2);
             //ed.WriteMessage("iPF2={0}\n", PF2);
            //ed.WriteMessage("iR={0}\n", R);
            //ed.WriteMessage("iX={0}\n", X);
           // ed.WriteMessage("iDesignCode={0}\n", DesignCode);
             //ed.WriteMessage("iModeNumber={0}\n", ModeNumber);
             //ed.WriteMessage("iTypeCode={0}\n", TypeCode);
            //ed.WriteMessage("iName={0}\n", Name);

            //OleDbParameter InsertedCode = new OleDbParameter();
            //InsertedCode.OleDbType = OleDbType.Integer;
            //InsertedCode.Direction = ParameterDirection.Output;
            //InsertedCode.ParameterName = "iCode";

            //command.Parameters.Add(InsertedCode);

            try
            {
                Connection.Open();
                //ed.WriteMessage("***\n");
                command.ExecuteNonQuery();
                //ed.WriteMessage("Select @@ Identity\n");
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                Connection.Close();
               // Code = Convert.ToInt32(InsertedCode.Value.ToString());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("CloadFActor.Insert Error" + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_LoadFactor_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iP", P));
            command.Parameters.Add(new OleDbParameter("iQ", Q));
            //command.Parameters.Add(new SqlParameter("iP1", P1));
            command.Parameters.Add(new OleDbParameter("iPF", PF));
            command.Parameters.Add(new OleDbParameter("iI2", I2));
            command.Parameters.Add(new OleDbParameter("iPF2", PF2));
            command.Parameters.Add(new OleDbParameter("iR", R));
            command.Parameters.Add(new OleDbParameter("iX", X));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            //command.Parameters.Add(new SqlParameter("iDloadFactorCode", DLoadFactorCode));
            command.Parameters.Add(new OleDbParameter("iModeNumber", ModeNumber));
            command.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));
            //command.Parameters.Add(new SqlParameter("iCount", Count));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iCode", Code));


            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("CloadFActor.Insert Error" + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand  command = new OleDbCommand("C_LoadFactor_Delete", connection);
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

        public static CLoadFactor AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("C_LoadFactor_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            CLoadFactor cLoadFActor = new CLoadFactor();
            if (reader.Read())
            {
                cLoadFActor.Code = Convert.ToInt32(reader["Code"]);
                cLoadFActor.PF = Convert.ToDouble(reader["PF"]);
                cLoadFActor.PF2 = Convert.ToDouble(reader["PF2"]);
                //cLoadFActor.Cos3 = Convert.ToDouble(reader["Cos3"]);
                //cLoadFActor.Count = Convert.ToInt32(reader["Count"]);
                //cLoadFActor.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                //cLoadFActor.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
                cLoadFActor.I2 = Convert.ToDouble(reader["I2"]);
                cLoadFActor.ModeNumber = reader["ModeNumber"].ToString();
                cLoadFActor.P = Convert.ToDouble(reader["P"]);

                cLoadFActor.R = Convert.ToDouble(reader["R"]);
                cLoadFActor.Q = Convert.ToDouble(reader["Q"]);
                cLoadFActor.X = Convert.ToDouble(reader["X"]);

                cLoadFActor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
                cLoadFActor.Name = reader["Name"].ToString();

            }
            else
            {
                cLoadFActor.Code = 0;
                cLoadFActor.PF = 0;
                cLoadFActor.PF2 = 0;
                //cLoadFActor.Cos3 = Convert.ToDouble(reader["Cos3"]);
                //cLoadFActor.Count = Convert.ToInt32(reader["Count"]);
                cLoadFActor.DesignCode = 0;
                //cLoadFActor.DLoadFactorCode = Convert.ToInt32(reader["DLoadFactorCode"]);
                cLoadFActor.I2 = 0;
                cLoadFActor.ModeNumber = string.Empty;
                cLoadFActor.P = 0;

                cLoadFActor.R = 0;
                cLoadFActor.Q = 0;
                cLoadFActor.X = 0;

                cLoadFActor.TypeCode = 0;
                cLoadFActor.Name = string.Empty;


            }

            reader.Close();

            connection.Close();
            return cLoadFActor;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("C_LoadFactor_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsLoad = new DataSet();
            adapter.Fill(dsLoad);
            return dsLoad.Tables[0];
        }

       
    }
}
