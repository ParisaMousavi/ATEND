using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;


namespace Atend.Base.Equipment
{
    public class EBreaker
    {

        private int code;
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

        private double amper;
        public double Amper
        {
            get { return amper; }
            set { amper = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
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

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private int containerCode;
        public int ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; }
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

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();


        public static ArrayList nodeKeys = new ArrayList();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR EBreaker.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Breaker_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
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
        //        ed.WriteMessage(string.Format(" ERROR EBreaker.Insert : {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool Insert(string strConnectionString)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(strConnectionString);
            SqlCommand command = new SqlCommand("E_Breaker_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();





                    //Package
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                        if (containerPackage.Insert(transaction, Connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    //ed.writeMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
                    Counter = 0;
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        if (_EProductPackage.Insert(transaction, Connection) && canCommitTransaction)
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

                    //****************

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
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

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Breaker_Insert", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("Name:{0} \n", Name);
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Ampre:{0} \n", Amper);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Breaker, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in breaker");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Breaker, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Breaker_Update", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("Name:{0} \n", Name);
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Ampre:{0} \n", Amper);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Breaker))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Breaker, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in breaker");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Breaker");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Breaker, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
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
                ed.WriteMessage(string.Format(" ERROR EBreaker.Update: {0} \n", ex1.Message));


                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Delete", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));

                    if ((EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }

                }


                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    transaction.Rollback();
                    Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Breaker_Delete", connection);
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
        //        ed.WriteMessage(string.Format(" ERROR EBreaker.Delete {0}\n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }

        //}

        //ASHKTORAB

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_Delete", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();
                    //ed.WriteMessage("Code = {0} \n ", Code.ToString());

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
                    //ed.WriteMessage("ContainerCode = {0} \n ", containerPackage.ContainerCode.ToString());

                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if ((EContainerPackage.Delete(transaction, Connection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //ed.WriteMessage("ContainerCode = {0} \n ", containerPackage.Code.ToString());
                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        //transaction.Commit();
                        //Connection.Close();
                        //ed.WriteMessage("\nINNNNNNNNNNNNNNN\n");
                        return true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //Connection.Close();
                        return false;
                    }

                }


                catch (System.Exception ex1)
                {
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Delete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EBreaker SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iCode", breaker.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            //ed.writeMessage("insulator.code:" + breaker.code + "Type:" + Atend.Control.Enum.ProductType.Breaker + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.writeMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.writeMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************




            Connection.Close();
            return breaker;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                //breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                ///breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Amper = Convert.ToSingle(reader["Amper"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }
            reader.Close();

            Connection.Close();

        }

        //public static EBreaker SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Breaker_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    EBreaker breaker = new EBreaker();
        //    if (reader.Read())
        //    {
        //        breaker.Code = Convert.ToInt16(reader["Code"].ToString());
        //        breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
        //        breaker.Type = Convert.ToByte(reader["Type"].ToString());
        //        breaker.Comment = reader["Comment"].ToString();
        //        breaker.Name = reader["Name"].ToString();
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return breaker;

        //}

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Breaker_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_breaker_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];
        }

        public static EBreaker SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Breaker_Select", connection);
            EBreaker breaker = new EBreaker();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                    breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                    breaker.Type = Convert.ToByte(reader["Type"].ToString());
                    breaker.Comment = reader["Comment"].ToString();
                    breaker.Name = reader["Name"].ToString();
                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found for productCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error  EBreaker.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return breaker;

        }

        //ASHKTORAB
        public static EBreaker ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt32(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", breaker.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            //ed.writeMessage("insulator.code:" + breaker.code + "Type:" + Atend.Control.Enum.ProductType.Breaker + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.writeMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.writeMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************




            //Connection.Close();
            return breaker;
        }

