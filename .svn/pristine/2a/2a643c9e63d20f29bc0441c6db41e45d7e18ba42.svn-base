using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;

namespace Atend.Base.Equipment
{
    public class EPT
    {

        public EPT()
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

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double pt_Vol;
        public double PT_Vol
        {
            get { return pt_Vol; }
            set { pt_Vol = value; }
        }

        private string pt_Convert;
        public string PT_Convert
        {
            get { return pt_Convert; }
            set { pt_Convert = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                ed.WriteMessage(string.Format("Error EPT.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                //connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PT);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //.WriteMessage("Error For Insert \n");
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
                ed.WriteMessage(string.Format("Error EPost.Delete : {0} \n", ex1.Message));
                string s = ex1.Message;
                //connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PT, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in PT");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PT, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PT failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPT.ServerInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_PT_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.PT))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PT, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in PT");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PT");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PT, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PT failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPost.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_Delete", connection);
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
                ed.WriteMessage(string.Format("Error EPT.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT)))
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
                ed.WriteMessage(string.Format("Error EPT.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EPT SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();

            }
            reader.Close();
            connection.Close();
            return pt;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EPT pt = new EPT();
            if (reader.Read())
            {
                //pt.Code = Convert.ToInt32(reader["Code"].ToString());
                //pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                PT_Convert = reader["PT_Convert"].ToString();
                PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();

            }
            reader.Close();
            connection.Close();
            //return pt;
        }

        public static EPT SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PT_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EPT pt = new EPT();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pt.Code = Convert.ToInt32(reader["Code"].ToString());
                    pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                    pt.PT_Convert = reader["PT_Convert"].ToString();
                    pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                    pt.Comment = reader["Comment"].ToString();
                    pt.Name = reader["Name"].ToString();
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
                ed.WriteMessage(string.Format("Error EPT.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return pt;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adpapter = new SqlDataAdapter("E_PT_Select", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adpapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adpapter = new SqlDataAdapter("E_PT_Search", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adpapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
        }

        //ASHKTORAB
        public static EPT ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());

                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //connection.Close();
            return pt;
        }

        //MEDHAT //ShareOnServer
        public static EPT ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();

            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());

                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {

                pt.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : pt\n");
            }
            reader.Close();
            return pt;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PT);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                        Counter++;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PT);
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
                    ed.WriteMessage(string.Format("Error EPT.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPT.Insert 02 : {0} \n", ex2.Message));
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
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPost.Delete : {0} \n", ex1.Message));
            //    string s = ex1.Message;
            //    connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PT);
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
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.Insert : {0} \n", ex1.Message));
                //Connection.Close();
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
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPost.Delete : {0} \n", ex1.Message));
            //    string s = ex1.Message;
            //    //connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_PT_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.PT, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in PT");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PT, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PT failed");
                    }
                }
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_PT_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.PT))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code , (int)Atend.Control.Enum.ProductType.PT, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in PT");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PT");
                    }
                }
                ed.WriteMessage("ept.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PT, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PT failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPT.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        //ASHKTORAB
        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));
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


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PT)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PT);

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
                    ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PT));
                    ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                    ed.WriteMessage(string.Format("Error EPT.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPT.UpdateX 02: {0} \n", ex2.Message));
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
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPost.DeleteX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", PT_Convert));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format("Error EPT.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            ////////////////////
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
            //    ed.WriteMessage(string.Format("Error EPost.DeleteX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.PT);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PT))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EPT.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPT.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            /////////////////////
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
            //    ed.WriteMessage(string.Format("Error EPT.DeleteX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_DeleteX", connection);
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
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT)))
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
                    ed.WriteMessage(string.Format("Error EPT.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPT.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            //////////////////////////
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
            //    ed.WriteMessage(string.Format("Error EPT.DeleteX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EPT SelectByXCode(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());

                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                pt.code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", pt.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PT));
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
            command.Parameters.Add(new SqlParameter("iXCode", pt.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PT));
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
            return pt;
        }

        //ASHKTORAB //ShareOnServer
        public static EPT SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_PT_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            EPT pt = new EPT();
            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pt.Code = Convert.ToInt32(reader["Code"].ToString());
                    pt.XCode = new Guid(reader["XCode"].ToString());

                    pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                    pt.PT_Convert = reader["PT_Convert"].ToString();
                    pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                    pt.Comment = reader["Comment"].ToString();
                    pt.Name = reader["Name"].ToString();
                    pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    pt.code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EPT.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return pt;
        }

        public static EPT SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());

                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", pt.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PT));
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
            command.Parameters.Add(new SqlParameter("iXCode", pt.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PT));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            //connection.Close();
            return pt;
        }

        //ASHKTORAB
        public static EPT SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());

                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                pt.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return pt;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adpapter = new SqlDataAdapter("E_PT_Search", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adpapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adpapter = new SqlDataAdapter("E_PT_SelectAll", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adpapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_SearchByName", connection);
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

        public static EPT CheckForExist(double _PTVol, string _PTConvert)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PT_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iPT_Vol", _PTVol));
            command.Parameters.Add(new SqlParameter("iPT_Convert", _PTConvert));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.XCode = new Guid(reader["XCode"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                pt.Code = -1;
            }
            reader.Close();
            connection.Close();
            return pt;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new OleDbParameter("iPT_Convert", PT_Convert));
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
                ed.WriteMessage(string.Format("Error EPT.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new OleDbParameter("iPT_Convert", PT_Convert));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PT);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.PT, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PT failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_PT_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iPT_Vol", PT_Vol));
            command.Parameters.Add(new OleDbParameter("iPT_Convert", PT_Convert));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.PT);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.PT, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PT failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //StatusReport
        public static EPT AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();

            }
            else
            {
                pt.Code = -1;
            }
            reader.Close();
            connection.Close();
            return pt;
        }

        public static EPT AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();

            }
            else
            {
                pt.Code = -1;
            }
            reader.Close();
            return pt;
        }

        public static EPT AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_PT_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();

            }
            else
            {
                pt.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return pt;
        }

        public static EPT AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PT_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPT pt = new EPT();
            if (reader.Read())
            {
                pt.Code = Convert.ToInt32(reader["Code"].ToString());
                pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                pt.PT_Convert = reader["PT_Convert"].ToString();
                pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                pt.Comment = reader["Comment"].ToString();
                pt.Name = reader["Name"].ToString();
                pt.XCode = new Guid(reader["XCode"].ToString());

            }
            reader.Close();
            connection.Close();
            return pt;
        }

        //MOUSAVI //SentFromLocalToAccess 
        //public static EPT AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_PT_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EPT pt = new EPT();
        //    if (reader.Read())
        //    {
        //        pt.Code = Convert.ToInt32(reader["Code"].ToString());
        //        pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

        //        pt.PT_Convert = reader["PT_Convert"].ToString();
        //        pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
        //        pt.Comment = reader["Comment"].ToString();
        //        pt.Name = reader["Name"].ToString();
        //        pt.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        pt.Code = -1;
        //    }
        //    reader.Close();
        //    return pt;
        //}

        public static EPT AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PT_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EPT pt = new EPT();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pt.Code = Convert.ToInt32(reader["Code"].ToString());
                    pt.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());

                    pt.PT_Convert = reader["PT_Convert"].ToString();
                    pt.PT_Vol = Convert.ToDouble(reader["PT_Vol"].ToString());
                    pt.Comment = reader["Comment"].ToString();
                    pt.Name = reader["Name"].ToString();
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
                ed.WriteMessage(string.Format("Error EPT.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return pt;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adpapter = new OleDbDataAdapter("E_PT_Select", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adpapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adpapter = new OleDbDataAdapter("E_PT_Search", connection);
            adpapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adpapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsCTPT = new DataSet();
            adpapter.Fill(dsCTPT);
            return dsCTPT.Tables[0];
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

        //        EPT _EPT = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PT));

        //            EPT Ap = Atend.Base.Equipment.EPT.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EPT.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EPT.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPT.XCode);
        //            _EPT.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPT.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPT.Code, (int)Atend.Control.Enum.ProductType.PT))
        //                {
        //                    if (!_EPT.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n112\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPT.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PT, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                Servertransaction.Commit();
        //                Serverconnection.Close();

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EPT.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT));
        //            ed.WriteMessage("\n111\n");

        //            EPT Ap = Atend.Base.Equipment.EPT.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPT.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPT.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }


        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PT));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PT, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPT.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
