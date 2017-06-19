using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Equipment
{
    public class EJackPanelWeekCell
    {
        private int num;
        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private int jackPanelWeekCode;
        public int JackPanelWeekCode
        {
            get { return jackPanelWeekCode; }
            set { jackPanelWeekCode = value; }
        }

        private Guid jackPanelWeekXCode;
        public Guid JackPanelWeekXCode
        {
            get { return jackPanelWeekXCode; }
            set { jackPanelWeekXCode = value; }
        }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Boolean isNightLight;
        public Boolean IsNightLight
        {
            get { return isNightLight; }
            set { isNightLight = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private bool isdefault;
        public bool IsDefault
        {
            get { return isdefault; }
            set { isdefault = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~//


        public bool Insert(SqlTransaction sqlTransaction, SqlConnection connection)
        {

            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPAnelWeekCode", JackPanelWeekCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                command.Transaction = sqlTransaction;
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.Insert: {0} \n", ex1.Message));

                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_Insert", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPAnelWeekCode", JackPanelWeekCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Cell);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in Cell");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Cell, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Cell failed");
                    }
                }





                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.ServerInsert: {0} \n", ex1.Message));

                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_Update", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPanelWeekCode", JackPanelWeekCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();


                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Cell))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Cell, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in Cell");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation WeekCell Failed");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    ed.WriteMessage("xcode={0} : {1} : code={2}", XCode, (int)Atend.Control.Enum.ProductType.Cell, Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Cell, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Cell failed");
                    }
                }





                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.ServerUpdate: {0} \n", ex1.Message));

                return false;
            }
        }

        public static bool Delete(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int jackPanelWeekCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_JackPanelWeekCell_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("iJackPanelWeekCode", jackPanelWeekCode));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" E_JackPanelWeekCell.Delete : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        //ASHKTORAB
        //public static bool ServerDelete(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int jackPanelWeekCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection _connection = sqlConnection;
        //    SqlCommand _command = new SqlCommand("E_JackPanelWeekCell_Delete", _connection);
        //    _command.CommandType = CommandType.StoredProcedure;
        //    _command.Transaction = sqlTransaction;

        //    _command.Parameters.Add(new SqlParameter("iJackPanelWeekCode", jackPanelWeekCode));
        //    DataTable jpwcTbl = EJackPanelWeekCell.SelectByJackPanelWeekCode(sqlTransaction, sqlConnection, jackPanelWeekCode);

        //    try
        //    {
        //        _command.ExecuteNonQuery();
        //        bool canCommitTransaction = true;
        //        foreach (DataRow jpwcRow in jpwcTbl.Rows)
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(sqlTransaction, sqlConnection, Convert.ToInt32(jpwcRow["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));

        //            //if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)))
        //            //{
        //            //    canCommitTransaction = true;

        //            //}
        //            //else
        //            //{
        //            //    canCommitTransaction = false;
        //            //}

        //            if ((EContainerPackage.Delete(sqlTransaction, sqlConnection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Cell))))
        //            {
        //                canCommitTransaction = true;
        //            }
        //            else
        //            {
        //                canCommitTransaction = false;
        //            }

        //            if (EProductPackage.Delete(sqlTransaction, sqlConnection, containerPackage.Code) & canCommitTransaction)
        //            {
        //                //transaction.Commit();
        //                //connection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                //transaction.Rollback();
        //                //connection.Close();
        //                return false;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" E_JackPanelWeekCell.ServerDelete : {0}", ex1.Message));
        //        return false;
        //    }
        //    return true;
        //}

        //MOUSAVI
        public static DataTable SelectByJackPanelWeekCode(int JPWCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekCode", JPWCode));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];

        }

        //ASHKTORAB
        //public static DataTable ServerSelectByJackPanelWeekCode(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int JPWCode)
        //{
        //    SqlConnection connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Transaction = sqlTransaction;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekCode", JPWCode));
        //    DataSet dsG = new DataSet();
        //    adapter.Fill(dsG);
        //    return dsG.Tables[0];

        //}

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];
        }

        //ASHKTORAB
        public static DataTable ServerSelectByJackPanelWeekXCode(SqlTransaction sqlTransaction, SqlConnection connection, Guid JPWXCode)
        {
            //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", JPWXCode));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];

        }
        //Hatami/GetFromServer
        public static DataTable ServerSelectByJackPanelWeekCode(SqlTransaction sqlTransaction, SqlConnection _connection,int  JPWCode)
        {
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = sqlTransaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekCode", JPWCode));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];

        }

        //ASHKTORAB
        public static EJackPanelWeekCell ServerSelectByXCode(SqlTransaction sqlTransaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Transaction = sqlTransaction;

            SqlDataReader reader = command.ExecuteReader();

            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                jpwc.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();

            return jpwc;

        }

        //MEDHAT //ShareOnServer
        public static EJackPanelWeekCell ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();

            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekCode = Convert.ToInt32(reader["JackPanelWeekCode"].ToString());
                //jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                jpwc.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                jpwc.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : jack panel cell\n");
            }
            reader.Close();
            return jpwc;
        }

        //ASHKTORAB
        public static EJackPanelWeekCell SelectByCodeForServer(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            connection.Open();
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            //command.Transaction = sqlTransaction;

            SqlDataReader reader = command.ExecuteReader();

            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                //jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekCode = Convert.ToInt32(reader["JackPanelWeekCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                ed.WriteMessage("\nhhhhh\n");
                jpwc.XCode = new Guid(reader["XCode"].ToString());
                ed.WriteMessage("\nbbbbb\n");

            }

            reader.Close();
            connection.Close();
            return jpwc;

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX(SqlTransaction sqlTransaction, SqlConnection connection)
        {

            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPAnelWeekXCode", JackPanelWeekXCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));



            try
            {

                command.Transaction = sqlTransaction;
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.InsertX: {0} \n", ex1.Message));

                return false;
            }
        }

        //ASHKTORAB
        public bool InsertXX(SqlTransaction sqlTransaction, SqlConnection connection)
        {

            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPAnelWeekXCode", JackPanelWeekXCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));



            try
            {

                command.Transaction = sqlTransaction;
                Code = Convert.ToInt32(command.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.InsertX: {0} \n", ex1.Message));

                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_InsertX", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPAnelWeekXCode", JackPanelWeekXCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault",IsDefault));

            try
            {

                command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Cell, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in Cell");
                    }
                }
                if (BringSubEquips)
                {
                    //ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Cell, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Cell failed");
                    }
                }
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.LocalInsertX: {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_UpdateX", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNum", Num));
            command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", JackPanelWeekXCode));
            command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();


                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Cell))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Cell, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection , ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in Cell");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation WeekCell Failed");
                    }
                }
                ed.WriteMessage("EJackpanelWeekCell.Operation passed \n");


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Cell, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Cell failed");
                    }
                }





                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.ServerUpdate: {0} \n", ex1.Message));

                return false;
            }
        }


        //MEDHAT
        public bool UpdateX(SqlConnection _Connection, SqlTransaction _Transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _Connection;
            SqlCommand Command = new SqlCommand("E_JackPanelWeekCell_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", JackPanelWeekXCode));
            Command.Parameters.Add(new SqlParameter("iNum", Num));
            Command.Parameters.Add(new SqlParameter("iIsNightLight", IsNightLight));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Command.Transaction = _Transaction;
                Command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR IN Updatex(transaction) {0}\n", ex.Message);
                return false;
            }
        }

        //SendFromLocalToAccess
        public static DataTable SelectByJackPanelWeekXCode(Guid JPWXCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", JPWXCode));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            ed.WriteMessage("Cell Found : {0} \n", dsG.Tables[0].Rows.Count);
            return dsG.Tables[0];

        }

        //ASHKTORAB //ShareOnServer
        public static DataTable SelectByJackPanelWeekXCode(SqlTransaction sqlTransaction, SqlConnection sqlConnection, Guid JPWXCode)
        {
            SqlConnection connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectByJackPanelWeekXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = sqlTransaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", JPWXCode));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];

        }

        public static bool DeleteX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, Guid jackPanelWeekXCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_JackPanelWeekCell_DeleteX", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", jackPanelWeekXCode));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" E_JackPanelWeekCell.DeleteX : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeekCell_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];
        }

        //SendFromLocalToAccess
        public static EJackPanelWeekCell SelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                jpwc.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("Cell Only aws found \n");
            }
            else
            {
                jpwc.Code = -1;
            }
            reader.Close();
            connection.Close();
            return jpwc;

        }

        //ASHKTORAB //ShareOnServer
        public static EJackPanelWeekCell SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                    jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                    jpwc.JackPanelWeekCode = 0;
                    jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                    jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                    jpwc.XCode = new Guid(reader["XCode"].ToString());
                    //ed.WriteMessage("Cell Only aws found \n");
                }
                else
                {
                    jpwc.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EJackPanelWeekCell.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return jpwc;

        }


        public static EJackPanelWeekCell SelectByCodeForLocal(int Code, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                    jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                    jpwc.JackPanelWeekCode = 0;
                    jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                    jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                    jpwc.XCode = new Guid(reader["XCode"].ToString());
                    //ed.WriteMessage("Cell Only aws found \n");
                }
                else
                {
                    jpwc.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EJackPanelWeekCell.In SelectByCode4Design.TransAction:{0}\n", ex.Message);
            }

            return jpwc;

        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~//

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeekCell_Insert", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iNum", Num));
            command.Parameters.Add(new OleDbParameter("iJackPAnelWeekCode", JackPanelWeekCode));
            command.Parameters.Add(new OleDbParameter("iIsNightLight", IsNightLight));

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Cell);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in Cell");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Cell, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Cell failed");
                    }
                }





                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.AccessInsert: {0} \n", ex1.Message));

                return false;
            }
        }



        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _Newconnection,bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeekCell_Insert", _Newconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iNum", Num));
            command.Parameters.Add(new OleDbParameter("iJackPAnelWeekCode", JackPanelWeekCode));
            command.Parameters.Add(new OleDbParameter("iIsNightLight", IsNightLight));
            int oldCode = Code;
            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Cell);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction,_Newconnection))
                            throw new System.Exception("operation failed in Cell");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Cell: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.Cell, Code, _oldtransaction, _Oldconnection,_NewTransaction,_Newconnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Cell failed");
                    }
                }





                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR JackPanelWeekCell.AccessInsert: {0} \n", ex1.Message));

                return false;
            }
        }
        public static EJackPanelWeekCell AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));


            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                jpwc.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();
            connection.Close();
            return jpwc;

        }

        //MOUSAVI
        public static EJackPanelWeekCell AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeekCell_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));

            command.Transaction = _transaction;
            OleDbDataReader reader = command.ExecuteReader();

            EJackPanelWeekCell jpwc = new EJackPanelWeekCell();
            if (reader.Read())
            {
                jpwc.Code = Convert.ToInt32(reader["Code"].ToString());
                jpwc.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                jpwc.IsNightLight = Convert.ToBoolean(reader["IsNightLight"].ToString());
                jpwc.JackPanelWeekCode = 0;
                jpwc.JackPanelWeekXCode = new Guid(reader["JackPanelWeekXCode"].ToString());
                jpwc.Num = Convert.ToInt32(reader["Num"].ToString());
                jpwc.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                jpwc.Code = -1;
            }
            reader.Close();
            return jpwc;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeekCell_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsG = new DataSet();
            adapter.Fill(dsG);
            return dsG.Tables[0];
        }

        public static DataTable SelectAllAndMerge()
        {

            DataTable AccTbl = AccessSelectAll();
            DataTable SqlTbl = SelectAllX();

            DataTable MergeTbl = SqlTbl.Copy();
            DataColumn IsSql = new DataColumn("IsSql", typeof(bool));
            IsSql.DefaultValue = true;
            MergeTbl.Columns.Add(IsSql);

            foreach (DataRow Dr in AccTbl.Rows)
            {
                DataRow MergeRow = MergeTbl.NewRow();

                foreach (DataColumn Dc in AccTbl.Columns)
                {
                    MergeRow[Dc.ColumnName] = Dr[Dc.ColumnName];
                }

                MergeRow["IsSql"] = false;
                MergeRow["XCode"] = new Guid("00000000-0000-0000-0000-000000000000");
                MergeTbl.Rows.Add(MergeRow);
            }


            return MergeTbl;

        }

        //MOUSAVI
        public static DataTable AccessSelectByJackPanelWeekCode(int jackPanelCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeekCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelWeekCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }


        public static DataTable AccessSelectByJackPanelWeekCode(int jackPanelCode , OleDbTransaction _transaction , OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeekCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelWeekCode", jackPanelCode));
            adapter.SelectCommand.Transaction = _transaction;
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }


        //status report
        public static DataTable AccessSelectByJackPanelWeekCode(int jackPanelCode, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeekCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelWeekCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }


    }
}
