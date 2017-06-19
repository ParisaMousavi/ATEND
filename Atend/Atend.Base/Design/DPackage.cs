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
    public class DPackage
    {

        public DPackage()
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

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private Guid parentCode;

        public Guid ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private int productCode;

        public int ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private int isExistance;

        public int IsExistance
        {
            get { return isExistance; }
            set { isExistance = value; }
        }

        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int loadCode;

        public int LoadCode
        {
            get { return loadCode; }
            set { loadCode = value; }
        }

        private int projectCode;

        public int ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Access Part ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        public bool AccessInsert()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Package_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;


            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iCount", count));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessInsert : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //MOUSAVI
        public bool AccessInsert(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Package_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;


            Code = Guid.NewGuid();
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iCount", count));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            //ed.WriteMessage("~~Type:{0} ~~\n",Type);
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));

            try
            {
                command.ExecuteScalar();
                ed.WriteMessage(string.Format("DPackage.AccessInsert Done. \n"));
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessInsert : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //Hatami
        public bool AccessUpdate()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Package_Update", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iCount", Count));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessUpdate : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //MEDHAT
        public bool AccessUpdate(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Package_Update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            command.Parameters.Add(new OleDbParameter("iCount", Count));
            command.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            command.Parameters.Add(new OleDbParameter("iType", Type));
            command.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iNumber", Number));
            command.Parameters.Add(new OleDbParameter("iLoadCode", LoadCode));
            command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessUpdate(transaction) : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //MEDHAT
        public bool AccessUpdateProjectCodeAndIsExistance(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Package_UpdateProjectCodeAndIsExistance", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessUpdateProjectCodeAndIsExistance(transaction) : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //MEDHAT
        public bool AccessUpdateSubMiddleJackPanel(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbCommand command = new OleDbCommand("D_Package_UpdateSubMiddleJackPanel", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = _transaction;

            command.Parameters.Add(new OleDbParameter("iCode", Code));
            command.Parameters.Add(new OleDbParameter("iIsExistance", IsExistance));
            command.Parameters.Add(new OleDbParameter("iProjectCode", ProjectCode));
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessUpdateSubMiddleJackPanel(transaction) : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        //MOUSAVI
        public static bool AccessDelete(Guid Code)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand command = new OleDbCommand("D_Package_Delete", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessDelete : {0} \n", ex1.Message));
                connection.Close();
                return false;
            }
            return true;
        }

        //MOUSAVI->drawing delete
        public static bool AccessDeleteSpecial(Guid Code, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            try
            {
                //ed.WriteMessage("1 : {0} \n", Code);
                DataTable _DPackages = Atend.Base.Design.DPackage.AccessSelectByParentCode(Code, _transaction, _connection);

                if (!AccessDelete(Code, _transaction, _connection))
                {
                    throw new System.Exception("Parent AccessDelete failed");
                }

                if (_DPackages.Rows.Count == 0)
                {
                    //ed.WriteMessage("No records found \n");
                    if (!AccessDelete(Code, _transaction, _connection))
                    {
                        throw new System.Exception("AccessDelete failed");
                    }
                }
                else
                {
                    //ed.WriteMessage("some records found \n");
                    foreach (DataRow dr in _DPackages.Rows)
                    {
                        if (!AccessDeleteSpecial(new Guid(dr["Code"].ToString()), _transaction, _connection))
                        {
                            throw new System.Exception("loop failed");
                        }
                    }
                }
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessDelete : {0} \n", ex1.Message));
                return false;
            }
            return true;

        }

        //MOUSAVI->drawing delete
        public static bool AccessDelete(Guid Code, OleDbTransaction _Transaction, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbCommand command = new OleDbCommand("D_Package_Delete", connection);
            command.Transaction = _Transaction;

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new OleDbParameter("iCode", Code));
            try
            {
                //ed.WriteMessage("DpackageCode:{0}\n", Code);
                Atend.Base.Design.DPackage _DPackage = Atend.Base.Design.DPackage.AccessSelectByCode(Code, _Transaction, _Connection);
                if (_DPackage.Code != Guid.Empty)
                {
                    switch (((Atend.Control.Enum.ProductType)_DPackage.Type))
                    {
                        case Atend.Control.Enum.ProductType.Pole:
                            //ed.WriteMessage("~~~~~~~~~~~ pole found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DPoleInfo.AccessDeleteByNodeCode(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DPoleInfo.AccessDelete failed");
                            }
                            if (!Atend.Base.Design.DNode.AccessDelete(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DNode.AccessDelete failed");
                            }
                            break;
                        case Atend.Control.Enum.ProductType.Consol:
                            //ed.WriteMessage("~~~~~~~~~~~ CONSOL found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DConsol.AccessDelete(_DPackage.Code, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DConsol.AccessDelete failed");
                            }
                            break;
                        case Atend.Control.Enum.ProductType.GroundPost:
                            //ed.WriteMessage("~~~~~~~~~~~ GROUND POST found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DPost.AccessDelete(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DPost.AccessDelete failed");
                            }

                            break;
                        case Atend.Control.Enum.ProductType.AirPost:
                            //ed.WriteMessage("~~~~~~~~~~~ AIRPOST found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DPost.AccessDelete(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DPost.AccessDelete failed");
                            }

                            break;
                        case Atend.Control.Enum.ProductType.MiddleJackPanel:
                            //ed.WriteMessage("~~~~~~~~~~~ MJ found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DCellStatus.AccessDeleteByJackPanelCode(_DPackage.Code, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DCellStatus.AccessDeleteByJackPanelCode failed");
                            }

                            break;
                        case Atend.Control.Enum.ProductType.PoleTip:
                            //ed.WriteMessage("~~~~~~~~~~~ poleTIP found ~~~~~~~~~~~~~~~\n");
                            if (!Atend.Base.Design.DPoleInfo.AccessDeleteByNodeCode(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DPoleInfo.AccessDeleteTip failed");
                            }
                            if (!Atend.Base.Design.DNode.AccessDelete(_DPackage.NodeCode, _Transaction, _Connection))
                            {
                                throw new System.Exception("~~~***~~~DNode.AccessDeleteTip failed");
                            }
                            break;
                    }
                }
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex1)
            {
                ed.WriteMessage(string.Format("Error DPackage.AccessDelete : {0} \n", ex1.Message));
                return false;
            }
            return true;
        }

        public static DPackage AccessSelectByNodeCodeType(Guid NodeCode, int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByNodeCodeType", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));

            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            ed.WriteMessage("NodeCode:{0}\n", NodeCode);
            DPackage package = new DPackage();
            if (reader.Read())
            {
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
            }
            else
            {
                package.ProductCode = -1;
            }
            Connection.Close();
            reader.Close();
            return package;

        }

        public static DPackage AccessSelectByProductCodeType(int ProductCode, int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByProductCodeAndType", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new OleDbParameter("iProductCode", ProductCode));
            sqlCommand.Parameters.Add(new OleDbParameter("iType", Type));

            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
            }
            else
            {
                package.ProductCode = -1;
            }
            Connection.Close();
            reader.Close();
            return package;

        }

        //MOUSAVI->AutoPoleInstallation,Delete Equips ,
        public static DPackage AccessSelectByNodeCode(Guid NodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByNodeCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));

            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
            }
            else
            {
                package.code = Guid.Empty;
            }
            //ed.WriteMessage("ProjectCode={0}\n", package.ProjectCode);
            Connection.Close();
            reader.Close();
            return package;

        }

        //status report
        public static DPackage AccessSelectByNodeCode(Guid NodeCode, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByNodeCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));

            //Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
            }
            else
            {
                package.code = Guid.Empty;
            }
            //ed.WriteMessage("ProjectCode={0}\n", package.ProjectCode);
            //Connection.Close();
            reader.Close();
            return package;

        }

        //AcDrawGround   //AcDrawKhazan
        public static DPackage AccessSelectByNodeCode(Guid NodeCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByNodeCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new OleDbParameter("iNodeCode", NodeCode));
            sqlCommand.Transaction = _transaction;

            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
            }
            else
            {
                package.code = Guid.Empty;
            }
            reader.Close();
            return package;

        }

        //AcDrawGroundPostForShield
        public static DPackage AccessSelectByCode(Guid NodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("NodeCode={0}\n", NodeCode.ToString());
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", NodeCode));

            Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                //ed.WriteMessage("^^^^\n");
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"].ToString());
            }
            else
            {
                //ed.WriteMessage("***\n");
                package.code = Guid.Empty;
            }
            reader.Close();

            Connection.Close();
            return package;
        }

        //CALCULATION HATAMI // status report
        public static DPackage AccessSelectByCode(Guid NodeCode, OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _Connection;
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //ed.WriteMessage("NodeCode={0}\n", NodeCode.ToString());
            sqlCommand.Parameters.Add(new OleDbParameter("iCode", NodeCode));

            //Connection.Open();
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                //ed.WriteMessage("^^^^\n");
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"].ToString());
            }
            else
            {
                //ed.WriteMessage("***\n");
                package.code = Guid.Empty;
            }
            reader.Close();

            // Connection.Close();
            return package;
        }

        //MOUSAVI->drawing delete
        public static DPackage AccessSelectByCode(Guid NodeCode, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection Connection = _connection;
            OleDbCommand sqlCommand = new OleDbCommand("D_Package_SelectByCode", Connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = _transaction;


            sqlCommand.Parameters.Add(new OleDbParameter("iCode", NodeCode));
            OleDbDataReader reader = sqlCommand.ExecuteReader();
            DPackage package = new DPackage();
            if (reader.Read())
            {
                //ed.WriteMessage("^^^^\n");
                package.Code = new Guid(reader["Code"].ToString());
                package.Count = Convert.ToInt32(reader["Count"]);
                package.NodeCode = new Guid(reader["NodeCode"].ToString());
                package.ParentCode = new Guid(reader["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(reader["ProductCode"]);
                package.Type = Convert.ToInt32(reader["Type"]);
                package.IsExistance = Convert.ToInt32(reader["IsExistance"]);
                package.Number = reader["Number"].ToString();
                package.LoadCode = Convert.ToInt32(reader["LoadCode"].ToString());
                package.ProjectCode = Convert.ToInt32(reader["ProjectCode"].ToString());
            }
            else
            {
                //ed.WriteMessage("***\n");
                package.code = Guid.Empty;
            }
            reader.Close();
            return package;
        }

        public static DataTable AccessSelectByParentCodeAndType(Guid ParentCode, int Type)
        {
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //CALCULATION HATAMI // status report
        public static DataTable AccessSelectByParentCodeAndType(Guid ParentCode, int Type, OleDbConnection _Connection)
        {
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        public static DataTable AccessSelectByParentCodeAndType(Guid ParentCode, int Type, OleDbTransaction _transaction, OleDbConnection _connection)
        {
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCodeType", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = _transaction;

            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        public static DataTable AccessSelectByParentCode(Guid ParentCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);

            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCode", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);

            return dsPackage.Tables[0];
        }

        //StatusReport
        public static DataTable AccessSelectByParentCode(Guid ParentCode, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = _connection;

            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCode", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);

            return dsPackage.Tables[0];
        }

        //MEDHAT 
        public static DataTable AccessSelectByParentCodeForConsol(Guid ParentCode, int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_package_SelectByParentCodeCONSOL", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));

            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);

            return dsPackage.Tables[0];
        }

        public static DataTable AccessSelectByParentCode(Guid ParentCode, OleDbTransaction Transaction, OleDbConnection Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByParentCode", connection);
            adapter.SelectCommand.Transaction = Transaction;

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iParentCode", ParentCode));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //ASHKTORAB
        public static DataTable AccessSelectByTypeDistinct(int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByType", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //HATAMI
        public static DataTable AccessSelectByType(int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectByType", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //HATAMI
        public static DataTable AccessSelectCalamp(int Type)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectCalamp", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        public static DataTable AccessSelectCalamp(int Type, OleDbConnection aConnection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = aConnection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectCalamp", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add(new OleDbParameter("iType", Type));
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //Hatami
        public static DataTable AccessSelectAll()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectAll", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        //calculation HATAMI
        public static DataTable AccessSelectAll(OleDbConnection _Connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _Connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_SelectAll", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];
        }

        public static DataTable AccessAllWorkOrders()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = new OleDbConnection(Atend.Control.ConnectionString.AccessCnString);
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_WorkOrders", connection);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];

        }

        //frmSaveDesignServer
        public static DataTable AccessAllWorkOrders(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            OleDbConnection connection = _connection;
            OleDbDataAdapter adapter = new OleDbDataAdapter("D_Package_WorkOrders", connection);
            adapter.SelectCommand.Transaction = _transaction;

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dsPackage = new DataSet();
            adapter.Fill(dsPackage);
            return dsPackage.Tables[0];

        }

        public static DataTable AccessWorkOrders()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("SELECT WORK ORDER\n");
            DataTable Dorders = Atend.Base.Design.DPackage.AccessAllWorkOrders();
            DataTable Borders = Atend.Base.Design.DBranch.AccessAllWorkOrders();

            foreach (DataRow dr in Borders.Rows)
            {
                DataRow[] drs = Dorders.Select("WorkOrderCode=" + dr["WorkOrderCode"].ToString());
                if (drs.Length == 0)
                {
                    DataRow newdr = Dorders.NewRow();
                    newdr["WorkOrderCode"] = dr["WorkOrderCode"];
                    Dorders.Rows.Add(newdr);
                }

            }

            foreach (DataRow dr in Dorders.Rows)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("WO:{0}\n", dr["WorkOrderCode"]);
            }


            return Dorders;
        }

        //frmSaveDesignServer
        public static DataTable AccessWorkOrders(OleDbTransaction _transaction, OleDbConnection _connection)
        {
            DataTable Dorders = Atend.Base.Design.DPackage.AccessAllWorkOrders(_transaction, _connection);
            DataTable Borders = Atend.Base.Design.DBranch.AccessAllWorkOrders(_transaction, _connection);
            foreach (DataRow dr in Borders.Rows)
            {
                DataRow[] drs = Dorders.Select("WorkOrderCode=" + dr["WorkOrderCode"].ToString());
                if (drs.Length == 0)
                {
                    DataRow newdr = Dorders.NewRow();
                    newdr["WorkOrderCode"] = dr["WorkOrderCode"];
                    Dorders.Rows.Add(newdr);
                }
            }

            foreach (DataRow dr in Dorders.Rows)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("WO:{0}\n", dr["WorkOrderCode"]);
            }


            return Dorders;
        }

        //****************************************Access To MEMORY Fo Calculation********************

        public static DPackage AccessSelectByCode(DataTable dtPackage, Guid Code)
        {
            DPackage package = new DPackage();
            DataRow[] dr = dtPackage.Select(string.Format(" Code='{0}'", Code.ToString()));
            if (dr.Length != 0)
            {
                package.Code = new Guid(dr[0]["Code"].ToString());
                package.Count = Convert.ToInt32(dr[0]["Count"]);
                package.NodeCode = new Guid(dr[0]["NodeCode"].ToString());
                package.ParentCode = new Guid(dr[0]["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(dr[0]["ProductCode"]);
                package.Type = Convert.ToInt32(dr[0]["Type"]);
                package.IsExistance = Convert.ToInt32(dr[0]["IsExistance"]);
                package.Number = dr[0]["Number"].ToString();
                package.LoadCode = Convert.ToInt32(dr[0]["LoadCode"].ToString());
                package.ProjectCode = Convert.ToInt32(dr[0]["ProjectCode"].ToString());
            }
            else
            {
                package.Code = Guid.Empty;
            }
            return package;

        }

        public static DPackage AccessSelectByNodeCode(DataTable dtPack, Guid NodeCode)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            DataRow[] dr = dtPack.Select(string.Format("NodeCode='{0}'", NodeCode.ToString()));
            DPackage package = new DPackage();
            if (dr.Length != 0)
            {
                package.Code = new Guid(dr[0]["Code"].ToString());
                package.Count = Convert.ToInt32(dr[0]["Count"]);
                package.NodeCode = new Guid(dr[0]["NodeCode"].ToString());
                package.ParentCode = new Guid(dr[0]["ParentCode"].ToString());
                package.ProductCode = Convert.ToInt32(dr[0]["ProductCode"]);
                package.ProjectCode = Convert.ToInt32(dr[0]["ProjectCode"]);
                package.Type = Convert.ToInt32(dr[0]["Type"]);
                package.IsExistance = Convert.ToInt32(dr[0]["IsExistance"]);
                package.Number = dr[0]["Number"].ToString();
                package.LoadCode = Convert.ToInt32(dr[0]["LoadCode"].ToString());
            }
            else
            {
                package.code = Guid.Empty;
            }
            //ed.WriteMessage("ProjectCode={0}\n", package.ProjectCode);
            return package;

        }

    }
}
