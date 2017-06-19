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
    public class BUnit
    {
        public BUnit()
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~SERVER Part
        public bool Insert()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_Unit_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BUnitInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool Update()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("B_Unit_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BUnit Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static DataTable SelectAllServer()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static BUnit Select_ByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BUnit Unit = new BUnit();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Unit.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Unit.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
            }
            else
            {
                Unit.Code = -1;
            }

            return Unit;

        }

        public static DataTable SelectAllPoshtiban()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Units", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll Unit passed={0} \n", dsProduct.Tables[0].Rows.Count);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }
        public static bool GetFromPoshtiban()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAllServer();
            DataTable dtAllPoshtiban = SelectAllPoshtiban();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                if (!Convert.ToBoolean(dr["IsDeleted"]))
                {
                    BUnit unit = Select_ByCode(Convert.ToInt16(dr["Code"].ToString()));
                    //ed.WriteMessage("Up1\n");
                    //if (bp.Code != -1)
                    //{
                    //BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                    unit.Name = dr["Name"].ToString();

                    //ed.WriteMessage("Up2\n");
                    if (unit.Code != -1)
                    {
                        if (!unit.Update())
                        {
                            //ed.WriteMessage("Up\n");
                            return false;
                        }
                    }
                    else
                    {
                        unit.Code = Convert.ToInt16(dr["Code"].ToString());
                        if (!unit.Insert())
                        {
                            //ed.WriteMessage("Insert\n");
                            return false;
                        }
                    }
                }
                //}
            }
            return true;

        }




        public static BUnit Select_ByProductID(int ID)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_SelectByProductID", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", ID));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BUnit Unit = new BUnit();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Unit.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Unit.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
            }
            return Unit;

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Local PART

        public bool InsertLocal()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Unit_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BUnitInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public bool UpdateLocal()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlCommand command = new SqlCommand("B_Unit_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BUnit Update {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        public static DataTable SelectAllLocal()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        public static BUnit Select_ByCodeLocal(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BUnit Unit = new BUnit();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Unit.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Unit.Name = dsProduct.Tables[0].Rows[0]["Name"].ToString();
            }
            else
            {
                Unit.Code = -1;
            }

            return Unit;

        }

       
        public static bool GetFromServer()
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            DataTable dtAllServer = SelectAllServer();
            DataTable dtAllLocal = SelectAllLocal();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            foreach (DataRow dr in dtAllServer.Rows)
            {
               
                    BUnit unit = Select_ByCodeLocal(Convert.ToInt16(dr["Code"].ToString()));
                    //ed.WriteMessage("Up1\n");
                    //if (bp.Code != -1)
                    //{
                    //BProduct b = Select_ByCode(Convert.ToInt32(dr["Code"].ToString()));
                    unit.Name = dr["Name"].ToString();

                    //ed.WriteMessage("Up2\n");
                    if (unit.Code != -1)
                    {
                        if (!unit.UpdateLocal())
                        {
                            //ed.WriteMessage("Up\n");
                            return false;
                        }
                    }
                    else
                    {
                        unit.Code = Convert.ToInt16(dr["Code"].ToString());
                        if (!unit.InsertLocal())
                        {
                            //ed.WriteMessage("Insert\n");
                            return false;
                        }
                    }
               
                //}
            }
            return true;

        }




      

        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

            OleDbCommand command = new OleDbCommand("B_Unit_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR BUnit AccessInsert {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }


        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand();
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "B_Unit_Update";
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iName", Name));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BUnit.Update {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }

        //}

        public bool AccessUpdate()
        {
            OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

            OleDbCommand command = new OleDbCommand("B_Unit_Update", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iName", Name));
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
                ed.WriteMessage(string.Format(" ERROR BUnit.AccessUpdate {0}\n", ex1.Message));

                connection.Close();
                return false;
            }

        }

        //public static bool Delete(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand();
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "B_Unit_Delete";
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //        ed.WriteMessage(string.Format(" ERROR BUnit.Delete {0}\n", ex1.Message));

        //        connection.Close();
        //        return false;
        //    }



        //}


        public static bool AccessDelete(int Code)
        {
            OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

            OleDbCommand command = new OleDbCommand("B_Unit_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
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
                ed.WriteMessage(string.Format(" ERROR BUnit.AccessDelete {0}\n", ex1.Message));

                connection.Close();
                return false;
            }



        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];
        }

        //public static BUnit Select(int Code)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("B_Unit_Select", connection);
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = db.GetStoredProcCommand("B_Unit_Select");
        //    db.AddInParameter(dbCommand, "iCode", DbType.Int16, Code);
        //    DataSet dsUnit = db.ExecuteDataSet(dbCommand);
        //    BUnit unit = new BUnit();
        //    if (dsUnit.Tables[0].Rows.Count > 0)
        //    {
        //        unit.Code = Convert.ToInt32(dsUnit.Tables[0].Rows[0]["Code"].ToString());
        //        unit.Name = dsUnit.Tables[0].Rows[0]["Name"].ToString();
        //    }
        //    return unit;
        //}

        //public static DataTable SelectAll()
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = db.GetStoredProcCommand("B_Unit_Select");
        //    db.AddInParameter(dbCommand, "iCode", DbType.Int16, -1);
        //    DataSet dsUnit = db.ExecuteDataSet(dbCommand);
        //    return dsUnit.Tables[0];
        //}

    }
}
