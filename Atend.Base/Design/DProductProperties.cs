using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

using System.Data;
using System.Data.SqlClient;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DProductProperties
    {


        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private byte nodeType;
        public byte NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        private int softwareCode;
        public int SoftwareCode
        {
            get { return softwareCode; }
            set { softwareCode = value; }
        }

        private byte nodehead;//Edit
        public byte NodeHead //Edit
        {
            get { return nodehead; }
            set { nodehead = value; }
        }

        private bool drawable;
        public bool Drawable
        {
            get { return drawable; }
            set { drawable = value; }
        }

        private double scale;
        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        double commentScale;
        public double CommentScale
        {
            get { return commentScale; }
            set { commentScale = value; }
        }


        /*public static DProductProperties SelectBySoftwareCode(int SoftwareCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlCommand command = new SqlCommand("D_ProductProperties_SelectBySoftwareCode", connection);//Edit
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("iSoftWareCode", SoftwareCode));


            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DProductProperties dProductProperties = new DProductProperties();
            if (reader.Read())
            {

                dProductProperties.Code =Convert.ToInt32(reader["Code"].ToString());  //Edit
                dProductProperties.ProductName = Convert.ToString(reader["ProductName"]);
                dProductProperties.NodeType = Convert.ToByte(reader["NodeType"]);
                dProductProperties.SoftwareCode = Convert.ToInt32(reader["SoftwareCode"]);
                dProductProperties.NodeHead = Convert.ToByte(reader["NodeHead"]);  //Edit
                //ed.writeMessage("Recode for DProductProperties.SelectBySoftwareCode found \n ");
            }

            reader.Close();
            connection.Close();
            return dProductProperties;

        }
        */

        //~~~~~~~~~~~~~~~~AccessPart

        //MOUSAVI

        //MOUSAVI->Drwaings
        public static DProductProperties AccessSelectBySoftwareCode(int SoftwareCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("-7\n");
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            //ed.WriteMessage("-6\n");
            OleDbCommand command = new OleDbCommand("D_ProductProperties_SelectBySoftwareCode", connection);//Edit
            //ed.WriteMessage("-5\n");
            command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("-4\n");
            command.Parameters.Add(new OleDbParameter("iSoftWareCode", SoftwareCode));
            //ed.WriteMessage("-3\n");


            connection.Open();
            //ed.WriteMessage("-2\n");
            OleDbDataReader reader = command.ExecuteReader();
            //ed.WriteMessage("-1\n");
            DProductProperties dProductProperties = new DProductProperties();
            //ed.WriteMessage("0\n");
            if (reader.Read())
            {

                dProductProperties.Code = Convert.ToInt32(reader["Code"].ToString());  //Edit
                //ed.WriteMessage("1\n");
                dProductProperties.ProductName = Convert.ToString(reader["ProductName"]);
                //ed.WriteMessage("2\n");
                dProductProperties.NodeType = Convert.ToByte(reader["NodeType"]);
                //ed.WriteMessage("3\n");
                dProductProperties.SoftwareCode = Convert.ToInt32(reader["SoftwareCode"]);
                //ed.WriteMessage("4\n");
                dProductProperties.NodeHead = Convert.ToByte(reader["NodeHead"]);  //Edit
                //ed.WriteMessage("5\n");
                dProductProperties.Drawable = Convert.ToBoolean(reader["Drawable"]);  //Edit
                //ed.WriteMessage("6\n");
                dProductProperties.Scale = Convert.ToDouble(reader["Scale"]);  //Edit
                //ed.WriteMessage("7\n");
                dProductProperties.CommentScale = Convert.ToDouble(reader["CommentScale"]);  //Edit
                //ed.WriteMessage("Recode for DProductProperties.SelectBySoftwareCode found \n ");
            }
            else
            {
                dProductProperties.Code = -1;
                //ed.WriteMessage("Recode for DProductProperties.SelectBySoftwareCode NOT found \n ");
            }

            reader.Close();
            connection.Close();
            return dProductProperties;

        }

        public static DataTable AccessSelectAllDrawable()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_ProductProperties_SelectByCodeDrawable", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iCode",-1));

            DataSet ds = new DataSet();

            adapter.Fill(ds);
            return ds.Tables[0];



        }

        public static DProductProperties AccessSelectByCodeDrawable(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_ProductProperties_SelectByCodeDrawable", connection);//Edit
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));


            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            DProductProperties dProductProperties = new DProductProperties();
            if (reader.Read())
            {

                dProductProperties.Code = Convert.ToInt32(reader["Code"].ToString());  //Edit
                dProductProperties.ProductName = Convert.ToString(reader["ProductName"]);
                dProductProperties.NodeType = Convert.ToByte(reader["NodeType"]);
                dProductProperties.SoftwareCode = Convert.ToInt32(reader["SoftwareCode"]);
                dProductProperties.NodeHead = Convert.ToByte(reader["NodeHead"]);  //Edit
                dProductProperties.Drawable = Convert.ToBoolean(reader["Drawable"]);  //Edit
                dProductProperties.Scale = Convert.ToDouble(reader["Scale"]);  //Edit
                dProductProperties.CommentScale = Convert.ToDouble(reader["CommentScale"]);  //Edit
                //ed.writeMessage("Recode for DProductProperties.SelectBySoftwareCode found \n ");
            }

            reader.Close();
            connection.Close();
            return dProductProperties;
        }

        //MOUSAVI
        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_ProductProperties_Update", connection);

            command.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("Code:{0}\n",Code);
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //ed.WriteMessage("PN:{0}\n", ProductName);
            command.Parameters.Add(new OleDbParameter("iProductName", ProductName));
            //ed.WriteMessage("NT:{0}\n", NodeType);
            command.Parameters.Add(new OleDbParameter("iNodeType", NodeType));
            //ed.WriteMessage("SC:{0}\n", SoftwareCode);
            command.Parameters.Add(new OleDbParameter("iSoftwareCode", SoftwareCode));
            //ed.WriteMessage("NH:{0}\n", NodeHead);
            command.Parameters.Add(new OleDbParameter("iNodeHead", NodeHead));
            //ed.WriteMessage("DRAW:{0}\n", Drawable);
            command.Parameters.Add(new OleDbParameter("iDrawable", Drawable));
            //ed.WriteMessage("Scale:{0}\n", Scale);
            command.Parameters.Add(new OleDbParameter("iScale", Scale));
            command.Parameters.Add(new OleDbParameter("iCommentScale", CommentScale));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("ERROR d_productPropertie AccessUpdate : {0} \n",ex.Message);
                connection.Close();
                return false;
            }
            connection.Close();
            return false;
        }

    }
}
