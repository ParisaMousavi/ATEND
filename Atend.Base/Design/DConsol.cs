using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DConsol
    {

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        public DConsol()
        {
        }

        private Guid code;

        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private int loadCode;

        public int LoadCode
        {
            get { return loadCode; }
            set { loadCode = value; }
        }

        private Guid parentCode;

        public Guid ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        private int productCode;

        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }



        //MOUSAVI->drawing
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.writeMessage("Enter To Save dConsol:{0}\n", "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPathLocal);
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Consol_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("Code:{0}\nParentCode:{1}\nProductCode:{2}\nLoadCoad:{3}\n", Code, ParentCode, ProductCode, LoadCode);
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                //ed.writeMessage(string.Format("DConsol.Insert Done. \n"));
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DConsol.AccessInsert : {0} ", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool AccessUpdate_LoadCode()
        {
            ed.WriteMessage("i am in Dconsol.Update_LoadCode \n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Consol_UpdateLoadCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                ed.WriteMessage("i done Dconsol.update_loadcode success fully \n");

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR Dconsol.Update_LoadCode {0}\n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;


        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction transaction, OleDbConnection connection)
        {
            ed.WriteMessage("Enter To Save dConsol\n");

            OleDbCommand command = new OleDbCommand("D_Consol_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //command.Parameters.Add(new SqlParameter("iAutoCadCode",AutoCadCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                ed.WriteMessage(string.Format("DConsol.Insert Done. \n"));
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DConsol.Insert : {0} ", ex1.Message));
                //connection.Close();
                return false;
            }


        }

        public static bool AccessDelete(Guid _Code)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Consol_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", _Code));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(String.Format("ERROR DConsol.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            return true;
        }

        //MOUSAVI->drawing delete
        public static bool AccessDelete(Guid _Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Consol_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", _Code));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(String.Format("ERROR DConsol.Delete : {0} \n", ex1.Message));
                return false;
            }

            return true;
        }



        public static bool AccessDeleteByParentCode(Guid _ParentCode, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {

            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("D_Consol_DeleteByParentCodeAndDesignCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _Transaction;

            command.Parameters.Add(new OleDbParameter("iParentCode", _ParentCode));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
            }
            catch (Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(String.Format("ERROR DConsol.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            return true;
        }

        public static bool AccessDeleteByParentCode(Guid _ParentCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Consol_DeleteByParentCodeAndDesignCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iParentCode", _ParentCode));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(String.Format("ERROR DConsol.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            return true;
        }


        //Hatami
        public static DConsol AccessSelectByCode(Guid _Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Consol_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", _Code));
            //command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

            DConsol dconsol = new DConsol();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dconsol.Code = new Guid(reader["Code"].ToString());
                    dconsol.ParentCode = new Guid(reader["ParentCode"].ToString());
                    dconsol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    dconsol.LoadCode = Convert.ToInt32(reader["LoadCode"]);


                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DConsol.SelectByCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dconsol;
        }
        //Hatami
        public static DataTable AccessSelectByParentCode(Guid _ParentCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Consol_SelectByParentCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", _ParentCode));

            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        //Hatami
        public static DataTable AccessSelectByType()//Type=انتهایی و عبوری
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Consol_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        public static DataTable AccessSelectByType(OleDbConnection aConnection)//Type=انتهایی و عبوری
        {

            OleDbConnection connection = aConnection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Consol_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", _DesignCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        public static DataTable AccessSelectAll()
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Consol_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }


        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {

            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Consol_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        //****************************Access To Memory For Calc
        //Hatami
        public static DConsol AccessSelectByCode(DataTable dtDConsol, Guid _Code)
        {
            DataRow[] dr = dtDConsol.Select(string.Format("Code='{0}'", _Code.ToString()));
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DConsol dconsol = new DConsol();

            if (dr.Length != 0)
            {
                dconsol.Code = new Guid(dr[0]["Code"].ToString());
                dconsol.ParentCode = new Guid(dr[0]["ParentCode"].ToString());
                dconsol.ProductCode = Convert.ToInt32(dr[0]["ProductCode"].ToString());
                dconsol.LoadCode = Convert.ToInt32(dr[0]["LoadCode"]);

            }
            
            return dconsol;
        }



    }
}
