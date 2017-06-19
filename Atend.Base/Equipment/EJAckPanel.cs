using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Equipment
{
    public class EJAckPanel
    {
        public EJAckPanel()
        {
            jackPanelCell = new List<EJackPanelCell>();
            //subEquipment = new ArrayList();
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

        private int masterProductCode;
        public int MasterProductCode
        {
            get { return masterProductCode; }
            set { masterProductCode = value; }
        }

        private Guid masterProductxCode;
        public Guid MasterProductXCode
        {
            get { return masterProductxCode; }
            set { masterProductxCode = value; }
        }

        private byte masterProductType;
        public byte MasterProductType
        {
            get { return masterProductType; }
            set { masterProductType = value; }
        }

        private byte cellCount;
        public byte CellCount
        {
            get { return cellCount; }
            set { cellCount = value; }
        }

        private bool is20kv;
        public bool Is20kv
        {
            get { return is20kv; }
            set { is20kv = value; }
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

        private List<EJackPanelCell> jackPanelCell;
        public List<EJackPanelCell> JackPanelCell
        {
            get { return jackPanelCell; }
            set { jackPanelCell = value; }
        }

        public static ArrayList nodeProductCode = new ArrayList();
        public static ArrayList nodeProductType = new ArrayList();

        public static ArrayList nodeNumCell = new ArrayList();

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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Insert", Connection);
            SqlTransaction transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductCode", MasterProductCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            //Connection.Open();
            //transaction = Connection.BeginTransaction();
            //Command.Transaction = transaction;
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage(string.Format(" ERROR EJaclPAnel.Insert: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
            //try
            //{
            //    ed.WriteMessage("Execute Scaler \n");
            //    int InsertedRowCode = Convert.ToInt32(Command.ExecuteScalar());
            //    ed.WriteMessage("Executed scaler1 " + InsertedRowCode.ToString() + " \n");
            //    foreach (EJackPanelCell cell in JackPanelCell)
            //    {
            //        cell.JackPanelCode = InsertedRowCode;
            //        if (!cell.Insert(transaction, Connection))
            //        {
            //            transaction.Rollback();
            //            Connection.Close();
            //            return false;
            //        }


            //    }
            //    transaction.Commit();
            //    Connection.Close();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    ed.WriteMessage("Error In EJackPanel_Insert:"+ex.Message);
            //    transaction.Rollback();
            //    Connection.Close();
            //    return false;
            //}
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Insert", Connection);
            SqlTransaction transaction = _transaction;

            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductCode", MasterProductCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            //Connection.Open();
            //transaction = Connection.BeginTransaction();
            Command.Transaction = transaction;

            try
            {
                //ed.WriteMessage("Execute Scaler 1\n");

                Code = Convert.ToInt32(Command.ExecuteScalar());
                //ed.WriteMessage("Executed scaler1 " + Code.ToString() + " \n");
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
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
            catch (Exception ex)
            {
                ed.WriteMessage("Error In EJackPanel_Insert ERROR:" + ex.Message);
                //transaction.Rollback();
                //Connection.Close();
                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductCode", MasterProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {



                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("FC:{0}\n",Code);

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in JACKPANEL");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServre MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanel_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductCode", MasterProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {



                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("FC:{0}\n",Code);

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in JACKPANEL");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Faild in Jackpanel");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServre MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        //public bool Update()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("E_JackPanel_Update", Connection);
        //    SqlTransaction transaction;
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iName", Name));
        //    Command.Parameters.Add(new SqlParameter("iMasterProductCode", MasterProductCode));
        //    Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
        //    Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
        //    Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
        //    Command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    try
        //    {
        //        bool canCommitTransaction = true;
        //        Connection.Open();
        //        transaction = Connection.BeginTransaction();
        //        Command.Transaction = transaction;
        //        try
        //        {
        //            Command.ExecuteNonQuery();

        //            if (EJackPanelCell.Delete(transaction, Connection, Code) && canCommitTransaction)
        //            {
        //                ed.WriteMessage("DeleteCell\n");
        //                Atend.Base.Equipment.EJackPanelCell jackPanelCell = new EJackPanelCell();
        //                foreach (EJackPanelCell cell in JackPanelCell)
        //                {
        //                    cell.JackPanelCode = Code;
        //                    ed.WriteMessage("cell.JackPanelCode"+cell.JackPanelCode.ToString()+"\n");
        //                    if (!cell.Insert(transaction, Connection))
        //                    {
        //                        ed.WriteMessage("Error In Insert");
        //                        transaction.Rollback();
        //                        Connection.Close();
        //                        return false;
        //                    }
        //                }
        //            }
        //            if (canCommitTransaction)
        //            {
        //                transaction.Commit();
        //                Connection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                transaction.Rollback();
        //                Connection.Close();
        //                return false;
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            ed.WriteMessage(string.Format("Error InTransaction:{0}", ex.Message));
        //            transaction.Rollback();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch(System.Exception ex)

        //    {
        //        ed.WriteMessage(string.Format("Error In E_JackPanel_Update:{0}", ex.Message));
        //        //transaction.Rollback();
        //        Connection.Close();
        //        return false;
        //    }
        //    //try
        //    //{
        //    //    Command.ExecuteNonQuery();
        //    //    foreach (EJackPanelCell cell in JackPanelCell)
        //    //    {

        //    //        if (!cell.Update(Connection, transaction))
        //    //        {
        //    //            transaction.Rollback();
        //    //            Connection.Close();
        //    //            return false;
        //    //        }


        //    //    }
        //    //    transaction.Commit();
        //    //    Connection.Close();
        //    //    return true;
        //    //}
        //    //catch
        //    //{
        //    //    transaction.Rollback();
        //    //    Connection.Close();
        //    //    return false;
        //    //}
        //}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~//

        public static bool Delete(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool canCommittransaction = true;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    if (EJackPanelCell.Delete(transaction, Connection, Code))
                    {
                        canCommittransaction = true;
                    }
                    else
                    {
                        canCommittransaction = false;
                    }
                    if (canCommittransaction)
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
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error In E_JackPanel_Delete:{0}", ex.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch
            {

                Connection.Close();
                return true;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool canCommittransaction = true;
            SqlTransaction transaction = _transaction;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    //Atend.Base.Equipment.EJAckPanel jp = Atend.Base.Equipment.EJAckPanel.SelectByXCode(XCode);

                    if (EOperation.Delete(_transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel)))
                    {
                        canCommittransaction = true;

                    }
                    else
                    {
                        canCommittransaction = false;
                    }

                    if (EJackPanelCell.Delete(transaction, Connection, Code))
                    {
                        canCommittransaction = true;
                    }
                    else
                    {
                        canCommittransaction = false;
                    }
                    if (canCommittransaction)
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
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error In E_JackPanel_ServerDelete:{0}", ex.Message));
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch
            {

                //Connection.Close();
                return true;
            }
        }

        public static EJAckPanel SelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelCode";
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", jackPanel.Code));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductCode"].ToString() + "\n");
            }

            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            //EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                CellCount = Convert.ToByte(reader["CellCount"]);
                //Code = Convert.ToInt32(reader["Code"]);
                Comment = reader["Comment"].ToString();
                Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                Name = reader["Name"].ToString();
                //ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            reader.Close();
            //Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            //Command.Parameters.Clear();
            //Command.CommandText = "E_JackPanelCell_SelectByJackPanelCode";
            //Command.Parameters.Add(new SqlParameter("iJackPanelCode", jackPanel.Code));
            ////ed.WriteMessage("code:" + jackPanel.Code + "\n");
            //reader = Command.ExecuteReader();
            //nodeNumCell.Clear();
            //nodeProductCode.Clear();
            //nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    nodeProductType.Add(reader["ProductType"].ToString());
            //    nodeProductCode.Add(reader["ProductCode"].ToString());
            //    nodeNumCell.Add(reader["CellNum"].ToString());
            //    ed.WriteMessage("ProductCode:=" + reader["ProductCode"].ToString() + "\n");
            //}

            //reader.Close();
            Connection.Close();
            //return jackPanel;
        }

        public static DataTable SelectByType(bool Is20kv)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_SelectByType", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        public static EJAckPanel SelectByProductCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        public static DataTable DrawSearch(string Name, int CellCount)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            //ed.WriteMessage("***Rows:{0}\n", dsJackPanel.Tables[0].Rows.Count);
            return dsJackPanel.Tables[0];
        }

        public static EJAckPanel ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            Command.Transaction = _transaction;

            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            //Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            //Command.Parameters.Clear();
            //Command.CommandText = "E_JackPanelCell_SelectByJackPanelXCode";
            //Command.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanel.XCode));
            ////ed.WriteMessage("code:" + jackPanel.Code + "\n");
            //reader = Command.ExecuteReader();
            //nodeNumCell.Clear();
            //nodeProductCode.Clear();
            //nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    nodeProductType.Add(reader["ProductType"].ToString());
            //    nodeProductCode.Add(reader["ProductXCode"].ToString());
            //    nodeNumCell.Add(reader["CellNum"].ToString());
            //    ed.WriteMessage("ProductCode:=" + reader["ProductXCode"].ToString() + "\n");
            //}

            //reader.Close();
            //Connection.Close();
            return jackPanel;
        }

        //MEDHAT //ShareOnServer
        public static EJAckPanel ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = ServerConnection;
            SqlCommand Command = new SqlCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Transaction = ServerTransaction;

            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();

            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                jackPanel.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : jack panel \n");
            }
            reader.Close();
            return jackPanel;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_InsertX", Connection);
            SqlTransaction transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            Connection.Open();
            transaction = Connection.BeginTransaction();
            Command.Transaction = transaction;

            try
            {
                //ed.WriteMessage("Execute Scaler \n");
                int InsertedRowCode = Convert.ToInt32(Command.ExecuteScalar());
                bool canCommitTransaction = true;
                int Counter = 0;

                foreach (EJackPanelCell cell in JackPanelCell)
                {
                    if (canCommitTransaction == true)
                    {
                        cell.JackPanelXCode = XCode;
                        if (!cell.InsertX(transaction, Connection))
                        {
                            //transaction.Rollback();
                            //Connection.Close();
                            //return false;
                            canCommitTransaction = false;
                        }
                    }
                }


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
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

                //Package
                Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                if (canCommitTransaction)
                {
                    containerPackage.XCode = XCode;
                    containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
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
                /////////////
                transaction.Commit();
                Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error In EJackPanel_InsertX:" + ex.Message);
                transaction.Rollback();
                Connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_InsertX", Connection);
            SqlTransaction transaction = _transaction;
            Command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            //Connection.Open();
            //transaction = Connection.BeginTransaction();
            Command.Transaction = transaction;

            try
            {
                //ed.WriteMessage("Execute Scaler \n");
                //int InsertedRowCode = Convert.ToInt32(Command.ExecuteScalar());
                Command.ExecuteNonQuery();
                //ed.WriteMessage("Executed scaler1 " + InsertedRowCode.ToString() + " \n");
                //foreach (EJackPanelCell cell in JackPanelCell)
                //{
                //    cell.JackPanelXCode = XCode;
                //    if (!cell.InsertX(transaction, Connection))
                //    {
                //        //transaction.Rollback();
                //        //Connection.Close();
                //        return false;
                //    }


                //}
                //transaction.Commit();
                //Connection.Close();

                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);
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
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error In EJackPanel_InsertX:" + ex.Message);
                //transaction.Rollback();
                //Connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanel_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in JACKPANEL");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServre MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanel_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {



                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("FC:{0}\n",Code);

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in JACKPANEL");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Faild in Jackpanel");
                    }
                }
                ed.WriteMessage("EJackpanel.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServre MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_UpdateX", Connection);
            SqlTransaction transaction;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            try
            {
                bool canCommitTransaction = true;
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();

                    if (EJackPanelCell.DeleteX(transaction, Connection, XCode) && canCommitTransaction)
                    {
                        //ed.WriteMessage("DeleteCell\n");
                        Atend.Base.Equipment.EJackPanelCell jackPanelCell = new EJackPanelCell();
                        foreach (EJackPanelCell cell in JackPanelCell)
                        {
                            if (canCommitTransaction)
                            {
                                cell.JackPanelXCode = XCode;
                                if (!cell.InsertX(transaction, Connection))
                                {
                                    //ed.WriteMessage("Error In Insert");
                                    //transaction.Rollback();
                                    //Connection.Close();
                                    //return false;
                                    canCommitTransaction = false;
                                }
                            }
                        }
                    }

                    int Counter = 0;
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel)) && canCommitTransaction)
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel);

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

                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
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
                    //*************
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
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error InTransaction:{0}", ex.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanel_UpdateX:{0}", ex.Message));
                //transaction.Rollback();
                Connection.Close();
                return false;
            }
            //try
            //{
            //    Command.ExecuteNonQuery();
            //    foreach (EJackPanelCell cell in JackPanelCell)
            //    {

            //        if (!cell.Update(Connection, transaction))
            //        {
            //            transaction.Rollback();
            //            Connection.Close();
            //            return false;
            //        }


            //    }
            //    transaction.Commit();
            //    Connection.Close();
            //    return true;
            //}
            //catch
            //{
            //    transaction.Rollback();
            //    Connection.Close();
            //    return false;
            //}
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_UpdateX", Connection);
            SqlTransaction transaction = _transaction;

            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iMasterProductXCode", MasterProductXCode));
            Command.Parameters.Add(new SqlParameter("iMasterProductType", MasterProductType));
            Command.Parameters.Add(new SqlParameter("iCellCount", CellCount));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iIs20kv", Is20kv));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            try
            {
                bool canCommitTransaction = true;
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();

                    //if (EJackPanelCell.DeleteX(transaction, Connection, XCode) && canCommitTransaction)
                    //{
                    //    ed.WriteMessage("DeleteCell\n");
                    //    Atend.Base.Equipment.EJackPanelCell jackPanelCell = new EJackPanelCell();
                    //    foreach (EJackPanelCell cell in JackPanelCell)
                    //    {
                    //        cell.JackPanelXCode = XCode;
                    //        ed.WriteMessage("cell.JackPanelXCode" + cell.JackPanelXCode.ToString() + "\n");
                    //        if (!cell.InsertX(transaction, Connection))
                    //        {
                    //            ed.WriteMessage("Error In Insert");
                    //            transaction.Rollback();
                    //            Connection.Close();
                    //            return false;
                    //        }
                    //    }
                    //}


                    int Counter = 0;

                    return true;

                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error InTransaction:{0}", ex.Message));
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanel_UpdateX:{0}", ex.Message));
                //transaction.Rollback();
                //Connection.Close();
                return false;
            }
            //try
            //{
            //    Command.ExecuteNonQuery();
            //    foreach (EJackPanelCell cell in JackPanelCell)
            //    {

            //        if (!cell.Update(Connection, transaction))
            //        {
            //            transaction.Rollback();
            //            Connection.Close();
            //            return false;
            //        }


            //    }
            //    transaction.Commit();
            //    Connection.Close();
            //    return true;
            //}
            //catch
            //{
            //    transaction.Rollback();
            //    Connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool canCommittransaction = true;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    //Atend.Base.Equipment.EJAckPanel jp = Atend.Base.Equipment.EJAckPanel.SelectByXCode(XCode);

                    if (EJackPanelCell.DeleteX(transaction, Connection, XCode))
                    {
                        canCommittransaction = true;
                    }
                    else
                    {
                        canCommittransaction = false;
                    }
                    if (EOperation.DeleteX(transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel)) && canCommittransaction)
                    {
                        canCommittransaction = true;
                    }
                    else
                    {
                        canCommittransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel))) & canCommittransaction)
                    {
                        canCommittransaction = true;
                    }
                    else
                    {
                        canCommittransaction = false;
                    }

                    if (EProductPackage.Delete(transaction, Connection, containerPackage.Code) & canCommittransaction)
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
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error In E_JackPanel_DeleteX:{0}", ex.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }


            }
            catch
            {

                Connection.Close();
                return true;
            }
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            bool canCommittransaction = true;
            SqlTransaction transaction = _transaction;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    //Atend.Base.Equipment.EJAckPanel jp = Atend.Base.Equipment.EJAckPanel.SelectByXCode(XCode);

                    //if (EJackPanelCell.DeleteX(transaction, Connection, XCode))
                    //{
                    //    canCommittransaction = true;
                    //}
                    //else
                    //{
                    //    canCommittransaction = false;
                    //}
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel)))
                    {
                        canCommittransaction = true;

                    }
                    else
                    {
                        canCommittransaction = false;
                    }
                    if (canCommittransaction)
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
                catch (System.Exception ex)
                {
                    ed.WriteMessage(string.Format("Error In E_JackPanel_DeleteX:{0}", ex.Message));
                    //transaction.Rollback();
                    //Connection.Close();
                    return false;
                }


            }
            catch
            {

                //Connection.Close();
                return true;
            }
        }

        //frmDrawGroundPost
        public static EJAckPanel SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelXCode";
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanel.XCode));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductXCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductXCode"].ToString() + "\n");
            }

            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", jackPanel.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MiddleJackPanel));
            reader = Command.ExecuteReader();
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
            Command.Parameters.Clear();
            Command.CommandText = "E_Operation_SelectByXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", jackPanel.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MiddleJackPanel));
            reader = Command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Atend.Base.Equipment.EOperation Operation = new EOperation();
                Operation.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Operation.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeys.Add(Operation);

            }

            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        //ASHKTORAB
        public static EJAckPanel SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            Command.Transaction = _transaction;

            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                //ed.WriteMessage("NNNNAAAAAMEEEE = " + reader["Name"].ToString() + "\n");
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelXCode";
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanel.XCode));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductXCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductXCode"].ToString() + "\n");
            }

            reader.Close();
            //EQUIPMENT
            Command.Parameters.Clear();
            Command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", jackPanel.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MiddleJackPanel));
            reader = Command.ExecuteReader();
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
            Command.Parameters.Clear();
            Command.CommandText = "E_Operation_SelectByXCodeType";
            Command.Parameters.Add(new SqlParameter("iXCode", jackPanel.XCode));
            Command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.MiddleJackPanel));
            reader = Command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            //Connection.Close();
            return jackPanel;
        }

        //ASHKTORAB
        public static EJAckPanel SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\n331\n");

            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_Select", Connection);
            //ed.WriteMessage("\n332\n");
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            //Connection.Open();
            Command.Transaction = _transaction;
            //ed.WriteMessage("\n333\n");

            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            //ed.WriteMessage("\n334\n");
            if (reader.Read())
            {
                //ed.WriteMessage("\n336\n");

                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                //ed.WriteMessage("\n335\n");
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                //ed.WriteMessage("\n337\n");
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                //ed.WriteMessage("NNNNAAAAAMEEEE = " + reader["Name"].ToString() + "\n");
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                //ed.WriteMessage("\n338\n");
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                //ed.WriteMessage("\n339\n");
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            //Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            //Command.Parameters.Clear();
            //Command.CommandText = "E_JackPanelCell_SelectByJackPanelXCode";
            //Command.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanel.XCode));
            ////ed.WriteMessage("code:" + jackPanel.Code + "\n");
            //reader = Command.ExecuteReader();
            //nodeNumCell.Clear();
            //nodeProductCode.Clear();
            //nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    nodeProductType.Add(reader["ProductType"].ToString());
            //    nodeProductCode.Add(reader["ProductXCode"].ToString());
            //    nodeNumCell.Add(reader["CellNum"].ToString());
            //    ed.WriteMessage("ProductCode:=" + reader["ProductXCode"].ToString() + "\n");
            //}

            //reader.Close();
            //ed.WriteMessage("\n340\n");
            //Connection.Close();
            return jackPanel;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EJAckPanel SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["XCode"].ToString());
                jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        //ShareOnServer
        public static EJAckPanel SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = LocalConnection;
            SqlCommand Command = new SqlCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EJAckPanel jackPanel = new EJAckPanel();

            try
            {
                Command.Transaction = LocalTransaction;
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                    jackPanel.Code = Convert.ToInt32(reader["Code"]);
                    jackPanel.Comment = reader["Comment"].ToString();
                    jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                    jackPanel.MasterProductXCode = new Guid(reader["MasterProductXCode"].ToString());
                    jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                    jackPanel.Name = reader["Name"].ToString();
                    jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    jackPanel.XCode = new Guid(reader["XCode"].ToString());
                    jackPanel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                }
                else
                {
                    jackPanel.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Ejackpanel.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }
            return jackPanel;
        }

        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_JackPanel_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductCode", MasterProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new OleDbParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new OleDbParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));

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
                ed.WriteMessage(string.Format("Error EJackpanel.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductCode", MasterProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new OleDbParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new OleDbParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));

            try
            {



                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("FC:{0}\n",Code);

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("i as in jackpanel insert \n");
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductCode", MasterProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMasterProductType", MasterProductType));
            insertCommand.Parameters.Add(new OleDbParameter("iCellcount", CellCount));
            insertCommand.Parameters.Add(new OleDbParameter("iIs20kv", Is20kv));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
