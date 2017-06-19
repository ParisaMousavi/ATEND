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
    public class EPoleTip
    {

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid xCode;
        public Guid XCode
        {
            get { return xCode; }
            set { xCode = value; }
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

        private int halterCount;
        public int HalterCount
        {
            get { return halterCount; }
            set { halterCount = value; }
        }

        private Guid halterXID;
        public Guid HalterXID
        {
            get { return halterXID; }
            set { halterXID = value; }
        }

        private int halterID;

        public int HalterID
        {
            get { return halterID; }
            set { halterID = value; }
        }

        private Guid poleXCode;
        public Guid PoleXCode
        {
            get { return poleXCode; }
            set { poleXCode = value; }
        }

        private int poleCode;
        public int PoleCode
        {
            get { return poleCode; }
            set { poleCode = value; }
        }

        private byte factor;
        public byte Factor
        {
            get { return factor; }
            set { factor = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }


        private byte[] image;

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
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

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeIsContainerEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackage = new ArrayList();


        public static ArrayList nodeTypeEPackageX = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        //~~~~~~~~~~~~~~~~~~~~~~~ SERVER PART ~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            int containerCode;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip);
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
                    ed.WriteMessage(string.Format("Error In TransAction(poletip):{0}\n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR POLETIP.Insert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            int containerCode;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleCode", PoleCode));
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
                    Code = containerCode;
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip);
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
                    ed.WriteMessage(string.Format("Error In TransAction(poletip):{0}\n", ex1.Message));
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR POLETIP.Insert {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }
        //HATAMI
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new SqlParameter("iPoleCode", PoleCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));

            try
            {
                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PoleTip, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in PoleTip");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PoleTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.ServerInsert : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }

        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new SqlParameter("iPoleCode", PoleCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PoleTip, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in PoleTip");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PoleTip");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.PoleTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.ServerUpdate : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                    int containerPackageCode = containerPackage.Code;

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
                ////Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EPOLETIP.Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool UpdateXX(SqlTransaction transaction, SqlConnection Connection, int Code, int NewCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction = transaction;
            SqlConnection connection = Connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_UpdateXX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNewCode", NewCode));

            try
            {
                //connection.Open();
                //_transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {

                    command.ExecuteNonQuery();
                    return true;
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
                ////Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EPOLETIP.Update {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Delete", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

                    if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))))
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
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    transaction.Rollback();
                    connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR POLETIP.Delete: {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Delete", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

                    if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))))
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
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR POLETIP.Delete: {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static EPoleTip SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();
            try
            {
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                //command.Parameters.Clear();
                //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                //command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                //reader = command.ExecuteReader();
                //nodeCountEPackageX.Clear();
                //nodeKeysEPackageX.Clear();
                //nodeTypeEPackageX.Clear();
                //while (reader.Read())
                //{
                //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //    ed.WriteMessage("True\n");
                //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                //    nodeCountEPackageX.Add(reader["Count"].ToString());
                //    ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                //}

                //reader.Close();
                //connection.Close();


                //connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dspoletip = new DataSet();
            adapter.Fill(dspoletip);

            return dspoletip.Tables[0];
        }

        //ASHKTORAB
        public static EPoleTip ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //ed.WriteMessage("\naaaaaaaaa\n");

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                //ed.WriteMessage("\nbbbbbbbb\n");
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("\nccccccccc\n");

                if (reader.Read())
                {
                    //ed.WriteMessage("\ndddddddddddd\n");

                    EP.code = Convert.ToInt32(reader["Code"]);
                    //ed.WriteMessage("\neeeeeeeeee\n");
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    //ed.WriteMessage("\nffffffff\n");
                    EP.PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    //ed.WriteMessage("\nggggggggg\n");
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    //ed.WriteMessage("\nhhhhhhhhhh\n");
                    EP.HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                    //ed.WriteMessage("\niiiiiiiiii\n");
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                //command.Parameters.Clear();
                //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                //command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                //reader = command.ExecuteReader();
                //nodeCountEPackageX.Clear();
                //nodeKeysEPackageX.Clear();
                //nodeTypeEPackageX.Clear();
                //while (reader.Read())
                //{
                //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //    ed.WriteMessage("True\n");
                //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                //    nodeCountEPackageX.Add(reader["Count"].ToString());
                //    ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                //}

                //reader.Close();
                //connection.Close();


                //connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.ServerSelectByXCode:{0} \n ", ex1.Message);
            }
            //ed.WriteMessage("\njjjjjjjjjj\n");

            return EP;
        }

        //MEDHAT
        public static EPoleTip ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                }
                else
                {
                    EP.code = -1;
                    ed.WriteMessage("ServerSelectByCode found no row in : pole tip\n");
                }
                reader.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.ServerSelectByCode:{0} \n ", ex1.Message);
            }
            return EP;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            connection.Open();

            SqlCommand command = new SqlCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            //command.Transaction = _transaction;
            //ed.WriteMessage("\naaaaaaaaa\n");

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //EPoleTip EP = new EPoleTip();
            try
            {
                //ed.WriteMessage("\nbbbbbbbb\n");
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("\nccccccccc\n");

                if (reader.Read())
                {
                    //ed.WriteMessage("\ndddddddddddd\n");

                    //EP.code = Convert.ToInt32(reader["Code"]);
                    //ed.WriteMessage("\neeeeeeeeee\n");
                    //EP.XCode = new Guid(reader["XCode"].ToString());
                    Name = Convert.ToString(reader["Name"]);
                    comment = Convert.ToString(reader["Comment"]);
                    //WriteMessage("\nffffffff\n");
                    PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                    Factor = Convert.ToByte(reader["Factor"].ToString());
                    //ed.WriteMessage("\nggggggggg\n");
                    HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    //ed.WriteMessage("\nhhhhhhhhhh\n");
                    HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                    //ed.WriteMessage("\niiiiiiiiii\n");
                    //EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    code = -1;
                }

                reader.Close();
                //command.Parameters.Clear();
                //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                //command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                //reader = command.ExecuteReader();
                //nodeCountEPackageX.Clear();
                //nodeKeysEPackageX.Clear();
                //nodeTypeEPackageX.Clear();
                //while (reader.Read())
                //{
                //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //    ed.WriteMessage("True\n");
                //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                //    nodeCountEPackageX.Add(reader["Count"].ToString());
                //    ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                //}

                //reader.Close();
                //connection.Close();


                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.ServerSelectByCode:{0} \n ", ex1.Message);
                connection.Close();
            }
            //ed.WriteMessage("\njjjjjjjjjj\n");

            //return EP;
        }

        public static DataTable SelectAll()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable SelectAllX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~~ LOCAL PART ~~~~~~~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("HHH\n");
            int containerCode;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //ed.WriteMessage("Code={0}\n", Code);
            //ed.WriteMessage("XCode={0}\n",XCode);
            //ed.WriteMessage("Comment={0}\n",Comment);
            //ed.WriteMessage("HalterCount={0}\n",HalterCount);
            //ed.WriteMessage("HalterID={0}\n",HalterID);
            //ed.WriteMessage("Factor={0}\n",Factor);
            //ed.WriteMessage("PoleXCode={0}\n",PoleXCode);
            //ed.WriteMessage("IsDefault={0}\n",IsDefault);
            //ed.WriteMessage("Name={0}\n",Name);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    // ed.WriteMessage("Enter Try\n");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //ed.writeMessage("CountEquipment" + equipmentList.Count.ToString()+"\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip);
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
                        //ed.WriteMessage("ContainerPackage.Code={0}\n",containerPackage.Code);
                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
                        _EProductPackage.TableType = Convert.ToByte(Atend.Control.Enum.ProductType.Consol);
                        if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
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
                ed.WriteMessage(string.Format(" ERROR EPOLETIP.InsertX {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("HHH\n");
            int containerCode;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            //ed.WriteMessage("Code={0}\n", Code);
            //ed.WriteMessage("XCode={0}\n",XCode);
            //ed.WriteMessage("Comment={0}\n",Comment);
            //ed.WriteMessage("HalterCount={0}\n",HalterCount);
            //ed.WriteMessage("HalterID={0}\n",HalterID);
            //ed.WriteMessage("Factor={0}\n",Factor);
            //ed.WriteMessage("PoleXCode={0}\n",PoleXCode);
            //ed.WriteMessage("IsDefault={0}\n",IsDefault);
            //ed.WriteMessage("Name={0}\n",Name);
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    // ed.WriteMessage("Enter Try\n");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Counter = 0;
                    //ed.writeMessage("CountEquipment" + equipmentList.Count.ToString()+"\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    //containerPackage.ContainerCode = 0;
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip);
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
                    //    //ed.WriteMessage("ContainerPackage.Code={0}\n",containerPackage.Code);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                    //    _EProductPackage.TableType = Convert.ToByte(Atend.Control.Enum.ProductType.Consol);
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
                ed.WriteMessage(string.Format(" ERROR EPOLETIP.InsertX {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_PoleTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.PoleTip, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in PoleTip");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PoleTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.LocalInsertX : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            SqlCommand command = new SqlCommand("E_PoleTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iImage", Image));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.PoleTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.PoleTip, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in PoleTip");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in PoleTip");
                    }
                }
                ed.WriteMessage("Epoletip.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.PoleTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.LocalUpdate : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
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
                    if (EProductPackage.Delete(_transaction, connection, containerPackageCode))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
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
                    ed.WriteMessage("Error In Transaction(local):{0}", ex1.Message);
                    connection.Close();
                    _transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR EPoleTip.Update(local) {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool UpdateX(SqlTransaction _Transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new SqlParameter("iHalterID", HalterXID));
            command.Parameters.Add(new SqlParameter("iFactor", Factor));
            command.Parameters.Add(new SqlParameter("iPoleXCode", PoleXCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    //ed.WriteMessage("GOTO\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                    //int containerPackageCode = containerPackage.Code;

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
                ed.WriteMessage(string.Format(" ERROR EPoleTip.Update(local) {0}\n", ex1.Message));

                //connection.Close();
                return false;
            }
        }
        //Hatami
        public static bool DeleteX(Guid XCode)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Code={0}\n",containerPackag.Code);
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_DeleteX", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
                    //ed.WriteMessage("XCode={0}\n",XCode);
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))))
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
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction(local):{0}\n", ex1.Message);
                    transaction.Rollback();
                    connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR PoleTip.DeleteX(local): {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_DeleteX", connection);
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

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));

                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip))))
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
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("Error In TransAction(local):{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR PoleTip.DeleteX(local): {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }

        //MOUSAVi //SentFromLocalToAccess
        public static EPoleTip SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //ASHKTORAB
        public static EPoleTip SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCodeForDesign:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //HATAMI //frmDrawPoleTip
        public static EPoleTip SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);

                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                command.Parameters.Clear();
                command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                reader = command.ExecuteReader();
                nodeCountEPackageX.Clear();
                nodeKeysEPackageX.Clear();
                nodeTypeEPackageX.Clear();
                while (reader.Read())
                {
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                    //ed.WriteMessage("True\n");
                    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                    nodeCountEPackageX.Add(reader["Count"].ToString());
                    //ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                }

                reader.Close();
                connection.Close();


                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //MEDHAT
        public static EPoleTip SelectByPoleXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByPoleXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                command.Parameters.Clear();
                command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                reader = command.ExecuteReader();
                nodeCountEPackageX.Clear();
                nodeKeysEPackageX.Clear();
                nodeTypeEPackageX.Clear();
                while (reader.Read())
                {
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                    //ed.WriteMessage("True\n");
                    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                    nodeCountEPackageX.Add(reader["Count"].ToString());
                    //ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                }

                reader.Close();
                connection.Close();


                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //ASHKTORAB
        public static EPoleTip SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                //command.Parameters.Clear();
                //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                //command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                //reader = command.ExecuteReader();
                //nodeCountEPackageX.Clear();
                //nodeKeysEPackageX.Clear();
                //nodeTypeEPackageX.Clear();
                //while (reader.Read())
                //{
                //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //    ed.WriteMessage("True\n");
                //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                //    nodeCountEPackageX.Add(reader["Count"].ToString());
                //    ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                //}

                //reader.Close();
                //connection.Close();


                //connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //ASHKTORAB
        public static EPoleTip SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();
            try
            {
                //connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.PoleXCode = new Guid(reader["PoleXCode"].ToString());
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterXID = new Guid(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                    //ed.WriteMessage("EP.Comment={0}\n", EP.Comment);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                //command.Parameters.Clear();
                //command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
                //command.Parameters.Add(new SqlParameter("iXCode", EP.XCode));
                //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                //reader = command.ExecuteReader();
                //nodeCountEPackageX.Clear();
                //nodeKeysEPackageX.Clear();
                //nodeTypeEPackageX.Clear();
                //while (reader.Read())
                //{
                //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //    ed.WriteMessage("True\n");
                //    nodeKeysEPackageX.Add(reader["XCode"].ToString());
                //    nodeTypeEPackageX.Add(reader["TableType"].ToString());
                //    nodeCountEPackageX.Add(reader["Count"].ToString());
                //    ed.WriteMessage("Type:" + reader["XCode"].ToString() + "\n");
                //}

                //reader.Close();
                //connection.Close();


                //connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //HATAMI
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dspoletip = new DataSet();
            adapter.Fill(dspoletip);

            return dspoletip.Tables[0];
        }

        public static DataTable DrawSearch()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dspoletip = new DataSet();
            adapter.Fill(dspoletip);

            return dspoletip.Tables[0];
        }

        public static DataTable DrawSearchByPoleXCode()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_DrawSearchByPoleXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dspoletip = new DataSet();
            adapter.Fill(dspoletip);

            return dspoletip.Tables[0];
        }

        public static DataTable SearchByConsolType(int ConsolType)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_PoleTip_SearchByConsolType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("iConsolType", ConsolType);

            DataSet dspoletip = new DataSet();
            adapter.Fill(dspoletip);

            return dspoletip.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_PoleTip_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~ ACCESS PART ~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iFactor", Factor));
            command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new OleDbParameter("iHalterType", HalterID));
            /*Hatami*/
            command.Parameters.Add(new OleDbParameter("iPoleCode", PoleCode));
            command.Parameters.Add(new OleDbParameter("iImage", Image));
            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.AccessInsert : {0} \n", ex1.Message);
                connection.Close();
                return false;
            }

            return true;
        }

        //frmDrawPoleTip
        public static EPoleTip AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();
            try
            {
                connection.Open();
                //ed.WriteMessage("E_PoleTip.Select\n");
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //ed.WriteMessage("raeder\n");

                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    EP.PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }
                //ed.WriteMessage("TTT\n");
                reader.Close();
                //ed.WriteMessage("E_Pro\n");
                command.Parameters.Clear();
                command.CommandText = "E_ProductPackage_SelectByContainerCodeType";

                command.Parameters.Add(new OleDbParameter("iCode", EP.Code));
                command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                reader = command.ExecuteReader();
                nodeCountEPackage.Clear();
                nodeKeysEPackage.Clear();
                nodeKeysEPackage.Clear();
                while (reader.Read())
                {
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                    //ed.WriteMessage("True\n");
                    nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                    //ed.WriteMessage("bb\n");

                    nodeTypeEPackage.Add(reader["TableType"].ToString());
                    //ed.WriteMessage("cc\n");

                    nodeCountEPackage.Add(reader["Count"].ToString());
                    //ed.WriteMessage("Type:" + reader["Code"].ToString() + "\n");
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByCode:{0} \n ", ex1.Message);
            }

            return EP;
        }


        public static EPoleTip AccessSelectByCodeForConvertor(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();

            //connection.Open();
            //ed.WriteMessage("E_PoleTip.Select\n");
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //ed.WriteMessage("raeder\n");

                EP.code = Convert.ToInt32(reader["Code"]);
                EP.XCode = new Guid(reader["XCode"].ToString());
                EP.Name = Convert.ToString(reader["Name"]);
                EP.comment = Convert.ToString(reader["Comment"]);
                EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                EP.HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                EP.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                EP.PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                EP.Image = (byte[])(reader["Image"]);
            }
            else
            {
                //nothing has been found
                EP.code = -1;
            }
            //ed.WriteMessage("TTT\n");
            reader.Close();

            connection.Close();

            return EP;
        }

        //CALCULATION HATAMI //StatusReport
        public static EPoleTip AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_PoleTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iCode", Code));
            EPoleTip EP = new EPoleTip();
            try
            {
                //connection.Open();
                //ed.WriteMessage("E_PoleTip.Select\n");
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //ed.WriteMessage("raeder\n");

                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                    EP.Factor = Convert.ToByte(reader["Factor"].ToString());
                    EP.HalterCount = Convert.ToInt32(reader["HalterCount"].ToString());
                    EP.HalterID = Convert.ToInt32(reader["HalterID"].ToString());
                    EP.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    EP.PoleCode = Convert.ToInt32(reader["PoleCode"].ToString());
                    EP.Image = (byte[])(reader["Image"]);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }
                //ed.WriteMessage("TTT\n");
                reader.Close();
                //ed.WriteMessage("E_Pro\n");
                command.Parameters.Clear();
                command.CommandText = "E_ProductPackage_SelectByContainerCodeType";

                command.Parameters.Add(new OleDbParameter("iCode", EP.Code));
                command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.PoleTip));
                //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
                reader = command.ExecuteReader();
                nodeCountEPackage.Clear();
                nodeKeysEPackage.Clear();
                nodeKeysEPackage.Clear();
                while (reader.Read())
                {
                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                    //ed.WriteMessage("True\n");
                    nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                    //ed.WriteMessage("bb\n");

                    nodeTypeEPackage.Add(reader["TableType"].ToString());
                    //ed.WriteMessage("cc\n");

                    nodeCountEPackage.Add(reader["Count"].ToString());
                    //ed.WriteMessage("Type:" + reader["Code"].ToString() + "\n");
                }

                reader.Close();
                //connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new OleDbParameter("iHalterType", HalterID));
            command.Parameters.Add(new OleDbParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new OleDbParameter("iPoleCode", PoleCode));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iImage", Image));
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.PoleTip);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in PoleTip");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.PoleTip, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.AccessInsert : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PoleTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new OleDbParameter("iHalterType", HalterID));
            command.Parameters.Add(new OleDbParameter("iFactor", Factor));

            /*Hatami*/
            command.Parameters.Add(new OleDbParameter("iPoleCode", PoleCode));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iImage", Image));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.PoleTip);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in PoleTip");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.PoleTip, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess PoleTip failed");
                    }
                }


            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR EPoleTip.AccessInsert : {0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

            return true;
        }

        //public bool AccessUpadte()
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection connection = _connection;// new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("E_PoleTip_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Transaction = _transaction;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Parameters.Add(new OleDbParameter("iName", Name));
        //    command.Parameters.Add(new OleDbParameter("iComment", Comment));
        //    command.Parameters.Add(new OleDbParameter("iFactor", Factor));
        //    command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
        //    command.Parameters.Add(new OleDbParameter("iHalterType", HalterType));
        //    try
        //    {
        //        //connection.Open();
        //        Code = Convert.ToInt32(command.ExecuteScalar());
        //        //connection.Close();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage("ERROR EPoleTip.AccessUpdate : {0} \n", ex1.Message);
        //        //connection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        public static bool AccessDelete()
        {
            return false;
        }

        //MOUSAVI
        public static EPoleTip AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_PoleTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            EPoleTip EP = new EPoleTip();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EP.code = Convert.ToInt32(reader["Code"]);
                    EP.XCode = new Guid(reader["XCode"].ToString());
                    EP.Name = Convert.ToString(reader["Name"]);
                    EP.comment = Convert.ToString(reader["Comment"]);
                }
                else
                {
                    //nothing has been found
                    EP.code = -1;
                }

                reader.Close();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
            }

            return EP;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EPoleTip AccessSelectByXCode(Guid XCode, OleDbTransaction _transction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_PoleTip_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    EPoleTip EP = new EPoleTip();
        //    try
        //    {
        //        OleDbDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            EP.code = Convert.ToInt32(reader["Code"]);
        //            EP.XCode = new Guid(reader["XCode"].ToString());
        //            EP.Name = Convert.ToString(reader["Name"]);
        //            EP.comment = Convert.ToString(reader["Comment"]);
        //            EP.XCode = new Guid(reader["Comment"].ToString());
        //        }
        //        else
        //        {
        //            //nothing has been found
        //            EP.code = -1;
        //        }

        //        reader.Close();

        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage("ERRROR EPoleTip.SelectByXCode:{0} \n ", ex1.Message);
        //    }

        //    return EP;
        //}


        public static DataTable SelectAllAndMerge()
        {
            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = DrawSearchByPoleXCode();

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
        //public static DataTable SelectAllAndMerge()
        //{
        //    DataTable AccTbl = AccessDrawSearch();
        //    DataTable SqlTbl = DrawSearch();

        //    DataTable MergeTbl = SqlTbl.Copy();
        //    DataColumn IsSql = new DataColumn("IsSql", typeof(bool));
        //    IsSql.DefaultValue = true;
        //    MergeTbl.Columns.Add(IsSql);

        //    foreach (DataRow Dr in AccTbl.Rows)
        //    {
        //        DataRow MergeRow = MergeTbl.NewRow();

        //        foreach (DataColumn Dc in AccTbl.Columns)
        //        {
        //            MergeRow[Dc.ColumnName] = Dr[Dc.ColumnName];
        //        }

        //        MergeRow["IsSql"] = false;
        //        MergeRow["XCode"] = new Guid("00000000-0000-0000-0000-000000000000");
        //        MergeTbl.Rows.Add(MergeRow);
        //    }


        //    return MergeTbl;

        //}

        public static DataTable SelectAllAndMergeByConsolType(int ConsolType)
        {
            DataTable AccTbl = AccessSearchByConsolType(ConsolType);
            DataTable SqlTbl = SearchByConsolType(ConsolType);

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

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PoleTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PoleTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable AccessDrawSearch()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PoleTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            //System.Windows.Forms.MessageBox.Show(dsPola.Tables[0].Rows.Count.ToString());
            return dsPola.Tables[0];
        }

        public static DataTable AccessSearchByConsolType(int ConsolType)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_PoleTip_SearchByConsolType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("iType", ConsolType);

            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            //System.Windows.Forms.MessageBox.Show(dsPola.Tables[0].Rows.Count.ToString());
            return dsPola.Tables[0];
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

        //        EPoleTip _EPoleTip = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //            //ed.WriteMessage("\n111\n");

        //            EPoleTip Ap = Atend.Base.Equipment.EPoleTip.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            //ed.WriteMessage("\n222\n");
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                //ed.WriteMessage("\n333\n");

        //                if (!Atend.Base.Equipment.EPoleTip.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n444\n");

        //            }

        //            //ed.WriteMessage("\n555\n");

        //            if (Atend.Base.Equipment.EPole.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EPoleTip.PoleXCode))
        //            {
        //                //ed.WriteMessage("\n145n");

        //                _EPoleTip.PoleCode = Atend.Base.Equipment.EPole.ServerSelectByXCode(Servertransaction, Serverconnection, _EPoleTip.PoleXCode).Code;
        //                //ed.WriteMessage("\n146\n");
        //            }
        //            else
        //            {
        //                //throw new Exception("while .... Share On Server at Pole Of Pole Tip in AtendServer");
        //                //ed.WriteMessage("\n666\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            //ed.WriteMessage("\n777\n");


        //            if (_EPoleTip.Insert(Servertransaction, Serverconnection))
        //            {
        //                //ed.WriteMessage("\n888\n");

        //                if (!_EPoleTip.UpdateX(Localtransaction, Localconnection))
        //                {
        //                    //ed.WriteMessage("\n115\n");

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPoleTip.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PoleTip, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPoleTip.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PoleTip));
        //            //ed.WriteMessage("\n111\n");

        //            EPoleTip Ap = Atend.Base.Equipment.EPoleTip.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPoleTip.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPoleTip.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            //ed.WriteMessage("\n222\n");

        //            Atend.Base.Equipment.EPole Pole = Atend.Base.Equipment.EPole.SelectByCode(Localtransaction, Localconnection, Ap.PoleCode);
        //            //ed.WriteMessage("\n333\n");
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid PoleDeleted = Guid.NewGuid();
        //            if (Pole.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                PoleDeleted = Pole.XCode;
        //                //ed.WriteMessage("\n444\n");

        //                if (!Atend.Base.Equipment.EPole.DeleteX(Localtransaction, Localconnection, Pole.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n555\n");

        //                Pole.ServerSelectByCode(Ap.PoleCode);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\nCode = {0}\n", Code.ToString());

        //                Pole = Atend.Base.Equipment.EPole.SelectByCode(Ap.PoleCode);
        //                Pole.XCode = PoleDeleted;
        //                PoleDeleted = Guid.Empty;
        //            }

        //            //ed.WriteMessage("\n666\n");

        //            DataTable OperationTbl = new DataTable();
        //            Pole.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, Pole.XCode);
        //            Pole.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (!Pole.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }


        //            Ap.PoleXCode = Pole.XCode;

        //            //ed.WriteMessage("\n777\n");

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }

        //            //ed.WriteMessage("\n112\n");

        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.PoleTip, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPoleTip.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}




        //****************************Access To Memory For Calculation*****
        public static EPoleTip AccessSelectByCode(DataTable dtPoleTip, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataRow[] drPoleTip = dtPoleTip.Select(string.Format("Code={0}", Code));
            EPoleTip EP = new EPoleTip();

            if (drPoleTip.Length > 0)
            {
                //ed.WriteMessage("raeder\n");

                EP.code = Convert.ToInt32(drPoleTip[0]["Code"]);
                EP.XCode = new Guid(drPoleTip[0]["XCode"].ToString());
                EP.Name = Convert.ToString(drPoleTip[0]["Name"]);
                EP.comment = Convert.ToString(drPoleTip[0]["Comment"]);
                EP.Factor = Convert.ToByte(drPoleTip[0]["Factor"].ToString());
                EP.HalterCount = Convert.ToInt32(drPoleTip[0]["HalterCount"].ToString());
                EP.HalterID = Convert.ToInt32(drPoleTip[0]["HalterID"].ToString());
                EP.IsDefault = Convert.ToBoolean(drPoleTip[0]["IsDefault"]);
                EP.PoleCode = Convert.ToInt32(drPoleTip[0]["PoleCode"].ToString());

            }
            else
            {
                //nothing has been found
                EP.code = -1;
            }


            return EP;
        }
    }
}
