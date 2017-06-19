using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class ERod
    {
        public ERod()
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

        private double vol;
        public double Vol
        {
            get { return vol; }
            set { vol = value; }
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

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
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

        private int containerCode;
        public int ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~/
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                // Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR ERod.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));




            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;


                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(transaction, connection) && canCommitTransaction)
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

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During Pole Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Pole Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Rod_Insert", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Rod, LocalTransaction, LocalConnection);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Rod, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Rod failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Rod_Update", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Rod))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Rod, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Deleted Operation Failed in Rod");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Rod, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Rod failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        //public  bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Rod_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));
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
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("Error ERod.Insert : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));


            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.ProductCode = code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);

                            if (_EOperation.Insert(_transaction, connection) && canCommitTransaction)
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
                    //ed.writeMessage("2 \n");
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
                    ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Rod_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));
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
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("Error ERod.Update : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
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
                    ed.WriteMessage(string.Format("Error ERod.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ERod.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
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
                    ed.WriteMessage(string.Format("Error ERod.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ERod.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //public static  bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Rod_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        command.Clone();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("Error ERod.Delete : {0} \n", ex1.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public static ERod SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
            }
            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", rod.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            connection.Close();
            return rod;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //ERod rod = new ERod();
            if (reader.Read())
            {
                //rod.Code = Convert.ToInt16(reader["Code"].ToString());
                //XCode = new Guid(reader["XCode"].ToString());
                //rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                Vol = Convert.ToSingle(reader["Vol"].ToString());
                Amper = Convert.ToSingle(reader["Amper"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
            }
            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", rod.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            connection.Close();
        }

        public static ERod SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Rod_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            ERod rod = new ERod();

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rod.Code = Convert.ToInt16(reader["Code"].ToString());
                    rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                    rod.Comment = reader["Comment"].ToString();
                    rod.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.writeMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERod.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return rod;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Rod_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Rod_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        public static DataTable DrawSearch(double Vol, double Amper)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Rod_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iVol", Vol));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iAmper", Amper));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        //ASHKTORAB
        public static ERod ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();

            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //connection.Close();
            return rod;
        }

        //MEDHAT //ShareOnServer
        public static ERod ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();

            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                rod.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : rod\n");
            }
            reader.Close();
            return rod;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
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
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    //Package
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);
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
                            ed.WriteMessage("Error For Insert \n");
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
                    ed.WriteMessage(string.Format("Error Occured During Pole Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Pole Insertion 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));




            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During Pole Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Pole Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Rod_InsertX", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Rod, ServerConnection, ServerTransaction);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Rod, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Rod failed");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Rod_UpdateX", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Rod))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Rod, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Deleted Operation Failed in Rod");
                    }
                }
                ed.WriteMessage("ERod.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code , (int)Atend.Control.Enum.ProductType.Rod, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Rod failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
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

                    //ed.writeMessage("2 \n");
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
                    ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _Transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {
                //connection.Open();
                //_transaction = connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    //Counter = 0;
                    //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Rod);

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
                    ////ed.writeMessage("2 \n");
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex1.Message));
                    //_transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Rod.Update : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Rod);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error ERod.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ERod.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod)))
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
                    ed.WriteMessage(string.Format("Error ERod.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ERod.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static ERod SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
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
            command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("ooo \n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeys.Add(Op);

                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //************
            connection.Close();
            return rod;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static ERod SelectByXCodeForDesign(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                rod.code = -1;
            }
            reader.Close();
            connection.Close();
            return rod;
        }

        //ASHKTORAB //ShareOnServer
        public static ERod SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            ERod rod = new ERod();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rod.Code = Convert.ToInt16(reader["Code"].ToString());
                    rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                    rod.Comment = reader["Comment"].ToString();
                    rod.Name = reader["Name"].ToString();
                    rod.XCode = new Guid(reader["XCode"].ToString());
                    rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    rod.code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ERod.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return rod;
        }

        public static ERod SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
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
            command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.writeMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            //***************
            //connection.Close();
            return rod;
        }

        //ASHKTORAB
        public static ERod SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                rod.Code = -1;
            }

            reader.Close();

            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", rod.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Rod));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            //connection.Close();
            return rod;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Rod_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Rod_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_SearchByName", connection);
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

        public static ERod CheckForExist(double _Vol, double _Amper)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Rod_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iVol", _Vol));
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
                rod.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
            connection.Close();
            return rod;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbTransaction transaction;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Rod_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iType", Type));


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.AccessInsert : {0} \n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Rod_Insert", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iType", Type));


            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Rod);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Rod, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Rod failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Rod_Insert", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iType", Type));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Rod);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Rod, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Rod failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ERod.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //MEDHAT //StatusReport
        public static ERod AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
            command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new OleDbParameter("iProductCode", rod.Code));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Rod));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.writeMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            connection.Close();
            return rod;
        }

        public static ERod AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
            command.Parameters.Clear();
            return rod;
        }

        public static ERod AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Rod_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
         
            //connection.Close();
            return rod;
        }

        public static ERod AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Rod_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ERod rod = new ERod();
            if (reader.Read())
            {
                rod.Code = Convert.ToInt16(reader["Code"].ToString());
                rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                rod.Comment = reader["Comment"].ToString();
                rod.Name = reader["Name"].ToString();
                rod.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                rod.Code = -1;
            }
            reader.Close();
            connection.Close();
            return rod;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ERod AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Rod_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ERod rod = new ERod();
        //    if (reader.Read())
        //    {
        //        rod.Code = Convert.ToInt16(reader["Code"].ToString());
        //        rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
        //        rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
        //        rod.Comment = reader["Comment"].ToString();
        //        rod.Name = reader["Name"].ToString();
        //        rod.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        rod.Code = -1;
        //    }
        //    reader.Close();
        //    return rod;
        //}

        public static ERod AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Rod_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            ERod rod = new ERod();

            try
            {

                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rod.Code = Convert.ToInt16(reader["Code"].ToString());
                    rod.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    rod.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    rod.Amper = Convert.ToSingle(reader["Amper"].ToString());
                    rod.Comment = reader["Comment"].ToString();
                    rod.Name = reader["Name"].ToString();
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.writeMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERod.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return rod;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Rod_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Rod_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsRod = new DataSet();
            adapter.Fill(dsRod);
            return dsRod.Tables[0];
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

        //        ERod catout = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
        //            ERod Ap = Atend.Base.Equipment.ERod.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.ERod.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
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
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, catout.Code, (int)Atend.Control.Enum.ProductType.Rod))
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
        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, catout.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Rod, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EROD.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;


        //    //*********************************************
        //    //SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    //SqlTransaction Servertransaction;

        //    //SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    //SqlTransaction Localtransaction;B

        //    //int DeletedCode = 0;

        //    //try
        //    //{
        //    //    Serverconnection.Open();
        //    //    Servertransaction = Serverconnection.BeginTransaction();

        //    //    Localconnection.Open();
        //    //    Localtransaction = Localconnection.BeginTransaction();

        //    //    ERod _ERod = SelectByXCode(Localtransaction, Localconnection, XCode);

        //    //    bool canCommitTransaction = true;
        //    //    int Counter = 0;

        //    //    try
        //    //    {

        //    //        Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
        //    //        ed.WriteMessage("\n111\n");

        //    //        ERod Ap = Atend.Base.Equipment.ERod.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //    //        if (Ap.XCode != Guid.Empty)
        //    //        {
        //    //            DeletedCode = Ap.Code;

        //    //            if (!Atend.Base.Equipment.ERod.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //    //            {
        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();

        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //            }
        //    //        }
        //    //        DataTable OperationTbl = new DataTable();
        //    //        _ERod.OperationList = new ArrayList();
        //    //        OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _ERod.XCode);
        //    //        _ERod.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //    //        if (_ERod.Insert(Servertransaction, Serverconnection))
        //    //        {
        //    //            if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _ERod.Code, (int)Atend.Control.Enum.ProductType.Rod))
        //    //            {
        //    //                if (!_ERod.UpdateX(Localtransaction, Localconnection))
        //    //                {
        //    //                    ed.WriteMessage("\n115\n");

        //    //                    Servertransaction.Rollback();
        //    //                    Serverconnection.Close();
        //    //                    Localtransaction.Rollback();
        //    //                    Localconnection.Close();
        //    //                    return false;
        //    //                }
        //    //                else
        //    //                {
        //    //                    ed.WriteMessage("\n112\n");

        //    //                    //Servertransaction.Commit();
        //    //                    //Serverconnection.Close();

        //    //                    //Localtransaction.Commit();
        //    //                    //Localconnection.Close();
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                Servertransaction.Rollback();
        //    //                Serverconnection.Close();
        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //                return false;
        //    //            }


        //    //        }
        //    //        else
        //    //        {
        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();
        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }

        //    //        if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ERod.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Rod, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //    //        {
        //    //            ed.WriteMessage("\n113\n");

        //    //            Servertransaction.Commit();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Commit();
        //    //            Localconnection.Close();
        //    //        }
        //    //        else
        //    //        {
        //    //            ed.WriteMessage("\n114\n");

        //    //            Servertransaction.Rollback();
        //    //            Serverconnection.Close();

        //    //            Localtransaction.Rollback();
        //    //            Localconnection.Close();
        //    //            return false;
        //    //        }


        //    //    }
        //    //    catch (System.Exception ex1)
        //    //    {
        //    //        ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //    //        Servertransaction.Rollback();
        //    //        Serverconnection.Close();

        //    //        Localtransaction.Rollback();
        //    //        Localconnection.Close();
        //    //        return false;
        //    //    }


        //    //}
        //    //catch (System.Exception ex1)
        //    //{
        //    //    ed.WriteMessage(string.Format(" ERROR EMiddleGroundCabel.ShareOnServer {0}\n", ex1.Message));

        //    //    Serverconnection.Close();
        //    //    Localconnection.Close();
        //    //    return false;
        //    //}

        //    //return true;
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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
        //            ERod Ap = Atend.Base.Equipment.ERod.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ERod.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.ERod.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Rod, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EROD.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;



        //    //************************************************
        //    //SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    //SqlTransaction Localtransaction;

        //    //Guid DeletedXCode = Guid.NewGuid();

        //    //try
        //    //{

        //    //    Localconnection.Open();
        //    //    Localtransaction = Localconnection.BeginTransaction();


        //    //    try
        //    //    {

        //    //        Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));
        //    //        ed.WriteMessage("\n111\n");

        //    //        ERod Ap = Atend.Base.Equipment.ERod.SelectByCode(Localtransaction, Localconnection, Code);

        //    //        if (Ap.XCode != Guid.Empty)
        //    //        {
        //    //            DeletedXCode = Ap.XCode;
        //    //            if (!Atend.Base.Equipment.ERod.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //    //            {
        //    //                Localtransaction.Rollback();
        //    //                Localconnection.Close();
        //    //            }

        //    //            Ap.ServerSelectByCode(Code);
        //    //        }
        //    //        else
        //    //        {
        //    //            Ap = Atend.Base.Equipment.ERod.SelectByCode(Code);
        //    //            Ap.XCode = DeletedXCode;
        //    //        }

        //    //        Ap.OperationList = new ArrayList();
        //    //        DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Rod));

        //    //        Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //    //        if (Ap.InsertX(Localtransaction, Localconnection))
        //    //        {

        //    //            Localtransaction.Commit();
        //    //            Localconnection.Close();
        //    //            return true;
        //    //        }
        //    //        else
        //    //        {
        //    //            ed.WriteMessage("\n114\n");

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
        //    //    ed.WriteMessage(string.Format(" ERROR ERod.GetFromServer {0}\n", ex1.Message));

        //    //    Localconnection.Close();
        //    //    return false;
        //    //}

        //    //return true;
        //}


    }
}
