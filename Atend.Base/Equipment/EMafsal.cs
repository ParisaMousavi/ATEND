using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.OleDb;
using System.Collections;

namespace Atend.Base.Equipment
{

    public class EMafsal
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

        private double crossSectionArea;
        public double CrossSectionArea
        {
            get { return crossSectionArea; }
            set { crossSectionArea = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        private int voltage;
        public int Voltage
        {
            get { return voltage; }
            set { voltage = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));

            try
            {
                connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.Insert : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //Code = 0;
            //sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Transaction = _transaction;

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal);
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.Insert : {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Mafsal, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in MAFSAL");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Mafsal, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Mafsal failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.ServerInsert : {0} \n", ex1.Message));

                return false;
            }

        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Update", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            //sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Mafsal))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Mafsal, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in MAFSAL");
                        }
                    }
                    else
                    {
                        throw new System.Exception("deleted Operation Failed in Mafsal");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Mafsal, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Mafsal failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.ServerUpdate : {0} \n", ex1.Message));

                return false;
            }

        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Update", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.Update : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Delete", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.Delete : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Delete", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;

            try
            {
                //connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal)))
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.DeleteX : {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        public static EMafsal SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);

            }
            connection.Close();
            reader.Close();
            return mafsal;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                //mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                //mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Voltage = Convert.ToInt32(reader["Voltage"]);

            }
            connection.Close();
            reader.Close();

        }

        public static EMafsal SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_SelectByProductCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EMafsal mafsal = new EMafsal();

            try
            {
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                    mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                    mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                    mafsal.Comment = reader["Comment"].ToString();
                    mafsal.Name = reader["Name"].ToString();
                    mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                connection.Close();
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return mafsal;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Mafsal_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        public static DataTable DrawSearch(int Type, int Voltage, double CrossSectionArea)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Mafsal_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Mafsal_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        //ASHKTORAB
        public static EMafsal ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            //connection.Close();
            reader.Close();
            return mafsal;
        }

        //MEDHAT //ShareOnServer
        public static EMafsal ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            sqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();

            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                mafsal.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : mafsal\n");
            }
            reader.Close();
            return mafsal;
        }

        //~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal);
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
                    ed.WriteMessage(string.Format("Error EMafsal.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EMafsal.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            //////////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.Insert : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_InsertX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //Code = 0;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal);
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
                ed.WriteMessage(string.Format("Error EMafsal.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            ////////////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.Insert : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_InsertX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Mafsal, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in MAFSAL");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Mafsal, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Mafsal failed");
                    }
                }
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.LocalInsertX : {0} \n", ex1.Message));

                return false;
            }

        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_UpdateX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Mafsal))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Mafsal, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in MAFSAL");
                        }
                    }
                    else
                    {
                        throw new System.Exception("deleted Operation Failed in Mafsal");
                    }
                }
                ed.WriteMessage("EMafsal.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Mafsal, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Mafsal failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EMafsal.LocalUpdate : {0} \n", ex1.Message));

                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_UpdateX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                _transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal));
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
                    ed.WriteMessage(string.Format("Error EMafsal.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EMafsal.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }



            ////////////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.UpdateX : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_UpdateX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            /////////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.UpdateX : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.Mafsal);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_DeleteX", Connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EMafsal.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMafsal.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }


            /////////////////////////////
            //try
            //{
            //    connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.DeleteX : {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_DeleteX", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            sqlCommand.Transaction = _transaction;
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal)))
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
                    ed.WriteMessage(string.Format("Error EMafsal.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMafsal.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            ////////////////////////
            //try
            //{
            //    //connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EMafsal.DeleteX : {0} \n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}

        }

        //MOUSAVI //SentFromLocalToAccess
        public static EMafsal SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                mafsal.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", mafsal.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Mafsal));
            reader = sqlCommand.ExecuteReader();
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
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", mafsal.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Mafsal));
            reader = sqlCommand.ExecuteReader();
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
            return mafsal;
        }

        //ASHKTORAB //ShareOnServer
        public static EMafsal SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            EMafsal mafsal = new EMafsal();

            try
            {
                sqlCommand.Transaction = LocalTransaction;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                    mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                    mafsal.XCode = new Guid(reader["XCode"].ToString());
                    mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                    mafsal.Comment = reader["Comment"].ToString();
                    mafsal.Name = reader["Name"].ToString();
                    mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                    mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    mafsal.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EMafsal.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return mafsal;
        }



        //ASHKTORAB
        public static EMafsal SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", mafsal.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Mafsal));
            reader = sqlCommand.ExecuteReader();
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
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", mafsal.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Mafsal));
            reader = sqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }
            reader.Close();
            //connection.Close();
            return mafsal;
        }

        //ASHKTORAB
        public static EMafsal SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
                mafsal.Code = -1;

            //connection.Close();
            reader.Close();
            return mafsal;
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Mafsal_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Mafsal_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Mafsal_SearchByName", connection);
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
        public static EMafsal CheckForExist(double _CrossSectionArea, byte _Type, int _Voltage)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Mafsal_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", _CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iVoltage", _Voltage));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.XCode = new Guid(reader["XCode"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                mafsal.Code = -1;
            }
            reader.Close();
            connection.Close();
            return mafsal;
        }

        //~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));

            try
            {
                connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.AccessInsert : {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Mafsal);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Mafsal, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Mafsal failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.AccessInsert : {0} \n", ex1.Message));

                return false;
            }

        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _NewConnection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Insert", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _NewTransaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            int oldCode = Code;
            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Mafsal);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.Mafsal, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Mafsal failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.AccessInsert : {0} \n", ex1.Message));

                return false;
            }

        }

        //StatusReport
        public static EMafsal AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);

            }
            else
            {
                mafsal.Code = -1;
            }
            connection.Close();
            reader.Close();
            return mafsal;
        }

        public static EMafsal AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);

            }
            else
            {
                mafsal.Code = -1;
            }
            reader.Close();
            return mafsal;
        }

        public static EMafsal AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_Select", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);

            }
            else
            {
                mafsal.Code = -1;
            }
            //connection.Close();
            reader.Close();
            return mafsal;
        }

        public static EMafsal AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_SelectByXCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EMafsal mafsal = new EMafsal();
            if (reader.Read())
            {
                mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                mafsal.Comment = reader["Comment"].ToString();
                mafsal.Name = reader["Name"].ToString();
                mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                mafsal.XCode = new Guid(reader["XCode"].ToString());

            }
            connection.Close();
            reader.Close();
            return mafsal;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EMafsal AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_SelectByXCode", connection);
        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    sqlCommand.Transaction = _transaction;
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    EMafsal mafsal = new EMafsal();
        //    if (reader.Read())
        //    {
        //        mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
        //        mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
        //        mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        mafsal.Type = Convert.ToByte(reader["Type"].ToString());
        //        mafsal.Comment = reader["Comment"].ToString();
        //        mafsal.Name = reader["Name"].ToString();
        //        mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
        //        mafsal.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        mafsal.Code = -1;
        //    }

        //    reader.Close();
        //    return mafsal;
        //}

        public static EMafsal AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Mafsal_SelectByProductCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EMafsal mafsal = new EMafsal();

            try
            {
                connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    mafsal.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                    mafsal.Code = Convert.ToInt32(reader["Code"].ToString());
                    mafsal.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    mafsal.Type = Convert.ToByte(reader["Type"].ToString());
                    mafsal.Comment = reader["Comment"].ToString();
                    mafsal.Name = reader["Name"].ToString();
                    mafsal.Voltage = Convert.ToInt32(reader["Voltage"]);
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                connection.Close();
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EMafsal.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return mafsal;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Mafsal_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Mafsal_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsMafsal = new DataSet();
            adapter.Fill(dsMafsal);
            return dsMafsal.Tables[0];

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

        //        EMafsal _EMafsal = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal));
        //            //ed.WriteMessage("\n111\n");

        //            EMafsal Ap = Atend.Base.Equipment.EMafsal.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EMafsal.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EMafsal.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EMafsal.XCode);
        //            _EMafsal.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EMafsal.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EMafsal.Code, (int)Atend.Control.Enum.ProductType.Mafsal))
        //                {
        //                    if (!_EMafsal.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EMafsal.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Mafsal, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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


        //            //ed.WriteMessage("\n112\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EMafsal.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal));
        //            //ed.WriteMessage("\n111\n");

        //            EMafsal Ap = Atend.Base.Equipment.EMafsal.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EMafsal.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EMafsal.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Mafsal));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Mafsal, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EMafsal.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
