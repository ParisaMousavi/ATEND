using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Equipment
{
    public class EConductor
    {
        public EConductor()
        { }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int prouctCode;
        public int ProductCode
        {
            get { return prouctCode; }
            set { prouctCode = value; }
        }

        private byte materialCode;
        public byte MaterialCode
        {
            get { return materialCode; }
            set { materialCode = value; }
        }

        private double crossSectionArea;
        public double CrossSectionArea
        {
            get { return crossSectionArea; }
            set { crossSectionArea = value; }
        }

        private double diagonal;
        public double Diagonal
        {
            get { return diagonal; }
            set { diagonal = value; }
        }

        private double alasticity;
        public double Alasticity
        {
            get { return alasticity; }
            set { alasticity = value; }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private double alpha;
        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        private double uts;
        public double UTS
        {
            get { return uts; }
            set { uts = value; }
        }

        private double maxCurrent;
        public double MaxCurrent
        {
            get { return maxCurrent; }
            set { maxCurrent = value; }
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

        private byte damperType;
        public byte DamperType
        {
            get { return damperType; }
            set { damperType = value; }
        }

        private bool isCabel;
        public bool IsCabel
        {
            get { return isCabel; }
            set { isCabel = value; }
        }

        private byte cabelTypeCode;
        public byte CabelTypeCode
        {
            get { return cabelTypeCode; }
            set { cabelTypeCode = value; }
        }

        private double gmr;
        public double GMR
        {
            get { return gmr; }
            set { gmr = value; }
        }

        private int beginHeaderCabel;
        public int BeginHeaderCabel
        {
            get { return beginHeaderCabel; }
            set { beginHeaderCabel = value; }
        }

        private int endHeaderCabel;
        public int EndHeaderCabel
        {
            get { return endHeaderCabel; }
            set { endHeaderCabel = value; }
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

        private double wc;
        public double Wc
        {
            get { return wc; }
            set { wc = value; }
        }

        private int typeCode;
        public int TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
        }

        private double capacitance;
        public double Capacitance
        {
            get { return capacitance; }
            set { capacitance = value; }
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

        private double maxCurrent1Second;
        public double MaxCurrent1Second
        {
            get { return maxCurrent1Second; }
            set { maxCurrent1Second = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
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
                ed.WriteMessage(string.Format("Error EConductor.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }


        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Conductor_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            command.Transaction = _transaction;

            try
            {

                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                //connection.Close();
                bool canCommitTransaction = true;
                int Counter = 0;


                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                    _EOperation.ProductCode = Code;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);
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
                ed.WriteMessage(string.Format("Error EConductor.InsertX : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Conductor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new SqlParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new SqlParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new SqlParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new SqlParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iWC", Wc));
            insertCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Conductor, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Conductor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Conductor_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new SqlParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new SqlParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new SqlParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new SqlParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iWC", Wc));
            insertCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Conductor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Conductor, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in Conductor");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Conductor, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Conductor_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();

                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)))
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
            catch
            {
                //connection.Close();
                return false;
            }
        }

        public static EConductor SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                //ed.WriteMessage("\nss555\n");

                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //ed.WriteMessage("\nss666\n");


            }

            reader.Close();
            connection.Close();
            return Conductor;

        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                //Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                //Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                UTS = Convert.ToDouble(reader["UTS"].ToString());
                Weight = Convert.ToDouble(reader["Weight"].ToString());
                GMR = Convert.ToDouble(reader["GMR"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Wc = Convert.ToDouble(reader["WC"]);
                TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

            }

            reader.Close();
            //connection.Close();
        }

        public static EConductor SelectByProductCode(int productCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Conductor_SelectByProductCode", connection);
            command.Parameters.Add(new SqlParameter("iProductCode", productCode));
            command.CommandType = CommandType.StoredProcedure;
            EConductor Conductor = new EConductor();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                    Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                    Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                    Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                    Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                    Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                    Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                    Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                    Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                    Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                    Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                    Conductor.Comment = reader["Comment"].ToString();
                    Conductor.Name = reader["Name"].ToString();
                    Conductor.Wc = Convert.ToDouble(reader["WC"]);
                    Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                    Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found for productCode : {0} \n", productCode));
                }

                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECounductor.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }


            return Conductor;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        //public static DataTable SelectByType(int Type)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_SelectByType", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iTypeCode", Type));
        //    DataSet dsConductor = new DataSet();
        //    adapter.Fill(dsConductor);
        //    return dsConductor.Tables[0];

        //}

        public static DataTable SelectForCalculate(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_SelectForCalculate", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static DataTable Search(string Name, bool IsCabel)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        public static DataTable DrawSearch(double crossSectionArea, int materialCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iMaterialCode", materialCode));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        public static EConductor SelectByProductBlockCode(int ProductBlockCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_Conductor_SelectByProductBlockCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iProductBlockCode", ProductBlockCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());

            }
            reader.Close();
            Connection.Close();
            return Conductor;

        }

        public static EConductor ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Conductor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("\nss111\n");
            //connection.Open();
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //ed.WriteMessage("\nss222\n");
            SqlDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("\nss333\n");

            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                //ed.WriteMessage("\nss444\n");

                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                //ed.WriteMessage("\nss555\n");

                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //ed.WriteMessage("\nss666\n");

            }

            reader.Close();
            //connection.Close();
            return Conductor;

        }

        //MEDHAT //ShareOnServer
        public static EConductor ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();

            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());

                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : conductor\n");
            }
            reader.Close();
            return Conductor;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Conductor_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            Command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            Command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            Command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            Command.Parameters.Add(new SqlParameter("iWeight", Weight));
            Command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            Command.Parameters.Add(new SqlParameter("iUTS", UTS));
            Command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            Command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            Command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            Command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            Command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            Command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            Command.Parameters.Add(new SqlParameter("iGMR", GMR));
            Command.Parameters.Add(new SqlParameter("iComment", Comment));
            Command.Parameters.Add(new SqlParameter("iName", Name));
            Command.Parameters.Add(new SqlParameter("iWC", Wc));
            Command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            Command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;

                try
                {
                    Command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);
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

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = new EContainerPackage();
                    if (canCommitTransaction)
                    {
                        containerPackage.XCode = XCode;
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);
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

                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error EConductor.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EConductor.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }


            ////////////////////////
            //try
            //{

            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error EConductor.InsertX : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                while (canCommitTransaction && Counter < OperationList.Count)
                {
                    Atend.Base.Equipment.EOperation _EOperation;
                    _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                    _EOperation.ProductCode = 0;// containerCode;
                    _EOperation.XCode = XCode;
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);
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
                //Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConductor.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }


            ///////////////////
            //try
            //{

            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error EConductor.InsertX : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Conductor_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new SqlParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new SqlParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new SqlParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new SqlParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iWC", Wc));
            insertCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                ////insertCommand.ExecuteNonQuery();
                ////insertCommand.Parameters.Clear();
                ////insertCommand.CommandType = CommandType.Text;
                ////insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                insertCommand.ExecuteNonQuery();

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Conductor, ServerConnection, ServerTransaction);
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
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Conductor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_Conductor_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new SqlParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new SqlParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new SqlParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new SqlParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iWC", Wc));
            insertCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Conductor))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Conductor, ServerTransaction , ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in airpost");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Delete Operation Failed in Conductor");
                    }
                }
                ed.WriteMessage("EConductor.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Conductor, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlTransaction _transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);

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
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
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
                    ed.WriteMessage(string.Format("Error EConductor.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error EConductor.UpdateX 02: {0} \n", ex2.Message));
                connection.Close();
                return false;
            }



            ////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    ed.WriteMessage("ERROR ECovductor.UpdateX :{0}", ex);
            //    connection.Close();
            //    return false;
            //}
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("E_Conductor_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialCode", MaterialCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", Alasticity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            command.Parameters.Add(new SqlParameter("iCabelTypeCode", CabelTypeCode));
            command.Parameters.Add(new SqlParameter("iDamperType", DamperType));
            command.Parameters.Add(new SqlParameter("iIsCabel", isCabel));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iWC", Wc));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                //if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)))
                //{

                //    while (canCommitTransaction && Counter < operationList.Count)
                //    {

                //        Atend.Base.Equipment.EOperation _EOperation;
                //        _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                //        _EOperation.XCode = XCode;
                //        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor);

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
                //if (canCommitTransaction)
                //{
                //    _transaction.Commit();
                //    connection.Close();
                //    return true;
                //}
                //else
                //{
                //    _transaction.Rollback();
                //    connection.Close();
                //    return false;
                //    //throw new Exception("can not commit transaction");

                //}
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EConductor.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }




            //////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch
            //{
            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid XCode)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Conductor);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_DeleteX", connection);
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

                    if (EOperation.DeleteX(transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)))
                    {
                        canCommitTransaction = true;
                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
                    if ((EContainerPackage.DeleteX(transaction, connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error EConductor.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EConductor.Delete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }



            ////////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch
            //{
            //    connection.Close();
            //    return false;
            //}
        }

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor)))
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
                    ed.WriteMessage(string.Format("Error EConductor.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction EConductor.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }



            //////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch
            //{
            //    //connection.Close();
            //    return false;
            //}
        }

        //MOUSAVI //SentFromLocalToAccess
        public static EConductor SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Conductor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Conductor));
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
            command.Parameters.Add(new SqlParameter("iXCode", Conductor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Conductor));
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
            return Conductor;

        }

        //MEDHAT //ShareOnServer
        public static EConductor SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            return Conductor;

        }

        //ASHKTORAB
        public static EConductor SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", Conductor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Conductor));
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
            command.Parameters.Add(new SqlParameter("iXCode", Conductor.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.Conductor));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());
            }

            reader.Close();
            //connection.Close();
            return Conductor;

        }

        //ASHKTORAB
        public static EConductor SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return Conductor;

        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }
        //Hatami
        public static DataTable SelectByType(int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iTypeCode", Type));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Conductor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iIsCabel", IsCabel));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_SearchByName", connection);
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
        public static EConductor CheckForExist(double _MaterialCode, int _TypeCode, double _CrossSectionArea,
                                               double _Diagonal, double _Alasticity, double _Alpha,
                                               double _UTS, double _MaxCurrent, double _Resistance,
                                               double _Reactance, double _WC, double _Capacitance, double _MaxCurrent1Second)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Conductor_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iMaterialCode", _MaterialCode));
            command.Parameters.Add(new SqlParameter("iTypeCode", _TypeCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionArea", _CrossSectionArea));
            command.Parameters.Add(new SqlParameter("iDiagonal", _Diagonal));
            command.Parameters.Add(new SqlParameter("iAlasticity", _Alasticity));
            command.Parameters.Add(new SqlParameter("iAlpha", _Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", _UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", _MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", _Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", _Reactance));
            command.Parameters.Add(new SqlParameter("iWC", _WC));
            command.Parameters.Add(new SqlParameter("iCapacitance", _Capacitance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", _MaxCurrent1Second));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"].ToString());
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            connection.Close();
            return Conductor;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Conductor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new OleDbParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new OleDbParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new OleDbParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new OleDbParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new OleDbParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new OleDbParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iWC", Wc));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Conductor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new OleDbParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new OleDbParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new OleDbParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new OleDbParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new OleDbParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new OleDbParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iWC", Wc));
            insertCommand.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));

            try
            {


                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Conductor);
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
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Conductor, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_Conductor_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iMaterialCode", MaterialCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", CrossSectionArea));
            insertCommand.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            insertCommand.Parameters.Add(new OleDbParameter("iAlasticity", Alasticity));
            insertCommand.Parameters.Add(new OleDbParameter("iWeight", Weight));
            insertCommand.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            insertCommand.Parameters.Add(new OleDbParameter("iUTS", UTS));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            insertCommand.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            insertCommand.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            insertCommand.Parameters.Add(new OleDbParameter("iDamperType", DamperType));
            insertCommand.Parameters.Add(new OleDbParameter("iIsCabel", IsCabel));
            insertCommand.Parameters.Add(new OleDbParameter("iCabelTypeCode", CabelTypeCode));
            insertCommand.Parameters.Add(new OleDbParameter("iGMR", GMR));
            insertCommand.Parameters.Add(new OleDbParameter("iComment", Comment));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iWC", Wc));
            insertCommand.Parameters.Add(new OleDbParameter("iTypeCode", TypeCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            insertCommand.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
            int oldCode = Code;
            try
            {


                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(oldCode, (int)Atend.Control.Enum.ProductType.Conductor);
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
                    ed.WriteMessage("Main Parent is Counductor: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(oldCode, (int)Atend.Control.Enum.ProductType.Conductor, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess Conductor failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EConductor.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //MOUSAVI //StatusReport
        public static EConductor AccessSelectByCode(int Code)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                //ed.WriteMessage("**** AccessSelectByCode2 ***** \n");
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                //Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"]);
                //Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //ed.WriteMessage("~~~~%%%%%%%%%%%%~~~~~~~~~conductor found \n");
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            connection.Close();
            return Conductor;

        }

        public static EConductor AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection =_connection;
            OleDbCommand command = new OleDbCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                //ed.WriteMessage("**** AccessSelectByCode2 ***** \n");
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                //Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"]);
                //Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //ed.WriteMessage("~~~~%%%%%%%%%%%%~~~~~~~~~conductor found \n");
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return Conductor;

        }

        //CALCULATION HATAMI
        public static EConductor AccessSelectByCode(int Code, OleDbConnection _Connection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_Conductor_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                //ed.WriteMessage("**** AccessSelectByCode2 ***** \n");
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                //Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"]);
                //Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //ed.WriteMessage("~~~~%%%%%%%%%%%%~~~~~~~~~conductor found \n");
            }
            else
            {
                Conductor.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return Conductor;

        }

        public static EConductor AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Conductor_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                //Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
                Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"]);
                Conductor.XCode = new Guid(reader["XCode"].ToString());
                //Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }

            else
            {
                Conductor.Code = -1;
            }

            reader.Close();
            connection.Close();
            return Conductor;

        }

        //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static EConductor AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Conductor_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    EConductor Conductor = new EConductor();
        //    if (reader.Read())
        //    {
        //        Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
        //        Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
        //        Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
        //        Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
        //        Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
        //        Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
        //        Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
        //        //Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
        //        Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
        //        Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
        //        Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
        //        Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
        //        Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
        //        Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
        //        Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
        //        Conductor.Comment = reader["Comment"].ToString();
        //        Conductor.Name = reader["Name"].ToString();
        //        Conductor.Wc = Convert.ToDouble(reader["WC"]);
        //        Conductor.TypeCode = Convert.ToInt32(reader["TypeCode"]);
        //        Conductor.Capacitance = Convert.ToDouble(reader["Capacitance"]);
        //        Conductor.XCode = new Guid(reader["XCode"].ToString());
        //        //Conductor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
        //        Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
        //    }
        //    else
        //    {
        //        Conductor.Code = -1;
        //    }
        //    reader.Close();
        //    return Conductor;
        //}

        //Hatami
        public static DataTable AccessSelectByType(int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Conductor_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iTypeCode", Type));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static EConductor AccessSelectByProductCode(int productCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Conductor_SelectByProductCode", connection);
            command.Parameters.Add(new OleDbParameter("iProductCode", productCode));
            command.CommandType = CommandType.StoredProcedure;
            EConductor Conductor = new EConductor();

            try
            {
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                    Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                    Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                    Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                    Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                    Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                    Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                    Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                    Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                    Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                    Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                    Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                    Conductor.Comment = reader["Comment"].ToString();
                    Conductor.Name = reader["Name"].ToString();
                    Conductor.Wc = Convert.ToDouble(reader["WC"]);
                    Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

                }
                else
                {
                    //ed.WriteMessage(string.Format("No record found for productCode : {0} \n", productCode));
                }

                reader.Close();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ECounductor.SelectByProductCode : {0} \n", ex1.Message));
                connection.Close();
            }


            return Conductor;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Conductor_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static DataTable AccessSearch(string Name, bool IsCabel)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Conductor_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsCabel", IsCabel));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        public static DataTable AccessDrawSearch(double crossSectionArea, int materialCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Conductor_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iMaterialCode", materialCode));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        public static EConductor AccessSelectByProductBlockCode(int ProductBlockCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_Conductor_SelectByProductBlockCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iProductBlockCode", ProductBlockCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EConductor Conductor = new EConductor();
            if (reader.Read())
            {
                Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
                Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
                Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
                Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
                Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
                Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
                Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
                Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
                Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
                Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
                Conductor.Comment = reader["Comment"].ToString();
                Conductor.Name = reader["Name"].ToString();
                Conductor.Wc = Convert.ToDouble(reader["WC"]);
                Conductor.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());

            }
            reader.Close();
            Connection.Close();
            return Conductor;

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

        //        EConductor conductor = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        //            //ed.WriteMessage("\n111\n");

        //            EConductor Ap = Atend.Base.Equipment.EConductor.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EConductor.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            conductor.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, conductor.XCode);
        //            conductor.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (conductor.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, conductor.Code, (int)Atend.Control.Enum.ProductType.Conductor))
        //                {
        //                    if (Atend.Base.Equipment.EConductorTip.Update(Servertransaction, Serverconnection, DeletedCode, conductor.Code))
        //                    {
        //                        if (!conductor.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");

        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //ed.WriteMessage("\n114\n");

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

        //                //Servertransaction.Commit();
        //                //Serverconnection.Close();

        //                //Localtransaction.Commit();
        //                //Localconnection.Close();
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, conductor.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Conductor, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ECell.ShareOnServer {0}\n", ex1.Message));

        //        Serverconnection.Close();
        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool ShareOnServer(SqlTransaction _servertransaction, SqlConnection _serverconnection, SqlTransaction _localtransaction, SqlConnection _localconnection, Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = _serverconnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction = _servertransaction; ;

        //    SqlConnection Localconnection = _localconnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction = _localtransaction;

        //    int DeletedCode = 0;

        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();

        //        EConductor conductor = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        //            //ed.WriteMessage("\n111\n");

        //            EConductor Ap = Atend.Base.Equipment.EConductor.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.EConductor.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    return false;
        //                }
        //            }

        //            DataTable OperationTbl = new DataTable();
        //            conductor.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, conductor.XCode);
        //            conductor.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (conductor.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, conductor.Code, (int)Atend.Control.Enum.ProductType.Conductor))
        //                {
        //                    if (Atend.Base.Equipment.EConductorTip.Update(Servertransaction, Serverconnection, DeletedCode, conductor.Code))
        //                    {
        //                        if (!conductor.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            //ed.WriteMessage("\n115\n");


        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //ed.WriteMessage("\n114\n");


        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    //ed.WriteMessage("\n114\n");


        //                    return false;
        //                }


        //            }


        //            //ed.WriteMessage("\n112\n");

        //        }
        //        catch (System.Exception ex1)
        //        {
        //            ed.WriteMessage(string.Format("Error In TransAction:{0}\n", ex1.Message));

        //            return false;
        //        }


        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format(" ERROR ECell.ShareOnServer {0}\n", ex1.Message));


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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        //            ////ed.WriteMessage("\n111\n");

        //            EConductor Ap = Atend.Base.Equipment.EConductor.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n222\n");
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EConductor.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                //ed.WriteMessage("\n333\n");

        //                Ap.ServerSelectByCode(Code);
        //                //ed.WriteMessage("\n444\n");
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n555\n");
        //                Ap = Atend.Base.Equipment.EConductor.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Conductor));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);

        //            //ed.WriteMessage("\n666\n");

        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;

        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.Conductor, Localtransaction, Localconnection))
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
        //            //    //ed.WriteMessage("\n113\n");

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
