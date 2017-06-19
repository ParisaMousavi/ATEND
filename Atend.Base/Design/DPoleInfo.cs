using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace Atend.Base.Design
{
    public class DPoleInfo
    {


        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        public DPoleInfo()
        {
        }

        private Guid code;
        public Guid Code
        {
            get { return code; }
            set { code = value; }
        }

        private Guid nodeCode;
        public Guid NodeCode
        {
            get { return nodeCode; }
            set { nodeCode = value; }
        }

        private int halterCount;
        public int HalterCount
        {
            get { return halterCount; }
            set { halterCount = value; }
        }

        private int halterType;
        public int HalterType
        {
            get { return halterType; }
            set { halterType = value; }
        }

        private byte factor;
        public byte Factor
        {
            get { return factor; }
            set { factor = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


        //MOUSAVI
        public bool AcessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_PoleInfo_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            //ed.WriteMessage("field one assigned\n");
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new OleDbParameter("iHalterType", HalterType));
            command.Parameters.Add(new OleDbParameter("iFactor", Factor));
            try
            {
                command.ExecuteNonQuery();
                //ed.WriteMessage("DPole info AcessInsert done\n");
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPoleInfo.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
            return true;


        }

        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {

            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_PoleInfo_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iHalterCount", HalterCount));
            command.Parameters.Add(new OleDbParameter("iHalterType", HalterType));
                       command.Parameters.Add(new OleDbParameter("iFactor", Factor));
        
            try
            {
                //connection.Open();
                command.ExecuteNonQuery();
                //ed.writeMessage(string.Format("DPoleInfo.Update Done. \n"));
                //connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPoleInfo.Update : {0} \n", ex1.Message));
                //connection.Close();
                return false;
            }
            return true;


        }

        public static bool AccessDelete(Guid NodeCode)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Pole_Delete", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPoleInfo.AccessDelete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;


        }

        //MOUASVI->drawing delete
        public static bool AccessDeleteByNodeCode(Guid NodeCode, OleDbTransaction Transaction, OleDbConnection Connection)
        {

            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.WriteMessage("D_POLEINFO NODE CODE : {0} \n", NodeCode);
            OleDbConnection connection = Connection;
            OleDbCommand command = new OleDbCommand("D_PoleInfo_DeleteByNodeCode", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = Transaction;

            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPoleInfo.AccessDelete : {0} \n", ex1.Message));
                return false;
            }
            return true;


        }

        //public static bool AccessDelete(Guid Code, OleDbTransaction Transaction, OleDbConnection Connection)
        //{

        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

        //    ed.WriteMessage("D_POLEINFO CODE : {0} \n",Code);
        //    OleDbConnection connection = Connection;
        //    OleDbCommand command = new OleDbCommand("D_PoleInfo_Delete", connection);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.Transaction = Transaction;

        //    command.Parameters.Add(new OleDbParameter("iCode", Code));
        //    try
        //    {

        //        command.ExecuteNonQuery();
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        ed.WriteMessage(string.Format("Error DPoleInfo.AccessDelete : {0} \n", ex1.Message));
        //        return false;
        //    }
        //    return true;


        //}

        //MOUSAVI->AutoPoleInstallation

        public static DPoleInfo AccessSelectByNodeCode(Guid NodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            DPoleInfo PoleInfo = new DPoleInfo();
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_PoleInfo_SelectByNodeCode", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            //try
            //{
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                PoleInfo.code = new Guid(reader["Code"].ToString());
                PoleInfo.NodeCode = new Guid(reader["NodeCode"].ToString());
                PoleInfo.HalterCount = Convert.ToInt32(reader["HalterCount"]);
                PoleInfo.HalterType = Convert.ToInt32(reader["HalterType"]);
                PoleInfo.Factor = Convert.ToByte(reader["Factor"]);
            }
            else
            {
                PoleInfo.Code = Guid.Empty;
            }

            reader.Close();
            connection.Close();

            //}
            //catch (System.Exception ex1)
            //{
            //    ed.WriteMessage(string.Format("Error DPoleInfo.AccessSelectByNodeCode =: {0} \n", ex1.Message));
            //    connection.Close();
            //}
            return PoleInfo;


        }



    }
}
