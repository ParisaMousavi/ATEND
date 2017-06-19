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
    public class EDisconnector
    {
        public EDisconnector()
        { }

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

        private int insertedCode;
        public int InsertedCode
        {
            get { return insertedCode; }
            set { insertedCode = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
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

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeKeysX = new ArrayList();

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();



        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

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
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    InsertedCode = Convert.ToInt32(command.ExecuteScalar());
                    Code = insertedCode;
                    bool canCommitTransaction = true;
                    int counter = 0;

                    //ed.WriteMessage("count of subequip " + operationList.Count.ToString() + " \n");
                    while (canCommitTransaction && counter < operationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);

                        _EOperation.ProductCode = InsertedCode;
                        _EOperation.XCode = XCode;
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        counter++;
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
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During EDisconnector Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Insert: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_disconnector_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Disconnector, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Disconnector, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_disconnector_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Disconnector))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Disconnector, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Failed in Disconnector");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Disconnector, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if ((EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))))
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
                    ed.WriteMessage(string.Format("Error EDisconector.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }

                //connection.Close();
                //return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if ((EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))))
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
                    ed.WriteMessage(string.Format("Error EDisconector.ServerDelete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }

                //connection.Close();
                //return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.ServerDelete: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EDisconnector SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();

            }

            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", Disconnector.code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            //reader = command.ExecuteReader();

            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            //}

            //reader.Close();
            connection.Close();
            return Disconnector;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //Code = Convert.ToInt32(reader["Code"].ToString());
                //XCode = new Guid(reader["XCode"].ToString());
                //IsDefault = Convert.ToBoolean(reader["Code"].ToString());
                //ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();

            }

            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", Disconnector.code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            //reader = command.ExecuteReader();

            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            //}

            //reader.Close();
            connection.Close();
        }

        public static EDisconnector SelectByProductCode(int ProductCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EDisconnector Disconnector = new EDisconnector();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                    Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                    Disconnector.Comment = reader["Comment"].ToString();
                    Disconnector.Name = reader["Name"].ToString();

                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EDisconnector.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return Disconnector;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Disconnector_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Disconnector_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        public static DataTable DrawSearch(Single Amper, byte IsExistance)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Disconnector_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        //ASHKTORAB
        public static EDisconnector ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
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
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            reader = command.ExecuteReader();

            nodeKeysX.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            }

            reader.Close();
            //connection.Close();
            return Disconnector;
        }

        //MEDHAT //ShareOnServer
        public static EDisconnector ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();

            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                Disconnector.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : disconnector\n");
            }
            reader.Close();
            return Disconnector;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_InsertX", connection);
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
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    InsertedCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    //Package
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);
                        if (containerPackage.InsertX(transaction, connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    while (canCommitTransaction && Counter < EquipmentList.Count)
                    {
                        Atend.Base.Equipment.EProductPackage _EProductPackage;
                        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                        Counter++;
                    }

                    //****************
                    //Opration
                    Counter = 0;
                    //ed.WriteMessage("count of subequip " + operationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < operationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);

                        //_EOperation.ProductCode = 0;// InsertedCode;
                        _EOperation.XCode = XCode;
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error Occured During EDisconnector Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    InsertedCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int counter = 0;

                    while (canCommitTransaction && counter < operationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);

                        _EOperation.ProductCode = 0;// InsertedCode;
                        _EOperation.XCode = XCode;
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                            //ed.WriteMessage("Error For Insert \n");
                        }
                        counter++;
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
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During EDisconnector Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Insert: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_disconnector_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Disconnector, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Disconnector, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_disconnector_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Disconnector))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Disconnector, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Failed in Disconnector");
                    }
                }
                ed.WriteMessage("Disconnector.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Disconnector, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Update", connection);
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
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommittransaction = true;
                    int counter = 0;

                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector)))
                    {

                        while (canCommittransaction && counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                            _EOperation.ProductCode = Code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);
                            if (_EOperation.Insert(transaction, connection) && canCommittransaction)
                            {
                                canCommittransaction = true;
                            }
                            else
                            {
                                canCommittransaction = false;
                            }
                            counter++;
                        }
                    }
                    //ed.WriteMessage("2 \n");
                    if (canCommittransaction)
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
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommittransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    {
                        while (canCommittransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;
                            if (_EProductPackage.InsertX(transaction, connection) && canCommittransaction)
                            {
                                canCommittransaction = true;
                            }
                            else
                            {
                                canCommittransaction = false;
                            }
                            Counter++;
                        }
                    }
                    else
                    {
                        canCommittransaction = false;
                    }

                    //Operation
                    Counter = 0;
                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector)))
                    {
                        while (canCommittransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);
                            if (_EOperation.InsertX(transaction, connection) && canCommittransaction)
                            {
                                canCommittransaction = true;
                            }
                            else
                            {
                                canCommittransaction = false;
                            }
                            Counter++;
                        }
                    }
                    if (canCommittransaction)
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
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    bool canCommittransaction = true;
                    int counter = 0;

                    //if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector)))
                    //{

                    //    while (canCommittransaction && counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                    //        //_EOperation.ProductCode = 0;// Code;
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector);
                    //        if (_EOperation.InsertX(transaction, connection) && canCommittransaction)
                    //        {
                    //            canCommittransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canCommittransaction = false;
                    //        }
                    //        counter++;
                    //    }
                    //}
                    //ed.WriteMessage("2 \n");
                    if (canCommittransaction)
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
                        //throw new Exception("can not commit transaction");

                    }
                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Update: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Disconnector);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    bool canCommitTransaction = true;
                    command.ExecuteNonQuery();
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));

                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //Operation
                    if ((EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))))
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
                    ed.WriteMessage(string.Format("Error EDisconector.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }

                //connection.Close();
                //return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    bool canCommitTransaction = true;
                    command.ExecuteNonQuery();
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if ((EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector))))
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
                    ed.WriteMessage(string.Format("Error EDisconector.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }

                //connection.Close();
                //return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EDisconnector.Delete: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EDisconnector SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }

            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
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
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            reader = command.ExecuteReader();

            nodeKeysX.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeysX.Add(Op);
                //ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            }

            reader.Close();
            connection.Close();
            return Disconnector;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EDisconnector SelectByXCodeForDesign(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                Disconnector.Code = -1;
            }
            reader.Close();
            connection.Close();
            return Disconnector;
        }

        //MEDHAT //ShareOnServer
        public static EDisconnector SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                Disconnector.Code = -1;
            }
            reader.Close();
            return Disconnector;
        }

        public static EDisconnector SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
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
            command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            reader = command.ExecuteReader();

            nodeKeysX.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            }

            reader.Close();
            //connection.Close();
            return Disconnector;
        }

        //ASHKTORAB
        public static EDisconnector SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                Disconnector.Code = -1;
            }

            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", Disconnector.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            //reader = command.ExecuteReader();

            //nodeKeysX.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeysX.Add(reader["ProductID"].ToString());
            //    ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            //}

            //reader.Close();
            //connection.Close();
            return Disconnector;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Disconnector_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        //BindDataToTreeViewX - SelectAllAndMerge
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Disconnector_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_SearchByName", connection);
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

        public static EDisconnector CheckForExist(double _Ampre, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Disconnector_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Ampre));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                Disconnector.Code = -1;
            }
            reader.Close();
            connection.Close();
            return Disconnector;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_disconnector_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_disconnector_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Disconnector);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Disconnector, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_disconnector_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Disconnector);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Disconnector, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Disconnector failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EDisconnector.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EDisconnector AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();

            }
            else
            {
                Disconnector.Code = -1;
            }

            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", Disconnector.code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            reader = command.ExecuteReader();

            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            }

            reader.Close();
            connection.Close();
            return Disconnector;
        }

        public static EDisconnector AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();

            }
            else
            {
                Disconnector.Code = -1;
            }

            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", Disconnector.code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Disconnector));
            reader = command.ExecuteReader();

            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //ed.WriteMessage("ProductId=" + reader["ProductID"].ToString() + "\n");

            }

            reader.Close();
            return Disconnector;
        }

        public static EDisconnector AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Disconnector_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();

            }
            else
            {
                Disconnector.Code = -1;
            }

          
            //connection.Close();
            return Disconnector;
        }

        public static EDisconnector AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Disconnector_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EDisconnector Disconnector = new EDisconnector();
            if (reader.Read())
            {
                Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                Disconnector.Comment = reader["Comment"].ToString();
                Disconnector.Name = reader["Name"].ToString();
                Disconnector.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                Disconnector.Code = -1;
            }

            reader.Close();
            connection.Close();
            return Disconnector;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EDisconnector AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Disconnector_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EDisconnector Disconnector = new EDisconnector();
        //    if (reader.Read())
        //    {
        //        Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
        //        Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
        //        Disconnector.Comment = reader["Comment"].ToString();
        //        Disconnector.Name = reader["Name"].ToString();
        //        Disconnector.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        Disconnector.Code = -1;
        //    }
        //    reader.Close();
        //    return Disconnector;
        //}

        public static EDisconnector AccessSelectByProductCode(int ProductCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Disconnector_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EDisconnector Disconnector = new EDisconnector();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Disconnector.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    Disconnector.Code = Convert.ToInt32(reader["Code"].ToString());
                    Disconnector.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    Disconnector.Type = Convert.ToByte(reader["Type"].ToString());
                    Disconnector.Comment = reader["Comment"].ToString();
                    Disconnector.Name = reader["Name"].ToString();

                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EDisconnector.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return Disconnector;
        }

        //SelectAllAndMerge
        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Disconnector_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Disconnector_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        public static DataTable AccessDrawSearch(Single Amper, byte IsExistance)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Disconnector_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            DataSet dsDisconnector = new DataSet();
            adapter.Fill(dsDisconnector);
            return dsDisconnector.Tables[0];
        }

        //frmDrawDisconnector
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
        //        EDisconnector catout = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
        //            EDisconnector Ap = Atend.Base.Equipment.EDisconnector.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EDisconnector.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            DataTable OperationTbl = new DataTable();
        //            catout.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, catout.XCode);
        //            catout.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);
        //            if (catout.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, catout.Code, (int)Atend.Control.Enum.ProductType.Disconnector))
        //                {
        //                    if (!catout.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
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
        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, catout.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Disconnector, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                Servertransaction.Commit();
        //                Serverconnection.Close();
        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
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
        //        ed.WriteMessage(string.Format(" ERROR EDisconnector.ShareOnServer {0}\n", ex1.Message));
        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;



        //    //**********************************
        //    //SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    //SqlTransaction Servertransaction;

        //    //SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    //SqlTransaction Localtransaction;

        //    //int DeletedCode = 0;


        //    //try
        //    //{
        //    //    Serverconnection.Open();
        //    //    Servertransaction = Serverconnection.BeginTransaction();

        //    //    Localconnection.Open();
        //    //    Localtransaction = Localconnection.BeginTransaction();

        //    //    EDisconnector disconnector = SelectByXCode(Localtransaction, Localconnection, XCode);

        //    //    bool canCommitTransaction = true;
        //    //    int Counter = 0;

        //    //    try
        //    //    {


        //    //        Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
        //    //        //ed.WriteMessage("\n111\n");

        //    //        EDisconnector Ap = Atend.Base.Equipment.EDisconnector.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //    //        if (Ap.XCode != Guid.Empty)
        //    //        {
        //    //            DeletedCode = Ap.Code;
        //    //            if (!Atend.Base.Equipment.EDisconnector.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //    //            {
        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();

        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //            }
        //    //        }

        //    //        DataTable OperationTbl = new DataTable();
        //    //        disconnector.OperationList = new ArrayList();
        //    //        OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, disconnector.XCode);
        //    //        disconnector.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //    //        if (disconnector.Insert(Servertransaction, Serverconnection))
        //    //        {
        //    //            if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, disconnector.Code, (int)Atend.Control.Enum.ProductType.Disconnector))
        //    //            {
        //    //                if (!disconnector.UpdateX(Localtransaction, Localconnection))
        //    //                {
        //    //                    //ed.WriteMessage("\n115\n");

        //    //                    Servertransaction.Rollback();
        //    //                    Serverconnection.Close();
        //    //                    Localtransaction.Rollback();
        //    //                    Localconnection.Close();
        //    //                    return false;
        //    //                }
        //    //                else
        //    //                {
        //    //                    //ed.WriteMessage("\n113\n");

        //    //                    //Servertransaction.Commit();
        //    //                    //Serverconnection.Close();

        //    //                    //Localtransaction.Commit();
        //    //                    //Localconnection.Close();
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                //ed.WriteMessage("\n114\n");

        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();

        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //                return false;
        //    //            }


        //    //        }
        //    //        else
        //    //        {
        //    //            //ed.WriteMessage("\n114\n");

        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }

        //    //        if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, disconnector.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Disconnector, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //    //        {
        //    //            //ed.WriteMessage("\n113\n");

        //    //            Servertransaction.Commit();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Commit();
        //    //            Localconnection.Close();
        //    //        }
        //    //        else
        //    //        {
        //    //            //ed.WriteMessage("\n114\n");

        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }



        //    //        //ed.WriteMessage("\n112\n");




        //    //    }
        //    //    catch (System.Exception ex1)
        //    //    {
        //    //        ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //    //        Servertransaction.Rollback();
        //    //        Serverconnection.Close();

        //    //        Localtransaction.Rollback();
        //    //        Localconnection.Close();
        //    //        return false;
        //    //    }


        //    //}
        //    //catch (System.Exception ex1)
        //    //{
        //    //    ed.WriteMessage(string.Format(" ERROR ECatOut.ShareOnServer {0}\n", ex1.Message));

        //    //    Serverconnection.Close();
        //    //    Localconnection.Close();
        //    //    return false;
        //    //}

        //    //return true;
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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
        //            EDisconnector Ap = Atend.Base.Equipment.EDisconnector.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EDisconnector.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EDisconnector.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Disconnector));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Disconnector, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EDisconnector.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;
        //}


    }
}
