using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DDesign
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string archiveNo;
        public string ArchiveNo
        {
            get { return archiveNo; }
            set { archiveNo = value; }
        }

        private byte region;
        public byte Region
        {
            get { return region; }
            set { region = value; }
        }

        string atendCoed;
        public string AtendCoed
        {
            get { return atendCoed; }
            set { atendCoed = value; }
        }

        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        long prgCode;
        public long PRGCode
        {
            get { return prgCode; }
            set { prgCode = value; }
        }

        long workOrder;
        public long WorkOrder
        {
            get { return workOrder; }
            set { workOrder = value; }
        }

        int requestType;
        public int RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        int additionalCode;
        public int AdditionalCode
        {
            get { return additionalCode; }
            set { additionalCode = value; }
        }

        private bool isAtend;

        public bool IsAtend
        {
            get { return isAtend; }
            set { isAtend = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public bool Insert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_Design_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iTitle", Title));
            command.Parameters.Add(new SqlParameter("iArchiveNo", ArchiveNo));
            command.Parameters.Add(new SqlParameter("iPRGCode", PRGCode));
            command.Parameters.Add(new SqlParameter("iRequestType", RequestType));
            command.Parameters.Add(new SqlParameter("iRegion", Region));
            command.Parameters.Add(new SqlParameter("iIsAtend", IsAtend));
            command.Parameters.Add(new SqlParameter("iAddress", Address));


            try
            {
                Connection.Open();
                Id = Convert.ToInt32(command.ExecuteScalar());
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR DDesign.Insert {0}\n", ex1.Message));
                Connection.Close();
                return false;
            }

        }

        public bool Update()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_Design_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iId", Id));
            command.Parameters.Add(new SqlParameter("iTitle", Title));
            command.Parameters.Add(new SqlParameter("iArchiveNo", ArchiveNo));
            command.Parameters.Add(new SqlParameter("iPRGCode", PRGCode));
            command.Parameters.Add(new SqlParameter("iRequestType", RequestType));
            command.Parameters.Add(new SqlParameter("iRegion", Region));

            command.Parameters.Add(new SqlParameter("iIsAtend", IsAtend));
            command.Parameters.Add(new SqlParameter("iAddress", Address));

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch(System.Exception ex)
            {
                ed.WriteMessage("Error In Design.Update={0}\n",ex.Message);
                Connection.Close();
                return false;
            }

        }

        public bool Update(SqlTransaction _transction , SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            SqlConnection Connection = _connection;
            SqlCommand command = new SqlCommand("D_Design_Update", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transction;
            command.Parameters.Add(new SqlParameter("iId", Id));
            command.Parameters.Add(new SqlParameter("iTitle", Title));
            command.Parameters.Add(new SqlParameter("iArchiveNo", ArchiveNo));
            command.Parameters.Add(new SqlParameter("iPRGCode", PRGCode));
            command.Parameters.Add(new SqlParameter("iRequestType", RequestType));
            command.Parameters.Add(new SqlParameter("iRegion", Region));

            command.Parameters.Add(new SqlParameter("iIsAtend", IsAtend));
            command.Parameters.Add(new SqlParameter("iAddress", Address));

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error In Design.Update={0}\n", ex.Message);
                return false;
            }

        }


        public static DDesign SelectByProjectCode(int ProjectCode)
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_Design_SelectByProjectcode", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            command.Parameters.Add(new SqlParameter("iprojectCode", ProjectCode));
            SqlDataReader reader = command.ExecuteReader();
            DDesign design = new DDesign();
            if (reader.Read())
            {
                design.Address = reader["Address"].ToString();
                design.ArchiveNo = reader["ArchiveNo"].ToString();
                design.Id = Convert.ToInt32(reader["ID"].ToString());
                design.IsAtend = Convert.ToBoolean(reader["IsAtend"]);
                design.PRGCode = Convert.ToInt64(reader["PRGcode"].ToString());
                design.Region = Convert.ToByte(reader["Region"].ToString());
                design.RequestType = Convert.ToInt32(reader["RequestType"].ToString());
                design.Title = reader["Title"].ToString();
                //design.WorkOrder = Convert.ToInt64(reader["WorkOrder"].ToString());
            }
            else
            {
                design.Id = -1;
            }
            reader.Close();
            connection.Close();
            return design;
        }

        public static DataTable SelectAllServer()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_SelectByID", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iID", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            //ed.WriteMessage("FiNIsh sElectAll\n");
            //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll Design Server passed \n");
            return dsProduct.Tables[0];

        }

        public static DataTable SelectAllPoshtiban()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            try
            {
                SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Projects", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                //adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll P-Project Poshtiban passed ={0}\n", dsProduct.Tables[0].Rows.Count);
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public static DataTable SelectAllCompliment()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            try
            {
                SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Project_Defined_Compliments", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //ed.WriteMessage("FiNIsh sElectAll\n");
                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll Compliment Server passed \n");
                return dsProduct.Tables[0];
            }
            catch
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable SelectAllWorkOrder()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("Start SelectAll:Conection={0}\n", ConnectionString.ServercnString);
            try
            {
                SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
                //string strSelect = string.Format("Select * From P_Project_WorkOrders where PRGCode={0}",prgCode);
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From P_Project_WorkOrders", connection);
                adapter.SelectCommand.CommandType = CommandType.Text;
                DataSet dsProduct = new DataSet();
                adapter.Fill(dsProduct);
                //ed.WriteMessage("FiNIsh sElectAll\n");
                //Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SelectAll WorkOrder Poshtiban passed \n");
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
            //ed.WriteMessage("GoTo SelectServer\n");
            DataTable dtAllServer = SelectAllServer();
            //ed.WriteMessage("Go Select Poshtiban\n");
            DataTable dtAllPoshtiban = SelectAllPoshtiban();

            //ed.WriteMessage("Count={0}\n", dtAllServer.Rows.Count);
            //ed.WriteMessage("1\n");
            foreach (DataRow dr in dtAllPoshtiban.Rows)
            {
                DDesign  design = SelectByProjectCode(Convert.ToInt32(dr["PRGCode"].ToString()));
                design.Address = "";
                design.ArchiveNo = dr["ArchiveNo"].ToString();
                design.IsAtend = false;
                design.PRGCode = Convert.ToInt64(dr["PRGCode"].ToString());
                design.Region = Convert.ToByte(dr["Region"].ToString());
                design.Title = dr["Title"].ToString();
                DataRow[] drRequestType = SelectAllCompliment().Select(string.Format("PRGCode={0}",dr["PRGCode"].ToString()));
                
                if (drRequestType.Length != 0)
                {
                    design.RequestType = Convert.ToInt32(drRequestType[0]["RequestType"].ToString());
                }
                else
                {
                    design.RequestType = 0;
                }
                DataRow[] drWorkOrderPoshtiban = SelectAllWorkOrder().Select(string.Format("PRGCode={0}",dr["PRGCode"].ToString()));
                //ed.WriteMessage("Design.Id={0}\n",design.Id);
                if (design.Id != -1)
                {
                    //ed.WriteMessage("GOTO Upadte Design\n");
                    if (!design.Update())
                    {
                        //ed.WriteMessage("Up\n");
                        return false;
                    }
                    else
                    {
                        //ed.WriteMessage("Update\n");
                        if (Atend.Base.Design.DWorkOrder.Delete(design.Id))
                        {
                            foreach(DataRow drWorkOrder in drWorkOrderPoshtiban)
                            {
                                Atend.Base.Design.DWorkOrder dWorkOrder = new DWorkOrder();
                                dWorkOrder.DesignID = design.Id;
                                //ed.WriteMessage("WORKORDERCODE={0}\n", drWorkOrder["WorkOrderCode"].ToString());
                                dWorkOrder.WorkOrder = Convert.ToInt32(drWorkOrder["WorkOrderCode"].ToString());
                                if (!dWorkOrder.Insert())
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    design.PRGCode = Convert.ToInt32(dr["PRGCode"].ToString());
                    if (!design.Insert())
                    {
                        //ed.WriteMessage("Insert\n");
                        return false;
                    }
                    else
                    {
                        foreach (DataRow drWorkOrder in drWorkOrderPoshtiban)
                        {
                            Atend.Base.Design.DWorkOrder dWorkOrder = new DWorkOrder();
                            dWorkOrder.DesignID = design.Id;
                            dWorkOrder.WorkOrder = Convert.ToInt32(drWorkOrder["WorkOrderCode"].ToString());
                            if (!dWorkOrder.Insert())
                            {
                                return false;
                            }
                        }
                    }
                }

                //ed.WriteMessage("6\n");
                //}
            }
            return true;
        }









        //frmDesignSaveServer
        public bool Insert(SqlTransaction _transaction, SqlConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection Connection = _connection;
            SqlCommand command = new SqlCommand("D_Design_Insert", Connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new SqlParameter("iTitle", Title));
            command.Parameters.Add(new SqlParameter("iArchiveNo", ArchiveNo));
            command.Parameters.Add(new SqlParameter("iPRGCode", PRGCode));
            command.Parameters.Add(new SqlParameter("iRequestType", RequestType));
            command.Parameters.Add(new SqlParameter("iRegion", Region));
            command.Parameters.Add(new SqlParameter("iIsAtend", IsAtend));
            command.Parameters.Add(new SqlParameter("iAddress", Address));

            try
            {
                Id = Convert.ToInt32(command.ExecuteScalar());
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format(" ERROR DDesign.Insert {0}\n", ex1.Message));
                return false;
            }

        }

        public bool UpdateAdditionalCodeByCode(int _Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand command = new SqlCommand("D_Design_UpdateByAdditionalCode", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("iCode", _Code));
            command.Parameters.Add(new SqlParameter("iAdditionalCode", AdditionalCode));

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                Connection.Close();
                return false;
            }

        }


        public static bool Delete(int ID)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand Command = new SqlCommand("D_Design_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new SqlParameter("iId", ID));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                Connection.Close();
                return false;
            }
        }

        //public static DDesign SelectByCode(int Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand sqlCommand = new SqlCommand("D_Design_SelectByCode", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    sqlCommand.Parameters.Add(new SqlParameter("iCode", Code));
        //    Connection.Open();
        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    DDesign design = new DDesign();
        //    if (reader.Read())
        //    {
        //        design.Id = Convert.ToInt32(reader["Id"]);
        //        design.Code = Convert.ToInt32(reader["Code"].ToString());
        //        design.Title = reader["Title"].ToString();
        //        design.ArchiveNo = reader["ArchiveNo"].ToString();
        //        design.PRGCode = Convert.ToInt64(reader["PRGCode"].ToString());
        //        design.WorkOrder = Convert.ToInt64(reader["WorkOrder"].ToString());
        //        design.RequestType = Convert.ToInt32(reader["RequestType"].ToString());
        //        design.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
        //    }
        //    else
        //    {
        //        design.Code = -1;
        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return design;
        //}


        //frmDesignSearch
        public static DataTable SelectByCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_SelectByCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsDesign = new DataSet();
            adapter.Fill(dsDesign);
            return dsDesign.Tables[0];
        }

        //frmSaveDesignServer
        public static DataTable SelectByCode(SqlTransaction _transaction, SqlConnection _connection, int Code)
        {
            SqlConnection Connection = _connection;
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_SelectByCode", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsDesign = new DataSet();
            adapter.Fill(dsDesign);
            return dsDesign.Tables[0];
        }


        //public static int MaxId()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand sqlCommand = new SqlCommand("D_Design_MaxId", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;

        //    Connection.Open();
        //    int ID = Convert.ToInt32(sqlCommand.ExecuteScalar());
        //    Connection.Close();
        //    return ID;
        //}

        public static DDesign SelectByID(int ID)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("D_Design_SelectById", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iId", ID));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DDesign design = new DDesign();
            if (reader.Read())
            {
                //design.Code = Convert.ToInt32(reader["Code"].ToString());
                design.Title = reader["Title"].ToString();
                design.ArchiveNo = reader["ArchiveNo"].ToString();
                design.PRGCode = Convert.ToInt64(reader["PRGCode"].ToString());
                //design.WorkOrder = Convert.ToInt64(reader["WorkOrder"].ToString());
                design.RequestType = Convert.ToInt32(reader["RequestType"].ToString());
                design.Id = Convert.ToInt32(reader["Id"]);
                design.IsAtend = Convert.ToBoolean(reader["IsAtend"]);
                design.Address = reader["Address"].ToString();
                design.Region = Convert.ToByte(reader["Region"].ToString());
            }
            else
            {
                design.Id = -1;
            }
            reader.Close();
            Connection.Close();
            return design;
        }

        //frmSaveDesignServer
        public static DDesign SelectByID(SqlTransaction _transaction, SqlConnection _connection, int ID)
        {
            SqlConnection Connection = _connection;
            SqlCommand sqlCommand = new SqlCommand("D_Design_SelectById", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;

            sqlCommand.Parameters.Add(new SqlParameter("iId", ID));
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DDesign design = new DDesign();
            if (reader.Read())
            {

                //design.Code = Convert.ToInt32(reader["Code"].ToString());
                design.Title = reader["Title"].ToString();
                design.ArchiveNo = reader["ArchiveNo"].ToString();
                design.PRGCode = Convert.ToInt64(reader["PRGCode"].ToString());
                //design.WorkOrder = Convert.ToInt64(reader["WorkOrder"].ToString());
                design.RequestType = Convert.ToInt32(reader["RequestType"].ToString());
                design.Id = Convert.ToInt32(reader["Id"]);
                //design.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
                design.IsAtend = Convert.ToBoolean(reader["IsAtend"]);
                design.Address = reader["Address"].ToString();
                design.Region =Convert.ToByte(reader["Region"].ToString());
            }
            else
            {
                design.Id = -1;
            }
            reader.Close();
            return design;
        }


        public static DDesign SelectByIDAndRequestType(int ID, int RequestType)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("D_Design_SelectByIDAndRequestType", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("iId", ID));
            sqlCommand.Parameters.Add(new SqlParameter("iRequestType", RequestType));
            Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            DDesign design = new DDesign();
            if (reader.Read())
            {
                design.Code = Convert.ToInt32(reader["Code"].ToString());
                design.Title = reader["Title"].ToString();
                design.ArchiveNo = reader["ArchiveNo"].ToString();
                design.PRGCode = Convert.ToInt64(reader["PRGCode"].ToString());
                design.WorkOrder = Convert.ToInt64(reader["WorkOrder"].ToString());
                design.RequestType = Convert.ToInt32(reader["RequestType"].ToString());
                design.Id = Convert.ToInt32(reader["Id"]);
                design.AdditionalCode = Convert.ToInt32(reader["AdditionalCode"].ToString());
            }
            else
            {
                design.Id = -1;
            }
            reader.Close();
            Connection.Close();
            return design;
        }

        public static DataTable SelectByArchiveNo(string ArchiveNo)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_SelectByArchiveNo", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iArchiveNo", ArchiveNo));
            DataSet dsDesign = new DataSet();
            adapter.Fill(dsDesign);
            return dsDesign.Tables[0];
        }

        //frmDesignSearch
        public static DataTable SelectAll()
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_SelectById", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iId", -1));
            DataSet dsDesign = new DataSet();
            adapter.Fill(dsDesign);
            return dsDesign.Tables[0];
        }

        //public static int SelectMaxId()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
        //    SqlCommand sqlCommand = new SqlCommand("D_Design_SelectTopId", Connection);
        //    sqlCommand.CommandType = CommandType.StoredProcedure;
        //    Connection.Open();
        //    int max = Convert.ToInt32(sqlCommand.ExecuteScalar());
        //    Connection.Close();
        //    return max + 1;
        //}

        public static DataTable Search(string Number)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_Design_Search", Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iArchiveNo", Number));
            DataSet dsDesign = new DataSet();
            adapter.Fill(dsDesign);
            return dsDesign.Tables[0];
        }

        public static int SelectMaxAdditionalCode(int Code)
        {
            SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlCommand sqlCommand = new SqlCommand("D_Design_SelectMaxAdditionalCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            int max = Convert.ToInt32(sqlCommand.ExecuteScalar());
            Connection.Close();
            return max;
        }





    }
}
