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
    public class EPhusePole
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

        private double amper;
        public double Amper
        {
            get { return amper; }
            set { amper = value; }
        }

        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        private string comment = "";
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

        private bool isDefaul;
        public bool IsDefault
        {
            get { return isDefaul; }
            set { isDefaul = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            ed.WriteMessage("Comment is : {0} \n", Comment);
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhusePole.Insert : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            //sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            ed.WriteMessage("Transaction Comment : {0} \n", Comment);
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                //Connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);
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
                ed.WriteMessage(string.Format("Error EPhusePole.Insert : {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhusePole, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in PhusePole");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PhusePole, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhusePole failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.ServerInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            //sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.PhusePole))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhusePole, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in PhusePole");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PhusePole");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PhusePole, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhusePole failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.Update : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Delete", Connection);
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.Delete : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;
            try
            {
                //Connection.Open();
                sqlCommand.ExecuteNonQuery();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole)))
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
                ed.WriteMessage(string.Format("Error EPhusePole.ServerDelete : {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EPhusePole SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            return phusePole;

        }

        //ASHKTORAB
        public void ServerSelectByCode(/*SqlTransaction _transaction, SqlConnection _connection, */int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();

            SqlDataReader reader = SqlCommand.ExecuteReader();
            //EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                Amper = Convert.ToDouble(reader["Amper"].ToString());
                //phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                //phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Mark = reader["Mark"].ToString();

            }
            //return phusePole;
            reader.Close();
            Connection.Close();
        }

        public static EPhusePole SelectByProductCode(int ProductCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_SelectByProductCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EPhusePole phusePole = new EPhusePole();
            try
            {
                Connection.Open();
                SqlDataReader reader = SqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                    phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    phusePole.Comment = reader["Comment"].ToString();
                    phusePole.Name = reader["Name"].ToString();
                    phusePole.Mark = reader["Mark"].ToString();

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
                ed.WriteMessage(string.Format("Error EPhusePole.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return phusePole;

        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhusePole_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhusePole_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
        }

        //ASHKTORAB
        public static EPhusePole ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_SelectByXCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlCommand.Transaction = _transaction;

            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();

            return phusePole;
        }

        //ShareOnServer
        public static EPhusePole ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            SqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();

            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phusePole.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : phuse pole\n");
            }
            reader.Close();
            return phusePole;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~Local PArt~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);
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
                        //ed.WriteMessage("1\n");
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);
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
                        //ed.WriteMessage("2\n");
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
                    ed.WriteMessage(string.Format("Error EPhusePole.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhusePole.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            ////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.Insert : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            ed.WriteMessage("TR comment : {0} \n", comment);
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Transaction = _transaction;
            try
            {
                //connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);
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
                ed.WriteMessage(string.Format("Error EPhusePole.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            ////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.Insert : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("++++++++++ insert\n");
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.PhusePole, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in PhusePole");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PhusePole, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhusePole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhusePole.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //sqlCommand.ExecuteNonQuery();
                //sqlCommand.Parameters.Clear();
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = "SELECT @@IDENTITY";
                sqlCommand.ExecuteNonQuery();
                ed.WriteMessage("++++++++++ update\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.PhusePole))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.PhusePole, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in PhusePole");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PhusePole");
                    }
                }
                ed.WriteMessage("ephusepole.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PhusePole, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PhusePole failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhusePole.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
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
                    ed.WriteMessage(string.Format("Error EPhusePole.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EPhusePole.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }

            //////////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.UpdateX : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("__________ XCode.update:{0}\n", XCode);
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iMark", Mark));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Transaction = _transaction;
            try
            {
                //connection.Open();
                sqlCommand.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                //if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole)))
                //{

                //    while (canCommitTransaction && Counter < operationList.Count)
                //    {

                //        Atend.Base.Equipment.EOperation _EOperation;
                //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                //        _EOperation.XCode = XCode;
                //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole);

                //        if (_EOperation.InsertX(_transaction, Connection) && canCommitTransaction)
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
                    //Connection.Close();
                    return true;
                }
                else
                {
                    //_transaction.Rollback();
                    //Connection.Close();
                    return false;
                    //throw new Exception("can not commit transaction");

                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EPhuse.UpdateX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }


            ////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.UpdateX : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.PhusePole);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PhusePole_DeleteX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EPhusePole.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhusePole.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }


            //////////////////////
            //try
            //{
            //    Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.DeleteX : {0} \n", ex1.Message));

            //    Connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_PhusePole_DeleteX", Connection);
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
                    if (EOperation.DeleteX(_transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole)))
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
                    ed.WriteMessage(string.Format("Error EPhusePole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPhusePole.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            ////////////////////////
            //try
            //{
            //    //Connection.Open();
            //    sqlCommand.ExecuteNonQuery();
            //    //Connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage(string.Format("Error EPhusePole.DeleteX : {0} \n", ex1.Message));

            //    //Connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess //AcDrawDB
        public static EPhusePole SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_SelectByXCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phusePole.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            SqlCommand.Parameters.Clear();
            SqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", phusePole.XCode));
            SqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhusePole));
            reader = SqlCommand.ExecuteReader();
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
            SqlCommand.Parameters.Clear();
            SqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", phusePole.XCode));
            SqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhusePole));
            reader = SqlCommand.ExecuteReader();
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
            return phusePole;
        }

        //ASHKTORAB //ShareOnServer
        public static EPhusePole SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_SelectByXCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPhusePole phusePole = new EPhusePole();

            try
            {
                SqlCommand.Transaction = LocalTransaction;
                SqlDataReader reader = SqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                    phusePole.XCode = new Guid(reader["XCode"].ToString());
                    phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    phusePole.Comment = reader["Comment"].ToString();
                    phusePole.Name = reader["Name"].ToString();
                    phusePole.Mark = reader["Mark"].ToString();
                    phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    phusePole.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EPhusePole.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return phusePole;
        }

        //ASHKTORAB
        public static EPhusePole SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_SelectByXCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlCommand.Transaction = _transaction;
            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phusePole.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            SqlCommand.Parameters.Clear();
            SqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", phusePole.XCode));
            SqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhusePole));
            reader = SqlCommand.ExecuteReader();
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
            SqlCommand.Parameters.Clear();
            SqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            SqlCommand.Parameters.Add(new SqlParameter("iXCode", phusePole.XCode));
            SqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PhusePole));
            reader = SqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            return phusePole;

        }

        //ASHKTORAB
        public static EPhusePole SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand SqlCommand = new SqlCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            SqlCommand.Transaction = _transaction;
            SqlDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phusePole.Code = -1;
            }

            reader.Close();

            return phusePole;

        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhusePole_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PhusePole_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PhusePole_SearchByName", connection);
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
        public static EPhusePole CheckForExist(double _Amper, string _Mark)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PhusePole_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));
            command.Parameters.Add(new SqlParameter("iMark", _Mark));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.XCode = new Guid(reader["XCode"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                phusePole.Code = -1;
            }
            reader.Close();
            connection.Close();
            return phusePole;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));

            try
            {
                Connection.Open();
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.AccessInsert : {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess //AcDrawDB
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PhusePole);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.PhusePole, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PhusePole failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection Connection = _NewConnection;
            OleDbCommand sqlCommand = new OleDbCommand("E_PhusePole_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _NewTransaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iAmper", Amper));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iMark", Mark));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.PhusePole);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.PhusePole, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PhusePole failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPhusePole.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public static EPhusePole AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.XCode = new Guid(reader["XCode"].ToString());

            }
            else
            {
                phusePole.Code = -1;
            }
            return phusePole;

        }


        public static EPhusePole AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Transaction = _transaction;
            SqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.XCode = new Guid(reader["XCode"].ToString());

            }
            else
            {
                phusePole.Code = -1;
            }
            reader.Close();
            return phusePole;

        }

        //status report
        public static EPhusePole AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_Select", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.XCode = new Guid(reader["XCode"].ToString());

            }
            else
            {
                phusePole.Code = -1;
            }
            reader.Close();
            return phusePole;

        }


        public static EPhusePole AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_SelectByXCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = SqlCommand.ExecuteReader();
            EPhusePole phusePole = new EPhusePole();
            if (reader.Read())
            {
                phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                phusePole.Comment = reader["Comment"].ToString();
                phusePole.Name = reader["Name"].ToString();
                phusePole.Mark = reader["Mark"].ToString();
                phusePole.XCode = new Guid(reader["XCode"].ToString());

            }
            reader.Close();
            Connection.Close();
            return phusePole;

        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EPhusePole AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection Connection = _connection;
        //    OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_SelectByXCode", Connection);
        //    SqlCommand.CommandType = CommandType.StoredProcedure;
        //    SqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    SqlCommand.Transaction = _transaction;
        //    OleDbDataReader reader = SqlCommand.ExecuteReader();
        //    EPhusePole phusePole = new EPhusePole();
        //    if (reader.Read())
        //    {
        //        phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
        //        phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
        //        phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        phusePole.Comment = reader["Comment"].ToString();
        //        phusePole.Name = reader["Name"].ToString();
        //        phusePole.Mark = reader["Mark"].ToString();
        //        phusePole.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        phusePole.Code = -1;
        //    }
        //    reader.Close();
        //    return phusePole;

        //}

        public static EPhusePole AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand SqlCommand = new OleDbCommand("E_PhusePole_SelectByProductCode", Connection);
            SqlCommand.CommandType = CommandType.StoredProcedure;
            SqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EPhusePole phusePole = new EPhusePole();
            try
            {
                Connection.Open();
                OleDbDataReader reader = SqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    phusePole.Amper = Convert.ToDouble(reader["Amper"].ToString());
                    phusePole.Code = Convert.ToInt32(reader["Code"].ToString());
                    phusePole.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    phusePole.Comment = reader["Comment"].ToString();
                    phusePole.Name = reader["Name"].ToString();
                    phusePole.Mark = reader["Mark"].ToString();

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
                ed.WriteMessage(string.Format("Error EPhusePole.SelectByProductCode : {0} \n", ex1.Message));
                Connection.Close();
            }
            return phusePole;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PhusePole_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PhusePole_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsPhusePole = new DataSet();
            adapter.Fill(dsPhusePole);
            return dsPhusePole.Tables[0];
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

        //    //int DeletedCode = 0;

        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();

        //        EPhusePole _EPhusePole = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Code = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            //ed.WriteMessage("\n111\n");

        //            EPhusePole Ap = Atend.Base.Equipment.EPhusePole.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                Code = Ap.Code;
        //                if (!Atend.Base.Equipment.EPhusePole.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }
        //            DataTable OperationTbl = new DataTable();
        //            _EPhusePole.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPhusePole.XCode);
        //            _EPhusePole.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EPhusePole.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, Code, _EPhusePole.Code, (int)Atend.Control.Enum.ProductType.PhusePole))
        //                {
        //                    if (Atend.Base.Equipment.EPhuse.Update(Servertransaction, Serverconnection, _EPhusePole.Code, Code))
        //                    {
        //                        if (!_EPhusePole.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");

        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                        else
        //                        {

        //                            //ed.WriteMessage("\n112\n");

        //                            //Servertransaction.Commit();
        //                            //Serverconnection.Close();

        //                            //Localtransaction.Commit();
        //                            //Localconnection.Close();
        //                        }

        //                    }
        //                    else
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(Code, _EPhusePole.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPhusePole.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            //ed.WriteMessage("\n111\n");

        //            EPhusePole Ap = Atend.Base.Equipment.EPhusePole.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPhusePole.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPhusePole.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPhusePole.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
