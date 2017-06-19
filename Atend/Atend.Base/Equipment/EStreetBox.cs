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
    public class EStreetBox
    {
        public EStreetBox()
        {
            subEquipment = new ArrayList();
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

        private int inputCount;
        public int InputCount
        {
            get { return inputCount; }
            set { inputCount = value; }
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

        private int outputCount;
        public int OutputCount
        {
            get { return outputCount; }
            set { outputCount = value; }
        }

        private ArrayList subEquipment;

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

        public ArrayList SubEquipment
        {
            get { return subEquipment; }
            set { subEquipment = value; }
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

        public static ArrayList nodeCountEPackageX = new ArrayList();
        public static ArrayList nodeKeysEPackageX = new ArrayList();
        public static ArrayList nodeTypeEPackageX = new ArrayList();
        public static ArrayList nodeKeys = new ArrayList();
        public static ArrayList nodeKeysX = new ArrayList();

        //~~~~~~~~~~~~~~~~~~~~~~~~Server PArt~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {

            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));

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
                ed.WriteMessage(string.Format(" ERROR EStreetBox.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }


        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {

            SqlTransaction Transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
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
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
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
                //connection.Close();
                if (canCommitTransaction)
                    return true;
                else
                    return false;

            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR EStreetBox.Insert: {0} \n", ex1.Message));

                //connection.Close();
                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            SqlConnection con = _connection;
            //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
            SqlCommand insertCommand = new SqlCommand("E_StreetBox_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCode:{0}\nProductCode:{1}\nInputCount:{2}\nOutputCound:{3}\nComment:{4}\nName:{5}\n",
            //    XCode, ProductCode, InputCount, OutputCount, Comment, Name);


            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iInputPhuse", InputPhuse));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.StreetBox, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in StreetBox");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.StreetBox, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer StreetBox failed");
                    }
                }



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            SqlConnection con = _connection;
            //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
            SqlCommand insertCommand = new SqlCommand("E_StreetBox_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCode:{0}\nProductCode:{1}\nInputCount:{2}\nOutputCound:{3}\nComment:{4}\nName:{5}\n",
            //    XCode, ProductCode, InputCount, OutputCount, Comment, Name);


            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iInputPhuse", InputPhuse));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.StreetBox))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.StreetBox, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in StreetBox");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in StreetBox");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.StreetBox, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer StreetBox failed");
                    }
                }



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        public bool Update_XX()
        {
            Editor edd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            edd.WriteMessage("error\n");
            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));

            Boolean canCommiTransaction = true;
            try
            {

                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    command.ExecuteNonQuery();
                    int counter = 0;
                    if (EStreetBoxPhuse.Delete(Transaction, connection, Code))
                    {
                        while (canCommiTransaction && counter < subEquipment.Count)
                        {
                            EStreetBoxPhuse streetBoxPhuse = ((EStreetBoxPhuse)subEquipment[counter]);
                            streetBoxPhuse.StreetBoxXCode = XCode;
                            streetBoxPhuse.Comment = "";
                            if (streetBoxPhuse.Insert(Transaction, connection) && canCommiTransaction)
                            {
                                canCommiTransaction = true;
                            }
                            else
                            {
                                canCommiTransaction = false;
                            }
                            counter++;
                        }
                    }


                    //Operation
                    counter = 0;
                    if (EOperation.Delete(Transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {

                        while (canCommiTransaction && counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                            _EOperation.ProductCode = code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                            if (_EOperation.Insert(Transaction, connection) && canCommiTransaction)
                            {
                                canCommiTransaction = true;
                            }
                            else
                            {
                                canCommiTransaction = false;
                            }
                            counter++;
                        }
                    }
                    //****
                    if (canCommiTransaction)
                    {
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex2)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EStreetBox.Update : {0} \n", ex2.Message));
                    Transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EStreetBox.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            bool canCommitTransaction = true;
            try
            {
                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EStreetBoxPhuse.Delete(Transaction, connection, Code))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        ed.WriteMessage("Error EStreetBoxPhuse.Delete");
                        canCommitTransaction = false;
                    }

                    //operation
                    if (EOperation.Delete(Transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****

                    if (canCommitTransaction)
                    {
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In ExecutedNonQuery {0} \n", ex1.Message));
                    Transaction.Rollback();
                    connection.Close();

                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error :{0} \n", ex1.Message);
                connection.Close();
                return false;
            }

        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction Transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            bool canCommitTransaction = true;
            try
            {
                //connection.Open();
                //Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EStreetBoxPhuse.Delete(Transaction, connection, Code))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        ed.WriteMessage("Error EStreetBoxPhuse.Delete");
                        canCommitTransaction = false;
                    }

                    //operation
                    if (EOperation.Delete(Transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****

                    if (canCommitTransaction)
                    {
                        //Transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //Transaction.Rollback();
                        //connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In ExecutedNonQuery {0} \n", ex1.Message));
                    //Transaction.Rollback();
                    //connection.Close();

                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error :{0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

        }


        public static EStreetBox SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            reader.Close();
            connection.Close();


            //command.CommandText = "E_StreetBoxPhuse_Select";
            //command.Parameters.Clear();
            //command.Parameters.Add(new SqlParameter("iStreetBoxCode", streetBox.Code));

            connection.Open();
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    EStreetBoxPhuse streetBoxPhuse = new EStreetBoxPhuse();
            //    streetBoxPhuse.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"]);
            //    streetBoxPhuse.FeederNum = Convert.ToInt32(reader["FeederNum"]);
            //    streetBoxPhuse.PhuseCode = Convert.ToInt32(reader["PhuseCode"]);
            //    streetBoxPhuse.Comment = Convert.ToString(reader["Comment"]);
            //    streetBox.subEquipment.Add(streetBoxPhuse);
            //}
            //reader.Close();

            //OPERATION
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new SqlParameter("iProductCode", streetBox.Code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.StreetBox));
            ed.WriteMessage("Typ= " + Atend.Control.Enum.ProductType.StreetBox.ToString() + streetBox.Code.ToString() + "\n");
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("ooo \n");
                ed.WriteMessage("ProductID= " + reader["ProductID"].ToString() + "\n");
                nodeKeys.Add(reader["ProductID"].ToString());


            }

            reader.Close();
            //************
            connection.Close();


            return streetBox;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                //streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                //treetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                //streetBox.= Convert.ToInt32(reader["ShemshCount"].ToString());
                OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                //streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            reader.Close();
            connection.Close();




            //return streetBox;
        }

        //ASHKTORAB
        public static EStreetBox ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            reader.Close();
            //connection.Close();


            //command.CommandText = "E_StreetBoxPhuse_Select";
            //command.Parameters.Clear();
            //command.Parameters.Add(new SqlParameter("iStreetBoxCode", streetBox.Code));

            //connection.Open();
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    EStreetBoxPhuse streetBoxPhuse = new EStreetBoxPhuse();
            //    streetBoxPhuse.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"]);
            //    streetBoxPhuse.FeederNum = Convert.ToInt32(reader["FeederNum"]);
            //    streetBoxPhuse.PhuseCode = Convert.ToInt32(reader["PhuseCode"]);
            //    streetBoxPhuse.Comment = Convert.ToString(reader["Comment"]);
            //    streetBox.subEquipment.Add(streetBoxPhuse);
            //}
            //reader.Close();

            //OPERATION
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", streetBox.Code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.StreetBox));
            //ed.WriteMessage("Typ= " + Atend.Control.Enum.ProductType.StreetBox.ToString() + streetBox.Code.ToString() + "\n");
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");
            //    ed.WriteMessage("ProductID= " + reader["ProductID"].ToString() + "\n");
            //    nodeKeys.Add(reader["ProductID"].ToString());


            //}

            //reader.Close();
            ////************
            //connection.Close();


            return streetBox;
        }

        //MEDHAT //ShareOnServer
        public static EStreetBox ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();

            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                streetBox.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : streetbox\n");
            }
            reader.Close();
            return streetBox;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBox_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable SelectByInputOutputCode(int code, int input, int output)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBox_SelectByInputOutput", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", code));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iInputCount", input));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iOutputCount", output));

            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBox_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static EStreetBox SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EStreetBox streetBox = new EStreetBox();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                //EStreetBox streetBox = new EStreetBox();
                if (reader.Read())
                {
                    streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                    streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                    streetBox.Comment = reader["Comment"].ToString();
                    streetBox.Name = reader["Name"].ToString();
                    streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());

                }
                else
                {
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EStreetBox.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return streetBox;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    ed.WriteMessage("StreeBox Command Executed \n");
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    while (canCommitTransaction && Counter < subEquipment.Count)
                    {
                        EStreetBoxPhuse streetBoxPhuse = ((EStreetBoxPhuse)subEquipment[Counter]);
                        streetBoxPhuse.StreetBoxXCode = XCode;
                        streetBoxPhuse.Comment = "";
                        if (streetBoxPhuse.InsertX(Transaction, connection) && canCommitTransaction)
                        {
                            canCommitTransaction = true;
                        }
                        else
                        {
                            canCommitTransaction = false;
                        }
                        Counter++;
                    }


                    //operationList
                    Counter = 0;
                    ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(Transaction, connection) && canCommitTransaction)
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
                    //*************
                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                        if (containerPackage.InsertX(Transaction, connection))
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
                        if (_EProductPackage.InsertX(Transaction, connection) && canCommitTransaction)
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
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex2)
                {
                    ed.WriteMessage(string.Format("Error Insert StreetBox 01 : {0}", ex2.Message));
                    Transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Insert StreetBox 02 : {0}", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction Transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                //connection.Open();
                //Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());
                    ed.WriteMessage("StreeBox Command Executed \n");
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    //while (canCommitTransaction && Counter < subEquipment.Count)
                    //{
                    //    EStreetBoxPhuse streetBoxPhuse = ((EStreetBoxPhuse)subEquipment[Counter]);
                    //    streetBoxPhuse.StreetBoxXCode = XCode;
                    //    streetBoxPhuse.Comment = "";
                    //    if (streetBoxPhuse.InsertX(Transaction, connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;
                    //    }
                    //    Counter++;
                    //}


                    //operationList
                    Counter = 0;
                    ed.WriteMessage("count of subequipOperation " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                        //_EOperation.ProductID = 
                        if (_EOperation.InsertX(Transaction, connection) && canCommitTransaction)
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
                    //*************
                    if (canCommitTransaction)
                    {
                        //Transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //Transaction.Rollback();
                        //connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex2)
                {
                    ed.WriteMessage(string.Format("Error Insert StreetBox 01 : {0}", ex2.Message));
                    //Transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Insert StreetBox 02 : {0}", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_StreetBox_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iInputPhuse", InputPhuse));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.StreetBox, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in StreetBox");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.StreetBox, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer StreetBox failed");
                    }
                }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EStreetBox.LocalInsertX : {0} \n", ex.Message));
                return false;
            }

            return true;
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            SqlConnection con = _connection;
            //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
            SqlCommand insertCommand = new SqlCommand("E_StreetBox_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCode:{0}\nProductCode:{1}\nInputCount:{2}\nOutputCound:{3}\nComment:{4}\nName:{5}\n",
            //    XCode, ProductCode, InputCount, OutputCount, Comment, Name);


            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iInputPhuse", InputPhuse));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.StreetBox))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.StreetBox, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in StreetBox");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in StreetBox");
                    }
                }
                ed.WriteMessage("EStreetbox.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.StreetBox, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer StreetBox failed");
                    }
                }



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.StreetBox);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;
                try
                {
                    command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EStreetBoxPhuse.DeleteX(Transaction, connection, XCode))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        ed.WriteMessage("Error EStreetBoxPhuse.Delete");
                        canCommitTransaction = false;
                    }

                    //operation
                    if (EOperation.DeleteX(Transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****
                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox));
                    if ((EContainerPackage.DeleteX(Transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox))) & canCommitTransaction)
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    if (EProductPackage.Delete(Transaction, connection, containerPackage.Code) & canCommitTransaction)
                    {
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return false;
                    }

                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In ExecutedNonQuery {0} \n", ex1.Message));
                    Transaction.Rollback();
                    connection.Close();

                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error :{0} \n", ex1.Message);
                connection.Close();
                return false;
            }

        }


        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction Transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            bool canCommitTransaction = true;
            try
            {
                //connection.Open();
                //Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;
                try
                {
                    command.ExecuteNonQuery();
                    //if (EStreetBoxPhuse.DeleteX(Transaction, connection, XCode))
                    //{
                    //    canCommitTransaction = true;
                    //}
                    //else
                    //{
                    //    ed.WriteMessage("Error EStreetBoxPhuse.Delete");
                    //    canCommitTransaction = false;
                    //}

                    //operation
                    if (EOperation.DeleteX(Transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }
                    //****

                    if (canCommitTransaction)
                    {
                        //Transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //Transaction.Rollback();
                        //connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error In ExecutedNonQuery {0} \n", ex1.Message));
                    //Transaction.Rollback();
                    //connection.Close();

                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("Error :{0} \n", ex1.Message);
                //connection.Close();
                return false;
            }

        }

        public bool UpdateX()
        {
            Editor edd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            edd.WriteMessage("error\n");
            SqlTransaction Transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));

            Boolean canCommiTransaction = true;
            try
            {

                connection.Open();
                Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    command.ExecuteNonQuery();
                    int counter = 0;
                    if (EStreetBoxPhuse.DeleteX(Transaction, connection, XCode))
                    {
                        edd.WriteMessage("\nSubEquipCount = " + subEquipment.Count.ToString() + "\n");
                        while (canCommiTransaction && counter < subEquipment.Count)
                        {
                            EStreetBoxPhuse streetBoxPhuse = ((EStreetBoxPhuse)subEquipment[counter]);
                            streetBoxPhuse.StreetBoxXCode = XCode;
                            streetBoxPhuse.Comment = "";
                            if (streetBoxPhuse.InsertX(Transaction, connection) && canCommiTransaction)
                            {
                                canCommiTransaction = true;
                            }
                            else
                            {
                                canCommiTransaction = false;
                            }
                            counter++;
                        }
                    }


                    //Operation
                    counter = 0;
                    if (EOperation.DeleteX(Transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    {

                        while (canCommiTransaction && counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                            if (_EOperation.InsertX(Transaction, connection) && canCommiTransaction)
                            {
                                canCommiTransaction = true;
                            }
                            else
                            {
                                canCommiTransaction = false;
                            }
                            counter++;
                        }
                    }
                    //****
                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox));
                    counter = 0;
                    if (EProductPackage.Delete(Transaction, connection, containerPackage.Code))
                    {
                        while (canCommiTransaction && counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[counter]);
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(Transaction, connection) && canCommiTransaction)
                            {
                                canCommiTransaction = true;
                            }
                            else
                            {
                                canCommiTransaction = false;

                            }
                            counter++;
                        }
                    }
                    else
                    {
                        canCommiTransaction = false;
                    }
                    //*************
                    if (canCommiTransaction)
                    {
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex2)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EStreetBox.Update : {0} \n", ex2.Message));
                    Transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EStreetBox.Update : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor edd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            edd.WriteMessage("error\n");
            SqlTransaction Transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iInputCount", InputCount));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //command.Parameters.Add(new SqlParameter("iInputPhuse", InputPhuse));
            //command.Parameters.Add(new SqlParameter("iShemshCount", ShemshCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iOutputCount", OutputCount));

            Boolean canCommiTransaction = true;
            try
            {

                //connection.Open();
                //Transaction = connection.BeginTransaction();
                command.Transaction = Transaction;

                try
                {
                    command.ExecuteNonQuery();
                    int counter = 0;
                    //if (EStreetBoxPhuse.DeleteX(Transaction, connection, XCode))
                    //{
                    //    edd.WriteMessage("\nSubEquipCount = " + subEquipment.Count.ToString() + "\n");
                    //    while (canCommiTransaction && counter < subEquipment.Count)
                    //    {
                    //        EStreetBoxPhuse streetBoxPhuse = ((EStreetBoxPhuse)subEquipment[counter]);
                    //        streetBoxPhuse.StreetBoxXCode = XCode;
                    //        streetBoxPhuse.Comment = "";
                    //        if (streetBoxPhuse.InsertX(Transaction, connection) && canCommiTransaction)
                    //        {
                    //            canCommiTransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canCommiTransaction = false;
                    //        }
                    //        counter++;
                    //    }
                    //}


                    ////Operation
                    //counter = 0;
                    //if (EOperation.DeleteX(Transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox)))
                    //{

                    //    while (canCommiTransaction && counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox);
                    //        if (_EOperation.InsertX(Transaction, connection) && canCommiTransaction)
                    //        {
                    //            canCommiTransaction = true;
                    //        }
                    //        else
                    //        {
                    //            canCommiTransaction = false;
                    //        }
                    //        counter++;
                    //    }
                    //}
                    ////****
                    if (canCommiTransaction)
                    {
                        //Transaction.Commit();
                        //connection.Close();
                        return true;
                    }
                    else
                    {
                        //Transaction.Rollback();
                        //connection.Close();
                        return false;
                    }
                }
                catch (System.Exception ex2)
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("Error EStreetBox.Update : {0} \n", ex2.Message));
                    //Transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EStreetBox.Update : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }


        public static EStreetBox SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.XCode = new Guid(reader["XCode"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", streetBox.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.StreetBox));
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
            connection.Close();


            //command.CommandText = "E_StreetBoxPhuse_Select";
            //command.Parameters.Clear();
            //command.Parameters.Add(new SqlParameter("iStreetBoxCode", streetBox.Code));

            connection.Open();
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    EStreetBoxPhuse streetBoxPhuse = new EStreetBoxPhuse();
            //    streetBoxPhuse.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"]);
            //    streetBoxPhuse.FeederNum = Convert.ToInt32(reader["FeederNum"]);
            //    streetBoxPhuse.PhuseCode = Convert.ToInt32(reader["PhuseCode"]);
            //    streetBoxPhuse.Comment = Convert.ToString(reader["Comment"]);
            //    streetBox.subEquipment.Add(streetBoxPhuse);
            //}
            //reader.Close();

            //OPERATION
            command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", streetBox.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.StreetBox));
            //ed.WriteMessage("Typ= " + Atend.Control.Enum.ProductType.StreetBox.ToString() + streetBox.Code.ToString() + "\n");
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("ooo \n");
                ed.WriteMessage("ProductID= " + reader["ProductID"].ToString() + "\n");

                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());
                nodeKeysX.Add(Op);


            }

            reader.Close();
            //************
            connection.Close();


            return streetBox;
        }

        //ASHKTORAB
        public static EStreetBox SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.XCode = new Guid(reader["XCode"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            reader.Close();
            //connection.Close();


            ////command.CommandText = "E_StreetBoxPhuse_Select";
            ////command.Parameters.Clear();
            ////command.Parameters.Add(new SqlParameter("iStreetBoxCode", streetBox.Code));

            //connection.Open();
            ////reader = command.ExecuteReader();
            ////while (reader.Read())
            ////{
            ////    EStreetBoxPhuse streetBoxPhuse = new EStreetBoxPhuse();
            ////    streetBoxPhuse.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"]);
            ////    streetBoxPhuse.FeederNum = Convert.ToInt32(reader["FeederNum"]);
            ////    streetBoxPhuse.PhuseCode = Convert.ToInt32(reader["PhuseCode"]);
            ////    streetBoxPhuse.Comment = Convert.ToString(reader["Comment"]);
            ////    streetBox.subEquipment.Add(streetBoxPhuse);
            ////}
            ////reader.Close();

            ////OPERATION
            //command.Parameters.Clear();
            ////command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", streetBox.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.StreetBox));
            ////ed.WriteMessage("Typ= " + Atend.Control.Enum.ProductType.StreetBox.ToString() + streetBox.Code.ToString() + "\n");
            //reader = command.ExecuteReader();
            //nodeKeysX.Clear();
            //while (reader.Read())
            //{
            //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //    ed.WriteMessage("ooo \n");
            //    ed.WriteMessage("ProductID= " + reader["ProductID"].ToString() + "\n");
            //    nodeKeysX.Add(reader["ProductID"].ToString());


            //}

            //reader.Close();
            ////************
            //connection.Close();


            return streetBox;
        }

        //ASHKTORAB
        public static EStreetBox SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            command.Transaction = _transaction;
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.XCode = new Guid(reader["XCode"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                streetBox.Code = -1;
            }

            reader.Close();


            return streetBox;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EStreetBox SelectByXCodeForDesign(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                streetBox.XCode = new Guid(reader["XCode"].ToString());
                streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                streetBox.Code = -1;
            }
            reader.Close();
            connection.Close();
            return streetBox;
        }

        //ShareOnServer
        public static EStreetBox SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction trans = LocalTransaction;
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EStreetBox streetBox = new EStreetBox();

            try
            {
                command.Transaction = trans;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                    streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                    streetBox.Comment = reader["Comment"].ToString();
                    streetBox.Name = reader["Name"].ToString();
                    //streetBox.ShemshCount = Convert.ToInt32(reader["ShemshCount"].ToString());
                    streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
                    streetBox.XCode = new Guid(reader["XCode"].ToString());
                    streetBox.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                }
                else
                {
                    streetBox.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error EStreetBox.In SelectByXCodeForDesign.TransAction:{0}\n", ex.Message);
            }

            return streetBox;
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_StreetBox_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_StreetBox_SearchByName", connection);
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~Access PArt~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            foreach (DataRow dr in dtStreetBox.Rows)
            {
                //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
                OleDbCommand insertCommand = new OleDbCommand("E_StreetBox_Insert", con);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.Add(new OleDbParameter("iCode", Convert.ToInt32(dr["Code"])));
                insertCommand.Parameters.Add(new OleDbParameter("iProductCode", Convert.ToInt32(dr["ProductCode"])));
                insertCommand.Parameters.Add(new OleDbParameter("iInputCount", Convert.ToInt32(dr["InputCount"])));
                insertCommand.Parameters.Add(new OleDbParameter("iComment", dr["Comment"].ToString()));
                insertCommand.Parameters.Add(new OleDbParameter("iName", dr["Name"].ToString()));
                insertCommand.Parameters.Add(new OleDbParameter("iOutputCount", dr["OutputCount"].ToString()));

                try
                {
                    //if (con.State == ConnectionState.Closed)
                    //{
                    //    con.Open();
                    //}

                    con.Open();
                    insertCommand.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                    con.Close();
                    return false;
                }

            }
            return true;
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //DataTable dtStreetBox = Atend.Base.Equipment.EStreetBox.SelectAll();
            OleDbConnection con = _connection;
            //ed.WriteMessage("Name= " + dr["Name"].ToString() + "\n");
            OleDbCommand insertCommand = new OleDbCommand("E_StreetBox_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("XCode:{0}\nProductCode:{1}\nInputCount:{2}\nOutputCound:{3}\nComment:{4}\nName:{5}\n",
            //    XCode, ProductCode, InputCount, OutputCount, Comment, Name);


            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new OleDbParameter("iOutputCount", OutputCount));
            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            //insertCommand.Parameters.Add(new OleDbParameter("iInputPhuse", InputPhuse));

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.StreetBox);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.StreetBox, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess StreetBox failed");
                    }
                }



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_StreetBox_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;


            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iInputCount", InputCount));
            insertCommand.Parameters.Add(new OleDbParameter("iOutputCount", OutputCount));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            int OldCode = Code;

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.StreetBox);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.StreetBox, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess StreetBox failed");
                    }
                }



            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Transfer StreetBox: " + ex.Message + "\n");
                return false;
            }

            return true;
        }

        //StatusReport
        public static EStreetBox AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
            }
            else
            {
                streetBox.Code = -1;
            }
            reader.Close();
            connection.Close();


            //command.CommandText = "E_StreetBoxPhuse_Select";
            //command.Parameters.Clear();
            //command.Parameters.Add(new OleDbParameter("iStreetBoxCode", streetBox.Code));

            //connection.Open();
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    EStreetBoxPhuse streetBoxPhuse = new EStreetBoxPhuse();
            //    streetBoxPhuse.StreetBoxCode = Convert.ToInt32(reader["StreetBoxCode"].ToString());
            //    streetBoxPhuse.FeederNum = Convert.ToInt32(reader["FeederNum"]);
            //    streetBoxPhuse.PhuseCode = Convert.ToInt32(reader["PhuseCode"]);
            //    streetBoxPhuse.Comment = Convert.ToString(reader["Comment"]);
            //    streetBox.subEquipment.Add(streetBoxPhuse);
            //}
            //reader.Close();

            //connection.Close();


            return streetBox;
        }

        public static EStreetBox AccessSelectByCode(int Code,OleDbConnection _connction)
        {
            OleDbConnection connection = _connction; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
            }
            else
            {
                streetBox.Code = -1;
            }
            reader.Close();
            return streetBox;
        }


        public static EStreetBox AccessSelectByCodeForConvertor(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_StreetBox_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
            }
            else
            {
                streetBox.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return streetBox;
        }

        public static EStreetBox AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_StreetBox_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EStreetBox streetBox = new EStreetBox();
            if (reader.Read())
            {
                streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                streetBox.Comment = reader["Comment"].ToString();
                streetBox.Name = reader["Name"].ToString();
                streetBox.XCode = new Guid(reader["XCode"].ToString());
                streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
            }
            else
            {
                streetBox.Code = -1;
            }
            reader.Close();
            connection.Close();
            return streetBox;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EStreetBox AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_StreetBox_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EStreetBox streetBox = new EStreetBox();
        //    if (reader.Read())
        //    {
        //        streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
        //        streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
        //        streetBox.Comment = reader["Comment"].ToString();
        //        streetBox.Name = reader["Name"].ToString();
        //        streetBox.XCode = new Guid(reader["XCode"].ToString());
        //        streetBox.OutputCount = Convert.ToInt32(reader["OutputCount"].ToString());
        //    }
        //    else
        //    {
        //        streetBox.Code = -1;
        //    }
        //    reader.Close();
        //    return streetBox;
        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_StreetBox_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_StreetBox_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsStreetBox = new DataSet();
            adapter.Fill(dsStreetBox);
            return dsStreetBox.Tables[0];
        }

        public static EStreetBox AccessSelectByProductCode(int ProductCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_StreetBox_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EStreetBox streetBox = new EStreetBox();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                //EStreetBox streetBox = new EStreetBox();
                if (reader.Read())
                {
                    streetBox.Code = Convert.ToInt16(reader["Code"].ToString());
                    streetBox.ProductCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    streetBox.InputCount = Convert.ToInt16(reader["InputCount"].ToString());
                    streetBox.Comment = reader["Comment"].ToString();
                    streetBox.Name = reader["Name"].ToString();
                }
                else
                {
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", ProductCode));

                }
                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EStreetBox.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return streetBox;
        }

        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
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

        //        EStreetBox _EStreetBox = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox));
        //            EStreetBox Ap = Atend.Base.Equipment.EStreetBox.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EStreetBox.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EStreetBox.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EStreetBox.XCode);
        //            _EStreetBox.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            DataTable STBPTbl = EStreetBoxPhuse.SelectByStreetBoxXCode(_EStreetBox.XCode, Localtransaction, Localconnection);

        //            if (_EStreetBox.Insert(Servertransaction, Serverconnection))
        //            {

        //                bool PhuseFlag = true;
        //                foreach (DataRow Row in STBPTbl.Rows)
        //                {
        //                    if (Atend.Base.Equipment.EPhuse.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, new Guid(Row["PhuseXCode"].ToString())))
        //                    {
        //                        EStreetBoxPhuse SPhuse = EStreetBoxPhuse.SelectByXCode(Localtransaction, Localconnection, new Guid(Row["XCode"].ToString()));
        //                        SPhuse.PhuseCode = EPhuse.ServerSelectByXCode(Servertransaction, Serverconnection, new Guid(Row["PhuseXCode"].ToString())).Code;
        //                        SPhuse.StreetBoxCode = _EStreetBox.Code;

        //                        if (SPhuse.Insert(Servertransaction, Serverconnection))
        //                        {
        //                            if (!SPhuse.LocalUpdateX(Localtransaction, Localconnection))
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

        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EStreetBox.Code, (int)Atend.Control.Enum.ProductType.StreetBox))
        //                {
        //                    if (!_EStreetBox.UpdateX(Localtransaction, Localconnection))
        //                    {
        //                        ed.WriteMessage("\n115\n");

        //                        Servertransaction.Rollback();
        //                        Serverconnection.Close();
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        ed.WriteMessage("\n112\n");

        //                        //Servertransaction.Commit();
        //                        //Serverconnection.Close();

        //                        //Localtransaction.Commit();
        //                        //Localconnection.Close();
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

        //                if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EStreetBox.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.StreetBox, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //                {
        //                    ed.WriteMessage("\n113\n");

        //                    Servertransaction.Commit();
        //                    Serverconnection.Close();

        //                    Localtransaction.Commit();
        //                    Localconnection.Close();
        //                }
        //                else
        //                {
        //                    ed.WriteMessage("\n114\n");

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


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction StreetBox:{0}\n", ex1.Message));
        //            Servertransaction.Rollback();
        //            Serverconnection.Close();

        //            Localtransaction.Rollback();
        //            Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR ERecloser.ShareOnServer {0}\n", ex1.Message));

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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox));
        //            EStreetBox Ap = Atend.Base.Equipment.EStreetBox.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EStreetBox.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EStreetBox.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.StreetBox));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            /////////////////////////////////////////////////////////////////////////////

        //            DataTable SBPTbl = Atend.Base.Equipment.EStreetBoxPhuse.SelectByStreetBoxCode(Ap.Code);
        //            foreach (DataRow SBPRow in SBPTbl.Rows)
        //            {
        //                DeletedXCode = Guid.NewGuid();

        //                EStreetBoxPhuse StreetBP = EStreetBoxPhuse.SelectByCode(Localtransaction, Localconnection, Convert.ToInt32(SBPRow["Code"].ToString()));
        //                if (StreetBP.XCode != Guid.Empty)
        //                {
        //                    ed.WriteMessage("\n222\n");
        //                    DeletedXCode = StreetBP.XCode;
        //                    if (!Atend.Base.Equipment.EStreetBoxPhuse.DeleteX(Localtransaction, Localconnection, StreetBP.StreetBoxXCode))
        //                    {
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                    }
        //                    ed.WriteMessage("\n333\n");

        //                    StreetBP.ServerSelectByCode(Convert.ToInt32(SBPRow["Code"].ToString()));
        //                    ed.WriteMessage("\n444\n");
        //                }
        //                else
        //                {
        //                    ed.WriteMessage("\n555\n");
        //                    StreetBP = Atend.Base.Equipment.EStreetBoxPhuse.SelectByCode(Convert.ToInt32(SBPRow["Code"].ToString()));
        //                    StreetBP.XCode = DeletedXCode;
        //                }


        //                StreetBP.StreetBoxXCode = Ap.XCode;

        //                ////////////////////////////////////
        //                ed.WriteMessage("\nPhuseCode = {0}\n", StreetBP.PhuseCode.ToString());

        //                EPhuse Phuse = Atend.Base.Equipment.EPhuse.SelectByCode(Localtransaction, Localconnection, StreetBP.PhuseCode);

        //                DeletedXCode = Guid.NewGuid();

        //                if (Phuse.XCode != Guid.Empty)
        //                {
        //                    ed.WriteMessage("\n222\n");
        //                    DeletedXCode = Phuse.XCode;
        //                    if (!Atend.Base.Equipment.EPhuse.DeleteX(Localtransaction, Localconnection, Phuse.XCode))
        //                    {
        //                        Localtransaction.Rollback();
        //                        Localconnection.Close();
        //                    }
        //                    ed.WriteMessage("\n333\n");

        //                    Phuse.ServerSelectByCode(StreetBP.PhuseCode);
        //                    ed.WriteMessage("\n444\n");
        //                }
        //                else
        //                {
        //                    ed.WriteMessage("\n555\n");
        //                    Phuse = Atend.Base.Equipment.EPhuse.SelectByCode(StreetBP.PhuseCode);
        //                    Phuse.XCode = DeletedXCode;
        //                }

        //                Phuse.OperationList = new ArrayList();
        //                OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(StreetBP.PhuseCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //                Phuse.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Phuse.XCode);
        //                ed.WriteMessage("\n666 PhuseCode = {0} , Comment = {1}\n" ,StreetBP.PhuseCode.ToString(), Phuse.Comment);

        //                if (!Phuse.InsertX(Localtransaction, Localconnection))
        //                {
        //                    ed.WriteMessage("\n114\n");
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;

        //                }



        //                ////////////////////////////////////

        //                StreetBP.PhuseXCode = Phuse.XCode;
        //                if (!StreetBP.InsertXX(Localtransaction, Localconnection))
        //                {
        //                    ed.WriteMessage("\n114\n");
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;

        //                }

        //            }


        //            //foreach (DataRow SBPRow in SBPTbl.Rows)
        //            //{
        //            //    DeletedXCode = Guid.NewGuid();
        //            //    EStreetBoxPhuse StreetBP = EStreetBoxPhuse.SelectByCode(Localtransaction, Localconnection, Convert.ToInt32(SBPRow["Code"].ToString()));
        //            //    if (StreetBP.XCode != Guid.Empty)
        //            //    {
        //            //        DeletedXCode = StreetBP.XCode;
        //            //        if (!Atend.Base.Equipment.EStreetBoxPhuse.DeleteX(Localtransaction, Localconnection, StreetBP.StreetBoxXCode))
        //            //        {
        //            //            Localtransaction.Rollback();
        //            //            Localconnection.Close();
        //            //        }

        //            //        StreetBP.ServerSelectByCode(Convert.ToInt32(SBPRow["Code"].ToString()));
        //            //    }
        //            //    else
        //            //    {
        //            //        StreetBP = Atend.Base.Equipment.EStreetBoxPhuse.SelectByCode(Convert.ToInt32(SBPRow["Code"].ToString()));
        //            //        StreetBP.XCode = DeletedXCode;
        //            //    }
        //            //    StreetBP.StreetBoxXCode = Ap.XCode;

        //            //    ////////////////////////////////////

        //            //    //*****************************************
        //            //    //-----------------------------------------
        //            //    //Guid DeletedXCodePhuse = Guid.NewGuid();
        //            //    //Atend.Base.Equipment.EContainerPackage containerPackagephuse = EContainerPackage.selectByContainerCodeAndType(StreetBP.PhuseCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            //    //ed.WriteMessage("++++++++++++ StreetBP.PhuseCode:{0}\n", StreetBP.PhuseCode);
        //            //    //EPhuse Apphuse = Atend.Base.Equipment.EPhuse.SelectByCode(Localtransaction, Localconnection, StreetBP.PhuseCode);
        //            //    //ed.WriteMessage("++++++++++++ Apphuse.XCode:{0}\n", Apphuse.XCode);
        //            //    //if (Apphuse.XCode != Guid.Empty)
        //            //    //{
        //            //    //    DeletedXCodePhuse = Apphuse.XCode;
        //            //    //    if (!Atend.Base.Equipment.EPhuse.DeleteX(Localtransaction, Localconnection, Apphuse.XCode))
        //            //    //    {
        //            //    //        Localtransaction.Rollback();
        //            //    //        Localconnection.Close();
        //            //    //    }
        //            //    //    Apphuse.ServerSelectByCode(StreetBP.PhuseCode);
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    Apphuse = Atend.Base.Equipment.EPhuse.SelectByCode(StreetBP.PhuseCode);
        //            //    //    Apphuse.XCode = DeletedXCodePhuse;
        //            //    //}
        //            //    //ed.WriteMessage("++++++++++++ Apphuse.PhusePoleCode:{0}\n", Apphuse.PhusePoleCode);
        //            //    //Atend.Base.Equipment.EPhusePole PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(Localtransaction, Localconnection, Apphuse.PhusePoleCode);
        //            //    //Guid PhusePoleDeleted = Guid.NewGuid();
        //            //    //ed.WriteMessage("++++++++++++ PHP.XCode:{0}\n", PHP.XCode);
        //            //    //if (PHP.XCode != Guid.Empty)
        //            //    //{
        //            //    //    PhusePoleDeleted = PHP.XCode;
        //            //    //    if (!Atend.Base.Equipment.EPhusePole.DeleteX(Localtransaction, Localconnection, PHP.XCode))
        //            //    //    {
        //            //    //        Localtransaction.Rollback();
        //            //    //        Localconnection.Close();
        //            //    //        return false;
        //            //    //    }
        //            //    //    PHP.ServerSelectByCode(Apphuse.PhusePoleCode);
        //            //    //}
        //            //    //else
        //            //    //{
        //            //    //    PHP = Atend.Base.Equipment.EPhusePole.SelectByCode(Apphuse.PhusePoleCode);
        //            //    //    PHP.XCode = PhusePoleDeleted;
        //            //    //    PhusePoleDeleted = Guid.Empty;
        //            //    //}

        //            //    //PHP.OperationList = new ArrayList();
        //            //    //DataTable OperationTblphuse = Atend.Base.Equipment.EOperation.SelectByProductCodeType(PHP.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            //    //PHP.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTblphuse, PHP.XCode);
        //            //    //Atend.Base.Equipment.EContainerPackage containerPackagePhusePole = EContainerPackage.selectByContainerCodeAndType(PHP.Code, Convert.ToInt32(Atend.Control.Enum.ProductType.PhusePole));
        //            //    //if (!PHP.InsertX(Localtransaction, Localconnection))
        //            //    //{
        //            //    //    Localtransaction.Rollback();
        //            //    //    Localconnection.Close();
        //            //    //    return false;
        //            //    //}
        //            //    //if (!Atend.Base.Design.NodeTransaction.SubProductsForServer(PHP.Code, PHP.XCode, containerPackagePhusePole.Code, (int)Atend.Control.Enum.ProductType.PhusePole, Localtransaction, Localconnection))
        //            //    //{
        //            //    //    Localtransaction.Rollback();
        //            //    //    Localconnection.Close();
        //            //    //    return false;
        //            //    //}
        //            //    //Apphuse.PhusePoleXCode = PHP.XCode;

        //            //    //Apphuse.OperationList = new ArrayList();
        //            //    //DataTable OperationTbl1 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(StreetBP.PhuseCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Phuse));
        //            //    //Apphuse.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl1, Apphuse.XCode);

        //            //    //if (!Apphuse.InsertX(Localtransaction, Localconnection))
        //            //    //{
        //            //    //    Localtransaction.Rollback();
        //            //    //    Localconnection.Close();
        //            //    //    return false;
        //            //    //}
        //            //    //if (!Atend.Base.Design.NodeTransaction.SubProductsForServer(Apphuse.Code, Apphuse.XCode, containerPackagephuse.Code, (int)Atend.Control.Enum.ProductType.Phuse, Localtransaction, Localconnection))
        //            //    //{
        //            //    //    Localtransaction.Rollback();
        //            //    //    Localconnection.Close();
        //            //    //    return false;
        //            //    //}
        //            //    //-----------------------------------------

        //            //    ////////////////////////////////////

        //            //    StreetBP.PhuseXCode = Apphuse.XCode;
        //            //    if (!StreetBP.InsertXX(Localtransaction, Localconnection))
        //            //    {
        //            //        ed.WriteMessage("\n114\n");
        //            //        Localtransaction.Rollback();
        //            //        Localconnection.Close();
        //            //        return false;

        //            //    }

        //            //}



        //            /////////////////////////////////////////////////////////////////////////////


        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //                if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.StreetBox, Localtransaction, Localconnection))
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


        //            //if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Conductor, Localtransaction, Localconnection))
        //            //{
        //            //    ed.WriteMessage("\n113\n");

        //            //    Localtransaction.Commit();
        //            //    Localconnection.Close();
        //            //}
        //            //else
        //            //{
        //            //    ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR EConductor.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


    }
}
