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
    public class XXDEquipLayer
    {

        private int code;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }


        private int designCode;

        public int DesignCode
        {
            get { return designCode; }
            set { designCode = value; }
        }

        private int layerCode;

        public int LayerCode
        {
            get { return layerCode; }
            set { layerCode = value; }
        }



        private int colerIndex;

        public int ColerIndex
        {
            get { return colerIndex; }
            set { colerIndex = value; }
        }



        private int productTypeCode;

        public int ProductTypeCode
        {
            get { return productTypeCode; }
            set { productTypeCode = value; }
        }


        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

   

        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;


        //public bool Insert()
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_EquipLayer_Insert", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iDesignCode",DesignCode));
        //    command.Parameters.Add(new SqlParameter("iLayerCode",LayerCode));
        //    command.Parameters.Add(new SqlParameter("iColerIndex",ColerIndex));
        //    command.Parameters.Add(new SqlParameter("iProductTypeCode",ProductTypeCode));
        //    command.Parameters.Add(new SqlParameter("iScale", scale));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch(System.Exception ex)
        //    {
        //        ed.WriteMessage("Error in D_EquipLayer_Inser :"+ex.Message+"\n");
        //        connection.Close();
        //        return false;
        //    }

        //}


        public bool AccessInsert()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_EquipLayer_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iLayerCode", LayerCode));
            command.Parameters.Add(new OleDbParameter("iColerIndex", ColerIndex));
            command.Parameters.Add(new OleDbParameter("iProductTypeCode", ProductTypeCode));
            command.Parameters.Add(new OleDbParameter("iScale", scale));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_EquipLayer_AccessInser :" + ex.Message + "\n");
                connection.Close();
                return false;
            }

        }

        //public bool UpdateByDesiegnCodeProductTypeCode()
        //{
        //    //ed.writeMessage("Enter In Update\n");
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand command = new SqlCommand("D_EquipLayer_Update", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    command.Parameters.Add(new SqlParameter("iLayerCode", LayerCode));
        //    command.Parameters.Add(new SqlParameter("iColerIndex", ColerIndex));
        //    command.Parameters.Add(new SqlParameter("iProductTypeCode", ProductTypeCode));
        //    command.Parameters.Add(new SqlParameter("iScale", Scale));
        //    try
        //    {
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //        connection.Close();
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ed.WriteMessage("Error in D_equipLayer_Update :" + ex.Message + "\n");
        //        connection.Close();
        //        return false;
        //    }
        //}

        public bool AccessUpdateByDesiegnCodeProductTypeCode()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_EquipLayer_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new OleDbParameter("iLayerCode", LayerCode));
            command.Parameters.Add(new OleDbParameter("iColerIndex", ColerIndex));
            command.Parameters.Add(new OleDbParameter("iProductTypeCode", ProductTypeCode));
            command.Parameters.Add(new OleDbParameter("iScale", Scale));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_equipLayer_AccessUpdate :" + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }
        
        /*public bool UpdateByCodeDeignCode()
        {
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_EquipLayer_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iCode", Code));
            command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
            command.Parameters.Add(new SqlParameter("iLayerCode", LayerCode));
            command.Parameters.Add(new SqlParameter("iColerIndex", ColerIndex));
            command.Parameters.Add(new SqlParameter("iProductTypeCode", ProductTypeCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("Error in D_equipLayer_Update :" + ex.Message + "\n");
                connection.Close();
                return false;
            }
        }*/

        //public static bool Delete(int DesignCode,int ProducTypeCode)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_EquipLayer_Delete", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));
        //    Command.Parameters.Add(new SqlParameter("iProductTypeCode", ProducTypeCode));

        //    try
        //    {
        //        Connection.Open();
        //        Command.ExecuteNonQuery();
        //        Connection.Close();
        //        return true;
        //    }
        //    catch(SystemException ex)
        //    {
        //        ed.WriteMessage("Error in DEquipLayer.Delete: "+ex.Message+"\n");
        //        Connection.Close();
        //        return false;
        //    }
        //}

        public static bool AccessDelete(int DesignCode, int ProducTypeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_EquipLayer_Delete", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));
            Command.Parameters.Add(new OleDbParameter("iProductTypeCode", ProducTypeCode));

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch (SystemException ex)
            {
                ed.WriteMessage("Error in DEquipLayer.AccessDelete: " + ex.Message + "\n");
                Connection.Close();
                return false;
            }
        }

        //public static DataTable  SelectByProductTypeCode(int ProductTypeCode)
        //{

        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_EquipLayer_SelectByProductTypeCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iProductTypeCode", ProductTypeCode));

        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset);
        //    return dataset.Tables[0];

        //}

        public static DataTable AccessSelectByProductTypeCode(int ProductTypeCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_EquipLayer_SelectByProductTypeCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iProductTypeCode", ProductTypeCode));

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];
        }

        //public static DataTable SelectByDesignCode(int DesignCode)
        //{
        //    SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("D_Equiplayer_SelectByDesignCode", connection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    adapter.SelectCommand.Parameters.Add(new SqlParameter("iDesignCode",DesignCode));

        //    DataSet dataset = new DataSet();
        //    adapter.Fill(dataset);
        //    return dataset.Tables[0];
           
        //}

        public static DataTable AccessSelectByDesignCode(int DesignCode)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Equiplayer_SelectByDesignCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset.Tables[0];
        }

        //public static DEquipLayer SelectByLayerCode(int Code)
        //{

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_EquipLayer_SelectByLayerCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iCode", Code));
        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DEquipLayer dequipLayer = new DEquipLayer();
        //    if (reader.Read())
        //    {
        //        dequipLayer.Code = Convert.ToInt32(reader["Code"]);
        //        dequipLayer.ColerIndex = Convert.ToInt32(reader["ColerIndex"]);
        //        dequipLayer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        dequipLayer.LayerCode = Convert.ToInt32(reader["LayerCode"]);
        //        dequipLayer.ProductTypeCode = Convert.ToInt32(reader["ProductTypeCode"]);
              

        //    }
        //    reader.Close();

        //    Connection.Close();
        //    return dequipLayer;
          
        //}

        public static XXDEquipLayer AccessSelectByLayerCode(int Code)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_EquipLayer_SelectByLayerCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iCode", Code));
            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            XXDEquipLayer dequipLayer = new XXDEquipLayer();
            if (reader.Read())
            {
                dequipLayer.Code = Convert.ToInt32(reader["Code"]);
                dequipLayer.ColerIndex = Convert.ToInt32(reader["ColerIndex"]);
                dequipLayer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dequipLayer.LayerCode = Convert.ToInt32(reader["LayerCode"]);
                dequipLayer.ProductTypeCode = Convert.ToInt32(reader["ProductTypeCode"]);


            }
            reader.Close();

            Connection.Close();
            return dequipLayer;

        }

        //public static DEquipLayer SelectByProductTypeCodeDesignCode(int ProductTypeCode,int DesignCode)
        //{

        //    SqlConnection Connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
        //    SqlCommand Command = new SqlCommand("D_EquipLayer_SelectByProductTypeCodeDesignCode", Connection);
        //    Command.CommandType = CommandType.StoredProcedure;
        //    Command.Parameters.Add(new SqlParameter("iProductTypeCode", ProductTypeCode));
        //    Command.Parameters.Add(new SqlParameter("iDesignCode", DesignCode));

        //    Connection.Open();
        //    SqlDataReader reader = Command.ExecuteReader();
        //    DEquipLayer dequipLayer = new DEquipLayer();
        //    if (reader.Read())
        //    {
        //        dequipLayer.Code = Convert.ToInt32(reader["Code"]);
        //        dequipLayer.ColerIndex = Convert.ToInt32(reader["ColerIndex"]);
        //        dequipLayer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
        //        dequipLayer.LayerCode = Convert.ToInt32(reader["LayerCode"]);
        //        dequipLayer.ProductTypeCode = Convert.ToInt32(reader["ProductTypeCode"]);
        //        dequipLayer.Scale = Convert.ToSingle(reader["Scale"]);

        //    }
        //    else
        //    {
        //        dequipLayer.Code = -1;
        //    }
        //    reader.Close();

        //    Connection.Close();
        //    return dequipLayer;

        //}

        public static XXDEquipLayer SelectByProductTypeCodeDesignCode(int ProductTypeCode, int DesignCode)
        {
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand Command = new OleDbCommand("D_EquipLayer_SelectByProductTypeCodeDesignCode", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.Add(new OleDbParameter("iProductTypeCode", ProductTypeCode));
            Command.Parameters.Add(new OleDbParameter("iDesignCode", DesignCode));

            Connection.Open();
            OleDbDataReader reader = Command.ExecuteReader();
            XXDEquipLayer dequipLayer = new XXDEquipLayer();
            if (reader.Read())
            {
                dequipLayer.Code = Convert.ToInt32(reader["Code"]);
                dequipLayer.ColerIndex = Convert.ToInt32(reader["ColerIndex"]);
                dequipLayer.DesignCode = Convert.ToInt32(reader["DesignCode"]);
                dequipLayer.LayerCode = Convert.ToInt32(reader["LayerCode"]);
                dequipLayer.ProductTypeCode = Convert.ToInt32(reader["ProductTypeCode"]);
                dequipLayer.Scale = Convert.ToSingle(reader["Scale"]);

            }
            else
            {
                dequipLayer.Code = -1;
            }
            reader.Close();

            Connection.Close();
            return dequipLayer;

        }


    }
}
