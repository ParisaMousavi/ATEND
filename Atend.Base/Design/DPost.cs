using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DPost
    {
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        private Guid code;

        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int productCode;

        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }



        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlTransaction Transaction;
        //    SqlCommand command = new SqlCommand("D_Post_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;


        //    Code = Guid.NewGuid();
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

        //    try
        //    {
        //        connection.Open();
        //        Transaction = connection.BeginTransaction();
        //        command.Transaction = Transaction;

        //        try
        //        {
        //            //Code = new Guid(command.ExecuteScalar().ToString());
        //            command.ExecuteNonQuery();
        //            DPackage package = new DPackage();
        //            package.NodeCode = Code;
        //            package.Count = 1;
        //            package.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
        //            package.ProductCode = ProductCode;

        //            if (!package.AccessInsert(Transaction, connection))
        //            {
        //                //ed.writeMessage("ËÈÊ ÓÊ ÇãÇä ÐíÑ äãíÈÇÔÏ\n");
        //                return false;
        //            }

        //            Transaction.Commit();
        //            return true;
        //        }
        //        catch(System.Exception ex)
        //        {
        //            ed.WriteMessage(string.Format("Error Occured During Post Insertion 01 : {0} \n", ex.Message));
        //            Transaction.Rollback();
        //            connection.Close();
        //            return false;

        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DPost.Insert : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }


        //}


        //public bool Insert(SqlTransaction _transaction , SqlConnection _connection)
        //{
        //    SqlConnection connection = _connection;

        //    SqlCommand command = new SqlCommand("D_Post_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Transaction = _transaction;


        //    Code = Guid.NewGuid();
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

        //    try
        //    {
        //        try
        //        {
        //            //Code = new Guid(command.ExecuteScalar().ToString());
        //            command.ExecuteNonQuery();

        //            //DPackage package = new DPackage();
        //            //package.NodeCode = Code;
        //            //package.Count = 1;
        //            //package.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
        //            //package.ProductCode = ProductCode;

        //            //SqlTransaction Tr = _transaction;

        //            //if (!package.Insert(Tr, connection))
        //            //{
        //            //    ed.WriteMessage("ËÈÊ ÓÊ ÇãÇä ÐíÑ äãíÈÇÔÏ\n");
        //            //    return false;
        //            //}

        //            return true;
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage(string.Format("Error Occured During Post Insertion 01 : {0} \n", ex.Message));
        //            //connection.Close();
        //            return false;

        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DPost.Insert : {0} \n", ex1.Message));
        //        //connection.Close();
        //        return false;
        //    }


        //}


        /*public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_Post_Update", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNumber", Number));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        public static bool Delete(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_Post_Delete", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        public static DPost SelectByCode(Guid Code, int DesignCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("D_Post_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DPost post = new DPost();
            if (reader.Read())
            {
                post.Code = new Guid(reader["Code"].ToString());
                post.Number = reader["Number"].ToString();
                post.ProductCode = Convert.ToInt32(reader["ProductCode"]);

            }
            Connection.Close();
            reader.Close();
            return post;
        }
        */
        //~~~~~~~~~~~AccessPart~~~~~~~~~~~~~~~~~
        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbTransaction Transaction;
            OleDbCommand command = new OleDbCommand("D_Post_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            try
            {
                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    //Code = new Guid(command.ExecuteScalar().ToString());
                    command.ExecuteNonQuery();
                    DPackage package = new DPackage();
                    package.NodeCode = Code;
                    package.Count = 1;
                    package.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundPost);
                    package.ProductCode = ProductCode;

                    if (!package.AccessInsert(Transaction, connection))
                    {
                        //ed.writeMessage("ËÈÊ ÓÊ ÇãÇä ÐíÑ äãíÈÇÔÏ\n");
                        return false;
                    }

                    Transaction.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error Occured During Post Insertion 01 : {0} \n", ex.Message));
                    Transaction.Rollback();
                    connection.Close();
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }


        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Post_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));


            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error DPost.AccessInsert : {0} \n", ex.Message));
                return false;

            }



        }


        public bool AccessUpdate()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Post_Update", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        public static bool AccessDelete(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Post_Delete", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //MOUSAVI->drawing delete
        public static bool AccessDelete(Guid Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection; ;
            OleDbCommand command = new OleDbCommand("D_Post_Delete", connection);
            command.Transaction = _transaction;

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPost.Delete : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }


        public static DPost AccessSelectByCode(Guid Code, int DesignCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_Post_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            sqlCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPost post = new DPost();
            if (reader.Read())
            {
                post.Code = new Guid(reader["Code"].ToString());
                post.Number = reader["Number"].ToString();
                post.ProductCode = Convert.ToInt32(reader["ProductCode"]);

            }
            Connection.Close();
            reader.Close();
            return post;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Post_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsNode = new DataSet();
            adapter.Fill(dsNode);

            return dsNode.Tables[0];
        }

        //StatusReport
        public static DataTable AccessSelectAll(OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Post_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsNode = new DataSet();
            adapter.Fill(dsNode);

            return dsNode.Tables[0];
        }


    }
}
