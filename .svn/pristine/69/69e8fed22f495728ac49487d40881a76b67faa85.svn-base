using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
namespace Atend.Base.Equipment
{
    public class EHalter
    {

        public EHalter()
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

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }


        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
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

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        private double power;
        public double Power
        {
            get { return power; }
            set { power = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));
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
                ed.WriteMessage(string.Format(" ERROR EHalter.Insert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));

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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Halter);
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

                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EHalter.InsertX {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }

        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start AccessInsert \n");
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //command.ExecuteNonQuery();
                ////ed.WriteMessage("0\n");
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("1\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Halter, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        //ed.WriteMessage("2\n");
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in HALTER");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Halter, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Halter failed");
                    }
                }



                //ed.WriteMessage("halter saved \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.ServerInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start AccessInsert \n");
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Halter_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //command.ExecuteNonQuery();
                ////ed.WriteMessage("0\n");
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                //ed.WriteMessage("1\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Halter))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Halter, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            //ed.WriteMessage("2\n");
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in HALTER");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Halter");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Halter, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Halter failed");
                    }
                }



                //ed.WriteMessage("halter saved \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.ServerInsert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand command = new SqlCommand("E_Halter_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iXCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
        //        ed.WriteMessage(string.Format(" ERROR EAutoKey3p.Update {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Halter_Delete", connection);
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
                ed.WriteMessage(string.Format(" ERROR EHalter.Delete {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static EHalter SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Halter_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());

            }
            dataReader.Close();
            connection.Close();
            return Halter;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Halter_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            //EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                //Code = Convert.ToInt32(dataReader["Code"].ToString());
                //ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                //XCode = new Guid(dataReader["XCode"].ToString());
                X = Convert.ToDouble(dataReader["X"].ToString());
                Y = Convert.ToDouble(dataReader["Y"].ToString());
                //IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Name = dataReader["Name"].ToString();
                Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            dataReader.Close();
            connection.Close();

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Halter_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsHalter = new DataSet();
            adapter.Fill(dsHalter);
            return dsHalter.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Halter_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKey = new DataSet();
            adapter.Fill(dsKey);
            return dsKey.Tables[0];
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter)))
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
                ed.WriteMessage(string.Format(" ERROR EHalter.DeleteX {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static EHalter ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_SelectByXCode", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            dataReader.Close();
            //connection.Close();
            return Halter;
        }

        //MEDHAT //ShareOnServer
        public static EHalter ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Halter_Select", connection);
            command.Transaction = ServerTransaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();

            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            else
            {
                Halter.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : halter\n");
            }
            dataReader.Close();
            return Halter;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Halter_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            XCode = Guid.NewGuid();

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));
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
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Halter);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Halter);
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
                    ed.WriteMessage(string.Format("Error EHalter.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EHalter.Insert 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }

            ///////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.InsertX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Transaction = _transaction;
            try
            {
                //ed.WriteMessage("\nBefor Execute\n");
                //Connection.Open();                 
                command.ExecuteNonQuery();
                //ed.WriteMessage("\nAfter Execute\n");
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Halter);
                    //_EOperation.ProductID = 
                    //ed.WriteMessage("\nBefor Operation\n");

                    if (_EOperation.InsertX(_transaction, connection) && canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                        //ed.writeMessage("Error For Insert \n");
                    }
                    //ed.WriteMessage("\nAfter Operation\n");
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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EHalter.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Halter_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                command.ExecuteNonQuery();
                ////ed.WriteMessage("0\n");
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());
                
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Halter, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        //ed.WriteMessage("2\n");
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in HALTER");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Halter, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Halter failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.LocalInsertX : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start AccessInsert \n");
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Halter_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //command.ExecuteNonQuery();
                ////ed.WriteMessage("0\n");
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                //ed.WriteMessage("1\n");
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Halter))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Halter, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            //ed.WriteMessage("2\n");
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in HALTER");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Halter");
                    }
                }
                ed.WriteMessage("EHalter.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Halter, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Halter failed");
                    }
                }



                //ed.WriteMessage("halter saved \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.LocalUpdate : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Halter_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));
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

                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Halter);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
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
                    ed.WriteMessage(string.Format("Error EHalter.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EHalter.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }

            ///////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.UpdateX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}
        }

        //AHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iX", X));
            command.Parameters.Add(new SqlParameter("iY", Y));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p)))
                //{

                //    while (canCommitTransaction && Counter < operationList.Count)
                //    {

                //        Atend.Base.Equipment.EOperation _EOperation;
                //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                //        _EOperation.XCode = XCode;
                //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p);

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
                //if (canCommitTransaction)
                //{
                //    _transaction.Commit();
                //    connection.Close();
                //    return true;
                //}
                //else
                //{
                //    _transaction.Rollback();
                //    connection.Close();
                //    return false;
                //    //throw new Exception("can not commit transaction");

                //}
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EHalter.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }

            ////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.UpdateX {0}\n", ex1.Message));

            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Halter);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Halter_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EHalter.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHalter.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }

            /////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.DeleteX {0}\n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackage.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter)))
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
                    ed.WriteMessage(string.Format("Error EHalter.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHalter.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

            /////////////////////////
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
            //    ed.WriteMessage(string.Format(" ERROR EAutoKey3p.DeleteX {0}\n", ex1.Message));

            //    //connection.Close();
            //    return false;
        }


        //SentFromLocalToAccess //frmDrawPoleTip
        public static EHalter SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Halter_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());

            }
            else
            {
                Halter.code = -1;
            }
            dataReader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Halter.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Halter));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {
                //ed.WriteMessage("XCode={0},TableType={1}Count={2}\n", dataReader["XCode"].ToString(), dataReader["TableType"].ToString(), dataReader["Count"].ToString());
                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Halter.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Halter));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(dataReader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(dataReader["Count"].ToString());

                nodeKeys.Add(Op);
            }
            dataReader.Close();
            connection.Close();
            return Halter;
        }

        //ASHKTORAB //ShareOnServer
        public static EHalter SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Halter_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EHalter Halter = new EHalter();
            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                    Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                    Halter.XCode = new Guid(dataReader["XCode"].ToString());
                    Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                    Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                    Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                    Halter.Name = dataReader["Name"].ToString();
                    Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());

                }
                else
                {
                    Halter.code = -1;
                }
                dataReader.Close();
               
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Isulatorchain.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
           
            return Halter;
        }


        public static EHalter SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_SelectByXCode", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            else
            {
                Halter.Code = -1;
            }
            dataReader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Halter.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Halter));
            dataReader = command.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (dataReader.Read())
            {

                nodeKeysEPackageX.Add(dataReader["XCode"].ToString());
                nodeTypeEPackageX.Add(dataReader["TableType"].ToString());
                nodeCountEPackageX.Add(dataReader["Count"].ToString());
            }
            dataReader.Close();
            //**************
            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Halter.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Halter));
            dataReader = command.ExecuteReader();
            nodeKeys.Clear();
            while (dataReader.Read())
            {
                nodeKeys.Add(dataReader["ProductID"].ToString());
            }
            dataReader.Close();
            //connection.Close();
            return Halter;
        }

        //ASHKTORAB
        public static EHalter SelectByCodeForLocal(int Code,SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Halter_Select", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            else
            {
                Halter.code = -1;
                //return autoKey_3p;//nothing operation
            }
            dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", autoKey_3p.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.AuoKey3p));
            //dataReader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (dataReader.Read())
            //{
            //    nodeKeys.Add(dataReader["ProductID"].ToString());
            //}
            //dataReader.Close();
            return Halter;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Halter_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKey = new DataSet();
            adapter.Fill(dsKey);
            return dsKey.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX //FrmEditDrawPole
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Halter_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsHalter = new DataSet();
            adapter.Fill(dsHalter);
            return dsHalter.Tables[0];
        }

        public static EHalter SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Halter_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            SqlDataReader dataReader = command.ExecuteReader();
            EHalter Halter = new EHalter();
            if (dataReader.Read())
            {
                Halter.Code = Convert.ToInt32(dataReader["Code"].ToString());
                Halter.ProductCode = Convert.ToInt32(dataReader["ProductCode"].ToString());
                Halter.XCode = new Guid(dataReader["XCode"].ToString());
                Halter.X = Convert.ToDouble(dataReader["X"].ToString());
                Halter.Y = Convert.ToDouble(dataReader["Y"].ToString());
                Halter.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Halter.Name = dataReader["Name"].ToString();
                Halter.Power = Convert.ToDouble(dataReader["Power"].ToString());
            }
            else
            {
                Halter.Code = -1;
            }
            dataReader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return Halter;
        }

        public static bool GetFromBProductLocal()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DataTable bp = Atend.Base.Base.BProduct.SelectByType(Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
            //ed.WriteMessage("bp.Count={0}\n",bp.Rows.Count);
            foreach (DataRow dr in bp.Rows)
            {
                EHalter Halter = Atend.Base.Equipment.EHalter.SelectByProductCode(Convert.ToInt32(dr["ID"].ToString()));
                if (Halter.Code != -1)
                {
                    //ed.WriteMessage("ramp.Name={0}\n",ramp.Name);
                    Halter.Name = dr["Name"].ToString();
                    //ed.WriteMessage("ProductCode={0}\n",dr["ID"].ToString());
                    Halter.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    if (!Halter.UpdateX())
                        return false;

                }
                else
                {
                    Halter.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    Halter.Name = dr["Name"].ToString();
                    if (!Halter.InsertX())
                        return false;
                }
            }
            return true;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iX", X));
            command.Parameters.Add(new OleDbParameter("iY", Y));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            try
            {

                connection.Open();

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EHAlter.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start AccessInsert \n");
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iX", X));
            command.Parameters.Add(new OleDbParameter("iY", Y));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            try
            {
                command.ExecuteNonQuery();
                //ed.WriteMessage("0\n");
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("1\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Halter);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        //ed.WriteMessage("2\n");
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Halter, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Halter failed");
                    }
                }



                //ed.WriteMessage("halter saved \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }


        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("start AccessInsert \n");
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Halter_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iX", X));
            command.Parameters.Add(new OleDbParameter("iY", Y));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            int OldCode = Code;
            try
            {
                command.ExecuteNonQuery();
                //ed.WriteMessage("0\n");
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("1\n");
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Halter);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        //ed.WriteMessage("2\n");
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Halter, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Halter failed");
                    }
                }



                //ed.WriteMessage("halter saved \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EHAlter.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI->AutoPoleInstallation //StatusReport //frmDrawPoleTip
        public static EHalter AccessSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Halter_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EHalter halter = new EHalter();
            if (reader.Read())
            {
                halter.Code = Convert.ToInt16(reader["Code"].ToString());

                halter.Name = reader["Name"].ToString();
                halter.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                halter.XCode = new Guid(reader["XCode"].ToString());
                halter.X = Convert.ToDouble(reader["X"].ToString());
                halter.Y = Convert.ToDouble(reader["Y"].ToString());
                //halter.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                halter.IsDefault = false;
                halter.Power = Convert.ToDouble(reader["Power"].ToString());
                //ed.WriteMessage("halter found \n");
            }
            else
            {
                halter.Code = -1;
            }
            reader.Close();
            connection.Close();


            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return halter;
        }

        public static EHalter AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Halter_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EHalter halter = new EHalter();
            if (reader.Read())
            {
                halter.Code = Convert.ToInt16(reader["Code"].ToString());

                halter.Name = reader["Name"].ToString();
                halter.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                halter.XCode = new Guid(reader["XCode"].ToString());
                halter.X = Convert.ToDouble(reader["X"].ToString());
                halter.Y = Convert.ToDouble(reader["Y"].ToString());
                //halter.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                halter.IsDefault = false;
                halter.Power = Convert.ToDouble(reader["Power"].ToString());
                //ed.WriteMessage("halter found \n");
            }
            else
            {
                halter.Code = -1;
            }
            reader.Close();
            return halter;
        }

        public static EHalter AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Halter_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EHalter halter = new EHalter();
            if (reader.Read())
            {
                halter.Code = Convert.ToInt16(reader["Code"].ToString());

                halter.Name = reader["Name"].ToString();
                halter.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                halter.XCode = new Guid(reader["XCode"].ToString());
                halter.X = Convert.ToDouble(reader["X"].ToString());
                halter.Y = Convert.ToDouble(reader["Y"].ToString());
                //halter.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                halter.IsDefault = false;
                halter.Power = Convert.ToDouble(reader["Power"].ToString());
                //ed.WriteMessage("halter found \n");
            }
            else
            {
                halter.Code = -1;
            }
            reader.Close();
            //connection.Close();


            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return halter;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Halter_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsAutoKey_3p = new DataSet();
            adapter.Fill(dsAutoKey_3p);
            return dsAutoKey_3p.Tables[0];
        }


        public static EHalter AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Halter_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EHalter halter = new EHalter();
            if (reader.Read())
            {
                halter.Code = Convert.ToInt16(reader["Code"].ToString());
                halter.Name = reader["Name"].ToString();
                halter.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                halter.XCode = new Guid(reader["XCode"].ToString());
                halter.X = Convert.ToDouble(reader["X"].ToString());
                halter.Y = Convert.ToDouble(reader["Y"].ToString());
                //halter.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                halter.Power = Convert.ToDouble(reader["Power"].ToString());
            }
            else
            {
                halter.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("****Finish\n");



            return halter;
        }

        //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EHalter AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Halter_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EHalter eHalter = new EHalter();
        //    if (reader.Read())
        //    {
        //        eHalter.Code = Convert.ToInt16(reader["Code"].ToString());
        //        eHalter.Name = reader["Name"].ToString();
        //        eHalter.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        eHalter.XCode = new Guid(reader["XCode"].ToString());
        //        eHalter.X = Convert.ToDouble(reader["X"].ToString());
        //        eHalter.Y = Convert.ToDouble(reader["Y"].ToString());
        //        //halter.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
        //        eHalter.Power = Convert.ToDouble(reader["Power"].ToString());
        //    }
        //    else
        //    {
        //        eHalter.Code = -1;
        //    }
        //    reader.Close();
        //    return eHalter;
        //}


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

        //        EHalter Halter = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
        //            //ed.WriteMessage("\n111\n");

        //            EHalter Ap = Atend.Base.Equipment.EHalter.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EHalter.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            Halter.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, Halter.XCode);
        //            Halter.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (Halter.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, Halter.Code, (int)Atend.Control.Enum.ProductType.Halter))
        //                {

        //                    if (!Halter.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    //else
        //                    //{
        //                    //    ed.WriteMessage("\n113\n");

        //                    //    Servertransaction.Commit();
        //                    //    Serverconnection.Close();

        //                    //    Localtransaction.Commit();
        //                    //    Localconnection.Close();
        //                    //}

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, Halter.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Halter, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EHalter.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
        //            //ed.WriteMessage("\n111\n");

        //            EHalter Ap = Atend.Base.Equipment.EHalter.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EHalter.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EHalter.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Halter));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n112\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Halter, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EHalter.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
