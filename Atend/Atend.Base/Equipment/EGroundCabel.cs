using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;

using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Equipment
{
    public class EGroundCabel
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

        private int numString;
        public int NumString
        {
            get { return numString; }
            set { numString = value; }
        }

        private double crossSectionArea;
        public double CrossSectionArea
        {
            get { return crossSectionArea; }
            set { crossSectionArea = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private double vol;
        public double Vol
        {
            get { return vol; }
            set { vol = value; }
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

        private double resistance;
        public double Resistance
        {
            get { return resistance; }
            set { resistance = value; }
        }

        private double reactance;
        public double Reactance
        {
            get { return reactance; }
            set { reactance = value; }
        }

        private double maxcurrent;
        public double MaxCurrent
        {
            get { return maxcurrent; }
            set { maxcurrent = value; }
        }

        private double capacitance;
        public double Capacitance
        {
            get { return capacitance; }
            set { capacitance = value; }
        }

        private ArrayList operationList;
        public ArrayList OperationList
        {
            get { return operationList; }
            set { operationList = value; }
        }

        private int containerCode;
        public int ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; }
        }

        private int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private double maxCurrent1Second;

        public double MaxCurrent1Second
        {
            get { return maxCurrent1Second; }
            set { maxCurrent1Second = value; }
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


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));


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
                ed.WriteMessage(string.Format(" ERROR EMiddleGroundCable.Insert: {0} \n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrenr1Second", MaxCurrent1Second));
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

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        _EOperation.ProductCode = Code;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);
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
                    ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //ed.WriteMessage("Xcode:{0}\n", XCode);
            //ed.WriteMessage("ProductCode:{0}\n", ProductCode);
            //ed.WriteMessage("NumString:{0}\n", NumString);
            //ed.WriteMessage("CrossSectionArea:{0}\n", CrossSectionArea);
            ////ed.WriteMessage("Type:{0}\n", Type);
            ////ed.WriteMessage("Vol:{0}\n", Vol);
            //ed.WriteMessage("Comment:{0}\n", Comment);
            //ed.WriteMessage("Name:{0}\n", Name);
            //ed.WriteMessage("Resistance:{0}\n", Resistance);
            //ed.WriteMessage("MaxCurrent:{0}\n", MaxCurrent);
            //ed.WriteMessage("iCapacitance:{0}\n", Capacitance);
            //ed.WriteMessage("iSize:{0}\n", Size);

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in gound cable");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_GroundCabel_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //ed.WriteMessage("Xcode:{0}\n", XCode);
            //ed.WriteMessage("ProductCode:{0}\n", ProductCode);
            //ed.WriteMessage("NumString:{0}\n", NumString);
            //ed.WriteMessage("CrossSectionArea:{0}\n", CrossSectionArea);
            ////ed.WriteMessage("Type:{0}\n", Type);
            ////ed.WriteMessage("Vol:{0}\n", Vol);
            //ed.WriteMessage("Comment:{0}\n", Comment);
            //ed.WriteMessage("Name:{0}\n", Name);
            //ed.WriteMessage("Resistance:{0}\n", Resistance);
            //ed.WriteMessage("MaxCurrent:{0}\n", MaxCurrent);
            //ed.WriteMessage("iCapacitance:{0}\n", Capacitance);
            //ed.WriteMessage("iSize:{0}\n", Size);

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.GroundCabel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in gound cable");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in GroundCabel");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        //public bool Update()
        //{
        //    SqlTransaction _transaction;
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("E_GroundCabel_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iNumString", NumString));
        //    command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
        //    command.Parameters.Add(new SqlParameter("iType", Type));
        //    command.Parameters.Add(new SqlParameter("iVol", Vol));
        //    command.Parameters.Add(new SqlParameter("iComment", Comment));
        //    command.Parameters.Add(new SqlParameter("iName", Name));


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
        //            if (EOperation.Delete(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
        //            {

        //                while (canCommitTransaction && Counter < operationList.Count)
        //                {

        //                    Atend.Base.Equipment.EOperation _EOperation;
        //                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
        //                    _EOperation.ProductCode = code;
        //                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);

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
        //            ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex1.Message));
        //            _transaction.Rollback();
        //            connection.Close();
        //            return false;
        //        }
        //    }
        //    catch (Exception ex2)
        //    {
        //        ed.WriteMessage(string.Format("Error Pole.Update : {0} \n", ex2.Message));
        //        connection.Close();
        //        return false;
        //    }
        //}

        public static bool Delete(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Delete", connection);
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


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
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
                    ed.WriteMessage(string.Format("Error EMiddeGroundCabel.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMiddeGroundCabel.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();


                    if (EOperation.Delete(transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
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
                    ed.WriteMessage(string.Format("Error EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        public static EGroundCabel SelectByProductCode(int ProductCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            EGroundCabel GroundCabel = new EGroundCabel();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                    GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                    GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                    GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                    GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    GroundCabel.Comment = reader["Comment"].ToString();
                    GroundCabel.Name = reader["Name"].ToString();
                    GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                    GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
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
            return GroundCabel;
        }

        //ASHKTORAB
        public static EGroundCabel SelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            EGroundCabel GroundCabel = new EGroundCabel();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                    GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                    GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                    GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());
                    GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                    GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                    GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    GroundCabel.Comment = reader["Comment"].ToString();
                    GroundCabel.Name = reader["Name"].ToString();
                    GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                    GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

                }
                else
                {
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format("No Record found for productCode : {0} \n", Code));

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
            return GroundCabel;
        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //EGroundCabel GroundCabel = new EGroundCabel();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {


                    CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                    NumString = Convert.ToInt32(reader["NumString"].ToString());

                    Type = Convert.ToInt32(reader["Type"].ToString());
                    Vol = Convert.ToSingle(reader["Vol"].ToString());
                    Comment = reader["Comment"].ToString();
                    Name = reader["Name"].ToString();
                    Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    Size = Convert.ToInt32(reader["Size"].ToString());
                    MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
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
            //return GroundCabel;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        public static DataTable DrawSearch(/*float crossSectionArea, int TypeCode , int Volt*/)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", TypeCode));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iVoltage", Volt));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //ed.WriteMessage("bbb\n");
            return ds.Tables[0];
        }

        public static EGroundCabel ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            }

            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", GroundCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            //connection.Close();
            return GroundCabel;
        }

        //MEDHAT //ShareOnServer
        public static EGroundCabel ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed =Autodesk.AutoCAD.ApplicationServices. Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();

            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                GroundCabel.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : ground cable\n");
            }
            reader.Close();
            return GroundCabel;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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

                    //ed.WriteMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                        //_EOperation.ProductCode = 0;//containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);
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
                    ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 02 : {0} \n", ex2.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));

            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));

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
                        //_EOperation.ProductCode = 0;//containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);
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
                    ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 01 : {0} \n", ex1.Message));
                    //transaction.Rollback();

                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Occured During GroundCabel Insertion 02 : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_GroundCabel_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundCabel, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in gound cable");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundCabel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.localInsertX : {0} \n", ex.Message));
                return false;
            }
        }

        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_GroundCabel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //ed.WriteMessage("Xcode:{0}\n", XCode);
            //ed.WriteMessage("ProductCode:{0}\n", ProductCode);
            //ed.WriteMessage("NumString:{0}\n", NumString);
            //ed.WriteMessage("CrossSectionArea:{0}\n", CrossSectionArea);
            ////ed.WriteMessage("Type:{0}\n", Type);
            ////ed.WriteMessage("Vol:{0}\n", Vol);
            //ed.WriteMessage("Comment:{0}\n", Comment);
            //ed.WriteMessage("Name:{0}\n", Name);
            //ed.WriteMessage("Resistance:{0}\n", Resistance);
            //ed.WriteMessage("MaxCurrent:{0}\n", MaxCurrent);
            //ed.WriteMessage("iCapacitance:{0}\n", Capacitance);
            //ed.WriteMessage("iSize:{0}\n", Size);

           command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.GroundCabel))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundCabel, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in gound cable");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in GroundCabel");
                    }
                }
                ed.WriteMessage("EGroundCable.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundCabel, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));

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
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
                    {

                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            //_EOperation.ProductCode = 0;
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));
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
                    ed.WriteMessage(string.Format("Error Pole.UpdateX : {0} \n", ex1.Message));
                    _transaction.Rollback();
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

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iNumString", NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iVol", Vol));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iSize", Size));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));


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
                    //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
                    //{

                    //    while (canCommitTransaction && Counter < operationList.Count)
                    //    {

                    //        Atend.Base.Equipment.EOperation _EOperation;
                    //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    //        //_EOperation.ProductCode = 0;
                    //        _EOperation.XCode = XCode;
                    //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel);

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
                    ed.WriteMessage(string.Format("Error Pole.UpdateX : {0} \n", ex1.Message));
                    //_transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error Pole.UpdateX : {0} \n", ex2.Message));
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlTransaction transaction = _transaction;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_DeleteX", connection);
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


                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel)))
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
                    ed.WriteMessage(string.Format("Error EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EMiddeGroundCabel.DeleteX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //در فرم ترسیم کابل زمینی
        public static EGroundCabel SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                GroundCabel.Code = -1;
            }
            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", GroundCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
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
            command.Parameters.Add(new SqlParameter("iXCode", GroundCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                //ed.WriteMessage("ooo \n");
                EOperation Op = new EOperation();
                Op.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                Op.Count = Convert.ToDouble(reader["Count"].ToString());

                nodeKeysX.Add(Op);
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            connection.Close();
            return GroundCabel;
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EGroundCabel SelectByXCodeForDeign(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            }
            else
            {
                GroundCabel.Code = -1;
            }
            // for check boxes
            reader.Close();
            connection.Close();
            return GroundCabel;
        }

        //ASHKTORAB //ShareOnServer
        public static EGroundCabel SelectByXCodeForDeign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EGroundCabel GroundCabel = new EGroundCabel();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                    GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                    GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                    GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                    GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    GroundCabel.Comment = reader["Comment"].ToString();
                    GroundCabel.Name = reader["Name"].ToString();
                    GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                    GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                    GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                }
                else
                {
                    GroundCabel.Code = -1;
                }
                // for check boxes
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error EGroundCable.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return GroundCabel;
        }

        //ASHKTORAB
        public static EGroundCabel SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            }

            // for check boxes
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", GroundCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
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
            command.Parameters.Add(new SqlParameter("iXCode", GroundCabel.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
            reader = command.ExecuteReader();
            nodeKeysX.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("ooo \n");

                nodeKeysX.Add(reader["ProductID"].ToString());
                //nodeCount.Add(reader["Count"].ToString());

            }

            reader.Close();

            //connection.Close();
            return GroundCabel;
        }

        //ASHKTORAB
        public static EGroundCabel SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            //connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            }
            else
            {
                GroundCabel.Code = -1;
            }

            // for check boxes
            reader.Close();

            //connection.Close();
            return GroundCabel;
        }

        //SelectAllAndMerge
        public static DataTable LocalDrawSearch(/*float crossSectionArea, int TypeCode , int Volt*/)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", TypeCode));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iVoltage", Volt));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //ed.WriteMessage("bbb\n");
            return ds.Tables[0];
        }


        //Hatami //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabel_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_SearchByName", connection);
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


        //frmHeaderCabel
        public static DataTable SelectAllCrossSection()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabel_SelectAllCrossSection", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];

        }


        //MEDHAT
        public static EGroundCabel CheckForExist(int _NumString, double _CrossSectionArea, int _Type,
                                                 double _Vol, int _Size, double _Resistance, double _Reactance,
                                                 double _MaxCurrent, double _Capacitance)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabel_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iNumString", _NumString));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", _CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iType", _Type));
            command.Parameters.Add(new SqlParameter("iVol", _Vol));
            command.Parameters.Add(new SqlParameter("iSize", _Size));
            command.Parameters.Add(new SqlParameter("iResistance", _Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", _Reactance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", _MaxCurrent));
            command.Parameters.Add(new SqlParameter("iCapacitance", _Capacitance));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());
                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                GroundCabel.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabel;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor; OleDbTransaction transaction;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iNumString", NumString));
            command.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new OleDbParameter("iSize", Size));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.AccessInsert : {0} \n", ex.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //ed.WriteMessage("Xcode:{0}\n", XCode);
            //ed.WriteMessage("ProductCode:{0}\n", ProductCode);
            //ed.WriteMessage("NumString:{0}\n", NumString);
            //ed.WriteMessage("CrossSectionArea:{0}\n", CrossSectionArea);
            ////ed.WriteMessage("Type:{0}\n", Type);
            ////ed.WriteMessage("Vol:{0}\n", Vol);
            //ed.WriteMessage("Comment:{0}\n", Comment);
            //ed.WriteMessage("Name:{0}\n", Name);
            //ed.WriteMessage("Resistance:{0}\n", Resistance);
            //ed.WriteMessage("MaxCurrent:{0}\n", MaxCurrent);
            //ed.WriteMessage("iCapacitance:{0}\n", Capacitance);
            //ed.WriteMessage("iSize:{0}\n", Size);

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iNumString", NumString));
            command.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new OleDbParameter("iSize", Size));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_transaction, _connection))
                            throw new System.Exception("operation failed in gound cable");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.GroundCabel, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }



        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;


            //ed.WriteMessage("Xcode:{0}\n", XCode);
            //ed.WriteMessage("ProductCode:{0}\n", ProductCode);
            //ed.WriteMessage("NumString:{0}\n", NumString);
            //ed.WriteMessage("CrossSectionArea:{0}\n", CrossSectionArea);
            ////ed.WriteMessage("Type:{0}\n", Type);
            ////ed.WriteMessage("Vol:{0}\n", Vol);
            //ed.WriteMessage("Comment:{0}\n", Comment);
            //ed.WriteMessage("Name:{0}\n", Name);
            //ed.WriteMessage("Resistance:{0}\n", Resistance);
            //ed.WriteMessage("MaxCurrent:{0}\n", MaxCurrent);
            //ed.WriteMessage("iCapacitance:{0}\n", Capacitance);
            //ed.WriteMessage("iSize:{0}\n", Size);

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iNumString", NumString));
            command.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iVol", Vol));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new OleDbParameter("iSize", Size));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
            int oldcode = Code;
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldcode, (int)Atend.Control.Enum.ProductType.GroundCabel);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction, _NewConnection))
                            throw new System.Exception("operation failed in gound cable");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldcode, (int)Atend.Control.Enum.ProductType.GroundCabel, Code, _OldTransaction , _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess GroundCable failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EMiddleGroundCAble.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }
        //StatusReport // frmDrawBranchCable
        public static EGroundCabel AccessSelectByCode(int Code)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

            }
            else
            {
                GroundCabel.Code = -1;
            }
            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", GroundCabel.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
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
            return GroundCabel;
        }

        public static EGroundCabel AccessSelectByCode(int Code,OleDbConnection _connection)
        {

            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

            }
            else
            {
                GroundCabel.Code = -1;
            }
            // for check boxes
            reader.Close();
            command.Parameters.Clear();
            command.CommandText = "E_Operation_SelectByProductCodeType";
            command.Parameters.Add(new OleDbParameter("iProductCode", GroundCabel.Code));
            command.Parameters.Add(new OleDbParameter("iType", Atend.Control.Enum.ProductType.GroundCabel));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                nodeKeys.Add(reader["ProductID"].ToString());
            }
            reader.Close();
            return GroundCabel;
        }

        public static EGroundCabel AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundCabel_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

            }
            else
            {
                GroundCabel.Code = -1;
            }
          
            reader.Close();

            //connection.Close();
            return GroundCabel;
        }

        public static EGroundCabel AccessSelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabel_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabel GroundCabel = new EGroundCabel();
            if (reader.Read())
            {
                GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                GroundCabel.Comment = reader["Comment"].ToString();
                GroundCabel.Name = reader["Name"].ToString();
                GroundCabel.XCode = new Guid(reader["XCode"].ToString());
                GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }

            // for check boxes
            reader.Close();
            connection.Close();
            return GroundCabel;
        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EGroundCabel AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_GroundCabel_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EGroundCabel GroundCabel = new EGroundCabel();
        //    if (reader.Read())
        //    {
        //        GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
        //        GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
        //        GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

        //        GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
        //        GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
        //        GroundCabel.Comment = reader["Comment"].ToString();
        //        GroundCabel.Name = reader["Name"].ToString();
        //        GroundCabel.XCode = new Guid(reader["XCode"].ToString());
        //        GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
        //        GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
        //        GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
        //        GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
        //        GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
        //    }
        //    else
        //    {
        //        GroundCabel.Code = -1;
        //    }

        //    // for check boxes
        //    reader.Close();
        //    return GroundCabel;
        //}

        public static EGroundCabel AccessSelectByProductCode(int ProductCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabel_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            EGroundCabel GroundCabel = new EGroundCabel();
            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GroundCabel.Code = Convert.ToInt16(reader["Code"].ToString());
                    GroundCabel.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    GroundCabel.CrossSectionArea = Convert.ToSingle(reader["CrossSectionArea"].ToString());
                    GroundCabel.NumString = Convert.ToInt32(reader["NumString"].ToString());

                    GroundCabel.Type = Convert.ToInt32(reader["Type"].ToString());
                    GroundCabel.Vol = Convert.ToSingle(reader["Vol"].ToString());
                    GroundCabel.Comment = reader["Comment"].ToString();
                    GroundCabel.Name = reader["Name"].ToString();
                    GroundCabel.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    GroundCabel.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    GroundCabel.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    GroundCabel.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    GroundCabel.Size = Convert.ToInt32(reader["Size"].ToString());
                    GroundCabel.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
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
                ed.WriteMessage(string.Format("Error EPole.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }
            return GroundCabel;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundCabel_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        public static DataTable AccessSearch(string Name)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundCabel_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            return dsGroundCabel.Tables[0];
        }

        //SelectAllAndMerge
        public static DataTable AccessDrawSearch()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessDrawSearch\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundCabelTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            //ed.WriteMessage("Finish AccessDrawSearch\n");
            return dsGroundCabel.Tables[0];
        }

        //frmDrawGroundCabletip
        public static DataTable SelectAllAndMerge()
        {

            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = LocalDrawSearch();

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
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //foreach (DataRow dr in MergeTbl.Rows)
            //{
            //    ed.WriteMessage("Name={0}\n",dr["Name"].ToString());
            //}

            return MergeTbl;

        }

        //public static bool ShareOnServer(SqlTransaction _Servertransaction, SqlConnection _Serverconnection, SqlTransaction _Localtransaction, SqlConnection _Localconnection, Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = _Serverconnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction = _Servertransaction;

        //    SqlConnection Localconnection = _Localconnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction = _Localtransaction;

        //    int DeletedCode = 0;
        //    EGroundCabel _EGroundCabel = SelectByXCode(XCode);

        //    try
        //    {
        //        //Serverconnection.Open();
        //        //Servertransaction = Serverconnection.BeginTransaction();

        //        //Localconnection.Open();
        //        //Localtransaction = Localconnection.BeginTransaction();

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));
        //            //ed.WriteMessage("\n111\n");

        //            EGroundCabel Ap = Atend.Base.Equipment.EGroundCabel.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EGroundCabel.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    //Servertransaction.Rollback();
        //                    //Serverconnection.Close();

        //                    //Localtransaction.Rollback();
        //                    //Localconnection.Close();
        //                    return false;
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EGroundCabel.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EGroundCabel.XCode);
        //            _EGroundCabel.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EGroundCabel.Insert(Servertransaction, Serverconnection))
        //            {
        //                //ed.WriteMessage("\naaaa1111\n");
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EGroundCabel.Code, (int)Atend.Control.Enum.ProductType.GroundCabel))
        //                {
        //                    //ed.WriteMessage("\naaaa2222\n");
        //                    if (Atend.Base.Equipment.EGroundCabelTip.UpdateXX(Servertransaction, Serverconnection, DeletedCode, _EGroundCabel.Code))
        //                    {
        //                        if (!_EGroundCabel.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            //ed.WriteMessage("\nbbbb3333\n");

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
        //                    //Servertransaction.Rollback();
        //                    //Serverconnection.Close();

        //                    //Localtransaction.Rollback();
        //                    //Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n112\n");

        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EGroundCabel.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.GroundCabel, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n113\n");

        //                //Servertransaction.Commit();
        //                //Serverconnection.Close();

        //                //Localtransaction.Commit();
        //                //Localconnection.Close();
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
        //            //Servertransaction.Rollback();
        //            //Serverconnection.Close();

        //            //Localtransaction.Rollback();
        //            //Localconnection.Close();
        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR EGroundCabel.ShareOnServer {0}\n", ex1.Message));

        //        //Serverconnection.Close();
        //        //Localconnection.Close();
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
        //    EGroundCabel _EGroundCabel = SelectByXCode(XCode);

        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));
        //            //ed.WriteMessage("\n111\n");

        //            EGroundCabel Ap = Atend.Base.Equipment.EGroundCabel.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EGroundCabel.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            _EGroundCabel.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _EGroundCabel.XCode);
        //            _EGroundCabel.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);


        //            if (_EGroundCabel.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _EGroundCabel.Code, (int)Atend.Control.Enum.ProductType.GroundCabel))
        //                {
        //                    if (Atend.Base.Equipment.EGroundCabelTip.UpdateXX(Servertransaction, Serverconnection, DeletedCode, _EGroundCabel.Code))
        //                    {
        //                        if (!_EGroundCabel.UpdateX(Localtransaction, Localconnection))
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
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n112\n");

        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _EGroundCabel.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.GroundCabel, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EGroundCabel.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));
        //            //ed.WriteMessage("\n111\n");

        //            EGroundCabel Ap = Atend.Base.Equipment.EGroundCabel.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EGroundCabel.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.EGroundCabel.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));

        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.GroundCabel, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR EMiddleGroundCable.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

    }
}