        //MEDHAT //ShareOnServer
        public static EBreaker ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();

            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : breaker\n");
            }
            reader.Close();
            return breaker;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part ~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //Opration
                    Counter = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
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
                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;//ContainerCode;
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                        if (containerPackage.InsertX(transaction, Connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    //ed.writeMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
                    Counter = 0;
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
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

                    //****************

                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Breaker_InsertX", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {

                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Breaker, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in breaker");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Breaker, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.LcalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Breaker_UpdateX", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("Name:{0} \n", Name);
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Ampre:{0} \n", Amper);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Breaker))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Breaker, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in breaker");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Breaker");
                    }
                }
                ed.WriteMessage("EBreaker.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Breaker, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(_transaction, Connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                        Counter++;
                    }



                    //Package
                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.ContainerCode = 0;//ContainerCode;
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    ////ed.writeMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
                    //Counter = 0;
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.writeMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
                    //}

                    //****************

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
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In EBreaker Transaction01:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Insert02: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public bool XXUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.writeMessage("GOTO\n");


                    //Package
                    //ed.writeMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
                    //ed.writeMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.writeMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    Connection.Close();
                    transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EBreaker.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.writeMessage("GOTO\n");


                    //Package
                    //ed.writeMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
                    //ed.writeMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.writeMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
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

                    //Operation
                    Counter = 0;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);

                            if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
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
                    if (canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction(local):{0}", ex1.Message);
                    Connection.Close();
                    transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EBreaker.UpdateX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.writeMessage("GOTO\n");
                    //if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker);

                    //        if (_EOperation.InsertX(_transaction, Connection) && canCommitTransaction)
                    //        {
                    //            canCommitTransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canCommitTransaction = false;
                    //        }
                    //        Counter++;
                    //    }
                    //}

                    //Package
                    //ed.writeMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
                    ////ed.writeMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    //Counter = 0;
                    //if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        //ed.writeMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                    //        _EProductPackage.ContainerPackageCode = containerPackage.Code;

                    //        if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
                    //        {
                    //            canCommitTransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canCommitTransaction = false;

                    //        }
                    //        Counter++;
                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
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
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction(local):{0}", ex1.Message);
                    //Connection.Close();
                    //transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EBreaker.UpdateX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Breaker);
            //ed.WriteMessage("*00*\n");
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }
            //ed.WriteMessage("*01*\n");
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_DeleteX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));

                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //Operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker)) & canCommitTransaction)
                    {
                        transaction.Commit();
                        Connection.Close();
                        return true;

                    }
                    else
                    {
                        transaction.Rollback();
                        Connection.Close();
                        return false;
                    }

                }


                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    transaction.Rollback();
                    Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR EBreaker.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackage.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_DeleteX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));

                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) && canCommitTransaction)
                    {
                        //transaction.Commit();
                        //Connection.Close();
                        canCommitTransaction = true;
                    }
                    else
                    {
                        //transaction.Rollback();
                        //Connection.Close();
                        canCommitTransaction = false;
                    }
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker)) && canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBreaker.Delete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EBreaker SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", breaker.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            //ed.writeMessage("insulator.code:" + breaker.code + "Type:" + Atend.Control.Enum.ProductType.Breaker + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.writeMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.writeMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************
            //Operation
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", breaker.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeys.Add(Op);
            }
            reader.Close();

            Connection.Close();
            return breaker;
        }

        //MOUSAVI->SentFromLocalToAccess
        public static EBreaker SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return breaker;
        }

        //ShareOnServer //ShareOnServer
        public static EBreaker SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();
            //Connection.Close();
            return breaker;
        }

        public static EBreaker SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", breaker.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            //ed.writeMessage("insulator.code:" + breaker.code + "Type:" + Atend.Control.Enum.ProductType.Breaker + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

                //ed.writeMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.writeMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************
            //Operation
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", breaker.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }
            reader.Close();



            //Connection.Close();
            return breaker;
        }

        //ASHKTORAB
        public static EBreaker SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();

            //Connection.Close();
            return breaker;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_breaker_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Breaker_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_SearchByName", connection);
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

        public static EBreaker CheckForExist(double _Amper, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Breaker_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();
            connection.Close();
            return breaker;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbTransaction transaction;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Breaker_Insert", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                //if (Connection.State == ConnectionState.Closed)
                //{
                //    Connection.Open();
                //}

                Connection.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.AccessInsert : {0} \n", ex.Message));
                Connection.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Breaker_Insert", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("Name:{0} \n", Name);
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Ampre:{0} \n", Amper);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));

            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Breaker);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in breaker");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Breaker, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }
        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Breaker_Insert", Connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            //ed.WriteMessage("Name:{0} \n", Name);
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Ampre:{0} \n", Amper);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            int OldCode = Code;
            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Breaker);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in breaker");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Breaker: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Breaker, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Breaker failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBreaker.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EBreaker AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();

            ////EQUIPMENT
            //command.Parameters.Clear();
            //command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            //command.Parameters.Add(new OleDbParameter("iCode", breaker.Code));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Breaker));
            ////ed.writeMessage("insulator.code:" + breaker.code + "Type:" + Atend.Control.Enum.ProductType.Breaker + "\n");
            //reader = command.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (reader.Read())
            //{

            //    //ed.writeMessage("True\n");
            //    nodeKeysEPackage.Add(reader["ProductCode"].ToString());
            //    nodeTypeEPackage.Add(reader["TableType"].ToString());
            //    nodeCountEPackage.Add(reader["Count"].ToString());
            //    //ed.writeMessage("Type:" + nodeTypeEPackage + "\n");
            //}

            //reader.Close();
            ////**************


            Connection.Close();
            return breaker;
        }

        public static EBreaker AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _Connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();
            return breaker;
        }

        public static EBreaker AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Breaker_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();

            //Connection.Close();
            return breaker;
        }

        public static EBreaker AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Breaker_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EBreaker breaker = new EBreaker();
            if (reader.Read())
            {
                breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                breaker.Type = Convert.ToByte(reader["Type"].ToString());
                breaker.Comment = reader["Comment"].ToString();
                breaker.Name = reader["Name"].ToString();
                breaker.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                breaker.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return breaker;
        }

        //MOUSAVI->SentFromLocalToAccess
        //public static EBreaker AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection Connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Breaker_SelectByXCode", Connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("ixCode", XCode));
        //    command.Transaction = _transaction;

        //    OleDbDataReader reader = command.ExecuteReader();
        //    EBreaker breaker = new EBreaker();
        //    if (reader.Read())
        //    {
        //        breaker.Code = Convert.ToInt16(reader["Code"].ToString());
        //        breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
        //        breaker.Type = Convert.ToByte(reader["Type"].ToString());
        //        breaker.Comment = reader["Comment"].ToString();
        //        breaker.Name = reader["Name"].ToString();
        //        breaker.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        breaker.Code = -1;
        //    }
        //    reader.Close();
        //    return breaker;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Breaker_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));

            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_breaker_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsBreaker = new DataSet();
            adapter.Fill(dsBreaker);
            return dsBreaker.Tables[0];
        }

        public static EBreaker AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Breaker_Select", connection);
            EBreaker breaker = new EBreaker();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    breaker.Code = Convert.ToInt16(reader["Code"].ToString());
                    breaker.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    breaker.Amper = Convert.ToSingle(reader["Amper"].ToString());
                    breaker.Type = Convert.ToByte(reader["Type"].ToString());
                    breaker.Comment = reader["Comment"].ToString();
                    breaker.Name = reader["Name"].ToString();
                }
                else
                {
                    ed.WriteMessage(string.Format("No record found for productCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error  EBreaker.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return breaker;

        }

        //frmDrawBreaker
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

        //        EBreaker breaker = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
        //            //ed.WriteMessage("\n111\n");

        //            EBreaker Ap = Atend.Base.Equipment.EBreaker.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n222\n");

        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EBreaker.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    //ed.WriteMessage("\n333\n");

        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            DataTable OperationTbl = new DataTable();
        //            breaker.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, breaker.XCode);
        //            breaker.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (breaker.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, breaker.Code, (int)Atend.Control.Enum.ProductType.Breaker))
        //                {
        //                    if (!breaker.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("\n114\n");

        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }


        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, breaker.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Breaker, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                Servertransaction.Commit();
        //                Serverconnection.Close();

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EAirPost.ShareOnServer {0}\n", ex1.Message));

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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
        //            EBreaker Ap = Atend.Base.Equipment.EBreaker.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EBreaker.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EBreaker.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Breaker));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Breaker, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EBreaker.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;
        //}

    }
}
