using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DRemark
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private byte[] file;
        public byte[] File
        {
            get { return file; }
            set { file = value; }
        }

        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_Remark_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iFile", File));
            
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DRemark.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_Remark_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iFile", File));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR DRemark.Update: {0} \n", ex1.Message));


                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            //SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_Remark_Delete", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));


            //transaction = Connection.BeginTransaction();
            //command.Transaction = transaction;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;


            }


            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
            //    transaction.Rollback();
            //    Connection.Close();
            //    return false;

            //}



            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR D_Remark.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }


        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Remark_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];

        }

        public static DRemark SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("D_Remark_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DRemark Rem = new DRemark();
            if (reader.Read())
            {
                Rem.Code = Convert.ToInt16(reader["Code"].ToString());

                Rem.File = (byte[])(reader["File"]);
                Rem.Name = reader["Name"].ToString();
            }
            reader.Close();



            Connection.Close();
            return Rem;
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_remark_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsRemark = new DataSet();
            adapter.Fill(dsRemark);
            return dsRemark.Tables[0];

        }

        //--------------------- Access Part ---------------------------

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Remark_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iFile", File));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DRemark.AccessInsert : {0} ", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Remark_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iFile", File));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR DRemark.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        public static bool AccessDelete(int _Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Remark_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", _Code));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(String.Format("ERROR DRemark.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //frmWordRemark
        public static DRemark AccessSelectByCode(int _Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Remark_SelectByCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", _Code));

            DRemark remark = new DRemark();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    remark.Code = Convert.ToInt32(reader["Code"].ToString());

                    try
                    {
                        remark.File = (byte[])(reader["File"]);
                    }
                    catch
                    {
                        remark.File = new byte[0];
                    }
                }
                else
                {
                    remark.Code = -1;
                }
                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR DRemark.SelectByCode {0}\n", ex1.Message));
                connection.Close();
            }
            return remark;
        }

    }
}
