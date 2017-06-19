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
    public class ECatOut
    {
        public ECatOut()
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

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        private double amper;
        public double Amper
        {
            get { return amper; }
            set { amper = value; }
        }

        private double vol;
        public double Vol
        {
            get { return vol; }
            set { vol = value; }
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

        private bool isdefault;
        public bool IsDefault
        {
            get { return isdefault; }
            set { isdefault = value; }
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





        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_CatOut_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }


        //HATAMI//SendFRomLocalToServer //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_CatOut_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                ed.WriteMessage("operation started \n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.CatOut, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                ed.WriteMessage("Sub started \n");
                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.CatOut, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Catout failed4");
                    }
                }

                ed.WriteMessage("finished \n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_CatOut_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                insertCommand.ExecuteNonQuery();

                ed.WriteMessage("operation started \n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.CatOut))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.CatOut, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in CatOut");
                    }
                }

                ed.WriteMessage("Sub started \n");
                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.CatOut, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Catout failed1");
                    }
                }

                ed.WriteMessage("finished \n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int ContainerCode = 0;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();


                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    //ed.WriteMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
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
                    //        ed.WriteMessage("Error For Insert \n");
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
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECatOut.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_CatOut_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));
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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.Insert: {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_CatOut_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_CatOut_Delete", Connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
                    //operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut))) & canCommitTransaction)
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_Delete", Connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
                    //operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.Delete(transaction, Connection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut))) & canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECatOut.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_CatOut_Delete", connection);
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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.Delete: {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public static ECatOut SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();

            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iCode", catOut.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.code:" + catOut.Code + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

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
            command.Parameters.Add(new SqlParameter("iProductCode", catOut.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
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
            return catOut;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                //catOut.Code = Convert.ToInt32(reader["Code"]);
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Vol = Convert.ToDouble(reader["Vol"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();

            }
            reader.Close();



            Connection.Close();
        }

        //public static ECatOut SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_CatOut_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    connection.Open();
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    SqlDataReader reader = command.ExecuteReader();
        //    ECatOut catOut = new ECatOut();
        //    if (reader.Read())
        //    {
        //        catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        catOut.Type = Convert.ToByte(reader["Type"].ToString());
        //        catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
        //        catOut.Comment = reader["Comment"].ToString();
        //        catOut.Name = reader["Name"].ToString();

        //    }
        //    reader.Close();
        //    connection.Close();
        //    return catOut;
        //}

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_CatOut_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_CatOut_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        public static ECatOut SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            ECatOut catOut = new ECatOut();
            SqlCommand command = new SqlCommand("E_CatOut_Select", connection);
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    catOut.Code = Convert.ToInt32(reader["Code"]);

                    catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    catOut.Type = Convert.ToByte(reader["Type"].ToString());
                    catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                    catOut.Comment = reader["Comment"].ToString();
                    catOut.Name = reader["Name"].ToString();

                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found for ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECatout.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return catOut;
        }

        public static DataTable DrawSearch(Single Amper, Single Vol, byte IsExistance)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_CatOut_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        //ASHKTORAB
        public static ECatOut ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            //Connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.Xcode:" + catOut.XCode + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

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
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************


            //Connection.Close();
            return catOut;
        }

        //MEDHAT //ShareOnServer
        public static ECatOut ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = ServerTransaction;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                catOut.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : catout\n");
            }
            reader.Close();
            return catOut;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part ~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
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
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    //ContainerCode = 
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();


                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;// ContainerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECatOut.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            int ContainerCode = 0;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iCode", Code));
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


                    //operationList
                    //ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = 0;// ContainerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    //ed.WriteMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
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
                    //        ed.WriteMessage("Error For Insert \n");
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
                    ed.WriteMessage("Error In Transaction:{0}", ex1);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECatOut.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_CatOut_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {

                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.CatOut, ServerConnection, ServerTransaction);
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
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.CatOut, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Catout failed2");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_CatOut_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                insertCommand.ExecuteNonQuery();

                ed.WriteMessage("operation started \n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.CatOut))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.CatOut, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in CatOut");
                    }
                }
                ed.WriteMessage("ECatout.Operation passed \n");

                ed.WriteMessage("Sub started \n");
                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.CatOut, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Catout failed3");
                    }
                }

                ed.WriteMessage("finished \n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool XXUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
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
                    //ed.WriteMessage("GOTO\n");
                    //Operation
                    if (EOperation.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;//_EOperation.ProductCode = code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    //ed.WriteMessage("GOTO\n");
                    //Operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;// code;
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.Update: {0} \n", ex1.Message));

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
            SqlCommand command = new SqlCommand("E_CatOut_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

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
                    //ed.WriteMessage("GOTO\n");
                    //Operation
                    //if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        //_EOperation.ProductCode = 0;// code;
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut);
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
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    //Counter = 0;
                    //if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR ECatOut.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.CatOut);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_DeleteX", Connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
                    //operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut))) & canCommitTransaction)
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
                ed.WriteMessage(string.Format(" ERROR ECatOut.DeleteX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_DeleteX", Connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
                    //operation
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut))) & canCommitTransaction)
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
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ECatOut.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static ECatOut SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                catOut.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.Xcode:" + catOut.XCode + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

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
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
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

            return catOut;
        }

        //MOUSAVI->SentFromLocalToAccess
        public static ECatOut SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                catOut.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                catOut.Code = -1;
            }
            reader.Close();
            Connection.Close();

            return catOut;
        }

        public static ECatOut SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            //Connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            reader.Close();

            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.Xcode:" + catOut.XCode + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            reader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {

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
            command.Parameters.Add(new SqlParameter("iXCode", catOut.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************


            //Connection.Close();
            return catOut;
        }

        //MEDHAT //ShareOnServer
        public static ECatOut SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_SelectByXCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                catOut.xcode = Guid.Empty;
                catOut.Code = -1;
            }
            reader.Close();
            return catOut;
        }

        //ASHKTORAB
        public static ECatOut SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            //Connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                catOut.Code = -1;
            }
            reader.Close();


            //Connection.Close();
            return catOut;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_CatOut_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_CatOut_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_SearchByName", connection);
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

        public static ECatOut CheckForExist(double _Amper, double _Vol, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_CatOut_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iVol", _Vol));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());
                catOut.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                catOut.Code = -1;
            }
            reader.Close();
            connection.Close();
            return catOut;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ACCESS Part~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_CatOut_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
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
                ed.WriteMessage(string.Format("Error ECatOut.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_CatOut_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                ed.WriteMessage("operation started \n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.CatOut);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                ed.WriteMessage("Sub started \n");
                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.CatOut, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Catout failed");
                    }
                }

                ed.WriteMessage("finished \n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFomAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_CatOut_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            insertCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
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

                ed.WriteMessage("operation started \n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.CatOut);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                ed.WriteMessage("Sub started \n");
                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Catout: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.CatOut, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Catout failed");
                    }
                }

                ed.WriteMessage("finished \n");
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECatOut.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //StatusReport
        public static ECatOut AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();

            }
            else
            {
                catOut.Code = -1;
            }

            reader.Close();
            //ed.WriteMessage("Finish Part1\n");
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", catOut.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.code:" + catOut.Code + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            //ed.WriteMessage("B\n");
            reader = command.ExecuteReader();
            //ed.WriteMessage("A\n");
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************
            //ed.WriteMessage("Finish Part2\n");
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", catOut.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.CatOut));
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

            //ed.WriteMessage("Finish Part3\n");
            Connection.Close();
            return catOut;
        }

        public static ECatOut AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();

            }
            else
            {
                catOut.Code = -1;
            }

            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", catOut.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.CatOut));
            //ed.WriteMessage("CatOut.code:" + catOut.Code + "Type:" + Atend.Control.Enum.ProductType.CatOut + "\n");
            //ed.WriteMessage("B\n");
            reader = command.ExecuteReader();
            //ed.WriteMessage("A\n");
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {

                //ed.WriteMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //**************
            //ed.WriteMessage("Finish Part2\n");
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", catOut.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.CatOut));
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
            
            return catOut;
        }


        public static ECatOut AccessSelectByCodeForConvertor(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand command = new OleDbCommand("E_CatOut_Select", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            // Connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();

            }
            else
            {
                catOut.Code = -1;
            }

            reader.Close();
            return catOut;
        }

        public static ECatOut AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_CatOut_SelectByXcode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            Connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            ECatOut catOut = new ECatOut();
            if (reader.Read())
            {
                catOut.Code = Convert.ToInt32(reader["Code"]);
                catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                catOut.Type = Convert.ToByte(reader["Type"].ToString());
                catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                catOut.Comment = reader["Comment"].ToString();
                catOut.Name = reader["Name"].ToString();
                catOut.XCode = new Guid(reader["XCode"].ToString());

            }
            else
            {
                catOut.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return catOut;
        }

        //MOUSVI->SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ECatOut AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection Connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_CatOut_SelectByXcode", Connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ECatOut catOut = new ECatOut();
        //    if (reader.Read())
        //    {
        //        catOut.Code = Convert.ToInt32(reader["Code"]);
        //        catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        catOut.Type = Convert.ToByte(reader["Type"].ToString());
        //        catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
        //        catOut.Comment = reader["Comment"].ToString();
        //        catOut.Name = reader["Name"].ToString();
        //        catOut.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        catOut.Code = -1;
        //    }
        //    reader.Close();
        //    return catOut;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_CatOut_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_CatOut_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
        }

        public static ECatOut AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            ECatOut catOut = new ECatOut();
            OleDbCommand command = new OleDbCommand("E_CatOut_Select", connection);
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.CommandType = CommandType.StoredProcedure;


            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    catOut.Code = Convert.ToInt32(reader["Code"]);

                    catOut.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    catOut.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    catOut.Type = Convert.ToByte(reader["Type"].ToString());
                    catOut.Vol = Convert.ToDouble(reader["Vol"].ToString());
                    catOut.Comment = reader["Comment"].ToString();
                    catOut.Name = reader["Name"].ToString();

                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found for ProductCode : {0} \n", ProductCode));
                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECatout.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }

            return catOut;
        }

        public static DataTable AccessDrawSearch(Single Amper, Single Vol, byte IsExistance)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_CatOut_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iVol", Vol));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            DataSet dsCatOut = new DataSet();
            adapter.Fill(dsCatOut);
            return dsCatOut.Tables[0];
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
        //        ECatOut catout = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
        //            ECatOut Ap = Atend.Base.Equipment.ECatOut.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ECatOut.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
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
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, catout.Code, (int)Atend.Control.Enum.ProductType.CatOut))
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
        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, catout.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.CatOut, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.ShareOnServer {0}\n", ex1.Message));
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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
        //            ECatOut Ap = Atend.Base.Equipment.ECatOut.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ECatOut.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.ECatOut.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.CatOut));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.CatOut, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;
        //}

    }
}

