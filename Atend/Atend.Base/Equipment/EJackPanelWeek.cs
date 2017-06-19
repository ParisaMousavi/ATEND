using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EJackPanelWeek
    {
        public EJackPanelWeek()
        {

        }

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

        private Boolean isNightLight;
        public Boolean IsNightLight
        {
            get { return isNightLight; }
            set { isNightLight = value; }
        }

        private int feederCount;
        public int FeederCount
        {
            get { return feederCount; }
            set { feederCount = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int autoKey3pCodeCode;
        public int AutoKey3pCode
        {
            get { return autoKey3pCodeCode; }
            set { autoKey3pCodeCode = value; }
        }


        private Guid autoKey3pCodexCode;
        public Guid AutoKey3pXCode
        {
            get { return autoKey3pCodexCode; }
            set { autoKey3pCodexCode = value; }
        }

        private Guid busXCode;

        public Guid BusXCode
        {
            get { return busXCode; }
            set { busXCode = value; }
        }

        private int busCode;

        public int BusCode
        {
            get { return busCode; }
            set { busCode = value; }
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

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private int insertRowCode;
        public int InsertRowCode
        {
            get { return insertRowCode; }
            set { insertRowCode = value; }
        }

        private ArrayList equipmentList;
        public ArrayList EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; }
        }

        private ArrayList cellList;
        public ArrayList CellList
        {
            get { return cellList; }
            set { cellList = value; }
        }

        public DataTable dtglobal = new DataTable();

        public static DataTable JackPanelWeekSubEquip = new DataTable();

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        public static ArrayList nodeKeys = new ArrayList();






        public bool Insert()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iBusCode", BusCode));
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
                ed.WriteMessage(string.Format(" ERROR EJAckPAnelWeek.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
            //try
            //{
            //    connection.Open();
            //    transaction = connection.BeginTransaction();
            //    command.Transaction = transaction;

            //    try
            //    {
            //        Code=Convert.ToInt32(command.ExecuteScalar());
            //        InsertRowCode = Code;
            //        bool canCommitTransaction = true;
            //        int Counter = 0;
            //        int Counter1 = 0;
            //        ////ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
            //        Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
            //        ////ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");

            //        if (canCommitTransaction)
            //        {
            //            while (Counter1<cellList.Count && canCommitTransaction)
            //            {
            //                Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
            //                _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter1]);
            //                _EJackPanelWeekCell.JackPanelWeekCode = insertRowCode;


            //                if (_EJackPanelWeekCell.Insert(transaction, connection))
            //                {
            //                    canCommitTransaction = true;
            //                    ////ed.WriteMessage("_EJackPanelWeekCell.Code="+_EJackPanelWeekCell.Code+"\n");
            //                    if (canCommitTransaction)
            //                    {
            //                        containerPackage.XCode = _EJackPanelWeekCell.XCode;
            //                        //containerPackage.ContainerCode = _EJackPanelWeekCell.Code;
            //                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Cell);
            //                        if (containerPackage.Insert(transaction, connection))
            //                        {
            //                            canCommitTransaction = true;
            //                        }
            //                        else
            //                        {
            //                            canCommitTransaction = false;
            //                        }
            //                    }

            //                    //while (canCommitTransaction && Counter < EquipmentList.Count)
            //                    //{
            //                    ////ed.WriteMessage("ContainerPackage.containerCode= "+containerPackage.+"\n");
            //                    ////ed.WriteMessage("Counter1= "+Counter1+"\n");
            //                        int i = Counter1 + 1;
            //                        Atend.Base.Equipment.EProductPackage _EProductPackage;
            //                        DataRow[] drProductPackage = dtglobal.Select("Cell='" + i.ToString() + "'");
            //                        ////ed.WriteMessage("drProductPackage.Lenght= "+drProductPackage.Length+"\n");
            //                       for (int j=0;j<drProductPackage.Length;j++)
            //                       {
            //                            _EProductPackage = new EProductPackage();
            //                            _EProductPackage.ContainerPackageCode = containerPackage.Code;
            //                            _EProductPackage.Count = Convert.ToInt32(drProductPackage[j]["Count"].ToString());
            //                            _EProductPackage.ProductCode = Convert.ToInt32(drProductPackage[j]["ProductCode"].ToString());
            //                            _EProductPackage.TableType = Convert.ToByte(drProductPackage[j]["TableType"].ToString());
            //                            if (_EProductPackage.Insert(transaction, connection) && canCommitTransaction)
            //                            {
            //                                canCommitTransaction = true;
            //                            }
            //                            else
            //                            {
            //                                canCommitTransaction = false;
            //                                //ed.WriteMessage("Error For Insert \n");
            //                            }

            //                        //}




            //                        //Counter++;
            //                    }
            //                }
            //                Counter1++; 
            //        }
            //            }
            //        Counter = 0;


            //        if (canCommitTransaction)
            //        {
            //            transaction.Commit();
            //            connection.Close();
            //            return true;
            //        }
            //        else
            //        {
            //            transaction.Rollback();
            //            connection.Close();
            //            return false;
            //        }

            //    }
            //    catch (System.Exception ex1)
            //    {
            //        //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
            //        transaction.Rollback();
            //        connection.Close();
            //        return false;
            //    }
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.Insert: {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iBusCode", BusCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in JackPanelWeek");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.ServerInsert : {0} \n", ex.Message));
                return false;
            }


        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            // command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iBusCode", BusCode));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in JackPanelWeek");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted  Operation Failed in WeekJackPanel ");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.ServerUpdate : {0} \n", ex.Message));
                return false;
            }


        }


        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iBusCode", BusCode));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    Code = Convert.ToInt32(command.ExecuteScalar());
                    //bool canCommitTransaction = true;
                    //int Counter = 0;
                    //int Counter1 = 0;
                    //////ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
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
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.InsertX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static bool Update_XX(SqlTransaction _transaction, SqlConnection _connection, int Code, int NewCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_JackPAnelWeek_Update", connection);
            command.CommandType = CommandType.StoredProcedure;


            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNewCode", NewCode));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGJackPanelWeelCell.Update: {0} \n", ex1.Message));

                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeekCell.Update: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

            return true;
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));


            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    DataTable dtJackPAnelWeekCellCode = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Code);

                    if ((EJackPanelWeekCell.Delete(transaction, connection, Code)) && canComitTransaction)
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        Atend.Base.Equipment.EContainerPackage ContainerPackageCode = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(dr["Code"]), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
                        if (EProductPackage.Delete(transaction, connection, ContainerPackageCode.Code) && canComitTransaction)
                        {
                            canComitTransaction = true;
                        }
                        else
                        {
                            canComitTransaction = false;
                        }
                    }


                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (EContainerPackage.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)) && canComitTransaction)
                        {
                            canComitTransaction = true;

                        }
                        else
                        {
                            canComitTransaction = false;

                        }
                    }

                    //if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))) & canComitTransaction)
                    //{

                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;
                    //}

                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    //{
                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;

                    //}
                    if (canComitTransaction)
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
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.Delete: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.Delete: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        //public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int JPWCode)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlTransaction transaction = _transaction;
        //    SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_JackPanelWeek_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", JPWCode));

        //    //ed.WriteMessage("\n11111\n");
        //    try
        //    {
        //        //connection.Open();
        //        //transaction = connection.BeginTransaction();
        //        command.Transaction = transaction;
        //        Boolean canComitTransaction = true;
        //        try
        //        {
        //            command.ExecuteNonQuery();

        //            //ed.WriteMessage("\n22222\n");

        //            DataTable dtJackPAnelWeekCellCode = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(transaction, connection, JPWCode);

        //            //ed.WriteMessage("\n33333\n");

        //            if (EOperation.Delete(_transaction, connection, JPWCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel)))
        //            {
        //                canComitTransaction = true;

        //            }
        //            else
        //            {
        //                canComitTransaction = false;
        //            }

        //            if ((EJackPanelWeekCell.ServerDelete(transaction, connection, JPWCode)) && canComitTransaction)
        //            {
        //                canComitTransaction = true;

        //            }
        //            else
        //            {
        //                canComitTransaction = false;

        //            }
        //            //ed.WriteMessage("\n44444\n");

        //            foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
        //            {
        //                Atend.Base.Equipment.EContainerPackage ContainerPackageCode = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(transaction, connection, Convert.ToInt32(dr["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
        //                if (EProductPackage.Delete(transaction, connection, ContainerPackageCode.Code) && canComitTransaction)
        //                {
        //                    canComitTransaction = true;
        //                }
        //                else
        //                {
        //                    canComitTransaction = false;
        //                }
        //            }

        //            //ed.WriteMessage("\n55555\n");

        //            foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
        //            {
        //                if (EContainerPackage.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)) && canComitTransaction)
        //                {
        //                    canComitTransaction = true;

        //                }
        //                else
        //                {
        //                    canComitTransaction = false;

        //                }
        //            }
        //            //ed.WriteMessage("\n66666\n");

        //            //if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))) & canComitTransaction)
        //            //{

        //            //    canComitTransaction = true;
        //            //}
        //            //else
        //            //{
        //            //    canComitTransaction = false;
        //            //}

        //            //if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
        //            //{
        //            //    canComitTransaction = true;
        //            //}
        //            //else
        //            //{
        //            //    canComitTransaction = false;

        //            //}
        //            if (canComitTransaction)
        //            {
        //                //transaction.Commit();
        //                //connection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                //transaction.Rollback();
        //                //connection.Close();
        //                return false;
        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

        //            //transaction.Rollback();
        //            //connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

        //        //connection.Close();
        //        return false;
        //    }
        //}

        public static EJackPanelWeek SelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("productCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            reader.Close();
            JackPanelWeekSubEquip.Rows.Clear();
            DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Code);
            //ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            //foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            //{
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerCodeType";
            command.Parameters.Add(new SqlParameter("iJackPanelWeekCode", Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            ////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ////ed.WriteMessage("True\n");
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
                dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
                ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);

                //}


            }
            reader.Close();



            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", groundPost.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("productCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                //jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                //jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            reader.Close();
            //JackPanelWeekSubEquip.Rows.Clear();
            //DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Code);
            ////ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            ////foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            ////{
            //command.Parameters.Clear();
            //command.CommandText = "E_JackPanelWeekCell_SelectByContainerCodeType";
            //command.Parameters.Add(new SqlParameter("iJackPanelWeekCode", Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            //////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            //reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ////ed.WriteMessage("True\n");
            //    DataRow dr = JackPanelWeekSubEquip.NewRow();
            //    ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
            //    dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
            //    ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
            //    dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
            //    ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    JackPanelWeekSubEquip.Rows.Add(dr);

            //    //}


            //}
            //reader.Close();



            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", groundPost.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            //return jackPanelWeek;
        }

        public static EJackPanelWeek SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                    jackPanelWeek.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                    jackPanelWeek.Comment = reader["Comment"].ToString();
                    jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                    jackPanelWeek.Name = reader["Name"].ToString();
                    jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EJackPanelWeek.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return jackPanelWeek;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeek_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeek_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
        }

        //ASHKTORAB
        public static EJackPanelWeek ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("XCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            DataColumn dc5 = new DataColumn("Code");

            dc.DataType = System.Type.GetType("System.Guid");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            dc5.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc5);

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());

                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = 0;// Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            reader.Close();
            //JackPanelWeekSubEquip.Rows.Clear();
            //DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(XCode);
            ////ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            ////foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            ////{
            //command.Parameters.Clear();
            //command.CommandText = "E_JackPanelWeekCell_SelectByContainerXCodeType";
            //command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            //////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            //reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ////ed.WriteMessage("True\n");
            //    DataRow dr = JackPanelWeekSubEquip.NewRow();
            //    ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
            //    dr["XCode"] = new Guid(reader["XCode"].ToString());
            //    dr["Code"] = Convert.ToInt32(reader["Code"].ToString());
            //    ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
            //    dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
            //    ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    JackPanelWeekSubEquip.Rows.Add(dr);

            //    //}


            //}
            //reader.Close();



            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", groundPost.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }

        //MEDHAT
        public static EJackPanelWeek ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //JackPanelWeekSubEquip.Columns.Clear();
            //DataColumn dc = new DataColumn("XCode");
            //DataColumn dc1 = new DataColumn("TableType");
            //DataColumn dc2 = new DataColumn("cell");
            //DataColumn dc3 = new DataColumn("Count");
            //DataColumn dc4 = new DataColumn("IsNightLight");
            //DataColumn dc5 = new DataColumn("Code");

            //dc.DataType = System.Type.GetType("System.Guid");
            //JackPanelWeekSubEquip.Columns.Add(dc);
            //dc1.DataType = System.Type.GetType("System.Int32");
            //JackPanelWeekSubEquip.Columns.Add(dc1);
            //dc2.DataType = System.Type.GetType("System.Int32");
            //JackPanelWeekSubEquip.Columns.Add(dc2);
            //dc3.DataType = System.Type.GetType("System.Int32");
            //JackPanelWeekSubEquip.Columns.Add(dc3);
            //dc4.DataType = System.Type.GetType("System.Boolean");
            //JackPanelWeekSubEquip.Columns.Add(dc4);
            //dc5.DataType = System.Type.GetType("System.Int32");
            //JackPanelWeekSubEquip.Columns.Add(dc5);

            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Transaction = ServerTransaction;
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());

                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : jack panel week\n");
            }
            reader.Close();
            return jackPanelWeek;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    InsertRowCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    int Counter1 = 0;
                    //ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    //ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
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
                    Counter = 0;
                    if (canCommitTransaction)
                    {
                        while (Counter1 < cellList.Count && canCommitTransaction)
                        {
                            Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                            _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter1]);
                            _EJackPanelWeekCell.JackPanelWeekXCode = XCode;


                            if (_EJackPanelWeekCell.InsertX(transaction, connection))
                            {
                                canCommitTransaction = true;
                                //ed.WriteMessage("_EJackPanelWeekCell.XCode=" + _EJackPanelWeekCell.XCode + "\n");
                                if (canCommitTransaction)
                                {
                                    containerPackage.XCode = _EJackPanelWeekCell.XCode;
                                    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Cell);
                                    if (containerPackage.InsertX(transaction, connection))
                                    {
                                        canCommitTransaction = true;
                                    }
                                    else
                                    {
                                        canCommitTransaction = false;
                                    }
                                }

                                //while (canCommitTransaction && Counter < EquipmentList.Count)
                                //{
                                //ed.WriteMessage("ContainerPackage.XCode= " + containerPackage.XCode + "\n");
                                //ed.WriteMessage("Counter1= " + Counter1 + "\n");
                                int i = Counter1 + 1;
                                Atend.Base.Equipment.EProductPackage _EProductPackage;
                                DataRow[] drProductPackage = dtglobal.Select("Cell='" + i.ToString() + "'");
                                //ed.WriteMessage("drProductPackage.Lenght= " + drProductPackage.Length + "\n");
                                for (int j = 0; j < drProductPackage.Length; j++)
                                {
                                    _EProductPackage = new EProductPackage();
                                    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                                    _EProductPackage.Count = Convert.ToInt32(drProductPackage[j]["Count"].ToString());
                                    //_EProductPackage.ProductCode = 0;
                                    _EProductPackage.XCode = new Guid(drProductPackage[j]["XCode"].ToString());
                                    _EProductPackage.TableType = Convert.ToByte(drProductPackage[j]["TableType"].ToString());
                                    if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                                    {
                                        canCommitTransaction = true;
                                    }
                                    else
                                    {
                                        canCommitTransaction = false;
                                        //ed.WriteMessage("Error For Insert \n");
                                    }

                                    //}




                                    //Counter++;
                                }
                            }
                            Counter1++;
                        }
                    }
                    Counter = 0;
                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.ContainerCode = InsertRowCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                    //    if (containerPackage.Insert(transaction, connection))
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
                    //    Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                    //    if (_EProductPackage.Insert(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EJackPanelWeekCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    if (_EJackPanelWeekCell.Insert(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For _EJackPanelWeekCell.Insert \n");
                    //    }
                    //    Counter++;
                    //}

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
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.InsertX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {

                    InsertRowCode = Convert.ToInt32(command.ExecuteScalar());
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    int Counter1 = 0;
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
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
                    ////ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    ////ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");

                    //if (canCommitTransaction)
                    //{
                    //    while (Counter1 < cellList.Count && canCommitTransaction)
                    //    {
                    //        Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                    //        _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter1]);
                    //        _EJackPanelWeekCell.JackPanelWeekXCode = XCode;


                    //        if (_EJackPanelWeekCell.InsertX(transaction, connection))
                    //        {
                    //            canCommitTransaction = true;
                    //            //ed.WriteMessage("_EJackPanelWeekCell.XCode=" + _EJackPanelWeekCell.XCode + "\n");
                    //            if (canCommitTransaction)
                    //            {
                    //                containerPackage.XCode = _EJackPanelWeekCell.XCode;
                    //                containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Cell);
                    //                if (containerPackage.InsertX(transaction, connection))
                    //                {
                    //                    canCommitTransaction = true;
                    //                }
                    //                else
                    //                {
                    //                    canCommitTransaction = false;
                    //                }
                    //            }

                    //            //while (canCommitTransaction && Counter < EquipmentList.Count)
                    //            //{
                    //            //ed.WriteMessage("ContainerPackage.XCode= " + containerPackage.XCode + "\n");
                    //            //ed.WriteMessage("Counter1= " + Counter1 + "\n");
                    //            int i = Counter1 + 1;
                    //            Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //            DataRow[] drProductPackage = dtglobal.Select("Cell='" + i.ToString() + "'");
                    //            //ed.WriteMessage("drProductPackage.Lenght= " + drProductPackage.Length + "\n");
                    //            for (int j = 0; j < drProductPackage.Length; j++)
                    //            {
                    //                _EProductPackage = new EProductPackage();
                    //                _EProductPackage.ContainerPackageCode = containerPackage.Code;
                    //                _EProductPackage.Count = Convert.ToInt32(drProductPackage[j]["Count"].ToString());
                    //                //_EProductPackage.ProductCode = 0;
                    //                _EProductPackage.XCode = new Guid(drProductPackage[j]["XCode"].ToString());
                    //                _EProductPackage.TableType = Convert.ToByte(drProductPackage[j]["TableType"].ToString());
                    //                if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
                    //                {
                    //                    canCommitTransaction = true;
                    //                }
                    //                else
                    //                {
                    //                    canCommitTransaction = false;
                    //                    //ed.WriteMessage("Error For Insert \n");
                    //                }

                    //                //}




                    //                //Counter++;
                    //            }
                    //        }
                    //        Counter1++;
                    //    }
                    //}
                    Counter = 0;
                    //if (canCommitTransaction)
                    //{
                    //    containerPackage.ContainerCode = InsertRowCode;
                    //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                    //    if (containerPackage.Insert(transaction, connection))
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
                    //    Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
                    //    if (_EProductPackage.Insert(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EJackPanelWeekCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    if (_EJackPanelWeekCell.Insert(transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //        //ed.WriteMessage("Error For _EJackPanelWeekCell.Insert \n");
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
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.InsertX: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }

        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in JackPanelWeek");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

             command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in JackPanelWeek");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted  Operation Failed in WeekJackPanel ");
                    }
                }
                ed.WriteMessage("EJackpanelWeek.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.LocalUpdate : {0} \n", ex.Message));
                return false;
            }


        }


        public bool UpdateX()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPAnelWeek_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                int Counter1 = 0;
                try
                {
                    command.ExecuteNonQuery();
                    DataTable dtJackPAnelWeekCellCode = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(XCode);

                    if ((EJackPanelWeekCell.DeleteX(transaction, connection, XCode)) && canComitTransaction)
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            Atend.Base.Equipment.EContainerPackage ContainerPackageCode = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
                            if (EProductPackage.Delete(transaction, connection, ContainerPackageCode.Code) && canComitTransaction)
                            {
                                canComitTransaction = true;
                            }
                            else
                            {
                                canComitTransaction = false;
                            }
                        }
                    }


                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            if (EContainerPackage.DeleteX(transaction, connection, new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)) && canComitTransaction)
                            {
                                canComitTransaction = true;

                            }
                            else
                            {
                                canComitTransaction = false;

                            }
                        }
                    }









                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canComitTransaction)
                    {
                        while (Counter1 < cellList.Count && canComitTransaction)
                        {
                            Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                            _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter1]);
                            _EJackPanelWeekCell.JackPanelWeekXCode = XCode;


                            if (_EJackPanelWeekCell.InsertX(transaction, connection))
                            {
                                canComitTransaction = true;
                                //ed.WriteMessage("_EJackPanelWeekCell.Code=" + _EJackPanelWeekCell.Code + "\n");
                                if (canComitTransaction)
                                {
                                    containerPackage.XCode = _EJackPanelWeekCell.XCode;
                                    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Cell);
                                    if (containerPackage.InsertX(transaction, connection))
                                    {
                                        canComitTransaction = true;
                                    }
                                    else
                                    {
                                        canComitTransaction = false;
                                    }
                                }

                                //while (canCommitTransaction && Counter < EquipmentList.Count)
                                //{
                                ////ed.WriteMessage("ContainerPackage.containerCode= " + containerPackage.ContainerCode + "\n");
                                ////ed.WriteMessage("Counter1= " + Counter1 + "\n");
                                int i = Counter1 + 1;
                                Atend.Base.Equipment.EProductPackage _EProductPackage;
                                DataRow[] drProductPackage = dtglobal.Select("Cell='" + i.ToString() + "'");
                                ////ed.WriteMessage("drProductPackage.Lenght= " + drProductPackage.Length + "\n");
                                for (int j = 0; j < drProductPackage.Length; j++)
                                {
                                    _EProductPackage = new EProductPackage();
                                    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                                    _EProductPackage.Count = Convert.ToInt32(drProductPackage[j]["Count"].ToString());
                                    _EProductPackage.XCode = new Guid(drProductPackage[j]["XCode"].ToString());
                                    _EProductPackage.TableType = Convert.ToByte(drProductPackage[j]["TableType"].ToString());
                                    if (_EProductPackage.InsertX(transaction, connection) && canComitTransaction)
                                    {
                                        canComitTransaction = true;
                                    }
                                    else
                                    {
                                        canComitTransaction = false;
                                        //ed.WriteMessage("Error For Insert \n");
                                    }

                                    //}




                                    //Counter++;
                                }
                            }
                            Counter1++;
                        }
                    }

                    //operation
                    int Counter = 0;
                    if (canComitTransaction)
                    {
                        if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel)))
                        {
                            while (canComitTransaction && Counter < operationList.Count)
                            {

                                Atend.Base.Equipment.EOperation _EOperation;
                                _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                                _EOperation.XCode = XCode;
                                _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);

                                if (_EOperation.InsertX(transaction, connection) && canComitTransaction)
                                {
                                    canComitTransaction = true;
                                }
                                else
                                {
                                    canComitTransaction = false;
                                }
                                Counter++;
                            }
                        }
                    }
                    //Counter = 0;
                    ////ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
                    ////ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    //DataTable dt = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeAndType(containerPackage.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (EJackPanelWeekCell.Delete(transaction, connection, Convert.ToInt32(dr["Code"].ToString())) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;

                    //    }
                    //}
                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    //{
                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;
                    //}
                    //while (canComitTransaction && Counter < EquipmentList.Count)
                    //{
                    //    Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //    Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
                    //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
                    //    _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter]);
                    //    _EProductPackage.ContainerPackageCode = containerPackage.Code;
                    //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    if (_EProductPackage.Insert(transaction, connection) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;
                    //        //ed.WriteMessage("Error For Insert \n");
                    //    }
                    //    //_EJackPanelWeekCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
                    //    if (_EJackPanelWeekCell.Insert(transaction, connection) && canComitTransaction)
                    //    {
                    //        canComitTransaction = true;

                    //    }
                    //    else
                    //    {
                    //        canComitTransaction = false;
                    //        //ed.WriteMessage("Error For _EJackPanelWeekCell.Insert \n");
                    //    }
                    //    Counter++;
                    //}




                    if (canComitTransaction)
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
                        //throw new Exception("can not commit transaction");

                    }

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EGJackPanelWeelCell.Update: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeekCell.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }


        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction; ;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_JackPAnelWeek_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new SqlParameter("iAutoKey3pXCode", AutoKey3pXCode));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iBusXCode", BusXCode));

            //command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                bool canComitTransaction = true;
                int Counter1 = 0;
                try
                {
                    command.ExecuteNonQuery();
                    int Counter = 0;

                    return true;

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format(" ERROR EGJackPanelWeelCell.Update: {0} \n", ex1.Message));

                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {

                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeekCell.Update: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }


        }

        public static bool DeleteX(Guid JPWXCode)
        {

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(JPWXCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", JPWXCode));


            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    DataTable dtJackPAnelWeekCellCode = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(JPWXCode);

                    if ((EJackPanelWeekCell.DeleteX(transaction, connection, JPWXCode)) && canComitTransaction)
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            Atend.Base.Equipment.EContainerPackage ContainerPackageCode = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
                            if (EProductPackage.Delete(transaction, connection, ContainerPackageCode.Code) && canComitTransaction)
                            {
                                canComitTransaction = true;
                            }
                            else
                            {
                                canComitTransaction = false;
                            }
                        }
                    }


                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            if (EContainerPackage.DeleteX(transaction, connection, new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)) && canComitTransaction)
                            {
                                canComitTransaction = true;

                            }
                            else
                            {
                                canComitTransaction = false;

                            }
                        }
                    }



                    if (canComitTransaction)
                    {
                        if (EOperation.DeleteX(transaction, connection, JPWXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel)))
                        {
                            canComitTransaction = true;

                        }
                        else
                        {
                            canComitTransaction = false;
                        }
                    }
                    //if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))) & canComitTransaction)
                    //{

                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;
                    //}

                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    //{
                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;

                    //}
                    if (canComitTransaction)
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
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid JPWXCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", JPWXCode));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                Boolean canComitTransaction = true;
                try
                {
                    command.ExecuteNonQuery();

                    DataTable dtJackPAnelWeekCellCode = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(JPWXCode);

                    if ((EJackPanelWeekCell.DeleteX(transaction, connection, JPWXCode)) && canComitTransaction)
                    {
                        canComitTransaction = true;

                    }
                    else
                    {
                        canComitTransaction = false;

                    }
                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            Atend.Base.Equipment.EContainerPackage ContainerPackageCode = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
                            if (EProductPackage.Delete(transaction, connection, ContainerPackageCode.Code) && canComitTransaction)
                            {
                                canComitTransaction = true;
                            }
                            else
                            {
                                canComitTransaction = false;
                            }
                        }
                    }


                    foreach (DataRow dr in dtJackPAnelWeekCellCode.Rows)
                    {
                        if (canComitTransaction)
                        {
                            if (EContainerPackage.DeleteX(transaction, connection, new Guid(dr["XCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell)) && canComitTransaction)
                            {
                                canComitTransaction = true;

                            }
                            else
                            {
                                canComitTransaction = false;

                            }
                        }
                    }

                    if (canComitTransaction)
                    {
                        if (EOperation.DeleteX(_transaction, connection, JPWXCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel)))
                        {
                            canComitTransaction = true;

                        }
                        else
                        {
                            canComitTransaction = false;
                        }
                    }
                    //if ((EContainerPackage.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel))) & canComitTransaction)
                    //{

                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;
                    //}

                    //if (EProductPackage.Delete(transaction, connection, containerPackage.Code) & canComitTransaction)
                    //{
                    //    canComitTransaction = true;
                    //}
                    //else
                    //{
                    //    canComitTransaction = false;

                    //}
                    if (canComitTransaction)
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
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.DeleteX: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //frmDrawGroundPost
        public static EJackPanelWeek SelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("XCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Guid");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());

                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                ed.WriteMessage("*****{0}\n", reader["AutoKey3pXCode"].ToString());
                jackPanelWeek.AutoKey3pXCode = new Guid(reader["AutoKey3pXCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusXCode = new Guid(reader["BusXCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
            }
            reader.Close();
            JackPanelWeekSubEquip.Rows.Clear();
            DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(XCode);
            //ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            //foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            //{
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            ////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ////ed.WriteMessage("True\n");
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
                dr["XCode"] = new Guid(reader["XCode"].ToString());
                ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);

                //}


            }
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", jackPanelWeek.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.WeekJackPanel));
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
            return jackPanelWeek;
        }

        //ASHKTORAB
        public static EJackPanelWeek SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("XCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Guid");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());

                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = 0;// Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pXCode = new Guid(reader["AutoKey3pXCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusXCode = new Guid(reader["BusXCode"].ToString());
            }
            reader.Close();
            JackPanelWeekSubEquip.Rows.Clear();
            DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(XCode);
            //ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            //foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            //{
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            ////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                ////ed.WriteMessage("True\n");
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
                dr["XCode"] = new Guid(reader["XCode"].ToString());
                ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);

                //}


            }
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", jackPanelWeek.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.WeekJackPanel));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }
            reader.Close();
            return jackPanelWeek;
        }

        //ASHKTORAB
        public static EJackPanelWeek SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("XCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Guid");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());

                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = 0;// Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pXCode = new Guid(reader["AutoKey3pXCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusXCode = new Guid(reader["BusXCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
            }
            reader.Close();
            //JackPanelWeekSubEquip.Rows.Clear();
            //DataTable dtJackPAnelWeekCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(XCode);
            ////ed.WriteMessage("dtJackPAnelWeekCell.Count=" + dtJackPAnelWeekCell.Rows.Count + "\n");
            ////foreach (DataRow dr1 in dtJackPAnelWeekCell.Rows)
            ////{
            //command.Parameters.Clear();
            //command.CommandText = "E_JackPanelWeekCell_SelectByContainerXCodeType";
            //command.Parameters.Add(new SqlParameter("iJackPanelWeekXCode", XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Cell));
            //////ed.WriteMessage("JackPanelWeek.code:" + .code + "Type:" + Atend.Control.Enum.ProductType.Cell + "\n");
            //reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //    ////ed.WriteMessage("True\n");
            //    DataRow dr = JackPanelWeekSubEquip.NewRow();
            //    ////ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
            //    dr["XCode"] = new Guid(reader["XCode"].ToString());
            //    ////ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
            //    dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
            //    ////ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
            //    dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
            //    ////ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
            //    dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
            //    ////ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
            //    dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
            //    JackPanelWeekSubEquip.Rows.Add(dr);

            //    //}


            //}
            //reader.Close();



            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", groundPost.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundPost));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage("Productd= " + reader["ProductID"].ToString() + "\n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();
            //connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EJackPanelWeek SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pXCode = new Guid(reader["AutoKey3pXCode"].ToString());
                jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelWeek.BusXCode = new Guid(reader["BusXCode"].ToString());
            }
            else
            {
                jackPanelWeek.code = -1;
            }
            reader.Close();
            connection.Close();
            return jackPanelWeek;
        }

        //ShareOnServer
        public static EJackPanelWeek SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                    jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());
                    jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                    jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    jackPanelWeek.Comment = reader["Comment"].ToString();
                    jackPanelWeek.Name = reader["Name"].ToString();
                    jackPanelWeek.AutoKey3pXCode = new Guid(reader["AutoKey3pXCode"].ToString());
                    jackPanelWeek.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    jackPanelWeek.BusXCode = new Guid(reader["BusXCode"].ToString());
                }
                else
                {
                    jackPanelWeek.code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Ejackpanelweek.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return jackPanelWeek;
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeek_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelWeek_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanelWeek_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~//
        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbTransaction transaction;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iBusCode", BusCode));
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
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.AccessInsert : {0} \n", ex.Message));
                connection.Close();
                return false;
            }
            //try
            //{
            //    connection.Open();
            //    transaction = connection.BeginTransaction();
            //    command.Transaction = transaction;

            //    try
            //    {
            //        Code = Convert.ToInt32(command.ExecuteScalar());
            //        InsertRowCode = Code;
            //        bool canCommitTransaction = true;
            //        int Counter = 0;
            //        int Counter1 = 0;
            //        ////ed.WriteMessage("CountEquipment" + equipmentList.Count.ToString() + "\n");
            //        Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
            //        ////ed.WriteMessage("InsertedRowCode =:" + InsertRowCode.ToString() + "\n");

            //        if (canCommitTransaction)
            //        {
            //            while (Counter1 < cellList.Count && canCommitTransaction)
            //            {
            //                Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
            //                _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter1]);
            //                _EJackPanelWeekCell.JackPanelWeekCode = insertRowCode;


            //                if (_EJackPanelWeekCell.AccessInsert(transaction, connection))
            //                {
            //                    canCommitTransaction = true;
            //                    ////ed.WriteMessage("_EJackPanelWeekCell.Code="+_EJackPanelWeekCell.Code+"\n");
            //                    if (canCommitTransaction)
            //                    {
            //                        containerPackage.XCode = _EJackPanelWeekCell.XCode;
            //                        //containerPackage.ContainerCode = _EJackPanelWeekCell.Code;
            //                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Cell);
            //                        if (containerPackage.Insert(transaction, connection))
            //                        {
            //                            canCommitTransaction = true;
            //                        }
            //                        else
            //                        {
            //                            canCommitTransaction = false;
            //                        }
            //                    }

            //                    //while (canCommitTransaction && Counter < EquipmentList.Count)
            //                    //{
            //                    ////ed.WriteMessage("ContainerPackage.containerCode= "+containerPackage.+"\n");
            //                    ////ed.WriteMessage("Counter1= "+Counter1+"\n");
            //                    int i = Counter1 + 1;
            //                    Atend.Base.Equipment.EProductPackage _EProductPackage;
            //                    DataRow[] drProductPackage = dtglobal.Select("Cell='" + i.ToString() + "'");
            //                    ////ed.WriteMessage("drProductPackage.Lenght= "+drProductPackage.Length+"\n");
            //                    for (int j = 0; j < drProductPackage.Length; j++)
            //                    {
            //                        _EProductPackage = new EProductPackage();
            //                        _EProductPackage.ContainerPackageCode = containerPackage.Code;
            //                        _EProductPackage.Count = Convert.ToInt32(drProductPackage[j]["Count"].ToString());
            //                        _EProductPackage.ProductCode = Convert.ToInt32(drProductPackage[j]["ProductCode"].ToString());
            //                        _EProductPackage.TableType = Convert.ToByte(drProductPackage[j]["TableType"].ToString());
            //                        if (_EProductPackage.Insert(transaction, connection) && canCommitTransaction)
            //                        {
            //                            canCommitTransaction = true;
            //                        }
            //                        else
            //                        {
            //                            canCommitTransaction = false;
            //                            //ed.WriteMessage("Error For Insert \n");
            //                        }

            //                        //}




            //                        //Counter++;
            //                    }
            //                }
            //                Counter1++;
            //            }
            //        }
            //        Counter = 0;
            //        //if (canCommitTransaction)
            //        //{
            //        //    containerPackage.ContainerCode = InsertRowCode;
            //        //    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
            //        //    if (containerPackage.Insert(transaction, connection))
            //        //    {
            //        //        canCommitTransaction = true;
            //        //    }
            //        //    else
            //        //    {
            //        //        canCommitTransaction = false;
            //        //    }
            //        //}
            //        //while (canCommitTransaction && Counter < EquipmentList.Count)
            //        //{
            //        //    Atend.Base.Equipment.EProductPackage _EProductPackage;
            //        //    Atend.Base.Equipment.EJackPanelWeekCell _EJackPanelWeekCell;
            //        //    _EProductPackage = ((Atend.Base.Equipment.EProductPackage)equipmentList[Counter]);
            //        //    _EJackPanelWeekCell = ((Atend.Base.Equipment.EJackPanelWeekCell)CellList[Counter]);
            //        //    _EProductPackage.ContainerPackageCode = containerPackage.ContainerPackageCode;
            //        //    //_EProductPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel);
            //        //    if (_EProductPackage.Insert(transaction, connection) && canCommitTransaction)
            //        //    {
            //        //        canCommitTransaction = true;
            //        //    }
            //        //    else
            //        //    {
            //        //        canCommitTransaction = false;
            //        //        //ed.WriteMessage("Error For Insert \n");
            //        //    }
            //        //    //_EJackPanelWeekCell.ProductPackageCode = _EProductPackage.InsertRowCodeProductPackage;
            //        //    if (_EJackPanelWeekCell.Insert(transaction, connection) && canCommitTransaction)
            //        //    {
            //        //        canCommitTransaction = true;

            //        //    }
            //        //    else
            //        //    {
            //        //        canCommitTransaction = false;
            //        //        //ed.WriteMessage("Error For _EJackPanelWeekCell.Insert \n");
            //        //    }
            //        //    Counter++;
            //        //}

            //        if (canCommitTransaction)
            //        {
            //            transaction.Commit();
            //            connection.Close();
            //            return true;
            //        }
            //        else
            //        {
            //            transaction.Rollback();
            //            connection.Close();
            //            return false;
            //        }

            //    }
            //    catch (System.Exception ex1)
            //    {
            //        //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
            //        transaction.Rollback();
            //        connection.Close();
            //        return false;
            //    }
            //}
            //catch (System.Exception ex1)
            //{
            //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //    //ed.WriteMessage(string.Format(" ERROR Transaction EJackPanelWeek.Insert: {0} \n", ex1.Message));

            //    connection.Close();
            //    return false;
            //}

        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new OleDbParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iBusCode", BusCode));

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.AccessInsert : {0} \n", ex.Message));
                return false;
            }


        }

        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iFeederCount", FeederCount));
            command.Parameters.Add(new OleDbParameter("iAutoKey3pCode", AutoKey3pCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iBusCode", BusCode));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.WeekJackPanel, Code, _oldtransaction, _oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess WeekJackPanel failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPAnelWeek.AccessInsert : {0} \n", ex.Message));
                return false;
            }


        }

        //frmDrawGroundPost //StatusReport 
        public static EJackPanelWeek AccessSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("productCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
            }
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.WeekJackPanel));
            //ed.WriteMessage("JackPanelWeek.code:" + jackPanelWeek.code + "Type:" + Atend.Control.Enum.ProductType.WeekJackPanel + "\n");
            reader = command.ExecuteReader();
            JackPanelWeekSubEquip.Rows.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                //ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
                dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                //ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                //ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                //ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);

            }

            reader.Close();


            connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }

        public static EJackPanelWeek AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("productCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
            }
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.WeekJackPanel));
            reader = command.ExecuteReader();
            JackPanelWeekSubEquip.Rows.Clear();
            while (reader.Read())
            {
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);
            }

            reader.Close();
            return jackPanelWeek;
        }

        //AcDrawGroundPost
        public static EJackPanelWeek AccessSelectByCode(int Code, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            JackPanelWeekSubEquip.Columns.Clear();
            DataColumn dc = new DataColumn("productCode");
            DataColumn dc1 = new DataColumn("TableType");
            DataColumn dc2 = new DataColumn("cell");
            DataColumn dc3 = new DataColumn("Count");
            DataColumn dc4 = new DataColumn("IsNightLight");
            dc.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc);
            dc1.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc1);
            dc2.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc2);
            dc3.DataType = System.Type.GetType("System.Int32");
            JackPanelWeekSubEquip.Columns.Add(dc3);
            dc4.DataType = System.Type.GetType("System.Boolean");
            JackPanelWeekSubEquip.Columns.Add(dc4);
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _Transaction;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            else
            {
                jackPanelWeek.Code = -1;
            }
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_JackPanelWeekCell_SelectByContainerCodeType";
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.WeekJackPanel));
            //ed.WriteMessage("JackPanelWeek.code:" + jackPanelWeek.code + "Type:" + Atend.Control.Enum.ProductType.WeekJackPanel + "\n");
            reader = command.ExecuteReader();
            JackPanelWeekSubEquip.Rows.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                //ed.WriteMessage("True\n");
                DataRow dr = JackPanelWeekSubEquip.NewRow();
                //ed.WriteMessage("ProductCode= " + reader["ProductCode"].ToString() + "\n");
                dr["ProductCode"] = Convert.ToInt32(reader["ProductCode"].ToString());
                //ed.WriteMessage("TAbleType= " + reader["TableType"].ToString() + "\n");
                dr["TableType"] = Convert.ToInt32(reader["TableType"].ToString());
                //ed.WriteMessage("Count= " + reader["Count"].ToString() + "\n");
                dr["Count"] = Convert.ToInt32(reader["Count"].ToString());
                //ed.WriteMessage("IsNightLight= " + reader["IsNightLight"].ToString() + "\n");
                dr["IsNightLight"] = Convert.ToBoolean(reader["IsnightLight"]);
                //ed.WriteMessage("Cell= " + reader["Num"].ToString() + "\n");
                dr["Cell"] = Convert.ToInt32(reader["Num"].ToString());
                JackPanelWeekSubEquip.Rows.Add(dr);

            }

            reader.Close();


            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }


        public static EJackPanelWeek AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("I Am In SelectBy Code");
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            if (reader.Read())
            {
                jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelWeek.Comment = reader["Comment"].ToString();
                jackPanelWeek.Name = reader["Name"].ToString();
                jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());
                jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
            }
            reader.Close();
            connection.Close();
            //ed.WriteMessage("FinishSelect\n");
            return jackPanelWeek;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EJackPanelWeek AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_JackPanelWeek_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    //ed.WriteMessage("I Am In SelectBy Code");
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
        //    if (reader.Read())
        //    {
        //        jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
        //        jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
        //        jackPanelWeek.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        jackPanelWeek.Comment = reader["Comment"].ToString();
        //        jackPanelWeek.Name = reader["Name"].ToString();
        //        jackPanelWeek.XCode = new Guid(reader["XCode"].ToString());
        //        jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
        //    }
        //    else
        //    {
        //        jackPanelWeek.Code = -1;
        //    }
        //    reader.Close();
        //    //ed.WriteMessage("FinishSelect\n");
        //    return jackPanelWeek;
        //}

        public static EJackPanelWeek AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_JackPanelWeek_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EJackPanelWeek jackPanelWeek = new EJackPanelWeek();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    jackPanelWeek.Code = Convert.ToInt32(reader["Code"].ToString());
                    jackPanelWeek.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    jackPanelWeek.FeederCount = Convert.ToInt32(reader["FeederCount"].ToString());
                    jackPanelWeek.Comment = reader["Comment"].ToString();
                    jackPanelWeek.AutoKey3pCode = Convert.ToInt32(reader["AutoKey3pCode"].ToString());
                    jackPanelWeek.Name = reader["Name"].ToString();
                    jackPanelWeek.BusCode = Convert.ToInt32(reader["BusCode"].ToString());
                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    //ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EJackPanelWeek.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return jackPanelWeek;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeek_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsGroundPost = new DataSet();
            adapter.Fill(dsGroundPost);
            return dsGroundPost.Tables[0];

        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelWeek_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsgroundPost = new DataSet();
            adapter.Fill(dsgroundPost);

            return dsgroundPost.Tables[0];
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

        //        EJackPanelWeek _EJackPanelWeek = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
        //            //ed.WriteMessage("\n111\n");

        //            EJackPanelWeek Ap = Atend.Base.Equipment.EJackPanelWeek.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            DataTable DeletedJPWCells = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Servertransaction, Serverconnection, Ap.Code);
        //            //ed.WriteMessage("\n111\n");

        //            if (Ap.XCode != Guid.Empty)
        //            {

        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EJackPanelWeek.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                //ed.WriteMessage("\n222\n");

        //            }

        //            //EAutoKey_3p Auto = EAutoKey_3p.ServerSelectByXCode(Servertransaction, Serverconnection, _EJackPanelWeek.AutoKey3pXCode);

        //            //ed.WriteMessage("\n144\n");

        //            if (EAutoKey_3p.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EJackPanelWeek.AutoKey3pXCode))
        //            {
        //                //ed.WriteMessage("\n145n");

        //                _EJackPanelWeek.AutoKey3pCode = EAutoKey_3p.ServerSelectByXCode(Servertransaction, Serverconnection, _EJackPanelWeek.AutoKey3pXCode).Code;
        //                //ed.WriteMessage("\n146\n");
        //                //ed.WriteMessage("\n3333\n");

        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                //ed.WriteMessage("\n444\n");

        //                return false;
        //            }

        //            _EJackPanelWeek.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EJackPanelWeek.XCode);

        //            _EJackPanelWeek.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            //ed.WriteMessage("\n555\n");

        //            if (_EJackPanelWeek.Insert(Servertransaction, Serverconnection))
        //            {
        //                //ed.WriteMessage("\n666\n");

        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EJackPanelWeek.Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel))
        //                {
        //                    //ed.WriteMessage("\n777\n");

        //                    if (!_EJackPanelWeek.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        //ed.WriteMessage("\n888\n");

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
        //                    //ed.WriteMessage("\n999\n");

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
        //                //ed.WriteMessage("\n10\n");

        //                return false;
        //            }

        //            DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(Localtransaction, Localconnection, _EJackPanelWeek.XCode);

        //            foreach (DataRow JPWCRow in JPWCTbl.Rows)
        //            {
        //                Atend.Base.Equipment.EJackPanelWeekCell JWpc = new Atend.Base.Equipment.EJackPanelWeekCell();
        //                JWpc.XCode = new Guid(JPWCRow["XCode"].ToString());
        //                JWpc.IsDefault = Convert.ToBoolean(JPWCRow["IsDefault"].ToString());
        //                JWpc.JackPanelWeekCode = _EJackPanelWeek.Code; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                JWpc.Num = Convert.ToByte(JPWCRow["Num"].ToString());
        //                JWpc.IsNightLight = Convert.ToBoolean(JPWCRow["IsNightLight"].ToString());

        //                //ed.WriteMessage("\n11\n");


        //                if (!JWpc.Insert(Servertransaction, Serverconnection))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    //ed.WriteMessage("\n12\n");

        //                    return false;
        //                }

        //                Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, JWpc.XCode, (int)Atend.Control.Enum.ProductType.Cell);


        //                DataRow[] DR = DeletedJPWCells.Select("XCode = \'" + JWpc.XCode.ToString() + "\'");
        //                int d = 0;

        //                if (DR.Length > 0)
        //                    d = Convert.ToInt32(DR[0]["Code"].ToString());

        //                //ed.WriteMessage("\n13\n");

        //                if (Atend.Base.Design.NodeTransaction.SubProducts(d, JWpc.Code, JWpc.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //                {
        //                    //ed.WriteMessage("\n113\n");
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();

        //                    //ed.WriteMessage("\n14\n");
        //                    throw new Exception("while Calling SubProducts in EJackPanelWEEK");
        //                }
        //            }


        //            Servertransaction.Commit();
        //            Serverconnection.Close();

        //            Localtransaction.Commit();
        //            Localconnection.Close();
        //            //ed.WriteMessage("\n15\n");


        //            //ed.WriteMessage("\n112\n");

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            //ed.WriteMessage("\n16\n");

        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        //ed.WriteMessage("\n17\n");

        //        return false;
        //    }

        //    return true;
        //}

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

        //        EJackPanelWeek _EJackPanelWeek = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
        //            //ed.WriteMessage("\n111\n");

        //            EJackPanelWeek Ap = Atend.Base.Equipment.EJackPanelWeek.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            DataTable DeletedJPWCells = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Servertransaction, Serverconnection, Ap.Code);
        //            ed.WriteMessage("\n111\n");

        //            if (Ap.XCode != Guid.Empty)
        //            {

        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EJackPanelWeek.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                ed.WriteMessage("\n222\n");

        //            }

        //            //EAutoKey_3p Auto = EAutoKey_3p.ServerSelectByXCode(Servertransaction, Serverconnection, _EJackPanelWeek.AutoKey3pXCode);

        //            //ed.WriteMessage("\n144\n");

        //            if (EAutoKey_3p.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EJackPanelWeek.AutoKey3pXCode))
        //            {
        //                //ed.WriteMessage("\n145n");

        //                _EJackPanelWeek.AutoKey3pCode = EAutoKey_3p.ServerSelectByXCode(Servertransaction, Serverconnection, _EJackPanelWeek.AutoKey3pXCode).Code;
        //                //ed.WriteMessage("\n146\n");
        //                ed.WriteMessage("\n3333\n");

        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Servertransaction.Rollback();
        //                Serverconnection.Close();

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                ed.WriteMessage("\n444\n");

        //                return false;
        //            }

        //            _EJackPanelWeek.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EJackPanelWeek.XCode);

        //            _EJackPanelWeek.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            ed.WriteMessage("\n555\n");

        //            if (_EJackPanelWeek.Insert(Servertransaction, Serverconnection))
        //            {
        //                ed.WriteMessage("\n666\n");

        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EJackPanelWeek.Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel))
        //                {
        //                    ed.WriteMessage("\n777\n");

        //                    if (!_EJackPanelWeek.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        //ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        ed.WriteMessage("\n888\n");

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
        //                    ed.WriteMessage("\n999\n");

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
        //                ed.WriteMessage("\n10\n");

        //                return false;
        //            }

        //            DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(Localtransaction, Localconnection, _EJackPanelWeek.XCode);

        //            foreach (DataRow JPWCRow in JPWCTbl.Rows)
        //            {
        //                Atend.Base.Equipment.EJackPanelWeekCell JWpc = new Atend.Base.Equipment.EJackPanelWeekCell();
        //                JWpc.XCode = new Guid(JPWCRow["XCode"].ToString());
        //                JWpc.IsDefault = Convert.ToBoolean(JPWCRow["IsDefault"].ToString());
        //                JWpc.JackPanelWeekCode = _EJackPanelWeek.Code; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                JWpc.Num = Convert.ToByte(JPWCRow["Num"].ToString());
        //                JWpc.IsNightLight = Convert.ToBoolean(JPWCRow["IsNightLight"].ToString());

        //                ed.WriteMessage("\n11\n");


        //                if (!JWpc.Insert(Servertransaction, Serverconnection))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    ed.WriteMessage("\n12\n");

        //                    return false;
        //                }

        //                Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, JWpc.XCode, (int)Atend.Control.Enum.ProductType.Cell);


        //                DataRow[] DR = DeletedJPWCells.Select("XCode = \'" + JWpc.XCode.ToString() + "\'");
        //                int d = 0;

        //                if (DR.Length > 0)
        //                    d = Convert.ToInt32(DR[0]["Code"].ToString());

        //                ed.WriteMessage("\n13\n");

        //                if (Atend.Base.Design.NodeTransaction.SubProducts(d, JWpc.Code, JWpc.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //                {
        //                    //ed.WriteMessage("\n113\n");
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();

        //                    ed.WriteMessage("\n14\n");
        //                    throw new Exception("while Calling SubProducts in EJackPanelWEEK");
        //                }
        //            }


        //            Servertransaction.Commit();
        //            Serverconnection.Close();

        //            Localtransaction.Commit();
        //            Localconnection.Close();
        //            ed.WriteMessage("\n15\n");


        //            //ed.WriteMessage("\n112\n");

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            //ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            ed.WriteMessage("\n16\n");

        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        //ed.WriteMessage(string.Format(" ERROR EJackPanelWeek.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        ed.WriteMessage("\n17\n");

        //        return false;
        //    }

        //    return true;
        //}

        ////public static bool GetFromServer(int Code)
        ////{
        ////    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        ////    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        ////    SqlTransaction Localtransaction;

        ////    Guid DelXCode = Guid.NewGuid();

        ////    try
        ////    {

        ////        Localconnection.Open();
        ////        Localtransaction = Localconnection.BeginTransaction();


        ////        try
        ////        {

        ////            Atend.Base.Equipment.EJackPanelWeek eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Localtransaction, Localconnection, Code);
        ////            //Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
        ////            ed.WriteMessage("\n111\n");

        ////            DataTable JPWCDelTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(Localtransaction, Localconnection, eJAckPanelW.XCode);
        ////            ed.WriteMessage("\n1112222\n");

        ////            int del = 0;
        ////            if (eJAckPanelW.XCode != Guid.Empty)
        ////            {
        ////                ed.WriteMessage("\n1113333\n");

        ////                DelXCode = eJAckPanelW.XCode;
        ////                if (!Atend.Base.Equipment.EJackPanelWeek.DeleteX(Localtransaction, Localconnection, eJAckPanelW.XCode))
        ////                {
        ////                    Localtransaction.Rollback();
        ////                    Localconnection.Close();
        ////                    return false;
        ////                }
        ////                ed.WriteMessage("\n1114444\n");
        ////                eJAckPanelW.ServerSelectByCode(Code);
        ////            }
        ////            else
        ////            {
        ////                ed.WriteMessage("\n111555\n");
        ////                eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Code);
        ////                eJAckPanelW.XCode = DelXCode;
        ////                DelXCode = Guid.Empty;
        ////            }


        ////            ed.WriteMessage("\n555\n");

        ////            Atend.Base.Equipment.EAutoKey_3p Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(Localtransaction, Localconnection, eJAckPanelW.AutoKey3pCode);
        ////            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        ////            ed.WriteMessage("\n666\n");
        ////            Guid AutoDeleted = Guid.NewGuid();
        ////            if (Auto.XCode != Guid.Empty)
        ////            {
        ////                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        ////                AutoDeleted = Auto.XCode;
        ////                ed.WriteMessage("\n777\n");

        ////                if (!Atend.Base.Equipment.EAutoKey_3p.DeleteX(Localtransaction, Localconnection, Auto.XCode))
        ////                {
        ////                    throw new Exception("while delete Auto in AtendServer");
        ////                }

        ////                Auto.ServerSelectByCode(eJAckPanelW.AutoKey3pCode);
        ////            }
        ////            else
        ////            {
        ////                ed.WriteMessage("\n888\n");

        ////                Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(eJAckPanelW.AutoKey3pCode);
        ////                ed.WriteMessage("\n999\n");
        ////                Auto.XCode = AutoDeleted;
        ////                AutoDeleted = Guid.Empty;
        ////            }

        ////            ed.WriteMessage("\n1000\n");

        ////            Auto.OperationList = new ArrayList();
        ////            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Auto.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p));
        ////            Auto.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Auto.XCode);


        ////            if (!Auto.InsertX(Localtransaction, Localconnection))
        ////            {
        ////                Localtransaction.Rollback();
        ////                Localconnection.Close();
        ////                return false;
        ////            }
        ////            ed.WriteMessage("\n1001\n");

        ////            eJAckPanelW.AutoKey3pXCode = Auto.XCode;

        ////            DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Code);

        ////            foreach (DataRow JPWCRow in JPWCTbl.Rows)
        ////            {
        ////                ed.WriteMessage("\n1002 XCode = {0}\n", JPWCRow["XCode"].ToString());

        ////                Atend.Base.Equipment.EJackPanelWeekCell JackpWCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByCodeForServer(Convert.ToInt32(JPWCRow["Code"].ToString()));

        ////                DataRow[] Dr = JPWCDelTbl.Select("XCode = \'" + JPWCRow["XCode"].ToString() + "\'");
        ////                if (Dr.Length > 0)
        ////                    JackpWCell.XCode = new Guid(JPWCRow["XCode"].ToString());
        ////                else
        ////                    JackpWCell.XCode = Guid.NewGuid();
        ////                ed.WriteMessage("\n1003\n");

        ////                JackpWCell.JackPanelWeekXCode = eJAckPanelW.XCode;
        ////                if (!JackpWCell.InsertXX(Localtransaction, Localconnection))
        ////                {
        ////                    Localtransaction.Rollback();
        ////                    Localconnection.Close();
        ////                    return false;
        ////                }

        ////                ed.WriteMessage("\n1004\n");


        ////                Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JPWCRow["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        ////                if (Atend.Base.Design.NodeTransaction.SubProductsForServer(JackpWCell.Code, JackpWCell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Localtransaction, Localconnection))
        ////                {
        ////                    ed.WriteMessage("\n113\n");
        ////                }
        ////                else
        ////                {
        ////                    ed.WriteMessage("\n114\n");
        ////                    Localtransaction.Rollback();
        ////                    Localconnection.Close();
        ////                    return false;
        ////                }
        ////            }

        ////            //DelXCode = del;


        ////            eJAckPanelW.OperationList = new ArrayList();
        ////            DataTable OperationTbl1 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
        ////            eJAckPanelW.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl1, eJAckPanelW.XCode);

        ////            if (eJAckPanelW.InsertX(Localtransaction, Localconnection))
        ////            {
        ////                Localtransaction.Commit();
        ////                Localconnection.Close();
        ////                return true;
        ////            }
        ////            else
        ////            {
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
        ////        ed.WriteMessage(string.Format(" ERROR EJackPanel.GetFromServer {0}\n", ex1.Message));

        ////        Localconnection.Close();
        ////        return false;
        ////    }

        ////    return true;
        ////}


        //public static bool GetFromServer(int Code)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    Guid DelXCode = Guid.NewGuid();

        //    try
        //    {

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        try
        //        {

        //            Atend.Base.Equipment.EJackPanelWeek eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Localtransaction, Localconnection, Code);
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
        //            //ed.WriteMessage("\n111\n");

        //            DataTable JPWCDelTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekXCode(Localtransaction, Localconnection, eJAckPanelW.XCode);

        //            int del = 0;
        //            if (eJAckPanelW.XCode != Guid.Empty)
        //            {

        //                DelXCode = eJAckPanelW.XCode;
        //                if (!Atend.Base.Equipment.EJackPanelWeek.DeleteX(Localtransaction, Localconnection, eJAckPanelW.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                eJAckPanelW.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                eJAckPanelW = Atend.Base.Equipment.EJackPanelWeek.SelectByCode(Code);
        //                eJAckPanelW.XCode = DelXCode;
        //                DelXCode = Guid.Empty;
        //            }


        //            //ed.WriteMessage("\n555\n");

        //            Atend.Base.Equipment.EAutoKey_3p Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(Localtransaction, Localconnection, eJAckPanelW.AutoKey3pCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            //ed.WriteMessage("\n666\n");
        //            Guid AutoDeleted = Guid.NewGuid();
        //            if (Auto.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                AutoDeleted = Auto.XCode;
        //                //ed.WriteMessage("\n777\n");

        //                if (!Atend.Base.Equipment.EAutoKey_3p.DeleteX(Localtransaction, Localconnection, Auto.XCode))
        //                {
        //                    throw new Exception("while delete Auto in AtendServer");
        //                }

        //                Auto.ServerSelectByCode(eJAckPanelW.AutoKey3pCode);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n888\n");

        //                Auto = Atend.Base.Equipment.EAutoKey_3p.SelectByCode(eJAckPanelW.AutoKey3pCode);
        //                //ed.WriteMessage("\n999\n");
        //                Auto.XCode = AutoDeleted;
        //                AutoDeleted = Guid.Empty;
        //            }

        //            //ed.WriteMessage("\n1000\n");

        //            Auto.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Auto.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.AuoKey3p));
        //            Auto.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Auto.XCode);


        //            if (!Auto.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            //ed.WriteMessage("\n1001\n");

        //            eJAckPanelW.AutoKey3pXCode = Auto.XCode;

        //            DataTable JPWCTbl = Atend.Base.Equipment.EJackPanelWeekCell.SelectByJackPanelWeekCode(Code);

        //            foreach (DataRow JPWCRow in JPWCTbl.Rows)
        //            {
        //                //ed.WriteMessage("\n1002 XCode = {0}\n", JPWCRow["XCode"].ToString());

        //                Atend.Base.Equipment.EJackPanelWeekCell JackpWCell = Atend.Base.Equipment.EJackPanelWeekCell.SelectByCodeForServer(Convert.ToInt32(JPWCRow["Code"].ToString()));

        //                DataRow[] Dr = JPWCDelTbl.Select("XCode = \'" + JPWCRow["XCode"].ToString() + "\'");
        //                if (Dr.Length > 0)
        //                    JackpWCell.XCode = new Guid(JPWCRow["XCode"].ToString());
        //                else
        //                    JackpWCell.XCode = Guid.NewGuid();
        //                //ed.WriteMessage("\n1003\n");

        //                JackpWCell.JackPanelWeekXCode = eJAckPanelW.XCode;
        //                if (!JackpWCell.InsertXX(Localtransaction, Localconnection))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                //ed.WriteMessage("\n1004\n");


        //                Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JPWCRow["Code"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        //                if (Atend.Base.Design.NodeTransaction.SubProductsForServer(JackpWCell.Code, JackpWCell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Localtransaction, Localconnection))
        //                {
        //                    //ed.WriteMessage("\n113\n");
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("\n114\n");
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }

        //            //DelXCode = del;


        //            eJAckPanelW.OperationList = new ArrayList();
        //            DataTable OperationTbl1 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.WeekJackPanel));
        //            eJAckPanelW.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl1, eJAckPanelW.XCode);

        //            if (!eJAckPanelW.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(eJAckPanelW.Code, eJAckPanelW.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.WeekJackPanel, Localtransaction, Localconnection))
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
        //            //if (eJAckPanelW.InsertX(Localtransaction, Localconnection))
        //            //{
        //            //    Localtransaction.Commit();
        //            //    Localconnection.Close();
        //            //    return true;
        //            //}
        //            //else
        //            //{
        //            //    Localtransaction.Rollback();
        //            //    Localconnection.Close();
        //            //    return false;
        //            //}

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
        //        ed.WriteMessage(string.Format(" ERROR EJackPanel.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
