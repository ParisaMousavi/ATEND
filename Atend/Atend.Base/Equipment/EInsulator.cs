using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EInsulator
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

        private byte materialCode;
        public byte MaterialCode
        {
            get { return materialCode; }
            set { materialCode = value; }
        }

        private double volt;
        public double Volt
        {
            get { return volt; }
            set { volt = value; }
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

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        //private double lenghtInsulatorChain;
        //public double LenghtInsulatorChain
        //{
        //    get { return lenghtInsulatorChain; }
        //    set { lenghtInsulatorChain = value; }
        //}

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

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeKeysX = new ArrayList();
        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_Insert", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            // sqlCommand.Parameters.Add(new SqlParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format(" ERROR EInsulator.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_Insert", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            // sqlCommand.Parameters.Add(new SqlParameter("iLenghtInsulatorChain", LenghtInsulatorChain));

            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();


                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(transaction, Connection) && canCommitTransaction)
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
                    //*************


                    //Package
                    //if (canCommitTransaction)
                    //{
                    //    //containerPackage.ContainerCode = 0;// containerCode;
                    //    containerPackage.XCode = XCode;

                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}

                    ////ed.WriteMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
                    //Counter = 0;
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
                    //}

                    ////****************

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
                    //ed.WriteMessage("Error In TransactionAAA:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format(" ERROR EInsulator.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Insulator_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Insulator, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in EINSULATOR");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Insulator, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Insulator_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Insulator))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Insulator, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in EINSULATOR");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Insulator");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Insulator, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator))) & canCommitTransaction)
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
                ed.WriteMessage(string.Format(" ERROR EInsulator.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.Delete(transaction, Connection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
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
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransActionX:INSULATOR.SERVERDEWLETE{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EINSULATOR.SERVERDEWLETE: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EInsulator SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
            }
            reader.Close();



            Connection.Close();
            return insulator;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            //EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                //insulator.XCode = Convert.ToInt32(reader["Code"].ToString());
                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                //insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                Type = Convert.ToByte(reader["Type"].ToString());
                //LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
            }
            reader.Close();


            Connection.Close();
        }

        public static EInsulator SelectByProductCode(int ProductCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulator_SelectByProductCode", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EInsulator insulator = new EInsulator();
            try
            {
                Connection.Open();
                SqlDataReader reader = adapter.SelectCommand.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                    insulator.Comment = reader["Comment"].ToString();
                    insulator.Name = reader["Name"].ToString();
                    insulator.Type = Convert.ToByte(reader["Type"].ToString());

                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EInsulator.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return insulator;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulator_Select", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulator_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        //ASHKTORAB
        public static EInsulator ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            //ed.WriteMessage(" Parameter Added \n");
            //Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");


            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());

                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            //ed.WriteMessage("insulator.Xcode:" + insulator.XCode + "Type:" + Atend.Control.Enum.ProductType.Insulator + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************

            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************


            //Connection.Close();
            return insulator;
        }

        //MEDHAT //ShareOnServer
        public static EInsulator ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Insulator_Select", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EInsulator insulator = new EInsulator();

            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.Name = reader["Name"].ToString();
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                insulator.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : insulator\n");
            }
            reader.Close();
            return insulator;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            //sqlCommand.Parameters.Add(new SqlParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    containerCode = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    //ed.WriteMessage("Xcode="+XCode.ToString()+"\n");

                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
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
                    //*************


                    //Package
                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;// containerCode;
                        containerPackage.XCode = XCode;

                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                        if (containerPackage.InsertX(transaction, Connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }

                    //ed.WriteMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
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
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In TransactionAAA:{0}", ex1.Message);
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EInsulator.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            //sqlCommand.Parameters.Add(new SqlParameter("iLenghtInsulatorChain", LenghtInsulatorChain));

            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));



            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    containerCode = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();


                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
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
                    //*************


                    //Package
                    //if (canCommitTransaction)
                    //{
                    //    //containerPackage.ContainerCode = 0;// containerCode;
                    //    containerPackage.XCode = XCode;

                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}

                    ////ed.WriteMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
                    //Counter = 0;
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.InsertX(transaction, Connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
                    //}

                    ////****************

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
                    ed.WriteMessage("Error In TransactionAAA:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EInsulator.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Insulator_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Insulator, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in EINSULATOR");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Insulator, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Insulator_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Insulator))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Insulator, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in EINSULATOR");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Insulator");
                    }
                }
                ed.WriteMessage("EInsulator.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Insulator, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            //sqlCommand.Parameters.Add(new SqlParameter("ilenghtInsulatorChain", LenghtInsulatorChain));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    //Operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.ProductCode = code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                            if (_EOperation.Insert(transaction, Connection) && canCommitTransaction)
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
                    //****

                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                ed.WriteMessage(string.Format(" ERROR EInsulator.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //sqlCommand.Parameters.Add(new SqlParameter("ilenghtInsulatorChain", LenghtInsulatorChain));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    //Operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;//code;
                            _EOperation.XCode = XCode;

                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
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
                    //****

                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                ed.WriteMessage(string.Format(" ERROR EInsulator.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVolt", Volt));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //sqlCommand.Parameters.Add(new SqlParameter("ilenghtInsulatorChain", LenghtInsulatorChain));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    ////Operation
                    //if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        //_EOperation.ProductCode = 0;//code;
                    //        _EOperation.XCode = XCode;

                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator);
                    //        if (_EOperation.InsertX(transaction, Connection) && canCommitTransaction)
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
                    ////****

                    ////Package
                    ////ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    ////ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    //Counter = 0;
                    //if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    //Connection.Close();
                    //transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EInsulator.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Insulator);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator))) & canCommitTransaction)
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
                    ed.WriteMessage("Error In TransActionX:{0}\n", ex1.Message);
                    transaction.Rollback();
                    Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Insulator_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
                    //operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator))) & canCommitTransaction)
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
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
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransActionX:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EInsulator.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EInsulator SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());

                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //ed.WriteMessage("GoToSelect Equipment\n");
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            //ed.WriteMessage("insulator.Xcode:" + insulator.XCode + "Type:" + Atend.Control.Enum.ProductType.Insulator + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:1" + "\n");
            }

            reader.Close();
            //**************
            //ed.WriteMessage("Go To Select Operation\n");
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeysX.Add(Op);
            }

            reader.Close();
            //************


            Connection.Close();
            //ed.WriteMessage("Finish\n");
            return insulator;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EInsulator SelectByXCodeForDesign(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());

                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            Connection.Close();
            //ed.WriteMessage("Finish\n");
            return insulator;
        }

        //ASHKTORAB //ShareOnServer
        public static EInsulator SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage(" Parameter Added \n");
            EInsulator insulator = new EInsulator();

            try
            {
                command.Transaction = LocalTransaction;

                //ed.WriteMessage("Execute \n");
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("Executed \n");
                //ed.WriteMessage("Insulator crreated \n");
                if (reader.Read())
                {
                    //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                    //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.XCode = new Guid(reader["XCode"].ToString());

                    //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                    insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                    insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                    insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                    //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                    insulator.Comment = reader["Comment"].ToString();
                    //ed.WriteMessage(reader["Name"].ToString() + "\n");
                    insulator.Name = reader["Name"].ToString();
                    //ed.WriteMessage(reader["Type"].ToString() + "\n");
                    insulator.Type = Convert.ToByte(reader["Type"].ToString());
                    //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                    insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    insulator.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorchain.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            //ed.WriteMessage("Finish\n");
            return insulator;
        }

        public static EInsulator SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            //ed.WriteMessage(" Parameter Added \n");
            //Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");


            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());

                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            //ed.WriteMessage("insulator.Xcode:" + insulator.XCode + "Type:" + Atend.Control.Enum.ProductType.Insulator + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackageX.Add(reader["XCode"].ToString());
                nodeTypeEPackageX.Add(reader["TableType"].ToString());
                nodeCountEPackageX.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************

            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", insulator.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************


            //Connection.Close();
            return insulator;
        }

        //ASHKTORAB
        public static EInsulator SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            ////ed.WriteMessage("Add Code Parameter " + XCode.ToString() + "\n");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _transaction;

            //ed.WriteMessage(" Parameter Added \n");
            //Connection.Open();
            //ed.WriteMessage("Execute \n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");


            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                //ed.WriteMessage("XCode : " + reader["XCode"].ToString() + "\n");

                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());

                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            //Connection.Close();
            return insulator;
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulator_SelectAll", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Insulator_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Insulator_SearchByName", connection);
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
        public static EInsulator CheckForExist(byte _MaterialCode, double _Volt, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Insulator_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iMaterialCode", _MaterialCode));
            command.Parameters.Add(new SqlParameter("iVolt", _Volt));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EInsulator insulator = new EInsulator();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.Name = reader["Name"].ToString();
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                insulator.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();
            connection.Close();
            return insulator;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Insulator_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }

            catch (System.Exception ex)
            {
                //ed.WriteMessage(string.Format("Error ETransferInsulator.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Insulator_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Insulator);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Insulator, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Insulator_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iVolt", Volt));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            //insertCommand.Parameters.Add(new OleDbParameter("iLenghtInsulatorChain", LenghtInsulatorChain));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            int oldcode = Code;
            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldcode, (int)Atend.Control.Enum.ProductType.Insulator);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldcode, (int)Atend.Control.Enum.ProductType.Insulator, Code, _oldtransaction, _oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAceessToAccess Insulator failed");
                    }
                }



                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ETransferInsulator.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EInsulator AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            OleDbDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", insulator.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            //ed.WriteMessage("insulator.code:" + insulator.code + "Type:" + Atend.Control.Enum.ProductType.Insulator + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************

            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", insulator.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************


            Connection.Close();
            return insulator;
        }

        public static EInsulator AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EInsulator insulator = new EInsulator();
            if (reader.Read())
            {
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                insulator.Comment = reader["Comment"].ToString();
                insulator.Name = reader["Name"].ToString();
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", insulator.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            //ed.WriteMessage("insulator.code:" + insulator.code + "Type:" + Atend.Control.Enum.ProductType.Insulator + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************

            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", insulator.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Insulator));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************
            return insulator;
        }


        public static EInsulator AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Insulator_Select", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //ed.WriteMessage(" Parameter Added \n");
            command.Transaction = _transaction;
            //ed.WriteMessage("Execute \n");
            OleDbDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
            }
            else
            {
                insulator.Code = -1;
            }
            reader.Close();

            return insulator;
        }

        public static EInsulator AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Insulator_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            //ed.WriteMessage(" Parameter Added \n");
            Connection.Open();
            //ed.WriteMessage("Execute \n");
            OleDbDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("Executed \n");
            EInsulator insulator = new EInsulator();
            //ed.WriteMessage("Insulator crreated \n");
            if (reader.Read())
            {
                //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
                insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
                insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
                insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage(reader["Volt"].ToString() + "\n");
                insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                //ed.WriteMessage(reader["Comment"].ToString() + "\n");
                insulator.Comment = reader["Comment"].ToString();
                //ed.WriteMessage(reader["Name"].ToString() + "\n");
                insulator.Name = reader["Name"].ToString();
                //ed.WriteMessage(reader["Type"].ToString() + "\n");
                insulator.Type = Convert.ToByte(reader["Type"].ToString());
                insulator.XCode = new Guid(reader["XCode"].ToString());
                //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
            }
            reader.Close();
            Connection.Close();
            return insulator;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EInsulator AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection Connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Insulator_SelectByXCode", Connection);
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    //ed.WriteMessage("Add Code Parameter " + Code.ToString() + "\n");
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    //ed.WriteMessage(" Parameter Added \n");
        //    command.Transaction = _transaction;
        //    //ed.WriteMessage("Execute \n");
        //    OleDbDataReader reader = command.ExecuteReader();
        //    //ed.WriteMessage("Executed \n");
        //    EInsulator insulator = new EInsulator();
        //    //ed.WriteMessage("Insulator crreated \n");
        //    if (reader.Read())
        //    {
        //        //ed.WriteMessage("Code : " + reader["Code"].ToString() + "\n");
        //        insulator.Code = Convert.ToInt32(reader["Code"].ToString());
        //        //ed.WriteMessage("Material Code : " + reader["MaterialCode"].ToString() + "\n");
        //        insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
        //        //ed.WriteMessage(reader["ProductCode"].ToString() + "\n");
        //        insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        //ed.WriteMessage(reader["Volt"].ToString() + "\n");
        //        insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
        //        //ed.WriteMessage(reader["Comment"].ToString() + "\n");
        //        insulator.Comment = reader["Comment"].ToString();
        //        //ed.WriteMessage(reader["Name"].ToString() + "\n");
        //        insulator.Name = reader["Name"].ToString();
        //        //ed.WriteMessage(reader["Type"].ToString() + "\n");
        //        insulator.Type = Convert.ToByte(reader["Type"].ToString());
        //        insulator.XCode = new Guid(reader["XCode"].ToString());
        //        //insulator.LenghtInsulatorChain = Convert.ToDouble(reader["LenghtInsulatorChain"].ToString());
        //    }
        //    else
        //    {
        //        insulator.Code = -1;
        //    }
        //    reader.Close();
        //    return insulator;
        //}

        public static EInsulator AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Insulator_SelectByProductCode", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EInsulator insulator = new EInsulator();
            try
            {
                Connection.Open();
                OleDbDataReader reader = adapter.SelectCommand.ExecuteReader();
                if (reader.Read())
                {
                    insulator.Code = Convert.ToInt32(reader["Code"].ToString());
                    insulator.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    insulator.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    insulator.Volt = Convert.ToDouble(reader["Volt"].ToString());
                    insulator.Comment = reader["Comment"].ToString();
                    insulator.Name = reader["Name"].ToString();
                    insulator.Type = Convert.ToByte(reader["Type"].ToString());

                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format("Error EInsulator.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return insulator;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Insulator_Select", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Insulator_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));

            Connection.Open();
            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            Connection.Close();
            return dsInsulator.Tables[0];
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

        //        EInsulator _EInsulator = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
        //            //ed.WriteMessage("\n111\n");

        //            EInsulator Ap = Atend.Base.Equipment.EInsulator.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EInsulator.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EInsulator.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EInsulator.XCode);
        //            _EInsulator.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EInsulator.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EInsulator.Code, (int)Atend.Control.Enum.ProductType.Insulator))
        //                {
        //                    if (!_EInsulator.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EInsulator.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Insulator, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //            //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        //ed.WriteMessage(string.Format(" ERROR EInsulator.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));
        //            //ed.WriteMessage("\n111\n");

        //            EInsulator Ap = Atend.Base.Equipment.EInsulator.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EInsulator.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EInsulator.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Insulator, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EInsulator.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
