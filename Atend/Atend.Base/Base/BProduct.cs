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

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;



namespace Atend.Base.Base
{
    public class BProduct
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

        private byte unit;
        public byte Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        //private string number;

        //public string Number
        //{
        //    get { return number; }
        //    set { number = value; }
        //}

        private bool isProduct;
        public bool IsProduct
        {
            get { return isProduct; }
            set { isProduct = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private long price;
        public long Price
        {
            get { return price; }
            set { price = value; }
        }

        private long executePrice;
        public long ExecutePrice
        {
            get { return executePrice; }
            set { executePrice = value; }
        }

        private long wagePrice;
        public long WagePrice
        {
            get { return wagePrice; }
            set { wagePrice = value; }
        }


        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //~~~~~~~~~~~~~~~~~~~~~~~~Server Part~~~~~~~~~~~~~~~~~~//

        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_Product_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.Insert{0}\n", ex1.Message));
                connection.Close();
                return false;
            }


        }

        public bool Insert(SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand("B_Product_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
            //command.Transaction = ServerTransaction;
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BProduct.Insert{0}\n", ex1.Message));
                //connection.Close();
                return false;
            }


        }


        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Product_Update";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));

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
                ed.WriteMessage(string.Format(" ERROR BProduct.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //به روز رسانی فهرست بها
        public bool Update(SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            SqlConnection connection = ServerConnection;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Product_Update";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
            //command.Transaction = ServerTransaction;

            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BProduct.Update {0}\n", ex1.Message));
                //connection.Close();
                return false;
            }

        }


        public static bool Delete(int Code)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_Product_Delete";
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
                ed.WriteMessage(string.Format(" ERROR BProduct.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //Ribbon->btnTransferBProduct
        public static DataTable SelectAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            //ed.WriteMessage("FiNIsh sElectAll\n");
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll passed \n");
            return dsProduct.Tables[0];

        }

        public static DataTable SelectAllPoshtiban()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Operetions", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll passed ={0}\n",dsProduct.Tables[0].Rows.Count);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        //به روزرسانی فهرست بها
        public static DataTable SelectAllPoshtiban(int _type, SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                //ed.WriteMessage("#1\n");
                SqlConnection connection = ServerConnection;
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Operetions WHERE Type=@t", connection);

                SqlParameter type = new SqlParameter("@t", _type);
                adapter.SelectCommand.Parameters.Add(type);

                //adapter.SelectCommand.Transaction = ServerTransaction;

                adapter.SelectCommand.CommandType = CommandType.Text;
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //ed.WriteMessage("#2\n");
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public static DataTable SelectAllPoshtiban(int _type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Operetions WHERE Type=@t", connection);

                SqlParameter type = new SqlParameter("@t", _type);
                adapter.SelectCommand.Parameters.Add(type);

                adapter.SelectCommand.CommandType = CommandType.Text;
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        //Ribbon->btnTransferBProduct//به روزرسانی فهرست بها
        public static BProduct Select_ByCode(int Code, SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            SqlConnection connection = ServerConnection;
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            //adapter.SelectCommand.Transaction = ServerTransaction;

            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                //product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ID"].ToString());
                product.Price = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());
            }
            else
            {
                product.Code = -1;
            }
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByCode passed \n");
            return product;
        }

        public static BProduct Select_ByCode(int Code)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                //product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ID"].ToString());
                product.Price = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());
            }
            else
            {
                product.Code = -1;
            }
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByCode passed \n");
            return product;
        }

        public static DataTable Select_ByName(string Name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByName", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }


        //به روزرسانی فهرست بها
        public static DataTable SelectByType(int Type, SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            //string s = Atend.Control.ConnectionString.ServercnString;
            try
            {
                SqlConnection connection = ServerConnection;
                SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByType", connection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //adapter.SelectCommand.Transaction = ServerTransaction;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public static DataTable SelectByType(int Type)
        {
            //string s = Atend.Control.ConnectionString.ServercnString;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }


        public static DataTable Search(int Type, string Name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        //public static BProduct Select_ById(int ID)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectById", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", ID));
        //    DataSet dsProduct = new DataSet();
        //    adapter.Fill(dsProduct);
        //    BProduct product = new BProduct();
        //    if (dsProduct.Tables[0].Rows.Count > 0)
        //    {
        //        product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
        //        product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
        //        product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
        //        //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
        //        product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
        //        product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
        //        product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ID"].ToString());
        //        product.Price = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["Price"].ToString());
        //        product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
        //        product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());

        //    }
        //    return product;
        //}

        public static bool GetFromPoshtiban()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAll();
            DataTable dtAllPoshtiban = SelectAllPoshtiban();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                BProduct bp = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                //ed.WriteMessage("Up1\n");

                bp.ExecutePrice = Convert.ToInt64(dr["ExecutePrice"].ToString());
                bp.IsProduct = Convert.ToBoolean(dr["IsProduct"]);
                bp.Name = dr["Name"].ToString();
                bp.Price = Convert.ToInt64(dr["Price"].ToString());
                bp.Type = Convert.ToInt32(dr["Type"].ToString());
                bp.Unit = Convert.ToByte(dr["Unit"].ToString());
                bp.WagePrice = Convert.ToInt64(dr["WagePrice"].ToString());

                //ed.WriteMessage("Up2\n");
                if (bp.Code != -1)
                {
                    if (!bp.Update())
                    {
                        ed.WriteMessage("Up\n");
                        return false;
                    }
                }
                else
                {
                    bp.Code = Convert.ToInt32(dr["Code"].ToString());
                    if (!bp.Insert())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
                //}
            }
            return true;
        }

        //به روزرسانی فهرست بها
        public static bool GetFromPoshtiban(int Type, SqlConnection ServerConnection)//, SqlTransaction ServerTransaction)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            //DataTable dtAllServer = SelectByType(Type, ServerConnection, ServerTransaction);


            //ed.WriteMessage("@1\n");
            DataTable dtAllPoshtiban = SelectAllPoshtiban(Type, ServerConnection);//, ServerTransaction);
            //ed.WriteMessage("@2\n");

            //ed.WriteMessage("########### dtAllServer.Count:{0}\n",dtAllServer.Rows.Count);
            //ed.WriteMessage("########### dtAllPoshtiban.Count:{0}\n", dtAllPoshtiban.Rows.Count);
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                BProduct bp = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()), ServerConnection);//, ServerTransaction);
                //ed.WriteMessage("Up1\n");

                bp.ExecutePrice = Convert.ToInt64(dr["ExecutePrice"].ToString());
                bp.IsProduct = Convert.ToBoolean(dr["IsProduct"]);
                bp.Name = dr["Name"].ToString();
                bp.Price = Convert.ToInt64(dr["Price"].ToString());
                bp.Type = Convert.ToInt32(dr["Type"].ToString());
                bp.Unit = Convert.ToByte(dr["Unit"].ToString());
                bp.WagePrice = Convert.ToInt64(dr["WagePrice"].ToString());

                //ed.WriteMessage("Up2\n");
                if (bp.Code != -1)
                {
                    if (!bp.Update(ServerConnection))//, ServerTransaction))
                    {
                        //ed.WriteMessage("Up\n");
                        return false;
                    }
                }
                else
                {
                    bp.Code = Convert.ToInt32(dr["Code"].ToString());
                    if (!bp.Insert(ServerConnection))//, ServerTransaction))
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
            }
            //ed.WriteMessage("@3\n");
            return true;

        }

        public static bool GetFromPoshtiban(int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectByType(Type);
            DataTable dtAllPoshtiban = SelectAllPoshtiban(Type);
            //ed.WriteMessage("########### dtAllServer.Count:{0}\n",dtAllServer.Rows.Count);
            //ed.WriteMessage("########### dtAllPoshtiban.Count:{0}\n", dtAllPoshtiban.Rows.Count);
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                BProduct bp = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                //ed.WriteMessage("Up1\n");

                bp.ExecutePrice = Convert.ToInt64(dr["ExecutePrice"].ToString());
                bp.IsProduct = Convert.ToBoolean(dr["IsProduct"]);
                bp.Name = dr["Name"].ToString();
                bp.Price = Convert.ToInt64(dr["Price"].ToString());
                bp.Type = Convert.ToInt32(dr["Type"].ToString());
                bp.Unit = Convert.ToByte(dr["Unit"].ToString());
                bp.WagePrice = Convert.ToInt64(dr["WagePrice"].ToString());

                //ed.WriteMessage("Up2\n");
                if (bp.Code != -1)
                {
                    if (!bp.Update())
                    {
                        //ed.WriteMessage("Up\n");
                        return false;
                    }
                }
                else
                {
                    bp.Code = Convert.ToInt32(dr["Code"].ToString());
                    if (!bp.Insert())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
            }
            return true;

        }


        //*******************************************Local Part
        public bool InsertX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Product_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.Insert{0}\n", ex1.Message));
                connection.Close();
                return false;
            }


        }

        //به روز رسانی فهرست بها
        public bool InsertX(SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand("B_Product_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
            command.Transaction = LocalTransaction;
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BProduct.Insert{0}\n", ex1.Message));
                //connection.Close();
                return false;
            }


        }


        //MEDHAT
        public bool InsertProduct()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Product_InsertProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            Code = 0;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.InsertProduct{0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public bool UpdateX()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Product_Update";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.Update {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //Ribbon->btnTransferBProduct // به روز رسانی فهرست بها
        public bool UpdateXBYID(SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            SqlConnection connection = LocalConnection;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Product_UpdateByID";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
            command.Parameters.Add(new SqlParameter("iID", ID));
            command.Transaction = LocalTransaction;
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR BProduct.UpdateByID {0}\n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        public bool UpdateXBYID()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "B_Product_UpdateByID";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iUnit", Unit));
            //command.Parameters.Add(new SqlParameter("iNumber", Number));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
            command.Parameters.Add(new SqlParameter("iType", Type));
            command.Parameters.Add(new SqlParameter("iPrice", Price));
            command.Parameters.Add(new SqlParameter("iExecutePrice", ExecutePrice));
            command.Parameters.Add(new SqlParameter("iWagePrice", WagePrice));
            command.Parameters.Add(new SqlParameter("iID", ID));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.UpdateByID {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }


        //MEDHAT
        public bool UpdateProduct()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_Product_UpdateProduct";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iIsProduct", IsProduct));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.UpdateProduct {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteX(int Code)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_Product_Delete";
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
                ed.WriteMessage(string.Format(" ERROR BProduct.Delete {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        public static bool DeleteXByID(int ID)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "B_Product_DeleteByID";
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iID", ID));
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
                ed.WriteMessage(string.Format(" ERROR BProduct.DeleteByID {0}\n", ex1.Message));
                connection.Close();
                return false;
            }

        }

        //Use In FrmOperation02
        public static DataTable SelectAllX()//Select Operation
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            int code = 0;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        //Ribbon->btnTransferBProduct
        public static BProduct Select_ByXCode(int Code, SqlConnection LocalConnection, SqlTransaction LocalTransaction)
        {
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByXCode Start \n");
            SqlConnection connection = LocalConnection;
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            adapter.SelectCommand.Transaction = LocalTransaction;
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Id"].ToString());
                product.Price = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());

            }
            else
            {
                product.Code = -1;
            }
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByXCode passed \n");
            return product;
        }

        public static BProduct Select_ByXCode(int Code)
        {
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByXCode Start \n");
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Id"].ToString());
                product.Price = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());

            }
            else
            {
                product.Code = -1;
            }
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Select_ByXCode passed \n");
            return product;
        }

        public static DataTable Select_ByNameX(string Name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByName", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        //GroundPost //Ribbon->btnTransferBProduct
        public static DataTable SelectByTypeX(int Type)
        {
            //string s = Atend.Control.ConnectionString.LocalcnString;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }

        public static DataTable SelectByNameTypeX(int Type, string Name)
        {
            string s = Atend.Control.ConnectionString.LocalcnString;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByNameType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }

        public static DataTable SelectByUniqueNameTypeX(int Type, string Name)
        {
            string s = Atend.Control.ConnectionString.LocalcnString;

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectByUniqueNameType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }

        public static DataTable SearchX(int Type, string Name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_Search", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iName", Name));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static BProduct Select_ByIdX(int ID)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectById", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", ID));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                //ed.WriteMessage("NAme={0} : {1} \n", product.Name, ID);

                product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ID"].ToString());
                product.Price = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());
                //ed.WriteMessage("Product\n");
            }
            else
            {
                product.Code = -1;
            }
            //ed.WriteMessage("Product.Count=0\n", dsProduct.Tables[0].Rows.Count);
            //ed.WriteMessage("NAme={0}\n",product.Name);
            return product;
        }

        //StatusReport
        public static BProduct Select_ByIdX(int ID, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("B_Product_SelectById", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", ID));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            BProduct product = new BProduct();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                product.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                product.IsProduct = Convert.ToBoolean(dsProduct.Tables[0].Rows[0]["IsProduct"].ToString());
                product.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                //product.Number = dsProduct.Tables[0].Rows[0]["Number"].ToString();
                product.Unit = Convert.ToByte(dsProduct.Tables[0].Rows[0]["Unit"].ToString());
                product.Type = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Type"].ToString());
                //ed.WriteMessage("NAme={0} : {1} \n", product.Name, ID);

                product.ID = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["ID"].ToString());
                product.Price = Convert.ToInt32(dsProduct.Tables[0].Rows[0]["Price"].ToString());
                product.ExecutePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["ExecutePrice"].ToString());
                product.WagePrice = Convert.ToInt64(dsProduct.Tables[0].Rows[0]["WagePrice"].ToString());
                //ed.WriteMessage("Product\n");
            }
            else
            {
                product.Code = -1;
            }
            //ed.WriteMessage("Product.Count=0\n", dsProduct.Tables[0].Rows.Count);
            //ed.WriteMessage("NAme={0}\n",product.Name);
            return product;
        }

        //Ribbon->btnTransferBProduct
        public static bool GetFromServer()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAll();
            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllServer.Rows)
            {
                BProduct bp = Select_ByXCode(Convert.ToInt32(dr["Code"].ToString()));
                //ed.WriteMessage("Up1\n");
                //if (bp.Code != -1)
                //{
                BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                //ed.WriteMessage("Up2\n");
                if (bp.Code != -1)
                {
                    b.ID = bp.ID;
                    if (!b.UpdateXBYID())
                    {
                        // ed.WriteMessage("UpDAte\n");
                        return false;
                    }
                }
                else
                {
                    if (!b.InsertX())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
                //}
            }

            if (!Atend.Base.Base.BRegion.GetFromServer())
            {
                ed.WriteMessage("انتقال اطلاعات Regionبا موفقیت انجام نشد");
                return false;
            }
            if (!Atend.Base.Base.BUnit.GetFromServer())
            {
                ed.WriteMessage("انتقال اطلاعات Regionبا موفقیت انجام نشد");
                return false;
            }
            //ed.WriteMessage("GoToRamp\n");
            if (!Atend.Base.Equipment.ERamp.GetFromBProductLocal())
            {
                ed.WriteMessage("انتقال اطلاعات پیچ با موفقیت انجام نشد\n");
                return false;
            }
            else
            {
                ed.WriteMessage("انتقال اطلاعات پیچ با موفقیت انجام شد\n");
            }
            ed.WriteMessage("\nHello444\n");

            if (!Atend.Base.Equipment.EProp.GetFromBProductLocal())
            {
                ed.WriteMessage("انتقال اطلاعات تسمه حائل با موفقیت انجام نشد\n");
                return false;
            }
            else
            {
                ed.WriteMessage("انتقال اطلاعات تسمه حائل با موفقیت انجام شد\n");
            }


            //if (!Atend.Base.Equipment.EHalter.GetFromBProductLocal())
            //{
            //    ed.WriteMessage("انتقال اطلاعات مهار با موفقیت انجام نشد\n");
            //    return false;
            //}
            //else
            //{
            //    ed.WriteMessage("انتقال اطلاعات مهار با موفقیت انجام شد\n");
            //}
            //ed.WriteMessage("\nHello555\n");
            if (!Atend.Base.Equipment.EInsulatorPipe.GetFromBProductLocal())
            {
                ed.WriteMessage("انتقال اطلاعات پایه مقره با موفقیت انجام نشد\n");
                return false;
            }
            else
            {
                ed.WriteMessage("انتقال اطلاعات پایه مقره با موفقیت انجام شد\n");
            }


            //if (!Atend.Base.Equipment.EInsulatorChain.GetFromBProductLocal())
            //{
            //    ed.WriteMessage("انتقال اطلاعات میل مقره با موفقیت انجام نشد\n");
            //    return false;
            //}
            //else
            //{
            //    ed.WriteMessage("انتقال اطلاعات میل مقره با موفقیت انجام شد\n");
            //}

            return true;
        }

        //به روزرسانی فهرست بها
        public static bool GetFromServer(int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


            SqlConnection LocalConnection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlTransaction LocalTransaction;

            SqlConnection ServerConnection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            //SqlTransaction ServerTransaction;


            try
            {
                LocalConnection.Open();
                ServerConnection.Open();

                try
                {
                    LocalTransaction = LocalConnection.BeginTransaction();
                    //ServerTransaction = ServerConnection.BeginTransaction();
                 
                    if (GetFromPoshtiban(Type, ServerConnection))//, ServerTransaction))
                    {
                        //ed.WriteMessage("3\n");
                        DataTable dtAllServer = SelectByType(Type, ServerConnection);// ServerTransaction);
                        //ed.WriteMessage("4\n");

                        //ed.WriteMessage("@@@ dtAllServer.Count:{0}\n", dtAllServer.Rows.Count);
                        foreach (DataRow dr in dtAllServer.Rows)
                        {
                            BProduct bp = Select_ByXCode(Convert.ToInt32(dr["Code"].ToString()), LocalConnection, LocalTransaction);//Local
                            BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()), ServerConnection);//, ServerTransaction);//Server
                            if (bp.Code != -1)
                            {
                                b.ID = bp.ID;
                                if (!b.UpdateXBYID(LocalConnection, LocalTransaction))
                                {
                                    throw new System.Exception("Erorr in GetFromPoshtiban.b.UpdateXBYID");
                                }
                            }
                            else
                            {
                                if (!b.InsertX(LocalConnection, LocalTransaction))
                                {
                                    throw new System.Exception("Erorr in GetFromPoshtiban.b.InsertX");
                                }
                            }
                        }

                    }
                    else
                    {
                        throw new System.Exception("Erorr in GetFromPoshtiban");
                        //ed.WriteMessage("Erorr in GetFromPoshtiban\n");
                        //return false;
                    }

                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("ERROR GetFromServer : {0} \n", ex.Message);

                    LocalTransaction = LocalConnection.BeginTransaction();
                    //ServerTransaction = ServerConnection.BeginTransaction();


                    ServerConnection.Close();
                    LocalConnection.Close();
                    return false;

                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage("ERROR GetFromServer : {0} \n", ex1.Message);

                ServerConnection.Close();
                LocalConnection.Close();
                return false;
            }
            //ed.WriteMessage("9\n");
            //ServerTransaction.Commit();
            LocalTransaction.Commit();

            ServerConnection.Close();
            LocalConnection.Close();
            ed.WriteMessage("10\n");
            return true;

        }

    }
}
