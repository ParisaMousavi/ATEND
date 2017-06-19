using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Atend.Base.Equipment
{
    public class EJackPanelCell
    {
        public EJackPanelCell()
        { }

        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int jackPanelCode;
        public int JackPanelCode
        {
            get { return jackPanelCode; }
            set { jackPanelCode = value; }
        }

        private Guid jackPanelXCode;
        public Guid JackPanelXCode
        {
            get { return jackPanelXCode; }
            set { jackPanelXCode = value; }
        }

        private byte num;
        public byte Num
        {
            get { return num; }
            set { num = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private Guid productXCode;
        public Guid ProductXCode
        {
            get { return productXCode; }
            set { productXCode = value; }
        }

        private byte productType;
        public byte ProductType
        {
            get { return productType; }
            set { productType = value; }
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


        public bool Insert()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new SqlParameter("iNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            try
            {
                Connection.Open();

                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_Insert :{0}", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        //HATAMI //ShareOnServer
        public bool ServerInsert(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanelCell_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage(" go for E_JackPanelCell_Insert \n");
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            insertCommand.Parameters.Add(new SqlParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                //ed.WriteMessage(" finished E_JackPanelCell_Insert \n");

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).ServerInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //ShareOnServer
        public bool ServerUpdate(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction LocalTransaction, SqlConnection LocalConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanelCell_Update", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage(" go for E_JackPanelCell_Insert \n");
            //insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            insertCommand.Parameters.Add(new SqlParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                //ed.WriteMessage(" finished E_JackPanelCell_Insert \n");

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).ServerUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        public bool Insert(SqlTransaction transaction, SqlConnection Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new SqlParameter("iCellNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            try
            {

                Command.Transaction = transaction;
                Code = Convert.ToInt32(Command.ExecuteScalar());


                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_Insert :{0}", ex1.Message));


                return false;
            }
        }

        public static bool Update_XX(SqlConnection Connection, SqlTransaction Transaction, int Code, int NewCode)
        {

            SqlCommand Command = new SqlCommand("E_JackPanelCell_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iNewCode", NewCode));
            try
            {

                Command.Transaction = Transaction;
                Command.ExecuteNonQuery();

                return true;
            }
            catch
            {


                return false;
            }
        }

        public bool Update_XX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", JackPanelCode));
            Command.Parameters.Add(new SqlParameter("iNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_Update: {0}", ex.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool Delete(SqlTransaction transaction, SqlConnection connection, int Code)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Delete", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iJackPanelCode", Code));
            Command.Transaction = transaction;
            try
            {

                Command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In  E_JackPanelCell_Delete:{0}", ex.Message));

                return false;
            }
        }

        //MEDHAT //ShareOnServer
        public static EJackPanelCell ServerSelectByCode(int Code, SqlConnection ServerConnection, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = ServerConnection;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = ServerTransaction;
            Command.Parameters.Add(new SqlParameter("iCode", Code));

            SqlDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();

            if (reader.Read())
            {
                jackPanelCell.XCode = new Guid(reader["XCode"].ToString());
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelCode = Convert.ToInt32(reader["JackPanelCode"].ToString());
                //jackPanelCell.ProductXCode = new Guid(reader["ProductXCode"].ToString());
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"].ToString());
                jackPanelCell.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
            }
            else
            {
                jackPanelCell.Code = -1;
                ed.WriteMessage("ServerSelectByCode found no row in : jack panel cell\n");
            }
            reader.Close();
            return jackPanelCell;
        }

        public static DataTable SelectByJackPanelCode(int jackPanelCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        public static DataTable SelectByJackPanelCode(int jackPanelCode,SqlTransaction ServerTransaction,SqlConnection ServerConnection)
        {
            SqlConnection Connection = ServerConnection;
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = ServerTransaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        public static EJackPanelCell SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelCode = Convert.ToInt32(reader["JackPanelCode"]);
                jackPanelCell.Num = Convert.ToByte(reader["Num"]);
                jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);


            }
            reader.Close();
            Connection.Close();
            return jackPanelCell;
        }

        public void ServerSelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            //EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                //jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                //JackPanelCode = Convert.ToInt32(reader["JackPanelCode"]);
                Num = Convert.ToByte(reader["CellNum"]);
                //jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                ProductType = Convert.ToByte(reader["ProductType"]);


            }
            reader.Close();
            Connection.Close();
            //return jackPanelCell;
        }

        //ASHKTORAB
        public static EJackPanelCell SelectByCodeForServer(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.Code = Convert.ToInt32(reader["Code"].ToString());
                //jackPanelCell.JackPanelXCode = new Guid(reader["JackPanelCode"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
                //jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);


            }
            reader.Close();
            Connection.Close();
            return jackPanelCell;
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsJack = new DataSet();
            adapter.Fill(dsJack);
            return dsJack.Tables[0];
        }

        //~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            Command.Parameters.Add(new SqlParameter("iNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));

            try
            {
                Connection.Open();

                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_InsertX :{0}", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        public bool InsertX(SqlTransaction transaction, SqlConnection Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_InsertX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            XCode = Guid.NewGuid();//Hatami
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            Command.Parameters.Add(new SqlParameter("iCellNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                Command.Transaction = transaction;
                Command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_InsertX :{0}", ex1.Message));


                return false;
            }
        }

        //MEDHAT
        public bool LocalInsertX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanelCell_InsertX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            XCode = Guid.NewGuid();
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            insertCommand.Parameters.Add(new SqlParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            //insertCommand.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                //Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).LocalInsertX : {0} \n", ex.Message));
                return false;
            }
        }


        public bool LocalUpdateX(SqlTransaction _transaction, SqlConnection _connection, bool BringOperation, bool BringSubEquips, SqlTransaction ServerTransaction, SqlConnection ServerConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection con = _connection;
            SqlCommand insertCommand = new SqlCommand("E_JackPanelCell_UpdateX", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage(" go for E_JackPanelCell_Insert \n");
            insertCommand.Parameters.Add(new SqlParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            insertCommand.Parameters.Add(new SqlParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            insertCommand.Parameters.Add(new SqlParameter("iProductType", ProductType));
            insertCommand.Parameters.Add(new SqlParameter("iCode", Code));
            insertCommand.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            try
            {

                //insertCommand.ExecuteNonQuery();
                //insertCommand.Parameters.Clear();
                //insertCommand.CommandType = CommandType.Text;
                //insertCommand.CommandText = "SELECT @@IDENTITY";
                insertCommand.ExecuteNonQuery();

                //ed.WriteMessage(" finished E_JackPanelCell_Insert \n");

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).LocalUpdate : {0} \n", ex.Message));
                return false;
            }
        }

        //ASHKTORAB
        public bool InsertXX(SqlTransaction transaction, SqlConnection Connection)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_InsertXX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            //XCode = Guid.NewGuid();
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iCode", Code));
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            Command.Parameters.Add(new SqlParameter("iCellNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            try
            {

                Command.Transaction = transaction;
                Command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_InsertXX :{0}", ex1.Message));


                return false;
            }
        }

        //ShareOnServer
        public bool UpdateX(SqlConnection _Connection, SqlTransaction _Transaction)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _Connection;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            Command.Parameters.Add(new SqlParameter("iCellNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            Command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            Command.Parameters.Add(new SqlParameter("iCode", Code));

            try
            {
                Command.Transaction = _Transaction;
                Command.ExecuteNonQuery();
                return true;
            }
            catch   (System.Exception ex)
            {
                ed.WriteMessage("ERROR IN Updatex {0}\n", ex.Message);
                return false;
            }
        }

        public bool UpdateX()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_UpdateX", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", JackPanelXCode));
            Command.Parameters.Add(new SqlParameter("iNum", Num));
            Command.Parameters.Add(new SqlParameter("iProductXCode", ProductXCode));
            Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In E_JackPanelCell_UpdateX: {0}", ex.Message));
                Connection.Close();
                return false;
            }
        }

        public static bool DeleteX(SqlTransaction transaction, SqlConnection connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_DeleteX", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iJackPanelXCode", XCode));
            Command.Transaction = transaction;
            try
            {

                Command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In  E_JackPanelCell_DeleteX:{0}", ex.Message));

                return false;
            }
        }

        //ASHKTORAB
        public static bool DeleteXCode(SqlTransaction transaction, SqlConnection connection, Guid XCode)
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_DeleteXCode", connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Command.Transaction = transaction;
            try
            {

                Command.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error In  E_JackPanelCell_DeleteX:{0}", ex.Message));

                return false;
            }
        }

        //MOUSAVI //SendFromLocalToAccess
        public static DataTable SelectByJackPanelXCode(Guid jackPanelXCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_SelectByJackPanelXCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanelXCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            ed.WriteMessage("Cells Found : {0} \n", dsKackPanelCell.Tables[0].Rows.Count);
            return dsKackPanelCell.Tables[0];
        }

        //ASHKTORAB //ShareOnServer
        public static DataTable SelectByJackPanelXCode(SqlConnection _Connection, SqlTransaction _Transaction, Guid jackPanelXCode)
        {
            SqlConnection Connection = _Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_SelectByJackPanelXCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _Transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iJackPanelXCode", jackPanelXCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        //MOUSAVI //SendFromLocalToAcces
        public static EJackPanelCell SelectByXCode(Guid XCode)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.XCode = new Guid(reader["XCode"].ToString());
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelXCode = new Guid(reader["JackPanelXCode"].ToString());
                jackPanelCell.ProductXCode = new Guid(reader["ProductXCode"].ToString());
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                jackPanelCell.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);


            }
            else
            {
                jackPanelCell.Code = -1;
            }
            reader.Close();
            Connection.Close();
            return jackPanelCell;
        }

        //ASHKTORAB //ShareOnServer
        public static EJackPanelCell SelectByXCode(Guid XCode, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection Connection = LocalConnection;
            SqlCommand Command = new SqlCommand("E_JackPanelCell_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            EJackPanelCell jackPanelCell = new EJackPanelCell();

            try
            {
                Command.Transaction = LocalTransaction;
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    jackPanelCell.XCode = new Guid(reader["XCode"].ToString());
                    jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                    jackPanelCell.JackPanelXCode = new Guid(reader["JackPanelXCode"].ToString());
                    jackPanelCell.ProductXCode = new Guid(reader["ProductXCode"].ToString());
                    jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                    jackPanelCell.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);


                }
                else
                {
                    jackPanelCell.Code = -1;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("Error Ejackpanelcell.In SelectByXCode4Design.TransAction:{0}\n", ex.Message);
            }

            return jackPanelCell;
        }

        //Medhat
        public static EJackPanelCell SelectByXCode(SqlTransaction _tarnsaction,   SqlConnection _connection, Guid XCode)
        {
            SqlConnection Connection = _connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand Command = new SqlCommand("E_JackPanelCell_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = _tarnsaction;
            Command.Parameters.Add(new SqlParameter("iXCode", XCode));
            //Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.XCode = new Guid(reader["XCode"].ToString());
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelXCode = new Guid(reader["JackPanelXCode"].ToString());
                jackPanelCell.ProductXCode = new Guid(reader["ProductXCode"].ToString());
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                jackPanelCell.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
            }
            else
            {
                jackPanelCell.Code = -1;
            }
            reader.Close();
            return jackPanelCell;
        }

        //ASHKTORAB
        public static EJackPanelCell SelectByCodeForLocal(int Code, SqlTransaction _Transaction, SqlConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //SqlConnection Connection = _Connection;// new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            try
            {
                SqlCommand Command = new SqlCommand("E_JackPanelCell_Select", _Connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Transaction = _Transaction;
                Command.Parameters.Add(new SqlParameter("iCode", Code));
                //Connection.Open();
                try
                {
                    SqlDataReader reader = Command.ExecuteReader();
                    if (reader.Read())
                    {
                        jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                        jackPanelCell.JackPanelXCode = new Guid(reader["JackPanelXCode"].ToString());
                        jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
                        jackPanelCell.ProductXCode = new Guid(reader["ProductXCode"].ToString());
                        jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                        jackPanelCell.XCode = new Guid(reader["XCode"].ToString());
                        jackPanelCell.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    }
                    else
                    {
                        Code = -1;
                    }
                    reader.Close();
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("__ ;{0}\n", ex.Message);
                }
                //Connection.Close();B
                return jackPanelCell;
            }
            catch (System.Exception ex)
            {
                //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("error :{0}\n", ex.Message);
            }
            return jackPanelCell;
        }

        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("E_JackPanelCell_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsJack = new DataSet();
            adapter.Fill(dsJack);
            return dsJack.Tables[0];
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~Access Part~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanelCell_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelXCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));

            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //}

                con.Open();
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());
                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell.AccessInsert : {0} \n", ex.Message));
                con.Close();
                return false;
            }
        }

        //MEDHAT //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _connection;
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanelCell_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _transaction;

            //ed.WriteMessage(" go for E_JackPanelCell_Insert \n");
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));

            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                //ed.WriteMessage(" finished E_JackPanelCell_Insert \n");

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        public bool AccessInsert(OleDbTransaction _oldtransaction, OleDbConnection _oldconnection,OleDbTransaction _Newtransaction,OleDbConnection _NewConnection, bool BringOperation, bool BringSubEquips)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection con = _NewConnection;
            OleDbCommand insertCommand = new OleDbCommand("E_JackPanelCell_Insert", con);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Transaction = _Newtransaction;

            //ed.WriteMessage(" go for E_JackPanelCell_Insert \n");
            insertCommand.Parameters.Add(new OleDbParameter("iXCode", XCode));
            insertCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", JackPanelCode));
            insertCommand.Parameters.Add(new OleDbParameter("iCellNum", Num));
            insertCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            insertCommand.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            
            try
            {

                insertCommand.ExecuteNonQuery();
                insertCommand.Parameters.Clear();
                insertCommand.CommandType = CommandType.Text;
                insertCommand.CommandText = "SELECT @@IDENTITY";
                Code = Convert.ToInt32(insertCommand.ExecuteScalar());

                //ed.WriteMessage(" finished E_JackPanelCell_Insert \n");

                return true;
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(string.Format("Error EJackPanelCell(transaction).AccessInsert : {0} \n", ex.Message));
                return false;
            }
        }

        //MOUSAVI
        public static DataTable AccessSelectByJackPanelCode(int jackPanelCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }


        public static DataTable AccessSelectByJackPanelCodeForConvertor(int jackPanelCode,OleDbTransaction _transaction,OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanelCode));
            adapter.SelectCommand.Transaction = _transaction;
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        //status report
        public static DataTable AccessSelectByJackPanelCode(int jackPanelCode, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        //MOUSAVI
        public static DataTable AccessSelectByJackPanelCode(int jackPanelCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelCell_SelectByJackPanelCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iJackPanelCode", jackPanelCode));
            DataSet dsKackPanelCell = new DataSet();
            adapter.Fill(dsKackPanelCell);
            return dsKackPanelCell.Tables[0];
        }

        //MOUSAVI //frmDrawGroundPost
        public static EJackPanelCell AccessSelectByCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanelCell_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelCode = Convert.ToInt32(reader["JackPanelCode"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
                jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);

            }
            reader.Close();
            Connection.Close();
            return jackPanelCell;
        }


        public static EJackPanelCell AccessSelectByXCode(Guid XCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("E_JackPanelCell_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelCode = Convert.ToInt32(reader["JackPanelCode"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
                jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                jackPanelCell.XCode = new Guid(reader["XCode"].ToString());

            }
            reader.Close();
            Connection.Close();
            return jackPanelCell;
        }

        //MOUSAVI
        public static EJackPanelCell AccessSelectByXCode(Guid XCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection Connection = _connection;
            OleDbCommand Command = new OleDbCommand("E_JackPanelCell_SelectByXCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iXCode", XCode));
            Command.Transaction = _transaction;
            OleDbDataReader reader = Command.ExecuteReader();
            EJackPanelCell jackPanelCell = new EJackPanelCell();
            if (reader.Read())
            {
                jackPanelCell.Code = Convert.ToInt32(reader["Code"]);
                jackPanelCell.JackPanelCode = Convert.ToInt32(reader["JackPanelCode"]);
                jackPanelCell.Num = Convert.ToByte(reader["CellNum"]);
                jackPanelCell.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                jackPanelCell.ProductType = Convert.ToByte(reader["ProductType"]);
                jackPanelCell.XCode = new Guid(reader["XCode"].ToString());

            }
            else
            {
                jackPanelCell.Code = -1;
            }
            reader.Close();
            return jackPanelCell;
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("E_JackPanelCell_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", -1));
            DataSet dsJack = new DataSet();
            adapter.Fill(dsJack);
            return dsJack.Tables[0];
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
    }
}
