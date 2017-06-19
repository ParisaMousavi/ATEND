using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DDesignFile
    {
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int designId;
        public int DesignId
        {
            get { return designId; }
            set { designId = value; }
        }

        private byte[] file;
        public byte[] File
        {
            get { return file; }
            set { file = value; }
        }

        private byte[] image;
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        private byte[] book;
        public byte[] Book
        {
            get { return book; }
            set { book = value; }
        }


        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Delete(DesignId);
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_DesignFile_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iFile", File));
            Command.Parameters.Add(new SqlParameter("iImage", Image));
            Command.Parameters.Add(new SqlParameter("iBook", Book));
            //Command.Parameters.Add(new SqlParameter("iFileSize", FileSize));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                ed.WriteMessage(string.Format("Error DDesignFile.Insert {0} \n ", ex.Message));
                Connection.Close();
                return false;
            }
        }

        //frmDesignSaveServer
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand Command = new SqlCommand("D_DesignFile_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;
            Command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            Command.Parameters.Add(new SqlParameter("iFile", File));
            Command.Parameters.Add(new SqlParameter("iImage", Image));
            Command.Parameters.Add(new SqlParameter("iBook", Book));

            //Command.Parameters.Add(new SqlParameter("iFileSize", FileSize));
            try
            {
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                ed.WriteMessage(string.Format("Error DDesignFile.Insert {0} \n ", ex.Message));
                return false;
            }
        }


        public bool Update()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_DesignFile_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            command.Parameters.Add(new SqlParameter("iFile", File));
            command.Parameters.Add(new SqlParameter("iId", Id));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            command.Parameters.Add(new SqlParameter("iBook", Book));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                //ed.WriteMessage("update done \n");
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //frmDesignSaveServer
        public bool Update(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("D_DesignFile_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            command.Parameters.Add(new SqlParameter("iFile", File));
            command.Parameters.Add(new SqlParameter("iId", Id));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            command.Parameters.Add(new SqlParameter("iBook", Book));

            try
            {
                command.ExecuteNonQuery();
                //ed.WriteMessage("update done \n");
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.Update {0}\n", ex1.Message));
                return false;
            }
            return true;
        }

        public static bool Delete(int ID)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_DesignFile_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iId", ID));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.Delete {0}\n", ex1.Message));

                Connection.Close();
                return false;

            }
        }

        public static DDesignFile SelectByDesignId(int DesignId)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectByDesignCode\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_DesignFile_SelectByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            DDesignFile dDesignFile = new DDesignFile();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dDesignFile.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    dDesignFile.Id = Convert.ToInt32(reader["Id"]);

                    try
                    {
                        dDesignFile.File = (byte[])(reader["File"]);
                    }
                    catch
                    {

                        dDesignFile.File = new byte[0];
                    }


                    try
                    {
                        dDesignFile.Image = (byte[])(reader["Image"]);
                    }
                    catch
                    {

                        dDesignFile.Image = new byte[0];
                    }

                    try
                    {
                        dDesignFile.Book = (byte[])(reader["Book"]);
                    }
                    catch
                    {

                        dDesignFile.Book = new byte[0];
                    }
                }
                else
                {
                    dDesignFile.id = -1;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.SelectByDesignCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dDesignFile;
        }

        //frmDesignSearch //frmSaveDesignServer
        public static DDesignFile SelectByDesignId(SqlTransaction _transaction, SqlConnection _connection,  int DesignId)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectByDesignCode\n");
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("D_DesignFile_SelectByDesignId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iDesignId", DesignId));
            DDesignFile dDesignFile = new DDesignFile();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dDesignFile.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    try
                    {
                        dDesignFile.File = (byte[])(reader["File"]);
                    }
                    catch
                    {
                        dDesignFile.File = new byte[0];

                    }

                    dDesignFile.Id = Convert.ToInt32(reader["Id"]);
                    try
                    {
                        dDesignFile.Image = (byte[])(reader["Image"]);
                    }
                    catch
                    {
                        dDesignFile.Image = new byte[0];
                    }
                    
                    try
                    {
                        dDesignFile.Book = (byte[])(reader["Book"]);
                    }
                    catch
                    {
                        dDesignFile.Book = new byte[0];
                    }

                }
                else
                {
                    dDesignFile.Id = -1;
                }

                reader.Close();

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.SelectByDesignCode {0}\n", ex1.Message));
            }
            return dDesignFile;
        }

        public static DDesignFile SelectById(int ID)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_DesignFile_SelectById", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iId", ID));
            DDesignFile dDesignFile = new DDesignFile();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dDesignFile.DesignId = Convert.ToInt32(reader["DesignId"].ToString());
                    dDesignFile.File = (byte[])(reader["File"]);
                    dDesignFile.Id = Convert.ToInt32(reader["Id"]);
                    dDesignFile.Image = (byte[])(reader["Image"]);
                    dDesignFile.Book = (byte[])(reader["BooK"]);
                }
                else
                {
                    dDesignFile.id = -1;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DDesignFile.SelectByDesignCode {0}\n", ex1.Message));
                connection.Close();
            }
            return dDesignFile;
        }
    }
}
