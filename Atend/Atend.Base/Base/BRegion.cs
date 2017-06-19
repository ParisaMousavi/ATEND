using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using Atend.Control;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Base
{
    public class BRegion
    {
        public BRegion()
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

        private string secondCode;

        public string SecondCode
        {
            get { return secondCode; }
            set { secondCode = value; }
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~SERVER Part
        //Hatami
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_Region_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR BRegionInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }
        //HAtami
        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_Region_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));

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
                ed.WriteMessage(string.Format(" ERROR Bregion Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }
        //HAtami
        public static DataTable SelectAllServer()
        {
            try
            {
                string seCode = "-1";
                SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectBySecondCode", connection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add(new SqlParameter("iSecondCode", seCode));
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

        public static BRegion SelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BRegion Region = new BRegion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Region.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Region.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                Region.SecondCode = dsProduct.Tables[0].Rows[0]["SecondCode"].ToString();
            }
            else
            {
                Region.Code = -1;
            }

            return Region;
        }

        //Hatami
        public static BRegion Select_BySecondCode(string SecondCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectBySecondCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BRegion Region = new BRegion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Region.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Region.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                Region.SecondCode = dsProduct.Tables[0].Rows[0]["SecondCode"].ToString();
            }
            else
            {
                Region.Code = -1;
            }

            return Region;
        }
        //Hatami
        public static DataTable SelectAllPoshtiban()
        {
            try
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
                SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Regions", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll Unit passed={0} \n",dsProduct.Tables[0].Rows.Count);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }
        //Hatamiاتقال اطلاعات از پشتیبان به سرور 
        public static bool GetFromPoshtiban()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAllServer();
            DataTable dtAllPoshtiban = SelectAllPoshtiban();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                BRegion region = Select_BySecondCode(dr["SecondCode"].ToString());
                //ed.WriteMessage("Up1\n");
                //if (bp.Code != -1)
                //{
                //BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                region.Name = dr["Name"].ToString();

                //ed.WriteMessage("Up2\n");
                if (region.Code != -1)
                {
                    if (!region.Update())
                    {
                        //ed.WriteMessage("Up\n");
                        return false;
                    }
                }
                else
                {
                    region.SecondCode = dr["SecondCode"].ToString();
                    if (!region.Insert())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
                //}
            }
            return true;

        }





        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~LocalPart~~~~~~~~~~~~~
        public bool InsertLocal()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Region_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));
            command.Parameters.Add(new SqlParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR BRegionInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }


        public bool UpdateLocal()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Region_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iName", Name));
            command.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));

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
                ed.WriteMessage(string.Format(" ERROR Bregion Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static DataTable SelectAllLocal()
        {
            string seCode = "-1";
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectBySecondCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iSecondCode", seCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static BRegion SelectByCodeLoacal(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BRegion Region = new BRegion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Region.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Region.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                Region.SecondCode = dsProduct.Tables[0].Rows[0]["SecondCode"].ToString();
            }
            else
            {
                Region.Code = -1;
            }

            return Region;
        }

        public static BRegion Select_BySecondCodeLocal(string SecondCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Region_SelectBySecondCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iSecondCode", SecondCode));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BRegion Region = new BRegion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Region.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Region.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
                Region.SecondCode = dsProduct.Tables[0].Rows[0]["SecondCode"].ToString();
                ed.WriteMessage("Code:{0} Name:{1} SecondCode:{2} \n", Region.Code, Region.Name, Region.SecondCode);
            }
            else
            {
                Region.Code = -1;
            }

            return Region;
        }
        //HATAMI انتقال اطلاعات از سرور به  پایگاه داده محلی 
        public static bool GetFromServer()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAllServer();
            DataTable dtAllLocal = SelectAllLocal();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllServer.Rows)
            {
                BRegion region = Select_BySecondCodeLocal(dr["SecondCode"].ToString());
                //ed.WriteMessage("Up1\n");
                //if (bp.Code != -1)
                //{
                //BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                region.Name = dr["Name"].ToString();

                //ed.WriteMessage("Up2\n");
                if (region.Code != -1)
                {
                    if (!region.UpdateLocal())
                    {
                        //ed.WriteMessage("Up\n");
                        return false;
                    }
                }
                else
                {
                    region.SecondCode = dr["SecondCode"].ToString();
                    if (!region.InsertLocal())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                }
                //}
            }
            return true;

        }



    }
}
