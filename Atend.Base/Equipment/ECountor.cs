using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;
using System.Collections;

namespace Atend.Base.Equipment
{
    public class ECountor
    {

        public ECountor()
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

        private int phaseCount;
        public int PhaseCount
        {
            get { return phaseCount; }
            set { phaseCount = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Sever Part~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {


                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECountor.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Transaction = _transaction;

            try
            {


                //Connection.Open();
                Code = Convert.ToInt32(Command.ExecuteScalar());
                //Connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);
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


                return true;


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECountor.InsertX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Countor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Countor, LocalTransaction, LocalConnection);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Countor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Countor_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Countor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Countor, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in Countor");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Countor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECountor.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECountor.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                //Connection.Close();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor)))
                {
                    canCommitTransaction = true;

                }
                else
                {
                    canCommitTransaction = false;
                }

                if (canCommitTransaction)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECountor.ServerDelete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static ECountor SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            return countor;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            //ECountor countor = new ECountor();
            if (reader.Read())
            {
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //countor.Code = Convert.ToInt32(reader["Code"].ToString());
                PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                //countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }

            reader.Close();
            Connection.Close();
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Countor_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);

            return dsCountor.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Countor_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);

            return dsCountor.Tables[0];
        }

        public static ECountor SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            ECountor countor = new ECountor();

            try
            {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    countor.Code = Convert.ToInt32(reader["Code"].ToString());
                    countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                    countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    countor.Comment = reader["Comment"].ToString();
                    countor.Name = reader["Name"].ToString();
                }
                else
                {
                    //ed.writeMessage(string.Format("No record found fot ProductCode : {0} \n",ProductCode));
                }

                reader.Close();
                Connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECounter.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }

            return countor;
        }

        //ASHKTORAB
        public static ECountor ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();

