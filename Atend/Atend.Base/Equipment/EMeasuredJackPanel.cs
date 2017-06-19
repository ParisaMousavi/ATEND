using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EMeasuredJackPanel
    {


        Guid xCode;
        public Guid XCode
        {
            get { return xCode; }
            set { xCode = value; }
        }

        int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCount", Count));

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //.WriteMessage("Error For Insert \n");
                    }
                    Counter++;
                }

                if (canCommitTransaction)
                {
                    //transaction.Commit();
                    //Connection.Close();
                    return true;
                }
                else
                {
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }

                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_MeasuredJackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCount", Count));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("~~~~~~~~~~~~ inserted~~~~~~~~~~~~~~\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in MeasureJackPAnel");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_MeasuredJackPanel_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCount", Count));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("~~~~~~~~~~~~ inserted~~~~~~~~~~~~~~\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in MeasureJackPAnel");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in MeasureJackPanel");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCount", Count));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel)))
                {
                    canCommitTransaction = true;

                }
                else
                {
                    canCommitTransaction = false;
                }
                //connection.Close();
                if (canCommitTransaction)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EMeasuredJackPanel SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            reader.Close();
            connection.Close();
            return MeasuredJackPanel;
        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EPhotoCell photoCell = new EPhotoCell();
            if (reader.Read())
            {
                //photoCell.Code = Convert.ToInt16(reader["Code"].ToString());
                //photoCell.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Count = Convert.ToInt32(reader["Count"].ToString());
            }
            reader.Close();
            connection.Close();
            //return photoCell;
        }

        public static EMeasuredJackPanel SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                    MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    MeasuredJackPanel.Comment = reader["Comment"].ToString();
                    MeasuredJackPanel.Name = reader["Name"].ToString();
                    MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return MeasuredJackPanel;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_MeasuredJackPanel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_MeasuredJackPanel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
        }

        public static EMeasuredJackPanel ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt32(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            reader.Close();
            //connection.Close();
            return MeasuredJackPanel;
        }

        //MEDHAT //ShareOnServer
        public static EMeasuredJackPanel ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();

            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt32(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : measured jack panel\n");
            }
            reader.Close();
            return MeasuredJackPanel;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.writeMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel);
                        if (containerPackage.InsertX(transaction, connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    Counter = 0;
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        Counter++;
                    }
                    //****************

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Insert 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }


        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel);
                    //_EOperation.ProductID = 
                    if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //ed.writeMessage("Error For Insert \n");
                    }
                    Counter++;
                }



                if (canCommitTransaction)
                {
                    //transaction.Commit();
                    //connection.Close();
                    return true;
                }
                else
                {
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_MeasuredJackPanel_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCount", Count));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in MeasureJackPAnel");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_MeasuredJackPanel_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCount", Count));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("~~~~~~~~~~~~ inserted~~~~~~~~~~~~~~\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode ;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in MeasureJackPAnel");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in MeasureJackPanel");
                    }
                }
                ed.WriteMessage("EMeasuredJackpanel.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, XCode, ServerTransaction , ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel);

                            if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                            {
                                canCommitTransaction = true;
                            }
                            else
                            {
                                canCommitTransaction = false;
                            }
                            Counter++;
                        }
                    }


                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel));
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(_transaction, connection) && canCommitTransaction)
                            {
                                canCommitTransaction = true;
                            }
                            else
                            {
                                canCommitTransaction = false;

                            }
                            Counter++;
                        }
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //*************
                    if (canCommitTransaction)
                    {
                        _transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        _transaction.Rollback();
                        connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }


                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EMeasuredJackPanel.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }


        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCount", Count));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.UpdateX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canCommitTransaction)
                    {
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMeasuredJackPanel.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel)))
                    {
                        //transaction.Commit();
                        //connection.Close();
                        return true;

                    }
                    else
                    {
                        //transaction.Rollback();
                        //connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EMeasuredJackPanel.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMeasuredJackPanel.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        //SentFromLocalToAccess
        public static EMeasuredJackPanel SelectByXCode(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", MeasuredJackPanel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MeasuredJackPanel));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
            }

            reader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", MeasuredJackPanel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MeasuredJackPanel));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //nodeKeys.Add(reader["ProductID"].ToString());
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeys.Add(Op);
            }

            reader.Close();
            connection.Close();
            return MeasuredJackPanel;
        }

        //ASHKTORAB //ShareOnServer
        public static EMeasuredJackPanel SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                    MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                    MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    MeasuredJackPanel.Comment = reader["Comment"].ToString();
                    MeasuredJackPanel.Name = reader["Name"].ToString();
                    MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
                }
                else
                {
                    MeasuredJackPanel.Code = -1;
                }
                reader.Close();


            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EPhuseKey.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return MeasuredJackPanel;
        }

        public static EMeasuredJackPanel SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", MeasuredJackPanel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MeasuredJackPanel));
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
            }

            reader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", MeasuredJackPanel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MeasuredJackPanel));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            //connection.Close();
            return MeasuredJackPanel;
        }

        //ASHKTORAB
        public static EMeasuredJackPanel SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return MeasuredJackPanel;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_MeasuredJackPanel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_MeasuredJackPanel_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_MeasuredJackPanel_SearchByName", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iName", Name));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                connection.Close();
                return true;
            }
            else
            {
                reader.Close();
                connection.Close();
                return false;
            }

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_MeasuredJackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("~~~~~~~~~~~~ inserted~~~~~~~~~~~~~~\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_MeasuredJackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            int oldCode = Code;
            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("~~~~~~~~~~~~ inserted~~~~~~~~~~~~~~\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction , _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess MeasuredJackPanel failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error MeasuredJackPanel.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EMeasuredJackPanel AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
            }
            reader.Close();
            connection.Close();
            return MeasuredJackPanel;
        }

        public static EMeasuredJackPanel AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
            }
            reader.Close();
            return MeasuredJackPanel;
        }

        public static EMeasuredJackPanel AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            else
            {
                MeasuredJackPanel.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return MeasuredJackPanel;
        }

        public static EMeasuredJackPanel AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            if (reader.Read())
            {
                MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                MeasuredJackPanel.Comment = reader["Comment"].ToString();
                MeasuredJackPanel.Name = reader["Name"].ToString();
                MeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
                MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
            }
            reader.Close();
            connection.Close();
            return MeasuredJackPanel;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EMeasuredJackPanel AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EMeasuredJackPanel _EMeasuredJackPanel = new EMeasuredJackPanel();
        //    if (reader.Read())
        //    {
        //        _EMeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
        //        _EMeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        _EMeasuredJackPanel.Comment = reader["Comment"].ToString();
        //        _EMeasuredJackPanel.Name = reader["Name"].ToString();
        //        _EMeasuredJackPanel.XCode = new Guid(reader["XCode"].ToString());
        //        _EMeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
        //    }
        //    else
        //    {
        //        _EMeasuredJackPanel.Code = -1;
        //    }
        //    reader.Close();
        //    return _EMeasuredJackPanel;
        //}

        public static EMeasuredJackPanel AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_MeasuredJackPanel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EMeasuredJackPanel MeasuredJackPanel = new EMeasuredJackPanel();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    MeasuredJackPanel.Code = Convert.ToInt16(reader["Code"].ToString());
                    MeasuredJackPanel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    MeasuredJackPanel.Comment = reader["Comment"].ToString();
                    MeasuredJackPanel.Name = reader["Name"].ToString();
                    MeasuredJackPanel.Count = Convert.ToInt32(reader["Count"].ToString());
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMeasuredJackPanel.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return MeasuredJackPanel;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_MeasuredJackPanel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_MeasuredJackPanel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsPhotoCell = new DataSet();
            adapter.Fill(dsPhotoCell);
            return dsPhotoCell.Tables[0];
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

        //public static bool ShareOnServer(Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction;

        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    int DeletedCode = 0;


        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();

        //        EMeasuredJackPanel _EPhotoCell = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel));
        //            ed.WriteMessage("\n111\n");

        //            EMeasuredJackPanel Ap = Atend.Base.Equipment.EMeasuredJackPanel.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EMeasuredJackPanel.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EPhotoCell.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPhotoCell.XCode);
        //            _EPhotoCell.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPhotoCell.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPhotoCell.Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel))
        //                {
        //                    if (!_EPhotoCell.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n112\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();

        //                    }
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPhotoCell.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                Servertransaction.Commit();
        //                Serverconnection.Close();

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }



        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EMeasuredJackPanel.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool GetFromServer(int Code)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    Guid DeletedXCode = Guid.NewGuid();

        //    try
        //    {

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        try
        //        {

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel));

        //            EMeasuredJackPanel Ap = Atend.Base.Equipment.EMeasuredJackPanel.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EMeasuredJackPanel.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EMeasuredJackPanel.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MeasuredJackPanel));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.MeasuredJackPanel, Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EMeasuredJackPanel.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
