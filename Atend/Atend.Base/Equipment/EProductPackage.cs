using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EProductPackage
    {
        private int containerPackageCode;
        public int ContainerPackageCode
        {
            get { return containerPackageCode; }
            set { containerPackageCode = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private Int16 tableType;
        public Int16 TableType
        {
            get { return tableType; }
            set { tableType = value; }
        }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        //فقط در اکسس و سرور استفاده می شود
        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }


        //~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~//

        public bool Insert(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_ProductPackage_Insert", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;
            _command.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            _command.Parameters.Add(new SqlParameter("iCount", Count));
            _command.Parameters.Add(new SqlParameter("iTableType", TableType));
            _command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            _command.Parameters.Add(new SqlParameter("iXCode", XCode));


            try
            {
                Code = Convert.ToInt32(_command.ExecuteScalar());
                ed.WriteMessage("ProductPackage Insert Done Success,Code={0},iContainerPackageCode={1},iTableType={2}, \n", Code,ContainerPackageCode,TableType);
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ProductPackage.Insert : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        public static bool Update_XX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int CurrentProductCode, int NewProductCode, int Type)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            SqlTransaction transaction = sqlTransaction;
            SqlConnection Connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ProductPackage_Update", Connection);
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iCurrentProductCode", CurrentProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iNewProductCode", NewProductCode));
            //sqlCommand.Parameters.Add(new SqlParameter("iLength", Length));
            sqlCommand.Parameters.Add(new SqlParameter("iTableType", Type));
            //sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            //sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            //sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            //sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            //sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            //sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    //if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;

                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

                    //if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                    //        _EProductPackage.ContainerPackageCode = containerPackage.Code;

                    //        if (_EProductPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //if (canCommitTransaction)
                    //{
                    //    transaction.Commit();
                    //    Connection.Close();
                    //    return true;
                    //}
                    //else
                    //{
                    //    transaction.Rollback();
                    //    Connection.Close();
                    //    return false;
                    //    //throw new Exception("can not commit transaction");

                    //}
                    return true;
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Product Package Transaction:{0}", ex1.Message);
                    //Connection.Close();
                    //transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EProductPackage.Update: {0} \n", ex1.Message));

                //Connection.Close();
                return false;
            }
        }

        //Share -Local-Server ,Please Donot Change it //frmGroundPost
        public static bool Delete(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int _ContainerCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_productPackage_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;
            //ed.WriteMessage("\nContainerCode = " + _ContainerCode.ToString());
            _command.Parameters.Add(new SqlParameter("iContainerPackageCode", _ContainerCode));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" E_ProductPackage.Delete : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        public static DataTable SelectByContainerPackageCode(int ContainerPackageCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectByContainerPackageCode(int ContainerPackageCode, SqlTransaction transaction, SqlConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectBycCode(int ContainerPackageCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("@@cc:{0}\n", Atend.Control.ConnectionString.LocalcnString);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerPackageCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("@@ContainerPackageCode:{0}\n", ContainerPackageCode);
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //ed.WriteMessage("@@Count:{0}\n", ds.Tables[0].Rows.Count);
            return ds.Tables[0];

        }

        public static DataTable SelectByContainerPackageCodeAndType(int ContainerPackageCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerPackageCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable SelectByContainerCodeAndType(int ContainerCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", ContainerCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProductPackage = new DataSet();
            adapter.Fill(dsProductPackage);
            return dsProductPackage.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~~//

        //frmGroundPost
        public bool InsertX(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_ProductPackage_InsertX", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;
            _command.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            _command.Parameters.Add(new SqlParameter("iCount", Count));
            _command.Parameters.Add(new SqlParameter("iTableType", TableType));
            //XCode = Guid.NewGuid();
            _command.Parameters.Add(new SqlParameter("iXCode", XCode));


            //DataTable ProductTbl = Atend.Base.Equipment.EProductPackage.SelectByContainerCodeAndType(ContainerPackageCode, TableType);
            ////DataRow Dr = ProductTbl.NewRow();
            //DataRow[] Dr = ProductTbl.Select("XCode = " + XCode);

            //if (Dr.Length == 0)
            //{
            try
            {
                if (Count != 0)
                {

                    Code = Convert.ToInt32(_command.ExecuteScalar());
                }
                else
                {
                    Code = -1;
                    //return true;
                }
                //ed.WriteMessage("ProductPackage Insert Done Success. \n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error ProductPackage.Insert : {0} \n", ex1.Message));
                return false;
            }

            //}

        }

        public static bool DeleteX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int _ContainerCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_productPackage_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;
            //ed.WriteMessage("\nContainerCode = " + _ContainerCode.ToString());
            _command.Parameters.Add(new SqlParameter("iContainerPackageCode", _ContainerCode));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" E_ProductPackage.Delete : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        public static bool UpdateX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, Guid XCode, int Type)
        {

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            SqlTransaction transaction = sqlTransaction;
            SqlConnection Connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("E_ProductPackage_UpdateX", Connection);
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new SqlParameter("iCurrentProductCode", CurrentProductCode));
            //sqlCommand.Parameters.Add(new SqlParameter("iNewProductCode", NewProductCode));
            sqlCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            sqlCommand.Parameters.Add(new SqlParameter("iTableType", Type));
            //sqlCommand.Parameters.Add(new SqlParameter("iComment", Comment));
            //sqlCommand.Parameters.Add(new SqlParameter("iName", Name));
            //sqlCommand.Parameters.Add(new SqlParameter("iImage", Image));
            //sqlCommand.Parameters.Add(new SqlParameter("iDistancePhase", distancePhase));
            //sqlCommand.Parameters.Add(new SqlParameter("iDistanceCrossArm", DistanceCrossArm));
            //sqlCommand.Parameters.Add(new SqlParameter("iConsolType", ConsolType));
            //sqlCommand.Parameters.Add(new SqlParameter("iVoltagelevel", VoltageLevel));
            try
            {
                //Connection.Open();
                //transaction = Connection.BeginTransaction();
                sqlCommand.Transaction = transaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    bool canCommitTransaction = true;
                    int Counter = 0;
                    //ed.WriteMessage("GOTO\n");
                    //if (EContainerPackage.Delete(transaction, Connection, Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol)))
                    //{
                    //    ed.WriteMessage("EContainerPackage.update\n");
                    //    Atend.Base.Equipment.EContainerPackage _EContainerPackage = new EContainerPackage(); ;
                    //    ed.WriteMessage("Code:"+Code.ToString()+"\n");

                    //    _EContainerPackage.ContainerCode = Code;
                    //    ed.WriteMessage("Type:" + "\n");

                    //    _EContainerPackage.Type = Convert.ToInt32(Atend.Control.Enum.ProductType.Consol);
                    //    ed.WriteMessage("_EContainerPackage.Insert:\n");
                    //    if (_EContainerPackage.Insert(transaction, Connection) && canCommitTransaction)
                    //    {
                    //        canCommitTransaction = true;
                    //    }
                    //    else
                    //    {
                    //        canCommitTransaction = false;

                    //    }
                    //}
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //ed.WriteMessage("EProductPackage.Delete:\n");
                    //Atend.Base.Equipment.EContainerPackage containerPackage = Atend.Base.Equipment.EContainerPackage.selectByContainerCodeAndType(Code, Convert.ToInt32(Atend.Control.Enum.ProductType.Consol));
                    //ed.WriteMessage("containerPackage.Code.ToString()" + containerPackage.Code.ToString() + "\n");

                    //if (EProductPackage.Delete(transaction, Connection, containerPackage.Code))
                    //{
                    //    while (canCommitTransaction && Counter < EquipmentList.Count)
                    //    {
                    //        Atend.Base.Equipment.EProductPackage _EProductPackage;
                    //        _EProductPackage = ((Atend.Base.Equipment.EProductPackage)EquipmentList[Counter]);
                    //        ed.WriteMessage("ContainerPackageCode:" + containerPackage.Code.ToString() + "\n");
                    //        _EProductPackage.ContainerPackageCode = containerPackage.Code;

                    //        if (_EProductPackage.Insert(transaction, Connection) && canCommitTransaction)
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
                    //else
                    //{
                    //    canCommitTransaction = false;
                    //}
                    //if (canCommitTransaction)
                    //{
                    //    transaction.Commit();
                    //    Connection.Close();
                    //    return true;
                    //}
                    //else
                    //{
                    //    transaction.Rollback();
                    //    Connection.Close();
                    //    return false;
                    //    //throw new Exception("can not commit transaction");

                    //}
                    return true;
                }
                catch (System.Exception ex1)
                {
                    ed.WriteMessage("Error In Product Package Transaction:{0}", ex1.Message);
                    Connection.Close();
                    transaction.Rollback();
                    return false;
                }

            }
            catch (System.Exception ex1)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\n");
                ed.WriteMessage(string.Format(" ERROR EProductPackage.UpdateX: {0} \n", ex1.Message));

                Connection.Close();
                return false;
            }
        }

        //MOUSAVI , frmGroundPost
        public static DataTable SelectByContainerXCodeType(Guid ContainerXCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerXCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", ContainerXCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        //Medhat //frmGroundPost
        public static DataTable SelectByXCodeType(Guid ContainerXCode, int Type)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("start\n");
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByXCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", ContainerXCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            ed.WriteMessage("end\n");
            return ds.Tables[0];
        }

        public static EProductPackage SelectX(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectX", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            DataSet dsProductPackage = new DataSet();
            adapter.Fill(dsProductPackage);
            EProductPackage ProPck = new EProductPackage();
            if (dsProductPackage.Tables[0].Rows.Count > 0)
            {
                ProPck.Code = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Code"].ToString());
                ProPck.ContainerPackageCode = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ContainerPackageCode"].ToString());
                ProPck.Count = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Count"].ToString());
                //ProPck.ProductCode  = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ProductCode"].ToString());
                ProPck.TableType = Convert.ToInt16(dsProductPackage.Tables[0].Rows[0]["TableType"].ToString());
                ProPck.XCode = new Guid(dsProductPackage.Tables[0].Rows[0]["XCode"].ToString());

            }
            return ProPck;
        }

        //MEDHAT //frmGroundPost
        public static EProductPackage SelectByXCode(Guid XCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            DataSet dsProductPackage = new DataSet();
            adapter.Fill(dsProductPackage);
            EProductPackage ProPck = new EProductPackage();
            if (dsProductPackage.Tables[0].Rows.Count > 0)
            {
                ProPck.Code = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Code"].ToString());
                ProPck.ContainerPackageCode = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ContainerPackageCode"].ToString());
                ProPck.Count = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Count"].ToString());
                //ProPck.ProductCode  = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ProductCode"].ToString());
                ProPck.TableType = Convert.ToInt16(dsProductPackage.Tables[0].Rows[0]["TableType"].ToString());
                ProPck.XCode = new Guid(dsProductPackage.Tables[0].Rows[0]["XCode"].ToString());
            }
            else
            {
                ProPck.Code = -1;
            }
            return ProPck;
        }

        //ASHKTORAB
        public static DataTable SelectByContainerPackageCodeAndType(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int ContainerPackageCode, int Type)
        {
            SqlConnection connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerPackageCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = sqlTransaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iContainerPackageCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        //MOUSAVI
        public static DataTable SelectByContainerPackageCodeForLocal(int ContainerPackageCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectByContainerPackageCodeForLocal(int ContainerPackageCode, SqlTransaction transaction, SqlConnection _connection)
        {

            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //MOUSAVI
        public static EProductPackage SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_ProductPackage_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProductPackage = new DataSet();
            adapter.Fill(dsProductPackage);
            EProductPackage ProPck = new EProductPackage();
            if (dsProductPackage.Tables[0].Rows.Count > 0)
            {
                ProPck.Code = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Code"].ToString());
                ProPck.ContainerPackageCode = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ContainerPackageCode"].ToString());
                ProPck.Count = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["Count"].ToString());
                //ProPck.ProductCode  = Convert.ToInt32(dsProductPackage.Tables[0].Rows[0]["ProductCode"].ToString());
                ProPck.TableType = Convert.ToInt16(dsProductPackage.Tables[0].Rows[0]["TableType"].ToString());
                ProPck.XCode = new Guid(dsProductPackage.Tables[0].Rows[0]["XCode"].ToString());

            }
            else
            {
                ProPck.Code = -1;
            }
            return ProPck;
        }


        //~~~~~~~~~~~~~~~ ACCESS PART ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_ProductPackage_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iContainerPackageCode", containerPackageCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            insertCommand.Parameters.Add(new OleDbParameter("iTableType", TableType));

            try
            {
                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EProductPackage.AccessInsert: {0} ", ex.Message);
                con.Close();
                return false;
            }

            return true;
        }

        //MOUSAVI   
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_ProductPackage_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("Code:{0}\nCP:{1}\nPC:{2}\nCount:{3}\ntt:{4}\n", Code, containerPackageCode, ProductCode, Count, TableType);

            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iContainerPackageCode", containerPackageCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            insertCommand.Parameters.Add(new OleDbParameter("iTableType", TableType));

            try
            {
                insertCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EProductPackage.AccessInsert: {0} ", ex.Message);
                return false;
            }

            return true;
        }

        public static DataTable AccessSelectByContainerPackageCode(int ContainerPackageCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //status report
        public static DataTable AccessSelectByContainerPackageCode(int ContainerPackageCode, OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //AcDrawGroundPost
        public static DataTable AccessSelectByContainerPackageCode(int ContainerPackageCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }


        public static DataTable AccessSelectByContainerPackageCodeAndType(int ContainerPackageCode, int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerPackageCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iContainerPackageCode", ContainerPackageCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        //AcDrawPole
        public static DataTable AccessSelectByContainerCodeAndType(OleDbTransaction _Transaction, OleDbConnection _Connection, int ContainerCode, int Type)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _Transaction;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable AccessSelectByContainerCodeAndType(int ContainerCode, int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_SelectByContainerCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", ContainerCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }


        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_ProductPackage_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsProductPackage = new DataSet();
            adapter.Fill(dsProductPackage);
            return dsProductPackage.Tables[0];
        }

        //public static DataTable SelectAllAndMerge()
        //{

        //    DataTable AccTbl = AccessSelectAll();
        //    DataTable SqlTbl = SelectAll();

        //    DataTable MergeTbl = SqlTbl.Copy();
        //    DataColumn IsSql = new DataColumn("IsSql", typeof(bool));
        //    IsSql.DefaultValue = true;
        //    MergeTbl.Columns.Add(IsSql);

        //    foreach (DataRow Dr in AccTbl.Rows)
        //    {
        //        DataRow MergeRow = MergeTbl.NewRow();

        //        foreach (DataColumn Dc in AccTbl.Columns)
        //        {
        //            MergeRow[Dc.ColumnName] = Dr[Dc.ColumnName];
        //        }

        //        MergeRow["IsSql"] = false;
        //        MergeRow["XCode"] = new Guid("00000000-0000-0000-0000-000000000000");
        //        MergeTbl.Rows.Add(MergeRow);
        //    }


        //    return MergeTbl;

        //}

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public static bool SentFromLocalToAccess(int Code, Guid XCode, int Type, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            int NewCode = 0;
            try
            {


                switch ((Atend.Control.Enum.ProductType)Type)
                {
                    case Atend.Control.Enum.ProductType.Consol:
                        Atend.Base.Equipment.EConsol SelectedConsol = Atend.Base.Equipment.EConsol.SelectByXCodeForDesign(XCode);
                        if (SelectedConsol.Code != -1)
                        {
                            if (SelectedConsol.AccessInsert(_transaction, _connection, true, false))
                            {
                                NewCode = SelectedConsol.Code;
                            }
                            else
                            {
                                throw new Exception("SelectedConsol.AccessInsert failed");
                            }
                        }
                        break;
                }

                //aval tazhiz bayad dar jadvale khodesh bere bad Code baraie ContainerCode ersal shavad
                if (Atend.Base.Equipment.EContainerPackage.SentFromLocalToAccess(XCode, Type, 1, _transaction, _connection))
                {
                    EProductPackage SubProducts = Atend.Base.Equipment.EProductPackage.SelectByCode(Code);
                    if (SubProducts.Code != -1)
                    {
                        Atend.Base.Equipment.EProductPackage SelectedSub = Atend.Base.Equipment.EProductPackage.SelectByCode(Code);
                        if (SelectedSub.Code != -1)
                        {
                            SelectedSub.productCode = NewCode;
                            if (!SelectedSub.AccessInsert(_transaction, _connection))
                            {
                                throw new Exception("SelectedSub.AccessInsert failed");
                            }
                        }
                        else
                        {
                            //error
                        }
                        //insert access here 
                    }
                    else
                    {
                        //error
                    }
                }
                else
                {
                    //error
                }


            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
