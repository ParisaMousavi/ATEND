using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;
using System.Collections;

namespace Atend.Base.Equipment
{
    public class EPhuseKey
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

        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        private double amper;
        public double Amper
        {
            get { return amper; }
            set { amper = value; }
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server PArt~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;


            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.Insert : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }


        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Transaction = _transaction;

            try
            {
                //Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey);
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
                ed.WriteMessage(string.Format("Error EPhuseKey.Insert : {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }


        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                ed.WriteMessage("PhuseKey Insert Succedd Code={0}\n",Code);
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in PhuseKey");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhuseKey failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
               // Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.ServerInsert : {0} \n", ex1.Message));
                return false;
            }

        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;


            //sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.PhuseKey))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in PhuseKey");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PhuseKey");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhuseKey failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }


        }

        public bool Update_XX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.Update : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }


        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.Delete : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }


        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;

            try
            {
                //Connection.Open();


                sqlCommand.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey)))
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
                ed.WriteMessage(string.Format("Error EPhuseKey.ServerDelete : {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }


        }

        public static EPhuseKey SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
            }
            Connection.Close();
            reader.Close();
            return key;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //key.Code = Convert.ToInt32(reader["Code"].ToString());
                //key.XCode = new Guid(reader["XCode"].ToString());
                //key.IsDe= Convert.ToInt32(reader["Code"].ToString());
                Mark = reader["Mark"].ToString();
                //key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }
            Connection.Close();
            reader.Close();
        }

        public static EPhuseKey SelectByProductCode(int ProductCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_SelectByProductCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EPhuseKey key = new EPhuseKey();
            try
            {
                Connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    key.Code = Convert.ToInt32(reader["Code"].ToString());
                    key.Mark = reader["Mark"].ToString();
                    key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    key.Comment = reader["Comment"].ToString();
                    key.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return key;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhuseKey_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

        }

        //ASHKTORAB
        public static EPhuseKey ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            //Connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            //Connection.Close();
            reader.Close();
            return key;
        }

        //MEDHAT //ShareOnServer
        public static EPhuseKey ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                key.code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : phuse key\n");
            }
            reader.Close();
            return key;
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhuseKey_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey);
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EPhuseKey.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhuseKey.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            ///////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.Insert : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}


        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            sqlCommand.Transaction = _transaction;
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey);
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
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            ////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.Insert : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}


        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("_________ OKinsert\n");
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.PhuseKey, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in PhuseKey");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PhuseKey, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhuseKey failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhuseKey.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;


            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("_________ OKupdate\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.PhuseKey))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.PhuseKey, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in PhuseKey");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PhuseKey");
                    }
                }
                ed.WriteMessage("Ephusekey.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PhuseKey, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhuseKey failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhuseKey.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }


        }

        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
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
                    ed.WriteMessage(string.Format("Error EPhuseKey.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhuseKey.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            ///////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.UpdateX : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}


        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format("Error EPhusekey.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            //////////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.UpdateX : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}


        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.PhuseKey);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;

                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EPhuseKey.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhuseKey.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }

            ////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.DeleteX : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}


        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
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
                    if (EOperation.DeleteX(_transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey)))
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
                    ed.WriteMessage(string.Format("Error EPhuseKey.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhuseKey.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            /////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhuseKey.DeleteX : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}


        }

        //MOUSAVI //SentFromLocalToAccess
        public static EPhuseKey SelectByXCode(Guid Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                key.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", key.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhuseKey));
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
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", key.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhuseKey));
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
            return key;
        }

        //ShareOnServer
        public static EPhuseKey SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            EPhuseKey key = new EPhuseKey();

            try
            {
                sqlCommand.Transaction = LocalTransaction;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    key.Code = Convert.ToInt32(reader["Code"].ToString());
                    key.XCode = new Guid(reader["XCode"].ToString());
                    key.Mark = reader["Mark"].ToString();
                    key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    key.Comment = reader["Comment"].ToString();
                    key.Name = reader["Name"].ToString();
                    key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    key.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EPhuseKey.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return key;
        }

        public static EPhuseKey SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", Code));
            //Connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", key.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhuseKey));
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
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", key.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhuseKey));
            reader = sqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            return key;
        }

        public static EPhuseKey SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.XCode = new Guid(reader["XCode"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                key.Code = -1;
            }
            //Connection.Close();
            reader.Close();
            return key;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhuseKey_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhuseKey_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PhuseKey_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;


            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            try
            {
                Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.AccessInsert : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }


        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;


            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.PhuseKey, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PhuseKey failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.AccessInsert : {0} \n", ex1.Message));
                return false;
            }


        }


        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection Connection = _NewConnection;
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _NewTransaction;


            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            int OldCode = Code;

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.PhuseKey);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.PhuseKey, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PhuseKey failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //StatusReport
        public static EPhuseKey AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
            }
            else
            {
                key.Code = -1;
            }
            Connection.Close();
            reader.Close();
            return key;
        }

        public static EPhuseKey AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
            }
            else
            {
                key.Code = -1;
            }
            reader.Close();
            return key;
        }

        public static EPhuseKey AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
           // Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
            }
            else
            {
                key.Code = -1;
            }
            //Connection.Close();
            reader.Close();
            return key;
        }

        public static EPhuseKey AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EPhuseKey key = new EPhuseKey();
            if (reader.Read())
            {
                key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                key.Code = Convert.ToInt32(reader["Code"].ToString());
                key.Mark = reader["Mark"].ToString();
                key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                key.Comment = reader["Comment"].ToString();
                key.Name = reader["Name"].ToString();
                key.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            Connection.Close();
            return key;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EPhuseKey AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection Connection = _connection;
        //    OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_SelectByXCode", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    sqlCommand.Transaction = _transaction;
        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    EPhuseKey key = new EPhuseKey();
        //    if (reader.Read())
        //    {
        //        key.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        key.Code = Convert.ToInt32(reader["Code"].ToString());
        //        key.Mark = reader["Mark"].ToString();
        //        key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        key.Comment = reader["Comment"].ToString();
        //        key.Name = reader["Name"].ToString();
        //        key.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        key.Code = -1;
        //    }
        //    reader.Close();
        //    return key;
        //}

        public static EPhuseKey AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhuseKey_SelectByProductCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EPhuseKey key = new EPhuseKey();
            try
            {
                Connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    key.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    key.Code = Convert.ToInt32(reader["Code"].ToString());
                    key.Mark = reader["Mark"].ToString();
                    key.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    key.Comment = reader["Comment"].ToString();
                    key.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhuseKey.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return key;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PhuseKey_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PhuseKey_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsPhuseKey = new DataSet();
            adapter.Fill(dsPhuseKey);
            return dsPhuseKey.Tables[0];

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

        //        EPhuseKey _EPhuseKey = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
        //            //ed.WriteMessage("\n111\n");

        //            EPhuseKey Ap = Atend.Base.Equipment.EPhuseKey.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EPhuseKey.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EPhuseKey.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPhuseKey.XCode);
        //            _EPhuseKey.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPhuseKey.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPhuseKey.Code, (int)Atend.Control.Enum.ProductType.PhuseKey))
        //                {
        //                    if (!_EPhuseKey.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPhuseKey.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PhuseKey, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPhuseKey.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
        //            EPhuseKey Ap = Atend.Base.Equipment.EPhuseKey.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPhuseKey.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPhuseKey.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PhuseKey, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPhuseKey.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;


        //    //******************************
        //    //SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    //SqlTransaction Localtransaction;
        //    //Guid DeletedXCode = Guid.NewGuid();
        //    //try
        //    //{
        //    //    Localconnection.Open();
        //    //    Localtransaction = Localconnection.BeginTransaction();
        //    //    try
        //    //    {
        //    //        Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
        //    //        EPhuseKey Ap = Atend.Base.Equipment.EPhuseKey.SelectByCode(Localtransaction, Localconnection, Code);
        //    //        if (Ap.XCode != Guid.Empty)
        //    //        {
        //    //            DeletedXCode = Ap.XCode;
        //    //            if (!Atend.Base.Equipment.EPhuseKey.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //    //            {
        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //            }

        //    //            Ap.ServerSelectByCode(Code);
        //    //        }
        //    //        else
        //    //        {
        //    //            Ap = Atend.Base.Equipment.EPhuseKey.SelectByCode(Code);
        //    //            Ap.XCode = DeletedXCode;
        //    //        }

        //    //        Ap.OperationList = new ArrayList();
        //    //        DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhuseKey));
        //    //        Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //    //        if (Ap.InsertX(Localtransaction, Localconnection))
        //    //        {
        //    //            //ed.WriteMessage("\n113\n");

        //    //            Localtransaction.Commit();
        //    //            Localconnection.Close();
        //    //            return true;
        //    //        }
        //    //        else
        //    //        {
        //    //            //ed.WriteMessage("\n114\n");

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;

        //    //        }

        //    //    }
        //    //    catch (System.Exception ex1)
        //    //    {
        //    //        ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));

        //    //        Localtransaction.Rollback();
        //    //        Localconnection.Close();
        //    //        return false;
        //    //    }


        //    //}
        //    //catch (System.Exception ex1)
        //    //{
        //    //    ed.WriteMessage(string.Format(" ERROR EPhuseKey.GetFromServer {0}\n", ex1.Message));

        //    //    Localconnection.Close();
        //    //    return false;
        //    //}

        //    //return true;
        //}

    }
}
