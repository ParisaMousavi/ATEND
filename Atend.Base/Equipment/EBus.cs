using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EBus
    {
        public EBus()
        {
        }


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

        private string size;
        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        //private byte materialCode;
        //public byte MaterialCode
        //{
        //    get { return materialCode; }
        //    set { materialCode = value; }
        //}

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

        private double voltage;

        public double Voltage
        {
            get { return voltage; }
            set { voltage = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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
                ed.WriteMessage(string.Format(" ERROR EBus.Insert: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));

            command.Transaction = _transaction;

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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus);
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
                ed.WriteMessage(string.Format(" ERROR EBus.InsertX: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

                //connection.Close();
                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Bus_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            ed.WriteMessage("********* 1\n");

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            ed.WriteMessage("********* 2\n");
            ed.WriteMessage("* ;{0}\n", XCode);
            ed.WriteMessage("* ;{0}\n", ProductCode);
            ed.WriteMessage("* ;{0}\n", Size);
            ed.WriteMessage("* ;{0}\n", Type);
            ed.WriteMessage("* ;{0}\n", Comment);
            ed.WriteMessage("* ;{0}\n", Name);
            ed.WriteMessage("* ;{0}\n", Voltage);
            ed.WriteMessage("* ;{0}\n", IsDefault);
            try
            {

                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                ed.WriteMessage("********* 3\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Bus, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Bus, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.ServerInsert 1: {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Bus_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Bus))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Bus, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in Bus");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Bus, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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
                ed.WriteMessage(string.Format(" ERROR EBus.Update: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_Delete", connection);
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
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBus.Deletet : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus)))
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
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EBus.ServerDeletet : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        public static EBus SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return bus;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                //bus.Code = Convert.ToInt16(reader["Code"].ToString());
                //bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Bus_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Bus_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];
        }

        public static EBus SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Bus_SelectByProductCode", connection);
            EBus bus = new EBus();
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("go to E_Bus_SelectByProductCode \n");
                if (reader.Read())
                {
                    bus.Code = Convert.ToInt16(reader["Code"].ToString());
                    //ed.WriteMessage("delete : " + reader["Code"].ToString() + "\n");
                    bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    bus.Size = reader["Size"].ToString();
                    //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    bus.Type = Convert.ToByte(reader["Type"].ToString());
                    bus.Comment = reader["Comment"].ToString();
                    bus.Name = reader["Name"].ToString();
                    bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record Found for your ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
                //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus.Type));

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EBus.SelectByProductCode", ex1.Message));
                connection.Close();
            }
            return bus;
        }

        //ASHKTORAB
        public static EBus ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt32(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            reader.Close();
            //connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return bus;
        }

        //MEDHAT //ShareOnServer
        public static EBus ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt32(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : bus\n");
            }
            reader.Close();
            return bus;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus);
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
                    ed.WriteMessage(string.Format("Error EBus.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EBus.Insert 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }


            ////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.InsertX: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

            //    connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Bus_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {

                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Bus, ServerConnection, ServerTransaction);
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
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Bus, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Bus_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Bus))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Bus, ServerTransaction , ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in Bus");
                    }
                }
                ed.WriteMessage("EBus.Operation passed \n");


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Bus, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            command.Transaction = _transaction;
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus);
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
                ed.WriteMessage(string.Format("Error EBus.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            //////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.InsertX: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

            //    //connection.Close();
            //    return false;
            //}
        }

        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Bus);

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
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                    ed.WriteMessage(string.Format("Error EBus.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EBus.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }


            //////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.UpdateX: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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
                ed.WriteMessage(string.Format("Error EBus.UpdateX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            ///////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.UpdateX: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Bus);
            if (ProdTbl.Rows.Count > 0)
            {
                //return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EBus.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EBus.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }


            //////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.DeletetX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}

        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackage.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus)))
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
                    ed.WriteMessage(string.Format("Error EBus.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EBus.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            //////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format(" ERROR EBus.DeletetX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}

        }

        //MOUSAVI //drawGroundPost //SentFromLocalToAccess //frmDrawBus
        public static EBus SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", bus.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Bus));
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
            command.Parameters.Add(new SqlParameter("iXCode", bus.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Bus));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeys.Add(Op);
                //nodeKeys.Add(reader["Count"].ToString());

            }

            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return bus;
        }

        public static EBus SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", bus.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Bus));
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
            command.Parameters.Add(new SqlParameter("iXCode", bus.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Bus));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            //connection.Close();
            return bus;
        }

        //MEDHAT //ShareOnServer
        public static EBus SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            return bus;
        }

        //ASHKTORAB
        public static EBus SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            //connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return bus;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Bus_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Bus_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];

        }
        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_SearchByName", connection);
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
        public static EBus CheckForExist(string _Size, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Bus_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iSize", _Size));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            connection.Close();
            return bus;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ACCESS~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Bus_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
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
                ed.WriteMessage(string.Format("Error EBUS.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Bus_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Bus);
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
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Bus, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }
        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Bus_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType (OldCode, (int)Atend.Control.Enum.ProductType.Bus);
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
                    ed.WriteMessage("Main Parent is Bus: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess (OldCode, (int)Atend.Control.Enum.ProductType.Bus, Code, _OldTransaction,_OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Bus failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EBUS.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport //frmDrawBus
        public static EBus AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return bus;
        }

        public static EBus AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            return bus;
        }

        public static EBus AccessSelectByCode(int Code , OleDbTransaction _transaction , OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            //connection.Close();

            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return bus;
        }


        public static EBus AccessSelectByCodeForConvertor(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Bus_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            //connection.Close();

            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return bus;
        }


        public static EBus AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Bus_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EBus bus = new EBus();
            if (reader.Read())
            {
                bus.Code = Convert.ToInt16(reader["Code"].ToString());
                bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                bus.Size = reader["Size"].ToString();
                //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                bus.Type = Convert.ToByte(reader["Type"].ToString());
                bus.Comment = reader["Comment"].ToString();
                bus.Name = reader["Name"].ToString();
                bus.XCode = new Guid(reader["XCode"].ToString());
                bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
            }
            else
            {
                bus.Code = -1;
            }
            reader.Close();
            connection.Close();

            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));
            return bus;
        }

        //MOUSAVI->SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EBus AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Bus_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EBus bus = new EBus();
        //    if (reader.Read())
        //    {
        //        bus.Code = Convert.ToInt16(reader["Code"].ToString());
        //        bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        bus.Size = reader["Size"].ToString();
        //        //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
        //        bus.Type = Convert.ToByte(reader["Type"].ToString());
        //        bus.Comment = reader["Comment"].ToString();
        //        bus.Name = reader["Name"].ToString();
        //        bus.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        bus.Code = -1;
        //    }
        //    reader.Close();
        //    //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));
        //    return bus;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Bus_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));


            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Bus_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];
        }

        public static EBus AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Bus_SelectByProductCode", connection);
            EBus bus = new EBus();
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("go to E_Bus_SelectByProductCode \n");
                if (reader.Read())
                {
                    bus.Code = Convert.ToInt16(reader["Code"].ToString());
                    //ed.WriteMessage("delete : " + reader["Code"].ToString() + "\n");
                    bus.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    bus.Size = reader["Size"].ToString();
                    //bus.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    bus.Type = Convert.ToByte(reader["Type"].ToString());
                    bus.Comment = reader["Comment"].ToString();
                    bus.Name = reader["Name"].ToString();
                    bus.Voltage = Convert.ToDouble(reader["Voltage"].ToString());
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record Found for your ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
                //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus.Type));

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EBus.SelectByProductCode", ex1.Message));
                connection.Close();
            }
            return bus;
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

        //        EBus bus = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
        //            //ed.WriteMessage("\n111\n");

        //            EBus Ap = Atend.Base.Equipment.EBus.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EBus.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    return false;
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            bus.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, bus.XCode);
        //            bus.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (bus.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, bus.Code, (int)Atend.Control.Enum.ProductType.Bus))
        //                {
        //                    if (!bus.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");


        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("\n114\n");


        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                return false;
        //            }


        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, bus.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Bus, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");
        //                return true;
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                return false;

        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            return false;

        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EAirPost.ShareOnServer {0}\n", ex1.Message));

        //        return false;

        //    }

        //    return true;
        //}

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

        //        EBus bus = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
        //            //ed.WriteMessage("\n111\n");

        //            EBus Ap = Atend.Base.Equipment.EBus.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EBus.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            bus.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, bus.XCode);
        //            bus.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (bus.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, bus.Code, (int)Atend.Control.Enum.ProductType.Bus))
        //                {
        //                    if (!bus.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, bus.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Bus, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
        //            //ed.WriteMessage("\n111\n");

        //            EBus Ap = Atend.Base.Equipment.EBus.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EBus.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EBus.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Bus, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EBus.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
