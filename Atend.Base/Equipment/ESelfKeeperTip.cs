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
    public class ESelfKeeperTip
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


        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int crossSection;
        public int CrossSection
        {
            get { return crossSection; }
            set { crossSection = value; }
        }

        private int crossSectionCount;
        public int CrossSectionCount
        {
            get { return crossSectionCount; }
            set { crossSectionCount = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        private Guid phaseProductxCode;
        public Guid PhaseProductxCode
        {
            get { return phaseProductxCode; }
            set { phaseProductxCode = value; }
        }

        private Guid nightProductxCode;
        public Guid NightProductxCode
        {
            get { return nightProductxCode; }
            set { nightProductxCode = value; }
        }

        private Guid neutralProductxCode;
        public Guid NeutralProductxCode
        {
            get { return neutralProductxCode; }
            set { neutralProductxCode = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~Server PArt~~~~~~~~~~~~~~~~~~~~//
        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            try
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //Code = 0;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", phaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                //connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                //connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //HATAMI
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.ServerInsert : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Update", connection);
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
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In E_SelfKeeperTip_Update: =" + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public static bool Update(SqlTransaction _transaction, SqlConnection _connection, int Code, int NewCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Update", connection);
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
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In E_SelfKeeperTip_Update: =" + ex.Message + "\n");
                //connection.Close();
                return false;
            }
        }

        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Delete", connection);
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

        public static bool ServerDelete(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_DeleteX", connection);
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

        public static ESelfKeeperTip SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
            }

            reader.Close();
            connection.Close();
            return SelfKeeperTip;

        }

        //ASHKTORAB
        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                // SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                Name = Convert.ToString(reader["Name"].ToString());
                PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                Description = reader["Description"].ToString();
                CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
            }

            reader.Close();
            connection.Close();
            //return SelfKeeperTip;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        public static DataTable SearchSelfKeeperSelfKeeperTipSelfKeeperMaterialType(double crossSectionArea, int materialCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_SearchForTipMaterial", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iMaterialCode", materialCode));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }

        public static ESelfKeeperTip ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());

                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());

                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());

                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }

            reader.Close();
            //connection.Close();
            return SelfKeeperTip;

        }

        //MEDHAT
        public static ESelfKeeperTip ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());

                //SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());

                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                //SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());

                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                //SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());

                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);

            }
            else
            {
                SelfKeeperTip.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : selfkeeper tip\n");
            }
            reader.Close();
            return SelfKeeperTip;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", phaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", phaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            //XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
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
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", PhaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {

                command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(command.ExecuteScalar());
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", PhaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {

                //command.ExecuteNonQuery();
                //command.Parameters.Clear();
                //command.CommandType = CommandType.Text;
                //command.CommandText = "SELECT @@IDENTITY";
                command.ExecuteNonQuery();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.LocalUpdate : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", PhaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In E_SelfKeeperTip_UpdateX: " + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }

        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", PhaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", CrossSectionCount));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In E_SelfKeeperTip_UpdateX: " + ex.Message + "\n");
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid Code)
        {
            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByXCodeType(Code, (int)Atend.Control.Enum.ProductType.SelfKeeperTip);
            if (ProdTbl.Rows.Count > 0)
            {
                return false;
            }


            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
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
        public static bool DeleteX(SqlTransaction _transaction, SqlConnection _connection, Guid Code)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_DeleteX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", Code));
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

        //MOUSAVI //SentFromLocalToAccess
        public static ESelfKeeperTip SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }

            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            connection.Close();
            return SelfKeeperTip;

        }

        //MEDHAT
        public static ESelfKeeperTip SelectByPhaseProductxCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByPhaseProductxCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            connection.Close();
            return SelfKeeperTip;
        }

        //MEDHAT
        public static ESelfKeeperTip SelectByNeutralProductxCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByNeutralProductxCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            connection.Close();
            return SelfKeeperTip;
        }

        //MEDHAT
        public static ESelfKeeperTip SelectByNightProductxCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByNightProductxCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            connection.Close();
            return SelfKeeperTip;
        }

        //ASHKTORAB
        public static ESelfKeeperTip SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                //SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());

                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                //SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());

                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                //SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());

                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }

            reader.Close();
            //connection.Close();
            return SelfKeeperTip;

        }


        //ASHKTORAB
        public static ESelfKeeperTip SelectByXCodeForDesign(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                //SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());

                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                //SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());

                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                //SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());

                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return SelfKeeperTip;

        }

        //ASHKTORAB
        public static ESelfKeeperTip SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                //SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());

                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                //SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());

                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                //SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());

                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return SelfKeeperTip;

        }

        public static DataTable DrawSearchX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        //HATAMI
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];

        }

        //HATAMI
        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }


        //HATAMI
        public static DataTable SearchConductorSelfKeeperTipConductorTypeX(double crossSectionArea, int TypeCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_SelfKeeperTip_SearchForTipType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iTypeCode", TypeCode));
            DataSet dsConductorTip = new DataSet();
            adapter.Fill(dsConductorTip);
            return dsConductorTip.Tables[0];
        }


        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_SearchByName", connection);
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
        public static ESelfKeeperTip CheckForExist(int _PhaseCount, Guid _PhaseProductxCode,
                                                   int _NeutralCount, Guid _NeutralProductxCode,
                                                   int _NightCount, Guid _NightProductxCode,
                                                   int _CrossSectionCount, int _CrossSection)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_SelfKeeperTip_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iPhaseCount", _PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductxCode", _PhaseProductxCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", _NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductxCode", _NeutralProductxCode));
            command.Parameters.Add(new SqlParameter("iNightCount", _NightCount));
            command.Parameters.Add(new SqlParameter("iNightProductxCode", _NightProductxCode));
            command.Parameters.Add(new SqlParameter("iCrossSectionCount", _CrossSectionCount));
            command.Parameters.Add(new SqlParameter("iCrossSection", _CrossSection));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductxCode = new Guid(reader["PhaseProductxCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductxCode = new Guid(reader["NeutralProductxCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductxCode = new Guid(reader["NightProductxCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return SelfKeeperTip;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new OleDbParameter("iPhaseProductxCode", phaseProductxCode));
            command.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new OleDbParameter("iNeutralProductxCode", NeutralProductxCode));
            command.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            command.Parameters.Add(new OleDbParameter("iNightProductxCode", NightProductxCode));
            command.Parameters.Add(new OleDbParameter("iDescription", Description));
            command.Parameters.Add(new OleDbParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new OleDbParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));

            try
            {

                connection.Open();
                Code = Convert.ToInt32(command.ExecuteNonQuery());
                connection.Close();
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //MOUSAVI //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            command.Parameters.Add(new OleDbParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new OleDbParameter("iDescription", Description));
            command.Parameters.Add(new OleDbParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new OleDbParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new OleDbParameter("iNightCount", NightCount));
            command.Parameters.Add(new OleDbParameter("iNightProductCode", NightProductCode));
            command.Parameters.Add(new OleDbParameter("iDescription", Description));
            command.Parameters.Add(new OleDbParameter("iCrossSection", CrossSection));
            command.Parameters.Add(new OleDbParameter("iCrossSectionCount", CrossSectionCount));
            command.Parameters.Add(new OleDbParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            int OldCode = Code;

            try
            {

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());
                return true;

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ESelfKeeperTip.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //MOUSAVI->auto poleinstallation //StatusReport
        public static ESelfKeeperTip AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return SelfKeeperTip;

        }

        public static ESelfKeeperTip AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            reader.Close();
            return SelfKeeperTip;
        }

        public static ESelfKeeperTip AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return SelfKeeperTip;

        }

        public static ESelfKeeperTip AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (reader.Read())
            {
                SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
                SelfKeeperTip.Description = reader["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
                SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return SelfKeeperTip;

        }

        //MOUSAVI //SentFromLocalToAccess //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ESelfKeeperTip AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_SelfKeeperTip_SelectByXCode", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Transaction = _transaction;
        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
        //    if (reader.Read())
        //    {
        //        SelfKeeperTip.Code = Convert.ToInt32(reader["Code"].ToString());
        //        SelfKeeperTip.Name = Convert.ToString(reader["Name"].ToString());
        //        SelfKeeperTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
        //        SelfKeeperTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
        //        SelfKeeperTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
        //        SelfKeeperTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
        //        SelfKeeperTip.NightCount = Convert.ToInt32(reader["NightCount"].ToString());
        //        SelfKeeperTip.NightProductCode = Convert.ToInt32(reader["NightProductCode"].ToString());
        //        SelfKeeperTip.Description = reader["Description"].ToString();
        //        SelfKeeperTip.CrossSection = Convert.ToInt32(reader["CrossSection"].ToString());
        //        SelfKeeperTip.CrossSectionCount = Convert.ToInt32(reader["CrossSectionCount"].ToString());
        //        SelfKeeperTip.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        SelfKeeperTip.Code = -1;
        //    }
        //    reader.Close();
        //    return SelfKeeperTip;

        //}

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_SelfKeeperTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsSKT = new DataSet();
            adapter.Fill(dsSKT);
            return dsSKT.Tables[0];

        }

        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_SelfKeeperTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsSKT = new DataSet();
            adapter.Fill(dsSKT);
            return dsSKT.Tables[0];

        }

        //Hatami
        public static DataTable AccessDrawSearch()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_SelfKeeperTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new OleDbParameter("iName", Name));
            DataSet dskhazantip = new DataSet();
            adapter.Fill(dskhazantip);

            return dskhazantip.Tables[0];
        }

        public static DataTable SelectAllAndMerge()
        {

            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = DrawSearchX();

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

        //        ESelfKeeperTip _ESelfKeeperTip = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction, Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeperTip));
        //            ed.WriteMessage("\n111\n");

        //            ESelfKeeperTip Ap = Atend.Base.Equipment.ESelfKeeperTip.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedCode = Ap.Code;
        //                if (!Atend.Base.Equipment.ESelfKeeperTip.ServerDelete(Servertransaction, Serverconnection, Ap.XCode))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //            }


        //            if (ESelfKeeper.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _ESelfKeeperTip.NeutralProductxCode)
        //                && ESelfKeeper.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _ESelfKeeperTip.NightProductxCode)
        //                && ESelfKeeper.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _ESelfKeeperTip.PhaseProductxCode))
        //            {
        //                _ESelfKeeperTip.NeutralProductCode = ESelfKeeper.ServerSelectByXCode(Servertransaction, Serverconnection, _ESelfKeeperTip.NeutralProductxCode).Code;
        //                _ESelfKeeperTip.NightProductCode = ESelfKeeper.ServerSelectByXCode(Servertransaction, Serverconnection, _ESelfKeeperTip.NightProductxCode).Code;
        //                _ESelfKeeperTip.PhaseProductCode = ESelfKeeper.ServerSelectByXCode(Servertransaction, Serverconnection, _ESelfKeeperTip.PhaseProductxCode).Code;

        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (_ESelfKeeperTip.Insert(Servertransaction, Serverconnection))
        //            {

        //                if (!_ESelfKeeperTip.UpdateX(Localtransaction, Localconnection))
        //                {
        //                    ed.WriteMessage("\n115\n");

        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                ed.WriteMessage("\n112\n");

        //                //Servertransaction.Commit();
        //                //Serverconnection.Close();

        //                //Localtransaction.Commit();
        //                //Localconnection.Close();

        //                //return true;


        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (Atend.Base.Design.NodeTransaction.SubProducts(DeletedCode, _ESelfKeeperTip.Code, XCode, containerPackage.Code, (int)Atend.Control.Enum.ProductType.SelfKeeperTip, Servertransaction, Serverconnection, Localtransaction, Localconnection))
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



        //            ed.WriteMessage("\n112\n");

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
        //        ed.WriteMessage(string.Format(" ERROR ESelfKeeperTip.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.SelfKeeperTip));
        //            ed.WriteMessage("\n111\n");

        //            ESelfKeeperTip Ap = Atend.Base.Equipment.ESelfKeeperTip.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.ESelfKeeperTip.DeleteX(Localtransaction, Localconnection, Ap.XCode))
        //                {
        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                }
        //                ed.WriteMessage("\n222\n");

        //                Ap.ServerSelectByCode(Code);
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n333\n");

        //                Ap = Atend.Base.Equipment.ESelfKeeperTip.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            ///////////////////////////////////////////////

        //            ed.WriteMessage("\n444\n");

        //            Atend.Base.Equipment.ESelfKeeper Cond = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Localtransaction, Localconnection, Ap.NeutralProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted = Guid.NewGuid();
        //            if (Cond.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted = Cond.XCode;

        //                if (!Atend.Base.Equipment.ESelfKeeper.DeleteX(Localtransaction, Localconnection, Cond.XCode))
        //                {
        //                    ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                ed.WriteMessage("\n555\n");


        //                Cond.ServerSelectByCode(Ap.NeutralProductCode);
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n666\n");

        //                Cond = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Ap.NeutralProductCode);
        //                Cond.XCode = Deleted;
        //                Deleted = Guid.Empty;
        //            }

        //            if (!Cond.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.NeutralProductxCode = Cond.XCode;

        //            //////////////////////////////////////////////
        //            ed.WriteMessage("\n777\n");


        //            Atend.Base.Equipment.ESelfKeeper Cond1 = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Localtransaction, Localconnection, Ap.NightProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted1 = Guid.NewGuid();
        //            if (Cond1.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted1 = Cond1.XCode;

        //                if (!Atend.Base.Equipment.ESelfKeeper.DeleteX(Localtransaction, Localconnection, Cond1.XCode))
        //                {
        //                    ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //                ed.WriteMessage("\n888\n");

        //                Cond1.ServerSelectByCode(Ap.NightProductCode);
        //            }
        //            else
        //            {
        //                Cond1 = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Ap.NightProductCode);
        //                Cond1.XCode = Deleted1;
        //                Deleted1 = Guid.Empty;
        //            }

        //            if (!Cond1.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.NightProductxCode = Cond1.XCode;

        //            //////////////////////////////////////////////

        //            ed.WriteMessage("\n999\n");

        //            Atend.Base.Equipment.ESelfKeeper Cond2 = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Localtransaction, Localconnection, Ap.PhaseProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted2 = Guid.NewGuid();
        //            if (Cond2.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted2 = Cond2.XCode;

        //                if (!Atend.Base.Equipment.ESelfKeeper.DeleteX(Localtransaction, Localconnection, Cond2.XCode))
        //                {
        //                    ed.WriteMessage("\n114\n");

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }

        //                Cond2.ServerSelectByCode(Ap.PhaseProductCode);
        //            }
        //            else
        //            {
        //                Cond2 = Atend.Base.Equipment.ESelfKeeper.SelectByCode(Ap.PhaseProductCode);
        //                Cond2.XCode = Deleted2;
        //                Deleted2 = Guid.Empty;
        //            }

        //            if (!Cond2.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.PhaseProductxCode = Cond2.XCode;

        //            //////////////////////////////////////////////


        //            if (Ap.InsertX(Localtransaction, Localconnection))
        //            {

        //                ed.WriteMessage("\n113\n");

        //                Localtransaction.Commit();
        //                Localconnection.Close();
        //                return true;
        //            }
        //            else
        //            {
        //                ed.WriteMessage("\n114\n");

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
        //        ed.WriteMessage(string.Format(" ERROR ESelfKeeperTip.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}



        //************************************Access To Memory For Calc
        public static ESelfKeeperTip AccessSelectByCode(DataTable dtSelf, int Code)
        {
            DataRow[] dr = dtSelf.Select(string.Format("Code={0}", Code));
            ESelfKeeperTip SelfKeeperTip = new ESelfKeeperTip();
            if (dr.Length != 0)
            {
                SelfKeeperTip.Code = Convert.ToInt32(dr[0]["Code"].ToString());
                SelfKeeperTip.Name = Convert.ToString(dr[0]["Name"].ToString());
                SelfKeeperTip.PhaseCount = Convert.ToInt32(dr[0]["PhaseCount"].ToString());
                SelfKeeperTip.PhaseProductCode = Convert.ToInt32(dr[0]["PhaseProductCode"].ToString());
                SelfKeeperTip.NeutralCount = Convert.ToInt32(dr[0]["NeutralCount"].ToString());
                SelfKeeperTip.NeutralProductCode = Convert.ToInt32(dr[0]["NeutralProductCode"].ToString());
                SelfKeeperTip.NightCount = Convert.ToInt32(dr[0]["NightCount"].ToString());
                SelfKeeperTip.NightProductCode = Convert.ToInt32(dr[0]["NightProductCode"].ToString());
                SelfKeeperTip.Description = dr[0]["Description"].ToString();
                SelfKeeperTip.CrossSection = Convert.ToInt32(dr[0]["CrossSection"].ToString());
                SelfKeeperTip.CrossSectionCount = Convert.ToInt32(dr[0]["CrossSectionCount"].ToString());
            }
            else
            {
                SelfKeeperTip.Code = -1;
            }
            return SelfKeeperTip;

        }

    }
}