int OldCode=Code;
            try
            {



                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("FC:{0}\n",Code);

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction , _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess MiddleJackPanel failed");
                    }
                }


                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackpanel.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //frmDrawGroundPost //StatusReport 
        public static EJAckPanel AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelCode";
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanel.Code));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductCode"].ToString() + "\n");
            }

            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        public static EJAckPanel AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelCode";
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanel.Code));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductCode"].ToString() + "\n");
            }

            reader.Close();
            return jackPanel;
        }


        public static EJAckPanel AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _transaction;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
           
            reader.Close();
            //Connection.Close();
            return jackPanel;
        }

        //AcDrawGroundPost
        public static EJAckPanel AccessSelectByCode(int Code, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _Connection;
            OleDbCommand Command = new OleDbCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Transaction = _Transaction;

            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            else
            {
                jackPanel.Code = -1;
            }
            reader.Close();
            Atend.Base.Equipment.EJackPanelCell jackpanelCel = new Atend.Base.Equipment.EJackPanelCell();
            Command.Parameters.Clear();
            Command.CommandText = "E_JackPanelCell_SelectByJackPanelCode";
            Command.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanel.Code));
            //ed.WriteMessage("code:" + jackPanel.Code + "\n");
            reader = Command.ExecuteReader();
            nodeNumCell.Clear();
            nodeProductCode.Clear();
            nodeProductType.Clear();
            //ed.WriteMessage("DoSelect");
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeProductType.Add(reader["ProductType"].ToString());
                nodeProductCode.Add(reader["ProductCode"].ToString());
                nodeNumCell.Add(reader["CellNum"].ToString());
                //ed.WriteMessage("ProductCode:=" + reader["ProductCode"].ToString() + "\n");
            }

            reader.Close();
            return jackPanel;
        }

        public static EJAckPanel AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanel_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanel.XCode = new Guid(reader["ProductCode"].ToString());
            }
            reader.Close();
            Connection.Close();
            return jackPanel;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EJAckPanel AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection Connection = _connection;
        //    OleDbCommand Command = new OleDbCommand("E_JackPanel_SelectByXCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    Command.Transaction = _transaction;

        //    OleDbDataReader reader = Command.ExecuteReader();
        //    EJAckPanel jackPanel = new EJAckPanel();
        //    if (reader.Read())
        //    {
        //        jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
        //        jackPanel.Code = Convert.ToInt32(reader["Code"]);
        //        //ed.WriteMessage("EJ code : {0}\n", jackPanel.Code);
        //        jackPanel.Comment = reader["Comment"].ToString();
        //        jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
        //        jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
        //        jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
        //        jackPanel.Name = reader["Name"].ToString();
        //        jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
        //        jackPanel.XCode = new Guid(reader["XCode"].ToString());

        //    }
        //    else
        //    {
        //        jackPanel.Code = -1;
        //    }
        //    reader.Close();
        //    return jackPanel;
        //}

        public static DataTable AccessSelectByType(bool Is20kv)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanel_SelectByType", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIs20kv", Is20kv));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanel_Select", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsJackPanel = new DataSet();
            adapter.Fill(dsJackPanel);
            return dsJackPanel.Tables[0];
        }

        public static EJAckPanel AccessSelectByProductCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanel_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJAckPanel jackPanel = new EJAckPanel();
            if (reader.Read())
            {
                jackPanel.CellCount = Convert.ToByte(reader["CellCount"]);
                jackPanel.Code = Convert.ToInt32(reader["Code"]);
                jackPanel.Comment = reader["Comment"].ToString();
                jackPanel.Is20kv = Convert.ToBoolean(reader["Is20kv"]);
                jackPanel.MasterProductCode = Convert.ToInt32(reader["MasterProductCode"]);
                jackPanel.MasterProductType = Convert.ToByte(reader["MasterProductType"]);
                jackPanel.Name = reader["Name"].ToString();
                jackPanel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
            }
            reader.Close();
            Connection.Close();
            return jackPanel;
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

        //        EJAckPanel _EJAckPanel = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
        //            ed.WriteMessage("______ containerpackage:{0}\n", containerPackage.Code);
        //            EJAckPanel Ap = Atend.Base.Equipment.EJAckPanel.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            ed.WriteMessage("_______ xcode:{0}\n", XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EJAckPanel.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }
        //            ed.WriteMessage("__________  _EJAckPanel.XCode:{0}\n", _EJAckPanel.XCode);
        //            ed.WriteMessage("__________  _EJAckPanel.MasterProductXCode:{0}\n", _EJAckPanel.MasterProductXCode);
        //            if (Atend.Base.Equipment.EBus.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EJAckPanel.MasterProductXCode))
        //            {
        //                _EJAckPanel.MasterProductCode = Atend.Base.Equipment.EBus.ServerSelectByXCode(Servertransaction, Serverconnection, _EJAckPanel.MasterProductXCode).Code;
        //            }
        //            else
        //            {
        //                throw new Exception("while .... Share On Server at Bus Of JackPanel in AtendServer");
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EJAckPanel.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EJAckPanel.XCode);
        //            _EJAckPanel.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_EJAckPanel.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EJAckPanel.Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel))
        //                {
        //                    if (!_EJAckPanel.UpdateX(Localtransaction, Localconnection))
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

        //            DataTable JPCTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelXCode(Localconnection, Localtransaction, _EJAckPanel.XCode);
        //            foreach (DataRow JPCRow in JPCTbl.Rows)
        //            {
        //                //---------------------------------
        //                //Atend.Base.Equipment.EJackPanelCell Jpc = new Atend.Base.Equipment.EJackPanelCell();
        //                //Jpc.XCode = new Guid(JPCRow["XCode"].ToString());
        //                //Jpc.IsDefault = Convert.ToBoolean(JPCRow["IsDefault"].ToString());
        //                //Jpc.JackPanelCode = _EJAckPanel.Code; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                //Jpc.Num = Convert.ToByte(JPCRow["CellNum"].ToString());
        //                //Jpc.ProductType = Convert.ToByte(JPCRow["ProductType"].ToString());
        //                //Jpc.ProductCode = cell.Code;

        //                //if (!Jpc.Insert(Servertransaction, Serverconnection))
        //                //{
        //                //    Servertransaction.Rollback();
        //                //    Serverconnection.Close();
        //                //    Localtransaction.Rollback();
        //                //    Localconnection.Close();
        //                //    return false;
        //                //}
        //                //********************************* 
        //                Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByXCode(Localtransaction, Localconnection, new Guid(JPCRow["ProductXCode"].ToString()));
        //                Atend.Base.Equipment.EContainerPackage containerPackagecell = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, new Guid(JPCRow["ProductXCode"].ToString()), Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
        //                ECell Apcell = Atend.Base.Equipment.ECell.ServerSelectByXCode(Servertransaction, Serverconnection, new Guid(JPCRow["ProductXCode"].ToString()));
        //                if (Apcell.XCode != Guid.Empty)
        //                {
        //                    DeletedCode = Apcell.Code;
        //                    if (!Atend.Base.Equipment.ECell.ServerDelete(Servertransaction, Serverconnection, Apcell.Code))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();

        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                    }
        //                }

        //                DataTable OperationTbl2 = new DataTable();
        //                cell.OperationList = new ArrayList();
        //                OperationTbl2 = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, cell.XCode);
        //                cell.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl2);

        //                if (cell.Insert(Servertransaction, Serverconnection))
        //                {
        //                    if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, cell.Code, (int)Atend.Control.Enum.ProductType.Cell))
        //                    {
        //                        if (Atend.Base.Equipment.EJackPanelCell.Update(Serverconnection, Servertransaction, DeletedCode, cell.Code))
        //                        {
        //                            if (!cell.UpdateX(Localtransaction, Localconnection))
        //                            {
        //                                Servertransaction.Rollback();
        //                                Serverconnection.Close();
        //                                Localtransaction.Rollback();
        //                                Localconnection.Close();
        //                                return false;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
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

        //                if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, cell.Code, cell.XCode, containerPackagecell.Code, (int)Atend.Control.Enum.ProductType.Cell, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //                {
        //                    //Servertransaction.Commit();
        //                    //Serverconnection.Close();
        //                    //Localtransaction.Commit();
        //                    //Localconnection.Close();
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                ed.WriteMessage("___________1\n");
        //                EJackPanelCell JackPanelCell = EJackPanelCell.SelectByXCode(Localtransaction, Localconnection, new Guid(JPCRow["XCode"].ToString()));
        //                int DeletedCodejackcell = 0;
        //                ed.WriteMessage("___________2\n");
        //                EJackPanelCell Apjackcell = Atend.Base.Equipment.EJackPanelCell.SelectByCode(Serverconnection, Servertransaction, Convert.ToInt32(JPCRow["Code"].ToString()));
        //                ed.WriteMessage("___________3\n");
        //                if (Apjackcell.XCode != Guid.Empty)
        //                {
        //                    ed.WriteMessage("___________4\n");
        //                    DeletedCodejackcell = Apjackcell.Code;
        //                    if (!Atend.Base.Equipment.EJackPanelCell.Delete(Servertransaction, Serverconnection, Apjackcell.Code))
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                }
        //                ed.WriteMessage("___________5\n");
        //                JackPanelCell.XCode = new Guid(JPCRow["XCode"].ToString());
        //                JackPanelCell.IsDefault = Convert.ToBoolean(JPCRow["IsDefault"].ToString());
        //                JackPanelCell.JackPanelCode = _EJAckPanel.Code; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                JackPanelCell.Num = Convert.ToByte(JPCRow["CellNum"].ToString());
        //                JackPanelCell.ProductType = Convert.ToByte(JPCRow["ProductType"].ToString());
        //                JackPanelCell.ProductCode = cell.Code;
        //                if (JackPanelCell.Insert(Servertransaction, Serverconnection))
        //                {
        //                    ed.WriteMessage("___________6\n");
        //                    if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCodejackcell, JackPanelCell.Code, (int)Atend.Control.Enum.ProductType.Cell))
        //                    {
        //                        ed.WriteMessage("___________7\n");
        //                        if (!JackPanelCell.UpdateX(Localconnection, Localtransaction))
        //                        {
        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                        ed.WriteMessage("___________7.1\n");
        //                    }
        //                    else
        //                    {
        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    ed.WriteMessage("___________7.2\n");
        //                }
        //                else
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //*********************************

        //                ed.WriteMessage("___________8\n");

        //                //---------------------------------

        //                //Atend.Base.Equipment.ECell cell11 = Atend.Base.Equipment.ECell.ServerSelectByXCode(Servertransaction, Serverconnection, new Guid(JPCRow["ProductXCode"].ToString()));
        //                //int Code = cell11.Code;
        //                //if (!Atend.Base.Equipment.ECell.ServerDelete(Servertransaction, Serverconnection, cell11.Code))
        //                //    throw new Exception("while delete cell in AtendServer");
        //                ////ed.WriteMessage("\n122\n");

        //                //Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByXCode(Localtransaction, Localconnection, new Guid(JPCRow["ProductXCode"].ToString()));

        //                ////ed.WriteMessage("\n225\n");

        //                //OperationTbl = new DataTable();
        //                //cell.OperationList = new ArrayList();
        //                //OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, cell.XCode);
        //                //cell.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //                //if (cell.Insert(Servertransaction, Serverconnection))
        //                //{
        //                //    if (Atend.Base.Equipment.EJackPanelCell.Update(Serverconnection, Servertransaction, Code, cell.Code))
        //                //    {
        //                //        if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, Code, cell.Code, (int)Atend.Control.Enum.ProductType.Cell))
        //                //        {
        //                //            if (!cell.UpdateX(Localtransaction, Localconnection))
        //                //                throw new Exception("while insert cell in AtendServer");
        //                //        }
        //                //        else
        //                //            throw new Exception("while insert cell in AtendServer");
        //                //    }
        //                //    else
        //                //        throw new Exception("while insert cell in AtendServer");
        //                //}
        //                //else
        //                //    throw new Exception("while insert cell in AtendServer");



        //                //Atend.Base.Equipment.EJackPanelCell Jpc = new Atend.Base.Equipment.EJackPanelCell();
        //                ////Jpc.Code = 0;
        //                //Jpc.XCode = new Guid(JPCRow["XCode"].ToString());
        //                //Jpc.IsDefault = Convert.ToBoolean(JPCRow["IsDefault"].ToString());
        //                //Jpc.JackPanelCode = _EJAckPanel.Code; //Convert.ToInt32(JPCRow["JackPanelXCode"].ToString());
        //                //Jpc.Num = Convert.ToByte(JPCRow["CellNum"].ToString());
        //                //Jpc.ProductType = Convert.ToByte(JPCRow["ProductType"].ToString());
        //                //Jpc.ProductCode = cell.Code;
        //                ////_EJAckPanel.JackPanelCell.Add(Jpc);
        //                ////ed.WriteMessage("\n124\n");

        //                //if (!Jpc.Insert(Servertransaction, Serverconnection))
        //                //{
        //                //    Servertransaction.Rollback();
        //                //    Serverconnection.Close();
        //                //    Localtransaction.Rollback();
        //                //    Localconnection.Close();
        //                //    return false;
        //                //}

        //                ////ed.WriteMessage("\n125\n");


        //                //Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, new Guid(JPCRow["ProductXCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);

        //                ////ed.WriteMessage("\n126\n");

        //                //if (Atend.Base.Design.NodeTransaction.SubProducts(Code, cell.Code, cell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //                //{
        //                //    //ed.WriteMessage("\n113\n");
        //                //}
        //                //else
        //                //{
        //                //    Servertransaction.Rollback();
        //                //    Serverconnection.Close();
        //                //    Localtransaction.Rollback();
        //                //    Localconnection.Close();

        //                //    //ed.WriteMessage("\n114\n");
        //                //    throw new Exception("while Calling SubProducts in EJackPanel");
        //                //}
        //            }


        //            Servertransaction.Commit();
        //            Serverconnection.Close();

        //            Localtransaction.Commit();
        //            Localconnection.Close();


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
        //        ed.WriteMessage(string.Format(" ERROR EJackPanel.ShareOnServer {0}\n", ex1.Message));

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

        //    Guid DelXCode = Guid.NewGuid();

        //    try
        //    {

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        try
        //        {

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
        //            //ed.WriteMessage("\n111\n");

        //            Atend.Base.Equipment.EJAckPanel eJAckPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (eJAckPanel.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n222\n");

        //                DelXCode = eJAckPanel.XCode;
        //                if (!Atend.Base.Equipment.EJAckPanel.DeleteX(Localtransaction, Localconnection, eJAckPanel.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;

        //                }
        //                //ed.WriteMessage("\n333\n");

        //                eJAckPanel.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n444\n");

        //                eJAckPanel = Atend.Base.Equipment.EJAckPanel.SelectByCode(Code);
        //                eJAckPanel.XCode = DelXCode;
        //                DelXCode = Guid.Empty;
        //            }
        //            //ed.WriteMessage("\n555\n");

        //            Atend.Base.Equipment.EBus bus = Atend.Base.Equipment.EBus.SelectByCode(Localtransaction, Localconnection, eJAckPanel.MasterProductCode);
        //            ////ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");

        //            Guid busDeleted = Guid.NewGuid();
        //            //ed.WriteMessage("\n666\n");

        //            if (bus.XCode != Guid.Empty)
        //            {
        //                ////ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                busDeleted = bus.XCode;

        //                //ed.WriteMessage("\n777\n");

        //                if (!Atend.Base.Equipment.EBus.DeleteX(Localtransaction, Localconnection, bus.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                //ed.WriteMessage("\n888\n");

        //                bus.ServerSelectByCode(eJAckPanel.MasterProductCode);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n999\n");

        //                bus = Atend.Base.Equipment.EBus.SelectByCode(eJAckPanel.MasterProductCode);
        //                bus.XCode = busDeleted;
        //                busDeleted = Guid.Empty;
        //            }

        //            //ed.WriteMessage("\n11\n");

        //            bus.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(bus.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Bus));
        //            bus.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, bus.XCode);

        //            if (!bus.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            //ed.WriteMessage("\n12\n");


        //            eJAckPanel.MasterProductXCode = bus.XCode;


        //            DataTable JPCTbl = Atend.Base.Equipment.EJackPanelCell.SelectByJackPanelCode(Code);

        //            //ed.WriteMessage("\n13\n");

        //            foreach (DataRow JPCRow in JPCTbl.Rows)
        //            {
        //                //ed.WriteMessage("\n14\n");

        //                Atend.Base.Equipment.ECell cell11 = Atend.Base.Equipment.ECell.SelectByCode(Localtransaction, Localconnection, Convert.ToInt32(JPCRow["ProductCode"].ToString()));

        //                //DelXCode = cell11.XCode;
        //                //ed.WriteMessage("\n15\n");

        //                if (!Atend.Base.Equipment.ECell.DeleteX(Localtransaction, Localconnection, cell11.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                //ed.WriteMessage("\n16   {0}\n", JPCRow["ProductCode"].ToString());

        //                Atend.Base.Equipment.ECell cell = Atend.Base.Equipment.ECell.SelectByCode(Convert.ToInt32(JPCRow["ProductCode"].ToString()));

        //                //ed.WriteMessage("\n17\n");

        //                cell.OperationList = new ArrayList();
        //                DataTable OperationTbl11 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(cell.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Cell));
        //                cell.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl11, cell.XCode);

        //                if (!cell.InsertX(Localtransaction, Localconnection))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }


        //                //ed.WriteMessage("\n18\n");



        //                ///////////////////////

        //                Atend.Base.Equipment.EJackPanelCell JackpCell = Atend.Base.Equipment.EJackPanelCell.SelectByCode(Localconnection, Localtransaction, Convert.ToInt32(JPCRow["Code"].ToString()));
        //                //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //                Guid JPCDeleted = Guid.NewGuid();
        //                //ed.WriteMessage("\n19\n");

        //                if (JackpCell.XCode != Guid.Empty)
        //                {
        //                    //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                    JPCDeleted = JackpCell.XCode;
        //                    //ed.WriteMessage("\n20 EJackPanel.XCode = {0}\n", eJAckPanel.XCode.ToString());

        //                    if (!Atend.Base.Equipment.EJackPanelCell.DeleteX(Localtransaction, Localconnection, eJAckPanel.XCode))
        //                    {
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }

        //                    //ed.WriteMessage("\n21\n");
        //                    JackpCell.ServerSelectByCode(Convert.ToInt32(JPCRow["Code"].ToString()));
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("\n22 JackPanelCell.Code = {0}\n", JPCRow["Code"].ToString());

        //                    JackpCell = Atend.Base.Equipment.EJackPanelCell.SelectByCodeForServer(Convert.ToInt32(JPCRow["Code"].ToString()));
        //                    JackpCell.XCode = JPCDeleted;
        //                    JPCDeleted = Guid.Empty;
        //                }
        //                //ed.WriteMessage("\n23\n");

        //                JackpCell.JackPanelXCode = eJAckPanel.XCode;
        //                JackpCell.ProductXCode = cell.XCode;


        //                if (!JackpCell.InsertXX(Localtransaction, Localconnection))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                //ed.WriteMessage("\n24\n");


        //                Atend.Base.Equipment.EContainerPackage CP = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Convert.ToInt32(JPCRow["ProductCode"].ToString()), (int)Atend.Control.Enum.ProductType.Cell);
        //                //ed.WriteMessage("\n25\n");


        //                if (Atend.Base.Design.NodeTransaction.SubProductsForServer(cell.Code, cell.XCode, CP.Code, (int)Atend.Control.Enum.ProductType.Cell, Localtransaction, Localconnection))
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
        //            //ed.WriteMessage("\n26\n");

        //            eJAckPanel.OperationList = new ArrayList();
        //            DataTable OperationTbl1 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.MiddleJackPanel));
        //            eJAckPanel.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl1, eJAckPanel.XCode);

        //            if (!eJAckPanel.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(eJAckPanel.Code, eJAckPanel.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.MiddleJackPanel, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EJackPanel.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
