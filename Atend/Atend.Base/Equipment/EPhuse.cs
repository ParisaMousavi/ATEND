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
    public class EPhuse
    {
        public EPhuse()
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

        private int phusePoleCode;
        public int PhusePoleCode
        {
            get { return phusePoleCode; }
            set { phusePoleCode = value; }
        }

        private Guid phusePolexCode;
        public Guid PhusePoleXCode
        {
            get { return phusePolexCode; }
            set { phusePolexCode = value; }
        }

        private int phuseKeyCode;
        public int PhuseKeyCode
        {
            get { return phuseKeyCode; }
            set { phuseKeyCode = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format("Error EPhuse.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {
                Code = Convert.ToInt32(command.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse);
                    if (_EOperation.Insert(_transaction, connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
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
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Phuse, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in PHUSE");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Phuse, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Phuse failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.ServerInsert : {0} \n", ex1.Message));
                return false;
            }
        }


        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Phuse_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Phuse))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Phuse, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in PHUSE");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Phuse");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Phuse, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Phuse failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();


            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            connection.Close();
            return true;
        }

        //ASHKTORAB
        public static bool Update(SqlTransaction _transaction, SqlConnection _connection, int PhusePoleCode, int LastPhusePoleCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iLastPhusePolCode", LastPhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            //command.Parameters.Add(new SqlParameter("iAmper", Amper));
            //command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            //command.Parameters.Add(new SqlParameter("iComment", Comment));
            //command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.Update : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Phuse_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse)))
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
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Delete", connection);
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
                ed.WriteMessage(string.Format("Error EPhuse.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static EPhuse SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
            }
            reader.Close();
            connection.Close();
            return phuse;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                //phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                //phuse.XCode = new Guid(reader["XCode"].ToString());
                //phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }
            reader.Close();
            connection.Close();
            //return phuse;
        }

        public static EPhuse SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Phuse_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EPhuse phuse = new EPhuse();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                    phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phuse.Type = Convert.ToByte(reader["Type"].ToString());
                    //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                    phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                    phuse.Comment = reader["Comment"].ToString();
                    phuse.Name = reader["Name"].ToString();
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
                ed.WriteMessage(string.Format("Error EPhuse.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return phuse;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Phuse_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Phuse_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
        }

        //ASHKTORAB
        public static EPhuse ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Phuse_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt32(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                //phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //connection.Close();
            return phuse;

        }

        //MEDHAT //ShareOnServer
        public static EPhuse ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();

            if (reader.Read())
            {
                phuse.Code = Convert.ToInt32(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                //phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phuse.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : phuse\n");
            }
            reader.Close();
            return phuse;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iType", Type));
            Command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //Command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse);
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
                    ed.WriteMessage(string.Format("Error EPhuse.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhuse.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();

            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuse.Insert : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
            //connection.Close();
            //return true;
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iType", Type));
            Command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //Command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            try
            {
                //Connection.Open();
                Command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse);
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
                    //Connection.Close();
                    return true;
                }
                else
                {
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }
                //Connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Phuse_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Phuse, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in PHUSE");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Phuse, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Phuse failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Phuse_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Phuse))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Phuse, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in PHUSE");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Phuse");
                    }
                }
                ed.WriteMessage("Ephuse.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("IN Phuse :Code={0},XCode={1}\n",Code,XCode);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Phuse, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Phuse failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
               // Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iAmper", Amper));
            Command.Parameters.Add(new SqlParameter("iType", Type));
            Command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //Command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
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
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
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
                    ed.WriteMessage(string.Format("Error EPhuse.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhuse.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();

            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuse.UpdateX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Phuse_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //ed.WriteMessage("\naaa111\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleXCode", PhusePoleXCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //ed.WriteMessage("\naaa222\n");
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                //ed.WriteMessage("\naaa333\n");
                bool canCommitTransaction = true;
                int Counter = 0;
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhuse.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.Phuse);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EPhuse.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhuse.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }


        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Phuse_DeleteX", connection);
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
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse)))
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
                    ed.WriteMessage(string.Format("Error EPhuse.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhuse.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            /////////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuse.DeleteX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess //AcDrawDB
        public static EPhuse SelectByXCode(Guid Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_SelectByXCode", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            SqlDataReader reader = null;
            EPhuse phuse = new EPhuse();
            connection.Open();
            reader = Command.ExecuteReader();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                //phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
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
            Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
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
            //************
            connection.Close();
            return phuse;
        }

        //ASHKTORAB //ShareOnServer
        public static EPhuse SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = LocalConnection;
            SqlCommand Command = new SqlCommand("E_Phuse_SelectByXCode", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            EPhuse phuse = new EPhuse();
            try
            {
                Command.Transaction = LocalTransaction;
                SqlDataReader reader = null;

                reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                    phuse.XCode = new Guid(reader["XCode"].ToString());
                    phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phuse.Type = Convert.ToByte(reader["Type"].ToString());
                    //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                    //phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                    phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                    phuse.Comment = reader["Comment"].ToString();
                    phuse.Name = reader["Name"].ToString();
                    phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                }
                else
                {
                    phuse.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error EPhuseKey.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return phuse;
        }

        public static EPhuse SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_Phuse_SelectByXCode", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;

            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
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
            Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
            reader = Command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            //************
            //connection.Close();
            return phuse;
        }

        //Medhat
        public static EPhuse SelectByPhusePoleXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Phuse_SelectByPhusePoleXCode", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = null;
            EPhuse phuse = new EPhuse();
            connection.Open();
            reader = Command.ExecuteReader();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                //phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            ////EQUIPMENT
            //Command.Parameters.Clear();
            //Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            //Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            //Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
            //reader = Command.ExecuteReader();
            //nodeCountEPackageX.Clear();
            //nodeKeysEPackageX.Clear();
            //nodeTypeEPackageX.Clear();
            //while (reader.Read())
            //{

            //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
            //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
            //    nodeCountEPackageX.Add(reader["Count"].ToString());
            //}

            //reader.Close();
            ////**************
            ////OPERATION
            //Command.Parameters.Clear();
            //Command.CommandText = "E_Operation_SelectByXCodeType";
            //Command.Parameters.Add(new SqlParameter("iXCode", phuse.XCode));
            //Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Phuse));
            //reader = Command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    EOperation Op = new EOperation();
            //    Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
            //    Op.Count = Convert.ToInt32(reader["Count"].ToString());
            //    nodeKeys.Add(Op);

            //}
            //reader.Close();
            ////************
            connection.Close();
            return phuse;
        }

        //ASHKTORAB
        public static EPhuse SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return phuse;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Phuse_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Phuse_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Phuse_SearchByName", connection);
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

        public static EPhuse CheckForExist(double _Amper, byte _Type, Guid _PhusePoleXCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Phuse_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iPhusePoleXCode", _PhusePoleXCode));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.XCode = new Guid(reader["XCode"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                phuse.PhusePoleXCode = new Guid(reader["PhusePoleXCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            connection.Close();
            return phuse;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
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
                ed.WriteMessage(string.Format("Error EPhuse.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Phuse);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Phuse, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Phuse failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Phuse_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iPhusePoleCode", PhusePoleCode));
            //command.Parameters.Add(new SqlParameter("iPhuseKeyCode", PhuseKeyCode));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            int OldCode = Code;

            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Phuse);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Phuse, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Phuse failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuse.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public static EPhuse AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            connection.Close();
            return phuse;
        }


        public static EPhuse AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return phuse;
        }

        //StatusReport
        public static EPhuse AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Phuse_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                phuse.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return phuse;
        }


        public static EPhuse AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Phuse_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPhuse phuse = new EPhuse();
            if (reader.Read())
            {
                phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phuse.Type = Convert.ToByte(reader["Type"].ToString());
                //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                phuse.Comment = reader["Comment"].ToString();
                phuse.Name = reader["Name"].ToString();
                phuse.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            connection.Close();
            return phuse;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EPhuse AccessSelectByXCode(Guid XCode, OleDbTransaction _transction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Phuse_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EPhuse phuse = new EPhuse();
        //    if (reader.Read())
        //    {
        //        phuse.Code = Convert.ToInt16(reader["Code"].ToString());
        //        phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        phuse.Type = Convert.ToByte(reader["Type"].ToString());
        //        //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
        //        phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
        //        phuse.Comment = reader["Comment"].ToString();
        //        phuse.Name = reader["Name"].ToString();
        //        phuse.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        phuse.Code = -1;
        //    }
        //    reader.Close();
        //    return phuse;
        //}

        public static EPhuse AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Phuse_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EPhuse phuse = new EPhuse();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    phuse.Code = Convert.ToInt16(reader["Code"].ToString());
                    phuse.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    phuse.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phuse.Type = Convert.ToByte(reader["Type"].ToString());
                    //phuse.PhuseKeyCode = Convert.ToInt32(reader["PhuseKeyCode"].ToString());
                    phuse.PhusePoleCode = Convert.ToInt32(reader["PhusePoleCode"].ToString());
                    phuse.Comment = reader["Comment"].ToString();
                    phuse.Name = reader["Name"].ToString();
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
                ed.WriteMessage(string.Format("Error EPhuse.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return phuse;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Phuse_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Phuse_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsPhuse = new DataSet();
            adapter.Fill(dsPhuse);
            return dsPhuse.Tables[0];
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

        //        EPhuse _EPhuse = SelectByXCode(Localtransaction, Localconnection, XCode);
        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            EPhuse Ap = Atend.Base.Equipment.EPhuse.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EPhuse.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            Atend.Base.Equipment.EContainerPackage containerPackage1 = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, _EPhuse.PhusePoleXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(Servertransaction, Serverconnection, _EPhuse.PhusePoleXCode);
        //            int PhusePoleDeleted = 0;
        //            if (PHP.XCode != Guid.Empty)
        //            {
        //                PhusePoleDeleted = PHP.Code;
        //                if (!Atend.Base.Equipment.EPhusePole.ServerDelete(Servertransaction, Serverconnection, PHP.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            PHP = Atend.Base.Equipment.EPhusePole.SelectByXCode(Localtransaction, Localconnection, _EPhuse.PhusePoleXCode);
        //            DataTable OperationTbl = new DataTable();
        //            PHP.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, PHP.XCode);
        //            PHP.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (PHP.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, PhusePoleDeleted, PHP.Code, (int)Atend.Control.Enum.ProductType.PhusePole))
        //                {
        //                    if (!PHP.UpdateX(Localtransaction, Localconnection))
        //                        throw new Exception("while update PhusePole in AtendLocal");
        //                }
        //            }
        //            else
        //                throw new Exception("while insert PhusePole in AtendServer");

        //            if (!Atend.Base.Design.NodeTransaction.SubProducts(PhusePoleDeleted, PHP.Code, PHP.XCode, containerPackage1.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            _EPhuse.PhusePoleCode = PHP.Code;
        //            OperationTbl = new DataTable();
        //            _EPhuse.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPhuse.XCode);
        //            _EPhuse.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPhuse.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code, (int)Atend.Control.Enum.ProductType.Phuse))
        //                {
        //                    if (Atend.Base.Equipment.EStreetBoxPhuse.ServerUpdate(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code))
        //                    {
        //                        if (!_EPhuse.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                        else
        //                        { }
        //                    }
        //                    //else if (Atend.Base.Equipment.EDBPhuse.ServerUpdate(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code))
        //                    //{
        //                    //    if (!_EPhuse.UpdateX(Localtransaction, Localconnection))
        //                    //    {
        //                    //        //ed.WriteMessage("\n115\n");

        //                    //        Servertransaction.Rollback();
        //                    //        Serverconnection.Close();
        //                    //        Localtransaction.Rollback();
        //                    //        Localconnection.Close();
        //                    //        return false;
        //                    //    }
        //                    //    else
        //                    //    { }
        //                    //}
        //                    //else
        //                    //{
        //                    //    Servertransaction.Rollback();
        //                    //    Serverconnection.Close();
        //                    //    Localtransaction.Rollback();
        //                    //    Localconnection.Close();
        //                    //    return false;
        //                    //}
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPhuse.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Phuse, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
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
        //        ed.WriteMessage(string.Format(" ERROR EPhuse.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool ShareOnServer(SqlTransaction _Servertransaction, SqlConnection _Serverconnection, SqlTransaction _Localtransaction, SqlConnection _Localconnection, Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = _Serverconnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction = _Servertransaction;

        //    SqlConnection Localconnection = _Localconnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction = _Localtransaction;

        //    int DeletedCode = 0;

        //    try
        //    {
        //        //Serverconnection.Open();
        //        //Servertransaction = Serverconnection.BeginTransaction();

        //        //Localconnection.Open();
        //        //Localtransaction = Localconnection.BeginTransaction();

        //        EPhuse _EPhuse = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            EPhuse Ap = Atend.Base.Equipment.EPhuse.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EPhuse.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    //Servertransaction.Rollback();
        //                    //Serverconnection.Close();

        //                    //Localtransaction.Rollback();
        //                    //Localconnection.Close();

        //                    return false;
        //                }
        //            }

        //            Atend.Base.Equipment.EContainerPackage containerPackage1 = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, _EPhuse.PhusePoleXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(Servertransaction, Serverconnection, _EPhuse.PhusePoleXCode);
        //            int PhusePoleDeleted = 0;
        //            if (PHP.XCode != Guid.Empty)
        //            {
        //                PhusePoleDeleted = PHP.Code;
        //                if (!Atend.Base.Equipment.EPhusePole.ServerDelete(Servertransaction, Serverconnection, PHP.Code))
        //                {
        //                    //Servertransaction.Rollback();
        //                    //Serverconnection.Close();

        //                    //Localtransaction.Rollback();
        //                    //Localconnection.Close();

        //                    return false;
        //                }
        //            }
        //            PHP = Atend.Base.Equipment.EPhusePole.SelectByXCode(Localtransaction, Localconnection, _EPhuse.PhusePoleXCode);
        //            //ed.WriteMessage("1@@@@@@@@@@ :{0}\n", PHP.Code);
        //            DataTable OperationTbl = new DataTable();
        //            PHP.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, PHP.XCode);
        //            PHP.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);
        //            //ed.WriteMessage("2@@@@@@@@@@ :{0}\n", PHP.Code);

        //            if (PHP.Insert(Servertransaction, Serverconnection))
        //            {
        //                //ed.WriteMessage("3@@@@@@@@@@ :{0}\n", PHP.Code);
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, PhusePoleDeleted, PHP.Code, (int)Atend.Control.Enum.ProductType.PhusePole))
        //                {
        //                    if (!PHP.UpdateX(Localtransaction, Localconnection))
        //                        throw new Exception("while update PhusePole in AtendLocal");
        //                }
        //            }
        //            else
        //                throw new Exception("while insert PhusePole in AtendServer");


        //            if (!Atend.Base.Design.NodeTransaction.SubProducts(PhusePoleDeleted, PHP.Code, PHP.XCode, containerPackage1.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                //Servertransaction.Rollback();
        //                //Serverconnection.Close();

        //                //Localtransaction.Rollback();
        //                //Localconnection.Close();
        //                return false;
        //            }

        //            //ed.WriteMessage("\n444\n");
        //            //ed.WriteMessage("4@@@@@@@@@@ :{0}\n", PHP.Code);
        //            //ed.WriteMessage("5@@@@@@@@@@_EPhuse.PhusePoleXCode:{0}\n", _EPhuse.PhusePoleXCode);
        //            _EPhuse.PhusePoleCode = PHP.Code;
        //            //_EPhuse.Code = PHP.Code;
        //            //_EPhuse.PhusePoleCode = Atend.Base.Equipment.EPhusePole.SelectByXCode(_EPhuse.PhusePoleXCode).Code;
        //            OperationTbl = new DataTable();
        //            _EPhuse.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPhuse.XCode);
        //            _EPhuse.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPhuse.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code, (int)Atend.Control.Enum.ProductType.Phuse))
        //                {
        //                    if (Atend.Base.Equipment.EStreetBoxPhuse.ServerUpdate(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code))
        //                    {
        //                        if (!_EPhuse.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");

        //                            //Servertransaction.Rollback();
        //                            //Serverconnection.Close();
        //                            //Localtransaction.Rollback();
        //                            //Localconnection.Close();
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            //Servertransaction.Commit();
        //                            //Serverconnection.Close();

        //                            //Localtransaction.Commit();
        //                            //Localconnection.Close();
        //                            //return true;
        //                        }
        //                    }
        //                    //else if (Atend.Base.Equipment.EDBPhuse.ServerUpdate(Servertransaction, Serverconnection, DeletedCode, _EPhuse.Code))
        //                    //{
        //                    //    if (!_EPhuse.UpdateX(Localtransaction, Localconnection))
        //                    //    {
        //                    //        //ed.WriteMessage("\n115\n");

        //                    //        //Servertransaction.Rollback();
        //                    //        //Serverconnection.Close();
        //                    //        //Localtransaction.Rollback();
        //                    //        //Localconnection.Close();
        //                    //        return false;
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        //Servertransaction.Commit();
        //                    //        //Serverconnection.Close();

        //                    //        //Localtransaction.Commit();
        //                    //        //Localconnection.Close();
        //                    //        //return true;
        //                    //    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    //Servertransaction.Rollback();
        //                    //    //Serverconnection.Close();
        //                    //    //Localtransaction.Rollback();
        //                    //    //Localconnection.Close();
        //                    //    return false;
        //                    //}
        //                }
        //                else
        //                {
        //                    //Servertransaction.Rollback();
        //                    //Serverconnection.Close();
        //                    //Localtransaction.Rollback();
        //                    //Localconnection.Close();
        //                    return false;
        //                }

        //            }
        //            else
        //            {
        //                //Servertransaction.Rollback();
        //                //Serverconnection.Close();
        //                //Localtransaction.Rollback();
        //                //Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPhuse.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Phuse, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                //Servertransaction.Commit();
        //                //Serverconnection.Close();

        //                //Localtransaction.Commit();
        //                //Localconnection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                //Servertransaction.Rollback();
        //                //Serverconnection.Close();

        //                //Localtransaction.Rollback();
        //                //Localconnection.Close();
        //                return false;
        //            }
        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction Phuse:{0}\n", ex1.Message));
        //            //Servertransaction.Rollback();
        //            //Serverconnection.Close();

        //            //Localtransaction.Rollback();
        //            //Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EPhuse.ShareOnServer {0}\n", ex1.Message));

        //        //Serverconnection.Close();
        //        //Localconnection.Close();
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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            EPhuse Ap = Atend.Base.Equipment.EPhuse.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPhuse.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPhuse.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(Localtransaction, Localconnection, Ap.PhusePoleCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid PhusePoleDeleted = Guid.NewGuid();
        //            if (PHP.XCode != Guid.Empty)
        //            {
        //                PhusePoleDeleted = PHP.XCode;

        //                if (!Atend.Base.Equipment.EPhusePole.DeleteX(Localtransaction, Localconnection, PHP.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                PHP.ServerSelectByCode(Ap.PhusePoleCode);
        //            }
        //            else
        //            {
        //                PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(Ap.PhusePoleCode);
        //                PHP.XCode = PhusePoleDeleted;
        //                PhusePoleDeleted = Guid.Empty;
        //            }

        //            //******************
        //            PHP.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(PHP.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            PHP.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, PHP.XCode);
        //            Atend.Base.Equipment.EContainerPackage containerPackagePhusePole = EContainerPackage.selectByContainerCodeAndType(PHP.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            if (!PHP.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (!Atend.Base.Design.NodeTransaction.SubProductsForServer(PHP.Code, PHP.XCode, containerPackagePhusePole.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            //********************
        //            Ap.PhusePoleXCode = PHP.XCode;


        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl1 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl1, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Phuse, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPhuse.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}



    }
}
