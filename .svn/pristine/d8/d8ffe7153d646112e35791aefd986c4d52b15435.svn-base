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
    public class DProductSubEquip
    {
        //Edit********
        private int containercode;
        public int ContainerCode
        {
            get { return containercode; }
            set { containercode = value; }
        }

        private int subcode;
        public int SubCode
        {
            get { return subcode; }
            set { subcode = value; }
        }



        /*public static DataTable selectBySubCode(int SubCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_ProductSubEquip_SelectBySubCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iSubCode", SubCode));
            DataSet dsSection = new DataSet();
            adapter.Fill(dsSection);
            return dsSection.Tables[0];

        }

        public static DataTable selectByContainerCode(int ContainerCode)
        {

            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            SqlDataAdapter adapter = new SqlDataAdapter("D_ProductSubEquip_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iContainerCode", ContainerCode));
            DataSet dsSection = new DataSet();
            adapter.Fill(dsSection);
            return dsSection.Tables[0];

        }
        */
        //~~~~~~~~~~~AccessPArt

        //MOUSAVI
        public static DataTable AccessselectBySubCode(int SubCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_ProductSubEquip_SelectBySubCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iSubCode", SubCode));
            DataSet dsSection = new DataSet();
            adapter.Fill(dsSection);
            return dsSection.Tables[0];

        }

        public static DataTable AccessselectByContainerCode(int ContainerCode)
        {

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_ProductSubEquip_SelectByContainerCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iContainerCode", ContainerCode));
            DataSet dsSection = new DataSet();
            adapter.Fill(dsSection);
            return dsSection.Tables[0];

        }
    }
}
