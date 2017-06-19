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
    //ASHKTORAB
    public class EGroundCabelTip
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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
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
                ed.WriteMessage(string.Format("Error EGroundCabelTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
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
                ed.WriteMessage(string.Format("Error EGroundCabelTip.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }
        //HATAMI

        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundCabelTip_Insert", con);
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, LocalTransaction, LocalConnection);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCableTip failed");
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
            SqlCommand insertCommand = new SqlCommand("E_GroundCabelTip_Update", con);
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.GroundCabelTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, LocalTransaction, LocalConnection);
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
                        throw new System.Exception("Delete Operation Failed in GroundCabelTip");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCableTip failed");
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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductCode", PhaseProductCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductCode", NeutralProductCode));
            command.Parameters.Add(new SqlParameter("iDescription", Description));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Update", connection);
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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_DeleteX", connection);
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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Delete", connection);
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

        public static EGroundCabelTip SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }

            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            //EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                //GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                Name = Convert.ToString(reader["Name"].ToString());
                PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                Description = reader["Description"].ToString();
            }

            reader.Close();
            connection.Close();
            //return GroundCabelTip;

        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundCabelTip = new DataSet();
            adapter.Fill(dsGroundCabelTip);
            return dsGroundCabelTip.Tables[0];

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsGroundCabelTip = new DataSet();
            adapter.Fill(dsGroundCabelTip);
            return dsGroundCabelTip.Tables[0];
        }

        //public static DataTable SearchGroundCabelGroundCabelTipGroundCabelMaterialType(double crossSectionArea, int materialCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_SearchForTipMaterial", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iMaterialCode", materialCode));
        //    DataSet dsGroundCabelTip = new DataSet();
        //    adapter.Fill(dsGroundCabelTip);
        //    return dsGroundCabelTip.Tables[0];
        //}

        public static EGroundCabelTip ServerSelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            //connection.Open();
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }

            reader.Close();
            //connection.Close();
            return GroundCabelTip;

        }

        //MEDHAT
        public static EGroundCabelTip ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = ServerTransaction;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();

            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                GroundCabelTip.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : ground cable tip\n");
            }

            reader.Close();
            return GroundCabelTip;
        }

        //public static DataTable DrawSearch(float crossSectionArea, int TypeCode)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCableTip_DrawSearch", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iCrossSectionArea", crossSectionArea));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", TypeCode));
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iVoltage", ));
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);
        //    ed.WriteMessage("bbb\n");
        //    return ds.Tables[0];
        //}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~//
        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("InsertX\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
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
                ed.WriteMessage(string.Format("Error EGroundCabelTip.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("InsertX\n");
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            //Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
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
                ed.WriteMessage(string.Format("Error EGroundCabelTip.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundCabelTip_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iName", Name));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iDescription", Description));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundCabelTip, ServerConnection, ServerTransaction);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundCabelTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCableTip failed");
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {//@
                ed.WriteMessage(string.Format("Error ECODUCTORTIP.localInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_GroundCabelTip_UpdateX", con);
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.GroundCabelTip, ServerTransaction, ServerConnection);
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
                        throw new System.Exception("Delete Operation Failed in GroundCabelTip");
                    }
                }
                ed.WriteMessage("EGroundcableTip.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.GroundCabelTip, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer GroundCableTip failed");
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



        public bool UpdateX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
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
            SqlCommand command = new SqlCommand("E_GroundCabelTip_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iPhaseCount", PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", NeutralProductXCode));
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
        public static bool UpdateXX(SqlTransaction _transaction, SqlConnection _connection, int Code, int NewCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_UpdateXX", connection);
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

        public static bool DeleteX(Guid XCode)
        {
            Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabelTip));

            DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            if (ProdTbl.Rows.Count > 0)
            {
                //return false;
            }

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_DeleteX", connection);
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
            //Atend.Base.Equipment.EContainerPackage containerPackag = Atend.Base.Equipment.EContainerPackage.selectByContainerXCodeAndType(XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabelTip));

            //DataTable ProdTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerPackageCodeForLocal(containerPackag.Code);
            //if (ProdTbl.Rows.Count > 0)
            //{
            //    return false;
            //}

            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_DeleteX", connection);
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

        public static EGroundCabelTip SelectByXCode(Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                //ed.WriteMessage("PhaseProductXCode:{0}\n", GroundCabelTip.PhaseProductXCode);
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                //ed.WriteMessage("NeutralProductXCode:{0}\n", GroundCabelTip.NeutralProductXCode);
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        //MEDHAT
        public static EGroundCabelTip SelectByPhaseProductXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByPhaseProductXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        //MEDHAT
        public static EGroundCabelTip SelectByNeutralProductXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByNeutralProductXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        //ASHKTORAB
        public static EGroundCabelTip SelectByXCode(SqlTransaction _transaction, SqlConnection _connection, Guid XCode)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }

            reader.Close();
            //connection.Close();
            return GroundCabelTip;

        }

        //ASHKTORAB
        public static EGroundCabelTip SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                    GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                    GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                    GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                    GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                    GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                    GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                    GroundCabelTip.Description = reader["Description"].ToString();
                    GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Ejackpanel.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }
            return GroundCabelTip;

        }

        //ASHKTORAB
        public static EGroundCabelTip SelectByCodeForLocal(int Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new SqlParameter("iCode", Code));
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return GroundCabelTip;

        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsGroundCabelTip = new DataSet();
            adapter.Fill(dsGroundCabelTip);
            return dsGroundCabelTip.Tables[0];
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsGroundCabelTip = new DataSet();
            adapter.Fill(dsGroundCabelTip);
            return dsGroundCabelTip.Tables[0];

        }

        public static DataTable DrawSearch()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("LocalDrawSearch\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_GroundCabelTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            ed.WriteMessage("LocalDrawSearch\n");
            return dsGroundCabel.Tables[0];
        }

        //MEDHAT
        public static bool SearchByName(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_SearchByName", connection);
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
        public static EGroundCabelTip CheckForExist(int _PhaseCount, Guid _PhaseProductXCode,
                                                    int _NeutralCount, Guid _NeutralProductXCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_GroundCabelTip_Check", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iPhaseCount", _PhaseCount));
            command.Parameters.Add(new SqlParameter("iPhaseProductXCode", _PhaseProductXCode));
            command.Parameters.Add(new SqlParameter("iNeutralCount", _NeutralCount));
            command.Parameters.Add(new SqlParameter("iNeutralProductXCode", _NeutralProductXCode));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductXCode = new Guid(reader["PhaseProductXCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductXCode = new Guid(reader["NeutralProductXCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabelTip;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ACCESS Part~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_GroundCabelTip_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iName", Name));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseCount", PhaseCount));
            insertCommand.Parameters.Add(new OleDbParameter("iPhaseProductCode", PhaseProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralCount", NeutralCount));
            insertCommand.Parameters.Add(new OleDbParameter("iNeutralProductCode", NeutralProductCode));
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
            OleDbCommand insertCommand = new OleDbCommand("E_GroundCabelTip_Insert", con);
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess GroundCableTip failed");
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

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection,OleDbTransaction _NewTransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_GroundCabelTip_Insert", con);
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.AccessSelectByCode(Convert.ToInt32(dr["Code"].ToString()),_OldConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.AccessInsert(_NewTransaction , _NewConnection))
                            throw new System.Exception("operation failed in airpost");
                    }
                }


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.GroundCabelTip, Code, _OldTransaction, _OldConnection,_NewTransaction,_NewConnection))
                    {
                        throw new System.Exception("SentFromAccessToAccess GroundCableTip failed");
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

        //StatusReport // frmDrawBranchCable
        public static EGroundCabelTip AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        public static EGroundCabelTip AccessSelectByCode(int Code,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            return GroundCabelTip;

        }

        public static EGroundCabelTip AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundCabelTip_Select", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
            }
            else
            {
                GroundCabelTip.Code = -1;
            }
            reader.Close();
            //connection.Close();
            return GroundCabelTip;

        }

        public static EGroundCabelTip AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
            }

            reader.Close();
            connection.Close();
            return GroundCabelTip;

        }

        //MOUSAVI
        public static EGroundCabelTip AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_GroundCabelTip_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            OleDbDataReader reader = command.ExecuteReader();
            EGroundCabelTip GroundCabelTip = new EGroundCabelTip();
            if (reader.Read())
            {
                GroundCabelTip.Code = Convert.ToInt32(reader["Code"].ToString());
                GroundCabelTip.Name = Convert.ToString(reader["Name"].ToString());
                GroundCabelTip.PhaseCount = Convert.ToInt32(reader["PhaseCount"].ToString());
                GroundCabelTip.PhaseProductCode = Convert.ToInt32(reader["PhaseProductCode"].ToString());
                GroundCabelTip.NeutralCount = Convert.ToInt32(reader["NeutralCount"].ToString());
                GroundCabelTip.NeutralProductCode = Convert.ToInt32(reader["NeutralProductCode"].ToString());
                GroundCabelTip.Description = reader["Description"].ToString();
                GroundCabelTip.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                GroundCabelTip.Code = -1;
            }

            reader.Close();
            return GroundCabelTip;

        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundCabelTip_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsGroundCabelTip = new DataSet();
            adapter.Fill(dsGroundCabelTip);
            return dsGroundCabelTip.Tables[0];
        }

        public static DataTable AccessDrawSearch()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("AccessDrawSearch\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_GroundCabelTip_DrawSearch", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsGroundCabel = new DataSet();
            adapter.Fill(dsGroundCabel);
            ed.WriteMessage("Finish AccessDrawSearch\n");
            return dsGroundCabel.Tables[0];
        }

        public static DataTable SelectAllAndMerge()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable AccTbl = AccessDrawSearch();
            DataTable SqlTbl = DrawSearch();
            ed.WriteMessage("AccTbl :{0}\n", AccTbl.Rows.Count);
            ed.WriteMessage("sqlTbl :{0}\n", SqlTbl.Rows.Count);
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

            ed.WriteMessage("FinishMerge\n");
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


        //        EGroundCabelTip _EGroundCabelTip = SelectByXCode(Localtransaction, Localconnection, XCode);

        //        bool canCommitTransaction = true;
        //        int Counter = 0;

        //        try
        //        {


        //            //Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.SelectByContainerXCodeAndType(Localtransaction , Localconnection, XCode, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabelTip));
        //            ed.WriteMessage("\n111\n");
        //            ed.WriteMessage("\naaaaaaaaaaaaaaaaaaa\n");

        //            EGroundCabelTip Ap = Atend.Base.Equipment.EGroundCabelTip.ServerSelectByXCode(Servertransaction, Serverconnection, XCode);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                if (!Atend.Base.Equipment.EGroundCabelTip.ServerDelete(Servertransaction, Serverconnection, Ap.XCode))
        //                {
        //                    Servertransaction.Rollback();
        //                    Serverconnection.Close();

        //                    Localtransaction.Rollback();
        //                    Localconnection.Close();
        //                    return false;
        //                }
        //            }


        //            if (EGroundCabel.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EGroundCabelTip.NeutralProductXCode)
        //                && EGroundCabel.ShareOnServer(Servertransaction, Serverconnection, Localtransaction, Localconnection, _EGroundCabelTip.PhaseProductXCode))
        //            {
        //                _EGroundCabelTip.NeutralProductCode = EGroundCabel.ServerSelectByXCode(Servertransaction, Serverconnection, _EGroundCabelTip.NeutralProductXCode).Code;
        //                _EGroundCabelTip.PhaseProductCode = EGroundCabel.ServerSelectByXCode(Servertransaction, Serverconnection, _EGroundCabelTip.PhaseProductXCode).Code;

        //            }
        //            else
        //            {
        //                Servertransaction.Rollback();
        //                Serverconnection.Close();
        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            if (_EGroundCabelTip.Insert(Servertransaction, Serverconnection))
        //            {
        //                if (_EGroundCabelTip.UpdateX(Localtransaction, Localconnection))
        //                {
        //                    Servertransaction.Commit();
        //                    Serverconnection.Close();

        //                    Localtransaction.Commit();
        //                    Localconnection.Close();
        //                    return true;
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
        //        ed.WriteMessage(string.Format(" ERROR EGroundCabelTip.ShareOnServer {0}\n", ex1.Message));

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

        //            Atend.Base.Equipment.EContainerPackage containerPackage = EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabelTip));
        //            ed.WriteMessage("\n111\n");

        //            EGroundCabelTip Ap = Atend.Base.Equipment.EGroundCabelTip.SelectByCode(Localtransaction, Localconnection, Code);

        //            if (Ap.XCode != Guid.Empty)
        //            {
        //                DeletedXCode = Ap.XCode;
        //                if (!Atend.Base.Equipment.EGroundCabelTip.DeleteX(Localtransaction, Localconnection, Ap.XCode))
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

        //                Ap = Atend.Base.Equipment.EGroundCabelTip.SelectByCode(Code);
        //                Ap.XCode = DeletedXCode;
        //            }

        //            ///////////////////////////////////////////////

        //            ed.WriteMessage("\n444\n");

        //            Atend.Base.Equipment.EGroundCabel Cond = Atend.Base.Equipment.EGroundCabel.SelectByCode(Localtransaction, Localconnection, Ap.NeutralProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted = Guid.NewGuid();
        //            if (Cond.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted = Cond.XCode;

        //                if (!Atend.Base.Equipment.EGroundCabel.DeleteX(Localtransaction, Localconnection, Cond.XCode))
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

        //                Cond = Atend.Base.Equipment.EGroundCabel.SelectByCode(Ap.NeutralProductCode);
        //                Cond.XCode = Deleted;
        //                Deleted = Guid.Empty;
        //            }

        //            Cond.OperationList = new ArrayList();
        //            DataTable OperationTbl = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));

        //            Cond.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl, Cond.XCode);

        //            if (!Cond.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.NeutralProductXCode = Cond.XCode;

        //            //////////////////////////////////////////////
        //            ed.WriteMessage("\n777\n");



        //            //////////////////////////////////////////////

        //            ed.WriteMessage("\n999\n");

        //            Atend.Base.Equipment.EGroundCabel Cond2 = Atend.Base.Equipment.EGroundCabel.SelectByCode(Localtransaction, Localconnection, Ap.PhaseProductCode);
        //            //ed.WriteMessage("\n XCode = " + PHP.XCode.ToString() + "\n");
        //            Guid Deleted2 = Guid.NewGuid();
        //            if (Cond2.XCode != Guid.Empty)
        //            {
        //                //ed.WriteMessage("\n IN PHUSEPOLEEEEEE\n");
        //                Deleted2 = Cond2.XCode;

        //                if (!Atend.Base.Equipment.EGroundCabel.DeleteX(Localtransaction, Localconnection, Cond2.XCode))
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
        //                Cond2 = Atend.Base.Equipment.EGroundCabel.SelectByCode(Ap.PhaseProductCode);
        //                Cond2.XCode = Deleted2;
        //                Deleted2 = Guid.Empty;
        //            }

        //            Cond2.OperationList = new ArrayList();
        //            DataTable OperationTbl2 = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.GroundCabel));

        //            Cond2.OperationList = Atend.Base.Design.NodeTransaction.GetOperationListForServer(OperationTbl2, Cond2.XCode);


        //            if (!Cond2.InsertX(Localtransaction, Localconnection))
        //            {
        //                ed.WriteMessage("\n114\n");

        //                Localtransaction.Rollback();
        //                Localconnection.Close();
        //                return false;
        //            }

        //            Ap.PhaseProductXCode = Cond2.XCode;

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
        //        ed.WriteMessage(string.Format(" ERROR EGroundCabelTip.GetFromServer {0}\n", ex1.Message));

        //        Localconnection.Close();
        //        return false;
        //    }

        //    return true;
        //}
    }
}
