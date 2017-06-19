using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Equipment
{
    public class ERamp
    {

        public ERamp()
        { }

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

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("E_Ramp_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.Insert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }


        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Ramp_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Ramp, LocalTransaction, LocalConnection);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                        SelectedOperation.ProductCode = Code;
                        if (!SelectedOperation.Insert(_transaction, _connection))
                            throw new System.Exception("operation failed in RAMp");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Ramp, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.ServerInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Ramp_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            //command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.Delete(_transaction, _connection, Code, (int)Atend.Control.Enum.ProductType.Ramp))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Ramp, LocalTransaction, LocalConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()), LocalTransaction, LocalConnection);
                            SelectedOperation.ProductCode = Code;
                            if (!SelectedOperation.Insert(_transaction, _connection))
                                throw new System.Exception("operation failed in RAMp");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Ramp");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToServer(XCode, (int)Atend.Control.Enum.ProductType.Ramp, Code, _transaction, _connection, LocalTransaction, LocalConnection))
                    {
                        throw new System.Exception("SentFromLocalToServer Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Ramp_Select", Connection);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsRamp = new DataSet();
            adapter.Fill(dsRamp);
            return dsRamp.Tables[0];
        }

        //MEDHAT //ShareOnServer
        public static ERamp ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("E_Ramp_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            ERamp ramp = new ERamp();

            try
            {
                command.Transaction = ServerTransaction;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ramp.Code = Convert.ToInt16(reader["Code"].ToString());

                    ramp.Name = reader["Name"].ToString();
                    ramp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    ramp.XCode = new Guid(reader["XCode"].ToString());
                    ramp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                }
                else
                {
                    ramp.Code = -1;
                    ed.WriteMessage("ServerSelectByCode found no row in : ramp\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ed.WriteMessage("Error ERamp.In SelectByCode.TransAction:{0}\n", ex.Message);
            }
            return ramp;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //Ribbon->btnTransferBProduct
        public bool InsertX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Ramp_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.Insert : {0} \n", ex1.Message));
                string s = ex1.Message;
                connection.Close();
                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Ramp_InsertX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            XCode = Guid.NewGuid();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));

            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByCodeType(Code, (int)Atend.Control.Enum.ProductType.Ramp, ServerConnection, ServerTransaction);
                    foreach (DataRow dr in operations.Rows)
                    {
                        Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                        SelectedOperation.XCode = XCode;
                        if (!SelectedOperation.InsertX(_transaction, _connection))
                            throw new System.Exception("operation failed in RAMp");
                    }
                }

                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Ramp, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.LocalInsertX : {0} \n", ex1.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Ramp_UpdateX", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                    if (Atend.Base.Equipment.EOperation.DeleteX(_transaction, _connection, XCode, (int)Atend.Control.Enum.ProductType.Ramp))
                    {
                        DataTable operations = Atend.Base.Equipment.EOperation.SelectByProductCodeType(Code, (int)Atend.Control.Enum.ProductType.Ramp, ServerTransaction, ServerConnection);
                        foreach (DataRow dr in operations.Rows)
                        {
                            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForServer(new Guid(dr["Code"].ToString()), ServerConnection, ServerTransaction);
                            SelectedOperation.XCode = XCode;
                            if (!SelectedOperation.InsertX(_transaction, _connection))
                                throw new System.Exception("operation failed in RAMp");
                        }
                    }
                    else
                    {
                        throw new System.Exception("Deleted Operation Failed in Ramp");
                    }
                }
                ed.WriteMessage("ERamp.Operation passed \n");


                if (BringSubEquips)
                {
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromServerToLocal(Code, (int)Atend.Control.Enum.ProductType.Ramp, XCode, ServerTransaction, ServerConnection, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToServer Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ERamp.ServerUpdate : {0} \n", ex1.Message));
                return false;
            }
        }


        //Ribbon->btnTransferBProduct
        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Start UU\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Ramp_UpdateX", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
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
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.Update : {0} \n", ex1.Message));
                string s = ex1.Message;
                connection.Close();
                return false;
            }
        }


        //ShareOnServer
        public bool UpdateX(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Start UU\n");
            SqlConnection connection = _connection;
            SqlCommand command = new SqlCommand("E_Ramp_UpdateX", connection);
            command.Transaction = _transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                ed.WriteMessage("UpdateXRamp Succeed,XCode={0},Code={1}\n",XCode,Code);
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                //Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.Update : {0} \n", ex1.Message));
                string s = ex1.Message;
                //connection.Close();
                return false;
            }
        }

        public static bool DeleteX(Guid XCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_Ramp_DeleteX", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            try
            {
                connection.Open();
                Command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error in Delete Ramp={0}\n", ex.Message));
                connection.Close();
                return false;
            }
        }

        public static DataTable SearchLocal(string Name)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Ramp_Search", Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));

            DataSet dsInsulator = new DataSet();
            adapter.Fill(dsInsulator);

            return dsInsulator.Tables[0];
        }

        //SentFromLocalToAccess
        public static ERamp SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Ramp_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iXCode", XCode));
            SqlDataReader reader = command.ExecuteReader();
            ERamp ramp = new ERamp();
            if (reader.Read())
            {
                ramp.Code = Convert.ToInt16(reader["Code"].ToString());

                ramp.Name = reader["Name"].ToString();
                ramp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                ramp.XCode = new Guid(reader["XCode"].ToString());
                ramp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
            }
            else
            {
                ramp.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return ramp;
        }

        //ASHKTORAB //ShareOnServer
        public static ERamp SelectByXCodeForDesign(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Ramp_SelectByXCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iXCode", XCode));

            ERamp ramp = new ERamp();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ramp.Code = Convert.ToInt16(reader["Code"].ToString());

                    ramp.Name = reader["Name"].ToString();
                    ramp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    ramp.XCode = new Guid(reader["XCode"].ToString());
                    ramp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                }
                else
                {
                    ramp.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ERamp.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return ramp;
        }

        public static ERamp SelectByCodeForLocal(int Code, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("E_Ramp_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));

            ERamp ramp = new ERamp();

            try
            {
                command.Transaction = LocalTransaction;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ramp.Code = Convert.ToInt16(reader["Code"].ToString());

                    ramp.Name = reader["Name"].ToString();
                    ramp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                    ramp.XCode = new Guid(reader["XCode"].ToString());
                    ramp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());
                }
                else
                {
                    ramp.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error ERamp.In SelectByXCode.TransAction:{0}\n", ex.Message);
            }

            return ramp;
        }

        //Ribbon->btnTransferBProduct
        public static ERamp SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("E_Ramp_SelectByProductCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            SqlDataReader reader = command.ExecuteReader();
            ERamp ramp = new ERamp();
            if (reader.Read())
            {
                ramp.Code = Convert.ToInt16(reader["Code"].ToString());

                ramp.Name = reader["Name"].ToString();
                ramp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                ramp.XCode = new Guid(reader["XCode"].ToString());
                ramp.IsDefault = Convert.ToBoolean(reader["IsDefault"].ToString());

            }
            else
            {
                ramp.Code = -1;
            }
            reader.Close();
            connection.Close();

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage(string.Format(" Values: {0} , {1} , {2}  \n", bus.ProductCode, bus.Size, bus, bus.Type));



            return ramp;
        }

        //Ribbon->btnTransferBProduct
        public static bool GetFromBProductLocal()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DataTable bp = Atend.Base.Base.BProduct.SelectByTypeX(Convert.ToInt32(Atend.Control.Enum.ProductType.Ramp));
            ed.WriteMessage("bp.Count={0}\n", bp.Rows.Count);
            foreach (DataRow dr in bp.Rows)
            {
                ERamp ramp = Atend.Base.Equipment.ERamp.SelectByProductCode(Convert.ToInt32(dr["ID"].ToString()));
                if (ramp.Code != -1)
                {
                    ed.WriteMessage("ramp.Name={0}\n", ramp.Name);
                    ramp.Name = dr["Name"].ToString();
                    ed.WriteMessage("ProductCode={0}\n", dr["ID"].ToString());
                    ramp.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    if (!ramp.UpdateX())
                        return false;

                }
                else
                {
                    ed.WriteMessage("InsertRamp\n");
                    ramp.ProductCode = Convert.ToInt32(dr["ID"].ToString());
                    ramp.Name = dr["Name"].ToString();
                    if (!ramp.InsertX())
                        return false;
                }
            }
            return true;
        }

        //MOUSAVI->BindDataToTreeViewX
        public static DataTable SelectAllX()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Ramp_SelectAll", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsRamp = new DataSet();
            adapter.Fill(dsRamp);
            return dsRamp.Tables[0];
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Ramp_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iName", Name));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            try
            {
                connection.Open();
                Code = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error EPT.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
        }

        //SentFromLocalToAccess
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Ramp_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            command.Parameters.Add(new OleDbParameter("iName", Name));
            try
            {
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(command.ExecuteScalar());

                if (BringOperation)
                {
                    DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, (int)Atend.Control.Enum.ProductType.Ramp);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, (int)Atend.Control.Enum.ProductType.Ramp, Code, _transaction, _connection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _OldTransaction, OleDbConnection _OldConnection, OleDbTransaction _NewTransaction, OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            OleDbConnection connection = _NewConnection;
            OleDbCommand command = new OleDbCommand("E_Ramp_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _NewTransaction;

            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));

            command.Parameters.Add(new OleDbParameter("iName", Name));
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
                    DataTable operations = Atend.Base.Equipment.EOperation.AccessSelectByProductCodeType(OldCode, (int)Atend.Control.Enum.ProductType.Ramp);
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
                    if (!Atend.Base.Equipment.EContainerPackage.SentFromAccessToAccess(OldCode, (int)Atend.Control.Enum.ProductType.Ramp, Code, _OldTransaction, _OldConnection, _NewTransaction, _NewConnection))
                    {
                        throw new System.Exception("SentFromLocalToAccess Ramp failed");
                    }
                }

                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("Error ERamp.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //StatusReport
        public static ERamp AccessSelectByCode(int Code)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Ramp_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ERamp _ERamp = new ERamp();
            if (reader.Read())
            {
                _ERamp.Code = Convert.ToInt32(reader["Code"].ToString());
                _ERamp.Name = Convert.ToString(reader["Name"].ToString());
                _ERamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                _ERamp.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                _ERamp.Code = -1;
            }

            reader.Close();
            connection.Close();
            return _ERamp;

        }

        public static ERamp AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection; //new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Ramp_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ERamp _ERamp = new ERamp();
            if (reader.Read())
            {
                _ERamp.Code = Convert.ToInt32(reader["Code"].ToString());
                _ERamp.Name = Convert.ToString(reader["Name"].ToString());
                _ERamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                _ERamp.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                _ERamp.Code = -1;
            }

            reader.Close();
            return _ERamp;

        }

        public static ERamp AccessSelectByCodeForConvertor(int Code,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("E_Ramp_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            //connection.Open();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            OleDbDataReader reader = command.ExecuteReader();
            ERamp _ERamp = new ERamp();
            if (reader.Read())
            {
                _ERamp.Code = Convert.ToInt32(reader["Code"].ToString());
                _ERamp.Name = Convert.ToString(reader["Name"].ToString());
                _ERamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                _ERamp.XCode = new Guid(reader["XCode"].ToString());
            }
            else
            {
                _ERamp.Code = -1;
            }

            reader.Close();
            //connection.Close();
            return _ERamp;

        }

        //SentFromLocalToAccess
        public static ERamp AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("E_Ramp_SelectByXCode", Connection);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            ERamp _ERamp = new ERamp();
            if (reader.Read())
            {
                _ERamp.Code = Convert.ToInt32(reader["Code"].ToString());
                _ERamp.Name = reader["Name"].ToString();
                _ERamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                _ERamp.XCode = new Guid(reader["XCode"].ToString());
            }
            reader.Close();
            Connection.Close();
            return _ERamp;
        }

        //XCODE IS NEVER UNIQUE IN THIS QUERY
        //public static ERamp AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    OleDbConnection connection = _connection;
        //    OleDbCommand command = new OleDbCommand("E_Ramp_SelectByXCode", _connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.Add(new OleDbParameter("iXCode", XCode));
        //    command.Transaction = _transaction;
        //    OleDbDataReader reader = command.ExecuteReader();
        //    ERamp _ERamp = new ERamp();
        //    if (reader.Read())
        //    {
        //        _ERamp.Code = Convert.ToInt32(reader["Code"].ToString());
        //        _ERamp.Name = reader["Name"].ToString();
        //        _ERamp.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
        //        _ERamp.XCode = new Guid(reader["XCode"].ToString());
        //    }
        //    else
        //    {
        //        _ERamp.Code = -1;
        //    }
        //    reader.Close();
        //    return _ERamp;
        //}


    }
}
