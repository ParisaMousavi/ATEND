using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EOperation
    {
        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private Int32 productID;
        public Int32 ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        //فقط برای اکسس است جای دیگری استفاده نمی شود
        private Int32 productCode;
        public Int32 ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }


        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private double count;
        public double Count
        {
            get { return count; }
            set { count = value; }
        }


        //همان ProductCode است
        private Guid xcode;
        public Guid XCode
        {
            get { return xcode; }
            set { xcode = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~~~~~//

        public bool Insert(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_Operation_Insert", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;


            _command.Parameters.Add(new SqlParameter("iProductID", ProductID));
            _command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            _command.Parameters.Add(new SqlParameter("iType", Type));
            _command.Parameters.Add(new SqlParameter("iCount", Count));
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                _command.ExecuteNonQuery();
                ed.WriteMessage("Operation Insert Done Success. \n");
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Operation.Insert : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        public static bool Delete(SqlTransaction sqlTransaction, SqlConnection sqlConnection, int _ContainerCode, int _Type)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection _connection = sqlConnection;
            SqlCommand _command = new SqlCommand("E_Opearation_Delete", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("iProductCode", _ContainerCode));
            _command.Parameters.Add(new SqlParameter("iType", _Type));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" Error Opearation.Delete : {0}", ex1.Message));
                return false;
            }
            return true;
        }

        public static DataTable SelectByProductCodeType(int ProductCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByProductCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectByProductCodeType(int ProductCode, int Type,SqlTransaction ServerTransaction,SqlConnection ServerConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            SqlConnection connection = ServerConnection;
            ed.WriteMessage("  >>  EOperation.SelectByProductCodeType - Database Name : {0} \n", connection.Database);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByProductCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = ServerTransaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectByProductCode(int ProductCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByProductCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static EOperation SelectByCode(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //return ds.Tables[0];

            EOperation Operation = new EOperation();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Operation.Code = new Guid(ds.Tables[0].Rows[0]["Code"].ToString());
                Operation.ProductCode = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductCode"].ToString());
                Operation.ProductID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                Operation.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString());
                Operation.Count = Convert.ToDouble(ds.Tables[0].Rows[0]["Count"].ToString());
            }

            //SqlCommand command = new SqlCommand("E_Operation_SelectByCode", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            //command.ExecuteNonQuery();

            return Operation;

        }

        //MEDHAT
        public static DataTable SelectByCodeType(int Code, int Type, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            SqlConnection connection = ServerConnection; //new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = ServerTransaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //MEDHAT
        public static EOperation SelectByCodeForServer(Guid Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            SqlConnection connection = ServerConnection; //new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            ed.WriteMessage("  >> EOperation.SelectByCodeForServer - Database Name : {0} \n", connection.Database);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = ServerTransaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            EOperation Operation = new EOperation();
            ed.WriteMessage("Code={0}\n",Code);
            ed.WriteMessage("***Operation.Count={0}\n", ds.Tables[0].Rows.Count);
            if (ds.Tables[0].Rows.Count == 1)
            {
                Operation.Code = new Guid(ds.Tables[0].Rows[0]["Code"].ToString());
                Operation.ProductID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                Operation.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString());
                //Operation.XCode = new Guid(ds.Tables[0].Rows[0]["XCode"].ToString());
                Operation.Count = Convert.ToDouble(ds.Tables[0].Rows[0]["Count"].ToString());
            }
            ed.WriteMessage("****\n");

            return Operation;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsOp = new DataSet();
            adapter.Fill(dsOp);
            return dsOp.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~~~//

        //GroundPost
        public bool InsertX(SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            SqlConnection _connection = sqlConnection;
            ed.WriteMessage("  >> EOperation.Insertx - Database Name : {0} \n", _connection.Database);
            SqlCommand _command = new SqlCommand("E_Operation_InsertX", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("iProductID", ProductID));
            //_command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            _command.Parameters.Add(new SqlParameter("iType", Type));
            //XCode = Guid.NewGuid();
            ed.WriteMessage("XCode={0}\n",XCode.ToString());
            _command.Parameters.Add(new SqlParameter("iXCode", XCode));
            _command.Parameters.Add(new SqlParameter("iCount", Count));


            try
            {
                if (Count != 0)
                {
                    _command.ExecuteNonQuery();
                }
                else
                {
                    Code = Guid.Empty;
                }
                //ed.WriteMessage("Operation Insert Done Success. \n");
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error Operation.Insert : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //frmGroundPost //EFloor.LocalUpdateX
        public static bool DeleteX(SqlTransaction sqlTransaction, SqlConnection sqlConnection, Guid _XCode, int _Type)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            SqlConnection _connection = sqlConnection;
            ed.WriteMessage("  >> EFloor.DeleteX - Database Name : {0} \n", _connection.Database);
            SqlCommand _command = new SqlCommand("E_Opearation_DeleteX", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Transaction = sqlTransaction;

            _command.Parameters.Add(new SqlParameter("iXCode", _XCode));
            _command.Parameters.Add(new SqlParameter("iType", _Type));

            try
            {
                _command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" Error Opearation.DeleteX : {0}", ex1.Message));
                return false;
            }
            ed.WriteMessage("EFloor DeleteX Done \n");
            return true;
        }

        //MOUSAVI
        public static DataTable SelectByXCodeType(Guid XCode, int Type)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByXCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static DataTable SelectByXCodeType(Guid XCode, int Type, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByXCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        //ASHKTORAB
        public static DataTable SelectByXCode(SqlTransaction sqlTransaction, SqlConnection sqlConnection, Guid XCode)
        {
            SqlConnection connection = sqlConnection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByXCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = sqlTransaction;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        //MOUSAVI
        public static EOperation SelectByCodeForLocal(Guid Code)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            EOperation Operation = new EOperation();

            if (ds.Tables[0].Rows.Count == 1)
            {
                Operation.Code = new Guid(ds.Tables[0].Rows[0]["Code"].ToString());
                Operation.ProductID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                Operation.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString());
                Operation.XCode = new Guid(ds.Tables[0].Rows[0]["XCode"].ToString());
                Operation.Count = Convert.ToDouble(ds.Tables[0].Rows[0]["Count"].ToString());
            }

            return Operation;
        }

        public static EOperation SelectByCodeForLocal(Guid Code, SqlTransaction _transaction, SqlConnection _connection)
        {
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            EOperation Operation = new EOperation();

            if (ds.Tables[0].Rows.Count == 1)
            {
                Operation.Code = new Guid(ds.Tables[0].Rows[0]["Code"].ToString());
                Operation.ProductID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                Operation.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString());
                Operation.XCode = new Guid(ds.Tables[0].Rows[0]["XCode"].ToString());
                Operation.Count = Convert.ToDouble(ds.Tables[0].Rows[0]["Count"].ToString());
            }

            return Operation;
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_Operation_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsOp = new DataSet();
            adapter.Fill(dsOp);
            return dsOp.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Acces Part~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Operation_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductID", ProductID));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EOperation.AccessInsert:{0} \n ", ex.Message);
                con.Close();
                return false;
            }

            return true;
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Operation_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            Code = Guid.NewGuid();
            insertCommand.Parameters.Add(new OleDbParameter("iCode", Code));
            insertCommand.Parameters.Add(new OleDbParameter("iProductID", ProductID));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                insertCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EOperation.AccessInsert:{0} \n ", ex.Message);
                return false;
            }

            return true;
        }

        //MOUSAVI
        //public static bool SentFromLocalToAccess(Guid XCode, int Type, int ProductCode, OleDbTransaction _transaction, OleDbConnection _connection)
        //{
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        //    try
        //    {
        //        DataTable operations = Atend.Base.Equipment.EOperation.SelectByXCodeType(XCode, Type);
        //        foreach (DataRow dr in operations.Rows)
        //        {
        //            Atend.Base.Equipment.EOperation SelectedOperation = Atend.Base.Equipment.EOperation.SelectByCodeForLocal(new Guid(dr["Code"].ToString()));
        //            SelectedOperation.productCode = ProductCode;
        //            if (!SelectedOperation.AccessInsert(_transaction, _connection))
        //            {
        //                throw new Exception("opeartion failed");
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error EOperation.SentFromLocalToAccess :{0} \n", ex.Message);
        //        return false;
        //    }
        //    return true;
        //}


        //public bool AccessInsert(OleDbTransaction sqlTransaction, OleDbConnection sqlConnection)
        //{
        //    OleDbConnection _connection = sqlConnection;
        //    OleDbCommand _command = new OleDbCommand("E_Operation_Insert", _connection);
        //    _command.CommandType = CommandType.StoredProcedure;
        //    _command.Transaction = sqlTransaction;


        //    _command.Parameters.Add(new OleDbParameter("iProductID", ProductID));
        //    _command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
        //    _command.Parameters.Add(new OleDbParameter("iType", Type));
        //    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        //    try
        //    {
        //        _command.ExecuteNonQuery();
        //        ed.WriteMessage("Operation Insert Done Success. \n");
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error Operation.AccessInsert : {0} \n", ex1.Message));
        //        return false;
        //    }
        //    return true;
        //}



       

        public bool AccessUpdate()
        {
            //E_Operation_Update
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_Operation_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                con.Open();
                insertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EOperation.AccessUpdate:{0} \n ", ex.Message);
                con.Close();
                return false;
            }

            return true;
        }


        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            //E_Operation_Update
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_Operation_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage("ProductCode:{0} \n", ProductCode);
            //ed.WriteMessage("Type:{0} \n", Type);
            //ed.WriteMessage("Count:{0} \n", Count);
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iType", Type));
            insertCommand.Parameters.Add(new OleDbParameter("iCount", Count));
            try
            {
                //con.Open();
                insertCommand.ExecuteNonQuery();
                //con.Close();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error EOperation.AccessUpdate:{0} \n ", ex.Message);
                //con.Close();
                return false;
            }

            return true;
        }


        //frmDrawBranchCable.cs
        public static DataTable AccessSelectByProductCodeType(int ProductCode, int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Operation_SelectByProductCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }

        public static EOperation AccessSelectByCode(int Code, OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter((string.Format("Select * From E_Operation Where Code={0}", Code)), connection);
            adapter.SelectCommand.CommandType = CommandType.Text;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            //return ds.Tables[0];

            EOperation Operation = new EOperation();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Operation.Code = new Guid(ds.Tables[0].Rows[0]["Code"].ToString());
                Operation.ProductCode = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductCode"].ToString());
                Operation.ProductID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductID"].ToString());
                Operation.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString());
                Operation.Count = Convert.ToDouble(ds.Tables[0].Rows[0]["Count"].ToString());
            }

            //SqlCommand command = new SqlCommand("E_Operation_SelectByCode", connection);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add(new SqlParameter("iCode", Code));
            //command.ExecuteNonQuery();

            return Operation;

        }
        public static DataTable AccessSelectByProductCodeType(int ProductCode, int Type, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_Operation_SelectByProductCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];

        }


        //public static DataTable AccessSelectByProductCode(int ProductCode)
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("E_Operation_SelectByProductCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);
        //    return ds.Tables[0];
        //}
        //public static DataTable AccessSelectAll()
        //{
        //    OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter adapter = new OleDbDataAdapter("E_Operation_Select", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    DataSet dsOp = new DataSet();
        //    adapter.Fill(dsOp);
        //    return dsOp.Tables[0];
        //}

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
    }
}
