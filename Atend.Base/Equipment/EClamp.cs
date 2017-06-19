using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EClamp
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

        private int voltagelevel;
        public int VoltageLevel
        {
            get { return voltagelevel; }
            set { voltagelevel = value; }
        }

        private byte kind;
        public byte Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
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

        private double distanceSupport;
        public double DistanceSupport
        {
            get { return distanceSupport; }
            set { distanceSupport = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EClamp.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
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


                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp);
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
                ed.WriteMessage(string.Format(" ERROR EClamp.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
            return true;
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Clamp_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Kalamp, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Kalamp, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.AccessInsert11 : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Clamp_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Kalamp))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Kalamp, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in Clamp");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Kalamp, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));

            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EClamp.Update: {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EClamp.Delete: {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;

            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();
                //Connection.Close();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)))
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
                ed.WriteMessage(string.Format(" ERROR EClamp.Delete: {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }
        }

        public static EClamp SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            reader.Close();
            Connection.Close();
            return clamp;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //EClamp clamp = new EClamp();
            if (reader.Read())
            {
                //clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                //clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Kind = Convert.ToByte(reader["Kind"].ToString());
                VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                //IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                //clamp.XCode = new Guid(reader["XCode"].ToString());
                DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            reader.Close();
            Connection.Close();
            //return clamp;
        }

        //ASHKTORAB
        public static EClamp ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Transaction = _transaction;

            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            reader.Close();
            //Connection.Close();
            return clamp;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_Select", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsClamp = new DataSet();
            adapter.Fill(dsClamp);
            return dsClamp.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_Search", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsClamp = new DataSet();
            adapter.Fill(dsClamp);
            return dsClamp.Tables[0];
        }

        public static EClamp SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByProductCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EClamp clamp = new EClamp();

            try
            {
                Connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                    clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    clamp.Type = Convert.ToByte(reader["Type"].ToString());
                    clamp.Comment = reader["Comment"].ToString();
                    clamp.Name = reader["Name"].ToString();
                    clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                    clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                    clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    clamp.XCode = new Guid(reader["XCode"].ToString());
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record found for ProductCode : {0} \n", ProductCode));
                }

                reader.Close();

                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EClamp.SelectByProductCode : {0} \n", ex1.Message));
            }
            return clamp;
        }

        public static DataTable SelectByType(int Type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_SelectByType", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsClamp = new DataSet();
            adapter.Fill(dsClamp);
            return dsClamp.Tables[0];
        }

        //MEDHAT //ShareOnServer
        public static EClamp ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = ServerConnection;
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();

            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : clamp\n");
            }
            reader.Close();
            return clamp;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp);
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
                    ed.WriteMessage(string.Format("Error EKalamp.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EKalamp.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }



            /////////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.Insertx: {0} \n", ex1.Message);
            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp);
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
                ed.WriteMessage(string.Format("Error EKalamp.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }




            //////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.Insertx: {0} \n", ex1.Message);
            //    //Connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Clamp_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Kalamp, ServerConnection, ServerTransaction);
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
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Kalamp, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.LocalInsert1X : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Clamp_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Kalamp))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Kalamp, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in Clamp");
                    }
                }
                ed.WriteMessage("EClamp.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Kalamp, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
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
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
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
                    ed.WriteMessage(string.Format("Error EKalamp.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EKalamp.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }



            ///////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.UpdateX: {0} \n", ex1.Message);
            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iKind", Kind));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceSupport", DistanceSupport));
            sqlCommand.Transaction = _transaction;
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
                ed.WriteMessage(string.Format("Error EKalamp.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }




            ///////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.UpdateX: {0} \n", ex1.Message);
            //    //Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Kalamp);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EKalamp.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKalamp.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }



            ///////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.DeleteX: {0} \n", ex1.Message);
            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                sqlCommand.Transaction = _transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp)))
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
                    ed.WriteMessage(string.Format("Error EKalamp.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKalamp.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            ///////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(" ERROR EClamp.DeleteX: {0} \n", ex1.Message);
            //    //Connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EClamp SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
                ed.WriteMessage("selected clamp was not found : {0} \n", XCode);
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
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
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
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
            Connection.Close();

            return clamp;
        }

        public static EClamp SelectByXCode(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = LocalConnection;
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            EClamp clamp = new EClamp();

            try
            {
                sqlCommand.Transaction = LocalTransaction;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                    clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    clamp.Type = Convert.ToByte(reader["Type"].ToString());
                    clamp.Comment = reader["Comment"].ToString();
                    clamp.Name = reader["Name"].ToString();
                    clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                    clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                    clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    clamp.XCode = new Guid(reader["XCode"].ToString());
                    clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
                }
                else
                {
                    clamp.Code = -1;
                }
                reader.Close();
                //EQUIPMENT
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
                sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
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
                sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
                sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
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
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error EClamp.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return clamp;
        }

        //MOUSAVI , AutoPoleInstallation
        public static EClamp SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("%%% XCODE : {0} \n", XCode);
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }

            //ed.WriteMessage("%%% Xcode :{0} \n", clamp.XCode);
            reader.Close();
            Connection.Close();
            return clamp;
        }

        //MEDHAT //ShareOnServer
        public static EClamp SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();

            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }
            reader.Close();
            return clamp;
        }

        //ASWHKTORAB
        public static EClamp SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
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
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", clamp.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Kalamp));
            reader = sqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }
            reader.Close();
            //Connection.Close();

            return clamp;
        }

        //ASWHKTORAB
        public static EClamp SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }
            reader.Close();
            //Connection.Close();

            return clamp;
        }

        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsClamp = new DataSet();
            adapter.Fill(dsClamp);
            return dsClamp.Tables[0];
        }



        public static DataTable SelectByTypeX(int Type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_SelectByType", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));

            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Clamp_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsclamp = new DataSet();
            adapter.Fill(dsclamp);
            return dsclamp.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Clamp_SearchByName", connection);
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
        public static EClamp CheckForExist(byte _Type, byte _Kind, int _Voltagelevel, double _DistanceSupport)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Clamp_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iKind", _Kind));
            command.Parameters.Add(new SqlParameter("iVoltagelevel", _Voltagelevel));
            command.Parameters.Add(new SqlParameter("iDistanceSupport", _DistanceSupport));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }
            reader.Close();
            connection.Close();
            return clamp;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~`
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Clamp_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iKind", Kind));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltagelevel", VoltageLevel));
            //ed.WriteMessage("DistanceSupport={0}\n", DistanceSupport);
            insertCommand.Parameters.Add(new OleDbParameter("iDistanceSupport", DistanceSupport));
            try
            {
                con.Open();
                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EClamp.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Clamp_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;


            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            //ed.WriteMessage("Type:{0} \n", Type);
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            //ed.WriteMessage("Kind:{0} \n", Kind);
            insertCommand.Parameters.Add(new OleDbParameter("iKind", Kind));
            //ed.WriteMessage("Comment:{0} \n", Comment);
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            //ed.WriteMessage("Name:{0} \n", Name);
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //ed.WriteMessage("VoltageLevel:{0} \n", VoltageLevel);
            insertCommand.Parameters.Add(new OleDbParameter("iVoltagelevel", VoltageLevel));
            //ed.WriteMessage("DistanceSupport:{0} \n", DistanceSupport);
            insertCommand.Parameters.Add(new OleDbParameter("iDistanceSupport", DistanceSupport));
            try
            {

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Kalamp);
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
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Kalamp, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.AccessInsert11 : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Clamp_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;


            //ed.WriteMessage("XCode:{0} \n", XCode);
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            //ed.WriteMessage("Type:{0} \n", Type);
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            //ed.WriteMessage("Kind:{0} \n", Kind);
            insertCommand.Parameters.Add(new OleDbParameter("iKind", Kind));
            //ed.WriteMessage("Comment:{0} \n", Comment);
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            //ed.WriteMessage("Name:{0} \n", Name);
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //ed.WriteMessage("VoltageLevel:{0} \n", VoltageLevel);
            insertCommand.Parameters.Add(new OleDbParameter("iVoltagelevel", VoltageLevel));
            //ed.WriteMessage("DistanceSupport:{0} \n", DistanceSupport);
            insertCommand.Parameters.Add(new OleDbParameter("iDistanceSupport", DistanceSupport));
            int OldCode = Code;
            try
            {

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Kalamp);
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
                    ed.WriteMessage("Main Parent is Clamp: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess (OldCode, (int)Atend.Control.Enum.ProductType.Kalamp, Code, _OldTransaction,_OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Clamp failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECalamp.AccessInsert11 : {0} \n", ex.Message));
                return false;
            }
        }

        //MOUSAVI->AutoPoleInstallation , drawconductor //StatusReport
        public static EClamp AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
                ed.WriteMessage("selected clamp was not found :{0} \n", Code);
            }
            reader.Close();
            Connection.Close();
            return clamp;
        }

        public static EClamp AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
                ed.WriteMessage("selected clamp was not found :{0} \n", Code);
            }
            reader.Close();
            return clamp;
        }

        public static EClamp AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection =_connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
                ed.WriteMessage("selected clamp was not found :{0} \n", Code);
            }
            reader.Close();
            //Connection.Close();
            return clamp;
        }

        public static EClamp AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EClamp clamp = new EClamp();
            if (reader.Read())
            {
                clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(reader["Type"].ToString());
                clamp.Comment = reader["Comment"].ToString();
                clamp.Name = reader["Name"].ToString();
                clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(reader["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
            }
            else
            {
                clamp.Code = -1;
            }

            //ed.WriteMessage("%%% code :{0} \n", clamp.Code);
            reader.Close();
            Connection.Close();
            return clamp;
        }

        //MOUSAVi //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EClamp AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection Connection = _connection;
        //    OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_SelectByXCode", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    sqlCommand.Transaction = _transaction;
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    EClamp clamp = new EClamp();
        //    if (reader.Read())
        //    {
        //        clamp.Code = Convert.ToInt32(reader["Code"].ToString());
        //        clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        clamp.Type = Convert.ToByte(reader["Type"].ToString());
        //        clamp.Comment = reader["Comment"].ToString();
        //        clamp.Name = reader["Name"].ToString();
        //        clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
        //        clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
        //        //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
        //        clamp.XCode = new Guid(reader["XCode"].ToString());
        //        clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
        //    }
        //    else
        //    {
        //        clamp.Code = -1;
        //    }
        //    reader.Close();
        //    return clamp;
        //}

        public static EClamp AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Clamp_SelectByProductCode", connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EClamp clamp = new EClamp();

            try
            {
                connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    clamp.Code = Convert.ToInt32(reader["Code"].ToString());
                    clamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    clamp.Type = Convert.ToByte(reader["Type"].ToString());
                    clamp.Comment = reader["Comment"].ToString();
                    clamp.Name = reader["Name"].ToString();
                    clamp.Kind = Convert.ToByte(reader["Kind"].ToString());
                    clamp.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                    clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    clamp.XCode = new Guid(reader["XCode"].ToString());
                    clamp.DistanceSupport = Convert.ToDouble(reader["DistanceSupport"].ToString());
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                connection.Close();
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EClamp.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return clamp;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Clamp_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsclamp = new DataSet();
            adapter.Fill(dsclamp);
            return dsclamp.Tables[0];
        }

        //calculation HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection Connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Clamp_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsclamp = new DataSet();
            adapter.Fill(dsclamp);
            return dsclamp.Tables[0];
        }

        //Hatami
        public static DataTable AccessSelectbyCalampType(int type)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Clamp_SelectByType", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", type));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Clamp_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsclamp = new DataSet();
            adapter.Fill(dsclamp);
            return dsclamp.Tables[0];
        }

        //AutoPoleInstallation-frmDrawClamp-frmDrawClampOrHeader
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessSelectAll();
            DataTable SqlTbl = SelectAllX();
            //ed.WriteMessage("AccTbl.Count={0}\n", AccTbl.Rows.Count);
            //ed.WriteMessage("SqlTbl.Count={0}\n", SqlTbl.Rows.Count);
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
            //ed.WriteMessage("~~~~~~~~~~Clamp column ~~~~~~~~~~\n");
            //foreach (DataColumn Dc1 in AccTbl.Columns)
            //{
            //    ed.WriteMessage("ColumnName={0}\n", Dc1.ColumnName);
            //}

            //ed.WriteMessage("MergeTbl.Count={0}\n", MergeTbl.Rows.Count);

            return MergeTbl;

        }

        public static DataTable SelectAllAndMergeOburi()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessSelectbyCalampType(5);
            DataTable SqlTbl = SelectByTypeX(5);
            //ed.WriteMessage("AccTbl.Count={0}\n", AccTbl.Rows.Count);
            //ed.WriteMessage("SqlTbl.Count={0}\n", SqlTbl.Rows.Count);
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
            //ed.WriteMessage("~~~~~~~~~~Clamp column ~~~~~~~~~~\n");
            //foreach (DataColumn Dc1 in AccTbl.Columns)
            //{
            //    ed.WriteMessage("ColumnName={0}\n", Dc1.ColumnName);
            //}

            //ed.WriteMessage("MergeTbl.Count={0}\n", MergeTbl.Rows.Count);

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

        //        EClamp Clamp = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
        //            //ed.WriteMessage("\n111\n");

        //            EClamp Ap = Atend.Base.Equipment.EClamp.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EClamp.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            Clamp.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, Clamp.XCode);
        //            Clamp.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (Clamp.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, Clamp.Code, (int)Atend.Control.Enum.ProductType.Kalamp))
        //                {
        //                    if (Clamp.UpdateX(Localtransaction, Localconnection))
        //                    {
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, Clamp.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Kalamp, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EcLAMP.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
        //            //ed.WriteMessage("\n111\n");

        //            EClamp Ap = Atend.Base.Equipment.EClamp.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EClamp.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EClamp.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Kalamp));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Kalamp, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EClamp.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}



        //******************************Access To Memory For Calculation
        public static EClamp AccessSelectByCode(DataTable dtCalamp, int Code)
        {
            EClamp clamp = new EClamp();
            DataRow[] dr = dtCalamp.Select(string.Format("Code={0}", Code));
            if (dr.Length != 0)
            {
                clamp.Code = Convert.ToInt32(dr[0]["Code"].ToString());
                clamp.ProductCode = Convert.ToInt32(dr[0]["ProductCode"].ToString());
                clamp.Type = Convert.ToByte(dr[0]["Type"].ToString());
                clamp.Comment = dr[0]["Comment"].ToString();
                clamp.Name = dr[0]["Name"].ToString();
                clamp.Kind = Convert.ToByte(dr[0]["Kind"].ToString());
                clamp.VoltageLevel = Convert.ToInt32(dr[0]["VoltageLevel"].ToString());
                //clamp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                clamp.XCode = new Guid(dr[0]["XCode"].ToString());
                clamp.DistanceSupport = Convert.ToDouble(dr[0]["DistanceSupport"].ToString());
            }
            return clamp;
        }

    }
}
