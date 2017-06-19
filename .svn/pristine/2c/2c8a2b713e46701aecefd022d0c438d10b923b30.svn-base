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
    public class EHeaderCabel
    {
        public EHeaderCabel()
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

        private byte material;
        public byte Material
        {
            get { return material; }
            set { material = value; }
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

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private int containerCode;
        public int ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private double voltage;
        public double Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

        private double crossSectionArea;
        public double CrossSectionArea
        {
            get { return crossSectionArea; }
            set { crossSectionArea = value; }
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

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            //command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            //command.Parameters.Add(new SqlParameter("iVol", Vol));


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
                ed.WriteMessage(string.Format(" ERROR EHeaderCable.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            //command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iVol", Vol));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));

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


                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Occured During EHeader Insertion 01 : {0} \n", ex1.Message));
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
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_HeaderCabel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("CODE={0}\n",Code);
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in HeaderCabel");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.ServerInsert : {0} \n", ex.Message));
                return false;
            }

        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_HeaderCabel_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("CODE={0}\n",Code);
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.HeaderCabel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in HeaderCabel");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in HeaderCabel");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.ServerInsert : {0} \n", ex.Message));
                return false;
            }

        }

        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_HeaderCabel_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
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
        //        ed.WriteMessage(string.Format(" ERROR EHeaderCabel.Insert: {0} \n", ex1.Message));


        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Delete", connection);
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


                    //if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
                    //{
                    //    transaction.Commit();
                    //    connection.Close();
                    //    return true;

                    //}
                    //else
                    //{
                    //    transaction.Rollback();
                    //    connection.Close();
                    //    return false;
                    //}
                    transaction.Commit();
                    connection.Close();
                    return true;

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EHeaderCabel.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHeaderCabel.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Delete", connection);
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


                    bool canCommitTransaction = true;
                    if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
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
                    ed.WriteMessage(string.Format("Error EHeaderCabel.ServerDelete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHeaderCabel.ServerDelete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EHeaderCabel SelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            reader.Close();
            connection.Close();
            return headerCabel;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                //headerCabel.Code = Convert.ToInt32(reader["Code"]);
                Comment = reader["Comment"].ToString();
                //headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                Name = reader["Name"].ToString();
                //headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                Type = Convert.ToByte(reader["Type"]);
                Material = Convert.ToByte(reader["Material"]);
                //headerCabel.Vol = Convert.ToDouble(reader["Vol"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", headerCabel.code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            reader.Close();

            connection.Close();
        }


        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_HeaderCabel_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
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
        //        ed.WriteMessage(string.Format(" ERROR EHeaderCabel.Delete: {0} \n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }
        //}

        public static EHeaderCabel SelectByProductCode(int ProductCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EHeaderCabel headerCabel = new EHeaderCabel();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    headerCabel.Code = Convert.ToInt32(reader["Code"]);
                    headerCabel.Comment = reader["Comment"].ToString();
                    headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                    headerCabel.Name = reader["Name"].ToString();
                    headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    headerCabel.Material = Convert.ToByte(reader["Material"]);
                    headerCabel.Type = Convert.ToByte(reader["Type"]);
                    headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
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
                ed.WriteMessage(string.Format("Error EHeaderCabel.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return headerCabel;
        }

        //public static EHeaderCabel SelectByProductCode(int ProductCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByProductCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    EHeaderCabel headerCabel = new EHeaderCabel();
        //    try{
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        headerCabel.Code = Convert.ToInt16(reader["Code"].ToString());
        //        headerCabel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        headerCabel.Material = Convert.ToByte(reader["Material"].ToString());
        //        headerCabel.Type = Convert.ToByte(reader["Type"].ToString());
        //        headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
        //        headerCabel.Comment = reader["Comment"].ToString();
        //        headerCabel.Name = reader["Name"].ToString();
        //    }
        //    else
        //    {
        //        Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

        //    }
        //    reader.Close();
        //    connection.Close();
        //}
        //catch (System.Exception ex1)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    ed.WriteMessage(string.Format("Error EHeaderCable.SelectByProductCode : {0} \n", ex1.Message));
        //    connection.Close();
        //}
        //    return headerCabel;
        //}

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_HeaderCabel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_HeaderCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        //ASHKTORAB
        public static EHeaderCabel ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            reader.Close();

            //connection.Close();
            return headerCabel;
        }

        //MEDHAT //ShareOnServer
        public static EHeaderCabel ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();

            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                headerCabel.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : header cable\n");
            }
            reader.Close();
            return headerCabel;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part ~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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

                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        // _EOperation.ProductCode = 0;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);
                        //_EOperation.ProductID = 
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

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);
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
                    ed.WriteMessage(string.Format("Error Occured During EHeader Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During EHeaderCabel Insertion 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
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

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = 0;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);
                        //_EOperation.ProductID = 
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
                    ed.WriteMessage(string.Format("Error Occured During EHeader Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During EHeaderCabel Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_HeaderCabel_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();


                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in HeaderCabel");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.LocalInsertX : {0} \n", ex.Message));
                return false;
            }

        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_HeaderCabel_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new SqlParameter("iType", Type));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                //Code = insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();
                //ed.WriteMessage("CODE={0}\n",Code);
                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in HeaderCabel");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in HeaderCabel");
                    }
                }
                ed.WriteMessage("EHeaderCable.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.LocalUpdate : {0} \n", ex.Message));
                return false;
            }

        }

    

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR EHeaderCabel.Update: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlTransaction _transaction;
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_HeaderCabel_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    //command.Parameters.Add(new SqlParameter("iMaterial", Material));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
        //    command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));

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
        //            if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
        //            {

        //                while (canCommitTransaction && Counter < operationList.Count)
        //                {

        //                    Atend.Base.Equipment.EOperation _EOperation;
        //                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
        //                    _EOperation.ProductCode = code;
        //                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);

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
        //            ed.WriteMessage(string.Format("Error Header.Update : {0} \n", ex1.Message));
        //            _transaction.Rollback();
        //            connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (Exception ex2)
        //    {
        //        ed.WriteMessage(string.Format("Error Header.Update : {0} \n", ex2.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));

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


                    //Counter = 0;
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel));
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
                    ed.WriteMessage(string.Format("Error Header.UpdateX : {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Header.UpdateX : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _Transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction = _Transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterial", Material));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iVoltage", Voltage));

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

                    return true;

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Header.UpdateX : {0} \n", ex1.Message));
                    //_transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Header.UpdateX : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EHeaderCabel.DeleteX : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHeaderCabel.DeleteX : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_DeleteX", connection);
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


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel)))
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

                    //transaction.Commit();
                    //connection.Close();
                    return true;

                }
                catch (System.Exception ex1)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EHeaderCabel.DeleteX : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EHeaderCabel.DeleteX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //public static EHeaderCabel SelectByCode(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_HeaderCabel_Select", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    EHeaderCabel headerCabel = new EHeaderCabel();
        //    if (reader.Read())
        //    {
        //        headerCabel.Code = Convert.ToInt16(reader["Code"].ToString());
        //        headerCabel.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        headerCabel.Material = Convert.ToByte(reader["Material"].ToString());
        //        headerCabel.Type = Convert.ToByte(reader["Type"].ToString());
        //        headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
        //        headerCabel.Comment = reader["Comment"].ToString();
        //        headerCabel.Name = reader["Name"].ToString();
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return headerCabel;
        //}

        public static EHeaderCabel SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            //ed.WriteMessage("Finish SELECT\n");
            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
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
            command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
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
            return headerCabel;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EHeaderCabel SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                headerCabel.Code = -1;
            }
            reader.Close();
            connection.Close();
            return headerCabel;
        }

        //ASHKTORAB //ShareOnServer
        public static EHeaderCabel SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EHeaderCabel headerCabel = new EHeaderCabel();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    headerCabel.Code = Convert.ToInt32(reader["Code"]);
                    headerCabel.Comment = reader["Comment"].ToString();
                    headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                    headerCabel.Name = reader["Name"].ToString();
                    headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    headerCabel.Material = Convert.ToByte(reader["Material"]);
                    headerCabel.Type = Convert.ToByte(reader["Type"]);
                    headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                    headerCabel.XCode = new Guid(reader["XCode"].ToString());
                    headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                }
                else
                {
                    headerCabel.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error Header cable In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return headerCabel;
        }

        public static EHeaderCabel SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                headerCabel.Code = -1;
            }

            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
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
            command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            //connection.Close();
            return headerCabel;
        }

        //ASHKTORAB
        public static EHeaderCabel SelectByCodeForLocal(int Code,SqlTransaction _transaction, SqlConnection _connection )
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                headerCabel.Code = -1;
            }

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", headerCabel.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            reader.Close();

            //connection.Close();
            return headerCabel;
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_HeaderCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        //BindDataToTreeViewX //method:SelectAllAndMerge
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_HeaderCabel_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_SearchByName", connection);
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
        public static EHeaderCabel CheckForExist(byte _Material, byte _Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_HeaderCabel_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iMaterial", _Material));
            command.Parameters.Add(new SqlParameter("iType", _Type));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
            }
            else
            {
                headerCabel.Code = -1;
            }
            reader.Close();
            connection.Close();
            return headerCabel;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }

        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("CODE={0}\n",Code);
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.AccessInsert : {0} \n", ex.Message));
                return false;
            }

        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _Oldtransaction, OleDbConnection _Oldconnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_HeaderCabel_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iproductCode", productCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterial", Material));
            insertCommand.Parameters.Add(new OleDbParameter("iVoltage", Voltage));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            int OldCode = Code;
            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                Code = insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                //ed.WriteMessage("CODE={0}\n",Code);
                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType (OldCode, (int)Atend.Control.Enum.ProductType.HeaderCabel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_Oldconnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.HeaderCabel, Code, _Oldtransaction, _Oldconnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess HeaderCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EHeaderCabel.AccessInsert : {0} \n", ex.Message));
                return false;
            }

        }

        //ASHKTORAB //MOUSAVI->draw conductor //StatusReport
        public static EHeaderCabel AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);

            }
            else
            {
                headerCabel.Code = -1;
            }

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new OleDbParameter("iProductCode", headerCabel.code));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.HeaderCabel));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            reader.Close();

            connection.Close();
            return headerCabel;
        }

        public static EHeaderCabel AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);

            }
            else
            {
                headerCabel.Code = -1;
            }

            reader.Close();
            return headerCabel;
        }

        public static EHeaderCabel AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_HeaderCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);

            }
            else
            {
                headerCabel.Code = -1;
            }

          
            reader.Close();

            //connection.Close();
            return headerCabel;
        }

        public static EHeaderCabel AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_HeaderCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EHeaderCabel headerCabel = new EHeaderCabel();
            if (reader.Read())
            {
                headerCabel.Code = Convert.ToInt32(reader["Code"]);
                headerCabel.Comment = reader["Comment"].ToString();
                headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                headerCabel.Name = reader["Name"].ToString();
                headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                headerCabel.Material = Convert.ToByte(reader["Material"]);
                headerCabel.Type = Convert.ToByte(reader["Type"]);
                headerCabel.XCode = new Guid(reader["XCode"].ToString());
                headerCabel.Voltage = Convert.ToDouble(reader["Vol"]);

            }
            else
            {
                headerCabel.Code = -1;
            }

            reader.Close();
            connection.Close();
            return headerCabel;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EHeaderCabel AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_HeaderCabel_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EHeaderCabel headerCabel = new EHeaderCabel();
        //    if (reader.Read())
        //    {
        //        headerCabel.Code = Convert.ToInt32(reader["Code"]);
        //        headerCabel.Comment = reader["Comment"].ToString();
        //        //headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
        //        headerCabel.Name = reader["Name"].ToString();
        //        headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
        //        headerCabel.Material = Convert.ToByte(reader["Material"]);
        //        headerCabel.Type = Convert.ToByte(reader["Type"]);
        //        headerCabel.XCode = new Guid(reader["XCode"].ToString());
        //        //headerCabel.Vol = Convert.ToDouble(reader["Vol"]);

        //    }
        //    else
        //    {
        //        headerCabel.Code = -1;
        //    }
        //    reader.Close();
        //    return headerCabel;
        //}

        public static EHeaderCabel AccessSelectByProductCode(int ProductCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_HeaderCabel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EHeaderCabel headerCabel = new EHeaderCabel();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    headerCabel.Code = Convert.ToInt32(reader["Code"]);
                    headerCabel.Comment = reader["Comment"].ToString();
                    headerCabel.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"]);
                    headerCabel.Name = reader["Name"].ToString();
                    headerCabel.Material = Convert.ToByte(reader["Material"]);
                    headerCabel.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                    headerCabel.Type = Convert.ToByte(reader["Type"]);
                    headerCabel.Voltage = Convert.ToDouble(reader["Voltage"]);
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
                ed.WriteMessage(string.Format("Error EHeaderCabel.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return headerCabel;
        }

        //method:SelectAllAndMerge
        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_HeaderCabel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_HeaderCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsHeaderCabel = new DataSet();
            adapter.Fill(dsHeaderCabel);
            return dsHeaderCabel.Tables[0];
        }

        //frmDrawKablsho
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
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
                    //ed.WriteMessage("Dc.ColumnName:{0} \n", Dc.ColumnName);
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

        //        EHeaderCabel _EHeaderCabel = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel));
        //            //ed.WriteMessage("\n111\n");

        //            EHeaderCabel Ap = Atend.Base.Equipment.EHeaderCabel.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;

        //                if (!Atend.Base.Equipment.EHeaderCabel.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EHeaderCabel.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EHeaderCabel.XCode);
        //            _EHeaderCabel.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EHeaderCabel.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EHeaderCabel.Code, (int)Atend.Control.Enum.ProductType.HeaderCabel))
        //                {
        //                    if (!_EHeaderCabel.UpdateX(Localtransaction, Localconnection))
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
        //                        //ed.WriteMessage("\n113\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EHeaderCabel.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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


        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;

        //    Guid DeletedXCode = Guid.NewGuid();

        //    try
        //    {

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        try
        //        {

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel));
        //            //ed.WriteMessage("\n111\n");

        //            EHeaderCabel Ap = Atend.Base.Equipment.EHeaderCabel.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EHeaderCabel.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EHeaderCabel.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.HeaderCabel));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.HeaderCabel, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EHeaderCabel.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}



    }
}
