using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EAirPost
    {
        public EAirPost()
        { }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private double capacity;
        public double Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        //private int outputFeederCount;
        //public int OutPutFeederCount
        //{
        //    get { return outputFeederCount; }
        //    set { outputFeederCount = value; }
        //}

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        private ArrayList containerList;
        public ArrayList ContainerList
        {
            get { return containerList; }
            set { containerList = value; }
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

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private byte[] image;
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();


        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            int containerCode;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iImage", Image));
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
                ed.WriteMessage(string.Format(" ERROR AirPost.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }


        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            int containerCode;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //SqlConnection _connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_Insert", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",outputFeederCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                // _connection.Open();
                //_transaction = _connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    Code = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //ed.writeMessage("CountEquipment" + equipmentList.Count.ToString()+"\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;// containerCode;
                        //containerPackage.XCode = XCode;
                        //containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                        //if (containerPackage.InsertX(_transaction, _connection))
                        //{
                        //    canCommitTransaction = true;
                        //}
                        //else
                        //{
                        //    canCommitTransaction = false;
                        //}
                    }
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.InsertX(_transaction, _connection) && canCommitTransaction)
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

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
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
                        //_transaction.Commit();
                        //_connection.Close();
                        return true;
                    }
                    else
                    {
                        //_transaction.Rollback();
                        //_connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    //_transaction.Rollback();
                    //_connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Insert {0}\n", ex1.Message));

                //_connection.Close();
                return false;
            }

        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_AirPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            ed.WriteMessage("ServerInsert\n");
            //ed.WriteMessage("start para \n");

            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //ed.WriteMessage("end  para \n");

            try
            {

                ed.WriteMessage("Befor Executation\n");
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                ed.WriteMessage("end try \n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.AirPost, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is AirPost: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.AirPost, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Server failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_AirPost_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("start para \n");
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //ed.WriteMessage("end  para \n");

            try
            {

                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("end try \n");

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.AirPost))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.AirPost, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation in AirPost ");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is AirPost: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.AirPost, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Server failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",OutPutFeederCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
                    int containerPackageCode = containerPackage.Code;

                    //if (EContainerPackage.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(_transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;

                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackageCode.ToString() + "\n");
                    if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackageCode;

                            if (_EProductPackage.Insert(_transaction, connection) && canCommitTransaction)
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

                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    connection.Close();
                    _transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));

                    if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost))))
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
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    transaction.Rollback();
                    connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR AirPost.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));

                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost))))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction(local):{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR AirPost.DeleteX(local): {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EAirPost SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iCode", airPost.code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

                nodeKeysEPackage.Add(dataReader["ProductCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            connection.Close();
            return airPost;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            //EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                //airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                Comment = dataReader["Comment"].ToString();
                //airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
            }
            //dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            //command.Parameters.Add(new SqlParameter("iCode", airPost.code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            //dataReader = command.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (dataReader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

            //    nodeKeysEPackage.Add(dataReader["ProductCode"].ToString());
            //    nodeTypeEPackage.Add(dataReader["TableType"].ToString());
            //    nodeCountEPackage.Add(dataReader["Count"].ToString());

            //}

            dataReader.Close();
            connection.Close();
            //return airPost;
        }

        //MEDHAT //ShareOnServer
        public static EAirPost ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.Code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
            }
            else
            {
                airPost.code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : air post\n");
            }
            dataReader.Close();
            return airPost;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_AirPost_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);

            return dsAirPost.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_AirPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);

            return dsAirPost.Tables[0];
        }

        public static EAirPost SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_AirPost_SelectByProductCode", connection);
            EAirPost airPost = new EAirPost();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                    airPost.name = Convert.ToString(dataReader["Name"]);
                    airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                    //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                    airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                    airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                    airPost.Comment = dataReader["Comment"].ToString();
                    airPost.Image = (byte[])(dataReader["Image"]);
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));
                }
                dataReader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EAirPost.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return airPost;
        }

        public static DataTable DrawSearch(int Capacity, byte IsExistance)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_AirPost_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);
            return dsAirPost.Tables[0];
        }

        //ASHKTORAB
        public static EAirPost ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt32(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }

            dataReader.Close();
            //connection.Close();
            return airPost;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            int containerCode;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",outputFeederCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    containerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //ed.writeMessage("CountEquipment" + equipmentList.Count.ToString()+"\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
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
                            //ed.writeMessage("Error For Insert \n");
                        }
                        //ed.WriteMessage("XCODE={0},FatherType={1},ChildXCode={2},ChildType={3}\n", XCode, Atend.Control.Enum.ProductType.AirPost,_EProductPackage.XCode,_EProductPackage.TableType);
                        //bool answer=EContainerPackage.FindLoopNode(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost), _EProductPackage.XCode, Convert.ToInt32(_EProductPackage.TableType));
                        //ed.WriteMessage("Answer={0}\n",answer);
                        //if (!answer)
                        //{
                        //    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        //    if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                        //    {
                        //        canCommitTransaction = true;
                        //    }
                        //    else
                        //    {
                        //        canCommitTransaction = false;
                        //        //ed.writeMessage("Error For Insert \n");
                        //    }
                        //}
                        Counter++;
                    }

                    Counter = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
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
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Insert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction Localtransaction, SqlConnection Localconnection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = Localconnection;
            SqlCommand insertCommand = new SqlCommand("E_AirPost_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = Localtransaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {

                //Code = Convert.ToInt32(insertCommand.ExecuteNonQuery());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {//@@
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.AirPost, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(Localtransaction, Localconnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.AirPost, XCode, ServerTransaction , ServerConnection, Localtransaction, Localconnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Server failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction Localtransaction, SqlConnection Localconnection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = Localconnection;
            SqlCommand insertCommand = new SqlCommand("E_AirPost_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = Localtransaction;

            //ed.WriteMessage("start para \n");
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //ed.WriteMessage("end  para \n");

            try
            {

                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("end try \n");

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(Localtransaction, Localconnection, XCode, (int)Atend.Control.Enum.ProductType.AirPost))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.AirPost, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(Localtransaction, Localconnection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation in AirPost ");
                    }
                }
                ed.WriteMessage("EAirPost.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is AirPost: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.AirPost, XCode, ServerTransaction, ServerConnection, Localtransaction, Localconnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Server failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            int containerCode;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",outputFeederCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {

                    containerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //ed.writeMessage("CountEquipment" + equipmentList.Count.ToString()+"\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    //containerPackage.ContainerCode = 0;
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                    //    if (containerPackage.InsertX(transaction, connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
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

                    Counter = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                        //_EOperation.ProductID = 
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
                    ed.WriteMessage(string.Format("Error In TransAction in AirPost InsertX:{0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Insert {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",OutPutFeederCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
                    int containerPackageCode = containerPackage.Code;

                    //if (EContainerPackage.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(_transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;

                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}

                    //****************************
                    //ed.WriteMessage("@@ :{0}\n", XCode);
                    //while (Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //    ed.WriteMessage("##ContainerPackageCode:{0}\n", containerPackageCode);
                    //    ed.WriteMessage("##XCode:{0}\n", _EProductPackage.XCode);
                    //    //Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    //_EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //    //ed.WriteMessage("@@@ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                    //    //_EProductPackage.ContainerPackageCode = containerPackageCode;
                    //    //ed.WriteMessage("@@ {0} , {1}\n", _EProductPackage.XCode, _EProductPackage.TableType);
                    //    //if (!EContainerPackage.FindLoopNode(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost), _EProductPackage.XCode, _EProductPackage.TableType))
                    //    //{
                    //    //}
                    //    Counter++;
                    //}
                    //Counter = 0;
                    //****************************

                    ed.WriteMessage("containerPackageCode:{0}\n", containerPackageCode);
                    if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("@@@ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackageCode;
                            if (_EProductPackage.InsertX(_transaction, connection) && canCommitTransaction)
                            {
                                canCommitTransaction = true;
                            }
                            else
                            {
                                canCommitTransaction = false;

                            }
                            //if (!EContainerPackage.FindLoopNode(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost), _EProductPackage.XCode, _EProductPackage.TableType))
                            //{
                            //    ed.WriteMessage("InsertProductPackage\n");
                            //    if (_EProductPackage.InsertX(_transaction, connection) && canCommitTransaction)
                            //    {
                            //        canCommitTransaction = true;
                            //    }
                            //    else
                            //    {
                            //        canCommitTransaction = false;

                            //    }
                            //}
                            Counter++;
                        }
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);

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

                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction(local):{0}", ex1.Message);
                    connection.Close();
                    _transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Update(local) {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _Transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            //command.Parameters.Add(new SqlParameter("iOutputFeederCount",OutPutFeederCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                //connection.Open();
                //_transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
                    int containerPackageCode = containerPackage.Code;

                    //if (EContainerPackage.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(_transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;

                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //ed.WriteMessage("containerPackage.XCode.ToString()" + containerPackageCode.ToString() + "\n");
                    //if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                    //        _EProductPackage.ContainerPackageCode = containerPackageCode;

                    //        if (_EProductPackage.InsertX(_transaction, connection) && canCommitTransaction)
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

                    Counter = 0;

                    //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost);

                    //        if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
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
                    if (canCommitTransaction)
                    {
                        //_transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //_transaction.Rollback();
                        //connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }
                }

                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction(local):{0}", ex1.Message);
                    //connection.Close();
                    //_transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EAirPost.Update(local) {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));

                    DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.AirPost);
                    if (ProdTbl.Rows.Count > 0)
                    {
                        transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost))))
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)) && canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction(local):{0}\n", ex1.Message);
                    transaction.Rollback();
                    connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR AirPost.DeleteX(local): {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    Boolean canCommitTransaction = true;
                    command.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_transaction, _connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));

                    //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackage.Code);
                    //if (ProdTbl.Rows.Count > 0)
                    //{
                    //    transaction.Rollback();
                    //    connection.Close();
                    //    return false;
                    //}

                    //if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost))))
                    //{
                    //    canCommitTransaction = true;
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}

                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code) && canCommitTransaction)
                    //{
                    //    canCommitTransaction = true;
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}

                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost)) && canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction(local):{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR AirPost.DeleteX(local): {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EAirPost SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }

            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                EOperation Operation = new EOperation();
                Operation.ProductID = Convert.ToInt32(dataReader["ProductID"].ToString());
                Operation.Count = Convert.ToDouble(dataReader["Count"].ToString());
                nodeKeys.Add(Operation);

            }

            dataReader.Close();
            connection.Close();
            return airPost;
        }

        //MOUSAVI->SentFromLocalToAccess
        public static EAirPost SelectByXCodeForDesign(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            else
            {
                airPost.Code = -1;
            }
            dataReader.Close();
            connection.Close();
            return airPost;
        }

        //ShareOnServer
        public static EAirPost SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlTransaction transaction = LocalTransaction;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            EAirPost airPost = new EAirPost();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("SelectByXCodeForDesign\n");
            try
            {
                command.Transaction = transaction;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                    airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                    //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                    //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                    airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                    airPost.Comment = dataReader["Comment"].ToString();
                    airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                    airPost.XCode = new Guid(dataReader["XCode"].ToString());
                    airPost.Image = (byte[])(dataReader["Image"]);

                }
                else
                {
                    airPost.Code = -1;
                }
                ed.WriteMessage("Air.Code={0}\n", airPost.Code);
                dataReader.Close();
            }
            catch (Exception ex)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EAirpost.In SelectByXCodeForDesign.TransAction:{0}\n", ex.Message);
            }

            return airPost;
        }

        public static EAirPost SelectByXCode(Guid XCode, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.Code = Convert.ToInt16(dataReader["Code"].ToString());

                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());

                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());

                airPost.Comment = dataReader["Comment"].ToString();

                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());

                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }


            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();

            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }

            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                nodeKeys.Add(dataReader["ProductID"].ToString());

            }

            dataReader.Close();
            //connection.Close();
            return airPost;
        }

        //ASHKTORAB
        public static EAirPost SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.Code = Convert.ToInt16(dataReader["Code"].ToString());

                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());

                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());

                airPost.Comment = dataReader["Comment"].ToString();

                airPost.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());

                airPost.Image = (byte[])(dataReader["Image"]);
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
            }
            else
            {
                airPost.Code = -1;
            }


            //dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", airPost.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            //dataReader = command.ExecuteReader();

            //nodeCountEPackageX.Clear();
            //nodeKeysEPackageX.Clear();
            //nodeTypeEPackageX.Clear();
            //while (dataReader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("Count" + dataReader["Count"].ToString()+"\n");

            //    nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
            //    nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
            //    nodeCountEPackageX.Add(dataReader["Count"].ToString());
            //}

            dataReader.Close();
            //connection.Close();
            return airPost;
        }


        //Hattami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_AirPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);

            return dsAirPost.Tables[0];
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("I MA in SelectAllX\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_AirPost_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);
            //ed.WriteMessage("Finish SelectAllX\n");
            return dsAirPost.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_AirPost_SearchByName", connection);
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int containerCode;
            OleDbTransaction transaction;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_AirPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));

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
                ed.WriteMessage(string.Format("Error EAirPost.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_AirPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("start para \n");

            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            //ed.WriteMessage("end  para \n");

            try
            {
                //ed.WriteMessage("start try \n");
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("end try \n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.AirPost);
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
                    ed.WriteMessage("Main Parent is AirPost: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.AirPost, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess AirPost failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_AirPost_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            //ed.WriteMessage("start para \n");

            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            //ed.WriteMessage("end  para \n");
            int oldCode = Code;
            try
            {
                //ed.WriteMessage("start try \n");
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("end try \n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.AirPost);
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
                    ed.WriteMessage("Main Parent is AirPost: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.AirPost, Code,_OldTransaction,_OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess AirPost failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EAirPost.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static EAirPost AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            else
            {
                airPost.Code = -1;
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", airPost.code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["ProductCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            connection.Close();
            return airPost;
        }

        public static EAirPost AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            else
            {
                airPost.Code = -1;
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", airPost.code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.AirPost));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["ProductCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            //connection.Close();
            return airPost;
        }

        public static EAirPost AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_AirPost_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.XCode = new Guid(dataReader["XCode"].ToString());
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            dataReader.Close();
            dataReader.Close();
            connection.Close();
            return airPost;
        }


        public static EAirPost AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection ;
            OleDbCommand command = new OleDbCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            else
            {
                airPost.Code = -1;
            }
            dataReader.Close();
            //connection.Close();
            return airPost;
        }

        //MOUSAVI->SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EAirPost AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction , OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_AirPost_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;

        //    OleDbDataReader dataReader = command.ExecuteReader();
        //    EAirPost airPost = new EAirPost();
        //    if (dataReader.Read())
        //    {
        //        airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
        //        airPost.Name = Convert.ToString(dataReader["Name"].ToString());
        //        //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
        //        //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
        //        airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
        //        airPost.Comment = dataReader["Comment"].ToString();
        //        airPost.XCode = new Guid(dataReader["XCode"].ToString());
        //        airPost.Image = (byte[])(dataReader["Image"]);
        //    }
        //    else
        //    {
        //        airPost.Code = -1;
        //    }
        //    dataReader.Close();
        //    return airPost;
        //}

        public static DataTable AccessSelectAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("I MA In AccessSelectAll\n");

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_AirPost_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);
            //ed.WriteMessage("Finish AccessSelectAll\n");
            return dsAirPost.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_AirPost_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);

            return dsAirPost.Tables[0];
        }

        public static EAirPost AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_AirPost_SelectByProductCode", connection);
            EAirPost airPost = new EAirPost();
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                OleDbDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                    airPost.name = Convert.ToString(dataReader["Name"]);
                    airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                    //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                    airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                    airPost.Comment = dataReader["Comment"].ToString();
                    airPost.Image = (byte[])(dataReader["Image"]);
                }
                else
                {
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));
                }
                dataReader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EAirPost.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return airPost;
        }

        public static DataTable AccessDrawSearch(int Capacity, byte IsExistance)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_AirPost_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            DataSet dsAirPost = new DataSet();
            adapter.Fill(dsAirPost);
            return dsAirPost.Tables[0];
        }

        public static EAirPost AccessSelectByCodeForDesign(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_AirPost_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EAirPost airPost = new EAirPost();
            if (dataReader.Read())
            {
                airPost.code = Convert.ToInt16(dataReader["Code"].ToString());
                airPost.Name = Convert.ToString(dataReader["Name"].ToString());
                //airPost.ProductCode = Convert.ToInt16(dataReader["ProductCode"].ToString());
                //airPost.OutPutFeederCount = Convert.ToInt16(dataReader["OutputFeederCount"].ToString());
                airPost.Capacity = Convert.ToDouble(dataReader["Capacity"].ToString());
                airPost.Comment = dataReader["Comment"].ToString();
                airPost.Image = (byte[])(dataReader["Image"]);
            }
            dataReader.Close();
            connection.Close();
            return airPost;
        }

        //frmDrawAirPost
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("I MA In SelectAllAndMerge\n");
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

            //ed.WriteMessage("Finish SelectAllAndMerge\n");
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

        //        EAirPost airpost = SelectByXCode(XCode, Localtransaction, Localconnection);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
        //            //ed.WriteMessage("\n111\n");

        //            EAirPost Ap = Atend.Base.Equipment.EAirPost.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EAirPost.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            DataTable OperationTbl = new DataTable();
        //            airpost.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, airpost.XCode);
        //            airpost.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (airpost.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, airpost.Code, (int)Atend.Control.Enum.ProductType.AirPost))
        //                {
        //                    if (!airpost.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, airpost.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.AirPost, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
        //            ed.WriteMessage("\n~~~~~~~Code ={0} \n", Code);

        //            EAirPost Ap = Atend.Base.Equipment.EAirPost.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EAirPost.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EAirPost.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }


        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AirPost));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.AirPost, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EAirPost.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
