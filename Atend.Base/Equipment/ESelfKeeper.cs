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
    public class ESelfKeeper
    {
        public ESelfKeeper()
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

        private int materialConductorCode;
        public int MaterialConductorCode
        {
            get { return materialConductorCode; }
            set { materialConductorCode = value; }
        }

        private int materialDamperCode;
        public int MaterialDamperCode
        {
            get { return materialDamperCode; }
            set { materialDamperCode = value; }
        }

        private double crossSectionAreaConductor;
        public double CrossSectionAreaConductor
        {
            get { return crossSectionAreaConductor; }
            set { crossSectionAreaConductor = value; }
        }

        private double crossSectionKeeper;
        public double CrossSectionKeeper
        {
            get { return crossSectionKeeper; }
            set { crossSectionKeeper = value; }
        }

        private double diagonal;
        public double Diagonal
        {
            get { return diagonal; }
            set { diagonal = value; }
        }

        private double alastisity;
        public double Alastisity
        {
            get { return alastisity; }
            set { alastisity = value; }
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

        private double capacitance;
        public double Capacitance
        {
            get { return capacitance; }
            set { capacitance = value; }
        }

        private double gmr;
        public double GMR
        {
            get { return gmr; }
            set { gmr = value; }
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

        private double namedVoltage;
        public double NamedVoltage
        {
            get { return namedVoltage; }
            set { namedVoltage = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private double maxCurrent1Second;
        public double MaxCurrent1Second
        {
            get { return maxCurrent1Second; }
            set { maxCurrent1Second = value; }
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

        //~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
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
                ed.WriteMessage(string.Format("Error ESelfKeeper.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));


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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);
                    //_EOperation.ProductID = 
                    if (_EOperation.Insert(_transaction, _connection) && canCommitTransaction)
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
                ed.WriteMessage(string.Format("Error ESelfKeeper.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }
        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in SelfKeeper");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer SelfKeeper failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.SeverInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in SelfKeeper");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation failed in SelfKeeper");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer SelfKeeper failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.SeverUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            //command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error iN ESelfKeeper.Update : " + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Delete", connection);
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
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                if (EOperation.Delete(_transaction, _connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper)))
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

        public static ESelfKeeper SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                //selfKeeper.XCode = new Guid(reader["XCode"].ToString());

                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }

            reader.Close();
            connection.Close();
            return selfKeeper;

        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                // selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                //ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                UTS = Convert.ToDouble(reader["UTS"].ToString());
                Weight = Convert.ToDouble(reader["Weight"].ToString());
                GMR = Convert.ToDouble(reader["GMR"].ToString());
                Comment = reader["Comment"].ToString();
                Name = reader["Name"].ToString();
                Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }

            reader.Close();
            connection.Close();
            //return selfKeeper;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static DataTable Search(string Name, bool IsCabel)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

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

        public static ESelfKeeper ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }

            reader.Close();
            //connection.Close();
            return selfKeeper;

        }

        //MEDHAT //ShareOnServer
        public static ESelfKeeper ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();

            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }
            else
            {
                selfKeeper.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : selfkeeper\n");
            }
            reader.Close();
            return selfKeeper;
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Local PArt~~~~~~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_InsertX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;

                    //ed.writeMessage("count of subequip " + OperationList.Count.ToString() + " \n");
                    while (canCommitTransaction && Counter < OperationList.Count)
                    {
                        Atend.Base.Equipment.EOperation _EOperation;
                        _EOperation = ((Atend.Base.Equipment.EOperation)OperationList[Counter]);
                        //_EOperation.ProductCode = 0;// containerCode;
                        _EOperation.XCode = XCode;
                        _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);
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
                        containerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);
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
                    ed.WriteMessage(string.Format("Error ESelfKeeper.Insert 01 : {0} \n", ex1.Message));
                    transaction.Rollback();

                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.Insert 02 : {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }



            /////////////////////////////
            //try
            //{

            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error ESelfKeeper.Insert : {0} \n", ex1.Message));
            //    connection.Close();
            //    return false;
            //}
        }

        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            command.Transaction = _transaction;
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
                    _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);
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
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.Insert : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }




            ////////////////////////////
            //try
            //{

            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;

            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error ESelfKeeper.Insert : {0} \n", ex1.Message));
            //    //connection.Close();
            //    return false;
            //}
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in SelfKeeper");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer SelfKeeper failed");
                    }
                }

                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iWeight", Weight));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in SelfKeeper");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation failed in SelfKeeper");
                    }
                }
                ed.WriteMessage("ESelfkeeper.Operation passed \n");

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer SelfKeeper failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlTransaction _transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                Connection.Open();
                _transaction = Connection.BeginTransaction();
                command.Transaction = _transaction;

                try
                {
                    //ed.WriteMessage("1");
                    command.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;


                    Counter = 0;
                    if (EOperation.DeleteX(_transaction, Connection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper)))
                    {
                        while (canCommitTransaction && Counter < operationList.Count)
                        {

                            Atend.Base.Equipment.EOperation _EOperation;
                            _EOperation = ((Atend.Base.Equipment.EOperation)operationList[Counter]);
                            _EOperation.XCode = XCode;
                            _EOperation.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper);

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

                    //Package
                    ed.WriteMessage("EProductPackage.Delete:\n");
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                    ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");
                    Counter = 0;
                    if (EProductPackage.Delete(_transaction, Connection, containerPackage.Code))
                    {
                        while (canCommitTransaction && Counter < EquipmentList.Count)
                        {
                            Atend.Base.Equipment.EProductPackage _EProductPackage;
                            _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                            ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                            _EProductPackage.ContainerPackageCode = containerPackage.Code;

                            if (_EProductPackage.InsertX(_transaction, Connection) && canCommitTransaction)
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
                        Connection.Close();
                        return true;
                    }
                    else
                    {
                        _transaction.Rollback();
                        Connection.Close();
                        return false;
                        //throw new Exception("can not commit transaction");

                    }


                }
                catch (Exception ex1)
                {
                    ed.WriteMessage(string.Format("Error ESelfKeeper.UpdateX 01: {0} \n", ex1.Message));
                    _transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (Exception ex2)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.UpdateX 02: {0} \n", ex2.Message));
                Connection.Close();
                return false;
            }




            //////////////////////////
            //try
            //{
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    ed.WriteMessage("Error iN ESelfKeeper.UpdateX : " + ex.Message + "\n");
            //    connection.Close();
            //    return false;
            //}
        }

        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_UpdateX", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new SqlParameter("iWeight", weight));
            command.Parameters.Add(new SqlParameter("iAlpha", Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", Reactance));
            command.Parameters.Add(new SqlParameter("iGMR", GMR));
            command.Parameters.Add(new SqlParameter("iComment", Comment));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", MaxCurrent1Second));
            try
            {
                //Connection.Open();
                command.ExecuteNonQuery();
                bool canCommitTransaction = true;
                int Counter = 0;

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.UpdateX : {0} \n", ex1.Message));
                //Connection.Close();
                return false;
            }



            //////////////////////////
            //try
            //{
            //    //connection.Open();
            //    command.ExecuteNonQuery();
            //    //connection.Close();
            //    return true;
            //}
            //catch (System.Exception ex)
            //{
            //    ed.WriteMessage("Error iN ESelfKeeper.UpdateX : " + ex.Message + "\n");
            //    //connection.Close();
            //    return false;
            //}
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.SelfKeeper);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }

            SqlTransaction transaction;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_SelfKeeper_DeleteX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", Code));
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                Command.Transaction = transaction;
                try
                {
                    Command.ExecuteNonQuery();
                    Boolean canCommitTransaction = true;
                    if (EOperation.DeleteX(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper)))
                    {
                        canCommitTransaction = true;

                    }
                    else
                    {
                        canCommitTransaction = false;
                    }

                    //Package
                    Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
                    if ((EContainerPackage.DeleteX(transaction, Connection, containerPackage.XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper))) & canCommitTransaction)
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
                    ed.WriteMessage(string.Format("Error ESelfKeeper.Delete : {0} \n", ex1.Message));
                    transaction.Rollback();
                    Connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ESelfKeeper.Delete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }



            ///////////////////////
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

        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            command.Transaction = _transaction;
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();
                command.Transaction = _transaction;
                try
                {
                    command.ExecuteNonQuery();
                    if (EOperation.DeleteX(_transaction, connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper)))
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
                    ed.WriteMessage(string.Format("Error ESelfKeeper.Delete : {0} \n", ex1.Message));
                    string s = ex1.Message;
                    //transaction.Rollback();
                    //connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error Transaction ESelfKeeper.Delete : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }




            ////////////////////////////
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
        public static ESelfKeeper SelectByXCode(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }
            else
            {
                selfKeeper.Code = -1;
            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", selfKeeper.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.SelfKeeper));
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
            command.Parameters.Add(new SqlParameter("iXCode", selfKeeper.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.SelfKeeper));
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
            return selfKeeper;

        }

        //ASHKTORAB //ShareOnServer
        public static ESelfKeeper SelectByXCodeForDesign(Guid Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_SelfKeeper_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
            SqlTransaction trans = LocalTransaction;
            ESelfKeeper selfKeeper = new ESelfKeeper();

            try
            {
                command.Transaction = trans;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                    selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                    selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                    selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                    selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                    selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                    selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                    selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                    selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                    selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                    selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                    selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                    selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                    selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                    selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                    selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                    selfKeeper.Comment = reader["Comment"].ToString();
                    selfKeeper.Name = reader["Name"].ToString();
                    selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                    selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                    //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

                }
                else
                {
                    selfKeeper.Code = -1;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ESelfKeeper.In SelectByXCodeForDesign.TransAction:{0}\n", ex.Message);
            }

            return selfKeeper;

        }

        //ASHKTORAB
        public static ESelfKeeper SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }
            else
            {
                selfKeeper.code = -1;

            }
            reader.Close();
            //EQUIPMENT
            command.Parameters.Clear();
            command.CommandText = "E_ProductPackage_SelectByContainerXCodeType";
            command.Parameters.Add(new SqlParameter("iXCode", selfKeeper.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.SelfKeeper));
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
            command.Parameters.Add(new SqlParameter("iXCode", selfKeeper.XCode));
            command.Parameters.Add(new SqlParameter("iType", Atend.Control.Enum.ProductType.SelfKeeper));
            reader = command.ExecuteReader();
            nodeKeys.Clear();
            while (reader.Read())
            {
                nodeKeys.Add(reader["ProductID"].ToString());

            }

            reader.Close();
            //connection.Close();
            return selfKeeper;

        }

        //ASHKTORAB
        public static ESelfKeeper SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
                //selfKeeper.Wc = Convert.ToDouble(reader["WC"]);

            }
            else
            {
                selfKeeper.Code = -1;
            }


            reader.Close();
            //connection.Close();
            return selfKeeper;

        }

        //Hatami
        public static DataTable SelectByType(int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iMaterialConductorCode", Type));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        public static DataTable SelectAllXCode()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_SelectAllByXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        //HAtami
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        //Hatami
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeper_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_SearchByName", connection);
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
        public static ESelfKeeper CheckForExist(int _MaterialConductorCode, int _MaterialDamperCode, double _CrossSectionAreaConductor,
                                                double _CrossSectionKeeper, double _Diagonal, double _Alastisity, double _Alpha,
                                                double _UTS, double _MaxCurrent, double _Resistance, double _Reactance, double _Weight,
                                                double _NamedVoltage, double _Capacitance, double _MaxCurrent1Second)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeper_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iMaterialConductorCode", _MaterialConductorCode));
            command.Parameters.Add(new SqlParameter("iMaterialDamperCode", _MaterialDamperCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionAreaConductor", _CrossSectionAreaConductor));
            command.Parameters.Add(new SqlParameter("iCrossSectionKeeper", _CrossSectionKeeper));
            command.Parameters.Add(new SqlParameter("iDiagonal", _Diagonal));
            command.Parameters.Add(new SqlParameter("iAlastisity", _Alastisity));
            command.Parameters.Add(new SqlParameter("iAlpha", _Alpha));
            command.Parameters.Add(new SqlParameter("iUTS", _UTS));
            command.Parameters.Add(new SqlParameter("iMaxCurrent", _MaxCurrent));
            command.Parameters.Add(new SqlParameter("iResistance", _Resistance));
            command.Parameters.Add(new SqlParameter("iReactance", _Reactance));
            command.Parameters.Add(new SqlParameter("iWeight", _Weight));
            command.Parameters.Add(new SqlParameter("iNamedVoltage", _NamedVoltage));
            command.Parameters.Add(new SqlParameter("iCapacitance", _Capacitance));
            command.Parameters.Add(new SqlParameter("iMaxCurrent1Second", _MaxCurrent1Second));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                selfKeeper.Code = -1;
            }
            reader.Close();
            connection.Close();
            return selfKeeper;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new OleDbParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new OleDbParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new OleDbParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new OleDbParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new OleDbParameter("iWeight", Weight));
            command.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            command.Parameters.Add(new OleDbParameter("iUTS", UTS));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iGMR", GMR));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));

            try
            {

                connection.Open();
                Code = Convert.ToInt32(command.ExecuteNonQuery());
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new OleDbParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new OleDbParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new OleDbParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new OleDbParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            command.Parameters.Add(new OleDbParameter("iUTS", UTS));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iGMR", GMR));
            command.Parameters.Add(new OleDbParameter("iWeight", Weight));
            ed.WriteMessage("Comment:{0} \n", Comment);
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess SelfKeeper failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;


            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iMaterialConductorCode", MaterialConductorCode));
            command.Parameters.Add(new OleDbParameter("iMaterialDamperCode", MaterialDamperCode));
            command.Parameters.Add(new OleDbParameter("iCrossSectionAreaConductor", CrossSectionAreaConductor));
            command.Parameters.Add(new OleDbParameter("iCrossSectionKeeper", CrossSectionKeeper));
            command.Parameters.Add(new OleDbParameter("iDiagonal", Diagonal));
            command.Parameters.Add(new OleDbParameter("iAlastisity", Alastisity));
            command.Parameters.Add(new OleDbParameter("iAlpha", Alpha));
            command.Parameters.Add(new OleDbParameter("iUTS", UTS));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent", MaxCurrent));
            command.Parameters.Add(new OleDbParameter("iResistance", Resistance));
            command.Parameters.Add(new OleDbParameter("iReactance", Reactance));
            command.Parameters.Add(new OleDbParameter("iGMR", GMR));
            command.Parameters.Add(new OleDbParameter("iWeight", Weight));
            command.Parameters.Add(new OleDbParameter("iComment", Comment));
            command.Parameters.Add(new OleDbParameter("iNamedVoltage", NamedVoltage));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iCapacitance", Capacitance));
            command.Parameters.Add(new OleDbParameter("iMaxCurrent1Second", MaxCurrent1Second));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.SelfKeeper);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.SelfKeeper, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess SelfKeeper failed");
                    }
                }


                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeper.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //StatusReport
        public static ESelfKeeper AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                selfKeeper.Code = -1;
            }
            reader.Close();
            connection.Close();
            return selfKeeper;

        }

        public static ESelfKeeper AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                selfKeeper.Code = -1;
            }
            reader.Close();
            return selfKeeper;

        }

        public static ESelfKeeper AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
           // connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                selfKeeper.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return selfKeeper;

        }

        public static ESelfKeeper AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeper_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeper selfKeeper = new ESelfKeeper();
            if (reader.Read())
            {
                selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
                selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

                selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
                selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
                selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
                selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
                selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
                selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
                selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
                selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
                selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
                selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
                selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
                selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
                selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
                selfKeeper.Comment = reader["Comment"].ToString();
                selfKeeper.Name = reader["Name"].ToString();
                selfKeeper.XCode = new Guid(reader["XCode"].ToString());
                selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
                selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
            }
            else
            {
                selfKeeper.Code = -1;
            }

            reader.Close();
            connection.Close();
            return selfKeeper;

        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ESelfKeeper AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_SelfKeeper_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;


        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ESelfKeeper selfKeeper = new ESelfKeeper();
        //    if (reader.Read())
        //    {
        //        selfKeeper.Alastisity = Convert.ToDouble(reader["Alastisity"].ToString());
        //        selfKeeper.Alpha = Convert.ToDouble(reader["Alpha"].ToString());

        //        selfKeeper.Code = Convert.ToInt32(reader["Code"].ToString());
        //        selfKeeper.CrossSectionAreaConductor = Convert.ToDouble(reader["CrossSectionAreaConductor"].ToString());
        //        selfKeeper.CrossSectionKeeper = Convert.ToDouble(reader["CrossSectionKeeper"].ToString());
        //        selfKeeper.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
        //        selfKeeper.NamedVoltage = Convert.ToDouble(reader["NamedVoltage"].ToString());
        //        selfKeeper.MaterialConductorCode = Convert.ToInt32(reader["MaterialConductorCode"].ToString());
        //        selfKeeper.MaterialDamperCode = Convert.ToInt32(reader["MaterialDamperCode"].ToString());
        //        selfKeeper.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
        //        selfKeeper.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        selfKeeper.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
        //        selfKeeper.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
        //        selfKeeper.UTS = Convert.ToDouble(reader["UTS"].ToString());
        //        selfKeeper.Weight = Convert.ToDouble(reader["Weight"].ToString());
        //        selfKeeper.GMR = Convert.ToDouble(reader["GMR"].ToString());
        //        selfKeeper.Comment = reader["Comment"].ToString();
        //        selfKeeper.Name = reader["Name"].ToString();
        //        selfKeeper.XCode = new Guid(reader["XCode"].ToString());
        //        selfKeeper.Capacitance = Convert.ToDouble(reader["Capacitance"].ToString());
        //        selfKeeper.MaxCurrent1Second = Convert.ToDouble(reader["MaxCurrent1Second"].ToString());
        //    }
        //    else
        //    {
        //        selfKeeper.Code = -1;
        //    }
        //    reader.Close();
        //    return selfKeeper;

        //}

        //public static EConductor AccessSelectByProductCode(int productCode)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand command = new OleDbCommand("E_Conductor_SelectByProductCode", connection);
        //    command.Parameters.Add(new OleDbParameter("iProductCode", productCode));
        //    command.CommandType = CommandType.StoredProcedure;
        //    EConductor Conductor = new EConductor();

        //    try
        //    {
        //        connection.Open();
        //        OleDbDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            Conductor.Alasticity = Convert.ToDouble(reader["Alasticity"].ToString());
        //            Conductor.Alpha = Convert.ToDouble(reader["Alpha"].ToString());
        //            Conductor.CabelTypeCode = Convert.ToByte(reader["CabelTypeCode"].ToString());
        //            Conductor.Code = Convert.ToInt32(reader["Code"].ToString());
        //            Conductor.CrossSectionArea = Convert.ToDouble(reader["CrossSectionArea"].ToString());
        //            Conductor.DamperType = Convert.ToByte(reader["DamperType"].ToString());
        //            Conductor.Diagonal = Convert.ToDouble(reader["Diagonal"].ToString());
        //            Conductor.IsCabel = Convert.ToBoolean(reader["IsCabel"].ToString());
        //            Conductor.MaterialCode = Convert.ToByte(reader["MaterialCode"].ToString());
        //            Conductor.MaxCurrent = Convert.ToDouble(reader["MaxCurrent"].ToString());
        //            Conductor.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //            Conductor.Reactance = Convert.ToDouble(reader["Reactance"].ToString());
        //            Conductor.Resistance = Convert.ToDouble(reader["Resistance"].ToString());
        //            Conductor.UTS = Convert.ToDouble(reader["UTS"].ToString());
        //            Conductor.Weight = Convert.ToDouble(reader["Weight"].ToString());
        //            Conductor.GMR = Convert.ToDouble(reader["GMR"].ToString());
        //            Conductor.Comment = reader["Comment"].ToString();
        //            Conductor.Name = reader["Name"].ToString();
        //            Conductor.Wc = Convert.ToDouble(reader["WC"]);


        //        }
        //        else
        //        {
        //            ed.WriteMessage(string.Format("No record found for productCode : {0} \n", productCode));
        //        }

        //        reader.Close();
        //        connection.Close();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error ECounductor.SelectByProductCode : {0} \n", ex1.Message));
        //        connection.Close();
        //    }


        //    return Conductor;

        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_SelfKeeper_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            return dsConductor.Tables[0];

        }

        //public static DataTable AccessSearch(string Name, bool IsCabel)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("E_Conductor_Search", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iIsCabel", IsCabel));
        //    DataSet dsConductor = new DataSet();
        //    adapter.Fill(dsConductor);
        //    return dsConductor.Tables[0];
        //}

        //public static DataTable AccessDrawSearch(double crossSectionArea, int materialCode)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("E_SelfKeeperTip_DrawSearch", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCrossSectionArea", crossSectionArea));
        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iMaterialCode", materialCode));
        //    DataSet dsConductor = new DataSet();
        //    adapter.Fill(dsConductor);
        //    return dsConductor.Tables[0];
        //}

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

        //frmSelfKeeper
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

        //        ESelfKeeper _ESelfKeeper = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        //            ed.WriteMessage("\n111\n");

        //            ESelfKeeper Ap = Atend.Base.Equipment.ESelfKeeper.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ESelfKeeper.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }



        //            DataTable OperationTbl = new DataTable();
        //            _ESelfKeeper.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _ESelfKeeper.XCode);
        //            _ESelfKeeper.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_ESelfKeeper.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _ESelfKeeper.Code, (int)Atend.Control.Enum.ProductType.SelfKeeper))
        //                {
        //                    if (Atend.Base.Equipment.ESelfKeeperTip.Update(Servertransaction, Serverconnection, DeletedCode, _ESelfKeeper.Code))
        //                    {
        //                        if (!_ESelfKeeper.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            ed.WriteMessage("\n115\n");

        //                            Servertransaction.Rollback();
        //                            Serverconnection.Close();
        //                            Localtransaction.Rollback();
        //                            Localconnection.Close();
        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            ed.WriteMessage("\n112\n");

        //                            //Servertransaction.Commit();
        //                            //Serverconnection.Close();

        //                            //Localtransaction.Commit();
        //                            //Localconnection.Close();

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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ESelfKeeper.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.ShareOnServer {0}\n", ex1.Message));

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

        //        ESelfKeeper _ESelfKeeper = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        //            ed.WriteMessage("\n111\n");

        //            ESelfKeeper Ap = Atend.Base.Equipment.ESelfKeeper.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ESelfKeeper.ServerDelete(Servertransaction, Serverconnection, Ap.Code))
        //                {
        //                    return false;
        //                }
        //            }



        //            DataTable OperationTbl = new DataTable();
        //            _ESelfKeeper.OperationList = new ArrayList();
        //            OperationTbl = Atend.Base.Equipment.EOperation.SelectByXCode(Localtransaction, Localconnection, _ESelfKeeper.XCode);
        //            _ESelfKeeper.OperationList = Atend.Base.Design.NodeTransaction.GetOperationList(OperationTbl);

        //            if (_ESelfKeeper.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (Atend.Base.Equipment.EProductPackage.Update(Servertransaction, Serverconnection, DeletedCode, _ESelfKeeper.Code, (int)Atend.Control.Enum.ProductType.SelfKeeper))
        //                {
        //                    if (Atend.Base.Equipment.ESelfKeeperTip.Update(Servertransaction, Serverconnection, DeletedCode, _ESelfKeeper.Code))
        //                    {
        //                        if (!_ESelfKeeper.UpdateX(Localtransaction, Localconnection))
        //                        {
        //                            ed.WriteMessage("\n115\n");

        //                            return false;
        //                        }
        //                        else
        //                        {
        //                            ed.WriteMessage("\n112\n");


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

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ESelfKeeper.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, Servertransaction, Serverconnection, Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n113\n");

        //                return true;
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR ECatOut.ShareOnServer {0}\n", ex1.Message));

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
        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        //            ESelfKeeper Ap = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Localtransaction, Localconnection, Code);
        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ESelfKeeper.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                Ap = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }
        //            Ap.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeper));
        //            Ap.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Ap.XCode);
        //            if (!Ap.InsertX(Localtransaction, Localconnection))
        //            {
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }
        //            if (Atend.Base.Design.NodeTransaction.SubProductsForServer(Ap.Code, Ap.XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.SelfKeeper, Localtransaction, Localconnection))
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
        //        ed.WriteMessage(string.Format(" ERROR ESelfKeeper.GetFromServer {0}\n", ex1.Message));
        //        Localconnection.Close();
        //        return false;
        //    }
        //    return true;
        //}



    }
}
