using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Equipment
{
    public class EKhazan
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

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private string maker;
        public string Maker
        {
            get { return maker; }
            set { maker = value; }
        }

        private double capacity;
        public double Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        private int amper;
        public int Amper
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


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Enter Insert Method\n");

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));

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
                ed.WriteMessage(string.Format(" ERROR EKhazan.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Khazan, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in Khazan");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Khazan, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Khazan failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.ServerInsert : {0} \n", ex2.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazan_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Khazan))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Khazan, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in Khazan");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Khazan");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Khazan, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Khazan failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.ServerUpdate : {0} \n", ex2.Message));
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Enter Insert Method\n");

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    //ed.WriteMessage("Exec\n");
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    //ed.WriteMessage("After Exec\n");
                    //ed.WriteMessage("ContainerCode=", ContainerCode.ToString() + "\n");
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);
                        //_EOperation.ProductID = 
                        if (_EOperation.Insert(transaction, connection) && canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlTransaction _transaction;
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Khazan_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iMaker", Maker));
        //    command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));
        //    command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iAmper", Amper));


        //    try
        //    {
        //        connection.Open();
        //        _transaction = connection.BeginTransaction();
        //        command.Transaction = _transaction;

        //        try
        //        {
        //            //ed.WriteMessage("1");
        //            command.ExecuteNonQuery();
        //            bool canCommitTransaction = true;
        //            int Counter = 0;


        //            Counter = 0;
        //            if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
        //            {

        //                while (canCommitTransaction && Counter < operationList.Count)
        //                {

        //                    Atend.Base.Equipment.EOperation _EOperation;
        //                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
        //                    _EOperation.ProductCode = code;
        //                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);

        //                    if (_EOperation.Insert(_transaction, connection) && canCommitTransaction)
        //                    {
        //                        canCommitTransaction = true;
        //                    }
        //                    else
        //                    {
        //                        canCommitTransaction = false;
        //                    }
        //                    Counter++;
        //                }
        //            }
        //            ed.WriteMessage("2 \n");
        //            if (canCommitTransaction)
        //            {
        //                _transaction.Commit();
        //                connection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                _transaction.Rollback();
        //                connection.Close();
        //                return false;
        //                //throw new Exception("can not commit transaction");

        //            }


        //        }
        //        catch (Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex1.Message));
        //            _transaction.Rollback();
        //            connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (Exception ex2)
        //    {
        //        ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex2.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_Delete", connection);
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


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
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
                    ed.WriteMessage(string.Format("Error EKhazan.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKhazan.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Khazan_Delete", connection);
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


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
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
                    ed.WriteMessage(string.Format("Error EKhazan.ServerDelete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKhazan.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EKhazan SelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }

            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new SqlParameter("iProductCode", khazan.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            connection.Close();
            return khazan;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                //khazan.Code = Convert.ToInt32(reader["Code"]);
                Capacity = Convert.ToDouble(reader["Capacity"]);
                Comment = reader["Comment"].ToString();
                Maker = reader["Maker"].ToString();
                Name = reader["Name"].ToString();
                //khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                Vol = Convert.ToDouble(reader["Vol"]);
                Amper = Convert.ToInt32(reader["Amper"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }

            // for check boxes
            reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", khazan.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();

            connection.Close();
            //return khazan;
        }

        public static EKhazan SelectByProductCode(int ProductCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EKhazan khazan = new EKhazan();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    khazan.Code = Convert.ToInt32(reader["Code"]);
                    khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                    khazan.Comment = reader["Comment"].ToString();
                    khazan.Maker = reader["Maker"].ToString();
                    khazan.Name = reader["Name"].ToString();
                    khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    khazan.Vol = Convert.ToDouble(reader["Vol"]);
                    khazan.Amper = Convert.ToInt32(reader["Amper"]);
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
                ed.WriteMessage(string.Format("Error EKhazan.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return khazan;
        }

        public static EKhazan SelectByCodeOnly(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }

            reader.Close();
            connection.Close();
            return khazan;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        public static DataTable DrawSearch(int ContainerPackageCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];


        }

        public static DataTable DrawSearch(Guid ContainerPackageCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", ContainerPackageCode));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];


        }

        //ASHKTORAB
        public static EKhazan ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            command.Transaction = _transaction;

            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }

            // for check boxes
            reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();

            //connection.Close();
            return khazan;
        }

        //MEDHAT //ShareOnServer
        public static EKhazan ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed =Autodesk.AutoCAD.ApplicationServices. Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Transaction = ServerTransaction;

            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();

            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                khazan.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : khazan\n");
            }
            reader.Close();
            return khazan;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Enter Insert Method\n");

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
                    //ed.WriteMessage("Exec\n");
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    //ed.WriteMessage("After Exec\n");
                    //ed.WriteMessage("ContainerCode=", ContainerCode.ToString() + "\n");
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
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

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);
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
                    ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Enter Insert Method\n");

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    //ed.WriteMessage("Exec\n");
                    ContainerCode = Convert.ToInt32(command.ExecuteScalar());
                    //ed.WriteMessage("After Exec\n");
                    //ed.WriteMessage("ContainerCode=", ContainerCode.ToString() + "\n");
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(transaction, connection) && canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During Khazan Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazan_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Khazan, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in Khazan");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Khazan, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Khazan failed");
                    }
                }
                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.LocalInsertX : {0} \n", ex2.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazan_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Khazan))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Khazan, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in Khazan");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Khazan");
                    }
                }
                ed.WriteMessage("EKhazan.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Khazan, XCode, ServerTransaction, ServerConnection, _transaction , _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Khazan failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.LocalUpdate : {0} \n", ex2.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));
            command.Parameters.Add(new SqlParameter("iCode", Code));

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
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));
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
                    ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex2.Message));
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
            SqlCommand command = new SqlCommand("E_Khazan_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));


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


                    Counter = 0;
                    //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        //_EOperation.ProductCode = 0;
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan);

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
                    //ed.WriteMessage("2 \n");
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
                    ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex1.Message));
                    //_transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.Update : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Khazan);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EKhazan.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKhazan.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _Transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_DeleteX", connection);
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


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan)))
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
                    ed.WriteMessage(string.Format("Error EKhazan.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EKhazan.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EKhazan SelectByXCode(Guid XCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }
            else
            {
                khazan.Code = -1;
            }
            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
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
            command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeys.Add(Op);
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            connection.Close();
            return khazan;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EKhazan SelectByXCodeForDesign(Guid XCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }
            else
            {
                khazan.Code = -1;
            }
            // for check boxes
            reader.Close();
            connection.Close();
            return khazan;
        }

        //ASHKTORAB //ShareOnServer
        public static EKhazan SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {

            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EKhazan khazan = new EKhazan();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    khazan.Code = Convert.ToInt32(reader["Code"]);
                    khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                    khazan.Comment = reader["Comment"].ToString();
                    khazan.Maker = reader["Maker"].ToString();
                    khazan.Name = reader["Name"].ToString();
                    khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    khazan.Vol = Convert.ToDouble(reader["Vol"]);
                    khazan.Amper = Convert.ToInt32(reader["Amper"]);
                    khazan.XCode = new Guid(reader["XCode"].ToString());
                    khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage("aaa\n");
                }
                else
                {
                    khazan.Code = -1;
                }
                // for check boxes
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EKhazan.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }
            return khazan;
        }



        //ASHKTORAB
        public static EKhazan SelectByXCode(SqlTransaction _Transaction, SqlConnection _connection, Guid XCode)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _Transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }

            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
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
            command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            //connection.Close();
            return khazan;
        }

        //ASHKTORAB
        public static EKhazan SelectByCodeForLocal(int Code, SqlTransaction _Transaction, SqlConnection _connection)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _Transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("aaa\n");
            }
            else
            {
                khazan.Code = -1;
            }

            // for check boxes
            reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", khazan.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();

            //connection.Close();
            return khazan;
        }

        //Hatami
        public static DataTable DrawSearchX(Guid ContainerPackageCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", ContainerPackageCode));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];


        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Khazan_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_SearchByName", connection);
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
        public static EKhazan CheckForExist(double _Capacity, double _Vol, int _Amper)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazan_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCapacity", _Capacity));
            command.Parameters.Add(new SqlParameter("iVol", _Vol));
            command.Parameters.Add(new SqlParameter("iAmper", _Amper));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
                khazan.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                khazan.Code = -1;
            }
            reader.Close();
            connection.Close();
            return khazan;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaker", Maker));
            command.Parameters.Add(new SqlParameter("iCapacity", Capacity));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iAmper", Amper));

            try
            {
                connection.Open();
                //transaction = connection.BeginTransaction();
                //command.Transaction = transaction;

                //ed.WriteMessage("Exec\n");
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.AccessInsert : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iMaker", Maker));
            command.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));

            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Khazan);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Khazan, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Khazan failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.AccessInsert : {0} \n", ex2.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            //ed.WriteMessage("Enter Insert Method\n");

            //SqlTransaction transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection ;
            OleDbCommand command = new OleDbCommand("E_Khazan_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iMaker", Maker));
            command.Parameters.Add(new OleDbParameter("iCapacity", Capacity));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAmper", Amper));
            int oldCode = Code;
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Khazan);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode (Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.Khazan, Code, _Oldtransaction,_Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Khazan failed");
                    }
                }



                return true;
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Khazan.AccessInsert : {0} \n", ex2.Message));
                return false;
            }
        }

        //StatusReport
        public static EKhazan AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                khazan.Code = -1;
            }

            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", khazan.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            connection.Close();
            return khazan;
        }

        public static EKhazan AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                khazan.Code = -1;
            }

            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", khazan.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Khazan));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeys.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            return khazan;
        }

        public static EKhazan AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {

            OleDbConnection connection = _connection ;
            OleDbCommand command = new OleDbCommand("E_Khazan_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                khazan.Code = -1;
            }

          
            reader.Close();

            //connection.Close();
            return khazan;
        }

        public static EKhazan AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Khazan_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EKhazan khazan = new EKhazan();
            if (reader.Read())
            {
                khazan.Code = Convert.ToInt32(reader["Code"]);
                khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                khazan.Comment = reader["Comment"].ToString();
                khazan.Maker = reader["Maker"].ToString();
                khazan.Name = reader["Name"].ToString();
                khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                khazan.Vol = Convert.ToDouble(reader["Vol"]);
                khazan.Amper = Convert.ToInt32(reader["Amper"]);
                khazan.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                khazan.Code = -1;
            }

            // for check boxes
            reader.Close();
            connection.Close();
            return khazan;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EKhazan AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connectin)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connectin;
        //    OleDbCommand command = new OleDbCommand("E_Khazan_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EKhazan khazan = new EKhazan();
        //    if (reader.Read())
        //    {
        //        khazan.Code = Convert.ToInt32(reader["Code"]);
        //        khazan.Capacity = Convert.ToInt32(reader["Capacity"]);
        //        khazan.Comment = reader["Comment"].ToString();
        //        khazan.Maker = reader["Maker"].ToString();
        //        khazan.Name = reader["Name"].ToString();
        //        khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
        //        khazan.Vol = Convert.ToDouble(reader["Vol"]);
        //        khazan.Amper = Convert.ToInt32(reader["Amper"]);
        //        khazan.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        khazan.Code = -1;
        //    }
        //    // for check boxes
        //    reader.Close();
        //    return khazan;
        //}

        public static EKhazan AccessSelectByProductCode(int ProductCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Khazan_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EKhazan khazan = new EKhazan();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    khazan.Code = Convert.ToInt32(reader["Code"]);
                    khazan.Capacity = Convert.ToDouble(reader["Capacity"]);
                    khazan.Comment = reader["Comment"].ToString();
                    khazan.Maker = reader["Maker"].ToString();
                    khazan.Name = reader["Name"].ToString();
                    khazan.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    khazan.Vol = Convert.ToDouble(reader["Vol"]);
                    khazan.Amper = Convert.ToInt32(reader["Amper"]);
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
                ed.WriteMessage(string.Format("Error EKhazan.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return khazan;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Khazan_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Khazan_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
        }

        public static DataTable AccessDrawSearch(int ContainerPackageCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Khazan_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerPackageCode));
            DataSet dsKhazan = new DataSet();
            adapter.Fill(dsKhazan);
            return dsKhazan.Tables[0];
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

        //        EKhazan _EKhazan = SelectByXCode(Localtransaction, Localconnection, XCode);


        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));
        //            //ed.WriteMessage("\n111\n");

        //            EKhazan Ap = Atend.Base.Equipment.EKhazan.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EKhazan.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EKhazan.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EKhazan.XCode);
        //            _EKhazan.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EKhazan.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EKhazan.Code, (int)Atend.Control.Enum.ProductType.Khazan))
        //                {
        //                    if (!_EKhazan.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EKhazan.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Khazan, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EKhazan.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));
        //            //ed.WriteMessage("\n111\n");

        //            EKhazan Ap = Atend.Base.Equipment.EKhazan.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EKhazan.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EKhazan.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Khazan, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EKhazan.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
