using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Equipment
{
    public class EConductorTip
    {

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int phaseCount;
        public int PhaseCount
        {
            get { return phaseCount; }
            set { phaseCount = value; }
        }

        private int phaseProductCode;
        public int PhaseProductCode
        {
            get { return phaseProductCode; }
            set { phaseProductCode = value; }
        }

        private Guid phaseProductXCode;
        public Guid PhaseProductXCode
        {
            get { return phaseProductXCode; }
            set { phaseProductXCode = value; }
        }

        private int neutralCount;
        public int NeutralCount
        {
            get { return neutralCount; }
            set { neutralCount = value; }
        }

        private int neutralProductCode;
        public int NeutralProductCode
        {
            get { return neutralProductCode; }
            set { neutralProductCode = value; }
        }


        private Guid neutralProductXCode;
        public Guid NeutralProductXCode
        {
            get { return neutralProductXCode; }
            set { neutralProductXCode = value; }
        }

        private int nightCount;
        public int NightCount
        {
            get { return nightCount; }
            set { nightCount = value; }
        }

        private int nightProductCode;
        public int NightProductCode
        {
            get { return nightProductCode; }
            set { nightProductCode = value; }
        }

        private Guid nightProductXCode;
        public Guid NightProductXCode
        {
            get { return nightProductXCode; }
            set { nightProductXCode = value; }
        }


        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));

            try
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConductorTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));



            try
            {

                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                //connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConductorTip.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }


        //HATAMI
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_ConductorTip_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip, LocalTransaction, LocalConnection);
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
                    ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_ConductorTip_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.ConductorTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in ConductorTip");
                    }
                }

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));

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

        //ASHKTORAB
        public static bool Update(SqlTransaction _transaction, SqlConnection _connection, int Code, int NewCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iNewCode", NewCode));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch
            {
                //connection.Close();
                return false;
            }
        }

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch
            {
                //connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Delete", connection);
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

        public static EConductorTip SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }

            reader.Close();
            connection.Close();
            return ConductorTip;

        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                //ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                Name = Convert.ToString(reader["Name"].ToString());
                PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                Description = reader["Description"].ToString();
            }

            reader.Close();
            connection.Close();
            //return ConductorTip;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        public static DataTable SearchConductorConductorTipConductorMaterialType(double crossSectionArea, int materialCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_SearchForTipMaterial", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iMaterialCode", materialCode));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        public static EConductorTip ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //connection.Open();
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }

            reader.Close();
            //connection.Close();
            return ConductorTip;

        }

        //MEDHAT
        public static EConductorTip ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();

            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                ConductorTip.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : conductor tip\n");
            }

            reader.Close();
            return ConductorTip;

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));



            try
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConductorTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("InsertX\n");
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            command.Transaction = _transaction;

            try
            {

                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error EConductorTip.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_ConductorTip_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.ConductorTip, ServerConnection, ServerTransaction);
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
                    //ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.ConductorTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_ConductorTip_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.ConductorTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.ConductorTip, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in ConductorTip");
                    }
                }
                ed.WriteMessage("EConductorTip.Operation passed \n");

                if (BringSubEquips)
                {
                    ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.ConductorTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }



        public bool UpdateX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
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

        //ASHKTORAB
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductXCode", NightProductXCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch
            {
                //connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool UpdateXX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode, Guid NewXCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_UpdateXX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iNewXCode", NewXCode));

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch
            {
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ConductorTip));

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            if (ProdTbl.Rows.Count > 0)
            {
                //return false;
            }

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
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

        //ASHKTORAB
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ConductorTip));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch
            {
                //connection.Close();
                return false;
            }
        }

        public static EConductorTip SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }

            reader.Close();
            connection.Close();
            return ConductorTip;

        }

        //MEDHAT
        public static EConductorTip SelectByPhaseProductXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByPhaseProductXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                //nothing find
                ConductorTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return ConductorTip;
        }

        //MEDHAT
        public static EConductorTip SelectByNeutralProductXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByNeutralProductXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                //nothing find
                ConductorTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return ConductorTip;
        }

        //MEDHAT
        public static EConductorTip SelectByNightProductXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByNightProductXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                //nothing find
                ConductorTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return ConductorTip;
        }

        //MEDHAT
        public static EConductorTip SelectByXCodeForDesign(Guid XCode, SqlConnection _connection, SqlTransaction _transaction)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                ConductorTip.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return ConductorTip;

        }

        //ASHKTORAB
        public static EConductorTip SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }

            reader.Close();
            //connection.Close();
            return ConductorTip;

        }

        //ASHKTORAB
        public static EConductorTip SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                ConductorTip.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return ConductorTip;

        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];

        }
        //Hatami
        public static DataTable SearchConductorConductorTipConductorTypeX(double crossSectionArea, int TypeCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_SearchForTipType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }


        //SelectAllAndMerge
        public static DataTable DrawSearch()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("LocalDrawSearch\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ConductorTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            //ed.WriteMessage("LocalDrawSearch\n");
            return dsConductor.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_SearchByName", connection);
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
        public static EConductorTip CheckForExist(int _PhaseCount, Guid _PhaseProductXCode,
                                                    int _NeutralCount, Guid _NeutralProductXCode,
                                                    int _NightCount, Guid _NightProductXCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_ConductorTip_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iPhaseCount", _PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", _PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", _NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", _NeutralProductXCode));
            command.Parameters.Add(new SqlParameter("iNightCount", _NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductXCode", _NightProductXCode));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductXCode = new Guid(reader["NightProductXCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                ConductorTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return ConductorTip;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ACCESS Part~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_ConductorTip_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNightProductCode", NightProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iDescription", Description));

            try
            {
                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                con.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_ConductorTip_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNightProductCode", NightProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iDescription", Description));

            try
            {
                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip);
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
                    ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.ConductorTip, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }


        //SendFromAccessToAccess
        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_ConductorTip_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _NewTransaction;

            //ed.WriteMessage("{0}\n{1}\n{2}\n", _PhaseProductCode, _NeutralProductCode, _NightProductCode);
            //ed.WriteMessage("XCode:{0}\niPhaseCount:{1}\niPhaseProductCode:{2}\niNeutralCount{3}\n", XCode, PhaseCount, phaseProductCode, NeutralCount);
            //ed.WriteMessage("iNeutralProductCode:{0}\niNightCount:{1}\niiNightProductCode:{2}\niDescription{3}\n",
            //    NeutralProductCode, NightCount, NightProductCode, Description);
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNightProductCode", NightProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.ConductorTip);
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
                    ed.WriteMessage("Main Parent is ConductorTip: {0}\n", Code);
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.ConductorTip, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess ConductorTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }


        //MOUSAVI->Auto pole installation //StatusReport
        public static EConductorTip AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
            }
            else
            {
                ConductorTip.Code = -1;
            }

            reader.Close();
            connection.Close();
            return ConductorTip;

        }


        public static EConductorTip AccessSelectByCodeForConvertor(int Code,OleDbTransaction _Transaction,OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _Transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
            }
            else
            {
                ConductorTip.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return ConductorTip;

        }



        public static EConductorTip AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("E_ConductorTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
            }
            else
            {
                ConductorTip.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return ConductorTip;

        }


        public static EConductorTip AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();
            connection.Close();
            return ConductorTip;

        }

        //MOUSAVI
        public static EConductorTip AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_ConductorTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EConductorTip ConductorTip = new EConductorTip();
            if (reader.Read())
            {
                ConductorTip.Code = Convert.ToInt32(reader["Code"].ToString());
                ConductorTip.Name = Convert.ToString(reader["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                ConductorTip.Description = reader["Description"].ToString();
                ConductorTip.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                ConductorTip.Code = -1;
            }

            reader.Close();
            return ConductorTip;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ConductorTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ConductorTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        //SelectAllAndMerge
        public static DataTable AccessDrawSearch()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("AccessDrawSearch\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ConductorTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsConductor = new DataSet();
            adapter.Fill(dsConductor);
            //ed.WriteMessage("Finish AccessDrawSearch\n");
            return dsConductor.Tables[0];
        }

        //frmDrawBranch
        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = DrawSearch();
            //ed.WriteMessage("AccTbl :{0}\n", AccTbl.Rows.Count);
            //ed.WriteMessage("sqlTbl :{0}\n", SqlTbl.Rows.Count);
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

            //ed.WriteMessage("FinishMerge\n");
            return MergeTbl;

        }

        //public static bool ShareOnServer(Guid XCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        //    SqlConnection Serverconnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlTransaction Servertransaction;

        //    SqlConnection Localconnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
        //    SqlTransaction Localtransaction;



        //    try
        //    {
        //        Serverconnection.Open();
        //        Servertransaction = Serverconnection.BeginTransaction();

        //        Localconnection.Open();
        //        Localtransaction = Localconnection.BeginTransaction();


        //        EConductorTip _EConductorTip = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            //Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.ConductorTip));
        //            //ed.WriteMessage("\n111\n");

        //            EConductorTip Ap = Atend.Base.Equipment.EConductorTip.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                if (!Atend.Base.Equipment.EConductorTip.ServerDelete(Servertransaction, Serverconnection, Ap.XCode))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }


        //            if (EConductor.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EConductorTip.NeutralProductXCode)
        //                && EConductor.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EConductorTip.NightProductXCode)
        //                && EConductor.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EConductorTip.PhaseProductXCode))
        //            {
        //                _EConductorTip.NeutralProductCode = EConductor.ServerSelectByXCode(Servertransaction, Serverconnection, _EConductorTip.NeutralProductXCode).Code;
        //                _EConductorTip.NightProductCode = EConductor.ServerSelectByXCode(Servertransaction, Serverconnection, _EConductorTip.NightProductXCode).Code;
        //                _EConductorTip.PhaseProductCode = EConductor.ServerSelectByXCode(Servertransaction, Serverconnection, _EConductorTip.PhaseProductXCode).Code;

        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (_EConductorTip.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (_EConductorTip.UpdateX(Localtransaction, Localconnection))
        //                {
        //                    Servertransaction.Commit();
        //                    Serverconnection.Close();

        //                    Localtransaction.Commit();
        //                    Localconnection.Close();
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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.ConductorTip));
        //            //ed.WriteMessage("\n111\n");

        //            EConductorTip Ap = Atend.Base.Equipment.EConductorTip.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EConductorTip.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                //ed.WriteMessage("\n222\n");

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n333\n");

        //                Ap = Atend.Base.Equipment.EConductorTip.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            ///////////////////////////////////////////////

        //            //ed.WriteMessage("\n444\n");

        //            Atend.Base.Equipment.EConductor Cond = Atend.Base.Equipment.EConductor.SelectByCode(Localtransaction, Localconnection, Ap.NeutralProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted = Guid.NewGuid();
        //            if (Cond.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted = Cond.XCode;

        //                if (!Atend.Base.Equipment.EConductor.DeleteX(Localtransaction, Localconnection, Cond.XCode))
        //                {
        //                    //ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n555\n");


        //                Cond.ServerSelectByCode(Ap.NeutralProductCode);
        //            }
        //            else
        //            {
        //                //ed.WriteMessage("\n666\n");

        //                Cond = Atend.Base.Equipment.EConductor.SelectByCode(Ap.NeutralProductCode);
        //                Cond.XCode = Deleted;
        //                Deleted = Guid.Empty;
        //            }

        //            if (!Cond.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.NeutralProductXCode = Cond.XCode;

        //            //////////////////////////////////////////////
        //            //ed.WriteMessage("\n777\n");


        //            Atend.Base.Equipment.EConductor Cond1 = Atend.Base.Equipment.EConductor.SelectByCode(Localtransaction, Localconnection, Ap.NightProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted1 = Guid.NewGuid();
        //            if (Cond1.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted1 = Cond1.XCode;

        //                if (!Atend.Base.Equipment.EConductor.DeleteX(Localtransaction, Localconnection, Cond1.XCode))
        //                {
        //                    //ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                //ed.WriteMessage("\n888\n");

        //                Cond1.ServerSelectByCode(Ap.NightProductCode);
        //            }
        //            else
        //            {
        //                Cond1 = Atend.Base.Equipment.EConductor.SelectByCode(Ap.NightProductCode);
        //                Cond1.XCode = Deleted1;
        //                Deleted1 = Guid.Empty;
        //            }

        //            if (!Cond1.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.NightProductXCode = Cond1.XCode;

        //            //////////////////////////////////////////////

        //            //ed.WriteMessage("\n999\n");

        //            Atend.Base.Equipment.EConductor Cond2 = Atend.Base.Equipment.EConductor.SelectByCode(Localtransaction, Localconnection, Ap.PhaseProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted2 = Guid.NewGuid();
        //            if (Cond2.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted2 = Cond2.XCode;

        //                if (!Atend.Base.Equipment.EConductor.DeleteX(Localtransaction, Localconnection, Cond2.XCode))
        //                {
        //                    //ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                Cond2.ServerSelectByCode(Ap.PhaseProductCode);
        //            }
        //            else
        //            {
        //                Cond2 = Atend.Base.Equipment.EConductor.SelectByCode(Ap.PhaseProductCode);
        //                Cond2.XCode = Deleted2;
        //                Deleted2 = Guid.Empty;
        //            }

        //            if (!Cond2.InsertX(Localtransaction, Localconnection))
        //            {
        //                //ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.PhaseProductXCode = Cond2.XCode;

        //            //////////////////////////////////////////////


        //            if (Ap.InsertX(Localtransaction, Localconnection))
        //            {

        //                //ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //                return true;
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
        //        ed.WriteMessage(string.Format(" ERROR EConductorTip.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}


        //**************************Access To Memory For Calc
        //HATAMI
        public static EConductorTip AccessSelectByCode(DataTable dtCondTip, int Code)
        {
            DataRow[] dr = dtCondTip.Select(string.Format("Code={0}", Code.ToString()));
            EConductorTip ConductorTip = new EConductorTip();
            if (dr.Length != 0)
            {
                ConductorTip.Code = Convert.ToInt32(dr[0]["Code"].ToString());
                ConductorTip.Name = Convert.ToString(dr[0]["Name"].ToString());
                ConductorTip.PhaseCount = Convert.ToInt32(dr[0]["PhaseCount"].ToString());
                ConductorTip.PhaseProductCode = Convert.ToInt32(dr[0]["PhaseProductCode"].ToString());
                ConductorTip.NeutralCount = Convert.ToInt32(dr[0]["NeutralCount"].ToString());
                ConductorTip.NeutralProductCode = Convert.ToInt32(dr[0]["NeutralProductCode"].ToString());
                ConductorTip.NightCount = Convert.ToInt32(dr[0]["NightCount"].ToString());
                ConductorTip.NightProductCode = Convert.ToInt32(dr[0]["NightProductCode"].ToString());
                ConductorTip.Description = dr[0]["Description"].ToString();
            }
            else
            {
                ConductorTip.Code = -1;
            }

            return ConductorTip;

        }

    }
}
