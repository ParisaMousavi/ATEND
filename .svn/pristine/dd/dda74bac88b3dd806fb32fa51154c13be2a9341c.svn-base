using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Base
{
    //public class BProductBlock
    //{
    //    private int productID;

    //    public int ProductID
    //    {
    //        get { return productID; }
    //        set { productID = value; }
    //    }

    //    private string blockID;

    //    public string BlockID
    //    {
    //        get { return blockID; }
    //        set { blockID = value; }
    //    }

    //    private bool isExistance;

    //    public bool IsExistance
    //    {
    //        get { return isExistance; }
    //        set { isExistance = value; }
    //    }

    //    private int type;

    //    public int Type
    //    {
    //        get { return type; }
    //        set { type = value; }
    //    }

    //    private int code;

    //    public int Code
    //    {
    //        get { return code; }
    //        set { code = value; }
    //    }




    //    public bool Insert()
    //    {
    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand command = new SqlCommand("B_ProductBlock_Insert", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new SqlParameter("iProductID", ProductID));
    //        command.Parameters.Add(new SqlParameter("iBlockID", BlockID));
    //        command.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
    //        command.Parameters.Add(new SqlParameter("iType", Type));
    //        try
    //        {
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.Insert :{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public bool AccessInsert()
    //    {
    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand command = new OleDbCommand("B_ProductBlock_Insert", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new OleDbParameter("iProductID", ProductID));
    //        command.Parameters.Add(new OleDbParameter("iBlockID", BlockID));
    //        command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
    //        command.Parameters.Add(new OleDbParameter("iType", Type));

    //        try
    //        {
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.AccessInsert :{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public bool Update()
    //    {
    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand command = new SqlCommand();
    //        command.CommandText = "B_ProductBlock_Update";

    //        command.CommandType = CommandType.StoredProcedure;
    //        command.Parameters.Add(new SqlParameter("iProductID", ProductID));
    //        command.Parameters.Add(new SqlParameter("iBlockID", BlockID));
    //        command.Parameters.Add(new SqlParameter("iIsExistance", IsExistance));
    //        command.Parameters.Add(new SqlParameter("iType", IsExistance));
    //        try
    //        {
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.Update :{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }


    //    public bool AccessUpdate()
    //    {
    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand command = new OleDbCommand("B_ProductBlock_Update", connection);

    //        command.CommandType = CommandType.StoredProcedure;
    //        command.Parameters.Add(new OleDbParameter("iProductID", ProductID));
    //        command.Parameters.Add(new OleDbParameter("iBlockID", BlockID));
    //        command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
    //        command.Parameters.Add(new OleDbParameter("iType", IsExistance));
    //        try
    //        {
    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.AccessUpdate :{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public static bool Delete(int ProductId, int Type)
    //    {
    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand command = new SqlCommand("B_ProductBlock_Delete", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new SqlParameter("iProductID", ProductId));
    //        command.Parameters.Add(new SqlParameter("iType", Type));
    //        try
    //        {

    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();

    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("Error BProductBlock.Delete : {0} \n", ex1.Message));
    //            connection.Close();
    //            return false;
    //        }

    //        return true;
    //    }


    //    public static bool AccessDelete(int ProductId, int Type)
    //    {
    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand command = new OleDbCommand("B_ProductBlock_Delete", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new OleDbParameter("iProductID", ProductId));
    //        command.Parameters.Add(new OleDbParameter("iType", Type));
    //        try
    //        {

    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();

    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("Error BProductBlock.AccessDelete : {0} \n", ex1.Message));
    //            connection.Close();
    //            return false;
    //        }

    //        return true;
    //    }

    //    public static DataTable SelectByType(int type)
    //    {


    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlDataAdapter adapter = new SqlDataAdapter("B_ProductBlock_SelectByType", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", type));

    //        DataSet dsProduct = new DataSet();
    //        adapter.Fill(dsProduct);
    //        return dsProduct.Tables[0];

    //    }

    //    public static DataTable AccessSelectByType(int type)
    //    {


    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbDataAdapter adapter = new OleDbDataAdapter("B_ProductBlock_SelectByType", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", type));

    //        DataSet dsProduct = new DataSet();
    //        adapter.Fill(dsProduct);
    //        return dsProduct.Tables[0];

    //    }

    //    public static bool SearchByBlockID(string blockID)
    //    {

    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand command = new SqlCommand("B_ProductBlock_SearchByBlockID", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new SqlParameter("iBlockID", blockID));
    //        try
    //        {
    //            connection.Open();
    //            SqlDataReader dataReader = command.ExecuteReader();
    //            if (dataReader.Read())
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return true;
    //            }
    //            else
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return false;
    //            }
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.SearchByBlockId:{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public static bool AccessSearchByBlockID(string blockID)
    //    {

    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand command = new OleDbCommand("B_ProductBlock_SearchByBlockID", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new OleDbParameter("iBlockID", blockID));
    //        try
    //        {
    //            connection.Open();
    //            OleDbDataReader dataReader = command.ExecuteReader();
    //            if (dataReader.Read())
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return true;
    //            }
    //            else
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return false;
    //            }
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.AccessSearchByBlockId:{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public static bool SearchByProductID(int productID, string blockID)
    //    {

    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand command = new SqlCommand("B_ProductBlock_SearchByProductID", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new SqlParameter("iProductID", productID));
    //        command.Parameters.Add(new SqlParameter("iBlockID", blockID));
    //        try
    //        {
    //            connection.Open();
    //            SqlDataReader dataReader = command.ExecuteReader();
    //            if (dataReader.Read())
    //            {

    //                dataReader.Close();
    //                connection.Close();
    //                return true;
    //            }
    //            else
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return false;
    //            }
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.SearchByProductByID:{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public static bool AccessSearchByProductID(int productID, string blockID)
    //    {

    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand command = new OleDbCommand("B_ProductBlock_SearchByProductID", connection);
    //        command.CommandType = CommandType.StoredProcedure;

    //        command.Parameters.Add(new OleDbParameter("iProductID", productID));
    //        command.Parameters.Add(new OleDbParameter("iBlockID", blockID));
    //        try
    //        {
    //            connection.Open();
    //            OleDbDataReader dataReader = command.ExecuteReader();
    //            if (dataReader.Read())
    //            {

    //                dataReader.Close();
    //                connection.Close();
    //                return true;
    //            }
    //            else
    //            {
    //                dataReader.Close();
    //                connection.Close();
    //                return false;
    //            }
    //        }
    //        catch (System.Exception ex1)
    //        {

    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("ERROR BProductBlock.AccessSearchByProductByID:{0} \n", ex1.Message));

    //            connection.Close();
    //            return false;
    //        }
    //    }

    //    public static DataTable SelectByProductCode(int ProductId)
    //    {

    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlDataAdapter adapter = new SqlDataAdapter("B_ProductBlock_SelectByProductId", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductId", ProductId));

    //        DataSet dsProduct = new DataSet();
    //        try
    //        {
    //            adapter.Fill(dsProduct);
    //            return dsProduct.Tables[0];
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format(" ERROR BProductBlock.SelectByProductId : {0} \n", ex1.Message));

    //            return null;
    //        }

    //    }

    //    public static DataTable AccessSelectByProductCode(int ProductId)
    //    {

    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbDataAdapter adapter = new OleDbDataAdapter("B_ProductBlock_SelectByProductId", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductId", ProductId));

    //        DataSet dsProduct = new DataSet();
    //        try
    //        {
    //            adapter.Fill(dsProduct);
    //            return dsProduct.Tables[0];
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format(" ERROR BProductBlock.AccessSelectByProductId : {0} \n", ex1.Message));

    //            return null;
    //        }

    //    }


    //    public static BProductBlock SelectByCode(int Code)
    //    {
    //        SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlCommand Command = new SqlCommand("B_ProductBlock_selectByCode", Connection);
    //        Command.CommandType = CommandType.StoredProcedure;



    //        Command.Parameters.Add(new SqlParameter("iCode", Code));
    //        Connection.Open();
    //        SqlDataReader reader = Command.ExecuteReader();
    //        BProductBlock productBlock = new BProductBlock();
    //        if (reader.Read())
    //        {
    //            productBlock.BlockID = reader["BlockId"].ToString();
    //            productBlock.IsExistance = Convert.ToBoolean(reader["IsExistance"].ToString());
    //            productBlock.ProductID = Convert.ToInt32(reader["ProductId"].ToString());
    //            productBlock.Type = Convert.ToInt32(reader["Type"].ToString());
    //            productBlock.Code = Convert.ToInt32(reader["Code"].ToString());
    //        }
    //        reader.Close();
    //        Connection.Close();
    //        return productBlock;
    //    }

    //    public static BProductBlock AccessSelectByCode(int Code)
    //    {
    //        OleDbConnection Connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbCommand Command = new OleDbCommand("B_ProductBlock_selectByCode", Connection);
    //        Command.CommandType = CommandType.StoredProcedure;



    //        Command.Parameters.Add(new OleDbParameter("iCode", Code));
    //        Connection.Open();
    //        OleDbDataReader reader = Command.ExecuteReader();
    //        BProductBlock productBlock = new BProductBlock();
    //        if (reader.Read())
    //        {
    //            productBlock.BlockID = reader["BlockId"].ToString();
    //            productBlock.IsExistance = Convert.ToBoolean(reader["IsExistance"].ToString());
    //            productBlock.ProductID = Convert.ToInt32(reader["ProductId"].ToString());
    //            productBlock.Type = Convert.ToInt32(reader["Type"].ToString());
    //            productBlock.Code = Convert.ToInt32(reader["Code"].ToString());
    //        }
    //        reader.Close();
    //        Connection.Close();
    //        return productBlock;
    //    }

    //    public static DataTable SelectByProductIdType(int ProductId, int Type)
    //    {
    //        SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
    //        SqlDataAdapter adapter = new SqlDataAdapter("B_ProductBlock_SelectByProductIdType", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductId", ProductId));
    //        adapter.SelectCommand.Parameters.Add(new SqlParameter("iType", Type));

    //        DataSet dsProduct = new DataSet();
    //        try
    //        {
    //            adapter.Fill(dsProduct);
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("Error BProductBlock.SelectByProductIdType : {0} \n", ex1.Message));
    //        }
    //        return dsProduct.Tables[0];

    //    }


    //    public static DataTable AccessSelectByProductIdType(int ProductId, int Type)
    //    {
    //        OleDbConnection connection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Atend.Control.Common.AccessPath);

    //        OleDbDataAdapter adapter = new OleDbDataAdapter("B_ProductBlock_SelectByProductIdType", connection);

    //        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductId", ProductId));
    //        adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));

    //        DataSet dsProduct = new DataSet();
    //        try
    //        {
    //            adapter.Fill(dsProduct);
    //        }
    //        catch (System.Exception ex1)
    //        {
    //            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
    //            ed.WriteMessage(string.Format("Error BProductBlock.AccessSelectByProductIdType : {0} \n", ex1.Message));
    //        }
    //        return dsProduct.Tables[0];

    //    }

    //}
}
