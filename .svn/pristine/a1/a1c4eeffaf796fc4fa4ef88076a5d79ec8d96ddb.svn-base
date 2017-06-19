using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using Atend.Control;
using System.Collections;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Base
{
    public class BEquipStatus
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int aCode;
        public int ACode
        {
            get { return aCode; }
            set { aCode = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        private double pricePercent;
        public double PricePercent
        {
            get { return pricePercent; }
            set { pricePercent = value; }
        }


        private double executePricePercent;
        public double ExecutePricePercent
        {
            get { return executePricePercent; }
            set { executePricePercent = value; }
        }


        private double wagePricePercent;
        public double WagePricePercent
        {
            get { return wagePricePercent; }
            set { wagePricePercent = value; }
        }

        private ArrayList arraySub;
        public ArrayList ArraySub
        {
            get { return arraySub; }
            set { arraySub = value; }
        }


        public BEquipStatus()
        {
            ArraySub = new ArrayList();
        }
        //~~~~~~~~~~~~~~~~~~~~~~~Local Part~~~~~~~~~~~~~~~~~//

        public bool InsertX()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_EquipStatus_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iACode", ACode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));
            command.Parameters.Add(new SqlParameter("iPricePercent", PricePercent));
            command.Parameters.Add(new SqlParameter("iExecutePricePercent", ExecutePricePercent));
            command.Parameters.Add(new SqlParameter("iWagePricePercent", WagePricePercent));
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    Code = Convert.ToInt32(command.ExecuteScalar());

                    for (int i = 0; i < arraySub.Count; i++)
                    {
                        Atend.Base.Base.BStatusWorkOrder status = new BStatusWorkOrder();
                        status.EquipStatusCode = Code;
                        status.WorkOrderCode = Convert.ToInt32(arraySub[i].ToString());

                        if(!status.InsertX())
                            throw new Exception("Failed");
                    }

                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (System.Exception ex)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BEquipStatus.Insert{0}\n", ex1.Message));
                connection.Close();
                return false;
            }


        }

        public bool UpdateX()
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_EquipStatus_Update";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iACode", ACode));
            command.Parameters.Add(new SqlParameter("iIsDefault", IsDefault));

            command.Parameters.Add(new SqlParameter("iPricePercent", PricePercent));
            command.Parameters.Add(new SqlParameter("iExecutePricePercent", ExecutePricePercent));
            command.Parameters.Add(new SqlParameter("iWagePricePercent", WagePricePercent));

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.ExecuteNonQuery();

                    for (int i = 0; i < arraySub.Count; i++)
                    {
                        Atend.Base.Base.BStatusWorkOrder status = new BStatusWorkOrder();
                        status.EquipStatusCode = Code;
                        status.WorkOrderCode = Convert.ToInt32(arraySub[i].ToString());

                        if (!status.InsertX())
                            throw new Exception("Failed");
                    }

                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                catch (System.Exception ex)
                {
                    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage(string.Format(" ERROR BEquipStatus.Update(transaction) {0}\n", ex.Message));
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }

                
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BEquipStatus.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteX(int Code)
        {
            SqlTransaction transaction;
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_EquipStatus_Delete";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BEquipStatus.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //frmDrawAirPost-frmEditDrawPole-frmDrawBranch-frmDrawDisconnector-frmDrawPole
        public static DataTable SelectAllX()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_EquipStatus_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        //frmDrawBus
        public static BEquipStatus SelectByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_EquipStatus_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            connection.Open();
            adapter.Fill(dsProduct);
            BEquipStatus product = new BEquipStatus();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                product.ACode = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ACode"].ToString());
                product.IsDefault = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsDefault"].ToString());
                product.PricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["PricePercent"].ToString());
                product.ExecutePricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["ExecutePricePercent"].ToString());
                product.WagePricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["WagePricePercent"].ToString());
            }
            else
            {
                product.Code = -1;
            }

            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandText = "B_Status_WorkOrder_SelectByStatusEquipCode";
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iEquipStatusCode", product.Code));
            SqlDataReader dr = adapter.SelectCommand.ExecuteReader();

            ArrayList a1 = new ArrayList();
            while (dr.Read())
            {
                a1.Add(dr["WorkOrderCode"].ToString());
            }
            dr.Close();
            product.ArraySub = a1;
            connection.Close();
            return product;
        }

        //AcDrawGroundPostShield
        public static BEquipStatus SelectByACode(int ACode)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_EquipStatus_SelectMyACode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iACode", ACode));
            DataSet dsProduct = new DataSet();
            connection.Open();
            adapter.Fill(dsProduct);
            BEquipStatus product = new BEquipStatus();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                product.ACode = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ACode"].ToString());
                product.IsDefault = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsDefault"].ToString());
                product.PricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["PricePercent"].ToString());
                product.ExecutePricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["ExecutePricePercent"].ToString());
                product.WagePricePercent = Convert.ToDouble(dsProduct.Tables[0].Rows[0]["WagePricePercent"].ToString());
            }
            else
            {
                product.Code = -1;
            }
            connection.Close();
            return product;
        }

        //Medhat
        public bool UnBindIsDedefault()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_EquipStatus_UpdateIsDefault";
            command.CommandType = CommandType.StoredProcedure;
            
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BEquipStatus.UnBindIsDedefault {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static DataTable Search(string Name)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_EquipStatus_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds.Tables[0];
        }


    }
}
