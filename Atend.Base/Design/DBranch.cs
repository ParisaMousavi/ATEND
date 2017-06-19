using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DBranch
    {

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid leftNodeCode;
        public Guid LeftNodeCode
        {
            get { return leftNodeCode; }
            set { leftNodeCode = value; }
        }

        private Guid rightNodeCode;
        public Guid RightNodeCode
        {
            get { return rightNodeCode; }
            set { rightNodeCode = value; }
        }

        private double lenght;
        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private double sag;
        public double Sag
        {
            get { return sag; }
            set { sag = value; }
        }

        private int productCode;
        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private int productType;
        public int ProductType
        {
            get { return productType; }
            set { productType = value; }
        }

        private bool isWeek;
        public bool IsWeek
        {
            get { return isWeek; }
            set { isWeek = value; }
        }

        private int isExist;
        public int IsExist
        {
            get { return isExist; }
            set { isExist = value; }
        }

        int projectCode;
        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }


        //public bool Insert()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlTransaction Transaction = MyTransaction;
        //    SqlCommand Command = new SqlCommand("D_Branch_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;

        //    code = Guid.NewGuid();
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iLenght", Lenght));
        //    Command.Parameters.Add(new SqlParameter("iOrder", Order));
        //    Command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iSag", Sag));
        //    Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    Command.Parameters.Add(new SqlParameter("iIsWeek", IsWeek));
        //    Command.Parameters.Add(new SqlParameter("iIsExist", IsExist));

        //    try
        //    {

        //        Connection.Open();
        //        //Command.Transaction = MyTransaction;
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        //ed.writeMessage("Branch insertted successfully \n");
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DBranch.Insert : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool InsertX()
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlTransaction Transaction = MyTransaction;
        //    SqlCommand Command = new SqlCommand("D_Branch_InsertX", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;

        //    code = Guid.NewGuid();
        //    Command.Parameters.Add(new SqlParameter("iXCode", XCode));
        //    Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iLenght", Lenght));
        //    Command.Parameters.Add(new SqlParameter("iOrder", Order));
        //    Command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iSag", Sag));
        //    Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    Command.Parameters.Add(new SqlParameter("iIsWeek", IsWeek));
        //    Command.Parameters.Add(new SqlParameter("iIsExist", IsExist));

        //    try
        //    {

        //        Connection.Open();
        //        //Command.Transaction = MyTransaction;
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        //ed.writeMessage("Branch insertted successfully \n");
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DBranch.InsertX : {0} \n", ex1.Message));
        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Insert(SqlTransaction Transaction, SqlConnection connection)
        //{
        //    SqlConnection Connection = connection;
        //    //SqlTransaction Transaction = MyTransaction;
        //    SqlCommand Command = new SqlCommand("D_Branch_Insert", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;

        //    code = Guid.NewGuid();
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iLenght", Lenght));
        //    Command.Parameters.Add(new SqlParameter("iOrder", Order));
        //    Command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iSag", Sag));
        //    Command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    Command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    Command.Parameters.Add(new SqlParameter("iIsWeek", IsWeek));
        //    Command.Parameters.Add(new SqlParameter("iIsExist", IsExist));

        //    try
        //    {

        //        //Connection.Open();
        //        //Command.Transaction = MyTransaction;
        //        Command.ExecuteNonQuery();
        //        //Connection.Close();
        //        ed.WriteMessage("Branch insertted successfully \n");
        //        return true;
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DBranch.Insert : {0} \n", ex1.Message));
        //        //Connection.Close();
        //        return false;
        //    }
        //}

        //public bool Update()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_Branch_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iCode", Code));
        //    command.Parameters.Add(new SqlParameter("iLenght", Lenght));
        //    command.Parameters.Add(new SqlParameter("iNumber", Number));
        //    command.Parameters.Add(new SqlParameter("iSag", Sag));
        //    command.Parameters.Add(new SqlParameter("iProductCode", ProductCode));
        //    command.Parameters.Add(new SqlParameter("iProductType", ProductType));
        //    command.Parameters.Add(new SqlParameter("iIsExist", IsExist));

        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();

        //        connection.Close();
        //        return true;
        //    }
        //    catch(System.Exception ex)
        //    {
        //        ed.WriteMessage("Error IN  D.Branch.Update "+ex.Message+"\n");
        //        connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(Guid Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_Branch_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch
        //    {

        //        Connection.Close();
        //        return false;
        //    }
        //}

        //public static bool Delete(Guid Code , SqlTransaction Transaction , SqlConnection Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection _Connection = Connection;
        //    SqlCommand Command = new SqlCommand("D_Branch_Delete", _Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;
        //    ed.WriteMessage("i am in DBranch.Delete \n");

        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        //Connection.Open();
        //        Command.ExecuteNonQuery();
        //        ed.WriteMessage("dBranch delete done \n");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch(System.Exception ex)
        //    {
        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DBranch.Delete : {0} \n",ex.Message));
        //        return false;
        //    }
        //}

        //public static bool DeleteLeftNodeCodeAndDesignCode(Guid LeftNodeCode,int DesignCode, SqlTransaction Transaction, SqlConnection Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection _Connection = Connection;
        //    SqlCommand Command = new SqlCommand("D_Branch_DeleteLeftNodeCode", _Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;
        //    ed.WriteMessage("i am in DBranch.Delete \n");

        //    Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));

        //    try
        //    {
        //        //Connection.Open();
        //        Command.ExecuteNonQuery();
        //        ed.WriteMessage("dBranch.LeftNodeCode delete done \n");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DBranch.DeleteLeftNodeCode : {0} \n", ex.Message));
        //        return false;
        //    }
        //}

        //public static bool DeleteRightNodeCodeAndDesignCode(Guid RightNodeCode, int DesignCode, SqlTransaction Transaction, SqlConnection Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection _Connection = Connection;
        //    SqlCommand Command = new SqlCommand("D_Branch_DeleteRightNodeCode", _Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;
        //    ed.WriteMessage("i am in DBranch.Delete \n");

        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));

        //    try
        //    {
        //        //Connection.Open();
        //        Command.ExecuteNonQuery();
        //        ed.WriteMessage("dBranch.RightNodeCode delete done \n");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DBranch.DeleteRightNodeCode : {0} \n", ex.Message));
        //        return false;
        //    }
        //}



        //public static bool IsValidBeginner(Guid BeginEquipmentCode)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter leftAdapter = new SqlDataAdapter("D_Branch_SelectByLeftNodeCode", Connection);
        //    leftAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    leftAdapter.SelectCommand.Parameters.Add(new SqlParameter("iLeftNodeCode", BeginEquipmentCode));
        //    DataSet dsLeft = new DataSet();
        //    leftAdapter.Fill(dsLeft);
        //    SqlDataAdapter rightAdapter = new SqlDataAdapter("D_Branch_SelectByRightNodeCode", Connection);
        //    rightAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    rightAdapter.SelectCommand.Parameters.Add(new SqlParameter("iRightNodeCode", BeginEquipmentCode));
        //    DataSet dsRight = new DataSet();
        //    rightAdapter.Fill(dsRight);
        //    if (dsLeft.Tables[0].Rows.Count == 0 || dsRight.Tables[0].Rows.Count == 0)
        //        return true;
        //    else
        //        return false;
        //}

        //public static DBranch SelectByLeftNodeCodeInnerGroupCode(Guid LeftNodeCode, int InnerGroupCode)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_Branch_SelectByLeftNodeCodeInnerGroup", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iInnerGroupCode", InnerGroupCode));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DBranch branch = new DBranch();
        //    if (reader.Read())
        //    {
        //        //branch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //        branch.Code = new Guid(reader["Code"].ToString());
        //        branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //        branch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //        branch.Order = Convert.ToInt32(reader["Order"]);
        //        branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //        branch.Number = reader["Number"].ToString();
        //        branch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        branch.Sag = Convert.ToDouble(reader["Sag"]);
        //        branch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //        branch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //        branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //        branch.IsExist = Convert.ToBoolean(reader["IsExist"]);
        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return branch;
        //}

        //public static DBranch SelectByRightNodeCodeInnerGroupCode(Guid RightNodeCode, int InnerGroupCode)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_Branch_SelectByRightNodeCodeInnerGroup", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iInnerGroupCode", InnerGroupCode));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DBranch branch = new DBranch();
        //    if (reader.Read())
        //    {
        //        //branch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //        branch.Code = new Guid(reader["Code"].ToString());
        //        branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //        branch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //        branch.Order = Convert.ToInt32(reader["Order"]);
        //        branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //        branch.Number = reader["Number"].ToString();
        //        branch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        branch.Sag = Convert.ToDouble(reader["Sag"]);
        //        branch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //        branch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //        branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //        branch.IsExist = Convert.ToBoolean(reader["IsExist"]);

        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return branch;
        //}

        //public static DBranch SelectByCode(Guid Code)
        //{
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_Branch_Select", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DBranch branch = new DBranch();
        //    if (reader.Read())
        //    {
        //        //branch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //        branch.Code = new Guid(reader["Code"].ToString());
        //        branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //        branch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //        branch.Order = Convert.ToInt32(reader["Order"]);
        //        branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //        branch.Number = reader["Number"].ToString();
        //        branch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        branch.Sag = Convert.ToDouble(reader["Sag"]);
        //        branch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //        branch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //        branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //        branch.IsExist = Convert.ToBoolean(reader["IsExist"]);

        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return branch;
        //}

        //public static DBranch SelectByDesignCodeAndCode(int DesignCode, Guid Code)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    //SqlConnection Connection = connection;
        //    SqlCommand Command = new SqlCommand("D_Branch_SelectByDesignCodeAndCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Atend.Base.Design.DBranch dBranch = new DBranch();

        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        Connection.Open();
        //        SqlDataReader reader = Command.ExecuteReader();
        //        if (reader.Read())
        //        {

        //            //dBranch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //            dBranch.Code = new Guid(reader["Code"].ToString());
        //            dBranch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //            dBranch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //            dBranch.Order = Convert.ToInt32(reader["Order"]);
        //            dBranch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //            dBranch.Number = reader["Number"].ToString();
        //            dBranch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //            dBranch.Sag = Convert.ToDouble(reader["Sag"]);
        //            dBranch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //            dBranch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //            dBranch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //            dBranch.IsExist = Convert.ToBoolean(reader["IsExist"]);

        //        }
        //        reader.Close();
        //        Connection.Close();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error D_Branch_SelectByDesignCodeAutocadCode : {0} \n", ex.Message));
        //        Connection.Close();
        //    }
        //    return dBranch;
        //}

        //public static DBranch SelectByDesignCodeAndCode(int DesignCode, Guid Code , SqlTransaction _Transaction , SqlConnection _Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    SqlConnection Connection = _Connection;
        //    //SqlConnection Connection = connection;
        //    SqlCommand Command = new SqlCommand("D_Branch_SelectByDesignCodeAndCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Atend.Base.Design.DBranch dBranch = new DBranch();
        //    Command.Transaction = _Transaction;

        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    try
        //    {
        //        //Connection.Open();
        //        SqlDataReader reader = Command.ExecuteReader();
        //        if (reader.Read())
        //        {

        //            //dBranch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //            dBranch.Code = new Guid(reader["Code"].ToString());
        //            dBranch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //            dBranch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //            dBranch.Order = Convert.ToInt32(reader["Order"]);
        //            dBranch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //            dBranch.Number = reader["Number"].ToString();
        //            dBranch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //            //dBranch.Name = reader["Name"].ToString();
        //            dBranch.Sag = Convert.ToDouble(reader["Sag"]);
        //            dBranch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //            dBranch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //            dBranch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //            dBranch.IsExist = Convert.ToBoolean(reader["IsExist"]);

        //        }
        //        reader.Close();
        //        //Connection.Close();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage(string.Format("Error D_Branch_SelectByDesignCodeAutocadCode : {0} \n", ex.Message));
        //        //Connection.Close();
        //    }
        //    return dBranch;
        //}

        ////public static bool IsExist(int DesignCode, Guid Code)
        ////{
        ////    bool BranchIsExist = false;
        ////    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ////    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        ////    SqlCommand Command = new SqlCommand("D_Branch_SelectByDesignCodeAndCode", Connection);
        ////    Command.CommandType = CommandType.StoredProcedure;
        ////    Atend.Base.Design.DBranch dBranch = new DBranch();

        ////    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        ////    Command.Parameters.Add(new SqlParameter("iCode", Code));
        ////    Connection.Open();
        ////    SqlDataReader reader = Command.ExecuteReader();
        ////    if (reader.Read())
        ////    {
        ////        BranchIsExist = true;
        ////    }
        ////    else
        ////    {
        ////        BranchIsExist = false;
        ////    }
        ////    reader.Close();
        ////    Connection.Close();
        ////    return BranchIsExist;
        ////}

        //public static DataTable SelectByLeftNodeCode(Guid LeftNodeCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Branch_SelectByLeftNodeCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iLeftNodeCode",  LeftNodeCode));
        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset);
        //    return dataset.Tables[0];
        //    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    ////SqlConnection Connection = connection;
        //    //SqlCommand Command = new SqlCommand("D_Branch_SelectByLeftNodeCode", Connection);
        //    //Command.CommandType = CommandType.StoredProcedure;
        //    //Atend.Base.Design.DBranch dBranch = new DBranch();

        //    //Command.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    //try
        //    //{
        //    //    Connection.Open();
        //    //    SqlDataReader reader = Command.ExecuteReader();
        //    //    if (reader.Read())
        //    //    {

        //    //        //dBranch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //    //        dBranch.CircuitType = Convert.ToByte(reader["CircuitType"]);
        //    //        dBranch.Code = new Guid(reader["Code"].ToString());
        //    //        dBranch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //    //        dBranch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //    //        dBranch.NetType = Convert.ToByte(reader["NetType"]);
        //    //        dBranch.Order = Convert.ToInt32(reader["Order"]);
        //    //        dBranch.ProductBlockCode = Convert.ToInt32(reader["ProductBlockCode"]);
        //    //        dBranch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //    //        dBranch.UnitType = Convert.ToByte(reader["UnitType"]);
        //    //        dBranch.Number = reader["Number"].ToString();
        //    //        dBranch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //    //        //dBranch.Name = reader["Name"].ToString();
        //    //        dBranch.Sag = Convert.ToDouble(reader["Sag"]);
        //    //        dBranch.ProductCode = Convert.ToInt32(reader["ProductCode"]);
        //    //        dBranch.ProductType = Convert.ToInt32(reader["ProductType"]);

        //    //    }
        //    //    reader.Close();
        //    //    Connection.Close();
        //    //}
        //    //catch (System.Exception ex)
        //    //{
        //    //    ed.WriteMessage(string.Format("Error D_Branch_SelectByLeftNodeCode : {0} \n", ex.Message));
        //    //    //Connection.Close();
        //    //}
        //    //return dBranch;

        //}

        //public static DataTable SelectByRigthNodeCode(Guid RightNodeCode)
        //{

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Branch_SelectByRightNodeCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset);
        //    return dataset.Tables[0];
        //    //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    //SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    ////SqlConnection Connection = connection;
        //    //SqlCommand Command = new SqlCommand("D_Branch_SelectByRightNodeCode", Connection);
        //    //Command.CommandType = CommandType.StoredProcedure;
        //    //Atend.Base.Design.DBranch dBranch = new DBranch();

        //    //Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    //try
        //    //{
        //    //    Connection.Open();
        //    //    SqlDataReader reader = Command.ExecuteReader();
        //    //    if (reader.Read())
        //    //    {

        //    //        //dBranch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //    //        dBranch.CircuitType = Convert.ToByte(reader["CircuitType"]);
        //    //        dBranch.Code = new Guid(reader["Code"].ToString());
        //    //        dBranch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //    //        dBranch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //    //        dBranch.NetType = Convert.ToByte(reader["NetType"]);
        //    //        dBranch.Order = Convert.ToInt32(reader["Order"]);
        //    //        dBranch.ProductBlockCode = Convert.ToInt32(reader["ProductBlockCode"]);
        //    //        dBranch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //    //        dBranch.UnitType = Convert.ToByte(reader["UnitType"]);
        //    //        dBranch.Number = reader["Number"].ToString();
        //    //        dBranch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //    //        //dBranch.Name = reader["Name"].ToString();
        //    //        dBranch.Sag = Convert.ToDouble(reader["Sag"]);
        //    //        dBranch.ProductCode = Convert.ToInt32(reader["ProductCode"]);
        //    //        dBranch.ProductType = Convert.ToInt32(reader["ProductType"]);

        //    //    }
        //    //    reader.Close();
        //    //    Connection.Close();
        //    //}
        //    //catch (System.Exception ex)
        //    //{
        //    //    ed.WriteMessage(string.Format("Error D_Branch_SelectByRightNodeCode : {0} \n", ex.Message));
        //    //    //Connection.Close();
        //    //}
        //    //return dBranch;
        //}

        //public static DataTable SelectByRightNodeCodeDesignCode(Guid RightNodeCode , int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter dataadapter = new SqlDataAdapter("D_Branch_SelectByRightNodeCodeDesignCode", connection);
        //    dataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    dataadapter.SelectCommand.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    dataadapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    DataSet ds = new DataSet();
        //    dataadapter.Fill(ds);
        //    return ds.Tables[0];
        //}

        //public static DataTable SelectByLeftNodeCodeDesignCode(Guid LeftNodeCode , int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter dataadapter = new SqlDataAdapter("D_Branch_SelectByLeftNodeCodeDesignCode", connection);
        //    dataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    dataadapter.SelectCommand.Parameters.Add(new SqlParameter("iLeftNodeCode", LeftNodeCode));
        //    dataadapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    DataSet ds = new DataSet();
        //    dataadapter.Fill(ds);
        //    return ds.Tables[0];
        //}

        //~~~~~~~~~~~~~~~~~~~~~~~~ ACCESS PAER ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //



        public bool AccessInsert()
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_Branch_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            code = Guid.NewGuid();
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
            Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            Command.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            Command.Parameters.Add(new OleDbParameter("iNumber", Number));
            Command.Parameters.Add(new OleDbParameter("iSag", Sag));
            Command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            Command.Parameters.Add(new OleDbParameter("iIsWeek", IsWeek));
            Command.Parameters.Add(new OleDbParameter("iIsExist", IsExist));

            try
            {

                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DBranch.AccessInsert : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction Transaction, OleDbConnection connection)
        {
            OleDbConnection Connection = connection;
            OleDbCommand Command = new OleDbCommand("D_Branch_Insert", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = Transaction;

            code = Guid.NewGuid();
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
            Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            Command.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            ed.WriteMessage("  **  **  Number : {0} \n", Number);
            Command.Parameters.Add(new OleDbParameter("iNumber", Number));
            Command.Parameters.Add(new OleDbParameter("iSag", Sag));
            Command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            Command.Parameters.Add(new OleDbParameter("iIsWeek", IsWeek));
            //ed.WriteMessage("@@@@@#####IsExist={0},ProjectCode={1}\n",IsExist,ProjectCode);
            Command.Parameters.Add(new OleDbParameter("iIsExist", IsExist));
            Command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));

            try
            {

                Command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DBranch.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
        }

        //Hatami
        public bool AccessUpdate(OleDbTransaction Transaction, OleDbConnection connection)
        {
            OleDbConnection Connection = connection;
            OleDbCommand Command = new OleDbCommand("D_Branch_Update", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = Transaction;
            //ed.WriteMessage("####StartAccess Update Code={0}\n",Code);
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
            Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            Command.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            Command.Parameters.Add(new OleDbParameter("iNumber", Number));
            Command.Parameters.Add(new OleDbParameter("iSag", Sag));
            Command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            Command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
            Command.Parameters.Add(new OleDbParameter("iIsWeek", IsWeek));
            Command.Parameters.Add(new OleDbParameter("iIsExist", IsExist));
            Command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));

            try
            {

                Command.ExecuteNonQuery();
                ed.WriteMessage("****Access Updated\n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DBranch.AccessUpdate : {0} \n", ex1.Message));
                return false;
            }
        }

        //ASHKTORAB
        public bool AccessUpdateLenghtByLeftAndRightNode(OleDbTransaction Transaction, OleDbConnection connection)
        {
            OleDbConnection Connection = connection;
            OleDbCommand Command = new OleDbCommand("D_Branch_UpdateLenghtByLeftAndRightNode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = Transaction;
            //ed.WriteMessage("####StartAccess Update Code={0}\n",Code);
            Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
            Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            Command.Parameters.Add(new OleDbParameter("iLenght", Lenght));
            Command.Parameters.Add(new OleDbParameter("iProductType", ProductType));
           

            try
            {

                ed.WriteMessage("****Befor Access UpdateLenghtByLeftAndRightNode\n");
                Command.ExecuteNonQuery();
                ed.WriteMessage("****Access UpdateLenghtByLeftAndRightNode\n");
                return true;
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DBranch.AccessUpdateLenghtByLeftAndRightNode : {0} \n", ex1.Message));
                return false;
            }
        }

        public static bool AccessDelete(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_Branch_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DBranch.AccessDelete : {0} \n", ex1.Message));
                Connection.Close();
                return false;
            }
        }

        //MOUSAVI->drawing delete
        public static bool AccessDelete(Guid Code, OleDbTransaction Transaction, OleDbConnection Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection _Connection = Connection;
            OleDbCommand Command = new OleDbCommand("D_Branch_Delete", _Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Transaction = Transaction;


            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {

                Command.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                //Connection.Close();
                ed.WriteMessage(string.Format("Error DBranch.AccessDelete : {0} \n", ex.Message));
                return false;
            }
        }

        //public static bool AccessDeleteLeftNodeCodeAndDesignCode(Guid LeftNodeCode, int DesignCode, OleDbTransaction  Transaction, OleDbConnection Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection _Connection = Connection;
        //    OleDbCommand  Command = new OleDbCommand("D_Branch_DeleteLeftNodeCode", _Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;
        //    ed.WriteMessage("i am in DBranch.Delete \n");

        //    Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

        //    try
        //    {
        //        //Connection.Open();
        //        Command.ExecuteNonQuery();
        //        ed.WriteMessage("dBranch.LeftNodeCode delete done \n");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DBranch.DeleteLeftNodeCode : {0} \n", ex.Message));
        //        return false;
        //    }
        //}

        //public static bool AccessDeleteRightNodeCodeAndDesignCode(Guid RightNodeCode, int DesignCode, OleDbTransaction Transaction, OleDbConnection  Connection)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    OleDbConnection _Connection = Connection;
        //    OleDbCommand Command = new OleDbCommand("D_Branch_DeleteRightNodeCode", _Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Transaction = Transaction;
        //    ed.WriteMessage("i am in DBranch.Delete \n");

        //    Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

        //    try
        //    {
        //        //Connection.Open();
        //        Command.ExecuteNonQuery();
        //        ed.WriteMessage("dBranch.RightNodeCode delete done \n");
        //        //Connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //Connection.Close();
        //        ed.WriteMessage(string.Format("Error DBranch.DeleteRightNodeCode : {0} \n", ex.Message));
        //        return false;
        //    }
        //}



        //public static bool AccessIsValidBeginner(Guid BeginEquipmentCode)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbDataAdapter leftAdapter = new OleDbDataAdapter("D_Branch_SelectByLeftNodeCode", Connection);
        //    leftAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    leftAdapter.SelectCommand.Parameters.Add(new OleDbParameter("iLeftNodeCode", BeginEquipmentCode));
        //    DataSet dsLeft = new DataSet();
        //    leftAdapter.Fill(dsLeft);
        //    OleDbDataAdapter rightAdapter = new OleDbDataAdapter("D_Branch_SelectByRightNodeCode", Connection);
        //    rightAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    rightAdapter.SelectCommand.Parameters.Add(new OleDbParameter("iRightNodeCode", BeginEquipmentCode));
        //    DataSet dsRight = new DataSet();
        //    rightAdapter.Fill(dsRight);
        //    if (dsLeft.Tables[0].Rows.Count == 0 || dsRight.Tables[0].Rows.Count == 0)
        //        return true;
        //    else
        //        return false;
        //}

        //public static DBranch AccessSelectByLeftNodeCodeInnerGroupCode(Guid LeftNodeCode, int InnerGroupCode)
        //{
        //    OleDbConnection  Connection = new OleDbConnection(Atend.Control.ConnectionString.cnString);
        //    OleDbCommand Command = new OleDbCommand("D_Branch_SelectByLeftNodeCodeInnerGroup", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
        //    Command.Parameters.Add(new OleDbParameter("iInnerGroupCode", InnerGroupCode));
        //    Connection.Open();
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    DBranch branch = new DBranch();
        //    if (reader.Read())
        //    {
        //        //branch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //        branch.Code = new Guid(reader["Code"].ToString());
        //        branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //        branch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //        branch.Order = Convert.ToInt32(reader["Order"]);
        //        branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //        branch.Number = reader["Number"].ToString();
        //        branch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        branch.Sag = Convert.ToDouble(reader["Sag"]);
        //        branch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //        branch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //        branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //        branch.IsExist = Convert.ToBoolean(reader["IsExist"]);
        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return branch;
        //}

        //public static DBranch AccessSelectByRightNodeCodeInnerGroupCode(Guid RightNodeCode, int InnerGroupCode)
        //{
        //    OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
        //    OleDbCommand Command = new OleDbCommand("D_Branch_SelectByRightNodeCodeInnerGroup", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iRightNodeCode", RightNodeCode));
        //    Command.Parameters.Add(new SqlParameter("iInnerGroupCode", InnerGroupCode));
        //    Connection.Open();
        //    OleDbDataReader reader = Command.ExecuteReader();
        //    DBranch branch = new DBranch();
        //    if (reader.Read())
        //    {
        //        //branch.AutocadCode = Convert.ToInt64(reader["AutocadCode"]);
        //        branch.Code = new Guid(reader["Code"].ToString());
        //        branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
        //        branch.Lenght = Convert.ToDouble(reader["Lenght"]);
        //        branch.Order = Convert.ToInt32(reader["Order"]);
        //        branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
        //        branch.Number = reader["Number"].ToString();
        //        branch.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        branch.Sag = Convert.ToDouble(reader["Sag"]);
        //        branch.ProductCode = new Guid(reader["ProductCode"].ToString());
        //        branch.ProductType = Convert.ToInt32(reader["ProductType"]);
        //        branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
        //        branch.IsExist = Convert.ToBoolean(reader["IsExist"]);

        //    }
        //    reader.Close();
        //    Connection.Close();
        //    return branch;
        //}

        //StatusReport
        public static DataTable AccessSelectAll()
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }



        //CALCULATION HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {

            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_SelectAll", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }



        //Hatami//MOUSAVI->AutoPoleInstallation
        public static DBranch AccessSelectByCode(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_Branch_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            DBranch branch = new DBranch();
            if (reader.Read())
            {
                branch.Code = new Guid(reader["Code"].ToString());
                branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
                branch.Lenght = Convert.ToDouble(reader["Lenght"]);
                branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
                branch.Number = reader["Number"].ToString();
                branch.Sag = Convert.ToDouble(reader["Sag"]);
                branch.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                branch.ProductType = Convert.ToInt32(reader["ProductType"]);
                branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
                branch.IsExist = Convert.ToByte(reader["IsExist"]);
                branch.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);

            }
            else
            {
                branch.Code = Guid.Empty;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            Connection.Close();
            return branch;
        }

        //Calculation Hatami
        public static DBranch AccessSelectByCode(Guid Code,OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = _Connection;
            OleDbCommand Command = new OleDbCommand("D_Branch_Select", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Branch SelectByCode\n");
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            //Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            DBranch branch = new DBranch();
            if (reader.Read())
            {
                branch.Code = new Guid(reader["Code"].ToString());
                branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
                branch.Lenght = Convert.ToDouble(reader["Lenght"]);
                branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
                branch.Number = reader["Number"].ToString();
                branch.Sag = Convert.ToDouble(reader["Sag"]);
                branch.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                branch.ProductType = Convert.ToInt32(reader["ProductType"]);
                branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
                branch.IsExist = Convert.ToByte(reader["IsExist"]);
                branch.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);

            }
            else
            {
                branch.Code = Guid.Empty;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");
            reader.Close();
            //Connection.Close();
            return branch;
        }

        //frmDrawBranch
        public static DBranch AccessSelectByRigthAndLeftNodeCode(Guid LeftNodeCode, Guid RightNodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

            OleDbCommand Command = new OleDbCommand("D_Branch_SelectByRigthAndLeftNodeCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            Command.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));

            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            DBranch branch = new DBranch();
            if (reader.Read())
            {
                branch.Code = new Guid(reader["Code"].ToString());
                branch.LeftNodeCode = new Guid(reader["LeftNodeCode"].ToString());
                branch.Lenght = Convert.ToDouble(reader["Lenght"]);
                branch.RightNodeCode = new Guid(reader["RightNodeCode"].ToString());
                branch.Number = reader["Number"].ToString();
                branch.Sag = Convert.ToDouble(reader["Sag"]);
                branch.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                branch.ProductType = Convert.ToInt32(reader["ProductType"]);
                branch.IsWeek = Convert.ToBoolean(reader["IsWeek"]);
                branch.IsExist = Convert.ToByte(reader["IsExist"]);
                branch.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);

            }
            else
            {
                branch.Code = Guid.Empty;
            }
            reader.Close();
            Connection.Close();
            return branch;
        }


        public static DataTable AccessSelectByLeftNodeCode(Guid LeftNodeCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_SelectByLeftNodeCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iLeftNodeCode", LeftNodeCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];


        }

        public static DataTable AccessSelectByRigthNodeCode(Guid RightNodeCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_SelectByRightNodeCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iRightNodeCode", RightNodeCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];

        }

        //MEDHAT
        public static DataTable AccessSelectByLeftOrRightNodeCode(Guid NodeCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_SelectByLeftOrRightNodeCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode", NodeCode));
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];
        }

        public static DataTable AccessAllWorkOrders()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_WorkOrders", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];

        }


        //frmSaveDesignServer
        public static DataTable AccessAllWorkOrders(OleDbTransaction _transction , OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Branch_WorkOrders", connection);
            adapter.SelectCommand.Transaction = _transction;

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];

        }


        //*****************************Access Memory For Calculation
        //Hatami
        public static DBranch AccessSelectByCode(DataTable dtDBranch, Guid Code)
        {
            DataRow[] dr = dtDBranch.Select(string.Format("Code='{0}'", Code.ToString()));
            DBranch branch = new DBranch();
            if (dr.Length != 0)
            {
                branch.Code = new Guid(dr[0]["Code"].ToString());
                branch.LeftNodeCode = new Guid(dr[0]["LeftNodeCode"].ToString());
                branch.Lenght = Convert.ToDouble(dr[0]["Lenght"]);
                branch.RightNodeCode = new Guid(dr[0]["RightNodeCode"].ToString());
                branch.Number = dr[0]["Number"].ToString();
                branch.Sag = Convert.ToDouble(dr[0]["Sag"]);
                branch.ProductCode = Convert.ToInt32(dr[0]["ProductCode"]);
                branch.ProductType = Convert.ToInt32(dr[0]["ProductType"]);
                branch.IsWeek = Convert.ToBoolean(dr[0]["IsWeek"]);
                branch.IsExist = Convert.ToByte(dr[0]["IsExist"]);
                branch.ProjectCode = Convert.ToInt32(dr[0]["ProjectCode"]);

            }
            else
            {
                branch.Code = Guid.Empty;
            }
            //ed.WriteMessage("Finish DBranch SelectBy Code\n");

            return branch;
        }

    }


}
