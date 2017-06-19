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
    public class EKhazanTip
    {

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


        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
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

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackage = new ArrayList();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            int containerCode;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                ed.WriteMessage(string.Format(" ERROR EKhazanTip.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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

                    Code = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan);
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
                    //        ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
                    //}

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
                    ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Insert {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }

        }
        //HATAMI
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in EKHAZANTIP");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.ServerInsert : {0} \n", ex.Message));
                return false;
            }

        }


        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazantip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.BankKhazan))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in EKHAZANTIP");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in KhazanTip");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer KhazanTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.ServerUpdate : {0} \n", ex.Message));
                return false;
            }

        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));
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
                    if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Delete", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));

                    if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan))))
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

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Khazan.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_DeleteX", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));

                    if ((EContainerPackage.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan))))
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

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Khazan.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        public static EKhazanTip SelectByCodeOnly(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());

            }
            dataReader.Close();
            connection.Close();
            return khazantip;
        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            //EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                //khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                Name = Convert.ToString(dataReader["Name"].ToString());
                IsDefault = Convert.ToBoolean(dataReader["IsDefault"].ToString());
                Description = dataReader["Description"].ToString();

            }
            dataReader.Close();
            connection.Close();
            //return khazantip;
        }

        public static DataTable DrawSearch(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        public static EKhazanTip ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            command.Transaction = _transaction;

            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }

            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", khazantip.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["XCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            //connection.Close();
            return khazantip;
        }


        //MEDHAT
        public static EKhazanTip ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Transaction = ServerTransaction;

            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();

            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            else
            {
                khazantip.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : khazan tip\n");
            }

            dataReader.Close();
            return khazantip;
        }

        //~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazantip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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

                    containerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan);
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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Insert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Khazantip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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

                    containerCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan);
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
                    //        ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
                    //}

                    if (canCommitTransaction)
                    {
                        //transaction.Commit();
                        //connection.Close();
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
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Insert {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazantip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.BankKhazan, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in EKHAZANTIP");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.BankKhazan, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.LocalInsertX : {0} \n", ex.Message));
                return false;
            }

        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Khazantip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.BankKhazan))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.BankKhazan, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in EKHAZANTIP");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in KhazanTip");
                    }
                }
                ed.WriteMessage("Ekhazantip.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.BankKhazan, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer KhazanTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.LocalUpdate : {0} \n", ex.Message));
                return false;
            }

        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan));
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
                    if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            _EProductPackage.ContainerPackageCode = containerPackageCode;

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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool UpdateX(SqlTransaction _Transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan));
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
                    //if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
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
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    //connection.Close();
                    //_transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR KhazanTip.Update {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_DeleteX", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));

                    if ((EContainerPackage.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan))))
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

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Khazan.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _Transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_DeleteX", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Khazan));

                    if ((EContainerPackage.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan))))
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
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Khazan.Delete: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static EKhazanTip SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();

            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iCode", khazantip.code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["ProductCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            connection.Close();
            return khazantip;
        }

        public static EKhazanTip SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", khazantip.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["XCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            connection.Close();
            return khazantip;
        }

        //ASHKTORAB
        public static EKhazanTip SelectByXCode(SqlTransaction _Transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Transaction = _Transaction;

            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            dataReader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", khazantip.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            dataReader = command.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (dataReader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

                nodeKeysEPackage.Add(dataReader["XCode"].ToString());
                nodeTypeEPackage.Add(dataReader["TableType"].ToString());
                nodeCountEPackage.Add(dataReader["Count"].ToString());

            }

            dataReader.Close();
            //connection.Close();
            return khazantip;
        }

        //ASHKTORAB
        public static EKhazanTip SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EKhazanTip khazantip = new EKhazanTip();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                    khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                    khazantip.Description = dataReader["Description"].ToString();
                    khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                    khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
                }
                dataReader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EKHazanTip.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return khazantip;
        }



        //ASHKTORAB
        public static EKhazanTip SelectByCodeForLocal(int Code, SqlTransaction _Transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _Transaction;

            //connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            else
            {
                khazantip.Code = -1;
            }

            dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", khazantip.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            //dataReader = command.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (dataReader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

            //    nodeKeysEPackage.Add(dataReader["XCode"].ToString());
            //    nodeTypeEPackage.Add(dataReader["TableType"].ToString());
            //    nodeCountEPackage.Add(dataReader["Count"].ToString());

            //}

            //dataReader.Close();
            //connection.Close();
            return khazantip;
        }



        public static DataTable DrawSearchX(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);
            return dskhazantip.Tables[0];
        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_KhazanTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_KhazanTip_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iDescription", Description));

            try
            {
                //if (connection.State == ConnectionState.Closed)
                //{
                //    connection.Open();
                //}

                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.AccessInsert : {0} \n", ex.Message));
                connection.Close();
                return false;
            }

        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iDescription", Description));

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.BankKhazan, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.AccessInsert : {0} \n", ex.Message));
                return false;
            }

        }

        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection ,bool BringOperation, bool BringSubEquips)
        {
            int containerCode;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Khazantip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.BankKhazan);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.BankKhazan, Code, _oldtransaction,_oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess KablSho failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EKhazanTip.AccessInsert : {0} \n", ex.Message));
                return false;
            }

        }

        //MEDHAT
        public static EKhazanTip AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
            }
            dataReader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            //command.Parameters.Add(new OleDbParameter("iXCode", khazantip.XCode));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.BankKhazan));
            //dataReader = command.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (dataReader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("Count" + dataReader["Count"].ToString() + "\n");

            //    nodeKeysEPackage.Add(dataReader["XCode"].ToString());
            //    nodeTypeEPackage.Add(dataReader["TableType"].ToString());
            //    nodeCountEPackage.Add(dataReader["Count"].ToString());

            //}

            //dataReader.Close();
            connection.Close();
            return khazantip;
        }


        public static EKhazanTip AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_KhazanTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
            }
            dataReader.Close();
        
            //connection.Close();
            return khazantip;
        }

        public static EKhazanTip AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                //khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            dataReader.Close();
            connection.Close();
            return khazantip;
        }

        //MOUSAVI
        public static EKhazanTip AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_KhazanTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Transaction = _transaction;
            OleDbDataReader dataReader = command.ExecuteReader();
            EKhazanTip khazantip = new EKhazanTip();
            if (dataReader.Read())
            {
                khazantip.code = Convert.ToInt32(dataReader["Code"].ToString());
                khazantip.Name = Convert.ToString(dataReader["Name"].ToString());
                khazantip.Description = dataReader["Description"].ToString();
                khazantip.XCode = new Guid(dataReader["XCode"].ToString());
                //khazantip.IsDefault = Convert.ToBoolean(dataReader["IsDefault"]);
            }
            else
            {
                khazantip.Code = -1;
            }
            dataReader.Close();
            return khazantip;
        }

        public static DataTable AccessSelectAll()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("SelectAccess\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_KhazanTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsKhazanTip = new DataSet();
            adapter.Fill(dsKhazanTip);
            ed.WriteMessage("Finish1\n");
            return dsKhazanTip.Tables[0];
        }

        public static DataTable AccessDrawSearch(int Code, int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_KhazanTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        //MEDHAT
        public static DataTable AccessDrawSearch()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_KhazanTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);
            return dskhazantip.Tables[0];
        }

        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Select And Merge\n");
            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = DrawSearchX("");
            ed.WriteMessage("#.{0}\n", AccTbl.Rows.Count);
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

        //        EKhazanTip _EKhazanTip = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan));
        //            ed.WriteMessage("\n111\n");

        //            EKhazanTip Ap = Atend.Base.Equipment.EKhazanTip.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EKhazanTip.ServerDelete(Servertransaction, Serverconnection, Ap.XCode))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }



        //            if (_EKhazanTip.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (!_EKhazanTip.UpdateX(Localtransaction, Localconnection))
        //                {
        //                    ed.WriteMessage("\n115\n");

        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
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


        //            ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EKhazanTip.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.BankKhazan, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EKhazanTip.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.BankKhazan));
        //            ed.WriteMessage("\n111\n");

        //            EKhazanTip Ap = Atend.Base.Equipment.EKhazanTip.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EKhazanTip.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EKhazanTip.SelectByCodeOnly(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.BankKhazan, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EKhazanTip.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
