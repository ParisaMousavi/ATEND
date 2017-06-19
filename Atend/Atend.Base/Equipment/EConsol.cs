using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EConsol
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

        private double length;
        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
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

        private double distancePhase;
        public double DistancePhase
        {
            get { return distancePhase; }
            set { distancePhase = value; }
        }

        private double distanceCrossArm;
        public double DistanceCrossArm
        {
            get { return distanceCrossArm; }
            set { distanceCrossArm = value; }
        }

        private byte consolType;
        public byte ConsolType
        {
            get { return consolType; }
            set { consolType = value; }
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

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private int voltagelevel;
        public int VoltageLevel
        {
            get { return voltagelevel; }
            set { voltagelevel = value; }
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

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeCountEPackageX = new ArrayList();

        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();

        public static ArrayList nodeTypeEPackage = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();

        public static ArrayList nodeKeys = new ArrayList();


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            try
            {
                Connection.Open();
                sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EConsol.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Insert", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
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
                    return true;
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
                ed.WriteMessage(string.Format(" ERROR EConsol.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }



        //HATAMI  //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Consol_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iLength", Length));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new SqlParameter("iVoltageLevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Consol, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Consol, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Consol_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iLength", Length));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new SqlParameter("iVoltageLevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Consol))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Consol, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in Consol");
                    }
                }


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Consol, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
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
                    //ed.WriteMessage("GOTO\n");
                    //if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

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
                ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EConsol.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool Update(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    if (canCommitTransaction)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Transaction:{0}", ex1.Message);
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EConsol.Update: {0} \n", ex1.Message));
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

                    if ((EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Insulator))))
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
                ed.WriteMessage(string.Format(" ERROR EConsol.Delete: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Delete", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

                    if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if ((EContainerPackage.Delete(transaction, Connection, containerPackage.ContainerCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))))
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
                    ed.WriteMessage("Error In TransAction-Econsol.ServerDelete:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EConsol.ServerDelete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static EConsol SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iCode", consol.code));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
            reader = sqlCommand.ExecuteReader();
            nodeCountEPackage.Clear();
            nodeKeysEPackage.Clear();
            nodeTypeEPackage.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            Connection.Close();

            return consol;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            //EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                //consol.Code = Convert.ToInt32(reader["Code"].ToString());
                Length = Convert.ToDouble(reader["Length"].ToString());
                //consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Type = Convert.ToByte(reader["Type"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Image = (byte[])(reader["Image"]);
                DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
            }
            reader.Close();
            //sqlCommand.Parameters.Clear();
            //sqlCommand.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            //sqlCommand.Parameters.Add(new SqlParameter("iCode", consol.code));
            //sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            ////ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
            //reader = sqlCommand.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    //ed.WriteMessage("True\n");
            //    nodeKeysEPackage.Add(reader["ProductCode"].ToString());
            //    nodeTypeEPackage.Add(reader["TableType"].ToString());
            //    nodeCountEPackage.Add(reader["Count"].ToString());
            //    //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            //}

            //reader.Close();
            Connection.Close();

            //return consol;
        }

        //MEDHAT
        public static EConsol SelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;
            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            //sqlCommand.Parameters.Clear();
            //sqlCommand.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            //sqlCommand.Parameters.Add(new SqlParameter("iCode", consol.Code));
            //sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            //reader = sqlCommand.ExecuteReader();
            //nodeCountEPackage.Clear();
            //nodeKeysEPackage.Clear();
            //nodeTypeEPackage.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    //ed.WriteMessage("True\n");
            //    nodeKeysEPackage.Add(reader["ProductCode"].ToString());
            //    nodeTypeEPackage.Add(reader["TableType"].ToString());
            //    nodeCountEPackage.Add(reader["Count"].ToString());
            //    //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            //}

            //reader.Close();
            ////Connection.Close();

            return consol;
        }

        public static EConsol SelectByCodeForDesign(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {

                //ed.writeMessage("I Am In ConSol.SelectByCodeForDesign\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                //ed.WriteMessage("1\n");
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                //ed.WriteMessage("2\n");
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage("3\n");
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                //ed.WriteMessage("4\n");
                consol.Comment = reader["Comment"].ToString();
                //ed.WriteMessage("5\n");
                consol.Name = reader["Name"].ToString();
                //ed.WriteMessage("6\n");
                consol.Image = (byte[])(reader["Image"]);
                //ed.WriteMessage("7\n");
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                //ed.WriteMessage("8\n");
                //consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                //ed.WriteMessage("9\n");
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                //ed.WriteMessage("10\n");
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"]);

            }
            else
            {
                consol.Code = -1;
                //ed.WriteMessage("\nNo Record Found For Code : {0}\n");
            }

            reader.Close();
            Connection.Close();

            return consol;
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_Select", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_Search", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static EConsol SelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByProductCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EConsol consol = new EConsol();

            try
            {
                Connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    consol.Code = Convert.ToInt32(reader["Code"].ToString());
                    consol.Length = Convert.ToDouble(reader["Length"].ToString());
                    consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    consol.Type = Convert.ToByte(reader["Type"].ToString());
                    consol.Comment = reader["Comment"].ToString();
                    consol.Name = reader["Name"].ToString();
                    consol.Image = (byte[])(reader["Image"]);
                    consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                    consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                    consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());

                }
                else
                {
                    consol.code = -1;
                    //ed.WriteMessage(string.Format("No Record found for ProductCode : {0} \n", ProductCode));
                }

                reader.Close();

                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConsol.SelectByProductCode : {0} \n", ex1.Message));
            }
            return consol;

        }

        public static DataTable SelectByType(int Type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_SelectByType", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        //ASHKTORAB
        public static EConsol ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Transaction = _transaction;
            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", consol.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
            reader = sqlCommand.ExecuteReader();
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
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            //Connection.Close();

            return consol;
        }

        //MEDHAT //ShareOnServer
        public static EConsol ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = ServerConnection;
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = ServerTransaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();

            if (reader.Read())
            {
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : consol\n");
            }
            reader.Close();
            return consol;
        }

        public static DataTable SelectLessPc(double Pc)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_SelectLessPc", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPC", Pc));
            DataSet dsPC = new DataSet();
            adapter.Fill(dsPC);
            return dsPC.Tables[0];
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Code = 0;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
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
                    Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    //ContainerCode = 
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    if (canCommitTransaction)
                    {
                        //containerPackage.ContainerCode = 0;// containerCode;
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        if (containerPackage.InsertX(transaction, Connection))
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                    }
                    //ed.writeMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
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
                            //ed.writeMessage("Error For Insert \n");
                        }
                        Counter++;
                    }

                    Counter = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
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
                ed.WriteMessage(string.Format(" ERROR EConsol.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_InsertX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            //XCode = Guid.NewGuid();
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Code = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    //ContainerCode = 
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
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
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();

                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.ContainerCode = 0;// containerCode;
                    //    containerPackage.XCode = XCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (containerPackage.InsertX(transaction, Connection))
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //}
                    ////ed.writeMessage("count of subequip " + EquipmentList.Count.ToString() + " \n");
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
                    //        //ed.writeMessage("Error For Insert \n");
                    //    }
                    //    Counter++;
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
                ed.WriteMessage(string.Format(" ERROR EConsol.Insert: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Consol_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iLength", Length));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new SqlParameter("iVoltageLevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                ////Code = insertCommand.ExecuteNonQuery();
                ////insertCommand.Parameters.Clear();
                ////insertCommand.CommandType = CommandType.Text;
                ////insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Consol, ServerConnection, ServerTransaction);
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
                    //ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Consol, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Consol_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iLength", Length));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iImage", Image));
            insertCommand.Parameters.Add(new SqlParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new SqlParameter("iVoltageLevel", VoltageLevel));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Consol))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Consol, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection , ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Consol");
                    }
                }
                ed.WriteMessage("EConsol.Operation passed \n");


                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Consol, XCode, ServerTransaction, ServerConnection , _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
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
                    //ed.WriteMessage("GOTO\n");
                    //if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

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

                    Counter = 0;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);

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
                ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EConsol.Update: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);

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
                    //if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

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
                ed.WriteMessage(string.Format(" ERROR EConsol.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool UpdateX02(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_UpdateX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Type));
            sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            sqlCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    {
                        //ed.WriteMessage("EContainerPackage.update\n");
                        Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                        //ed.WriteMessage("Code:" + Code.ToString() + "\n");

                        _EContainerPackage.ContainerCode = Code;
                        //ed.WriteMessage("Type:" + "\n");

                        _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                        //ed.WriteMessage("_EContainerPackage.Insert:\n");
                        if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;

                        }
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

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
                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);

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
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EConsol.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Consol);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))))
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)) && canCommitTransaction)
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
                ed.WriteMessage(string.Format(" ERROR EConsol.DeleteX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_transaction, _connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))))
                    {

                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)) && canCommitTransaction)
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
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EConsol.DeleteX: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        public static DataTable DrawSearch(int voltageLevel, int consolType, int type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iVoltageLevel", voltageLevel));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iConsolType", consolType));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", type));
            DataSet dsconsol = new DataSet();
            adapter.Fill(dsconsol);
            return dsconsol.Tables[0];
        }

        //frmEditDrawPole
        public static EConsol SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", consol.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
            reader = sqlCommand.ExecuteReader();
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
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", consol.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
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
            return consol;
        }

        //MOUSAVI , AutoPoleInstallation //SentFromLocalToAccess
        public static EConsol SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("%%% XCODE : {0} \n",XCode);
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));

            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            Connection.Close();

            return consol;
        }

        //MEDHAT //ShareOnServer
        public static EConsol SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();

            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            return consol;
        }


        public static EConsol SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();

            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            return consol;
        }

        public static EConsol SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Transaction = _transaction;
            //Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", consol.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            //ed.WriteMessage("consol.code:" + consol.code + "Type:" + Atend.Control.Enum.ProductType.Consol + "\n");
            reader = sqlCommand.ExecuteReader();
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
                //ed.WriteMessage("Type:" + nodeTypeEPackage + "\n");
            }

            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_Operation_SelectByXCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", consol.XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            reader = sqlCommand.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }
            reader.Close();
            //Connection.Close();

            return consol;
        }




        public static DataTable SelectByConsolType(int ConsolType)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_SelectByConsolType", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));

            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }


        //Hatami
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_SelectAll", Connection);
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable SelectOburiX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_SelectOburi", Connection);
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }
        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Consol_Search", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        //MEDHAT
        public static EConsol LocalSelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
            sqlCommand.Transaction = _transaction;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = 0;
            }
            reader.Close();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "E_ProductPackage_SelectByContainerCodeType";
            sqlCommand.Parameters.Add(new SqlParameter("iCode", consol.Code));
            sqlCommand.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Consol));
            reader = sqlCommand.ExecuteReader();
            nodeCountEPackageX.Clear();
            nodeKeysEPackageX.Clear();
            nodeTypeEPackageX.Clear();
            while (reader.Read())
            {
                nodeKeysEPackage.Add(reader["ProductCode"].ToString());
                nodeTypeEPackage.Add(reader["TableType"].ToString());
                nodeCountEPackage.Add(reader["Count"].ToString());
            }

            reader.Close();
            return consol;
        }

        //MEDHAT
        public static bool LocalDelete(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand sqlCommand = new SqlCommand("E_Consol_DeleteX", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    Boolean canCommitTransaction = true;
                    sqlCommand.ExecuteNonQuery();

                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(_transaction, _connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol))))
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
                    ed.WriteMessage("Error In TransAction-Econsol.LocalDelete:{0}\n", ex1.Message);
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;

                }


            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EConsol.LocalDelete: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Consol_SearchByName", connection);
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
        public static EConsol CheckForExist(double _Length, byte _Type, double _DistancePhase,
                                            double _DistanceCrossArm, byte _ConsolType, int _Voltagelevel)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Consol_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iLength", _Length));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iDistancePhase", _DistancePhase));
            command.Parameters.Add(new SqlParameter("iDistanceCrossArm", _DistanceCrossArm));
            command.Parameters.Add(new SqlParameter("iConsolType", _ConsolType));
            command.Parameters.Add(new SqlParameter("iVoltagelevel", _Voltagelevel));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.ConsolType = Convert.ToByte(reader["ConsolType"].ToString());
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"].ToString());
                consol.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                consol.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            connection.Close();
            return consol;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~`

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Consol_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iLength", Length));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            insertCommand.Parameters.Add(new OleDbParameter("iDistancePhase", DistancePhase));
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
                ed.WriteMessage(string.Format("Error EConsol.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Consol_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iLength", Length));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            insertCommand.Parameters.Add(new OleDbParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new OleDbParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new OleDbParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltageLevel", VoltageLevel));
            try
            {

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Consol);
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
                    ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Consol, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Consol_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iLength", Length));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            insertCommand.Parameters.Add(new OleDbParameter("iDistancePhase", DistancePhase));
            insertCommand.Parameters.Add(new OleDbParameter("iDistanceCrossArm", DistanceCrossArm));
            insertCommand.Parameters.Add(new OleDbParameter("iConsolType", ConsolType));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltageLevel", VoltageLevel));
            int OldCode = Code;
            try
            {

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Consol);
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
                    ed.WriteMessage("Main Parent is Consol: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess (OldCode, (int)Atend.Control.Enum.ProductType.Consol, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Consol failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConsol.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //MOUSAVI
        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_Update", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iLength", Length));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));
            sqlCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            sqlCommand.Parameters.Add(new OleDbParameter("iName", Name));
            sqlCommand.Parameters.Add(new OleDbParameter("iImage", Image));
            sqlCommand.Parameters.Add(new OleDbParameter("iDistancePhase", distancePhase));
            sqlCommand.Parameters.Add(new OleDbParameter("iDistanceCrossArm", DistanceCrossArm));
            sqlCommand.Parameters.Add(new OleDbParameter("iConsolType", ConsolType));
            sqlCommand.Parameters.Add(new OleDbParameter("iVoltagelevel", VoltageLevel));
            try
            {
                sqlCommand.ExecuteNonQuery();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR EConsol.AccessUpdate: {0} \n", ex1.Message));

                return false;
            }
            return true;
        }

        //MOUSAVI->AutoPoleInstallation , drawConductor //StatusReport //frmEditDrawPole
        public static EConsol AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("%%% CODE : {0} \n", Code);
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.consolType = Convert.ToByte(reader["ConsolType"]);
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("ConSol.NAme={0}\n",consol.Name);
            }
            else
            {
                consol.Code = -1;
                //ed.WriteMessage("NOT FOUND \n");
            }
            reader.Close();
            Connection.Close();
            //ed.WriteMessage("ECONSOL.AccessSelectByCode finished\n");
            return consol;
        }



        public static EConsol AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("%%% CODE : {0} \n", Code);
            OleDbConnection Connection =_Connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.consolType = Convert.ToByte(reader["ConsolType"]);
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("ConSol.NAme={0}\n",consol.Name);
            }
            else
            {
                consol.Code = -1;
                //ed.WriteMessage("NOT FOUND \n");
            }
            reader.Close();
           // Connection.Close();
            //ed.WriteMessage("ECONSOL.AccessSelectByCode finished\n");
            return consol;
        }

        public static EConsol AccessSelectByCode(int Code, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("%%% CODE : {0} \n", Code);
            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.consolType = Convert.ToByte(reader["ConsolType"]);
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("ConSol.NAme={0}\n",consol.Name);
            }
            else
            {
                consol.Code = -1;
                //ed.WriteMessage("NOT FOUND \n");
            }
            reader.Close();
            //Connection.Close();
            //ed.WriteMessage("ECONSOL.AccessSelectByCode finished\n");
            return consol;
        }


        //AcDrawPole
        public static EConsol AccessSelectByCode(OleDbTransaction _Transaction, OleDbConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_Select", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            sqlCommand.Transaction = _Transaction;

            OleDbDataReader reader = sqlCommand.ExecuteReader();
            EConsol consol = new EConsol();
            if (reader.Read())
            {
                //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                consol.Code = Convert.ToInt32(reader["Code"].ToString());
                consol.Length = Convert.ToDouble(reader["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                consol.Type = Convert.ToByte(reader["Type"].ToString());
                consol.Comment = reader["Comment"].ToString();
                consol.Name = reader["Name"].ToString();
                consol.Image = (byte[])(reader["Image"]);
                consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                consol.consolType = Convert.ToByte(reader["ConsolType"]);
                consol.VoltageLevel = Convert.ToInt32(reader["VoltageLevel"]);
                consol.DistanceCrossArm = Convert.ToDouble(reader["DistanceCrossArm"]);
                consol.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("ConSol.NAme={0}\n",consol.Name);
            }
            else
            {
                consol.Code = -1;
            }
            reader.Close();
            return consol;
        }

        //MOUSAVI->AutoPoleInstallation
        public static EConsol AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_SelectByXCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            EConsol consol = new EConsol();

            try
            {
                //ed.WriteMessage("~~~~ >>. ~~~ {0} \n",XCode);
                Connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
                    consol.Code = Convert.ToInt32(reader["Code"].ToString());
                    consol.Length = Convert.ToDouble(reader["Length"].ToString());
                    consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    consol.Type = Convert.ToByte(reader["Type"].ToString());
                    consol.Comment = reader["Comment"].ToString();
                    consol.Name = reader["Name"].ToString();
                    consol.Image = (byte[])(reader["Image"]);
                    consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                }
                else
                {
                    consol.Code = -1;
                    //ed.WriteMessage("CONSOL not FOUND\n");
                }
                reader.Close();
                Connection.Close();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EConsol AccessSelectByXCode :{0} \n", ex.Message);
            }
            return consol;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EConsol AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection Connection = _connection;
        //    OleDbCommand sqlCommand = new OleDbCommand("E_Consol_SelectByXCode", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Transaction = _transaction;
        //    sqlCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));


        //    OleDbDataReader reader = sqlCommand.ExecuteReader();
        //    EConsol consol = new EConsol();
        //    if (reader.Read())
        //    {
        //        //ed.WriteMessage("I Am In ConSol.SelectByCode\n");
        //        consol.Code = Convert.ToInt32(reader["Code"].ToString());
        //        consol.Length = Convert.ToDouble(reader["Length"].ToString());
        //        consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        consol.Type = Convert.ToByte(reader["Type"].ToString());
        //        consol.Comment = reader["Comment"].ToString();
        //        consol.Name = reader["Name"].ToString();
        //        consol.Image = (byte[])(reader["Image"]);
        //        consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
        //        consol.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        consol.Code = -1;

        //    }
        //    reader.Close();

        //    return consol;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_Select", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }


        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_Select", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }
        //Hatatmi
        public static DataTable AccessSelectbyConsolType(int type)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_SelectByConsolType", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iConsolType", type));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable AccessSelectOburi()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_SelectOburi", Connection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_Search", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        public static EConsol AccessSelectByProductCode(int ProductCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("E_Consol_SelectByProductCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EConsol consol = new EConsol();

            try
            {
                Connection.Open();
                OleDbDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    consol.Code = Convert.ToInt32(reader["Code"].ToString());
                    consol.Length = Convert.ToDouble(reader["Length"].ToString());
                    consol.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    consol.Type = Convert.ToByte(reader["Type"].ToString());
                    consol.Comment = reader["Comment"].ToString();
                    consol.Name = reader["Name"].ToString();
                    consol.Image = (byte[])(reader["Image"]);
                    consol.DistancePhase = Convert.ToDouble(reader["DistancePhase"]);
                }
                else
                {
                    consol.Code = -1;
                    //ed.WriteMessage(string.Format("No Record found for ProductCode : {0} \n", ProductCode));
                }

                reader.Close();

                Connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConsol.SelectByProductCode : {0} \n", ex1.Message));
            }
            return consol;

        }

        public static DataTable AccessSelectByType(int Type)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Consol_SelectByType", Connection);
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConsol = new DataSet();
            adapter.Fill(dsConsol);
            return dsConsol.Tables[0];
        }

        //MOUSAVI->AutoPoleInstallation
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessSelectAll();
            DataTable SqlTbl = SelectAllX();
            //ed.WriteMessage("AccTbl.Count={0}\n", AccTbl.Rows.Count);
            //ed.WriteMessage("SqlTbl.Count={0}\n", SqlTbl.Rows.Count);
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
            //ed.WriteMessage("~~~~~~~~~~consol column ~~~~~~~~~~\n");
            //foreach (DataColumn Dc1 in AccTbl.Columns)
            //{
            //    ed.WriteMessage("ColumnName={0}\n",Dc1.ColumnName);
            //}
            //ed.WriteMessage("MergeTbl.Count={0}\n", MergeTbl.Rows.Count);

            return MergeTbl;

        }

        //Hatatmi for Calculation
        public static DataTable SelectAllAndMergeOburi()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessSelectOburi();
            DataTable SqlTbl = SelectOburiX();
            //ed.WriteMessage("AccTbl.Count={0}\n", AccTbl.Rows.Count);
            //ed.WriteMessage("SqlTbl.Count={0}\n", SqlTbl.Rows.Count);
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
            //ed.WriteMessage("~~~~~~~~~~consol column ~~~~~~~~~~\n");
            //foreach (DataColumn Dc1 in AccTbl.Columns)
            //{
            //    ed.WriteMessage("ColumnName={0}\n",Dc1.ColumnName);
            //}
            //ed.WriteMessage("MergeTbl.Count={0}\n", MergeTbl.Rows.Count);

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

        //        EConsol consol = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        //            //ed.WriteMessage("\n111\n");

        //            EConsol Ap = Atend.Base.Equipment.EConsol.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EConsol.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            consol.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, consol.XCode);
        //            consol.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (consol.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, consol.Code, (int)Atend.Control.Enum.ProductType.Consol))
        //                {
        //                    if (!consol.UpdateX(Localtransaction, Localconnection))
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, consol.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Consol, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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

        //        EConsol consol = SelectByCode(Servertransaction, Serverconnection, Code);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection,consol.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));

        //            if (containerPackage.Type == -1)//if not exist
        //            {
        //                containerPackage.Type=Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
        //                containerPackage.XCode=consol.XCode;
        //                if (!containerPackage.InsertX(Localtransaction, Localconnection))
        //                {
        //                    throw new Exception("insert into containerPackage for consol failed");
        //                }
        //            }

        //            EConsol Ap = Atend.Base.Equipment.EConsol.LocalSelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.Code != 0)
        //            {
        //                //DeletedCode = Ap.Code;
        //                if (!consol.UpdateX02(Localtransaction,Localconnection))// Atend.Base.Equipment.EConsol.update(Localtransaction, Localconnection, Ap.XCode))
        //                {       
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }



        //            ////if (Ap.Code != 0)
        //            ////{
        //            ////    DeletedCode = Ap.Code;
        //            ////    if (!Atend.Base.Equipment.EConsol.LocalDelete(Localtransaction, Localconnection, Ap.XCode))
        //            ////    {
        //            ////        Servertransaction.Rollback();
        //            ////        Serverconnection.Close();

        //            ////        Localtransaction.Rollback();
        //            ////        Localconnection.Close();
        //            ////    }
        //            ////}
        //            ////if (consol.InsertX(Localtransaction, Localconnection))
        //            ////{
        //            ////    if (!Atend.Base.Equipment.EProductPackage.UpdateX(Localtransaction, Localconnection, consol.XCode, (int)Atend.Control.Enum.ProductType.Consol))
        //            ////    {
        //            ////        //if (!consol.Update(Servertransaction, Serverconnection))
        //            ////        {
        //            ////            Servertransaction.Rollback();
        //            ////            Serverconnection.Close();
        //            ////            Localtransaction.Rollback();
        //            ////            Localconnection.Close();
        //            ////            return false;
        //            ////        }
        //            ////    }
        //            ////    else
        //            ////    {
        //            ////        Servertransaction.Rollback();
        //            ////        Serverconnection.Close();

        //            ////        Localtransaction.Rollback();
        //            ////        Localconnection.Close();
        //            ////        return false;
        //            ////    }
        //            ////}
        //            ////else
        //            ////{
        //            ////    Servertransaction.Rollback();
        //            ////    Serverconnection.Close();

        //            ////    Localtransaction.Rollback();
        //            ////    Localconnection.Close();
        //            ////    return false;
        //            ////}

        //            //if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, consol.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Consol, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            //{
        //            //    ed.WriteMessage("\n113\n");

        //            //    Servertransaction.Commit();
        //            //    Serverconnection.Close();

        //            //    Localtransaction.Commit();
        //            //    Localconnection.Close();
        //            //}
        //            //else
        //            //{
        //            //    Servertransaction.Rollback();
        //            //    Serverconnection.Close();
        //            //    Localtransaction.Rollback();
        //            //    Localconnection.Close();
        //            //    return false;
        //            //}
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
        //        ed.WriteMessage(string.Format(" ERROR EConsol.GetFromServer{0}\n", ex1.Message));
        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        ////public static bool GetFromServer(int Code)
        ////{
        ////    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        ////    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlTransaction Localtransaction;

        ////    Guid DeletedXCode = Guid.NewGuid();

        ////    try
        ////    {

        ////        Localconnection.Open();
        ////        Localtransaction = Localconnection.BeginTransaction();


        ////        try
        ////        {

        ////            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////            //ed.WriteMessage("\n111\n");

        ////            EConsol Ap = Atend.Base.Equipment.EConsol.SelectByCode(Localtransaction, Localconnection, Code);

        ////            if (Ap.XCode != Guid.Empty)
        ////            {
        ////                DeletedXCode = Ap.XCode;
        ////                if (!Atend.Base.Equipment.EConsol.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        ////                {
        ////                    Localtransaction.Rollback();
        ////                    Localconnection.Close();
        ////                }

        ////                Ap.ServerSelectByCode(Code);
        ////            }
        ////            else
        ////            {
        ////                Ap = Atend.Base.Equipment.EConsol.SelectByCode(Code);
        ////                Ap.XCode = DeletedXCode;
        ////            }

        ////            Ap.OperationList = new ArrayList();
        ////            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
        ////            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        ////            if (!Ap.InsertX(Localtransaction, Localconnection))
        ////            {
        ////                //ed.WriteMessage("\n114\n");

        ////                Localtransaction.Rollback();
        ////                Localconnection.Close();
        ////                return false;

        ////            }

        ////            //ed.WriteMessage("\n112\n");

        ////            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Consol, Localtransaction, Localconnection))
        ////            {
        ////                //ed.WriteMessage("\n113\n");

        ////                Localtransaction.Commit();
        ////                Localconnection.Close();
        ////            }
        ////            else
        ////            {
        ////                //ed.WriteMessage("\n114\n");

        ////                Localtransaction.Rollback();
        ////                Localconnection.Close();
        ////                return false;
        ////            }


        ////        }
        ////        catch (System.Exception ex1)
        ////        {
        ////            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));

        ////            Localtransaction.Rollback();
        ////            Localconnection.Close();
        ////            return false;
        ////        }


        ////    }
        ////    catch (System.Exception ex1)
        ////    {
        ////        ed.WriteMessage(string.Format(" ERROR ECatOut.GetFromServer {0}\n", ex1.Message));

        ////        Localconnection.Close();
        ////        return false;
        ////    }

        ////    return true;
        ////}




        //******************************************Access To Memory For Calculation

        public static EConsol AccessSelectByCode(DataTable dtConsol, int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            EConsol consol = new EConsol();

            DataRow[] dr = dtConsol.Select(string.Format(" Code={0}", Code.ToString()));
            if (dr.Length != 0)
            {
                consol.Code = Convert.ToInt32(dr[0]["Code"].ToString());
                consol.Length = Convert.ToDouble(dr[0]["Length"].ToString());
                consol.ProductCode = Convert.ToInt32(dr[0]["ProductCode"].ToString());
                consol.Type = Convert.ToByte(dr[0]["Type"].ToString());
                consol.Comment = dr[0]["Comment"].ToString();
                consol.Name = dr[0]["Name"].ToString();
                consol.Image = (byte[])(dr[0]["Image"]);
                consol.DistancePhase = Convert.ToDouble(dr[0]["DistancePhase"]);
                consol.consolType = Convert.ToByte(dr[0]["ConsolType"]);
                consol.VoltageLevel = Convert.ToInt32(dr[0]["VoltageLevel"]);
                consol.DistanceCrossArm = Convert.ToDouble(dr[0]["DistanceCrossArm"]);
            }
            else
            {
                consol.Code = 0;
            }
            return consol;
        }

    }
}
