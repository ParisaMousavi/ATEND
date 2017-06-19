using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace Atend.Base.Equipment
{
    public class EKablsho
    {
        private Guid xCode;
        public Guid XCode
        {
            get { return xCode; }
            set { xCode = value; }
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

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        private byte material;
        public byte Material
        {
            get { return material; }
            set { material = value; }
        }

        private double size;
        public double Size
        {
            get { return size; }
            set { size = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
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
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Insert", connection);

            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("\nProductCode = {0}\n", ProductCode.ToString());
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            //ed.WriteMessage("\nSize = {0}\n", Size.ToString());
            command.Parameters.Add(new SqlParameter("iSize", Size));
            //ed.WriteMessage("\nMaterial = {0}\n", Material.ToString());
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            //ed.WriteMessage("\nType = {0}\n", Type.ToString());
            command.Parameters.Add(new SqlParameter("iType", Type));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new SqlParameter("iName", Name));
            //ed.WriteMessage("\nXCode = {0}\n", XCode.ToString());
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("\nIsDefualt = {0}\n", IsDefault.ToString());
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EKablsho.Insert: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Transaction = _transaction;

            try
            {
                //connection.Open();
                //ed.WriteMessage("\nBefor Execute\n");
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("\nAfter Execute\n");
                //connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    //ed.WriteMessage("\nCounter = {0}\n", Counter.ToString());
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        // ed.WriteMessage("Error For Insert \n");
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
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EKablsho.InsertX: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

                //connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {



            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_kablsho_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCODE:{0}\n", XCode);
            //ed.WriteMessage("XCODE:{0}\n", ProductCode);
            //ed.WriteMessage("XCODE:{0}\n", Type);
            //ed.WriteMessage("XCODE:{0}\n", Material);
            //ed.WriteMessage("XCODE:{0}\n", Size);
            //ed.WriteMessage("XCODE:{0}\n", Comment);
            //ed.WriteMessage("XCODE:{0}\n", Name);

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.KablSho, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in KAblsho");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.KablSho, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }


        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {



            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_kablsho_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCODE:{0}\n", XCode);
            //ed.WriteMessage("XCODE:{0}\n", ProductCode);
            //ed.WriteMessage("XCODE:{0}\n", Type);
            //ed.WriteMessage("XCODE:{0}\n", Material);
            //ed.WriteMessage("XCODE:{0}\n", Size);
            //ed.WriteMessage("XCODE:{0}\n", Comment);
            //ed.WriteMessage("XCODE:{0}\n", Name);

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.KablSho))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.KablSho, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in KAblsho");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Kablsho");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.KablSho, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
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
                ed.WriteMessage(string.Format(" ERROR EKablsho.Update: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR EKablsho.Deletet : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho)))
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
                ed.WriteMessage(string.Format(" ERROR EKablsho.ServerDeletet : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        public static EKablsho SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EKablsho Kablsho = new EKablsho();
            if (reader.Read())
            {
                Kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                Kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                Kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                Kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                Kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Kablsho.Comment = reader["Comment"].ToString();
                Kablsho.Name = reader["Name"].ToString();
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            return Kablsho;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //EKablsho Kablsho = new EKablsho();
            if (reader.Read())
            {
                //Kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                //Kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Size = Convert.ToDouble(reader["Size"].ToString());
                Material = Convert.ToByte(reader["Material"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            //return Kablsho;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Kablsho_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            DataSet dsKablsho = new DataSet();
            adapter.Fill(dsKablsho);
            return dsKablsho.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Kablsho_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKablsho = new DataSet();
            adapter.Fill(dsKablsho);
            return dsKablsho.Tables[0];
        }

        public static EKablsho SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Kablsho_SelectByProductCode", connection);
            EKablsho kablsho = new EKablsho();
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("go to E_Bus_SelectByProductCode \n");
                if (reader.Read())
                {
                    kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                    // ed.WriteMessage("delete : " + reader["Code"].ToString() + "\n");
                    kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                    kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                    kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                    kablsho.Comment = reader["Comment"].ToString();
                    kablsho.Name = reader["Name"].ToString();
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record Found for your ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
                //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", kablsho.ProductCode, kablsho.Size, kablsho.Type));

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EKablsho.SelectByProductCode", ex1.Message));
                connection.Close();
            }
            return kablsho;
        }

        //ASHKTORAB
        public static EKablsho ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt32(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.XCode = Guid.Empty;
            }
            reader.Close();
            //connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            return kablsho;
        }

        //MEDHAT //ShareOnServer
        public static EKablsho ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();

            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt32(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : kablsho\n");
            }

            reader.Close();
            return kablsho;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Kablsho_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iSize", Size));
            Command.Parameters.Add(new SqlParameter("iMaterial", Material));
            Command.Parameters.Add(new SqlParameter("iType", Type));
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho);
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
                    ed.WriteMessage(string.Format("Error EKablSho.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EKablSho.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            ///////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EKablsho.InsertX: {0}  \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            command.Transaction = _transaction;
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho);
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
                ed.WriteMessage(string.Format("Error EKablSho.Insert : {0} \n", ex1.Message));
                //Connection.Close();
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
            //    ed.WriteMessage(string.Format(" ERROR EKablsho.InsertX: {0} Values: {1} , {2} , {3} , {4} \n", ex1.Message, ProductCode, Size, Type));

            //    //connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_kablsho_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.KablSho, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in KAblsho");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.KablSho, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {



            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_kablsho_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCODE:{0}\n", XCode);
            //ed.WriteMessage("XCODE:{0}\n", ProductCode);
            //ed.WriteMessage("XCODE:{0}\n", Type);
            //ed.WriteMessage("XCODE:{0}\n", Material);
            //ed.WriteMessage("XCODE:{0}\n", Size);
            //ed.WriteMessage("XCODE:{0}\n", Comment);
            //ed.WriteMessage("XCODE:{0}\n", Name);

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iSize", Size));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.KablSho))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.KablSho, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in KAblsho");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Kablsho");
                    }
                }
                ed.WriteMessage("EKablsho.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.KablSho, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Kablsho_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iSize", Size));
            Command.Parameters.Add(new SqlParameter("iMaterial", Material));
            Command.Parameters.Add(new SqlParameter("iType", Type));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho));
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
                    ed.WriteMessage(string.Format("Error EKablSho.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EKablSho.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }




            ///////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EKhablsho.UpdateX: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EKablSho.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            /////////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EKablsho.UpdateX: {0} Values: {1} , {2} , {3}  \n", ex1.Message, ProductCode, Size, Type));

            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.KablSho);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Kablsho_DeleteX", Connection);
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
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EKablSho.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKablSho.Delete : {0} \n", ex1.Message));
                Connection.Close();
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
            //    ed.WriteMessage(string.Format(" ERROR EKablsho.DeletetX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}

        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Kablsho_DeleteX", connection);
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
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho)))
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
                    ed.WriteMessage(string.Format("Error EKablsho.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKablsho.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }




            ///////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EKablsho.DeletetX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}

        }

        //MOUSAVI //SentFromLocalToAccess
        public static EKablsho SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Kablsho_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", kablsho.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.KablSho));
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
            command.Parameters.Add(new SqlParameter("iXCode", kablsho.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.KablSho));
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
            connection.Close();
            return kablsho;
        }

        //ASHKTORAB //ShareOnServer
        public static EKablsho SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Kablsho_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            EKablsho kablsho = new EKablsho();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                    kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                    kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                    kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                    kablsho.Comment = reader["Comment"].ToString();
                    kablsho.Name = reader["Name"].ToString();
                    kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    kablsho.XCode = new Guid(reader["XCode"].ToString());
                }
                else
                {
                    kablsho.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EKablsho.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return kablsho;
        }

        public static EKablsho SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_kablsho_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", kablsho.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.KablSho));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    nodeKeys.Add(reader["ProductID"].ToString());

            //}
            //reader.Close();
            //connection.Close();
            return kablsho;
        }

        public static EKablsho SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            //connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            return kablsho;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_kablsho_Search", connection);
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
            SqlDataAdapter adapter = new SqlDataAdapter("E_kablsho_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsBus = new DataSet();
            adapter.Fill(dsBus);
            return dsBus.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Kablsho_SearchByName", connection);
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
        public static EKablsho CheckForExist(double _Size, byte _Type, byte _Material)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Kablsho_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iSize", _Size));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iMaterial", _Material));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            connection.Close();
            return kablsho;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ACCESS~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_kablsho_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                con.Open();
                //}
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {



            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_kablsho_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCODE:{0}\n", XCode);
            //ed.WriteMessage("XCODE:{0}\n", ProductCode);
            //ed.WriteMessage("XCODE:{0}\n", Type);
            //ed.WriteMessage("XCODE:{0}\n", Material);
            //ed.WriteMessage("XCODE:{0}\n", Size);
            //ed.WriteMessage("XCODE:{0}\n", Comment);
            //ed.WriteMessage("XCODE:{0}\n", Name);

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.KablSho);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.KablSho, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _oldconnection,OleDbTransaction _newTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {



            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_kablsho_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _newTransaction;

            //ed.WriteMessage("XCODE:{0}\n", XCode);
            //ed.WriteMessage("XCODE:{0}\n", ProductCode);
            //ed.WriteMessage("XCODE:{0}\n", Type);
            //ed.WriteMessage("XCODE:{0}\n", Material);
            //ed.WriteMessage("XCODE:{0}\n", Size);
            //ed.WriteMessage("XCODE:{0}\n", Comment);
            //ed.WriteMessage("XCODE:{0}\n", Name);

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new OleDbParameter("iSize", Size));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));

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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.KablSho);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_newTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.KablSho, Code, _oldtransaction, _oldconnection,_newTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport //drawConductor
        public static EKablsho AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return kablsho;
        }

        public static EKablsho AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            return kablsho;
        }
        
        public static EKablsho AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_kablsho_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            //connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return kablsho;
        }

        public static EKablsho AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_kablsho_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EKablsho kablsho = new EKablsho();
            if (reader.Read())
            {
                kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
                kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                kablsho.Comment = reader["Comment"].ToString();
                kablsho.Name = reader["Name"].ToString();
                kablsho.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                kablsho.Code = -1;
            }
            reader.Close();
            connection.Close();

            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));


            return kablsho;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EKablsho AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_kablsho_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EKablsho kablsho = new EKablsho();
        //    if (reader.Read())
        //    {
        //        kablsho.Code = Convert.ToInt16(reader["Code"].ToString());
        //        kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
        //        kablsho.Material = Convert.ToByte(reader["Material"].ToString());
        //        kablsho.Type = Convert.ToByte(reader["Type"].ToString());
        //        kablsho.Comment = reader["Comment"].ToString();
        //        kablsho.Name = reader["Name"].ToString();
        //        kablsho.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        kablsho.Code = -1;
        //    }
        //    reader.Close();
        //    return kablsho;
        //}

        public static DataTable AccessSelectAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Start Selectall\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_kablsho_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));


            DataSet dsKablsho = new DataSet();
            adapter.Fill(dsKablsho);
            return dsKablsho.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_kablsho_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsKablsho = new DataSet();
            adapter.Fill(dsKablsho);
            return dsKablsho.Tables[0];
        }

        public static EKablsho AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_kablsho_SelectByProductCode", connection);
            EKablsho kablsho = new EKablsho();
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("go to E_kablsho_SelectByProductCode \n");
                if (reader.Read())
                {
                    kablsho.Code = Convert.ToInt16(reader["Code"].ToString());

                    kablsho.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    kablsho.Size = Convert.ToDouble(reader["Size"].ToString());
                    kablsho.Material = Convert.ToByte(reader["Material"].ToString());
                    kablsho.Type = Convert.ToByte(reader["Type"].ToString());
                    kablsho.Comment = reader["Comment"].ToString();
                    kablsho.Name = reader["Name"].ToString();
                }
                else
                {
                    ed.WriteMessage(string.Format("No Record Found for your ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Ekablsho.SelectByProductCode", ex1.Message));
                connection.Close();
            }
            return kablsho;
        }

        //frmDrawKablsho
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            DataTable AccTbl = AccessSelectAll();
            //ed.WriteMessage("AccessTbl.rows.count={0}\n", AccTbl.Rows.Count);
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
        //        EKablsho Kabl = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho));
        //            //ed.WriteMessage("\n111\n");

        //            EKablsho Ap = Atend.Base.Equipment.EKablsho.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            //ed.WriteMessage("\n222\n");
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                //ed.WriteMessage("\n333\n");

        //                if (!Atend.Base.Equipment.EKablsho.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            //ed.WriteMessage("\n444\n");
        //            DataTable OperationTbl = new DataTable();
        //            Kabl.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, Kabl.XCode);
        //            Kabl.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);
        //            ////ed.WriteMessage("\n555 Kablsho.OpList.Count = {0}\n", Kabl.OperationList.Count.ToString());
        //            if (Kabl.Insert(Servertransaction, Serverconnection))
        //            {
        //                //ed.WriteMessage("\n666\n");
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, Kabl.Code, (int)Atend.Control.Enum.ProductType.KablSho))
        //                {
        //                    //ed.WriteMessage("\n777\n");

        //                    if (Kabl.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n888\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();
        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
        //                        //return true;
        //                    }
        //                    else
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
        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, Kabl.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.KablSho, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EKablsho.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho));
        //            //ed.WriteMessage("\n111\n");

        //            EKablsho Ap = Atend.Base.Equipment.EKablsho.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EKablsho.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EKablsho.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.KablSho));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.KablSho, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EKablsho.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
