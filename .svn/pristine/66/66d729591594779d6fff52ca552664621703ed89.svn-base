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
    public class EPole
    {
        public EPole()
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

        private double height;
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        private double power;
        public double Power
        {
            get { return power; }
            set { power = value; }
        }

        private byte type;
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        private double topCrossSectionArea;
        public double TopCrossSectionArea
        {
            get { return topCrossSectionArea; }
            set { topCrossSectionArea = value; }
        }

        private double buttomCrossSectionArea;
        public double ButtomCrossSectionArea
        {
            get { return buttomCrossSectionArea; }
            set { buttomCrossSectionArea = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double wc;
        public double Wc
        {
            get { return wc; }
            set { wc = value; }
        }

        private byte shape;
        public byte Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        private ArrayList subEquipment;
        public ArrayList SubEquipment
        {
            get { return subEquipment; }
            set { subEquipment = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
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

        private ArrayList containerList;
        public ArrayList ContainerList
        {
            get { return containerList; }
            set { containerList = value; }
        }

        public static ArrayList nodeCountEPackage = new ArrayList();
        public static ArrayList nodeKeysEPackage = new ArrayList();
        public static ArrayList nodeIsContainerEPackage = new ArrayList();

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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));


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
                ed.WriteMessage(string.Format(" ERROR EPole.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("\nPoleXCode = {0}\nComment = {1}", XCode.ToString(), Comment);
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    try
                    {
                        Code = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (System.Exception ee)
                    {
                        //ed.WriteMessage("\nErRorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr : {0}\n",ee.Message);
                    }


                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.WriteMessage("\n1111111111111\n");

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);
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
        //HATAMi //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("\nXCode = {0}\n" , XCode);
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("INSERTPOLE FINISH\n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Pole, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _Connection))
                            throw new System.Exception("operation failed in Pole");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("*********sub\n");
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Pole, Code, _transaction, _Connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToSever Pole failed");
                    }
                    ed.WriteMessage("*********Endsub\n");
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_Pole_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("\nXCode = {0}\n" , XCode);
            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                //ed.WriteMessage("INSERTPOLE FINISH\n");

                if (BringOperation)
                {
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _Connection, Code, (int)Atend.Control.Enum.ProductType.Pole))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Pole, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _Connection))
                                throw new System.Exception("operation failed in Pole");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Pole");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Pole, Code, _transaction, _Connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToSever Pole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));

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
                    if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.ProductCode = Code;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);

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
                    //ed.WriteMessage("2 \n");
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
                    ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Delete", connection);
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


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
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
                    ed.WriteMessage(string.Format("Error EPole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPole.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static EPole SelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new SqlParameter("iProductCode", pole.code));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            return pole;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //EPole pole = new EPole();
            if (reader.Read())
            {
                //Code = Convert.ToInt16(reader["Code"].ToString());
                //productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                height = Convert.ToSingle(reader["Height"].ToString());
                Power = Convert.ToSingle(reader["Power"].ToString());

                Type = Convert.ToByte(reader["Type"].ToString());
                TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Shape = Convert.ToByte(reader["Shape"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            // for check boxes
            reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new SqlParameter("iProductCode", pole.code));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            //return pole;
        }

        public static EPole SelectByProductCode(int ProductCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EPole pole = new EPole();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pole.Code = Convert.ToInt16(reader["Code"].ToString());
                    pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    pole.height = Convert.ToSingle(reader["Height"].ToString());
                    pole.Power = Convert.ToSingle(reader["Power"].ToString());


                    pole.Type = Convert.ToByte(reader["Type"].ToString());
                    pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                    pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                    pole.Comment = reader["Comment"].ToString();
                    pole.Name = reader["Name"].ToString();
                    pole.Shape = Convert.ToByte(reader["Shape"]);
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
                ed.WriteMessage(string.Format("Error EPole.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return pole;
        }

        public static DataTable SelectAll()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable DrawSearch(double height, double power, int type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iHeight", height));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPower", power));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", type));
            DataSet dsPole = new DataSet();
            adapter.Fill(dsPole);
            return dsPole.Tables[0];
        }

        //ASHKTORAB
        public static EPole ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("\nXCode = " + XCode + "\n");
            //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //ed.WriteMessage("ooo111 \n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            //ed.WriteMessage("ooo112 \n");
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            // for check boxes
            reader.Close();
            //ed.WriteMessage("ooo113 \n");
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByXCodeType";
            //command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
            //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
            //reader = command.ExecuteReader();
            //nodeKeys.Clear();
            //while (reader.Read())
            //{
            //    ed.WriteMessage("ooo114 \n");

            //    nodeKeys.Add(reader["ProductID"].ToString());
            //    //nodeCount.Add(reader["Count"].ToString());

            //}

            //reader.Close();

            //connection.Close();
            return pole;
        }

        //MEDHAT //ShareOnServer
        public static EPole ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();

            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                pole.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : pole\n");
            }
            reader.Close();
            return pole;
        }

        //ASHKTORAB
        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Pole_Delete", connection);
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


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
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
                    ed.WriteMessage(string.Format("Error EPole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPole.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            //ed.WriteMessage("$$$$$$$$$$$$$$ TYPE:{0} \n", Type);
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
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
                    try
                    {
                        Code = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch
                    {
                        //ed.WriteMessage("\nErRorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr\n");
                    }


                    bool canCommitTransaction = true;
                    int Counter = 0;

                    ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);
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

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Pole_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //ed.WriteMessage("\nCode = " + Code + "\n");
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            //ed.WriteMessage("\nProductCode = " + ProductCode + "\n");
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            //ed.WriteMessage("\nHeight = " + Height + "\n");
            command.Parameters.Add(new SqlParameter("iPower", Power));
            //ed.WriteMessage("\nPower = " + Power + "\n");
            command.Parameters.Add(new SqlParameter("iType", Type));
            //ed.WriteMessage("\nType = " + Type + "\n");
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            //ed.WriteMessage("\nTopCrossSectionArea = " + TopCrossSectionArea + "\n");
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nButtomCrossSectionArea = " + ButtomCrossSectionArea + "\n");
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nComment = " + Comment + "\n");
            command.Parameters.Add(new SqlParameter("iName", Name));
            //ed.WriteMessage("\nName = " + Name + "\n");
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            //ed.WriteMessage("\nShape = " + Shape + "\n");
            //XCode = Guid.NewGuid();
            //ed.WriteMessage("\nXCode = " + XCode + "\n");
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("\nIsDefault = " + IsDefault + "\n");
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));


            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex3)
                    {
                        ed.WriteMessage("\nErRorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr\n{0}\n", ex3.Message);
                        return false;
                    }


                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.WriteMessage("\n1111111111111\n");

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);
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
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_Pole_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {

                ed.WriteMessage("LocalInsertX Succedd\n");
                command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Pole, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _Connection))
                            throw new System.Exception("operation failed in Pole");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Pole, XCode, ServerTransaction, ServerConnection, _transaction, _Connection))
                    {
                        throw new System.Exception("SentFromLocalToSever Pole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _Connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _Connection;
            SqlCommand command = new SqlCommand("E_Pole_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("@@@@@@@@ xcode:{0}\n", XCode);
            //ed.WriteMessage("\nXCode = {0}\n" , XCode);
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {


                //Code = command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                //ed.WriteMessage("INSERTPOLE FINISH,XCode={0}\n",XCode);

                if (BringOperation)
                {
                    ed.WriteMessage("DeleteOperation\n");
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _Connection, XCode, (int)Atend.Control.Enum.ProductType.Pole))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Pole, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            //ed.WriteMessage("In ForEach Code={0}\n",dr["Code"].ToString());
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            //ed.WriteMessage("!!!!!XCode={0}\n",XCode);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _Connection))
                                throw new System.Exception("operation failed in Pole");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Pole");
                    }
                }
                ed.WriteMessage("epole.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Pole, XCode, ServerTransaction, ServerConnection, _transaction, _Connection))
                    {
                        throw new System.Exception("SentFromLocalToSever Pole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        //MEDHAT
        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

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


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);

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
                    }

                    //Package
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(transaction, connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            //ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(transaction, connection) && canCommitTransaction)
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
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error Pole.UpdateX : {0} \n", ex1.Message));
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Pole.UpdateX : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Pole_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iHeight", Height));
            command.Parameters.Add(new SqlParameter("iPower", Power));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iShape", Shape));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    //Counter = 0;
                    //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Pole);

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
                    ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex1.Message));
                    //_transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Pole);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EPole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPole.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Pole_DeleteX", connection);
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


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
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
                    ed.WriteMessage(string.Format("Error EPole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPole.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteXX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;//new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Pole_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    //command.ExecuteNonQuery();


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole)))
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
                    ed.WriteMessage(string.Format("Error EPole.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EPole.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EPole SelectByXCodeForDesign(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.LocalcnString+"\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("\nSelectByXcode1111\n");

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("\nSelectByXcode2222\n");

            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                //ed.WriteMessage("\nSelectByXcode3333\n");

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());

                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                pole.code = -1;
            }
            reader.Close();
            connection.Close();
            return pole;
        }

        //ShareOnServer
        public static EPole SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.LocalcnString+"\n");
            SqlConnection connection = LocalConnection; //new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("\nSelectByXcode1111\n");
            EPole pole = new EPole();

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                //ed.WriteMessage("\nSelectByXcode2222\n");

                if (reader.Read())
                {
                    pole.Code = Convert.ToInt16(reader["Code"].ToString());
                    pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    pole.height = Convert.ToSingle(reader["Height"].ToString());
                    pole.Power = Convert.ToSingle(reader["Power"].ToString());
                    //ed.WriteMessage("\nSelectByXcode3333\n");

                    pole.Type = Convert.ToByte(reader["Type"].ToString());
                    pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                    pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                    pole.Comment = reader["Comment"].ToString();
                    pole.Name = reader["Name"].ToString();
                    pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                    pole.XCode = new Guid(reader["XCode"].ToString());

                    pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                }
                else
                {
                    pole.code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error EPole.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return pole;
        }

        //SendFromLocalToAccess //frmDrawPoleTip
        public static EPole SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //ed.WriteMessage(Atend.Control.ConnectionString.LocalcnString+"\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //ed.WriteMessage("\nSelectByXcode1111\n");

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("\nSelectByXcode2222\n");

            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                //ed.WriteMessage("\nSelectByXcode3333\n");

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());

                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("!!!!Pole.Type={0}\n", pole.Type);
            }
            else
            {
                pole.code = -1;
            }

            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
            reader = command.ExecuteReader();

            nodeKeys.Clear();
            while (reader.Read())
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeys.Add(Op);
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();
            connection.Close();
            return pole;
        }

        //ASHKTORAB
        public static EPole SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt32(reader["Code"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                pole.Code = -1;
            }

            // for check boxes
            reader.Close();
            return pole;
        }

        //ASHKTORAB
        //public static EPole SelectByXCode(SqlTransaction _transaction, SqlConnection _connection ,Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Transaction = _transaction;

        //    ed.WriteMessage("\nSelectByXcode1111\n");

        //    command.Parameters.Add(new SqlParameter("iXCode", XCode));
        //    //connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    ed.WriteMessage("\nSelectByXcode2222\n");

        //    EPole pole = new EPole();
        //    if (reader.Read())
        //    {
        //        pole.Code = Convert.ToInt16(reader["Code"].ToString());
        //        pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        pole.height = Convert.ToSingle(reader["Height"].ToString());
        //        pole.Power = Convert.ToSingle(reader["Power"].ToString());
        //        ed.WriteMessage("\nSelectByXcode3333\n");

        //        pole.Type = Convert.ToByte(reader["Type"].ToString());
        //        pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
        //        pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
        //        pole.Comment = reader["Comment"].ToString();
        //        pole.Name = reader["Name"].ToString();
        //        pole.Shape = Convert.ToByte(reader["Shape"].ToString());
        //        pole.XCode = new Guid(reader["XCode"].ToString());

        //        pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

        //        //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    }
        //    else
        //    {
        //        pole.code = -1;
        //    }

        //    // for check boxes
        //    reader.Close();
        //    //command.Parameters.Clear();
        //    //command.CommandText = "E_Operation_SelectByXCodeType";
        //    //command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
        //    //command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
        //    //reader = command.ExecuteReader();

        //    //nodeKeys.Clear();
        //    //while (reader.Read())
        //    //{
        //    //    //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    //    //ed.WriteMessage("ooo \n");

        //    //    nodeKeys.Add(reader["ProductID"].ToString());
        //    //    //nodeCount.Add(reader["Count"].ToString());

        //    //}

        //    reader.Close();
        //    //connection.Close();
        //    return pole;
        //}

        public static EPole SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }
            else
            {
                pole.code = -1;
            }

            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            return pole;
        }

        //public static EPole SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_Pole_SelectByXCode", _connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Transaction = _transaction;
        //    ed.WriteMessage("ooo111 \n");
        //    command.Parameters.Add(new SqlParameter("iXCode", XCode));
        //    //connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    EPole pole = new EPole();
        //    ed.WriteMessage("ooo112 \n");
        //    if (reader.Read())
        //    {
        //        pole.Code = Convert.ToInt16(reader["Code"].ToString());
        //        pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        pole.height = Convert.ToSingle(reader["Height"].ToString());
        //        pole.Power = Convert.ToSingle(reader["Power"].ToString());

        //        pole.Type = Convert.ToByte(reader["Type"].ToString());
        //        pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
        //        pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
        //        pole.Comment = reader["Comment"].ToString();
        //        pole.Name = reader["Name"].ToString();
        //        pole.Shape = Convert.ToByte(reader["Shape"].ToString());
        //        pole.XCode = new Guid(reader["XCode"].ToString());
        //        pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
        //        //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    }

        //    // for check boxes
        //    reader.Close();
        //    ed.WriteMessage("ooo113 \n");
        //    command.Parameters.Clear();
        //    command.CommandText = "E_Operation_SelectByXCodeType";
        //    command.Parameters.Add(new SqlParameter("iXCode", pole.XCode));
        //    command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Pole));
        //    reader = command.ExecuteReader();
        //    nodeKeys.Clear();
        //    while (reader.Read())
        //    {
        //        ed.WriteMessage("ooo114 \n");

        //        nodeKeys.Add(reader["ProductID"].ToString());
        //        //nodeCount.Add(reader["Count"].ToString());

        //    }

        //    reader.Close();

        //    //connection.Close();
        //    return pole;
        //}

        public static DataTable SelectAllX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable DrawSearchLocal(double height, double power, int type)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iHeight", height));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPower", power));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", type));
            DataSet dsPole = new DataSet();
            adapter.Fill(dsPole);
            return dsPole.Tables[0];
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable SelectByPowerX(double Power)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(Atend.Control.ConnectionString.cnString + "\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Pole_SelectByPower", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iPower", Power));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }





        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_SearchByName", connection);
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
        public static EPole CheckForExist(double _Height, double _Power, double _TopCrossSectionArea,
                                            double _BottomCrossSectionArea, byte _Type, byte _Shape)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Pole_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iHeight", _Height));
            command.Parameters.Add(new SqlParameter("iPower", _Power));
            command.Parameters.Add(new SqlParameter("iTopCrossSectionArea", _TopCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iBottomCrossSectionArea", _BottomCrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iShape", _Shape));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                pole.Code = -1;
            }
            reader.Close();
            connection.Close();
            return pole;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~ Access PART ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iShape", Shape));

            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.AccessInsert : {0} \n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _Connection, bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //ed.WriteMessage("\nXCode = {0}\n" , XCode);
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iShape", Shape));

            try
            {


                Code = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("INSERTPOLE FINISH\n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Pole);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _Connection))
                            throw new System.Exception("operation failed in Pole");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Pole, Code, _transaction, _Connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Pole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Pole_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            //ed.WriteMessage("\nXCode = {0}\n" , XCode);
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            //ed.WriteMessage("\nComment = {0}\n", Comment);
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            //ed.WriteMessage("\nName = {0}\n", Name);
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iShape", Shape));
            int OldCode = Code;
            try
            {
                Code = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                //ed.WriteMessage("INSERTPOLE FINISH\n");

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Pole);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()), _OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in Pole");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Pole, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Pole failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EPole.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //MOUSAVI
        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Pole_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iHeight", Height));
            command.Parameters.Add(new OleDbParameter("iPower", Power));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iTopCrossSectionArea", TopCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iBottomCrossSectionArea", ButtomCrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iShape", Shape));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Pole.AccessUpdate : {0} \n", ex2.Message));
                return false;
            }
            return true;
        }

        //MOUSAVI->AutoPoleInstallation //frmDrawPoleTip // frmEditDrawPole
        public static EPole AccessSelectByCode(int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();

            }
            else
                pole.Code = -1;

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new OleDbParameter("iProductCode", pole.code));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            return pole;
        }

       
        public static EPole AccessSelectByCodeForConvertor(int Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();

            }
            else
                pole.Code = -1;



            reader.Close();

            //connection.Close();
            return pole;
        }

        //CAlculation HATAMI //status report
        public static EPole AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_Pole_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());
                pole.Shape = Convert.ToByte(reader["Shape"].ToString());
                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();

            }
            else
                pole.Code = -1;

            // for check boxes
            //reader.Close();
            //command.Parameters.Clear();
            //command.CommandText = "E_Operation_SelectByProductCodeType";
            //command.Parameters.Add(new OleDbParameter("iProductCode", pole.code));
            //command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.Pole));
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
            return pole;
        }



        //MOUSAVI
        public static EPole AccessSelectByXCode(Guid XCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Pole_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EPole pole = new EPole();
            if (reader.Read())
            {
                pole.XCode = new Guid(reader["XCode"].ToString());
                pole.Code = Convert.ToInt16(reader["Code"].ToString());
                pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                pole.height = Convert.ToSingle(reader["Height"].ToString());
                pole.Power = Convert.ToSingle(reader["Power"].ToString());

                pole.Type = Convert.ToByte(reader["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                pole.Comment = reader["Comment"].ToString();
                pole.Name = reader["Name"].ToString();
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            }

            else
            {
                pole.Code = -1;
            }

            // for check boxes
            reader.Close();
            connection.Close();
            return pole;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EPole AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Pole_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EPole pole = new EPole();
        //    if (reader.Read())
        //    {
        //        pole.XCode = new Guid(reader["XCode"].ToString());
        //        pole.Code = Convert.ToInt16(reader["Code"].ToString());
        //        pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
        //        pole.height = Convert.ToSingle(reader["Height"].ToString());
        //        pole.Power = Convert.ToSingle(reader["Power"].ToString());

        //        pole.Type = Convert.ToByte(reader["Type"].ToString());
        //        pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
        //        pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
        //        pole.Comment = reader["Comment"].ToString();
        //        pole.Name = reader["Name"].ToString();

        //    }

        //    else
        //    {
        //        pole.Code = -1;
        //    }
        //    reader.Close();
        //    return pole;
        //}

        public static EPole AccessSelectByProductCode(int ProductCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Pole_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EPole pole = new EPole();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pole.Code = Convert.ToInt16(reader["Code"].ToString());
                    pole.productCode = Convert.ToInt16(reader["ProductCode"].ToString());
                    pole.height = Convert.ToSingle(reader["Height"].ToString());
                    pole.Power = Convert.ToSingle(reader["Power"].ToString());


                    pole.Type = Convert.ToByte(reader["Type"].ToString());
                    pole.TopCrossSectionArea = Convert.ToSingle(reader["TopCrossSectionArea"].ToString());
                    pole.ButtomCrossSectionArea = Convert.ToSingle(reader["BottomCrossSectionArea"].ToString());
                    pole.Comment = reader["Comment"].ToString();
                    pole.Name = reader["Name"].ToString();
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
                ed.WriteMessage(string.Format("Error EPole.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return pole;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Pole_Select", connection);
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
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Pole_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Pole_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsPola = new DataSet();
            adapter.Fill(dsPola);
            return dsPola.Tables[0];
        }

        public static DataTable AccessDrawSearch(double height, double power, int type)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Pole_DrawSearch", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iHeight", height));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iPower", power));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", type));
            DataSet dsPole = new DataSet();
            adapter.Fill(dsPole);
            return dsPole.Tables[0];
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
            MergeTbl.DefaultView.Sort = "XCode,Power";
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

        //        EPole _EPole = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //            //ed.WriteMessage("\n111\n");

        //            EPole Ap = Atend.Base.Equipment.EPole.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EPole.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EPole.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPole.XCode);
        //            _EPole.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EPole.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPole.Code, (int)Atend.Control.Enum.ProductType.Pole))
        //                {
        //                    if (Atend.Base.Equipment.EPoleTip.UpdateXX(Servertransaction, Serverconnection, DeletedCode, _EPole.Code))
        //                    {

        //                        if (!_EPole.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");

        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            //Servertransaction.Commit();
        //                            //Serverconnection.Close();

        //                            //Localtransaction.Commit();
        //                            //Localconnection.Close();
        //                            //return true;

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

        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPole.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Pole, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPole.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool ShareOnServer(SqlTransaction _Servertransaction, SqlConnection _Serverconnection, SqlTransaction _Localtransaction, SqlConnection _Localconnection, Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = _Serverconnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction = _Servertransaction;

        //    SqlConnection Localconnection = _Localconnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction = _Localtransaction;

        //    int DeletedCode = 0;

        //    try
        //    {
        //        //Serverconnection.Open();
        //        //Servertransaction = Serverconnection.BeginTransaction();

        //        //Localconnection.Open();
        //        //Localtransaction = Localconnection.BeginTransaction();

        //        EPole _EPole = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //            //ed.WriteMessage("\n111\n");

        //            EPole Ap = Atend.Base.Equipment.EPole.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EPole.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    return false;
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EPole.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EPole.XCode);
        //            _EPole.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EPole.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EPole.Code, (int)Atend.Control.Enum.ProductType.Pole))
        //                {
        //                    if (Atend.Base.Equipment.EPoleTip.UpdateXX(Servertransaction, Serverconnection, DeletedCode, _EPole.Code))
        //                    {

        //                        if (!_EPole.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");

        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            //return true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //            }
        //            else
        //            {
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EPole.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Pole, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                return true;
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                return false;
        //            }


        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EPole.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));
        //            //ed.WriteMessage("\n111\n");

        //            EPole Ap = Atend.Base.Equipment.EPole.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EPole.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EPole.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Pole));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Pole, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EPole.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


        //**********************************Access To Memory for calculation

        public static EPole AccessSelectByCode(DataTable dtPole, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DataRow[] drPole = dtPole.Select(string.Format("Code={0}", Code));
            EPole pole = new EPole();
            if (drPole.Length > 0)
            {
                pole.Code = Convert.ToInt16(drPole[0]["Code"].ToString());
                pole.productCode = Convert.ToInt16(drPole[0]["ProductCode"].ToString());
                pole.height = Convert.ToSingle(drPole[0]["Height"].ToString());
                pole.Power = Convert.ToSingle(drPole[0]["Power"].ToString());
                pole.Shape = Convert.ToByte(drPole[0]["Shape"].ToString());
                pole.Type = Convert.ToByte(drPole[0]["Type"].ToString());
                pole.TopCrossSectionArea = Convert.ToSingle(drPole[0]["TopCrossSectionArea"].ToString());
                pole.ButtomCrossSectionArea = Convert.ToSingle(drPole[0]["BottomCrossSectionArea"].ToString());
                pole.Comment = drPole[0]["Comment"].ToString();
                pole.Name = drPole[0]["Name"].ToString();

            }
            else
            {
                pole.Code = -1;
            }

            return pole;
        }

    }
}
