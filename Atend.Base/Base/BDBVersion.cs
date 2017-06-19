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
    public class BDBVersion
    {
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private string version;
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public static DataTable SelectAll()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_DBVersion_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }

        public static BDBVersion SelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.LocalcnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_DBVersion_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BDBVersion Version = new BDBVersion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Version.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Version.Version = dsProduct.Tables[0].Rows[0]["Version"].ToString();
            }
            else
            {
                Version.Code = -1;
            }

            return Version;
        }

        public static DataTable ServerSelectAll()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_DBVersion_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", -1));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);
            return dsProduct.Tables[0];

        }

        public static BDBVersion ServerSelectByCode(int Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.ServercnString);
            SqlDataAdapter adapter = new SqlDataAdapter("B_DBVersion_SelectByCode", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new SqlParameter("iCode", Code));
            DataSet dsProduct = new DataSet();
            adapter.Fill(dsProduct);

            BDBVersion Version = new BDBVersion();

            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                Version.Code = Convert.ToInt16(dsProduct.Tables[0].Rows[0]["Code"].ToString());
                Version.Version = dsProduct.Tables[0].Rows[0]["Version"].ToString();
            }
            else
            {
                Version.Code = -1;
            }

            return Version;
        }

    }
}
