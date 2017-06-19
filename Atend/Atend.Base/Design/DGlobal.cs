using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace Atend.Base.Design
{
    public class DGlobal
    {
        private Guid poleGuid;

        public Guid PoleGuid
        {
            get { return poleGuid; }
            set { poleGuid = value; }
        }
        private Guid nodeGuid;

        public Guid NodeGuid
        {
            get { return nodeGuid; }
            set { nodeGuid = value; }
        }
        private Guid branchGuid;

        public Guid BranchGuid
        {
            get { return branchGuid; }
            set { branchGuid = value; }
        }
        private int branchType;

        public int BranchType
        {
            get { return branchType; }
            set { branchType = value; }
        }
        private int nodeType;

        public int NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        private int sectionNo;

        public int SectionNo
        {
            get { return sectionNo; }
            set { sectionNo = value; }
        }

        private int poleType;

        public int PoleType
        {
            get { return poleType; }
            set { poleType = value; }
        }

        public bool Accessinsert(OleDbTransaction transaction, OleDbConnection _connection)
        {
            //SqlConnection connection = new SqlConnection(Atend.Control.ConnectionString.cnString);
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Global_Insert", connection);
            command.Transaction = transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iPoleGuid", PoleGuid));
            command.Parameters.Add(new OleDbParameter("iNodeGuid", NodeGuid));
            command.Parameters.Add(new OleDbParameter("iBranchGuid", BranchGuid));
            command.Parameters.Add(new OleDbParameter("iBranchType", BranchType));
            command.Parameters.Add(new OleDbParameter("iNodeType", NodeType));
            command.Parameters.Add(new OleDbParameter("iSectionNo", SectionNo));
            command.Parameters.Add(new OleDbParameter("iPoleType", PoleType));
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format("#Error DGlobal.Insert : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }

        }

        public static bool AccessDelete()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Global_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
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
                ed.WriteMessage(string.Format(" ERROR DGlobal.Delete {0}\n", ex1.Message));

                connection.Close();
                return false;
            }
        }

        public static bool AccessDelete(OleDbConnection aConnection)
        {
            OleDbConnection connection =aConnection;
            OleDbCommand command = new OleDbCommand("D_Global_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception ex1)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage(string.Format(" ERROR DGlobal.Delete {0}\n", ex1.Message));

               // connection.Close();
                return false;
            }
        }

        public static DataTable AccessSelectAll()
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Global_Select", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet dsGlobal = new DataSet();
            adapter.Fill(dsGlobal);
            return dsGlobal.Tables[0];
        }
    }
}