            return countor;
        }

        //MEDHAT //ShareOnServer
        public static ECountor ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = ServerTransaction;

            Command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();

            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                countor.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : countor\n");
            }
            reader.Close();
            return countor;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Countor_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;

                try
                {
                    Command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);
                        if (containerPackage.InsertX(transaction, Connection))
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
                        if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error ECounter.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ECounter.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            ////////////////////////
            //try
            //{


            //    Connection.Open();
            //    Command.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;


            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.InsertX: {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Transaction = _transaction;

            try
            {
                //connection.Open();
                Command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);
                    //_EOperation.ProductID = 
                    if (_EOperation.InsertX(_transaction, Connection) && canCommitTransaction)
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
                ed.WriteMessage(string.Format("Error ECountor.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
            //////////////////////
            //try
            //{
            //    //Connection.Open();
            //    Command.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.InsertX: {0} \n", ex1.Message));
            //    //Connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Countor_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                ////insertCommand.ExecuteNonQuery();
                ////insertCommand.Parameters.Clear();
                ////insertCommand.CommandType = CommandType.Text;
                ////insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                insertCommand.ExecuteNonQuery();


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Countor, ServerConnection, ServerTransaction);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Countor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Countor_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode , (int)Atend.Control.Enum.ProductType.Countor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Countor, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in Countor");
                    }
                }
                ed.WriteMessage("ECountor.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Countor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Countor_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                _transaction = Connection.BeginTransaction();
                Command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    Command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);

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
                    }


                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(_transaction, Connection) && canCommitTransaction)
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
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        _transaction.Rollback();
                        Connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }


                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error ECountor.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ECountor.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            //////////////////////////
            //try
            //{
            //    Connection.Open();
            //    Command.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.UpdateX: {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_UpdateX", Connection);
            Command.Transaction = _transaction;

            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                //connection.Open();
                Command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                //if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor)))
                //{

                //    while (canCommitTransaction && Counter < operationList.Count)
                //    {

                //        Atend.Base.Equipment.EOperation _EOperation;
                //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                //        _EOperation.XCode = XCode;
                //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Countor);

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
                //if (canCommitTransaction)
                //{
                //    _transaction.Commit();
                //    Connection.Close();
                //    return true;
                //}
                //else
                //{
                //    _transaction.Rollback();
                //    Connection.Close();
                //    return false;
                //    //throw new Exception("can not commit transaction");

                //}
                return true;

            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ECountor.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }

            ////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    Command.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.UpdateX: {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Countor);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Countor_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error ECountor.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ECountor.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }


            //////////////////////////
            //try
            //{
            //    Connection.Open();
            //    Command.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.DeleteX: {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                Command.Transaction = _transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor)))
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
                    ed.WriteMessage(string.Format("Error ECountor.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ECountor.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            //////////////////
            //try
            //{
            //    //Connection.Open();
            //    Command.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR ECountor.DeleteX: {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess
        public static ECountor SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Countor_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.XCode = new Guid(reader["XCode"].ToString());
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                countor.code = -1;
            }
            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", countor.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Countor));
            reader = Command.ExecuteReader();
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
            Command.Parameters.Clear();
            Command.CommandText = "E_Operation_SelectByXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", countor.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Countor));
            reader = Command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            Command.Parameters.Clear();
            Command.CommandText = "E_Operation_SelectByXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", countor.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Countor));
            reader = Command.ExecuteReader();
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
            return countor;
        }

        public static ECountor SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", countor.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Countor));
            reader = Command.ExecuteReader();
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
            Command.Parameters.Clear();
            Command.CommandText = "E_Operation_SelectByXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", countor.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Countor));
            reader = Command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();

            return countor;
        }

        //MEDHAT //ShareOnServer
        public static ECountor SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                countor.Code = -1;
            }
            reader.Close();
            return countor;
        }

        //ASHKTORAB
        public static ECountor SelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();

            return countor;
        }

        public static ECountor SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                countor.Code = -1;
            }

            reader.Close();

            return countor;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Countor_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);
            return dsCountor.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Countor_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);

            return dsCountor.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Countor_SearchByName", connection);
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

        //MEDHAT
        public static ECountor CheckForExist(double _Amper, int _PhaseCount)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Countor_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iPhaseCount", _PhaseCount));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                countor.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                countor.Code = -1;
            }
            reader.Close();
            connection.Close();
            return countor;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Countor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
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
                ed.WriteMessage(string.Format("Error ECountor.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Countor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Countor);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Countor, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection ;
            OleDbCommand insertCommand = new OleDbCommand("E_Countor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            int oldCode = Code;
            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Countor);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess (oldCode, (int)Atend.Control.Enum.ProductType.Countor, Code, _OldTransaction,_OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Countor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECountor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static ECountor AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
            }
            else
            {
                countor.Code = -1;
            }
            return countor;
        }

        public static ECountor AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
            }
            else
            {
                countor.Code = -1;
            }
            return countor;
        }

        public static ECountor AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
            }
            else
            {
                countor.Code = -1;
            }
            reader.Close();
            return countor;
        }

        public static ECountor AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_Countor_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            ECountor countor = new ECountor();
            if (reader.Read())
            {
                countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                countor.Code = Convert.ToInt32(reader["Code"].ToString());
                countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                countor.Comment = reader["Comment"].ToString();
                countor.Name = reader["Name"].ToString();
                countor.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            Connection.Close();
            return countor;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ECountor AccessSelectByXCode(Guid XCode , OleDbTransaction _transaction , OleDbConnection _connection)
        //{
        //    OleDbConnection Connection = _connection;
        //    OleDbCommand Command = new OleDbCommand("E_Countor_SelectByXCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;

        //    Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    Command.Transaction = _transaction;
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    ECountor countor = new ECountor();
        //    if (reader.Read())
        //    {
        //        countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        countor.Code = Convert.ToInt32(reader["Code"].ToString());
        //        countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
        //        countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        countor.Comment = reader["Comment"].ToString();
        //        countor.Name = reader["Name"].ToString();
        //        countor.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        countor.Code = -1;
        //    }
        //    reader.Close();
        //    return countor;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Countor_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);

            return dsCountor.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Countor_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsCountor = new DataSet();
            adapter.Fill(dsCountor);

            return dsCountor.Tables[0];
        }

        public static ECountor AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_Countor_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            ECountor countor = new ECountor();

            try
            {

                Connection.Open();
                OleDbDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    countor.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    countor.Code = Convert.ToInt32(reader["Code"].ToString());
                    countor.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                    countor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    countor.Comment = reader["Comment"].ToString();
                    countor.Name = reader["Name"].ToString();
                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found fot ProductCode : {0} \n", ProductCode));
                }

                reader.Close();
                Connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECounter.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }

            return countor;
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

        //        ECountor _ECountor = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor));
        //            //ed.WriteMessage("\n111\n");

        //            ECountor Ap = Atend.Base.Equipment.ECountor.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ECountor.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _ECountor.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _ECountor.XCode);
        //            _ECountor.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_ECountor.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _ECountor.Code, (int)Atend.Control.Enum.ProductType.Countor))
        //                {
        //                    if (_ECountor.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n113\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();

        //                        //return true;
        //                    }
        //                    else
        //                    {
        //                        //ed.WriteMessage("\n114\n");

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ECountor.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Countor, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ECountor.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor));
        //            //ed.WriteMessage("\n111\n");

        //            ECountor Ap = Atend.Base.Equipment.ECountor.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n222\n");
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ECountor.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.ECountor.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Countor));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Countor, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EConductor.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
